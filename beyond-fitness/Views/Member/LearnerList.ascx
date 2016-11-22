﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<% if (_items != null && _items.Count() > 0)
    { %>

<table id="dt_basic" class="table table-striped table-bordered table-hover" width="100%">
    <thead>
        <tr>
            <th>#</th>
            <th data-class="expand"><i class="fa fa-fw fa-user text-muted hidden-md hidden-sm hidden-xs"></i>姓名</th>
            <th data-hide="phone"><i class="fa fa-fw fa-phone text-muted hidden-md hidden-sm hidden-xs"></i>電話</th>
            <th data-hide="phone"><i class="fa fa-fw fa-envelope text-muted hidden-md hidden-sm hidden-xs"></i>Email Address</th>
            <th data-hide="phone">編號</th>
            <th data-hide="phone">年齡</th>
            <th data-hide="phone">性別</th>
            <th data-hide="phone">購買堂數</th>
            <th data-hide="phone">剩餘堂數</th>
            <th>功能</th>
        </tr>
    </thead>
    <tbody>
        <%  int idx = 0;
            int? totalLessons;
            int? attended;
            foreach (var item in _items)
            {
                idx++;%>
        <tr>
            <td><%= idx %></td>
            <td><%= item.RealName %></td>
            <td><%= item.Phone %></td>
            <td><%= item.LevelID==(int)Naming.MemberStatusDefinition.ReadyToRegister ? "尚未註冊" :  item.PID %></td>
            <td><%= item.MemberCode %></td>
            <td><%= item.YearsOld() %></td>
            <td><%= item.UserProfileExtension!=null ? item.UserProfileExtension.Gender=="M" ? "男" : "女" : null %></td>
            <td><%  var items = item.RegisterLesson.Where(l => l.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練);
                    totalLessons = items.Sum(l => l.Lessons);
                    Writer.Write(totalLessons); %></td>
            <td><%  attended = 
                        items.Sum(l => l.LessonTime.Count(/*t => t.LessonAttendance != null*/))
                        + items.Sum(l => l.AttendedLessons)
                        + items.Where(c=>c.RegisterGroupID.HasValue)
                            .Sum(c=>c.GroupingLesson.LessonTime
                                .Where(l=>l.RegisterID!=c.RegisterID)
                                .Count(/*l=>l.LessonAttendance!= null*/));
                    Writer.Write(totalLessons - attended); %></td>
            <td>
                <div class="btn-group dropup">
                    <button class="btn bg-color-blueLight" data-toggle="dropdown">
                        請選擇功能
                    </button>
                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu">
                        <% if (item.LevelID == (int)Naming.MemberStatusDefinition.Deleted)
                            {   %>
                        <li>
                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/EnableUser/") + item.UID %>"><i class="fa fa-fw fa fa-check-square" aria-hidden="true"></i>啟用</a>
                        </li>
                        <li class="divider"></li>
                        <%  }
                            else
                            {
                                if (_userProfile.IsAuthorizedSysAdmin())
                                { %>
                                    <li>
                                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/AddLessons/") + item.UID %>"><i class="fa fa-fw fa fa-calendar-o" aria-hidden="true"></i>維護上課數</a>
                                    </li>
                        <%      } %>
                        <li>
                            <a href="<%= VirtualPathUtility.ToAbsolute("~/member/PDQ/") + item.UID %>"><i class="fa fa-fw fa fa-street-view" aria-hidden="true"></i>問卷調查表</a>
                        </li>
                        <li>
                            <a href="<%= Url.Action("LearnerFitness","Activity",new { uid = item.UID }) %>"><i class="fa fa-fw fa fa-heart" aria-hidden="true"></i>檢測體能</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a onclick='deleteLearner(<%= item.UID %>);'><i class="fa fa-fw fa fa-trash-o" aria-hidden="true"></i>刪除資料</a>
                        </li>
                        <%  } %>
                        <li>
                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/EditLearner/") + item.UID %>"><i class="fa fa-fw fa fa-edit" aria-hidden="true"></i>修改資料</a>
                        </li>
                        <li>
                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ShowLearner/") + item.UID %>"><i class="fa fa-fw fa fa-eye" aria-hidden="true"></i>檢視資料</a>
                        </li>
                    </ul>
                </div>
                <%--<a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Member/GroupLessons/") + item.UID %>">設定團體學員 <i class="fa fa-link" aria-hidden="true"></i></a>--%>
            </td>
        </tr>
        <%  } %>
    </tbody>
</table>
<%  }
    else
    { %>
<h4>未建立資料</h4>
<%  } %>


<script runat="server">

    ModelSource<UserProfile> models;
    IEnumerable<UserProfile> _items;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = (IEnumerable<UserProfile>)this.Model;
        _userProfile = Context.GetUser();
    }




</script>

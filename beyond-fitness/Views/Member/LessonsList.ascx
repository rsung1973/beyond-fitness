<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table class="panel panel-default table">
    <tbody>
        <tr class="info">
            <th class="text-center">建檔日期</th>
            <th>課程類別</th>
            <th class="text-center">團體課程</th>
            <th class="text-center">堂數</th>
            <%  if (ViewBag.ShowOnly != true)
                { %>
                    <th class="text-center">功能</th>
            <%  } %>
        </tr>
        <% if (_items != null && _items.Count() > 0)
        {
            foreach (var item in _items)
            { %>
        <%      if (item.GroupingMemberCount > 1)
                {      
                    var currentGroups = models.GetTable<GroupingLesson>().Where(g => g.GroupID == item.RegisterGroupID)
                        .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterID != item.RegisterID), g => g.GroupID,
                            r => r.RegisterGroupID, (g, r) => r);   %>

                    <tr>
                            <td class="text-center" rowspan="<%= currentGroups.Count()+1 %>"><%= item.RegisterDate.ToString("yyyy/MM/dd") %></td>
                            <td rowspan="<%= currentGroups.Count()+1 %>"><%= item.LessonPriceType.Description + " " + item.LessonPriceType.ListPrice %></td>
                            <td class="text-center">
                                <i class="fa fa-check-circle" aria-hidden="true"></i><%= item.GroupingMemberCount %>人
                            </td>
                            <td class="text-center" rowspan="<%= currentGroups.Count()+1 %>"><%= item.Lessons %></td>
                            <%  if (ViewBag.ShowOnly != true)
                                { %>
                                    <td class="text-center" rowspan="<%= currentGroups.Count()+1 %>">
                                <%  if (item.Attended == (int)Naming.LessonStatus.準備上課
                                        && ((!item.RegisterGroupID.HasValue && item.LessonTime.Count() == 0) || (item.RegisterGroupID.HasValue && item.GroupingLesson.LessonTime.Count() == 0)))
                                    { %>
                                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/DeleteLessons/") + item.RegisterID %>"><i class="fa fa-trash-o fa-2x" aria-hidden="true"></i></a>
                                <%  } %>
                                    </td>
                            <%  } %>
                        </tr>
<%                  foreach (var g in currentGroups)
                    {
                                %>
                        <tr>
                            <td class="text-center"><%= g.UserProfile.RealName %>
                                <%  if (g.GroupingLesson.LessonTime.Count() == 0)
                                                    { %>
                                <%--<a href="<%= VirtualPathUtility.ToAbsolute("~/Member/RemoveGroupUser/") + g.RegisterID %>" class="btn btn-system btn-small">刪除<i class="fa fa-user-times" aria-hidden="true"></i></a>--%>
                                <%  } %>
                            </td>
                        </tr>
        <%          } %>
        <%      }
                else
                { %>
                        <tr>            
                    <td class="text-center"><%= item.RegisterDate.ToString("yyyy/MM/dd") %></td>
                    <td><%= item.LessonPriceType.Description + " " + item.LessonPriceType.ListPrice %></td>
                    <td class="text-center">
                    </td>
                    <td class="text-center"><%= item.Lessons %></td>
                    <%  if (ViewBag.ShowOnly != true)
                        { %>
                            <td class="text-center">
                                <%  if (item.Attended == (int)Naming.LessonStatus.準備上課
                                        && ((!item.RegisterGroupID.HasValue && item.LessonTime.Count() == 0) || (item.RegisterGroupID.HasValue && item.GroupingLesson.LessonTime.Count() == 0)))
                                    { %>
                                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/DeleteLessons/") + item.RegisterID %>"><i class="fa fa-trash-o fa-2x" aria-hidden="true"></i></a>
                                <%  } %>
                            </td>
                    <%  } %>
                </tr>
        <%      } %>
        <%  }
        }
        else
        {   %>
            <tr>
                <td colspan="<%= ViewBag.ShowOnly!=true ? 5 : 4 %>">未購買任何課程</td>
            </tr>
        <%  }   %>
    </tbody>
</table>

<script runat="server">

    IEnumerable<RegisterLesson> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = (IEnumerable<RegisterLesson>)this.Model;
    }

</script>

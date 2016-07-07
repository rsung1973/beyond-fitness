<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<p><strong>日期：<%= _model.ClassTime.Value.ToString("yyyy/MM/dd") %></strong></p>
<p><strong>時間：<%= _model.ClassTime.Value.ToString("HH:mm") %> - <%= _model.ClassTime.Value.AddMinutes(_model.DurationInMinutes.Value).ToString("HH:mm") %></strong></p>
<%  if (_assessment == true)
    { %>
        <div class="row">
            <div class="col-md-2">
                <strong>教練：</strong>
            </div>
            <div class="col-md-4">
                <% Html.RenderPartial("~/Views/Lessons/CoachSelector.ascx", new InputViewModel { Id = "coachID", Name = "coachID", DefaultValue = _model.AttendingCoach }); %>
            </div>
        </div>
<%  }
    else
    { %>
        <p><strong>教練：<%= _model.AsAttendingCoach.UserProfile.RealName %></strong></p>
<%  } %>

<h4 class="classic-title"><span>適配結果</span></h4>
<div class="panel panel-default">
    <table class="table">
        <tr class="info">
            <th>目標</th>
            <th>風格</th>
            <th>訓練水準</th>
        </tr>
        <tr>
            <td>減肥</td>
            <td>保守型</td>
            <td>初期</td>
        </tr>
    </table>
</div>
<h4 class="classic-title"><span>目前近況</span></h4>
<%  if (ViewBag.Preview == true)
    { %>
        <div class="call-action call-action-boxed call-action-style4 clearfix">
            <p><%= String.IsNullOrEmpty(_model.RegisterLesson.UserProfile.RecentStatus) ? "目前尚無任何近況哦!!" : _model.RegisterLesson.UserProfile.RecentStatus %></p>
        </div>
<%  }
    else
    { %>
        <textarea class="form-control" name="recentStatus" rows="3"><%= _model.RegisterLesson.UserProfile.RecentStatus %></textarea>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    bool? _assessment;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        _assessment = (bool?)ViewBag.Assessment;
    }

</script>

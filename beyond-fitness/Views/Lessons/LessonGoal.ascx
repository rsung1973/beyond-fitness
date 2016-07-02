<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<p><strong>教練：</strong><%= _model.AsAttendingCoach.UserProfile.RealName %></p>

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
<textarea class="form-control" name="recentStatus" rows="3"><%= _model.LessonPlan!=null ? _model.LessonPlan.RecentStatus : null %></textarea>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
    }

</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Timeline" %>
<%@ Import Namespace="WebHome.Controllers" %>
<li>
    <div class="smart-timeline-icon bg-color-greenDark">
        <i class="fa fa-bar-chart-o"></i>
    </div>
    <div class="smart-timeline-time">
        <small><%= _model.Lesson.ClassTime.Value.ToString("yyyy/MM/dd HH:mm") %>~<%= _model.Lesson.ClassTime.Value.AddMinutes(_model.Lesson.DurationInMinutes.Value).ToString("HH:mm") %></small>
    </div>
    <div class="smart-timeline-content">
        <div class="well well-sm display-inline">
            <p>恭喜您已於<strong> <%= _model.Lesson.ClassTime.Value.ToString("yyyy/MM/dd HH:mm") %>~<%= _model.Lesson.ClassTime.Value.AddMinutes(_model.Lesson.DurationInMinutes.Value).ToString("HH:mm") %> </strong>往目標邁向一大步！</p>
            <p>
                <button class="btn btn-xs btn-default" onclick="showLearnerLesson(<%= _model.Lesson.LessonID %>,true);"><i class="fa fa-anchor"></i> 檢視成果</button>
                <%  if (!_model.Lesson.LessonPlan.CommitAttendance.HasValue && _model.Lesson.ClassTime<DateTime.Today.AddDays(1))
                    { %>
                <button class="btn btn-xs btn-success" onclick="learnerAttendLesson(<%= _model.Lesson.LessonID %>);"><i class="fa fa-check"></i> 上課打卡</button>
                <%  } %>
            </p>
        </div>
    </div>
</li>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonEvent _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonEvent)this.Model;
    }

</script>

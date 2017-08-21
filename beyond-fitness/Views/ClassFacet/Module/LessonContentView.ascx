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
<%@ Import Namespace="Newtonsoft.Json" %>

<%  var prefix = "content" + DateTime.Now.Ticks + "_"; %>
<ul class="nav nav-tabs">
    <li class="active">
        <a data-toggle="tab" class="editLessonTab" href="#<%= prefix %>2"><i class="fa fa-heartbeat"></i><span>訓練內容</span></a>
    </li>
    <%  if (_item.TrainingBySelf != 1)
        { %>
    <li>
        <a data-toggle="tab" href="#<%= prefix %>4"><i class="fa fa-pie-chart"></i><span>評量指數</span></a>
    </li>
    <%  } %>
    <li class="pull-right">
        <span class="margin-top-10 display-inline">
            <a href="#" onclick="showLoading();window.location.href = '<%= Url.Action("ClassIndex","ClassFacet",new { lessonID = _item.LessonID }) %>';" class="btn btn-xs bg-color-yellow">編輯</a>
            <i class="fa fa-rss text-success"></i><span id="classTime"><%= String.Format("{0:yyyy/MM/dd H:mm}",_item.ClassTime) %>-<%= String.Format("{0:H:mm}",_item.ClassTime.Value.AddMinutes(_item.DurationInMinutes.Value)) %></span> (體能顧問：<%= _item.AsAttendingCoach.UserProfile.RealName %>)
        </span>
    </li>
</ul>
<div class="tab-content padding-top-10">
    <!-- end s1 tab pane -->
    <div class="tab-pane fade in active" id="<%= prefix %>2">
            <%--<%  Html.RenderPartial("~/Views/Lessons/Feedback/CommonFeedback.ascx", _item); %>--%>
            <%                          
                if (_item.TrainingPlan.Count > 0)
                    {
                        ViewBag.ShowOnly = true;
                        Html.RenderPartial("~/Views/Lessons/SingleTrainingExecutionPlan.ascx", _item.TrainingPlan.First().TrainingExecution);
                    } %>
    </div>
    <%  if (_item.TrainingBySelf != 1)
        { %>
    <div class="tab-pane fade" id="<%= prefix %>4">
        <%--<div class="jarviswidget" data-widget-editbutton="false" data-widget-custombutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-colorbutton="false" data-widget-fullscreenbutton="false">
            <!-- widget options:
                                                                                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
                                                                                
                                                                                data-widget-colorbutton="false" 
                                                                                data-widget-editbutton="false"
                                                                                data-widget-togglebutton="false"
                                                                                data-widget-deletebutton="false"
                                                                                data-widget-fullscreenbutton="false"
                                                                                data-widget-custombutton="false"
                                                                                data-widget-collapsed="true" 
                                                                                data-widget-sortable="false"
                                                                                
                                                                            -->
            <header>
                <span class="widget-icon"><i class="fa fa-edit"></i></span>
                <h2>身體健康指數 </h2>
            </header>

            <!-- widget div-->
            <div>
                <!-- widget content -->
                <div class="widget-body">
                    <%  Html.RenderPartial("~/Views/Activity/HealthIndex.ascx", _model.UserProfile); %>
                </div>
                <!-- end widget content -->
            </div>
            <!-- end widget div -->

        </div>--%>
        <%  
            var assessment = _item.LessonFitnessAssessment.Where(f => f.UID == _model.UID).FirstOrDefault();
            ViewBag.Index = 0;
            Html.RenderPartial("~/Views/Lessons/LessonLearnerAssessment.ascx", assessment); %>
    </div>
    <%  } %>
</div>

<%--<%  Html.RenderPartial("~/Views/Shared/MorrisGraphView.ascx"); %>--%>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    RegisterLesson _model;
    LessonTime _item;
    LessonPlan _plan;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterLesson)this.Model;
        _item = (LessonTime)ViewBag.LessonTime;
        _plan = _item.LessonPlan ?? new LessonPlan { };
        ViewBag.CloneLesson = true;
    }

</script>

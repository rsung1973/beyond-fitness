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

<%  var prefix = "content" + _model.RegisterID + "_"; %>
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
    <li>
        <a data-toggle="tab" href="#<%= prefix %>5"><i class="fa fa-trophy"></i><span>競賽</span> 
            <%  var contestant = _model.UserProfile.ExerciseGameContestant;
                if (contestant != null && contestant.ExerciseGamePersonalRank != null)
                { %>
            <span class="badge bg-color-red txt-color-white">No. <%= contestant.ExerciseGamePersonalRank.Rank %></span>
            <%  } %>
        </a>
    </li>
    <li class="pull-right">
        <span class="margin-top-10 display-inline">
            <i class="fa fa-rss text-success"></i><span id="classTime"><%= String.Format("{0:yyyy/MM/dd H:mm}",_item.ClassTime) %>-<%= String.Format("{0:H:mm}",_item.ClassTime.Value.AddMinutes(_item.DurationInMinutes.Value)) %></span> (體能顧問：<%= _item.AsAttendingCoach.UserProfile.FullName() %>)
        </span>
    </li>
</ul>
<div class="tab-content padding-top-10">
    <!-- end s1 tab pane -->
    <div class="tab-pane fade in active" id="<%= prefix %>2">
        <%  if (ViewBag.HasContent != true)
            { %>
        <div id="editLesson">
            <%--<%  Html.RenderPartial("~/Views/Lessons/Feedback/CommonFeedback.ascx", _item); %>--%>
            <form action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/UpdateTrainingItemSequence/") + _item.LessonID %>" method="post" id="updateSeq">
                <% Html.RenderAction("SingleTrainingExecutionPlan", "Lessons", new { LessonID = _item.LessonID }); %>
            </form>
        </div>
        <%  } %>
    </div>
    <%  if (_item.TrainingBySelf != 1)
        { %>
    <div class="tab-pane fade" id="<%= prefix %>4">
        <%  var assessment = _item.LessonFitnessAssessment.Where(f => f.UID == _model.UID).FirstOrDefault();
            Html.RenderPartial("~/Views/Lessons/LessonLearnerAssessment.ascx", assessment); %>
    </div>
    <%  } %>
    <div class="tab-pane fade in" id="<%= prefix %>5">
        <%  Html.RenderPartial("~/Views/ExerciseGame/Module/GameWidgetGrid.ascx", _model.UserProfile); %>
    </div>
</div>

<%  Html.RenderPartial("~/Views/Shared/MorrisGraphView.ascx"); %>
<% Html.RenderPartial("~/Views/Shared/EasyPieView.ascx"); %>
<%  Html.RenderPartial("~/Views/Lessons/Module/TrainingItemScript.ascx"); %>

<script>
    function commitGameStatus(status) {
        var event = event || window.event;
        var $container = $('#<%= prefix %>5');
        showLoading();
        $.post('<%= Url.Action("CommitGameStatus","ExerciseGame",new { _model.UID }) %>', { 'status': status }, function (data) {
            hideLoading();
            $container.html(data);
        });
    }

    function loadExerciseResult() {
        var $container = $('#<%= prefix %>5');
        showLoading();
        $.post('<%= Url.Action("GameIndex", "ExerciseGame", new { _model.UID }) %>', {}, function (data) {
            hideLoading();
            $container.html(data);
        });
    }

    function deleteExerciseResult(testID) {
        var event = event || window.event;
        var $tr = $(event.target).closest('tr');
        if (confirm('確定刪除?')) {
            var event = event || window.event;
            showLoading();
            $.post('<%= Url.Action("DeleteExerciseResult","ExerciseGame") %>', { 'testID': testID }, function (data) {
                hideLoading();
                if ($.isPlainObject(data)) {
                    if (data.result) {
                        $(event.target).closest('tr').remove();
                    } else {
                        alert(data.message);
                    }
                } else {
                    $(data).appendTo($('body')).remove();
                }
            });
        }
    }
</script>

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

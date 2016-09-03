<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<script>

    var $global = {};

    function showRecentLessons(uid, lessonID) {
        $.post('<%= Url.Action("LearnerRecentLessons","Report") %>', { 'uid': uid, 'lessonID': lessonID,'cloneLesson': <%= ViewBag.CloneLesson==true ? "true" : "false"  %> }, function (data) {
            $(data).appendTo($('#content'));
        });
    }

    function makeTrainingPlan(lessonID) {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/MakeTrainingPlan") %>');
        $('<input type="hidden"/>')
            .prop('name', 'lessonID').prop('value', lessonID).appendTo($form);
        startLoading();
        $form.submit();
    }

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

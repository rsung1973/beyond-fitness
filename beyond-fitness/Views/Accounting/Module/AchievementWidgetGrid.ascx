﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<section id="widget-grid" class="">
    <!-- row -->
    <div class="row">
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false">
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
                    <span class="widget-icon"><i class="fa fa-search txt-color-white"></i></span>
                    <h2>查詢條件 </h2>
                    <!-- <div class="widget-toolbar">
                           add: non-hidden - to disable auto hide
                           
                           </div>-->
                </header>
                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->
                    <!-- widget content -->
                    <div class="widget-body bg-color-darken txt-color-white no-padding">
                        <form action="" method="post" id="queryForm" class="smart-form">
                            <fieldset>
                                <div class="row">
                                    <section class="col col-xs-12 col-sm-4 col-md-4">
                                        <label class="label">請選擇查詢月份</label>
                                        <label class="input input-group">
                                            <i class="icon-append far fa-calendar-alt"></i>
                                            <input type="text" name="AchievementYearMonthFrom" readonly="readonly" class="form-control date form_month" data-date-format="yyyy/mm" placeholder="請輸入查詢起日" value='<%= _viewModel.AchievementYearMonthFrom %>' />
                                            <input type="hidden" name="AchievementYearMonthTo" value='' />
                                        </label>
                                    </section>
                                    <section class="col col-xs-12 col-sm-4 col-md-4">
                                        <label class="label">依體能顧問查詢</label>
                                        <label class="select">
                                            <select name="CoachID" class="input">
                                                <%  if (_profile.IsAssistant() || _profile.IsManager() || _profile.IsAccounting() || _profile.IsOfficer())
                                                    { %>
                                                <option value="">全部</option>
                                                <%  } %>
                                                <%  if (_profile.IsAssistant()|| _profile.IsAccounting() || _profile.IsOfficer())
                                                    {
                                                        Html.RenderPartial("~/Views/SystemInfo/ServingCoachOptions.cshtml", models.GetTable<ServingCoach>());
                                                    }
                                                    else if (_profile.IsManager())
                                                    {
                                                        Html.RenderPartial("~/Views/SystemInfo/ServingCoachOptions.cshtml", _profile.GetServingCoachInSameStore(models));
                                                    }
                                                    else
                                                    {
                                                        Html.RenderPartial("~/Views/SystemInfo/ServingCoachOptions.cshtml", models.GetTable<ServingCoach>().Where(c => c.CoachID == _profile.UID));
                                                    } %>
                                            </select>
                                            <i class="icon-append far fa-keyboard"></i>
                                        </label>
                                    </section>
                                    <%  if (_profile.IsAssistant() || _profile.IsAccounting() || _profile.IsOfficer())
                                    { %>
                                    <section class="col col-xs-12 col-sm-4 col-md-4">
                                        <label class="label">或依分店查詢</label>
                                        <label class="select">
                                            <select name="BranchID">
                                                <option value="">全部</option>
                                                <%  Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.cshtml", model: -1);    %>
                                            </select>
                                            <i class="icon-append far fa-keyboard"></i>
                                        </label>
                                    </section>
                                    <%  }
                                    else
                                    {
                                        //ViewBag.DataItems = models.GetTable<CoachWorkplace>().Where(w => w.CoachID == _profile.UID)
                                            //.Select(w => w.BranchStore);
                                        //Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.cshtml", model: -1);
                                    }   %>
                                </div>
                            </fieldset>
                            <%--<fieldset>
                                <label class="label"><i class="fa fa-tags"></i>更多查詢條件</label>
                                <div class="row">
                                    <section class="col col-6">
                                        <label class="label">請選擇查詢月份</label>
                                        <label class="input input-group">
                                            <i class="icon-append far fa-calendar-alt"></i>
                                            <input type="text" name="AchievementYearMonthFrom" readonly="readonly" class="form-control date form_month" data-date-format="yyyy/mm" placeholder="請輸入查詢起日" value='<%= _viewModel.AchievementYearMonthFrom %>' />
                                            <input type="hidden" name="AchievementYearMonthTo" value='' />
                                        </label>
                                    </section>
                                    <section class="col col-6">
                                        <label class="label">請選擇查詢查詢迄月</label>
                                        <label class="input input-group">
                                            <i class="icon-append far fa-calendar-alt"></i>
                                            <input type="text" name="AchievementYearMonthTo" readonly="readonly" class="form-control date form_month" data-date-format="yyyy/mm" placeholder="請輸入查詢迄日" value='<%= _viewModel.AchievementYearMonthTo %>' />
                                        </label>
                                    </section>
                                </div>
                            </fieldset>--%>
                            <footer>
                                <button onclick="inquireAchievement();" type="button" name="btnSend" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                                <button type="reset" name="cancel" class="btn btn-default">
                                    清除 <i class="fa fa-undo" aria-hidden="true"></i>
                                </button>
                            </footer>
<%--                            <div class="message">
                                <i class="fa fa-check fa-lg"></i>
                                <p>
                                    Your comment was successfully added!
                                </p>
                            </div>--%>
                        </form>
                    </div>
                    <!-- end widget content -->
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->
    </div>
    <!-- end row -->
    <!-- row -->
    <div class="row">
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-6 col-md-7 col-lg-7">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
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
                    <span class="widget-icon"><i class="fa fa-table"></i></span>
                    <h2>上課統計表 - 人員 <span class="achievement"></span>
                    </h2>
                    <div class="widget-toolbar">
                    </div>
                </header>
                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->
                    <!-- widget content -->
                    <div class="widget-body no-padding" id="achievementList">
                        <%  Html.RenderPartial("~/Views/Accounting/Module/LessonAttendanceAchievementList.ascx", models.GetTable<V_Tuition>().Where(c => false)); %>
                    </div>
                    <!-- end widget content -->
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-6 col-md-5 col-lg-5">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-1" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
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
                    <span class="widget-icon"><i class="fa fa-table"></i></span>
                    <h2>業績統計表 - 人員 
                        <span class="achievement"></span>
                    </h2>
                    <div class="widget-toolbar">
                        <a onclick="downloadAchievement();" id="btnDownloadAchievement" style="display: none;" class="btn btn-primary"><i class="fa fa-fw fa-cloud-download-alt"></i>下載檔案</a>
                    </div>
                </header>
                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->
                    <!-- widget content -->
                    <div class="widget-body no-padding" id="tuitionList">
                        <%  Html.RenderPartial("~/Views/Accounting/Module/TuitionAchievementList.ascx", models.GetTable<TuitionAchievement>().Where(c => false)); %>
                    </div>
                    <!-- end widget content -->
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->
    </div>
    <!-- end row -->
</section>

<script>
    function inquireAchievement() {
        var event = event || window.event;
        var $form = $(event.target).closest('form');
        var formData = $form.serializeObject();
        $('#btnDownloadAchievement').css('display', 'none');
        $('#btnDownloadTuition').css('display', 'none');
        clearErrors();
        showLoading();
        var downloadCount = 0;
        $('#achievementList').load('<%= Url.Action("InquireAchievement","Accounting") %>', formData, function (data) {
            downloadCount++;
            if (downloadCount == 2)
                hideLoading();
        });

       $('#tuitionList').load('<%= Url.Action("InquireTuitionAchievement","Accounting") %>', formData, function (data) {
            downloadCount++;
            if (downloadCount == 2)
                hideLoading();
        });

    }

    function downloadAchievement() {
        $('#queryForm').launchDownload('<%= Url.Action("CreateAchievementXlsx","Accounting") %>');
    }

    function showAttendanceAchievement(coachID) {
        var formData = $('#queryForm').serializeObject();
        formData.CoachID = coachID;
        showLoading();
        $.post('<%= Url.Action("ListAttendanceAchievement","Accounting") %>', formData, function (data) {
            hideLoading();
            $(data).appendTo($('body'));
        });
    }

    function showTuitionAchievement(coachID) {
        var formData = $('#queryForm').serializeObject();
        formData.CoachID = coachID;
        showLoading();
        $.post('<%= Url.Action("ListTuitionAchievement","Accounting") %>', formData, function (data) {
            hideLoading();
            $(data).appendTo($('body'));
        });
    }



</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _profile;
    AchievementQueryViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _profile = Context.GetUser();
        _viewModel = (AchievementQueryViewModel)ViewBag.ViewModel;
    }

</script>

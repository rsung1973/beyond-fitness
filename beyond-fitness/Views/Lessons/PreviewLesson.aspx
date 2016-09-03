﻿<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-eye"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>人員管理></li>
            <li>VIP管理</li>
            <li>檢視VIP</li>
        </ol>
        <!-- end breadcrumb -->

        <!-- You can also add more buttons to the
                ribbon for further usability

                Example below:

                <span class="ribbon-button-alignment pull-right">
                <span id="search" class="btn btn-ribbon hidden-xs" data-title="search"><i class="fa-grid"></i> Change Grid</span>
                <span id="add" class="btn btn-ribbon hidden-xs" data-title="add"><i class="fa-plus"></i> Add</span>
                <span id="search" class="btn btn-ribbon" data-title="search"><i class="fa-search"></i> <span class="hidden-mobile">Search</span></span>
                </span> -->

    </div>
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
    <h1 class="page-title txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-eye"></i>VIP管理
                            <span>>  
                                檢視VIP
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">


        <article class="col-xs-12 col-sm-12 col-md-9 col-lg-9">
            <%  Html.RenderPartial("~/Views/Lessons/LessonsInfo.ascx", _model.LessonTime); %>
            <div class="jarviswidget" id="wid-id-list" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false">
                <%  Html.RenderAction("LessonContent", "Report", new { lessonID = _model.LessonID, edit = true }); %>
            </div>
        </article>

        <article class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-external-link"></i>快速功能</h5>
                <ul class="no-padding no-margin">
                    <ul class="icons-list">
                        <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ListLearners.ascx"); %>
                        <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ListCoaches.ascx"); %>
                        <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/Overview.ascx",_model); %>
                        <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ViewVip.ascx",_model.RegisterLesson.UserProfile); %>
                    </ul>
                </ul>
            </div>
        </article>
    </div>

    <% Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>
    <% Html.RenderPartial("~/Views/Shared/EasyPieView.ascx"); %>

    <script>

        function updateRemark(lessonID) {
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/UpdateLessonPlanRemark") %>',
                {
                    'lessonID': lessonID,
                    'remark': $('#remark').val()
                }, function (data) {
                if (data.result) {
                    smartAlert('資料已更新!!');
                } else {
                    smartAlert(data.message);
                }
            });
        }

        function updateConclusion(lessonID) {
            var event = event || window.event;
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitConclusion") %>',
                {
                    'lessonID': lessonID,
                    'conclusion': $(event.target).prev('input').val()
                }, function (data) {
                    if (data.result) {
                        smartAlert('資料已更新!!');
                    } else {
                        smartAlert(data.message);
                    }
                });
        }
    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonTimeExpansion _model;
    UserProfile _profile;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonTimeExpansion)this.Model;
        _profile = _model.LessonTime.RegisterLesson.UserProfile;
    }

</script>

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
                <i class="fa fa-dashboard"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>課程管理></li>
            <li>課程總覽</li>
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
        <i class="fa-fw fa fa-dashboard"></i>課程管理
							<span>>  
								課程總覽
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <article class="col-sm-12 col-md-6 col-lg-6">
            <!-- new widget -->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-3" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">

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
                    <span class="widget-icon"><i class="fa fa-calendar"></i></span>
                    <h2>BEYOND FITNESS Events </h2>
                    <div class="widget-toolbar">
                        <a class="btn btn-primary" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/BookingByFreeAgent") %>"><i class="fa fa-fw fa-bookmark"></i>登記上課時間</a>
                    </div>

                </header>

                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox bg-color-darken">
                    </div>
                    <!-- end widget edit box -->

                    <div class="widget-body bg-color-darken txt-color-white no-padding">
                        <!-- content goes here -->
                        <div class="widget-body-toolbar">

                            <div id="calendar-buttons">

                                <div class="btn-group">
                                    <a href="javascript:void(0)" class="btn btn-default btn-xs" id="btn-prev"><i class="fa fa-chevron-left"></i></a>
                                    <a href="javascript:void(0)" class="btn btn-default btn-xs" id="btn-next"><i class="fa fa-chevron-right"></i></a>
                                </div>
                            </div>
                        </div>
                        <% Html.RenderPartial("~/Views/Lessons/LessonsCalendar.ascx", _model); %>

                        <!-- end content -->
                    </div>

                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->

        </article>

        <%--<article class="col-sm-12 col-md-6 col-lg-6">

            <!-- new widget -->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-4" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">

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
                    <span class="widget-icon"><i class="fa fa-check txt-color-white"></i></span>
                    <h2>待辦事項 </h2>
                    <!-- <div class="widget-toolbar">
									add: non-hidden - to disable auto hide

									</div>-->
                </header>

                <!-- widget div-->
                <div>

                    <% Html.RenderPartial("~/Views/Lessons/DailyTodoList.ascx", _lessonDate); %>
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->

        </article>--%>
    </div>

    <!-- row -->
    <div class="row">
        <article class="col-sm-12">
            <!-- new widget -->
            <div class="jarviswidget" id="wid-id-0" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false">
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
                    <span class="widget-icon"><i class="fa fa-rss text-success"></i></span>
                    <h2><%= _lessonDate.Value.ToString("yyyy/MM/dd") %> 課程表 </h2>

                    <ul class="nav nav-tabs pull-right in" id="myTab">
                        <li class="active">
                            <a data-toggle="tab" href="#s1"><i class="fa fa-list"></i><span>列表</span></a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#s2"><i class="fa fa-bar-chart"></i><span>統計表</span></a>
                        </li>
                    </ul>

                </header>

                <!-- widget div-->
                <div class="no-padding">
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                    </div>
                    <!-- end widget edit box -->

                    <div class="widget-body">
                        <!-- content -->
                        <div id="myTabContent" class="tab-content">
                            <div class="tab-pane fade active in" id="s1">
                                <% Html.RenderPartial("~/Views/Lessons/DailyBookingList.ascx", _lessonDate); %>
                            </div>
                            <!-- end s1 tab pane -->
                            <div class="tab-pane fade" id="s2">
                                <% Html.RenderPartial("~/Views/Lessons/DailyBarGraph.ascx", _lessonDate); %>
                            </div>
                            <!-- end s3 tab pane -->
                        </div>
                        <script>
                            $('a[data-toggle="tab"]').on('shown.bs.tab', function (evt) {
                                if ($('#s2').css('display') == 'block') {
                                    plotData('<%= _lessonDate.Value.ToString("yyyy-MM-dd") %>');
                                }
                            });
                        </script>

                        <!-- end content -->
                    </div>

                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->

        </article>
    </div>
    <!-- Start Content -->
    <% Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>
    <!-- End content -->

    <script>

        var pageParam = {
            lessonDate: '<%= _lessonDate.Value.ToString("yyyy-MM-dd") %>',
            endQueryDate: '<%= String.Format("{0:yyyy-MM-dd}",ViewBag.EndQueryDate??_lessonDate) %>'
        };

        $(function () {
            showLoading();
        });

    </script>


</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _model;
    DateTime? _lessonDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _lessonDate = (DateTime?)ViewBag.LessonDate;
    }

</script>

<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

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
                <i class="fa fa-search"></i>
            </span>
        </span>
        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>課程管理></li>
            <li>我的VIP</li>
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
        <i class="fa-fw fa fa-search"></i>課程管理
                     <span>>  
                     我的VIP
                     </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

                <div class="row">
                    <article class="col-sm-12 col-md-6 col-lg-6">
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
                                <span class="widget-icon"> <i class="fa fa-search txt-color-white"></i> </span>
                                <h2> 查詢條件 </h2>
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
                                    <form id="pageForm" action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/QueryVip") %>" class="smart-form" method="post">
                                        <fieldset>
                                            <div class="row">
                                                <section class="col col-6">
                                                    <label class="input"> <i class="icon-append fa fa-user"></i>
                                                        <input type="text" class="input-lg" name="userName" id="userName" placeholder="請輸入VIP姓名" value="<%= _viewModel.UserName %>" />
                                                    </label>
                                                </section>
                                                <section class="col col-6">
                                                    <label class="select">
                                                        <%  ViewBag.SelectAll = true;
                                                            Html.RenderPartial("~/Views/Lessons/SimpleCoachSelector.ascx", new InputViewModel { Id = "coachID", Name = "coachID",DefaultValue = _viewModel.CoachID }); %>
                                                        <i class="icon-append fa fa-file-word-o"></i>
                                                    </label>
                                                </section>
                                            </div>
                                            <div class="row">
                                                <section class="col col-6">
                                                    <label class="input input-group">
                                                        <i class="icon-append fa fa-calendar"></i>
                                                        <input type="text" name="dateFrom" id="dateFrom" class="form-control input-lg date form_month" data-date-format="yyyy/mm/dd" placeholder="請輸入查詢起月" value="<%= String.Format("{0:yyyy/MM/dd}", _viewModel.DateFrom) %>" />
                                                    </label>
                                                </section>
                                                <section class="col col-6">
                                                    <label class="select">
                                                        <select class="input-lg" name="monthInterval" id="monthInterval">
                                                            <option value="1">查詢區間1個月</option>
                                                            <option value="2">查詢區間2個月</option>
                                                        </select>
                                                        <i></i>
                                                    </label>
                                                </section>
                                            </div>
                                        </fieldset>
                                        <footer>
                                            <button type="submit" name="submit" class="btn btn-primary">
                                                送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                            </button>
                                        </footer>
                                    </form>
                                </div>
                                <!-- end widget content -->
                            </div>
                            <!-- end widget div -->
                        </div>
                        <!-- end widget -->
                    </article>
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
                                    <a class="btn btn-primary" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/BookingByCoach") %>"><i class="fa fa-fw fa-bookmark"></i>登記上課時間</a>
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
                                <h2>2016/07/01 - 2016/08/01 </h2>

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
                                        <div class="tab-pane fade active widget-body in no-padding-bottom" id="s1">
                                            <div class="well bg-color-blueDark no-padding">
                                                <% Html.RenderAction("QueryBookingList"); %>
                                            </div>
                                        </div>
                                        <!-- end s1 tab pane -->
                                        <div class="tab-pane fade" id="s2">
                                            <div class="well bg-color-blueDark no-padding">
                                                <% Html.RenderPartial("~/Views/Lessons/DailyBarGraph.ascx", _lessonDate); %>
                                            </div>


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
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        function inquire() {
            <%--        var $modal = $('<div class="form-horizontal modal fade" tabindex="-1" role="dialog" aria-labelledby="searchdilLabel" aria-hidden="true" />');
        $modal.on('hidden.bs.modal', function (evt) {
            $modal.remove();
        });
        $modal.appendTo($('body'))
            .load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/QueryModal") %>', pageParam, function () {
                    $modal.modal('show');
                });--%>
            $('#queryModal').css('display', 'block');
        }

<%--        $(function () {
            $('#loading').css('display', 'table');
            $('#attendeeList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingMembers") %>', { 'lessonDate': '<%= _lessonDate.Value.ToString("yyyy-MM-dd") %>' }, function () {
                $('#loading').css('display', 'none');
            });
        });--%>

    </script>

    <script>
    function revokeBooking(lessonID) {
        confirmIt({ title: '取消預約', message: '確定取消預約此課程?' }, function (evt) {
            $('#loading').css('display', 'table');
            $('<form method="post"/>')
                .appendTo($('body'))
                .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/RevokeBooking") %>')
                .append($('<input type="hidden" name="lessonID"/>').val(lessonID))
                .submit();
        });
    }

    function makeLessonPlan(arg)
    {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/TrainingPlan") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        startLoading();
        $form.submit();
    }

    function attendLesson(arg)
    {
        startLoading();
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Attendance/TrainingPlan") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        $form.submit();
    }

    function previewLesson(arg)
    {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/PreviewLesson") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        startLoading();
        $form.submit();
    }

    </script>


</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _model;
    DailyBookingQueryViewModel _viewModel;
    IEnumerable<LessonTime> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _viewModel = (DailyBookingQueryViewModel)ViewBag.ViewModel;
        _items = (IEnumerable<LessonTime>)this.Model;
    }

</script>

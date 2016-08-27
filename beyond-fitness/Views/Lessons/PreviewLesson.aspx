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
            <div class="well well-sm bg-color-darken txt-color-white">
                <div class="row">

                    <%--<%  Html.RenderPartial("~/Views/Layout/Carousel.ascx"); %>--%>

                    <div class="col-sm-12">

                        <div class="row">

                            <%  Html.RenderPartial("~/Views/Member/LessonCount.ascx", _model.RegisterLesson.UserProfile); %>

                            <div class="col-xs-8 col-sm-6">
                                <h1>
                                    <span class="semi-bold"><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.RegisterLesson.UID %>"><%= _model.RegisterLesson.UserProfile.RealName %> <%= _model.RegisterLesson.UserProfile.UserName %></a></span>
                                </h1>
                                <p class="font-md">關於<%= _model.RegisterLesson.UserProfile.RealName %>...</p>
                                <p>
                                    <%= _model.RegisterLesson.UserProfile.RecentStatus %>
                                </p>
                            </div>
                            <div class="col-xs-12 col-sm-3">
                                <%  Html.RenderPartial("~/Views/Member/ContactInfo.ascx", _model.RegisterLesson.UserProfile); %>
                                <%  Html.RenderPartial("~/Views/Member/UserAssessmentInfo.ascx", _model.RegisterLesson.UserProfile); %>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <hr />
                </div>
                <div class="row no-padding">
                    <div class="col-sm-12">
                        <ul class="nav nav-tabs tabs-pull-right">
                            <li>
                                <a data-toggle="tab" href="#s5"><i class="fa fa-pie-chart"></i><span>分析圖</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s4"><i class="fa fa-child"></i><span>收操</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s3"><i class="fa fa-heartbeat"></i><span>訓練內容</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s2"><i class="fa fa-child "></i><span>暖身</span></a>
                            </li>
                            <li class="active">
                                <a data-toggle="tab" href="#s1"><i class="fa fa-commenting-o"></i><span>有話想說</span></a>
                            </li>
                            <li class="pull-left">
                                <span class="margin-top-10 display-inline"><i class="fa fa-rss text-success"></i><%= _model.ClassDate.ToString("yyyy/MM/dd") %> <%= String.Format("{0:00}",_model.Hour) %>:00~<%= String.Format("{0:00}",_model.Hour+1) %>:00</span>
                            </li>
                        </ul>
                        <div class="tab-content padding-top-10">
                            <div class="tab-pane fade in active" id="s1">
                                <div class="row">
                                    <div class="chat-body no-padding profile-message">
                                        <ul>
                                            <li class="message">
                                                <% _model.LessonTime.AsAttendingCoach.UserProfile.RenderUserPicture(Writer,new { @class = "profileImg online" }); %>
                                                <span class="message-text">
                                                    <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.LessonTime.AttendingCoach %>"><%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></a>
                                                    <%= _model.LessonTime.LessonPlan.Remark %>
                                                </span>
                                            </li>

                                            <li class="message message-reply">
                                                <% _model.RegisterLesson.UserProfile.RenderUserPicture(Writer, new { @class = "authorImg online" }); %>
                                                <span class="message-text">
                                                    <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.RegisterLesson.UID %>"><%= _model.RegisterLesson.UserProfile.UserName ?? _model.RegisterLesson.UserProfile.RealName %></a>
                                                    <%= _model.LessonTime.LessonPlan.FeedBack %>
                                                </span>

                                                <ul class="list-inline font-xs">
                                                    <li>
                                                        <a href="javascript:void(0);" class="text-muted"><%= String.Format("{0:yyyy/MM/dd HH:mm}",_model.LessonTime.LessonPlan.FeedBackDate) %></a>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>
                                                <div class="input-group wall-comment-reply">
                                                    <input id="remark" name="remark" type="text" class="form-control" placeholder="Type your message here..." value="" />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-primary" onclick="updateRemark(<%= _model.LessonTime.LessonID %>);">
                                                            <i class="fa fa-reply"></i>回覆
                                                        </button>
                                                    </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade in" id="s2">
                                <div class="row">
                                    <div class="col-xs-2 col-sm-1">
                                        <i class="fa fa-child fa-3x"></i>
                                    </div>
                                    <div class="col-xs-10 col-sm-11">
                                        <p><%= _model.LessonTime.LessonPlan.Warming %></p>
                                    </div>
                                    <div class="col-sm-12">
                                        <hr>
                                    </div>

                                </div>
                            </div>
                            <div class="tab-pane fade in" id="s3">
                                <% Html.RenderPartial("~/Views/Lessons/ViewTrainingExecutionPlan.ascx", _model); %>
                            </div>
                            <div class="tab-pane fade in" id="s4">
                                <div class="row">
                                    <div class="col-xs-2 col-sm-1">
                                        <i class="fa fa-child fa-3x"></i>
                                    </div>
                                    <div class="col-xs-10 col-sm-11">
                                        <p><%= _model.LessonTime.LessonPlan.EndingOperation %></p>
                                    </div>
                                    <div class="col-sm-12">
                                        <hr/>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade in" id="s5">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                        <%  Html.RenderPartial("~/Views/Lessons/DailyTrendPieView.ascx", _model); %>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                        <%  Html.RenderPartial("~/Views/Lessons/DailyFitnessPieView.ascx", _model); %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>

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

                    <div class="col-sm-12">
                        <div id="myCarousel" class="carousel fade profile-carousel">
                            <ol class="carousel-indicators">
                                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                <li data-target="#myCarousel" data-slide-to="1" class=""></li>
                                <li data-target="#myCarousel" data-slide-to="2" class=""></li>
                            </ol>
                            <div class="carousel-inner">
                                <!-- Slide 1 -->
                                <div class="item active">
                                    <img alt="demo user" src="<%= VirtualPathUtility.ToAbsolute("~/img/slider/bg8.jpg") %>">
                                </div>
                                <!-- Slide 2 -->
                                <div class="item">
                                    <img alt="demo user" src="<%= VirtualPathUtility.ToAbsolute("~/img/slider/bg5.jpg") %>">
                                </div>
                                <!-- Slide 3 -->
                                <div class="item">
                                    <img alt="demo user" src="<%= VirtualPathUtility.ToAbsolute("~/img/slider/bg6.jpg") %>">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">

                        <div class="row">

                            <div class="col-xs-4 col-sm-3 profile-pic">
                                <% _model.RenderUserPicture(Writer, "authorImg"); %>
                                <div class="padding-10">
                                    <i class="fa fa-birthday-cake"></i>&nbsp;&nbsp;<span class="txt-color-darken"> 28歲</span>
                                    <br />
                                    <h4 class="font-md"><strong><%= _currentLessons.Sum(c=>c.Lessons) - _currentLessons.Sum(c=>c.LessonTime.Count(l=>l.LessonAttendance!= null)) %> / <%= _currentLessons.Sum(c=>c.Lessons) %></strong>
                                        <br>
                                        <small>剩餘/全部 上課數</small></h4>
                                </div>
                            </div>
                            <%  Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model); %>
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
                                <a data-toggle="tab" href="#s2"><i class="fa fa-street-view"></i><span>問卷調查表</span></a>
                            </li>
                            <li class="active">
                                <a data-toggle="tab" href="#s1"><i class="fa fa-credit-card"></i><span>購買上課記錄</span></a>
                            </li>
                        </ul>
                        <div class="tab-content padding-top-10">
                            <div class="tab-pane fade in active" id="s1">
                                <div class="row ">
                                    <div class="col-xs-12 col-sm-12">
                                        <% Html.RenderPartial("~/Views/Member/LessonsList.ascx", _items); %>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade in" id="s2">
                                <div class="row">
                                    <%  ViewBag.DataItems = models.GetTable<PDQQuestion>().OrderBy(q => q.QuestionNo).ToArray();
                                        Html.RenderPartial("~/Views/Member/PDQInfoByLearner.ascx", _model); %>
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
                        <li>
                            <a title="VIP列表" href="viplist.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-user fa-stack-1x"></i></span>VIP列表</a>
                        </li>
                        <li>
                            <a title="員工列表" href="coachlist.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-user-secret fa-stack-1x"></i></span>員工列表</a>
                        </li>
                        <li>
                            <a title="我的課程總覽" href="coachdashboard.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-dashboard fa-stack-1x"></i></span>我的課程總覽</a>
                        </li>
                    </ul>
                </ul>
            </div>
        </article>
    </div>

    <script>

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    IEnumerable<RegisterLesson> _items;
    IEnumerable<RegisterLesson> _currentLessons;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;

        _items = models.GetTable<RegisterLesson>().Where(r => r.UID == _model.UID)
            .OrderByDescending(r => r.RegisterID);
        _currentLessons = _items.Where(i => i.Lessons != i.LessonTime.Count(s => s.LessonAttendance != null));
        ViewBag.ShowOnly = true;
    }


</script>

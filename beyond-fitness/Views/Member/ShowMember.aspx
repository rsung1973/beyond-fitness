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
                <i class="fa fa-user-plus"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>人員管理></li>
            <li>員工管理</li>
            <li>新增員工</li>
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
        <i class="fa-fw fa fa-user-plus"></i>員工管理
                            <span>>  
                                新增員工
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <article class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
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

                            <div class="col-xs-4 col-sm-2 profile-pic">
                                <% _model.RenderUserPicture(Writer, "authorImg"); %>
                                <div class="padding-10">
                                    <h4 class="font-md"><small><%= _model.IsFreeAgent() ? "自由教練" : _userProfile.IsSysAdmin() && _model.ServingCoach.ProfessionalLevel!=null ? _model.ServingCoach.ProfessionalLevel.LevelName : "" %></small></h4>
                                </div>
                            </div>
                            <%  Html.RenderPartial("~/Views/Member/CoachInfo.ascx", _model); %>
                        </div>
                    </div>
                </div>
            </div>
        </article>
        <article class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-external-link"></i>快速功能</h5>
                <ul class="no-padding no-margin">
                    <ul class="icons-list">
                        <li>
                            <a title="忘記密碼" href="forgetpasswd.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-key fa-stack-1x"></i></span>忘記密碼</a>
                        </li>
                        <li>
                            <a title="註冊" href="regester.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-user fa-stack-1x"></i></span>會員註冊</a>
                        </li>
                    </ul>
                </ul>
            </div>
        </article>

    </div>


    <script>
        $(function () {
            $('.carousel.slide').carousel({
                interval: 3000,
                cycle: true
            });
            $('.carousel.fade').carousel({
                interval: 3000,
                cycle: true
            });
        });
    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _userProfile = Context.GetUser();
    }


</script>

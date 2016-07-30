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
                <i class="fa fa-user"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>我的簡介</li>
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
        <i class="fa-fw fa fa-user"></i>我的簡介
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <div class="col-sm-12 col-md-9 col-lg-9">
            <!--Start Profile-->
            <div class="well well-sm bg-color-darken txt-color-white no-margin no-padding">

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
                                    <img src="../img/slider/bg1.jpg" alt="demo user"/>
                                </div>
                                <!-- Slide 2 -->
                                <div class="item">
                                    <img src="../img/slider/bg2.jpg" alt="demo user">
                                </div>
                                <!-- Slide 3 -->
                                <div class="item">
                                    <img src="../img/slider/bg3.jpg" alt="demo user">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">

                        <div class="row">

                            <div class="col-sm-3 profile-pic">
                                <% _item.PictureID.RenderUserPicture(this.Writer, "authorImg"); %>
                                <div class="padding-10">
                                    <i class="fa fa-birthday-cake"></i>&nbsp;&nbsp;<span class="txt-color-darken"> <%= _item.YearsOld() %>歲</span>
                                    <br/>
                                    <% Html.RenderPartial("~/Views/Member/MemberLessonsInfo.ascx", _item); %>
                                </div>
                            </div>
                            <div class="col-sm-9">
                                <h1><span class="semi-bold"><%= String.IsNullOrEmpty(_item.UserName) ? _item.RealName :_item.UserName %></span>
                                    <br/>
                                    <small></small></h1>

                                <ul class="list-unstyled">
                                    <li>
                                        <p class="text-muted">
                                            <i class="fa fa-phone"></i>&nbsp;&nbsp;(<span class="txt-color-darken">886) <%= _item.Phone %></span>
                                        </p>
                                    </li>
                                    <li>
                                        <p class="text-muted">
                                            <i class="fa fa-envelope"></i>&nbsp;&nbsp;<a href="mailto:<%= _item.PID %>"><%= _item.PID %></a>
                                        </p>
                                    </li>
                                </ul>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <!--End Profile-->

        </div>
        <div class="col-sm-12 col-md-3 col-lg-3">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-thumbs-o-up"></i>快速功能</h5>
                <ul class="no-padding no-margin">
                    <p class="no-margin">
                        <ul class="icons-list">
                            <li>
                                <a title="我的足印" href="timeline.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-paw fa-stack-1x"></i></span>我的足印</a>
                            </li>
                            <li>
                                <a title="我的總覽" href="vipdashboard.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-tachometer fa-stack-1x"></i></span>我的總覽</a>
                            </li>
                            <li>
                                <a title="修改資料" href="setting.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-cog fa-stack-1x"></i></span>修改資料</a>
                            </li>
                            <li>
                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Logout") %>" title="登出"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-sign-out fa-stack-1x"></i></span>登出</a>
                            </li>
                        </ul>
                    </p>
                </ul>
            </div>
            <!-- /well -->
        </div>
    </div>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (UserProfile)this.Model;
    }


</script>

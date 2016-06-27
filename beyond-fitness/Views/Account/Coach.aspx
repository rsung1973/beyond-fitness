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


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="pageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-4">

                    <!-- Classic Heading -->
                    <% Html.RenderPartial("~/Views/Member/SimpleMemberInfo.ascx", _model); %>
                    <!-- End Classic -->

                    <!-- Start Contact Form -->
                    <!-- Categories Widget -->
                    <div class="widget widget-categories">
                        <ul>
                            <li>
                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/EditMySelf") %>"><i class="fa fa-cog" aria-hidden="true"></i>修改個人資料</a>
                            </li>
                        </ul>
                    </div>
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                    <!-- Responsive calendar - START -->
                    <% Html.RenderPartial("~/Views/Lessons/LessonsCalendar.ascx"); %>

                    <!-- Responsive calendar - END -->

                    <!-- End Contact Form -->

                </div>
                <div class="col-md-8">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-calendar"> 行事曆清單</span></h4>

                    <!-- Start Contact Form -->
                    <!-- Stat Search -->
                    <div class="navbar bg_gray" style="min-height: 30px;">
                        <div class="search-side">
                            <a class="btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/BookingByCoach") %>">登記上課時間 <span class="fa fa-calendar-plus-o"></span></a>
                            <a href="#" class="btn-system btn-small">自由教練打卡 <i class="fa fa-check-square" aria-hidden="true"></i></a>
                            <a class="btn btn-search" data-toggle="modal" data-target="#searchdil" data-whatever="搜尋"><i class="fa fa-search"></i></a>
                        </div>
                    </div>

                    <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>

                    <div id="dailyBooking" class="panel panel-default">
                        <!-- TABLE 1 -->
                        <% Html.RenderPartial("~/Views/Lessons/DailyBookingList.ascx", _lessonDate); %>
                    </div>

                    <!-- End Contact Form -->
                    <% Html.RenderPartial("~/Views/Lessons/DailyStackView.ascx", _lessonDate); %>

                    <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>
                    <div id="attendeeList" class="panel panel-default">
                    </div>

            </div>
        </div>
    </div>
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <!-- End content -->

    <script>
    $('#vip,#m_vip').addClass('active');
    $('#theForm').addClass('contact-form');

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

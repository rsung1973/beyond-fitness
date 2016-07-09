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
                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <%  ViewBag.Argument = new ArgumentModel { Model = _model, PartialViewName = "~/Views/Account/LessonSummary.ascx" };
                        Html.RenderPartial("~/Views/Member/SimpleMemberInfo.ascx", _model); %>
                    <!-- End Classic -->
                </div>
                <div class="col-md-12">
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                    <!-- Responsive calendar - START -->
                    <% Html.RenderPartial("~/Views/Lessons/VipCalendar.ascx"); %>
                    <!-- Responsive calendar - END -->

                    <!-- End Contact Form -->

                </div>
                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>
                    <h4 class="classic-title"><span><%= _lessonDate.Value.ToString("yyyy/M/d") %>~<%= _endQueryDate.Value.ToString("yyyy/M/d") %> 運動走勢圖</span></h4>

                    <!-- Start Contact Form -->

                    <div class="row">
                        <div class="col-md-6">
                            <h4><span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>著重方向：</h4>
                            <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>
                            <% Html.RenderPartial("~/Views/Lessons/TrendGraphView.ascx"); %>
                        </div>
                        <div class="col-md-6">
                            <h4><span class="glyphicon glyphicon-heart-empty" aria-hidden="true"></span>體適能：</h4>
                            <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>
                            <% Html.RenderPartial("~/Views/Lessons/FitnessGraphView.ascx"); %>
                        </div>
                    </div>

                    <!-- End Contact Form -->
                </div>
            </div>
        </div>
    </div>
    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/GraphView.ascx"); %>
    <script>
    $('#vip,#m_vip').addClass('active');
    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    DateTime? _lessonDate;
    DateTime? _endQueryDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _lessonDate = (DateTime?)ViewBag.LessonDate;
        _endQueryDate = (DateTime?)ViewBag.EndQueryDate;
    }

</script>

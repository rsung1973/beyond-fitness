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

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-edit"> 修改學員資料</span></h4>

                    <!-- Start Contact Form -->

                    <p><strong>會員編號：</strong><span class="text-primary"><%= _model.MemberCode %></span></p>

                    <!-- Divider -->
                    <div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <% Html.RenderPartial("~/Views/Member/EditLearnerItem.ascx",_model); %>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>" class="btn-system btn-medium">回清單頁 <i class="fa fa-th-list" aria-hidden="true"></i></a>
                    <a id="nextStep" class="btn-system btn-medium">確定 <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></a>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->





    <script>
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/EditLearner") %>')
              .submit();

            });

    </script>
</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;
    LearnerViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LearnerViewModel)this.Model;
    }
</script>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
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
                    <h4 class="classic-title"><span>教練資料更新完成</span></h4>

                    <!-- Start Post -->
                    <div class="blog-post quote-post">
                        <!-- Post Content -->
                        <div class="user-info clearfix">
                            <div class="author-image">
                                <div class="user-image">
                                    <% _model.RenderUserPicture(this.Writer, "authorImg"); %>
                                </div>
                                <div class="user-bio">
                                    <h2 class="text-primary"><%= _model.RealName %> </h2>

                                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                                    <p><strong>教練編號：</strong><%= _model.MemberCode %></p>

                                    <!-- Divider -->
                                    <div class="hr1" style="margin-bottom: 10px;"></div>
                                    <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>" class="btn-system btn-small">繼續修改教練 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- End Post -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');
    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
    }



</script>

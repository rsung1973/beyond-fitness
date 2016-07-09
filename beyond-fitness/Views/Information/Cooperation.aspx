<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Start Page Banner -->
    <div class="page-banner" style="padding: 40px 0; background: url(../images/page_banner_bg.gif);">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>相關合作</h2>
                    <p>Cooperation</p>
                </div>
                <div class="col-md-6">
                    <ul class="breadcrumbs">
                        <li><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                        <li>相關合作</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- End Page Banner -->

    <!-- Start Content -->
    <div id="content">
        <div class="container">
            <div class="page-content">

                <div class="row">


                    <div class="col-md-7">

                        <!-- Some Text -->
                        <p>BEYOND FITNESS 提供您有關健康減重、運動、營養、體適能、健康生活及相關講座及演講邀約服務。若您有以上相關需求，歡迎您與我們聯繫，我們也提供以下諮詢服務：</p>
                        <p><span class="text-primary"><i class="fa fa-check-square" aria-hidden="true"></i></span>企業合作顧問服務</p>
                        <p><span class="text-primary"><i class="fa fa-check-square" aria-hidden="true"></i></span>相關產業管理及顧問服務</p>
                        <p><span class="text-primary"><i class="fa fa-check-square" aria-hidden="true"></i></span>產業人才培育</p>

                    </div>
                    <!-- Start Memebr 1 -->
                    <div class="col-md-5 image-service-box">
                        <img class="img-thumbnail" src="../images/COOPERATION-01.png" alt="" />
                    </div>
                </div>

            </div>

        </div>
    </div>
    <!-- End content -->
    <script>
    $('#cooperation,#m_cooperation').addClass('active');
    </script>
</asp:Content>

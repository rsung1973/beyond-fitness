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

    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>註冊 - Step 3</span></h4>

                    <!-- Start Post -->
                    <div class="blog-post quote-post">
                        <!-- Post Content -->
                        <div class="user-info clearfix">
                            <div class="author-image">
                                <img alt="" src="<%= _item.PictureID.HasValue ? VirtualPathUtility.ToAbsolute("~/Information/GetResource/") + _item.PictureID : VirtualPathUtility.ToAbsolute("~/images/blog_pic.png") %>" />
                            </div>
                            <div class="user-bio">
                                <h2 class="text-primary"><%= _item.UserName %> <span class="subtext">您好</span></h2>

                                <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                                <p><strong>會員編號：</strong><%= _item.MemberCode %></p>
                                <p><strong>Email：</strong><%= _item.PID %></p>

                                <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                                <p>您的註冊已經完成。歡迎加入BEYOND FITNESS。</p>
                                <!-- Divider -->
                                <div class="hr1" style="margin-bottom: 10px;"></div>
                                <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Vip") %>" class="btn-system btn-small">進入會員專區 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                            </div>
                        </div>
                    </div>
                    <!-- End Post -->

                </div>

            </div>
        </div>
    </div>

    <script>
    $('#vip,#m_vip').addClass('active');
    </script>

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

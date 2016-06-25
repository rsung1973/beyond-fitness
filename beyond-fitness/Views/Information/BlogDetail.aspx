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

    <uc1:PageBanner runat="server" ID="PageBanner" Title="專業知識" TitleInEng="Knowledge" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row blog-page">

                <!-- Start Blog Posts -->
                <div class="col-md-9">

                    <!-- Start Big Heading -->
                    <p class="title-desc"><span class="accent-color"><%= _item.Document.DocDate.ToString("yyyy-MM-dd") %> <%: _item.AuthorID.HasValue ? _item.UserProfile.UserName : ""  %></span></p>
                    <h4 class="classic-title"><span><%= _item.Title  %></span></h4>
                    <!-- End Big Heading -->

                    <!-- Some Text -->
                    <p><%= _item.ArticleContent %></p>

                </div>
                <!-- End Blog Posts -->


                <!--Sidebar-->
                <div class="col-md-3 sidebar right-sidebar">

                    <!-- Tags Widget -->
                    <h4 class="classic-title"><span>熱門關鍵字</span></h4>
                    <div class="widget widget-tags">
                        <div class="tagcloud">
                            <a >啞鈴訓練</a>
                            <a >單邊訓練</a>
                            <a >團體課程</a>
                            <a >均衡訓練</a>
                            <a >增強式訓練</a>
                            <a >壺鈴</a>
                            <a >多關節活動</a>
                            <a >奧林匹克槓片</a>
                            <a >女性減脂</a>
                            <a >女性運動</a>
                            <a >孕婦運動</a>
                        </div>
                    </div>

                </div>
                <!--End sidebar-->


            </div>

        </div>
    </div>
    <!-- End content -->
    <script>
    $('#blog,#m_blog').addClass('active');
    </script>

</asp:Content>
<script runat="server">

    ModelSource<Article> models;
    Article _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<Article>)ViewContext.Controller).DataSource;
        _item = (Article)this.Model;
    }

</script>
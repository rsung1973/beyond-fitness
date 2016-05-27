<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" EnableViewState="false" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Register Src="~/Views/Shared/PagingControl.ascx" TagPrefix="uc1" TagName="PagingControl" %>
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
                        <div class="col-md-9 blog-box">

                            <asp:Repeater ID="rpList" runat="server" ItemType="WebHome.Models.DataEntity.Article">
                                <ItemTemplate>
                                    <!-- Start Post -->
                                    <div class="blog-post quote-post">
                                        <!-- Post Content -->
                                        <div class="author-info clearfix">
                                            <div class="author-image">
                                                <img alt="" src="../images/blog_pic.png" />
                                            </div>
                                            <div class="author-date"><%# Item.Document.DocDate.ToString("yyyy-MM-dd") %></div>
                                            <div class="author-bio">
                                                <h3><%# Item.Title %></h3>
                                                <p><%# getBrief(Item.ArticleContent) %></p>
                                                <!-- Divider -->
                                                <div class="hr1" style="margin-bottom: 10px;"></div>
                                                <p><a href="<%# VirtualPathUtility.ToAbsolute("~/Information/BlogDetail/") + Item.DocID.ToString() %>" class="btn-system btn-small">閱讀內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Post -->
                                </ItemTemplate>
                            </asp:Repeater>

                            <!-- Start Pagination -->
                            <uc1:PagingControl runat="server" ID="pagingControl" ControllerName="Information" ActionName="Blog" />
                            <!-- End Pagination -->

                        </div>
                        <!-- End Blog Posts -->


                        <!--Sidebar-->
                        <div class="col-md-3 sidebar right-sidebar">

                            <!-- Tags Widget -->
                            <h4 class="classic-title"><span>熱門關鍵字</span></h4>
                            <div class="widget widget-tags">
                                <div class="tagcloud">
                                    <a href="#">啞鈴訓練</a>
                                    <a href="#">單邊訓練</a>
                                    <a href="#">團體課程</a>
                                    <a href="#">均衡訓練</a>
                                    <a href="#">增強式訓練</a>
                                    <a href="#">壺鈴</a>
                                    <a href="#">多關節活動</a>
                                    <a href="#">奧林匹克槓片</a>
                                    <a href="#">女性減脂</a>
                                    <a href="#">女性運動</a>
                                    <a href="#">孕婦運動</a>
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<Article>();
        rpList.DataSource = models.Items;
        rpList.DataBind();
        pagingControl.Item = (PagingIndexViewModel)ViewBag.PagingModel;
        pagingControl.RecordCount = models.EntityList.Count();
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

    String getBrief(String content)
    {
        if (content == null)
            return null;
        String result = Regex.Replace(content.Substring(0, 150), "<\\s*[Bb][Rr]\\s*/?\\s*>", "\r\n").Trim();
        result = Regex.Replace(result, @"<[^>]+>|&nbsp;", String.Empty);
        result = Regex.Replace(result, "(\\r\\n){2,}", "\r\n");
        return result.Length > 100 ? (result.Substring(0, 100) + "...").Replace("\r\n","<br/>") : result.Replace("\r\n","<br/>");
    }

</script>

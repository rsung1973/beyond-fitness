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
<%@ Register Src="~/Views/Shared/PagingControl.ascx" TagPrefix="uc1" TagName="PagingControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-9">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>知識上稿</span></h4>

                    <!-- Start Contact Form -->
                    <!-- Stat Search -->
                    <div class="navbar bg_gray" style="min-height: 30px;">
                        <div class="search-side">
                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Information/CreateNew") %>" class="btn-system btn-small">新增體適能資訊 <span class="glyphicon glyphicon-calendar"></span></a>
                        </div>
                    </div>

                    <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>

                    <div class="panel panel-default">
                        <!-- TABLE 1 -->
                        <table class="table">
                            <tr class="info">
                                <th>日期</th>
                                <th>標題</th>
                                <th>功能</th>
                            </tr>
                            <asp:Repeater ID="rpList" runat="server" ItemType="WebHome.Models.DataEntity.Article">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Item.Document.DocDate.ToString("yyyy/MM/dd") %></td>
                                        <td><%# Item.Title %></td>
                                        <td>
                                            <a href="<%# VirtualPathUtility.ToAbsolute("~/Information/EditBlog/"+Item.DocID) %>" class="btn btn-system btn-small">修改文章 <i class="fa fa-edit" aria-hidden="true"></i></a>
                                            <a  onclick='<%# "javascript:deleteArticle(" + Item.DocID + ");" %>' class="btn btn-system btn-small" data-toggle="modal" data-target="#confirm" data-whatever="刪除">刪除 <i class="fa fa-times" aria-hidden="true"></i></a>
                                            <a href="<%# VirtualPathUtility.ToAbsolute("~/Information/Preview/"+Item.DocID) %>" class="btn btn-system btn-small">檢視 <i class="fa fa-eye" aria-hidden="true"></i></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <%--<uc1:PagingControl runat="server" ID="pagingControl" />--%>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');

        function deleteArticle(docID) {
            if (confirm('確定刪除此文件?')) {
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Information/DeleteBlog")%>' , { 'docID': docID }, function (data) {
                    alert(data.message);
                    if (data.result) {
                        window.location.reload();
                    }
                });
            }
        }
    </script>

</asp:Content>
<script runat="server">

    ModelSource<Article> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<Article>)ViewContext.Controller).DataSource;
        rpList.DataSource = models.Items;
        rpList.DataBind();
        //pagingControl.Item = (PagingIndexViewModel)ViewBag.PagingModel;
        //pagingControl.RecordCount = models.EntityList.Count();
    }


</script>

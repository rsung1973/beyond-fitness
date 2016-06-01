<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>
<%@ Register Src="~/Views/Shared/UploadResource.ascx" TagPrefix="uc1" TagName="UploadResource" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../CKEditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span><%= _item.Document.CurrentStep==(int)Naming.DocumentLevelDefinition.暫存 ? "知識上稿" : "編輯體適能知識" %></span></h4>

                    <!-- Start Contact Form -->

                    <div class="blog-post quote-post">
                        <div class="form-group has-feedback">
                            <label class="control-label" for="title">標題：</label>
                            <input type="text" class="form-control" placeholder="請輸入文章標題" value="<%= _item.Title %>" name="title" id="title" aria-describedby="coachnameStatus" />
                            <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                            <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                            <span id="coachnameStatus" class="sr-only">(success)</span>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="docDate">日期：</label>
                            <div class="input-group date form_date" data-date="<%= _item.Document.DocDate.ToString("yyyy/MM/dd") %>" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
                                <input class="form-control" size="16" type="text" value="<%= _item.Document.DocDate.ToString("yyyy/MM/dd") %>" name="docDate" readonly>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="docType">文章分類：</label>
                            <div class="form-control">
                                <select name="docType">
                                    <option value="1">專業體能訓練</option>
                                    <option value="2">專業知識</option>
                                    <option value="3">場地租借</option>
                                    <option value="4">相關商品</option>
                                    <option value="5">相關合作</option>
                                    <option value="6">聯絡我們</option>
                                </select>
                            </div>
                        </div>

                        <uc1:UploadResource runat="server" ID="uploadResource" />
                        <div id="resource" class="form-group has-feedback">
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="nickname">內文：</label>
                            <textarea class="form-control" id="articleContent" rows="20"><%= _item.ArticleContent!=null ? _item.ArticleContent.Replace("\r\n","<br/>"):null %></textarea>
                        </div>
                    </div>

                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a href="#" onclick="javascript:updateArticle();" class="btn-system btn-medium">送出</a>
                    <a href="#" onclick="window.location.href = '<%= Request.UrlReferrer %>';" class="btn-system btn-medium border-btn">取消</a>

                    <!-- End Contact Form -->
                </div>

            </div>
        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');
        $(function() {
            loadResource(<%= _item.DocID %>);
            CKEDITOR.config.height = 300;
            CKEDITOR.config.width = 'auto';
            CKEDITOR.replace( 'articleContent' );
        });

        function updateArticle() {
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Information/UpdateArticle")%>', 
                { 
                    'docID': <%= _item.DocID %>, 
                    'docType': $('select[name="docType"]').val(),
                    'docDate': $('input[name="docDate"]').val(),
                    'title': $('input[name="title"]').val(),
                    'content': $('<div>').text(CKEDITOR.instances.articleContent.getData()).html()
                }, function (data) {
                    alert(data.message);
                    if(data.result) {
                        window.location.href = '<%= Request.UrlReferrer %>';
                    }
                });
        }

        function deleteResource(attachmentID) {
            if (confirm('確定刪除此圖片?')) {
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Information/DeleteResource")%>', { 'docID': <%= _item.DocID %>, 'attachmentID': attachmentID }, function (data) {
                    if(data.result) {
                        loadResource(<%= _item.DocID %>);
                    }
                    alert(data.message);
                });
            }
        }

        function loadResource(docID) {
            $('#resource').load('<%= VirtualPathUtility.ToAbsolute("~/Information/Resource") %>' + '/' + docID, function () {
                $('input[name="rbTitleImg"]:radio').on('change', function (evt) {
                    var $this = $(this);
                    if ($this.val() != '') {
                        $.post('<%= VirtualPathUtility.ToAbsolute("~/Information/MakeTheme")%>', { 'docID':docID , 'attachmentID':$this.val() } , function (data) {
                            alert(data.message);
                        });
                    }
                });
            });
        }

    </script>
    <% if (_item.Document.DocType.HasValue)
        { %>
    <script>
        $(function(){
            $('select[name="docType"]').val(<%= _item.Document.DocType %>);
        });
    </script>
    <%  } %>
</asp:Content>
<script runat="server">

    ModelSource<Article> models;
    Article _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<Article>();
        _item = (Article)this.Model;
        uploadResource.DocID = _item.DocID;
    }
    
    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }
</script>

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
<%@ Register Src="~/Views/Shared/BSModal.ascx" TagPrefix="uc1" TagName="BSModal" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>人員管理</span></h4>

                    <!-- Start Contact Form -->
                    <!-- Stat Search -->
                    <div class="navbar bg_gray" style="min-height: 30px;">
                        <div class="search-side">
                            <a class="btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Member/AddCoach") %>">新增教練 <span class="fa fa-user-plus"></span></a>
                            <a class="btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Member/AddLearner") %>">新增學員 <span class="fa fa-user-plus"></span></a>
                            <a class="btn btn-search" data-toggle="modal" data-target="#searchdil" data-whatever="搜尋"><i class="fa fa-search"></i></a>
                        </div>
                    </div>
                    <ul class="nav nav-tabs">
                        <li class="active"><a id="toggleTab-1" href="#tab-1" data-toggle="tab"><i class="fa fa-th-list"></i>教練清單</a></li>
                        <li><a id="toggleTab-2" href="#tab-2" data-toggle="tab"><i class="fa fa-th-list" aria-hidden="true"></i>學員清單</a></li>
                    </ul>
                    <!-- Tab panels -->
                    <div class="tab-content">
                        <!-- Tab Content 1 -->
                        <div class="tab-pane fade in active" id="tab-1">
                            <% Html.RenderPartial("~/Views/Member/ListCoaches.ascx", models); %>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                        </div>
                    </div>
                    <!-- End Contact Form -->
                </div>
                <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
            </div>
        </div>
    </div>
    <!-- End content -->
    <input type="hidden" name="uid" id="uid" />
    <uc1:BSModal runat="server" ID="confirm" />
    <div class="form-horizontal modal fade" id="searchdil" tabindex="-1" role="dialog" aria-labelledby="searchdilLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="searchdilLabel"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>搜尋</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="exampleInputFile" class="col-md-2 control-label">依學員：</label>
                        <div class="col-md-10">
                            <input class="form-control" name="byName" type="text" value=""/>
                        </div>
                    </div>
<%--                    <div class="form-group">
                        <label for="exampleInputFile" class="col-md-2 control-label">依教練：</label>
                        <div class="col-md-10">
                            <select name="select1" class="form-control">
                                <option value="O">李連杰</option>
                                <option value="I">巨石強森</option>
                                <option value="I">阿諾</option>
                                <option value="I">史蒂芬席格</option>
                            </select>
                        </div>
                    </div>--%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-system btn-sm" name="btnQuery" id="btnQuery" ><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確認</button>
                        <button type="button" class="btn btn-default btn-sm" data-dismiss="modal"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>取消</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        //刪除對話方塊
        $('#confirm').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            var recipient = button.data('whatever');            // Extract info from data-* attributes
            var action = button.data('action');
            $('#uid').val(button.data('key'));
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this);
            modal.find('.modal-title label').text(recipient + action);
            modal.find('.modal-body label').text('確認' + recipient + '這筆' + action + '?');
            $('#confirm').find('button[name="btnConfirm"]').off('click').on('click', function (evt) {
                $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/Delete") %>')
                    .submit();
            });
        });

        $('.nav-tabs').on('shown.bs.tab', function (evt) {
            //console.log(evt.target);
            //console.log(evt.relatedTarget);
            if ($(evt.target).prop('id') == 'toggleTab-2') {
                $('#searchdil').modal('show');
            }
        });

        $('#btnQuery').on('click', function (evt) {
            $('#loading').css('display', 'table');
            $('#tab-2').load('<%= VirtualPathUtility.ToAbsolute("~/Member/ListLearners") %>', { 'byName': $('input[name="byName"]').val() }, function () {
                $('#searchdil').modal('hide');
                $('#loading').css('display', 'none');
            });
        });

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }




</script>

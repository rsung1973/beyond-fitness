<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
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
                    <h4 class="classic-title"><span>編輯教練資料</span></h4>

                    <!-- Start Contact Form -->

                    <p><strong>員工編號：</strong><span class="text-primary"><%= _model.MemberCode %></span></p>

                    <!-- Divider -->
                    <div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <% Html.RenderPartial("~/Views/Member/CoachItem.ascx", _model); %>


                    <div class="hr1" style="margin: 5px 0px;"></div>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <div class="hr1" style="margin: 5px 0px;"></div>

                    <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">回上頁 <i class="fa fa-reply" aria-hidden="true"></i></a>
                    <a href="#" id="nextStep" class="btn-system btn-medium"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確認</a>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
        <!-- End content -->


        <script>
            $('#vip,#m_vip').addClass('active');
            $('#theForm').addClass('contact-form');

            $('#nextStep').on('click', function (evt) {

                $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/EditCoach") %>')
              .submit();

        });

        $("form").validate({
            //debug: true,
            //errorClass: "label label-danger",
            success: function (label, element) {
                label.remove();
                var id = $(element).prop("id");
                $('#' + id + 'Icon').removeClass('glyphicon-remove').removeClass('text-danger')
                    .addClass('glyphicon-ok').addClass('text-success');
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element);
                var id = $(element).prop("id");
                $('#' + id + 'Icon').addClass('glyphicon-remove').addClass('text-danger')
                    .removeClass('glyphicon-ok').removeClass('text-success');
            },
            rules: {
                realName: {
                    required: true,
                    maxlength: 20
                },
                phone: {
                    required: true,
                    regex: /^[0-9]{6,20}$/
                }
            }
        });

        </script>
</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
    }
</script>

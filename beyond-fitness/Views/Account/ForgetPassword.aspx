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
                    <h4 class="classic-title"><span>忘記密碼</span></h4>

                    <!-- Start Contact Form -->
                    <p>請輸入您的email，我們會協助您取回密碼。</p>
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <div class="form-group has-feedback">
                        <% Html.RenderInput("EMail：", "email", "email", "請輸入EMail", _modelState); %>
                        <% if (ViewBag.Success != null)
                            {
                                Writer.WriteLine(Html.Label((String)ViewBag.Success, new { @class = "text-success" }));
                            } %>
                    </div>

                    <!--<div class="hr1" style="margin:10px 0px;"></div>
              
              <div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <div class="hr1" style="margin: 10px 0px;"></div>
                    <button type="reset" id="button" class="btn-system btn-medium border-btn">清除</button>
                    <button type="button" id="nextStep" class="btn-system btn-medium">送出 <i class="fa fa-paper-plane" aria-hidden="true"></i></button>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->


    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/ForgetPassword") %>')
              .submit();

        });
    </script>

    <script>
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
            // compound rule
            email: {
                required: true,
                email: true
            }
        }
    });
    </script>


</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = TempData.GetModelSource<UserProfile>();
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

</script>

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
                    <h4 class="classic-title"><span>重新設定密碼</span></h4>

                    <!-- Start Contact Form -->
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>
                    <p><strong>會員編號：</strong><span class="text-primary"><%= _item.MemberCode %></span></p>
                    <p><strong>Email：</strong><%= _item.EMail %></p>

                    <div class="form-group has-feedback">
                        <label class="text-success">密碼設定完成，請重新登入。</label>
                    </div>

                    <!--<div class="hr1" style="margin:10px 0px;"></div>
              
              <div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <!-- End Contact Form -->
                    <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Login") %>" class="btn-system btn-small">登入會員專區 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
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


</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = TempData.GetModelSource<UserProfile>();
        _item = (UserProfile)this.Model;
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

</script>

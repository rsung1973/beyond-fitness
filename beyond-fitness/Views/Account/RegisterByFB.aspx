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
                    <h4 class="classic-title"><span>註冊 - Step 2</span></h4>

                    <!-- Start Post -->
                    <div class="blog-post quote-post">
                        <!-- Post Content -->
                        <div class="author-info clearfix">
                            <div class="author-image">
                                <img alt="" width="100" src="<%= _item.PictureID.HasValue ? VirtualPathUtility.ToAbsolute("~/Information/GetResource/") + _item.PictureID : VirtualPathUtility.ToAbsolute("~/images/noMember.jpg") %>" />
                            </div>
                            <div class="author-bio">
                                <h2 class="text-primary"><%= _item.UserName %> <span class="subtext">您好</span></h2>

                                <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                                <p><strong>會員編號：</strong><%= _item.MemberCode %></p>

                                <div class="form-group has-feedback">
                                    <label class="control-label" for="nickname">Email：</label>
                                    <input type="text" class="form-control" placeholder="Email" name="email" id="email" value="<%= _viewModel.EMail %>" aria-describedby="nicknameStatus"/>
                                    <input type="hidden" name="userName" id="userName" value="<%= _viewModel.UserName %>" />
                                    <input type="hidden" name="userID" id="userID" value="<%= _viewModel.UserID %>" />
                                    <input type="hidden" name="memberCode" id="memberCode" value="<%= _viewModel.MemberCode %>" />

                                    <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                                    <!--<span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>-->
                                    <span id="nicknameStatus" class="sr-only">(success)</span>
                                </div>

                                <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>
                            </div>
                        </div>
                    </div>
                    <!-- End Post -->

                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a href="#" id="nextStep" class="btn-system btn-medium">下一步</a>
                    <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Register") %>" class="btn-system btn-medium border-btn">取消</a>

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
            var $email = $('#email').val();
            if ($email == '') {
                alert('請填入email!!');
                return;
            }

          $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/CompleteRegister") %>')
            .submit();

        });
    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;
    RegisterViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<UserProfile>();
        _item = (UserProfile)this.Model;
        _viewModel = (RegisterViewModel)ViewBag.ViewModel;
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

</script>

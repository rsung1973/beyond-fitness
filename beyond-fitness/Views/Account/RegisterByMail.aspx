<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>
<%@ Register Src="~/Views/Shared/LockScreen.ascx" TagPrefix="uc1" TagName="LockScreen" %>



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

                    <!-- Start Contact Form -->
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>
                    <p><strong>會員編號：</strong><span class="text-primary"><%= _item.MemberCode %></span></p>

                    <!-- Divider -->
                    <div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <div class="form-group has-feedback">
                        <% Html.RenderInput("EMail：", "email", "email", "請輸入EMail", _modelState); %>
                    </div>

                    <div class="form-group has-feedback">
                        <% Html.RenderInput("暱稱：", "userName", "userName", "暱稱", _modelState); %>
                    </div>

                    <div class="form-group has-feedback">
                        <% Html.RenderInput("頭像：", "photopic", "photopic", "", _modelState, "file"); %>
                    </div>
                    <div class="author-image">
                        <img width="100" id="authorImg" alt="" src="<%= _item.PictureID.HasValue ? VirtualPathUtility.ToAbsolute("~/Information/GetResource/") + _item.PictureID : VirtualPathUtility.ToAbsolute("~/images/noMember.jpg") %>" />
                    </div>

                    <% Html.RenderPartial("~/Views/Shared/SetPassword.ascx"); %>
                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a  id="nextStep" class="btn-system btn-medium">下一步</a>
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

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/CompleteRegister") %>')
              .submit();

        });

        $('#email').rules('add', {
            'required': true,
            'email': true
        });

        var fileUpload = $('#photopic');
        var elmt = fileUpload.prev();

        fileUpload.off('click').on('change', function () {

            $('<form method="post" id="myForm" enctype="multipart/form-data"></form>')
            .append(fileUpload).ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>",
                data: {'memberCode':'<%= _item.MemberCode %>'},
                beforeSubmit: function () {
                    //status.show();
                    //btn.hide();
                    //console.log('提交時');
                },
                success: function (data) {
                    elmt.after(fileUpload);
                    if (data.result) {
                        $('#authorImg').prop('src','<%= VirtualPathUtility.ToAbsolute("~/Information/GetResource/") %>' + data.pictureID );
                    } else {
                        alert(data.message);
                    }
                    //status.hide();
                    //console.log('提交成功');
                },
                error: function () {
                    elmt.after(fileUpload);
                    //status.hide();
                    //btn.show();
                    //console.log('提交失败');
                }
            }).submit();
        });

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;
    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<UserProfile>();
        _item = (UserProfile)this.Model;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

</script>

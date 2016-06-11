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
                        <label class="control-label" for="nickname">Email：</label>
                        <input type="text" class="form-control" placeholder="Email" name="email" id="email" aria-describedby="emailStatus" required autofocus>
                        <input type="hidden" name="userID" id="userID" />
                        <input type="hidden" name="memberCode" id="memberCode" value="<%= _viewModel.MemberCode %>" />
                        <!--<span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                  <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>-->
                        <span id="emailStatus" class="sr-only">(success)</span>
                    </div>

                    <div class="form-group has-feedback">
                        <label class="control-label" for="userName">暱稱：</label>
                        <input type="text" class="form-control" placeholder="暱稱" name="userName" id="userName" aria-describedby="nicknameStatus">
                        <!--<span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                  <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>-->
                        <span id="nicknameStatus" class="sr-only">(success)</span>
                    </div>

                    <div class="form-group has-feedback">
                        <label class="control-label" for="nickname">頭像：</label>
                        <input type="file" class="form-control" name="photopic" id="photopic" aria-describedby="photopicStatus">
                        <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                        <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                        <span id="photopicStatus" class="sr-only">(success)</span>
                    </div>
                    <div class="author-image">
                        <img width="100" id="authorImg" alt="" src="<%= _item.PictureID.HasValue ? VirtualPathUtility.ToAbsolute("~/Information/GetResource/") + _item.PictureID : VirtualPathUtility.ToAbsolute("~/images/noMember.jpg") %>" />
                    </div>

                    <div class="tabs-section">
                        <!-- Nav Tabs -->
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-share-alt"></i>設定圖形密碼</a></li>
                            <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>設定文字密碼</a></li>
                        </ul>

                        <!-- Tab panels -->
                        <div class="tab-content">
                            <!-- Tab Content 1 -->
                            <div class="tab-pane fade in active" id="tab-1">
                                <uc1:LockScreen runat="server" ID="lockScreen" />
                            </div>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                            <div class="form-group has-feedback">
                                <label class="control-label" for="email">密碼：</label>
                                <input type="password" class="form-control" placeholder="密碼" name="password" id="password" aria-describedby="passwordStatus">
                                <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                                <span id="passwordStatus" class="sr-only">(success)</span>
                            </div>
                            <div class="form-group has-feedback">
                                <label class="control-label" for="email">請再輸入一次密碼：</label>
                                <input type="password" class="form-control" placeholder="再輸入一次密碼" name="password2" id="password2" aria-describedby="password2Status">
                                <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                                <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                                <span id="password2Status" class="sr-only">(success)</span>
                            </div>
                        </div>
                    </div>
                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <!-- End Tab Panels -->
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

            $('#userID').val($email);
            if ($('#tab-1').css('display') == 'block') {
                var userPath = $appLock.getUserPath();
                if (userPath == null) {
                    alert('請設定圖形密碼!!');
                    return;
                } else if (userPath.length < 9) {
                    alert('您設定圖形的密碼過短!!');
                    return;
                }
                $('#password').val(userPath);
            }

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/CompleteRegister") %>')
              .submit();

        });


        var fileUpload = $('#photopic');
        var elmt = fileUpload.prev();

        fileUpload.off('click').on('change', function () {

            $('<form method="post" id="myForm" enctype="multipart/form-data"></form>')
            .append(fileUpload).ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>",
                data: {'memberCode':'<%= _viewModel.MemberCode %>'},
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

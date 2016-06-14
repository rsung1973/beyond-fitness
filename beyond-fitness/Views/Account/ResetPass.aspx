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
                    <h4 class="classic-title"><span>重新設定密碼</span></h4>

                    <!-- Start Contact Form -->
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>
                    <p><strong>會員編號：</strong><span class="text-primary"><%= _item.MemberCode %></span></p>
                    <p><strong>Email：</strong><%= _item.EMail %></p>

                    <!-- Divider -->
                    <div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

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
                                <input type="hidden" name="lockScreen" style="display:none" />
                            </div>
                            <label id="lockScreen-error" class="error" for="lockScreen"></label>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                            <div class="form-group has-feedback">
                                <% Html.RenderPassword("密碼：", "password", "password", "密碼", _modelState); %>
                            </div>
                            <div class="form-group has-feedback">
                                <% Html.RenderPassword("請再輸入一次密碼：", "password2", "password2", "再輸入一次密碼", _modelState); %>
                            </div>
                        </div>
                    </div>
                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a id="nextStep" class="btn-system btn-medium">確定</a>

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

            if ($('#tab-1').css('display') == 'block') {
                var userPath = $appLock.getUserPath();
                if (userPath == null) {
                    $('#lockScreen-error').text('請您設圖形密碼!!');
                    return;
                } else if (userPath.length < 9) {
                    $('#lockScreen-error').text('您設定圖形的密碼過短!!');
                    return;
                } else {
                    var $lockScreen = $('input[name="lockScreen"]');
                    $lockScreen.val(userPath);
                }
            }

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/ResetPass") %>')
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
                password: {
                    required: {
                        param: true,
                        depends: function (elment) {
                            return $('input[name="lockScreen"]').val() == '';
                        }
                    },
                    maxlength: 20
                },
                password2: {
                    required: false,
                    confirmPassword: $('#password')
                }
            }
        });

        $(function () {
            $.validator.addMethod("confirmPassword", function (value, element, pwd) {
                return $(element).val() == pwd.val();
            }, "密碼確認錯誤!!");
        });

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;
    RegisterViewModel _viewModel;
    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<UserProfile>();
        _item = (UserProfile)this.Model;
        _viewModel = (RegisterViewModel)ViewBag.ViewModel;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }

</script>

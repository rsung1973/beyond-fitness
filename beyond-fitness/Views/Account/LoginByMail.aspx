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
                    <h4 class="classic-title"><span>登入</span></h4>

                    <!-- Start Contact Form -->
                    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
                    <div class="form-group has-feedback">
                        <% Html.RenderInput("EMail：", "pid", "pid", "請輸入EMail", null); %>
                        <input type="hidden" name="returnUrl" value="<%= Request["returnUrl"] %>" />
                    </div>

                    <div class="tabs-section">
                        <!-- Nav Tabs -->
                        <span style="float: right; margin-right: 10px;"><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/ForgetPassword") %>">忘記密碼 <span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span></a></span>
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-share-alt"></i>圖形密碼</a></li>
                            <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>文字密碼</a></li>
                        </ul>

                        <!-- Tab panels -->
                        <div class="tab-content">
                            <!-- Tab Content 1 -->
                            <div class="tab-pane fade in active" id="tab-1">
                                <uc1:LockScreen runat="server" ID="lockScreen" />
                            </div>
                            <label id="lockPattern-error" class="error" for="lockPattern" style="display: none;"></label>
                            <div class="tab-pane fade" id="tab-2">
                                <div class="form-group has-feedback">
                                    <% Html.RenderPassword("密碼：", "password", "password", "密碼", null); %>
                                </div>
                            </div>
                        </div>
                        <!-- Tab Content 2 -->
                    </div>
                    <!-- End Tab Panels -->
                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <button type="button" id="btnLogin" class="btn btn-warning btn-lg btn-block"><i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;&nbsp;使用 Email 登入</button>
                </div>

                <!-- End Contact Form -->
            </div>

        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#btnLogin').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= FormsAuthentication.LoginUrl %>')
              .submit();

        });

        $(function () {

            $formValidator.settings.submitHandler = function (form) {

                if ($('#tab-1').css('display') == 'block') {
                    var userPath = $appLock.getUserPath();
                    if (userPath == null) {
                        $('#lockPattern-error').css('display', 'block');
                        $('#lockPattern-error').text('請輸入圖形密碼!!');
                        return false;
                    } else {
                        $('#password').val(userPath);
                    }
                }

                //$(form).submit();
                return true;
            };

            $('#password').rules('add', {
                'required': true,
                'maxlength': 20
            });

            $('#pid').rules('add', {
                'required': true,
                'email': true
            });
        });


    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (UserProfile)this.Model;
    }

</script>

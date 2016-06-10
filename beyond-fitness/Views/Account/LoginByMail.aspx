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
    <!-- lockScreen CSS Styles  -->
    <link rel="stylesheet" href="<%= VirtualPathUtility.ToAbsolute("~/lockScreen/app.css") %>" type="text/css" />

    <script src="<%= VirtualPathUtility.ToAbsolute("~/lockScreen/kinetic-v4.6.js") %>"></script>
    <script src="<%= VirtualPathUtility.ToAbsolute("~/lockScreen/html5-android-pattern-lockScreen.js") %>"></script>

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

                    <div class="form-group has-feedback">
                        <label class="control-label" for="email">Email：</label>
                        <input type="email" class="form-control" placeholder="請輸入Email" name="PID" id="email" aria-describedby="emailStatus" required autofocus>
                        <input type="hidden" name="returnUrl" value="<%= Request["returnUrl"] %>" />
                        <span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>
                        <!--<span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>-->
                        <span id="emailStatus" class="sr-only">(success)</span>
                    </div>

                    <div class="tabs-section">
                        <!-- Nav Tabs -->
                        <span style="float: right; margin-right: 10px;"><a href="forgetpw.htm">忘記密碼 <span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span></a></span>
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-share-alt"></i>圖形密碼</a></li>
                            <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>文字密碼</a></li>
                        </ul>

                        <!-- Tab panels -->
                        <div class="tab-content">
                            <!-- Tab Content 1 -->
                            <div class="tab-pane fade in active" id="tab-1">
                                <button id="reset-button" class="button blue">Reset</button>
                                <div class="hr1" style="margin: 5px 0px;"></div>
                                <div id="lock-screen-container">
                                    <div id="lock-screen" />
                                </div>
                            </div>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                            <div class="form-group has-feedback">
                                <label class="control-label" for="email">Password：</label>
                                <input type="password" class="form-control" placeholder="Password" name="Password" id="password" aria-describedby="passwordStatus">
                                <!--<span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>-->
                                <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                                <span id="passwordStatus" class="sr-only">(success)</span>
                            </div>
                        </div>
                    </div>
                    <!-- End Tab Panels -->
                </div>

                <div class="hr1" style="margin: 5px 0px;"></div>
                <button type="button" id="btnLogin" class="btn btn-warning btn-lg btn-block"><i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;&nbsp;使用 Email 登入</button>
                <!-- End Contact Form -->
            </div>

        </div>
    </div>
    <!-- End content -->


    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#btnLogin').on('click', function (evt) {
            var $email = $('#email').val();
            if ($email == '') {
                alert('請填入email!!');
                return;
            }

            if ($('#tab-1').css('display') == 'block') {
                var userPath = $appLock.getUserPath();
                if (userPath == null) {
                    alert('請輸入圖形密碼!!');
                    return;
                }
                $('#password').val(userPath);
            }

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/Login") %>')
              .submit();

        });

    </script>

    </script>
    <% if (ViewBag.Message != null)
        { %>
    <script>
        $(function () {
            alert('<%= ViewBag.Message %>');
        });
    </script>
    <%  } %>

    </div>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
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

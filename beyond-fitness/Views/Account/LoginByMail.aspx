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


<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <!-- RIBBON -->
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-sign-in"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>登入</li>
        </ol>
        <!-- end breadcrumb -->

        <!-- You can also add more buttons to the
				ribbon for further usability

				Example below:

				<span class="ribbon-button-alignment pull-right">
				<span id="search" class="btn btn-ribbon hidden-xs" data-title="search"><i class="fa-grid"></i> Change Grid</span>
				<span id="add" class="btn btn-ribbon hidden-xs" data-title="add"><i class="fa-plus"></i> Add</span>
				<span id="search" class="btn btn-ribbon" data-title="search"><i class="fa-search"></i> <span class="hidden-mobile">Search</span></span>
				</span> -->

    </div>
    <!-- END RIBBON -->
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
    <h1 class="page-title txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-sign-in"></i>登入
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <!-- NEW COL START -->
        <article class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget" id="wid-id-6" data-widget-editbutton="false" data-widget-custombutton="false" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
                <!-- widget options:
                                    usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
                                    
                                    data-widget-colorbutton="false" 
                                    data-widget-editbutton="false"
                                    data-widget-togglebutton="false"
                                    data-widget-deletebutton="false"
                                    data-widget-fullscreenbutton="false"
                                    data-widget-custombutton="false"
                                    data-widget-collapsed="true" 
                                    data-widget-sortable="false"
                                    
                                -->
                <header>
                    <span class="widget-icon"><i class="fa fa-edit"></i></span>
                    <h2>填寫相關資訊 </h2>

                </header>

                <!-- widget div-->
                <div>

                    <!-- widget content -->
                    <div class="widget-body bg-color-darken txt-color-white no-padding">

                        <form action="<%= FormsAuthentication.LoginUrl %>" id="login-form" class="smart-form" method="post">
                            <%= Html!=null ? Html.AntiForgeryToken() : null %>
                            <fieldset>
                                <section>
                                    <label class="input">
                                        <i class="icon-append fa fa-envelope-o "></i>
                                        <input class="form-control input-lg" maxlength="30" placeholder="請輸入註冊時的E-mail" type="email" name="PID" id="PID"/>
                                        <input type="hidden" name="returnUrl" value="<%= Request["returnUrl"] %>" />
                                    </label>
                                </section>

                            </fieldset>
                            <fieldset>
                                <ul id="myTab1" class="nav nav-tabs bordered">
                                    <li class="active">
                                        <a href="#pw1" data-toggle="tab"><i class="fa fa-picture-o  fa-lg fa-gear"></i>圖形密碼</a>
                                    </li>
                                    <li>
                                        <a href="#pw2" data-toggle="tab"><i class="fa fa-keyboard-o  fa-lg fa-gear"></i>文字密碼</a>
                                    </li>
                                </ul>
                                <div id="myTabContent1" class="tab-content padding-10">

                                    <div class="tab-pane fade in active" id="pw1">
                                        <uc1:LockScreen runat="server" ID="lockScreen" />
                                        <label id="lockPattern-error" class="error" for="lockPattern" style="display: none;"></label>
                                    </div>
                                    
                                    <div class="tab-pane fade" id="pw2">
                                        <fieldset>
                                            <section>
                                                <label class="input">
                                                    <i class="icon-append fa fa-lock "></i>
                                                    <input class="form-control input-lg" maxlength="10" placeholder="請輸入註冊時的密碼" type="password" name="password" id="password"/>
                                                </label>
                                            </section>
                                        </fieldset>
                                    </div>
                                </div>
                            </fieldset>

                            <footer class="text-right">
                                <button type="submit" id="btnLogin" name="submit" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                            </footer>
                        </form>

                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->
            </div>
        </article>

        <!-- NEW COL START -->
        <% Html.RenderPartial("~/Views/Layout/QuickLink.ascx"); %>
        <!-- END COL -->
    </div>

    <script>

        $('#btnLogin').on('click', function (evt) {
            startLoading();
        });


        $(function () {

            var $commentForm = $("#login-form").validate({
                // Rules for form validation
                rules: {
                    PID: {
                        required: true,
                        email: true
                    },
                    password: {
                        required: true,
                        'maxlength': 20
                    }
                },

                // Messages for form validation
                messages: {
                    PID: {
                        required: '請輸入您的 email address',
                        email: '請輸入合法的 email address'
                    },
                    password: {
                        required: '請輸入您的文字密碼',
                    }
                },

                // Ajax form submition
                submitHandler: function (form) {
                    if ($('#pw1').css('display') == 'block') {
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

                    //$(form).ajaxSubmit({
                    //    success: function () {
                    //        $("#login-form").addClass('submited');
                    //    }
                    //});
                },

                // Do not change code below
                errorPlacement: function (error, element) {
                    error.insertAfter(element.parent());
                }
            });

        });

    </script>


</asp:Content>

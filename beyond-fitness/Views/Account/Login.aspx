<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

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
                    <h4 class="classic-title"><span>登入</span></h4>

                    <!-- Start Contact Form -->
                    <p>若您已有<span class="orange-text">會員編號</span>，第一次登入請先註冊 <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Register") %>" class="btn-system btn-small"><i class="glyphicon glyphicon-user" aria-hidden="true"></i>&nbsp;&nbsp;註冊</a></p>
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <button type="button" id="btnFB" class="btn btn-primary btn-lg btn-block"><i class="fa fa-facebook-square" aria-hidden="true"></i>&nbsp;&nbsp;使用 Facebook 登入</button>
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                    <button type="button" class="btn btn-warning btn-lg btn-block" onclick="location.href='<%= VirtualPathUtility.ToAbsolute("~/Account/LoginByMail") %>';"><i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;&nbsp;使用 Email 登入</button>
                    <!-- End Contact Form -->
                </div>

            </div>
        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        function fbSignOn() {
            FB.login(function (response) {
                statusChangeCallback(response);
            }, { scope: 'public_profile,email' });
        }

        $('#btnFB').on('click', function (evt) {
            fbSignOn();
        });
    </script>

    <script>

        function statusChangeCallback(response) {

            console.log('statusChangeCallback');
            console.log(JSON.stringify(response));

            if (response.status === 'connected') {
                // Logged into your app and Facebook.
                loginByFB(response.authResponse);

            } else if (response.status === 'not_authorized') {
                // The person is logged into Facebook, but not your app.
                loginByFB(response.authResponse);

            } else {
                // The person is not logged into Facebook, so we're not sure if
                // they are logged into this app or not.
                //document.getElementById('status').innerHTML = 'Please log ' +
                //  'into Facebook.';
            }
        }

        // This function is called when someone finishes with the Login
        // Button.  See the onlogin handler attached to it in the sample
        // code below.
        function checkLoginState() {
            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
            });
        }

        window.fbAsyncInit = function () {
            FB.init({
                appId: '1027299250717597', //'1552642415030511',
                cookie: true,  // enable cookies to allow the server to access
                // the session
                xfbml: true,  // parse social plugins on this page
                version: 'v2.2' // use version 2.2
            });

            // Now that we've initialized the JavaScript SDK, we call
            // FB.getLoginStatus().  This function gets the state of the
            // person visiting this page and can return one of three states to
            // the callback you provide.  They can be:
            //
            // 1. Logged into your app ('connected')
            // 2. Logged into Facebook, but not your app ('not_authorized')
            // 3. Not logged into Facebook and can't tell if they are logged into
            //    your app or not.
            //
            // These three cases are handled in the callback function.

            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
            });

        };

        // Load the SDK asynchronously
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        // Here we run a very simple test of the Graph API after login is
        // successful.  See statusChangeCallback() for when this call is made.
        function loginByFB(auth) {

            $.post('<%= VirtualPathUtility.ToAbsolute("~/Account/LoginByFB") %>', auth, function (data) {
                if (data.result) {
                    window.location.href = data.url;
                } else {
                    alert(data.message);
                }
            });

        }
    </script>


</asp:Content>

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
                    <h4 class="classic-title"><span>註冊 - Step 1</span></h4>

                    <!-- Start Contact Form -->
                    <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="vipno">會員編號：</label>
                            <input type="text" class="form-control" placeholder="會員編號" name="memberCode" id="vipno" class="form-control" required autofocus aria-describedby="vipnoStatus" />
                            <input type="hidden" name="userName" id="userName" />
                            <input type="hidden" name="userID" id="userID" />
                            <input type="hidden" name="email" id="email" />
                            <!--<span class="glyphicon glyphicon-ok form-control-feedback text-success" aria-hidden="true"></span>-->
                            <span class="glyphicon glyphicon-remove form-control-feedback text-danger" aria-hidden="true"></span>
                            <span id="vipnoStatus" class="sr-only">(學員編號不存在)</span>
                        </div>


                        <button type="button" class="btn btn-primary btn-lg btn-block" id="btnFB"><i class="fa fa-facebook-square" aria-hidden="true"></i>&nbsp;&nbsp;使用 Facebook</button>
                        <div class="tabs-section">


                            <div class="hr1" style="margin: 5px 0px;"></div>
                            <button type="button" id="btnMail" class="btn btn-warning btn-lg btn-block"><i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;&nbsp;使用 Email 登入</button>

                        </div>
                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->
    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        var fbLogined = false;
        var memberCodeChecked = false;

        function checkMemberCode(channel) {

            var $memberCode = $('#vipno').val();
            if ($memberCode == '') {
                alert('請輸入學員編號!!');
                return;
            }

            $.post('<%= VirtualPathUtility.ToAbsolute("~/Account/CheckMemberCode") %>', { 'memberCode': $memberCode }, function (data) {
                if (data.result) {
                    memberCodeChecked = true;
                    if (channel == 'fb') {
                        if (!fbLogined) {
                            fbSignOn();
                        } else {
                            registerByFB();
                        }
                    } else {
                        registerByMail();
                    }
                } else {
                    alert(data.message);
                }
            });
        }

        function fbSignOn() {
            FB.login(function (response) {
                statusChangeCallback(response);
                if (fbLogined) {
                    if (memberCodeChecked) {
                        registerByFB();
                    }
                }
            }, { scope: 'public_profile,email' });
        }

        $('#btnFB').on('click', function (evt) {

            if (!memberCodeChecked) {
                checkMemberCode('fb');
            } else if (fbLogined) {
                registerByFB();
            } else {
                fbSignOn();
            }
        });

        $('#btnMail').on('click', function (evt) {
            if (!memberCodeChecked) {
                checkMemberCode('mail');
            } else {
                registerByMail();
            }
        });

        function registerByMail() {

          $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/RegisterByMail") %>')
            .submit();

        }


    </script>
    <% if (ViewBag.Message != null)
        { %>
    <script>
        $(function () {
            alert('<%= ViewBag.Message %>');
        });
    </script>
    <%  } %>

    <script>
        // This is called with the results from from FB.getLoginStatus().
  function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(JSON.stringify(response));
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
      // Logged into your app and Facebook.
        fbLogined = true;
    } else if (response.status === 'not_authorized') {
      // The person is logged into Facebook, but not your app.
      //document.getElementById('status').innerHTML = 'Please log ' +
      //  'into this app.';
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
    FB.getLoginStatus(function(response) {
      statusChangeCallback(response);
    });
  }

  window.fbAsyncInit = function() {
  FB.init({
      appId: '1027299250717597', //'1552642415030511',
    cookie     : true,  // enable cookies to allow the server to access
                        // the session
    xfbml      : true,  // parse social plugins on this page
    version    : 'v2.2' // use version 2.2
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

  FB.getLoginStatus(function(response) {
    statusChangeCallback(response);
  });

  };

  // Load the SDK asynchronously
  (function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
  }(document, 'script', 'facebook-jssdk'));

  // Here we run a very simple test of the Graph API after login is
  // successful.  See statusChangeCallback() for when this call is made.
  function registerByFB() {
      console.log('Welcome!  Fetching your information.... ');
      FB.api('/me/picture', function (response) {
          $.post('<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>', { 'memberCode': $('#vipno').val(), 'imgUrl': response.data.url }, function (data) {
              if (!data.result) {
                  alert(data.message);
              } else {

              }
          });
      });

      FB.api('/me?fields=id,name,email', function (response) {
          $('#userName').val(response.name);
          $('#userID').val(response.id);
          $('#email').val(response.email);
          $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/RegisterByFB") %>')
            .submit();
          //console.log('Successful login for: ' + response.name);
          //document.getElementById('status').innerHTML =
          //  'Thanks for logging in, ' + response.name + '!';
      });
  }
    </script>

</asp:Content>

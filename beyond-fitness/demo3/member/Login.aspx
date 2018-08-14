﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Timeline" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Register Src="~/Views/CornerKick/Module/UserGuideNotice.ascx" TagPrefix="uc1" TagName="UserGuideNotice" %>
<!DOCTYPE html>
<html>
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS - 登入</title>
      <!-- CSS  -->
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
      <!-- materialize  -->
      <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
      <!-- livIconsevo  -->
      <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
      <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
      <!-- patternLock  -->
      <link href='css/patternLock/patternLock.css?1' rel='stylesheet' type='text/css'>
      <!-- scrollup-master  -->
      <link href="css/scrollup-master/themes/image.css?1.1" rel="stylesheet" id="scrollUpTheme">
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css?1" type="text/css" rel="stylesheet" media="screen,projection"/>
                 <link rel="icon" href="favicons/favicon_96x96.png">
      <!-- Specifying a Webpage Icon for Web Clip -->
      <link rel="apple-touch-icon-precomposed" href="favicons/favicon_57x57.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="favicons/favicon_72x72.png">
      <link rel="apple-touch-icon-precomposed" sizes="114x114" href="favicons/favicon_114x114.png">
      <link rel="apple-touch-icon-precomposed" sizes="144x144" href="favicons/favicon_144x144.png">
      <link rel="apple-touch-icon-precomposed" sizes="180x180" href="favicons/favicon_180x180.png">       

   </head>
   <body>
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /--> 
      <div class="wrapper home-fixed mode-default">
         <div class="wrapper-fixed">
            <!--Header --> 
            <!-- <nav class="white" role="navigation">
               <div class="nav-wrapper container"> <a id="logo-container" href="#" class="brand-logo center">BEYOND FITNESS</a> 
               </div>
               </nav>--> 
            <!-- // End of Header --> 
            <!-- main -->
            <div class="main">
               <div class="container">
                  <!--品牌LOGO -->
                  <div class="brand-wrapper">
                     <div class="brand-area"> <img src="images/logo-black.png" alt="BEYOND FITNESS" class="responsive-img"> </div>
                  </div>
                  <!-- // End of 品牌LOGO -->
                  <div class="registered-forms">
                     <!-- 登入 - TAB -->
                     <ul id="tabs-swipe-demo" class="tabs col s12">
                        <li class="tab col s6"><a class="active" onclick="gtag('event', '一般登入', { 'event_category': '頁籤點擊','event_label': '登入'});" href="#test-swipe-2" id="graphTab">快速登入</a></li>
                        <li class="tab col s6"><a onclick="gtag('event', '快速登入', { 'event_category': '頁籤點擊', 'event_label': '登入'});" href="#test-swipe-1" id="alphaTab">一般登入</a></li>
                     </ul>
                     <div id="test-swipe-1" class="tab-content col s12">
                        <form id="loginForm1">
                           <div class="row">
                              <!--1-->
                              <div class="input-field col s12">
                                 <i class="livicon-evo prefix" data-options="name: envelope-put.svg; size: 30px; style: lines; strokeColor:#05232d; autoPlay:true"></i>
                                 <input type="text" id="email1" name="PID" maxlength="32" data-length="32"/>
                                 <label for="email1">請輸入電子郵件信箱</label>
                              </div>
                              <!--2-->
                              <div class="input-field col s12"> 
                                 <i class="livicon-evo prefix" data-options="name: lock.svg; size: 30px; style: lines;  strokeColor:#05232d; autoPlay:true"></i>
                                 <input id="password" type="password" maxlength="10" name="Password"/>
                                 <label for="password">請輸入英數字密碼</label>
                                 <%--<span class="help-error-text">您輸入的資料錯誤，請確認後再重新輸入</span>--%>
                              </div>
                              <span class="memo-text right">＊<a href="javascript:gtag('event', '重設密碼', {  'event_category': '連結點擊',  'event_label': '登入'});window.location.href = '<%= Url.Action("ForgetPassword","CornerKick") %>';">忘記密碼</a>？</span> 
                           </div>
                        </form>
                        <!-- Button -->
                        <div class="content-area">
                           <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="signOn($('#loginForm1'));">登入</button>
                        </div>
                        <!--// End of button--> 
                     </div>
                     <div id="test-swipe-2" class="tab-content col s12">
                        <form id="loginForm2" class="">
                           <div class="row">
                              <!--1-->
                              <div class="input-field col s12">
                                 <i class="livicon-evo prefix" data-options="name: envelope-put.svg; size: 30px; style: lines;  strokeColor:#05232d; autoPlay:true"></i>
                                 <input type="text" id="email2" name="PID" maxlength="32" data-length="32" />
                                 <label for="email2">請輸入電子郵件信箱</label>
                                 <%--<span class="help-error-text">您輸入的資料錯誤，請確認後再重新輸入</span>--%>
                              </div>
                              <!--2-->
                              <div class="input-field col s12">
                                 <p class="memo-text center">請輸入圖形密碼</p>
                                 <div id="patternContainer"></div>
                              </div>
                              <span class="memo-text right">＊<a onclick="gtag('event', '登入', { 'event_category': '按鈕點擊', 'event_label': '快速登入' });" href="<%= Url.Action("ForgetPassword","CornerKick") %>">忘記密碼</a>？</span> 
                           </div>
                        </form>
                     </div>
                  </div>
                  <!-- //End of 登入 - TAB --> 
               </div>
            </div>
            <!-- // End of main --> 
         </div>
         <!--// End of wrapper-fixed--> 
         <!-- Botton -->
         <!--<div class="bottom-area">
            <button class="btn waves-effect waves-light btn-send" type="submit" name="action" onclick="javascript:window.location.assign('index-girl.html');">登  入</button>            
            </div>-->
      </div>
      <!--// End of wrapper--> 
      <!-- Footer --> 
      <!--<footer class="page-footer teal">
         <div class="footer-copyright">
           <div class="container"><a class="brown-text text-lighten-3" href="#">BEYOND FITNESS</a> </div>
         </div>
         </footer>--> 
      <!-- // End of Footer --> 
      <!--  Scripts--> 
      <script src="js/libs/jquery-2.2.4.min.js"></script> 
      <script src="js/materialize.js"></script> 
      <script src="js/init.js"></script>
      <!-- LivIconsEvo  -->
      <script src="js/plugin/LivIconsEvo/tools/snap.svg-min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/TweenMax.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/DrawSVGPlugin.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/MorphSVGPlugin.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/verge.min.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.Tools.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.defaults.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.js"></script> 
      <!-- patternLock  -->
      <script src="js/plugin/patternLock/patternLock.min.js"></script>        
      <!-- scrollup-master  -->
      <script src="js/plugin/scrollup-master/jquery.scrollUp.min.js"></script>
      <script>
          var lock = new PatternLock("#patternContainer", {
              onDraw: function (pattern) {

                  var pwd = [];
                  pattern.split('').forEach(function (n) {
                      pwd.push(Number(n) - 1);
                  });
                  signOn($('#loginForm2'), pwd.join('-'));

              },
              allowRepeat: true
          });
         
         $(function () {
            $.scrollUp({
                animation: 'fade',
                scrollImg: {
                    active: true,
                    type: 'background',
                    src: '../images/top.png'
                }
            });
         });
          
         $('.bottom-area').hide();
         $('#alphaTab').click(function() {
              $('.bottom-area').show();
         });  
         
         $('#graphTab').click(function() {
              $('.bottom-area').hide();
         });              
      </script>
   </body>
</html>
<script>
   function signOn($form,lockPattern) {
       var $formData = $form.serializeObject();
       if (lockPattern != null) {
           $formData.Password = lockPattern;
           gtag('event', '登入', { 'event_category': '按鈕點擊', 'event_label': '快速登入' });
       }
       else {
           gtag('event', '登入', { 'event_category': '按鈕點擊', 'event_label': '一般登入' });
       }
       clearErrors();
       showLoading();
       $('').launchDownload('<%= Url.Action("SignOn", "CornerKick") %>',$formData);
   }
</script>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  Html.RenderAction("AutoLogin", "CornerKick"); %>
<%  Html.RenderPartial("~/Views/Shared/Materialize/ReportInputError.ascx"); %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
    }

</script>
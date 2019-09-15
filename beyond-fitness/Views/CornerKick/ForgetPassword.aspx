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
      <title>BEYOND FITNESS - 忘記密碼</title>
      <!-- CSS  -->
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
      <!-- materialize  -->
      <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
      <!-- livIconsevo  -->
      <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
      <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
      <!-- scrollup-master  -->
      <link href="css/scrollup-master/themes/image.css?1.1" rel="stylesheet" id="scrollUpTheme">
      
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css?1.2" type="text/css" rel="stylesheet" media="screen,projection"/>
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
            <!-- main-->
            <div class="main">
               <div class="container">
                  <!--品牌LOGO -->
                  <div class="brand-wrapper">
                     <div class="brand-area"> <img src="images/logo-black.png" alt="BEYOND FITNESS" class="responsive-img"> </div>
                  </div>
                  <!-- // End of 品牌LOGO --> 
                  <!-- 表單 -->
                  <div class="pw-forms">
                     <div class="row">
                        <form class="col s12">
                           <div class="row">
                              <!--1-->
                              <div class="input-field col s12">
                                 <i class="livicon-evo prefix" data-options="name: envelope-put.svg; size: 30px; style: lines; strokeColor:#05232d; autoPlay:true"></i>
                                 <textarea id="icon_prefix2" class="materialize-textarea" maxlength="30" data-length="30" name="email"></textarea>                                 
                                 <label for="icon_prefix2">請輸入電子郵件信箱<br/>ex：info@beyond-fitness.tw</label>
                                 <span class="memo-text">系統將會發送驗證碼到您的電子郵件信箱，透過驗證連結進行驗證。</span>                                 
                              </div>
                           </div>
                        </form>
                     </div>
                  </div>
                  <!-- // End of 表單 --> 
               </div>
            </div>
            <!-- // End of main -->
         </div>
         <!--// End of wrapper-fixed--> 
         <!-- Botton -->
         <div class="bottom-area">
            <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="commitToReset();">驗證聯絡信箱</button>            
         </div>
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
        
        
          
      </script>
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<script>
    function commitToReset() {
        clearErrors();
        gtag('event', '驗證信箱', {
            'event_category': '按鈕點擊',
            'event_label': '重設密碼'
        });
        showLoading();
        $.post('<%= Url.Action("CommitToForgetPassword", "CornerKick") %>', {'email':$('textarea[name="email"]').val()}, function (data) {
            hideLoading();
            if ($.isPlainObject(data)) {
                alert(data.message);
            } else {
                $(data).appendTo($('body')).remove();
            }
        });
    }
</script>
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
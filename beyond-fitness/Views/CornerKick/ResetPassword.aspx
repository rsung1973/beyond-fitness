﻿<%@  Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<!DOCTYPE html>
<html>
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS - 修改密碼</title>
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
      <div class="wrapper mode-default">
         <div class="wrapper-fixed login-wrap">
            <!--Header -->
            <nav class="non-line white" role="navigation">
               <div class="nav-wrapper container">
                  <!-- BACK --> 
                  <%  Html.RenderPartial("~/Views/CornerKick/Module/ReturnHome.ascx",model:Url.Action("Settings","CornerKick",new { learnerSettings = true })); %>
                  <!-- // End of BACK --> 
                  <a id="logo-container" href="#" class="brand-logo toptitle center" >修改密碼</a>
               </div>
            </nav>
            <!-- // End of Header --> 
            <!-- main-->
            <div class="main">
               <div class="container">
                  <!--品牌LOGO -->
                  <!-- // End of 品牌LOGO --> 
                  <!-- 表單 -->
                  <div class="pw-forms">
                     <div class="row">
                        <form id="queryForm" class="col s12">
                           <div class="row">
                              <!-- Radio -->
                              <div class="col s12 registered-radio">
                                 <ul>
                                    <li>
                                       <input class="with-gap" name="group1" type="radio" id="patternlogin" value="graph" checked/>
                                       <label for="patternlogin">快速登入</label>
                                    </li>
                                    <li>
                                       <input class="with-gap" name="group1" type="radio" id="alphalogin" value="alpha"/>
                                       <label for="alphalogin">一般登入</label>
                                    </li>
                                 </ul>
                              </div>
                              <!--3-->
                              <div id="type-swipe-alpha" class="input-field col s12"> 
                                 <i class="livicon-evo prefix" data-options="name: lock.svg; size: 30px; style: lines; strokeColor:#05232d; autoPlay:true"></i>
                                 <input id="passwd" type="password" maxlength="10" name="Password" data-length="10"/>
                                 <label for="passwd">請設定6-10位英數字密碼</label>
                              </div>
                              <!--4-->
                              <div id="type-swipe-pattern" class="input-field col s12">
                                 <p class="memo-text center">請設定圖形密碼</p>
                                 <div id="patternContainer"></div>
                              </div>
                           </div>
                            <input type="hidden" name="PID" value="<%= _profile?.PID %>" />
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
             <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="resetPassword();">確 定</button>
         </div>
         <!-- // End of Botton -->
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
                  clearErrors();
                  if (pattern.length < 6 || pattern.length > 12) {
                      alert('圖形密碼長度須為6~12個連續點位');
                      lock.reset();
                      $('input[name="Password"]').val('');
                  } else {
                      //alert(pattern);
                      var pwd = [];
                      pattern.split('').forEach(function (n) {
                          pwd.push(Number(n) - 1);
                      });
                      resetPassword(pwd.join('-'));
                      //window.location.href = 'personal-settings-line.html';
                  }
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
         
         
          
         $("#type-swipe-alpha").hide();
         $("#type-swipe-pattern").show();  
         $('.bottom-area').hide();
         $('input:radio[name="group1"]').click(function() {
              if ($(this).val() =='alpha') {
                  $("#type-swipe-alpha").show();
                  $("#type-swipe-pattern").hide();   
                  $('.bottom-area').show();
              } else {
                  $("#type-swipe-alpha").hide();
                   $("#type-swipe-pattern").show();
                  $('.bottom-area').hide();
              }
           });
      </script>      
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<script>

    function resetPassword(lockPattern) {
       var $formData = $('#queryForm').serializeObject();
       if (lockPattern != null)
           $formData.Password = lockPattern;
       $formData.UUID = '<%= _viewModel?.UUID %>';
       clearErrors();
       showLoading();
       $.post('<%= Url.Action("CommitPassword", "CornerKick",new { keyID = _profile.UID.EncryptKey() }) %>', $formData, function (data) {
           hideLoading();
           if ($.isPlainObject(data)) {
               if (data.result) {
                   alert('密碼已更新!!');
                   window.location.href = '<%= Url.Action("Settings","CornerKick",new { learnerSettings = true }) %>';
               } else {
                   lock.reset();
                   $('input[name="Password"]').val('');
               }
           } else {
               $(data).appendTo($('body'));
               lock.reset();
               $('input[name="Password"]').val('');
           }
       });
   }

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _profile;
    RegisterViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _profile = (UserProfile)this.Model;
        _viewModel = ViewBag.ViewModel as RegisterViewModel;
    }

</script>
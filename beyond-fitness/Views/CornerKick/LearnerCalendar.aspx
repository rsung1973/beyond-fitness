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
      <title>BEYOND FITNESS - 我的行事曆</title>
      <!-- CSS  -->
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
      <!-- materialize  -->
      <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
      <!-- livIconsevo  -->
      <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
      <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
      <!-- scrollup-master  -->
      <link href="css/scrollup-master/themes/image.css?1.1" rel="stylesheet" id="scrollUpTheme">
      
      <!-- jalendar  -->
      <link href="css/jalendar/jalendar.css" rel="stylesheet" type="text/css" />
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css?1.2" type="text/css" rel="stylesheet" media="screen,projection"/>
                 <link rel="icon" href="favicons/favicon_96x96.png">
      <!-- Specifying a Webpage Icon for Web Clip -->
      <link rel="apple-touch-icon-precomposed" href="favicons/favicon_57x57.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="favicons/favicon_72x72.png">
      <link rel="apple-touch-icon-precomposed" sizes="114x114" href="favicons/favicon_114x114.png">
      <link rel="apple-touch-icon-precomposed" sizes="144x144" href="favicons/favicon_144x144.png">
      <link rel="apple-touch-icon-precomposed" sizes="180x180" href="favicons/favicon_180x180.png">       

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
      <!-- scrollup-master  -->
      <script src="js/plugin/scrollup-master/jquery.scrollUp.min.js"></script>
      <!-- jalendar  -->
      <script src="js/plugin/jalendar/jalendar.js"></script>
    <script>
        var $global = {
            'onReady': [],
            call: function (name) {
                var fn = $global[name];
                if (typeof fn === 'function') {
                    fn();
                }
            },
        };
    </script>
   </head>
   <body>
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /  若需背景色設定在後面加上-->
      <div class="wrapper light-gray ">
         <div class="wrapper-fixed">
            <!--Header -->
            <!--//深藍背景，可用於新增行事曆與個人設定頁 /--> 
            <nav class="dark-bg non-line" role="navigation">
               <div class="nav-wrapper container">
                  <!-- BACK --> 
                  <%  Html.RenderPartial("~/Views/CornerKick/Module/ReturnHome.ascx"); %>
                  <!-- // End of BACK --> 
                  <a id="logo-container" href="#" class="brand-logo toptitle center" >我的行事曆</a>
                  <a class="btn waves-effect waves-light right btn-add" onclick="gtag('event', '新增行事曆', { 'event_category': '按鈕點擊', 'event_label': '我的行事曆'});" href="<%= Url.Action("EditUserEvent","CornerKick") %>">＋新增</a>
               </div>
            </nav>
            <!-- // End of Header -->         
            <!-- 新增行事曆面板 -->
            <div class="calendar-panel">
               <form class="col s12">
                  <div class="row">
                     <div id="mycalendar" class="jalendar">
                        <!-- 若要在放上日期與教練地點 架構為 <a> <span class="right">日期</span> </a> <p>教練 / 地點</p> -->
                        <%  Html.RenderPartial("~/Views/CornerKick/Module/LearnerEvents.ascx", _model); %>
<%--                         <input class="first-range-data" type="hidden" value="2018-07-12"/>
                         <input class="last-range-data" type="hidden" value="2018-07-21"/>--%>
<%--                         <input type="hidden" class="data1" value="2018-07-12"/>
                         <input type="hidden" class="data2" value="2018-07-21"/>--%>
                     </div>
                  </div>
               </form>
            </div>
            <!-- // End of 新增行事曆面板 -->
            <!-- main-->
            <!-- // End of main --> 
         </div>
         <!--// End of wrapper-fixed--> 
         <!-- Botton -->
         <!--<div class="bottom-area">
            <button class="btn waves-effect waves-light btn-send" type="submit" name="action" onclick="javascript:window.location.assign('calendar-add.html');">＋新增行事曆</button>
            </div>-->
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
         
          
          
         //debugger;
         var $calendar = $('#mycalendar').jalendar({
             color: '#ffffff', // Unlimited
             color2: '#ffffff', // Unlimited
             lang: 'EN',
             dateType: 'yyyy-mm-dd',
             sundayStart: false,
             //type: 'range',
             eventH3Color: '#4a4a4a',
             eventColor1: '#F5A623', // //P.T / P.I / S.T/ 多種混合
             eventColor2: '#7ED321', //自主訓練(暫不使用) 
             eventColor3: '#F00A68', //體驗訓練
             eventColor4: '#4A90E2', //自訂行事曆
             eventColor5: '#8B572A', //自訂行事曆
             eventColor6: '#BD10E0', //多種混合行事曆(暫不使用) 
             weekColor: '#000000',
             monthChanged: function (year, month, callback) {
                 $calendar.find('div.added-event').remove();
                 var dateFrom = year+'/'+(month+1)+'/01';
                 showLoading();
                 $.post('<%= Url.Action("LearnerEvents", "CornerKick") %>', { 'dateFrom': dateFrom }, function (data) {
                     hideLoading();
                     if ($.isPlainObject(data)) {
                         alert(data.message);
                     } else {
                         $(data).appendTo($calendar);
                         callback();
                     }
                 });
             },
             onEvent: 'showUserEvent',
         });

          function showUserEvent(eventID) {
              var event = event || window.event;
              event.preventDefault();
              gtag('event', '課表內容', { 'event_category': '連結點擊', 'event_label': '我的行事曆' });
              showLoading();
              $('').launchDownload('<%= Url.Action("ShowLearnerEvent","CornerKick") %>', { 'eventID': eventID });
              return false;
          }
      </script>
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  Html.RenderPartial("~/Views/CornerKick/Module/ModeGirls.ascx",_model); %>
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
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
<!doctype html>
<html class="no-js">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Fira+Sans:400,300,700" rel="stylesheet">
    <!-- materialize  -->
    <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <!-- livIconsevo  -->
    <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
    <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
    <!-- slider-master  -->
    <link href="css/slider-master/slider-master.css?1" rel="stylesheet">
    <!-- STYLE 要放最下面  -->
    <link href="css/style.css?1" type="text/css" rel="stylesheet" media="screen,projection" />
              <link rel="icon" href="favicons/favicon_96x96.png">
      <!-- Specifying a Webpage Icon for Web Clip -->
      <link rel="apple-touch-icon-precomposed" href="favicons/favicon_57x57.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="favicons/favicon_72x72.png">
      <link rel="apple-touch-icon-precomposed" sizes="114x114" href="favicons/favicon_114x114.png">
      <link rel="apple-touch-icon-precomposed" sizes="144x144" href="favicons/favicon_144x144.png">
      <link rel="apple-touch-icon-precomposed" sizes="180x180" href="favicons/favicon_180x180.png">       

    <!-- slider style -->
    <script src="js/plugin/slider-master/modernizr.js"></script>
    <!-- Modernizr -->
    <title>BEYOND FITNESS - 新手上路</title>
</head>
<body>
    <div class="navi-slider-wrapper">
        <!--skip -->
        <div class="btn-skip">
            <a onclick="gtag('event', '卡片總覽', {
  'event_category': '連結點擊',
  'event_label': '返回'
});" href="<%= Url.Action("LearnerIndex","CornerKick") %>">Skip</a>
        </div>
        <!-- // End of skip -->
        <ul class="navi-slider">
            <li class="is-visible">
               <div class="navi-half-block image"></div>
               <div class="navi-half-block content">
                  <div>
                     <h2>線上個人化功能</h2>
                     <p>專業的團隊陪您一起突破目標</p>
                  </div>
               </div>
            </li>
            <!-- .navi-half-block.content -->
            <li>
               <div class="navi-half-block image"></div>
               <div class="navi-half-block content">
                  <div>
                     <h2>更多互動信息</h2>
                     <p>提供貼心服務並確保您的權益</p>
                  </div>
               </div>
               <!-- .navi-half-block.content --> 
            </li>
            <li>
               <div class="navi-half-block image"></div>
               <div class="navi-half-block content">
                  <div>
                     <h2>深入探索運動數據</h2>
                     <p>詳細記錄課程內容展現最佳自我</p>
                  </div>
               </div>
               <!-- .navi-half-block.content --> 
            </li>
         </ul>
        <!-- .navi-slider -->
        <!-- Button -->
        <!--<div class="markting-bottom">
            <button class="btn waves-effect waves-light btn-cancel" type="button" name="cancel" onclick="javascript:window.location.assign('registered.html');">立即加入</button>
            <button class="btn waves-effect waves-light btn-confirm" type="button" name="confirm" onclick="javascript:window.location.assign('login-girl.html');">登  入</button>
            </div>-->
        <!--// End of button-->
    </div>
    <!-- .navi-slider-wrapper -->
    <script src="js/libs/jquery-2.2.4.min.js"></script>
    <script src="js/plugin/slider-master/jquery.mobile.custom.min.js"></script>
    <script src="js/plugin/slider-master/navi.js"></script>
    <!-- Resource jQuery -->
</body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  if (_viewModel.CreateNew == true)
    { %>
<script>
    gtag('event', '新手上路', {
        'event_category': '自動導入',
        'event_label': '登入'
    });
</script>
<%  } %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    UserEventViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _viewModel = (UserEventViewModel)ViewBag.ViewModel;
    }

</script>

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
<!DOCTYPE html>
<html>
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS - 上課打卡</title>
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
                 <link rel="icon" href="favicons/favicon_96x96.png">
      <!-- Specifying a Webpage Icon for Web Clip -->
      <link rel="apple-touch-icon-precomposed" href="favicons/favicon_57x57.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="favicons/favicon_72x72.png">
      <link rel="apple-touch-icon-precomposed" sizes="114x114" href="favicons/favicon_114x114.png">
      <link rel="apple-touch-icon-precomposed" sizes="144x144" href="favicons/favicon_144x144.png">
      <link rel="apple-touch-icon-precomposed" sizes="180x180" href="favicons/favicon_180x180.png">       

      <link href="css/style.css?1.3" type="text/css" rel="stylesheet" media="screen,projection"/>
   </head>
   <body class="light-gray">
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /  全版 full-fixed / 背景色 light-gray-->
      <div class="wrapper full-fixed">
         <div class="wrapper-fixed">
            <!--Header -->
            <nav class="white non-line" role="navigation">
               <div class="nav-wrapper container">
                  <!-- BACK --> 
                   <%  Html.RenderPartial("~/Views/CornerKick/Module/ReturnHome.cshtml"); %>
                  <!-- // End of BACK --> 
                  <a id="logo-container" href="#" class="brand-logo toptitle center" >上課打卡</a> 
               </div>
            </nav>
            <!-- // End of Header --> 
            <!-- main -->
            <div class="main">
               <div class="container">
                  <!--品牌LOGO --> 
                  <!-- // End of 品牌LOGO -->
                  <div class="checkin-wrap">
                     <div class="personal-info">
                        <div class="row valign-wrapper">
                           <div class="col s4 m2"> 
                               <%   ViewBag.ImgClass = "circle responsive-img valign";
                                    Html.RenderPartial("~/Views/CornerKick/Module/ProfileImage.cshtml", _model); %>
                           </div>
                           <div class="col s8 m10 text-box"> <span class="black-t18"><%= _model.UserName ?? _model.RealName %></span> <span class="black-t12"><%= _model.UserProfileExtension.Gender=="F" ? "親愛的" : "兄弟" %>，你有 <%= _items.Where(l=>l.ClassTime<DateTime.Today.AddDays(1)).Count() %> 堂課沒打卡</span> </div>
                        </div>
                     </div>
                     <div class="container">
                        <form action="<%= Url.Action("CommitAttendance","CornerKick") %>" method="post">
                           <ul>
                               <%  var tomorrow = DateTime.Today.AddDays(1);
                                   bool hasItem = false;
                                   foreach (var item in _items.OrderByDescending(l => l.ClassTime))
                                   {
                                       String tagClass = item.TrainingBySelf == 1
                                                    ? "tag-green badge"
                                                    : item.IsTrialLesson()
                                                        ? "tag-luminous-vivid-raspberry badge"
                                                        : "tag-yellow badge";
                                       if (item.ClassTime < tomorrow)
                                           hasItem = true;  %>
                              <li>
                                 <input type="checkbox" name="LessonID" value="<%= item.LessonID %>" id="lesson<%=item.LessonID %>" <%= item.ClassTime>=tomorrow ? "disabled" : null %> />
                                 <label for="lesson<%=item.LessonID %>">
                                     <span class="<%= tagClass %>"><%= item.RegisterLesson.LessonPriceType.Status.LessonTypeStatus().Replace(".session","").Replace("課程","") %></span> <%= $"{item.ClassTime:yyyy/MM/dd HH:mm}" %>-<%= $"{item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value):HH:mm}" %>
                                     <span class="help-text"> 體能顧問：<%= item.AsAttendingCoach.UserProfile.FullName() %></span>
                                 </label>
                              </li>
                              <%    } %>
                           </ul>
                        </form>
                     </div>
                  </div>
               </div>
            </div>
            <!-- // End of main --> 
            <!-- Button -->
            <div class="content-area">
               <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="commitAttendance();" <%= hasItem ? null : "disabled" %>>完成打卡</button>
            </div>
            <!--// End of button--> 
         </div>
         <!--// End of wrapper-fixed--> 
      </div>
      <!--// End of wrapper--> 
      <!-- Footer --> 
      <!--<footer class="page-footer teal">
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
             //countup
             $('.countuptime').each(function() {
                 // no need to specify options unless they differ from the defaults
                 var target = this;
                 var endVal = parseInt($(this).attr('data-endVal'));
                 theAnimation = new countUp(target, 0, endVal, 0, 2.5);
                 theAnimation.start();
             });   
         
         });
         
             
      </script>       
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.cshtml"); %>
<%  Html.RenderPartial("~/Views/CornerKick/Module/ModeGirls.cshtml",_model); %>
<script>
    function commitAttendance() {
        if ($('form input:checkbox[name="LessonID"]:checked').length > 0) {
            gtag('event', '完成打卡', {
                'event_category': '按鈕點擊',
                'event_label': '上課打卡'
            });
            showLoading();
            $('form').submit();
        } else {
            alert('請勾選課程!!');
        }
    }
</script>
<%  if (_viewModel?.LineID != null)
    { %>
<script>
    gtag('event', '上課打卡', {
        'event_category': '卡片點擊',
        'event_label': 'LINE圖文選單'
    });
</script>
<%  } %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    IQueryable<LessonTime> _items;
    RegisterViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _viewModel = ViewBag.ViewModel as RegisterViewModel;
        _items = _model.LearnerGetUncheckedLessons(models, true);
        if (_items.Count() == 0)
        {
            Response.Redirect(Url.Action("AttendanceAccomplished"));
        }
    }

</script>

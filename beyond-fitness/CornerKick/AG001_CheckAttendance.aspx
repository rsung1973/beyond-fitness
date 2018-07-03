<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

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
      <link href="css/scrollup-master/themes/tab.css" rel="stylesheet" id="scrollUpTheme">
      <link href="css/scrollup-master/labs.css" rel="stylesheet">
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
   </head>
   <body class="light-gray">
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /  全版 full-fixed / 背景色 light-gray-->
      <div class="wrapper full-fixed">
         <div class="wrapper-fixed">
            <!--Header -->
            <nav class="white non-line" role="navigation">
               <div class="nav-wrapper container">
                  <!-- BACK --> 
                  <a href="#" class="button-collapse" onclick="javascript:window.location.assign('index.html');">
                     <div class="livicon-evo" data-options="name: angle-wide-left.svg; size: 40px; style: original; strokeWidth:2px; autoPlay:true"></div>
                  </a>
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
                                    Html.RenderPartial("~/Views/CornerKick/Module/ProfileImage.ascx", _model); %>
                           </div>
                           <div class="col s8 m10 text-box"> <span class="black-t18"><%= _model.UserName ?? _model.RealName %></span> <span class="black-t12"><%= _model.UserProfileExtension.Gender=="F" ? "親愛的" : "兄弟" %>，你有 <%= _items.Where(l=>l.ClassTime<DateTime.Today.AddDays(1)).Count() %> 堂課沒打卡</span> </div>
                        </div>
                     </div>
                     <div class="container">
                        <form action="<%= Url.Action("AG001_CommitAttendance","CornerKick") %>" method="post">
                           <ul>
                               <%  var tomorrow = DateTime.Today.AddDays(1);
                                   foreach (var item in _items)
                                   {
                                       String tagClass = item.TrainingBySelf == 1
                                                    ? "tag-green badge"
                                                    : item.IsTrialLesson()
                                                        ? "tag-luminous-vivid-raspberry badge" 
                                                        : "tag-yellow badge"; %>
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
               <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="commitAttendance();">全部打卡</button>
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
                 activeOverlay: '#00FFFF',
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
         $('#scrollUpTheme').attr('href', 'css/scrollup-master/themes/image.css?1.1');
         $('.image-switch').addClass('active');    
      </script>       
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  Html.RenderPartial("~/Views/CornerKick/Module/ModeGirls.ascx",_model); %>
<script>
    function commitAttendance() {
        showLoading();
        $('form').submit();
    }
</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    IQueryable<LessonTime> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _items = _model.LearnerGetUncheckedLessons(models, true);
        if (_items.Count() == 0)
        {
            Response.Redirect(Url.Action("AG001_AttendanceAccomplished"));
        }
    }

</script>

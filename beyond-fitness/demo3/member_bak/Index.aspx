<%@  Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
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
<html lang="en">
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS</title>
      <!-- CSS  -->
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
      <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
      <link href='css/LivIconsEvo.css' rel='stylesheet' type='text/css'>
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
   </head>
   <body>
      <div class="wrapper">
         <div class="wrapper-fixed">
            <!--Header -->
            <nav class="white" role="navigation">
               <div class="nav-wrapper container">
                  <a id="logo-container" href="#" class="brand-logo center" ><img src="images/Nav-logo-black.png" title="BEYOND FITNESS" alt="BEYOND FITNESS"></a>
                  <ul class="right hide-on-med-and-down">
                     <li>
                        <a href="calendar.html">
                           <div class="livicon-evo" data-options="name: calendar.svg; size: 40px; style: lines;  strokeColor:#0061d2"></div>
                        </a>
                     </li>
                  </ul>
                  <!-- 側邊選單 -->
                  <ul id="slide-out" class="side-nav">
                     <li>
                        <div class="user-view">
                           <div class="background"><img src="images/background4.jpg"></div>
                           <a href="personal-settings.html"><img class="circle" src="images/garfaild.png"></a> <a href="personal-settings.html"><span class="white-text name">劉加菲（garfaild）</span></a> <a href="index-marketing.html"><span class="white-text email">登出</span></a> 
                        </div>
                     </li>
                     <li><a href="calendar.html">行事曆</a></li>
                     <li><a href="check-in.html">上課打卡通知（2）</a></li>
                     <li><a href="qa.html">運動小學堂（💯）</a></li>
                     <li><a href="goal.html">個人目標</a></li>
                     <li><a href="points-exchange.html">點數兌換</a></li>
                     <li><a href="navigation.html">新手上路</a></li>
                     <!--
                        <li><div class="divider"></div></li>
                           <li><a class="subheader">Subheader</a></li>
                           <li><a class="waves-effect" href="#!">Third Link With Waves</a></li>
                        -->
                  </ul>
                  <a href="#" data-activates="slide-out" class="button-collapse">
                     <div class="livicon-evo" data-options="name: morph-menu-collapse.svg; size: 40px; style: solid;"></div>
                  </a>
                  <!-- // End of 側邊選單 --> 
               </div>
            </nav>
            <!-- // End of Header --> 
            <!-- main -->
            <div class="main">
               <div class="container">
                  <!-- 訊息通知 -->
                  <div class="notice-block">
                     <ul id="dropdown1" class="dropdown-content notice-dropdown">
                        <li>
                           <a href="qa.html">
                              親愛的，你有 2 堂課沒打卡喔
                              <div class="livicon-evo right" data-options="name: arrow-right.svg; size: 30px; style: original;"></div>
                           </a>
                        </li>
                        <li>
                           <a href="qa.html">
                              來挑戰一下運動小學堂吧
                              <div class="livicon-evo right" data-options="name: arrow-right.svg; size: 30px; style: original;"></div>
                           </a>
                        </li>
                        <li>
                           <a href="#!">
                              合約到期通知（2018/01/01）
                              <div class="livicon-evo right" data-options="name: arrow-right.svg; size: 30px; style: original;"></div>
                           </a>
                        </li>
                        <li>
                           <a href="#!">
                              Hi，跟著 Beyond 走，新手導航去
                              <div class="livicon-evo right" data-options="name: arrow-right.svg; size: 30px; style: original;"></div>
                           </a>
                        </li>
                     </ul>
                     <ul class="hide-on-med-and-down">
                        <!-- Dropdown Trigger -->
                        <li>
                           <a class="dropdown-button" href="#!" data-activates="dropdown1">
                              <span class="btn-floating waves-effect waves-light btn-notice red">4</span>提醒通知
                              <div class="livicon-evo right" data-options="name: chevron-bottom.svg; size: 30px; style: original;"></div>
                           </a>
                        </li>
                     </ul>
                  </div>
                  <!-- // End of 活動通知 --> 
                  <!-- 累計運動時間 -->
                  <div class="notice-block">
                     <ul id="dropdown2" class="dropdown-content notice-dropdown ">
                     </ul>
                     <ul class="hide-on-med-and-down notice-non">
                        <!-- Dropdown Trigger -->
                        <li><a class="dropdown-button" data-activates="dropdown2">本月累計訓練時間：<span class="f-green countuptime" data-endVal="859">859</span>分鐘（<span class="f-green countuptime" data-endVal="14">14</span>小時:<span class="f-green countuptime" data-endVal="19">19</span>分鐘）</a></li>
                     </ul>
                  </div>
                  <!-- // End of 活動通知 --> 
                  <!-- 體能分析圖 -->
                  <div class="goal-block">
                     <h3>體能分析圖</h3>
                     <div id="" div class="parallax-container" onclick="javascript:window.location.assign('chart.html');">
                        <div class="section no-pad-bot">
                           <div class="container">
                              <div style="width: 100%; margin: 0 auto 10px;">
                                 <canvas id="radarChart"></canvas>
                              </div>
                           </div>
                        </div>
                        <div class="parallax white"></div>
                     </div>
                  </div>
                  <!-- // End of 體能分析圖 -->                     
                  <!-- 個人目標 -->
                  <div class="goal-block" onclick="javascript:window.location.assign('goal.html');">
                     <h3>個人目標</h3>
                     <div id="" class="parallax-container">
                        <div class="section no-pad-bot">
                           <div class="container">
                              <div class="text-area">
                                 <h4>週期性目標 - 耐力期</h4>
                              </div>
                           </div>
                        </div>
                        <div class="parallax"><img src="images/background9.jpg" alt="Unsplashed background img 1"></div>
                     </div>
                  </div>
                  <!-- // End of 個人目標 --> 
                  <!-- 課表內容 Slider -->
                  <div class="class-block">
                     <h3>課表內容</h3>
                     <div class="carousel carousel-slider center " data-indicators="true">
                        <div class="carousel-fixed-item center middle-indicator">
                           <div class="left"> <a href="Previo" class="movePrevCarousel middle-indicator-text waves-effect waves-light content-indicator"><i class="material-icons left  middle-indicator-text">chevron_left</i></a> </div>
                           <div class="right"> <a href="Siguiente" class=" moveNextCarousel middle-indicator-text waves-effect waves-light content-indicator"><i class="material-icons right middle-indicator-text">chevron_right</i></a> </div>
                        </div>
                        <div class="carousel-item red white-text" onclick="javascript:window.location.assign('lesson.html');">
                           <img src="images/background5.jpg" alt="Unsplashed background img 1">
                           <div class="container">
                              <div class="text-area" >
                                 <h4>課程日期 - 2018/05/17 14:00-15:00</h4>
                                 <p class="white-text left">肌力/燃脂</p>
                              </div>
                           </div>
                        </div>
                        <div class="carousel-item amber white-text" onclick="javascript:window.location.assign('lesson.html');">
                           <img src="images/background6.jpg" alt="Unsplashed background img 2">
                           <div class="container">
                              <div class="text-area" >
                                 <h4>課程日期 - 2018/05/21 15:00-16:00 </h4>
                                 <p class="white-text left">功能性整合</p>
                              </div>
                           </div>
                        </div>
                        <div class="carousel-item green white-text" onclick="javascript:window.location.assign('lesson.html');">
                           <img src="images/background7.jpg" alt="Unsplashed background img 3">
                           <div class="container">
                              <div class="text-area">
                                 <h4>課程日期 - 2018/06/06 16:00-17:00</h4>
                                 <p class="white-text left"></p>
                              </div>
                           </div>
                        </div>
                        <div class="carousel-item green white-text" onclick="javascript:window.location.assign('lesson.html');">
                           <img src="images/background8.jpg" alt="Unsplashed background img 4">
                           <div class="container">
                              <div class="text-area">
                                 <h4>課程日期 - 2018/06/18 12:00-13:00</h4>
                                 <p class="white-text left"></p>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <!-- // End of 課表內容 Slider -->                    
                  <!-- 專業文章 Slider -->
                  <div class="article-block">
                     <h3>專業文章</h3>
                     <div class="carousel carousel-slider center " data-indicators="true">
                        <div class="carousel-fixed-item center middle-indicator">
                           <div class="left"> <a href="Previo" class="movePrevCarousel middle-indicator-text waves-effect waves-light content-indicator"><i class="material-icons left  middle-indicator-text">chevron_left</i></a> </div>
                           <div class="right"> <a href="Siguiente" class=" moveNextCarousel middle-indicator-text waves-effect waves-light content-indicator"><i class="material-icons right middle-indicator-text">chevron_right</i></a> </div>
                        </div>
                        <div class="carousel-item red white-text" href="#one!">
                           <img src="images/background4.jpg" alt="Unsplashed background img 1">
                           <div class="container">
                              <div class="text-area" >
                                 <h4>感冒能運動嗎？</h4>
                              </div>
                           </div>
                        </div>
                        <div class="carousel-item amber white-text" href="#two!">
                           <img src="images/background10.jpg" alt="Unsplashed background img 2">
                           <div class="container">
                              <div class="text-area" >
                                 <h4>肌肉痠痛還可以運動嗎？</h4>
                              </div>
                           </div>
                        </div>
                        <div class="carousel-item green white-text" href="#three!">
                           <img src="images/background11.jpg" alt="Unsplashed background img 3">
                           <div class="container">
                              <div class="text-area" >
                                 <h4>訓練喝肌酸有什麼用處呢？</h4>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <!-- // End of 專業文章 Slider --> 
                  <!-- 點數兌換 -->
                  <div class="points-block">
                     <h3>點數兌換</h3>
                     <div id="" div class="parallax-container parallax-min" onclick="javascript:window.location.assign('points-exchange.html');">
                        <div class="section no-pad-bot">
                           <div class="container">
                              <div class="points-text-area">
                                 <h5>目前您累積<span class="f-green"> 5 </span>點，答題共<span> 6 </span>題。</h5>
                                 <p class="white-text">每答對一題將能累積點一點，而點數能夠兌換各類精美禮品喔！</p>
                              </div>
                           </div>
                        </div>
                        <div class="parallax bgcolor"></div>
                     </div>
                  </div>
                  <!-- // End of 點數兌換 --> 
               </div>
            </div>
            <!-- // End of main --> 
         </div>
         <!--// End of wrapper-fixed--> 
         <!-- Footer -->
         <footer class="page-footer teal">
            <div class="footer-copyright">
               <div class="container"><a class="brown-text text-lighten-3" href="#">BEYOND FITNESS</a> </div>
            </div>
         </footer>
         <!-- // End of Footer --> 
      </div>
      <!--// End of wrapper-->        
      <!--  Scripts--> 
      <script src="js/libs/jquery-3.3.1.min.js"></script> 
      <script src="js/materialize.js"></script> 
      <script src="js/init.js"></script>
      <!--LivIconsEvo plugin-->
      <script src="js/plugin/LivIconsEvo/tools/snap.svg-min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/TweenMax.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/DrawSVGPlugin.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/MorphSVGPlugin.min.js"></script>
      <script src="js/plugin/LivIconsEvo/tools/verge.min.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.Tools.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.defaults.js"></script>
      <script src="js/plugin/LivIconsEvo/LivIconsEvo.js"></script> 
      <!--scrollup plugin-->
      <script src="js/plugin/scrollup-master/jquery.scrollUp.min.js"></script>
      <!--counup plugin-->
      <script src="js/plugin/countup/countUp.min.js"></script>
      <!--chartjs plugin-->
      <script src="js/plugin/chartjs/chart.min.js"></script>
      <script>
         // initialize sortable
         $(function() {
             //ScrollAuto
             $.scrollUp({
                 animation: 'slide',
                 scrollText: '',
                 scrollImg: true,
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
         
         var RadarConfig = {
         type: 'radar',
         data: {
           labels: ["身體質量", "相對肌力", "爆發力", "柔軟度", "心肺適能"],
           datasets: [{
               label: "分佈圖",
               backgroundColor: "rgba(0,97,210,.43)",
			   pointBackgroundColor: "rgba(0,97,210,1)",
               data: [6, 6, 7, 10, 8]
           }]
         },
         options: {
           legend: {
              display:false
           },
           
           scale: {
             reverse: false,
                      display: true,
             ticks: {
                        showLabelBackdrop: false,
               beginAtZero: true,
                        backdropColor: '#0061d2',
                        maxTicksLimit: 5,
                        max: 10,
                        fontSize: 5,
                        backdropPaddingX: 5,
                        backdropPaddingY: 5
             },
                      gridLines: {
                          color: "#888888",
                          lineWidth: 1
                      },
                      pointLabels: {
                          fontSize: 12,
                          fontColor: "#AAAAAA"
                      }
           }
         }
         };
         window.myRadar = new Chart(document.getElementById("radarChart"), RadarConfig);
      </script>
   </body>
</html>
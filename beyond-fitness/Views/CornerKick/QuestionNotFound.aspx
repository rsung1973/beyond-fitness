<!DOCTYPE html>
<html>
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS - 運動小學堂</title>
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
      <!--ionicons -->
      <link href='css/ionicons/ionicons.min.css' rel="stylesheet" type="text/css" media="screen" />
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
   </head>
   <body>
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /-->
      <div class="wrapper full-fixed mode-girls">
         <div class="wrapper-fixed">
            <!--Header -->
            <nav class="white non-line" role="navigation">
               <div class="nav-wrapper container">
                  <!-- BACK --> 
                  <a href="#" class="button-collapse" onclick="javascript:window.location.assign('index.html');">
                     <div class="livicon-evo" data-options="name: angle-wide-left.svg; size: 40px; style: original; strokeWidth:2px; autoPlay:true"></div>
                  </a>
                  <!-- // End of BACK --> 
                  <a id="logo-container" href="#" class="brand-logo toptitle center" >運動小學堂</a> 
               </div>
            </nav>
            <!-- // End of Header --> 
            <!-- main -->
            <div class="main">
               <div class="container">
                  <div class="registered-forms">
                     <!-- 切換頁籤 - TAB -->
                     <ul id="tabs-swipe-demo" class="tabs col s12">
                        <li class="tab col s6"><a class="active"  href="#test-swipe-1">題目卷</a></li>
                        <li class="tab col s6"><a href="#test-swipe-2">答案卷</a></li>
                     </ul>
                     <div id="test-swipe-1" class="tab-content acc-content col s12">

                        <!-- QA info -->
                        <div class="qa-content">                          
                           <img class="responsive-img nodata" src="images/nodata.png">
                           <p class="collection center"> <span class="gray-t16">今日腦汁已用盡，下次再來！</span>
                        </div>
                        <!--//End of QA info-->
                     </div>
                     <div id="test-swipe-2" class="tab-content acc-content col s12">
                         <img class="responsive-img nodata" src="images/nodata.png">
                         <p class="collection center"> <span class="gray-t16">Oooops！！您沒有參加過小學堂喔！</span>
                     </div>
                  </div>
                  <!-- //End of 登入 - TAB --> 
               </div>
            </div>
            <!-- // End of main --> 
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
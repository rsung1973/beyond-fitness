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
<html>
   <head>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <title>BEYOND FITNESS - 個人設定</title>
      <!-- CSS  -->
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
      <!-- materialize  -->
      <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection"/>
      <!-- livIconsevo  -->
      <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
      <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
      <!-- patternLock  -->
      <link href='css/patternLock/patternLock.css' rel='stylesheet' type='text/css'>
      <!-- scrollup-master  -->
      <link href="css/scrollup-master/themes/tab.css" rel="stylesheet" id="scrollUpTheme">
      <link href="css/scrollup-master/labs.css" rel="stylesheet">
      <!-- STYLE 要放最下面  -->
      <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
   </head>
   <body>
      <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /--> 
      <div class="wrapper">
         <div class="wrapper-fixed login-wrap">
            <!--Header -->
            <nav class="non-line dark-bg" role="navigation">
               <div class="nav-wrapper container">
                  <a id="logo-container" href="#" class="brand-logo toptitle center" >個人設定</a>
                  <div class="user-photo">
                      <%  Html.RenderPartial("~/Views/CornerKick/Module/ProfileImage.ascx", _profile); %>
                      <input type="file" id="photopic" name="photopic" style="display:none;"/>
                  </div>
               </div>
            </nav>
            <!-- // End of Header --> 
            <!-- main-->
            <div class="main">
               <div class="container">
                  <!--品牌LOGO -->
                  <!-- // End of 品牌LOGO --> 
                  <!-- 表單 -->
                  <div class="setting-forms">
                     <div class="row">
                        <form id="queryForm" class="col s12">
                           <div class="row">
                              <!--1-->
                              <div class="input-field col s12">
                                 <i class="livicon-evo prefix" data-options="name: user.svg; size: 30px; style: lines; strokeColor:#0061d2; autoPlay:true"></i>
                                 <input value="<%= _profile.UserName %>" id="nickname" name="UserName" type="text" class="validate"/>
                                 <label class="active" for="nickname">請輸入暱稱</label>  
                              </div>
                              <!--2-->
                              <div class="input-field col s12">
                                 <i class="livicon-evo prefix" data-options="name: envelope-put.svg; size: 30px; style: lines; strokeColor:#0061d2; autoPlay:true"></i>
                                 <input value="<%= _profile.PID %>" id="PID" name="PID" type="email" disabled/>
                                 <label class="active" for="email">電子郵件</label> 
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
            <button class="btn waves-effect waves-light btn-send" type="button" name="action" onclick="commitSettings();">設定完成</button>
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
        });
        $('#scrollUpTheme').attr('href', 'css/scrollup-master/themes/image.css?1.1');
        $('.image-switch').addClass('active');

        $("#profileImg").click(function () {
            $("#photopic").click();
        });

        $("#photopic").change(function () {

        });              
      </script>       
   </body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  Html.RenderPartial("~/Views/CornerKick/Module/ModeGirls.ascx",_profile); %>
<script>

    $(function () {
        var fileUpload = $('#photopic');
        var elmt = fileUpload.parent();

        fileUpload.off('click').on('change', function () {

            $('<form method="post" id="myForm" enctype="multipart/form-data"></form>')
            .append(fileUpload).ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>",
                data: { 'memberCode': '<%= _profile.MemberCode %>' },
                beforeSubmit: function () {
                    //status.show();
                    //btn.hide();
                    //console.log('提交時');
                },
                success: function (data) {
                    elmt.append(fileUpload);
                    if (data.result) {
                        $('#profileImg').prop('src', '<%= VirtualPathUtility.ToAbsolute("~/Information/GetResource/") %>' + data.pictureID + "?stretch=true");
                    } else {
                        alert(data.message);
                    }
                    //status.hide();
                    //console.log('提交成功');
                },
                error: function () {
                    elmt.after(fileUpload);
                    //status.hide();
                    //btn.show();
                    //console.log('提交失败');
                }
            }).submit();
        });
    });

   function commitSettings() {
       var $formData = $('#queryForm').serializeObject();
       clearErrors();
       showLoading();
       $.post('<%= Url.Action("AG001_CommitSettings", "CornerKick") %>', $formData, function (data) {
           hideLoading();
           if ($.isPlainObject(data)) {
               if (data.result) {
                   //window.location.href = '';
               }
           } else {
               $(data).appendTo($('body'));
           }
       });
   }

</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _profile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _profile = Context.GetUser().LoadInstance(models);
    }

</script>
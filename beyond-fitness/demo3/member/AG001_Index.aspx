<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

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
<!doctype html>
<html lang="en" class="no-js">
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
    <link href="css/slider-master/slider-master.css" rel="stylesheet">
    <!-- STYLE 要放最下面  -->
    <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <title>BEYOND FITNESS</title>
</head>
<body>
    <div class="cd-slider-wrapper">
        <!--品牌LOGO -->
        <div class="brandbox">
            <div class="brandbox-block">
                <img src="images/logo-white.png" alt="BEYOND FITNESS" class="responsive-img">
            </div>
        </div>
        <!-- // End of 品牌LOGO -->
        <ul class="cd-slider">
            <li class="is-visible">
                <div class="cd-half-block image"></div>
                <div class="cd-half-block content">
                    <div>
                        <p>你永遠比你想像的值得更多</p>
                        <p>You are always deserve more than you think</p>
                    </div>
                </div>
            </li>
            <!-- .cd-half-block.content -->
            <li>
                <div class="cd-half-block image"></div>
                <div class="cd-half-block content">
                    <div>
                        <p>在運動的時候，你完全屬於你自己</p>
                        <p>You always deserve more than you think.</p>
                    </div>
                </div>
                <!-- .cd-half-block.content -->
            </li>
            <li>
                <div class="cd-half-block image"></div>
                <div class="cd-half-block content">
                    <div>
                        <p>多愛自己一點，也多信任自己一點</p>
                        <p>Love yourself more, trust yourself more.</p>
                    </div>
                </div>
                <!-- .cd-half-block.content -->
            </li>
        </ul>
        <!-- .cd-slider -->
        <!-- Button -->
        <div class="markting-bottom">
            <button class="btn waves-effect waves-light btn-cancel" type="button" name="cancel" onclick="">解除設定</button>
            <button class="btn waves-effect waves-light btn-confirm" type="button" name="confirm" onclick="nextStep();"><%= _bound ? "登　　入" : "首次設定" %></button>
        </div>
        <!--// End of button-->
    </div>
    <!-- .cd-slider-wrapper -->
    <script src="js/libs/jquery-2.2.4.min.js"></script>
    <script src="js/plugin/slider-master/jquery.mobile.custom.min.js"></script>
    <script src="js/plugin/slider-master/main.js"></script>
    <!-- Resource jQuery -->
    <script src="js/plugin/slider-master/modernizr.js"></script>
    <!-- Modernizr -->
</body>
</html>
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.ascx"); %>
<%  Html.RenderPartial("~/Views/Shared/JsAlert.ascx"); %>
<script>
    function nextStep() {
        $('').launchDownload('<%= Url.Action("AG001_Register","CornerKick") %>', { 'lineID': '<%= _viewModel.LineID %>' });
    }
    <%  if (_bound)
    { %>
    $('button.btn-cancel').on('click', function (event) {
        $('').launchDownload('<%= Url.Action("AG001_Unbind","CornerKick") %>', { 'lineID': '<%= _viewModel.LineID %>' });
    });
    <%  }
    else
    { %>
    $(function () {
        $('button.btn-cancel').addClass('disabled');
    });
<%  }   %>
</script>
<script runat="server">
    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    RegisterViewModel _viewModel;
    bool _bound;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _viewModel = (RegisterViewModel)ViewBag.ViewModel;
        _bound = models.GetTable<UserProfileExtension>().Any(u => u.LineID == _viewModel.LineID);
    }

</script>

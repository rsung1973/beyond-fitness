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
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>BEYOND FITNESS - 體能分析庫</title>
    <!-- CSS  -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!-- materialize  -->
    <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <!-- livIconsevo  -->
    <link href='css/livIconsevo/LivIconsEvo.css' rel='stylesheet' type='text/css'>
    <link href="css/livIconsevo/LivIconsEvo.css" rel="stylesheet" type="text/css">
    <!-- scrollup-master  -->
    <link href="css/scrollup-master/themes/image.css?1.1" rel="stylesheet" id="scrollUpTheme">
    
    <!-- STYLE 要放最下面  -->
    <link href="css/style.css?1.3" type="text/css" rel="stylesheet" media="screen,projection" />
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
    <!--chartjs plugin-->
    <%--<script src="js/plugin/chartjs/chart.min.js"></script>--%>
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
    <!--//預設值為藍色 / 若要設定女生 請加上 mode-girls /  全版 full-fixed / 背景色 light-gray-->
    <div class="wrapper light-gray full-fixed mode-girls">
        <div class="wrapper-fixed">
            <!--Header -->
            <nav class="white non-line" role="navigation">
                <div class="nav-wrapper container">
                    <!-- BACK -->
                    <%  Html.RenderPartial("~/Views/CornerKick/Module/ReturnHome.cshtml"); %>
                    <!-- // End of BACK -->
                    <a id="logo-container" href="#" class="brand-logo toptitle center">體能分析庫</a>
                </div>
            </nav>
            <!-- // End of Header -->
            <!-- main -->
            <div class="main main-fixed">
                <div class="container">
                    <!--品牌LOGO -->
                    <!-- // End of 品牌LOGO -->
                    <div class="chart-wrap">
                        <!-- Ｃhart -->
                        <ul class="collection">
                            <!-- Card 5-->
                            <li class="collection-item avatar list-box">
                                <!-- Left -->
                                <div class="row">
                                    <div class="col s12 ">
                                        <span class="title">累積運動時間</span>
                                        <div class="bar-chart">
                                            <%  Html.RenderPartial("~/Views/CornerKick/Module/MonthlyLessonBarChart.ascx", _model); %>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <!-- Card 2-->
                            <li class="collection-item avatar">
                                <!-- 上 -->
                                <div class="row">
                                    <ul class="chart-card">
                                        <li class="chart-top">
                                            <span class="title">本月訓練時間比例</span>
                                            <%  Html.RenderPartial("~/Views/CornerKick/Module/TrainingContentAnalysis.ascx", _model); %>
                                        </li>
                                        <!-- 下-->
                                        <li class="chart-bottom" id="pieChartBottom">
                                            <div class="col s6">
                                                <ul class="list-2">
                                                    <li>基本  --</li>
                                                    <li>運動技巧  --</li>
                                                    <li>肌力  --</li>
                                                </ul>
                                            </div>
                                            <div class="col s6">
                                                <ul class="list-3">
                                                    <li>心肺  --</li>
                                                    <li>恢復  --</li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                    <script>
                                        $global.showPieChartBottom = function () {
                                            var data = window.myPieChart.chart.data;
                                            var $bottom = $('#pieChartBottom li');
                                            $bottom.each(function (idx, element) {
                                                if (idx < data.labels.length) {
                                                    $(this).text(data.labels[idx] + ' ' + (data.datasets[0].data[idx] ? data.datasets[0].data[idx] + '%' : '--'));
                                                    if (data.datasets[0].data[idx] < data.datasets[0].dataCompareTo[idx]) {
                                                        //$(this).append($('<span class="livicon-evo" data-options="name: arrow-down.svg; size: 20px; style: lines; strokeColor:#ec3b57; strokeWidth:2px; autoPlay:true"></span>'));
                                                        $(this).append('<span class="f-darkgreen">▼</span>');
                                                    } else if (data.datasets[0].data[idx] > data.datasets[0].dataCompareTo[idx]) {
                                                        //$(this).append($('<span class="livicon-evo" data-options="name: arrow-top.svg; size: 20px; style: lines; strokeColor:#ec3b57; strokeWidth:2px; autoPlay:true"></span>'));
                                                        $(this).append('<span class="f-red">▲</span>');
                                                    }
                                                }
                                            });
                                        };
                                    </script>
                                </div>
                            </li>
                            <!-- Card 3-->
                            <%
                                var endDate = DateTime.Today.AddDays(1);
                                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                                var totalMinutes = _model.TotalLessonMinutes(models, startDate, endDate);
                                var totalMinutesCompareTo = _model.TotalLessonMinutes(models, startDate.AddMonths(-1), startDate);

                            %>
                            <li class="collection-item avatar list-box box-left">
                                <!-- Left -->
                                <div class="row">
                                    <div class="col s12 "><span class="title">本月訓練時間</span> </div>
                                    <div class="col s12">
                                        <div class="time-box">
                                            <span class="flow-text">
                                                <%= $"{totalMinutes/60:00}" %>:<%= $"{totalMinutes%60:00}" %>
                                                <small>小時:分鐘<%  if (totalMinutes > totalMinutesCompareTo)
                                                        {%><span class="f-red">▲</span><%  }
                                                        else if (totalMinutes < totalMinutesCompareTo)
                                                        {%><span class="f-darkgreen">▼</span><%  } %></small>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <!-- Card 4-->
                            <li class="collection-item avatar list-box box-right">
                                <!-- Left -->
                                <div class="row">
                                    <div class="col s12"><span class="title">器材使用率</span> </div>
                                    <!-- Right -->
                                    <div class="">
                                        <ul class="list-4">
                                            <li class="chart-bottom">
                                                <div class="col s6 w-half">
                                                    <%
                                                        String[] aidsName = new String[10];

                                                        var items = _model.UID.LearnerTrainingAids(models)
                                                            .Select(s => s.TrainingAids.ItemName);
                                                        var totalCount = items.Count();
                                                        int idx = 0;
                                                        var result = items.GroupBy(s => s)
                                                                .Select(g => new { ItemName = g.Key, Count = g.Count() })
                                                                .OrderByDescending(g => g.Count)
                                                                .Take(10).ToArray();
                                                        foreach (var item in result)
                                                        {
                                                            aidsName[idx] = item.ItemName;
                                                            idx++;
                                                        }  %>
                                                    <table>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 25px"></th>
                                                                <th style="min-width: 80px; width: 95px;"></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%  for (idx = 0; idx < 5; idx++)
                                                                { %>
                                                            <tr>
                                                                <td><%= idx+1 %>. </td>
                                                                <td><%= aidsName[idx] ?? "--" %></td>
                                                                <td><%= aidsName[idx]!=null ? $"{result[idx].Count*100/totalCount}%" : "--" %></td>
                                                            </tr>
                                                            <%      
                                                                } %>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col s6 w-half">
                                                    <table>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 25px"></th>
                                                                <th style="min-width: 80px; width: 95px;"></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%  for (; idx < 10; idx++)
                                                                { %>
                                                            <tr>
                                                                <td><%= idx+1 %>. </td>
                                                                <td><%= aidsName[idx] ?? "--" %></td>
                                                                <td><%= aidsName[idx]!=null ? $"{result[idx].Count*100/totalCount}%" : "--" %></td>
                                                            </tr>
                                                            <%      
                                                                } %>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </li>
                                <!-- Card 1-->
                            <li class="collection-item avatar">
                                <!-- 上 -->
                                <div class="row">
                                    <ul class="chart-card">
                                        <li class="chart-top">
                                            <span class="title">體能分析</span>
                                            <%  var contestant = _model.ExerciseGameContestant;
                                                if (contestant != null && contestant.Status == (int)Naming.GeneralStatus.Successful
                                                        && contestant.ExerciseGameRank.Any(r => r.RankingScore.HasValue))
                                                {
                                                    Html.RenderPartial("~/Views/CornerKick/Module/GameRankRadarChart.ascx", contestant);
                                                }
                                                else if (_model.PersonalExercisePurpose != null
                                                    && (_model.PersonalExercisePurpose.Cardiopulmonary.HasValue
                                                        || _model.PersonalExercisePurpose.Flexibility.HasValue
                                                        || _model.PersonalExercisePurpose.MuscleStrength.HasValue))
                                                {
                                                    Html.RenderPartial("~/Views/CornerKick/Module/BodyPowerAbility.ascx", _model.PersonalExercisePurpose);
                                                }
                                                else
                                                {
                                                    Html.RenderPartial("~/Views/CornerKick/Module/EmptyGameRankRadarChart.ascx", _model);
                                                } %>
                                        </li>
                                        <!-- 下-->
                                        <li class="chart-bottom" id="radarChartBottom">
                                            <div class="col s6">
                                                <ul class="list-2">
                                                    <li></li>
                                                    <li></li>
                                                    <li></li>
                                                </ul>
                                            </div>
                                            <div class="col s6">
                                                <ul class="list-3">
                                                    <li></li>
                                                    <li></li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                    <script>
                                        $global.showRadarChartBottom = function () {
                                            var data = window.myRadar.chart.data;
                                            var $bottom = $('#radarChartBottom li');
                                            $bottom.each(function (idx, element) {
                                                if (idx < data.labels.length) {
                                                    $(this).text(data.labels[idx] + ' ' + (data.datasets[0].data[idx] || '--'));
                                                } else {
                                                    $(this).remove();
                                                }
                                            });
                                        };
                                    </script>
                                </div>
                            </li>
                        </ul>
                        <!-- //End of Card  -->
                    </div>
                </div>
                <!--//End of Chart-->
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
<%  Html.RenderPartial("~/Views/Common/JQueryHelper.cshtml"); %>
<%  Html.RenderPartial("~/Views/Shared/InitBarChartNoRegister.ascx"); %>

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

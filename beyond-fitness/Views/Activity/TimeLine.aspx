<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-paw"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>我的足印</li>
        </ol>
        <!-- end breadcrumb -->

        <!-- You can also add more buttons to the
                ribbon for further usability

                Example below:

                <span class="ribbon-button-alignment pull-right">
                <span id="search" class="btn btn-ribbon hidden-xs" data-title="search"><i class="fa-grid"></i> Change Grid</span>
                <span id="add" class="btn btn-ribbon hidden-xs" data-title="add"><i class="fa-plus"></i> Add</span>
                <span id="search" class="btn btn-ribbon" data-title="search"><i class="fa-search"></i> <span class="hidden-mobile">Search</span></span>
                </span> -->

    </div>
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
    <h1 class="txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-paw"></i>我的足印
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">
        <article class="col-xs-12 col-sm-12 col-md-9 col-lg-9">

            <div class="well well-sm bg-color-darken txt-color-white">
                <!-- Timeline Content -->
                <div class="smart-timeline">
                    <ul class="smart-timeline-list">
                        <li>
                            <div class="smart-timeline-icon">
                                <img src="img/avatars/male.png" width="32" height="32" alt="user" />
                            </div>
                            <div class="smart-timeline-time">
                                <small>2016/07/28 14:30~17:30</small>
                            </div>
                            <div class="smart-timeline-content">
                                <div class="well well-sm display-inline bg-color-pinkDark">
                                    <p>提醒您記得<strong> 2016/07/28 14:30~17:30 </strong>與黃比爾一起運動喔！</p>
                                    <p>
                                        <button class="btn btn-xs btn-default" onclick="javascript:(window.location.href='vipdiarypreview.html');">預先確認上課內容</button>
                                    </p>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="smart-timeline-icon bg-color-greenDark">
                                <i class="fa fa-bar-chart-o"></i>
                            </div>
                            <div class="smart-timeline-time">
                                <small>2016/07/22 14:30~17:30</small>
                            </div>
                            <div class="smart-timeline-content">
                                <div class="well well-sm display-inline">
                                    <p>恭喜您已於<strong> 2016/07/18 14:30~17:30 </strong>往目標邁向一大步！</p>
                                    <p>
                                        <button class="btn btn-xs btn-default" onclick="javascript:(window.location.href='vipdiary.html');">檢視成果</button>
                                    </p>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="smart-timeline-icon bg-color-magenta">
                                <i class="fa fa-bar-chart-o"></i>
                            </div>
                            <div class="smart-timeline-time">
                                <small>2016/07/01</small>
                            </div>
                            <div class="smart-timeline-content">
                                <p>
                                    <a href="vipdashboard.html"><strong>快點來確認你的運動成果喔！</strong></a>
                                </p>

                                <div class="sparkline" data-sparkline-type="compositeline" data-sparkline-spotradius-top="5" data-sparkline-color-top="#3a6965" data-sparkline-line-width-top="3" data-sparkline-color-bottom="#2b5c59" data-sparkline-spot-color="#2b5c59" data-sparkline-minspot-color-top="#97bfbf"
                                    data-sparkline-maxspot-color-top="#c2cccc" data-sparkline-highlightline-color-top="#cce8e4" data-sparkline-highlightspot-color-top="#9dbdb9" data-sparkline-width="170px" data-sparkline-height="40px" data-sparkline-line-val="[6,4,7,8,4,3,2,2,5,6,7,4,1,5,7,9,9,8,7,6]"
                                    data-sparkline-bar-val="[4,1,5,7,9,9,8,7,6,6,4,7,8,4,3,2,2,5,6,7]">
                                </div>

                                <br>
                            </div>
                        </li>
                        <li>
                            <div class="smart-timeline-icon bg-color-red">
                                <i class="fa fa-birthday-cake"></i>
                            </div>
                            <div class="smart-timeline-time">
                                <small>2016/06/23</small>
                            </div>
                            <div class="smart-timeline-content">
                                <p>
                                    在這美好的一天 - 很開心的祝您生日快樂！
                                </p>
                            </div>
                        </li>

                        <li class="text-center">
                            <a href="javascript:void(0)" class="btn btn-sm btn-default"><i class="fa fa-arrow-down text-muted"></i>LOAD MORE</a>
                        </li>
                    </ul>
                </div>
                <!-- END Timeline Content -->

            </div>

        </article>

        <article class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10 ">
                <h5 class="margin-top-0 "><i class="fa fa-external-link "></i>快速功能</h5>
                <ul class="no-padding no-margin ">
                    <p class="no-margin ">
                        <ul class="icons-list ">
                            <li>
                                <a title="我的總覽 " href="vipdashboard.html "><span class="fa-stack fa-lg "><i class="fa fa-square-o fa-stack-2x "></i><i class="fa fa-tachometer fa-stack-1x "></i></span>我的總覽</a>
                            </li>
                            <li>
                                <a title="我的簡介 " href="profile.html "><span class="fa-stack fa-lg "><i class="fa fa-square-o fa-stack-2x "></i><i class="fa fa-user fa-stack-1x "></i></span>我的簡介</a>
                            </li>
                            <li>
                                <a title="登出 " href="index.html "><span class="fa-stack fa-lg "><i class="fa fa-square-o fa-stack-2x "></i><i class="fa fa-sign-out fa-stack-1x "></i></span>登出</a>
                            </li>
                        </ul>
                    </p>
                </ul>
            </div>
            <!-- /well -->
        </article>
        <!-- end row -->
        <!-- Modal -->
        <div class="modal fade" id="question" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;
                        </button>
                        <h4 class="modal-title" id="myModalLabel"><i class="fa-fw fa fa-question"></i>每日小提問</h4>
                    </div>
                    <div class="modal-body bg-color-darken txt-color-white">
                        <div class="panel panel-default bg-color-darken">
                            <form action="#" method="post" id="question-form" class="smart-form">
                                <div class="panel-body status smart-form vote">
                                    <div class="who clearfix">
                                        <img src="img/avatars/male.png" alt="img" class="busy">
                                        <span class="name font-lg"><b>黃比爾</b></span>
                                        <span class="from font-md"><b>Hi, 劉加菲</b> 請試試回答以下問的答案，答對會有意想不到的驚喜喔！</span>
                                    </div>
                                    <div class="image font-md">
                                        <strong>營養素裡面一公克脂質為幾大卡（仟卡）的熱量？</strong>
                                    </div>
                                    <ul class="comments">
                                        <li>
                                            <label class="radio font-md">
                                                <input type="radio" name="radio">
                                                <i></i>4大卡</label>
                                        </li>
                                        <li>
                                            <label class="radio font-md">
                                                <input type="radio" name="radio">
                                                <i></i>7大卡</label>
                                        </li>
                                        <li>
                                            <label class="radio font-md">
                                                <input type="radio" name="radio">
                                                <i></i>9大卡</label>
                                        </li>
                                        <li>
                                            <label class="radio font-md">
                                                <input type="radio" name="radio">
                                                <i></i>脂質沒有熱量</label>
                                        </li>
                                    </ul>
                                    <div class="message" style="display: block">
                                        <i class="fa fa-check fa-lg"></i>
                                        <p class="text-center">
                                            恭喜你答對囉，可獲得點數1點，集滿一定點數後有意想不到的驚喜喔！
                                        </p>
                                    </div>
                                    <!--<div class="errormessage" style="display:block">
                                    <i class="fa fa-times fa-lg"></i>
                                        <p class="text-center">
                                            非常可惜答錯囉！試著再次請教黃比爾正確答案吧！
                                        </p>
                                </div>-->
                                </div>
                            </form>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->
        </div>
        <!-- END MAIN CONTENT -->
    </div>

    <script>
    </script>

</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
    }

</script>

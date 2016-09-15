<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-calculator"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>報表管理></li>
            <li>課程統計報表</li>
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
    <h1 class="page-title txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-calculator"></i>報表管理
                            <span>>  
                                課程統計報表
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <article class="col-sm-12 col-md-6 col-lg-6">
            <!-- new widget -->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-4" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false">
                <!-- widget options:
                           usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
                           
                           data-widget-colorbutton="false"
                           data-widget-editbutton="false"
                           data-widget-togglebutton="false"
                           data-widget-deletebutton="false"
                           data-widget-fullscreenbutton="false"
                           data-widget-custombutton="false"
                           data-widget-collapsed="true"
                           data-widget-sortable="false"
                           
                           -->
                <header>
                    <span class="widget-icon"><i class="fa fa-search txt-color-white"></i></span>
                    <h2>查詢條件 </h2>
                    <!-- <div class="widget-toolbar">
                              add: non-hidden - to disable auto hide
                              
                              </div>-->
                </header>
                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->
                    <!-- widget content -->
                    <div class="widget-body bg-color-darken txt-color-white no-padding">
                        <form method="post" id="pageForm" class="smart-form">
                            <fieldset>
                                <section>
                                    <label class="label"></label>
                                    <label class="select">
                                        <%  var inputItem = new InputViewModel { Id = "coachID", Name = "coachID" };
                                            ViewBag.SelectIndication = "<option value=''>請選擇教練</option>";
                                            Html.RenderPartial("~/Views/Lessons/SimpleCoachSelector.ascx", inputItem); %>
                                        <i class="icon-append fa fa-file-word-o"></i>
                                    </label>
                                </section>
                                <div class="row">
                                    <% DateTime dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); %>
                                    <section class="col col-6">
                                        <label class="label">&nbsp;</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <input type="text" name="dateFrom" id="dateFrom" readonly="readonly" class="form-control input-lg date form_date" data-date-format="yyyy/mm/dd" placeholder="請點選日曆" value='<%= dateFrom.ToString("yyyy/MM/dd") %>' />
                                        </label>
                                    </section>
                                    <section class="col col-6">
                                        <label class="label">&nbsp;</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <input type="text" name="dateTo" id="dateTo" readonly="readonly" class="form-control input-lg date form_date" data-date-format="yyyy/mm/dd" placeholder="請點選日曆" value='<%= dateFrom.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") %>' />
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <footer>
                                <button type="button" onclick="showAchievement();" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                            </footer>
                            <div class="message">
                                <i class="fa fa-check fa-lg"></i>
                                <p>
                                    Your comment was successfully added!
                                </p>
                            </div>
                        </form>
                    </div>
                    <!-- end widget content -->
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <article class="col-sm-12 col-md-6 col-lg-6">
            <!-- new widget -->
            <div class="jarviswidget jarviswidget-color-darken" id="priceType" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false">
            </div>
            <!-- end widget -->
        </article>
    </div>
    <div class="row" id="lessonAttendance">
    </div>

    <div class="row">
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-6 col-lg-6">

            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-1" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
                <!-- widget options:
                                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

                                data-widget-colorbutton="false"
                                data-widget-editbutton="false"
                                data-widget-togglebutton="false"
                                data-widget-deletebutton="false"
                                data-widget-fullscreenbutton="false"
                                data-widget-custombutton="false"
                                data-widget-collapsed="true"
                                data-widget-sortable="false"

                                -->
                <header>
                    <span class="widget-icon"><i class="fa fa-table"></i></span>
                    <h2>業績統計表 - 課程類別 (2016/07/01~2016/07/31)</h2>
                </header>

                <!-- widget div-->
                <div>

                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->

                    <!-- widget content -->
                    <div class="widget-body no-padding">

                        <table id="dt_basic4" class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th data-class="expand">姓名</th>
                                    <th>購買課程類別</th>
                                    <th data-hide="phone">購買課數(現金/信用卡)</th>
                                    <th><i class="fa fa-fw fa-money text-muted hidden-md hidden-sm hidden-xs"></i>總購買金額</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>黃比爾</td>
                                    <td>50堂-1 小時(2015)</td>
                                    <td>50【45 / 5】</td>
                                    <td>69500</td>
                                </tr>
                                <tr>
                                    <td>黃比爾</td>
                                    <td>25堂-1 小時(2015)</td>
                                    <td>25【25 / 0】</td>
                                    <td>37500</td>
                                </tr>
                                <tr>
                                    <td>邱鈺烘</td>
                                    <td>50堂-1 小時(2015)</td>
                                    <td>20【20 / 0】</td>
                                    <td>35000</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->

            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->

        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-6 col-lg-6">

            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-1" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
                <!-- widget options:
                                usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

                                data-widget-colorbutton="false"
                                data-widget-editbutton="false"
                                data-widget-togglebutton="false"
                                data-widget-deletebutton="false"
                                data-widget-fullscreenbutton="false"
                                data-widget-custombutton="false"
                                data-widget-collapsed="true"
                                data-widget-sortable="false"

                                -->
                <header>
                    <span class="widget-icon"><i class="fa fa-table"></i></span>
                    <h2>業績統計表 - 人員 (2016/07/01~2016/07/31)</h2>
                </header>

                <!-- widget div-->
                <div>

                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->

                    <!-- widget content -->
                    <div class="widget-body no-padding">

                        <table id="dt_basic5" class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th data-class="expand">姓名</th>
                                    <th>總購買課數</th>
                                    <th><i class="fa fa-fw fa-money text-muted hidden-md hidden-sm hidden-xs"></i>總購買金額</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>黃比爾</td>
                                    <td>75</td>
                                    <td>10500</td>
                                </tr>
                                <tr>
                                    <td>邱鈺烘</td>
                                    <td>25</td>
                                    <td>35000</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->

            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->
    </div>


    <script>
        function showAchievement() {
            var loadingCount = 2;
            showLoading(true);
            $('#priceType').load('<%= Url.Action("ListLessonPriceType","Report") %>', function () {
                loadingCount--;
                if (loadingCount <= 0)
                    hideLoading();
            });

            $('#lessonAttendance').load('<%= Url.Action("ListLessonAttendance","Report") %>',
                {
                    'coachID': $('#coachID').val(),
                    'dateFrom': $('#dateFrom').val(),
                    'dateTo': $('#dateTo').val(),
                }, function () {
                    loadingCount--;
                    if (loadingCount <= 0)
                        hideLoading();
                });
        }
    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LearnerPaymentViewModel _viewModel;
    IEnumerable<RegisterLesson> _model;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _viewModel = (LearnerPaymentViewModel)ViewBag.ViewModel;
        _model = (IEnumerable<RegisterLesson>)this.Model;

    }



</script>

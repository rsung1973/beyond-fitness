<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<section id="widget-grid" class="">
    <!-- row -->
    <div class="row">
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
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
                    <h2>活動方案列表</h2>
                    <div class="widget-toolbar">
                        <a href="#" class="btn bg-color-green"><i class="fa fa-fw fa-cloud-download-alt"></i>下載檔案</a>
                        <a href="#" class="btn btn-primary listProjectDialog_link"><i class="fa fa-fw fa-plus"></i>新增</a>
                    </div>
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
                        <table id="giftpoints_dt" class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th data-class="expand">活動名稱</th>
                                    <th data-hide="phone">活動起日</th>
                                    <th data-hide="phone">活動迄日</th>
                                    <th>贈送點數</th>
                                    <th data-hide="phone">贈送方式</th>
                                    <th>目前贈送人數</th>
                                    <th>狀態</th>
                                    <th data-hide="phone">功能</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>LINE綁定會員帳號活動</td>
                                    <td nowrap="noWrap" class="text-center">2018/08/15</td>
                                    <td nowrap="noWrap" class="text-center">--</td>
                                    <td nowrap="noWrap" class="text-center">2</td>
                                    <td>程式連結</td>
                                    <td nowrap="noWrap" class="text-center">--</td>
                                    <td>待生效</td>
                                    <td nowrap="noWrap">
                                        <a href="#" class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>&nbsp;&nbsp;
                                         <a href="#" class="btn btn-circle btn-primary listAttendantDialog_link"><i class="fa fa-fw fa fa-lg fa-user-plus" aria-hidden="true"></i></a>&nbsp;&nbsp;
                                         <a href="#" class="btn btn-circle bg-color-red"><i class="fa fa-fw fa-lg fa-trash-alt" aria-hidden="true"></i></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>加入LINE官方帳號回饋活動</td>
                                    <td nowrap="noWrap" class="text-center">2018/07/15</td>
                                    <td nowrap="noWrap" class="text-center">2018/10/15</td>
                                    <td nowrap="noWrap" class="text-center">1</td>
                                    <td>手動</td>
                                    <td nowrap="noWrap" class="text-center">20</td>
                                    <td>已生效</td>
                                    <td nowrap="noWrap">
                                        <a href="#" class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>&nbsp;&nbsp;
                                         <a href="#" class="btn btn-circle btn-primary listAttendantDialog_link"><i class="fa fa-fw fa fa-lg fa-user-plus" aria-hidden="true"></i></a>&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>母親節帶著媽咪來運動</td>
                                    <td nowrap="noWrap" class="text-center">2018/04/20</td>
                                    <td nowrap="noWrap" class="text-center">2018/05/20</td>
                                    <td nowrap="noWrap" class="text-center">1</td>
                                    <td>手動</td>
                                    <td nowrap="noWrap" class="text-center">55</td>
                                    <td>已停用</td>
                                    <td nowrap="noWrap">
                                        <a href="#" class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>&nbsp;&nbsp;
                                         <a href="#" class="btn btn-circle bg-color-red"><i class="fa fa-fw fa-lg fa-check-square" aria-hidden="true"></i></a>
                                    </td>
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
    <!-- end row -->
</section>

<script>
    //debugger;
    function inquireTrust() {
        var event = event || window.event;
        var $form = $(event.target).closest('form');
        var formData = $form.serializeObject();
        $('#btnDownloadTrustTrack').css('display', 'none');
        $('#btnDownloadTrustSummary').css('display', 'none');
        //$('#btnDownloadTrustLesson').css('display', 'none');

        showLoading();
        $('#trustList').load('<%= Url.Action("InquireContractTrust","Accounting") %>', formData, function (data) {
            hideLoading();
        });

    }

    function downloadTrustTrack() {
        $('#queryForm').launchDownload('<%= Url.Action("CreateTrustTrackXlsx","Accounting") %>');
    }

    function downloadTrustSummary() {
        $('#queryForm').launchDownload('<%= Url.Action("CreateTrustSummaryXlsx","Accounting") %>');
    }

    function downloadTrustLesson() {
        $('#queryForm').launchDownload('<%= Url.Action("CreateTrustLessonXlsx","Accounting") %>');
    }



</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _profile;
    TrustQueryViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _profile = Context.GetUser();
        _viewModel = (TrustQueryViewModel)ViewBag.ViewModel;
    }

</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
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
                    <span class="widget-icon"><i class="fa fa-search"></i></span>
                    <h2>查詢條件</h2>
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
                        <form action="<%= Url.Action("InquireInvoiceDispatchLog","Invoice") %>" method="post" id="queryForm" class="smart-form">
                            <fieldset>
                                <div class="row">
                                    <section class="col col-6">
                                        <label class="label">請選擇傳送起日</label>
                                        <label class="input">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <%  var dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); %>
                                            <input type="text" name="DateFrom" readonly="readonly" class="form-control date form_date" data-date-format="yyyy/mm/dd" placeholder="請點選日曆" value="<%= String.Format("{0:yyyy/MM/dd}",DateTime.Today) %>" />
                                        </label>
                                    </section>
                                    <section class="col col-6">
                                        <label class="label">請選擇傳送迄日</label>
                                        <label class="input">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <input type="text" name="DateTo" readonly="readonly" class="form-control date form_date" data-date-format="yyyy/mm/dd" placeholder="請點選日曆" value="<%= String.Format("{0:yyyy/MM/dd}",DateTime.Today) %>" />
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <footer>
                                <button onclick="inquireDispatch();" type="button" name="btnSend" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                                <button type="reset" name="cancle" class="btn btn-default">
                                    清除 <i class="fa fa-undo" aria-hidden="true"></i>
                                </button>
                            </footer>
                        </form>
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
    <!-- row -->
    <div class="row">
        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
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
                    <h2>結果清單</h2>
                </header>
                <!-- widget div-->
                <div>
                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->
                    <!-- widget content -->
                    <div class="widget-body bg-color-darken txt-color-white no-padding" id="dispatchList">
                        <%  Html.RenderPartial("~/Views/Invoice/Module/InvoiceDispatchLogSummary.ascx", models.GetTable<InvoiceItemDispatchLog>().Where(d => false)); %>
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
    function inquireDispatch() {
        var event = event || window.event;
        var $form = $(event.target).closest('form');
        $form.ajaxSubmit({
            beforeSubmit: function () {
                showLoading();
            },
            success: function (data) {
                hideLoading();
                $('#dispatchList').empty()
                    .append($(data));
            }
        });
    }

    function showInvoiceByDispatch(branchID, status) {
        var $form = $('#queryForm');
        var formData = $form.serializeObject();
        formData.BranchID = branchID;
        formData.DispatchStatus = status;

        showLoading();
        $.post('<%= Url.Action("InquireInvoiceByDispatch","Invoice") %>', formData, function (data) {
            hideLoading();
            if ($.isPlainObject(data)) {
                if (data.result) {

                } else {
                    alert(data.message);
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
    InvoiceQueryViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _profile = Context.GetUser();
        _viewModel = (InvoiceQueryViewModel)ViewBag.ViewModel;
    }

</script>

<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage2017.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
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
    <span class="ribbon-button-alignment">
        <span id="refresh" class="btn btn-ribbon">
            <i class="fa fa-gift"></i>
        </span>
    </span>
    <!-- breadcrumb -->
    <ol class="breadcrumb">
        <li>點數兌換報表</li>
    </ol>
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">
        <article class="col-sm-12 col-md-12 col-lg-12">
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
                        <form action="<%= Url.Action("ListBonusAward","Report") %>" method="post" id="search-form" class="smart-form">
                            <fieldset>
                                <div class="row">
                                    <section class="col col-6">
                                        <label class="label">&nbsp;</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <input type="text" name="startDate" id="startDate" readonly="readonly" class="form-control input-lg date form_date" data-date-format="yyyy/mm/dd" placeholder="請輸入兌換起日" value='' />
                                        </label>
                                    </section>
                                    <section class="col col-6">
                                        <label class="label">&nbsp;</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-calendar"></i>
                                            <input type="text" name="endDate" id="endDate" readonly="readonly" class="form-control input-lg date form_date" data-date-format="yyyy/mm/dd" placeholder="請輸入兌換迄日" value='' />
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <footer>
                                <button type="button" id="btnSend" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
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
    </div>
    <!-- row -->
    <div class="row" id="bonusList">

    </div>

    <script>

        $('#btnSend').on('click', function (evt) {

            var form = $(this)[0].form;

            clearErrors();
            showLoading();
            $($(this)[0].form).ajaxSubmit({
                success: function (data) {
                    hideLoading();
                    $('#bonusList').empty();
                    $(data).appendTo($('#bonusList'));
                }
            });
        });

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

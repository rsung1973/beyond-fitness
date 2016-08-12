<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>

<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-cog"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>修改個人資料</li>
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
        <i class="fa-fw fa fa-cog"></i>修改個人資料
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <!-- NEW COL START -->
        <article class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget" id="wid-id-6" data-widget-editbutton="false" data-widget-custombutton="false" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
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
                    <span class="widget-icon"><i class="fa fa-edit"></i></span>
                    <h2>修改個人資料 </h2>

                </header>

                <!-- widget div-->
                <div>
                    <!-- widget content -->
                    <div class="widget-body no-padding bg-color-darken txt-color-white">

                        <form action="<%= VirtualPathUtility.ToAbsolute("~/Account/EditMySelf") %>" id="pageForm" class="smart-form" method="post">

                            <% Html.RenderPartial("~/Views/Account/RegisterItem.ascx", _model); %>

                            <footer class="text-right">
                                <button type="submit" name="submit" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                            </footer>
                        </form>
                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->
            </div>
        </article>

        <article class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-envelope"></i>聯絡我們</h5>
                <ul class="no-padding no-margin">
                    <ul class="icons-list">
                        <li>
                            <a title="電話" href="tel:+886227152733"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-phone fa-stack-1x"></i></span>(02)2715-2733</a>
                        </li>
                        <li>
                            <a title="地址"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-map-marker fa-stack-1x"></i></span>台北市松山區南京東路四段17號B1</a>
                        </li>
                        <li>
                            <a title="Email" href="mailto:info@beyond-fitness.tw"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-envelope-o fa-stack-1x"></i></span>info@beyond-fitness.tw</a>
                        </li>
                    </ul>
                </ul>
            </div>
            <!-- /well -->
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-external-link"></i>快速功能</h5>
                <ul class="no-padding no-margin">
                    <p class="no-margin">
                        <ul class="no-padding no-margin ">
                            <p class="no-margin ">
                                <ul class="icons-list ">
                                    <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/TimeLine.ascx"); %>
                                    <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/VipOverview.ascx"); %>
                                    <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/Overview.ascx"); %>
                                    <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ViewProfile.ascx"); %>
                                    <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/Logout.ascx"); %>
                                </ul>
                            </p>
                        </ul>
                    </p>
                </ul>
            </div>
            <!-- /well -->
        </article>

    </div>

    <script>
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/EditMySelf") %>')
              .submit();

                });

    </script>
</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;
    RegisterViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterViewModel)this.Model;
    }
</script>

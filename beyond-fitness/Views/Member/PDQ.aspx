﻿<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

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
                <i class="fa fa-user-plus"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>人員管理></li>
            <li>員工管理</li>
            <li>填寫問卷</li>
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
        <i class="fa-fw fa fa-user"></i> VIP管理
                            <span>>  
                                填寫問卷
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">
        <article class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-6" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-deletebutton="false">
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
                    <h2><strong>Step<%= _pdqGroup.GroupID %>.</strong> <i><%= _pdqGroup.GroupName %></i> </h2>
                    <div class="widget-toolbar">

                        <div class="progress progress-striped active" rel="tooltip" data-original-title="20%" data-placement="bottom">
                            <div class="progress-bar progress-bar-success" role="progressbar" style="width: 20%">20%</div>
                        </div>

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
                        <form action="<%= VirtualPathUtility.ToAbsolute("~/Member/UpdatePDQ/") + _model.UID %>" id="pageForm" class="smart-form" method="post">
                            <input type="hidden" name="groupID" value="<%= _pdqGroup.GroupID %>" />
                            <fieldset>
                                <div class="row">
                                <%  
                                    for (int idx = 0; idx < 13; idx++)
                                    {   %>
                                    <section class="col col-6">
                                        <% renderItem(idx); %>
                                    </section>
                                <%  } %>
                                </div>
                            </fieldset>
                            <%  
                                    for (int idx = 13; idx < _items.Length; idx++)
                                    {   %>
                                        <fieldset>
                                        <% renderItem(idx); %>
                                        </fieldset>
                               <%   } %>

                            <div class="widget-footer">

                                <button class="btn btn-lg btn-primary" type="button" onclick="saveAll();">
                                    下一步
                                </button>
                            </div>
                        </form>

                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->

            </div>
            <!-- end widget -->
        </article>

        <!-- NEW COL START -->
        <article class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
            <!-- /well -->
            <div class="well bg-color-darken txt-color-white padding-10">
                <h5 class="margin-top-0"><i class="fa fa-external-link"></i>快速功能</h5>
                <ul class="no-padding no-margin">
                    <p class="no-margin">
                        <ul class="icons-list">
                            <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ListLearners.ascx"); %>
                            <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ListCoaches.ascx"); %>
                            <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ViewLessons.ascx",_model); %>
<%--                            <li>
                                <a title="檢測體能" href="addtesting.html"><span class="fa-stack fa-lg"><i class="fa fa-square-o fa-stack-2x"></i><i class="fa fa-heart fa-stack-1x"></i></span>檢測體能</a>
                            </li>--%>
                            <%  Html.RenderPartial("~/Views/Layout/QuickLinkItem/ViewVip.ascx",_model); %>
                        </ul>
                    </p>
                </ul>
            </div>
            <!-- /well -->

        </article>
        <!-- END COL -->

    </div>
    
    <script>

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/AddLessons") %>')
              .submit();

        });

        function saveAll() {
            $('#pageForm').ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Member/UpdatePDQ/") + _model.UID %>",
                beforeSubmit: function () {
                },
                success: function (data) {
                    if (data.result) {
                        smartAlert("資料已儲存!!", function () {
                            window.location.href = '<%= VirtualPathUtility.ToAbsolute("~/Member/PDQ") + "?id=" + _model.UID + "&groupID=" + (_pdqGroup.GroupID+1) %>';
                        });
                    } else {
                        smartAlert(data.message);
                    }
                },
                error: function () {
                }
            }).submit();
        }

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    PDQQuestion[] _items;
    PDQGroup _pdqGroup;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _pdqGroup = models.GetTable<PDQGroup>().Where(g => g.GroupID == 1).First();
        _items = _pdqGroup.PDQQuestion.ToArray();


    }

    void renderItem(int idx)
    {
        var item = _items[idx];
        ViewBag.PDQTask = item.PDQTask.Where(p => p.UID == _model.UID).FirstOrDefault();
        ViewBag.Answer = item.PDQTask.Where(p => p.UID == _model.UID && !p.SuggestionID.HasValue).FirstOrDefault();
        if (item.QuestionID == _pdqGroup.ConclusionID)
        {
            ViewBag.InlineGroup = false;
            Html.RenderPartial("~/Views/Member/PDQItemII.ascx", item);
        }
        else
        {
            ViewBag.InlineGroup = true;
            Html.RenderPartial("~/Views/Member/PDQItem.ascx", item);
        }
    }

</script>

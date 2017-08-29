﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<section class="">
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
                        <form action="<%= Url.Action("InquireLearner","Learner") %>" method="post" id="search-form" class="smart-form">
                            <fieldset>
                                <div class="row">
                                    <section class="col col-4">
                                        <label class="label">依真實姓名或暱稱查詢</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-user"></i>
                                            <input type="text" name="RealName" class="form-control input" maxlength="20" placeholder="請輸入學員姓名"/>
                                        </label>
                                    </section>
                                    <section class="col col-4">
                                        <label class="label">或依身分證字號查詢</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-id-card-o"></i>
                                            <input type="text" name="IDNo" class="form-control input" maxlength="20" placeholder="請輸入身分證字號"/>
                                        </label>
                                    </section>
                                    <section class="col col-4">
                                        <label class="label">或依聯絡電話查詢</label>
                                        <label class="input input-group">
                                            <i class="icon-append fa fa-phone"></i>
                                            <input type="text" name="Phone" class="form-control input" maxlength="20" placeholder="請輸入合約編號"/>
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <fieldset>
                                <label class="label"><i class="fa fa-tags"></i>更多查詢條件</label>
                                <div class="row">
                                    <section class="col col-3">
                                        <label class="label">學員類型</label>
                                        <label class="select">
                                            <select class="input" name="CurrentTrial">
                                                <option value="">全部</option>
                                                <option value="-1">正式學員</option>
                                                <option value="1">體驗學員</option>
                                            </select>
                                            <i class="icon-append fa fa-file-word-o"></i>
                                        </label>
                                    </section>
                                    <section class="col col-3">
                                        <label class="label">狀態</label>
                                        <label class="select">
                                            <select class="input" name="MemberStatus">
                                                <option value="">全部</option>
                                                <option value="1003">已註冊</option>
                                                <option value="1001">尚未註冊</option>
                                                <option value="1002">已停用</option>
                                            </select>
                                            <i class="icon-append fa fa-file-word-o"></i>
                                        </label>
                                    </section>
                                    <section class="col col-3">
                                        <label class="label">性別</label>
                                        <label class="select">
                                            <select class="input" name="Gender">
                                                <option value="">全部</option>
                                                <option value="M">男</option>
                                                <option value="F">女</option>
                                            </select>
                                            <i class="icon-append fa fa-file-word-o"></i>
                                        </label>
                                    </section>
                                    <section class="col col-3">
                                        <label class="label">是否為運動員</label>
                                        <label class="select">
                                            <select class="input" name="AthleticLevel">
                                                <option value="">全部</option>
                                                <option value="1">是</option>
                                                <option value="0">否</option>
                                            </select>
                                            <i class="icon-append fa fa-file-word-o"></i>
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <footer>
                                <button type="button" onclick="inquireLearner();" name="submit" class="btn btn-primary">
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
                    <h2>人員列表</h2>
                    <div class="widget-toolbar">
                        <a onclick="$global.editLearner();" class="btn btn-primary modifyStudentDialog_link"><i class="fa fa-fw fa-user-plus"></i>新增學員</a>
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
                    <div id="learnerList" class="widget-body bg-color-darken txt-color-white no-padding">
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
    function inquireLearner() {
        var event = event || window.event;
        var $form = $(event.target).closest('form');
        $form.ajaxSubmit({
            beforeSubmit: function () {
                showLoading();
            },
            success: function (data) {
                hideLoading();
                $('#learnerList').empty()
                    .append($(data));
            }
        });
    }
</script>
<script>

    $(function () {

        $global.renderLearnerList = function () {
            showLoading();
            inquireLearner();
        };

        $global.editLearner = function (uid) {
            startLoading();
            $.post('<%= Url.Action("EditLearner","Learner") %>', { 'uid': uid }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
        };

        $global.deleteLearner = function (uid) {
            if (confirm('確定刪除此學員資料?')) {
                startLoading();
                $.post('<%= Url.Action("DeleteLearner","Learner") %>', { 'uid': uid }, function (data) {
                    hideLoading();
                    if ($.isPlainObject(data)) {
                        if (data.result) {
                            if (data.message) {
                                alert(data.message);
                            } else {
                                alert('資料已刪除!!');
                            }
                            inquireLearner();
                        } else {
                            alert(data.message);
                        }
                    } else {
                        $(data).appendTo($('body')).remove();
                    }
                });
            }
        };

        $global.enableLearner = function (uid) {
            startLoading();
            $.post('<%= Url.Action("EnableLearner","Learner") %>', { 'uid': uid }, function (data) {
                hideLoading();
                if ($.isPlainObject(data)) {
                    if (data.result) {
                        if (data.message) {
                            alert(data.message);
                        } else {
                            alert('資料已修改!!');
                        }
                        inquireLearner();
                    } else {
                        alert(data.message);
                    }
                } else {
                    $(data).appendTo($('body')).remove();
                }
            });
        };


        $global.editPDQ = function (uid) {
            startLoading();
            $.post('<%= Url.Action("PDQ","Learner") %>', { 'uid': uid }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
        };

        $global.listAdvisor = function (uid) {
            startLoading();
            $.post('<%= Url.Action("ListFitnessAdvisor","Learner") %>', { 'uid': uid }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
        };


    });

    
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
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
                        <%  Html.RenderPartial("~/Views/Learner/Module/LearnerList.ascx", _model); %>
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

    $(function () {

        $global.renderLearnerList = function () {
            showLoading();
            $('#learnerList').load('<%= Url.Action("LearnerList","Learner") %>', {}, function (data) {
                hideLoading();
            });
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
                            showLoading();
                            window.location.href = '<%= Url.Action("Index","Learner") %>';
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
                        showLoading();
                        window.location.href = '<%= Url.Action("Index","Learner") %>';
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

    function inquirePrice() {
        var event = event || window.event;
        var $form = $(event.target).closest('form');

        showLoading();
        $.post('<%= Url.Action("InquirePriceSeries","LessonPrice") %>', $form.serialize(), function (data) {
            hideLoading();
            $('#priceList').empty()
                .append($(data));
        });

        $.post('<%= Url.Action("InquireProjectCourse","LessonPrice") %>', $form.serialize(), function (data) {
            hideLoading();
            $('#courseList').empty()
                .append($(data));
        });
    }

    function deletePrice(priceID) {
        if (confirm('確定刪除此價目項次?')) {
            var event = event || window.event;
            startLoading();
            $.post('<%= Url.Action("DeleteLessonPrice","LessonPrice") %>', { 'priceID': priceID }, function (data) {
                hideLoading();
                if (data.result) {
                    var $a = $(event.target).closest('a');
                    if (data.message) {
                        alert(data.message);
                        $a.closest('td').prev().text('已停用');
                        $a.remove();
                    } else {
                        alert('價目已刪除!!');
                        $a.closest('tr').remove();
                    }
                } else {
                    alert(data.message);
                }
            });
        }
    }

    function deletePriceSeries(seriesID) {
        if (confirm('確定刪除此標準價目?')) {
            var event = event || window.event;
            startLoading();
            $.post('<%= Url.Action("DeletePriceSeries","LessonPrice") %>', { 'seriesID': seriesID }, function (data) {
                hideLoading();
                if (data.result) {
                    var $a = $(event.target).closest('a');
                    if (data.message) {
                        alert(data.message);
                        $a.closest('td').prev().text('已停用');
                        $a.remove();
                    } else {
                        alert('價目已刪除!!');
                        $a.closest('tr').remove();
                    }
                } else {
                    alert(data.message);
                }
            });
        }
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    IQueryable<UserProfile> _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<UserProfile>)this.Model;
    }

</script>

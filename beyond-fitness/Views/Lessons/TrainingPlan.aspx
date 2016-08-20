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
                <i class="fa fa-edit"></i>
            </span>
        </span>
        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>課程管理></li>
            <li>編輯上課內容</li>
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
        <i class="fa-fw fa fa-edit"></i>編輯上課內容
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="well well-sm bg-color-darken txt-color-white">
                <div class="row">

                    <%--<%  Html.RenderPartial("~/Views/Layout/Carousel.ascx"); %>--%>

                    <div class="col-sm-12">

                        <div class="row">

                            <%  Html.RenderPartial("~/Views/Member/LessonCount.ascx", _model.RegisterLesson.UserProfile); %>
                            <div class="col-xs-8 col-sm-6">
                                <h1>
                                    <span class="semi-bold"><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.RegisterLesson.UID %>"><%= _model.RegisterLesson.UserProfile.RealName %> <%= _model.RegisterLesson.UserProfile.UserName %></a></span>
                                </h1>
                                <p class="font-md">關於<%= _model.RegisterLesson.UserProfile.RealName %>...</p>
                                <p>
                                    <form action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitPlan") %>" class="smart-form" method="post">
                                        <fieldset>
                                            <section>
                                                <label class="textarea">
                                                    <textarea rows="3" id="recentStatus" name="recentStatus" class="custom-scroll"><%= _model.RegisterLesson.UserProfile.RecentStatus %></textarea>
                                                </label>
                                                <div class="note">
                                                    <strong>Note:</strong> 最多輸入250個中英文字
                                                </div>
                                            </section>
                                        </fieldset>
                                        <p class="text-right">
                                            <button type="button" name="submit" class="btn btn-primary btn-sm" id="btnUpdateStatus" onclick="commitPlan();">
                                                <i class="fa fa-reply"></i>更新
                                            </button>
                                        </p>
                                    </form>
                                </p>
                            </div>
                            <div class="col-xs-12 col-sm-3">
                                <%  Html.RenderPartial("~/Views/Member/ContactInfo.ascx", _model.RegisterLesson.UserProfile); %>
                                <%  Html.RenderPartial("~/Views/Member/UserAssessmentInfo.ascx", _model.RegisterLesson.UserProfile); %>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <hr>
                </div>
                <div class="row no-padding">
                    <div class="col-sm-12">
                        <label><%= _model.ClassDate.ToString("yyyy/MM/dd") %> <%= String.Format("{0:00}",_model.Hour) %>:00-<%= String.Format("{0:00}",_model.Hour+1) %>:00 上課內容</label>
                        <ul class="nav nav-tabs tabs-pull-right">
                            <li>
                                <a data-toggle="tab" href="#s5"><i class="fa fa-commenting-o"></i><span>有話想說</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s4"><i class="fa fa-pie-chart"></i><span>評量指數</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s3"><i class="fa fa-child"></i><span>收操</span></a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#s2"><i class="fa fa-heartbeat"></i><span>訓練內容</span></a>
                            </li>
                            <li class="active">
                                <a data-toggle="tab" href="#s1"><i class="fa fa-child "></i><span>暖身</span></a>
                            </li>
                        </ul>
                        <div class="tab-content padding-top-10">
                            <div class="tab-pane fade in active" id="s1">
                                <div class="chat-body no-padding profile-message">
                                    <ul>
                                        <li class="message">
                                            <% _model.LessonTime.AsAttendingCoach.UserProfile.RenderUserPicture(Writer, new { @class = "profileImg online" }); %>
                                            <span class="message-text">
                                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.LessonTime.AttendingCoach %>"><%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></a>
                                                <%= _plan.Warming %>
                                            </span>
                                        </li>
                                        <li>
                                            <div class="input-group wall-comment-reply">
                                                <input id="warming" type="text" name="warming" class="form-control" placeholder="請輸入50個中英文字" value="<%= _plan.Warming %>" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-primary" id="btnUpdateWarming" onclick="commitPlan();">
                                                        <i class="fa fa-reply"></i>更新
                                                    </button>
                                                </span>
                                            </div>
                                        </li>
                                    </ul>

                                </div>
                            </div>
                            <div class="tab-pane fade" id="s2">
                                <form action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/UpdateTrainingItemSequence/") + _model.LessonID %>" method="post" id="updateSeq">
                                    <% Html.RenderAction("SingleTrainingExecutionPlan","Lessons", new { LessonID = _model.LessonID }); %>
                                </form>
                            </div>
                            <div class="tab-pane fade" id="s3">
                                <div class="chat-body no-padding profile-message">
                                    <ul>
                                        <li class="message">
                                            <% _model.LessonTime.AsAttendingCoach.UserProfile.RenderUserPicture(Writer, new { @class = "profileImg online" }); %>
                                            <span class="message-text">
                                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.LessonTime.AttendingCoach %>"><%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></a>
                                                <%= _plan.EndingOperation %>
                                            </span>
                                        </li>
                                        <li>
                                            <div class="input-group wall-comment-reply">
                                                <input id="endingOperation" name="endingOperation" type="text" class="form-control" placeholder="請輸入50個中英文字" value="<%= _plan.EndingOperation %>" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-primary" onclick="commitPlan();">
                                                        <i class="fa fa-reply"></i>更新
                                                    </button>
                                                </span>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="s4">
                                <% Html.RenderPartial("~/Views/Lessons/LessonTrendItem.ascx", _model); %>
                            </div>
                            <div class="tab-pane fade" id="s5">
                                <div class="chat-body no-padding profile-message">
                                    <ul>
                                        <li class="message">
                                            <% _model.LessonTime.AsAttendingCoach.UserProfile.RenderUserPicture(Writer, new { @class = "profileImg online" }); %>
                                            <span class="message-text">
                                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.LessonTime.AttendingCoach %>"><%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></a>
                                                <%= _plan.Remark %>
                                            </span>
                                        </li>
                                        <li>
                                            <div class="input-group wall-comment-reply">
                                                <input id="remark" name="remark" type="text" class="form-control" placeholder="請輸入50個中英文字" value="<%= _plan.Remark %>" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-primary" onclick="commitPlan();">
                                                        <i class="fa fa-reply"></i>更新
                                                    </button>
                                                </span>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </article>
    </div>
    <%  Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>
    <script>

        function commitPlan() {
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitPlan") %>',
                {
                    'recentStatus': $('#recentStatus').val(),
                    'warming': $('#warming').val(),
                    'endingOperation': $('#endingOperation').val(),
                    'remark': $('#remark').val()
                }, function (data) {
                if (data.result) {
                    smartAlert('資料已更新!!');
                } else {
                    smartAlert(data.message);
                }
            });
        }

        function deleteTraining(itemID) {
            confirmIt({ title: '刪除訓練組數', message: '確定刪除此訓練組數?' }, function (evt) {
                startLoading();
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeleteTraining/") %>' + itemID, {}, function (data) {
                    $('#s2').empty().append($(data));
                    finishLoading();
                });
            });
        }

        function deletePlan() {
            confirmIt({ title: '刪除課表', message: '確定刪除此課表?' }, function (evt) {
                startLoading();
                $('<form method="post"/>').appendTo($('body'))
                    .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeletePlan") %>')
                    .submit();
            });
        }

        function addTraining() {
            startLoading();
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/AddTraining") %>', {}, function (data) {
                if (data) {
                    $('#trainingPlan').before($(data));
                }
            });
        }


        var $modal;
        function addTrainingItem(executionID) {
            $('#addItem').remove();
            $modal = $('<div class="modal fade" id="addItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" />');
            $('#loading').css('display', 'table');
            $modal.appendTo($('#content'))
                .load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/AddTrainingItem") %>' + '?id=' + executionID , {}, function () {
                    $('#loading').css('display', 'none');
                    $modal.modal('show');
                });
        }

        function editTrainingItem(executionID, itemID) {
            $('#addItem').remove();
            $modal = $('<div class="modal fade" id="addItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" />');
            $('#loading').css('display', 'table');
            $modal.appendTo($('#content'))
                .load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/EditTrainingItem") %>', { 'executionID': executionID, 'itemID': itemID }, function () {
                    $('#loading').css('display', 'none');
                    $modal.modal('show');
                });
        }


        function deleteItem(executionID,itemID) {
            var event = event || window.event;
            confirmIt({ title: '刪除訓練項目', message: '確定刪除此訓練項目?' }, function (evt) {
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeleteTrainingItem") %>', { 'itemID': itemID,'executionID':executionID }, function (data) {
                    if (data.result) {
                        $(event.target).parent().parent().parent().remove();
                    } else {
                        smartAlert(data.message);
                    }
                });
            });
        }

        function moveItem(direction) {
            var $tr = $(event.target).parent().parent().parent();
            if (direction == 'up') {
                var $target = $tr.prev();
                $target.before($tr);
            } else if (direction == 'down') {
                var $target = $tr.next();
                $target.after($tr);
            }
            $('.fa-arrow-circle-o-up').removeClass('disabled');
            $('.fa-arrow-circle-o-down').removeClass('disabled');
            $('.fa-arrow-circle-o-up').first().addClass('disabled');
            $('.fa-arrow-circle-o-down').last().addClass('disabled');

        }



        function commitTrainingItem() {
            $('#addItem').find('form').ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitTrainingItem") %>",
                beforeSubmit: function () {
                },
                success: function (data) {
                    if (data.result) {
                        smartAlert("資料已儲存!!", function (message) {
                            $modal.modal('hide');
                            //$('#addItem').remove();
                            $('#updateSeq').ajaxForm({
                                success: function (data) {
                                    $('#updateSeq').html(data);
                                }
                            }).submit();
                        });
                    } else {
                        smartAlert(data.message, function () {
                            $modal.modal('hide');
                            //$('#addItem').remove();
                        });
                    }
                },
                error: function () {
                }
            }).submit();
        }

        function commitTraining() {

            var event = event || window.event;
            $(event.target).parents('form').ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitTraining") %>",
                beforeSubmit: function () {
                },
                success: function (data) {
                    if (data.result) {
                        smartAlert("資料已儲存!!", function (message) {
                        });
                    } else {
                        smartAlert(data.message, function () {
                        });
                    }
                },
                error: function () {
                }
            }).submit();
        }

        function commitAssessment() {
            $('#assessment').ajaxForm({
                url: "<%= VirtualPathUtility.ToAbsolute("~/Attendance/CommitAssessment") %>",
                beforeSubmit: function () {
                },
                success: function (data) {
                    if (data.result) {
                        smartAlert("資料已儲存!!", function (message) {
                        });
                    } else {
                        if (data.forceLogout) {
                            window.location.href = '<%= VirtualPathUtility.ToAbsolute("~/Account/AlertTimeout") %>';
                        } else {
                            smartAlert(data.message, function () {
                            });
                        }
                    }
                },
                error: function () {
                }
            }).submit();
        }

    function previewLesson(arg)
    {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/PreviewLesson") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        startLoading();
        $form.submit();
    }

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonTimeExpansion _model;
    LessonPlan _plan;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonTimeExpansion)this.Model;
        _plan = _model.LessonTime.LessonPlan ?? new LessonPlan { };
    }



</script>

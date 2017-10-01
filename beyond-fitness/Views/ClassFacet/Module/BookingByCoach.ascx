﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="bookingDialog" title="預約課程" class="bg-color-darken">
    <form class="smart-form" id="bookingForm" autofocus>
        <fieldset>
            <div class="row">
                <section class="col col-4">
                    <label class="label">請選擇課程類別</label>
                    <label class="select">
                        <select id="lessonType">
                            <option value="0">P.T session</option>
                            <option value="1">P.I session</option>
                        </select>
                        <script>
                            $('#lessonType').on('change', function (evt) {
                                var lessonType = $(this).val();

                                switch (lessonType)
                                {
                                    case '0':
                                        $('.part0').css('display', 'block');
                                        break;
                                    case '1':
                                        $('.part0').css('display', 'none');
                                        break;
                                }
                            });
                        </script>
                        <i class="icon-append fa fa-clock-o"></i>
                    </label>
                </section>
                <section class="col col-4">
                    <label class="label">請選擇開始時間</label>
                    <label class="input">
                        <i class="icon-append fa fa-calendar"></i>
                        <input type="text" name="ClassDate" id="classDate" class="form-control date input_time" data-date-format="yyyy/mm/dd hh:ii" readonly="readonly" value="<%= String.Format("{0:yyyy/MM/dd HH:mm}",_viewModel.LessonDate) %>" placeholder="請輸入上課開始時間" />
                    </label>
                </section>
                <section class="col col-4">
                    <label class="label">請選擇上課地點</label>
                    <label class="select">
                        <select name="BranchID">
                            <%  Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.ascx", model: _viewModel.BranchID); %>
                        </select>
                        <i class="icon-append fa fa-file-word-o"></i>
                    </label>
                </section>
            </div>
        </fieldset>
        <%--<fieldset>
            <div class="row">
                <section class="col col-6">
                    <label class="label">請選擇上課長度</label>
                    <label class="select">
                        <select name="Duration" class="input-lg">
                            <option value="60" <%= _viewModel.Duration==60 ? "selected": null %>>60 分鐘</option>
                            <option value="90" <%= _viewModel.Duration==90 ? "selected": null %>>90 分鐘</option>
                        </select>
                        <i class="icon-append fa fa-file-word-o"></i>
                    </label>
                </section>
                
            </div>
        </fieldset>--%>
        <fieldset class="part0">
            <div class="row">
                <section id="attendeeSelector" class="col col-12">
                    <%
                        var items = models.GetTable<RegisterLesson>()
                            .Where(l => l.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                            .Where(l => l.Attended != (int)Naming.LessonStatus.課程結束
                                && l.UID == _viewModel.UID)
                            .Where(l => l.Lessons > l.GroupingLesson.LessonTime.Count)
                            .OrderBy(l => l.ClassLevel).ThenBy(l => l.Lessons);

                        Html.RenderPartial("~/Views/Lessons/AttendeeSelector.ascx", items);
                     %>
                </section>
            </div>
        </fieldset>
        <input type="hidden" name="CoachID" value="<%= _viewModel.CoachID %>" />
        <input type="hidden" name="UID" value="<%= _viewModel.UID %>" />
    </form>
    <script>
        $(function () {
            $global.commitBooking = function (callback) {
                var lessonType = $('#lessonType').val();
                switch (lessonType) {
                    case '0':
                        $.post('<%= Url.Action("CommitBookingByCoach","Lessons") %>', $('#bookingForm').serialize(), function (data) {
                            if (data.result) {
                                smartAlert(data.message);
                                callback();
                            } else {
                                $(data).appendTo('body').remove();
                            }
                        });
                        break;
                    case '1':
                        $.post('<%= Url.Action("CommitBookingByCoach","Lessons") %>', $('#bookingForm').serialize()+'&trainingBySelf=1', function (data) {
                            if (data.result) {
                                smartAlert(data.message);
                                callback();
                            } else {
                                $(data).appendTo('body').remove();
                            }
                        });
                        break;
                }
            };
        });
        $('.input_time').datetimepicker({
            language: 'zh-TW',
            weekStart: 0,
            todayBtn: 1,
            clearBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 0,
            minuteStep: 30,
            forceParse: 0,
            startDate: '<%= String.Format("{0:yyyy-MM-dd}",DateTime.Today) %>'
        });
    </script>
</div>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    FullCalendarViewModel _viewModel;
    UserProfile _profile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (FullCalendarViewModel)ViewBag.ViewModel;
        _profile = Context.GetUser();
    }

</script>

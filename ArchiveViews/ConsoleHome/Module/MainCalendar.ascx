﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<div id="<%= _calendarID %>" class="m-t-20"></div>
<script>
    var refetchCalendarEvents;

    $(function () {

        showLoading();

        if ($("#<%= _calendarID %>").length) {
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            var calendar = $('#<%= _calendarID %>').fullCalendar({
                draggable: true,
                selectable: false,
                selectHelper: true,
                unselectAuto: false,
                disableResizing: false,
                allDaySlot: true,
                editable: false,
                height: "auto",
                defaultView: "agendaWeek",
                defaultDate: <%= JsonConvert.SerializeObject(startDate) %>,
                firstDay: 1,
                businessHours: {
                    start: '08:00',
                    end: '22:00',
                    dow: [1, 2, 3, 4, 5, 6, 0]
                },
                minTime: "06:00:00",
                maxTime: "24:00:00",
                header: {
                    left: 'prev',
                    center: 'title',
                    right: 'next'
                },
                eventLimit: 3,
                events: function (start, end, timezone, callback) {

                    $global.viewModel.DateFrom = start.format('YYYY-MM-DD');
                    $global.viewModel.DateTo = end.format('YYYY-MM-DD');
                    //$global.viewModel.DefaultView = defaultView;
                    $.post('<%= Url.Action("CalendarEvents","ConsoleHome") %>', $global.viewModel, function (data) {
                        callback(data);
                    });
                },
                eventRender: function (event, element, icon) {
                    if (!event.description == "") {
                        element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.description + "</span>");
                    }
                    if (!event.icon == "") {
                        element.find('.fc-title').append("<i class='" + event.icon + " '></i>");
                    }
                },

                eventClick: function (calEvent, jsEvent, view) {
                    if (calEvent.allDay == true) {
                        //$('#undolistDialog').dialog('open');
                    } else {
                        if (calEvent.id.indexOf('my') == 0) {
                            showUserEventModal(calEvent.keyID);
                        } else {
                            showLessonEventModal(calEvent.keyID);
                        }
                    }
                },

                dayClick: function (date, allDay, jsEvent, view) {
                    showLoading();
                    $.post('<%= Url.Action("AddEvent", "ConsoleEvent") %>', { 'startDate': date.format('YYYY-MM-DD HH:mm:ss') }, function (data) {
                        hideLoading();
                        if ($.isPlainObject(data)) {
                            alert(data.message);
                        } else {
                            $(data).appendTo($('body'));
                        }
                    });
                    //var formattedDate = date.toString().replace(" GMT+0000", "");
                    //$('#moreEventModal').modal('show');
                    //alert("see, I can get " + formattedDate + " if I _left click_, but try doing this with a right click");
                },
                eventDrop: function (calEvent, delta, revertFunc, jsEvent, ui, view) {

                    showLoading();
                    if (calEvent.id.indexOf('my') == 0) {
                        $.post('<%= Url.Action("UpdateCoachEvent","CoachFacet",new { _model.UID }) %>', {
                            'startDate': calEvent.start.format('YYYY-MM-DD HH:mm:ss'),
                            'endDate': calEvent.end.format('YYYY-MM-DD HH:mm:ss'),
                            'keyID': calEvent.keyID,
                        }, function (data) {
                            hideLoading();
                            if (data.result) {
                                refreshEvents();
                                //alert(data.message);
                                swal("幹得好!", "更新時間成功!", "success");
                            } else {
                                revertFunc();
                                var $dialog = $(data);
                                $dialog.appendTo('body').remove();
                            }
                        });
                    } else {
                        $.post('<%= Url.Action("UpdateBookingByCoach","CoachFacet") %>', {
                            'keyID': calEvent.keyID,
                            'classTimeStart': calEvent.start.format('YYYY-MM-DD HH:mm:ss'),
                            'classTimeEnd': calEvent.end.format('YYYY-MM-DD HH:mm:ss'),
                        }, function (data) {
                            hideLoading();
                            if (data.result) {
                                refreshEvents();
                                swal("幹得好!", "更新時間成功!", "success");
                                //alert(data.message);
                            } else {
                                revertFunc();
                                var $dialog = $(data);
                                $dialog.appendTo('body').remove();
                            }
                        });
                    }
                },
                eventResize: function (calEvent, delta, revertFunc, jsEvent, ui, view) {

                    showLoading();
                    if (calEvent.id.indexOf('my') == 0) {
                        $.post('<%= Url.Action("UpdateCoachEvent","CoachFacet",new { _model.UID }) %>',
                            {
                                'keyID': calEvent.keyID,
                                'startDate': calEvent.start.format('YYYY-MM-DD HH:mm:ss'),
                                'endDate': calEvent.end.format('YYYY-MM-DD HH:mm:ss'),
                            }, function (data) {
                                hideLoading();
                                if (data.result) {
                                    refreshEvents();
                                    swal("幹得好!", "更新時間成功!", "success");
                                    //alert(data.message);
                                } else {
                                    revertFunc();
                                    var $dialog = $(data);
                                    $dialog.appendTo('body').remove();
                                }
                            });
                    } else {
                        $.post('<%= Url.Action("UpdateBookingByCoach","CoachFacet") %>',
                            {
                                'keyID': calEvent.keyID,
                                'classTimeStart': calEvent.start.format('YYYY-MM-DD HH:mm:ss'),
                                'classTimeEnd': calEvent.end.format('YYYY-MM-DD HH:mm:ss'),
                            }, function (data) {
                                hideLoading();
                                if (data.result) {
                                    refreshEvents();
                                    swal("幹得好!", "更新時間成功!", "success");
                                    //alert(data.message);
                                } else {
                                    revertFunc();
                                    var $dialog = $(data);
                                    $dialog.appendTo('body').remove();
                                }
                            });
                    }
                },
                windowResize: function (event, ui) {
                    $('#<%= _calendarID %>').fullCalendar('render');
                },
                eventAfterAllRender: function (view) {
                    hideLoading();
                },
            });

            refetchCalendarEvents = function () {
                $('#<%= _calendarID %>').fullCalendar("refetchEvents");
            };

        }


        /* hide default buttons */
        // Previous month action
        $('#cal-prev').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('prev');
        });

        // Next month action
        $('#cal-next').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('next');
        });

        // calendar today
        $('#<%= _calendarID %>-buttons #btn-today').click(function () {
            $('.fc-button-today').click();
            return false;
        });


        // calendar agenda day
        $('#td').click(function () {
            $('#<%= _calendarID %>').fullCalendar('changeView', 'agendaDay');
        });

        // Change to month view
        $('#mt').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('changeView', 'month');

            // safari fix
            $('#content .main').fadeOut(0, function () {
                setTimeout(function () {
                    $('#content .main').css({ 'display': 'table' });
                }, 0);
            });

        });

        // Change to week view
        $('#ag').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('changeView', 'agendaWeek');

            // safari fix
            $('#content .main').fadeOut(0, function () {
                setTimeout(function () {
                    $('#content .main').css({ 'display': 'table' });
                }, 0);
            });

        });

        // Change to day view
        $('#td').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('changeView', 'agendaDay');

            // safari fix
            $('#content .main').fadeOut(0, function () {
                setTimeout(function () {
                    $('#content .main').css({ 'display': 'table' });
                }, 0);
            });

        });

        // Change to today view
        $('#today').on('click', function () {
            $('#<%= _calendarID %>').fullCalendar('today');
        });

    });
</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    String _calendarID = $"calendar{DateTime.Now.Ticks}";
    DateTime? startDate;
    DailyBookingQueryViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _viewModel = (DailyBookingQueryViewModel)ViewBag.ViewModel;
        startDate = _viewModel.DateFrom ?? DateTime.Today;
    }


</script>

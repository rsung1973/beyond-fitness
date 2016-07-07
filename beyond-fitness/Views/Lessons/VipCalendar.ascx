<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<link rel='stylesheet' href='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/lib/cupertino/jquery-ui.min.css")  %>' />
<link href='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/fullcalendar.css")  %>' rel='stylesheet' />
<link href='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/fullcalendar.print.css")  %>' rel='stylesheet' media='print' />

<script type="text/javascript" src='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/lib/moment.min.js") %>'></script>
<script type="text/javascript" src='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/fullcalendar.min.js") %>'></script>
<script type="text/javascript" src='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/lang-all.js") %>'></script>

<div id='calendar'></div>

<script>
    $(document).ready(function () {

        $('#calendar').fullCalendar({
            lang: 'zh-TW',
            theme: true,
            header: {
                left: 'prev,next today',
                center: 'title'
            },
            defaultDate: '<%= DateTime.Today.ToString("yyyy-MM-dd") %>',
            editable: false,
            eventLimit: false, // allow "more" link when too many events
            events: '<%= VirtualPathUtility.ToAbsolute("~/Lessons/VipEvents") %>',
            aspectRatio: 1,
            eventClick: function (calEvent, jsEvent, view) {

                pageParam.lessonDate = calEvent.start.format('YYYY-MM-DD');
                $('#dailyBooking').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingList") %>', { 'lessonDate': calEvent.start.format('YYYY-MM-DD') }, function () { });
                plotData(calEvent.start.format('YYYY-MM-DD'));
                //$('#attendeeList').empty();
                $('#attendeeList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingMembers") %>', { 'lessonDate': calEvent.start.format('YYYY-MM-DD') }, function () { });

                //alert('Event: ' + calEvent.title);
                //alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                //alert('View: ' + view.name);

                //// change the border color just for fun
                //$(this).css('border-color', 'red');
            }

        });
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

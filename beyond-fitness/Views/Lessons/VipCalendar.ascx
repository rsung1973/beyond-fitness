<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<link rel='stylesheet' href='<%= VirtualPathUtility.ToAbsolute("~/fullcalendar-2.8.0/lib/custom/jquery-ui.min.css")  %>' />
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
            <%--defaultDate: '<%= DateTime.Today.ToString("yyyy-MM-dd") %>',--%>
            nowIndicator: true,
            editable: false,
            eventLimit: false, // allow "more" link when too many events
            events: '<%= VirtualPathUtility.ToAbsolute("~/Lessons/VipEvents") %>',
            aspectRatio: 1,
            eventColor: '#666',
            eventRender: function (event, element, view) {
                $(element).height(50)
                    .prop('href', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/VipDay/") %>' + event.lessonID);
            }
<%--            ,
            eventClick: function (calEvent, jsEvent, view) {
                var lessonDate = calEvent.start.format('YYYY-MM-DD');
                var $this = $(this);
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/VipEvent") %>', { 'lessonDate': lessonDate }, function (data) {
                    if (data) {
                        if (data.result == false) {
                            alert(data.message);
                            return;
                        } else {
                            $(data).appendTo($('#calendar'));
                        }
                    }
                });
            },
            dayClick: function (calEvent, jsEvent, view) {
                //alert(calEvent);
                var lessonDate = calEvent.format('YYYY-MM-DD');
                var $this = $(this);
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/VipEvent") %>', { 'lessonDate': lessonDate }, function (data) {
                    if (data) {
                        if (data.result == false) {
                            alert(data.message);
                            return;
                        } else {
                            $(data).appendTo($('#calendar'));
                        }
                    }
                });
            }--%>
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

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<link href="<%= VirtualPathUtility.ToAbsolute("~/flot/custom.css") %>" rel="stylesheet" type="text/css">
<script src="<%= VirtualPathUtility.ToAbsolute("~/flot/jquery.flot.js") %>" type="text/javascript"></script>
<script type="text/javascript" src="<%= VirtualPathUtility.ToAbsolute("~/flot/jquery.flot.categories.js") %>"></script>

<div class="stack-container">
    <div id="placeholder" class="stack-placeholder"></div>
</div>

<script>

    function plotData(lessonDate) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingSummary") %>', { 'lessonDate': lessonDate }, function (data) {
            $.plot("#placeholder", [data], {
                series: {
                    bars: {
                        show: true,
                        barWidth: 0.6,
                        align: "center"
                    }
                },
                xaxis: {
                    mode: "categories",
                    tickLength: 0
                },
                yaxis: {
                    minTickSize: 1,
                    tickDecimals: 0,
                    min: 0
                }
            });
        });
    }

    $(function () {

        var data = <% Html.RenderAction("DailyBookingSummary", "Lessons", new { lessonDate = _lessonDate }); %>;

        $.plot("#placeholder", [data], {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.6,
                    align: "center"
                }
            },
            xaxis: {
                mode: "categories",
                tickLength: 0
            },
            yaxis: {
                minTickSize: 1,
                tickDecimals: 0,
                min: 0
            }
        });

        // Add the Flot version string to the footer

        $("#footer").prepend("Flot " + $.plot.version + " &ndash; ");
    });

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _lessonDate = (DateTime?)this.Model;
    }

</script>

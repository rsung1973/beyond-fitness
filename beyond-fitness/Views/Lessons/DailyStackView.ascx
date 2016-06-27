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

    var $p;

    function plotData(lessonDate) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingSummary") %>', { 'lessonDate': lessonDate }, function (data) {
            $p = $.plot("#placeholder", [data], {
                series: {
                    bars: {
                        show: true,
                        barWidth: 0.6,
                        align: "center",
                        fillColor: {
                            colors: ["#f8ba01", "#f36510"]
                        }
                    },
                    color: "#f8ba01"
                },
                grid: {
                    hoverable: true,
                    clickable: false,
                    borderColor: '#666',
                    labelMargin: 8,
                    backgroundColor: { colors: ["#eee", "#ddd"] },
                    borderWidth: {
                        top: 1,
                        right: 1,
                        bottom: 2,
                        left: 2
                    }
                },
                xaxis: {
                    //mode: "categories",
                    tickLength: 0,
                    ticks: [[8, '8:00'], [9, '9:00'], [10, '10:00'], [11, '11:00'], [12, '12:00'], [13, '13:00'], [14, '14:00'], [15, '15:00'], [16, '16:00'], [17, '17:00'], [18, '18:00'], [19, '19:00'], [20, '20:00'], [21, '21:00'], [22, '22:00']]
                },
                yaxis: {
                    minTickSize: 5,
                    tickDecimals: 0,
                    min: 0,
                    tickFormatter: function (val, axis) {
                        return val + "人";
                    }
                }
            });

            $.each($p.getData()[0].data, function(i, el){
                var o = $p.pointOffset({x: el[0], y: el[1]});
                if(el[1]>0) {
                    console.log(o);
                    $('<div class="data-point-label">' + el[1] + '人</div>').css( {
                        position: 'absolute',
                        left: o.left - 15,
                        top: o.top - 20,
                        display: 'none'
                    }).appendTo($p.getPlaceholder()).fadeIn('slow');
                }
            });


        });
    }

    $(function () {

        plotData('<%= _lessonDate.Value.ToString("yyyy-MM-dd") %>');
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

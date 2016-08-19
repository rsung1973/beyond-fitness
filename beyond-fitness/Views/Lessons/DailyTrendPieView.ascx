<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

    <div id="placeholder" class="chart-large has-legend-unique"></div>

<script>

    $(function () {
        var lessonDate = <%= JsonConvert.SerializeObject(new
                    {
                        classDate = _model.ClassDate.ToString("yyyy-MM-dd"),
                        hour = _model.Hour,
                        registerID = _model.RegisterID,
                        lessonID = _model.LessonID
                    }) %>;

        $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyTrendPie") %>', lessonDate, function (data) {
            drawPie($("#placeholder"),data);
        });
    });


</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTimeExpansion _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTimeExpansion)this.Model;
    }

</script>

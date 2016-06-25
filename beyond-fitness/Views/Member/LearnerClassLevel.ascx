<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="form-group has-feedback">
    <label class="control-label" for="classLevel">課程類別：</label>
    <%= Html.DropDownList("classLevel", _items, new { @class = "form-control" }) %>
</div>

<script runat="server">

    LessonViewModel _model;
    IEnumerable<SelectListItem> _items;
    List<SelectListItem> _groups;

    //static String[] _Favorable =
    //    {
    //        "",
    //        "1 vs 2 = 每人7折",
    //        "1 vs 3 = 每人6.5折",
    //        "1 vs 4 = 每人6折",
    //        "1 vs 5 = 每人5.5折",
    //        "1 vs 6 = 每人5折"
    //    };

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _model = this.Model as LessonViewModel;
        var models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = models.GetTable<LessonPriceType>()
            .Select(l => new SelectListItem
            {
                Text = l.Description + " " + l.ListPrice,
                Value = l.PriceID.ToString(),
                Selected = l.PriceID == _model.ClassLevel
            });
    }
</script>

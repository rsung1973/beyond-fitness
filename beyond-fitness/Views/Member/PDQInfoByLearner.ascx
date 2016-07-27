<%@  Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<h4><span class="fa fa-hourglass-start">第一步：目標</span></h4>
<table class="panel panel-default table">
    <%  
        ViewBag.Offset = 0;
        for (int idx = 0; idx < 7; idx++)
        {
            renderItem(idx);
        } %>
</table>
<h4><span class="fa fa-hourglass-half">第二步：風格</span></h4>
<table class="panel panel-default table">
    <%                                  
        ViewBag.Offset = 7;
        for (int idx = 7; idx < 13; idx++)
        {
            renderItem(idx);
        } %>
</table>
<h4><span class="fa fa-hourglass-end">第三步：訓練水平</span></h4>
<table class="panel panel-default table">
    <%  
        ViewBag.Offset = 13;
        for (int idx = 13; idx < 18; idx++)
        {
            renderItem(idx);
        } %>
</table>
<h4><span class="fa fa-hourglass">第四步：參與目標動機</span></h4>
<table class="panel panel-default table">
    <%                                  
        ViewBag.Offset = 18;
        for (int idx = 18; idx < 30; idx++)
        {
            renderItem(idx);
        } %>
</table>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    PDQQuestion[] _items;
    UserProfile _model;
    Dictionary<int, String> _evalIndex;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _items = (PDQQuestion[])ViewBag.DataItems;
        _evalIndex = new Dictionary<int, string>();
        _evalIndex[7] = null;
        _evalIndex[11] = "風格評分：";
        _evalIndex[16] = null;
        _evalIndex[28] = null;

    }

    void renderItem(int idx)
    {
        var item = _items[idx];
        ViewBag.PDQTask = item.PDQTask.Where(p => p.UID == _model.UID).FirstOrDefault();
        if (_evalIndex.ContainsKey(item.QuestionID))
        {
            ViewBag.AdditionalTitle = _evalIndex[item.QuestionID];
            Html.RenderPartial("~/Views/Member/PDQItemInfoII.ascx", item);
        }
        else
        {
            Html.RenderPartial("~/Views/Member/PDQItemInfo.ascx", item);
        }
    }

</script>

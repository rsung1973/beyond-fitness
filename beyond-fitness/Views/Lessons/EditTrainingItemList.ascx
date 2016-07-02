<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table class="table" id="itemList">
    <tr class="info">
        <th>肌力訓練</th>
        <th>目標次數</th>
        <th>目標強度</th>
        <th>功能</th>
    </tr>
    <%  foreach (var item in _item.TrainingItem)
        {
            Html.RenderPartial("~/Views/Lessons/EditTrainingItem.ascx", item);
        } %>
</table>
<% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    TrainingExecution _item;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (TrainingExecution)this.Model;
    }

</script>

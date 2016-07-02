<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>


    <tr>
        <td>
            <% Html.RenderPartial("~/Views/Lessons/BodyPartsSelector.ascx", _model.TrainingID); %>
            <input type="text" class="form-control" name="description" value="<%= _model.Description %>">
        </td>
        <td>
            <input type="text" class="form-control" name="goalTurns" value="<%= _model.GoalTurns %>">
            <span>例：1、2、3...</span>
            <%--<% Html.RenderPartial("~/Views/Lessons/GoalTurnsSelector.ascx", _model.GoalTurns.HasValue ? _model.GoalTurns.Value : 0); %>--%>
        </td>
        <td>
            <input type="text" class="form-control" name="goalStrength" value="<%= _model.GoalStrength %>">
            <span>例：20KG或30秒</span>
        </td>
        <td>
            <a onclick="deleteItem(<%= _model.ItemID %>);" class="btn btn-system btn-small">刪除 <i class="fa fa-trash-o" aria-hidden="true"></i></a>
        </td>
    </tr>



<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    TrainingItem _model;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (TrainingItem)this.Model;
    }

</script>

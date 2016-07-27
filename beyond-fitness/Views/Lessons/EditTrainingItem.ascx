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
        <td><a onclick="deleteItem(<%= _model.ItemID %>);" class="red-text fa fa-minus-circle fa-2x"></a></td>
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
            <input type="text" class="form-control" name="goalStrength" value="<%= _model.GoalStrength %>" maxlength="100">
            <span>例：20KG或30秒</span>
        </td>
        <td>
            <input type="text" name="remark" class="form-control" value="<%= _model.Remark %>" placeholder="請輸入100個字內的備註"/>
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

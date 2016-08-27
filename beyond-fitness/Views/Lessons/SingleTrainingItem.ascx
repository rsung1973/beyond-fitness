<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_model.TrainingID.HasValue)
    { %>
        <tr>
            <td>
                <a onclick="editTrainingItem(<%= _model.ExecutionID %>,<%= _model.ItemID %>);">
                    <i class="fa fa-pencil-square-o fa-2x text-warning btn btn-xs bg-color-orange"></i>
                </a>
                <a onclick="deleteItem(<%= _model.ExecutionID %>,<%= _model.ItemID %>);">
                    <i class="fa fa-trash-o fa-2x btn btn-xs bg-color-redLight"></i>
                </a>
                <a onclick="moveItem('up');">
                    <i class="fa fa-arrow-circle-o-up fa-2x btn btn-xs bg-color-blueLight"></i>
                </a>
                <a onclick="moveItem('down');">
                    <i class="fa fa-arrow-circle-o-down fa-2x btn btn-xs bg-color-blueLight"></i>
                </a>
                <input type="hidden" name="ItemID" value="<%= _model.ItemID %>" />
            </td>
            <td><%= _model.TrainingType.BodyParts %><%= String.IsNullOrEmpty(_model.Description) ? null : "【" + _model.Description + "】" %></td>
            <td><%= String.IsNullOrEmpty(_model.ActualTurns) ? "--" : _model.ActualTurns %>/<%= String.IsNullOrEmpty(_model.GoalTurns) ? "--" : _model.GoalTurns %></td>
            <td><%= String.IsNullOrEmpty(_model.ActualStrength) ? "--" : _model.ActualStrength %>/<%= String.IsNullOrEmpty(_model.GoalStrength) ? "--" : _model.GoalStrength %></td>
            <td><%= _model.Remark %></td>
        </tr>
<%  }
    else
    { %>
        <tr class="line_btm">
            <td>
                <a onclick="editBreakInterval(<%= _model.ExecutionID %>,<%= _model.ItemID %>);">
                    <i class="fa fa-pencil-square-o fa-2x text-warning btn btn-xs bg-color-orange"></i>
                </a>
                <a onclick="deleteItem(<%= _model.ExecutionID %>,<%= _model.ItemID %>);">
                    <i class="fa fa-trash-o fa-2x btn btn-xs bg-color-redLight"></i>
                </a>
                <a onclick="moveItem('up');">
                    <i class="fa fa-arrow-circle-o-up fa-2x btn btn-xs bg-color-blueLight"></i>
                </a>
                <a onclick="moveItem('down');">
                    <i class="fa fa-arrow-circle-o-down fa-2x btn btn-xs bg-color-blueLight"></i>
                </a>
                <input type="hidden" name="ItemID" value="<%= _model.ItemID %>" />
            </td>
            <td><%= String.IsNullOrEmpty(_model.BreakIntervalInSecond) ? null : "休息時間 " + _model.BreakIntervalInSecond + "秒" %></td>
            <td>以上項目重複<%= _model.Repeats %>組</td>
            <td></td>
            <td><%= _model.Remark %></td>
        </tr>
<%  } %>
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

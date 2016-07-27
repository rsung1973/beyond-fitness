<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<tr class="info">
    <th><%=  _model.QuestionNo - (int?)ViewBag.Offset %>.<%= _model.Question %></th>
</tr>
<tr>
    <td>
        <div class="form-group has-feedback">
            <select class="form-control" name='<%= "_" + _model.QuestionID %>'>
                <option value="1">是</option>
                <option value="0">否</option>
            </select>
            <%  if (_task != null && _task.YesOrNo.HasValue)
                { %>
            <script>
                $('select[name="<%= "_" + _model.QuestionID %>"]').val(<%= _task.YesOrNo==true ? 1 : 0%>);
            </script>
            <%  } %>
        </div>
    </td>
</tr>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    PDQQuestion _model;
    PDQTask _task;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (PDQQuestion)this.Model;
        _task = (PDQTask)ViewBag.PDQTask;
    }

</script>

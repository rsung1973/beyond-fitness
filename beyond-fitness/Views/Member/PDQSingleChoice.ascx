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
    <th><%= _model.QuestionID - (int?)ViewBag.Offset %>.<%= _model.Question %></th>
</tr>
<tr>
    <td>
        <div class="form-group has-feedback">
            <%  if (_model.PDQSuggestion.Count > 5)
                {   %>
                    <select class="form-control" name='<%= "_" + _model.QuestionID %>'>
                        <%  foreach (var item in _model.PDQSuggestion)
                            { %>
                                <option value="<%= item.SuggestionID %>"><%= item.Suggestion %></option>
                        <%  } %>
                    </select>
            <%      if (_item != null && _item.SuggestionID.HasValue)
                    {   %>
                        <script>
                            $('select[name="<%= "_" + _model.QuestionID %>"]').val(<%= _item.SuggestionID %>);
                        </script>
            <%      }
                }
                else
                {
                    foreach (var item in _model.PDQSuggestion)
                    { %>
                    <div class="radio">
                        <label>
                            <input type="radio" name='<%= "_" + _model.QuestionID %>' value="<%= item.SuggestionID %>"><%= item.Suggestion %></label>
                    </div>
                <%  }   
                    if(_item!=null && _item.SuggestionID.HasValue)
                    {   %>
                        <script>
                            $('input[name="<%= "_" + _model.QuestionID %>"][value="<%= _item.SuggestionID %>"]').prop('checked',true);
                        </script>
            <%      }
                }
                if (_model.QuestionType == (int)Naming.QuestionType.單選其他)
                {%>
                    <input type="text" name='<%= "_" + _model.QuestionID %>' class="form-control" value="<%= _item!=null ? _item.PDQAnswer : null %>" />
            <%  } %>
        </div>
    </td>
</tr>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    PDQQuestion _model;
    PDQTask _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (PDQQuestion)this.Model;
        _item = (PDQTask)ViewBag.PDQTask;

    }

</script>

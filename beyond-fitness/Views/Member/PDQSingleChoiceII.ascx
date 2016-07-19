<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<tr class="warning">
    <td>
        <%  if (ViewBag.AdditionalTitle == null)
            { %>
                <span class="fa fa-commenting"><%= _model.Question %></span>
        <%  }
            else
            { %>
                <span class="fa fa-commenting"><%= ViewBag.AdditionalTitle %></span>
                <p><%= _model.Question %></p>
        <%  } %>
        <div class="form-group has-feedback">
            <%  foreach (var item in _model.PDQSuggestion)
                { %>
            <div class="radio">
                <label>
                    <input type="radio" name='<%= "_" + _model.QuestionID %>' value="<%= item.SuggestionID %>"><%= item.Suggestion %></label>
            </div>
            <%  } 
                if(_item!=null && _item.SuggestionID.HasValue)
                {   %>
                    <script>
                        $('input[name="<%= "_" + _model.QuestionID %>"][value="<%= _item.SuggestionID %>"]').prop('checked', true);
                    </script>
        <%      }   %>
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

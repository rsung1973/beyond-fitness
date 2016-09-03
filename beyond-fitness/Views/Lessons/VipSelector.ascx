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

    <% if (_items != null && _items.Count() > 0)
        {
            foreach (var item in _items)
            { %>
                <label class="radio">
                <input type="radio" name="UID" value="<%= item.UID %>" />
                <i></i><div><%= item.RealName %>「自主訓練」
                    </div>
                </label>

    <%      }
        }
        else
        { %>
            查無相符條件的上課資料!!
    <%  } %>


<script runat="server">

    ModelStateDictionary _modelState;
    IEnumerable<UserProfile> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _items = (IEnumerable<UserProfile>)this.Model;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

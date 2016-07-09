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

<select name="<%= _model.Name ?? "coach" %>" id="<%= _model.Id ?? "coach" %>" class='<%= ViewBag.Inline==true ? "" : "form-control" %>'>
    <%  if (ViewBag.SelectAll == true)
        { %>
            <option value="">全部</option>
    <%  } %>
    <% foreach (var item in _items)
        { %>
    <option value="<%= item.CoachID %>" <%= item.CoachID == (int?)_model.DefaultValue ? "selected" : null %> ><%= item.UserProfile.RealName %></option>
    <%  } %>
</select>
<script runat="server">

    InputViewModel _model;
    IEnumerable<ServingCoach> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _model = (InputViewModel)this.Model;
        var models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = models.GetTable<ServingCoach>();
    }
</script>

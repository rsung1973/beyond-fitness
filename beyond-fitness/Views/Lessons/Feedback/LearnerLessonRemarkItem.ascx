<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Timeline" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  foreach (var item in _model.LessonFeedBack.Where(f => f.Remark != null))
    { %>
<li class="message message-reply">
    <% item.RegisterLesson.UserProfile.RenderUserPicture(Writer, new { @class = "authorImg online" }); %>
    <span class="message-text">
        <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + item.RegisterLesson.UID %>"><%= item.RegisterLesson.UserProfile.UserName ?? item.RegisterLesson.UserProfile.RealName %></a>
        <%= item.Remark %>
    </span>
    <ul class="list-inline font-xs">
        <li>
            <a href="javascript:void(0);" class="text-muted"><%= String.Format("{0:yyyy/MM/dd HH:mm}",item.RemarkDate) %></a>
        </li>
    </ul>
</li>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_userProfile.IsAssistant())
    { %>
<%--<li>
    <a href="<%= VirtualPathUtility.ToAbsolute("~/SystemInfo/ConfigureAll") %>" title="基本資料"><i class="fa fa-lg fa-fw fa-database"></i><span class="menu-item-parent">基本資料</span></a>
</li>--%>
<%  } %>

<li>
    <a href=""><i class="fa fa-lg fa-fw fa-tasks"></i>體能顧問服務費</a>
    <ul>
        <li>
            <a href="../../../front-end/pricing-arena.html" target="_blank"><i class="fa fa-fw fa-th-list"></i>南京小巨蛋</a>
        </li>
        <li>
            <a href="../../../front-end/pricing-101.html" target="_blank"><i class="fa fa-fw fa-th-list"></i>Enhanced 101</a>
        </li>
        <li>
            <a href="../../../front-end/pricing-sogo.html" target="_blank"><i class="fa fa-fw fa-th-list"></i>忠孝</a>
        </li>
    </ul>
</li>

<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _userProfile = Context.GetUser();
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_userProfile != null
        && (_userProfile.IsCoach() || _userProfile.IsAssistant()))
    { %>
<li>
    <a href="#" title="收款管理"><i class="fa fa-lg fa-fw fa-usd"></i><span class="menu-item-parent">收款管理</span></a>
    <ul>
        <li>
            <a href="<%= Url.Action("PaymentIndex","Payment") %>" title="收款新增/作廢"><i class="fa fa-lg fa-fw fa-usd"></i>收款新增/作廢</a>
        </li>
        <%  if(_userProfile.IsAssistant() || _userProfile.IsManager())
            { %>
        <li>
            <a href="<%= Url.Action("AchievementIndex","Payment") %>" title="業績分潤設定"><i class="fa fa-lg fa-fw fa-trophy"></i>業績分潤設定</a>
        </li>
        <%  } %>
        <!--
                        <li>
                            <a href="paychecklist.html" title="轉帳收款勾記"><i class="fa fa-lg fa-fw fa-check-square-o"></i> 轉帳收款勾記</a>
                        </li>
-->
        <li>
            <a href="<%= Url.Action("QueryIndex","Payment") %>" title="收款/作廢紀錄查詢"><i class="fa fa-lg fa-fw fa-search"></i>收款/作廢紀錄查詢</a>
        </li>
    </ul>
</li>
<%  } %>

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

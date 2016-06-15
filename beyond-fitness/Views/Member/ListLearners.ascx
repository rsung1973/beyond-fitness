<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

<% if (_items != null && _items.Count() > 0)
    { %>
<table class="table">
    <tr class="info">
        <th>姓名</th>
        <th>學員編號</th>
        <th>功能</th>
    </tr>
    <% foreach (var item in _items)
        { %>
    <tr>
        <td><%= item.RealName %></td>
        <td><%= item.MemberCode %></td>
        <td>
            <a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Member/EditLearner/") + item.UID %>">修改資料 <i class="fa fa-edit" aria-hidden="true"></i></a>
            <a href="view-vip.htm" class="btn btn-system btn-small">新增上課數 <i class="fa fa-plus-square" aria-hidden="true"></i></a>
            <a  class="btn btn-system btn-small" data-toggle="modal" data-target="#confirm" data-whatever="刪除" data-key="<%= item.UID %>" data-action="學員資料">刪除 <i class="fa fa-times" aria-hidden="true"></i></a>
            <a href="view-vip.htm" class="btn btn-system btn-small">檢視 <i class="fa fa-eye" aria-hidden="true"></i></a>
        </td>
    </tr>
    <%     } %>
</table>
<%  }
    else { %>
<h4>未建立資料</h4>
<% } %>


<script runat="server">

    ModelSource<UserProfile> models;
    IEnumerable<UserProfile> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = TempData.GetModelSource<UserProfile>();
        _items = (IEnumerable<UserProfile>)this.Model;
    }

    public override void Dispose()
    {
        if (models != null)
            models.Dispose();

        base.Dispose();
    }


</script>

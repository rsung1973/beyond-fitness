<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>

<% if (RecordCount > 0 && item != null)
    { %>
<div id="pagination">
    <span class="all-pages">Page <%= item.CurrentIndex+1 %> of <%= pagingCount %></span>
    <% if (startPaging > 0)
        { %>
    <%: Html.ActionLink("First", ActionName,ControllerName,new PagingIndexViewModel { CurrentIndex=0, PageSize = item.PageSize,PagingSize = item.PagingSize },new { @class="next-page" }) %>
    <% } %>
    <% if (prevPaging)
        { %>
    <%: Html.ActionLink("Prev", ActionName,ControllerName,new PagingIndexViewModel { CurrentIndex=startPaging-item.PagingSize, PageSize = item.PageSize,PagingSize = item.PagingSize },new { @class="next-page" }) %>
    <% } %>
    <% for (int i = startPaging; i < Math.Min(pagingCount, startPaging + item.PagingSize); i++)
        {
            if (i == item.CurrentIndex)
            { %>
    <span class="current page-num"><%= i+1 %></span>
    <%      }
        else { %>
    <%: Html.ActionLink((i+1).ToString(), ActionName,ControllerName,new PagingIndexViewModel { CurrentIndex=i, PageSize = item.PageSize,PagingSize = item.PagingSize },new { @class="page-num" }) %>
    <%      }%>
    <%       } %>
    <% if (nextPaging)
        { %>
    <%: Html.ActionLink("Next", ActionName,ControllerName,new PagingIndexViewModel { CurrentIndex=startPaging+item.PagingSize, PageSize = item.PageSize,PagingSize = item.PagingSize },new { @class="next-page" }) %>
    <% } %>
    <% if (startPaging + item.PagingSize < pagingCount)
        { %>
    <%: Html.ActionLink("Last", ActionName,ControllerName,new PagingIndexViewModel { CurrentIndex=pagingCount-1, PageSize = item.PageSize,PagingSize = item.PagingSize },new { @class="next-page" }) %>
    <% } %>
</div>
<% } %>
<script runat="server">

    PagingIndexViewModel item;    int startPaging, pagingCount;
    bool nextPaging, prevPaging;
    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    item = (PagingIndexViewModel)this.Model;
    //}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (item != null && item.PageSize > 0 && item.PagingSize > 0 && item.CurrentIndex >= 0)
        {
            pagingCount = (RecordCount + item.PageSize - 1) / item.PageSize;
            startPaging = item.CurrentIndex / item.PagingSize * item.PagingSize;
            prevPaging = pagingCount > 0 && startPaging >= item.PagingSize;
            nextPaging = pagingCount > 0 && (startPaging + item.PagingSize) < pagingCount;
        }
        else
        {
            item = null;
        }
    }

    [System.ComponentModel.Bindable(true)]    public PagingIndexViewModel Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
        }
    }    [System.ComponentModel.Bindable(true)]    public int RecordCount
    { get; set; }    [System.ComponentModel.Bindable(true)]    public String ActionName
    { get; set; }

    [System.ComponentModel.Bindable(true)]    public String ControllerName
    { get; set; }
</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (ViewBag.Message != null)
    { %>
<%--<label class="error"><%= ViewBag.Message %></label>--%>
<script>
    $(function () {
        alert('<%= ViewBag.Message %>');
    });
</script>
<%  } %>

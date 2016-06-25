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

<div class="blog-post quote-post">
    <!-- Post Content -->

    <div class="user-info clearfix">
        <div class="author-image">
            <div class="user-image">
                <% _model.RenderUserPicture(this.Writer, "userImg"); %>
            </div>
            <div class="user-bio">
                <h2 class="text-primary"><%= _model.RealName %> </h2>

                <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                <p><strong>會員編號：</strong><%= _model.MemberCode %></p>
                <p><strong>電話：</strong><%= _model.Phone %></p>
                <p><strong>Email：</strong><%= _model.PID %></p>

            </div>
        </div>
    </div>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
    }

</script>

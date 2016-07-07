<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="blog-post">
    <!-- Classic Content -->
    <div class="user-info clearfix">
        <div class="user-image">
                <% _model.RenderUserPicture(this.Writer, "userImg"); %>
        </div>
        <div class="user-bio" style="padding-top: 20px;">
            <h4 class="classic-title"><span>
                <%  if (ViewBag.UserInfo == null)
                    { %> 
                        Hi <%= String.IsNullOrEmpty(_model.UserName) ? _model.RealName :_model.UserName %>
                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/EditMySelf") %>" class="pull-right" style="font-size: 13px; margin-bottom: 5px;"><i class="fa fa-cog" aria-hidden="true"></i>修改個人資料</a>
                <%  }
                    else
                    { %>
                        <%= ViewBag.UserInfo %>
                <%  } %>
                </span></h4>
            <%  if (ViewBag.Argument is ArgumentModel)
                {
                    Html.RenderPartial(((ArgumentModel)ViewBag.Argument).PartialViewName, ((ArgumentModel)ViewBag.Argument).Model);
                } %>
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

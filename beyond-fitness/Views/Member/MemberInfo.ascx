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

                <%  if (ViewBag.Argument == null)
                    { %>
                        <p><strong>會員編號：</strong><%= _model.MemberCode %></p>
                        <p><strong>電話：</strong><%= _model.Phone %></p>
                        <p><strong>Email：</strong><%= _model.PID.Contains("@") ? _model.PID : null %></p>
                <%  } %>

                <%  if (ViewBag.ShowPerson == true)
                    {   %>
                        <p class="fa fa-tags"><strong>方案設計工具結果</strong></p>
                        <table class="table">
                            <tr class="info">
                                <th>目標</th>
                                <th>風格</th>
                                <th>訓練水準</th>
                            </tr>
                            <tr>
                                <td>健身</td>
                                <td>保守型</td>
                                <td>初期</td>
                            </tr>
                        </table>
                <%  }
                    else if (ViewBag.EditMySelf == true)
                    { %>
                        <div class="hr1" style="margin-top: 10px; margin-bottom: 10px;"></div>

                        <p>您的修改已經完成。</p>
                        <!-- Divider -->
                        <div class="hr1" style="margin-bottom: 10px;"></div>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute(_model.CurrentUserRole.RoleID == (int)Naming.RoleID.Learner ? "~/Account/Vip" : "~/Account/Coach") %>" class="btn-system btn-small">進入會員專區 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                <%  }
                    else if (ViewBag.Argument is ArgumentModel)
                    {
                        Html.RenderPartial(((ArgumentModel)ViewBag.Argument).PartialViewName, ((ArgumentModel)ViewBag.Argument).Model);
                    } %>
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

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="row">

    <%  Html.RenderPartial("~/Views/Member/LessonCount.ascx", _model.UserProfile); %>
    <div class="col-xs-8 col-sm-6">
        <h1>
            <span class="semi-bold"><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.UID %>"><%= _model.UserProfile.RealName %></a></span>
        </h1>
        <p class="font-md">關於<%= _model.UserProfile.UserName ?? _model.UserProfile.RealName %>...</p>
        <p>
            <form action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitPlan") %>" class="smart-form" method="post">
                <fieldset>
                    <section>
                        <label class="textarea">
                            <textarea rows="3" id="recentStatus" name="recentStatus" class="custom-scroll"><%= _model.UserProfile.RecentStatus %></textarea>
                        </label>
                        <div class="note">
                            <strong>Note:</strong> 最多輸入250個中英文字
                        </div>
                    </section>
                </fieldset>
                <p class="text-right">
                    <button type="button" name="submit" class="btn btn-primary btn-sm" id="btnUpdateStatus" onclick="commitPlan();">
                        <i class="fa fa-reply"></i>更新
                    </button>
                </p>
            </form>
        </p>
    </div>
    <div class="col-xs-12 col-sm-3">
        <%  Html.RenderPartial("~/Views/Member/ContactInfo.ascx", _model.UserProfile); %>
        <%  Html.RenderPartial("~/Views/Member/UserAssessmentInfo.ascx", _model.UserProfile); %>
    </div>
</div>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    RegisterLesson _model;
    LessonTime _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterLesson)this.Model;
        _item = (LessonTime)ViewBag.LessonTime;

    }

</script>

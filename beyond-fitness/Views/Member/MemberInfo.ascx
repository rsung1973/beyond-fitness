﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="col-xs-8 col-sm-6">
    <h1>
        <span class="semi-bold"><%= _model.RealName %></span>
    </h1>
    <p class="font-md">關於<%= _model.UserName ?? _model.RealName %>...</p>
    <p>
        <%= _model.RecentStatus!=null ? _model.RecentStatus.Replace("\r\n","<br/>") : null %>
    </p>
</div>
<div class="col-xs-12 col-sm-3">
    <h1><small>聯絡方式</small></h1>
    <ul class="list-unstyled">
        <li>
            <p class="text-muted">
                <i class="fa fa-phone"></i>
                (886) <%= _model.Phone %> </li>
        </p>
        <li>
            <p class="text-muted">
                <i class="fa fa-envelope"></i><a href="mailto:<%= _model.PID %>"><%= _model.PID %></a>
            </p>
        </li>
        <li>
            <p class="text-muted">
                <a href="javascript:void(0);" class="btn bg-color-blueLight btn-xs"><i class="fa fa-envelope-o"></i>Send Message</a>
            </p>
        </li>
    </ul>
    <h1><small>方案設計工具結果</small></h1>
    <ul class="list-unstyled">
        <li>
            <p class="text-muted">
                目標 - <%= _model.PDQUserAssessment!=null && _model.PDQUserAssessment.GoalAboutPDQ!=null ? _model.PDQUserAssessment.GoalAboutPDQ.Goal : null %>
            </p>
        </li>
        <li>
            <p class="text-muted">
                風格 - <%= _model.PDQUserAssessment!=null && _model.PDQUserAssessment.StyleAboutPDQ!=null ? _model.PDQUserAssessment.StyleAboutPDQ.Style : null %>
            </p>
        </li>
        <li>
            <p class="text-muted">
                訓練水準 - <%= _model.PDQUserAssessment!=null && _model.PDQUserAssessment.TrainingLevelAboutPDQ!=null ? _model.PDQUserAssessment.TrainingLevelAboutPDQ.TrainingLevel : null %>
            </p>
        </li>
    </ul>
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

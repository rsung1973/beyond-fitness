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

<div class="col-xs-6 col-sm-5">
    <h1>
        <span class="semi-bold"><%= _model.RealName %></span>
    </h1>
    <p class="font-md">關於<%= _model.UserName ?? _model.RealName %>...</p>
    <p>
        <%= _model.ServingCoach.Description.HtmlBreakLine() %>
    </p>
</div>
<%  if (_userProfile != null)
    { %>
<div class="col-xs-12 col-sm-4">
    <h1><small>聯絡方式</small></h1>
    <ul class="list-unstyled">
        <li>
            <p class="text-muted">
                <i class="fa fa-phone"></i>
                (886) <%= _model.Phone %>
            </p>
        </li>
        <li>
            <p class="text-muted">
                <i class="fa fa-envelope"></i><a href="mailto:<%= _model.PID.Contains("@") ? _model.PID : null %>"><%= _model.PID.Contains("@") ? _model.PID : null %></a>
            </p>
        </li>
        <li>
            <p class="text-muted">
                <a href="http://line.naver.jp/R/msg/text/?LINE%E3%81%A7%E9%80%81%E3%82%8B%0D%0Ahttp%3A%2F%2Fline.naver.jp%2F">
                    <img alt="用LINE傳送" height="20" src="<%= VirtualPathUtility.ToAbsolute("~/img/line/linebutton_84x20_zh-hant.png") %>" width="84" />
                </a>
            </p>
        </li>
    </ul>
</div>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _model;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _userProfile = Context.GetUser();
    }

</script>

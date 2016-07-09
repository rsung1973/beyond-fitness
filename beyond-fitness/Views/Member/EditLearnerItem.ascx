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

<div class="form-group has-feedback">
    <% Html.RenderInput("學員姓名：", "realName", "realName", "請輸入姓名", _modelState, defaultValue: _model.RealName); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("EMail：", "email", "email", "請輸入EMail", _modelState, defaultValue: _model.Email); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("電話：", "phone", "phone", "請輸入市話或手機號碼", _modelState, defaultValue: _model.Phone); %>
</div>

<div class="form-group has-feedback">
    <label class="control-label" for="classno">生日：</label>
    <div class="input-group date form_date" data-date="" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
        <input id="birthDay" name="birthDay" class="form-control" size="16" type="text" value='<%= _model.Birthday.HasValue ? _model.Birthday.Value.ToString("yyyy/MM/dd") : "" %>' readonly>
        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
    </div>
</div>

<%--<% Html.RenderPartial("~/Views/Member/LessonsItem.ascx", _model);  %>--%>

<script>

    $(function () {
        $('#email').rules('add', {
            'required': false,
            'email': true
        });

        $('#realName').rules('add', {
            'required': true,
            'maxlength': 20
        });

        $('#phone').rules('add', {
            'required': true,
            'regex': /^[0-9]{6,20}$/
        });
    });

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    LearnerViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = this.Model as LearnerViewModel;

    }
</script>

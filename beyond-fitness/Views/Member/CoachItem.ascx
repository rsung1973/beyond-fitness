<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="form-group has-feedback">
    <% Html.RenderInput("教練姓名：", "realName", "realName", "請輸入姓名", _modelState, defaultValue: _model.RealName); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("EMail：", "email", "email", "請輸入EMail", _modelState, defaultValue: _model.Email); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("電話：", "phone", "phone", "請輸入市話或手機號碼", _modelState, defaultValue: _model.Phone); %>
</div>

<div class="form-group has-feedback">
    <label class="control-label" for="nickname">是否為自由教練：</label>
    <select name="coachRole" class="form-control" id="coachRole">
        <option value="3">是</option>
        <option value="2">否</option>
    </select>
</div>

<script>

    $(function () {
        $('#realName').val('<%= _model.RealName %>');
        $('#phone').val('<%= _model.Phone %>');
        $('#coachRole').val('<%= _model.CoachRole %>');

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
    CoachViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = this.Model as CoachViewModel;
    }
</script>

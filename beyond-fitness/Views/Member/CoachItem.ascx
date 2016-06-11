<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

<div class="form-group has-feedback">
    <% Html.RenderInput( "教練姓名：","realName","realName","請輸入姓名",_modelState); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("電話：", "phone", "phone", "請輸入市話或手機號碼", _modelState); %>
</div>

<div class="form-group has-feedback">
    <label class="control-label" for="nickname">是否為自由教練：</label>
    <select name="coachRole" class="form-control">
        <option value="3">是</option>
        <option value="2">否</option>
    </select>
</div>
<% if (_model.UID.HasValue)
    { %>
<input type="hidden" name="uid" value="<%= _model.UID %>" />
<%  } %>
<% if (_model != null)
    { %>
<script>
    $(function () {
        $('#realName').val('<%= _model.RealName %>');
        $('#phone').val('<%= _model.Phone %>');
        $('#coachRole').val('<%= _model.CoachRole %>');
    });
</script>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    CoachViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = this.Model as CoachViewModel;
        if (_model == null)
        {
            UserProfile item = (UserProfile)this.Model;
            if(item!=null)
            {
                _model = new CoachViewModel
                {
                    CoachRole = item.UserRole[0].RoleID,
                    Phone = item.Phone,
                    RealName = item.RealName,
                    UID = item.UID
                };
            }
        }
    }
</script>

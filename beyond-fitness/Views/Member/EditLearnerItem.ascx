<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

<div class="form-group has-feedback">
    <% Html.RenderInput( "學員姓名：","realName","realName","請輸入姓名",_modelState); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("EMail：", "email", "email", "請輸入EMail", _modelState); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("電話：", "phone", "phone", "請輸入市話或手機號碼", _modelState); %>
</div>


<% if (_model!=null && _model.UID.HasValue)
    { %>
<input type="hidden" name="uid" value="<%= _model.UID %>" />
<%  } %>
<% if (_model != null)
    { %>
<script>
    $(function () {
        $('#realName').val('<%= _model.RealName %>');
        $('#phone').val('<%= _model.Phone %>');
        $('#email').val('<%= _model.Email %>');
    });
</script>
<%  } %>

<script>
    $("form").validate({
        //debug: true,
        //errorClass: "label label-danger",
        success: function (label, element) {
            label.remove();
            var id = $(element).prop("id");
            $('#' + id + 'Icon').removeClass('glyphicon-remove').removeClass('text-danger')
                .addClass('glyphicon-ok').addClass('text-success');
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
            var id = $(element).prop("id");
            $('#' + id + 'Icon').addClass('glyphicon-remove').addClass('text-danger')
                .removeClass('glyphicon-ok').removeClass('text-success');
        },
        rules: {
            realName: {
                required: true,
                maxlength: 20
            },
            phone: {
                required: true,
                regex: /^[0-9]{6,20}$/
            },
            // compound rule
            email: {
                required: false,
                email: true
            }
        }
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
        if (_model == null)
        {
            UserProfile item = (UserProfile)this.Model;
            if(item!=null)
            {
                _model = new LearnerViewModel
                {
                    Phone = item.Phone,
                    RealName = item.RealName,
                    Email = item.EMail,
                    UID = item.UID
                };
            }
        }
    }
</script>

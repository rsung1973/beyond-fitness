<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Src="~/Views/Shared/LockScreen.ascx" TagPrefix="uc1" TagName="LockScreen" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="tabs-section">
    <!-- Nav Tabs -->
    <ul class="nav nav-tabs">
        <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-share-alt"></i>設定圖形密碼</a></li>
        <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>設定文字密碼</a></li>
    </ul>

    <!-- Tab panels -->
    <div class="tab-content">
        <!-- Tab Content 1 -->
        <div class="tab-pane fade in active" id="tab-1">
            <uc1:LockScreen runat="server" ID="lockScreen" />
        </div>
        <input type="text" name="lockPattern" id="lockPattern" style="display: none" />
        <label id="lockPattern-error" class="error" for="lockPattern" style="display: none;"></label>
        <div class="tab-pane fade" id="tab-2">
            <div class="form-group has-feedback">
                <% Html.RenderPassword("密碼：", "password", "password", "密碼", _modelState); %>
            </div>
            <div class="form-group has-feedback">
                <% Html.RenderPassword("請再輸入一次密碼：", "password2", "password2", "再輸入一次密碼", _modelState); %>
            </div>
        </div>
    </div>
    <!-- Tab Content 2 -->
</div>

<script>

    $(function () {

        $formValidator.settings.submitHandler = function (form) {

            if ($('#tab-1').css('display') == 'block') {
                var userPath = $appLock.getUserPath();
                if (userPath == null) {
                    $('#lockPattern-error').css('display', 'block');
                    $('#lockPattern-error').text('請您設定圖形密碼!!');
                    return false;
                } else if (userPath.length < 9) {
                    $('#lockPattern-error').css('display', 'block');
                    $('#lockPattern-error').text('您設定圖形的密碼過短!!');
                    return false;
                } else {
                    $('#lockPattern').val(userPath);
                }
            }

            //$(form).submit();
            return true;
        };

        $.validator.addMethod("confirmPassword", function (value, element, pwd) {
            return $(element).val() == pwd.val();
        }, "密碼確認錯誤!!");

        $('#password').rules('add', {
            'required': {
                param: true,
                depends: function (elment) {
                    return $('input[name="lockPattern"]').val() == '';
                }
            },
            'maxlength': 20
        });

        $('#password2').rules('add', {
            'required': false,
            'confirmPassword': $('#password')
        });

    });
</script>
<script runat="server">

    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
    }


</script>

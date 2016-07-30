<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%  String basePath = VirtualPathUtility.ToAbsolute("~/"); %>
<!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
<script data-pace-options='{ "restartOnRequestAfter": true }' src="<%= basePath + "js/plugin/pace/pace.min.js" %>"></script>

<!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
<script src="<%= basePath + "Scripts/jquery-2.1.4.min.js" %>"></script>
<script src="<%= basePath + "Scripts/jquery-ui-1.10.3.min.js" %>"></script>
<!-- JQUERY VALIDATE -->
<script src="<%= basePath + "js/plugin/jquery-validate/jquery.validate.min.js" %>"></script>
<script src="<%= basePath + "js/jquery.form.js" %>"></script>
<script>
    var $formValidator;
    var $pageFormValidator;

    $(function () {

        $formValidator = $("#theForm").validate({
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
            }
        });

        $pageFormValidator = $("#pageForm").validate({

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        $.validator.addMethod("regex", function (value, element, regexpr) {
            return regexpr.test(value);
        }, "資料格式錯誤!!");
    });

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
</script>

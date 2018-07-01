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
<script src="<%= basePath + "js/plugin/jquery-blockui/jquery.blockUI.js" %>"></script>
<script src="<%= basePath + "js/jquery.form.js" %>"></script>

<script>
   
    function clearErrors() {
        $('span.help-error-text').remove();
    }

    function showLoading(autoHide,onBlock) {
        $.blockUI({
            message:  '<img src="<%= VirtualPathUtility.ToAbsolute("~/img/loading.gif") %>" /><h1>Loading</h1>', 
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            },
            // 背景圖層
            overlayCSS:  { 
                backgroundColor: '#3276B1', 
                opacity:         0.6, 
                cursor:          'wait' 
            },
            onBlock: onBlock
        });

        if(autoHide)
            setTimeout($.unblockUI, 2000);
    }

    function hideLoading() {
        $.unblockUI();
    }

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

    $.fn.launchDownload = function (url, params,target) {

        var data = this.serializeObject();
        if (params) {
            $.extend(data, params);
        }

        var form = $('<form></form>').attr('action', url).attr('method', 'post');//.attr('target', '_blank');
        if (target) {
            form.attr('target', target);
        }

        Object.keys(data).forEach(function (key) {
            var value = data[key];

            if (value instanceof Array) {
                value.forEach(function (v) {
                    form.append($("<input></input>").attr('type', 'hidden').attr('name', key).attr('value', v));
                });
            } else {
                form.append($("<input></input>").attr('type', 'hidden').attr('name', key).attr('value', value));
            }

        });

        //send request
        form.appendTo('body').submit().remove();
    };


</script>

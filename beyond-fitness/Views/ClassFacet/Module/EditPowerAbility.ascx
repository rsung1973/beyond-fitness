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

<div id="<%= _dialog %>" title="編輯人格與運動能力" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white">
        <div class="row">
            <div class="col col-12">
                <div class="user">
                    <a onclick="commitPowerAbility('多變型（初階）');" class="btn btn-circle btn-warning">變</a>
                    <email>多變型（初階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('多變型（中階）');" class="btn btn-circle btn-success">變</a>
                    <email>多變型（中階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('多變型（高階）');" class="btn btn-circle btn-danger">變</a>
                    <email>多變型（高階）</email>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-12">
                <div class="user">
                    <a onclick="commitPowerAbility('保守型（初階）');" class="btn btn-circle btn-warning">守</a>
                    <email>保守型（初階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('保守型（中階）');" class="btn btn-circle btn-success">守</a>
                    <email>保守型（中階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('保守型（高階）');" class="btn btn-circle btn-danger">守</a>
                    <email>保守型（高階）</email>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-12">
                <div class="user">
                    <a onclick="commitPowerAbility('混合型（初階）');" class="btn btn-circle btn-warning">混</a>
                    <email>混合型（初階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('混合型（中階）');" class="btn btn-circle btn-success">混</a>
                    <email>混合型（中階）</email>
                </div>
                <div class="user">
                    <a onclick="commitPowerAbility('混合型（高階）');" class="btn btn-circle btn-danger">混</a>
                    <email>混合型（高階）</email>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen : false,
            resizable: true,
            modal: true,
            width: 'auto',
            title: "<h4 class='modal-title'><i class='fa-fw fa fa-cog'></i>  設定人格與運動能力</h4>",
            close: function (event, ui) {
                $('#<%= _dialog %>').remove();
            },
        });

        function commitPowerAbility(ability) {
            showLoading();
            $.post('<%= Url.Action("CommitPowerAbility", "ClassFacet", new { _model.UID }) %>', { 'ability': ability }, function (data) {
                hideLoading();
                if ($.isPlainObject(data)) {
                    if (data.result) {
                        showAbility(ability);
                        $('#<%= _dialog %>').dialog('close');
                    } else {
                        alert(data.message);
                    }
                } else {
                    $(data).appendTo($('body'));
                    $('#<%= _dialog %>').dialog('close');
                }
            });
        }
    </script>
</div>



<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    String _dialog = "powerAbility" + DateTime.Now.Ticks;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
    }

</script>

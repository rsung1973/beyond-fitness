<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="<%= _dialog %>" title="編輯<%= _viewModel.CoachRole==(int)Naming.RoleID.Coach ? "員工" : "員工" %>資料" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white no-padding">
        <form action="<%= Url.Action("CommitMember","Member",new { uid = _viewModel.UID }) %>" class="smart-form" method="post" autofocus>
            <fieldset id="branchStore" style="<%= _viewModel.CoachRole==(int)Naming.RoleID.Coach ? "display:block": "display:none" %>">
                <section>
                    <label class="label">隸屬分店</label>
                    <label class="select">
                        <select name="BranchID">
                            <%  Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.ascx", model: _viewModel.BranchID); %>
                        </select>
                        <i class="icon-append fa fa-file-word-o"></i>
                    </label>
                </section>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">姓名</label>
                        <label class="input">
                            <i class="icon-append fa fa-user"></i>
                            <input type="text" name="RealName" maxlength="20" placeholder="請輸入員工姓名" value="<%= _viewModel.RealName %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">行動電話</label>
                        <label class="input">
                            <i class="icon-append fa fa-phone"></i>
                            <input type="tel" name="Phone" maxlength="20" placeholder="請輸入行動電話" data-mask="0999999999" value="<%= _viewModel.Phone %>">
                        </label>
                    </section>
                </div>
            </fieldset>
            <%  if (_viewModel.UID.HasValue)
                { %>
            <fieldset>
                <section>
                    <label class="label">Email</label>
                    <label class="input">
                        <i class="icon-append fa fa-envelope"></i>
                        <input type="text" name="Email" maxlength="20" placeholder="請輸入Email" value="<%= _viewModel.Email %>" />
                    </label>
                </section>
            </fieldset>
            <%  } %>
            <fieldset>
                <section>
                    <label class="label">適用角色</label>
                    <label class="select">
                        <select name="CoachRole">
<%--                            <%  if (_viewModel.CoachRole == (int)Naming.RoleID.Coach)
                                { %>--%>
                            <option value="2" <%= _viewModel.CoachRole == (int)Naming.RoleID.Coach ? "selected" : null %>>體能顧問</option>
<%--                            <%  }
                                else
                                { %>--%>
                            <option value="8" <%= _viewModel.CoachRole == (int)Naming.RoleID.Assistant ? "selected" : null %>>行政助理</option>
                            <option value="6" <%= _viewModel.CoachRole == (int)Naming.RoleID.Accounting ? "selected" : null %>>財務助理</option>
<%--                            <%  } %>--%>
                        </select>
                        <i class="icon-append fa fa-file-word-o"></i>
                        <script>
                            $('select[name="CoachRole"]').on('change', function (evt) {
                                if ($('select[name="CoachRole"]').val() == '2') {
                                    $('#coachLevel').css('display', 'block');
                                    $('#branchStore').css('display', 'block');
                                } else {
                                    $('#coachLevel').css('display', 'none');
                                    $('#branchStore').css('display', 'none');
                                }
                            });
                        </script>
                    </label>
                </section>
            </fieldset>
            <fieldset id="coachLevel" style="<%= _viewModel.CoachRole==(int)Naming.RoleID.Coach ? "display:block": "display:none" %>">
                <div class="row">
                    <section class="col col-6">
                        <label class="label">Level適用制度</label>
                        <label class="select">
                            <select name="LevelCategory">
                                <option value="1102" <%= _viewModel.LevelCategory == 1102 ? "selected" : null %>>新制</option>
                                <option value="1101" <%= _viewModel.LevelCategory == 1101 ? "selected" : null %>>舊制</option>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">Level</label>
                        <label class="select">
                            <select name="LevelID">
                            </select>
                            <i class="icon-append fa fa-thermometer-full"></i>
                        </label>
                    </section>
                    <script>
                        $(function () {

                            function buildLevel(category) {
                                var $levelID = $('select[name="LevelID"]');
                                $levelID.find('option').remove();
                                if (category == '<%= (int)Naming.ProfessionalCategory.新制 %>') {
                                    <%  foreach(var item in models.GetTable<ProfessionalLevel>().Where(l=>l.CategoryID==(int)Naming.ProfessionalCategory.新制))
                                        {   %>
                                    $('<option>').attr('value', '<%= item.LevelID %>').text('<%= item.LevelName %>').appendTo($levelID);
                                    <%  } %>
                                } else {
                                    <%  foreach(var item in models.GetTable<ProfessionalLevel>().Where(l=>l.CategoryID==(int)Naming.ProfessionalCategory.舊制))
                                        {   %>
                                    $('<option>').attr('value', '<%= item.LevelID %>').text('<%= item.LevelName %>').appendTo($levelID);
                                    <%  } %>
                                }
                            }

                            $('select[name="LevelCategory"]').on('change', function (evt) {
                                buildLevel($('select[name="LevelCategory"]').val());
                            });

                            <%  if(_viewModel.LevelID.HasValue)
                                {   %>
                            buildLevel('<%= _viewModel.LevelCategory %>');
                            $('select[name="LevelID"]').val('<%= _viewModel.LevelID %>');
                            <%  }
                                else
                                {   %>
                            buildLevel('<%= (int)Naming.ProfessionalCategory.新制 %>');
                            <%  }   %>

                        });
                    </script>
                </div>
            </fieldset>
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            width: "auto",
            resizable: false,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-edit'></i> 編輯<%= _viewModel.CoachRole==(int)Naming.RoleID.Coach ? "員工" : "員工" %>資料</h4></div>",
            buttons: [{
                html: "<i class='fa fa-send'></i>&nbsp; 確定",
                "class": "btn btn-primary",
                click: function () {
                    showLoading();
                    $('#<%= _dialog %> form').ajaxSubmit({
                        success: function (data) {
                            hideLoading();
                            if (data.result) {
                                alert('資料已儲存!!');
                                $('#<%= _dialog %>').dialog('close');
                                showLoading();
                                window.location.href = '<%= Url.Action("ListCoaches","Member") %>';
                            } else {
                                $(data).appendTo($('body')).remove();
                            }
                        }
                    });
                }
            }],
            close: function (event, ui) {
                $('#<%= _dialog %>').remove();
            }
        });
    </script>
</div>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _dialog = "modifyCoachDialog" + DateTime.Now.Ticks;
    CoachViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (CoachViewModel)ViewBag.ViewModel;
    }

</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="<%= _dialog %>" title="待辦事項：新合約(<%= ((Naming.CourseContractStatus?)_viewModel.Status).ToString() %>)" class="bg-color-darken">
    <!-- content -->
    <table id="<%= _tableId %>" class="table table-striped table-bordered table-hover" width="100%">
        <thead>
            <tr>
                <th data-hide="phone">體能顧問</th>
                <th data-class="expand">學員</th>
                <th>編輯日期</th>
                <th>合約名稱</th>
            </tr>
        </thead>
        <tbody>
            <%  foreach (var item in _model)
                { %>
            <tr>
                <td><%= item.ServingCoach.UserProfile.FullName() %></td>
                <td>
                    <%  if (item.CourseContractType.IsGroup==true)
                        { %>
                <%= String.Join("/",item.CourseContractMember.Select(m=>m.UserProfile.RealName)) %>
                    <%  }
                        else
                        { %>
                <%= item.ContractOwner.FullName() %>
                    <%  } %>
                </td>
                <td><%= String.Format("{0:yyyy/MM/dd}", item.ContractDate) %></td>
                <td>
                    <%  if (_viewModel.Status == (int)Naming.CourseContractStatus.草稿)
                        { %>
                    <a onclick="$global.editContract(<%= item.ContractID %>);" class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>&nbsp;&nbsp;
                    <a onclick="$global.deleteContract(<%= item.ContractID %>);" class="btn btn-circle bg-color-red delete"><i class="fa fa-fw fa fa-lg fa-trash-o" aria-hidden="true"></i></a>
                    <%  }
                        else if(_viewModel.Status == (int)Naming.CourseContractStatus.待確認)
                        {   %>
                    <a onclick="$global.openToApproveContract(<%= item.ContractID %>);" class="btn btn-circle bg-color-red"><i class="fa fa-fw fa fa-lg fa-check-square-o" aria-hidden="true"></i></a>
                    <%  }
                        else if(_viewModel.Status == (int)Naming.CourseContractStatus.待簽名)
                        {   %>
                    <a onclick="$global.openToSignContract(<%= item.ContractID %>);" class="btn btn-circle bg-color-green"><i class="fa fa-fw fa fa-lg fa-pencil" aria-hidden="true"></i></a>
                    <%  }
                        else if(_viewModel.Status == (int)Naming.CourseContractStatus.待審核)
                        {   %>
                    <%--<a href="<%= Url.Action("GetContractPdf","CourseContract",new { item.ContractID }) %>" target="_blank" class="btn btn-circle bg-color-green"><i class="fa fa-fw fa fa-lg fa-file-text-o" aria-hidden="true"></i></a>--%>
                    <a onclick="$global.openToEnableContract(<%= item.ContractID %>);" class="btn btn-circle bg-color-red"><i class="fa fa-fw fa fa-lg fa-check-square-o" aria-hidden="true"></i></a>
                    <%  } %>
                    <%= item.CourseContractType.TypeName %>(<%= item.LessonPriceType.DurationInMinutes %>分鐘)
                </td>
            </tr>
            <%  } %>
        </tbody>
    </table>
    <!-- end content -->
    <script>

        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            resizable: true,
            modal: true,
            width: "auto",
            height: "auto",
            title: "<h4 class='modal-title'><i class='fa fa-fw fa-list-ol'></i>  待辦事項：新合約(<%= ((Naming.CourseContractStatus?)_viewModel.Status).ToString() %>)</h4>",
            close: function () {
                $('#<%= _dialog %>').remove();
            }
        });

        $(function () {
            var responsiveHelper_<%= _tableId %> = undefined;

            var responsiveHelper_datatable_fixed_column = undefined;
            var responsiveHelper_datatable_col_reorder = undefined;
            var responsiveHelper_datatable_tabletools = undefined;

            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            $('#<%= _tableId %>').dataTable({
                "sDom": "",
                "autoWidth": true,
                "bPaginate": false,
                "order": [],
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_<%= _tableId %>) {
                        responsiveHelper_<%= _tableId %> = new ResponsiveDatatablesHelper($('#<%= _tableId %>'), breakpointDefinition);
                    }
                },
                "rowCallback": function (nRow) {
                    responsiveHelper_<%= _tableId %>.createExpandIcon(nRow);
                },
                "drawCallback": function (oSettings) {
                    responsiveHelper_<%= _tableId %>.respond();
                }
            });

            $global.editContract = function (contractID) {
                showLoading();
                $.post('<%= Url.Action("EditCourseContract","CourseContract") %>', { 'contractID': contractID }, function (data) {
                    hideLoading();
                    $(data).appendTo($('body'));
                    $('#<%= _dialog %>').dialog('close');
                });
            };

            $global.deleteContract = function (contractID) {
                if (confirm('確定刪除?')) {
                    var event = event || window.event;
                    showLoading();
                    $.post('<%= Url.Action("DeleteCourseContract","CourseContract") %>', { 'contractID': contractID }, function (data) {
                        hideLoading();
                        if (data.result) {
                            $(event.target).closest('tr').remove();
                        }
                    });
                }
            };

            $global.approveContract = function (contractID) {
                showLoading();
                $.post('<%= Url.Action("ApproveContract","CourseContract") %>', { 'contractID': contractID }, function (data) {
                    hideLoading();
                    $(data).appendTo($('body'));
                    $('#<%= _dialog %>').dialog('close');
                });
            };

            $global.signContract = function (contractID) {
                showLoading();
                $.post('<%= Url.Action("SignContract","CourseContract") %>', { 'contractID': contractID }, function (data) {
                    hideLoading();
                    $(data).appendTo($('body'));
                    $('#<%= _dialog %>').dialog('close');
                });
            };

            $global.openToApproveContract = function (contractID) {
                $('#<%= _dialog %>').dialog('close');
                window.open('<%= Url.Action("ContractApprovalView","CourseContract") %>' + '?contractID=' + contractID, '_blank', 'fullscreen=yes');
                smartAlert('合約審核中...', function () {
                    //showLoading();
                    window.location.href = '<%= Url.Action("Index","CoachFacet") %>';
                });
            };

            $global.openToSignContract = function (contractID) {
                $('#<%= _dialog %>').dialog('close');
                window.open('<%= Url.Action("ContractSignatureView","CourseContract") %>' + '?contractID=' + contractID, '_blank', 'fullscreen=yes');
                smartAlert('簽名進行中...', function () {
                    //showLoading();
                    window.location.href = '<%= Url.Action("Index","CoachFacet") %>';
                });
            };

            $global.enableContract = function (contractID) {
                showLoading();
                $.post('<%= Url.Action("EnableContract","CourseContract") %>', { 'contractID': contractID }, function (data) {
                    hideLoading();
                    $(data).appendTo($('body'));
                    $('#<%= _dialog %>').dialog('close');
                });
            };

            $global.openToEnableContract = function (contractID) {
                $('#<%= _dialog %>').dialog('close');
                window.open('<%= Url.Action("ContractAllowanceView","CourseContract") %>' + '?contractID=' + contractID, '_blank', 'fullscreen=yes');
                smartAlert('合約審核中...', function () {
                    //showLoading();
                    window.location.href = '<%= Url.Action("Index","CoachFacet") %>';
                });
            };

            $global.enableContractStatus = function (contractID) {
                var event = event || window.event;
                var $tr = $(event.target).closest('tr');
                showLoading();
                $.post('<%= Url.Action("EnableContractStatus","CourseContract",new { Status = (int)Naming.CourseContractStatus.已生效 }) %>', { 'contractID': contractID }, function (data) {
                    hideLoading();
                    if (data.result) {
                        alert('合約已生效!!');
                        $tr.remove();
                    } else {
                        $(data).appendTo($('body')).remove();
                    }
                });
            };

        });

    </script>
</div>



<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _tableId = "dt_contractTodo" + DateTime.Now.Ticks;
    IQueryable<CourseContract> _model;
    String _dialog = "draftContractlist" + DateTime.Now.Ticks;
    CourseContractViewModel _viewModel;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<CourseContract>)this.Model;
        _viewModel = (CourseContractViewModel)ViewBag.ViewModel;

    }

</script>

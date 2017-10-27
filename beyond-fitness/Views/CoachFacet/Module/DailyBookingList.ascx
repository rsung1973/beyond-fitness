﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<div id="undolistDialog" title="待辦事項" class="bg-color-darken">
    <!-- content -->
    <table id="<%= _tableId %>" class="table table-striped table-bordered table-hover" width="100%">
        <thead>
            <tr>
                <th>時間</th>
                <th>類別</th>
                <th>學員</th>
                <th data-hide="phone">體能顧問</th>
                <th data-hide="phone">目前狀態</th>
            </tr>
        </thead>
        <tbody>
            <%  foreach (var item in _model)
                { %>
            <tr>
                <td><%= item.ClassTime.Value.ToString("yyyy/MM/dd HH:mm") %>~<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %></td>
                <td><%  if(item.RegisterLesson.RegisterLessonEnterprise==null)
                        { %>
                            <%= item.RegisterLesson.LessonPriceType.Status.LessonTypeStatus() %>
                    <%  }
                        else
                        { %>
                            <%= item.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status.LessonTypeStatus() %>
                    <%  } %>
                </td>
                <td><%= String.Join("/",item.GroupingLesson.RegisterLesson.Select(r=>r.UserProfile.RealName)) %></td>
                <td><%= item.AsAttendingCoach.UserProfile.FullName() %></td>
                <td>
                    <%  if (item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自由教練預約)
                        {   %>
                            <a class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>
                    <%  }
                        else
                        {
                            var expansion = item.LessonTimeExpansion.First(); %>
                            <a onclick='makeLessonPlan(<%= JsonConvert.SerializeObject(new
                                {
                                    classDate = expansion.ClassDate.ToString("yyyy-MM-dd"),
                                    hour = expansion.Hour,
                                    registerID = item.RegisterID,
                                    lessonID = item.LessonID
                                }) %>);'
                                class="btn btn-circle bg-color-yellow"><i class="fa fa-fw fa fa-lg fa-edit" aria-hidden="true"></i></a>
                            <%  if (!item.LessonPlan.CommitAttendance.HasValue && _profile.IsAssistant())
                                { %>
                            <a href="#" onclick="$global.checkLessonAttendance(<%= item.LessonID %>);" class="btn btn-circle bg-color-red"><i class="fa fa-fw fa fa-lg fa-check-square-o" aria-hidden="true"></i></a>
                            <%  } %>
                    <%  }   %>
                    <%= item.TrainingPlan.Count==0
                                    ? item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自由教練預約 
                                        ? item.LessonAttendance == null 
                                            ? "已預約"
                                            : "已完成課程"
                                        : "已預約"
                                    : item.LessonAttendance!=null
                                        ? "已完成課程"
                                        : "編輯課程內容中" %>
                    <%  if (item.LessonPlan.CommitAttendance.HasValue)
                        { %>
                    (學員已打卡）
                    <%  }   %>
                </td>
            </tr>
            <%  } %>
        </tbody>
    </table>
    <!-- end content -->
    <script>
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
            //"bPaginate": false,
            "pageLength": 10,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "全部"]],
            "ordering": true,
            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
            "autoWidth": true,
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
    });
    </script>

</div>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    IQueryable<LessonTime> _model;
    String _tableId;
    UserProfile _profile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<LessonTime>)this.Model;
        _tableId = ViewBag.DataTableId ?? "dt_dailyList" + DateTime.Now.Ticks;
        _profile = Context.GetUser();
    }

</script>

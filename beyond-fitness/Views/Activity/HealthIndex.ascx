﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<br />
<br />
<table id="<%= _tableId %>" class="table table-striped table-bordered table-hover" width="100%">
    <thead>
        <tr>
            <th data-class="expand">日期</th>
            <th>疲勞指數</th>
            <th>睡眠時間(小時)</th>
            <th>體重(KG)</th>
            <th data-hide="phone">腰(CM)</th>
            <th data-hide="phone">腿(CM)</th>
            <th data-hide="phone">臀(CM)</th>
            <th data-hide="phone">皮脂厚度(MM)</th>
            <th data-hide="phone">體脂率(%)</th>
        </tr>
    </thead>
    <tbody>
        <%  foreach (var item in _items)
            {
                var item10 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 10).FirstOrDefault();
                var item12 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 12).FirstOrDefault();
                var item13 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 13).FirstOrDefault();
                var item14 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 14).FirstOrDefault();
                var item15 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 15).FirstOrDefault();
                var item49 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 49).FirstOrDefault();
                var item50 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 50).FirstOrDefault();
                var item51 = item.LessonFitnessAssessmentReport.Where(r => r.ItemID == 51).FirstOrDefault();

                if (item10 == null && item12 == null && item13 == null && item14 == null && item15 == null 
                    && item49 == null && item50 == null && item51 == null)
                {
                    continue;
                }

                %>
        <tr>
            <td><%= String.Format("{0:yyyy/MM/dd}", item.LessonTime.ClassTime) %></td>
            <td><%= item10!=null && item10.TotalAssessment>0 ? item10.TotalAssessment.ToString() : "--" %></td>
            <td><%= item12!=null && item12.TotalAssessment>0 ? item12.TotalAssessment.ToString() : "--" %></td>
            <td><%= item49!=null && item49.TotalAssessment>0 ? item49.TotalAssessment.ToString() : "--" %></td>
            <td><%= item13!=null && item13.TotalAssessment>0 ? item13.TotalAssessment.ToString() : "--" %></td>
            <td><%= item14!=null && item14.TotalAssessment>0 ? item14.TotalAssessment.ToString() : "--" %></td>
            <td><%= item15!=null && item15.TotalAssessment>0 ? item15.TotalAssessment.ToString() : "--" %></td>
            <td><%= item50!=null && item50.TotalAssessment>0 ? item50.TotalAssessment.ToString() : "--" %></td>
            <td><%= item51!=null && item51.TotalAssessment>0 ? item51.TotalAssessment.ToString() : "--" %></td>
        </tr>
        <%  } %>
    </tbody>
</table>

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
            "bPaginate": true,
            "pageLength": 10,
            "lengthMenu": [[10, 50, 100, -1], [10, 50, 100, "全部"]],
            "order": [[0, "desc"]],
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

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _tableId;
    LessonFitnessAssessment _model;
    IEnumerable<LessonFitnessAssessment> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _tableId = "dt_health" + DateTime.Now.Ticks;
        _model = (LessonFitnessAssessment)this.Model;
        _items = models.GetTable<LessonFitnessAssessment>().Where(f => f.UID == _model.UID)
                    .OrderByDescending(f => f.AssessmentID);
    }

</script>

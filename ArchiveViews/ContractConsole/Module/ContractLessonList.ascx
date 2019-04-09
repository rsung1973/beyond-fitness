﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table id="<%= _tableId %>" class="table table-striped table-custom nowrap bg-white col-charcoal dataTable dataTable-learner" width="100%">
    <thead>
        <tr>
            <th></th>
            <th>上課地點</th>
            <th>上課日期</th>
            <th>上課時間</th>            
            <th>完成上課</th>
            <th>學生打卡</th>
            <th>體能顧問</th>
            <th>學生</th>                        
        </tr>
    </thead>
    <tbody>
        <%  
            int idx = 1;
            foreach (var item in _model)
            { %>
        <tr>
            <td><%= idx++ %></td>
            <td><%= item.BranchStore.BranchName %></td>
            <td><%= $"{item.ClassTime:yyyy/MM/dd}" %></td>
            <td><%= $"{item.ClassTime:HH:mm}" %>-<%= $"{item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value):HH:mm}" %></td>            
            <td><%= $"{item.LessonAttendance?.CompleteDate:yyyy/MM/dd HH:mm}" %></td>
            <td><%= $"{item.LessonPlan.CommitAttendance:yyyy/MM/dd HH:mm}" %></td>                
            <td><%= item.AsAttendingCoach.UserProfile.FullName() %></td>
            <td><%= item.RegisterLesson.LessonLearner() %></td>                   
        </tr>
        <%  } %>
    </tbody>
</table>

<script>

    debugger;
    $(function () {

        $('#<%= _tableId %>').ready(function () {
            $(this).resize();
        });

        var theDataTable = null;
        function buildDataTable() {
            theDataTable = $('#<%= _tableId %>').DataTable({
                "filter": false,
                "bPaginate": false,
                "info": false,
                "order": [
                    [0, 'asc'],
                ],
                "language": {
                    "lengthMenu": "每頁顯示 _MENU_ 筆資料",
                    "zeroRecords": "沒有資料也是種福氣",
                    "info": "共 _TOTAL_ 筆，目前顯示第 _START_ 至 _END_筆資料",
                    "infoEmpty": "顯示 0 筆的資料",
                    "infoFiltered": "(總共從 _MAX_ 筆資料過濾)",
                    "loadingRecords": "快馬加鞭處理中...",
                    "processing": "快馬加鞭處理中...",
                    "search": "快速搜尋：",
                    "paginate": {
                        "first": "第一頁",
                        "last": "最後一頁",
                        "next": "下一頁",
                        "previous": "前一頁"
                    },
                },
                scrollX: true,
                scrollCollapse: true,
                "columnDefs": [
                    {
                        targets: [0, 1, 2, 3, 4, 7],
                        className: "align-center"
                    }
                ],
                "initComplete": function () {
                },
            });
        }

        var $collapse = $('#<%= _tableId %>').closest('.panel-collapse');
        if ($collapse.length > 0) {
            $collapse.on('shown.bs.collapse', function (event) {
                if (!theDataTable) {
                    buildDataTable();
                }
            });
        } else {
            buildDataTable();
        }
    });

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _tableId = "dt_lesson" + DateTime.Now.Ticks;
    IEnumerable<LessonTime> _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = ((IEnumerable<LessonTime>)this.Model).OrderBy(l=>l.ClassTime);
    }

</script>

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

<table id="dt_basic" class="table table-forum" width="100%">
    <thead>
        <tr>
            <th data-hide="phone" style="width: 40px"><i class="fa fa-fw fa-calendar-plus-o text-muted hidden-md hidden-sm hidden-xs"></i>時間</th>
            <th data-class="expand">課程類型</th>
            <th data-hide="phone">是否為團體課程</th>
            <th style="width: 120px"><i class="fa fa-fw fa-credit-card text-muted hidden-md hidden-sm hidden-xs"></i>購買/剩餘</th>
            <%  if (ViewBag.ShowOnly != true)
                { %>
            <th class="text-center">功能</th>
            <%  } %>
        </tr>
    </thead>
    <tbody>
        <% if (_items != null && _items.Count() > 0)
        {
            foreach (var item in _items)
            { %>
                <tr>
                    <td><%= item.RegisterDate.ToString("yyyy/MM/dd") %></td>
                    <td><%= item.LessonPriceType.Description + " " + item.LessonPriceType.ListPrice %></td>
                    <td>
                        <%  if (item.GroupingMemberCount > 1)
                            {   
                                var currentGroups = models.GetTable<GroupingLesson>().Where(g => g.GroupID == item.RegisterGroupID)
                                    .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterID != item.RegisterID), g => g.GroupID,
                                        r => r.RegisterGroupID, (g, r) => r);   
                                %>
                                <ul class="list-inline friends-list">
                                    <%  foreach (var g in currentGroups)
                                        { %>
                                            <li>
                                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ShowLearner/") + g.UID %>">
                                                    <% g.UserProfile.RenderUserPicture(Writer, "_" + g.UID ); %><%= g.UserProfile.RealName %></a>
                                            </li>
                                    <%  } %>
                                </ul>
                        <%  }
                            else
                            {   %>
                                否
                        <%  } %>
                    </td>
                    <td><%= item.Lessons %> / <%= item.Lessons-item.LessonTime.Count(l=>l.AttendingCoach!= null) %></td>
                </tr>
        <%  }
        } %>
    </tbody>
</table>
<script>
    $(function () {

        /* BASIC ;*/
        var responsiveHelper_dt_basic = undefined;
        var responsiveHelper_datatable_fixed_column = undefined;
        var responsiveHelper_datatable_col_reorder = undefined;
        var responsiveHelper_datatable_tabletools = undefined;

        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('#dt_basic').dataTable({
            "sDom": "",
            "autoWidth": true,
            "oLanguage": {
                "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
            },
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_dt_basic) {
                    responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#dt_basic'), breakpointDefinition);
                }
            },
            "rowCallback": function (nRow) {
                responsiveHelper_dt_basic.createExpandIcon(nRow);
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_dt_basic.respond();
            }
        });

        /* END BASIC */
    });
</script>
<script runat="server">

    IEnumerable<RegisterLesson> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = (IEnumerable<RegisterLesson>)this.Model;
    }

</script>

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

<!-- TABLE 1 -->
<%  if (_items != null && _items.Count() > 0)
    { %>
        <table id="dt_basic" class="table table-striped table-bordered table-hover" width="100%">
            <thead>
                <tr>
                    <th data-hide="phone">#</th>
                    <th data-class="expand"><i class="fa fa-fw fa-user text-muted hidden-md hidden-sm hidden-xs"></i>姓名</th>
                    <th data-hide="phone"><i class="fa fa-fw fa-phone text-muted hidden-md hidden-sm hidden-xs"></i>電話</th>
                    <th data-hide="phone"><i class="fa fa-fw fa-envelope text-muted hidden-md hidden-sm hidden-xs"></i>Email Address</th>
                    <th data-hide="phone">編號</th>
                    <th data-hide="phone">自由教練</th>
                    <th>功能</th>
                </tr>
            </thead>
            <tbody>
                <%  int idx = 0;
                    foreach (var item in _items)
                    {
                        idx++;  %>
                        <tr>
                            <td><%= idx %></td>
                            <td><%= item.RealName %></td>
                            <td><%= item.Phone %></td>
                            <td><%= item.LevelID==(int)Naming.MemberStatusDefinition.ReadyToRegister ? "尚未註冊" :  item.PID %></td>
                            <td><%= item.MemberCode %></td>
                            <td><%= item.CurrentUserRole.RoleID == (int)Naming.RoleID.FreeAgent ? "是" : "否" %></td>
                            <td>
                                <div class="btn-group dropup">
                                    <button class="btn bg-color-blueLight" data-toggle="dropdown">
                                        請選擇功能
                                    </button>
                                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                                        <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/EditCoach/") + item.UID %>"><i class="fa fa-fw fa fa-edit" aria-hidden="true"></i>修改資料</a>
                                        </li>
                                        <%  if (item.LevelID == (int)Naming.MemberStatusDefinition.Deleted)
                                            { %>
                                                <li>
                                                    <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/EnableCoach/") + item.UID %>"><i class="fa fa-fw fa fa-edit" aria-hidden="true"></i>啟用</a>
                                                </li>
                                        <%  }
                                            else
                                            { %>
                                                <li>
                                                    <a onclick='deleteCoach(<%= item.UID %>);'><i class="fa fa-fw fa fa-trash-o" aria-hidden="true"></i>刪除資料</a>
                                                </li>
                                        <%  } %>
                                        <li>
                                            <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ShowMember/") + item.UID %>"><i class="fa fa-fw fa fa-eye" aria-hidden="true"></i>檢視資料</a>
                                        </li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                <%  } %>
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
            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
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

<%  }
    else { %>
<h4>未建立資料</h4>
<% } %>
<script runat="server">

    ModelSource<UserProfile> models;
    IEnumerable<UserProfile> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = (ModelSource<UserProfile>)this.Model;

        _items = models.EntityList  //.Where(u => u.LevelID != (int)Naming.MemberStatusDefinition.Deleted)
            .Join(models.GetTable<UserRole>()
                .Where(r => r.RoleID == (int)Naming.RoleID.Coach || r.RoleID == (int)Naming.RoleID.FreeAgent),
            u => u.UID, r => r.UID, (u, r) => u);
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table id="<%= _tableId %>" class="table table-striped table-bordered table-hover" width="100%">
    <thead>
        <tr>
            <th>分店</th>
            <th>成功筆數</th>
            <th>失敗筆數</th>
            <th>功能</th>
        </tr>
    </thead>
    <tbody>
        <%  if (_model.Count() > 0)
            {
                var items = models.GetTable<Organization>().Where(o => o.BranchStore != null)
                    .GroupJoin(_model, o => o.CompanyID, d => d.InvoiceItem.SellerID, (o, d) => new { Seller = o, Logs = d });
                foreach (var item in items)
                { %>
        <tr>
            <td><%= item.Seller.BranchStore!=null ? item.Seller.BranchStore.BranchName : item.Seller.CompanyName %></td>
            <td><%= item.Logs.Where(d => d.Status == (int)Naming.GeneralStatus.Successful).Count() %></td>
            <td><%  var failed = item.Logs.Where(d => d.Status == (int)Naming.GeneralStatus.Failed).Count(); %>
                <%= failed %>
            </td>
            <td><%  if (failed > 0)
                    { %>
                <a onclick="showInvoiceByDispatch(<%= item.Seller.CompanyID %>,<%= (int)Naming.GeneralStatus.Failed %>);" class="btn btn-circle bg-color-yellow failListDialog_link"><i class="fa fa-fw fa fa-lg fa-list-ol" aria-hidden="true"></i></a>
                <%  } %>
            </td>
        </tr>
        <%      }
            } %>
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
            //"bPaginate": false,
            "pageLength": 30,
            "lengthMenu": [[30, 50, 100, -1], [30, 50, 100, "全部"]],
            "ordering": true,
            "order": [[0, "asc"]],
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
    String _tableId = "invoiceDispatch" + DateTime.Now.Ticks;
    IQueryable<InvoiceItemDispatchLog> _model;
    UserProfile _profile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<InvoiceItemDispatchLog>)this.Model;
        _profile = Context.GetUser();
    }

</script>

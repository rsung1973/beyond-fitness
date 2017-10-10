﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<label class="label label-warning">狀態後<i class="fa fa-asterisk"></i> 表示主管尚未對帳</label>
<table id="<%= _tableId %>" class="table table-striped table-bordered table-hover" width="100%">
    <thead>
        <tr>
            <th data-class="expand">發票號碼</th>
            <th>分店</th>
            <th data-hide="phone">收款人</th>
            <th>學員</th>
            <th>收款/作廢日期</th>
            <th data-hide="phone">收款品項</th>
            <th>收款金額</th>
            <th data-hide="phone">收款方式</th>
            <th data-hide="phone">發票類型</th>
            <th data-hide="phone">發票狀態</th>
            <th data-hide="phone,tablet">買受人統編</th>
            <th data-hide="phone,tablet">合約編號</th>
            <th data-hide="phone,tablet">合約總金額</th>
            <th data-hide="phone">狀態</th>
        </tr>
    </thead>
    <tbody>
        <%  foreach (var item in _model)
            { %>
        <tr>
            <td nowrap="noWrap"><%  if (item.InvoiceID.HasValue)
                                    {   %>
                        <%= item.InvoiceItem.TrackCode %><%= item.InvoiceItem.No %>
                <%  } %>
            </td>
            <td><%= item.PaymentTransaction.BranchStore.BranchName %></td>
            <td ><%= item.UserProfile.FullName() %></td>
            <td ><%= item.TuitionInstallment != null
                        ? item.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.FullName()
                        : item.ContractPayment != null
                            ? item.ContractPayment.CourseContract.ContractOwner.FullName()
                            : "--" %></td>
            <td nowrap="noWrap"><%= String.Format("{0:yyyy/MM/dd}", item.PayoffDate) %></td>
            <td><%= ((Naming.PaymentTransactionType)item.TransactionType).ToString() %>
                <%  if (item.TransactionType == (int)Naming.PaymentTransactionType.運動商品
    || item.TransactionType == (int)Naming.PaymentTransactionType.飲品)
                    { %>
                (<%= String.Join("、", item.PaymentTransaction.PaymentOrder.Select(p => p.MerchandiseWindow.ProductName)) %>)
                <%  } %>
            </td>
            <td nowrap="noWrap" class="text-right"><%= item.PayoffAmount >= 0 ? String.Format("{0:##,###,###,###}", item.PayoffAmount) : String.Format("({0:##,###,###,###})", -item.PayoffAmount) %></td>
            <td><%= item.PaymentType %></td>
            <td><%= item.InvoiceID.HasValue
                        ? item.InvoiceItem.InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票
                            ? "電子發票"
                            : "紙本"
                        : "--" %></td>
            <td><%= item.VoidPayment == null
                        ? "已開立"
                        : item.VoidPayment.Status == (int)Naming.CourseContractStatus.已生效
                            ? "已作廢"
                            : "已開立" %></td>
            <td><%= item.InvoiceID.HasValue
                        ? item.InvoiceItem.InvoiceBuyer.IsB2C() ? "--" : item.InvoiceItem.InvoiceBuyer.ReceiptNo
                        : "--" %></td>
            <td nowrap="noWrap">
                <%  if (item.ContractPayment != null)
                    { %>
                        <%= item.ContractPayment.CourseContract.ContractNo() %>
                <%  } %>
            </td>
            <td nowrap="noWrap" class="text-right">
                <%  if (item.ContractPayment != null)
                    { %>
                <%= String.Format("{0:##,###,###,###}", item.ContractPayment.CourseContract.TotalCost) %>
                <%  }
                    else
                    { %>
                --
                <%  } %>
            </td>
            <td><%= (Naming.CourseContractStatus)item.Status %><%= item.PaymentAudit!=null && item.PaymentAudit.AuditorID.HasValue ? "" : "(*)" %></td>
        </tr>
        <%  if (item.VoidPayment != null && ViewBag.ShowFooter != false)
            { %>
        <tr>
            <td nowrap="noWrap"><%  if (item.InvoiceID.HasValue)
                                    {   %>
                <%= item.InvoiceItem.TrackCode %><%= item.InvoiceItem.No %>
                <%  } %>
            </td>
            <td nowrap="noWrap"><%= item.PaymentTransaction.BranchStore.BranchName %></td>
            <td nowrap="noWrap"><%= item.UserProfile.FullName() %></td>
            <td nowrap="noWrap"><%= item.TuitionInstallment != null
                            ? item.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.FullName()
                            : item.ContractPayment != null
                                ? item.ContractPayment.CourseContract.ContractOwner.FullName()
                                : "--" %></td>
            <td nowrap="noWrap"><%= item.VoidPayment.Status == (int)Naming.CourseContractStatus.已生效 ? String.Format("{0:yyyy/MM/dd}", item.VoidPayment.VoidDate) : "--" %></td>
            <td><%= ((Naming.PaymentTransactionType)item.TransactionType).ToString() %>
                <%  if (item.TransactionType == (int)Naming.PaymentTransactionType.運動商品
        || item.TransactionType == (int)Naming.PaymentTransactionType.飲品)
                    { %>
                (<%= String.Join("、", item.PaymentTransaction.PaymentOrder.Select(p => p.MerchandiseWindow.ProductName)) %>)
                <%  } %>
            </td>
            <td nowrap="noWrap" class="text-right">(<%= String.Format("{0:##,###,###,###}", item.PayoffAmount) %>)</td>
            <td><%= item.PaymentType %></td>
            <td><%= item.InvoiceID.HasValue
                            ? item.InvoiceItem.InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票
                                ? "電子發票"
                                : "紙本"
                            : "--" %></td>
            <td><%= item.VoidPayment.Status == (int)Naming.CourseContractStatus.已生效
                            ? "已作廢"
                            : "--" %></td>
            <td><%= item.InvoiceID.HasValue
                            ? item.InvoiceItem.InvoiceBuyer.IsB2C() ? "--" : item.InvoiceItem.InvoiceBuyer.ReceiptNo
                            : "--" %></td>
            <td nowrap="noWrap">
                <%  if (item.ContractPayment != null)
                    { %>
                <%= item.ContractPayment.CourseContract.ContractNo() %>
                <%  } %>
            </td>
            <td>--</td>
            <td><%= (Naming.VoidPaymentStatus)item.VoidPayment.Status %>(作廢)</td>
        </tr>
            <%  } %>
        <%  } %>
    </tbody>
    <%  if(_model.Count()>0 && ViewBag.ShowFooter!=false )
        { %>
    <tfoot>
        <tr>
            <%  var items = _model.Where(c => c.Status == (int)Naming.CourseContractStatus.已生效);
                var voidItems = items.Where(p => p.VoidPayment != null
                    && p.VoidPayment.Status == (int)Naming.CourseContractStatus.已生效);
                var voidCount = voidItems.Count(); %>
            <td class="text-right" colspan="6">已生效現金收款總計：</td>
            <td class="text-right"><%= String.Format("{0:##,###,###,###}",items.Where(p=>p.PaymentType=="現金").Sum(p=>p.PayoffAmount)-(voidItems.Where(p => p.PaymentType == "現金").Sum(p => p.PayoffAmount) ?? 0)) %></td>
            <%--<td class="text-right" colspan="2">已生效現金作廢總計：</td>
            <td class="text-right">
                <%  if (voidCount > 0)
                    { %>
                (<%= String.Format("{0:##,###,###,###}", voidItems.Where(p => p.PaymentType == "現金").Sum(p => p.PayoffAmount)) %>)
                <%  } %>
            </td>--%>
            <td colspan="5"></td>
        </tr>
        <tr>
            <td class="text-right" colspan="6">已生效刷卡收款總計：</td>
            <td class="text-right"><%= String.Format("{0:##,###,###,###}",items.Where(p=>p.PaymentType=="刷卡").Sum(p=>p.PayoffAmount)-(voidItems.Where(p => p.PaymentType == "刷卡").Sum(p => p.PayoffAmount) ?? 0)) %></td>
<%--            <td class="text-right" colspan="2">已生效刷卡作廢總計：</td>
            <td class="text-right">
                <%  if (voidCount > 0)
                    { %>
                (<%= String.Format("{0:##,###,###,###}", voidItems.Where(p => p.PaymentType == "刷卡").Sum(p => p.PayoffAmount)) %>)
                <%  } %>
            </td>--%>
            <td colspan="5"></td>
        </tr>
        <tr>
            <td class="text-right" colspan="6">已生效轉帳收款總計：</td>
            <td class="text-right"><%= String.Format("{0:##,###,###,###}",items.Where(p=>p.PaymentType=="轉帳").Sum(p=>p.PayoffAmount)-(voidItems.Where(p => p.PaymentType == "轉帳").Sum(p => p.PayoffAmount) ?? 0)) %></td>
<%--            <td class="text-right" colspan="2">已生效轉帳作廢總計：</td>
            <td class="text-right">
                <%  if (voidCount > 0)
                    { %>
                (<%= String.Format("{0:##,###,###,###}", voidItems.Where(p => p.PaymentType == "轉帳").Sum(p => p.PayoffAmount)) %>)
                <%  } %>
            </td>--%>
            <td colspan="5"></td>
        </tr>
    </tfoot>
    <%  } %>
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
            "order": [[4, "desc"]],
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

<%  if(_model.Count()>0)
    {  %>
        $('#btnDownload').css('display', 'inline');
<%  }  %>

    });
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _tableId = "paymentList" + DateTime.Now.Ticks;
    IQueryable<Payment> _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = ((IQueryable<Payment>)this.Model).Where(p => p.TransactionType.HasValue);
    }

</script>

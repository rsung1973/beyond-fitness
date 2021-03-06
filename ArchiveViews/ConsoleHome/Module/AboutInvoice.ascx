﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%  var items = models.GetTable<InvoiceItem>().Where(i => i.InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票);
    var invoiceItems = items.Where(i => i.InvoiceDate >= DateTime.Today && i.InvoiceDate < DateTime.Today.AddDays(1));
    var cancellationItems = models.GetTable<InvoiceCancellation>().Where(i => i.CancelDate >= DateTime.Today && i.CancelDate < DateTime.Today.AddDays(1));
    var allowanceItems = models.GetTable<InvoiceAllowance>().Where(a => a.AllowanceDate >= DateTime.Today && a.AllowanceDate < DateTime.Today.AddDays(1));
    %>
<div class="container-fluid">
    <h4 class="card-outbound-header">我的發票</h4>
    <div class="card widget_3">
        <ul class="row clearfix list-unstyled m-b-0">
            <%  foreach (var branch in models.GetTable<BranchStore>())
                {   %>
            <li class="col-lg-4 col-md-6 col-sm-12">
                <div class="body">
                    <div class="row">
                        <div class="col-8">
                            <%  var calcItems = invoiceItems.Where(p => p.SellerID == branch.BranchID);
                                var calcCancellation = items.Where(i => i.SellerID == branch.BranchID)
                                    .Join(models.GetTable<Payment>()
                                            .Join(models.GetTable<VoidPayment>().Where(v => v.Status == (int)Naming.CourseContractStatus.已生效),
                                                p => p.PaymentID, v => v.VoidID, (p, v) => p),
                                        i => i.InvoiceID, p => p.InvoiceID, (i, p) => i)
                                    .Join(cancellationItems, i => i.InvoiceID, c => c.InvoiceID, (i, c) => c);
                                var calcAllowance = items.Where(i => i.SellerID == branch.BranchID)
                                    .Join(allowanceItems, i => i.InvoiceID, c => c.InvoiceID, (i, c) => c);

                                var invTurnkeyLogs = calcItems.Join(models.GetTable<InvoiceItemDispatchLog>(), i => i.InvoiceID, d => d.InvoiceID, (i, d) => d);
                                var cancellationTurnkeyLogs = calcCancellation.Join(models.GetTable<InvoiceCancellationDispatchLog>(), i => i.InvoiceID, d => d.InvoiceID, (i, d) => d);
                                var allowanceTurnkeyLogs = calcAllowance.Join(models.GetTable<InvoiceAllowanceDispatchLog>(), i => i.AllowanceID, d => d.AllowanceID, (i, d) => d);
                                %>
                            <h5 class="m-t-0"><%= branch.BranchName %></h5>
                            <p class="text-small">
                                新增：<%= calcItems.Count() %><br />
                                作廢：<%= calcCancellation.Count() %><br />
                                折讓：<%= calcAllowance.Count() %><br />
                            </p>
                        </div>
                        <div class="col-4 text-right">
                            <h2 class="col-red">
                                <%= invTurnkeyLogs.Where(i=>i.Status==(int)Naming.GeneralStatus.Successful).Count()
                                        + allowanceTurnkeyLogs.Where(i=>i.Status==(int)Naming.GeneralStatus.Successful).Count()
                                        + cancellationTurnkeyLogs.Where(i=>i.Status==(int)Naming.GeneralStatus.Successful).Count()  %>
                            </h2>
                            <small class="info">上傳成功</small>
                        </div>
                    </div>
                </div>
            </li>
            <%  } %>
            <%  var periodNo = (DateTime.Today.Month + 1) / 2;
                var trackCodeItems = models.GetTable<InvoiceTrackCode>().Where(t => t.Year == DateTime.Today.Year && t.PeriodNo == periodNo);
                var assignmentItems = models.GetTable<InvoiceTrackCodeAssignment>()
                        .Join(trackCodeItems, a => a.TrackID, t => t.TrackID, (a, t) => a);
                %>
            <li class="col-lg-6 col-md-6 col-sm-12">
                <div class="body">
                    <div class="row">
                        <div class="col-9">
                            <h5 class="m-t-0">發票號碼</h5>
                            <p class="text-small">
                                <%  foreach (var branch in models.GetTable<BranchStore>())
                                    {
                                        var intervalItems = assignmentItems.Where(a => a.SellerID == branch.BranchID)
                                                .SelectMany(a => a.InvoiceNoInterval);
                                        int assignedCount = 0;
                                        int remainedCount = 0;
                                        foreach(var v in intervalItems)
                                        {
                                            var intervalCount = v.EndNo - v.StartNo + 1;
                                            assignedCount += (intervalCount);
                                            var noItem = v.InvoiceNoAssignment.OrderByDescending(n => n.InvoiceID).FirstOrDefault();
                                            if (noItem != null)
                                            {
                                                remainedCount += (v.EndNo - noItem.InvoiceNo.Value);
                                            }
                                            else
                                            {
                                                remainedCount += intervalCount;
                                            }
                                        }
                                        %>
                                <%= branch.BranchName %>：共
                                    <%= assignedCount /50 %>
                                本，
                                <%  if (remainedCount < 50)
                                    {   %>
                                <span class="col-red">尚有0本<%= remainedCount %>張</span>
                                <%  }
                                    else
                                    {   %>
                                尚有<%= remainedCount/50 %>本<%= remainedCount%50 %>張
                                <%  }   %>
                                <br />
                                <%  } %>
                            </p>
                        </div>
                        <div class="col-3 text-right">
                            <h2 class=""><%= periodNo*2-1 %>~<%= periodNo*2 %></h2>
                            <small class="info">月</small>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
    }


</script>

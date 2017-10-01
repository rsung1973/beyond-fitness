<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<div id="<%= _dialog %>" title="編輯收款" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white no-padding">
        <form action="<%= Url.Action("CommitPaymentForShopping","Payment",new { _viewModel.PaymentID }) %>" class="smart-form" method="post" autofocus>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">收款類別</label>
                        <label class="select">
                            <select name="TransactionType">
                                <option value="1">體能顧問費</option>
                                <option value="2">自主訓練</option>
                                <option value="3" <%= _viewModel.TransactionType==(int)Naming.PaymentTransactionType.飲品 ? "selected" : null %> >飲品</option>
                                <option value="4" <%= _viewModel.TransactionType==(int)Naming.PaymentTransactionType.運動商品 ? "selected" : null %>>運動商品</option>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                        <script>
                            $('#<%= _dialog %> select[name="TransactionType"]').on('change', function (evt) {
                                showLoading();
                                $.post('<%= Url.Action("EditPayment","Payment") %>', { 'TransactionType': $(this).val() }, function (data) {
                                hideLoading();
                                $('#<%= _dialog %>').dialog('close');
                                $(data).appendTo($('body'));
                            });
                        });
                        </script>
                    </section>
                    <section class="col col-6">
                        <label class="label">分店</label>
                        <label class="select">
                            <select name="SellerID">
                                <%  BranchStore branch = models.GetTable<BranchStore>().First();
                                    if(_profile.IsManager() || _profile.IsViceManager())
                                    {
                                        branch = models.GetTable<BranchStore>().Where(b => b.ManagerID == _profile.UID || b.ViceManagerID == _profile.UID).FirstOrDefault();
                                    }
                                    else if(_profile.IsCoach())
                                    {
                                        branch = models.GetTable<CoachWorkplace>().Where(w => w.CoachID == _profile.UID).Select(w => w.BranchStore).FirstOrDefault();
                                    }
                                    Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.ascx", model: branch != null ? branch.BranchID : -1); %>
                            </select>
                            <i class="icon-append fa fa-at"></i>
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">項目</label>
                        <label class="select">
                            <select name="ProductID">
                                <%  var items = models.GetTable<MerchandiseTransaction>().Where(m => m.TransactionID == _viewModel.TransactionType)
                                        .Select(m => m.MerchandiseWindow)
                                        .Where(p => p.Status == (int)Naming.MerchandiseStatus.OnSale)
                                        .OrderBy(p => p.ProductName);
                                    Html.RenderPartial("~/Views/SystemInfo/MerchandiseOptions.ascx", items); %>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">數量</label>
                        <label class="input">
                            <i class="icon-append fa fa-cart-plus"></i>
                            <input type="number" name="ProductCount" maxlength="10" placeholder="請輸入數量" value="<%= _viewModel.ProductCount %>" />
                        </label>
                        <script>
                            $(function () {
                                function calc() {
                                    var count = Number($('#<%= _dialog %> input[name="ProductCount"]').val());
                                    if (!isNaN(count)) {
                                        var price = Number($('#<%= _dialog %> select[name="ProductID"] option:selected').attr('data-unit-price'));
                                        if (!isNaN(price)) {
                                            $('#<%= _dialog %> input[name="PayoffAmount"]').val(count * price);
                                        }
                                    }
                                }

                                $('#<%= _dialog %> input[name="ProductCount"]').on('blur', function (evt) {
                                    calc();
                                });

                                $('#<%= _dialog %> select[name="ProductID"]').on('change', function (evt) {
                                    calc();
                                });

                                calc();
                            });
                        </script>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">收款日期</label>
                        <label class="input">
                            <i class="icon-append fa fa-calendar"></i>
                            <input type="text" name="PayoffDate" readonly="readonly" class="form-control date form_date" data-date-format="yyyy/mm/dd" placeholder="請點選日曆" value='<%= String.Format("{0:yyyy/MM/dd}",_viewModel.PayoffDate) %>' />
                        </label>
                    </section>
                    <section class="col col-4">
                        <label class="label">發票類型</label>
                        <div class="inline-group">
                            <label class="radio">
                                <input type="radio" name="InvoiceType" value="<%= (int)Naming.InvoiceTypeDefinition.二聯式 %>" <%= _viewModel.InvoiceType!=Naming.InvoiceTypeDefinition.一般稅額計算之電子發票 ? "checked" : null %> />
                                <i></i>紙本</label>
                            <%--<label class="radio">
                                <input type="radio" disabled="disabled" name="InvoiceType" value="<%= (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票 %>" <%= _viewModel.InvoiceType==Naming.InvoiceTypeDefinition.一般稅額計算之電子發票 ? "checked" : null %> />
                                <i></i>電子</label>--%>
                        </div>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">收款方式</label>
                        <label class="select">
                            <select name="PaymentType">
                                <option value="現金">現金</option>
                                <option value="刷卡">刷卡</option>
                                <%--<option value="轉帳">轉帳</option>--%>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                        <script>
                            $('#<%= _dialog %> select[name="PaymentType"]').val('<%= _viewModel.PaymentType %>');
                        </script>
                    </section>
                    <section class="col col-6">
                        <label class="label">收款金額</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="PayoffAmount" maxlength="10" placeholder="請輸入收款金額" value="<%= _viewModel.PayoffAmount %>" />
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">買受人統編</label>
                        <label class="input">
                            <i class="icon-append fa fa-user"></i>
                            <input type="number" name="BuyerReceiptNo" maxlength="10" placeholder="請輸入買受人統編" value="<%= _viewModel.BuyerReceiptNo %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">發票號碼</label>
                        <label class="input">
                            <i class="icon-append fa fa-qrcode"></i>
                            <input type="text" name="InvoiceNo" maxlength="20" placeholder="請輸入發票號碼" value="<%= _viewModel.InvoiceNo %>" />
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <section>
                    <label class="label">備註</label>
                    <textarea class="form-control" name="Remark" placeholder="請輸入備註" rows="3"><%= _viewModel.Remark %></textarea>
                </section>
            </fieldset>
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            width: "auto",
            resizable: true,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-edit'></i>  編輯收款</h4></div>",
            buttons: [{
                html: "<i class='fa fa-send'></i>&nbsp; 確定",
                "class": "btn btn-primary",
                click: function () {
                    if (confirm("請再次確認收款資料正確?")) {
                        clearErrors();
                        var $form = $('#<%= _dialog %> form');
                        $form.ajaxSubmit({
                            beforeSubmit: function () {
                                showLoading();
                            },
                            success: function (data) {
                                hideLoading();
                                if ($.isPlainObject(data)) {
                                    if (data.result) {
                                        alert('收款資料已生效!!');
                                        <%--$('#<%= _dialog %>').dialog('close');--%>
                                        showLoading();
                                        window.location.href = '<%= Url.Action("PaymentIndex","Payment") %>';
                                    } else {
                                        alert(data.message);
                                    }
                                } else {
                                    $(data).appendTo($('body')).remove();
                                }
                            }
                        });
                    }
                }
            }
            <%--, {
                html: "<i class='fa fa-qrcode'></i>&nbsp; 確定產生發票",
                "class": "btn btn-primary",
                click: function () {
                    if (confirm("請再次確認收款資料正確")) {
                        $(this).dialog("close");
                        window.location.href = "invoicelist.html";
                    }
                }
            }--%>],
            close: function () {
                $('#<%= _dialog %>').remove();
            }
        });

        $('.form_date').datetimepicker({
            language: 'zh-TW',
            weekStart: 0,
            todayBtn: 1,
            clearBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });

    </script>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _dialog = "payment" + DateTime.Now.Ticks;
    PaymentViewModel _viewModel;
    UserProfile _profile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (PaymentViewModel)ViewBag.ViewModel;
        _profile = Context.GetUser();
    }

</script>

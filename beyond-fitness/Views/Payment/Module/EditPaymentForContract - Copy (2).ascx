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
        <form action="#" class="smart-form" autofocus>
            <fieldset>
                <section>
                    <label class="label">收款類別</label>
                    <label class="select">
                        <select name="TransactionType">
                            <option value="1">體能顧問費</option>
                            <option value="2">自主訓練</option>
                            <option value="3">飲品</option>
                            <option value="4">運動商品</option>
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
            </fieldset>
            <fieldset id="pisession" style="display: none">
                <div class="row">
                    <section class="col col-12">
                        <label class="label">尚未繳納自主訓練費用學員如下：</label>
                        <label class="radio">
                            <input type="radio" name="searchNameRadio">
                            <i></i>劉加菲（2017/5/30 PM13:00~14:00）
                        </label>
                        <label class="radio">
                            <input type="radio" name="searchNameRadio">
                            <i></i>宋小胖（2017/7/30 PM13:00~14:00）
                        </label>
                        <label class="radio">
                            <input type="radio" name="searchNameRadio" disabled="disabled">
                            <i></i>陳小瓦（2017/8/31 PM13:00~14:00）                       
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset id="other" style="display: none">
                <section>
                    <label class="label">項目</label>
                    <label class="select">
                        <select>
                            <option value="1">椰子水50</option>
                            <option value="2">礦泉水</option>
                            <option value="3">礦泉水</option>
                            <option value="4">氣泡水</option>
                        </select>
                        <i class="icon-append fa fa-file-word-o"></i>
                    </label>
                </section>
            </fieldset>
            <fieldset id="contract">
                <section>
                    <label class="label">合約編號</label>
                    <label class="input">
                        <i class="icon-append fa fa-file-text-o"></i>
                        <input type="text" name="contractno" maxlength="20" placeholder="請輸入合約編號">
                    </label>
                </section>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">收款日期</label>
                        <label class="input input-group date form_date" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
                            <i class="icon-append fa fa-calendar"></i>
                            <input type="text" name="birthday" class="form-control" placeholder="請選擇收款日期" readonly="readonly">
                        </label>
                    </section>
                    <section class="col col-4">
                        <label class="label">發票類型</label>
                        <div class="inline-group">
                            <label class="radio">
                                <input type="radio" name="AthleticLevel" value="1" />
                                <i></i>紙本</label>
                            <label class="radio">
                                <input type="radio" name="AthleticLevel" value="0" />
                                <i></i>電子</label>
                        </div>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">收款方式</label>
                        <label class="select">
                            <select>
                                <option value="1">現金</option>
                                <option value="2">刷卡</option>
                                <option value="3">轉帳</option>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">收款金額</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="name" maxlength="10" placeholder="請輸入收款金額">
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">買受人統編</label>
                        <label class="input">
                            <i class="icon-append fa fa-user"></i>
                            <input type="number" name="name" maxlength="10" placeholder="請輸入買受人統編">
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">發票號碼</label>
                        <label class="input">
                            <i class="icon-append fa fa-qrcode"></i>
                            <input type="text" name="name" maxlength="20" placeholder="請輸入發票號碼">
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <section>
                    <label class="label">備註</label>
                    <textarea class="form-control" placeholder="請輸入備註" rows="3"></textarea>
                </section>
            </fieldset>
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            autoOpen: false,
            width: "auto",
            resizable: true,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-edit'></i>  編輯收款</h4></div>",
            buttons: [{
                html: "<i class='fa fa-check-square-o'></i>&nbsp; 送交審核",
                "class": "btn bg-color-red",
                click: function () {
                    if (confirm("請再次確認收款資料正確")) {
                        $(this).dialog("close");
                    }
                }
            }, {
                html: "<i class='fa fa-qrcode'></i>&nbsp; 確定產生發票",
                "class": "btn btn-primary",
                click: function () {
                    if (confirm("請再次確認收款資料正確")) {
                        $(this).dialog("close");
                        window.location.href = "invoicelist.html";
                    }
                }
            }],
            close: function () {
                $('#<%= _dialog %>').remove();
            }
        });

    </script>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _dialog = "payment" + DateTime.Now.Ticks;
    PaymentViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (PaymentViewModel)ViewBag.ViewModel;
    }

</script>

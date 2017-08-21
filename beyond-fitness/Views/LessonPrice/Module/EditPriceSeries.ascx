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

<div id="<%= _dialog %>" title="編輯標準價目表" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white no-padding">
        <form action="<%= Url.Action("CommitPriceSeries","LessonPrice",new { _viewModel.SeriesID }) %>" class="smart-form" method="post" autofocus>
            <fieldset>
                <div class="row">
                    <section class="col col-4">
                        <label class="label">分店</label>
                        <label class="select">
                            <select name="BranchID">
                                <%  Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.ascx", model: _viewModel.BranchID ?? -1); %>
                            </select>
                            <i class="icon-append fa fa-at"></i>
                        </label>
                        <script>
                            $('#<%= _dialog %> select[name="BranchID"]').val('<%= _viewModel.BranchID %>')
                        </script>
                    </section>
                    <section class="col col-4">
                        <label class="label">請選擇價格適用年份</label>
                        <label class="select">
                            <select class="input" name="Year">
                                <%  for (int year = DateTime.Today.Year; year < DateTime.Today.Year + 5; year++)
                                    { %>
                                <option><%= year %></option>
                                <%  } %>
                            </select>
                            <i class="icon-append fa fa-clock-o"></i>
                        </label>
                        <script>
                            $('#<%= _dialog %> select[name="Year"]').val('<%= _viewModel.Year %>')
                        </script>
                    </section>
                    <section class="col col-4">
                        <label class="label">狀態</label>
                        <label class="select">
                            <select name="Status">
                                <option value="0">已停用</option>
                                <option value="1">已啟用</option>
                            </select>
                            <i class="icon-append fa fa-clock-o"></i>
                        </label>
                        <script>
                            $('#<%= _dialog %> select[name="Status"]').val('<%= _viewModel.Status %>')
                        </script>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">一般學員單堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[0] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[0] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">一般學員單堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[1] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[1] %>" />
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">舊學員單堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[2] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[2] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">舊學員單堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[3] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[3] %>" />
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">一般學員25堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[4] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[4] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">一般學員25堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[5] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[5] %>" />
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">舊學員25堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[6] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[6] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">舊學員25堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[7] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[7] %>" />
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">一般學員50堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[8] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[8] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">一般學員50堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[9] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[9] %>" />
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">舊學員50堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[10] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[10] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">舊學員50堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[11] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[11] %>" />
                        </label>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">一般學員75堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[12] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[12] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">一般學員75堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[13] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[13] %>" />
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">舊學員75堂 / 時間長度：60分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[14] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[14] %>" />
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">舊學員75堂 / 時間長度：90分鐘</label>
                        <label class="input">
                            <i class="icon-append fa fa-usd"></i>
                            <input type="number" name="ListPriceSeries" maxlength="20" placeholder="請輸入價格" value="<%= _viewModel.ListPriceSeries[15] %>" />
                            <input type="hidden" name="PriceSeriesID" value="<%= _viewModel.PriceSeriesID[15] %>" />
                        </label>
                    </section>
                </div>
            </fieldset>
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            width: "auto",
            resizable: true,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-edit'></i>  編輯標準價目表</h4></div>",
            buttons: [{
                html: "<i class='fa fa-trash-o' aria-hidden='true'></i>&nbsp; 刪除",
                "class": "btn bg-color-red",
                click: function () {

                    if (confirm('確定刪除此標準價目?')) {
                        startLoading();
                        $.post('<%= Url.Action("DeletePriceSeries","LessonPrice",new { SeriesID = _viewModel.SeriesID }) %>', { }, function (data) {
                            hideLoading();
                            if (data.result) {
                                if (data.message) {
                                    alert(data.message);
                                } else {
                                    alert('價目已刪除!!');
                                }
                                $('#<%= _dialog %>').dialog('close');
                                $global.renderPriceSeries();
                            } else {
                                alert(data.message);
                            }
                        });
                    }

                }
            },
            {
                html: "<i class='fa fa-send'></i>&nbsp; 確定",
                "class": "btn btn-primary",
                click: function () {
                    var $form = $('#<%= _dialog %> form');
                    clearErrors();
                    $form.ajaxSubmit({
                        beforeSubmit: function () {
                            showLoading();
                        },
                        success: function (data) {
                            hideLoading();
                            if ($.isPlainObject(data)) {
                                if (data.result) {
                                    alert('資料已儲存!!');
                                    $('#<%= _dialog %>').dialog('close');
                                    $global.renderPriceSeries();
                                } else {
                                    alert(data.message);
                                }
                            } else {
                                $(data).appendTo($('body')).remove();
                            }
                        }
                    });
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
    String _dialog = "priceSeries" + DateTime.Now.Ticks;
    LessonPriceQueryViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (LessonPriceQueryViewModel)ViewBag.ViewModel;
    }

</script>

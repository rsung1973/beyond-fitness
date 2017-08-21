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

<div id="<%= _dialog %>" title="學員服務申請" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white no-padding">
        <form action="<%= Url.Action("CommitAmendment","CourseContract",new { _model.ContractID }) %>" class="smart-form" method="post" autofocus>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">申請項目</label>
                        <label class="select" id="applyContractItem">
                            <select name="Reason">
                                <option value="">請選擇</option>
                                <option value="展延">展延</option>
                                <%  if (_viewModel.ContractType == 1)
                                    { %>
                                <option value="轉讓">轉讓</option>
                                <%  } %>
                                <option value="轉點">轉點</option>
                                <option value="終止">終止</option>
                                <option value="其他">其他</option>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                            <script>
                                $(function(){
                                    $('select[name="Reason"]').val('<%= _viewModel.Reason %>');
                                });

                                $('select[name="Reason"]').on('change', function (evt) {
                                    var reason = $(this).val();
                                    var allChange = <%= _viewModel.Reason== "展延" ||_viewModel.Reason== "轉讓" ||_viewModel.Reason== "轉點" ? "true" : "false" %>;
                                    if (reason == "展延" || reason == "轉讓" || reason == "轉點" || allChange) {
                                        showLoading();
                                        $.post('<%= Url.Action("AmendContract","CourseContract",new { _model.ContractID }) %>',{'reason':reason},function(data){
                                            hideLoading();
                                            if ($.isPlainObject(data)) {
                                                alert(data.message);
                                            } else {
                                                $(data).appendTo($('body'));
                                                $('#<%= _dialog %>').dialog('close');
                                            }
                                        });
                                    }
                                });
                            </script>
                        </label>
                    </section>
                    <%  if (_viewModel.Reason == "展延")
                        {
                            Html.RenderPartial("~/Views/CourseContract/Module/ApplyAmendmentForExtension.ascx");
                        } %>
                </div>
                <%  if (_viewModel.Reason == "轉點")
                    {
                        Html.RenderPartial("~/Views/CourseContract/Module/ApplyAmendmentForMigration.ascx");
                    } %>
            </fieldset>
            <%  if (_viewModel.Reason == "轉讓")
                    {
                        Html.RenderPartial("~/Views/CourseContract/Module/ApplyAmendmentForTransference.ascx");
                    } %>
            <fieldset>
                <section>
                    <%  if (_viewModel.Reason == "展延")
                        {   %>
                    <label class="label label-info">原合約編號 <%= _model.ContractNo %> 使用截止日為 <%= String.Format("{0:yyyy/MM/dd}",_model.Expiration) %> 展延至 <span id="expiration"><%= String.Format("{0:yyyy/MM/dd}",_model.Expiration.Value.AddMonths(3)) %></span> 止。</label>
                    <%  }
                        else if (_viewModel.Reason == "轉點")
                        {   %>
                    <label class="label label-info">原合約編號 <%= _model.ContractNo %> 上課地點為 <%= _model.LessonPriceType.BranchStore.BranchName %> 轉點至 <span id="location"></span>。</label>
                    <%  }
                        else if (_viewModel.Reason == "轉讓")
                        {   %>
                    <label class="label label-info">原合約編號 <%= _model.ContractNo %> 剩餘上課堂數：<%= _model.RegisterLessonContract.Count>0 ? _model.RegisterLessonContract.First().RegisterLesson.RemainedLessonCount() : _model.Lessons %>堂<span id="transference"></span>。</label>
                    <%  } %>
                    <label class="label">備註</label>
                    <textarea class="form-control" placeholder="請輸入備註" rows="3" name="Remark"></textarea>
                </section>
            </fieldset>
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            width: "600",
            resizable: false,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-cogs'></i> 學員服務申請</h4></div>",
            buttons: [<%--{
                html: "<i class='fa fa-floppy-o'></i>&nbsp; 暫存",
                "class": "btn bg-color-darken",
                click: function () {
                    $(this).dialog("close");
                }
            },--%> {
                html: "<i class='fa fa-check-square-o'></i>&nbsp; 送交審核",
                "class": "btn bg-color-red",
                click: function () {
                    if (confirm("請再次確認合約內容資料正確")) {
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
                                        alert('服務申請已送交審核!!');
                                        $('#<%= _dialog %>').dialog('close');
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
    CourseContractViewModel _viewModel;
    String _dialog = "amendment" + DateTime.Now.Ticks;
    CourseContract _model;
    bool _useLearnerDiscount;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (CourseContract)this.Model;
        _useLearnerDiscount = models.CheckLearnerDiscount(_model.CourseContractMember.Select(m => m.UID));
        _viewModel = (CourseContractViewModel)ViewBag.ViewModel;
    }

</script>

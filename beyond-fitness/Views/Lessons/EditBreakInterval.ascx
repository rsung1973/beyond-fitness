<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                &times;
            </button>
            <h4 class="modal-title" id="myModalLabel"><%= ViewBag.Edit == true ? "修改" : "新增" %>休息時間與總組數</h4>
        </div>
        <form action="<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitTrainingBreakInterval") %>" method="post">
            <input type="hidden" name="executionID" value="<%= _model.ExecutionID %>" />
            <input type="hidden" name="itemID" value="<%= _viewModel.ItemID %>" />
            <div class="modal-body bg-color-darken txt-color-white">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="input-group input-group-lg">
                                <span class="input-group-addon">休息時間</span>
                                <div class="icon-addon addon-lg">
                                    <input type="text" placeholder="" class="form-control" name="breakInterval" value="<%= _viewModel.BreakInterval %>"/>
                                </div>
                                <span class="input-group-addon">秒</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="input-group input-group-lg">
                                <span class="input-group-addon">總</span>
                                <div class="icon-addon addon-lg">
                                    <input type="text" placeholder="" class="form-control" name="repeats" value="<%= _viewModel.Repeats %>">
                                </div>
                                <span class="input-group-addon">總數</span>
                            </div>
                            <p class="note"><strong>Note:</strong> 輸入15或10-12</p>
                        </div>
                    </div>
                </div>
                <fieldset>
                    <div class="form-group">
                        <textarea class="form-control" placeholder="請輸入評論" rows="3" name="remark"><%= _viewModel.Remark %></textarea>
                        <p class="note"><strong>Note:</strong> 最多輸入50個中英文字</p>
                    </div>
                </fieldset>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">
                    取消
                </button>
                <button type="button" class="btn btn-primary" onclick="commitTrainingItem();">
                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                </button>
            </div>
        </form>
    </div>
    <!-- /.modal-content -->
</div>

<script>

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    TrainingExecution _model;
    TrainingItemViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (TrainingExecution)this.Model;
        _viewModel = (TrainingItemViewModel)ViewBag.ViewModel;
    }

</script>

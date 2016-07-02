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
    <div class="modal-content bg_gray">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title orange-text" id="confirmLabel"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>新增項目</h4>
        </div>
        <div class="modal-body">
            <div class="form-group">
                <label for="exampleInputFile" class="col-md-3 control-label">肌力訓練：</label>
                <div class="col-md-9">
                    <% Html.RenderPartial("~/Views/Lessons/BodyPartsSelector.ascx"); %>
                    <input type="text" class="form-control" name="description"/>
                </div>

            </div>

            <div class="form-group">
                <label for="goalTurns" class="col-md-3 control-label">目標次數：</label>
                <div class="col-md-9">
                    <% Html.RenderPartial("~/Views/Lessons/GoalTurnsSelector.ascx", 0); %>
                </div>
            </div>
            <div class="form-group">
                <label for="goalStrength" class="col-md-3 control-label">目標強度：</label>
                <div class="col-md-9">
                    <input type="text" class="form-control" name="goalStrength" value=""/>
                    <span>例：20KG或30秒</span>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnAddTraining" type="submit" class="btn btn-system btn-sm"><span class="fa fa fa-thumbs-o-up" aria-hidden="true"></span>確定</button>
                <button type="button" class="btn btn-default btn-sm" data-dismiss="modal"><span class="fa fa-times" aria-hidden="true"></span>取消</button>
            </div>
        </div>
    </div>
</div>

<script>
    $('#btnAddTraining').on('click', function (evt) {
        //$addTrainingModal.find('form').submit();
        $addTrainingModal.modal('hide');
    });
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="<%= _viewClassDialog %>" title="檢視上課內容" class="bg-color-darken">
    <div class="panel panel-default bg-color-darken">
        <div class="row">
            <div class="col-sm-12">
                <div class="padding-10">
                    <ul class="nav nav-tabs tabs-pull-right">
                        <li class="active">
                            <a data-toggle="tab" href="#os3_<%= _ticks %>"><i class="fa fa-heartbeat"></i><span>訓練內容</span></a>
                        </li>
                        <%  if (_model.TrainingBySelf != 1)
                            { %>
                        <li>
                            <a data-toggle="tab" href="#os5_<%= _ticks %>"><i class="fa fa-pie-chart"></i><span>評量指數</span></a>
                        </li>
                        <%  } %>
                    </ul>
                    <div class="tab-content padding-top-10">
                        <div class="tab-pane fade widget-body no-padding-bottom active in" id="os3_<%= _ticks %>">
                            <%  
//if (ViewBag.Edit == true)
//{
//    Html.RenderPartial("~/Views/Lessons/Feedback/CommonFeedback.ascx", _model);
//}
                            %>
                            <%  if (_model.TrainingPlan.Count > 0)
                                {
                                    ViewBag.ShowOnly = true;
                                    ViewBag.DataTableId = "itemList" + _ticks;
                                    Html.RenderPartial("~/Views/Lessons/SingleTrainingExecutionPlan.ascx", _model.TrainingPlan.First().TrainingExecution);
                                }
                                //if (ViewBag.Learner == true)
                                //    Html.RenderPartial("~/Views/Activity/LessonFeedBack.ascx", _model);
                                //else 
                                //    Html.RenderPartial("~/Views/Activity/ShowLessonFeedBack.ascx", _model); 
                                %>
                        </div>
                        <%  if (_model.TrainingBySelf != 1)
                            { %>
                        <div class="tab-pane fade widget-body no-padding-bottom" id="os5_<%= _ticks %>">
                            <%  ViewBag.ShowOnly = true;
                            ViewBag.Index = DateTime.Now.Ticks;
                            Html.RenderPartial("~/Views/Lessons/LessonAssessment.ascx", _model); %>
                        </div>
                        <%  } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $('#<%= _viewClassDialog %>').dialog({
            //autoOpen: false,
            resizable: true,
            modal: true,
            width: "100%",
            height: "auto",
            title: "<h4 class='modal-title'><i class='fa fa-fw fa-eye'></i>  檢視上課內容</h4>",
            close: function (event, ui) {
                $('#healthlistDialog').remove();
            }
        });

    </script>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    long _ticks;
    String _viewClassDialog;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        _ticks = DateTime.Now.Ticks;
        _viewClassDialog = "viewClassDialog" + _ticks;
    }

</script>

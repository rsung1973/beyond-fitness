<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<header>
    <span class="widget-icon"><i class="fa fa-rss text-success"></i></span>
    <h2><%= String.Format("{0:yyyy/MM/dd HH:mm}",_model.ClassTime) %>-<%= String.Format("{0:HH:mm}",_model.ClassTime.Value.AddMinutes(_model.DurationInMinutes.Value)) %> 課程表 </h2>
    <%  if (ViewBag.Edit == true)
        { %>
    <div class="widget-toolbar">
        <a onclick="makeTrainingPlan(<%= _model.LessonID %>);" class="btn btn-primary"><i class="fa fa-fw fa-files-o"></i>編輯課表</a>
    </div>
    <%  } %>
    <ul id="widget-tab-1" class="nav nav-tabs pull-right">
        <li>
            <a data-toggle="tab" href="#os1_<%= _ticks %>"><i class="fa fa-commenting-o"></i><span>有話想說</span></a>
        </li>
        <li>
            <a data-toggle="tab" href="#os2_<%= _ticks %>"><i class="fa fa-child "></i><span>暖身</span></a>
        </li>
        <li class="active">
            <a data-toggle="tab" href="#os3_<%= _ticks %>"><i class="fa fa-heartbeat"></i><span>訓練內容</span></a>
        </li>
        <li>
            <a data-toggle="tab" href="#os4_<%= _ticks %>"><i class="fa fa-child"></i><span>收操</span></a>
        </li>
        <%  if (_model.TrainingBySelf != 1)
            { %>
        <li>
            <a data-toggle="tab" href="#os5_<%= _ticks %>"><i class="fa fa-pie-chart"></i><span>分析圖</span></a>
        </li>
        <%  } %>
    </ul>

</header>
<!-- widget div-->
<div class="no-padding">
    <div class="widget-body no-padding">
        <!-- content -->
        <div id="myTabContent" class="tab-content padding-10">
            <div class="tab-pane fade widget-body no-padding-bottom" id="os1_<%= _ticks %>">
                <div class="chat-body no-padding profile-message">
                    <ul>
                        <li class="message">
                            <% _model.AsAttendingCoach.UserProfile.RenderUserPicture(Writer,new { @class = "profileImg online" }); %>
                            <span class="message-text">
                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.AttendingCoach %>"><%= _model.AsAttendingCoach.UserProfile.RealName %></a>
                                <%= _model.LessonPlan!=null ? _model.LessonPlan.Remark : null %>
                            </span>
                        </li>

                        <li class="message message-reply">
                            <% _model.RegisterLesson.UserProfile.RenderUserPicture(Writer, new { @class = "authorImg online" }); %>
                            <span class="message-text">
                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.RegisterLesson.UID %>"><%= _model.RegisterLesson.UserProfile.UserName ?? _model.RegisterLesson.UserProfile.RealName %></a>
                                <%= _model.LessonPlan!=null ? _model.LessonPlan.FeedBack : null %>
                            </span>

                            <ul class="list-inline font-xs">
                                <li>
                                    <a href="javascript:void(0);" class="text-muted"><%= String.Format("{0:yyyy/MM/dd HH:mm}",_model.LessonPlan.FeedBackDate) %></a>
                                </li>
                            </ul>
                        </li>

                    </ul>

                </div>
            </div>
            <!-- end s1 tab pane -->
            <div class="tab-pane fade widget-body no-padding-bottom" id="os2_<%= _ticks %>">
                <div class="chat-body no-padding profile-message">
                    <ul>
                        <li class="message">
                            <% _model.AsAttendingCoach.UserProfile.RenderUserPicture(Writer,new { @class = "profileImg online" }); %>
                            <span class="message-text">
                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.AttendingCoach %>"><%= _model.AsAttendingCoach.UserProfile.RealName %></a>
                                <%= _model.LessonPlan!=null ? _model.LessonPlan.Warming : null %>
                            </span>
                        </li>
                        <li></li>
                    </ul>
                </div>
            </div>
            <!-- end s2 tab pane -->
            <div class="tab-pane fade widget-body no-padding-bottom active in" id="os3_<%= _ticks %>">
                <%  if (_model.TrainingPlan.Count > 0)
                    {
                        ViewBag.ShowOnly = true;
                        ViewBag.DataTableId = "itemList" + _ticks;
                        Html.RenderPartial("~/Views/Lessons/SingleTrainingExecutionPlan.ascx", _model.TrainingPlan.First().TrainingExecution);
                    } %>
            </div>
            <!-- end s3 tab pane -->
            <div class="tab-pane fade widget-body no-padding-bottom" id="os4_<%= _ticks %>">
                <div class="chat-body no-padding profile-message">
                    <ul>
                        <li class="message">
                            <% _model.AsAttendingCoach.UserProfile.RenderUserPicture(Writer,new { @class = "profileImg online" }); %>
                            <span class="message-text">
                                <a class="username" href="<%= VirtualPathUtility.ToAbsolute("~/Account/ViewProfile/") + _model.AttendingCoach %>"><%= _model.AsAttendingCoach.UserProfile.RealName %></a>
                                <%= _model.LessonPlan!=null ? _model.LessonPlan.EndingOperation : null %>
                            </span>
                        </li>
                        <li></li>
                        <li></li>
                    </ul>
                </div>
            </div>
            <!-- end s4 tab pane -->
            <%  if (_model.TrainingBySelf != 1)
                { %>
            <div class="tab-pane fade widget-body no-padding-bottom" id="os5_<%= _ticks %>">
                <div class="row">
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <%  Html.RenderPartial("~/Views/Lessons/DailyTrendPieView.ascx", _model); %>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <%  Html.RenderPartial("~/Views/Lessons/DailyFitnessPieView.ascx", _model); %>
                    </div>
                </div>
            </div>
            <%  } %>
        </div>
        <!-- end s5 tab pane -->
    </div>

    <!-- end content -->
</div>

<% Html.RenderPartial("~/Views/Shared/EasyPieView.ascx"); %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    long _ticks;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        _ticks = DateTime.Now.Ticks;
    }

</script>

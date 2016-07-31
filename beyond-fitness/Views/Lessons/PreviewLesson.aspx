<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="pageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <%  if (_model.LessonTime.GroupID.HasValue)
                        {
                            Html.RenderPartial("~/Views/Member/GroupingLessonInfo.ascx", _model.LessonTime.GroupingLesson);
                        }
                        else
                        {
                            ViewBag.ShowPerson = true; ViewBag.Argument = new ArgumentModel { Model = _model.LessonTime, PartialViewName = "~/Views/Lessons/LessonGoal.ascx" };
                            Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model.LessonTime.RegisterLesson.UserProfile);
                        }   %>                    <!-- End Classic -->

                    <!-- Start Contact Form -->
                    <!-- Categories Widget -->
<%--                    <div class="widget widget-categories">
                        <ul>
                            <li>
                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/EditVip/") + _profile.UID %>"><i class="fa fa-cog" aria-hidden="true"></i>修改個人資料</a>
                            </li>
                        </ul>
                    </div>--%>
                    <!-- End Contact Form -->

                    <!-- Classic Heading -->

                    <!-- Start Contact Form -->

                    <div class="row">
                        <div class="col-md-12">
                            <h4><span class="glyphicon glyphicon-th-list" aria-hidden="true"></span><%= _model.LessonTime.ClassTime.Value.ToString("yyyy/M/d HH:mm") %>~<%= _model.LessonTime.ClassTime.Value.AddMinutes(_model.LessonTime.DurationInMinutes.Value).ToString("HH:mm") %> 課程內容 - <%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></h4>
                            <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <table class="table">
                                        <tr class="info">
                                            <th>暖身</th>
                                        </tr>
                                        <tr>
                                            <td><pre><%= _model.LessonTime.LessonPlan!=null ? _model.LessonTime.LessonPlan.Warming : null %></pre></td>
                                        </tr>
                                    </table>
                                    <table class="table">
                                        <tr class="info">
                                            <th rowspan="2" width="25"></th>
                                            <th rowspan="2" class="col-xs-4 col-md-3">肌力訓練</th>
                                            <th class="col-xs-2 col-md-2 text-center">實際次數</th>
                                            <th class="col-xs-2 col-md-2 text-center">實際強度</th>
                                            <th rowspan="2" class="col-xs-3 col-md-4">備註</th>
                                            <th rowspan="2" class="col-xs-1 col-md-1 text-center">組數</th>
                                        </tr>
                                        <tr class="info">
                                            <th class="text-center">目標次數</th>
                                            <th class="text-center">目標強度</th>
                                        </tr>
                                        <tr>
                                        <%      if (_model.LessonTime.TrainingPlan.Count > 0)
                                    {
                                        int idx = 0;
                                        foreach (var item in _model.LessonTime.TrainingPlan)
                                        {
                                            idx++;
                                            var execution = item.TrainingExecution;
                                            var training = execution.TrainingItem;
                                            if (training.Count > 0)
                                            {
                                                for (int i = 0; i < training.Count; i++)
                                                {
                                                    var tranItem = training[i];
                                                    if (i == 0)
                                                    {%>
                                        <tr>
                                            <td rowspan="<%= training.Count() + 3 %>" class="text-center"><%= idx %>
                                            </td>
                                            <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                            <td class="text-center">
                                                <%= !String.IsNullOrEmpty(tranItem.ActualTurns) ? tranItem.ActualTurns : "--" %>次<br />
                                                <%= !String.IsNullOrEmpty(tranItem.GoalTurns) ? tranItem.GoalTurns : "--" %>次
                                            </td>
                                            <td class="text-center">
                                                <%= !String.IsNullOrEmpty(tranItem.ActualStrength) ? tranItem.GoalStrength : "--" %><br />
                                                <%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %>
                                            </td>
                                            <td><%= tranItem.Remark %></td>
                                            <td rowspan="<%= training.Count() %>" class="text-center"><%= item.TrainingExecution.Repeats %></td>
                                        </tr>
                                        <%                  }
                                    else
                                    {   %>
                                        <tr>
                                            <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                            <td class="text-center">
                                                <%= !String.IsNullOrEmpty(tranItem.ActualTurns) ? tranItem.ActualTurns : "--" %>次<br />
                                                <%= !String.IsNullOrEmpty(tranItem.GoalTurns) ? tranItem.GoalTurns : "--" %>次
                                            </td>
                                            <td class="text-center">
                                                <%= !String.IsNullOrEmpty(tranItem.ActualStrength) ? tranItem.ActualStrength : "--" %><br />
                                                <%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %>
                                            </td>
                                            <td><%= tranItem.Remark %></td>
                                        </tr>
                                        <%                  }
                                    }   %>
                                        <tr>
                                            <td colspan="5"><strong>休息時間：</strong><%= execution.BreakIntervalInSecond %>秒</td>
                                        </tr>
                                        <tr class="active">
                                            <td colspan="5">
                                                <li class="glyphicon glyphicon-info-sign"></li>
                                                <strong>小提示：</strong><%= execution.Conclusion %></td>
                                        </tr>
                                        <tr class="active">
                                            <td colspan="5">
                                                <li class="fa fa-commenting-o"></li>
                                                <strong>迴響：</strong><%= execution.ExecutionFeedBack %></td>
                                        </tr>                                        
                                        <%              }
                                    else
                                    {   %>
                                        <tr>
                                            <td rowspan="3" class="text-center"><%= idx %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="5"><strong>休息時間：</strong><%= execution.BreakIntervalInSecond %>秒</td>
                                        </tr>
                                        <tr class="active">
                                            <td colspan="5">
                                                <li class="glyphicon glyphicon-info-sign"></li>
                                                <strong>小提示：</strong><%= execution.Conclusion %></td>
                                        </tr>
                                        <tr class="active">
                                            <td colspan="5">
                                                <li class="fa fa-commenting-o"></li>
                                                <strong>迴響：</strong><%= execution.ExecutionFeedBack %></td>
                                        </tr>
                                        <%              }
                                        }
                                    }
                                    else
                                    {   %>
                                        <tr>
                                            <td colspan="6">未建立任何項目</td>
                                        </tr>
                                        <%  }%>
                                    </table>
                                    <table class="table">
                                        <tr class="info">
                                            <th>收操</th>
                                        </tr>
                                        <tr>
                                            <td><pre><%= _model.LessonTime.LessonPlan!=null ?_model.LessonTime.LessonPlan.EndingOperation : null %></pre></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <h4><span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>著重方向：</h4>
                            <%  Html.RenderPartial("~/Views/Lessons/DailyTrendPieView.ascx", _model); %>
                        </div>


                        <div class="col-md-6">
                            <h4><span class="glyphicon glyphicon-heart-empty" aria-hidden="true"></span>體適能：</h4>
                            <%  Html.RenderPartial("~/Views/Lessons/DailyFitnessPieView.ascx", _model); %>
                        </div>

                    </div>
                    <%  if (_model.LessonTime.LessonPlan != null)
                        { %>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="hr1" style="margin-top: 20px; margin-bottom: 5px;"></div>
                                <h4 ><span class="fa fa-commenting" aria-hidden="true"></span> 教練總評：</h4>
                                <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                                <!-- Start Call Action -->
                                <pre class="call-action call-action-boxed call-action-style4 clearfix"><%= String.IsNullOrEmpty(_model.LessonTime.LessonPlan.Remark) ? "目前尚無總評" : _model.LessonTime.LessonPlan.Remark %></pre>
                                <!-- End Call Action -->
                                <div class="hr1" style="margin-top: 10px; margin-bottom: 5px;"></div>
                            </div>

                            <div class="col-md-6">
                                <div class="hr1" style="margin-top: 20px; margin-bottom: 5px;"></div>
                                <h4 ><span class="fa fa-comments-o" aria-hidden="true"></span> 學員意見反饋：</h4>
                                <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                                <!-- Start Call Action -->
                                <pre class="call-action call-action-boxed call-action-style4 clearfix"><%= String.IsNullOrEmpty(_model.LessonTime.LessonPlan.FeedBack) ? "目前尚無意見反饋" : _model.LessonTime.LessonPlan.FeedBack %></pre>
                                <!-- End Call Action -->
                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>" class="btn-system btn-medium">回行事曆清單 <i class="fa fa-calendar" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    <%  } %>
                    
                </div>


                <!-- End Contact Form -->

            </div>

        </div>
    </div>
    </div>
    <!-- End content -->
    
    <% Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>
    <% Html.RenderPartial("~/Views/Shared/PieView.ascx"); %>

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonTimeExpansion _model;
    UserProfile _profile;
    

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonTimeExpansion)this.Model;
        _profile = _model.LessonTime.RegisterLesson.UserProfile;
    }

</script>

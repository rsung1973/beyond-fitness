<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-12">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-edit"> 預編課程</span></h4>

                    <!-- Start Contact Form -->

                    <%  if (_model.LessonTime.GroupID.HasValue)
                        {
                            Html.RenderPartial("~/Views/Member/GroupingLessonInfo.ascx", _model.LessonTime.GroupingLesson);
                        }
                        else
                        {
                            ViewBag.ShowPerson = true; ViewBag.Argument = new ArgumentModel { Model = _model.LessonTime, PartialViewName = "~/Views/Lessons/LessonGoal.ascx" };
                            Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model.LessonTime.RegisterLesson.UserProfile);
                        }   %>
                    <div class="col-md-10">
                        <h4><span class="glyphicon glyphicon-th-list" aria-hidden="true"></span><%= _model.LessonTime.ClassTime.Value.ToString("yyyy/M/d HH:mm") %>~<%= _model.LessonTime.ClassTime.Value.AddMinutes(_model.LessonTime.DurationInMinutes.Value).ToString("HH:mm") %> 課程內容 - <%= _model.LessonTime.AsAttendingCoach.UserProfile.RealName %></h4>
                    </div>
                    <div class="col-md-2 text-right">
                        <a class="btn-system btn-small" onclick="addTraining();">新增項目組 <i class="fa fa-cart-plus" aria-hidden="true"></i></a>
                    </div>
                        <div class="hr1" style="margin-bottom: 10px;"></div>
                        <div class="panel panel-default">
                            <table class="table">
                                <tr class="info">
                                    <th>暖身</th>
                                </tr>
                                <tr>
                                    <td><textarea name="warming" class="form-control" rows="5"><%= _plan.Warming %></textarea>
                                        </td>
                                </tr>
                            </table>
                            <table class="table">
                                <tr class="info">
                                    <th class="text-center" width="25"></th>
                                    <th class="col-xs-4 col-md-3">肌力訓練</th>
                                    <th class="col-xs-2 col-md-2 text-center">目標次數</th>
                                    <th class="col-xs-2 col-md-2 text-center">目標強度</th>
                                    <th class="col-xs-3 col-md-4">備註</th>
                                    <th class="col-xs-1 col-md-1 text-center">組數</th>
                                </tr>
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
                                    <td class="text-center" rowspan="<%= training.Count() + 1 %>" class="text-center"><%= idx %>
                                        <br />
                                        <a onclick="deleteTraining(<%= execution.ExecutionID %>);" class="red-text fa fa-minus-circle fa-2x"></a>
                                    </td>
                                    <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                    <td class="text-center"><%= !String.IsNullOrEmpty(tranItem.GoalTurns) ? tranItem.GoalTurns : "--" %></td>
                                    <td class="text-center"><%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %></td>
                                    <td><%= tranItem.Remark %></td>
                                    <td rowspan="<%= training.Count() + 1 %>" class="text-center">
                                        <%= item.TrainingExecution.Repeats %>
                                        <a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/EditTraining/") + execution.ExecutionID %>">修改 <i class="fa fa-edit" aria-hidden="true"></i></a>
                                    </td>
                                </tr>
                                <%                  }
                                    else
                                    {   %>
                                <tr>
                                    <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                    <td class="text-center"><%= !String.IsNullOrEmpty(tranItem.GoalTurns) ? tranItem.GoalTurns : "--" %></td>
                                    <td class="text-center"><%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %></td>
                                    <td><%= tranItem.Remark %></td>
                                </tr>
                                <%                  }
                                    }   %>
                                <tr class="active">
                                    <td colspan="4"><strong>休息時間：</strong><%= execution.BreakIntervalInSecond %>秒</td>
                                </tr>
                                <%              }
                                    else
                                    {   %>
                                <tr>
                                    <td class="text-center"><%= idx %><br />
                                        <a onclick="deleteTraining(<%= execution.ExecutionID %>);" class="red-text fa fa-minus-circle fa-2x"></a>
                                    </td>
                                    <td colspan="4"><strong>休息時間：</strong><%= execution.BreakIntervalInSecond %>秒</td>
                                    <td>
                                        <a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/EditTraining/") + execution.ExecutionID %>">修改 <i class="fa fa-edit" aria-hidden="true"></i></a>
                                    </td>
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
                                    <td><textarea class="form-control" name="endingOperation" rows="5"><%= _plan.EndingOperation %></textarea></td>
                                </tr>
                            </table>
                        </div>

                        <div class="hr2" style="margin-bottom: 10px;"></div>
                        <h4 class="classic-title"><span class="fa fa-commenting" aria-hidden="true">教練總評：</span></h4>
                        <textarea class="form-control" name="remark" rows="5"><%= _plan.Remark %></textarea>

                        <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/CompletePlan") %>">回行事曆清單 <i class="fa fa-calendar" aria-hidden="true"></i></a>
                        <a id="nextStep" class="btn-system btn-medium">更新課表 <i class="fa fa-refresh" aria-hidden="true"></i></a>
                        <a onclick="deletePlan();" class="btn-system btn-medium">刪除課表 <i class="fa fa-trash-o" aria-hidden="true"></i></a>


                        <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>

                        <!-- End Contact Form -->

                </div>
            </div>
        </div>
    </div>

    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <% Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>

    <script>
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitPlan") %>')
          .submit();
        });

        function deleteTraining(itemID) {
            confirmIt({ title: '刪除訓練組數', message: '確定刪除此訓練組數?' }, function (evt) {
                startLoading();
                $('<form method="post"/>').appendTo($('body'))
                    .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeleteTraining/") %>' + itemID)
                    .submit();
            });
        }

        function deletePlan() {
            confirmIt({ title: '刪除課表', message: '確定刪除此課表?' }, function (evt) {
                startLoading();
                $('<form method="post"/>').appendTo($('body'))
                    .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeletePlan") %>')
                    .submit();
            });
        }

        function addTraining() {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/AddTraining") %>')
                .submit();
        }

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonTimeExpansion _model;
    LessonPlan _plan;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonTimeExpansion)this.Model;
        _plan = _model.LessonTime.LessonPlan ?? new LessonPlan { };
    }



</script>

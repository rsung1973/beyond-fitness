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
                    <h4 class="classic-title"><span class="fa fa-edit">預編課程</span></h4>

                    <!-- Start Contact Form -->

                    <div class="blog-post quote-post">

                        <%  ViewBag.Argument = new ArgumentModel { Model = _model.LessonTime, PartialViewName = "~/Views/Lessons/LessonGoal.ascx" };
                            Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model.LessonTime.RegisterLesson.UserProfile); %>

                        <div class="hr2" style="margin-bottom: 10px;"></div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno"><strong>暖身：</strong></label>
                        </div>
                        <textarea name="warming" class="form-control" rows="5"><%= _plan.Warming %></textarea>
                        <div>
                            <a class="btn-system btn-small" onclick="addTraining();">新增項目組 <i class="fa fa-cart-plus" aria-hidden="true"></i></a>
                        </div>

                        <div class="panel panel-default">
                            <!-- TABLE 1 -->
                            <table class="table">
                                <tr class="info">
                                    <th width="5%">排序</th>
                                    <th width="40%">肌力訓練</th>
                                    <th width="10%">目標次數</th>
                                    <th width="10%">目標強度</th>
                                    <th width="5%">組數</th>
                                    <th>功能</th>
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
                                    <td rowspan="<%= training.Count() + 1 %>" class="text-center"><%= idx %></td>
                                    <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                    <td><%= tranItem.GoalTurns.HasValue && tranItem.GoalTurns>0 ? tranItem.GoalTurns.ToString() : "--" %></td>
                                    <td><%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %></td>
                                    <td rowspan="<%= training.Count() + 1 %>" class="text-center"><%= item.TrainingExecution.Repeats %></td>
                                    <td rowspan="<%= training.Count() + 1 %>">
                                        <a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/EditTraining/") + execution.ExecutionID %>">修改 <i class="fa fa-edit" aria-hidden="true"></i></a>
                                        <a onclick="deleteTraining(<%= execution.ExecutionID %>);" class="btn btn-system btn-small">刪除 <i class="fa fa-times" aria-hidden="true"></i></a>
                                    </td>
                                </tr>
                                <%                  }
                                    else
                                    {   %>
                                <tr>
                                    <td><%= tranItem.TrainingType.BodyParts %>・<%= tranItem.Description %></td>
                                    <td><%= tranItem.GoalTurns.HasValue ? tranItem.GoalTurns.ToString() : "--" %></td>
                                    <td><%= !String.IsNullOrEmpty(tranItem.GoalStrength) ? tranItem.GoalStrength : "--" %></td>
                                </tr>
                                <%                  }
                                    }   %>
                                <tr class="warning">
                                    <td>休息</td>
                                    <td><%= execution.BreakIntervalInSecond %>秒</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <%              }
                                    else
                                    {   %>
                                <tr>
                                    <td class="text-center"><%= idx %></td>
                                    <td>休息</td>
                                    <td><%= execution.BreakIntervalInSecond %>秒</td>
                                    <td>&nbsp;</td>
                                    <td class="text-center">&nbsp;</td>
                                    <td>
                                        <a class="btn btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/EditTraining/") + execution.ExecutionID %>">修改 <i class="fa fa-edit" aria-hidden="true"></i></a>
                                        <a onclick="deleteTraining(<%= execution.ExecutionID %>);" class="btn btn-system btn-small">刪除 <i class="fa fa-times" aria-hidden="true"></i></a>
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
                        </div>
                        <div class="hr2" style="margin-bottom: 10px;"></div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno"><strong>收操：</strong></label>
                        </div>
                        <textarea class="form-control" name="endingOperation" rows="5"><%= _plan.EndingOperation %></textarea>

                        <div class="hr2" style="margin-bottom: 10px;"></div>
                        <h4 class="orange-text classic-title"><span class="fa fa-commenting" aria-hidden="true">教練總評：</span></h4>
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
    </div>

    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <% Html.RenderPartial("~/Views/Shared/ConfirmationDialog.ascx"); %>

    <script>
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitPlan") %>')
          .submit();
        });

        function deleteTraining(itemID) {
            confirmIt({ title: '刪除訓練組數', message: '確定刪除此訓練組數?' }, function (evt) {
                $('<form method="post"/>').appendTo($('body'))
                    .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeleteTraining/") %>' + itemID)
                    .submit();
            });
        }

        function deletePlan() {
            confirmIt({ title: '刪除課表', message: '確定刪除此課表?' }, function (evt) {
                $('<form method="post"/>').appendTo($('body'))
                    .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/DeletePlan") %>')
                    .submit();
            });
        }

        function addTraining() {
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

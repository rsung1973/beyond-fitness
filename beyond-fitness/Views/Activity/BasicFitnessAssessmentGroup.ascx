﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<form action="<%= Url.Action("UpdateLessonFitnessAssessment","Activity",new { assessmentID = _model.AssessmentID,groupID = 2 }) %>" id="<%= _formID %>" class="form-horizontal" method="post">
    <fieldset>
        <div class="form-group">
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">疲勞指數</span>
                    <% var item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 10).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入1-10" name="_10" value="<%= item!=null ? item.TotalAssessment : null %>"/>
                    <span class="input-group-addon">指數</span>
                </div>
            </div>
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">水分攝取</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 11).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_11" value="<%= item!=null ? item.TotalAssessment : null %>" />
                    <span class="input-group-addon">CC</span>
                </div>
            </div>
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">睡眠時間</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 12).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_12" value="<%= item!=null ? item.TotalAssessment : null %>" />
                    <span class="input-group-addon">小時</span>
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <div class="form-group">
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">腰</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 13).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_13" value="<%= item!=null ? item.TotalAssessment : null %>" />
                    <span class="input-group-addon">CM</span>
                </div>
            </div>
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">腿</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 14).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_14" value="<%= item!=null ? item.TotalAssessment : null %>" />
                    <span class="input-group-addon">CM</span>
                </div>
            </div>
            <div class="col col-sm-6 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">臀</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 15).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_15" value="<%= item!=null ? item.TotalAssessment : null %>" />
                    <span class="input-group-addon">CM</span>
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset class="<%= _formID %>">
        <%  Html.RenderPartial("~/Views/Activity/CardioPower.ascx", _model); %>
    </fieldset>
    <fieldset>
        <div class="form-group">
            <div class="col col-sm-5 col-md-4">
                <div class="input-group">
                    <span class="input-group-addon">安靜心跳</span>
                    <% item = models.GetTable<LessonFitnessAssessmentReport>()
                              .Where(r => r.ItemID == 16)
                              .Where(r => r.LessonFitnessAssessment.UID == _model.UID)
                              .OrderByDescending(r => r.AssessmentID)
                              .FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_16" value="<%= item!=null ? String.Format("{0:.}",item.TotalAssessment) : null %>" />
                    <span class="input-group-addon">次</span>
                </div>
            </div>
            <div class="col col-sm-7 col-md-8">
                <div class="input-group">
                    <span class="input-group-addon">能量系統訓練當下心跳</span>
                    <% item = _model.LessonFitnessAssessmentReport.Where(r => r.ItemID == 17).FirstOrDefault(); %>
                    <input class="form-control" type="number" placeholder="請輸入純數字" name="_17" value="<%= item!=null ? String.Format("{0:.}",item.TotalAssessment) : null %>" />
                    <span class="input-group-addon">次</span>
                </div>
            </div>
        </div>
    </fieldset>
    <%  if (ViewBag.ShowOnly != true)
        { %>
    <div class="form-actions">
        <div class="row">
            <div class="col-md-12">
                <button class="btn btn-primary" type="button" onclick="updateBasicAssessment();">
                    <i class="fa fa fa-reply"></i>
                    更新
                </button>
            </div>
        </div>
    </div>
    <%  }
        else
        { %>
            <script>
                $('#<%= _formID %> input').prop('disabled', true);
            </script>
    <%  } %>
</form>
<%--<script>
    $(function () {
        var $formValidator = $("#<%=_formID %>").validate({
            // Rules for form validation
            rules: {
                _10: {
                    required: true,
                    min: 1,
                    max:10
                },
                _11: {
                    required: true
                },
                _12: {
                    required: true
                },
                _16: {
                    required: true
                },
                _17: {
                    required: true
                }
            },

            // Messages for form validation
            messages: {
                _10: {
                    required: '請輸入分數1~10',
                    min: '請輸入分數1~10',
                    max: '請輸入分數1~10'
                },
                _11: {
                    required: '請輸入數字'
                },
                _12: {
                    required: '請輸入數字'
                },
                _16: {
                    required: '請輸入數字'
                },
                _17: {
                    required: '請輸入數字'
                }
            },

            // Ajax form submition
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    success: function (data) {
                        $(".<%= _formID %>").html(data);
                        smartAlert("資料已儲存!!");
                    }
                });
            },

            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

    });
</script>--%>
<script>
    function updateBasicAssessment() {
        var event = event || window.event;
        var hasValue = false;
        var form = event.target.form
        $(form).find('input').each(function (idx) {
            if ($(this).val() != '') {
                hasValue = true;
            }
        });
        if(hasValue) {
            $(form).ajaxSubmit({
                    success: function (data) {
                        $(".<%= _formID %>").html(data);
                        smartAlert("資料已儲存!!");

                    }
                });
        } else {
            smartAlert("請輸入至少一個項目!!");
        }
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonFitnessAssessment _model;
    String _formID;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonFitnessAssessment)this.Model;
        _formID = "form_" + _model.AssessmentID;
    }

</script>

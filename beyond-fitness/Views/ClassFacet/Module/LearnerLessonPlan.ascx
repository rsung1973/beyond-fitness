<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="row">

    <div class="col-sm-12 block block-drop-shadow">
        <%  if (_model.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
        {
            Html.RenderPartial("~/Views/ClassFacet/Module/CoachSelfLesson.ascx", _model.UserProfile);
        }
        else
        {
            Html.RenderPartial("~/Views/ClassFacet/Module/LessonCount.ascx", _model.UserProfile);
        } %>        
        <div class="list-group list-group-icons bg-color-darken">
            <a style="cursor:pointer;" onclick="flipflopByAnchor();" class="list-group-item"><i class="fa fa-eye-slash"></i>&nbsp;&nbsp; About <%= _model.UserProfile.RealName %><i class="fa fa-angle-down pull-right"></i><br />
                <span class="text-warning" onclick="editRecentStatus(<%= _model.UID %>,$(this));"><%= !String.IsNullOrEmpty(_model.UserProfile.RecentStatus) ? _model.UserProfile.RecentStatus.HtmlBreakLine() : "點此新增個人近況" %></span>
            </a>
            <a href="#" class="list-group-item" id="diagnosisDialog_link"><i class="fa fa-child"></i>&nbsp;&nbsp;Fitness Diagnosis<i class="fa fa-angle-right pull-right"></i></a>
            <a href="#" class="list-group-item" id="healthlist_link"><i class="fa fa-history"></i>&nbsp;&nbsp;Health<i class="fa fa-angle-right  pull-right"></i></a>
        </div>
    </div>
    <%--<div class="col-xs-9 col-sm-9">
        <h1>
            <span class="semi-bold"><a href="<%= VirtualPathUtility.ToAbsolute("~/Member/ShowLearner/") + _model.UID %>"><%= _model.UserProfile.RealName %></a></span>
        </h1>
        <p class="font-md"><a onclick="flipflop();" ><i class="fa fa-eye-slash fa-2x closeureye"></i></a> 關於<%= _model.UserProfile.UserName ?? _model.UserProfile.RealName %>...</p>
        <p style="display:none;">
            <img src="<%= VirtualPathUtility.ToAbsolute("~/img/confidential.png") %>" width="40%"/>
        </p>
        <p class="secret-info">
            <%= _model.UserProfile.RecentStatus!=null ? _model.UserProfile.RecentStatus.Replace("\n","<br/>") : null %>
        </p>
        <ul class="list-unstyled">
            <li>
                <p class="text-muted">
                    <i class="fa fa fa-reply"></i>&nbsp;&nbsp;<a href="#" class="btn bg-color-blue btn-xs" onclick="editRecentStatus(<%= _model.UID %>,$('.secret-info'));">更新個人近況</a>
                    <i class="fa fa-gift"></i>&nbsp;&nbsp;<a href="#" class="btn bg-color-blue btn-xs" onclick="checkBonus(<%= _model.UID %>);">點數兌換</a>
                    <i class="fa fa-cogs"></i>&nbsp;&nbsp;<a href="<%= Url.Action("EditLearner","Member",new { id = _model.UID }) %>" class="btn bg-color-blue btn-xs">修改個人資料</a>
                </p>
            </li>
            <li>
                <p class="text-muted">
                    <i class="fa fa-line-chart"></i>&nbsp;&nbsp;<a href="#" class="btn bg-color-blue btn-xs" onclick="showLearnerAssessment(<%= _model.UID %>,<%= _item.LessonID %>);">檢視體能分析表</a>
                    <i class="fa fa-history"></i>&nbsp;&nbsp;<a href="#" class="btn bg-color-blue btn-xs" id="healthlist_link">檢視健康指數</a>
                </p>
            </li>
        </ul>
    </div>--%>
    <script>

        function checkBonus(id) {
            showLoading();
            $.post('<%= Url.Action("CheckBonus","LearnerFacet") %>', { 'id': id }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
            return false;
        }

        function showLearnerAssessment(uid,lessonID) {
            showLoading();
            $.post('<%= Url.Action("LearnerAssessment","ClassFacet") %>', { 'uid': uid,'lessonID':lessonID }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
            return false;
        }

        $('#healthlist_link').click(function () {
            showLoading();
            $.post('<%= Url.Action("HealthIndex","LearnerFacet",new { id = _model.UID }) %>', {}, function (data) {
                $(data).appendTo($('body'));
                hideLoading();
            });
            return false;
        });

        $('#diagnosisDialog_link').click(function () {
            diagnose();
            return false;
        });

        function diagnose(diagnosisID) {
            showLoading();
            $.post('<%= Url.Action("Diagnose","FitnessDiagnosis",new { uid = _model.UID }) %>', { 'diagnosisID': diagnosisID }, function (data) {
                hideLoading();
                $(data).appendTo($('body'));
            });
        }

    </script>
</div>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    RegisterLesson _model;
    LessonTime _item;
 
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterLesson)this.Model;
        _item = (LessonTime)ViewBag.LessonTime;

    }

</script>

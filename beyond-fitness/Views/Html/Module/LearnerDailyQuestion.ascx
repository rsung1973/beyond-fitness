﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="questionDialog" title="每日問與答" class="bg-color-darken">
    <div class="panel panel-default bg-color-darken">
        <form action="<%= Url.Action("AnswerDailyQuestion","Activity",new { _model.QuestionID }) %>" method="post" id="dailyAns" class="smart-form">
            <div class="panel-body status smart-form vote">
                <div class="who clearfix">
                    <% _model.UserProfile.RenderUserPicture(Writer, new { @class = "busy" }); %>
                    <span class="name font-lg"><b><%= _model.UserProfile.UserName ?? _model.UserProfile.FullName() %></b></span>
                    <span class="from font-md"><b>Hi, <%= _userProfile.UserName ?? _userProfile.FullName() %></b> 請試試回答以下問的答案，答對會有意想不到的驚喜喔！</span>
                </div>
                <div class="image font-md">
                    <strong><%= _model.Question %></strong>
                </div>
                <ul class="comments">
                    <%  foreach (var quest in _model.PDQSuggestion)
                        { %>
                    <li>
                        <label class="radio font-md">
                            <input type="radio" name="suggestionID" value="<%= quest.SuggestionID %>" />
                            <i></i><%= quest.Suggestion %></label>
                    </li>
                    <%  } %>
                </ul>
                <footer>
                    <button id="btnAnswer" type="button" class="btn btn-primary">
                        送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                    </button>
                    <button id="btnDismiss" type="button" class="btn btn-default" data-dismiss="modal">
                        取消
                    </button>
                </footer>

                <div class="message">
                    <i class="fa fa-check fa-lg"></i>
                    <p class="text-center">
                    </p>
                </div>
                <div class="errormessage">
                    <i class="fa fa-times fa-lg"></i>
                    <p class="text-center">
                        非常可惜答錯囉！試著再次請教<%= _lesson.ServingCoach.UserProfile.FullName() %>正確答案吧！
                    </p>
                </div>
            </div>
        </form>
    </div>
</div>


<script>

    $(function () {

        $('#questionDialog').dialog({
            //autoOpen: false,
            resizable: true,
            modal: true,
            width: "auto",
            height: "auto",
            title: "<h4 class='modal-title'><i class='fa fa-fw fa-quora'></i>  每日問與答</h4>",
            close: function (event, ui) {
                $('#questionDialog').remove();
            }
        });

        $('#btnAnswer').on('click', function (evt) {

            var $ans = $('#dailyAns input:radio[name="suggestionID"]:checked');
            if ($ans.length == 0) {
                alert('請選擇!!');
                return;
            }

            showLoading();
            $.post('<%= Url.Action("AnswerDailyQuestion","Activity",new { _model.QuestionID }) %>', { 'suggestionID': $ans.val() }, function (data) {
                hideLoading();
                $('#dailyAns footer').remove();
                if (data.result) {
                    $('#dailyAns .message').css('display', 'block')
                    .find('p').text('恭喜你答對囉，可獲得點數' + data.message + '點，集滿一定點數後有意想不到的驚喜喔！');
                } else {
                    //$modal.modal('hide');
                    if (data.message) {
                        $('#dailyAns .errormessage p').text(data.message);
                    }
                    $('#dailyAns .errormessage').css('display', 'block');
                }
                $('#questionDialog').scrollTop(300);
            });

            $('#dailyAns input:radio[name="suggestionID"]').prop('disabled', true);
                                    
            <%--            $('#dailyAns').ajaxForm({
                beforeSubmit: function () {
                    showLoading(true);
                },
                success: function (data) {
                    hideLoading();
                    $('#dailyAns footer').remove();
                    if (data.result) {
                        $('#dailyAns .message').css('display', 'block')
                        .find('p').text('恭喜你答對囉，可獲得點數' + data.message + '點，集滿一定點數後有意想不到的驚喜喔！');
                    } else {
                        //$modal.modal('hide');
                        $('#dailyAns .errormessage').css('display', 'block');
                    }
                    $('#questionDialog').scrollTop(300);
                },
                error: function () {
                }
            }).submit();    --%>

        });

        $('#btnDismiss').on('click', function (evt) {
            $('#questionDialog').dialog('close');
        });

    });

</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    PDQQuestion _model;
    UserProfile _userProfile;
    RegisterLesson _lesson;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (PDQQuestion)this.Model;
        ViewBag.ModalId = "learnerQuest";
        _userProfile = Context.GetUser();
        _lesson = models.GetTable<RegisterLesson>().Where(r => r.UID == _userProfile.UID)
            .OrderByDescending(r => r.RegisterID).FirstOrDefault();
    }

</script>

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

<div class="modal fade" id="<%= ViewBag.ModalId ?? "theModal" %>" tabindex="-1" role="dialog" aria-labelledby="confirmLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                    &times;
                </button>
                <h4 class="modal-title" id="myModalLabel"><i class="fa-fw fa fa-question"></i>每日小提問</h4>
            </div>
            <div class="modal-body bg-color-darken txt-color-white">
                <div class="panel panel-default bg-color-darken">
                    <form action="<%= Url.Action("AnswerDailyQuestion","Activity") %>" method="post" id="dailyAns" class="smart-form">
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
                                        <input type="radio" name="suggestionID" value="<%= quest.SuggestionID %>"/>
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
        </div>
    </div>
</div>
<script>

    $(function () {
        var $modal = $('#<%= ViewBag.ModalId ?? "theModal" %>');
        $modal.on('hidden.bs.modal', function (evt) {
            $modal.remove();
        });

        //$modal.on('shown.bs.modal', function () {
        //    $(this).find('.modal-dialog').css({
        //        width: 'auto',
        //        height: 'auto',
        //        'max-height': '100%'
        //    });
        //});

        $modal.modal('show');

        $('#btnAnswer').on('click', function (evt) {
            $('#dailyAns').ajaxForm({
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
                    $('.modal-body').scrollTop(300);
                },
                error: function () {
                }
            }).submit();
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

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

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

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>登記上課時間</span></h4>

                    <!-- Start Contact Form -->

                    <div class="blog-post quote-post">
                        <div class="form-group has-feedback">
                            <% Html.RenderPartial("~/Views/Lessons/CoachSelector.ascx", new InputViewModel { Id = "coachID", Name = "coachID", DefaultValue = _model.UID }); %>
                        </div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno">學員：</label>
                            <a onclick="addUser();" data-toggle="modal" data-target="#addUserItem" data-whatever="請選擇"><i class="fa fa-user-plus fa-2x" aria-hidden="true"></i></a>
                            <div id="attendee"></div>
                            <label id="registerID-error" class="error" for="registerID" style="display: none;"></label>
                        </div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno">日期：</label>
                            <div class="input-group date form_date" data-date="" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
                                <input id="classDate" name="classDate" class="form-control" size="16" type="text" value="<%= _viewModel.ClassDate.ToString("yyyy/MM/dd") %>" readonly>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="exampleInputFile" class="control-label">上課時段：</label>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="input-group date form_time" data-date="" data-date-format="hh:ii" data-link-field="dtp_input1">
                                        <input id="classTime" name="classTime" class="form-control" size="16" type="text" value="<%= String.Format("{0:00}:{1:00}",_viewModel.ClassTime.Hours,_viewModel.ClassTime.Minutes) %>" readonly>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                </div>
                                <%--                                            <div class="col-md-1">
                                                <span style="line-height: 30px;"><i class="fa fa-long-arrow-right" aria-hidden="true"></i></span>
                                            </div>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile" class="control-label">課程時間：</label>
                            <select name="duration">
                                <option value="60" <%= _viewModel.Duration==60 ? "checked": null %>>1 小時</option>
                                <option value="90" <%= _viewModel.Duration==90 ? "checked": null %>>1.5 小時</option>
                            </select>
                        </div>
                    </div>

                    <a id="nextStep" class="btn-system btn-medium"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確認</a>
                    <a class="btn-system btn-medium border-btn" href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>取消</a>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>

    <script>
        $('#vip,#m_vip').addClass('active');
        //$('#theForm').addClass('contact-form');

        $(function () {

            $formValidator.settings.submitHandler = function (form) {

                var $items = $('input[name="registerID"]:checked');
                if ($items.length <= 0) {
                    $('#registerID-error').css('display', 'block');
                    $('#registerID-error').text('請選擇上課學員!!');
                    return;
                }

                //$(form).submit();
                return true;
            };

            $('#coachID').rules('add', {
                'required': true
            });

            $('#classDate').rules('add', {
                'required': true,
                'date': true
            });

            $('#classTime').rules('add', {
                'required': true
            });
        });


        $('#nextStep').on('click', function (evt) {

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/BookingByCoach") %>')
          .submit();
        });

        function addUser() {
            $('form').find('#addUserItem').remove();
            var $modal = $('<div class="form-horizontal modal fade" id="addUserItem" tabindex="-1" role="dialog" aria-labelledby="searchdilLabel" aria-hidden="true" />');
            $modal.on('hidden.bs.modal', function (evt) {
                $modal.remove();
            });
            $modal.appendTo($('form'))
                .load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/Attendee") %>', null, function () {
                $modal.modal('show');
            });
    }
    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    LessonTimeViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _viewModel = (LessonTimeViewModel)ViewBag.ViewModel;
    }



</script>

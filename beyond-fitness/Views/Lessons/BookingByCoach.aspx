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

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-calendar-plus-o"> 登記上課時間</span></h4>

                    <!-- Start Contact Form -->

                    <div class="blog-post quote-post">
                        <div class="form-group has-feedback">
                            <% Html.RenderPartial("~/Views/Lessons/CoachSelector.ascx", new InputViewModel { Id = "coachID", Name = "coachID", DefaultValue = _model.UID }); %>
                        </div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno">學員：</label>
                            <label class="control-label" for="classno">
                                <div id="attendee"></div>
                            </label>
                            <label id="registerID-error" class="error" for="registerID" style="display: none;"></label>
                            <a onclick="addUser();" class="btn btn-system btn-small">查詢 <i class="fa fa-search-plus" aria-hidden="true"></i></a>
                        </div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="classno">日期、時段：</label>
                            <div class="input-group date form_time" data-date="" data-date-format="yyyy/mm/dd hh:ii" data-link-field="dtp_input1">
                                <input id="classDate" name="classDate" class="form-control" size="16" type="text" value="<%= _viewModel.ClassDate.ToString("yyyy/MM/dd") %>" readonly>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                            </div>
                        </div>

<%--                        <div class="form-group">
                            <label for="exampleInputFile" class="control-label">上課時段：</label>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="input-group date form_time" data-date="" data-date-format="hh:ii" data-link-field="dtp_input1">
                                        <input id="classTime" name="classTime" class="form-control" size="16" type="text" value="<%= String.Format("{0:00}:{1:00}",_viewModel.ClassTime.Hours,_viewModel.ClassTime.Minutes) %>" readonly>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <label for="exampleInputFile" class="control-label">課程時間：</label>
                            <select name="duration">
                                <option value="60" <%= _viewModel.Duration==60 ? "checked": null %>>1 小時</option>
                                <option value="90" <%= _viewModel.Duration==90 ? "checked": null %>>1.5 小時</option>
                            </select>
                        </div>
                    </div>

                    <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Coach") %>" class="btn-system btn-medium">回行事曆清單 <i class="fa fa-calendar" aria-hidden="true"></i></a>
                    <a id="nextStep" class="btn-system btn-medium">確定 <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></a>

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

            //$('#classTime').rules('add', {
            //    'required': true
            //});
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

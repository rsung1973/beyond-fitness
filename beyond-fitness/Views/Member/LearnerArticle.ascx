<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<article class="col-xs-12 col-sm-12 col-md-9 col-lg-9">
    <div class="well well-sm bg-color-darken txt-color-white">
        <div class="row">

            <%  Html.RenderPartial("~/Views/Layout/Carousel.ascx"); %>

            <div class="col-sm-12">

                <div class="row">

                    <div class="col-xs-4 col-sm-3 profile-pic">
                        <% _model.RenderUserPicture(Writer, "profileImg"); %>
                        <div class="padding-10">
                            <i class="fa fa-birthday-cake"></i>&nbsp;&nbsp;<span class="txt-color-darken"> <%= _model.YearsOld() %>歲</span>
                            <br />
                            <h4 class="font-md"><strong><%= _currentLessons.Sum(c=>c.Lessons) - _currentLessons.Sum(c=>c.LessonTime.Count(l=>l.LessonAttendance!= null)) %> / <%= _currentLessons.Sum(c=>c.Lessons) %></strong>
                                <br/>
                                <small>剩餘/全部 上課數</small></h4>
                        </div>
                    </div>
                    <%  Html.RenderPartial("~/Views/Member/LearnerInfo.ascx", _model); %>
                </div>
            </div>
        </div>
        <div class="row">
            <hr />
        </div>
        <div class="row no-padding">
            <div class="col-sm-12">
                <ul class="nav nav-tabs tabs-pull-right">
                    <%  if(ViewBag.ShowOnly!=true)
                        { %>
                            <button type="button" class="btn btn-labeled btn-success bg-color-blueLight" onclick="addLessons();">
                                <span class="btn-label">
                                    <i class="fa fa-calendar-plus-o"></i>
                                </span>新增課數
                            </button>
                    <%  } %>
                    <li>
                        <a data-toggle="tab" href="#s2"><i class="fa fa-street-view"></i><span>問卷調查表</span></a>
                    </li>
                    <li class="active">
                        <a data-toggle="tab" href="#s1"><i class="fa fa-credit-card"></i><span>購買上課記錄</span></a>
                    </li>
                </ul>
                <div class="tab-content padding-top-10">
                    <div class="tab-pane fade in active" id="s1">
                        <div class="row ">
                            <div class="col-xs-12 col-sm-12">
                                <% Html.RenderPartial("~/Views/Member/LessonsList.ascx", _items); %>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade in" id="s2">
                        <div class="row">
                            <%  ViewBag.DataItems = models.GetTable<PDQQuestion>().OrderBy(q => q.QuestionNo).ToArray();
                                Html.RenderPartial("~/Views/Member/PDQInfoByLearner.ascx", _model); %>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%  if (ViewBag.ShowOnly != true)
            {
                Html.RenderPartial("~/Views/Member/RegisterLessonForm.ascx", _viewModel);
                if (_modelState!=null && !_modelState.IsValid)
                { %>
                    <script>
                            $(function () {
                                $('#registerLesson').modal('toggle');
                            });
                    </script>
        <%      }
            } %>
    </div>

</article>

<script>
    $(function () {
        $('.carousel.slide').carousel({
            interval: 3000,
            cycle: true
        });
        $('.carousel.fade').carousel({
            interval: 3000,
            cycle: true
        });

        $('#registerLesson').on('shown.bs.modal', function (evt) {
            $(this).find('select').first().focus();
        });
    });
</script>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    LessonViewModel _viewModel;

    IEnumerable<RegisterLesson> _items;
    IEnumerable<RegisterLesson> _currentLessons;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _viewModel = (LessonViewModel)ViewBag.ViewModel;

        _items = models.GetTable<RegisterLesson>().Where(r => r.UID == _model.UID)
            .OrderByDescending(r => r.RegisterID);
        _currentLessons = _items.Where(i => i.Lessons != i.LessonTime.Count(s => s.LessonAttendance != null));
    }


</script>

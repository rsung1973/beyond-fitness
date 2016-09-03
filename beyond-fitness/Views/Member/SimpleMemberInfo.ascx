<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<article class="col-xs-12 col-sm-12 col-md-9 col-lg-9">
    <div class="well well-sm bg-color-darken txt-color-white">
        <div class="row">

            <%  Html.RenderPartial("~/Views/Layout/Carousel.ascx"); %>

            <div class="col-sm-12">


                <div class="row">

                    <div class="col-sm-3 profile-pic">
                        <% _model.RenderUserPicture(Writer, "profileImg"); %>
                        <div class="padding-10">
                            <i class="fa fa-birthday-cake"></i>&nbsp;&nbsp;<span class="txt-color-darken"> 28歲</span>
                            <br />
                            <h4 class="font-md"><strong><% var totalLessons = _currentLessons.Sum(c => c.Lessons); %>
                                <%= totalLessons
                                        - _currentLessons.Sum(c=>c.LessonTime.Count(l=>l.LessonAttendance!= null))
                                        - _currentLessons
                                        .Where(c=>c.RegisterGroupID.HasValue)
                                        .Sum(c=>c.GroupingLesson.LessonTime
                                            .Where(l=>l.RegisterID!=c.RegisterID)
                                            .Count(l=>l.LessonAttendance!= null)) %> / <%= totalLessons %></strong>
                                <br/>
                                <small>剩餘/購買上課次數</small></h4>
                        </div>
                    </div>
                    <div class="col-sm-9">
                        <h1><span class="semi-bold"><%= _model.RealName %></span>
                            <br>
                            <small></small></h1>

                        <ul class="list-unstyled">
                            <li>
                                <p class="text-muted">
                                    <i class="fa fa-phone"></i>&nbsp;&nbsp;(<span class="txt-color-darken">886) <%= _model.Phone %> </span>
                                </p>
                            </li>
                            <li>
                                <p class="text-muted">
                                    <i class="fa fa-envelope"></i>&nbsp;&nbsp;<a href="mailto:<%= _model.PID.Contains("@") ? _model.PID : null %>"><%= _model.PID.Contains("@") ? _model.PID : null %></a>
                                </p>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>

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

        _items = models.GetTable<RegisterLesson>()
            .Where(l => l.ClassLevel.HasValue)
            .Where(r => r.UID == _model.UID)
            .OrderByDescending(r => r.RegisterID);
        _currentLessons = _items.Where(i => i.Attended != (int)Naming.LessonStatus.課程結束);
    }

</script>

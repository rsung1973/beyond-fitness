<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<div class="col-sm-12">
    <div class="row">
        <div class="col-sm-3 profile-pic">
            <% _model.UserProfile.RenderUserPicture(Writer, "profileImg"); %>
            <div class="padding-10">
                <h4 class="font-md"><strong>
                    <%  var items = _model.AttendingLesson.Where(l => l.ClassTime >= DateTime.Today && l.ClassTime < DateTime.Today.AddDays(1)); %>
                    <%= items.Count() %>堂</strong>
                    <br>
                    <small>今日上課</small>
                </h4>
            </div>
        </div>
        <div class="col-sm-6">
            <h3><%= _model.UserProfile.RealName %>
                <span class="label label-info" rel="tooltip" data-placement="bottom" data-original-title="<span class='label bg-color-darken font-md'><%= _items.Count() %>堂 / <%= String.Format("{0:##,###,###,###}",_tuition.Sum(i=>i.ShareAmount)) %>元</span>" data-html="true"><%= _model.ProfessionalLevel.LevelName %> <i class="fa fa-fw fa fa-info-circle"></i></span>
                <%--                                             <span class="label label-success" rel="tooltip" data-placement="bottom" data-original-title="<span class='label bg-color-darken font-md'>離下一等級的業績尚有１０萬<br/>平均上課數５堂即可晉級</span>" data-html="true">Level 3 <i class="fa fa-fw fa fa-info-circle"></i></span>
                                            <span class="label label-warning" rel="tooltip" data-placement="bottom" data-original-title="<span class='label bg-color-darken font-md'>您目前的業績尚有１０萬<br/>平均上課數５堂未達到</span>" data-html="true">Level 3 <i class="fa fa-fw fa fa-exclamation-triangle"></i></span>
                                            <span class="label label-danger" rel="tooltip" data-placement="bottom" data-original-title="<span class='label bg-color-darken font-md'>您目前的業績已落後１０萬<br/>平均上課數已落後５堂</span>" data-html="true">Level 3 <i class="fa fa-fw fa fa-exclamation-triangle"></i></span>--%>
            </h3>
            <ul class="list-unstyled">
                <li>
                    <p class="text-muted">
                        <%--                                                    <i class="fa fa-certificate"></i>&nbsp;&nbsp;<a href="#" class="btn bg-color-blue btn-xs">登錄證照</a></a>--%>
                        <%--                                                   <i class="fa fa-cogs"></i>&nbsp;&nbsp;<a href="<%= Url.Action("EditMyself","Account") %>" class="btn bg-color-blue btn-xs">修改個人資料</a>--%>
                    </p>
                </li>
            </ul>
        </div>
        <div class="col-sm-3">
            <h1><small>今日上課學員</small></h1>
            <ul class="list-inline friends-list">
                <%  foreach (var item in items)
                                                {
                                                    var expansion = item.LessonTimeExpansion.First();
                                                    foreach (var lesson in item.GroupingLesson.RegisterLesson)
                                                    { %>
                <li><a href='javascript:makeLessonPlan(<%= JsonConvert.SerializeObject(new
                                                            {
                                                                classDate = expansion.ClassDate.ToString("yyyy-MM-dd"),
                                                                hour = expansion.Hour,
                                                                registerID = item.RegisterID,
                                                                lessonID = item.LessonID
                                                            }) %>);'>
                    <%  lesson.UserProfile.RenderUserPicture(Writer, new { @class = "", @style = "width:40px" }); %></a></li>
                <%      }
                                                } %>
            </ul>
        </div>
    </div>
    <script>
        $(function () {
            $('span[rel="tooltip"]').tooltip();
        });
    </script>
</div>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    ServingCoach _model;
    IQueryable<LessonTime> _items;
    IQueryable<TuitionAchievement> _tuition;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (ServingCoach)this.Model;

        DateTime quarterStart = new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1);
        DateTime? dateTo = null;
        _items = models.GetLessonAttendance(_model.CoachID, quarterStart, ref dateTo, 3, null);
        _tuition = models.GetTuitionAchievement(_model.CoachID, quarterStart, ref dateTo, 3);
    }

</script>

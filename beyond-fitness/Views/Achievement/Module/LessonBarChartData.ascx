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
<%= JsonConvert.SerializeObject(result) %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    String _tableId = "lesson" + DateTime.Now.Ticks;
    IQueryable<LessonTime> _model;
    AchievementQueryViewModel _viewModel;
    String _chartID = "bar" + DateTime.Now.Ticks;
    Object result;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<LessonTime>)this.Model;
        _viewModel = (AchievementQueryViewModel)ViewBag.ViewModel;

        int[] scope = new int[] {
                        (int)Naming.LessonPriceStatus.一般課程,
                        //(int)Naming.LessonPriceStatus.企業合作方案,
                        (int)Naming.LessonPriceStatus.已刪除,
                        (int)Naming.LessonPriceStatus.點數兌換課程 };

        var dailyItems = models.GetTable<DailyWorkingHour>()
            .Select(d => new { d.Hour, Items = _model.Where(l => l.HourOfClassTime == d.Hour) });

        var groupItems = _model.GroupBy(d => d.ClassTime.Value.Date).OrderBy(p => p.Key);

        bool byHour = false;
        if (_viewModel.AchievementDateFrom == _viewModel.AchievementDateTo)
        {
            byHour = true;
        }

        List<_ResultItem> items;
        if (byHour)
        {
            items = dailyItems.Select(t =>
                new _ResultItem
                {
                    Hour = t.Hour,
                    PTCount = t.Items.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                                        || (l.RegisterLesson.RegisterLessonEnterprise != null
                                        && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                            .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status))).Count(),
                    PICount = t.Items.Where(l => l.TrainingBySelf == 1).Count(),
                    TrialCount = t.Items.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                                        || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count()
                }).ToList();
        }
        else
        {
            items = groupItems.Select(t =>
                new _ResultItem
                {
                    ClassTime = t.Key,
                    PTCount = t.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                                        || (l.RegisterLesson.RegisterLessonEnterprise != null
                                        && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                            .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status))).Count(),
                    PICount = t.Where(l => l.TrainingBySelf == 1).Count(),
                    TrialCount = t.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                                        || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count()
                }).ToList();
        }


        result = new
        {
            labels = byHour
                ? items.Select(g => $"{g.Hour:00}:00")
                : items.Select(g => String.Format("{0:yyyy/MM/dd}", g.ClassTime)),
            datasets = new object[]
            {
                new
                {
                    type= "bar",
                    label= "P.T",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(184,227,243,.43)",
                    data= items.Select(g=>g.PTCount),
                },
                new
                {
                    type= "bar",
                    label= "P.I",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(255,7,7,.43)",
                    data= items.Select(g=>g.PICount),
                },
                new
                {
                    type= "bar",
                    label= "體驗",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(233,157,201,.43)",
                    data= items.Select(g=>g.TrialCount),
                }
            }
        };
    }

    class _ResultItem
    {
        public DateTime? ClassTime { get; set; }
        public int Hour { get; set; }
        public int PTCount { get; set; }
        public int PICount { get; set; }
        public int TrialCount { get; set; }
    }
</script>

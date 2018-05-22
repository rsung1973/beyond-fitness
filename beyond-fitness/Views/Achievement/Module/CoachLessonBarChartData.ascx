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
    IQueryable<LessonTime> _model;
    AchievementQueryViewModel _viewModel;
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

        var items = _model.GroupBy(l=>l.AttendingCoach);
        result = new
        {
            labels = items.Join(models.GetTable<UserProfile>(), g => g.Key, u => u.UID, (g, u) => u)
                        .Select(u => u.RealName).ToArray(),
            datasets = new object[]
            {
                new
                {
                    type= "bar",
                    label= "P.T",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(184,227,243,.43)",
                    data= items.Select(g=>
                                    g.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                                            || (l.RegisterLesson.RegisterLessonEnterprise!=null
                                            && (new int?[] {(int)Naming.LessonPriceStatus.一般課程,(int)Naming.LessonPriceStatus.團體學員課程 })
                                                .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status))).Count()).ToArray(),
                },
                new
                {
                    type= "bar",
                    label= "P.I",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(255,7,7,.43)",
                    data= items.Select(g=>
                                    g.Where(l => l.TrainingBySelf == 1).Count()).ToArray(),
                },
                new
                {
                    type= "bar",
                    label= "體驗",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(233,157,201,.43)",
                    data= items.Select(g=>
                                    g.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                                            || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count()).ToArray(),
                }
            }
        };
    }

</script>

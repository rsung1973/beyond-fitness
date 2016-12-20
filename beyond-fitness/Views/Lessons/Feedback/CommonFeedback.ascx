<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_lessonFeedback.Count() > 0)
    { %>
        <p class="alert alert-success">
        <%  foreach (var f in _lessonFeedback)
            { %>
                <strong><i class="fa fa-commenting-o"></i><%= f.RegisterLesson.UserProfile.RealName %>針對<%= String.Format("{0:yyyy/MM/dd}",f.LessonTime.ClassTime) %>的課程有話要說:<%= f.Remark %></strong><br />
        <%  } %>
        </p>
<%  } %>
<%  if (_learnerFeedback.Count() > 0)
    { %>
        <p class="alert alert-info">
    <%  foreach (var f in _learnerFeedback)
        { %>
            <strong><i class="fa fa-comments-o"></i><%= f.RegisterLesson.UserProfile.RealName %>針對<%= String.Format("{0:yyyy/MM/dd}",f.LessonTime.ClassTime) %>的訓練內容回覆:<%= f.FeedBack %></strong><br />
    <%  } %>
        </p>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    IEnumerable<LessonFeedBack> _lessonFeedback;
    IEnumerable<LessonFeedBack> _learnerFeedback;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        var groupingLesson = models.GetTable<LessonTime>().Where(l => l.LessonID == _model.LessonID)
            .Select(l => l.GroupingLesson)
            .Join(models.GetTable<RegisterLesson>(), g => g.GroupID, r => r.RegisterGroupID, (g, r) => r);

        if (_model.RegisterLesson.GroupingMemberCount > 1)
        {
            _lessonFeedback = models.GetTable<LessonFeedBack>().Where(f => f.Remark != null && f.Remark.Length > 0)
                .Join(models.GetTable<RegisterLesson>()
                    .Join(groupingLesson,
                        r => r.UID, g => g.UID, (r, g) => r),
                    f => f.RegisterID, r => r.RegisterID, (f, r) => f)
                .OrderByDescending(l => l.RemarkDate).Take(2).ToList();

            _learnerFeedback = models.GetTable<LessonFeedBack>().Where(f => f.FeedBack != null && f.FeedBack.Length > 0)
                .Join(models.GetTable<RegisterLesson>()
                    .Join(groupingLesson,
                        r => r.UID, g => g.UID, (r, g) => r),
                    f => f.RegisterID, r => r.RegisterID, (f, r) => f)
                .OrderByDescending(l => l.FeedBackDate).Take(2).ToList();

        }
        else
        {
            _lessonFeedback = models.GetTable<LessonFeedBack>()
                    .Where(f => f.RegisterLesson.UID == _model.RegisterLesson.UID)
                    .Where(f => f.Remark != null && f.Remark.Length > 0)
                    .OrderByDescending(f => f.RemarkDate).Take(2).ToList();

            _learnerFeedback = models.GetTable<LessonFeedBack>()
                    .Where(f => f.RegisterLesson.UID == _model.RegisterLesson.UID)
                    .Where(f => f.FeedBack != null && f.FeedBack.Length > 0)
                    .OrderByDescending(f => f.FeedBackDate).Take(2).ToList();
        }
    }

</script>

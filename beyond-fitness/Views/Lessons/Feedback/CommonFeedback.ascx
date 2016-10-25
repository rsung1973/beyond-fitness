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
                <strong><i class="fa fa-commenting-o"></i><%= f.LessonTime.RegisterLesson.UserProfile.RealName %>針對<%= String.Format("{0:yyyy/MM/dd}",f.LessonTime.ClassTime) %>的課程有話要說:<%= f.FeedBack %></strong><br />
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
    IEnumerable<LessonPlan> _lessonFeedback;
    List<LessonFeedBack> _learnerFeedback;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        _lessonFeedback = models.GetTable<LessonTime>().Where(l => l.RegisterLesson.UID == _model.RegisterLesson.UID
                || l.GroupingLesson.RegisterLesson.Any(r => r.UID == _model.RegisterLesson.UID))
            .Select(l => l.LessonPlan)
            .Where(p => p.FeedBack!=null)
            .OrderByDescending(l => l.FeedBackDate).Take(2);
        if(_model.GroupID.HasValue)
        {
            _learnerFeedback = new List<LessonFeedBack>();
            foreach(var lesson in _model.GroupingLesson.RegisterLesson )
            {
                _learnerFeedback.AddRange(models.GetTable<LessonFeedBack>()
                    .Where(f => f.RegisterLesson.UID == lesson.UID)
                    .Where(f => f.FeedBack != null)
                    .OrderByDescending(f => f.FeedBackDate).Take(2));
            }
        }
        else
        {
            _learnerFeedback = models.GetTable<LessonFeedBack>()
                    .Where(f => f.RegisterLesson.UID == _model.RegisterLesson.UID)
                    .Where(f => f.FeedBack != null)
                    .OrderByDescending(f => f.FeedBackDate).Take(2).ToList();
        }
    }

</script>

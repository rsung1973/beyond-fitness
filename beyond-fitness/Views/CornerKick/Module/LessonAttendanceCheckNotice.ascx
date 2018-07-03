<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Timeline" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<%  if (_item != null)
    { %>
<li>
    <i class="livicon-evo" data-options="name: angle-wide-right-alt.svg; size: 30px; style: original;  strokeWidth:2px; autoPlay:true"></i>
    <a href="<%= Url.Action("AG001_LearnerToCheckAttendance","CornerKick") %>"><%= _model.UserProfileExtension.Gender=="F" ? "親愛的" : "兄弟" %>，還有 <%= _item.CheckCount %> 堂課沒打卡</a>
</li>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    List<TimelineEvent> _items;
    LessonAttendanceCheckEvent _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _items = (List<TimelineEvent>)ViewBag.UserNotice;

        var items = _model.LearnerGetUncheckedLessons(models);

        var count = items.Count();
        if(count>0)
        {
            _item = new LessonAttendanceCheckEvent
            {
                Profile = _model,
                CheckCount = count
            };
            _items.Add(_item);
        }

    }

</script>

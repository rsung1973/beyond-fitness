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
    <i class="livicon-evo" data-options="name: angle-wide-right-alt.svg; size: 30px; style: original; strokeWidth:2px; autoPlay:true"></i>
    <a href="qa.html">兄弟，來挑戰運動小學堂</a>
</li>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    List<TimelineEvent> _items;
    DailyQuestionEvent _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _items = (List<TimelineEvent>)ViewBag.UserNotice;

        var question = models.PromptLearnerDailyQuestion(_model);

        if (question!=null)
        {
            _item = new DailyQuestionEvent
            {
                Profile = _model,
                DailyQuestion = question
            };
            _items.Add(_item);
        }

    }

</script>

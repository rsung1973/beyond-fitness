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
    <i class="livicon-evo" data-options="name: bell.svg; size: 30px; style: original; strokeWidth:2px; autoPlay:true"></i>
    <label>兄弟，合約即將到期（<%= $"{_item.ExpiringContract.Expiration:yyyy/MM/dd}" %>）</label>
</li>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;
    List<TimelineEvent> _items;
    ExpiringContractEvent _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _items = (List<TimelineEvent>)ViewBag.UserNotice;

        var contract = models.PromptExpiringContract().Where(c => c.CourseContractMember.Any(m => m.UID == _model.UID)).FirstOrDefault();
        if (contract != null)
        {
            _item = new ExpiringContractEvent
            {
                Profile = _model,
                ExpiringContract = contract
            };

            _items.Add(_item);
        }

    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

    <% if (_items != null && _items.Count() > 0)
        {
            foreach (var item in _items)
            { %>
                <label class="radio">
                <input type="radio" name="registerID" value="<%= item.RegisterID %>" />
                <i></i><div><%= item.UserProfile.RealName %>「<%= item.Lessons %>堂-<%= item.LessonPriceType.Description %>」
                <%  if( item.GroupingMemberCount>1)
                    {   %>
                        <li class="fa fa-group"></li>
                        團體《<%= String.Join("·", models.GetTable<GroupingLesson>().Where(g => g.GroupID == item.RegisterGroupID)
                                                .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterID != item.RegisterID),
                                                    g => g.GroupID, r => r.RegisterGroupID, (g, r) => r)
                                                .Select(r => r.UserProfile.RealName)) %>》
                <%  }
                    else
                    {   %>
                        <li class="fa fa-child"></li>
                        個人
                <%  } %>
                    </div>
                </label>

    <%      }
        }
        else
        { %>
            查無相符條件的上課資料!!
    <%  } %>


<script runat="server">

    ModelStateDictionary _modelState;
    IEnumerable<RegisterLesson> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _items = (IEnumerable<RegisterLesson>)this.Model;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

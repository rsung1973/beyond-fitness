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

<table class="table">
    <% if (_items != null && _items.Count() > 0)
        {
            foreach (var item in _items)
            { %>
    <tr class="info">
        <td width="25">
            <div>
                <input type="radio" name="registerID" value="<%= item.RegisterID %>" />
                <%= item.UserProfile.RealName %> <%= item.Lessons %>堂課(<%= item.LessonPriceType.Description %>) 
                <%  if( item.GroupingMemberCount>1)
                    {   %>
                    團體[<%= String.Join("、", models.GetTable<GroupingLesson>().Where(g => g.GroupID == item.RegisterGroupID)
                                     .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterID != item.RegisterID),
                                         g => g.GroupID, r => r.RegisterGroupID, (g, r) => r)
                                      .Select(r => r.UserProfile.RealName)) %>]
                <%  }
                    else
                    {   %>
                    個人
                <%  } %>
            </div>
        </td>
    </tr>
    <%      }
        }
        else
        { %>
    <tr>
        <td>查無相符條件的上課資料!!</td>
    </tr>
    <%  } %>
</table>


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

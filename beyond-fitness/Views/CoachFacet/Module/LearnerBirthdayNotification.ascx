<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="jarviswidget" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
    <header class="bg-color-redLight">
        <span class="widget-icon"><i class="fa fa-birthday-cake"></i></span>
        <h2>今天有<%= models.GetTable<UserProfile>().Where(u => u.Birthday.HasValue
                        && u.Birthday.Value.DayOfYear == _lessonDate.Value.DayOfYear).Count() %>個學員生日喔!</h2>
    </header>
    <!-- widget div-->
    <div>
        <!-- widget content -->
        <div class="widget-body txt-color-white padding-5">
            <!-- content -->
            <%  foreach (var item in _items)
                    { %>
                        <div class="user">
                            <a href="http://line.me/R/msg/text/?｡◕‿◕｡╔ ░ⒽⒶⓅⓅⓎ░ ⒷⒾⓇⓉⒽ ⒹⒶⓎ░ ╗(◕‿◕✿)" target="_blank">
                                <%  item.RenderUserPicture(Writer, new { @class = "", @style = "width:40px" }); %><%= item.RealName %>
                                <div class="email">
                                <%  if (item.Birthday.Value.DayOfYear == DateTime.Today.DayOfYear)
                                    {   %>
                                    <label class="label bg-color-red">今天生日喔 <i class="fa fa-birthday-cake"></i></label>
                                <%  }
                                    else
                                    { %>
                                    <%= String.Format("{0:M/d}",item.Birthday) %>生日喔！
                                <%  } %>    
                                </div>
                            </a>
                        </div>
            <%  } %>
            <!-- end content -->
        </div>
        <!-- end widget content -->
    </div>
    <!-- end widget div -->
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;
    IQueryable<UserProfile> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _lessonDate = DateTime.Today;   //(DateTime?)this.Model;

        _items = models.GetTable<UserProfile>().Where(u => u.Birthday.HasValue
                && u.Birthday.Value.DayOfYear >= _lessonDate.Value.DayOfYear
                && u.Birthday.Value.DayOfYear <= _lessonDate.Value.AddDays(7).DayOfYear)
            .OrderBy(u => u.Birthday.Value.DayOfYear);

    }

</script>

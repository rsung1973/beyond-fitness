<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="widget-body bg-color-darken txt-color-white no-padding smart-form">
    <!-- content goes here -->
    <h5 class="todo-group-title"><i class="fa fa-exclamation"></i>生日提醒 (<small class="num-of-tasks"><%= _items.Count() %></small>)</h5>
    <%  if (_items.Count() > 0)
        { %>
            <ul id="sortable2" class="todo">
            <%  foreach (var item in _items)
                { %>
                    <li>
                        <span class="handle"><i class="fa fa-birthday-cake fa-2x text-warning"></i>&nbsp;&nbsp;</span>
                        <p>
                            <strong><%= item.RealName %>今天長尾巴喔！</strong> 
                                                            <a href="http://line.naver.jp/R/msg/text/?LINE%E3%81%A7%E9%80%81%E3%82%8B%0D%0Ahttp%3A%2F%2Fline.naver.jp%2F">
                                                                <img src="../img/line/linebutton_36x60_zh-hant.png" width="36" height="60" alt="用LINE傳送" /></a>
                        </p>
                    </li>
            <%  } %>
            </ul>
    <%  } %>
</div>

<script>
    function showAttendee(lessonDate, hour) {
        pageParam.hour = hour;
        $('#loading').css('display', 'table');
        $('#attendeeList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingMembers") %>', { 'lessonDate': lessonDate, 'hour': hour }, function () {
            $('#loading').css('display', 'none');
        });
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;
    IEnumerable<UserProfile> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _lessonDate = DateTime.Today;   //(DateTime?)this.Model;

        _items = models.GetTable<UserProfile>().Where(u => u.Birthday.HasValue
            && u.Birthday.Value.Month == _lessonDate.Value.Month
            && u.Birthday.Value.Day == _lessonDate.Value.Day);

    }

</script>

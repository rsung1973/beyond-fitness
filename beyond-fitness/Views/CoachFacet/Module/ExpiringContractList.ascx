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

<div class="jarviswidget" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
    <header class="bg-color-red">
        <span class="widget-icon"><i class="fa fa-exclamation-triangle"></i></span>
        <h2>尚有<%= _items.Where(c=>c.Expiration>=DateTime.Today).Count() %>張合約即將過期，<%= _items.Where(c=>c.Expiration<DateTime.Today).Count() %>張已過期!</h2>
    </header>
    <!-- widget div-->
    <div>
        <!-- widget content -->
        <div class="widget-body txt-color-white padding-5">
            <!-- content -->
            <table id="list_dt" class="table table-striped table-bordered table-hover" width="100%">
                <thead>
                    <tr>
                        <th>合約編號</th>
                        <th class="text-center">學員</th>
                        <th class="text-center">簽約體能顧問</th>
                        <th class="text-center">到期日</th>
                        <th class="text-center">是否過期</th>
                    </tr>
                </thead>
                <tbody>
                    <%  foreach (var item in _items)
                        { %>
                    <tr>
                        <td><%= item.ContractNo() %></td>
                        <td class="text-center"><%= String.Join("/",item.CourseContractMember.Select(m=>m.UserProfile).ToArray().Select(u=>u.FullName())) %></td>
                        <td class="text-center"><%= item.ServingCoach.UserProfile.FullName() %></td>
                        <td class="text-center"><%= String.Format("{0:yyyy/MM/dd}",item.Expiration) %></td>
                        <td class="text-center"><%= item.Expiration<DateTime.Today ? "是" : "否" %></td>
                    </tr>
                    <%  } %>
                </tbody>
            </table>
            <!-- end content -->
        </div>
        <!-- end widget content -->
    </div>
    <!-- end widget div -->
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    IQueryable<CourseContract> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        var profile = Context.GetUser();
        _items = models.GetTable<CourseContract>()
            .Where(c => !c.RegisterLessonContract.Any(r => r.RegisterLesson.Attended == (int)Naming.LessonStatus.課程結束))
            .Where(c => c.Expiration < DateTime.Today.AddMonths(1));
        if (profile.IsAssistant())
        {

        }
        else if (profile.IsManager() || profile.IsViceManager())
        {
            var coaches = profile.GetServingCoachInSameStore(models);
            _items = _items.Join(coaches, c => c.FitnessConsultant, h => h.CoachID, (c, h) => c);
        }
        else if (profile.IsCoach())
        {
            _items = _items.Where(c => c.FitnessConsultant == profile.UID);
        }
    }

</script>

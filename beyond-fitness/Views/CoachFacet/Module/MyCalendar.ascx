<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="calendarView" class="jarviswidget" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
    <header>
        <span class="widget-icon"><i class="fa fa-calendar"></i></span>
        <h2><span id="coachName"><%= _coach==null ? "全部練教" : _coach.CoachID==_model.UID ? "我" : _coach.UserProfile.RealName %></span>的行事曆：<span id="branchName"><%  Html.RenderPartial("~/Views/SystemInfo/BranchStoreText.ascx", _viewModel.BranchID); %></span></h2>
        <div class="widget-toolbar">
<%--            <div class="btn-group">
                <button class="btn dropdown-toggle btn-xs bg-color-blue" data-toggle="dropdown">
                    新增行事曆 <i class="fa fa-caret-down"></i>
                </button>
                <ul class="dropdown-menu pull-right">
                    <li>
                        <a href="addBeyondFitnessEvent.html"><i class="fa fa-fw fa-users"></i>BeyondFitness的行事曆</a>
                    </li>
                    <li>
                        <a href="addPersonalEvent.html"><i class="fa fa-fw fa-user-o"></i>自己的行事曆</a>
                    </li>
                </ul>
            </div>--%>
            <div class="btn-group">
                <button class="btn dropdown-toggle btn-xs btn-warning" data-toggle="dropdown">
                    上課地點 <i class="fa fa-caret-down"></i>
                </button>
                <ul class="dropdown-menu pull-right">
                    <li>
                        <a href="javascript:selectBranch(null,'全部');">全部</a>
                    </li>
                    <%  foreach (var b in models.GetTable<BranchStore>())
                        { %>
                    <li>
                        <a href="javascript:selectBranch(<%= b.BranchID %>,'<%= b.BranchName %>');"><%= b.BranchName %></a>
                    </li>
                    <%  } %>
                </ul>
            </div>
            <div class="btn-group">
                <button class="btn dropdown-toggle btn-xs bg-color-pinkDark" data-toggle="dropdown">
                    體能顧問 <i class="fa fa-caret-down"></i>
                </button>
                <ul class="dropdown-menu pull-right">
                    <li>
                        <a href="javascript:selectCoach(null,'全部');">全部</a>
                    </li>
                    <%  if (_model.IsAssistant() || _model.IsManager() || _model.IsViceManager())
                        {
                            IQueryable<ServingCoach> items = models.GetTable<ServingCoach>();
                            if(_model.IsManager() || _model.IsViceManager())
                            {
                                items = items
                                        .Join(models.GetTable<BranchStore>().Where(b => b.ManagerID == _model.UID || b.ViceManagerID == _model.UID)
                                            .Join(models.GetTable<CoachWorkplace>(),
                                                b => b.BranchID, w => w.BranchID, (b, w) => w),
                                            s => s.CoachID, w => w.CoachID, (s, w) => s);
                            }
                            foreach (var item in items)
                            { %>
                                <li>
                                    <a href="javascript:selectCoach(<%= item.CoachID %>,'<%= item.CoachID == _model.UID ? "我" : item.UserProfile.RealName %>');"><%= item.UserProfile.RealName %></a>
                                </li>
                    <%      }
                        }
                        else
                        {   %>
                            <li>
                                <a href="javascript:selectCoach(<%= _model.UID %>,'我');"><%= _model.RealName %></a>
                            </li>
                    <%  } %>
                </ul>
            </div>
            <a onclick="queryAttendee();" class="btn bg-color-blueLight" id="stduentsaerchDialog_link"><i class="fa fa-fw fa-search"></i>學員查詢</a>
        </div>
    </header>
    <!-- widget div-->
    <%  Html.RenderAction("CoachCalendar","CoachFacet",new FullCalendarViewModel { DefaultDate = DateTime.Today,DefaultView = "agendaWeek" }); %>
    <!-- end widget div -->
</div>
<script>
    function selectCoach(coachID, coachName) {
        $('#coachName').text(coachName);
        $global.viewModel.CoachID = coachID;
        $global.viewModel.QueryType = 'default';
        showLoading();
        //window.location.href = '<%= Url.Action("Index","CoachFacet") %>' + "?" + $.param($global.viewModel);
        $global.renderFullCalendar();
        if (coachID) {
            $('#coachToday').load('<%= Url.Action("CoachToday","CoachFacet") %>', { 'coachID': coachID }, function (data) {

            });
        } else {
            $('#coachToday').empty();
        }
    }

    function selectBranch(branchID, branchName) {
        $('#branchName').text(branchName);
        $global.viewModel.BranchID = branchID;
        $global.renderFullCalendar();
    }

    function queryAttendee() {
        showLoading();
        $.post('<%= Url.Action("QueryAttendee","CoachFacet") %>', {}, function (data) {
            hideLoading();
            $(data).appendTo($('body'));
        });
    }

    function selectAttendee(uid,userName) {
        $global.viewModel.UID = uid;
        $('#coachName').text(userName);
        $global.viewModel.QueryType = 'attendee';
        $global.renderFullCalendar();
    }

</script>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DailyBookingQueryViewModel _viewModel;
    UserProfile _model;
    ServingCoach _coach;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _viewModel = (DailyBookingQueryViewModel)ViewBag.ViewModel;
        _model = (UserProfile)this.Model;
        _coach = (ServingCoach)ViewBag.CurrentCoach;
    }

</script>

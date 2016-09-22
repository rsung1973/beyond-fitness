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

<div class="jarviswidget" id="wid-id-user" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false">

    <header>
        <ul id="widget-tab-user" class="nav nav-tabs pull-right">
            <%  int idx = 0;
                foreach (var item in _groups)
                {
                    idx++;%>
            <li class='<%= idx==1 ? "active" : null %>'><a id='<%= "toggleTab-" + idx %>' href='<%= "#tab-" + idx %>' role="tab" data-toggle="tab"><i class="<%= item.UserProfile.UserProfileExtension.Gender=="F" ? "fa fa-female" : "fa fa-male" %>"></i> <span><%= item.UserProfile.RealName %></span></a></li>
            <%  } %>
        </ul>

    </header>
    <!-- widget div-->
    <div class="no-padding">
        <div class="widget-body no-padding">
            <!-- content -->
            <div id="myTabContent-user" class="tab-content padding-10">
                <%  idx = 0;
                    ViewBag.LessonTime = _model;
                    foreach (var item in _groups)
                    {
                        idx++;%>
                        <div class='<%= idx==1 ? "tab-pane fade in active widget-body no-padding-bottom" : "tab-pane fade" %>' id='<%= "tab-" + idx %>'>
                            <% Html.RenderPartial("~/Views/Lessons/LearnerLessonPlan.ascx",item); %>
                        </div>
                <%  } %>
            </div>

            <!-- end content -->
        </div>

    </div>
    <!-- end widget div -->
</div>

<script>
    function flipflop() {
        var event = event || window.event;
        var $this = $(event.target);
        if ($this.hasClass('openureye')) {
            var $p = $this.parent().parent().next();
            $p.hide().next().show();//.next().show();
            $('p.secret-info').show();
            $this.removeClass('fa-eye openureye');
            $this.addClass('fa-eye-slash closeureye');
        } else {
            var $p = $this.parent().parent().next();
            $p.show().next().hide();//.next().hide();
            $('p.secret-info').hide();
            $this.removeClass('fa-eye-slash closeureye');
            $this.addClass('fa-eye openureye');
        }
    }

    function editRecentStatus(uid) {
        var event = event || window.event;
        $p_status = $(event.target).parent().prev();
        $.post('<%= Url.Action("EditRecentStatus","Lessons") %>', { 'uid': uid }, function (data) {
            $(data).appendTo($('#content'));
        });
    }


    var $p_status;
    function commitRecentStatus(uid, recentStatus) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitRecentStatus") %>', { 'uid': uid, 'recentStatus': recentStatus }, function (data) {
            if (data) {
                if (data.result) {
                    $p_status.html(recentStatus.replace(/\n/g, '<br/>'));
                }
                smartAlert(data.message);
            }
        });
    }

</script>

<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonTime _model;
    List<RegisterLesson> _groups;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonTime)this.Model;
        if(_model.GroupID.HasValue)
        {
            _groups = _model.GroupingLesson.RegisterLesson.ToList();
        }
        else
        {
            _groups = new List<RegisterLesson>();
            _groups.Add(_model.RegisterLesson);
        }
    }

</script>

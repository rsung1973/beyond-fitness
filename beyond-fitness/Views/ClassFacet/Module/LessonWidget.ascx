<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>


<!-- widget content -->
<div class="widget-body txt-color-white no-padding">
    <div class="row no-padding">
        <!--profile-->
        <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">
            <%  ViewBag.LessonTime = _model;
                Html.RenderPartial("~/Views/ClassFacet/Module/LearnerLessonPlan.ascx", _lesson); %>
            <div class="row">
                <div class="col-sm-12">
                    <hr>
                    <div class="padding-5">
                        <ul class="nav nav-tabs tabs-pull-right">
                            <li class="active">
                                <a href="#calendar_tab" data-toggle="tab">行事曆</a>
                            </li>
                            <li>
                                <a href="#pdq_tab" data-toggle="tab">PDQ</a>
                            </li>
                            <li>
                                <a href="#questionaire_tab" data-toggle="tab">階段性調整計劃</a>
                            </li>
                            <li>
                                <a href="#chatlist_tab" data-toggle="tab">運動留言板</a>
                            </li>
                            <li>
                                <a id="smRowTab" href="#smRow" data-toggle="tab" class="hidden-md hidden-lg">課程內容</a>
                            </li>
                        </ul>
                        <div id="<%= _tabContent %>" class="tab-content padding-top-10">
                            <div class="tab-pane fade in active" id="calendar_tab">
                                <!-- new widget -->
                                <div class="jarviswidget" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
                                    <header>
                                        <span class="widget-icon"><i class="fa fa-calendar"></i></span>
                                        <h2><%= _lesson.UserProfile.FullName() %>行事曆</h2>
                                        <div class="widget-toolbar">
                                            <a href="#" class="btn  btn-primary" onclick="bookingByCoach('<%= DateTime.Today.ToString("yyyy-MM-dd") %>');">預約上課</a>
                                        </div>
                                    </header>
                                    <!-- widget div-->
                                    <div>
                                        <div class="widget-body bg-color-darken txt-color-white no-padding">
                                            <!-- content goes here -->
                                            <div class="widget-body-toolbar">

                                                <div id="calendar-buttons">

                                                    <div class="btn-group">
                                                        <a href="javascript:void(0)" class="btn btn-default btn-xs" id="btn-prev"><i class="fa fa-chevron-left"></i></a>
                                                        <a href="javascript:void(0)" class="btn btn-default btn-xs" id="btn-next"><i class="fa fa-chevron-right"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <% 
                                                Html.RenderPartial("~/Views/ClassFacet/Module/VipCalendar.ascx", _lesson); %>
                                            <!-- end content -->
                                        </div>
                                    </div>
                                    <!-- end widget div -->
                                </div>
                                <!-- end widget -->
                            </div>
                            <div class="tab-pane fade" id="pdq_tab">
                                <%  Html.RenderPartial("~/Views/ClassFacet/Module/ShowPAQPDQ.ascx", _lesson.UserProfile); %>
                            </div>
                            <div class="tab-pane fade" id="questionaire_tab">
                                <%  Html.RenderPartial("~/Views/ClassFacet/Module/RecentQuestionnaire.ascx", _lesson.UserProfile); %>
                            </div>
                            <div class="tab-pane fade" id="chatlist_tab">
                                <div class="col-md-12 block block-drop-shadow" id="chatboard">
                                    <%  Html.RenderPartial("~/Views/CoachFacet/Module/LessonCommentList.ascx",_lesson.UserProfile); %>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="smRow">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end profile-->
        <!--content-->
        <div class="col-md-7 col-lg-7 hidden-xs hidden-sm" id="lgRow">
            <div class="padding-5" id="currentContent">
                <%  ViewBag.LessonTime = _model; %>
                <%  Html.RenderPartial("~/Views/ClassFacet/Module/LessonContent.ascx", _lesson); %>
            </div>
        </div>
        <!--end content -->
    </div>
</div>
<!-- end widget content -->
<script>
    $(function () {
        $global.cloneLesson = function(sourceID) {
            showLoading();
            $.post('<%= Url.Action("CloneTrainingPlan","Lessons") %>',{'sourceID': sourceID,'lessonID':<%= _model.LessonID %>},function(data){
                hideLoading();
                if(data.result) {
                    alert("資料已複製!!");
                    showLoading();
                    $('#currentContent').load('<%= Url.Action("LessonContent","ClassFacet",new { lessonID = _model.LessonID, registerID = _lesson.RegisterID }) %>',{},function(data){
                        hideLoading();
                    });
                } else {
                    alert(data.message);
                }
            });
        };

        if ($('#smRowTab').css('display') == 'block') {
<%--            $('ul.nav-tabs.tabs-pull-right li.active').removeClass('active');
            $('ul.nav-tabs.tabs-pull-right li:eq(4)').addClass('active');
            $('#<%= _tabContent %> div.active.in').removeClass('active in');
            $('#<%= _tabContent %> div:eq(4)').addClass('active in');--%>
            $('#currentContent').appendTo($('#smRow'));
            console.log($('#currentContent').css('display'));

        } else if ($('#lgRow').css('display') == 'block') {

        }

        $(window).resize(function () {
            if ($('#smRowTab').css('display') == 'block') {
                if($('#smRow').find('#currentContent').length==0) {
<%--                    $('ul.nav-tabs.tabs-pull-right li.active').removeClass('active');
                    $('ul.nav-tabs.tabs-pull-right li:eq(4)').addClass('active');
                    $('#<%= _tabContent %> div.active.in').removeClass('active in');
                    $('#<%= _tabContent %> div:eq(4)').addClass('active in');--%>
                    $('#currentContent').appendTo($('#smRow'));
                }
            } else if ($('#lgRow').css('display') == 'block') {
                if($('#lgRow').find('#currentContent').length==0) {
                    $('#currentContent').appendTo($('#lgRow'));
                    $('ul.nav-tabs.tabs-pull-right li.active').removeClass('active');
                    $('ul.nav-tabs.tabs-pull-right li:eq(0)').addClass('active');
                    $('#<%= _tabContent %> div.active.in').removeClass('active in');
                    $('#<%= _tabContent %> div:eq(0)').addClass('active in');
                }
            }
        });

    });

    function bookingByCoach(lessonDate) {
        showLoading();
        $.post('<%= Url.Action("BookingByCoach","ClassFacet",new { UID = _lesson.UID, CoachID= _lesson.AdvisorID }) %>',{'lessonDate':lessonDate},function(data){
            hideLoading();
            if(data) {
                var $dialog = $(data);
                $dialog.dialog({
                    width: "80%",
                    height: "auto",
                    resizable : true,
                    modal : true,
                    closeText: "關閉",
                    title : "<div class='modal-title'><h4><i class='fa fa-warning'></i> 預約課程</h4></div>",
                    buttons : [{
                        html : "<i class='fa fa-send'></i>&nbsp;確定",
                        "class" : "btn btn-primary",
                        click : function() {
                            var f = function() {  
                                $('#calendar').fullCalendar('refetchEvents');
                                $dialog.dialog("close");
                            };
                            $global.commitBooking(f);
                        }
                    }],
                    close: function (evt, ui) {
                        $dialog.remove();
                    }
                });
            }
        });
    }
</script>


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    RegisterLesson _lesson;
    String _tabContent = "tabContent" + DateTime.Now.Ticks;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        if (ViewBag.RegisterID != null)
        {
            _lesson = _model.GroupingLesson.RegisterLesson.Where(r => r.RegisterID == (int?)ViewBag.RegisterID).First();
        }
        else
        {
            _lesson = _model.GroupingLesson.RegisterLesson.First();
        }
    }

</script>

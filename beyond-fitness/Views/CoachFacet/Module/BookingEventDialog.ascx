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

<div id="<%= _dialogID %>" title="行事曆" class="bg-color-darken">
    <div class="panel panel-default bg-color-darken">
        <div class="panel-body status smart-form vote">
            <div class="who clearfix">
                <%  foreach (var item in _model.GroupingLesson.RegisterLesson)
                    {
                        item.UserProfile.RenderUserPicture(Writer, new { @class = "profileImg busy", @style = "width:40px" });  %>
                        <span class="name font-lg"><b><%= item.UserProfile.FullName() %></b></span><br />
                <%  }   %>
                <span class="from font-md text-warning"><%= _model.RegisterLesson.GroupingMemberCount>1
                                                                ? "團體課程"
                                                                : _model.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.已刪除 || _model.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.一般課程
                                                                    ? "P.T session"
                                                                    : _model.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.自主訓練
                                                                        ? "P.I session"
                                                                        :((Naming.LessonPriceStatus)_model.RegisterLesson.LessonPriceType.Status).ToString() %>：<%= _model.ClassTime.Value.ToString("yyyy/MM/dd HH:mm") %>~<%= _model.ClassTime.Value.AddMinutes(_model.DurationInMinutes.Value).ToString("HH:mm") %></span>
            </div>
        </div>
    </div>
    <script>
        $('#<%= _dialogID %>').dialog({
            resizable: true,
            modal: true,
            width: "auto",
            height: "auto",
            title: "<h4 class='modal-title'><i class='fa fa-fw fa-calendar'></i>  行事曆</h4>",
            buttons: [
    <%  if (_model.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自由教練預約)
        {   
            var expansion = _model.LessonTimeExpansion.First();%>
            {
                html: "<i class='fa fa-edit'></i>&nbsp;編輯",
                "class": "btn btn-primary",
                click: function () {
                    <%-- makeLessonPlan(<%= JsonConvert.SerializeObject(new
                                {
                                    classDate = expansion.ClassDate.ToString("yyyy-MM-dd"),
                                    hour = expansion.Hour,
                                    registerID = _model.RegisterID,
                                    lessonID = _model.LessonID
                                }) %>);
                    $(this).dialog("close");--%>
                    window.location.href = '<%= Url.Action("ClassIndex", "ClassFacet", new { lessonID = _model.LessonID }) %>';
                }
            },
<%      }
        if (couldBeCancelled())
        { %>
            {
                html: "<i class='fa fa-trash-o'></i>&nbsp;取消預約",
                "class": "btn bg-color-red",
                click: function () {
                    if (confirm('確定取消預約此課程?')) {
                        showLoading();
                        $.post('<%= Url.Action("RevokeBooking", "CoachFacet", new { lessonID = _model.LessonID }) %>', null, function (data) {
                            hideLoading();
                            $(data).appendTo('body').remove();
                            $('#<%= _dialogID %>').dialog("close");
                            $global.renderFullCalendar();
                        });
                    }
                    <%--                    confirmDialog({
                        title: '取消預約',
                        message: '確定取消預約此課程?',
                        confirm: function (evt) {
                            showLoading();
                            evt.dialog("close");
                            $.post('<%= Url.Action("RevokeBooking", "CoachFacet", new { lessonID = _model.LessonID }) %>', null, function (data) {
                                hideLoading();
                                $(data).appendTo('body').remove();
                                $('#<%= _dialogID %>').dialog("close");
                                $global.renderFullCalendar();
                            });
                        },
                    });--%>
                }
            }
    <%  }   %>
            ],
            close: function () {
                $('#<%= _dialogID %>').remove();
            }
        });
    </script>
</div>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    UserProfile _profile;
    String _dialogID;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;
        _profile = Context.GetUser();
        _dialogID = "modifyBookingDialog" + DateTime.Now.Ticks;
    }

    bool couldBeCancelled()
    {
        if (!_model.ContractTrustTrack.Any(s => s.SettlementID.HasValue))
        {
            if (_profile.IsAssistant() || _profile.IsAuthorizedSysAdmin())
                return true;
            if (_model.LessonAttendance == null && !_model.LessonPlan.CommitAttendance.HasValue && _model.ClassTime.Value >= DateTime.Today.AddDays(-3))
                return true;
        }
        return false;
    }

</script>

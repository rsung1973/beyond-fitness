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

<%  
    LessonTimeExpansion item = _model;
    if (item.LessonTime.LessonAttendance == null)
    { %>
        <a onclick='makeLessonPlan(<%= JsonConvert.SerializeObject(new
                                {
                                    classDate = item.ClassDate.ToString("yyyy-MM-dd"),
                                    hour = item.Hour,
                                    registerID = item.RegisterID,
                                    lessonID = item.LessonID
                                }) %>);'
            class="btn btn-system btn-small">預編課程 <i class="fa fa-edit" aria-hidden="true"></i></a>
<%  } 
    if (item.LessonTime.LessonAttendance == null && item.LessonTime.TrainingPlan.Count>0 )
    { %>
        <a onclick='attendLesson(<%= JsonConvert.SerializeObject(new
                                {
                                    classDate = item.ClassDate.ToString("yyyy-MM-dd"),
                                    hour = item.Hour,
                                    registerID = item.RegisterID,
                                    lessonID = item.LessonID
                                }) %>);'
            class="btn btn-system btn-small">上課囉 <i class="fa fa-heartbeat" aria-hidden="true"></i></a>
<%  }
    if (item.LessonTime.LessonAttendance == null )
    { %>
        <a onclick="revokeBooking(<%= item.LessonID %>);" class="btn btn-system btn-small">取消預約 <i class="fa fa-calendar-times-o" aria-hidden="true"></i></a>
<%  } %>
<%  if (item.LessonTime.LessonPlan != null)
    { %>
        <a onclick='previewLesson(<%= JsonConvert.SerializeObject(new
                    {
                        classDate = item.ClassDate.ToString("yyyy-MM-dd"),
                        hour = item.Hour,
                        registerID = item.RegisterID,
                        lessonID = item.LessonID
                    }) %>);'
        class="btn btn-system btn-small">檢視 <i class="fa fa-eye" aria-hidden="true"></i></a>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTimeExpansion _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTimeExpansion)this.Model;
    }

</script>

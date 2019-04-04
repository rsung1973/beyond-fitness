﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%= JsonConvert.SerializeObject(_result) %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    List<CalendarEventItem> _model;
    List<CalendarEvent> _result;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (List<CalendarEventItem>)this.Model;
        _userProfile = Context.GetUser();
        _result = new List<CalendarEvent>();

        foreach(var item in _model)
        {
            if (item.EventItem is LessonTime)
            {
                var g = (LessonTime)item.EventItem;
                _result.Add(new CalendarEvent
                {
                    id = g.LessonID.ToString(),
                    keyID = g.LessonID.EncryptKey(),
                    title = g.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.教練PI
                        ? g.GroupingLesson.RegisterLesson.Where(r=>r.MasterRegistration==true)
                            .Select(r => r.UserProfile).FirstOrDefault()?.RealName
                        : String.Join("、", g.GroupingLesson.RegisterLesson.Select(r => r.UserProfile.RealName))
                            + (g.PreferredLessonTime!=null && !g.PreferredLessonTime.ApprovalDate.HasValue ? "(待審核)" : ""),
                    start = String.Format("{0:O}", g.ClassTime),
                    end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                    //description = "自由教練",
                    allDay = false,
                    className = g.PreferredLessonTime!=null && !g.PreferredLessonTime.ApprovalDate.HasValue
                    ? new String[] { "b-l b-2x b-approve" }
                    : g.LessonAttendance != null
                        ? new String[] { "b-l b-2x b-finish" }
                        : g.IsPTSession() || g.RegisterLesson.LessonPriceType.IsWelfareGiftLesson != null
                            ? new string[] { "b-l b-2x b-PT" }
                            : g.IsPISession()
                                ? new string[] { "b-l b-2x b-PI" }
                                : g.IsTrialLesson()
                                    ? new string[] { "b-l b-2x b-PE" }
                                    : g.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.教練PI
                                        ? new String[] { "b-l b-2x b-CoachPI" }
                                            : g.IsSTSession()
                                                ? new string[] { "b-l b-2x b-ST" }
                                                : null,
                    editable = g.LessonAttendance == null,
                    icon = g.LessonAttendance != null
                            ? g.LessonPlan.CommitAttendance.HasValue
                                ? "zmdi zmdi-check-all"
                                : "zmdi zmdi-check"
                            : g.LessonPlan.CommitAttendance.HasValue
                                ? "zmdi zmdi-check"
                                : null,
                });
            }
            else if (item.EventItem is UserEvent)
            {
                var g = (UserEvent)item.EventItem;
                _result.Add(new CalendarEvent
                {
                    id = "my" + g.EventID,
                    title = g.Title ?? g.ActivityProgram,
                    start = String.Format("{0:O}", g.StartDate),
                    end = String.Format("{0:O}", g.EndDate),
                    description = g.Title == null ? "" : g.ActivityProgram,
                    keyID = g.EventID.EncryptKey(),
                    allDay = false,
                    editable = g.UID == _userProfile.UID,
                    className = new string[] { "b-l b-2x b-custom" },  //g.StartDate < today ? g.EndDate < today ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pink" },
                    icon = ""   //"fa-magic"
                });
            }

        }

        foreach (var g in _model.Where(v => v.EventItem is LessonTime)
            .Select(i => (LessonTime)i.EventItem)
            .Where(l => l.PreferredLessonTime == null || l.PreferredLessonTime.ApproverID.HasValue)
            .GroupBy(l => ((LessonTime)l).ClassTime.Value.Date))
        {
            var totalItems = g.ToList();
            var items = totalItems.TrialLesson();
            if (items.Count() > 0)
            {
                totalItems = totalItems.Except(items).ToList();
                _result.Add(new CalendarEvent
                {
                    id = "course",
                    title = items.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "T.S",
                    allDay = true,
                    //className = !items.Any(l => l.LessonAttendance == null) ? new string[] { "b-l b-2x b-PE" } : new string[] { "b-l b-2x b-finish" },
                    className = new string[] { "b-l b-2x b-PE" },
                });
            }

            items = totalItems.Where(l => l.TrainingBySelf == 1);
            if (items.Count() > 0)
            {
                totalItems = totalItems.Except(items).ToList();
                _result.Add(new CalendarEvent
                {
                    id = "course",
                    title = items.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "P.I",
                    allDay = true,
                    //className = !items.Any(l => l.LessonAttendance == null) ? new string[] { "b-l b-2x b-PI" } : new string[] { "b-l b-2x b-finish" },
                    className = new string[] { "b-l b-2x b-PI" },
                });
            }

            items = totalItems.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.教練PI);
            if (items.Count() > 0)
            {
                totalItems = totalItems.Except(items).ToList();
                _result.Add(new CalendarEvent
                {
                    id = "coach",
                    title = items.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "Coach P.I",
                    allDay = true,
                    //className = !items.Any(l => l.LessonAttendance == null) ? new string[] { "b-l b-2x b-CoachPI" } : new string[] { "b-l b-2x b-finish" },
                    className = new string[] { "b-l b-2x b-CoachPI" },
                });
            }

            items = totalItems.Where(l => l.TrainingBySelf == 2);
            if (items.Count() > 0)
            {
                totalItems = totalItems.Except(items).ToList();
                _result.Add(new CalendarEvent
                {
                    id = "course",
                    title = items.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "S.T",
                    allDay = true,
                    //className = !items.Any(l => l.LessonAttendance == null) ? new string[] { "b-l b-2x b-ST" } : new string[] { "b-l b-2x b-finish" },
                    className = new string[] { "b-l b-2x b-ST" },
                });
            }

            if (totalItems.Count > 0)
            {
                _result.Add(new CalendarEvent
                {
                    id = "course",
                    title = totalItems.Count.ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "P.T",
                    allDay = true,
                    //className = g.Key >= DateTime.Today ? new string[] { "b-l b-2x b-PT" } : new string[] { "b-l b-2x b-finish" },
                    className = new string[] { "b-l b-2x b-PT" },
                });
            }

        }

    }


</script>

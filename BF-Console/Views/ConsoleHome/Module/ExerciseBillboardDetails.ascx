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
<%@ Import Namespace="Newtonsoft.Json.Linq" %>
<%= JsonConvert.SerializeObject(result) %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    IQueryable<LessonTime> _model;
    ExerciseBillboardQueryViewModel _viewModel;
    DateTime compareDateFrom, compareDateTo;
    object result;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<LessonTime>)this.Model;
        _viewModel = (ExerciseBillboardQueryViewModel)ViewBag.ViewModel;

        compareDateFrom = DateTime.Today.FirstDayOfMonth().AddMonths(-1);
        compareDateTo = DateTime.Today.AddMonths(-1).AddDays(1);

        var items = _model.GroupBy(l => l.RegisterLesson.UID)
            .Select(g => new
            {
                g.Key,
                TotalMinutes = (decimal?)g.Sum(l => l.DurationInMinutes),
            }).ToArray();

        List<String[]> resultData = new List<string[]>();

        int idx = 1;
        foreach (var i in items.GroupBy(g => g.TotalMinutes).OrderByDescending(g => g.Key))
        {
            foreach (var v in i.OrderByDescending(m => m.Key))
            {
                resultData.Add(new string[] {
                    buildParticipant(v.Key),
                    workPlace(v.Key),
                    buildScore(v.Key,v.TotalMinutes),
                    buildAnalysis(v.Key),
                    $"<span class='{(idx==1 ? "badge bg-red" : "badge badge-info")}'>{idx}</span>"
                });
            }
            idx++;
        }

        result = new {
            data = resultData.ToArray()
        };

    }

    String buildParticipant(int uid)
    {
        var u = models.GetTable<UserProfile>().Where(p => p.UID == uid).First();
        return $"<span class='list-icon'><img class='patients-img' src='{buildPhoto(u)}'></span> <span class='hidden-sm-down'>{u.RealName}({u.Nickname})</span>";
    }

    String buildPhoto(UserProfile profile)
    {
        return profile.PictureID.HasValue
                              ? VirtualPathUtility.ToAbsolute("~/Information/GetResource/") + profile.PictureID + "?stretch=true"
                              : "images/avatars/noname.png";
    }

    String workPlace(int uid)
    {
        var workPlace = models.GetTable<CoachWorkplace>().Where(p => p.CoachID == uid);
        var w = workPlace.FirstOrDefault();
        if (w == null || workPlace.Count() > 1)
        {
            return null;
        }
        return w.BranchStore.BranchName;
    }

    String buildScore(int uid,decimal? totalMinutes)
    {
        var lessons = models.GetTable<RegisterLesson>().Where(r => r.UID == uid);
        var totalMinutesLastMonth = models.PromptMemberExerciseLessons(lessons)
                .Where(l => l.ClassTime >= compareDateFrom && l.ClassTime < compareDateTo)
                .Sum(l => l.DurationInMinutes) ?? 0;
        if (totalMinutes > totalMinutesLastMonth)
        {
            return $"{totalMinutes/60:.}:{totalMinutes%60:00} <i class='zmdi zmdi-caret-up zmdi-hc-2x text-danger float-right'></i>";
        }
        else if(totalMinutes < totalMinutesLastMonth)
        {
            return $"{totalMinutes/60:.}:{totalMinutes%60:00} <i class='zmdi zmdi-caret-down zmdi-hc-2x text-success float-right'></i>";
        }
        else
        {
            return $"{totalMinutes/60:.}:{totalMinutes%60:00}";
        }
    }

    String buildAnalysis(int uid)
    {
        var lessons = models.PromptMemberExerciseRegisterLesson().Where(f => f.UID == uid)
           .TotalLessons(models)
           .Where(l => l.ClassTime >= compareDateFrom && l.ClassTime < compareDateTo)
           .Join(models.GetTable<TrainingPlan>(), l => l.LessonID, p => p.LessonID, (l, p) => p)
           .Join(models.GetTable<TrainingExecution>(), p => p.ExecutionID, x => x.ExecutionID, (p, x) => x)
           .Join(models.GetTable<TrainingExecutionStage>(), x => x.ExecutionID, s => s.ExecutionID, (x, s) => s);

        var items = models.GetTable<TrainingStage>()
                .Select(s => new
                {
                    s.Stage,
                    TotalMinutes = lessons.Where(t => t.StageID == s.StageID).Sum(t => t.TotalMinutes) ?? 0
                }).ToArray();

        var total = items.Sum(t => t.TotalMinutes);

        if (total == 0)
            total = 1m;

        return $"<div class='sparkline-pie'>{JsonArrayValue(JsonConvert.SerializeObject(items.Select(t => Math.Round(t.TotalMinutes * 100 / total)).ToArray()))}</div>";

    }

    String JsonArrayValue(String jsonValue)
    {
        return jsonValue != null ? jsonValue.Substring(1, jsonValue.Length - 2) : null;
    }



</script>

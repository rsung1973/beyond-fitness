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
<%= JsonConvert.SerializeObject(result) %>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    IQueryable<LessonTime> _model;
    AchievementQueryViewModel _viewModel;
    Object result;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (IQueryable<LessonTime>)this.Model;
        _viewModel = (AchievementQueryViewModel)ViewBag.ViewModel;

        var items = _model.GroupBy(l=>l.AttendingCoach);
        result = new
        {
            labels = items.Join(models.GetTable<UserProfile>(),g => g.Key,u=>u.UID,(g,u)=>u).Select(u=>u.RealName).ToArray(),
            datasets = new object[]
            {
                new
                {
                    type= "bar",
                    label= "P.T",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(184,227,243,.43)",
                    data= items.Select(g=>
                                    g.PTLesson().Count()).ToArray(),
                },
                new
                {
                    type= "bar",
                    label= "P.I",
                    yAxisID= "y-axis-0",
                    backgroundColor= "rgba(255,7,7,.43)",
                    data= items.Select(g=>
                                    g.Where(l => l.TrainingBySelf == 1).Count()).ToArray(),
                }
            }
        };
    }

</script>

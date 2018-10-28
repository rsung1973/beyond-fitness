﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  var lessonItems = _model.Where(v => v.EventItem is LessonTime).Select(i => (LessonTime)i.EventItem).ToList();
    var eventItems = _model.Where(v => v.EventItem is UserEvent).Select(i => (UserEvent)i.EventItem);
    var items = lessonItems.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.教練PI);
    if(items.Count()>0)
    {
        lessonItems = lessonItems.Except(items).ToList();    %>
<hr/>
        <%  foreach (var item in items.OrderBy(l=>l.ClassTime))
            {   %>
<div class="event-name b-CoachPI row">
    <div class="col-2 text-center">
        <h4><%= $"{item.ClassTime:dd}" %><span><%= $"{item.ClassTime:MM}" %></span><span><%= Naming.DayOfWeek[(int) item.ClassTime.Value.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6><%= item.ClassTime.Value.ToString("HH:mm") %>-<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %> <%= item.RegisterLesson.UserProfile.FullName() %></h6>
        <p class="col-blush"><%= item.TrainingPlan.FirstOrDefault()?.TrainingExecution.Emphasis %></p>
        <address><i class="zmdi zmdi-pin"></i><%= item.BranchStore.BranchName %></address>
    </div>
</div>
<%      } %>
<%  }    %>
<%  
    items = lessonItems.TrialLesson();
    if (items.Count() > 0)
    {
        lessonItems = lessonItems.Except(items).ToList();
    %>
<hr />
        <%  foreach (var item in items.OrderBy(l=>l.ClassTime))
            {   %>
<div class="event-name b-PE row">
    <div class="col-2 text-center">
        <h4><%= $"{item.ClassTime:dd}" %><span><%= $"{item.ClassTime:MM}" %></span><span><%= Naming.DayOfWeek[(int) item.ClassTime.Value.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6><%= item.ClassTime.Value.ToString("HH:mm") %>-<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %> <%= item.RegisterLesson.UserProfile.FullName() %>
            <%  Html.RenderPartial("~/Views/ConsoleHome/Module/RenderLessonCheck.ascx", item); %>
        </h6>
        <p class="col-blush"><%= item.TrainingPlan.FirstOrDefault()?.TrainingExecution.Emphasis %></p>
        <address><i class="zmdi zmdi-pin"></i><%= item.BranchStore.BranchName %></address>
    </div>
</div>
        <%  }    %>
<%  } %>
<%  
    items = lessonItems.Where(l => l.TrainingBySelf == 1);
    if (items.Count() > 0)
    {
        lessonItems = lessonItems.Except(items).ToList();
    %>
<hr />
        <%  foreach (var item in items.OrderBy(l=>l.ClassTime))
            {   %>
<div class="event-name b-PI row">
    <div class="col-2 text-center">
        <h4><%= $"{item.ClassTime:dd}" %><span><%= $"{item.ClassTime:MM}" %></span><span><%= Naming.DayOfWeek[(int) item.ClassTime.Value.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6><%= item.ClassTime.Value.ToString("HH:mm") %>-<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %> <%= item.RegisterLesson.UserProfile.FullName() %>
            <%  Html.RenderPartial("~/Views/ConsoleHome/Module/RenderLessonCheck.ascx", item); %>
        </h6>
        <p class="col-blush"><%= item.TrainingPlan.FirstOrDefault()?.TrainingExecution.Emphasis %></p>
        <address><i class="zmdi zmdi-pin"></i><%= item.BranchStore.BranchName %></address>
    </div>
</div>
        <%  }    %>
<%  } %>
<%  
    items = lessonItems.Where(l => l.TrainingBySelf == 2);
    if (items.Count() > 0)
    {
        lessonItems = lessonItems.Except(items).ToList();
    %>
<hr />
        <%  foreach (var item in items.OrderBy(l=>l.ClassTime))
            {   %>
<div class="event-name b-ST row">
    <div class="col-2 text-center">
        <h4><%= $"{item.ClassTime:dd}" %><span><%= $"{item.ClassTime:MM}" %></span><span><%= Naming.DayOfWeek[(int) item.ClassTime.Value.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6><%= item.ClassTime.Value.ToString("HH:mm") %>-<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %> <%= item.RegisterLesson.UserProfile.FullName() %>
            <%  Html.RenderPartial("~/Views/ConsoleHome/Module/RenderLessonCheck.ascx", item); %>
        </h6>
        <p class="col-blush"><%= item.TrainingPlan.FirstOrDefault()?.TrainingExecution.Emphasis %></p>
        <address><i class="zmdi zmdi-pin"></i></address>
    </div>
</div>
        <%  }    %>
<%  } %>
<%  
    items = lessonItems;
    if (items.Count() > 0)
    {
    %>
<hr />
        <%  foreach (var item in items.OrderBy(l=>l.ClassTime))
            {   %>
<div class="event-name b-PT row">
    <div class="col-2 text-center">
        <h4><%= $"{item.ClassTime:dd}" %><span><%= $"{item.ClassTime:MM}" %></span><span><%= Naming.DayOfWeek[(int) item.ClassTime.Value.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6><%= item.ClassTime.Value.ToString("HH:mm") %>-<%= item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value).ToString("HH:mm") %> <%= String.Join("、", item.GroupingLesson.RegisterLesson.Select(r => r.UserProfile.RealName)) %>
            <%  Html.RenderPartial("~/Views/ConsoleHome/Module/RenderLessonCheck.ascx", item); %>
        </h6>
        <p class="col-blush"><%= item.TrainingPlan.FirstOrDefault()?.TrainingExecution.Emphasis %></p>
        <address><i class="zmdi zmdi-pin"></i><%= item.BranchStore.BranchName %></address>
    </div>
</div>
        <%  }    %>
<%  } %>
<%  if (eventItems.Count() > 0)
    {   %>
<hr/>
<%      foreach(var item in eventItems)
        {   %>
<div class="event-name b-custom row">
    <div class="col-2 text-center">
        <h4><%= $"{item.StartDate:MM}" %><span><%= $"{item.StartDate:dd}" %></span><span><%= Naming.DayOfWeek[(int)item.StartDate.DayOfWeek] %></span></h4>
    </div>
    <div class="col-10">
        <h6>
            <%  if ((item.EndDate - item.StartDate).TotalDays >= 1)
                {   %>
            <%= item.StartDate.ToString("yyyy/MM/dd") %>-<%= item.EndDate.ToString("yyyy/MM/dd") %>
            <%  }
                else
                {   %>
            <%= item.StartDate.ToString("HH:mm") %>-<%= item.EndDate.ToString("HH:mm") %>
            <%  } %>
            <%= item.Title %></h6>
        <p class="col-blush"><%= item.ActivityProgram %></p>
        <address><i class="zmdi zmdi-pin"></i><%= item.BranchID.HasValue ? item.BranchStore.BranchName : "其他" %></address>
    </div>
</div>
<%      }
    }   %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    List<CalendarEventItem> _model;
    DateTime? startDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (List<CalendarEventItem>)this.Model;
    }


</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Helper.DataOperation" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<%  CourseContract currentItem = models.GetTable<CourseContractRevision>()
                                    .Where(c => c.OriginalContract == _model.ContractID)
                                    .Where(r => r.Reason == "轉讓")
                                    .Select(r => r.CourseContract)
                                    .Where(c => c.Status < (int)Naming.CourseContractStatus.已生效)
                                    .FirstOrDefault();
    var bookingCount = _model.CourseContractType.ContractCode == "CFA"
            ? _model.RegisterLessonContract.Sum(c => c.RegisterLesson.GroupingLesson.LessonTime.Count())
            : _model.RegisterLessonContract.First().RegisterLesson.GroupingLesson.LessonTime.Count;
    var contractCost = _model.TotalCost / _model.Lessons * bookingCount;
    var totalPaid = _model.TotalPaidAmount();
    bool applicable = true; %>
<div class="body">
    <div class="row">
        <div class="col-8">
            <h5 class="m-t-0">轉讓</h5>
            <%  if (_model.Status.In((int)Naming.CourseContractStatus.已過期, (int)Naming.CourseContractStatus.已終止))
                {
                    applicable = false; %>
            <span class="col-red"><i class="zmdi zmdi-block"></i>合約<%= (Naming.CourseContractStatus)_model.Status %></span><br />
            <%  }
                if (_model.IsContractServiceInProgress(models))
                {   
                    applicable = false; %>
            <span class="col-red"><i class="zmdi zmdi-block"></i>服務申請進行中</span><br />
            <%  }   %>
            <%  if (_model.ContractType != 1)
                { 
                    applicable = false; %>
            <span class="col-red"><i class="zmdi zmdi-block"></i>非1對1合約禁止轉讓</span><br />
            <%  } %>
            <%  if (totalPaid < _model.TotalCost)
                { 
                    applicable = false; %>
            <span class="col-red"><i class="zmdi zmdi-block"></i>費用未結清</span><br />
            <%  } %>
        </div>
        <div class="col-4 text-right">
            <a href="javascript:void();">
                <h2 class="col-blue"><%= _model.RevisionList.Where(r=>r.Reason=="轉讓").Count() %></h2>
            </a>
            <%  if (currentItem != null)
                {   %>
            <small class="info"><%= (Naming.ContractServiceStatus)currentItem.Status %></small>
            <%  }
                else if(applicable)
                {   %>
            <p>
                <button class="btn btn-darkteal btn-icon btn-icon-mini btn-round waves-effect float-right" onclick="transferContract();"><i class="zmdi zmdi-plus"></i></button>
            </p>
            <%  } %>
        </div>
    </div>
</div>
<script>
    function transferContract() {
        $('').launchDownload('<%= Url.Action("TransferContract", "ConsoleHome") %>',
            <%= JsonConvert.SerializeObject(new 
            {
                KeyID = _model.ContractID.EncryptKey()
            }) %>);
    }
</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    CourseContract _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (CourseContract)this.Model;
    }


</script>

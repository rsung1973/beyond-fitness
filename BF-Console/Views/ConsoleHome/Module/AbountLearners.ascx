<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<div class="container-fluid">
    <div class="row clearfix">
        <h4 class="card-outbound-header m-l-15">我的學生 
            <%  var items = _model.ServingCoach.PromptLearnerWithBirthday(models);
                if (items.Count() > 0)
                {   %>
            <i class="zmdi livicon-evo" data-options="name: gift.svg; size: 30px; style: original; strokeWidth:2px;"></i>
            <%  } %>
        </h4>
        <div class="col-lg-12 col-md-12">
            <div class="row clearfix">
                <div class="col-12">
                    <div class="row clearfix">
                        <div class="col-12">
                            <ul class="row profile_state list-unstyled">
                                <li class="col-sm-4 col-6">
                                    <div class="card info-box-2 hover-zoom-effect">
                                        <div class="icon"></div>
                                        <div class="content">
                                            <div class="icon"><i class="zmdi livicon-evo" data-options="name: notebook.svg; size: 40px; style: original; strokeWidth:2px;"></i></div>
                                            <div class="text">待續約學生</div>
                                            <div class="number col-red">?</div>
                                        </div>
                                    </div>
                                </li>
                                <li class="col-sm-4 col-6 calendar-todolist">
                                    <div class="card info-box-2 hover-zoom-effect">
                                        <div class="icon"><i>
                                            <img src="images/lesson/stage1-girl-clear.png" width="75px"></i></div>
                                        <div class="content">
                                            <div class="text">兌換裝備 - P.T Session</div>
                                            <div class="number">6</div>
                                        </div>
                                    </div>
                                </li>
                                <li class="col-sm-4 col-6 calendar-todolist">
                                    <div class="card info-box-2">
                                        <div class="content">
                                            <div class="sparkline-pie">12,5</div>
                                            <div class="text">本月V.S.上月上課學生</div>
                                            <div class="number"><span class="col-grey">12</span> / <span class="col-amber">5</span></div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
    }


</script>

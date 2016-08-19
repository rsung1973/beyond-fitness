<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div id="loading" style="display: <%= _item==true ? "table" : "none" %>; position: fixed; top: 0; left: 0; z-index: 10000000; width: 100%; height: 100%; opacity: 0.6; filter: Alpha(opacity=50); background: #000;">

    <div class="modal-body text-center" style="display: table-cell; vertical-align: middle; z-index: 10000001;">
        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
        <i class="fa fa-spinner fa-pulse fa-3x fa-fw"></i>
        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
        <i class='fa fa-refresh fa-spin'></i>Loading...
    </div>
</div>
<script>
    function startLoading() {
        $('#loading').css('display', 'table');
        setTimeout(function () { $('#loading').css('display', 'none'); }, 1000);
    }
    function finishLoading() {
        $('#loading').css('display', 'none');
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    //ModelSource<UserProfile> models;
    bool? _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        //models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (bool?)ViewBag.Loading;
    }

</script>

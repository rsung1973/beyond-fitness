<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />
    

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-10">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-eye"> 檢視詳細資訊</span></h4>
                    <!-- Start Post -->
                    <%  ViewBag.Argument = new ArgumentModel { }; ViewBag.ShowPerson = true; Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model); %>
                    

                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-calendar-o"></i>購買上課紀錄</a></li>
                        <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>問卷調查</a></li>
                    </ul>
                    <div class="tab-content">
                        <!-- Tab Content 1 -->
                        <div class="tab-pane fade in active" id="tab-1">
                            <!-- TABLE 1 -->
                            <% Html.RenderPartial("~/Views/Member/LessonsList.ascx", _items); %>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                            <%  ViewBag.DataItems = models.GetTable<PDQQuestion>().OrderBy(q => q.QuestionNo).ToArray();
                                Html.RenderPartial("~/Views/Member/PDQInfoByLearner.ascx", _model); %>
                        </div>
                    </div>
                    <!-- End Post -->
                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">回清單頁 <i class="fa fa-th-list" aria-hidden="true"></i></a>

                </div>

            </div>
        </div>
    </div>

    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    IEnumerable<RegisterLesson> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;

        _items = models.GetTable<RegisterLesson>().Where(r => r.UID == _model.UID)
            .OrderByDescending(r => r.RegisterID);
        ViewBag.ShowOnly = true;
    }


</script>

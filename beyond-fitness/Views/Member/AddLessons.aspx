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
                    <h4 class="classic-title"><span class="fa fa-calendar-o"> 新增/刪除上課數</span></h4>
                    <!-- Stat Search -->
                    <!-- Start Post -->
                    <% Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _dataItem); %>
                    <!-- TABLE 1 -->
                    <% Html.RenderPartial("~/Views/Member/LessonsList.ascx", _items); %>
                    <div class="hr1" style="margin-bottom: 10px;"></div>

                    <% Html.RenderPartial("~/Views/Member/LessonsItem.ascx", _model);  %>

                    <div class="form-group has-feedback">
                        <input name="grouping" id="grouping" type="checkbox" value="Y" />
                        <label class="control-label" for="nickname">是否選用團體課程</label>
                    </div>

                    <div id="selectMemberCount" class="form-group has-feedback" style="display:none;">
                        <label class="control-label" for="nickname">團體人數：</label>
                        <select name="memberCount">
                            <option value="2">2人</option>
                            <option value="3">3人</option>
                            <option value="4">4人</option>
                            <option value="5">5人</option>
                            <option value="6">6人</option>
                        </select>
                    </div>

                    <div class="tabs-section">

                        <div class="hr1" style="margin: 5px 0px;"></div>

                        <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                        <div class="hr1" style="margin: 5px 0px;"></div>
                        <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">回清單頁 <i class="fa fa-th-list" aria-hidden="true"></i></a>
                        <a id="nextStep" class="btn-system btn-medium">確定 <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></a>

                        <!-- End Contact Form -->

                    </div>
                    <!-- End Post -->
                </div>
            </div>
        </div>
    </div>
    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/AddLessons") %>')
              .submit();

        });

        $('#grouping').on('click', function (evt) {
            if ($(this).is(':checked')) {
                $('#selectMemberCount').css('display', 'block');
            } else {
                $('#selectMemberCount').css('display', 'none');
            }
        });

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    LessonViewModel _model;
    IEnumerable<RegisterLesson> _items;
    UserProfile _dataItem;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LessonViewModel)this.Model;
        _dataItem = (UserProfile)ViewBag.DataItem;

        _items = models.GetTable<RegisterLesson>().Where(r => r.UID == _dataItem.UID)
            .OrderByDescending(r => r.RegisterID);
    }

</script>

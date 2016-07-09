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

                <div class="col-md-6">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-pencil">填寫問卷</span></h4>

                    <% Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model); %>

                    <div class="col-md-12">
                        <h4><span class="fa fa-hourglass-start">第一步：目標</span></h4>
                        <table class="panel panel-default table">
                            <%  
                                ViewBag.Offset = 0;
                                for (int idx = 0; idx < 7; idx++)
                                {
                                    renderItem(idx);
                                } %>
                        </table>
                        <a href="#" class="btn-system btn-medium"><span class="glyphicon glyphicon-save" aria-hidden="true"></span>存檔</a>

                        <h4><span class="fa fa-hourglass-half">第二步：風格</span></h4>
                        <table class="panel panel-default table">
                            <%                                  
                                ViewBag.Offset = 7;
                                for (int idx = 7; idx < 11; idx++)
                                {
                                    renderItem(idx);
                                } %>
                        </table>
                        <a href="#" class="btn-system btn-medium"><span class="glyphicon glyphicon-save" aria-hidden="true"></span>存檔</a>

                        <h4><span class="fa fa-hourglass-end">第三步：訓練水平</span></h4>
                        <table class="panel panel-default table">
                            <%  
                                ViewBag.Offset = 11;
                                for (int idx = 11; idx < 16; idx++)
                                {
                                    renderItem(idx);
                                } %>
                        </table>
                        <a href="#" class="btn-system btn-medium"><span class="glyphicon glyphicon-save" aria-hidden="true"></span>存檔</a>

                        <h4><span class="fa fa-hourglass">第四步：參與目標動機</span></h4>
                        <table class="panel panel-default table">
                            <%                                  
                                ViewBag.Offset = 16;
                                for (int idx = 16; idx < 28; idx++)
                                {
                                    renderItem(idx);
                                } %>
                        </table>

                        <a href="#" class="btn-system btn-medium"><span class="glyphicon glyphicon-save" aria-hidden="true"></span>存檔</a>
                        <h4 class="classic-title"><span class="fa fa-tags">方案設計工具結果，請在下面選擇每個方案分類的合適結果</span></h4>

                        <div class="panel panel-default">
                            <table class="table">
                                <tr class="info">
                                    <th>目標</th>
                                    <th>風格</th>
                                    <th>訓練水準</th>
                                </tr>
                                <tr>
                                    <td>
                                        <select class="form-control">
                                            <option>減肥</option>
                                            <option>健身</option>
                                            <option>健美體態</option>
                                            <option>運動表現</option>
                                        </select>
                                    </td>
                                    <td>
                                        <select class="form-control">
                                            <option>保守型</option>
                                            <option>挑戰型</option>
                                            <option>混合型</option>
                                        </select>
                                    </td>
                                    <td>
                                        <select class="form-control">
                                            <option>初期</option>
                                            <option>過渡期</option>
                                            <option>進步期</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <a href="#" class="btn-system btn-medium"><span class="glyphicon glyphicon-save" aria-hidden="true"></span>存檔</a>
                    </div>

                    <!-- End Contact Form -->

                </div>
                <!-- End Post -->


            </div>

        </div>
    </div>

    <!-- End content -->
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {

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
    PDQQuestion[] _items;
    UserProfile _model;
    Dictionary<int, String> _evalIndex;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;
        _items = (PDQQuestion[])ViewBag.DataItems;
        _evalIndex = new Dictionary<int, string>();
        _evalIndex[7] = null;
        _evalIndex[11] = "風格評分：";
        _evalIndex[16] = null;
        _evalIndex[28] = null;

    }

    void renderItem(int idx)
    {
        var item = _items[idx];
        if (_evalIndex.ContainsKey(item.QuestionID))
        {
            ViewBag.AdditionalTitle = _evalIndex[item.QuestionID];
            Html.RenderPartial("~/Views/Member/PDQItemII.ascx", item);
        }
        else
        {
            Html.RenderPartial("~/Views/Member/PDQItem.ascx", item);
        }
    }

</script>

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
<%@ Register Src="~/Views/Shared/LockScreen.ascx" TagPrefix="uc1" TagName="LockScreen" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <uc1:LockScreen runat="server" ID="LockScreen" />

    <% Html.RenderPartial("~/Views/Shared/HtmlInput.ascx",
               new InputViewModel
               {
                   ErrorMessage = "error",
                   Id = "realName",
                   IsValid = true,
                   Label = "學員姓名：",
                   PlaceHolder = "請輸入姓名",
                   Name = "realName"
               }); %>

    <div class="row">
        <div class="col-md-5">
            <div class="input-group date form_date" data-date="" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
                <input class="form-control" size="16" type="text" value="" id="dateFrom" name="dateFrom" readonly />
                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
            </div>

            <div class="input-group date form_month" id="testDate" data-date-format="yyyy/mm" data-link-field="dtp_input1">
                <input class="form-control" size="16" type="text" value="" id="dateFrom" name="dateFrom" readonly />
                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
            </div>
        </div>
    </div>

    <% Html.RenderPartial("~/Views/Lessons/DailyTrendPieView.ascx",new LessonTimeExpansion {
           ClassDate = new DateTime(2016,7,7),
           Hour = 14,
           RegisterID = 57,
           LessonID = 31
        }); %>

    <script>
        $('#vip,#m_vip').addClass('active');

        $(function () {
            //$('#testDate').datetimepicker({
            //    viewMode: 'years',
            //    format: 'YYYY/MM'
            //});
        });

    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>新增學員 - Step 1</span></h4>

                    <!-- Start Contact Form -->

                        <div class="form-group has-feedback">
                            <% Html.RenderInput( "學員姓名：","realName","realName","請輸入姓名",_modelState); %>
                        </div>

                        <div class="form-group has-feedback">
                            <% Html.RenderInput("電話：", "phone", "phone", "請輸入市話或手機號碼", _modelState); %>
                        </div>

                        <div class="form-group has-feedback">
                            <% Html.RenderInput("上課總次數：", "lessons", "lessons", "請輸入數字", _modelState); %>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="classLevel">課程類別：</label>
                            <select class="form-control" name="classLevel">
                                <option value="2001">A-1500</option>
                                <option value="2002">B-1600</option>
                                <option value="2003">C-1800</option>
                                <option value="2004">D-1900</option>
                            </select>
                        </div>

                        <div class="tabs-section">

                            <div class="hr1" style="margin: 5px 0px;"></div>

                            <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                            <div class="hr1" style="margin: 5px 0px;"></div>
                            <a href="member-manager.htm" class="btn-system btn-medium">回上頁 <i class="fa fa-reply" aria-hidden="true"></i></a>
                            <a href="#" id="nextStep" class="btn-system btn-medium">下一步 <i class="fa fa-hand-o-right" aria-hidden="true"></i></a>

                            <!-- End Contact Form -->

                        </div>
                    

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/AddLeaner") %>')
              .submit();

        });

        $("form").validate({
            //debug: true,
            //errorClass: "label label-danger",
            success: function (label, element) {
                label.remove();
                var id = $(element).prop("id");
                $('#' + id + 'Icon').removeClass('glyphicon-remove').removeClass('text-danger')
                    .addClass('glyphicon-ok').addClass('text-success');
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element);
                var id = $(element).prop("id");
                $('#' + id + 'Icon').addClass('glyphicon-remove').addClass('text-danger')
                    .removeClass('glyphicon-ok').removeClass('text-success');
            },
            rules: {
                realName: {
                    required: true,
                    maxlength: 20
                },
                lessons: {
                    required : true,
                    max: 9999
                },
                phone: {
                    required : true,
                    regex : /^[0-9]{6,20}$/
                },
                // compound rule
                email: {
                    required: true,
                    email: true
                }
            }
        });

    </script>

</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
    }
</script>

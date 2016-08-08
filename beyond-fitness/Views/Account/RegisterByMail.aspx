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


<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">

        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-user"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>會員註冊</li>
        </ol>
        <!-- end breadcrumb -->

        <!-- You can also add more buttons to the
				ribbon for further usability

				Example below:

				<span class="ribbon-button-alignment pull-right">
				<span id="search" class="btn btn-ribbon hidden-xs" data-title="search"><i class="fa-grid"></i> Change Grid</span>
				<span id="add" class="btn btn-ribbon hidden-xs" data-title="add"><i class="fa-plus"></i> Add</span>
				<span id="search" class="btn btn-ribbon" data-title="search"><i class="fa-search"></i> <span class="hidden-mobile">Search</span></span>
				</span> -->

    </div>
</asp:Content>
<asp:Content ID="pageTitle" ContentPlaceHolderID="pageTitle" runat="server">
    <h1 class="page-title txt-color-blueDark">
        <!-- PAGE HEADER -->
        <i class="fa-fw fa fa-user"></i>會員註冊
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <!-- NEW COL START -->
        <article class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget" id="wid-id-6" data-widget-editbutton="false" data-widget-custombutton="false" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false">
                <!-- widget options:
									usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
									
									data-widget-colorbutton="false"	
									data-widget-editbutton="false"
									data-widget-togglebutton="false"
									data-widget-deletebutton="false"
									data-widget-fullscreenbutton="false"
									data-widget-custombutton="false"
									data-widget-collapsed="true" 
									data-widget-sortable="false"
									
								-->
                <header>
                    <span class="widget-icon"><i class="fa fa-edit"></i></span>
                    <h2>Step2.輸入資本資料 </h2>

                </header>

                <!-- widget div-->
                <div>

                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->

                    </div>
                    <!-- end widget edit box -->

                    <!-- widget content -->
                    <div class="widget-body no-padding bg-color-darken txt-color-white">

                        <form id="pageForm" action="<%= VirtualPathUtility.ToAbsolute("~/Account/CompleteRegister") %>" class="smart-form" method="post">
                            <%=  Html.AntiForgeryToken() %>
                            <fieldset>
                                <div class="row">
                                    <section class="col col-6">
                                        <label class="input">
                                            <i class="icon-append fa fa-envelope-o "></i>
                                            <input class="form-control input-lg" maxlength="30" placeholder="請輸入註冊時的E-mail" type="email" name="EMail" id="EMail" value="<%= _viewModel.EMail %>" />
                                        </label>
                                    </section>
                                    <section class="col col-6">
                                        <label class="input">
                                            <i class="icon-append fa fa-user "></i>
                                            <input class="form-control input-lg" maxlength="20" placeholder="請輸入暱稱" type="text" name="userName" id="userName" value="<%= _viewModel.UserName %>" />
                                        </label>
                                    </section>
                                </div>
                            </fieldset>
                            <fieldset>
                                <div class="row">
                                    <div class="col col-12">
                                        <img src="<%= VirtualPathUtility.ToAbsolute("~/img/avatars/male.png") %>" alt="親愛的" class="online" id="authorImg" width="50" height="50" />
                                        <div class="input input-file">
                                            <span class="button">
                                                <input type="file" id="photopic" name="photopic" onchange="this.parentNode.nextSibling.value = this.value" />瀏覽
                                            </span>
                                            <input type="text" placeholder="請選擇圖片" readonly="" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <% Html.RenderPartial("~/Views/Shared/SetPassword.ascx"); %>
                            </fieldset>
                            <footer class="text-right">
                                <button type="submit" name="submit" class="btn btn-primary">
                                    送出 <i class="fa fa-paper-plane" aria-hidden="true"></i>
                                </button>
                            </footer>
                        </form>
                    </div>
                    <!-- end widget content -->

                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <!-- END COL -->

        <!-- NEW COL START -->
        <%  Html.RenderPartial("~/Views/Layout/QuickLink.ascx"); %>
        <!-- END COL -->
    </div>

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {
            startLoading();
            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Account/CompleteRegister") %>')
              .submit();

        });

        $(function () {

            $('#EMail').rules('add', {
                'required': true,
                'email': true,
                messages: {
                    'required': '請輸入您的 email address',
                    'email': '請輸入合法的 email address'
                }
            });

            $('#userName').rules('add', {
                'required': true,
                messages: {
                    'required': '請輸入您的暱稱'
                }
            });


            var fileUpload = $('#photopic');
            var elmt = fileUpload.prev();

            fileUpload.off('click').on('change', function () {

                $('<form method="post" id="myForm" enctype="multipart/form-data"></form>')
                .append(fileUpload).ajaxForm({
                    url: "<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>",
                    data: { 'memberCode': '<%= _item.MemberCode %>' },
                    beforeSubmit: function () {
                        //status.show();
                        //btn.hide();
                        //console.log('提交時');
                    },
                    success: function (data) {
                        elmt.after(fileUpload);
                        if (data.result) {
                            $('#authorImg').prop('src', '<%= VirtualPathUtility.ToAbsolute("~/Information/GetResource/") %>' + data.pictureID);
                        } else {
                            smartAlert(data.message);
                        }
                        //status.hide();
                        //console.log('提交成功');
                    },
                    error: function () {
                        elmt.after(fileUpload);
                        //status.hide();
                        //btn.show();
                        //console.log('提交失败');
                    }
                }).submit();
            });
        });


    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _item;
    ModelStateDictionary _modelState;
    RegisterViewModel _viewModel;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _item = (UserProfile)this.Model;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _viewModel = (RegisterViewModel)ViewBag.ViewModel;
        if (_viewModel == null)
            _viewModel = new RegisterViewModel { };
    }



</script>

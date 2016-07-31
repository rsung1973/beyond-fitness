<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<asp:Content ID="ribbonContent" ContentPlaceHolderID="ribbonContent" runat="server">
    <div id="ribbon">
        <span class="ribbon-button-alignment">
            <span id="refresh" class="btn btn-ribbon">
                <i class="fa fa-user"></i>
            </span>
        </span>

        <!-- breadcrumb -->
        <ol class="breadcrumb">
            <li>人員管理></li>
            <li>VIP管理</li>
            <li>VIP列表</li>
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
        <i class="fa-fw fa fa-user"></i>VIP管理
                            <span>>  
                                VIP列表
                            </span>
    </h1>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <div class="row">

        <!-- NEW WIDGET START -->
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

            <!-- Widget ID (each widget will need unique ID)-->
            <div class="jarviswidget jarviswidget-color-darken" id="wid-id-1" data-widget-editbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false" data-widget-togglebutton="false">
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
                    <span class="widget-icon"><i class="fa fa-table"></i></span>
                    <h2>VIP列表</h2>
                    <div class="widget-toolbar">
                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/AddLearner") %>" class="btn btn-primary"><i class="fa fa-fw fa-user-plus"></i>新增VIP</a>
                    </div>
                </header>

                <!-- widget div-->
                <div>

                    <!-- widget edit box -->
                    <div class="jarviswidget-editbox">
                        <!-- This area used as dropdown edit box -->
                    </div>
                    <!-- end widget edit box -->

                    <!-- widget content -->
                    <div class="widget-body no-padding">
                        <%   Html.RenderPartial("~/Views/Member/LearnerList.ascx", _items); %>
                    </div>
                    <!-- end widget content -->
                </div>
                <!-- end widget div -->
            </div>
            <!-- end widget -->
        </article>
        <!-- WIDGET END -->

    </div>

    <script>

        $(function(){
            /* BASIC ;*/
            var responsiveHelper_dt_basic = undefined;
            var responsiveHelper_datatable_fixed_column = undefined;
            var responsiveHelper_datatable_col_reorder = undefined;
            var responsiveHelper_datatable_tabletools = undefined;

            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            $('#dt_basic').dataTable({
                "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                    "t" +
                    "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
                "autoWidth": true,
                "oLanguage": {
                    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>'
                },
                "preDrawCallback": function() {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper_dt_basic) {
                        responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#dt_basic'), breakpointDefinition);
                    }
                },
                "rowCallback": function(nRow) {
                    responsiveHelper_dt_basic.createExpandIcon(nRow);
                },
                "drawCallback": function(oSettings) {
                    responsiveHelper_dt_basic.respond();
                }
            });

            /* END BASIC */
        });

        function deleteLearner(uid) {
            $.SmartMessageBox({
                title: "<i class=\"fa fa-fw fa fa-trash-o\" aria-hidden=\"true\"></i> 刪除學員",
                content: "確定刪除此學員資料?",
                buttons: '[刪除][取消]'
            }, function (ButtonPressed) {
                if (ButtonPressed == "刪除") {
                    $('<form method="post"/>').appendTo($('body'))
                        .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/Delete") %>' + '?uid=' + uid)
                        .submit();
                }
            });
        }

        $('.nav-tabs').on('shown.bs.tab', function (evt) {
            //console.log(evt.target);
            //console.log(evt.relatedTarget);
            startLoading();
            if ($(evt.target).prop('id') == 'toggleLearner') {
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Member/ChangeRoleList") %>', { 'roleID' : <%= (int)Naming.RoleID.Coach  %>},function(data) {
                    $('#loading').css('display', 'none');
                    $('#searchdil').modal('show');
                });
            } else if ($(evt.target).prop('id') == 'toggleCoach') {
                $.post('<%= VirtualPathUtility.ToAbsolute("~/Member/ChangeRoleList") %>', { 'roleID' : <%= (int)Naming.RoleID.Coach  %>},function(data) {
                    $('#loading').css('display', 'none');
                });
            }
        });

    $('#btnQuery').on('click', function (evt) {
        $('#loading').css('display', 'table');
        $('#tab-learner').load('<%= VirtualPathUtility.ToAbsolute("~/Member/ListLearners") %>', { 'byName': $('input[name="byName"]').val() }, function () {
            $('#searchdil').modal('hide');
            $('#loading').css('display', 'none');
        });
    });

    </script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    IEnumerable<UserProfile> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = (IEnumerable<UserProfile>)this.Model;
    }




</script>

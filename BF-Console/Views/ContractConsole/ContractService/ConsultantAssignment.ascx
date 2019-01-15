<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
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

<ul class="row clearfix list-unstyled m-b-0" id="<%= _viewID %>">
    <li class="col-lg-6 col-md-12 col-sm-12">
        <div class="header">
            <h2><strong>合約資料</strong> - <%= _model.ContractName() %> <span class="badge badge-danger"><i class="zmdi zmdi-pin"></i><%= _model.CourseContractExtension.BranchStore.BranchName %></span> </h2>
        </div>
        <div class="body">
            <div class="row">
                <div class="col-md-6 col-sm-6 invoice1">
                    <div class="clientlogo">
                        <%  _model.ContractOwner.PictureID.RenderUserPicture(this.Writer, new { @class = "fit" }, "images/avatar/noname.png"); %>
                    </div>
                    <address>
                        <strong><%= _model.ContractOwner.FullName() %></strong><br />
                        購買堂數：<%= _model.Lessons %><br />
                        <span class="p-l-40 col-red">剩餘堂數：<%= _model.RemainedLessonCount() %></span>
                    </address>
                </div>
                <div class="col-md-6 col-sm-6 text-right">
                    <p class="m-b-0"><strong>合約起日: </strong><%= $"{_model.ValidFrom:yyyy/MM/dd}" %></p>
                    <p class="m-b-0 col-red"><strong>合約迄日: </strong><%= $"{_model.Expiration:yyyy/MM/dd}" %></p>
                    <p class="m-b-0"><%= _model.ContractNo() %> <span class="badge bg-green"><%= (Naming.CourseContractStatus)_model.Status %></span></p>
                </div>
            </div>
            <div class="mt-40"></div>
            <!--
                            <div class="row">
                                <div class="col-md-12">
                                    <a href="javascript:showContractDetail();" class="btn btn-simple btn-info btn-round btn-sm float-right">更多資訊</a>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-custom nowrap bg-white col-charcoal dataTable dataTable-learner" style="width:100%">
                                            <thead>
                                                <tr>
                                                    <th>姓名</th>
                                                    <th>性別</th>
                                                    <th>連絡電話</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
-->
            <hr>
            <div class="row">
                <div class="col-md-6">
                    <h5>其他增補說明</h5>
                    <p><%= _model.Remark %></p>
                </div>
                <!--
                                <div class="col-md-6 text-right">
                                    <p><b>專業顧問建置與諮詢費：</b> 89,600</p>
                                    <p>教練課程費：22,400</p>                                    
                                </div>
-->
                <div class="col-12 text-right">
                    <h3 class="text-right col-red">專業顧問服務總費用：<%= $"{_model.TotalCost:##,###,###,###}" %></h3>
                </div>
            </div>
        </div>
    </li>
    <li class="col-lg-6 col-md-12 col-sm-12">
        <div class="header">
            <h2><strong>原負責體能顧問</strong> - <strong class="text-primary"><%= _model.ServingCoach.UserProfile.FullName() %></strong></h2>
        </div>
        <div class="body">
            <div class="row clearfix">
                <div class="col-lg-6 col-md-6 col-sm-6 col-12 m-b-20">
                    <div class="checkbox">
                        <input id="checkbox14" type="checkbox" checked="checked" name="FitnessConsultant" value="<%= _profile.UID %>" onclick="this.checked = true;" />
                        <label for="checkbox14">
                            <span class="col-red">轉換為自己</span>
                        </label>
                    </div>
                </div>
                <div class="col-12">
                    <div class="form-group">
                        <textarea rows="3" class="form-control no-resize" name="Remark" placeholder="請輸入任何想補充的事項..."></textarea>
                    </div>
                </div>
            </div>
        </div>
    </li>
    <li class="col-12">
        <button type="button" class="btn btn-darkteal btn-round waves-effect float-right finish" onclick="commitAssignment();">確定，不後悔</button>
        <button type="button" class="btn btn-danger btn-round btn-simple waves-effect quit" onclick="cancelAssignment();">不, 點錯了</button>
    </li>
</ul>
<script>
    function cancelAssignment() {
        $('').launchDownload('<%= Url.Action("ApplyContractService","ConsoleHome") %>',
            <%= JsonConvert.SerializeObject(new CourseContractQueryViewModel
                {
                    KeyID = _model.ContractID.EncryptKey(),
                }) %>);
    }

    function commitAssignment() {
        var viewModel = $('#<%= _viewID %>').find('input,select,textArea').serializeObject();
        viewModel.Reason = '轉換體能顧問';
        clearErrors();
        showLoading();
        $.post('<%= Url.Action("CommitContractService", "ContractConsole",new { KeyID = _model.ContractID.EncryptKey() }) %>', viewModel, function (data) {
            hideLoading();
            if ($.isPlainObject(data)) {
                swal(data.message);
            }
            else {
                $(data).appendTo($('body'));
            }
        });
    }

</script>
<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    CourseContract _model;
    UserProfile _profile;
    String _viewID = $"assign{DateTime.Now.Ticks}";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (CourseContract)this.Model;
        _profile = Context.GetUser();
    }


</script>

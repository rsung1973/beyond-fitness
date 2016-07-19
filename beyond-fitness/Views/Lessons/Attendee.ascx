<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title" id="searchdilLabel"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>選擇上課學員</h4>
        </div>
        <div class="modal-body">
            <!-- Stat Search -->
            <div class="form-group">
                <label for="exampleInputFile" class="col-md-2 control-label">依姓名：</label>
                <div class="col-md-6">
                    <input name="userName" class="form-control" type="text" value="" />
                </div>
                <div class="col-md-4">
                    <a id="btnQuery" class="btn btn-search"><i class="fa fa-search"></i></a>
                </div>

                <div class="col-md-12" id="userList">
                </div>

                <div class="col-md-12 modal-footer">
                    <button id="btnAttending" type="button" class="btn btn-system btn-sm"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確定</button>
                    <button type="button" id="btnDismiss" class="btn btn-default btn-sm" data-dismiss="modal"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>關閉</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $('#btnAttending').on('click', function (evt) {
        var $items = $('input[name="registerID"]:checked');
        if ($items.length <= 0) {
            alert('請勾選上課學員!!');
            return;
        }
        $('#attendee').empty();
        $items.parent().detach().appendTo($('#attendee'));
        $('#btnDismiss').trigger('click');

<%--        $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/ApplyGroupLessons") %>')
            .submit();--%>
    });

    $('#btnQuery').on('click', function (evt) {
        $('#loading').css('display', 'table');
        var userName = $('input[name="userName"]').val();
        $('#userList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/AttendeeSelector") %>', { 'userName': userName }, function () {
            $('#loading').css('display', 'none');
        });
    });

</script>


<script runat="server">

    ModelStateDictionary _modelState;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        var models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

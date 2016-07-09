<%@  Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<p><strong>會員編號：</strong><span class="text-primary"><%= _model.MemberCode %></span></p>
<!-- Divider -->
<div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

<div class="form-group has-feedback">
    <% Html.RenderInput("Email：", "email", "email", "請輸入Email", _modelState, defaultValue: _model.EMail); %>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("暱稱：", "userName", "userName", "請輸入暱稱", _modelState, defaultValue: _model.UserName); %>
</div>

<div class="form-group has-feedback">
    <label class="control-label" for="classno">生日：</label>
    <div class="input-group date form_date" data-date="" data-date-format="yyyy/mm/dd" data-link-field="dtp_input1">
        <input id="birthDay" name="birthDay" class="form-control" size="16" type="text" value='<%= _model.Birthday.HasValue ? _model.Birthday.Value.ToString("yyyy/MM/dd") : "" %>' readonly>
        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
    </div>
</div>

<div class="form-group has-feedback">
    <% Html.RenderInput("頭像：", "photopic", "photopic", "", _modelState, "file"); %>
</div>
<div class="author-image">
    <% _model.PictureID.RenderUserPicture(this.Writer, "authorImg"); %>
</div>

<% Html.RenderPartial("~/Views/Shared/SetPassword.ascx"); %>
<!-- End Tab Panels -->

<script>

    $(function () {
        $('#email').rules('add', {
            'required': true,
            'email': true
        });
    });


    var fileUpload = $('#photopic');
    var elmt = fileUpload.prev();

    fileUpload.off('click').on('change', function () {

        $('<form method="post" id="myForm" enctype="multipart/form-data"></form>')
        .append(fileUpload).ajaxForm({
            url: "<%= VirtualPathUtility.ToAbsolute("~/Account/UpdateMemberPicture") %>",
                data: { 'memberCode': '<%= _model.MemberCode %>' },
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
                        alert(data.message);
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

</script>

</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    RegisterViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterViewModel)this.Model;
    }


</script>

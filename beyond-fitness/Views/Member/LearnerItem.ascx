<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>

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

<% if (_model!=null && _model.UID.HasValue)
    { %>
<input type="hidden" name="uid" value="<%= _model.UID %>" />
<%  } %>
<% if (_model != null)
    { %>
<script>
    $(function () {
        $('#realName').val('<%= _model.RealName %>');
        $('#phone').val('<%= _model.Phone %>');
        $('#lessons').val('<%= _model.Lessons %>');
        $('#classLevel').val('<%= _model.ClassLevel %>');
    });
</script>
<%  } %>

<script>
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
                required: true,
                max: 9999
            },
            phone: {
                required: true,
                regex: /^[0-9]{6,20}$/
            },
            // compound rule
            email: {
                required: true,
                email: true
            }
        }
    });
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    LearnerViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = this.Model as LearnerViewModel;
        if (_model == null)
        {
            UserProfile item = (UserProfile)this.Model;
            if(item!=null)
            {
                var lesson = item.RegisterLesson.OrderByDescending(r => r.RegisterID).FirstOrDefault();

                _model = new LearnerViewModel
                {
                    Lessons = lesson != null ? lesson.Lessons : 0,
                    Phone = item.Phone,
                    RealName = item.RealName,
                    ClassLevel = lesson != null ? lesson.ClassLevel.Value : 2001,
                    UID = item.UID
                };
            }
        }
    }
</script>

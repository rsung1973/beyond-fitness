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
            <h4 class="modal-title" id="searchdilLabel"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>新增團體上課學員</h4>
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
                    <table class="table">
                        <% if (_items.Length > 0)
                            {

                                for (int i = 0; i < (_items.Length + 2) / 3; i++)
                                { %>
                        <tr class="info">
                                <%  for (int j = 0; j < 3; j++)
                                    {
                                        if ((i * 3 + j) < _items.Length)
                                        { %>                              
                            <td width="25">
                                <input type="checkbox" name="registerID" value="<%= _items[i * 3 + j].RegisterID %>" <%= _items[i * 3 + j].RegisterGroupID==_model.RegisterGroupID ? "checked" : null %> /></td>
                            <td><%= _items[i * 3 + j].UserProfile.RealName %></td>
                                <%      }
                                        else
                                        { %>
                            <td width="25"></td><td width="25"></td>
                                     <% }
                                    } %>
                        </tr>
                            <%  }
                            }
                            else
                            { %>
                        <tr><td colspan="3">查無相符條件的上課資料!!</td></tr>
                           <% } %>
                    </table>
                </div>

                <div class="col-md-12 modal-footer">
                    <button id="btnGrouping" type="button" class="btn btn-system btn-sm"><span class="fa fa-link" aria-hidden="true"></span>設定</button>
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal"><span class="fa fa-times" aria-hidden="true"></span>取消</button>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" name="lessonId" value="<%= _model.RegisterID %>" />
<script>
    $('#btnGrouping').on('click', function (evt) {
        var $items = $('input:checkbox[name="registerID"]:checked');
        var memberCount = <%= _model.GroupingMemberCount-1 %>;
        if ($items.length <= 0) {
            alert('請勾選本次團體課學員!!');
            return;
        } else if($items.length > memberCount) {
            alert('勾選的學員數超過本次團體課人數!!');
            return;
        }
        startLoading();
        $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/ApplyGroupLessons") %>')
            .submit();
    });

    $('#btnQuery').on('click',function(evt) {
        var userName = $('input[name="userName"]').val();
        $('#loading').css('display', 'table');
        $('#userList').load('<%= VirtualPathUtility.ToAbsolute("~/Member/GroupLessonUsersSelector") %>',{'lessonId':<%= _model.RegisterID %>,'userName':userName},function(){
            $('#loading').css('display', 'none');
        });
    });

</script>


<script runat="server">

    ModelStateDictionary _modelState;
    RegisterLesson _model;
    RegisterLesson[] _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (RegisterLesson)this.Model;
        var models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = models.GetTable<RegisterLesson>().Where(l => l.Attended == (int)Naming.LessonStatus.準備上課 && l.ClassLevel == _model.ClassLevel
            && l.Lessons == _model.Lessons 
            && l.RegisterID != _model.RegisterID 
            && l.GroupingMemberCount == _model.GroupingMemberCount).ToArray();
    }

</script>

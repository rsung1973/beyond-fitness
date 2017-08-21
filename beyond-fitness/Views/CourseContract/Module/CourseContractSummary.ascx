<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<table class="table table-striped table-bordered table-hover" width="100%">
    <thead>
        <tr>
            <th></th>
            <th class="text-center">草稿</th>
            <th class="text-center">待審核</th>
            <th class="text-center">待簽名</th>
            <th class="text-center">待生效</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><%  var items = models.GetApplyingContractByAgent(_model); %>
                新合約(<%= items.Count() %>)</td>
            <td class="text-center">
                <%  items = models.GetContractInEditingByAgent(_model);
                    if (items.Count() > 0)
                    {   %>
                <a onclick="showContractTodoList(<%= (int)Naming.CourseContractStatus.草稿 %>);" ><u>(<%= items.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>
            </td>
            <td class="text-center">
                <%  items = models.GetContractToAllowByAgent(_model);
                    if (items.Count() > 0)
                    {   %>
                <a onclick="showContractTodoList(<%= (int)Naming.CourseContractStatus.待審核 %>);"><u>(<%= items.Count() %>)</u></a>
            <%      }
                    else
                    { %>
                    --
                <%  } %>
            </td>
            <td class="text-center">
                <%  items = models.GetContractToSignByAgent(_model);
                    if (items.Count() > 0)
                    {   %>
                <a onclick="showContractTodoList(<%= (int)Naming.CourseContractStatus.待簽名 %>);" ><u>(<%= items.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>
            </td>
            <td class="text-center">
                <%  items = models.GetContractToConfirmByAgent(_model);
                    if (items.Count() > 0)
                    {   %>
                <a onclick="showContractTodoList(<%= (int)Naming.CourseContractStatus.待生效 %>);" ><u>(<%= items.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>
            </td>
        </tr>
        <tr>
            <td><%  var revisions = models.GetApplyingAmendmentByAgent(_model); %>
                服務申請(<%= revisions.Count() %>)</td>
            <td class="text-center">--</td>
            <td class="text-center">
                <%  revisions = models.GetAmendmentToAllowByAgent(_model);
                    if (revisions.Count() > 0)
                    {   %>
                <a onclick="showAmendmentTodoList(<%= (int)Naming.CourseContractStatus.待審核 %>);"><u>(<%= revisions.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>            
            </td>
            <td class="text-center">
                <%  revisions = models.GetAmendmentToSignByAgent(_model);
                    if (revisions.Count() > 0)
                    {   %>
                <a onclick="showAmendmentTodoList(<%= (int)Naming.CourseContractStatus.待簽名 %>);"><u>(<%= revisions.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>
            </td>
            <td class="text-center">
                <%  revisions = models.GetAmendmentToConfirmByAgent(_model);
                    if (revisions.Count() > 0)
                    {   %>
                <a onclick="showAmendmentTodoList(<%= (int)Naming.CourseContractStatus.待生效 %>);"><u>(<%= revisions.Count() %>)</u></a>
                <%  }
                    else
                    { %>
                    --
                <%  } %>
            </td>
        </tr>
        <tr>
            <td>付款(7)</td>
            <td class="text-center">--</td>
            <td class="text-center"><u>(5)</u></td>
            <td class="text-center">--</td>
            <td class="text-center"><a href="#" id=""><u>(2)</u></a></td>
        </tr>
    </tbody>
</table>

<script>
    function showContractTodoList(status) {
        showLoading();
        $.post('<%= Url.Action("ContractTodoList","CourseContract",new { agentID = _model.UID }) %>', { 'status': status }, function (data) {
            hideLoading();
            $(data).appendTo($('body'));
        });
    }

    function showAmendmentTodoList(status) {
        showLoading();
        $.post('<%= Url.Action("ContractAmendmentTodoList","CourseContract",new { agentID = _model.UID }) %>', { 'status': status }, function (data) {
            hideLoading();
            $(data).appendTo($('body'));
        });
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
    }

</script>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Models.Timeline" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table class="table" width="100%">
    <tbody>
        <tr>
            <td>
                <div class="chat-body no-padding profile-message">
                    <ul>
                        <li class="message">
                            <% _profile.PictureID.RenderUserPicture(Writer, new { @class = "profileImg online" }); %>
                            <span class="message-text" id="msgLessonFeedBack">
                                <%  if (_item != null)
                                    {
                                        Html.RenderPartial("~/Views/Activity/LessonFeedBackItem.ascx", _item);
                                    } %>
                            </span>
                        </li>
                        <li>
                            <div class="input-group wall-comment-reply">
                                <input id="lessonFeedBack" type="text" class="form-control" placeholder="請輸入50個中英文字"/>
                                <span class="input-group-btn">
                                    <button class="btn btn-primary" onclick="updateLessonFeedBack(<%= _model.LessonID %>);">
                                        <i class="fa fa-reply"></i>更新
                                    </button>
                                </span>
                            </div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </tbody>
</table>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    LessonTime _model;
    UserProfile _profile;
    LessonFeedBack _item;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (LessonTime)this.Model;

        _profile = Context.GetUser();
        if (_profile == null)
        {
            Response.Redirect(FormsAuthentication.LoginUrl);
        }
        else
        {
            _item = _model.LessonFeedBack.Where(f => f.RegisterLesson.UID == _profile.UID).FirstOrDefault();
        }

    }

</script>

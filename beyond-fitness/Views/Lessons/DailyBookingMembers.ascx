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

<%  if (_items != null && _items.Count() > 0)
    {     %>
        <table cellpadding="5" cellspacing="0" border="0" class="table table-hover table-condensed"> 
            <tbody>
                <%  foreach (var item in _items)
                    { %>
                    <tr> 
                        <td style="width: 50px;"></td> 
                        <td class="col-xs-2 col-sm-2"><%= item.LessonTime.ClassTime.Value.ToString("HH:mm") %> - <%= item.LessonTime.ClassTime.Value.AddMinutes(item.LessonTime.DurationInMinutes.Value).ToString("HH:mm") %><%= item.LessonTime.TrainingBySelf==1 ? "(自主訓練)" : null %></td> 
                        <td class="col-xs-3 col-sm-3"><%  if (item.RegisterLesson.GroupingMemberCount > 1)
                                { %>
                                    <i class="fa fa-group fa-2x text-danger"></i> <%= String.Join("/", item.RegisterLesson.GroupingLesson.RegisterLesson.Select(l => l.UserProfile.RealName)) %>
                            <%  }
                                else
                                { %>
                                    <i class="fa fa-child fa-2x text-danger"></i><%= item.RegisterLesson.UserProfile.RealName %>
                            <%  } %>
                        </td>
                        <td class="col-xs-1 col-sm-1"><%= item.LessonTime.AsAttendingCoach.UserProfile.RealName %></td> 
                        <td><%= item.LessonTime.TrainingPlan.Count==0 
                                    ? "待編輯課程內容"
                                    : item.LessonTime.LessonAttendance!=null 
                                        ? "已完成課程"
                                        : "編輯課程內容中" %>
                            <%  if (item.LessonTime.LessonPlan.CommitAttendance.HasValue)
                                { %>
                                    <br /> (學員已打卡）
                            <%  } %>
                        </td>
                        <td class="text-center">
                            <%  if (item.RegisterLesson.UserProfile.LevelID == (int)Naming.MemberStatusDefinition.Anonymous)
                                {
                                    if (item.LessonTime.LessonAttendance == null )
                                    { %>
                                        <a onclick="revokeBooking(<%= item.LessonID %>);" class="btn btn-system btn-small">取消預約 <i class="fa fa-calendar-times-o" aria-hidden="true"></i></a>
                                <%  }            
                                }
                                else
                                {
                                    Html.RenderPartial("~/Views/Lessons/LessonTimeExpansionHandler.ascx", item);
                                }  %>
                        </td> 
                    </tr> 
    <%          } %>
            </tbody> 
        </table>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;
    IEnumerable<LessonTimeExpansion> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _items = ((IEnumerable<LessonTimeExpansion>)this.Model).OrderBy(t => t.ClassDate).ThenBy(t => t.Hour);
    }

</script>

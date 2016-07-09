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

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="PageBanner" Title="會員專區" TitleInEng="VIP" />
    

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-10">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span class="fa fa-eye"> 檢視詳細資訊</span></h4>
                    <!-- Start Post -->
                    <%  ViewBag.ShowPerson = true; Html.RenderPartial("~/Views/Member/MemberInfo.ascx", _model); %>
                    

                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab-1" data-toggle="tab"><i class="fa fa-calendar-o"></i>購買上課紀錄</a></li>
                        <li><a href="#tab-2" data-toggle="tab"><i class="fa fa-pencil" aria-hidden="true"></i>問卷調查</a></li>
                    </ul>
                    <div class="tab-content">
                        <!-- Tab Content 1 -->
                        <div class="tab-pane fade in active" id="tab-1">
                            <!-- TABLE 1 -->
                            <% Html.RenderPartial("~/Views/Member/LessonsList.ascx", _items); %>
                        </div>
                        <!-- Tab Content 2 -->
                        <div class="tab-pane fade" id="tab-2">
                            <h4 ><span class="fa fa-hourglass-start">第一步：目標</span></h4>
                            <table class="panel panel-default table">
                                <tr class="info">
                                    <th>1.對於你我相處一小時，你有什麼期望？</th>
                                </tr>
                                <tr>
                                    <td>想增強體力</td>
                                </tr>
                                <tr class="info">
                                    <th>2.你希望你的訓練中產生怎樣的結果？</th>
                                </tr>
                                <tr>
                                    <td>希望身體健康</td>
                                </tr>
                                <tr class="info">
                                    <th>3.在訓練方案中，你需要實現的最重要目標是什麼？</th>
                                </tr>
                                <tr>
                                    <td>挑戰型(減肥/身強體壯/運動表現/克服疾病)</td>
                                </tr>
                                <tr class="info">
                                    <th>4.你希望實現自身目標的時間是多久？</th>
                                </tr>
                                <tr>
                                    <td>半年</td>
                                </tr>
                                <tr class="info">
                                    <th>5.除了訓練時段，每天願意額外花多少時間參加幫助你完成目標的活動？</th>
                                </tr>
                                <tr>
                                    <td>30分鐘</td>
                                </tr>
                                <tr class="info">
                                    <th>6.訓練計劃是否完全取決於你自己，或者有其他人、事會影響你的訓練</th>
                                </tr>
                                <tr>
                                    <td>自己</td>
                                </tr>
                                <tr class="warning">
                                    <td>
                                        <span class="fa fa-commenting">確定最佳化私人教練目標：</span>
                                        健康(身強體壯/克服疾病)
                                    </td>
                                </tr>
                            </table>
                            <h4 ><span class="fa fa-hourglass-half">第二步：風格</span></h4>
                            <table class="panel panel-default table">
                                <tr class="info">
                                    <th>1.在訓練過程中，處在充滿挑戰性的環境，或是在比較有安全感的環境下，對你而言哪一個比較重要？</th>
                                </tr>
                                <tr>
                                    <td>保守型(穩定、單純、實用)</td>
                                </tr>
                                <tr class="info">
                                    <th>2.每次訓練內容上循序漸進穩定的課程編排或是提供有趣多樣化的課程編排，你是否更享受這樣的訓練？</th>
                                </tr>
                                <tr>
                                    <td>保守型(常規)</td>
                                </tr>
                                <tr class="info">
                                    <th>3.你的職業？</th>
                                </tr>
                                <tr>
                                    <td>挑戰型(長時間久坐、重複性動作、容易焦慮或精神緊張)</td>
                                </tr>
                                <tr class="warning">
                                    <td>
                                        <span class="fa fa-commenting">風格評分：傳統型</span>
                                    </td>
                                </tr>
                            </table>
                            <h4 ><span class="fa fa-hourglass-end">第三步：訓練水平</span></h4>
                            <table class="panel panel-default table">
                                <tr class="info">
                                    <th>1.你認為你的工作是？</th>
                                </tr>
                                <tr>
                                    <td>久坐型</td>
                                </tr>
                                <tr class="info">
                                    <th>2.你的愛好是什麼？</th>
                                </tr>
                                <tr>
                                    <td>非活動型（例如：閱讀）</td>
                                </tr>
                                <tr class="info">
                                    <th>3.你是否定期參加娛樂活動？</th>
                                </tr>
                                <tr>
                                    <td>每月/每週一次</td>
                                </tr>
                                <tr class="info">
                                    <th>4.你現在鍛鍊嗎？</th>
                                </tr>
                                <tr>
                                    <td>目前在訓練（核心訓練）</td>
                                </tr>
                                <tr class="warning">
                                    <td>
                                        <span class="fa fa-commenting">訓練水平得分：0-2=初期(初級階段)</span>
                                    </td>
                                </tr>
                            </table>
                            <h4 ><span class="fa fa-hourglass">第四步：參與目標動機</span></h4>
                            <table class="panel panel-default table">
                                <tr class="info">
                                    <th>1.這個目標為什麼對你來說是最重要的?</th>
                                </tr>
                                <tr>
                                    <td>身體健康頭好壯壯</td>
                                </tr>
                                <tr class="info">
                                    <th>2.如果你不做這些改變，保持原狀，或是你的健康與健身出現退步，這將對你的生活產生怎樣的影響？將會出現什麼樣的後果？</th>
                                </tr>
                                <tr>
                                    <td>無法正常生活</td>
                                </tr>
                                <tr class="info">
                                    <th>3.當你成功達成自己的目標時，你的生活會發生何種變化？</th>
                                </tr>
                                <tr>
                                    <td>健康</td>
                                </tr>
                                <tr class="info">
                                    <th>4.對你而言，最大的益處是什麼？</th>
                                </tr>
                                <tr>
                                    <td>健康</td>
                                </tr>
                                <tr class="info">
                                    <th>5.立刻做出這些變化對你而言有多重要？請在1-10內打分數</th>
                                </tr>
                                <tr>
                                    <td>5分</td>
                                </tr>
                                <tr class="info">
                                    <th>6.根據第五題之分數，為什麼不是2分或3分？</th>
                                </tr>
                                <tr>
                                    <td>如果只有２分只需要自己平常去公園走走即可</td>
                                </tr>
                                <tr class="info">
                                    <th>7.根據第五題之分數，你如何得更高的分數？</th>
                                </tr>
                                <tr>
                                    <td>養成良好的運動習慣</td>
                                </tr>
                                <tr class="info">
                                    <th>8.根據第五題之分數，你是否相信你可以做出這些改變？(請在1-10分內為你的信心打分)</th>
                                </tr>
                                <tr>
                                    <td>5分</td>
                                </tr>
                                <tr class="info">
                                    <th>9.根據第五題之分數，如何讓你得信心提高1分？</th>
                                </tr>
                                <tr>
                                    <td>有人在旁邊加油打氣與督促</td>
                                </tr>
                                <tr class="info">
                                    <th>10.根據第五題之分數，你是否準備好？並願意在此時做出改變!</th>
                                </tr>
                                <tr>
                                    <td>是</td>
                                </tr>
                                <tr class="info">
                                    <th>11.根據第五題之分數，你認為我能以何種方式為你提供幫助？</th>
                                </tr>
                                <tr>
                                    <td>督促</td>
                                </tr>
                                <tr class="warning">
                                    <td>
                                        <span class="fa fa-commenting">請在下面寫出會員所使用的關鍵情緒詞語：督促與打氣</span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <!-- End Post -->
                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">回清單頁 <i class="fa fa-th-list" aria-hidden="true"></i></a>

                </div>

            </div>
        </div>
    </div>

    <!-- End content -->

    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    ModelStateDictionary _modelState;
    UserProfile _model;
    IEnumerable<RegisterLesson> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (UserProfile)this.Model;

        _items = models.GetTable<RegisterLesson>().Where(r => r.UID == _model.UID)
            .OrderByDescending(r => r.RegisterID);
        ViewBag.ShowOnly = true;
    }


</script>

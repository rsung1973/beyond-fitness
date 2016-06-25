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

                <div class="col-md-5">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>修改個人資料</span></h4>

                    <!-- Start Contact Form -->

                    <p><strong>會員編號：</strong><span class="text-primary"><%= _model.MemberCode %></span></p>

                    <!-- Divider -->
                    <div class="hr5" style="margin-top: 10px; margin-bottom: 10px;"></div>

                    <% Html.RenderPartial("~/Views/Member/EditLearnerItem.ascx",_model); %>

                    <h4 class="classic-title"><span>PDQ</span></h4>
                    <div class="col-md-12">
                        <h4 class="orange-text"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>第一步：目標</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">1.對於你我相處一小時，你有什麼期望？</label>
                            <textarea class="form-control" rows="3"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q2">2.你希望你的訓練中產生怎樣的結果？</label>
                            <textarea class="form-control" rows="3"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q3">3.在訓練方案中，你需要實現的最重要目標是什麼？</label>
                            <textarea class="form-control" rows="3"></textarea>
                            <div class="sidebar">

                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >A.減肥/身強體壯/運動表現/克服疾病=  挑戰型</a>
                                        <a >B.獲得肌肉/肌肉結實和有彈性=  保守型</a>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q4">4.你希望實現自身目標的時間是多久？</label>
                            <textarea class="form-control" rows="3"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q5">5.除了訓練時段，每天願意額外花多少時間參加幫助你完成目標的活動？</label>
                            <textarea class="form-control" rows="3"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q6">6.訓練計劃是否完全取決於你自己，或者有其他人、事會影響你的訓練</label>
                            <textarea class="form-control" rows="3"></textarea>
                            <div class="sidebar">

                                <!-- Tags Widget -->
                                <h6 class="classic-title"><span>確定最佳化私人教練目標</span></h6>
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >A.獲得肌肉/使肌肉有彈性=  健美</a>
                                        <a >B.體重減輕=  減肥</a>
                                        <a >C.身強體壯/克服疾病= 健康</a>
                                        <a >D.改善運動表現/表現=  運動訓練</a>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <h4 class="orange-text"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>第二步：風格</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">1.在訓練過程中，處在充滿挑戰性的環境，或是在比較有安全感的環境下，對你而言哪一個比較重要？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >Ａ.挑戰、冒險=   挑戰型</a>
                                        <a >B.穩定、單純、實用=  保守型</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">2.每次訓練內容上循序漸進穩定的課程編排或是提供有趣多樣化的課程編排，你是否更享受這樣的訓練？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >A.多樣化=   挑戰型</a>
                                        <a >B.常規=  保守型</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">3.你的職業？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >A.是否需要長時間坐著？   是= 挑戰型  否=保守型</a>
                                        <a >B.是否需要重複性動作？   是= 挑戰型  否=保守型</a>
                                        <a >C.導致你焦慮或精神緊張?  是= 挑戰型  否=保守型</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h6 class="classic-title"><span>風格評分</span></h6>
                        <!-- Some Info -->
                        <p>如果在7分風格問題中，客戶獲得5分，則他認定方案風格為漸進型或傳統型，低於5分或者默認的方案策略為混合型</p>

                        <h4 class="orange-text"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>第三步：訓練水平</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">1.你認為你的工作是？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >0.久坐型</a>
                                        <a >1.活動型</a>
                                        <a >2.耗費體力型</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">2.你的愛好是什麼？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >0.非活動型（例如：閱讀）</a>
                                        <a >1.活動型（例如：園藝）</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">3.你是否定期參加娛樂活動？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >0.每月/每週少於一次(更不頻繁)</a>
                                        <a >1.每月/每週一次</a>
                                        <a >2.每月/每週超過一次(更頻繁)</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">4.你現在鍛鍊嗎？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >0.過去訓練或者從不訓練</a>
                                        <a >2.目前在訓練（何種類型的訓練？）</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h6 class="classic-title"><span>訓練水平得分</span></h6>
                        <!-- Some Info -->
                        <p>0-2=初期(初級階段)</p>
                        <p>3-5=中級(中級階段)</p>
                        <p>6+=高級(高級階段)</p>

                        <h4 class="orange-text"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>第四步：參與目標動機</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">1.這個目標為什麼對你來說是最重要的?</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">2.如果你不做這些改變，保持原狀，或是你的健康與健身出現退步，這將對你的生活產生怎樣的影響？將會出現什麼樣的後果？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">3.當你成功達成自己的目標時，你的生活會發生何種變化？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">4.對你而言，最大的益處是什麼？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">5.立刻做出這些變化對你而言有多重要？請在1-10內打分數</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">6.根據第五題之分數，為什麼不是2分或3分？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">7.根據第五題之分數，你如何得更高的分數？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">8.根據第五題之分數，你是否相信你可以做出這些改變？(請在1-10分內為你的信心打分)</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">9.根據第五題之分數，如何讓你得信心提高1分？</label>
                            <div class="controls">
                                <input type="text" class="requiredField">
                            </div>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">10.根據第五題之分數，你是否準備好？並願意在此時做出改變!</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <div class="form-group has-feedback">
                            <label class="control-label" for="q1">11.根據第五題之分數，你認為我能以何種方式為你提供幫助？</label>
                            <textarea class="form-control" rows="2"></textarea>
                        </div>

                        <h6 class="classic-title"><span>請在下面寫出會員所使用的關鍵情緒詞語</span></h6>
                        <div class="form-group has-feedback">
                            <textarea class="form-control" rows="2"></textarea>
                            <div class="sidebar">
                                <!-- Tags Widget -->
                                <div class="widget widget-tags">
                                    <div class="tagcloud">
                                        <a >疲累</a>
                                        <a >開心</a>
                                        <a >壓力</a>
                                        <a >努力</a>
                                        <a >放鬆</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h6 class="classic-title"><span>方案設計工具結果，請在下面選擇每個方案分類的合適結果</span></h6>
                        <div class="panel panel-default">
                            <table class="table">
                                <tr class="info">
                                    <th>目標</th>
                                    <th>風格</th>
                                    <th>訓練水準</th>
                                </tr>
                                <tr>
                                    <td>
                                        <select class="form-control">
                                            <option>減肥</option>
                                            <option>健身</option>
                                            <option>健美體態</option>
                                            <option>運動表現</option>
                                        </select>
                                    </td>
                                    <td>
                                        <select class="form-control">
                                            <option>保守型</option>
                                            <option>挑戰型</option>
                                            <option>混合型</option>
                                        </select>
                                    </td>
                                    <td>
                                        <select class="form-control">
                                            <option>初期</option>
                                            <option>過渡期</option>
                                            <option>進步期</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <!--<div style="height:60px;border:1px solid #000;">驗證碼區塊</div>-->

                    <div class="hr1" style="margin: 5px 0px;"></div>
                    <a class="btn-system btn-medium" href="<%= VirtualPathUtility.ToAbsolute("~/Member/ListAll") %>">回上頁 <i class="fa fa-reply" aria-hidden="true"></i></a>
                    <a  id="nextStep" class="btn-system btn-medium"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確認</a>

                    <!-- End Contact Form -->

                </div>

            </div>
        </div>
    </div>
    <!-- End content -->





    <script>
        $('#vip,#m_vip').addClass('active');
        $('#theForm').addClass('contact-form');

        $('#nextStep').on('click', function (evt) {

            $('form').prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Member/EditLearner") %>')
              .submit();

            });

    </script>
</asp:Content>
<script runat="server">

    ModelStateDictionary _modelState;
    LearnerViewModel _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _model = (LearnerViewModel)this.Model;
    }
</script>

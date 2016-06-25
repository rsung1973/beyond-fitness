<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Src="~/Views/Shared/PageBanner.ascx" TagPrefix="uc1" TagName="PageBanner" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <uc1:PageBanner runat="server" ID="pageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-3">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>王大明 的行事曆</span></h4>

                    <!-- Start Contact Form -->
                    <!-- Categories Widget -->
                    <div class="widget widget-categories">
                        <ul>
                            <li>
                                <a href="edit-vip-info.htm"><i class="fa fa-cog" aria-hidden="true"></i>修改個人資料</a>
                            </li>
                        </ul>
                    </div>
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                    <div class="row">

                        <div class="col-md-6">
                            <p>剩餘上課次數：10</p>
                        </div>
                        <div class="col-md-6">
                            <p>曠課次數：2</p>
                        </div>

                    </div>
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                    <!-- Responsive calendar - START -->
                    <div class="responsive-calendar">
                        <div class="controls">
                            <a class="pull-left" data-go="prev">
                                <div class="btn btn-primary">上個月</div>
                            </a>
                            <h4><span data-head-year></span><span data-head-month></span></h4>
                            <a class="pull-right" data-go="next">
                                <div class="btn btn-primary">下個月</div>
                            </a>
                        </div>
                        <hr />
                        <div class="day-headers">
                            <div class="day header">一</div>
                            <div class="day header">二</div>
                            <div class="day header">三</div>
                            <div class="day header">四</div>
                            <div class="day header">五</div>
                            <div class="day header">六</div>
                            <div class="day header">日</div>
                        </div>
                        <div class="days" data-group="days">
                        </div>
                    </div>
                    <!-- Responsive calendar - END -->

                    <!-- End Contact Form -->

                </div>
                <div class="col-md-9">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>4/29 課程內容</span></h4>

                    <!-- Start Contact Form -->

                    <div class="row">
                        <div class="col-md-6">
                            <h4 class="orange-text"><span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>著重方向：</h4>
                            <div id="pie_1" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>


                        <div class="col-md-6">
                            <h4 class="orange-text"><span class="glyphicon glyphicon-heart-empty" aria-hidden="true"></span>體適能：</h4>
                            <div id="pie_2" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                        </div>


                        <div class="col-md-12">
                            <h4 class="orange-text"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span>課程：</h4>
                            <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                            <div class="panel panel-default" style="height: 250px; overflow: auto; -webkit-overflow-scrolling: touch;">
                                <div class="panel-body">
                                    <!-- TABLE 1 -->
                                    <table class="table">
                                        <tr class="info">
                                            <th>回合</th>
                                            <th>項目</th>
                                            <th>實際/目標次數</th>
                                            <th>實際/目標強度</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>RDL</td>
                                            <td>5/5次</td>
                                            <td>25/25KG</td>
                                        </tr>
                                        <tr class="warning">
                                            <td colspan="4"><strong>休息時間：</strong>90秒</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"><strong style="float: left;">教練小提示：</strong>
                                                <div style="margin-left: 3.2em;">正反握做得比較好喔</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"><strong style="float: left;">學員心得：</strong>
                                                <div style="margin-left: 3.2em;">應該早點叫我正反握,累死我了</div>
                                            </td>
                                        </tr>
                                    </table>

                                    <!-- TABLE 2 -->
                                    <table class="table">
                                        <tr class="info">
                                            <th>回合</th>
                                            <th>項目</th>
                                            <th>實際/目標次數</th>
                                            <th>實際/目標強度</th>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>TRX</td>
                                            <td>12/12次</td>
                                            <td>--</td>
                                        </tr>
                                        <tr class="warning">
                                            <td colspan="4"><strong>休息時間：</strong>90秒</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"><strong style="float: left;">教練小提示：</strong>
                                                <div style="margin-left: 3.2em;"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"><strong style="float: left;">學員心得：</strong>
                                                <div style="margin-left: 3.2em;">不是說好8次嗎?</div>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>

                        </div>



                    </div>

                    <div class="col-md-6">
                        <h4 class="orange-text"><span class="glyphicon glyphicon-comment" aria-hidden="true"></span>教練總評：</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                        <!-- Start Call Action -->
                        <div class="call-action call-action-boxed call-action-style1 clearfix">
                            <!-- Call Action Button -->
                            <p>加油可以再進步。</p>
                        </div>
                        <!-- End Call Action -->
                    </div>

                    <div class="col-md-6">
                        <h4 class="orange-text"><span class="glyphicon glyphicon-comment" aria-hidden="true"></span>學員心得：</h4>
                        <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>
                        <!-- Start Call Action -->
                        <div class="call-action call-action-boxed call-action-style1 clearfix">
                            <!-- Call Action Button -->
                            <p>我想以後我可以成為女超人拿200KG做RDW</p>
                        </div>
                        <!-- End Call Action -->
                    </div>

                </div>


                <!-- End Contact Form -->

            </div>

        </div>
    </div>
    <!-- End content -->

    <script>
    $('#vip,#m_vip').addClass('active');
    </script>
</asp:Content>

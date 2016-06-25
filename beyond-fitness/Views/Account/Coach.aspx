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

    <uc1:PageBanner runat="server" ID="pageBanner" Title="會員專區" TitleInEng="VIP" />

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-3">

                    <!-- Classic Heading -->
                    <div class="blog-post">
                        <!-- Classic Content -->
                        <div class="user-info clearfix">
                            <div class="user-image">
                                <% _model.RenderUserPicture(this.Writer, "userImg"); %>
                            </div>
                            <div class="user-bio" style="padding-top: 20px;">
                                <h4 class="classic-title"><span>Hi <%= _model.RealName %></span></h4>
                            </div>
                        </div>
                    </div>
                    <!-- End Classic -->

                    <!-- Start Contact Form -->
                    <!-- Categories Widget -->
                    <div class="widget widget-categories">
                        <ul>
                            <li>
                                <a href="<%= VirtualPathUtility.ToAbsolute("~/Member/EditCoach/")+_model.UID %>"><i class="fa fa-cog" aria-hidden="true"></i>修改個人資料</a>
                            </li>
                        </ul>
                    </div>
                    <div class="hr1" style="margin-top: 5px; margin-bottom: 5px;"></div>

                    <!-- Responsive calendar - START -->
                    <%--<div class="responsive-calendar">
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
                    </div>--%>
                    <div class="responsive-calendar">
                        <div class="controls">
                            <a class="pull-left" data-go="prev">
                                <div class="btn btn-primary">上個月</div>
                            </a>
                            <h4><span data-head-year="">2016</span> <span data-head-month="">四月</span></h4>
                            <a class="pull-right" data-go="next">
                                <div class="btn btn-primary">下個月</div>
                            </a>
                        </div>
                        <hr>
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
                            <div class="day mon past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0s;"><a data-day="28" data-month="3" data-year="2016">28</a></div>
                            <div class="day tue past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.01s;"><a data-day="29" data-month="3" data-year="2016">29</a></div>
                            <div class="day wed past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.02s;"><a data-day="30" data-month="3" data-year="2016">30</a></div>
                            <div class="day thu past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.03s;"><a data-day="31" data-month="3" data-year="2016">31</a></div>
                            <div class="day fri past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.04s;"><a data-day="1" data-month="4" data-year="2016">1</a></div>
                            <div class="day sat past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.05s;"><a data-day="2" data-month="4" data-year="2016">2</a></div>
                            <div class="day sun past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.06s;"><a data-day="3" data-month="4" data-year="2016">3</a></div>
                            <div class="day mon past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.07s;"><a data-day="4" data-month="4" data-year="2016">4</a></div>
                            <div class="day tue past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.08s;"><a data-day="5" data-month="4" data-year="2016">5</a></div>
                            <div class="day wed past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.09s;"><a data-day="6" data-month="4" data-year="2016">6</a></div>
                            <div class="day thu past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.1s;"><a data-day="7" data-month="4" data-year="2016">7</a></div>
                            <div class="day fri past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.11s;"><a data-day="8" data-month="4" data-year="2016">8</a></div>
                            <div class="day sat past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.12s;"><a data-day="9" data-month="4" data-year="2016">9</a></div>
                            <div class="day sun past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.13s;"><a data-day="10" data-month="4" data-year="2016">10</a></div>
                            <div class="day mon past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.14s;"><a data-day="11" data-month="4" data-year="2016">11</a></div>
                            <div class="day tue past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.15s;"><a data-day="12" data-month="4" data-year="2016">12</a></div>
                            <div class="day wed past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.16s;"><a data-day="13" data-month="4" data-year="2016">13</a></div>
                            <div class="day thu past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.17s;"><a data-day="14" data-month="4" data-year="2016">14</a></div>
                            <div class="day fri past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.18s;"><a data-day="15" data-month="4" data-year="2016">15</a></div>
                            <div class="day sat past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.19s;"><a data-day="16" data-month="4" data-year="2016">16</a></div>
                            <div class="day sun past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.2s;"><a data-day="17" data-month="4" data-year="2016">17</a></div>
                            <div class="day mon past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.21s;"><a data-day="18" data-month="4" data-year="2016">18</a></div>
                            <div class="day tue past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.22s;"><a data-day="19" data-month="4" data-year="2016">19</a></div>
                            <div class="day wed past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.23s;"><a data-day="20" data-month="4" data-year="2016">20</a></div>
                            <div class="day thu past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.24s;"><a data-day="21" data-month="4" data-year="2016">21</a></div>
                            <div class="day fri past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.25s;"><a data-day="22" data-month="4" data-year="2016">22</a></div>
                            <div class="day sat past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.26s;"><a data-day="23" data-month="4" data-year="2016">23</a></div>
                            <div class="day sun past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.27s;"><a data-day="24" data-month="4" data-year="2016">24</a></div>
                            <div class="day mon past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.28s;"><a data-day="25" data-month="4" data-year="2016">25</a></div>
                            <div class="day tue past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.29s;"><a data-day="26" data-month="4" data-year="2016">26</a></div>
                            <div class="day wed past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.3s;"><a data-day="27" data-month="4" data-year="2016">27</a></div>
                            <div class="day thu past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.31s;"><a data-day="28" data-month="4" data-year="2016">28</a></div>
                            <div class="day fri past active" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.32s;"><a data-day="29" data-month="4" data-year="2016" href="coach.htm">29</a><span class="badge badge-warning">220</span></div>
                            <div class="day sat past" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.33s;"><a data-day="30" data-month="4" data-year="2016">30</a></div>
                            <div class="day sun past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.34s;"><a data-day="1" data-month="5" data-year="2016">1</a></div>
                            <div class="day mon past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.35s;"><a data-day="2" data-month="5" data-year="2016">2</a></div>
                            <div class="day tue past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.36s;"><a data-day="3" data-month="5" data-year="2016">3</a></div>
                            <div class="day wed past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.37s;"><a data-day="4" data-month="5" data-year="2016">4</a></div>
                            <div class="day thu past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.38s;"><a data-day="5" data-month="5" data-year="2016">5</a></div>
                            <div class="day fri past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.39s;"><a data-day="6" data-month="5" data-year="2016">6</a></div>
                            <div class="day sat past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.4s;"><a data-day="7" data-month="5" data-year="2016">7</a></div>
                            <div class="day sun past not-current" style="transform: rotateY(0deg); backface-visibility: hidden; transition: -webkit-transform 0.5s ease 0.41s;"><a data-day="8" data-month="5" data-year="2016">8</a></div>
                        </div>
                    </div>

                    <!-- Responsive calendar - END -->

                    <!-- End Contact Form -->

                </div>
                <div class="col-md-9">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>行事曆清單</span></h4>

                    <!-- Start Contact Form -->
                    <!-- Stat Search -->
                    <div class="navbar bg_gray" style="min-height: 30px;">
                        <div class="search-side">
                            <a class="btn-system btn-small" href="<%= VirtualPathUtility.ToAbsolute("~/Lessons/BookingByCoach") %>">登記上課時間 <span class="fa fa-calendar-plus-o"></span></a>
                            <a href="#" class="btn-system btn-small">自由教練打卡 <i class="fa fa-check-square" aria-hidden="true"></i></a>
                            <a class="btn btn-search" data-toggle="modal" data-target="#searchdil" data-whatever="搜尋"><i class="fa fa-search"></i></a>
                        </div>
                    </div>

                    <div class="hr1" style="margin-top: 5px; margin-bottom: 10px;"></div>

                    <div class="panel panel-default">
                        <!-- TABLE 1 -->
                        <% Html.RenderPartial("~/Views/Lessons/DailyBookingList.ascx", _lessonDate); %>
                    </div>

                    <!-- End Contact Form -->

                    <div id="rotated-labels" style="min-width: 300px; height: 400px; margin: 0 auto"></div>

                </div>

            </div>
        </div>
    </div>
    <% Html.RenderPartial("~/Views/Shared/AlertMessage.ascx"); %>
    <!-- End content -->

    <script>
    $('#vip,#m_vip').addClass('active');
    $('#theForm').addClass('contact-form');

    </script>
</asp:Content>
<script runat="server">

    ModelSource<UserProfile> models;
    UserProfile _model;
    DateTime? _lessonDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (UserProfile)this.Model;
        _lessonDate = (DateTime?)ViewBag.LessonDate;
    }

</script>

<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

        <!-- Start HomePage Slider -->

        <section id="home">
            <!-- Carousel -->
            <div id="main-slide" class="carousel slide" data-ride="carousel">

                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#main-slide" data-slide-to="0" class="active"></li>
                    <li data-target="#main-slide" data-slide-to="1"></li>
                    <li data-target="#main-slide" data-slide-to="2"></li>
                </ol>
                <!--/ Indicators end-->

                <!-- Carousel inner -->
                <div class="carousel-inner">
                    <div class="item active">
                        <img class="img-responsive" src="../images/slider/bg1.jpg" alt="slider">
                        <div class="slider-content">
                            <div class="col-md-12 text-center">
                                <div style="padding: 20px 0px;"></div>
                                <h2 class="animated2 white">
                                    <span>BEYOND FITNESS</span>
                                </h2>
                                <h3 class="animated3 orange-text">
                                    <strong>專業運動科學</strong>
                                </h3>
                            </div>
                        </div>
                    </div>
                    <!--/ Carousel item end -->
                    <div class="item">
                        <img class="img-responsive" src="../images/slider/bg2.jpg" alt="slider">
                        <div class="slider-content">
                            <div class="col-md-12 text-center">
                                <div style="padding: 20px 0px;"></div>
                                <h2 class="animated4 white">
                                    <span>BEYOND FITNESS</span>
                                </h2>
                                <h3 class="animated5 orange-text">
                                    <strong>高端專業訓練品質</strong>
                                </h3>
                            </div>
                        </div>
                    </div>
                    <!--/ Carousel item end -->
                    <div class="item">
                        <img class="img-responsive" src="../images/slider/bg3.jpg" alt="slider">
                        <div class="slider-content">
                            <div class="col-md-12 text-center">
                                <div style="padding: 20px 0px;"></div>
                                <h2 class="animated7 white">
                                    <span>BEYOND FITNESS</span>
                                </h2>
                                <h3 class="animated8 orange-text">
                                    <strong>專業量身訂做規劃</strong>
                                </h3>
                            </div>
                        </div>
                    </div>
                    <!--/ Carousel item end -->
                </div>
                <!-- Carousel inner end-->

            </div>
            <!-- /carousel -->
        </section>
        <!-- End HomePage Slider -->


        <!-- Start Content -->
        <div id="content">
            <div class="container">

                <!-- Start Services Icons -->
                <div class="row">

                    <!-- Start Service Icon 1 -->
                    <div class="col-md-4 col-sm-6 service-box service-center">
                        <div class="service-icon">
                            <i class="fa fa-street-view icon-medium-effect icon-effect-2"></i>
                        </div>
                        <div class="service-content">
                            <h4>高端專業訓練品質</h4>
                            <p>Beyond Fitness 針對不同年齡及需求提供多種體適能課程，以高端專業訓練品質，提供您全方位的服務。</p>
                        </div>
                    </div>
                    <!-- End Service Icon 1 -->

                    <!-- Start Service Icon 2 -->
                    <div class="col-md-4 col-sm-6 service-box service-center">
                        <div class="service-icon">
                            <i class="fa fa-bar-chart icon-medium-effect icon-effect-2"></i>
                        </div>
                        <div class="service-content">
                            <h4>專業量身訂做規劃</h4>
                            <p>Beyond Fitness 了解或許您想一個人、三五好友，或公司團體，都能為您量身訂做規劃，讓您無後顧之憂。</p>
                        </div>
                    </div>
                    <!-- End Service Icon 2 -->

                    <!-- Start Service Icon 3 -->
                    <div class="col-md-4 col-sm-6 service-box service-center">
                        <div class="service-icon">
                            <i class="fa fa-gift icon-medium-effect icon-effect-2"></i>
                        </div>
                        <div class="service-content">
                            <h4>專業以及貼心服務</h4>
                            <p>Beyond Fitness 深入每位學員的體能狀況，提供運動前後的諮詢建議，貼心的服務，做的永遠比您想得更多。</p>
                        </div>
                    </div>

                </div>
                <!-- End Services Icons -->
            </div>
        </div>
        <!-- End content -->
        <!-- Start Full Width Section 2 -->
        <div class="section" style="padding-top: 60px; padding-bottom: 60px; border-top: 1px solid #eee; border-bottom: 1px solid #eee; background: #f9f9f9;">
            <div class="container">

                <!-- Start Call Action -->
                <div class="row">

                    <div class="col-md-6">
                        <!-- Start Big Heading -->
                        <div class="big-title">
                            <h1>關於 BEYOND FITNESS</h1>
                            <p class="title-desc"><span class="accent-color">BEYOND FITNESS ＆ 專業運動科學</span></p>
                        </div>
                        <!-- End Big Heading -->

                        <!-- Some Text -->
                        <p>運動科學是一門對於人類體能運動進行科學化分析的綜合性科學，其研究領域包含早期的人體基礎學理以及運動生理學、運動心理學、運動生物力學、動作控制等方面。 同時包括了營養學以及各種運動科技、人動靜態測量、身體形態測量學、動作功能性分析等。</p>

                        <!-- Divider -->
                        <div class="hr1" style="margin-bottom: 10px;"></div>

                        <!-- Some Text -->
                        <p>BEYOND FITNESS秉持著科學化訓練的原則，有系統、有計畫、有科學實證根據地進行專業訓練。並結合本公司專業體能團隊的訓練實務經驗，以藝術般的手法設計課程活動，提供給客戶最優的訓練品質與體能顧問水準。</p>

                        <!-- Divider -->
                        <div class="hr1" style="margin-bottom: 10px;"></div>

                        <a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional") %>" class="btn-system btn-large" style="margin-bottom: 10px;">選擇理想的體適能 <i class="fa fa-arrow-right" aria-hidden="true"></i></a>
                    </div>
                    <!-- Start Memebr 1 -->
                    <div class="col-md-6 image-service-box">
                        <img class="img-thumbnail" src="../images/index-01.jpg" alt="" />
                    </div>
                </div>
            </div>
        </div>

    <script>
    $('#home,#m_home').addClass('active');
    </script>

</asp:Content>

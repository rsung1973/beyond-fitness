<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Start Page Banner -->
    <div class="page-banner" style="padding: 40px 0; background: url(../images/page_banner_bg.gif);">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>場地租借</h2>
                    <p>Rental</p>
                </div>
                <div class="col-md-6">
                    <ul class="breadcrumbs">
                        <li><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                        <li>場地租借</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- End Page Banner -->

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">


                <div class="col-md-8">
                    <!-- Some Text -->
                    <p>BEYOND FITNESS 提供場地及器材租借服務。</p>
                    <p>無論是功能性訓練器材、重量訓練器材、運動表現訓練器材、拳擊訓練器材等皆能滿足您的需求 。</p>
                    <p>BEYOND FITNESS 歡迎運動相關產業、一般企業公司或相關需求人員來店租場。</p>
                </div>
                <!-- Start Memebr 1 -->
                <div class="col-md-4">
                    <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/ContactUs") %>" class="btn-system btn-large" style="margin-bottom: 10px;">場地器材諮詢服務 <i class="fa fa-question-circle" aria-hidden="true"></i></a></p>
                </div>
            </div>

            <!-- Divider -->
            <div class="hr1" style="margin-bottom: 20px;"></div>

            <div class="row">

                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_01.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_01.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_02.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_02.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_03.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_03.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_04.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_04.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_05.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_05.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_06.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_06.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_07.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_07.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_08.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_08.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_09.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_09.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="訓練場地" href="../images/place/place_10.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/place/place_10.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <!-- End content -->

    <script>
    $('#rental,#m_rental').addClass('active');
    </script>
</asp:Content>

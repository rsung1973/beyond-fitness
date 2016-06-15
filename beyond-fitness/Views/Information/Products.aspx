<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Start Page Banner -->
    <div class="page-banner" style="padding: 40px 0; background: url(../images/page_banner_bg.gif);">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>體適能商品專區</h2>
                    <p>Products</p>
                </div>
                <div class="col-md-6">
                    <ul class="breadcrumbs">
                        <li><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                        <li>體適能商品專區</li>
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

                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="HYPERICE振動滾筒" href="../images/product/product_01.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_01.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>HYPERICE振動滾筒</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：7480元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="HYPERICE冰敷袋" href="../images/product/product_02.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_02.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>HYPERICE 冰敷袋</h4>
                                    <span>（通用部件：膝、肘、腕及小腿）</span>
                                </a>
                                <p class="orange-text">售價：3280元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="HYPERICE冰敷袋" href="../images/product/product_03.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_03.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>HYPERICE冰敷袋</h4>
                                    <span>（膝蓋部件）</span>
                                </a>
                                <p class="orange-text">售價：3580元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="HYPERICE冰敷袋" href="../images/product/product_04.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_04.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>HYPERICE冰敷袋</h4>
                                    <span>（腰背部件）</span>
                                </a>
                                <p class="orange-text">售價：4380元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="HYPERICE人造冰" href="../images/product/product_05.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_05.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>HYPERICE人造冰</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：1280元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩滾筒（長版）" href="../images/product/product_06.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_06.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩滾筒（長版）</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：2600元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩滾筒" href="../images/product/product_07.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_07.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩滾筒</h4>
                                    <span>（長版狼牙棒）</span>
                                </a>
                                <p class="orange-text">售價：2600元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩滾筒（短版）" href="../images/product/product_08.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_08.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩滾筒（短版）</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：1600元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="MINI BAND 迷你彈力帶" href="../images/product/product_09.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_09.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>MINI BAND 迷你彈力帶</h4>
                                    <span>（1組）</span>
                                </a>
                                <p class="orange-text">售價：650元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="SUPER BAND 長版彈力帶" href="../images/product/product_10.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_10.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>SUPER BAND 長版彈力帶</h4>
                                    <span>（1組）</span>
                                </a>
                                <p class="orange-text">售價：3500元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩滾棒（長版）" href="../images/product/product_11.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_11.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩滾棒（長版）</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：900元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩滾棒（短版）" href="../images/product/product_12.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_12.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩滾棒（短版）</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：700元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="GYM BOSS電子訓練計時器" href="../images/product/product_13.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_13.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>GYM BOSS電子訓練計時器</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：1000元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="RIVAL拳擊手套" href="../images/product/product_14.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_14.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>RIVAL拳擊手套</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：1890元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="瑜珈墊" href="../images/product/product_15.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_15.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>瑜珈墊</h4>
                                    <span>(24*68*3.5cm)</span>
                                </a>
                                <p class="orange-text">售價：300元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="BOSU半圓球" href="../images/product/product_16.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_16.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>BOSU半圓球</h4>
                                    <span>(直徑60cm, 附plastic rim)</span>
                                </a>
                                <p class="orange-text">售價：2500元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="敏捷標示圈" href="../images/product/product_17.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_17.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>敏捷標示圈</h4>
                                    <span>(直徑50cm, 60cm)</span>
                                </a>
                                <p class="orange-text">售價：120元/個，150元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="敏捷角錐套組" href="../images/product/product_18.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_18.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>敏捷角錐套組</h4>
                                    <span>(碗型, 40個, 附收納掛勾)</span>
                                </a>
                                <p class="orange-text">售價：2000元/組</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="敏捷角錐" href="../images/product/product_19.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_19.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>敏捷角錐</h4>
                                    <span>(高15cm)</span>
                                </a>
                                <p class="orange-text">售價：60元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="重量訓練桿" href="../images/product/product_20.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_20.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>重量訓練桿</h4>
                                    <span>(4磅,6磅,9磅；長120cm)</span>
                                </a>
                                <p class="orange-text">售價：1000元, 1200元, 1500元</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="專利BEASTIE®深層按摩球" href="../images/product/product_21.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_21.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>專利BEASTIE®深層按摩球</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：1100元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="T-BALL 按摩花生球" href="../images/product/product_22.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_22.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>T-BALL 按摩花生球</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：800元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="平衡墊" href="../images/product/product_23.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_23.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>平衡墊</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：800元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="按摩花生球(大)" href="../images/product/product_24.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_24.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>按摩花生球(大)</h4>
                                    <span></span>
                                </a>
                                <p class="orange-text">售價：850元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6">
                    <!-- Start Project Item -->
                    <div class="portfolio-item item">
                        <div class="portfolio-border">
                            <!-- Start Project Thumb -->
                            <div class="portfolio-thumb">
                                <a class="lightbox" title="滑板" href="../images/product/product_25.jpg">
                                    <div class="thumb-overlay"><i class="fa fa-arrows-alt"></i></div>
                                    <img src="../images/product/product_25.jpg" alt="" />
                                </a>
                            </div>
                            <!-- End Project Thumb -->
                            <!-- Start Project Details -->
                            <div class="portfolio-details">
                                <a >
                                    <h4>滑板</h4>
                                    <span>（3公尺可調式滑板，附一對鞋套）</span>
                                </a>
                                <p class="orange-text">售價：30000元/個</p>
                            </div>
                            <!-- End Project Details -->
                        </div>
                    </div>
                </div>

            </div>


        </div>
    </div>
    <!-- End content -->

    <script>
    $('#products,#m_products').addClass('active');
    </script>
</asp:Content>

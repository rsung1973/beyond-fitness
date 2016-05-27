<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Start Page Banner -->
    <div class="page-banner" style="padding: 40px 0; background: url(../images/page_banner_bg.gif)">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>專業體能訓練</h2>
                    <p>We Are Professional</p>
                </div>
                <div class="col-md-6">
                    <ul class="breadcrumbs">
                        <li><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                        <li>專業體能訓練</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- End Page Banner -->

    <!-- Start Content -->
    <div id="content">
        <div class="container">
            <div class="page-content">

                <div class="row">

                    <!-- Start Image Service Box 1 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-01.jpg" alt="" />
                        <h4>私人體能訓練顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 是私人體能訓練的專家（Sport and Fitness Professional），我們擁有豐富的實務訓練經驗及充實的科學訓練背景知識。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content01" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 1 -->

                    <!-- Start Image Service Box 2 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-02.jpg" alt="" />
                        <h4>專業運動顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 提供最專業的運動競技能力訓練計畫，國際級的肌力與體能訓練專家(Strength and Conditioning Professional)將運動科學（Sport Science）與肌力及體能訓練</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content02" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 2 -->

                    <!-- Start Image Service Box 3 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-03.jpg" alt="" />
                        <h4>青少年體能訓練顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 擁有完善、安全及專業體能訓練背景知識，提供學齡兒童及青少年健康成長、多元發展體能訓練計畫。全程將由相關體能訓練專家團隊，陪同教學、訓練、紀錄與監控評值。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content03" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 3 -->

                    <!-- Start Image Service Box 4 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-04.jpg" alt="" />
                        <h4>銀髮族體能訓練顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness針對每位銀髮族做出最貼心，最安全。最照顧以及最完整四大方向健康體能訓練規劃流程，同時擁有相關的醫療合作機構及多方轉介服務，可提供最專業、最安全與最貼近銀髮族生活中的全方位健康體能需求規劃。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content04" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 4 -->

                    <!-- Start Image Service Box 5 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-05.jpg" alt="" />
                        <h4>小班制及團體課程規劃</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 提供1~6人小班制訓練 、 綜合性訓練、瑜珈、皮拉提斯、多功能循環訓練及TRX懸吊系統訓練。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content05" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 5 -->

                    <!-- Start Image Service Box 6 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-06.jpg" alt="" />
                        <h4>矯正訓練及復健後訓練</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 擁有多家醫療合作機構，如復建科醫師（Rehab Doctor），物理治療單位（Physical Therapy, RT）在於非體能訓練專業部分，將適度轉介(Transfer)相關機構，經醫療或診斷恢復後</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content06" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 6 -->

                    <!-- Start Image Service Box 7 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-07.jpg" alt="" />
                        <h4>孕婦產前產後運動顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">在安全環境下，適當的運動能提供孕婦增進腸胃消化、循環代謝、免疫系統以及增加有利於分娩之身體條件，同時能刺激其胎兒生長發育，有利於胎兒之感覺、器官、平衡、呼吸及免疫系統發展。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content07" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 7 -->

                    <!-- Start Image Service Box 8 -->
                    <div class="col-md-3 image-service-box">
                        <img class="img-thumbnail" src="../images/professional-fitness-08.jpg" alt="" />
                        <h4>訓練派遣與企業顧問</h4>
                        <p class="AutoSkip" style="height: 65px;">Beyond Fitness 提供各項到府派遣私人體能訓練服務包含專業體能顧問(Personal Trainer)、瑜珈(Yoga)、皮拉提斯(Pilates)以及各種符合客戶需求的體能規劃訓練。</p>
                        <p><a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Professional")+"?content=content08" %>" class="btn-system btn-small">詳細內容 <i class="fa fa-chevron-right" aria-hidden="true"></i></a></p>
                    </div>
                    <!-- End Image Service Box 8 -->

                </div>
            </div>

        </div>
    </div>
    <!-- End content -->

    <script>
    $('#professional,#m_professional').addClass('active');
    </script>
</asp:Content>

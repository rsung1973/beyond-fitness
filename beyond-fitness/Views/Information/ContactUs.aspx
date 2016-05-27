<%@ Page Title="" Language="C#" MasterPageFile="~/template/MainPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Start Page Banner -->
    <div class="page-banner" style="padding: 40px 0; background: url(../images/page_banner_bg.gif);">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h2>聯絡我們</h2>
                    <p>Contact Us</p>
                </div>
                <div class="col-md-6">
                    <ul class="breadcrumbs">
                        <li><a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Index") %>">首頁</a></li>
                        <li>聯絡我們</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- End Page Banner -->

    <!-- Start Map -->
    <div id="map" data-position-latitude="25.0519269" data-position-longitude="121.55007599999999"></div>
    <script>
      (function($) {
        $.fn.CustomMap = function(options) {

          var posLatitude = $('#map').data('position-latitude'),
            posLongitude = $('#map').data('position-longitude');

          var settings = $.extend({
            home: {
              latitude: posLatitude,
              longitude: posLongitude
            },
            text: '<div class="map-popup"><h4>BEYOND FITNESS</h4><p>台北市松山區南京東路四段17號B1</p></div>',
            icon_url: $('#map').data('marker-img'),
            zoom: 16
          }, options);

          var coords = new google.maps.LatLng(settings.home.latitude, settings.home.longitude);

          return this.each(function() {
            var element = $(this);

            var options = {
              zoom: settings.zoom,
              center: coords,
              mapTypeId: google.maps.MapTypeId.ROADMAP,
              mapTypeControl: false,
              scaleControl: false,
              streetViewControl: false,
              panControl: true,
              disableDefaultUI: true,
              zoomControlOptions: {
                style: google.maps.ZoomControlStyle.DEFAULT
              },
              overviewMapControl: true,
            };

            var map = new google.maps.Map(element[0], options);

            var icon = {
              url: settings.icon_url,
              origin: new google.maps.Point(0, 0)
            };

            var marker = new google.maps.Marker({
              position: coords,
              map: map,
              icon: icon,
              draggable: false
            });

            var info = new google.maps.InfoWindow({
              content: settings.text
            });

            google.maps.event.addListener(marker, 'click', function() {
              info.open(map, marker);
            });

            var styles = [{
              "featureType": "landscape",
              "stylers": [{
                "saturation": -100
              }, {
                "lightness": 65
              }, {
                "visibility": "on"
              }]
            }, {
              "featureType": "poi",
              "stylers": [{
                "saturation": -100
              }, {
                "lightness": 51
              }, {
                "visibility": "simplified"
              }]
            }, {
              "featureType": "road.highway",
              "stylers": [{
                "saturation": -100
              }, {
                "visibility": "simplified"
              }]
            }, {
              "featureType": "road.arterial",
              "stylers": [{
                "saturation": -100
              }, {
                "lightness": 30
              }, {
                "visibility": "on"
              }]
            }, {
              "featureType": "road.local",
              "stylers": [{
                "saturation": -100
              }, {
                "lightness": 40
              }, {
                "visibility": "on"
              }]
            }, {
              "featureType": "transit",
              "stylers": [{
                "saturation": -100
              }, {
                "visibility": "simplified"
              }]
            }, {
              "featureType": "administrative.province",
              "stylers": [{
                "visibility": "on"
              }]
            }, {
              "featureType": "water",
              "elementType": "labels",
              "stylers": [{
                "visibility": "on"
              }, {
                "lightness": -25
              }, {
                "saturation": -100
              }]
            }, {
              "featureType": "water",
              "elementType": "geometry",
              "stylers": [{
                "hue": "#ffff00"
              }, {
                "lightness": -25
              }, {
                "saturation": -97
              }]
            }];

            map.setOptions({
              styles: styles
            });
          });

        };
      }(jQuery));

      jQuery(document).ready(function() {
        jQuery('#map').CustomMap();
      });
    </script>
    <!-- End Map -->

    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d3614.472973471831!2d121.55001928836059!3d25.051953560977104!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3442abe83c187613%3A0x896b13dfd50e7a7b!2zMTA15Y-w5YyX5biC5p2-5bGx5Y2A5Y2X5Lqs5p2x6Lev5Zub5q61MTfomZ8!5e0!3m2!1szh-TW!2stw!4v1422857105442" width="1000" height="450" frameborder="0" style="border: 0"></iframe>

    <!-- Start Content -->
    <div id="content">
        <div class="container">

            <div class="row">

                <div class="col-md-8">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>聯絡我們</span></h4>

                    <!-- Start Contact Form -->
                        <div class="form-group">
                            <div class="controls">
                                <input type="text" placeholder="姓名" name="SendName">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="controls">
                                <input type="email" class="email" placeholder="Email" name="email">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="controls">
                                <input type="text" class="requiredField" placeholder="問題主旨" name="Title">
                            </div>
                        </div>

                        <div class="form-group">

                            <div class="controls">
                                <textarea rows="7" placeholder="您的信件內容" name="content"></textarea>
                            </div>
                        </div>
                        <button type="submit" id="submit" class="btn-system btn-large">送出 <i class="fa fa-paper-plane" aria-hidden="true"></i></button>
                        <div id="success" style="color: #34495e;"></div>
                    
                    <!-- End Contact Form -->

                </div>

                <div class="col-md-4">

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>相關訊息</span></h4>

                    <!-- Some Info -->
                    <p>若您有體適能運動相關問題，歡迎您與 Beyond Fitness 聯繫。能為您服務是我們的榮幸。</p>

                    <!-- Divider -->
                    <div class="hr1" style="margin-bottom: 10px;"></div>

                    <!-- Info - Icons List -->
                    <ul class="icons-list">
                        <li><i class="fa fa-phone"></i><strong>電 話：</strong> <u><a href="tel:+886227152733">02-2715 2733</a></u></li>
                        <li><i class="fa fa-map-marker"></i><strong>地 址：</strong> 台北市松山區南京東路四段17號B1</li>
                        <li><i class="fa fa-envelope-o"></i><strong>Email：</strong> <a href="mailto:info@beyond-fitness.tw"><u>info@beyond-fitness.tw</u></a></li>
                    </ul>

                    <!-- Divider -->
                    <div class="hr1" style="margin-bottom: 15px;"></div>

                    <!-- Classic Heading -->
                    <h4 class="classic-title"><span>營業時間</span></h4>

                    <!-- Info - List -->
                    <ul class="list-unstyled">
                        <li><strong>週一 - 週五</strong> - 9am to 9pm</li>
                        <li><strong>週末</strong> - 7am to 10pm</li>
                    </ul>

                </div>

            </div>

        </div>
    </div>
    <!-- End content -->

    <script>
        $('#contactUs,#m_contactUs').addClass('active');
        $('#theForm').addClass('contact-form');
    </script>
</asp:Content>

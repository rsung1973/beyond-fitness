﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<!--dropify-->
<link rel="stylesheet" href="plugins/dropify/css/dropify.min.css">

<h4 class="card-outbound-header">上傳內容</h4>
<div class="card widget_2">
    <ul class="row clearfix list-unstyled m-b-0">
        <li class="col-12">
            <div class="body">
                <input type="file" class="dropify" name="uploadFile" id="<%= _dropifyID %>" data-max-file-size="10M" data-show-remove="true" data-show-errors="true" data-errors-position="outside" data-allowed-file-extensions="zip">
            </div>
        </li>
    </ul>
</div>
<script>
    var drEvent;
    $(function () {

        function init() {

            var $file = $('#<%= _dropifyID %>');
            var $container = $file.parent();

            //$file.on('change', function (event) {
            //    if ($global.uploadFile) {
            //        $global.uploadFile($file, function () {
            //            $container.append($file);
            //        });
            //    }
            //});

            drEvent = $file.dropify({
                messages: {
                    'default': '點選或拖拉檔案到這邊',
                    'replace': '點選或拖拉檔案取代目前內容',
                    'remove': '刪除內容',
                    'error': 'Ooops, 肯定做錯了什麼'
                },
                error: {
                    'fileSize': 'Ooops, 上傳的檔案太大了啦！ ({{ value }} max).',
                    'imageFormat': 'Ooops, 上傳檔案是ZIP檔嗎？！ ({{ value }} only).'
                }
            });

            //drEvent.on('dropify.errors', function () {
            //    alert('error...');
            //});

            drEvent.on('dropify.ready', function () {
                if ($global.uploadFile) {
                    $global.uploadFile($file, function () {
                        $container.append($file);
                    });
                }
            });
        }

        if (!$.fn.dropify) {
            loadScript('plugins/dropify/js/dropify.js', function () {
                init();
            });
        } else {
            init();
        }
    });
</script>

<script runat="server">

    String _dropifyID = $"dropify{DateTime.Now.Ticks}";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

</script>

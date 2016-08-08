<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%--<div class="form-horizontal modal fade" id="confirmDialog" tabindex="-1" role="dialog" aria-labelledby="searchdilLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span><span id="confirmTitle">系統訊息</span></h4>
            </div>
            <div class="modal-body">
                <!-- Stat Search -->
                <div class="form-group">
                    <label for="alertMsg" id="confirmMsg" class="col-md-12"></label>
                    <div class="col-md-12 modal-footer">
                        <button id="btnConfirmIt" type="button" class="btn btn-system btn-sm"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span>確定</button>
                        <button type="button" class="btn btn-default btn-sm" data-dismiss="modal"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>取消</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>--%>

<script>
    function confirmIt(message, onConfirm) {

<%--        if (message.title)
            $('#confirmTitle').text(message.title);
        $('#confirmMsg').text(message.message);
        $('#btnConfirmIt').off('click').on('click', function (evt) {
            $('#confirmDialog').modal('hide');
            onConfirm(evt);
        });

        $('#confirmDialog').modal('show');
--%>
            $.SmartMessageBox({
                title: "<i class=\"fa fa-fw fa fa-trash-o\" aria-hidden=\"true\"></i> " + message.title,
                content: message.message,
                buttons: '[確定][取消]'
            }, function (ButtonPressed) {
                if (ButtonPressed == "確定") {
                    onConfirm();
                }
            });
    }
</script>

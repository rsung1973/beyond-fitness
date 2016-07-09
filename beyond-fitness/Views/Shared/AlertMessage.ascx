<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (ViewBag.Message != null)
    { %>
<div class="form-horizontal modal fade" id="alertDialog" tabindex="-1" role="dialog" aria-labelledby="searchdilLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="searchdilLabel"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>系統訊息</h4>
            </div>
            <div class="modal-body">
                <!-- Stat Search -->
                <div class="form-group">
                    <label for="alertMsg" class="col-md-12"><%= ViewBag.Message %></label>
                    <div class="col-md-12 modal-footer">
                        <button type="button" id="btnDismiss" class="btn btn-system btn-sm" data-dismiss="modal"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>關閉</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $(function () {
        var $modal = $('#alertDialog');
        $modal.on('hidden.bs.modal', function (evt) {
            $modal.remove();
        });
        $modal.modal('show');
    });
</script>
<%  } %>

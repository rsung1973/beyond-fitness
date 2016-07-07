<%@ Control Language="C#" AutoEventWireup="true" %>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery-2.1.4.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.migrate.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/modernizrr.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/asset/js/bootstrap.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.fitvids.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/owl.carousel.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/nivo-lightbox.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.isotope.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.appear.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/count-to.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.textillate.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.lettering.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.easypiechart.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.nicescroll.min.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.parallax.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.slicknav.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/bootstrap-datetimepicker.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/locales/bootstrap-datetimepicker.zh-TW.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/moment.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.form.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%=VirtualPathUtility.ToAbsolute("~/js/locales/messages_zh_TW.js") %>"></script>
<script>
    var $formValidator;

    $(function () {

        $formValidator = $("form").validate({
            //debug: true,
            //errorClass: "label label-danger",

            success: function (label, element) {
                label.remove();
                var id = $(element).prop("id");
                $('#' + id + 'Icon').removeClass('glyphicon-remove').removeClass('text-danger')
                    .addClass('glyphicon-ok').addClass('text-success');
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element);
                var id = $(element).prop("id");
                $('#' + id + 'Icon').addClass('glyphicon-remove').addClass('text-danger')
                    .removeClass('glyphicon-ok').removeClass('text-success');
            }
        });

        $.validator.addMethod("regex", function (value, element, regexpr) {
            return regexpr.test(value);
        }, "資料格式錯誤!!");
    });
</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<input type="text" class="knob" data-linecap="round" data-width="90" data-height="90" data-thickness="0.25" data-anglearc="250" data-angleoffset="-125" data-fgcolor="#ec74a1" readonly id="<%= _dialogID %>"/>
<script>
    $(function () {
        drawKnob($("#<%= _dialogID %>"),<%= _model %>, 3800);
    });
</script>
<%  Html.RenderPartial("~/Views/ConsoleHome/Shared/KnobJS.ascx"); %>
<%--<script>
    $(function () {

        function drawKnob() {
            $("#<%= _dialogID %>").knob();
            $({
                animatedVal: 0
            }).animate({
                animatedVal: <%= _model %>
            }, {
                    duration: 3800,
                    easing: "swing",
                    step: function () {
                        $("#<%= _dialogID %>").val(Math.ceil(this.animatedVal)).trigger("change");
                    }
                });
        }

        if ($global.knobJS == undefined) {
            loadScript('bundles/knob.bundle.js', function () {
                $global.knobJS = true;
                drawKnob();
            });
        } else {
            drawKnob();
        }
    });

</script>--%>
<script runat="server">

    int? _model;
    String _dialogID = $"knob{DateTime.Now.Ticks}";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _model = this.Model as int?;
    }

</script>

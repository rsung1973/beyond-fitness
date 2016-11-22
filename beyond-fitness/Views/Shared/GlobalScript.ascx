<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<script>

    var $global = {};

    function showRecentLessons(uid, lessonID) {
        showLoading();
        $.post('<%= Url.Action("LearnerRecentLessons","Report") %>', { 'uid': uid, 'lessonID': lessonID,'cloneLesson': <%= ViewBag.CloneLesson==true ? "true" : "false"  %> }, function (data) {
            $(data).appendTo($('#content'));
            hideLoading();
        });
    }

    function showLearnerLesson(lessonID,attendance) {
        var event = event || window.event;
        showLoading(true);
        $.post('<%= Url.Action("LearnerLesson","Activity") %>', { 'lessonID': lessonID,'attendance': attendance }, function (data) {
            $(data).appendTo($('#content'));
            hideLoading();
        });
    }

    function learnerAttendLesson(lessonID) {
        var event = event || window.event;
        showLoading();
        $.post('<%= Url.Action("LearnerAttendLesson","Attendance") %>', { 'lessonID': lessonID }, function (data) {
            hideLoading();
            if (data) {
                if(data.result) {
                    smartAlert("已完成打卡!!");
                    $(event.target).remove();
                } else {
                    smartAlert(data.message);
                }
            }
        });
    }
    
    function makeTrainingPlan(lessonID) {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/MakeTrainingPlan") %>');
        $('<input type="hidden"/>')
            .prop('name', 'lessonID').prop('value', lessonID).appendTo($form);
        startLoading();
        $form.submit();
    }

    function makeLessonPlan(arg)
    {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/TrainingPlan") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        showLoading(true,function(){
            $form.submit();
        });
    }

    function updateLessonFeedBack(lessonID) {
        showLoading(true);
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Activity/UpdateLessonFeedBack") %>',
            {
                'lessonID': lessonID,
                'feedBack': $('#lessonFeedBack').val()
            }, function (data) {
                hideLoading();
                if (data) {
                    $('.feedback-item').html(data);
                    smartAlert('資料已更新!!');
                } else {
                    smartAlert(data.message);
                }
            });
    }

    function previewLesson(arg)
    {
        var $form = $('<form method="post"/>')
            .appendTo($('body'))
            .prop('action', '<%= VirtualPathUtility.ToAbsolute("~/Lessons/PreviewLesson") %>');
        for (var key in arg) {
            $('<input type="hidden"/>')
            .prop('name', key).prop('value', arg[key]).appendTo($form);
        }
        showLoading(true, function () {
            $form.submit();
        });
    }

    function showLoading(autoHide,onBlock) {
        $.blockUI({
            message:  '<img src="<%= VirtualPathUtility.ToAbsolute("~/img/loading.gif") %>" /><h1>Loading</h1>', 
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            },
            // 背景圖層
            overlayCSS:  { 
                backgroundColor: '#3276B1', 
                opacity:         0.6, 
                cursor:          'wait' 
            },
            onBlock: onBlock
        });

        if(autoHide)
            setTimeout($.unblockUI, 5000);
    }

    function hideLoading() {
        $.unblockUI();
    }

    function validateForm(formElement) {
        var isValid = true;
        $(formElement).find('label.error').remove();
        $(formElement).find('.error').removeClass('error');
        $(formElement.elements).each(function(idx,elmt) {
            var $elmt = $(elmt);
            elmt.setCustomValidity('');
            if(!$elmt.is(':hidden') && $elmt.parents().filter(':hidden').length==0 && !elmt.checkValidity()) {
                isValid=false;
                $elmt.addClass('error');
                if($elmt.prop('placeholder')) {
                    //elmt.setCustomValidity($elmt.prop('placeholder'));
                    $('<label class="error"></label>').text($elmt.prop('placeholder'))
                        .insertAfter($elmt);
                }
            }
        });
        return isValid;
    }

    function drawTrendPie(assessmentID,idx) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Activity/FitnessAssessmentTrendPieData") %>', { 'assessmentID': assessmentID }, function (data) {
            if (idx) {
                drawPie($("#trend-pie" + assessmentID + idx), data);
            } else {
                drawPie($("#trend-pie" + assessmentID), data);
            }
        });
    }

    function drawStrengthPie(assessmentID,idx) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Activity/FitnessAssessmentStrengthPieData") %>', { 'assessmentID': assessmentID }, function (data) {
            if (idx) {
                drawPie($("#strength-pie" + assessmentID + idx), data);
            } else {
                drawPie($("#strength-pie" + assessmentID), data);
            }
        });
    }

    function drawGroupPie(item) {
        var $pie;
        if (item.index) {
            $pie = $("#group-pie" + item.assessmentID + "-" + item.itemID + item.index);
        } else {
            $pie = $("#group-pie" + item.assessmentID + "-" + item.itemID);
        }
        if ($pie) {
            $.post('<%= VirtualPathUtility.ToAbsolute("~/Activity/FitnessAssessmentGroupPieData") %>', item, function (data) {
                drawPie($pie, data);
            });
        }
    }

</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

</script>

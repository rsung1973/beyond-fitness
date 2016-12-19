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

    <%-- begin temp --%>
    function updateHealthAssessment() {
        var event = event || window.event;
        var hasValue = false;
        var $form = $(event.target).closest('form');
        $form.find('input').each(function (idx) {
            if ($(this).val() != '') {
                hasValue = true;
            }
        });
        if(hasValue) {
            $form.ajaxSubmit({
                success: function (data) {
                    $('.'+$form.prop('id')).html(data);
                    smartAlert("資料已儲存!!");
                }
            });
        } else {
            smartAlert("請輸入至少一個項目!!" + 'id:' + $form.prop('id'));
        }
    }

    function editRecentStatus(uid) {
        var event = event || window.event;
        $p_status = $(event.target).parent().prev();
        $.post('<%= Url.Action("EditRecentStatus","Lessons") %>', { 'uid': uid }, function (data) {
            $(data).appendTo($('#content'));
        });
    }


    var $p_status;
    function commitRecentStatus(uid, recentStatus) {
        $.post('<%= VirtualPathUtility.ToAbsolute("~/Lessons/CommitRecentStatus") %>', { 'uid': uid, 'recentStatus': recentStatus }, function (data) {
            if (data) {
                if (data.result) {
                    $p_status.html(recentStatus.replace(/\n/g, '<br/>'));
                }
                smartAlert(data.message);
            }
        });
    }

    function flipflop() {
        var event = event || window.event;
        var $this = $(event.target);
        if ($this.hasClass('openureye')) {
            var $p = $this.parent().parent().next();
            $p.hide().next().show();//.next().show();
            $('p.secret-info').show();
            $this.removeClass('fa-eye openureye');
            $this.addClass('fa-eye-slash closeureye');
        } else {
            var $p = $this.parent().parent().next();
            $p.show().next().hide();//.next().hide();
            $('p.secret-info').hide();
            $this.removeClass('fa-eye-slash closeureye');
            $this.addClass('fa-eye openureye');
        }
    }

    function editHealth(value) {
        var $li = $('li.contentTab.active');
        $li.removeClass('active');
        $($li.find('a').attr('href')).removeClass('active in');
        $('a[href="#content' + value + '_7"]').closest('li').addClass('active');
        $('#content' + value + '_7').addClass('active in');
    }

    <%-- end Temp --%>

    function bookingSelfTraining(lessonID) {
        var event = event || window.event;
        showLoading(true);
        $.post('<%= Url.Action("BookingSelfTraining","Lessons") %>', { 'lessonID': lessonID }, function (data) {
            $(data).appendTo($('body'));
            hideLoading();
        });
    }

    function showRecentLessons(uid, lessonID) {
        showLoading();
        $.post('<%= Url.Action("LearnerRecentLessons","Report") %>', { 'uid': uid, 'lessonID': lessonID,'cloneLesson': <%= ViewBag.CloneLesson==true ? "true" : "false"  %> }, function (data) {
            $(data).appendTo($('#content'));
            hideLoading();
        });
    }

    function assessLearnerHealth(lessonID,uid) {
        var event = event || window.event;
        showLoading(true);
        $.post('<%= Url.Action("EditLearnerHealth","Fitness") %>', { 'lessonID': lessonID,'uid': uid }, function (data) {
            $(data).appendTo($('body'));
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

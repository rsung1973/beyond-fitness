﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<div id="<%= _dialog %>" title="編輯合約資料(1對1)" class="bg-color-darken">
    <div class="modal-body bg-color-darken txt-color-white no-padding">
        <form class="smart-form" method="post" autofocus>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">合約名稱</label>
                        <label class="select">
                            <select name="ContractType">
                                <%  foreach (var item in models.GetTable<CourseContractType>().Where(c => c.TypeID < 5))
                                    { %>
                                <option value="<%= item.TypeID %>"><%= item.TypeName %></option>
                                <%  } %>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                            <script>
                                function selectContractType() {
                                    $('div.modal-title h4').html('<i class="fa fa-edit"></i>  合約名稱：' + $('#<%= _dialog %> select[name="ContractType"] option:selected').text());
                                    $global.contractType = $('#<%= _dialog %> select[name="ContractType"]').val();
                                    if($global.contractType=="2" || $global.contractType=="3") {
                                        $('#members label.label').html('學員資料 <span class="label-warning"><i class="fa fa-asterisk"></i> 為主約簽名代表</span>');
                                    } else {
                                        $('#members label.label').html('學員資料');
                                    }
                                }
                                $(function() {
                                    $('#<%= _dialog %> select[name="ContractType"]').on('change',function(evt) {
                                        selectContractType();
                                    });
                                    $('#<%= _dialog %> select[name="ContractType"]').val('<%= _viewModel.ContractType ?? 1 %>');
                                });
                            </script>
                        </label>
                    </section>
                    <section class="col col-6">
                        <%  if (_profile.IsAssistant() || _profile.IsManager() || _profile.IsViceManager())
                            { %>
                        <label class="label">體能顧問</label>
                        <label class="select">
                            <select name="FitnessConsultant">
                                <option value="">請選擇體能顧問</option>
                                <%  IQueryable<ServingCoach> items = _profile.IsAssistant()
                                                    ? models.GetTable<ServingCoach>()
                                                    : _profile.GetServingCoachInSameStore(models);
                                    Html.RenderPartial("~/Views/SystemInfo/ServingCoachOptions.ascx", items); %>
                            </select>
                            <i class="icon-append fa fa-file-word-o"></i>
                        </label>
                        <script>
                            $(function(){
                                $('#<%= _dialog %> select[name="FitnessConsultant"]').val('<%= _viewModel.FitnessConsultant %>');
                            });
                        </script>
                        <%  }
                            else
                            { %>
                        <input type="hidden" name="FitnessConsultant" value="<%= _profile.UID %>" />
                        <%  } %>
                    </section>
                </div>
            </fieldset>
            <fieldset>
                <section id="members">
                    <label class="label">學員資料</label>
                    <%  Html.RenderAction("ListContractMember", "CourseContract",new { _viewModel.UID, _viewModel.ContractType, _viewModel.OwnerID, viewOnly = (bool?)ViewBag.ViewOnly }); %>
                </section>
                <input type="hidden" name="OwnerID" value="<%= _viewModel.OwnerID %>" />
                <script>
                    $(function() {
                        $global.renderMember = function() {
                            $('#members table').remove();
                            showLoading();
                            $.post('<%= Url.Action("ListContractMember","CourseContract") %>',{'uid':$global.UID, 'contractType':$('#<%= _dialog %> select[name="ContractType"]').val(),'ownerID':$('input[name="OwnerID"]').val()},function(data){
                                hideLoading();
                                $(data).appendTo($('#members'));
                                $global.UID = [];
                                var hasOwner=false;
                                var owner = $('input[name="OwnerID"]').val();
                                $('input[name="UID"]').each(function(idx,element){
                                    var uid = $(this).val();
                                    $global.UID.push(uid);
                                    if(owner==uid)
                                        hasOwner=true;
                                });
                                $global.referenceUID = $global.UID.length>0 ? $global.UID[$global.UID.length-1] : null;
                                if(!hasOwner)
                                    $('input[name="OwnerID"]').val('');
                                loadPriceList();
                            });
                        };
                    });
                </script>
            </fieldset>
            <fieldset>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">上課地點</label>
                        <label class="select">
                            <select name="BranchID">
                                <%  if (_profile.IsAssistant())
                                    {
                                    }
                                    else
                                    {
                                        ViewBag.IntentStore = _profile.ServingCoach.CoachWorkplace.Select(w => w.BranchID).ToArray();
                                    }
                                    Html.RenderPartial("~/Views/SystemInfo/BranchStoreOptions.ascx", model: _viewModel.BranchID);
                                %>
                            </select>
                            <i class="icon-append fa fa-at"></i>
                            <script>
                                $('#<%= _dialog %> select[name="BranchID"]').on('change',function(evt) {
                                    loadPriceList();
                                });
                            </script>
                        </label>
                    </section>
                    <section class="col col-6">
                        <label class="label">上課時間長度 <span class="label-info"><i class="fa fa-info-circle "></i>單堂原價：<span id="singlePrice"><%= _model!=null && _model.LessonPriceType.SeriesID.HasValue ? String.Format("{0:##,###,###,###}",_model.LessonPriceType.CurrentPriceSeries.LessonPriceType.ListPrice) : null %></span></span>
                        </label>
                        <label class="select">
                            <select name="DurationInMinutes">
                                <option value="60">60分鐘</option>
                                <option value="90">90分鐘</option>
                            </select>
                            <i class="icon-append fa fa-clock-o"></i>
                            <script>
                                $('#<%= _dialog %> select[name="DurationInMinutes"]').on('change',function(evt) {
                                    loadPriceList();
                                });
                            </script>
                        </label>
                    </section>
                </div>
                <div class="row">
                    <section class="col col-6">
                        <label class="label">購買堂數</label>
                        <label class="input">
                            <i class="icon-append fa fa-shopping-cart"></i>
                            <input type="number" name="Lessons" min="1" maxlength="10" placeholder="請輸入數字" value="<%= _viewModel.Lessons %>" />
                        </label>
                        <script>
                            $('input[name="Lessons"]').on('blur',function(evt) {
                                matchLessonPrice();
                                calcTotalCost();
                            });

                            function matchLessonPrice() {
                                var $this = $('input[name="Lessons"]');
                                if($this.val()!='' && $priceList.length>0) {
                                    var lessons = parseInt($this.val());
                                    var idx;
                                    for(idx=0;idx<$priceList.length;idx++) {
                                        if(lessons>=$priceList[idx].lowerLimit) {
                                            continue;
                                        } else {
                                            break;
                                        }
                                    }
                                    if(idx>0)
                                        $priceList[idx-1].option.prop('selected',true);                                }
                            }
                        </script>
                    </section>
                    <section class="col col-6">
                        <label class="label">課程單價 <span class="label-warning"><i class="fa fa-info-circle "></i>服務總費用：<span id="totalCost"><%= _model!=null ? String.Format("{0:##,###,###,###}",_model.TotalCost) : null %></span></span>
                        </label>
                        <label class="select">
                            <select name="PriceID">
                                <option value="">請選擇</option>
                            </select>
                            <i class="icon-append fa fa-at"></i>
                        </label>
                        <script>
                            $(function(){
                                $global.defaultPriceID = '<%= _viewModel.PriceID %>';
                            });

                            $('#<%= _dialog %> select[name="PriceID"]').on('change',function(evt) {
                                calcTotalCost();
                            });

                            function calcTotalCost() {
                                var $form = $('#<%= _dialog%> form');
                                $.post('<%= Url.Action("CalcTotalCost","CourseContract",_viewModel.ContractID) %>',$form.serialize(),function(data) {
                                    if(data.result) {
                                        $('#totalCost').text(data.totalCost);
                                    } else {
                                        $('#totalCost').text(data.message);
                                    }
                                });
                            }
                        </script>
                    </section>
                    <script>
                        var $priceList;
                        console.log('debug...');
                        function loadPriceList() {
                            $.post('<%= Url.Action("ListLessonPrice","CourseContract",new { _viewModel.PriceID }) %>',{ 'branchID':$('#<%= _dialog %> select[name="BranchID"]').val(), 'duration':$('#<%= _dialog %> select[name="DurationInMinutes"]').val(),'feature': $global.useLearnerDiscount ? '<%= (int?)Naming.LessonPriceFeature.舊會員續約 %>':null },function(data){
                                $('#<%= _dialog %> select[name="PriceID"]').empty()
                                .html('<option value="">請選擇</option>')
                                .append($(data));
                                //$('input[name="Lessons"]').val('');
                                $priceList = [];
                                $('#singlePrice').text('');
                                $('#<%= _dialog %> select[name="PriceID"] option[lowerLimit]').each(function(idx,element) {
                                    var $opt = $(this);
                                    if($opt.attr('lowerLimit')!='') {
                                        $priceList.push({
                                            'lowerLimit':parseInt($opt.attr('lowerLimit')),
                                            'upperBound':$opt.attr('upperBound')=='' ? null : parseInt($opt.attr('upperBound')),
                                            'option':$opt
                                        });
                                    }
                                });

                                if($priceList.length>0) {
                                    $('#singlePrice').text($priceList[0].option.attr('listPrice'));
                                }

                                if($global.defaultPriceID!='') {
                                    $('#<%= _dialog %> select[name="PriceID"]').val($global.defaultPriceID);
                                    $global.defaultPriceID = '';
                                } else {
                                    if($priceList.length>0) {
                                        matchLessonPrice();
                                    }
                                }

    <%  if (ViewBag.ViewOnly == true)
        {   %>
                                $('#<%= _dialog %> select').each(function (idx, element) {
                                    var $this = $(this);
                                    var $option = $this.find('option:selected');
                                    $this.empty().append($option);
                                });
    <%  }   %>
                            });
                        }

                        $(function(){
                            $global.useLearnerDiscount = <%= _useLearnerDiscount ? "true" : "false" %>;
                            loadPriceList();
                        });
                    </script>
                </div>
            </fieldset>
            <fieldset>
                <section>
                    <label class="label">備註</label>
                    <textarea class="form-control" name="Remark" placeholder="請輸入備註" rows="3"><%= _viewModel.Remark %></textarea>
                </section>
            </fieldset>
            <input type="hidden" name="ContractID" value="<%= _viewModel.ContractID %>" />
        </form>
    </div>
    <script>
        $('#<%= _dialog %>').dialog({
            //autoOpen: false,
            width: "1024",
            resizable: false,
            modal: true,
            title: "<div class='modal-title'><h4><i class='fa fa-edit'></i>  合約名稱：1對1</h4></div>",
            buttons: [
        <%  if (ViewBag.ViewOnly != true)
        {   %>
            {
                html: "<i class='fa fa-floppy-o'></i>&nbsp; 儲存草稿",
                "class": "btn bg-color-darken",
                click: function () {
                    var $form = $('#<%= _dialog%> form');
                    clearErrors();
                    $.post('<%= Url.Action("SaveContract", "CourseContract", _viewModel.ContractID) %>',$form.serialize(),function(data) {
                        if(data.result) {
                            alert('合約資料已儲存草稿!!');
                            showLoading();
                            window.location.href='<%= Url.Action("CreateContract", "CourseContract") %>';
                        } else {
                            $(data).appendTo($('body'));
                        }
                    });
                    <%--$('#<%= _dialog%>').dialog("close");--%>
                }
            }, {
                html: "<i class='fa fa-send'></i>&nbsp; 確定產生合約",
                "class": "btn btn-primary",
                click: function () {
                    if (confirm("請再次確認合約內容資料正確?")) {
                        var $form = $('#<%= _dialog%> form');
                        clearErrors();
                        $.post('<%= Url.Action("CommitContract", "CourseContract", _viewModel.ContractID) %>',$form.serialize(),function(data) {
                            if(data.result) {
                                <%-- alert(data.status == 1202 ? '合約資料待店長審核，請至行事曆待辦事項點選待簽名連結！' : '合約資料待客戶簽名，請至行事曆待辦事項點選待簽名連結！' );--%>
                                //window.open('<%= Url.Action("ContractSignatureView", "CourseContract") %>' + '?contractID=' + data.contractID, '_blank', 'fullscreen=yes');
                                showLoading();
                                window.location.href='<%= Url.Action("Index", "CoachFacet",new { showTodoTab = true }) %>';
                            } else {
                                $(data).appendTo($('body'));
                            }
                        });
                    } 
                }
            }, 
        <%  }   %>
            <%--{
                html: "<i class='fa fa-pencil'></i>&nbsp; 送交簽名",
                "class": "btn bg-color-green",
                click: function () {
                    if (confirm("請再次確認合約內容資料正確")) {
                        $(this).dialog("close");
                        window.open("contractsign.html", "_blank", "width:21cm; height:29.7cm");
                    }
                }
            }--%>],
            close: function () {
                $('#<%= _dialog %>').remove();
            },
            open: function(event,ui) {
                selectContractType();
            }
        });

        $(function () {
            $global.UID = <%= _viewModel.UID!=null ? JsonConvert.SerializeObject(_viewModel.UID) : "[]"  %>;
            $global.referenceUID = $global.UID.length>0 ? $global.UID[$global.UID.length-1] : null;

            $global.addContractMember = function(uid,ownerID,realName) {
                if(ownerID!=null && ownerID!='')
                    $('input[name="OwnerID"]').val(ownerID);
                $global.UID.push(uid);
                $global.renderMember();
            };
            $global.editContractMember = function(uid) {
                showLoading();
                $.post('<%= Url.Action("EditContractMember","CourseContract") %>', { 'uid': uid,'ownerID':$('input[name="OwnerID"]').val() }, function (data) {
                    hideLoading();
                    $(data).appendTo($('body'));
                });
            }
            $global.deleteContractMember = function(uid) {
                if(confirm('確定刪除?')) {
                    for(var idx=0;idx<$global.UID.length;idx++) {
                        var item = $global.UID[idx];
                        if(''+item==''+uid) {
                            $global.UID.splice(idx,1);
                            break;
                        }
                    }
                    $global.renderMember();
                    $.post('<%= Url.Action("ClearPreliminaryMember","CourseContract") %>',{},function(data){});
                }
            };
        });

    
    </script>
</div>



<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    CourseContractViewModel _viewModel;
    String _dialog = "modifyContract" + DateTime.Now.Ticks;
    UserProfile _profile;
    bool _useLearnerDiscount;
    CourseContract _model;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _model = (CourseContract)this.Model;
        _viewModel = (CourseContractViewModel)ViewBag.ViewModel;
        _profile = Context.GetUser().LoadInstance(models);
        if(_viewModel.UID!=null && _viewModel.UID.Length>0)
        {
            _useLearnerDiscount = models.CheckLearnerDiscount(_viewModel.UID);
        }
    }

</script>

/**
 *
 * Color picker
 * Author: Stefan Petre www.eyecon.ro
 * 
 * Dual licensed under the MIT and GPL licenses
 * 
 */
(function ($) {
	var ColorPicker = function () {
		var
			ids = {},
			inAction,
			charMin = 65,
			visible,
			tpl = '<div class="colorpicker"><div class="colorpicker_color"><div><div></div></div></div><div class="colorpicker_hue"><div></div></div><div class="colorpicker_new_color"></div><div class="colorpicker_current_color"></div><div class="colorpicker_hex"><input type="text" maxlength="6" size="6" /></div><div class="colorpicker_rgb_r colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_rgb_g colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_rgb_b colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_hsb_h colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_hsb_s colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_hsb_b colorpicker_field"><input type="text" maxlength="3" size="3" /><span></span></div><div class="colorpicker_submit"></div></div>',
			defaults = {
				eventName: 'click',
				onShow: function () {},
				onBeforeShow: function(){},
				onHide: function () {},
				onChange: function () {},
				onSubmit: function () {},
				color: 'ff0000',
				livePreview: true,
				flat: false
			},
			fillRGBFields = function  (hsb, cal) {
				var rgb = HSBToRGB(hsb);
				$(cal).data('colorpicker').fields
					.eq(1).val(rgb.r).end()
					.eq(2).val(rgb.g).end()
					.eq(3).val(rgb.b).end();
			},
			fillHSBFields = function  (hsb, cal) {
				$(cal).data('colorpicker').fields
					.eq(4).val(hsb.h).end()
					.eq(5).val(hsb.s).end()
					.eq(6).val(hsb.b).end();
			},
			fillHexFields = function (hsb, cal) {
				$(cal).data('colorpicker').fields
					.eq(0).val(HSBToHex(hsb)).end();
			},
			setSelector = function (hsb, cal) {
				$(cal).data('colorpicker').selector.css('backgroundColor', '#' + HSBToHex({h: hsb.h, s: 100, b: 100}));
				$(cal).data('colorpicker').selectorIndic.css({
					left: parseInt(150 * hsb.s/100, 10),
					top: parseInt(150 * (100-hsb.b)/100, 10)
				});
			},
			setHue = function (hsb, cal) {
				$(cal).data('colorpicker').hue.css('top', parseInt(150 - 150 * hsb.h/360, 10));
			},
			setCurrentColor = function (hsb, cal) {
				$(cal).data('colorpicker').currentColor.css('backgroundColor', '#' + HSBToHex(hsb));
			},
			setNewColor = function (hsb, cal) {
				$(cal).data('colorpicker').newColor.css('backgroundColor', '#' + HSBToHex(hsb));
			},
			keyDown = function (ev) {
				var pressedKey = ev.charCode || ev.keyCode || -1;
				if ((pressedKey > charMin && pressedKey <= 90) || pressedKey == 32) {
					return false;
				}
				var cal = $(this).parent().parent();
				if (cal.data('colorpicker').livePreview === true) {
					change.apply(this);
				}
			},
			change = function (ev) {
				var cal = $(this).parent().parent(), col;
				if (this.parentNode.className.indexOf('_hex') > 0) {
					cal.data('colorpicker').color = col = HexToHSB(fixHex(this.value));
				} else if (this.parentNode.className.indexOf('_hsb') > 0) {
					cal.data('colorpicker').color = col = fixHSB({
						h: parseInt(cal.data('colorpicker').fields.eq(4).val(), 10),
						s: parseInt(cal.data('colorpicker').fields.eq(5).val(), 10),
						b: parseInt(cal.data('colorpicker').fields.eq(6).val(), 10)
					});
				} else {
					cal.data('colorpicker').color = col = RGBToHSB(fixRGB({
						r: parseInt(cal.data('colorpicker').fields.eq(1).val(), 10),
						g: parseInt(cal.data('colorpicker').fields.eq(2).val(), 10),
						b: parseInt(cal.data('colorpicker').fields.eq(3).val(), 10)
					}));
				}
				if (ev) {
					fillRGBFields(col, cal.get(0));
					fillHexFields(col, cal.get(0));
					fillHSBFields(col, cal.get(0));
				}
				setSelector(col, cal.get(0));
				setHue(col, cal.get(0));
				setNewColor(col, cal.get(0));
				cal.data('colorpicker').onChange.apply(cal, [col, HSBToHex(col), HSBToRGB(col)]);
			},
			blur = function (ev) {
				var cal = $(this).parent().parent();
				cal.data('colorpicker').fields.parent().removeClass('colorpicker_focus');
			},
			focus = function () {
				charMin = this.parentNode.className.indexOf('_hex') > 0 ? 70 : 65;
				$(this).parent().parent().data('colorpicker').fields.parent().removeClass('colorpicker_focus');
				$(this).parent().addClass('colorpicker_focus');
			},
			downIncrement = function (ev) {
				var field = $(this).parent().find('input').focus();
				var current = {
					el: $(this).parent().addClass('colorpicker_slider'),
					max: this.parentNode.className.indexOf('_hsb_h') > 0 ? 360 : (this.parentNode.className.indexOf('_hsb') > 0 ? 100 : 255),
					y: ev.pageY,
					field: field,
					val: parseInt(field.val(), 10),
					preview: $(this).parent().parent().data('colorpicker').livePreview					
				};
				$(document).bind('mouseup', current, upIncrement);
				$(document).bind('mousemove', current, moveIncrement);
			},
			moveIncrement = function (ev) {
				ev.data.field.val(Math.max(0, Math.min(ev.data.max, parseInt(ev.data.val + ev.pageY - ev.data.y, 10))));
				if (ev.data.preview) {
					change.apply(ev.data.field.get(0), [true]);
				}
				return false;
			},
			upIncrement = function (ev) {
				change.apply(ev.data.field.get(0), [true]);
				ev.data.el.removeClass('colorpicker_slider').find('input').focus();
				$(document).unbind('mouseup', upIncrement);
				$(document).unbind('mousemove', moveIncrement);
				return false;
			},
			downHue = function (ev) {
				var current = {
					cal: $(this).parent(),
					y: $(this).offset().top
				};
				current.preview = current.cal.data('colorpicker').livePreview;
				$(document).bind('mouseup', current, upHue);
				$(document).bind('mousemove', current, moveHue);
			},
			moveHue = function (ev) {
				change.apply(
					ev.data.cal.data('colorpicker')
						.fields
						.eq(4)
						.val(parseInt(360*(150 - Math.max(0,Math.min(150,(ev.pageY - ev.data.y))))/150, 10))
						.get(0),
					[ev.data.preview]
				);
				return false;
			},
			upHue = function (ev) {
				fillRGBFields(ev.data.cal.data('colorpicker').color, ev.data.cal.get(0));
				fillHexFields(ev.data.cal.data('colorpicker').color, ev.data.cal.get(0));
				$(document).unbind('mouseup', upHue);
				$(document).unbind('mousemove', moveHue);
				return false;
			},
			downSelector = function (ev) {
				var current = {
					cal: $(this).parent(),
					pos: $(this).offset()
				};
				current.preview = current.cal.data('colorpicker').livePreview;
				$(document).bind('mouseup', current, upSelector);
				$(document).bind('mousemove', current, moveSelector);
			},
			moveSelector = function (ev) {
				change.apply(
					ev.data.cal.data('colorpicker')
						.fields
						.eq(6)
						.val(parseInt(100*(150 - Math.max(0,Math.min(150,(ev.pageY - ev.data.pos.top))))/150, 10))
						.end()
						.eq(5)
						.val(parseInt(100*(Math.max(0,Math.min(150,(ev.pageX - ev.data.pos.left))))/150, 10))
						.get(0),
					[ev.data.preview]
				);
				return false;
			},
			upSelector = function (ev) {
				fillRGBFields(ev.data.cal.data('colorpicker').color, ev.data.cal.get(0));
				fillHexFields(ev.data.cal.data('colorpicker').color, ev.data.cal.get(0));
				$(document).unbind('mouseup', upSelector);
				$(document).unbind('mousemove', moveSelector);
				return false;
			},
			enterSubmit = function (ev) {
				$(this).addClass('colorpicker_focus');
			},
			leaveSubmit = function (ev) {
				$(this).removeClass('colorpicker_focus');
			},
			clickSubmit = function (ev) {
				var cal = $(this).parent();
				var col = cal.data('colorpicker').color;
				cal.data('colorpicker').origColor = col;
				setCurrentColor(col, cal.get(0));
				cal.data('colorpicker').onSubmit(col, HSBToHex(col), HSBToRGB(col), cal.data('colorpicker').el);
			},
			show = function (ev) {
				var cal = $('#' + $(this).data('colorpickerId'));
				cal.data('colorpicker').onBeforeShow.apply(this, [cal.get(0)]);
				var pos = $(this).offset();
				var viewPort = getViewport();
				var top = pos.top + this.offsetHeight;
				var left = pos.left;
				if (top + 176 > viewPort.t + viewPort.h) {
					top -= this.offsetHeight + 176;
				}
				if (left + 356 > viewPort.l + viewPort.w) {
					left -= 356;
				}
				cal.css({left: left + 'px', top: top + 'px'});
				if (cal.data('colorpicker').onShow.apply(this, [cal.get(0)]) != false) {
					cal.show();
				}
				$(document).bind('mousedown', {cal: cal}, hide);
				return false;
			},
			hide = function (ev) {
				if (!isChildOf(ev.data.cal.get(0), ev.target, ev.data.cal.get(0))) {
					if (ev.data.cal.data('colorpicker').onHide.apply(this, [ev.data.cal.get(0)]) != false) {
						ev.data.cal.hide();
					}
					$(document).unbind('mousedown', hide);
				}
			},
			isChildOf = function(parentEl, el, container) {
				if (parentEl == el) {
					return true;
				}
				if (parentEl.contains) {
					return parentEl.contains(el);
				}
				if ( parentEl.compareDocumentPosition ) {
					return !!(parentEl.compareDocumentPosition(el) & 16);
				}
				var prEl = el.parentNode;
				while(prEl && prEl != container) {
					if (prEl == parentEl)
						return true;
					prEl = prEl.parentNode;
				}
				return false;
			},
			getViewport = function () {
				var m = document.compatMode == 'CSS1Compat';
				return {
					l : window.pageXOffset || (m ? document.documentElement.scrollLeft : document.body.scrollLeft),
					t : window.pageYOffset || (m ? document.documentElement.scrollTop : document.body.scrollTop),
					w : window.innerWidth || (m ? document.documentElement.clientWidth : document.body.clientWidth),
					h : window.innerHeight || (m ? document.documentElement.clientHeight : document.body.clientHeight)
				};
			},
			fixHSB = function (hsb) {
				return {
					h: Math.min(360, Math.max(0, hsb.h)),
					s: Math.min(100, Math.max(0, hsb.s)),
					b: Math.min(100, Math.max(0, hsb.b))
				};
			}, 
			fixRGB = function (rgb) {
				return {
					r: Math.min(255, Math.max(0, rgb.r)),
					g: Math.min(255, Math.max(0, rgb.g)),
					b: Math.min(255, Math.max(0, rgb.b))
				};
			},
			fixHex = function (hex) {
				var len = 6 - hex.length;
				if (len > 0) {
					var o = [];
					for (var i=0; i<len; i++) {
						o.push('0');
					}
					o.push(hex);
					hex = o.join('');
				}
				return hex;
			}, 
			HexToRGB = function (hex) {
				var hex = parseInt(((hex.indexOf('#') > -1) ? hex.substring(1) : hex), 16);
				return {r: hex >> 16, g: (hex & 0x00FF00) >> 8, b: (hex & 0x0000FF)};
			},
			HexToHSB = function (hex) {
				return RGBToHSB(HexToRGB(hex));
			},
			RGBToHSB = function (rgb) {
				var hsb = {
					h: 0,
					s: 0,
					b: 0
				};
				var min = Math.min(rgb.r, rgb.g, rgb.b);
				var max = Math.max(rgb.r, rgb.g, rgb.b);
				var delta = max - min;
				hsb.b = max;
				if (max != 0) {
					
				}
				hsb.s = max != 0 ? 255 * delta / max : 0;
				if (hsb.s != 0) {
					if (rgb.r == max) {
						hsb.h = (rgb.g - rgb.b) / delta;
					} else if (rgb.g == max) {
						hsb.h = 2 + (rgb.b - rgb.r) / delta;
					} else {
						hsb.h = 4 + (rgb.r - rgb.g) / delta;
					}
				} else {
					hsb.h = -1;
				}
				hsb.h *= 60;
				if (hsb.h < 0) {
					hsb.h += 360;
				}
				hsb.s *= 100/255;
				hsb.b *= 100/255;
				return hsb;
			},
			HSBToRGB = function (hsb) {
				var rgb = {};
				var h = Math.round(hsb.h);
				var s = Math.round(hsb.s*255/100);
				var v = Math.round(hsb.b*255/100);
				if(s == 0) {
					rgb.r = rgb.g = rgb.b = v;
				} else {
					var t1 = v;
					var t2 = (255-s)*v/255;
					var t3 = (t1-t2)*(h%60)/60;
					if(h==360) h = 0;
					if(h<60) {rgb.r=t1;	rgb.b=t2; rgb.g=t2+t3}
					else if(h<120) {rgb.g=t1; rgb.b=t2;	rgb.r=t1-t3}
					else if(h<180) {rgb.g=t1; rgb.r=t2;	rgb.b=t2+t3}
					else if(h<240) {rgb.b=t1; rgb.r=t2;	rgb.g=t1-t3}
					else if(h<300) {rgb.b=t1; rgb.g=t2;	rgb.r=t2+t3}
					else if(h<360) {rgb.r=t1; rgb.g=t2;	rgb.b=t1-t3}
					else {rgb.r=0; rgb.g=0;	rgb.b=0}
				}
				return {r:Math.round(rgb.r), g:Math.round(rgb.g), b:Math.round(rgb.b)};
			},
			RGBToHex = function (rgb) {
				var hex = [
					rgb.r.toString(16),
					rgb.g.toString(16),
					rgb.b.toString(16)
				];
				$.each(hex, function (nr, val) {
					if (val.length == 1) {
						hex[nr] = '0' + val;
					}
				});
				return hex.join('');
			},
			HSBToHex = function (hsb) {
				return RGBToHex(HSBToRGB(hsb));
			},
			restoreOriginal = function () {
				var cal = $(this).parent();
				var col = cal.data('colorpicker').origColor;
				cal.data('colorpicker').color = col;
				fillRGBFields(col, cal.get(0));
				fillHexFields(col, cal.get(0));
				fillHSBFields(col, cal.get(0));
				setSelector(col, cal.get(0));
				setHue(col, cal.get(0));
				setNewColor(col, cal.get(0));
			};
		return {
			init: function (opt) {
				opt = $.extend({}, defaults, opt||{});
				if (typeof opt.color == 'string') {
					opt.color = HexToHSB(opt.color);
				} else if (opt.color.r != undefined && opt.color.g != undefined && opt.color.b != undefined) {
					opt.color = RGBToHSB(opt.color);
				} else if (opt.color.h != undefined && opt.color.s != undefined && opt.color.b != undefined) {
					opt.color = fixHSB(opt.color);
				} else {
					return this;
				}
				return this.each(function () {
					if (!$(this).data('colorpickerId')) {
						var options = $.extend({}, opt);
						options.origColor = opt.color;
						var id = 'collorpicker_' + parseInt(Math.random() * 1000);
						$(this).data('colorpickerId', id);
						var cal = $(tpl).attr('id', id);
						if (options.flat) {
							cal.appendTo(this).show();
						} else {
							cal.appendTo(document.body);
						}
						options.fields = cal
											.find('input')
												.bind('keyup', keyDown)
												.bind('change', change)
												.bind('blur', blur)
												.bind('focus', focus);
						cal
							.find('span').bind('mousedown', downIncrement).end()
							.find('>div.colorpicker_current_color').bind('click', restoreOriginal);
						options.selector = cal.find('div.colorpicker_color').bind('mousedown', downSelector);
						options.selectorIndic = options.selector.find('div div');
						options.el = this;
						options.hue = cal.find('div.colorpicker_hue div');
						cal.find('div.colorpicker_hue').bind('mousedown', downHue);
						options.newColor = cal.find('div.colorpicker_new_color');
						options.currentColor = cal.find('div.colorpicker_current_color');
						cal.data('colorpicker', options);
						cal.find('div.colorpicker_submit')
							.bind('mouseenter', enterSubmit)
							.bind('mouseleave', leaveSubmit)
							.bind('click', clickSubmit);
						fillRGBFields(options.color, cal.get(0));
						fillHSBFields(options.color, cal.get(0));
						fillHexFields(options.color, cal.get(0));
						setHue(options.color, cal.get(0));
						setSelector(options.color, cal.get(0));
						setCurrentColor(options.color, cal.get(0));
						setNewColor(options.color, cal.get(0));
						if (options.flat) {
							cal.css({
								position: 'relative',
								display: 'block'
							});
						} else {
							$(this).bind(options.eventName, show);
						}
					}
				});
			},
			showPicker: function() {
				return this.each( function () {
					if ($(this).data('colorpickerId')) {
						show.apply(this);
					}
				});
			},
			hidePicker: function() {
				return this.each( function () {
					if ($(this).data('colorpickerId')) {
						$('#' + $(this).data('colorpickerId')).hide();
					}
				});
			},
			setColor: function(col) {
				if (typeof col == 'string') {
					col = HexToHSB(col);
				} else if (col.r != undefined && col.g != undefined && col.b != undefined) {
					col = RGBToHSB(col);
				} else if (col.h != undefined && col.s != undefined && col.b != undefined) {
					col = fixHSB(col);
				} else {
					return this;
				}
				return this.each(function(){
					if ($(this).data('colorpickerId')) {
						var cal = $('#' + $(this).data('colorpickerId'));
						cal.data('colorpicker').color = col;
						cal.data('colorpicker').origColor = col;
						fillRGBFields(col, cal.get(0));
						fillHSBFields(col, cal.get(0));
						fillHexFields(col, cal.get(0));
						setHue(col, cal.get(0));
						setSelector(col, cal.get(0));
						setCurrentColor(col, cal.get(0));
						setNewColor(col, cal.get(0));
					}
				});
			}
		};
	}();
	$.fn.extend({
		ColorPicker: ColorPicker.init,
		ColorPickerHide: ColorPicker.hidePicker,
		ColorPickerShow: ColorPicker.showPicker,
		ColorPickerSetColor: ColorPicker.setColor
	});
})(jQuery)
    var returnVal;
    /*!
     * Bal - Email NewsLetter Builder Plugin
     * Author: Rufat Askerov
     * Author Uri : https://cidcode.net
     */

    ;
    (function(factory) {
        "use strict";

        if (typeof define === 'function' && define.amd) { // AMD
            define(['jquery'], factory);
        } else if (typeof exports == "object" && typeof module == "object") { // CommonJS
            module.exports = factory(require('jquery'));
        } else { // Browser
            factory(jQuery);
        }
    })(function($, undefined) {
        "use strict";

        // -------------------------- variables -------------------------- //
        //left menu all items
        const _tabMenuItems = {
            //elements tab
            'typography': {
                itemSelector: 'typography',
                parentSelector: 'tab-elements'
            },
            'media': {
                itemSelector: 'media',
                parentSelector: 'tab-elements'
            },
            'layout': {
                itemSelector: 'layout',
                parentSelector: 'tab-elements'
            },
            'button': {
                itemSelector: 'button',
                parentSelector: 'tab-elements'
            },
            'social': {
                itemSelector: 'social',
                parentSelector: 'tab-elements'
            },
            //property tab
            'background': {
                itemSelector: 'background',
                parentSelector: 'tab-property'
            },
            'border-radius': {
                itemSelector: 'border-radius',
                parentSelector: 'tab-property'
            },
            'text-style': {
                itemSelector: 'text-style',
                parentSelector: 'tab-property'
            },
            'padding': {
                itemSelector: 'padding',
                parentSelector: 'tab-property'
            },
            'youtube-frame': {
                itemSelector: 'youtube-frame',
                parentSelector: 'tab-property'
            },
            'hyperlink': {
                itemSelector: 'hyperlink',
                parentSelector: 'tab-property'
            },
            'image-settings': {
                itemSelector: 'image-settings',
                parentSelector: 'tab-property'
            },
            'social-content': {
                itemSelector: 'social-content',
                parentSelector: 'tab-property'
            }
        };


        /**
         * Using all variables in plugin
         */
        var _language = [];
        var _aceEditor, _popup_save_template, _popup_editor, _popup_send_email, _popup_demo, _popup_load_template;
        var _this, _nav, _result, _getHtml, _popupImagesContent, _selection, _span, _menuType, _top, _left, _contentText, _spanId, _dataId, _outputSideBar, _url, _width, _outputContent, _class, _socialRow, _socialType, _val, _menu, _value, _activeElement, _href, _html, _dataTypes, _typeArr, _arrSize, _style, _aceEditor, _parentSelector, _parent, _arrElement, _outputHtml, _settings, _tabElements, _tabProperty, _items, _contentMenu, _generatedElements, _elementsContainer, _elements, _element, _tabSelector, _menuItem, _tabMenuItem, _accordionMenuItem, _dataValue;

        var EmailBuilder = function(elem, options) {
            //Private variables
            this.elem = elem;
            this.$elem = $(elem);
            this.options = options;
            this.langArr;
        };
        EmailBuilder.prototype = {
            defaults: {
                //global settings
                elementJsonUrl: 'elements.json',
                langJsonUrl: 'lang.json',
                lang: 'en',
                loading_color1: '#3B7694',
                loading_color2: '#09181F',
                showLoading: true,

                blankPageHtml: '<table class="main" width="100%" cellspacing="0" cellpadding="0" border="0" data-types="background,text-style,padding" data-last-type="padding">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td align="left" class="element-content" style="padding-left:50px;padding-right:50px;padding-top:10px;padding-bottom:10px;background-color:#FFFFFF;">' +
                    '<table width="100%" cellspacing="0" cellpadding="0" border="0">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td contenteditable="true" align="center" class=" active" style="padding: 20px;">' +
                    'Drag elements from left menu&nbsp;</td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>' +
                    '</td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>',
                // when page load showing this html
                loadPageHtml: '<table class="main" width="100%" cellspacing="0" cellpadding="0" border="0" data-types="background,text-style,padding" data-last-type="padding">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td align="left" class="element-content" style="padding-left:50px;padding-right:50px;padding-top:10px;padding-bottom:10px;background-color:#FFFFFF;">' +
                    '<table width="100%" cellspacing="0" cellpadding="0" border="0">' +
                    '<tbody><tr>' +
                    '<td>' +
                    '<h1 contenteditable="true" style="font-weight: normal;text-align:center" class="">Welcome to Builder</h1>' +
                    '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td contenteditable="true" align="center" class=" active" style="padding: 20px;">' +
                    'Drag elements from left menu&nbsp;</td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>' +
                    '</td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>',

                //show context menu
                showContextMenu: true,
                showContextMenu_FontFamily: true,
                showContextMenu_FontSize: true,
                showContextMenu_Bold: true,
                showContextMenu_Italic: true,
                showContextMenu_Underline: true,
                showContextMenu_Strikethrough: true,
                showContextMenu_Hyperlink: true,

                //left menu
                showElementsTab: true,
                showPropertyTab: true,
                showCollapseMenu: true,
                showBlankPageButton: true,
                showCollapseMenuinBottom: true, //btn-collapse-bottom

                //setting items
                showSettingsBar: true,
                showSettingsPreview: true,
                showSettingsExport: true,
                showSettingsSendMail: true,
                showSettingsSave: true,
                showSettingsLoadTemplate: true,

                //show or hide elements actions
                showRowMoveButton: true,
                showRowRemoveButton: true,
                showRowDuplicateButton: true,
                showRowCodeEditorButton: true,

                //events of settings
                onSettingsPreviewButtonClick: function(e) {},
                onSettingsExportButtonClick: function(e) {},
                onBeforeSettingsSaveButtonClick: function(e) {},
                onSettingsSaveButtonClick: function(e) {},
                onBeforeSettingsLoadTemplateButtonClick: function(e) {},
                onSettingsSendMailButtonClick: function(e) {},

                //events in modal
                onBeforeChangeImageClick: function(e) {},
                onBeforePopupSelectImageButtonClick: function(e) {},
                onBeforePopupSelectTemplateButtonClick: function(e) {},
                onPopupSaveButtonClick: function(e) {},
                onPopupSendMailButtonClick: function(e) {},
                onPopupUploadImageButtonClick: function(e) {},

                //selected element events
                onBeforeRowRemoveButtonClick: function(e) {},
                onAfterRowRemoveButtonClick: function(e) {},

                onBeforeRowDuplicateButtonClick: function(e) {},
                onAfterRowDuplicateButtonClick: function(e) {},

                onBeforeRowEditorButtonClick: function(e) {},
                onAfterRowEditorButtonClick: function(e) {},

                onBeforeShowingEditorPopup: function(e) {},
                onAfterLoad: function(e) {},

                onElementDragStart:function(e) {},
                onElementDragFinished:function(e,contentHtml) {},
            },
            /**
             * Init Plugin
             */
            init: function() {
                _this = this;
                _this.config = $.extend({}, this.defaults, this.options);

                //show loading
                _this.show_loading();
                _this.getLangs();


                return this;
            },
            /**
             * Show loading
             */
            show_loading: function() {
                if (_this.config.showLoading == true) {
                    _this.$elem.css({
                        'position': 'relative'
                    });
                    _this.display('<div class="bal-loading-container"><div class="bal-loading"><div class="bal-loading-bounce-1" style="background-color:' + _this.config.loading_color1 + '"></div><div class="bal-loading-bounce-2" style="background-color:' + _this.config.loading_color2 + '"></div></div></div>');
                }
            },
            /**
             * Generate output information
             */
            generate: function(elementsHtml) {
                _nav = '<div class="bal-nav">' +
                    '<ul class="bal-left-menu">';
                if (_this.config.showElementsTab == true) {
                    _nav += '<li class="bal-menu-item tab-selector active" data-tab-selector="tab-elements">' +
                        '<i class="fa fa-puzzle-piece"></i>' +
                        '<span class="bal-menu-name">' + _this.langArr.elementsTab + '</span>' +
                        '</li>';
                }

                if (_this.config.showPropertyTab == true) {
                    _nav += '<li class="bal-menu-item tab-selector" data-tab-selector="tab-property">' +
                        '<i class="fa fa-pencil"></i>' +
                        '<span class="bal-menu-name">' + _this.langArr.propertyTab + '</span>' +
                        '</li>';
                }

                if (_this.config.showBlankPageButton == true) {
                    _nav += '<li class="bal-menu-item blank-page">' +
                        '<i class="fa fa-file"></i>' +
                        '<span class="bal-menu-name">' + _this.langArr.blankPage + '</span>' +
                        '</li>';
                }

                if (_this.config.showCollapseMenu == true) {
                    _class = '';
                    if (_this.config.showCollapseMenuinBottom == true) {
                        _class = 'btn-collapse-bottom ';
                    }

                    _nav += '<li class="bal-menu-item bal-collapse ' + _class + '">' +
                        '<i class="fa fa-chevron-circle-left"></i>' +
                        '<span class="bal-menu-name">' + _this.langArr.collapseMenu + '</span>' +
                        '</li>';
                }
                _nav += '</ul></div>';

                _settings = '';
                if (_this.config.showSettingsBar == true) {

                    //  _settings='<div class="bal-settings"><ul><li class="bal-setting-item preview" data-toggle="tooltip" title="" data-original-title="Preview"><i class="fa fa-eye"></i></li><li class="bal-setting-item export" data-toggle="tooltip" title="" data-original-title="Export"><i class="fa fa-share"></i></li><li class="bal-setting-item other-devices" data-toggle="tooltip" title="" data-original-title="Other devices"><i class="fa fa-mobile"></i></li><li class="bal-setting-item save-template" data-toggle="tooltip" title="" data-original-title="Save template"><i class="fa fa-floppy-o"></i></li><li class="bal-setting-item load-templates" data-toggle="tooltip" title="" data-original-title="Load template"><i class="fa fa-file-text"></i></li></ul><div class="bal-setting-content"><div class="bal-setting-content-item other-devices"><ul><li class="bal-setting-device-tab mobile " data-tab="mobile-content"><i class="fa fa-mobile"></i></li><li class="bal-setting-device-tab desktop active" data-tab="desktop-content"><i class="fa fa-desktop"></i></li></ul><div><div class="mobile-content bal-setting-device-content ">mobile-content </div><div class="desktop-content bal-setting-device-content active">desktop-content </div></div></div></div></div>';
                    _settings = '<div class="bal-settings">' +
                        '<ul>';
                    if (_this.config.showSettingsPreview == true) {
                        _settings += '<li class="bal-setting-item preview" data-toggle="tooltip" title="' + _this.langArr.settingsPreview + '">' +
                            '<i class="fa fa-eye"></i>' +
                            '</li>';
                    }

                    if (_this.config.showSettingsExport == true) {
                        _settings += '<li class="bal-setting-item export" data-toggle="tooltip" title="' + _this.langArr.settingsExport + '">' +
                            '<i class="fa fa-share"></i>' +
                            '</li>';
                    }

                    if (_this.config.showSettingsSave == true) {
                        _settings += '<li class="bal-setting-item save-template" data-toggle="tooltip" title="' + _this.langArr.settingsSaveTemplate + '">' +
                            '<i class="fa fa-floppy-o"></i>' +
                            '</li>';
                    }

                    if (_this.config.showSettingsLoadTemplate == true) {
                        _settings += '<li class="bal-setting-item load-templates" data-toggle="tooltip" title="' + _this.langArr.settingsLoadTemplate + '">' +
                            '<i class="fa fa-file-text"></i>' +
                            '</li>';
                    }

                    if (_this.config.showSettingsSendMail == true) {
                        _settings += '<li class="bal-setting-item send-email" data-toggle="tooltip" title="' + _this.langArr.settingsSendMail + '">' +
                            '<i class="fa fa-envelope"></i>' +
                            '</li>';
                    }
                    _settings += '</ul></div>';
                }

                _tabElements = '<div class="tab-elements bal-element-tab active"><ul class="bal-elements-accordion">' + elementsHtml + '</ul></div>';

                //  _tabProperty='<div class="tab-property bal-element-tab"><ul class="bal-elements-accordion"><li class="bal-elements-accordion-item" data-type="background"><a class="bal-elements-accordion-item-title">Background</a><div class="bal-elements-accordion-item-content clearfix"><div id="bg-color" class="bg-color bg-item" setting-type="background-color"><i class="fa fa-adjust"></i></div><!-- <div class="bg-item bg-image" setting-type="background-image"><i class="fa fa-image"></i></div>--></div></li><li class="bal-elements-accordion-item" data-type="padding"><a class="bal-elements-accordion-item-title">Padding</a><div class="bal-elements-accordion-item-content"><div class=" bal-element-boxs clearfix "><div class="big-box col-sm-6 "><input type="text" class="form-control padding all" setting-type="padding"></div><div class="small-boxs col-sm-6"><div class="row"><input type="text" class="form-control padding number" setting-type="padding-top"></div><div class="row clearfix"><div class="col-sm-6"><input type="text" class="form-control padding number" setting-type="padding-left"></div><div class="col-sm-6"><input type="text" class="form-control padding number" setting-type="padding-right"></div></div><div class="row"><input type="text" class="form-control padding number" setting-type="padding-bottom"></div></div></div></div></li><li class="bal-elements-accordion-item" data-type="border-radius"><a class="bal-elements-accordion-item-title">Border Radius</a><div class="bal-elements-accordion-item-content"><div class=" bal-element-boxs bal-border-radius-box clearfix "><div class="big-box col-sm-6 "><input type="text" class="form-control border-radius all" setting-type="border-radius"></div><div class="small-boxs col-sm-6"><div class="row clearfix"><div class="col-sm-6"><input type="text" class="form-control border-radius" setting-type="border-top-left-radius"></div><div class="col-sm-6"><input type="text" class="form-control border-radius" setting-type="border-top-right-radius"></div></div><div class="row clearfix margin"><div class="col-sm-6"><input type="text" class="form-control border-radius" setting-type="border-bottom-left-radius"></div><div class="col-sm-6"><input type="text" class="form-control border-radius" setting-type="border-bottom-right-radius"></div></div></div></div></div></li><li class="bal-elements-accordion-item" data-type="text-style"><a class="bal-elements-accordion-item-title">Text Style</a><div class="bal-elements-accordion-item-content"><div class="bal-element-boxs bal-text-style-box clearfix "><div class="bal-element-font-family col-sm-8"><select class="form-control font-family" setting-type="font-family"><option value="Arial">Arial</option><option value="Helvetica">Helvetica</option><option value="Georgia">Georgia</option><option value="Times New Roman">Times New Roman</option><option value="Verdana">Verdana</option><option value="Tahoma">Tahoma</option><option value="Calibri">Calibri</option></select></div><div class="bal-element-font-size col-sm-4"><input type="text" name="name" class="form-control number" value="14" setting-type="font-size" /></div><div class="bal-icon-boxs bal-text-icons clearfix"><div class="bal-icon-box-item fontStyle" setting-type="font-style" setting-value="italic"><i class="fa fa-italic"></i></div><div class="bal-icon-box-item active underline " setting-type="text-decoration" setting-value="underline"><i class="fa fa-underline"></i></div><div class="bal-icon-box-item line " setting-type="text-decoration" setting-value="line-through"><i class="fa fa-strikethrough"></i></div></div><div class="bal-icon-boxs bal-align-icons clearfix"><div class="bal-icon-box-item left active"><i class="fa fa-align-left"></i></div><div class="bal-icon-box-item center "><i class="fa fa-align-center"></i></div><div class="bal-icon-box-item right"><i class="fa fa-align-right"></i></div></div><div class="clearfix"></div><div class="bal-icon-boxs bal-text-icons "><div id="text-color" class="bal-icon-box-item text-color" setting-type="color"></div>Text Color </div><div class="bal-icon-boxs bal-font-icons clearfix"><div class="bal-icon-box-item" setting-type="bold"><i class="fa fa-bold"></i></div></div></div></div></li><li class="bal-elements-accordion-item" data-type="social-content"><a class="bal-elements-accordion-item-title">Social content</a><div class="bal-elements-accordion-item-content"><div class="col-sm-12 bal-social-content-box"><div class="row" data-social-type="instagram"><label class="small-title">Instagram</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" />Show </label></div><div class="row" data-social-type="pinterest"><label class="small-title">Pinterest</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" />Show </label></div><div class="row" data-social-type="google-plus"><label class="small-title">Google+</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div><div class="row" data-social-type="facebook"><label class="small-title">Facebook</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div><div class="row" data-social-type="twitter"><label class="small-title">Twitter</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div><div class="row" data-social-type="linkedin"><label class="small-title">Linkedin</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div><div class="row" data-social-type="youtube"><label class="small-title">Youtube</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div><div class="row" data-social-type="skype"><label class="small-title">Skype</label><input type="text" name="name" value="#" class="social-input" /><label class="checkbox-title"><input type="checkbox" name="name" checked />Show </label></div></div></div></li><li class="bal-elements-accordion-item" data-type="youtube-frame"><a class="bal-elements-accordion-item-title">Youtube</a><div class="bal-elements-accordion-item-content"><div class="bal-social-content-box "><label>Youtube Video ID</label><input type="text" class=" youtube" setting-type=""></div></div></li><li class="bal-elements-accordion-item" data-type="hyperlink"><a class="bal-elements-accordion-item-title">Hyperlink</a><div class="bal-elements-accordion-item-content"><div class="bal-social-content-box "><label>Url</label><input type="text" class="hyperlink-url" setting-type=""></div></div></li></ul></div>';
                _tabProperty = '<div class="tab-property bal-element-tab">' +
                    ' <ul class="bal-elements-accordion">' +
                    ' <li class="bal-elements-accordion-item" data-type="background">' +
                    '   <a class="bal-elements-accordion-item-title">' + _this.langArr.propertyBG + '</a>' +
                    '   <div class="bal-elements-accordion-item-content clearfix">' +
                    '   <div id="bg-color" class="bg-color bg-item" setting-type="background-color">' +
                    '   <i class="fa fa-adjust"></i>' +
                    ' </div>' +
                    ' </div>' +
                    ' </li>' +
                    '   <li class="bal-elements-accordion-item" data-type="padding">' +
                    '     <a class="bal-elements-accordion-item-title">' + _this.langArr.propertyPadding + '</a>' +
                    '   <div class="bal-elements-accordion-item-content">' +
                    '     <div class=" bal-element-boxs clearfix ">' +
                    '       <div class="big-box col-sm-6 ">' +
                    '         <input type="text" class="form-control padding all" setting-type="padding">' +
                    '   </div>' +
                    ' <div class="small-boxs col-sm-6">' +
                    ' <div class="row">' +
                    '   <input type="text" class="form-control padding number" setting-type="padding-top">' +
                    '   </div>' +
                    '   <div class="row clearfix">' +
                    '     <div class="col-sm-6">' +
                    '         <input type="text" class="form-control padding number" setting-type="padding-left">' +
                    '   </div>' +
                    '   <div class="col-sm-6">' +
                    ' <input type="text" class="form-control padding number" setting-type="padding-right">' +
                    ' </div>' +
                    ' </div>' +
                    '   <div class="row">' +
                    '   <input type="text" class="form-control padding number" setting-type="padding-bottom">' +
                    '   </div>' +
                    '   </div>' +
                    ' </div>' +
                    '   </div>' +
                    '   </li>' +
                    '   <li class="bal-elements-accordion-item" data-type="border-radius">' +
                    '   <a class="bal-elements-accordion-item-title">' + _this.langArr.propertyBorderRadius + '</a>' +
                    '   <div class="bal-elements-accordion-item-content">' +
                    '   <div class=" bal-element-boxs bal-border-radius-box clearfix ">' +
                    '   <div class="big-box col-sm-6 ">' +
                    '     <input type="text" class="form-control border-radius all" setting-type="border-radius">' +
                    '     </div>' +
                    '   <div class="small-boxs col-sm-6">' +
                    '     <div class="row clearfix">' +
                    '   <div class="col-sm-6">' +
                    '     <input type="text" class="form-control border-radius" setting-type="border-top-left-radius">' +
                    '     </div>' +
                    ' <div class="col-sm-6">' +
                    '   <input type="text" class="form-control border-radius" setting-type="border-top-right-radius">' +
                    '   </div>' +
                    ' </div>' +
                    '   <div class="row clearfix margin">' +
                    '   <div class="col-sm-6">' +
                    '   <input type="text" class="form-control border-radius" setting-type="border-bottom-left-radius">' +
                    '   </div>' +
                    ' <div class="col-sm-6">' +
                    ' <input type="text" class="form-control border-radius" setting-type="border-bottom-right-radius">' +
                    ' </div>' +
                    ' </div>' +
                    ' </div>' +
                    ' </div>' +
                    ' </div>' +
                    ' </li>' +
                    ' <li class="bal-elements-accordion-item" data-type="text-style">' +
                    '   <a class="bal-elements-accordion-item-title">' + _this.langArr.propertyTextStyle + '</a>' +
                    '   <div class="bal-elements-accordion-item-content">' +
                    '   <div class="bal-element-boxs bal-text-style-box clearfix ">' +
                    '   <div class="bal-element-font-family col-sm-8">' +
                    '   <select class="form-control font-family" setting-type="font-family">' +
                    '     <option value="Arial">Arial</option>' +
                    '   <option value="Helvetica">Helvetica</option>' +
                    ' <option value="Georgia">Georgia</option>' +
                    '<option value="Times New Roman">Times New Roman</option>' +
                    '<option value="Verdana">Verdana</option>' +
                    '<option value="Tahoma">Tahoma</option>' +
                    '<option value="Calibri">Calibri</option>' +
                    '</select>' +
                    '</div>' +
                    '<div class="bal-element-font-size col-sm-4">' +
                    '  <input type="text" name="name" class="form-control number" value="14" setting-type="font-size" />' +
                    '</div>' +
                    '<div class="bal-icon-boxs bal-text-icons clearfix">' +
                    '<div class="bal-icon-box-item fontStyle" setting-type="font-style" setting-value="italic">' +
                    '<i class="fa fa-italic"></i>' +
                    '</div>' +
                    '<div class="bal-icon-box-item active underline " setting-type="text-decoration" setting-value="underline">' +
                    '<i class="fa fa-underline"></i>' +
                    '</div>' +
                    '<div class="bal-icon-box-item line " setting-type="text-decoration" setting-value="line-through">' +
                    '  <i class="fa fa-strikethrough"></i>' +
                    '</div>' +
                    '</div>' +
                    '<div class="bal-icon-boxs bal-align-icons clearfix">' +
                    '<div class="bal-icon-box-item left active">' +
                    '<i class="fa fa-align-left"></i>' +
                    '</div>' +
                    '<div class="bal-icon-box-item center ">' +
                    '  <i class="fa fa-align-center"></i>' +
                    '</div>' +
                    '<div class="bal-icon-box-item right">' +
                    '  <i class="fa fa-align-right"></i>' +
                    '  </div>' +
                    '</div>' +
                    '  <div class="clearfix"></div>' +
                    '  <div class="bal-icon-boxs bal-text-icons ">' +
                    '  <div id="text-color" class="bal-icon-box-item text-color" setting-type="color">' +
                    '  </div>' +
                    '  Text Color' +
                    '  </div>' +
                    '<div class="bal-icon-boxs bal-font-icons clearfix">' +
                    '<div class="bal-icon-box-item" setting-type="bold">' +
                    '<i class="fa fa-bold"></i>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '  </li>' +
                    '<li class="bal-elements-accordion-item" data-type="social-content">' +
                    '  <a class="bal-elements-accordion-item-title">' + _this.langArr.propertySocialContent + '</a>' +
                    '<div class="bal-elements-accordion-item-content">' +
                    '  <div class="col-sm-12 bal-social-content-box">' +
                    '  <div class="row" data-social-type="instagram">' +
                    '<label class="small-title">Instagram</label>' +
                    '<input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '<input type="checkbox" name="name" /> Show' +
                    '</label>' +
                    '</div>' +
                    '  <div class="row" data-social-type="pinterest">' +
                    '<label class="small-title">Pinterest</label>' +
                    '  <input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '<input type="checkbox" name="name" /> Show' +
                    '  </label>' +
                    '</div>' +
                    '<div class="row" data-social-type="google-plus">' +
                    '  <label class="small-title">Google+</label>' +
                    '<input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '  <input type="checkbox" name="name" checked /> Show' +
                    '</label>' +
                    '</div>' +
                    '  <div class="row" data-social-type="facebook">' +
                    '  <label class="small-title">Facebook</label>' +
                    '  <input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '<input type="checkbox" name="name" checked /> Show' +
                    '</label>' +
                    '</div>' +
                    '  <div class="row" data-social-type="twitter">' +
                    '<label class="small-title">Twitter</label>' +
                    '<input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '  <input type="checkbox" name="name" checked /> Show' +
                    '</label>' +
                    '</div>' +
                    '<div class="row" data-social-type="linkedin">' +
                    '<label class="small-title">Linkedin</label>' +
                    '<input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '<input type="checkbox" name="name" checked /> Show' +
                    '</label>' +
                    '</div>' +
                    '<div class="row" data-social-type="youtube">' +
                    '  <label class="small-title">Youtube</label>' +
                    '  <input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '  <input type="checkbox" name="name" checked /> Show' +
                    '  </label>' +
                    '</div>' +
                    '<div class="row" data-social-type="skype">' +
                    '<label class="small-title">Skype</label>' +
                    '  <input type="text" name="name" value="#" class="social-input" />' +
                    '<label class="checkbox-title">' +
                    '  <input type="checkbox" name="name" checked /> Show' +
                    '</label>' +
                    '</div>' +
                    '</div>' +
                    '  </div>' +
                    '</li>' +
                    '<li class="bal-elements-accordion-item" data-type="youtube-frame">' +
                    '<a class="bal-elements-accordion-item-title">Youtube</a>' +
                    '<div class="bal-elements-accordion-item-content">' +
                    '<div class="bal-social-content-box ">' +
                    '<label>Youtube Video ID</label>' +
                    '<input type="text" class=" youtube" setting-type="">' +
                    '</div>' +
                    '</div>' +
                    '</li>' +
                    '  <li class="bal-elements-accordion-item" data-type="width">' +
                    '<a class="bal-elements-accordion-item-title">' + _this.langArr.propertyEmailWidth + '</a>' +
                    '<div class="bal-elements-accordion-item-content">' +
                    '  <div class="bal-social-content-box ">' +
                    '<label>Width</label>' +
                    '<input type="text" class="email-width number" setting-type="">' +
                    '<span class="help">' + _this.langArr.propertyEmailWidthHelp + '</span>' +
                    '  </div>' +
                    '</div>' +
                    '</li>' +
                    '<li class="bal-elements-accordion-item" data-type="image-settings">' +
                    '<a class="bal-elements-accordion-item-title">' + _this.langArr.propertyImageSettings + '</a>' +
                    '<div class="bal-elements-accordion-item-content">' +
                    '<div class="bal-social-content-box ">' +
                    '<div class="change-image">' + _this.langArr.propertyChangeImage + '</div>' +
                    '<label>Image width</label>' +
                    '<input type="text" class="image-width  image-size " setting-type="" >' +
                    '<label>Image height</label>' +
                    '<input type="text" class="image-height  image-size" setting-type="">' +
                    '</div>' +
                    '</div>' +
                    '</li>' +
                    '<li class="bal-elements-accordion-item" data-type="hyperlink">' +
                    '<a class="bal-elements-accordion-item-title">' + _this.langArr.propertyHyperlink + '</a>' +
                    '<div class="bal-elements-accordion-item-content">' +
                    '<div class="bal-social-content-box ">' +
                    '<label>Url</label>' +
                    '<input type="text" class="hyperlink-url" setting-type="">' +
                    '</div>' +
                    '</div>' +
                    '  </li>' +
                    '</ul>' +
                    '</div>';
                _elementsContainer = '<div class="bal-elements-container">' + _tabElements + _tabProperty + '</div>';
                _elements = '<div class="bal-elements">' + _elementsContainer + _settings + '</div>';

                _outputSideBar = '<aside class="bal-left-menu-container clearfix">' + _nav + _elements + '</aside>';
                _outputContent = '<div class="bal-content">' +
                    '<div class="bal-content-wrapper" data-types="background,padding,width">' +
                    '<div class="bal-content-main lg-width">' +
                    '<div class="email-editor-elements-sortable">' +
                    '<div class="sortable-row">' +
                    '<div class="sortable-row-container">' +
                    '<div class="sortable-row-actions">' +
                    '<div class="row-move row-action">' +
                    '<i class="fa fa-arrows-alt"></i>' +
                    '</div>' +
                    '<div class="row-remove row-action">' +
                    '<i class="fa fa-remove"></i>' +
                    '</div>' +
                    '<div class="row-duplicate row-action">' +
                    '<i class="fa fa-files-o"></i>' +
                    '</div>' +
                    '<div class="row-code row-action">' +
                    '<i class="fa fa-code"></i>' +
                    '</div>' +
                    '</div>' +
                    '<div class="sortable-row-content">' +
                    _this.config.loadPageHtml +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
                _contentMenu = '';
                // if (_this.config.showContextMenu == true) {
                //     _contentMenu = '<div class="bal-context-menu">' +
                //         '<ul class="context-menu-items">';
                //     if (_this.config.showContextMenu_FontFamily == true) {
                //         _contentMenu += '<li class="bal-context-menu-item font-style" data-menu-type="font-family">' +
                //             '	<div>' +
                //             '<select class="form-control font-family" >' +
                //             '<option value="Arial" selected="selected">Arial</option>' +
                //             '<option value="Helvetica">Helvetica</option>' +
                //             '<option value="Georgia">Georgia</option>' +
                //             '<option value="Times New Roman">Times New Roman</option>' +
                //             '<option value="Verdana">Verdana</option>' +
                //             '<option value="Tahoma">Tahoma</option>' +
                //             '<option value="Calibri">Calibri</option>' +
                //             '</select>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_FontSize == true) {
                //         _contentMenu += '<li class="bal-context-menu-item font-style" data-menu-type="font-size">' +
                //             '<div>' +
                //             '<select class="font-size form-control">' +
                //             '<option value="6px">6px</option>' +
                //             '<option value="8px">8px</option>' +
                //             '<option value="9px">9px</option>' +
                //             '<option value="10px">10px</option>' +
                //             '<option value="11px">11px</option>' +
                //             '<option value="12px">12px</option>' +
                //             '<option value="14px" selected="selected">14px</option>' +
                //             '<option value="18px">18px</option>' +
                //             '<option value="24px">24px</option>' +
                //             '<option value="30px">30px</option>' +
                //             '<option value="36px">36px</option>' +
                //             '<option value="48px">48px</option>' +
                //             '</select>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_Bold == true) {
                //         _contentMenu += '<li class="bal-context-menu-item" data-menu-type="bold">' +
                //             '<div>' +
                //             '<i class="fa fa-bold"></i>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_Italic == true) {
                //         _contentMenu += '<li class="bal-context-menu-item" data-menu-type="italic">' +
                //             '<div>' +
                //             '<i class="fa fa-italic"></i>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_Underline == true) {
                //         _contentMenu += '<li class="bal-context-menu-item" data-menu-type="underline">' +
                //             '<div>' +
                //             '<i class="fa fa-underline"></i>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_Strikethrough == true) {
                //         _contentMenu += '<li class="bal-context-menu-item" data-menu-type="strikethrough">' +
                //             '<div>' +
                //             '<i class="fa fa-strikethrough"></i>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //
                //     if (_this.config.showContextMenu_Hyperlink == true) {
                //         _contentMenu += '<li class="bal-context-menu-item" data-menu-type="link">' +
                //             '<div>' +
                //             '<i class="fa fa-link"></i>' +
                //             '</div>' +
                //             '</li>';
                //     }
                //     _contentMenu += '</ul>' +
                //         '<div class="context-menu-hyperlink ">' +
                //         '<div class="row">' +
                //         '<div class="col-md-10"> <input type="text" class="form-control context-menu-hyperlink-input" ></div>' +
                //         '<div class="col-md-1">' +
                //         '<a href="javascript:void(0)" class="context-menu-hyperlink-close" title="Close">&times;</a>' +
                //         '</div>' +
                //         '</div>' +
                //         '</div>' +
                //         '</div>';
                // }

                _outputHtml = '<div class="bal-editor-container clearfix"> ' + _outputSideBar + _outputContent + _contentMenu + '</div>';
                _this.generatePopups();
                _this.display(_outputHtml);


                _this.default_func();
                _this.events();

            },
            /**
             * Generate popups html
             */
            generatePopups: function() {
                _popupImagesContent = '<div class="modal fade popup_images" id="popup_images" role="dialog">' +
                    '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">' + _this.langArr.popupImageTitle + '</h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<div class="row">' +
                    '<div class="col-sm-6">' +
                    '<input type="file" class="input-file" accept="image/*" >' +
                    '</div>' +
                    '<div class="col-sm-6">' +
                    '<button class="btn-upload">' + _this.langArr.popupImageUpload + '</button>' +
                    '</div>' +
                    '</div>' +
                    '<div class="upload-images">' +
                    ' <div class="row">     ' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success btn-select" >' + _this.langArr.popupImageOk + '</button>' +
                    '<button type="button" class="btn btn-default" data-dismiss="modal">' + _this.langArr.popupImageClose + '</button> ' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';


                jQuery(_popupImagesContent).appendTo('body');

                _popup_save_template = '<div class="modal fade " id="popup_save_template" role="dialog">' +
                    '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">' + _this.langArr.popupSaveTemplateTitle + '<br>' +
                    '<small>' + _this.langArr.popupSaveTemplateSubTitle + '</small></h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<div class="row">' +
                    '<div class="col-sm-12">' +
                    '<input type="text" class="form-control template-name" placeholder="' + _this.langArr.popupSaveTemplatePLaceHolder + '"  >' +
                    '<br>' +
                    '<label class="input-error" style="color:red"></label>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success btn-save-template" >' + _this.langArr.popupSaveTemplateOk + '</button>' +
                    '<button type="button" class="btn btn-default" data-dismiss="modal">' + _this.langArr.popupSaveTemplateClose + '</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';
                jQuery(_popup_save_template).appendTo('body');

                _popup_editor = '<div class="modal fade modal-wide" id="popup_editor" role="dialog">' +
                    '<div class="modal-dialog modal-lg">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">' + _this.langArr.popupEditorTitle + '<br/>' +
                    '  <small></small></h4>' +
                    '  </div>' +
                    '<div class="modal-body">' +
                    '<div id="editorHtml" class="">' +
                    '</div>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success btn-save-editor" >' + _this.langArr.popupEditorOk + '</button>' +
                    '<button type="button" class="btn btn-default" data-dismiss="modal">' + _this.langArr.popupEditorClose + '</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                jQuery(_popup_editor).appendTo('body');


                _popup_send_email = '<div class="modal fade " id="popup_send_email" role="dialog">' +
                    '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">' + _this.langArr.popupSendEmailTitle + '<br>' +
                    '<small>' + _this.langArr.popupSendEmailSubTitle + '</small></h4>' +
                    '  </div>' +
                    '<div class="modal-body">' +
                    '  <div class="row">' +
                    '<div class="col-sm-12">' +
                    '  <input type="email" class="form-control recipient-email" placeholder="' + _this.langArr.popupSendEmailPlaceHolder + '"  >' +
                    '<br>' +
                    '<label class="popup_send_email_output" style="color:red"></label>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success btn-send-email-template" >' + _this.langArr.popupSendEmailOk + '</button>' +
                    '<button type="button" class="btn btn-default" data-dismiss="modal">' + _this.langArr.popupSendEmailClose + '</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                jQuery(_popup_send_email).appendTo('body');

                _popup_demo = '<div class="modal fade " id="popup_demo" role="dialog">' +
                    '<div class="modal-dialog">' +
                    '  <div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">Demo<br>' +
                    '</h4>' +
                    '  </div>' +
                    '<div class="modal-body">' +
                    '<label  style="color:red">This is demo version. There is not access to use more.' +
                    'If you want to more please buy plugin.<a href="https://codecanyon.net/item/bal-email-newsletter-builder-php-version/18060733" title="Buy">Buy Plugin</a></label>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                    '  </div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                jQuery(_popup_demo).appendTo('body');

                _popup_load_template = '<div class="modal fade " id="popup_load_template" role="dialog">' +
                    '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                    '<h4 class="modal-title">' + _this.langArr.popupLoadTemplateTitle + '<br>' +
                    '<small>' + _this.langArr.popupLoadTemplateSubtitle + '</small></h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<div class="template-list">' +
                    '</div>' +
                    '<label class="template-load-error" style="color:red"></label>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success btn-load-template" >' + _this.langArr.popupLoadTemplateOk + '</button>' +
                    '<button type="button" class="btn btn-default" data-dismiss="modal">' + _this.langArr.popupLoadTemplateClose + '</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                jQuery(_popup_load_template).appendTo('body');

            },
            /**
             * show output in page
             */
            display: function(data) {
                _this.$elem.html(data);
            },
            /**
             * this functions must be work after generate source code
             */
            default_func: function() {
                //Nicescroll
                _this.$elem.find(".bal-elements-container").niceScroll({
                    cursorcolor: "#5D8397",
                    cursorwidth: "10px",
                    background: "#253843"
                });
                //make bootstrap tooltip
                jQuery('[data-toggle="tooltip"]').tooltip();

                // set colorpicker  default values and chnaged events
                _this.$elem.find('#bg-color').ColorPicker({
                    color: '#fff',
                    onChange: function(hsb, hex, rgb) {
                        $('#bg-color').css('background-color', '#' + hex);
                        _this.changeSettings($('#bg-color').attr('setting-type'), '#' + hex);
                    }
                });

                _this.$elem.find('#text-color').ColorPicker({
                    color: '#000',
                    onChange: function(hsb, hex, rgb) {
                        $('#text-color').css('background-color', '#' + hex);
                        _this.changeSettings($('#text-color').attr('setting-type'), '#' + hex);
                    }
                });

                //show content edit on page load
                setTimeout(function() {
                    jQuery('.bal-content-wrapper').click();
                    _this.tabMenu('typography');
                }, 100);
                _this.makeSortable();
                //_this.tabMenu('typography');

                _aceEditor = ace.edit("editorHtml");
                _aceEditor.setTheme("ace/theme/monokai");
                _aceEditor.getSession().setMode("ace/mode/html");

                _this.tinymceContextMenu();
            },
            /**
             * Remove row buttons
             */
            remove_row_elements: function() {
                if (_this.config.showRowMoveButton == false) {
                    jQuery('.row-move').remove();
                }

                if (_this.config.showRowRemoveButton == false) {
                    jQuery('.row-remove').remove();
                }

                if (_this.config.showRowDuplicateButton == false) {
                    jQuery('.row-duplicate').remove();
                }

                if (_this.config.showRowCodeEditorButton == false) {
                    jQuery('.row-code').remove();
                }
            },
            /**
             *  Get content active element for change setting
             */
            getActiveElementContent: function() {

                _element = _this.$elem.find('.sortable-row.active .sortable-row-content .element-content');

                //element-contenteditable active
                if (_element.find('[contenteditable="true"]').hasClass('element-contenteditable')) {
                    _element = _element.find('.element-contenteditable.active');
                }

                if (_this.$elem.find('.bal-content-wrapper').hasClass('active')) {
                    _element = _this.$elem.find('.bal-content-wrapper');
                }

                return _element;
            },
            /**
             *  Make content elements sortable
             */
            makeSortable: function() {
                _this.$elem.find(".email-editor-elements-sortable").sortable({
                    group: 'no-drop',
                    handle: '.row-move'
                });
            },
            /**
             *  All events
             */
            events: function() {

                jQuery(function() {
                    if (_this.config.onAfterLoad !== undefined) {
                        _this.config.onAfterLoad();
                    }

                    setTimeout(function() {
                        _this.makeSortable();
                    }, 2000);
                });

                //left menu tab click
                _this.$elem.find('.tab-selector').on('click', function() {
                    _element = $(this);
                    _this.tabMenuItemClick(_element, true);
                });
                //menu accordion click
                _this.$elem.find('.bal-elements-accordion .bal-elements-accordion-item-title').on('click', function(j) {
                    _element = $(this);
                    _this.menuAccordionClick(_element, false);
                });

                _this.$elem.find('.bal-collapse').on('click', function() {
                    _element = $(this);
                    _dataValue = _element.attr('data-value');
                    //console.log(_dataValue);
                    if (_dataValue === 'closed') {
                        _this.$elem.find('.bal-left-menu-container').animate({
                            width: 380
                        }, 300, function() {
                            _this.$elem.find('.bal-elements').show();
                            _this.$elem.find('.bal-content').css({
                                'padding-left': '380px'
                            });
                            _this.$elem.find('.bal-left-menu-container').find('.bal-menu-item:eq(0)').addClass('active');
                        });
                        _element.find('.bal-menu-name').show();
                        _element.find('.fa').removeClass().addClass('fa fa-chevron-circle-left');
                        _element.attr('data-value', 'opened');
                    } else {
                        _this.$elem.find('.bal-left-menu-container').animate({
                            width: 70
                        }, 300, function() {
                            _this.$elem.find('.bal-elements').hide();
                            _this.$elem.find('.bal-left-menu-container').find('.bal-menu-item.active').removeClass('active');
                        });
                        _this.$elem.find('.bal-content').css({
                            'padding-left': '70px'
                        });
                        _element.find('.bal-menu-name').hide();
                        _element.find('.fa').removeClass().addClass('fa fa-chevron-circle-right');
                        _element.attr('data-value', 'closed');
                    }
                });

                _this.$elem.find('.blank-page').on('click', function() {
                    _element = $(this);
                    _this.$elem.find('.bal-content-main').html('<div class="email-editor-elements-sortable">' +
                        '<div class="sortable-row">' +
                        '<div class="sortable-row-container">' +
                        '<div class="sortable-row-actions">' +
                        '<div class="row-move row-action">' +
                        '<i class="fa fa-arrows-alt"></i>' +
                        '</div>' +
                        '<div class="row-remove row-action">' +
                        '<i class="fa fa-remove"></i>' +
                        '</div>' +
                        '<div class="row-duplicate row-action">' +
                        '<i class="fa fa-files-o"></i>' +
                        '</div>' +
                        '<div class="row-code row-action">' +
                        '<i class="fa fa-code"></i>' +
                        '</div>' +
                        '</div>' +
                        '<div class="sortable-row-content">' +
                        _this.config.blankPageHtml +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '</div>');
                    _this.makeSortable();
                    _this.remove_row_elements();
                });

                _this.$elem.find('.bal-elements-container .sortable-row-content').each(function(i) {
                    _element = $(this);
                    (function(_element) {
                        $.get(_element.attr('data-url'), function(responseText) {
                            _element.html(responseText.split('[site-url]').join(_this.getAbsolutePath()));
                        });
                    }(_element));
                });

                _this.$elem.find(".bal-elements-list .bal-elements-list-item").draggable({
                    connectToSortable: ".email-editor-elements-sortable",
                    helper: "clone",
                    //revert: "invalid",
                    create: function(event, ui) {
                        //console.log(event.target);
                    },
                    drag: function(event, ui) {
                        //console.log(ui.helper);
                    },
                    start: function(event, ui) {
                        _this.$elem.find(".bal-elements-container").css({
                            'overflow': ''
                        });
                        ui.helper.find('.bal-preview').hide();
                        ui.helper.find('.bal-view').show()
                            //$(this).find('.demo').show();

                        if (_this.config.onElementDragStart !== undefined) {
                            _this.config.onElementDragStart(event);
                        }
                    },
                    stop: function(event, ui) {

                        _this.$elem.find(".bal-elements-container").css({
                            'overflow': 'hidden'
                        });
                        ui.helper.html(ui.helper.find('.bal-view').html());
                        _this.$elem.find('.email-editor-elements-sortable .bal-elements-list-item').css({
                            'width': 'auto',
                            'height': 'auto'
                        });
                        var contentHtml=_this.getContentHtml();
                        if (_this.config.onElementDragFinished !== undefined) {
                            _this.config.onElementDragFinished(event,contentHtml);
                        }
                    }
                });



                _this.$elem.on('click', '.bal-content-wrapper', function(event) {
                    _this.$elem.find('.sortable-row.active').removeClass('active');
                    _this.$elem.find('.sortable-row .element-contenteditable.active').removeClass('.element-contenteditable .active');
                    jQuery(this).addClass('active');
                    _dataTypes = jQuery(this).attr('data-types');
                    if (_dataTypes.length < 1) {
                        return;
                    }
                    _typeArr = _dataTypes.toString().split(',');
                    _arrSize = _this.$elem.find('.tab-property .bal-elements-accordion-item').length;
                    for (var i = 0; i < _arrSize; i++) {
                        _accordionMenuItem = _this.$elem.find('.tab-property .bal-elements-accordion-item').eq(i);
                        //console.log(_accordionMenuItem.attr('data-type'))
                        if (_dataTypes.indexOf(_accordionMenuItem.attr('data-type')) > -1) {
                            _accordionMenuItem.show();
                        } else {
                            _accordionMenuItem.hide();
                        }
                    }
                    _this.$elem.find('[setting-type="padding-top"]').val(jQuery(this).css('padding-top').replace('px', ''));
                    _this.$elem.find('[setting-type="padding-bottom"]').val(jQuery(this).css('padding-bottom').replace('px', ''));
                    _this.$elem.find('[setting-type="padding-left"]').val(jQuery(this).css('padding-left').replace('px', ''));
                    _this.$elem.find('[setting-type="padding-right"]').val(jQuery(this).css('padding-right').replace('px', ''));

                    _this.$elem.find('.email-width').val(jQuery('.bal-content-main').width());

                    _this.tabMenu(_typeArr[0]);
                });

                _this.events_of_row();

                _this.events_of_property();

                _this.events_of_popup();

                _this.events_of_setting();

                _this.remove_row_elements();
            },
            /**
             *  Events of row
             */
            events_of_row: function() {
                //remove button
                _this.$elem.on('click', '.sortable-row .row-remove', function(e) {

                    if (_this.config.onBeforeRemoveButtonClick !== undefined) {
                        _this.config.onBeforeRemoveButtonClick(e);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (e.isDefaultPrevented() == true) {
                        return false;
                    }

                    if (_this.$elem.find('.bal-content .sortable-row').length == 1) {
                        alert('At least should be 1 item');
                        return;
                    }
                    jQuery(this).parents('.sortable-row').remove();

                    if (_this.config.onAfterRemoveButtonClick !== undefined) {
                        _this.config.onAfterRemoveButtonClick(e);
                    }
                });

                //duplicate button
                _this.$elem.on('click', '.sortable-row .row-duplicate', function(e) {
                    if (_this.config.onBeforeRowDuplicateButtonClick !== undefined) {
                        _this.config.onBeforeRowDuplicateButtonClick(e);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (e.isDefaultPrevented() == true) {
                        return false;
                    }
                    if (jQuery(this).hasParent('.bal-elements-list-item')) {
                        _parentSelector = '.bal-elements-list-item';
                    } else {
                        _parentSelector = '.sortable-row';
                    }
                    _parent = jQuery(this).parents(_parentSelector);
                    jQuery('.sortable-row').removeClass('active');
                    jQuery('.bal-elements-list-item').removeClass('active');
                    _parent.addClass('active');
                    //_parent.after('<div class="sortable-row">'+ _parent.html()+"</div>");
                    _parent.clone().insertAfter(_parentSelector + '.active');

                    if (_this.config.onAfterRowDuplicateButtonClick !== undefined) {
                        _this.config.onAfterRowDuplicateButtonClick(e);
                    }
                });

                //code button . for showing code editor popup
                _this.$elem.on('click', '.sortable-row .row-code', function(e) {
                    if (_this.config.onBeforeRowEditorButtonClick !== undefined) {
                        _this.config.onBeforeRowEditorButtonClick(e);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (e.isDefaultPrevented() == true) {
                        return false;
                    }
                    jQuery(this).parents('.sortable-row').addClass('code-editor');
                    _html = jQuery(this).parents('.sortable-row').find('.sortable-row-content').html();
                    _aceEditor.session.setValue(_html);

                    if (_this.config.onAfterRowEditorButtonClick !== undefined) {
                        _this.config.onAfterRowEditorButtonClick(e);
                    }

                    if (_this.config.onBeforeShowingEditorPopup !== undefined) {
                        _this.config.onBeforeShowingEditorPopup(e);
                    }
                    if (e.isDefaultPrevented() == true) {
                        return false;
                    }
                    jQuery('#popup_editor').modal('show');
                });

                _this.$elem.on('click', '.element-content', function(event) {
                    jQuery('.bal-content-wrapper').removeClass('active');
                    _this.$elem.find('[contenteditable="true"]').removeClass('element-contenteditable active');
                    _this.sortableClick(jQuery(this));
                    event.stopPropagation();
                });

                _this.$elem.on('click', '[contenteditable="true"]', function(event) {
                    jQuery('.bal-content-wrapper').removeClass('active');
                    _this.$elem.find('.bal-content [contenteditable="true"]').removeClass('element-contenteditable active')
                    jQuery(this).addClass('element-contenteditable active');
                    _this.sortableClick(jQuery(this));

                    event.stopPropagation();
                });
            },
            /**
             *  Events for Property
             */
            events_of_property: function() {
                //email width of template
                _this.$elem.on('keyup', '.email-width', function(event) {
                    _element = jQuery(this);
                    _val = jQuery('.email-width').val();
                    if (parseInt(_val) < 300 || parseInt(_val) > 1000) {
                        return false;
                    }
                    jQuery('.bal-content-main').css('width', _val + 'px');
                });

                //hyperlink
                _this.$elem.on('keyup', '.bal-elements-accordion-item-content .hyperlink-url', function(event) {
                    _element = jQuery(this);
                    _val = _element.val();
                    _activeElement = _this.getActiveElementContent();
                    _activeElement.attr('href', _val);
                });

                //youtube
                _this.$elem.on('keyup', '.bal-elements-accordion-item-content .youtube', function(event) {
                    _element = jQuery(this);
                    _val = _element.val();
                    _activeElement = _this.getActiveElementContent();
                    _activeElement.find('iframe').attr('src', 'https://www.youtube.com/embed/' + _val);
                });

                //text style
                _this.$elem.on('click', '.bal-text-icons .bal-icon-box-item', function(event) {
                    _element = jQuery(this);
                    if (_element.hasClass('active')) {
                        _element.removeClass('active');
                    } else {
                        _element.addClass('active');
                    }
                    if (_this.$elem.find('.bal-text-icons .bal-icon-box-item.fontStyle').hasClass('active')) {
                        _this.changeSettings('font-style', 'italic');
                    } else {
                        _this.changeSettings('font-style', '');
                    }
                    _value = '';
                    if (_this.$elem.find('.bal-text-icons .bal-icon-box-item.underline').hasClass('active')) {
                        _value += 'underline ';
                    }
                    if (_this.$elem.find('.bal-text-icons .bal-icon-box-item.line').hasClass('active')) {
                        _value += ' line-through';
                    }
                    _this.changeSettings('text-decoration', _value);
                });


                //font
                _this.$elem.on('click', '.bal-font-icons .bal-icon-box-item', function(event) {
                    _element = jQuery(this);
                    if (_element.hasClass('active')) {
                        _element.removeClass('active');
                    } else {
                        _element.addClass('active');
                    }
                    if (_this.$elem.find('.bal-font-icons .bal-icon-box-item').hasClass('active')) {
                        _this.changeSettings('font-weight', 'bold');
                    } else {
                        _this.changeSettings('font-weight', '');
                    }
                });

                //align
                _this.$elem.on('click', '.bal-align-icons .bal-icon-box-item', function(event) {
                    _element = jQuery(this);
                    _this.$elem.find('.bal-align-icons .bal-icon-box-item').removeClass('active');
                    _element.addClass('active');
                    _value = 'left';
                    if (_this.$elem.find('.bal-align-icons .bal-icon-box-item.center').hasClass('active')) {
                        _value = 'center';
                    }
                    if (_this.$elem.find('.bal-align-icons .bal-icon-box-item.right').hasClass('active')) {
                        _value = 'right';
                    }
                    _this.changeSettings('text-align', _value);
                });


                //chnage form cpntrol value for select
                _this.$elem.on('change', '.bal-left-menu-container  .form-control', function(event) {
                    _element = jQuery(this);
                    _this.changeSettings(_element.attr('setting-type'), _element.val());
                });

                //chnage form cpntrol value for input
                _this.$elem.on('keyup', '.bal-left-menu-container .form-control', function(event) {
                    _element = jQuery(this);
                    if (_element.hasClass('all') && _element.hasClass('padding')) {
                        _this.$elem.find('.padding:not(".all")').val(_element.val());
                    }
                    if (_element.hasClass('all') && _element.hasClass('border-radius')) {
                        _this.$elem.find('.border-radius:not(".all")').val(_element.val());
                    }
                    _this.changeSettings(_element.attr('setting-type'), _element.val() + 'px');
                });

                //social
                _this.$elem.on('keyup', '.bal-social-content-box .social-input', function(event) {
                    _element = jQuery(this);
                    _socialType = _element.parents('.row').attr('data-social-type');
                    _val = _element.val();
                    _activeElement = _this.getActiveElementContent();
                    if (_activeElement.hasClass('social-content')) {
                        _activeElement.find('a.' + _socialType).attr('href', _val);
                    }
                });

                //image-size
                _this.$elem.on('keyup', '.image-size', function(event) {
                    _activeElement = _this.getActiveElementContent();

                    if (jQuery(this).hasClass('image-height')) {
                        _activeElement.find('.content-image').css('height', jQuery(this).val());
                    } else if (jQuery(this).hasClass('image-width')) {
                        _activeElement.find('.content-image').css('width', jQuery(this).val());
                    }
                });

                //number
                _this.$elem.on('keydown', '.number', function(event) {
                    // Allow: backspace, delete, tab, escape, enter and .
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1
                        //// Allow: Ctrl+A
                        //(e.keyCode == 65 && e.ctrlKey === true) ||
                        //// Allow: Ctrl+C
                        //(e.keyCode == 67 && e.ctrlKey === true) ||
                        //// Allow: Ctrl+X
                        //(e.keyCode == 88 && e.ctrlKey === true) ||
                        //// Allow: home, end, left, right
                        //(e.keyCode >= 35 && e.keyCode <= 39)
                    ) {
                        // let it happen, don't do anything
                        return;
                    }
                    // Ensure that it is a number and stop the keypress
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });

                //example
                _this.$elem.on('change', '.bal-social-content-box .checkbox-title input', function(event) {
                    _socialType = jQuery(this).parents('.row').attr('data-social-type');
                    _activeElement = _this.getActiveElementContent();
                    if (jQuery(this).is(":checked")) {
                        _activeElement.find('a.' + _socialType).show();
                    } else {
                        _activeElement.find('a.' + _socialType).hide();
                    }
                });


            },
            /**
             *  Events for Settings
             */
            events_of_setting: function() {

                //other-devices
                _this.$elem.on('click', '.bal-setting-item.other-devices', function(event) {
                    _element = jQuery(this);
                    _parent = _element.parents('.bal-settings');
                    if (_element.hasClass('active')) {
                        _parent.find('.bal-setting-content .bal-setting-content-item.other-devices').hide();
                        _element.removeClass('active');
                    } else {
                        _parent.find('.bal-setting-content .bal-setting-content-item.other-devices').show();
                        _element.addClass('active');
                    }
                });

                //other devices content
                _this.$elem.on('click', '.bal-setting-content .bal-setting-device-tab', function(event) {
                    _element = jQuery(this);

                    _parent = _element.parents('.bal-setting-content');
                    _parent.find('.bal-setting-device-tab').removeClass('active');
                    _element.addClass('active');
                    _parent.find('.bal-setting-device-content').removeClass('active');
                    _parent.find('.bal-setting-device-content.' + _element.attr('data-tab')).addClass('active');
                    _removeClass = 'sm-width lg-width';
                    _addClass = '';
                    switch (_element.attr('data-tab')) {
                        case 'mobile-content':
                            _addClass = 'sm-width';
                            break;
                        case 'desktop-content':
                            _addClass = 'lg-width';
                            break;
                    }
                    _this.$elem.find('.bal-content-main').removeClass(_removeClass);
                    _this.$elem.find('.bal-content-main').addClass(_addClass);
                });

                //laod templates button
                _this.$elem.on('click', '.bal-setting-item.load-templates', function(event) {

                    if (_this.config.onBeforeSettingsLoadTemplateButtonClick !== undefined) {
                        _this.config.onBeforeSettingsLoadTemplateButtonClick(event);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }
                    jQuery('.btn-load-template').hide();
                    $('#popup_load_template').modal('show');
                });

                //export click
                _this.$elem.on('click', '.bal-setting-item.export', function(event) {
                    _getHtml = _this.getContentHtml();
                    if (_this.config.onSettingsExportButtonClick !== undefined) {
                        _this.config.onSettingsExportButtonClick(event, _getHtml);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }
                });

                //preview click
                _this.$elem.on('click', '.bal-setting-item.preview', function(event) {
                    _getHtml = _this.getContentHtml();
                    if (_this.config.onSettingsPreviewButtonClick !== undefined) {
                        _this.config.onSettingsPreviewButtonClick(event, _getHtml);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }
                });

                //save-template click
                _this.$elem.on('click', '.bal-setting-item.save-template', function(event) {
                    if (_this.config.onBeforeSettingsSaveButtonClick !== undefined) {
                        _this.config.onBeforeSettingsSaveButtonClick(event);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }


                    jQuery('.input-error').text('');
                    jQuery('.template-name').val('');
                    jQuery('#popup_save_template').modal('show');
                });


                //btn-save-template
                jQuery('#popup_save_template').on('click', '.btn-save-template', function(event) {

                    jQuery('.input-error').text('');
                    if (jQuery('.template-name').val().length < 1) {
                        jQuery('.input-error').text(_this.langArr.popupSaveTemplateError);
                        return false;
                    }

                    if (_this.config.onPopupSaveButtonClick !== undefined) {
                        _this.config.onPopupSaveButtonClick(event);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }

                });

                //send-email
                _this.$elem.on('click', '.bal-setting-item.send-email', function(event) {

                    if (_this.config.onSettingsSendMailButtonClick !== undefined) {
                        _this.config.onSettingsSendMailButtonClick(event);
                    }

                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }
                    jQuery('.recipient-email').val('');
                    jQuery('.popup_send_email_output').text('');
                    jQuery('#popup_send_email').modal('show');

                });

                jQuery('#popup_send_email').on('click', '.btn-send-email-template', function(event) {
                    _element = $(this);
                    if ($(this).hasClass('has-loading')) {
                        return;
                    }
                    _element.addClass('has-loading');
                    _element.text(_this.langArr.loading);

                    _getHtml = _this.getContentHtml();

                    if (_this.config.onPopupSendMailButtonClick !== undefined) {
                        _this.config.onPopupSendMailButtonClick(event, _getHtml);
                    }
                    //if user want stop this action : e.preventDefault();
                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }

                });
                //open modal for change image
                _this.$elem.on('click', '.change-image', function(event) {

                    if (_this.config.onBeforeChangeImageClick !== undefined) {
                        _this.config.onBeforeChangeImageClick(event);
                    }

                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }
                    jQuery('#popup_images').modal('show');

                });
                //select image
                jQuery('#popup_images').on('click', '.upload-image-item', function(event) {
                    jQuery('.modal .upload-image-item').removeClass('active');
                    jQuery(this).addClass('active');
                });

                //change select image button click
                jQuery('#popup_images').on('click', '.btn-select', function(event) {

                    if (_this.config.onBeforePopupSelectImageButtonClick !== undefined) {
                        _this.config.onBeforePopupSelectImageButtonClick(event);
                    }

                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }


                    _url = jQuery('.modal').find('.upload-image-item.active').attr('src');
                    _this.getActiveElementContent().find('.content-image').attr('src', _url);
                    jQuery('#popup_images').modal('hide');
                });

                jQuery('#popup_load_template').on('click', '.template-list .template-item', function(event) {

                    jQuery('.template-list .template-item').removeClass('active');
                    jQuery(this).addClass('active');
                    jQuery('.btn-load-template').show();
                });

                jQuery('#popup_load_template').on('click', '.btn-load-template', function(event) {

                    if (_this.config.onBeforePopupSelectTemplateButtonClick !== undefined) {
                        _this.config.onBeforePopupSelectTemplateButtonClick(event);
                    }

                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }

                    _dataId = jQuery('.template-list .template-item.active').attr('data-id');
                    //search template in array
                    var result = jQuery.grep(_templateListItems, function(e) {
                        return e.id == _dataId;
                    });
                    if (result.length == 0) {
                        //show error
                        jQuery('.template-load-error').text('An error has occurred');
                    }
                    _contentText = jQuery('<div/>').html(result[0].content).text();
                    jQuery('.bal-content-wrapper').html(_contentText);
                    jQuery('#popup_load_template').modal('hide');
                    _this.makeSortable();
                });

                jQuery('body').on('click', '.btn-upload', function(event) {

                    if (_this.config.onPopupUploadImageButtonClick !== undefined) {
                        _this.config.onPopupUploadImageButtonClick(event);
                    }

                    if (event.isDefaultPrevented() == true) {
                        return false;
                    }


                });

            },
            /**
             *  Events of popup save
             */
            events_of_popup: function() {

                //save code editor
                jQuery('#popup_editor').on('click', '.btn-save-editor', function() {
                    jQuery('.sortable-row.code-editor .sortable-row-content').html(_aceEditor.getSession().getValue());
                    jQuery('#popup_editor').modal('hide');
                    jQuery('.sortable-row.code-editor').removeClass('code-editor');
                });

            },
            /**
             *  Left menu tab click event
             */
            tabMenuItemClick: function(_element, handle) {
                _tabSelector = _element.data('tab-selector');
                if (_element.hasClass('bal-collapse')) {
                    return false;
                }
                _this.$elem.find('.bal-menu-item.active').removeClass('active');
                _this.$elem.find('.bal-element-tab.active').removeClass('active');
                //show tab content
                _this.$elem.find('.' + _tabSelector).addClass('active');
                //select new tab
                _element.addClass('active');
                if (!handle) {
                    _this.$elem.find('.sortable-row.active').removeClass('active');
                }
            },
            /**
             *  menu accordion
             */
            menuAccordionClick: function(_element, toggle) {
                var dropDown = _element.closest('.bal-elements-accordion-item').find('.bal-elements-accordion-item-content');
                _element.closest('.bal-elements-accordion').find('.bal-elements-accordion-item-content').not(dropDown).slideUp();
                if ($('.tab-property').hasClass('active')) {
                    _this.$elem.find('.sortable-row.active .main').attr('data-last-type', _element.closest('.bal-elements-accordion-item').attr('data-type'));
                }
                if (!toggle) {
                    _element.closest('.bal-elements-accordion').find('.bal-elements-accordion-item-title.active').removeClass('active');
                    _element.addClass('active');
                    dropDown.stop(false, true).slideDown();
                } else {
                    if (_element.hasClass('active')) {
                        _element.removeClass('active');
                    } else {
                        _element.closest('.bal-elements-accordion').find('.bal-elements-accordion-item-title.active').removeClass('active');
                        _element.addClass('active');
                    }
                    dropDown.stop(false, true).slideToggle();
                }
            },
            /**
             * Open/close left menu tab and it's child
             */
            tabMenu: function(tab) {
                _menuItem = _tabMenuItems[tab];
                _tabMenuItem = _this.$elem.find('.bal-left-menu-container .bal-menu-item[data-tab-selector="' + _menuItem.parentSelector + '"]');
                _accordionMenuItem = _this.$elem.find('.bal-elements-accordion .bal-elements-accordion-item[data-type="' + _menuItem.itemSelector + '"] .bal-elements-accordion-item-title');
                _this.tabMenuItemClick(_tabMenuItem, true);
                _this.menuAccordionClick(_accordionMenuItem, false);
            },
            /**
             * Get created email template
             */
            getContentHtml: function() {
                _html = '';
                _this.$elem.find('.bal-content .bal-content-wrapper .sortable-row').each(function() {
                    _html += $(this).find('.sortable-row-content').html().split('contenteditable="true"').join('');
                });
                _width = $('.bal-content-main').css('width');
                _style = '';
                _style += 'background:' + $('.bal-content-wrapper').css('background') + ';';
                _style += 'padding:' + $('.bal-content-wrapper').css('padding');
                _result = '<div class="email-content" style="' + _style + '">' + _html + '</div>';

                _result = '<table width="100%" cellspacing="0" cellpadding="0" border="0" style="' + _style + '"><tbody><tr><td><div style="margin:0 auto;width:' + _width + ';">' + _html + '</div></td></tr></table>';
                return _result;
            },
            /**
             * Generate left menu elements tab
             */
            generateElements: function() {
                _outputHtml = '';
                $.ajax({
                    url: _this.config.elementJsonUrl,
                    data: '',
                    success: function(data) {
                        data = data.elements;
                        for (var i = 0; i < data.length; i++) {

                            _outputHtml += '<li class="bal-elements-accordion-item" data-type="' + data[i].name.toLowerCase() + '"><a class="bal-elements-accordion-item-title">' + data[i].name + '</a>';

                            _outputHtml += '<div class="bal-elements-accordion-item-content"><ul class="bal-elements-list">';

                            _items = data[i].items;

                            for (var j = 0; j < _items.length; j++) {
                                _outputHtml += '<li>' +
                                    '<div class="bal-elements-list-item">' +
                                    '<div class="bal-preview">' +
                                    '<div class="bal-elements-item-icon">' +
                                    ' <i class="' + _items[j].icon + '"></i>' +
                                    '</div>' +
                                    '<div class="bal-elements-item-name">' +
                                    _items[j].name +
                                    '</div>' +
                                    '</div>' +
                                    '<div class="bal-view">' +
                                    '<div class="sortable-row">' +
                                    '<div class="sortable-row-container">' +
                                    ' <div class="sortable-row-actions">';

                                if (_this.config.showRowMoveButton == true) {
                                    _outputHtml += '<div class="row-move row-action">' +
                                        '<i class="fa fa-arrows-alt"></i>' +
                                        '</div>';
                                }

                                if (_this.config.showRowRemoveButton == true) {
                                    _outputHtml += '<div class="row-remove row-action">' +
                                        '<i class="fa fa-remove"></i>' +
                                        '</div>';
                                }

                                if (_this.config.showRowDuplicateButton == true) {
                                    _outputHtml += '<div class="row-duplicate row-action">' +
                                        '<i class="fa fa-files-o"></i>' +
                                        '</div>';
                                }

                                if (_this.config.showRowCodeEditorButton == true) {
                                    _outputHtml += '<div class="row-code row-action">' +
                                        '<i class="fa fa-code"></i>' +
                                        '</div>';
                                }
                                _outputHtml += '</div>' +
                                    '<div class="sortable-row-content" data-url="' + _items[j].content + '">' +
                                    '</div>' +
                                    '</div>' +
                                    '</div>' +
                                    ' </div>' +
                                    '</div>' +
                                    '</li>';
                            }


                            _outputHtml += '</ul></div>';
                            _outputHtml += '</li>';
                        }



                        _this.generate(_outputHtml);
                    },
                    error: function() {
                        console.error('Has error');
                    },
                    dataType: 'json'
                });
            },
            /**
             * Get Site Url
             */
            getAbsolutePath: function() {
                var loc = window.location;
                var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
                return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
            },
            /**
             * Content row click event
             */
            sortableClick: function(_thisElement) {
                _element = _thisElement.parents('.sortable-row');
                //select current item
                _this.$elem.find('.bal-content .sortable-row').removeClass('active');
                _element.addClass('active');
                _dataTypes = _element.find('.sortable-row-content .main').attr('data-types');
                if (typeof _dataTypes=='undefined') {
                  return;
                }

                if (_dataTypes.length < 1) {
                    return;
                }
                _typeArr = _dataTypes.toString().split(',');
                _arrSize = _this.$elem.find('.tab-property .bal-elements-accordion-item').length;
                for (var i = 0; i < _arrSize; i++) {
                    _accordionMenuItem = _this.$elem.find('.tab-property .bal-elements-accordion-item').eq(i);
                    //console.log(_accordionMenuItem.attr('data-type'))
                    if (_dataTypes.indexOf(_accordionMenuItem.attr('data-type')) > -1) {
                        _accordionMenuItem.show();
                    } else {
                        _accordionMenuItem.hide();
                    }
                }
                _this.tabMenu(_element.find('.sortable-row-content .main').attr('data-last-type'));
                _this.getSettings();
            },
            /**
             * Get active element settings
             */
            getSettings: function() {
                _element = _this.getActiveElementContent();
                _style = _element.attr('style');
                if (typeof _style === "undefined" || _style.length < 1) {
                    return;
                }
                //background
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="background-color"]').css('background-color', _element.css('background-color'));
                /*Paddings*/
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="padding-top"]').val(_element.css('padding-top').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="padding-bottom"]').val(_element.css('padding-bottom').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="padding-left"]').val(_element.css('padding-left').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="padding-right"]').val(_element.css('padding-right').replace('px', ''));
                /*Border radius*/
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="border-top-left-radius"]').val(_element.css('border-top-left-radius').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="border-top-right-radius"]').val(_element.css('border-top-right-radius').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="border-bottom-left-radius"]').val(_element.css('border-bottom-left-radius').replace('px', ''));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="border-bottom-right-radius"]').val(_element.css('border-bottom-right-radius').replace('px', ''));
                /*text style*/
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="font-family"]').val(_element.css('font-family'));
                _this.$elem.find('.tab-property .bal-elements-accordion-item [setting-type="font-size"]').val(_element.css('font-size').replace('px', ''));
                //text color
                _this.$elem.find('.tab-property .bal-icon-box-item[setting-type="color"]').css({
                    'background': _element.css('color')
                });
                //text align
                _this.$elem.find('.tab-property .bal-align-icons .bal-icon-box-item').removeClass('active');
                _this.$elem.find('.tab-property .bal-align-icons .bal-icon-box-item.' + _element.css('text-align')).addClass('active');
                //text bold
                if (_element.css('font-weight') == 'bold') {
                    _this.$elem.find('.tab-property .bal-icon-box-item[setting-type="bold"]').addClass('active');
                } else {
                    _this.$elem.find('.tab-property .bal-icon-box-item[setting-type="bold"]').removeClass('active');
                }
                //text group style
                _this.$elem.find('.tab-property .bal-text-icons .bal-icon-box-item').removeClass('active');
                if (_element.css('text-decoration').indexOf('underline') > -1) {
                    _this.$elem.find('.tab-property .bal-text-icons .bal-icon-box-item.underline').addClass('active');
                }
                if (_element.css('text-decoration').indexOf('line-through') > -1) {
                    _this.$elem.find('.tab-property .bal-text-icons .bal-icon-box-item.line').addClass('active');
                }
                if (_element.css('font-style').indexOf('italic') > -1) {
                    _this.$elem.find('.tab-property .bal-text-icons .bal-icon-box-item.fontStyle').addClass('active');
                }

                if (_element.hasClass('social-content')) {
                    $('.bal-content .sortable-row.active .sortable-row-content .element-content.social-content a').each(function() {
                        _socialType = jQuery(this).attr('class');
                        _socialRow = _this.$elem.find('[data-social-type="' + _socialType + '"]');
                        _socialRow.find('.social-input').val(jQuery(this).attr('href'));
                        if (jQuery(this).css('display') == 'none') {
                            _socialRow.find('.checkbox-title input').prop("checked", false);
                        } else {
                            _socialRow.find('.checkbox-title input').prop("checked", true);
                        }
                    });
                }
                if (_element.hasClass('youtube-frame')) {
                    _ytbUrl = _element.find('iframe').attr('src');
                    _this.$elem.find('.youtube').val(_ytbUrl.split('/')[4]);
                }

                //hyperlink
                if (_element.hasClass('hyperlink')) {
                    _href = _element.attr('href');
                    _this.$elem.find('.hyperlink-url').val(_href);
                }
                //image size
                _this.$elem.find('.tab-property .bal-elements-accordion-item .image-width').val(_element.find('.content-image').css('width'));
                _this.$elem.find('.tab-property .bal-elements-accordion-item .image-height').val(_element.find('.content-image').css('height'));

            },
            /**
             * Change active element settings
             */
            changeSettings: function(type, value) {
                _activeElement = _this.getActiveElementContent();
                if (type == 'font-size') {
                    _activeElement.find('>h1,>h4').css(type, value);
                } else if (type == 'background-image') {
                    _activeElement.css(type, 'url("' + value + '")');
                    _activeElement.css({
                        'background-size': 'cover',
                        'background-repeat': 'no-repeat'
                    });
                }
                _activeElement.css(type, value);
            },
            /**
             * Get selected html of the window
             */
            getSelectedHtml: function() {
                var html = "";
                if (typeof window.getSelection != "undefined") {
                    var sel = window.getSelection();
                    if (sel.rangeCount) {
                        var container = document.createElement("div");
                        for (var i = 0, len = sel.rangeCount; i < len; ++i) {
                            container.appendChild(sel.getRangeAt(i).cloneContents());
                        }
                        html = container.innerHTML;
                    }
                } else if (typeof document.selection != "undefined") {
                    if (document.selection.type == "Text") {
                        html = document.selection.createRange().htmlText;
                    }
                }
                return html;
            },
            /**
             * tinymce Context Menu
             */
            tinymceContextMenu:function () {
              if (_this.config.showContextMenu == false) {
                  return false;
              }
              var _toolBar='';//'fontselect fontsizeselect bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | bullist numlist | link | unlink removeformat',
              if (_this.config.showContextMenu_FontFamily == true) {
                  _toolBar+='fontselect ';
              }
              if (_this.config.showContextMenu_FontSize == true) {
                  _toolBar+='fontsizeselect ';
              }
              if (_this.config.showContextMenu_Bold == true) {
                  _toolBar+='bold ';
              }
              if (_this.config.showContextMenu_Italic == true) {
                  _toolBar+='italic ';
              }
              if (_this.config.showContextMenu_Underline == true) {
                  _toolBar+='underline ';
              }
              if (_this.config.showContextMenu_Strikethrough == true) {
                  _toolBar+='strikethrough ';
              }
              if (_this.config.showContextMenu_Hyperlink == true) {
                  _toolBar+='link ';
              }
              //default options
              _toolBar+=' | alignleft aligncenter alignright alignjustify | bullist numlist |  unlink removeformat  ';

              tinymce.init({
                    selector: 'div.bal-content-wrapper',
                    theme: 'inlite',
                    plugins: ' link',
                    width : 300,
                    selection_toolbar: _toolBar,
                    fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt 48pt 72pt",
                    inline: true,
                    paste_data_images: false
              });
            },
            /**
             * Get languages
             */
            getLangs: function() {
                $.ajax({
                    url: _this.config.langJsonUrl,
                    data: '',
                    success: function(data) {
                        jQuery.each(data, function(i, val) {
                            _language[i] = val[0];
                        });

                        //set language data to private variable
                        _this.langArr = _language[_this.config.lang];
                        _this.generateElements();
                    },
                    error: function() {
                        console.error('Has error');
                    },
                    dataType: 'json'
                });
            }
        };

        $.fn.emailBuilder = function(options) {

            var _emailBuilder;
            /**
             * Set elements json file url, include which elements want to show in email builder
             */
            this.setElementJsonUrl = function(elementJsonUrl) {
                    _emailBuilder.config.elementJsonUrl = elementJsonUrl;
                }
                /**
                 * Chnage language builder  (en | fr | de | ru | tr ).
                 */
            this.setLang = function(lang) {
                    _emailBuilder.config.lang = lang;
                }
                /**
                 *  Set json file url  include which supported languages .
                 *  If you want ,you can add any language very easily.
                 */
            this.setLangJsonUrl = function(value) {
                    _emailBuilder.config.langJsonUrl = value;
                }
                /**
                 * Set blank page html source. when users want to create blank page,they see this html
                 */
            this.setBlankPageHtml = function(blankPageHtml) {
                    _emailBuilder.config.blankPageHtml = blankPageHtml;
                }
                /**
                 * Set html when page loading you can load your template from database or you can show any html into editor
                 */
            this.setLoadPageHtml = function(loadPageHtml) {
                _emailBuilder.config.loadPageHtml = loadPageHtml;
            }


            /**
             * Show or hide context menu in editor
             */
            this.setShowContextMenu = function(showContextMenu) {
                    _emailBuilder.config.showContextMenu = showContextMenu;
                }
                /**
                 * Show or hide font family option context menu in editor
                 */
            this.setShowContextMenu_FontFamily = function(showContextMenu_FontFamily) {
                _emailBuilder.config.showContextMenu_FontFamily = showContextMenu_FontFamily;
            }

            /**
             * Show or hide font size option context menu in editor
             */
            this.setShowContextMenu_FontSize = function(showContextMenu_FontSize) {
                    _emailBuilder.config.showContextMenu_FontSize = showContextMenu_FontSize;
                }
                /**
                 * Show or hide bold option context menu in editor
                 */
            this.setShowContextMenu_Bold = function(showContextMenu_Bold) {
                    _emailBuilder.config.showContextMenu_Bold = showContextMenu_Bold;
                }
                /**
                 * Show or hide italic option context menu in editor
                 */
            this.setShowContextMenu_Italic = function(showContextMenu_Italic) {
                    _emailBuilder.config.showContextMenu_Italic = showContextMenu_Italic;
                }
                /**
                 * Show or hide underline option context menu in editor
                 */
            this.setShowContextMenu_Underline = function(showContextMenu_Underline) {
                    _emailBuilder.config.showContextMenu_Underline = showContextMenu_Underline;
                }
                /**
                 * Show or hide strikethrough option context menu in editor
                 */
            this.setShowContextMenu_Strikethrough = function(showContextMenu_Strikethrough) {
                    _emailBuilder.config.showContextMenu_Strikethrough = showContextMenu_Strikethrough;
                }
                /**
                 * Show or hide hyperlink option context menu in editor
                 */
            this.setShowContextMenu_Hyperlink = function(showContextMenu_Hyperlink) {
                _emailBuilder.config.showContextMenu_Hyperlink = showContextMenu_Hyperlink;
            }




            /**
             * Show or hide elements tab in left menu
             */
            this.setShowElementsTab = function(showElementsTab) {
                    _emailBuilder.config.showElementsTab = showElementsTab;
                }
                /**
                 * Show or hide property tab in left menu
                 */
            this.setShowPropertyTab = function(showPropertyTab) {
                    _emailBuilder.config.showPropertyTab = showPropertyTab;
                }
                /**
                 * Show or hide 'collapse menu' button in left menu
                 */
            this.setShowCollapseMenu = function(showCollapseMenu) {
                    _emailBuilder.config.showCollapseMenu = showCollapseMenu;
                }
                /**
                 * Show or hide 'blank page' button in left menu
                 */
            this.setShowBlankPageButton = function(showBlankPageButton) {
                    _emailBuilder.config.showBlankPageButton = showBlankPageButton;
                }
                /**
                 * Show or hide 'collapse menu' button bottom or above
                 */
            this.setShowCollapseMenuinBottom = function(showCollapseMenuinBottom) {
                _emailBuilder.config.showCollapseMenuinBottom = showCollapseMenuinBottom;
            }


            /**
             * Set value show or hide settings bar
             */
            this.setShowSettingsBar = function(showSettingsBar) {
                    _emailBuilder.config.showSettingsBar = showSettingsBar;
                }
                /**
                 * Set value  show or hide 'Preview' button in settings bar
                 */
            this.setShowSettingsPreview = function(showSettingsPreview) {
                    _emailBuilder.config.showSettingsPreview = showSettingsPreview;
                }
                /**
                 * Set value show or hide 'Export' button in settings bar
                 */
            this.setShowSettingsExport = function(showSettingsExport) {
                    _emailBuilder.config.showSettingsExport = showSettingsExport;
                }
                /**
                 * Set value show or hide 'Send Mail' button in settings bar
                 */
            this.setShowSettingsSendMail = function(showSettingsSendMail) {
                    _emailBuilder.config.showSettingsSendMail = showSettingsSendMail;
                }
                /**
                 * Set value show or hide 'Save' button in settings bar
                 */
            this.setShowSettingsSave = function(showSettingsSave) {
                    _emailBuilder.config.showSettingsSave = showSettingsSave;
                }
                /**
                 * Set value show or hide 'Load Template' button in settings bar
                 */
            this.setShowSettingsLoadTemplate = function(showSettingsLoadTemplate) {
                _emailBuilder.config.showSettingsLoadTemplate = showSettingsLoadTemplate;
            }


            /**
             * Set value show or hide 'Move' button in actions row item
             */
            this.setShowRowMoveButton = function(showRowMoveButton) {
                    _emailBuilder.config.showRowMoveButton = showRowMoveButton;
                }
                /**
                 * Set value show or hide 'Remove' button in actions row item
                 */
            this.setShowRowRemoveButton = function(showRowRemoveButton) {
                    _emailBuilder.config.showRowRemoveButton = showRowRemoveButton;
                }
                /**
                 * Set value show or hide 'Duplicate' button in actions row item
                 */
            this.setShowRowDuplicateButton = function(showRowDuplicateButton) {
                    _emailBuilder.config.showRowDuplicateButton = showRowDuplicateButton;
                }
                /**
                 * Set value show or hide 'Code Editor' button in actions row item
                 */
            this.setShowRowCodeEditorButton = function(showRowCodeEditorButton) {
                _emailBuilder.config.showRowCodeEditorButton = showRowCodeEditorButton;
            }

            /**
             * Init email builder any time
             */
            this.init = function() {
                _emailBuilder.init();
            }



            /**
             * Set settings preview button click event
             */
            this.setSettingsPreviewButtonClick = function(func) {
                    _emailBuilder.config.onSettingsPreviewButtonClick = func;
                }
                /**
                 * Set Settings export button click event
                 */
            this.setSettingsExportButtonClick = function(func) {
                    _emailBuilder.config.onSettingsExportButtonClick = func;
                }
                /**
                 * Set Settings before save button click event
                 */
            this.setBeforeSettingsSaveButtonClick = function(func) {
                    _emailBuilder.config.onBeforeSettingsSaveButtonClick = func;
                }
                /**
                 * Set Settings save button click event
                 */
            this.setSettingsSaveButtonClick = function(func) {
                    _emailBuilder.config.onSettingsSaveButtonClick = func;
                }
                /**
                 * Set Settings before load template button click event
                 */
            this.setBeforeSettingsLoadTemplateButtonClick = function(func) {
                    _emailBuilder.config.onBeforeSettingsLoadTemplateButtonClick = func;
                }
                /**
                 * Set Settings send mail button click event
                 */
            this.setSettingsSendMailButtonClick = function(func) {
                _emailBuilder.config.onSettingsSendMailButtonClick = func;
            }

            /**
             * Set Before 'change image' click event
             */
            this.setBeforeChangeImageClick = function(func) {
                    _emailBuilder.config.onBeforeChangeImageClick = func;
                }
                /**
                 * Set Before save button click event in 'select image' popup
                 */
            this.setBeforePopupSelectImageButtonClick = function(func) {
                    _emailBuilder.config.onBeforePopupSelectImageButtonClick = func;
                }
                /**
                 * Set xxxxxxxxxxxx
                 */
            this.setBeforePopupSelectTemplateButtonClick = function(func) {
                    _emailBuilder.config.onBeforePopupSelectTemplateButtonClick = func;
                }
                /**
                 * Set Save button click event in 'Save template' popup
                 */
            this.setPopupSaveButtonClick = function(func) {
                    _emailBuilder.config.onPopupSaveButtonClick = func;
                }
                /**
                 * Set Before select template button click event in 'load template' popup
                 */
            this.setPopupSendMailButtonClick = function(func) {
                    _emailBuilder.config.onPopupSendMailButtonClick = func;
                }
                /**
                 * Set 'Upload' button click for upload image in 'select image' popup
                 */
            this.setPopupUploadImageButtonClick = function(func) {
                _emailBuilder.config.onPopupUploadImageButtonClick = func;
            }

            /**
             * Set Before clicking 'Remove' button in element settings
             */
            this.setBeforeRowRemoveButtonClick = function(func) {
                    _emailBuilder.config.onBeforeRowRemoveButtonClick = func;
                }
                /**
                 * Set After clicking 'Remove' button in element settings
                 */
            this.setAfterRowRemoveButtonClick = function(func) {
                    _emailBuilder.config.onAfterRowRemoveButtonClick = func;
                }
                /**
                 * Set Before clicking 'Duplicate' button in element settings
                 */
            this.setBeforeRowDuplicateButtonClick = function(func) {
                    _emailBuilder.config.onBeforeRowDuplicateButtonClick = func;
                }
                /**
                 * Set After clicking 'Duplicate' button in element settings
                 */
            this.setAfterRowDuplicateButtonClick = function(func) {
                    _emailBuilder.config.onAfterRowDuplicateButtonClick = func;
                }
                /**
                 * Set Before clicking 'Code editor' button in element settings
                 */
            this.setBeforeRowEditorButtonClick = function(func) {
                    _emailBuilder.config.onBeforeRowEditorButtonClick = func;
                }
                /**
                 * Set After clicking 'Code editor' button in element settings
                 */
            this.setAfterRowEditorButtonClick = function(func) {
                    _emailBuilder.config.onAfterRowEditorButtonClick = func;
                }
                /**
                 * Set Before, show code editor for edit source any elemnt of template
                 */
            this.setBeforeShowingEditorPopup = function(func) {
                    _emailBuilder.config.onBeforeShowingEditorPopup = func;
                }
                /**
                 * Set After page loading event
                 */
            this.setAfterLoad = function(func) {
                    _emailBuilder.config.onAfterLoad = func;
                }
                /**
                 * Get created email template
                 */
            this.getContentHtml = function() {
                return _emailBuilder.getContentHtml();
            }
            return this.each(function() {
                _emailBuilder = new EmailBuilder(this, options);
                _emailBuilder.init();
            });
        };
    });

    jQuery.fn.hasParent = function(e) {
        return (jQuery(this).parents(e).length == 1 ? true : false);
    }

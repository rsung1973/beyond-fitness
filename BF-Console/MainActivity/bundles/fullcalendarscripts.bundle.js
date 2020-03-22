! function (a, b) {
	"object" == typeof exports && "undefined" != typeof module ? module.exports = b() : "function" == typeof define && define.amd ? define(b) : a.moment = b()
}(this, function () {
	"use strict";

	function a() {
		return zb.apply(null, arguments)
	}

	function b(a) {
		return a instanceof Array || "[object Array]" === Object.prototype.toString.call(a)
	}

	function c(a) {
		return null != a && "[object Object]" === Object.prototype.toString.call(a)
	}

	function d(a) {
		if (Object.getOwnPropertyNames) return 0 === Object.getOwnPropertyNames(a).length;
		var b;
		for (b in a)
			if (a.hasOwnProperty(b)) return !1;
		return !0
	}

	function e(a) {
		return void 0 === a
	}

	function f(a) {
		return "number" == typeof a || "[object Number]" === Object.prototype.toString.call(a)
	}

	function g(a) {
		return a instanceof Date || "[object Date]" === Object.prototype.toString.call(a)
	}

	function h(a, b) {
		var c, d = [];
		for (c = 0; c < a.length; ++c) d.push(b(a[c], c));
		return d
	}

	function i(a, b) {
		return Object.prototype.hasOwnProperty.call(a, b)
	}

	function j(a, b) {
		for (var c in b) i(b, c) && (a[c] = b[c]);
		return i(b, "toString") && (a.toString = b.toString), i(b, "valueOf") && (a.valueOf = b.valueOf), a
	}

	function k(a, b, c, d) {
		return Pa(a, b, c, d, !0).utc()
	}

	function l() {
		return {
			empty: !1,
			unusedTokens: [],
			unusedInput: [],
			overflow: -2,
			charsLeftOver: 0,
			nullInput: !1,
			invalidMonth: null,
			invalidFormat: !1,
			userInvalidated: !1,
			iso: !1,
			parsedDateParts: [],
			meridiem: null,
			rfc2822: !1,
			weekdayMismatch: !1
		}
	}

	function m(a) {
		return null == a._pf && (a._pf = l()), a._pf
	}

	function n(a) {
		if (null == a._isValid) {
			var b = m(a),
				c = Ab.call(b.parsedDateParts, function (a) {
					return null != a
				}),
				d = !isNaN(a._d.getTime()) && b.overflow < 0 && !b.empty && !b.invalidMonth && !b.invalidWeekday && !b.weekdayMismatch && !b.nullInput && !b.invalidFormat && !b.userInvalidated && (!b.meridiem || b.meridiem && c);
			if (a._strict && (d = d && 0 === b.charsLeftOver && 0 === b.unusedTokens.length && void 0 === b.bigHour), null != Object.isFrozen && Object.isFrozen(a)) return d;
			a._isValid = d
		}
		return a._isValid
	}

	function o(a) {
		var b = k(NaN);
		return null != a ? j(m(b), a) : m(b).userInvalidated = !0, b
	}

	function p(a, b) {
		var c, d, f;
		if (e(b._isAMomentObject) || (a._isAMomentObject = b._isAMomentObject), e(b._i) || (a._i = b._i), e(b._f) || (a._f = b._f), e(b._l) || (a._l = b._l), e(b._strict) || (a._strict = b._strict), e(b._tzm) || (a._tzm = b._tzm), e(b._isUTC) || (a._isUTC = b._isUTC), e(b._offset) || (a._offset = b._offset), e(b._pf) || (a._pf = m(b)), e(b._locale) || (a._locale = b._locale), Bb.length > 0)
			for (c = 0; c < Bb.length; c++) e(f = b[d = Bb[c]]) || (a[d] = f);
		return a
	}

	function q(b) {
		p(this, b), this._d = new Date(null != b._d ? b._d.getTime() : NaN), this.isValid() || (this._d = new Date(NaN)), !1 === Cb && (Cb = !0, a.updateOffset(this), Cb = !1)
	}

	function r(a) {
		return a instanceof q || null != a && null != a._isAMomentObject
	}

	function s(a) {
		return a < 0 ? Math.ceil(a) || 0 : Math.floor(a)
	}

	function t(a) {
		var b = +a,
			c = 0;
		return 0 !== b && isFinite(b) && (c = s(b)), c
	}

	function u(a, b, c) {
		var d, e = Math.min(a.length, b.length),
			f = Math.abs(a.length - b.length),
			g = 0;
		for (d = 0; d < e; d++)(c && a[d] !== b[d] || !c && t(a[d]) !== t(b[d])) && g++;
		return g + f
	}

	function v(b) {
		!1 === a.suppressDeprecationWarnings && "undefined" != typeof console && console.warn && console.warn("Deprecation warning: " + b)
	}

	function w(b, c) {
		var d = !0;
		return j(function () {
			if (null != a.deprecationHandler && a.deprecationHandler(null, b), d) {
				for (var e, f = [], g = 0; g < arguments.length; g++) {
					if (e = "", "object" == typeof arguments[g]) {
						e += "\n[" + g + "] ";
						for (var h in arguments[0]) e += h + ": " + arguments[0][h] + ", ";
						e = e.slice(0, -2)
					} else e = arguments[g];
					f.push(e)
				}
				v(b + "\nArguments: " + Array.prototype.slice.call(f).join("") + "\n" + (new Error).stack), d = !1
			}
			return c.apply(this, arguments)
		}, c)
	}

	function x(b, c) {
		null != a.deprecationHandler && a.deprecationHandler(b, c), Db[b] || (v(c), Db[b] = !0)
	}

	function y(a) {
		return a instanceof Function || "[object Function]" === Object.prototype.toString.call(a)
	}

	function z(a, b) {
		var d, e = j({}, a);
		for (d in b) i(b, d) && (c(a[d]) && c(b[d]) ? (e[d] = {}, j(e[d], a[d]), j(e[d], b[d])) : null != b[d] ? e[d] = b[d] : delete e[d]);
		for (d in a) i(a, d) && !i(b, d) && c(a[d]) && (e[d] = j({}, e[d]));
		return e
	}

	function A(a) {
		null != a && this.set(a)
	}

	function B(a, b) {
		var c = a.toLowerCase();
		Jb[c] = Jb[c + "s"] = Jb[b] = a
	}

	function C(a) {
		return "string" == typeof a ? Jb[a] || Jb[a.toLowerCase()] : void 0
	}

	function D(a) {
		var b, c, d = {};
		for (c in a) i(a, c) && (b = C(c)) && (d[b] = a[c]);
		return d
	}

	function E(a, b) {
		Kb[a] = b
	}

	function F(a) {
		var b = [];
		for (var c in a) b.push({
			unit: c,
			priority: Kb[c]
		});
		return b.sort(function (a, b) {
			return a.priority - b.priority
		}), b
	}

	function G(a, b, c) {
		var d = "" + Math.abs(a),
			e = b - d.length;
		return (a >= 0 ? c ? "+" : "" : "-") + Math.pow(10, Math.max(0, e)).toString().substr(1) + d
	}

	function H(a, b, c, d) {
		var e = d;
		"string" == typeof d && (e = function () {
			return this[d]()
		}), a && (Ob[a] = e), b && (Ob[b[0]] = function () {
			return G(e.apply(this, arguments), b[1], b[2])
		}), c && (Ob[c] = function () {
			return this.localeData().ordinal(e.apply(this, arguments), a)
		})
	}

	function I(a) {
		return a.match(/\[[\s\S]/) ? a.replace(/^\[|\]$/g, "") : a.replace(/\\/g, "")
	}

	function J(a) {
		var b, c, d = a.match(Lb);
		for (b = 0, c = d.length; b < c; b++) Ob[d[b]] ? d[b] = Ob[d[b]] : d[b] = I(d[b]);
		return function (b) {
			var e, f = "";
			for (e = 0; e < c; e++) f += y(d[e]) ? d[e].call(b, a) : d[e];
			return f
		}
	}

	function K(a, b) {
		return a.isValid() ? (b = L(b, a.localeData()), Nb[b] = Nb[b] || J(b), Nb[b](a)) : a.localeData().invalidDate()
	}

	function L(a, b) {
		var c = 5;
		for (Mb.lastIndex = 0; c >= 0 && Mb.test(a);) a = a.replace(Mb, function (a) {
			return b.longDateFormat(a) || a
		}), Mb.lastIndex = 0, c -= 1;
		return a
	}

	function M(a, b, c) {
		ec[a] = y(b) ? b : function (a, d) {
			return a && c ? c : b
		}
	}

	function N(a, b) {
		return i(ec, a) ? ec[a](b._strict, b._locale) : new RegExp(O(a))
	}

	function O(a) {
		return P(a.replace("\\", "").replace(/\\(\[)|\\(\])|\[([^\]\[]*)\]|\\(.)/g, function (a, b, c, d, e) {
			return b || c || d || e
		}))
	}

	function P(a) {
		return a.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&")
	}

	function Q(a, b) {
		var c, d = b;
		for ("string" == typeof a && (a = [a]), f(b) && (d = function (a, c) {
				c[b] = t(a)
			}), c = 0; c < a.length; c++) fc[a[c]] = d
	}

	function R(a, b) {
		Q(a, function (a, c, d, e) {
			d._w = d._w || {}, b(a, d._w, d, e)
		})
	}

	function S(a, b, c) {
		null != b && i(fc, a) && fc[a](b, c._a, c, a)
	}

	function T(a) {
		return U(a) ? 366 : 365
	}

	function U(a) {
		return a % 4 == 0 && a % 100 != 0 || a % 400 == 0
	}

	function V(b, c) {
		return function (d) {
			return null != d ? (X(this, b, d), a.updateOffset(this, c), this) : W(this, b)
		}
	}

	function W(a, b) {
		return a.isValid() ? a._d["get" + (a._isUTC ? "UTC" : "") + b]() : NaN
	}

	function X(a, b, c) {
		a.isValid() && !isNaN(c) && ("FullYear" === b && U(a.year()) && 1 === a.month() && 29 === a.date() ? a._d["set" + (a._isUTC ? "UTC" : "") + b](c, a.month(), Z(c, a.month())) : a._d["set" + (a._isUTC ? "UTC" : "") + b](c))
	}

	function Y(a, b) {
		return (a % b + b) % b
	}

	function Z(a, b) {
		if (isNaN(a) || isNaN(b)) return NaN;
		var c = Y(b, 12);
		return a += (b - c) / 12, 1 === c ? U(a) ? 29 : 28 : 31 - c % 7 % 2
	}

	function $(a, b, c) {
		var d, e, f, g = a.toLocaleLowerCase();
		if (!this._monthsParse)
			for (this._monthsParse = [], this._longMonthsParse = [], this._shortMonthsParse = [], d = 0; d < 12; ++d) f = k([2e3, d]), this._shortMonthsParse[d] = this.monthsShort(f, "").toLocaleLowerCase(), this._longMonthsParse[d] = this.months(f, "").toLocaleLowerCase();
		return c ? "MMM" === b ? -1 !== (e = pc.call(this._shortMonthsParse, g)) ? e : null : -1 !== (e = pc.call(this._longMonthsParse, g)) ? e : null : "MMM" === b ? -1 !== (e = pc.call(this._shortMonthsParse, g)) ? e : -1 !== (e = pc.call(this._longMonthsParse, g)) ? e : null : -1 !== (e = pc.call(this._longMonthsParse, g)) ? e : -1 !== (e = pc.call(this._shortMonthsParse, g)) ? e : null
	}

	function _(a, b) {
		var c;
		if (!a.isValid()) return a;
		if ("string" == typeof b)
			if (/^\d+$/.test(b)) b = t(b);
			else if (b = a.localeData().monthsParse(b), !f(b)) return a;
		return c = Math.min(a.date(), Z(a.year(), b)), a._d["set" + (a._isUTC ? "UTC" : "") + "Month"](b, c), a
	}

	function aa(b) {
		return null != b ? (_(this, b), a.updateOffset(this, !0), this) : W(this, "Month")
	}

	function ba() {
		function a(a, b) {
			return b.length - a.length
		}
		var b, c, d = [],
			e = [],
			f = [];
		for (b = 0; b < 12; b++) c = k([2e3, b]), d.push(this.monthsShort(c, "")), e.push(this.months(c, "")), f.push(this.months(c, "")), f.push(this.monthsShort(c, ""));
		for (d.sort(a), e.sort(a), f.sort(a), b = 0; b < 12; b++) d[b] = P(d[b]), e[b] = P(e[b]);
		for (b = 0; b < 24; b++) f[b] = P(f[b]);
		this._monthsRegex = new RegExp("^(" + f.join("|") + ")", "i"), this._monthsShortRegex = this._monthsRegex, this._monthsStrictRegex = new RegExp("^(" + e.join("|") + ")", "i"), this._monthsShortStrictRegex = new RegExp("^(" + d.join("|") + ")", "i")
	}

	function ca(a, b, c, d, e, f, g) {
		var h = new Date(a, b, c, d, e, f, g);
		return a < 100 && a >= 0 && isFinite(h.getFullYear()) && h.setFullYear(a), h
	}

	function da(a) {
		var b = new Date(Date.UTC.apply(null, arguments));
		return a < 100 && a >= 0 && isFinite(b.getUTCFullYear()) && b.setUTCFullYear(a), b
	}

	function ea(a, b, c) {
		var d = 7 + b - c;
		return -(7 + da(a, 0, d).getUTCDay() - b) % 7 + d - 1
	}

	function fa(a, b, c, d, e) {
		var f, g, h = 1 + 7 * (b - 1) + (7 + c - d) % 7 + ea(a, d, e);
		return h <= 0 ? g = T(f = a - 1) + h : h > T(a) ? (f = a + 1, g = h - T(a)) : (f = a, g = h), {
			year: f,
			dayOfYear: g
		}
	}

	function ga(a, b, c) {
		var d, e, f = ea(a.year(), b, c),
			g = Math.floor((a.dayOfYear() - f - 1) / 7) + 1;
		return g < 1 ? d = g + ha(e = a.year() - 1, b, c) : g > ha(a.year(), b, c) ? (d = g - ha(a.year(), b, c), e = a.year() + 1) : (e = a.year(), d = g), {
			week: d,
			year: e
		}
	}

	function ha(a, b, c) {
		var d = ea(a, b, c),
			e = ea(a + 1, b, c);
		return (T(a) - d + e) / 7
	}

	function ia(a, b) {
		return "string" != typeof a ? a : isNaN(a) ? "number" == typeof (a = b.weekdaysParse(a)) ? a : null : parseInt(a, 10)
	}

	function ja(a, b) {
		return "string" == typeof a ? b.weekdaysParse(a) % 7 || 7 : isNaN(a) ? null : a
	}

	function ka(a, b, c) {
		var d, e, f, g = a.toLocaleLowerCase();
		if (!this._weekdaysParse)
			for (this._weekdaysParse = [], this._shortWeekdaysParse = [], this._minWeekdaysParse = [], d = 0; d < 7; ++d) f = k([2e3, 1]).day(d), this._minWeekdaysParse[d] = this.weekdaysMin(f, "").toLocaleLowerCase(), this._shortWeekdaysParse[d] = this.weekdaysShort(f, "").toLocaleLowerCase(), this._weekdaysParse[d] = this.weekdays(f, "").toLocaleLowerCase();
		return c ? "dddd" === b ? -1 !== (e = pc.call(this._weekdaysParse, g)) ? e : null : "ddd" === b ? -1 !== (e = pc.call(this._shortWeekdaysParse, g)) ? e : null : -1 !== (e = pc.call(this._minWeekdaysParse, g)) ? e : null : "dddd" === b ? -1 !== (e = pc.call(this._weekdaysParse, g)) ? e : -1 !== (e = pc.call(this._shortWeekdaysParse, g)) ? e : -1 !== (e = pc.call(this._minWeekdaysParse, g)) ? e : null : "ddd" === b ? -1 !== (e = pc.call(this._shortWeekdaysParse, g)) ? e : -1 !== (e = pc.call(this._weekdaysParse, g)) ? e : -1 !== (e = pc.call(this._minWeekdaysParse, g)) ? e : null : -1 !== (e = pc.call(this._minWeekdaysParse, g)) ? e : -1 !== (e = pc.call(this._weekdaysParse, g)) ? e : -1 !== (e = pc.call(this._shortWeekdaysParse, g)) ? e : null
	}

	function la() {
		function a(a, b) {
			return b.length - a.length
		}
		var b, c, d, e, f, g = [],
			h = [],
			i = [],
			j = [];
		for (b = 0; b < 7; b++) c = k([2e3, 1]).day(b), d = this.weekdaysMin(c, ""), e = this.weekdaysShort(c, ""), f = this.weekdays(c, ""), g.push(d), h.push(e), i.push(f), j.push(d), j.push(e), j.push(f);
		for (g.sort(a), h.sort(a), i.sort(a), j.sort(a), b = 0; b < 7; b++) h[b] = P(h[b]), i[b] = P(i[b]), j[b] = P(j[b]);
		this._weekdaysRegex = new RegExp("^(" + j.join("|") + ")", "i"), this._weekdaysShortRegex = this._weekdaysRegex, this._weekdaysMinRegex = this._weekdaysRegex, this._weekdaysStrictRegex = new RegExp("^(" + i.join("|") + ")", "i"), this._weekdaysShortStrictRegex = new RegExp("^(" + h.join("|") + ")", "i"), this._weekdaysMinStrictRegex = new RegExp("^(" + g.join("|") + ")", "i")
	}

	function ma() {
		return this.hours() % 12 || 12
	}

	function na(a, b) {
		H(a, 0, 0, function () {
			return this.localeData().meridiem(this.hours(), this.minutes(), b)
		})
	}

	function oa(a, b) {
		return b._meridiemParse
	}

	function pa(a) {
		return a ? a.toLowerCase().replace("_", "-") : a
	}

	function qa(a) {
		for (var b, c, d, e, f = 0; f < a.length;) {
			for (b = (e = pa(a[f]).split("-")).length, c = (c = pa(a[f + 1])) ? c.split("-") : null; b > 0;) {
				if (d = ra(e.slice(0, b).join("-"))) return d;
				if (c && c.length >= b && u(e, c, !0) >= b - 1) break;
				b--
			}
			f++
		}
		return null
	}

	function ra(a) {
		var b = null;
		if (!Hc[a] && "undefined" != typeof module && module && module.exports) try {
			b = Dc._abbr, require("./locale/" + a), sa(b)
		} catch (a) {}
		return Hc[a]
	}

	function sa(a, b) {
		var c;
		return a && (c = e(b) ? ua(a) : ta(a, b)) && (Dc = c), Dc._abbr
	}

	function ta(a, b) {
		if (null !== b) {
			var c = Gc;
			if (b.abbr = a, null != Hc[a]) x("defineLocaleOverride", "use moment.updateLocale(localeName, config) to change an existing locale. moment.defineLocale(localeName, config) should only be used for creating a new locale See http://momentjs.com/guides/#/warnings/define-locale/ for more info."), c = Hc[a]._config;
			else if (null != b.parentLocale) {
				if (null == Hc[b.parentLocale]) return Ic[b.parentLocale] || (Ic[b.parentLocale] = []), Ic[b.parentLocale].push({
					name: a,
					config: b
				}), null;
				c = Hc[b.parentLocale]._config
			}
			return Hc[a] = new A(z(c, b)), Ic[a] && Ic[a].forEach(function (a) {
				ta(a.name, a.config)
			}), sa(a), Hc[a]
		}
		return delete Hc[a], null
	}

	function ua(a) {
		var c;
		if (a && a._locale && a._locale._abbr && (a = a._locale._abbr), !a) return Dc;
		if (!b(a)) {
			if (c = ra(a)) return c;
			a = [a]
		}
		return qa(a)
	}

	function va(a) {
		var b, c = a._a;
		return c && -2 === m(a).overflow && (b = c[hc] < 0 || c[hc] > 11 ? hc : c[ic] < 1 || c[ic] > Z(c[gc], c[hc]) ? ic : c[jc] < 0 || c[jc] > 24 || 24 === c[jc] && (0 !== c[kc] || 0 !== c[lc] || 0 !== c[mc]) ? jc : c[kc] < 0 || c[kc] > 59 ? kc : c[lc] < 0 || c[lc] > 59 ? lc : c[mc] < 0 || c[mc] > 999 ? mc : -1, m(a)._overflowDayOfYear && (b < gc || b > ic) && (b = ic), m(a)._overflowWeeks && -1 === b && (b = nc), m(a)._overflowWeekday && -1 === b && (b = oc), m(a).overflow = b), a
	}

	function wa(a, b, c) {
		return null != a ? a : null != b ? b : c
	}

	function xa(b) {
		var c = new Date(a.now());
		return b._useUTC ? [c.getUTCFullYear(), c.getUTCMonth(), c.getUTCDate()] : [c.getFullYear(), c.getMonth(), c.getDate()]
	}

	function ya(a) {
		var b, c, d, e, f = [];
		if (!a._d) {
			for (d = xa(a), a._w && null == a._a[ic] && null == a._a[hc] && za(a), null != a._dayOfYear && (e = wa(a._a[gc], d[gc]), (a._dayOfYear > T(e) || 0 === a._dayOfYear) && (m(a)._overflowDayOfYear = !0), c = da(e, 0, a._dayOfYear), a._a[hc] = c.getUTCMonth(), a._a[ic] = c.getUTCDate()), b = 0; b < 3 && null == a._a[b]; ++b) a._a[b] = f[b] = d[b];
			for (; b < 7; b++) a._a[b] = f[b] = null == a._a[b] ? 2 === b ? 1 : 0 : a._a[b];
			24 === a._a[jc] && 0 === a._a[kc] && 0 === a._a[lc] && 0 === a._a[mc] && (a._nextDay = !0, a._a[jc] = 0), a._d = (a._useUTC ? da : ca).apply(null, f), null != a._tzm && a._d.setUTCMinutes(a._d.getUTCMinutes() - a._tzm), a._nextDay && (a._a[jc] = 24), a._w && void 0 !== a._w.d && a._w.d !== a._d.getDay() && (m(a).weekdayMismatch = !0)
		}
	}

	function za(a) {
		var b, c, d, e, f, g, h, i;
		if (null != (b = a._w).GG || null != b.W || null != b.E) f = 1, g = 4, c = wa(b.GG, a._a[gc], ga(Qa(), 1, 4).year), d = wa(b.W, 1), ((e = wa(b.E, 1)) < 1 || e > 7) && (i = !0);
		else {
			f = a._locale._week.dow, g = a._locale._week.doy;
			var j = ga(Qa(), f, g);
			c = wa(b.gg, a._a[gc], j.year), d = wa(b.w, j.week), null != b.d ? ((e = b.d) < 0 || e > 6) && (i = !0) : null != b.e ? (e = b.e + f, (b.e < 0 || b.e > 6) && (i = !0)) : e = f
		}
		d < 1 || d > ha(c, f, g) ? m(a)._overflowWeeks = !0 : null != i ? m(a)._overflowWeekday = !0 : (h = fa(c, d, e, f, g), a._a[gc] = h.year, a._dayOfYear = h.dayOfYear)
	}

	function Aa(a) {
		var b, c, d, e, f, g, h = a._i,
			i = Jc.exec(h) || Kc.exec(h);
		if (i) {
			for (m(a).iso = !0, b = 0, c = Mc.length; b < c; b++)
				if (Mc[b][1].exec(i[1])) {
					e = Mc[b][0], d = !1 !== Mc[b][2];
					break
				}
			if (null == e) return void(a._isValid = !1);
			if (i[3]) {
				for (b = 0, c = Nc.length; b < c; b++)
					if (Nc[b][1].exec(i[3])) {
						f = (i[2] || " ") + Nc[b][0];
						break
					}
				if (null == f) return void(a._isValid = !1)
			}
			if (!d && null != f) return void(a._isValid = !1);
			if (i[4]) {
				if (!Lc.exec(i[4])) return void(a._isValid = !1);
				g = "Z"
			}
			a._f = e + (f || "") + (g || ""), Ia(a)
		} else a._isValid = !1
	}

	function Ba(a, b, c, d, e, f) {
		var g = [Ca(a), tc.indexOf(b), parseInt(c, 10), parseInt(d, 10), parseInt(e, 10)];
		return f && g.push(parseInt(f, 10)), g
	}

	function Ca(a) {
		var b = parseInt(a, 10);
		return b <= 49 ? 2e3 + b : b <= 999 ? 1900 + b : b
	}

	function Da(a) {
		return a.replace(/\([^)]*\)|[\n\t]/g, " ").replace(/(\s\s+)/g, " ").trim()
	}

	function Ea(a, b, c) {
		return !a || yc.indexOf(a) === new Date(b[0], b[1], b[2]).getDay() || (m(c).weekdayMismatch = !0, c._isValid = !1, !1)
	}

	function Fa(a, b, c) {
		if (a) return Qc[a];
		if (b) return 0;
		var d = parseInt(c, 10),
			e = d % 100;
		return (d - e) / 100 * 60 + e
	}

	function Ga(a) {
		var b = Pc.exec(Da(a._i));
		if (b) {
			var c = Ba(b[4], b[3], b[2], b[5], b[6], b[7]);
			if (!Ea(b[1], c, a)) return;
			a._a = c, a._tzm = Fa(b[8], b[9], b[10]), a._d = da.apply(null, a._a), a._d.setUTCMinutes(a._d.getUTCMinutes() - a._tzm), m(a).rfc2822 = !0
		} else a._isValid = !1
	}

	function Ha(b) {
		var c = Oc.exec(b._i);
		null === c ? (Aa(b), !1 === b._isValid && (delete b._isValid, Ga(b), !1 === b._isValid && (delete b._isValid, a.createFromInputFallback(b)))) : b._d = new Date(+c[1])
	}

	function Ia(b) {
		if (b._f !== a.ISO_8601)
			if (b._f !== a.RFC_2822) {
				b._a = [], m(b).empty = !0;
				var c, d, e, f, g, h = "" + b._i,
					i = h.length,
					j = 0;
				for (e = L(b._f, b._locale).match(Lb) || [], c = 0; c < e.length; c++) f = e[c], (d = (h.match(N(f, b)) || [])[0]) && ((g = h.substr(0, h.indexOf(d))).length > 0 && m(b).unusedInput.push(g), h = h.slice(h.indexOf(d) + d.length), j += d.length), Ob[f] ? (d ? m(b).empty = !1 : m(b).unusedTokens.push(f), S(f, d, b)) : b._strict && !d && m(b).unusedTokens.push(f);
				m(b).charsLeftOver = i - j, h.length > 0 && m(b).unusedInput.push(h), b._a[jc] <= 12 && !0 === m(b).bigHour && b._a[jc] > 0 && (m(b).bigHour = void 0), m(b).parsedDateParts = b._a.slice(0), m(b).meridiem = b._meridiem, b._a[jc] = Ja(b._locale, b._a[jc], b._meridiem), ya(b), va(b)
			} else Ga(b);
		else Aa(b)
	}

	function Ja(a, b, c) {
		var d;
		return null == c ? b : null != a.meridiemHour ? a.meridiemHour(b, c) : null != a.isPM ? ((d = a.isPM(c)) && b < 12 && (b += 12), d || 12 !== b || (b = 0), b) : b
	}

	function Ka(a) {
		var b, c, d, e, f;
		if (0 === a._f.length) return m(a).invalidFormat = !0, void(a._d = new Date(NaN));
		for (e = 0; e < a._f.length; e++) f = 0, b = p({}, a), null != a._useUTC && (b._useUTC = a._useUTC), b._f = a._f[e], Ia(b), n(b) && (f += m(b).charsLeftOver, f += 10 * m(b).unusedTokens.length, m(b).score = f, (null == d || f < d) && (d = f, c = b));
		j(a, c || b)
	}

	function La(a) {
		if (!a._d) {
			var b = D(a._i);
			a._a = h([b.year, b.month, b.day || b.date, b.hour, b.minute, b.second, b.millisecond], function (a) {
				return a && parseInt(a, 10)
			}), ya(a)
		}
	}

	function Ma(a) {
		var b = new q(va(Na(a)));
		return b._nextDay && (b.add(1, "d"), b._nextDay = void 0), b
	}

	function Na(a) {
		var c = a._i,
			d = a._f;
		return a._locale = a._locale || ua(a._l), null === c || void 0 === d && "" === c ? o({
			nullInput: !0
		}) : ("string" == typeof c && (a._i = c = a._locale.preparse(c)), r(c) ? new q(va(c)) : (g(c) ? a._d = c : b(d) ? Ka(a) : d ? Ia(a) : Oa(a), n(a) || (a._d = null), a))
	}

	function Oa(d) {
		var i = d._i;
		e(i) ? d._d = new Date(a.now()) : g(i) ? d._d = new Date(i.valueOf()) : "string" == typeof i ? Ha(d) : b(i) ? (d._a = h(i.slice(0), function (a) {
			return parseInt(a, 10)
		}), ya(d)) : c(i) ? La(d) : f(i) ? d._d = new Date(i) : a.createFromInputFallback(d)
	}

	function Pa(a, e, f, g, h) {
		var i = {};
		return !0 !== f && !1 !== f || (g = f, f = void 0), (c(a) && d(a) || b(a) && 0 === a.length) && (a = void 0), i._isAMomentObject = !0, i._useUTC = i._isUTC = h, i._l = f, i._i = a, i._f = e, i._strict = g, Ma(i)
	}

	function Qa(a, b, c, d) {
		return Pa(a, b, c, d, !1)
	}

	function Ra(a, c) {
		var d, e;
		if (1 === c.length && b(c[0]) && (c = c[0]), !c.length) return Qa();
		for (d = c[0], e = 1; e < c.length; ++e) c[e].isValid() && !c[e][a](d) || (d = c[e]);
		return d
	}

	function Sa(a) {
		for (var b in a)
			if (-1 === pc.call(Tc, b) || null != a[b] && isNaN(a[b])) return !1;
		for (var c = !1, d = 0; d < Tc.length; ++d)
			if (a[Tc[d]]) {
				if (c) return !1;
				parseFloat(a[Tc[d]]) !== t(a[Tc[d]]) && (c = !0)
			}
		return !0
	}

	function Ta(a) {
		var b = D(a),
			c = b.year || 0,
			d = b.quarter || 0,
			e = b.month || 0,
			f = b.week || 0,
			g = b.day || 0,
			h = b.hour || 0,
			i = b.minute || 0,
			j = b.second || 0,
			k = b.millisecond || 0;
		this._isValid = Sa(b), this._milliseconds = +k + 1e3 * j + 6e4 * i + 1e3 * h * 60 * 60, this._days = +g + 7 * f, this._months = +e + 3 * d + 12 * c, this._data = {}, this._locale = ua(), this._bubble()
	}

	function Ua(a) {
		return a instanceof Ta
	}

	function Va(a) {
		return a < 0 ? -1 * Math.round(-1 * a) : Math.round(a)
	}

	function Wa(a, b) {
		H(a, 0, 0, function () {
			var a = this.utcOffset(),
				c = "+";
			return a < 0 && (a = -a, c = "-"), c + G(~~(a / 60), 2) + b + G(~~a % 60, 2)
		})
	}

	function Xa(a, b) {
		var c = (b || "").match(a);
		if (null === c) return null;
		var d = ((c[c.length - 1] || []) + "").match(Uc) || ["-", 0, 0],
			e = 60 * d[1] + t(d[2]);
		return 0 === e ? 0 : "+" === d[0] ? e : -e
	}

	function Ya(b, c) {
		var d, e;
		return c._isUTC ? (d = c.clone(), e = (r(b) || g(b) ? b.valueOf() : Qa(b).valueOf()) - d.valueOf(), d._d.setTime(d._d.valueOf() + e), a.updateOffset(d, !1), d) : Qa(b).local()
	}

	function Za(a) {
		return 15 * -Math.round(a._d.getTimezoneOffset() / 15)
	}

	function $a() {
		return !!this.isValid() && this._isUTC && 0 === this._offset
	}

	function _a(a, b) {
		var c, d, e, g = a,
			h = null;
		return Ua(a) ? g = {
			ms: a._milliseconds,
			d: a._days,
			M: a._months
		} : f(a) ? (g = {}, b ? g[b] = a : g.milliseconds = a) : (h = Vc.exec(a)) ? (c = "-" === h[1] ? -1 : 1, g = {
			y: 0,
			d: t(h[ic]) * c,
			h: t(h[jc]) * c,
			m: t(h[kc]) * c,
			s: t(h[lc]) * c,
			ms: t(Va(1e3 * h[mc])) * c
		}) : (h = Wc.exec(a)) ? (c = "-" === h[1] ? -1 : (h[1], 1), g = {
			y: ab(h[2], c),
			M: ab(h[3], c),
			w: ab(h[4], c),
			d: ab(h[5], c),
			h: ab(h[6], c),
			m: ab(h[7], c),
			s: ab(h[8], c)
		}) : null == g ? g = {} : "object" == typeof g && ("from" in g || "to" in g) && (e = cb(Qa(g.from), Qa(g.to)), (g = {}).ms = e.milliseconds, g.M = e.months), d = new Ta(g), Ua(a) && i(a, "_locale") && (d._locale = a._locale), d
	}

	function ab(a, b) {
		var c = a && parseFloat(a.replace(",", "."));
		return (isNaN(c) ? 0 : c) * b
	}

	function bb(a, b) {
		var c = {
			milliseconds: 0,
			months: 0
		};
		return c.months = b.month() - a.month() + 12 * (b.year() - a.year()), a.clone().add(c.months, "M").isAfter(b) && --c.months, c.milliseconds = +b - +a.clone().add(c.months, "M"), c
	}

	function cb(a, b) {
		var c;
		return a.isValid() && b.isValid() ? (b = Ya(b, a), a.isBefore(b) ? c = bb(a, b) : ((c = bb(b, a)).milliseconds = -c.milliseconds, c.months = -c.months), c) : {
			milliseconds: 0,
			months: 0
		}
	}

	function db(a, b) {
		return function (c, d) {
			var e, f;
			return null === d || isNaN(+d) || (x(b, "moment()." + b + "(period, number) is deprecated. Please use moment()." + b + "(number, period). See http://momentjs.com/guides/#/warnings/add-inverted-param/ for more info."), f = c, c = d, d = f), c = "string" == typeof c ? +c : c, e = _a(c, d), eb(this, e, a), this
		}
	}

	function eb(b, c, d, e) {
		var f = c._milliseconds,
			g = Va(c._days),
			h = Va(c._months);
		b.isValid() && (e = null == e || e, h && _(b, W(b, "Month") + h * d), g && X(b, "Date", W(b, "Date") + g * d), f && b._d.setTime(b._d.valueOf() + f * d), e && a.updateOffset(b, g || h))
	}

	function fb(a, b) {
		var c, d = 12 * (b.year() - a.year()) + (b.month() - a.month()),
			e = a.clone().add(d, "months");
		return c = b - e < 0 ? (b - e) / (e - a.clone().add(d - 1, "months")) : (b - e) / (a.clone().add(d + 1, "months") - e), -(d + c) || 0
	}

	function gb(a) {
		var b;
		return void 0 === a ? this._locale._abbr : (null != (b = ua(a)) && (this._locale = b), this)
	}

	function hb() {
		return this._locale
	}

	function ib(a, b) {
		H(0, [a, a.length], 0, b)
	}

	function jb(a, b, c, d, e) {
		var f;
		return null == a ? ga(this, d, e).year : (f = ha(a, d, e), b > f && (b = f), kb.call(this, a, b, c, d, e))
	}

	function kb(a, b, c, d, e) {
		var f = fa(a, b, c, d, e),
			g = da(f.year, 0, f.dayOfYear);
		return this.year(g.getUTCFullYear()), this.month(g.getUTCMonth()), this.date(g.getUTCDate()), this
	}

	function lb(a) {
		return a
	}

	function mb(a, b, c, d) {
		var e = ua(),
			f = k().set(d, b);
		return e[c](f, a)
	}

	function nb(a, b, c) {
		if (f(a) && (b = a, a = void 0), a = a || "", null != b) return mb(a, b, c, "month");
		var d, e = [];
		for (d = 0; d < 12; d++) e[d] = mb(a, d, c, "month");
		return e
	}

	function ob(a, b, c, d) {
		"boolean" == typeof a ? (f(b) && (c = b, b = void 0), b = b || "") : (c = b = a, a = !1, f(b) && (c = b, b = void 0), b = b || "");
		var e = ua(),
			g = a ? e._week.dow : 0;
		if (null != c) return mb(b, (c + g) % 7, d, "day");
		var h, i = [];
		for (h = 0; h < 7; h++) i[h] = mb(b, (h + g) % 7, d, "day");
		return i
	}

	function pb(a, b, c, d) {
		var e = _a(b, c);
		return a._milliseconds += d * e._milliseconds, a._days += d * e._days, a._months += d * e._months, a._bubble()
	}

	function qb(a) {
		return a < 0 ? Math.floor(a) : Math.ceil(a)
	}

	function rb(a) {
		return 4800 * a / 146097
	}

	function sb(a) {
		return 146097 * a / 4800
	}

	function tb(a) {
		return function () {
			return this.as(a)
		}
	}

	function ub(a) {
		return function () {
			return this.isValid() ? this._data[a] : NaN
		}
	}

	function vb(a, b, c, d, e) {
		return e.relativeTime(b || 1, !!c, a, d)
	}

	function wb(a, b, c) {
		var d = _a(a).abs(),
			e = vd(d.as("s")),
			f = vd(d.as("m")),
			g = vd(d.as("h")),
			h = vd(d.as("d")),
			i = vd(d.as("M")),
			j = vd(d.as("y")),
			k = e <= wd.ss && ["s", e] || e < wd.s && ["ss", e] || f <= 1 && ["m"] || f < wd.m && ["mm", f] || g <= 1 && ["h"] || g < wd.h && ["hh", g] || h <= 1 && ["d"] || h < wd.d && ["dd", h] || i <= 1 && ["M"] || i < wd.M && ["MM", i] || j <= 1 && ["y"] || ["yy", j];
		return k[2] = b, k[3] = +a > 0, k[4] = c, vb.apply(null, k)
	}

	function xb(a) {
		return (a > 0) - (a < 0) || +a
	}

	function yb() {
		if (!this.isValid()) return this.localeData().invalidDate();
		var a, b, c, d = xd(this._milliseconds) / 1e3,
			e = xd(this._days),
			f = xd(this._months);
		b = s((a = s(d / 60)) / 60), d %= 60, a %= 60;
		var g = c = s(f / 12),
			h = f %= 12,
			i = e,
			j = b,
			k = a,
			l = d ? d.toFixed(3).replace(/\.?0+$/, "") : "",
			m = this.asSeconds();
		if (!m) return "P0D";
		var n = m < 0 ? "-" : "",
			o = xb(this._months) !== xb(m) ? "-" : "",
			p = xb(this._days) !== xb(m) ? "-" : "",
			q = xb(this._milliseconds) !== xb(m) ? "-" : "";
		return n + "P" + (g ? o + g + "Y" : "") + (h ? o + h + "M" : "") + (i ? p + i + "D" : "") + (j || k || l ? "T" : "") + (j ? q + j + "H" : "") + (k ? q + k + "M" : "") + (l ? q + l + "S" : "")
	}
	var zb, Ab;
	Ab = Array.prototype.some ? Array.prototype.some : function (a) {
		for (var b = Object(this), c = b.length >>> 0, d = 0; d < c; d++)
			if (d in b && a.call(this, b[d], d, b)) return !0;
		return !1
	};
	var Bb = a.momentProperties = [],
		Cb = !1,
		Db = {};
	a.suppressDeprecationWarnings = !1, a.deprecationHandler = null;
	var Eb;
	Eb = Object.keys ? Object.keys : function (a) {
		var b, c = [];
		for (b in a) i(a, b) && c.push(b);
		return c
	};
	var Fb = {
			sameDay: "[Today at] LT",
			nextDay: "[Tomorrow at] LT",
			nextWeek: "dddd [at] LT",
			lastDay: "[Yesterday at] LT",
			lastWeek: "[Last] dddd [at] LT",
			sameElse: "L"
		},
		Gb = {
			LTS: "h:mm:ss A",
			LT: "h:mm A",
			L: "MM/DD/YYYY",
			LL: "MMMM D, YYYY",
			LLL: "MMMM D, YYYY h:mm A",
			LLLL: "dddd, MMMM D, YYYY h:mm A"
		},
		Hb = /\d{1,2}/,
		Ib = {
			future: "in %s",
			past: "%s ago",
			s: "a few seconds",
			ss: "%d seconds",
			m: "a minute",
			mm: "%d minutes",
			h: "an hour",
			hh: "%d hours",
			d: "a day",
			dd: "%d days",
			M: "a month",
			MM: "%d months",
			y: "a year",
			yy: "%d years"
		},
		Jb = {},
		Kb = {},
		Lb = /(\[[^\[]*\])|(\\)?([Hh]mm(ss)?|Mo|MM?M?M?|Do|DDDo|DD?D?D?|ddd?d?|do?|w[o|w]?|W[o|W]?|Qo?|YYYYYY|YYYYY|YYYY|YY|gg(ggg?)?|GG(GGG?)?|e|E|a|A|hh?|HH?|kk?|mm?|ss?|S{1,9}|x|X|zz?|ZZ?|.)/g,
		Mb = /(\[[^\[]*\])|(\\)?(LTS|LT|LL?L?L?|l{1,4})/g,
		Nb = {},
		Ob = {},
		Pb = /\d/,
		Qb = /\d\d/,
		Rb = /\d{3}/,
		Sb = /\d{4}/,
		Tb = /[+-]?\d{6}/,
		Ub = /\d\d?/,
		Vb = /\d\d\d\d?/,
		Wb = /\d\d\d\d\d\d?/,
		Xb = /\d{1,3}/,
		Yb = /\d{1,4}/,
		Zb = /[+-]?\d{1,6}/,
		$b = /\d+/,
		_b = /[+-]?\d+/,
		ac = /Z|[+-]\d\d:?\d\d/gi,
		bc = /Z|[+-]\d\d(?::?\d\d)?/gi,
		cc = /[+-]?\d+(\.\d{1,3})?/,
		dc = /[0-9]*['a-z\u00A0-\u05FF\u0700-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+|[\u0600-\u06FF\/]+(\s*?[\u0600-\u06FF]+){1,2}/i,
		ec = {},
		fc = {},
		gc = 0,
		hc = 1,
		ic = 2,
		jc = 3,
		kc = 4,
		lc = 5,
		mc = 6,
		nc = 7,
		oc = 8;
	H("Y", 0, 0, function () {
		var a = this.year();
		return a <= 9999 ? "" + a : "+" + a
	}), H(0, ["YY", 2], 0, function () {
		return this.year() % 100
	}), H(0, ["YYYY", 4], 0, "year"), H(0, ["YYYYY", 5], 0, "year"), H(0, ["YYYYYY", 6, !0], 0, "year"), B("year", "y"), E("year", 1), M("Y", _b), M("YY", Ub, Qb), M("YYYY", Yb, Sb), M("YYYYY", Zb, Tb), M("YYYYYY", Zb, Tb), Q(["YYYYY", "YYYYYY"], gc), Q("YYYY", function (b, c) {
		c[gc] = 2 === b.length ? a.parseTwoDigitYear(b) : t(b)
	}), Q("YY", function (b, c) {
		c[gc] = a.parseTwoDigitYear(b)
	}), Q("Y", function (a, b) {
		b[gc] = parseInt(a, 10)
	}), a.parseTwoDigitYear = function (a) {
		return t(a) + (t(a) > 68 ? 1900 : 2e3)
	};
	var pc, qc = V("FullYear", !0);
	pc = Array.prototype.indexOf ? Array.prototype.indexOf : function (a) {
		var b;
		for (b = 0; b < this.length; ++b)
			if (this[b] === a) return b;
		return -1
	}, H("M", ["MM", 2], "Mo", function () {
		return this.month() + 1
	}), H("MMM", 0, 0, function (a) {
		return this.localeData().monthsShort(this, a)
	}), H("MMMM", 0, 0, function (a) {
		return this.localeData().months(this, a)
	}), B("month", "M"), E("month", 8), M("M", Ub), M("MM", Ub, Qb), M("MMM", function (a, b) {
		return b.monthsShortRegex(a)
	}), M("MMMM", function (a, b) {
		return b.monthsRegex(a)
	}), Q(["M", "MM"], function (a, b) {
		b[hc] = t(a) - 1
	}), Q(["MMM", "MMMM"], function (a, b, c, d) {
		var e = c._locale.monthsParse(a, d, c._strict);
		null != e ? b[hc] = e : m(c).invalidMonth = a
	});
	var rc = /D[oD]?(\[[^\[\]]*\]|\s)+MMMM?/,
		sc = "January_February_March_April_May_June_July_August_September_October_November_December".split("_"),
		tc = "Jan_Feb_Mar_Apr_May_Jun_Jul_Aug_Sep_Oct_Nov_Dec".split("_"),
		uc = dc,
		vc = dc;
	H("w", ["ww", 2], "wo", "week"), H("W", ["WW", 2], "Wo", "isoWeek"), B("week", "w"), B("isoWeek", "W"), E("week", 5), E("isoWeek", 5), M("w", Ub), M("ww", Ub, Qb), M("W", Ub), M("WW", Ub, Qb), R(["w", "ww", "W", "WW"], function (a, b, c, d) {
		b[d.substr(0, 1)] = t(a)
	});
	var wc = {
		dow: 0,
		doy: 6
	};
	H("d", 0, "do", "day"), H("dd", 0, 0, function (a) {
		return this.localeData().weekdaysMin(this, a)
	}), H("ddd", 0, 0, function (a) {
		return this.localeData().weekdaysShort(this, a)
	}), H("dddd", 0, 0, function (a) {
		return this.localeData().weekdays(this, a)
	}), H("e", 0, 0, "weekday"), H("E", 0, 0, "isoWeekday"), B("day", "d"), B("weekday", "e"), B("isoWeekday", "E"), E("day", 11), E("weekday", 11), E("isoWeekday", 11), M("d", Ub), M("e", Ub), M("E", Ub), M("dd", function (a, b) {
		return b.weekdaysMinRegex(a)
	}), M("ddd", function (a, b) {
		return b.weekdaysShortRegex(a)
	}), M("dddd", function (a, b) {
		return b.weekdaysRegex(a)
	}), R(["dd", "ddd", "dddd"], function (a, b, c, d) {
		var e = c._locale.weekdaysParse(a, d, c._strict);
		null != e ? b.d = e : m(c).invalidWeekday = a
	}), R(["d", "e", "E"], function (a, b, c, d) {
		b[d] = t(a)
	});
	var xc = "Sunday_Monday_Tuesday_Wednesday_Thursday_Friday_Saturday".split("_"),
		yc = "Sun_Mon_Tue_Wed_Thu_Fri_Sat".split("_"),
		zc = "Su_Mo_Tu_We_Th_Fr_Sa".split("_"),
		Ac = dc,
		Bc = dc,
		Cc = dc;
	H("H", ["HH", 2], 0, "hour"), H("h", ["hh", 2], 0, ma), H("k", ["kk", 2], 0, function () {
		return this.hours() || 24
	}), H("hmm", 0, 0, function () {
		return "" + ma.apply(this) + G(this.minutes(), 2)
	}), H("hmmss", 0, 0, function () {
		return "" + ma.apply(this) + G(this.minutes(), 2) + G(this.seconds(), 2)
	}), H("Hmm", 0, 0, function () {
		return "" + this.hours() + G(this.minutes(), 2)
	}), H("Hmmss", 0, 0, function () {
		return "" + this.hours() + G(this.minutes(), 2) + G(this.seconds(), 2)
	}), na("a", !0), na("A", !1), B("hour", "h"), E("hour", 13), M("a", oa), M("A", oa), M("H", Ub), M("h", Ub), M("k", Ub), M("HH", Ub, Qb), M("hh", Ub, Qb), M("kk", Ub, Qb), M("hmm", Vb), M("hmmss", Wb), M("Hmm", Vb), M("Hmmss", Wb), Q(["H", "HH"], jc), Q(["k", "kk"], function (a, b, c) {
		var d = t(a);
		b[jc] = 24 === d ? 0 : d
	}), Q(["a", "A"], function (a, b, c) {
		c._isPm = c._locale.isPM(a), c._meridiem = a
	}), Q(["h", "hh"], function (a, b, c) {
		b[jc] = t(a), m(c).bigHour = !0
	}), Q("hmm", function (a, b, c) {
		var d = a.length - 2;
		b[jc] = t(a.substr(0, d)), b[kc] = t(a.substr(d)), m(c).bigHour = !0
	}), Q("hmmss", function (a, b, c) {
		var d = a.length - 4,
			e = a.length - 2;
		b[jc] = t(a.substr(0, d)), b[kc] = t(a.substr(d, 2)), b[lc] = t(a.substr(e)), m(c).bigHour = !0
	}), Q("Hmm", function (a, b, c) {
		var d = a.length - 2;
		b[jc] = t(a.substr(0, d)), b[kc] = t(a.substr(d))
	}), Q("Hmmss", function (a, b, c) {
		var d = a.length - 4,
			e = a.length - 2;
		b[jc] = t(a.substr(0, d)), b[kc] = t(a.substr(d, 2)), b[lc] = t(a.substr(e))
	});
	var Dc, Ec = /[ap]\.?m?\.?/i,
		Fc = V("Hours", !0),
		Gc = {
			calendar: Fb,
			longDateFormat: Gb,
			invalidDate: "Invalid date",
			ordinal: "%d",
			dayOfMonthOrdinalParse: Hb,
			relativeTime: Ib,
			months: sc,
			monthsShort: tc,
			week: wc,
			weekdays: xc,
			weekdaysMin: zc,
			weekdaysShort: yc,
			meridiemParse: Ec
		},
		Hc = {},
		Ic = {},
		Jc = /^\s*((?:[+-]\d{6}|\d{4})-(?:\d\d-\d\d|W\d\d-\d|W\d\d|\d\d\d|\d\d))(?:(T| )(\d\d(?::\d\d(?::\d\d(?:[.,]\d+)?)?)?)([\+\-]\d\d(?::?\d\d)?|\s*Z)?)?$/,
		Kc = /^\s*((?:[+-]\d{6}|\d{4})(?:\d\d\d\d|W\d\d\d|W\d\d|\d\d\d|\d\d))(?:(T| )(\d\d(?:\d\d(?:\d\d(?:[.,]\d+)?)?)?)([\+\-]\d\d(?::?\d\d)?|\s*Z)?)?$/,
		Lc = /Z|[+-]\d\d(?::?\d\d)?/,
		Mc = [
			["YYYYYY-MM-DD", /[+-]\d{6}-\d\d-\d\d/],
			["YYYY-MM-DD", /\d{4}-\d\d-\d\d/],
			["GGGG-[W]WW-E", /\d{4}-W\d\d-\d/],
			["GGGG-[W]WW", /\d{4}-W\d\d/, !1],
			["YYYY-DDD", /\d{4}-\d{3}/],
			["YYYY-MM", /\d{4}-\d\d/, !1],
			["YYYYYYMMDD", /[+-]\d{10}/],
			["YYYYMMDD", /\d{8}/],
			["GGGG[W]WWE", /\d{4}W\d{3}/],
			["GGGG[W]WW", /\d{4}W\d{2}/, !1],
			["YYYYDDD", /\d{7}/]
		],
		Nc = [
			["HH:mm:ss.SSSS", /\d\d:\d\d:\d\d\.\d+/],
			["HH:mm:ss,SSSS", /\d\d:\d\d:\d\d,\d+/],
			["HH:mm:ss", /\d\d:\d\d:\d\d/],
			["HH:mm", /\d\d:\d\d/],
			["HHmmss.SSSS", /\d\d\d\d\d\d\.\d+/],
			["HHmmss,SSSS", /\d\d\d\d\d\d,\d+/],
			["HHmmss", /\d\d\d\d\d\d/],
			["HHmm", /\d\d\d\d/],
			["HH", /\d\d/]
		],
		Oc = /^\/?Date\((\-?\d+)/i,
		Pc = /^(?:(Mon|Tue|Wed|Thu|Fri|Sat|Sun),?\s)?(\d{1,2})\s(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s(\d{2,4})\s(\d\d):(\d\d)(?::(\d\d))?\s(?:(UT|GMT|[ECMP][SD]T)|([Zz])|([+-]\d{4}))$/,
		Qc = {
			UT: 0,
			GMT: 0,
			EDT: -240,
			EST: -300,
			CDT: -300,
			CST: -360,
			MDT: -360,
			MST: -420,
			PDT: -420,
			PST: -480
		};
	a.createFromInputFallback = w("value provided is not in a recognized RFC2822 or ISO format. moment construction falls back to js Date(), which is not reliable across all browsers and versions. Non RFC2822/ISO date formats are discouraged and will be removed in an upcoming major release. Please refer to http://momentjs.com/guides/#/warnings/js-date/ for more info.", function (a) {
		a._d = new Date(a._i + (a._useUTC ? " UTC" : ""))
	}), a.ISO_8601 = function () {}, a.RFC_2822 = function () {};
	var Rc = w("moment().min is deprecated, use moment.max instead. http://momentjs.com/guides/#/warnings/min-max/", function () {
			var a = Qa.apply(null, arguments);
			return this.isValid() && a.isValid() ? a < this ? this : a : o()
		}),
		Sc = w("moment().max is deprecated, use moment.min instead. http://momentjs.com/guides/#/warnings/min-max/", function () {
			var a = Qa.apply(null, arguments);
			return this.isValid() && a.isValid() ? a > this ? this : a : o()
		}),
		Tc = ["year", "quarter", "month", "week", "day", "hour", "minute", "second", "millisecond"];
	Wa("Z", ":"), Wa("ZZ", ""), M("Z", bc), M("ZZ", bc), Q(["Z", "ZZ"], function (a, b, c) {
		c._useUTC = !0, c._tzm = Xa(bc, a)
	});
	var Uc = /([\+\-]|\d\d)/gi;
	a.updateOffset = function () {};
	var Vc = /^(\-|\+)?(?:(\d*)[. ])?(\d+)\:(\d+)(?:\:(\d+)(\.\d*)?)?$/,
		Wc = /^(-|\+)?P(?:([-+]?[0-9,.]*)Y)?(?:([-+]?[0-9,.]*)M)?(?:([-+]?[0-9,.]*)W)?(?:([-+]?[0-9,.]*)D)?(?:T(?:([-+]?[0-9,.]*)H)?(?:([-+]?[0-9,.]*)M)?(?:([-+]?[0-9,.]*)S)?)?$/;
	_a.fn = Ta.prototype, _a.invalid = function () {
		return _a(NaN)
	};
	var Xc = db(1, "add"),
		Yc = db(-1, "subtract");
	a.defaultFormat = "YYYY-MM-DDTHH:mm:ssZ", a.defaultFormatUtc = "YYYY-MM-DDTHH:mm:ss[Z]";
	var Zc = w("moment().lang() is deprecated. Instead, use moment().localeData() to get the language configuration. Use moment().locale() to change languages.", function (a) {
		return void 0 === a ? this.localeData() : this.locale(a)
	});
	H(0, ["gg", 2], 0, function () {
			return this.weekYear() % 100
		}), H(0, ["GG", 2], 0, function () {
			return this.isoWeekYear() % 100
		}), ib("gggg", "weekYear"), ib("ggggg", "weekYear"), ib("GGGG", "isoWeekYear"), ib("GGGGG", "isoWeekYear"), B("weekYear", "gg"), B("isoWeekYear", "GG"), E("weekYear", 1), E("isoWeekYear", 1), M("G", _b), M("g", _b), M("GG", Ub, Qb), M("gg", Ub, Qb), M("GGGG", Yb, Sb), M("gggg", Yb, Sb), M("GGGGG", Zb, Tb), M("ggggg", Zb, Tb), R(["gggg", "ggggg", "GGGG", "GGGGG"], function (a, b, c, d) {
			b[d.substr(0, 2)] = t(a)
		}), R(["gg", "GG"], function (b, c, d, e) {
			c[e] = a.parseTwoDigitYear(b)
		}), H("Q", 0, "Qo", "quarter"), B("quarter", "Q"), E("quarter", 7), M("Q", Pb), Q("Q", function (a, b) {
			b[hc] = 3 * (t(a) - 1)
		}), H("D", ["DD", 2], "Do", "date"), B("date", "D"), E("date", 9),
		M("D", Ub), M("DD", Ub, Qb), M("Do", function (a, b) {
			return a ? b._dayOfMonthOrdinalParse || b._ordinalParse : b._dayOfMonthOrdinalParseLenient
		}), Q(["D", "DD"], ic), Q("Do", function (a, b) {
			b[ic] = t(a.match(Ub)[0], 10)
		});
	var $c = V("Date", !0);
	H("DDD", ["DDDD", 3], "DDDo", "dayOfYear"), B("dayOfYear", "DDD"), E("dayOfYear", 4), M("DDD", Xb), M("DDDD", Rb), Q(["DDD", "DDDD"], function (a, b, c) {
		c._dayOfYear = t(a)
	}), H("m", ["mm", 2], 0, "minute"), B("minute", "m"), E("minute", 14), M("m", Ub), M("mm", Ub, Qb), Q(["m", "mm"], kc);
	var _c = V("Minutes", !1);
	H("s", ["ss", 2], 0, "second"), B("second", "s"), E("second", 15), M("s", Ub), M("ss", Ub, Qb), Q(["s", "ss"], lc);
	var ad = V("Seconds", !1);
	H("S", 0, 0, function () {
		return ~~(this.millisecond() / 100)
	}), H(0, ["SS", 2], 0, function () {
		return ~~(this.millisecond() / 10)
	}), H(0, ["SSS", 3], 0, "millisecond"), H(0, ["SSSS", 4], 0, function () {
		return 10 * this.millisecond()
	}), H(0, ["SSSSS", 5], 0, function () {
		return 100 * this.millisecond()
	}), H(0, ["SSSSSS", 6], 0, function () {
		return 1e3 * this.millisecond()
	}), H(0, ["SSSSSSS", 7], 0, function () {
		return 1e4 * this.millisecond()
	}), H(0, ["SSSSSSSS", 8], 0, function () {
		return 1e5 * this.millisecond()
	}), H(0, ["SSSSSSSSS", 9], 0, function () {
		return 1e6 * this.millisecond()
	}), B("millisecond", "ms"), E("millisecond", 16), M("S", Xb, Pb), M("SS", Xb, Qb), M("SSS", Xb, Rb);
	var bd;
	for (bd = "SSSS"; bd.length <= 9; bd += "S") M(bd, $b);
	for (bd = "S"; bd.length <= 9; bd += "S") Q(bd, function (a, b) {
		b[mc] = t(1e3 * ("0." + a))
	});
	var cd = V("Milliseconds", !1);
	H("z", 0, 0, "zoneAbbr"), H("zz", 0, 0, "zoneName");
	var dd = q.prototype;
	dd.add = Xc, dd.calendar = function (b, c) {
		var d = b || Qa(),
			e = Ya(d, this).startOf("day"),
			f = a.calendarFormat(this, e) || "sameElse",
			g = c && (y(c[f]) ? c[f].call(this, d) : c[f]);
		return this.format(g || this.localeData().calendar(f, this, Qa(d)))
	}, dd.clone = function () {
		return new q(this)
	}, dd.diff = function (a, b, c) {
		var d, e, f;
		if (!this.isValid()) return NaN;
		if (!(d = Ya(a, this)).isValid()) return NaN;
		switch (e = 6e4 * (d.utcOffset() - this.utcOffset()), b = C(b)) {
			case "year":
				f = fb(this, d) / 12;
				break;
			case "month":
				f = fb(this, d);
				break;
			case "quarter":
				f = fb(this, d) / 3;
				break;
			case "second":
				f = (this - d) / 1e3;
				break;
			case "minute":
				f = (this - d) / 6e4;
				break;
			case "hour":
				f = (this - d) / 36e5;
				break;
			case "day":
				f = (this - d - e) / 864e5;
				break;
			case "week":
				f = (this - d - e) / 6048e5;
				break;
			default:
				f = this - d
		}
		return c ? f : s(f)
	}, dd.endOf = function (a) {
		return void 0 === (a = C(a)) || "millisecond" === a ? this : ("date" === a && (a = "day"), this.startOf(a).add(1, "isoWeek" === a ? "week" : a).subtract(1, "ms"))
	}, dd.format = function (b) {
		b || (b = this.isUtc() ? a.defaultFormatUtc : a.defaultFormat);
		var c = K(this, b);
		return this.localeData().postformat(c)
	}, dd.from = function (a, b) {
		return this.isValid() && (r(a) && a.isValid() || Qa(a).isValid()) ? _a({
			to: this,
			from: a
		}).locale(this.locale()).humanize(!b) : this.localeData().invalidDate()
	}, dd.fromNow = function (a) {
		return this.from(Qa(), a)
	}, dd.to = function (a, b) {
		return this.isValid() && (r(a) && a.isValid() || Qa(a).isValid()) ? _a({
			from: this,
			to: a
		}).locale(this.locale()).humanize(!b) : this.localeData().invalidDate()
	}, dd.toNow = function (a) {
		return this.to(Qa(), a)
	}, dd.get = function (a) {
		return a = C(a), y(this[a]) ? this[a]() : this
	}, dd.invalidAt = function () {
		return m(this).overflow
	}, dd.isAfter = function (a, b) {
		var c = r(a) ? a : Qa(a);
		return !(!this.isValid() || !c.isValid()) && ("millisecond" === (b = C(e(b) ? "millisecond" : b)) ? this.valueOf() > c.valueOf() : c.valueOf() < this.clone().startOf(b).valueOf())
	}, dd.isBefore = function (a, b) {
		var c = r(a) ? a : Qa(a);
		return !(!this.isValid() || !c.isValid()) && ("millisecond" === (b = C(e(b) ? "millisecond" : b)) ? this.valueOf() < c.valueOf() : this.clone().endOf(b).valueOf() < c.valueOf())
	}, dd.isBetween = function (a, b, c, d) {
		return ("(" === (d = d || "()")[0] ? this.isAfter(a, c) : !this.isBefore(a, c)) && (")" === d[1] ? this.isBefore(b, c) : !this.isAfter(b, c))
	}, dd.isSame = function (a, b) {
		var c, d = r(a) ? a : Qa(a);
		return !(!this.isValid() || !d.isValid()) && ("millisecond" === (b = C(b || "millisecond")) ? this.valueOf() === d.valueOf() : (c = d.valueOf(), this.clone().startOf(b).valueOf() <= c && c <= this.clone().endOf(b).valueOf()))
	}, dd.isSameOrAfter = function (a, b) {
		return this.isSame(a, b) || this.isAfter(a, b)
	}, dd.isSameOrBefore = function (a, b) {
		return this.isSame(a, b) || this.isBefore(a, b)
	}, dd.isValid = function () {
		return n(this)
	}, dd.lang = Zc, dd.locale = gb, dd.localeData = hb, dd.max = Sc, dd.min = Rc, dd.parsingFlags = function () {
		return j({}, m(this))
	}, dd.set = function (a, b) {
		if ("object" == typeof a)
			for (var c = F(a = D(a)), d = 0; d < c.length; d++) this[c[d].unit](a[c[d].unit]);
		else if (a = C(a), y(this[a])) return this[a](b);
		return this
	}, dd.startOf = function (a) {
		switch (a = C(a)) {
			case "year":
				this.month(0);
			case "quarter":
			case "month":
				this.date(1);
			case "week":
			case "isoWeek":
			case "day":
			case "date":
				this.hours(0);
			case "hour":
				this.minutes(0);
			case "minute":
				this.seconds(0);
			case "second":
				this.milliseconds(0)
		}
		return "week" === a && this.weekday(0), "isoWeek" === a && this.isoWeekday(1), "quarter" === a && this.month(3 * Math.floor(this.month() / 3)), this
	}, dd.subtract = Yc, dd.toArray = function () {
		var a = this;
		return [a.year(), a.month(), a.date(), a.hour(), a.minute(), a.second(), a.millisecond()]
	}, dd.toObject = function () {
		var a = this;
		return {
			years: a.year(),
			months: a.month(),
			date: a.date(),
			hours: a.hours(),
			minutes: a.minutes(),
			seconds: a.seconds(),
			milliseconds: a.milliseconds()
		}
	}, dd.toDate = function () {
		return new Date(this.valueOf())
	}, dd.toISOString = function () {
		if (!this.isValid()) return null;
		var a = this.clone().utc();
		return a.year() < 0 || a.year() > 9999 ? K(a, "YYYYYY-MM-DD[T]HH:mm:ss.SSS[Z]") : y(Date.prototype.toISOString) ? this.toDate().toISOString() : K(a, "YYYY-MM-DD[T]HH:mm:ss.SSS[Z]")
	}, dd.inspect = function () {
		if (!this.isValid()) return "moment.invalid(/* " + this._i + " */)";
		var a = "moment",
			b = "";
		this.isLocal() || (a = 0 === this.utcOffset() ? "moment.utc" : "moment.parseZone", b = "Z");
		var c = "[" + a + '("]',
			d = 0 <= this.year() && this.year() <= 9999 ? "YYYY" : "YYYYYY",
			e = b + '[")]';
		return this.format(c + d + "-MM-DD[T]HH:mm:ss.SSS" + e)
	}, dd.toJSON = function () {
		return this.isValid() ? this.toISOString() : null
	}, dd.toString = function () {
		return this.clone().locale("en").format("ddd MMM DD YYYY HH:mm:ss [GMT]ZZ")
	}, dd.unix = function () {
		return Math.floor(this.valueOf() / 1e3)
	}, dd.valueOf = function () {
		return this._d.valueOf() - 6e4 * (this._offset || 0)
	}, dd.creationData = function () {
		return {
			input: this._i,
			format: this._f,
			locale: this._locale,
			isUTC: this._isUTC,
			strict: this._strict
		}
	}, dd.year = qc, dd.isLeapYear = function () {
		return U(this.year())
	}, dd.weekYear = function (a) {
		return jb.call(this, a, this.week(), this.weekday(), this.localeData()._week.dow, this.localeData()._week.doy)
	}, dd.isoWeekYear = function (a) {
		return jb.call(this, a, this.isoWeek(), this.isoWeekday(), 1, 4)
	}, dd.quarter = dd.quarters = function (a) {
		return null == a ? Math.ceil((this.month() + 1) / 3) : this.month(3 * (a - 1) + this.month() % 3)
	}, dd.month = aa, dd.daysInMonth = function () {
		return Z(this.year(), this.month())
	}, dd.week = dd.weeks = function (a) {
		var b = this.localeData().week(this);
		return null == a ? b : this.add(7 * (a - b), "d")
	}, dd.isoWeek = dd.isoWeeks = function (a) {
		var b = ga(this, 1, 4).week;
		return null == a ? b : this.add(7 * (a - b), "d")
	}, dd.weeksInYear = function () {
		var a = this.localeData()._week;
		return ha(this.year(), a.dow, a.doy)
	}, dd.isoWeeksInYear = function () {
		return ha(this.year(), 1, 4)
	}, dd.date = $c, dd.day = dd.days = function (a) {
		if (!this.isValid()) return null != a ? this : NaN;
		var b = this._isUTC ? this._d.getUTCDay() : this._d.getDay();
		return null != a ? (a = ia(a, this.localeData()), this.add(a - b, "d")) : b
	}, dd.weekday = function (a) {
		if (!this.isValid()) return null != a ? this : NaN;
		var b = (this.day() + 7 - this.localeData()._week.dow) % 7;
		return null == a ? b : this.add(a - b, "d")
	}, dd.isoWeekday = function (a) {
		if (!this.isValid()) return null != a ? this : NaN;
		if (null != a) {
			var b = ja(a, this.localeData());
			return this.day(this.day() % 7 ? b : b - 7)
		}
		return this.day() || 7
	}, dd.dayOfYear = function (a) {
		var b = Math.round((this.clone().startOf("day") - this.clone().startOf("year")) / 864e5) + 1;
		return null == a ? b : this.add(a - b, "d")
	}, dd.hour = dd.hours = Fc, dd.minute = dd.minutes = _c, dd.second = dd.seconds = ad, dd.millisecond = dd.milliseconds = cd, dd.utcOffset = function (b, c, d) {
		var e, f = this._offset || 0;
		if (!this.isValid()) return null != b ? this : NaN;
		if (null != b) {
			if ("string" == typeof b) {
				if (null === (b = Xa(bc, b))) return this
			} else Math.abs(b) < 16 && !d && (b *= 60);
			return !this._isUTC && c && (e = Za(this)), this._offset = b, this._isUTC = !0, null != e && this.add(e, "m"), f !== b && (!c || this._changeInProgress ? eb(this, _a(b - f, "m"), 1, !1) : this._changeInProgress || (this._changeInProgress = !0, a.updateOffset(this, !0), this._changeInProgress = null)), this
		}
		return this._isUTC ? f : Za(this)
	}, dd.utc = function (a) {
		return this.utcOffset(0, a)
	}, dd.local = function (a) {
		return this._isUTC && (this.utcOffset(0, a), this._isUTC = !1, a && this.subtract(Za(this), "m")), this
	}, dd.parseZone = function () {
		if (null != this._tzm) this.utcOffset(this._tzm, !1, !0);
		else if ("string" == typeof this._i) {
			var a = Xa(ac, this._i);
			null != a ? this.utcOffset(a) : this.utcOffset(0, !0)
		}
		return this
	}, dd.hasAlignedHourOffset = function (a) {
		return !!this.isValid() && (a = a ? Qa(a).utcOffset() : 0, (this.utcOffset() - a) % 60 == 0)
	}, dd.isDST = function () {
		return this.utcOffset() > this.clone().month(0).utcOffset() || this.utcOffset() > this.clone().month(5).utcOffset()
	}, dd.isLocal = function () {
		return !!this.isValid() && !this._isUTC
	}, dd.isUtcOffset = function () {
		return !!this.isValid() && this._isUTC
	}, dd.isUtc = $a, dd.isUTC = $a, dd.zoneAbbr = function () {
		return this._isUTC ? "UTC" : ""
	}, dd.zoneName = function () {
		return this._isUTC ? "Coordinated Universal Time" : ""
	}, dd.dates = w("dates accessor is deprecated. Use date instead.", $c), dd.months = w("months accessor is deprecated. Use month instead", aa), dd.years = w("years accessor is deprecated. Use year instead", qc), dd.zone = w("moment().zone is deprecated, use moment().utcOffset instead. http://momentjs.com/guides/#/warnings/zone/", function (a, b) {
		return null != a ? ("string" != typeof a && (a = -a), this.utcOffset(a, b), this) : -this.utcOffset()
	}), dd.isDSTShifted = w("isDSTShifted is deprecated. See http://momentjs.com/guides/#/warnings/dst-shifted/ for more information", function () {
		if (!e(this._isDSTShifted)) return this._isDSTShifted;
		var a = {};
		if (p(a, this), (a = Na(a))._a) {
			var b = a._isUTC ? k(a._a) : Qa(a._a);
			this._isDSTShifted = this.isValid() && u(a._a, b.toArray()) > 0
		} else this._isDSTShifted = !1;
		return this._isDSTShifted
	});
	var ed = A.prototype;
	ed.calendar = function (a, b, c) {
		var d = this._calendar[a] || this._calendar.sameElse;
		return y(d) ? d.call(b, c) : d
	}, ed.longDateFormat = function (a) {
		var b = this._longDateFormat[a],
			c = this._longDateFormat[a.toUpperCase()];
		return b || !c ? b : (this._longDateFormat[a] = c.replace(/MMMM|MM|DD|dddd/g, function (a) {
			return a.slice(1)
		}), this._longDateFormat[a])
	}, ed.invalidDate = function () {
		return this._invalidDate
	}, ed.ordinal = function (a) {
		return this._ordinal.replace("%d", a)
	}, ed.preparse = lb, ed.postformat = lb, ed.relativeTime = function (a, b, c, d) {
		var e = this._relativeTime[c];
		return y(e) ? e(a, b, c, d) : e.replace(/%d/i, a)
	}, ed.pastFuture = function (a, b) {
		var c = this._relativeTime[a > 0 ? "future" : "past"];
		return y(c) ? c(b) : c.replace(/%s/i, b)
	}, ed.set = function (a) {
		var b, c;
		for (c in a) y(b = a[c]) ? this[c] = b : this["_" + c] = b;
		this._config = a, this._dayOfMonthOrdinalParseLenient = new RegExp((this._dayOfMonthOrdinalParse.source || this._ordinalParse.source) + "|" + /\d{1,2}/.source)
	}, ed.months = function (a, c) {
		return a ? b(this._months) ? this._months[a.month()] : this._months[(this._months.isFormat || rc).test(c) ? "format" : "standalone"][a.month()] : b(this._months) ? this._months : this._months.standalone
	}, ed.monthsShort = function (a, c) {
		return a ? b(this._monthsShort) ? this._monthsShort[a.month()] : this._monthsShort[rc.test(c) ? "format" : "standalone"][a.month()] : b(this._monthsShort) ? this._monthsShort : this._monthsShort.standalone
	}, ed.monthsParse = function (a, b, c) {
		var d, e, f;
		if (this._monthsParseExact) return $.call(this, a, b, c);
		for (this._monthsParse || (this._monthsParse = [], this._longMonthsParse = [], this._shortMonthsParse = []), d = 0; d < 12; d++) {
			if (e = k([2e3, d]), c && !this._longMonthsParse[d] && (this._longMonthsParse[d] = new RegExp("^" + this.months(e, "").replace(".", "") + "$", "i"), this._shortMonthsParse[d] = new RegExp("^" + this.monthsShort(e, "").replace(".", "") + "$", "i")), c || this._monthsParse[d] || (f = "^" + this.months(e, "") + "|^" + this.monthsShort(e, ""), this._monthsParse[d] = new RegExp(f.replace(".", ""), "i")), c && "MMMM" === b && this._longMonthsParse[d].test(a)) return d;
			if (c && "MMM" === b && this._shortMonthsParse[d].test(a)) return d;
			if (!c && this._monthsParse[d].test(a)) return d
		}
	}, ed.monthsRegex = function (a) {
		return this._monthsParseExact ? (i(this, "_monthsRegex") || ba.call(this), a ? this._monthsStrictRegex : this._monthsRegex) : (i(this, "_monthsRegex") || (this._monthsRegex = vc), this._monthsStrictRegex && a ? this._monthsStrictRegex : this._monthsRegex)
	}, ed.monthsShortRegex = function (a) {
		return this._monthsParseExact ? (i(this, "_monthsRegex") || ba.call(this), a ? this._monthsShortStrictRegex : this._monthsShortRegex) : (i(this, "_monthsShortRegex") || (this._monthsShortRegex = uc), this._monthsShortStrictRegex && a ? this._monthsShortStrictRegex : this._monthsShortRegex)
	}, ed.week = function (a) {
		return ga(a, this._week.dow, this._week.doy).week
	}, ed.firstDayOfYear = function () {
		return this._week.doy
	}, ed.firstDayOfWeek = function () {
		return this._week.dow
	}, ed.weekdays = function (a, c) {
		return a ? b(this._weekdays) ? this._weekdays[a.day()] : this._weekdays[this._weekdays.isFormat.test(c) ? "format" : "standalone"][a.day()] : b(this._weekdays) ? this._weekdays : this._weekdays.standalone
	}, ed.weekdaysMin = function (a) {
		return a ? this._weekdaysMin[a.day()] : this._weekdaysMin
	}, ed.weekdaysShort = function (a) {
		return a ? this._weekdaysShort[a.day()] : this._weekdaysShort
	}, ed.weekdaysParse = function (a, b, c) {
		var d, e, f;
		if (this._weekdaysParseExact) return ka.call(this, a, b, c);
		for (this._weekdaysParse || (this._weekdaysParse = [], this._minWeekdaysParse = [], this._shortWeekdaysParse = [], this._fullWeekdaysParse = []), d = 0; d < 7; d++) {
			if (e = k([2e3, 1]).day(d), c && !this._fullWeekdaysParse[d] && (this._fullWeekdaysParse[d] = new RegExp("^" + this.weekdays(e, "").replace(".", ".?") + "$", "i"), this._shortWeekdaysParse[d] = new RegExp("^" + this.weekdaysShort(e, "").replace(".", ".?") + "$", "i"), this._minWeekdaysParse[d] = new RegExp("^" + this.weekdaysMin(e, "").replace(".", ".?") + "$", "i")), this._weekdaysParse[d] || (f = "^" + this.weekdays(e, "") + "|^" + this.weekdaysShort(e, "") + "|^" + this.weekdaysMin(e, ""), this._weekdaysParse[d] = new RegExp(f.replace(".", ""), "i")), c && "dddd" === b && this._fullWeekdaysParse[d].test(a)) return d;
			if (c && "ddd" === b && this._shortWeekdaysParse[d].test(a)) return d;
			if (c && "dd" === b && this._minWeekdaysParse[d].test(a)) return d;
			if (!c && this._weekdaysParse[d].test(a)) return d
		}
	}, ed.weekdaysRegex = function (a) {
		return this._weekdaysParseExact ? (i(this, "_weekdaysRegex") || la.call(this), a ? this._weekdaysStrictRegex : this._weekdaysRegex) : (i(this, "_weekdaysRegex") || (this._weekdaysRegex = Ac), this._weekdaysStrictRegex && a ? this._weekdaysStrictRegex : this._weekdaysRegex)
	}, ed.weekdaysShortRegex = function (a) {
		return this._weekdaysParseExact ? (i(this, "_weekdaysRegex") || la.call(this), a ? this._weekdaysShortStrictRegex : this._weekdaysShortRegex) : (i(this, "_weekdaysShortRegex") || (this._weekdaysShortRegex = Bc), this._weekdaysShortStrictRegex && a ? this._weekdaysShortStrictRegex : this._weekdaysShortRegex)
	}, ed.weekdaysMinRegex = function (a) {
		return this._weekdaysParseExact ? (i(this, "_weekdaysRegex") || la.call(this), a ? this._weekdaysMinStrictRegex : this._weekdaysMinRegex) : (i(this, "_weekdaysMinRegex") || (this._weekdaysMinRegex = Cc), this._weekdaysMinStrictRegex && a ? this._weekdaysMinStrictRegex : this._weekdaysMinRegex)
	}, ed.isPM = function (a) {
		return "p" === (a + "").toLowerCase().charAt(0)
	}, ed.meridiem = function (a, b, c) {
		return a > 11 ? c ? "pm" : "PM" : c ? "am" : "AM"
	}, sa("en", {
		dayOfMonthOrdinalParse: /\d{1,2}(th|st|nd|rd)/,
		ordinal: function (a) {
			var b = a % 10;
			return a + (1 === t(a % 100 / 10) ? "th" : 1 === b ? "st" : 2 === b ? "nd" : 3 === b ? "rd" : "th")
		}
	}), a.lang = w("moment.lang is deprecated. Use moment.locale instead.", sa), a.langData = w("moment.langData is deprecated. Use moment.localeData instead.", ua);
	var fd = Math.abs,
		gd = tb("ms"),
		hd = tb("s"),
		id = tb("m"),
		jd = tb("h"),
		kd = tb("d"),
		ld = tb("w"),
		md = tb("M"),
		nd = tb("y"),
		od = ub("milliseconds"),
		pd = ub("seconds"),
		qd = ub("minutes"),
		rd = ub("hours"),
		sd = ub("days"),
		td = ub("months"),
		ud = ub("years"),
		vd = Math.round,
		wd = {
			ss: 44,
			s: 45,
			m: 45,
			h: 22,
			d: 26,
			M: 11
		},
		xd = Math.abs,
		yd = Ta.prototype;
	return yd.isValid = function () {
			return this._isValid
		}, yd.abs = function () {
			var a = this._data;
			return this._milliseconds = fd(this._milliseconds), this._days = fd(this._days), this._months = fd(this._months), a.milliseconds = fd(a.milliseconds), a.seconds = fd(a.seconds), a.minutes = fd(a.minutes), a.hours = fd(a.hours), a.months = fd(a.months), a.years = fd(a.years), this
		}, yd.add = function (a, b) {
			return pb(this, a, b, 1)
		}, yd.subtract = function (a, b) {
			return pb(this, a, b, -1)
		}, yd.as = function (a) {
			if (!this.isValid()) return NaN;
			var b, c, d = this._milliseconds;
			if ("month" === (a = C(a)) || "year" === a) return b = this._days + d / 864e5, c = this._months + rb(b), "month" === a ? c : c / 12;
			switch (b = this._days + Math.round(sb(this._months)), a) {
				case "week":
					return b / 7 + d / 6048e5;
				case "day":
					return b + d / 864e5;
				case "hour":
					return 24 * b + d / 36e5;
				case "minute":
					return 1440 * b + d / 6e4;
				case "second":
					return 86400 * b + d / 1e3;
				case "millisecond":
					return Math.floor(864e5 * b) + d;
				default:
					throw new Error("Unknown unit " + a)
			}
		}, yd.asMilliseconds = gd, yd.asSeconds = hd, yd.asMinutes = id, yd.asHours = jd, yd.asDays = kd, yd.asWeeks = ld, yd.asMonths = md, yd.asYears = nd, yd.valueOf = function () {
			return this.isValid() ? this._milliseconds + 864e5 * this._days + this._months % 12 * 2592e6 + 31536e6 * t(this._months / 12) : NaN
		}, yd._bubble = function () {
			var a, b, c, d, e, f = this._milliseconds,
				g = this._days,
				h = this._months,
				i = this._data;
			return f >= 0 && g >= 0 && h >= 0 || f <= 0 && g <= 0 && h <= 0 || (f += 864e5 * qb(sb(h) + g), g = 0, h = 0), i.milliseconds = f % 1e3, a = s(f / 1e3), i.seconds = a % 60, b = s(a / 60), i.minutes = b % 60, c = s(b / 60), i.hours = c % 24, g += s(c / 24), e = s(rb(g)), h += e, g -= qb(sb(e)), d = s(h / 12), h %= 12, i.days = g, i.months = h, i.years = d, this
		}, yd.clone = function () {
			return _a(this)
		}, yd.get = function (a) {
			return a = C(a), this.isValid() ? this[a + "s"]() : NaN
		}, yd.milliseconds = od, yd.seconds = pd, yd.minutes = qd, yd.hours = rd, yd.days = sd, yd.weeks = function () {
			return s(this.days() / 7)
		}, yd.months = td, yd.years = ud, yd.humanize = function (a) {
			if (!this.isValid()) return this.localeData().invalidDate();
			var b = this.localeData(),
				c = wb(this, !a, b);
			return a && (c = b.pastFuture(+this, c)), b.postformat(c)
		}, yd.toISOString = yb, yd.toString = yb, yd.toJSON = yb, yd.locale = gb, yd.localeData = hb, yd.toIsoString = w("toIsoString() is deprecated. Please use toISOString() instead (notice the capitals)", yb), yd.lang = Zc, H("X", 0, 0, "unix"), H("x", 0, 0, "valueOf"), M("x", _b), M("X", cc), Q("X", function (a, b, c) {
			c._d = new Date(1e3 * parseFloat(a, 10))
		}), Q("x", function (a, b, c) {
			c._d = new Date(t(a))
		}), a.version = "2.19.2",
		function (a) {
			zb = a
		}(Qa), a.fn = dd, a.min = function () {
			return Ra("isBefore", [].slice.call(arguments, 0))
		}, a.max = function () {
			return Ra("isAfter", [].slice.call(arguments, 0))
		}, a.now = function () {
			return Date.now ? Date.now() : +new Date
		}, a.utc = k, a.unix = function (a) {
			return Qa(1e3 * a)
		}, a.months = function (a, b) {
			return nb(a, b, "months")
		}, a.isDate = g, a.locale = sa, a.invalid = o, a.duration = _a, a.isMoment = r, a.weekdays = function (a, b, c) {
			return ob(a, b, c, "weekdays")
		}, a.parseZone = function () {
			return Qa.apply(null, arguments).parseZone()
		}, a.localeData = ua, a.isDuration = Ua, a.monthsShort = function (a, b) {
			return nb(a, b, "monthsShort")
		}, a.weekdaysMin = function (a, b, c) {
			return ob(a, b, c, "weekdaysMin")
		}, a.defineLocale = ta, a.updateLocale = function (a, b) {
			if (null != b) {
				var c, d, e = Gc;
				null != (d = ra(a)) && (e = d._config), (c = new A(b = z(e, b))).parentLocale = Hc[a], Hc[a] = c, sa(a)
			} else null != Hc[a] && (null != Hc[a].parentLocale ? Hc[a] = Hc[a].parentLocale : null != Hc[a] && delete Hc[a]);
			return Hc[a]
		}, a.locales = function () {
			return Eb(Hc)
		}, a.weekdaysShort = function (a, b, c) {
			return ob(a, b, c, "weekdaysShort")
		}, a.normalizeUnits = C, a.relativeTimeRounding = function (a) {
			return void 0 === a ? vd : "function" == typeof a && (vd = a, !0)
		}, a.relativeTimeThreshold = function (a, b) {
			return void 0 !== wd[a] && (void 0 === b ? wd[a] : (wd[a] = b, "s" === a && (wd.ss = b - 1), !0))
		}, a.calendarFormat = function (a, b) {
			var c = a.diff(b, "days", !0);
			return c < -6 ? "sameElse" : c < -1 ? "lastWeek" : c < 0 ? "lastDay" : c < 1 ? "sameDay" : c < 2 ? "nextDay" : c < 7 ? "nextWeek" : "sameElse"
		}, a.prototype = dd, a
}),
function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a(jQuery)
}(function (a) {
	function b(a) {
		for (var b = a.css("visibility");
			"inherit" === b;) a = a.parent(), b = a.css("visibility");
		return "hidden" !== b
	}

	function c(a) {
		for (var b, c; a.length && a[0] !== document;) {
			if (("absolute" === (b = a.css("position")) || "relative" === b || "fixed" === b) && (c = parseInt(a.css("zIndex"), 10), !isNaN(c) && 0 !== c)) return c;
			a = a.parent()
		}
		return 0
	}

	function d() {
		this._curInst = null, this._keyEvent = !1, this._disabledInputs = [], this._datepickerShowing = !1, this._inDialog = !1, this._mainDivId = "ui-datepicker-div", this._inlineClass = "ui-datepicker-inline", this._appendClass = "ui-datepicker-append", this._triggerClass = "ui-datepicker-trigger", this._dialogClass = "ui-datepicker-dialog", this._disableClass = "ui-datepicker-disabled", this._unselectableClass = "ui-datepicker-unselectable", this._currentClass = "ui-datepicker-current-day", this._dayOverClass = "ui-datepicker-days-cell-over", this.regional = [], this.regional[""] = {
			closeText: "Done",
			prevText: "Prev",
			nextText: "Next",
			currentText: "Today",
			monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
			monthNamesShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
			dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
			dayNamesShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
			dayNamesMin: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
			weekHeader: "Wk",
			dateFormat: "mm/dd/yy",
			firstDay: 0,
			isRTL: !1,
			showMonthAfterYear: !1,
			yearSuffix: ""
		}, this._defaults = {
			showOn: "focus",
			showAnim: "fadeIn",
			showOptions: {},
			defaultDate: null,
			appendText: "",
			buttonText: "...",
			buttonImage: "",
			buttonImageOnly: !1,
			hideIfNoPrevNext: !1,
			navigationAsDateFormat: !1,
			gotoCurrent: !1,
			changeMonth: !1,
			changeYear: !1,
			yearRange: "c-10:c+10",
			showOtherMonths: !1,
			selectOtherMonths: !1,
			showWeek: !1,
			calculateWeek: this.iso8601Week,
			shortYearCutoff: "+10",
			minDate: null,
			maxDate: null,
			duration: "fast",
			beforeShowDay: null,
			beforeShow: null,
			onSelect: null,
			onChangeMonthYear: null,
			onClose: null,
			numberOfMonths: 1,
			showCurrentAtPos: 0,
			stepMonths: 1,
			stepBigMonths: 12,
			altField: "",
			altFormat: "",
			constrainInput: !0,
			showButtonPanel: !1,
			autoSize: !1,
			disabled: !1
		}, a.extend(this._defaults, this.regional[""]), this.regional.en = a.extend(!0, {}, this.regional[""]), this.regional["en-US"] = a.extend(!0, {}, this.regional.en), this.dpDiv = e(a("<div id='" + this._mainDivId + "' class='ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all'></div>"))
	}

	function e(b) {
		var c = "button, .ui-datepicker-prev, .ui-datepicker-next, .ui-datepicker-calendar td a";
		return b.on("mouseout", c, function () {
			a(this).removeClass("ui-state-hover"), -1 !== this.className.indexOf("ui-datepicker-prev") && a(this).removeClass("ui-datepicker-prev-hover"), -1 !== this.className.indexOf("ui-datepicker-next") && a(this).removeClass("ui-datepicker-next-hover")
		}).on("mouseover", c, f)
	}

	function f() {
		a.datepicker._isDisabledDatepicker(p.inline ? p.dpDiv.parent()[0] : p.input[0]) || (a(this).parents(".ui-datepicker-calendar").find("a").removeClass("ui-state-hover"), a(this).addClass("ui-state-hover"), -1 !== this.className.indexOf("ui-datepicker-prev") && a(this).addClass("ui-datepicker-prev-hover"), -1 !== this.className.indexOf("ui-datepicker-next") && a(this).addClass("ui-datepicker-next-hover"))
	}

	function g(b, c) {
		a.extend(b, c);
		for (var d in c) null == c[d] && (b[d] = c[d]);
		return b
	}

	function h(a) {
		return function () {
			var b = this.element.val();
			a.apply(this, arguments), this._refresh(), b !== this.element.val() && this._trigger("change")
		}
	}
	a.ui = a.ui || {}, a.ui.version = "1.12.1";
	var i = 0,
		j = Array.prototype.slice;
	a.cleanData = function (b) {
			return function (c) {
				var d, e, f;
				for (f = 0; null != (e = c[f]); f++) try {
					(d = a._data(e, "events")) && d.remove && a(e).triggerHandler("remove")
				} catch (a) {}
				b(c)
			}
		}(a.cleanData), a.widget = function (b, c, d) {
			var e, f, g, h = {},
				i = b.split(".")[0];
			b = b.split(".")[1];
			var j = i + "-" + b;
			return d || (d = c, c = a.Widget), a.isArray(d) && (d = a.extend.apply(null, [{}].concat(d))), a.expr[":"][j.toLowerCase()] = function (b) {
				return !!a.data(b, j)
			}, a[i] = a[i] || {}, e = a[i][b], f = a[i][b] = function (a, b) {
				return this._createWidget ? void(arguments.length && this._createWidget(a, b)) : new f(a, b)
			}, a.extend(f, e, {
				version: d.version,
				_proto: a.extend({}, d),
				_childConstructors: []
			}), g = new c, g.options = a.widget.extend({}, g.options), a.each(d, function (b, d) {
				return a.isFunction(d) ? void(h[b] = function () {
					function a() {
						return c.prototype[b].apply(this, arguments)
					}

					function e(a) {
						return c.prototype[b].apply(this, a)
					}
					return function () {
						var b, c = this._super,
							f = this._superApply;
						return this._super = a, this._superApply = e, b = d.apply(this, arguments), this._super = c, this._superApply = f, b
					}
				}()) : void(h[b] = d)
			}), f.prototype = a.widget.extend(g, {
				widgetEventPrefix: e ? g.widgetEventPrefix || b : b
			}, h, {
				constructor: f,
				namespace: i,
				widgetName: b,
				widgetFullName: j
			}), e ? (a.each(e._childConstructors, function (b, c) {
				var d = c.prototype;
				a.widget(d.namespace + "." + d.widgetName, f, c._proto)
			}), delete e._childConstructors) : c._childConstructors.push(f), a.widget.bridge(b, f), f
		}, a.widget.extend = function (b) {
			for (var c, d, e = j.call(arguments, 1), f = 0, g = e.length; g > f; f++)
				for (c in e[f]) d = e[f][c], e[f].hasOwnProperty(c) && void 0 !== d && (b[c] = a.isPlainObject(d) ? a.isPlainObject(b[c]) ? a.widget.extend({}, b[c], d) : a.widget.extend({}, d) : d);
			return b
		}, a.widget.bridge = function (b, c) {
			var d = c.prototype.widgetFullName || b;
			a.fn[b] = function (e) {
				var f = "string" == typeof e,
					g = j.call(arguments, 1),
					h = this;
				return f ? this.length || "instance" !== e ? this.each(function () {
					var c, f = a.data(this, d);
					return "instance" === e ? (h = f, !1) : f ? a.isFunction(f[e]) && "_" !== e.charAt(0) ? (c = f[e].apply(f, g), c !== f && void 0 !== c ? (h = c && c.jquery ? h.pushStack(c.get()) : c, !1) : void 0) : a.error("no such method '" + e + "' for " + b + " widget instance") : a.error("cannot call methods on " + b + " prior to initialization; attempted to call method '" + e + "'")
				}) : h = void 0 : (g.length && (e = a.widget.extend.apply(null, [e].concat(g))), this.each(function () {
					var b = a.data(this, d);
					b ? (b.option(e || {}), b._init && b._init()) : a.data(this, d, new c(e, this))
				})), h
			}
		}, a.Widget = function () {}, a.Widget._childConstructors = [], a.Widget.prototype = {
			widgetName: "widget",
			widgetEventPrefix: "",
			defaultElement: "<div>",
			options: {
				classes: {},
				disabled: !1,
				create: null
			},
			_createWidget: function (b, c) {
				c = a(c || this.defaultElement || this)[0], this.element = a(c), this.uuid = i++, this.eventNamespace = "." + this.widgetName + this.uuid, this.bindings = a(), this.hoverable = a(), this.focusable = a(), this.classesElementLookup = {}, c !== this && (a.data(c, this.widgetFullName, this), this._on(!0, this.element, {
					remove: function (a) {
						a.target === c && this.destroy()
					}
				}), this.document = a(c.style ? c.ownerDocument : c.document || c), this.window = a(this.document[0].defaultView || this.document[0].parentWindow)), this.options = a.widget.extend({}, this.options, this._getCreateOptions(), b), this._create(), this.options.disabled && this._setOptionDisabled(this.options.disabled), this._trigger("create", null, this._getCreateEventData()), this._init()
			},
			_getCreateOptions: function () {
				return {}
			},
			_getCreateEventData: a.noop,
			_create: a.noop,
			_init: a.noop,
			destroy: function () {
				var b = this;
				this._destroy(), a.each(this.classesElementLookup, function (a, c) {
					b._removeClass(c, a)
				}), this.element.off(this.eventNamespace).removeData(this.widgetFullName), this.widget().off(this.eventNamespace).removeAttr("aria-disabled"), this.bindings.off(this.eventNamespace)
			},
			_destroy: a.noop,
			widget: function () {
				return this.element
			},
			option: function (b, c) {
				var d, e, f, g = b;
				if (0 === arguments.length) return a.widget.extend({}, this.options);
				if ("string" == typeof b)
					if (g = {}, d = b.split("."), b = d.shift(), d.length) {
						for (e = g[b] = a.widget.extend({}, this.options[b]), f = 0; d.length - 1 > f; f++) e[d[f]] = e[d[f]] || {}, e = e[d[f]];
						if (b = d.pop(), 1 === arguments.length) return void 0 === e[b] ? null : e[b];
						e[b] = c
					} else {
						if (1 === arguments.length) return void 0 === this.options[b] ? null : this.options[b];
						g[b] = c
					}
				return this._setOptions(g), this
			},
			_setOptions: function (a) {
				var b;
				for (b in a) this._setOption(b, a[b]);
				return this
			},
			_setOption: function (a, b) {
				return "classes" === a && this._setOptionClasses(b), this.options[a] = b, "disabled" === a && this._setOptionDisabled(b), this
			},
			_setOptionClasses: function (b) {
				var c, d, e;
				for (c in b) e = this.classesElementLookup[c], b[c] !== this.options.classes[c] && e && e.length && (d = a(e.get()), this._removeClass(e, c), d.addClass(this._classes({
					element: d,
					keys: c,
					classes: b,
					add: !0
				})))
			},
			_setOptionDisabled: function (a) {
				this._toggleClass(this.widget(), this.widgetFullName + "-disabled", null, !!a), a && (this._removeClass(this.hoverable, null, "ui-state-hover"), this._removeClass(this.focusable, null, "ui-state-focus"))
			},
			enable: function () {
				return this._setOptions({
					disabled: !1
				})
			},
			disable: function () {
				return this._setOptions({
					disabled: !0
				})
			},
			_classes: function (b) {
				function c(c, f) {
					var g, h;
					for (h = 0; c.length > h; h++) g = e.classesElementLookup[c[h]] || a(), g = a(b.add ? a.unique(g.get().concat(b.element.get())) : g.not(b.element).get()), e.classesElementLookup[c[h]] = g, d.push(c[h]), f && b.classes[c[h]] && d.push(b.classes[c[h]])
				}
				var d = [],
					e = this;
				return b = a.extend({
					element: this.element,
					classes: this.options.classes || {}
				}, b), this._on(b.element, {
					remove: "_untrackClassesElement"
				}), b.keys && c(b.keys.match(/\S+/g) || [], !0), b.extra && c(b.extra.match(/\S+/g) || []), d.join(" ")
			},
			_untrackClassesElement: function (b) {
				var c = this;
				a.each(c.classesElementLookup, function (d, e) {
					-1 !== a.inArray(b.target, e) && (c.classesElementLookup[d] = a(e.not(b.target).get()))
				})
			},
			_removeClass: function (a, b, c) {
				return this._toggleClass(a, b, c, !1)
			},
			_addClass: function (a, b, c) {
				return this._toggleClass(a, b, c, !0)
			},
			_toggleClass: function (a, b, c, d) {
				d = "boolean" == typeof d ? d : c;
				var e = "string" == typeof a || null === a,
					f = {
						extra: e ? b : c,
						keys: e ? a : b,
						element: e ? this.element : a,
						add: d
					};
				return f.element.toggleClass(this._classes(f), d), this
			},
			_on: function (b, c, d) {
				var e, f = this;
				"boolean" != typeof b && (d = c, c = b, b = !1), d ? (c = e = a(c), this.bindings = this.bindings.add(c)) : (d = c, c = this.element, e = this.widget()), a.each(d, function (d, g) {
					function h() {
						return b || !0 !== f.options.disabled && !a(this).hasClass("ui-state-disabled") ? ("string" == typeof g ? f[g] : g).apply(f, arguments) : void 0
					}
					"string" != typeof g && (h.guid = g.guid = g.guid || h.guid || a.guid++);
					var i = d.match(/^([\w:-]*)\s*(.*)$/),
						j = i[1] + f.eventNamespace,
						k = i[2];
					k ? e.on(j, k, h) : c.on(j, h)
				})
			},
			_off: function (b, c) {
				c = (c || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace, b.off(c).off(c), this.bindings = a(this.bindings.not(b).get()), this.focusable = a(this.focusable.not(b).get()), this.hoverable = a(this.hoverable.not(b).get())
			},
			_delay: function (a, b) {
				function c() {
					return ("string" == typeof a ? d[a] : a).apply(d, arguments)
				}
				var d = this;
				return setTimeout(c, b || 0)
			},
			_hoverable: function (b) {
				this.hoverable = this.hoverable.add(b), this._on(b, {
					mouseenter: function (b) {
						this._addClass(a(b.currentTarget), null, "ui-state-hover")
					},
					mouseleave: function (b) {
						this._removeClass(a(b.currentTarget), null, "ui-state-hover")
					}
				})
			},
			_focusable: function (b) {
				this.focusable = this.focusable.add(b), this._on(b, {
					focusin: function (b) {
						this._addClass(a(b.currentTarget), null, "ui-state-focus")
					},
					focusout: function (b) {
						this._removeClass(a(b.currentTarget), null, "ui-state-focus")
					}
				})
			},
			_trigger: function (b, c, d) {
				var e, f, g = this.options[b];
				if (d = d || {}, c = a.Event(c), c.type = (b === this.widgetEventPrefix ? b : this.widgetEventPrefix + b).toLowerCase(), c.target = this.element[0], f = c.originalEvent)
					for (e in f) e in c || (c[e] = f[e]);
				return this.element.trigger(c, d), !(a.isFunction(g) && !1 === g.apply(this.element[0], [c].concat(d)) || c.isDefaultPrevented())
			}
		}, a.each({
			show: "fadeIn",
			hide: "fadeOut"
		}, function (b, c) {
			a.Widget.prototype["_" + b] = function (d, e, f) {
				"string" == typeof e && (e = {
					effect: e
				});
				var g, h = e ? !0 === e || "number" == typeof e ? c : e.effect || c : b;
				e = e || {}, "number" == typeof e && (e = {
					duration: e
				}), g = !a.isEmptyObject(e), e.complete = f, e.delay && d.delay(e.delay), g && a.effects && a.effects.effect[h] ? d[b](e) : h !== b && d[h] ? d[h](e.duration, e.easing, f) : d.queue(function (c) {
					a(this)[b](), f && f.call(d[0]), c()
				})
			}
		}), a.widget,
		function () {
			function b(a, b, c) {
				return [parseFloat(a[0]) * (l.test(a[0]) ? b / 100 : 1), parseFloat(a[1]) * (l.test(a[1]) ? c / 100 : 1)]
			}

			function c(b, c) {
				return parseInt(a.css(b, c), 10) || 0
			}

			function d(b) {
				var c = b[0];
				return 9 === c.nodeType ? {
					width: b.width(),
					height: b.height(),
					offset: {
						top: 0,
						left: 0
					}
				} : a.isWindow(c) ? {
					width: b.width(),
					height: b.height(),
					offset: {
						top: b.scrollTop(),
						left: b.scrollLeft()
					}
				} : c.preventDefault ? {
					width: 0,
					height: 0,
					offset: {
						top: c.pageY,
						left: c.pageX
					}
				} : {
					width: b.outerWidth(),
					height: b.outerHeight(),
					offset: b.offset()
				}
			}
			var e, f = Math.max,
				g = Math.abs,
				h = /left|center|right/,
				i = /top|center|bottom/,
				j = /[\+\-]\d+(\.[\d]+)?%?/,
				k = /^\w+/,
				l = /%$/,
				m = a.fn.position;
			a.position = {
				scrollbarWidth: function () {
					if (void 0 !== e) return e;
					var b, c, d = a("<div style='display:block;position:absolute;width:50px;height:50px;overflow:hidden;'><div style='height:100px;width:auto;'></div></div>"),
						f = d.children()[0];
					return a("body").append(d), b = f.offsetWidth, d.css("overflow", "scroll"), c = f.offsetWidth, b === c && (c = d[0].clientWidth), d.remove(), e = b - c
				},
				getScrollInfo: function (b) {
					var c = b.isWindow || b.isDocument ? "" : b.element.css("overflow-x"),
						d = b.isWindow || b.isDocument ? "" : b.element.css("overflow-y"),
						e = "scroll" === c || "auto" === c && b.width < b.element[0].scrollWidth;
					return {
						width: "scroll" === d || "auto" === d && b.height < b.element[0].scrollHeight ? a.position.scrollbarWidth() : 0,
						height: e ? a.position.scrollbarWidth() : 0
					}
				},
				getWithinInfo: function (b) {
					var c = a(b || window),
						d = a.isWindow(c[0]),
						e = !!c[0] && 9 === c[0].nodeType;
					return {
						element: c,
						isWindow: d,
						isDocument: e,
						offset: d || e ? {
							left: 0,
							top: 0
						} : a(b).offset(),
						scrollLeft: c.scrollLeft(),
						scrollTop: c.scrollTop(),
						width: c.outerWidth(),
						height: c.outerHeight()
					}
				}
			}, a.fn.position = function (e) {
				if (!e || !e.of) return m.apply(this, arguments);
				e = a.extend({}, e);
				var l, n, o, p, q, r, s = a(e.of),
					t = a.position.getWithinInfo(e.within),
					u = a.position.getScrollInfo(t),
					v = (e.collision || "flip").split(" "),
					w = {};
				return r = d(s), s[0].preventDefault && (e.at = "left top"), n = r.width, o = r.height, p = r.offset, q = a.extend({}, p), a.each(["my", "at"], function () {
					var a, b, c = (e[this] || "").split(" ");
					1 === c.length && (c = h.test(c[0]) ? c.concat(["center"]) : i.test(c[0]) ? ["center"].concat(c) : ["center", "center"]), c[0] = h.test(c[0]) ? c[0] : "center", c[1] = i.test(c[1]) ? c[1] : "center", a = j.exec(c[0]), b = j.exec(c[1]), w[this] = [a ? a[0] : 0, b ? b[0] : 0], e[this] = [k.exec(c[0])[0], k.exec(c[1])[0]]
				}), 1 === v.length && (v[1] = v[0]), "right" === e.at[0] ? q.left += n : "center" === e.at[0] && (q.left += n / 2), "bottom" === e.at[1] ? q.top += o : "center" === e.at[1] && (q.top += o / 2), l = b(w.at, n, o), q.left += l[0], q.top += l[1], this.each(function () {
					var d, h, i = a(this),
						j = i.outerWidth(),
						k = i.outerHeight(),
						m = c(this, "marginLeft"),
						r = c(this, "marginTop"),
						x = j + m + c(this, "marginRight") + u.width,
						y = k + r + c(this, "marginBottom") + u.height,
						z = a.extend({}, q),
						A = b(w.my, i.outerWidth(), i.outerHeight());
					"right" === e.my[0] ? z.left -= j : "center" === e.my[0] && (z.left -= j / 2), "bottom" === e.my[1] ? z.top -= k : "center" === e.my[1] && (z.top -= k / 2), z.left += A[0], z.top += A[1], d = {
						marginLeft: m,
						marginTop: r
					}, a.each(["left", "top"], function (b, c) {
						a.ui.position[v[b]] && a.ui.position[v[b]][c](z, {
							targetWidth: n,
							targetHeight: o,
							elemWidth: j,
							elemHeight: k,
							collisionPosition: d,
							collisionWidth: x,
							collisionHeight: y,
							offset: [l[0] + A[0], l[1] + A[1]],
							my: e.my,
							at: e.at,
							within: t,
							elem: i
						})
					}), e.using && (h = function (a) {
						var b = p.left - z.left,
							c = b + n - j,
							d = p.top - z.top,
							h = d + o - k,
							l = {
								target: {
									element: s,
									left: p.left,
									top: p.top,
									width: n,
									height: o
								},
								element: {
									element: i,
									left: z.left,
									top: z.top,
									width: j,
									height: k
								},
								horizontal: 0 > c ? "left" : b > 0 ? "right" : "center",
								vertical: 0 > h ? "top" : d > 0 ? "bottom" : "middle"
							};
						j > n && n > g(b + c) && (l.horizontal = "center"), k > o && o > g(d + h) && (l.vertical = "middle"), l.important = f(g(b), g(c)) > f(g(d), g(h)) ? "horizontal" : "vertical", e.using.call(this, a, l)
					}), i.offset(a.extend(z, {
						using: h
					}))
				})
			}, a.ui.position = {
				fit: {
					left: function (a, b) {
						var c, d = b.within,
							e = d.isWindow ? d.scrollLeft : d.offset.left,
							g = d.width,
							h = a.left - b.collisionPosition.marginLeft,
							i = e - h,
							j = h + b.collisionWidth - g - e;
						b.collisionWidth > g ? i > 0 && 0 >= j ? (c = a.left + i + b.collisionWidth - g - e, a.left += i - c) : a.left = j > 0 && 0 >= i ? e : i > j ? e + g - b.collisionWidth : e : i > 0 ? a.left += i : j > 0 ? a.left -= j : a.left = f(a.left - h, a.left)
					},
					top: function (a, b) {
						var c, d = b.within,
							e = d.isWindow ? d.scrollTop : d.offset.top,
							g = b.within.height,
							h = a.top - b.collisionPosition.marginTop,
							i = e - h,
							j = h + b.collisionHeight - g - e;
						b.collisionHeight > g ? i > 0 && 0 >= j ? (c = a.top + i + b.collisionHeight - g - e, a.top += i - c) : a.top = j > 0 && 0 >= i ? e : i > j ? e + g - b.collisionHeight : e : i > 0 ? a.top += i : j > 0 ? a.top -= j : a.top = f(a.top - h, a.top)
					}
				},
				flip: {
					left: function (a, b) {
						var c, d, e = b.within,
							f = e.offset.left + e.scrollLeft,
							h = e.width,
							i = e.isWindow ? e.scrollLeft : e.offset.left,
							j = a.left - b.collisionPosition.marginLeft,
							k = j - i,
							l = j + b.collisionWidth - h - i,
							m = "left" === b.my[0] ? -b.elemWidth : "right" === b.my[0] ? b.elemWidth : 0,
							n = "left" === b.at[0] ? b.targetWidth : "right" === b.at[0] ? -b.targetWidth : 0,
							o = -2 * b.offset[0];
						0 > k ? (0 > (c = a.left + m + n + o + b.collisionWidth - h - f) || g(k) > c) && (a.left += m + n + o) : l > 0 && ((d = a.left - b.collisionPosition.marginLeft + m + n + o - i) > 0 || l > g(d)) && (a.left += m + n + o)
					},
					top: function (a, b) {
						var c, d, e = b.within,
							f = e.offset.top + e.scrollTop,
							h = e.height,
							i = e.isWindow ? e.scrollTop : e.offset.top,
							j = a.top - b.collisionPosition.marginTop,
							k = j - i,
							l = j + b.collisionHeight - h - i,
							m = "top" === b.my[1],
							n = m ? -b.elemHeight : "bottom" === b.my[1] ? b.elemHeight : 0,
							o = "top" === b.at[1] ? b.targetHeight : "bottom" === b.at[1] ? -b.targetHeight : 0,
							p = -2 * b.offset[1];
						0 > k ? (0 > (d = a.top + n + o + p + b.collisionHeight - h - f) || g(k) > d) && (a.top += n + o + p) : l > 0 && ((c = a.top - b.collisionPosition.marginTop + n + o + p - i) > 0 || l > g(c)) && (a.top += n + o + p)
					}
				},
				flipfit: {
					left: function () {
						a.ui.position.flip.left.apply(this, arguments), a.ui.position.fit.left.apply(this, arguments)
					},
					top: function () {
						a.ui.position.flip.top.apply(this, arguments), a.ui.position.fit.top.apply(this, arguments)
					}
				}
			}
		}(), a.ui.position, a.extend(a.expr[":"], {
			data: a.expr.createPseudo ? a.expr.createPseudo(function (b) {
				return function (c) {
					return !!a.data(c, b)
				}
			}) : function (b, c, d) {
				return !!a.data(b, d[3])
			}
		}), a.fn.extend({
			disableSelection: function () {
				var a = "onselectstart" in document.createElement("div") ? "selectstart" : "mousedown";
				return function () {
					return this.on(a + ".ui-disableSelection", function (a) {
						a.preventDefault()
					})
				}
			}(),
			enableSelection: function () {
				return this.off(".ui-disableSelection")
			}
		});
	var k = "ui-effects-",
		l = "ui-effects-style",
		m = "ui-effects-animated",
		n = a;
	a.effects = {
			effect: {}
		},
		function (a, b) {
			function c(a, b, c) {
				var d = k[b.type] || {};
				return null == a ? c || !b.def ? null : b.def : (a = d.floor ? ~~a : parseFloat(a), isNaN(a) ? b.def : d.mod ? (a + d.mod) % d.mod : 0 > a ? 0 : a > d.max ? d.max : a)
			}

			function d(c) {
				var d = i(),
					e = d._rgba = [];
				return c = c.toLowerCase(), n(h, function (a, f) {
					var g, h = f.re.exec(c),
						i = h && f.parse(h),
						k = f.space || "rgba";
					return i ? (g = d[k](i), d[j[k].cache] = g[j[k].cache], e = d._rgba = g._rgba, !1) : b
				}), e.length ? ("0,0,0,0" === e.join() && a.extend(e, f.transparent), d) : f[c]
			}

			function e(a, b, c) {
				return c = (c + 1) % 1, 1 > 6 * c ? a + 6 * (b - a) * c : 1 > 2 * c ? b : 2 > 3 * c ? a + 6 * (b - a) * (2 / 3 - c) : a
			}
			var f, g = /^([\-+])=\s*(\d+\.?\d*)/,
				h = [{
					re: /rgba?\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
					parse: function (a) {
						return [a[1], a[2], a[3], a[4]]
					}
				}, {
					re: /rgba?\(\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
					parse: function (a) {
						return [2.55 * a[1], 2.55 * a[2], 2.55 * a[3], a[4]]
					}
				}, {
					re: /#([a-f0-9]{2})([a-f0-9]{2})([a-f0-9]{2})/,
					parse: function (a) {
						return [parseInt(a[1], 16), parseInt(a[2], 16), parseInt(a[3], 16)]
					}
				}, {
					re: /#([a-f0-9])([a-f0-9])([a-f0-9])/,
					parse: function (a) {
						return [parseInt(a[1] + a[1], 16), parseInt(a[2] + a[2], 16), parseInt(a[3] + a[3], 16)]
					}
				}, {
					re: /hsla?\(\s*(\d+(?:\.\d+)?)\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
					space: "hsla",
					parse: function (a) {
						return [a[1], a[2] / 100, a[3] / 100, a[4]]
					}
				}],
				i = a.Color = function (b, c, d, e) {
					return new a.Color.fn.parse(b, c, d, e)
				},
				j = {
					rgba: {
						props: {
							red: {
								idx: 0,
								type: "byte"
							},
							green: {
								idx: 1,
								type: "byte"
							},
							blue: {
								idx: 2,
								type: "byte"
							}
						}
					},
					hsla: {
						props: {
							hue: {
								idx: 0,
								type: "degrees"
							},
							saturation: {
								idx: 1,
								type: "percent"
							},
							lightness: {
								idx: 2,
								type: "percent"
							}
						}
					}
				},
				k = {
					byte: {
						floor: !0,
						max: 255
					},
					percent: {
						max: 1
					},
					degrees: {
						mod: 360,
						floor: !0
					}
				},
				l = i.support = {},
				m = a("<p>")[0],
				n = a.each;
			m.style.cssText = "background-color:rgba(1,1,1,.5)", l.rgba = m.style.backgroundColor.indexOf("rgba") > -1, n(j, function (a, b) {
				b.cache = "_" + a, b.props.alpha = {
					idx: 3,
					type: "percent",
					def: 1
				}
			}), i.fn = a.extend(i.prototype, {
				parse: function (e, g, h, k) {
					if (e === b) return this._rgba = [null, null, null, null], this;
					(e.jquery || e.nodeType) && (e = a(e).css(g), g = b);
					var l = this,
						m = a.type(e),
						o = this._rgba = [];
					return g !== b && (e = [e, g, h, k], m = "array"), "string" === m ? this.parse(d(e) || f._default) : "array" === m ? (n(j.rgba.props, function (a, b) {
						o[b.idx] = c(e[b.idx], b)
					}), this) : "object" === m ? (e instanceof i ? n(j, function (a, b) {
						e[b.cache] && (l[b.cache] = e[b.cache].slice())
					}) : n(j, function (b, d) {
						var f = d.cache;
						n(d.props, function (a, b) {
							if (!l[f] && d.to) {
								if ("alpha" === a || null == e[a]) return;
								l[f] = d.to(l._rgba)
							}
							l[f][b.idx] = c(e[a], b, !0)
						}), l[f] && 0 > a.inArray(null, l[f].slice(0, 3)) && (l[f][3] = 1, d.from && (l._rgba = d.from(l[f])))
					}), this) : b
				},
				is: function (a) {
					var c = i(a),
						d = !0,
						e = this;
					return n(j, function (a, f) {
						var g, h = c[f.cache];
						return h && (g = e[f.cache] || f.to && f.to(e._rgba) || [], n(f.props, function (a, c) {
							return null != h[c.idx] ? d = h[c.idx] === g[c.idx] : b
						})), d
					}), d
				},
				_space: function () {
					var a = [],
						b = this;
					return n(j, function (c, d) {
						b[d.cache] && a.push(c)
					}), a.pop()
				},
				transition: function (a, b) {
					var d = i(a),
						e = d._space(),
						f = j[e],
						g = 0 === this.alpha() ? i("transparent") : this,
						h = g[f.cache] || f.to(g._rgba),
						l = h.slice();
					return d = d[f.cache], n(f.props, function (a, e) {
						var f = e.idx,
							g = h[f],
							i = d[f],
							j = k[e.type] || {};
						null !== i && (null === g ? l[f] = i : (j.mod && (i - g > j.mod / 2 ? g += j.mod : g - i > j.mod / 2 && (g -= j.mod)), l[f] = c((i - g) * b + g, e)))
					}), this[e](l)
				},
				blend: function (b) {
					if (1 === this._rgba[3]) return this;
					var c = this._rgba.slice(),
						d = c.pop(),
						e = i(b)._rgba;
					return i(a.map(c, function (a, b) {
						return (1 - d) * e[b] + d * a
					}))
				},
				toRgbaString: function () {
					var b = "rgba(",
						c = a.map(this._rgba, function (a, b) {
							return null == a ? b > 2 ? 1 : 0 : a
						});
					return 1 === c[3] && (c.pop(), b = "rgb("), b + c.join() + ")"
				},
				toHslaString: function () {
					var b = "hsla(",
						c = a.map(this.hsla(), function (a, b) {
							return null == a && (a = b > 2 ? 1 : 0), b && 3 > b && (a = Math.round(100 * a) + "%"), a
						});
					return 1 === c[3] && (c.pop(), b = "hsl("), b + c.join() + ")"
				},
				toHexString: function (b) {
					var c = this._rgba.slice(),
						d = c.pop();
					return b && c.push(~~(255 * d)), "#" + a.map(c, function (a) {
						return a = (a || 0).toString(16), 1 === a.length ? "0" + a : a
					}).join("")
				},
				toString: function () {
					return 0 === this._rgba[3] ? "transparent" : this.toRgbaString()
				}
			}), i.fn.parse.prototype = i.fn, j.hsla.to = function (a) {
				if (null == a[0] || null == a[1] || null == a[2]) return [null, null, null, a[3]];
				var b, c, d = a[0] / 255,
					e = a[1] / 255,
					f = a[2] / 255,
					g = a[3],
					h = Math.max(d, e, f),
					i = Math.min(d, e, f),
					j = h - i,
					k = h + i,
					l = .5 * k;
				return b = i === h ? 0 : d === h ? 60 * (e - f) / j + 360 : e === h ? 60 * (f - d) / j + 120 : 60 * (d - e) / j + 240, c = 0 === j ? 0 : .5 >= l ? j / k : j / (2 - k), [Math.round(b) % 360, c, l, null == g ? 1 : g]
			}, j.hsla.from = function (a) {
				if (null == a[0] || null == a[1] || null == a[2]) return [null, null, null, a[3]];
				var b = a[0] / 360,
					c = a[1],
					d = a[2],
					f = a[3],
					g = .5 >= d ? d * (1 + c) : d + c - d * c,
					h = 2 * d - g;
				return [Math.round(255 * e(h, g, b + 1 / 3)), Math.round(255 * e(h, g, b)), Math.round(255 * e(h, g, b - 1 / 3)), f]
			}, n(j, function (d, e) {
				var f = e.props,
					h = e.cache,
					j = e.to,
					k = e.from;
				i.fn[d] = function (d) {
					if (j && !this[h] && (this[h] = j(this._rgba)), d === b) return this[h].slice();
					var e, g = a.type(d),
						l = "array" === g || "object" === g ? d : arguments,
						m = this[h].slice();
					return n(f, function (a, b) {
						var d = l["object" === g ? a : b.idx];
						null == d && (d = m[b.idx]), m[b.idx] = c(d, b)
					}), k ? (e = i(k(m)), e[h] = m, e) : i(m)
				}, n(f, function (b, c) {
					i.fn[b] || (i.fn[b] = function (e) {
						var f, h = a.type(e),
							i = "alpha" === b ? this._hsla ? "hsla" : "rgba" : d,
							j = this[i](),
							k = j[c.idx];
						return "undefined" === h ? k : ("function" === h && (e = e.call(this, k), h = a.type(e)), null == e && c.empty ? this : ("string" === h && (f = g.exec(e)) && (e = k + parseFloat(f[2]) * ("+" === f[1] ? 1 : -1)), j[c.idx] = e, this[i](j)))
					})
				})
			}), i.hook = function (b) {
				var c = b.split(" ");
				n(c, function (b, c) {
					a.cssHooks[c] = {
						set: function (b, e) {
							var f, g, h = "";
							if ("transparent" !== e && ("string" !== a.type(e) || (f = d(e)))) {
								if (e = i(f || e), !l.rgba && 1 !== e._rgba[3]) {
									for (g = "backgroundColor" === c ? b.parentNode : b;
										("" === h || "transparent" === h) && g && g.style;) try {
										h = a.css(g, "backgroundColor"), g = g.parentNode
									} catch (a) {}
									e = e.blend(h && "transparent" !== h ? h : "_default")
								}
								e = e.toRgbaString()
							}
							try {
								b.style[c] = e
							} catch (a) {}
						}
					}, a.fx.step[c] = function (b) {
						b.colorInit || (b.start = i(b.elem, c), b.end = i(b.end), b.colorInit = !0), a.cssHooks[c].set(b.elem, b.start.transition(b.end, b.pos))
					}
				})
			}, i.hook("backgroundColor borderBottomColor borderLeftColor borderRightColor borderTopColor color columnRuleColor outlineColor textDecorationColor textEmphasisColor"), a.cssHooks.borderColor = {
				expand: function (a) {
					var b = {};
					return n(["Top", "Right", "Bottom", "Left"], function (c, d) {
						b["border" + d + "Color"] = a
					}), b
				}
			}, f = a.Color.names = {
				aqua: "#00ffff",
				black: "#000000",
				blue: "#0000ff",
				fuchsia: "#ff00ff",
				gray: "#808080",
				green: "#008000",
				lime: "#00ff00",
				maroon: "#800000",
				navy: "#000080",
				olive: "#808000",
				purple: "#800080",
				red: "#ff0000",
				silver: "#c0c0c0",
				teal: "#008080",
				white: "#ffffff",
				yellow: "#ffff00",
				transparent: [null, null, null, 0],
				_default: "#ffffff"
			}
		}(n),
		function () {
			function b(b) {
				var c, d, e = b.ownerDocument.defaultView ? b.ownerDocument.defaultView.getComputedStyle(b, null) : b.currentStyle,
					f = {};
				if (e && e.length && e[0] && e[e[0]])
					for (d = e.length; d--;) c = e[d], "string" == typeof e[c] && (f[a.camelCase(c)] = e[c]);
				else
					for (c in e) "string" == typeof e[c] && (f[c] = e[c]);
				return f
			}

			function c(b, c) {
				var d, f, g = {};
				for (d in c) f = c[d], b[d] !== f && (e[d] || (a.fx.step[d] || !isNaN(parseFloat(f))) && (g[d] = f));
				return g
			}
			var d = ["add", "remove", "toggle"],
				e = {
					border: 1,
					borderBottom: 1,
					borderColor: 1,
					borderLeft: 1,
					borderRight: 1,
					borderTop: 1,
					borderWidth: 1,
					margin: 1,
					padding: 1
				};
			a.each(["borderLeftStyle", "borderRightStyle", "borderBottomStyle", "borderTopStyle"], function (b, c) {
				a.fx.step[c] = function (a) {
					("none" !== a.end && !a.setAttr || 1 === a.pos && !a.setAttr) && (n.style(a.elem, c, a.end), a.setAttr = !0)
				}
			}), a.fn.addBack || (a.fn.addBack = function (a) {
				return this.add(null == a ? this.prevObject : this.prevObject.filter(a))
			}), a.effects.animateClass = function (e, f, g, h) {
				var i = a.speed(f, g, h);
				return this.queue(function () {
					var f, g = a(this),
						h = g.attr("class") || "",
						j = i.children ? g.find("*").addBack() : g;
					j = j.map(function () {
						return {
							el: a(this),
							start: b(this)
						}
					}), f = function () {
						a.each(d, function (a, b) {
							e[b] && g[b + "Class"](e[b])
						})
					}, f(), j = j.map(function () {
						return this.end = b(this.el[0]), this.diff = c(this.start, this.end), this
					}), g.attr("class", h), j = j.map(function () {
						var b = this,
							c = a.Deferred(),
							d = a.extend({}, i, {
								queue: !1,
								complete: function () {
									c.resolve(b)
								}
							});
						return this.el.animate(this.diff, d), c.promise()
					}), a.when.apply(a, j.get()).done(function () {
						f(), a.each(arguments, function () {
							var b = this.el;
							a.each(this.diff, function (a) {
								b.css(a, "")
							})
						}), i.complete.call(g[0])
					})
				})
			}, a.fn.extend({
				addClass: function (b) {
					return function (c, d, e, f) {
						return d ? a.effects.animateClass.call(this, {
							add: c
						}, d, e, f) : b.apply(this, arguments)
					}
				}(a.fn.addClass),
				removeClass: function (b) {
					return function (c, d, e, f) {
						return arguments.length > 1 ? a.effects.animateClass.call(this, {
							remove: c
						}, d, e, f) : b.apply(this, arguments)
					}
				}(a.fn.removeClass),
				toggleClass: function (b) {
					return function (c, d, e, f, g) {
						return "boolean" == typeof d || void 0 === d ? e ? a.effects.animateClass.call(this, d ? {
							add: c
						} : {
							remove: c
						}, e, f, g) : b.apply(this, arguments) : a.effects.animateClass.call(this, {
							toggle: c
						}, d, e, f)
					}
				}(a.fn.toggleClass),
				switchClass: function (b, c, d, e, f) {
					return a.effects.animateClass.call(this, {
						add: c,
						remove: b
					}, d, e, f)
				}
			})
		}(),
		function () {
			function b(b, c, d, e) {
				return a.isPlainObject(b) && (c = b, b = b.effect), b = {
					effect: b
				}, null == c && (c = {}), a.isFunction(c) && (e = c, d = null, c = {}), ("number" == typeof c || a.fx.speeds[c]) && (e = d, d = c, c = {}), a.isFunction(d) && (e = d, d = null), c && a.extend(b, c), d = d || c.duration, b.duration = a.fx.off ? 0 : "number" == typeof d ? d : d in a.fx.speeds ? a.fx.speeds[d] : a.fx.speeds._default, b.complete = e || c.complete, b
			}

			function c(b) {
				return !(b && "number" != typeof b && !a.fx.speeds[b]) || ("string" == typeof b && !a.effects.effect[b] || (!!a.isFunction(b) || "object" == typeof b && !b.effect))
			}

			function d(a, b) {
				var c = b.outerWidth(),
					d = b.outerHeight(),
					e = /^rect\((-?\d*\.?\d*px|-?\d+%|auto),?\s*(-?\d*\.?\d*px|-?\d+%|auto),?\s*(-?\d*\.?\d*px|-?\d+%|auto),?\s*(-?\d*\.?\d*px|-?\d+%|auto)\)$/,
					f = e.exec(a) || ["", 0, c, d, 0];
				return {
					top: parseFloat(f[1]) || 0,
					right: "auto" === f[2] ? c : parseFloat(f[2]),
					bottom: "auto" === f[3] ? d : parseFloat(f[3]),
					left: parseFloat(f[4]) || 0
				}
			}
			a.expr && a.expr.filters && a.expr.filters.animated && (a.expr.filters.animated = function (b) {
				return function (c) {
					return !!a(c).data(m) || b(c)
				}
			}(a.expr.filters.animated)), !1 !== a.uiBackCompat && a.extend(a.effects, {
				save: function (a, b) {
					for (var c = 0, d = b.length; d > c; c++) null !== b[c] && a.data(k + b[c], a[0].style[b[c]])
				},
				restore: function (a, b) {
					for (var c, d = 0, e = b.length; e > d; d++) null !== b[d] && (c = a.data(k + b[d]), a.css(b[d], c))
				},
				setMode: function (a, b) {
					return "toggle" === b && (b = a.is(":hidden") ? "show" : "hide"), b
				},
				createWrapper: function (b) {
					if (b.parent().is(".ui-effects-wrapper")) return b.parent();
					var c = {
							width: b.outerWidth(!0),
							height: b.outerHeight(!0),
							float: b.css("float")
						},
						d = a("<div></div>").addClass("ui-effects-wrapper").css({
							fontSize: "100%",
							background: "transparent",
							border: "none",
							margin: 0,
							padding: 0
						}),
						e = {
							width: b.width(),
							height: b.height()
						},
						f = document.activeElement;
					try {
						f.id
					} catch (a) {
						f = document.body
					}
					return b.wrap(d), (b[0] === f || a.contains(b[0], f)) && a(f).trigger("focus"), d = b.parent(), "static" === b.css("position") ? (d.css({
						position: "relative"
					}), b.css({
						position: "relative"
					})) : (a.extend(c, {
						position: b.css("position"),
						zIndex: b.css("z-index")
					}), a.each(["top", "left", "bottom", "right"], function (a, d) {
						c[d] = b.css(d), isNaN(parseInt(c[d], 10)) && (c[d] = "auto")
					}), b.css({
						position: "relative",
						top: 0,
						left: 0,
						right: "auto",
						bottom: "auto"
					})), b.css(e), d.css(c).show()
				},
				removeWrapper: function (b) {
					var c = document.activeElement;
					return b.parent().is(".ui-effects-wrapper") && (b.parent().replaceWith(b), (b[0] === c || a.contains(b[0], c)) && a(c).trigger("focus")), b
				}
			}), a.extend(a.effects, {
				version: "1.12.1",
				define: function (b, c, d) {
					return d || (d = c, c = "effect"), a.effects.effect[b] = d, a.effects.effect[b].mode = c, d
				},
				scaledDimensions: function (a, b, c) {
					if (0 === b) return {
						height: 0,
						width: 0,
						outerHeight: 0,
						outerWidth: 0
					};
					var d = "horizontal" !== c ? (b || 100) / 100 : 1,
						e = "vertical" !== c ? (b || 100) / 100 : 1;
					return {
						height: a.height() * e,
						width: a.width() * d,
						outerHeight: a.outerHeight() * e,
						outerWidth: a.outerWidth() * d
					}
				},
				clipToBox: function (a) {
					return {
						width: a.clip.right - a.clip.left,
						height: a.clip.bottom - a.clip.top,
						left: a.clip.left,
						top: a.clip.top
					}
				},
				unshift: function (a, b, c) {
					var d = a.queue();
					b > 1 && d.splice.apply(d, [1, 0].concat(d.splice(b, c))), a.dequeue()
				},
				saveStyle: function (a) {
					a.data(l, a[0].style.cssText)
				},
				restoreStyle: function (a) {
					a[0].style.cssText = a.data(l) || "", a.removeData(l)
				},
				mode: function (a, b) {
					var c = a.is(":hidden");
					return "toggle" === b && (b = c ? "show" : "hide"), (c ? "hide" === b : "show" === b) && (b = "none"), b
				},
				getBaseline: function (a, b) {
					var c, d;
					switch (a[0]) {
						case "top":
							c = 0;
							break;
						case "middle":
							c = .5;
							break;
						case "bottom":
							c = 1;
							break;
						default:
							c = a[0] / b.height
					}
					switch (a[1]) {
						case "left":
							d = 0;
							break;
						case "center":
							d = .5;
							break;
						case "right":
							d = 1;
							break;
						default:
							d = a[1] / b.width
					}
					return {
						x: d,
						y: c
					}
				},
				createPlaceholder: function (b) {
					var c, d = b.css("position"),
						e = b.position();
					return b.css({
						marginTop: b.css("marginTop"),
						marginBottom: b.css("marginBottom"),
						marginLeft: b.css("marginLeft"),
						marginRight: b.css("marginRight")
					}).outerWidth(b.outerWidth()).outerHeight(b.outerHeight()), /^(static|relative)/.test(d) && (d = "absolute", c = a("<" + b[0].nodeName + ">").insertAfter(b).css({
						display: /^(inline|ruby)/.test(b.css("display")) ? "inline-block" : "block",
						visibility: "hidden",
						marginTop: b.css("marginTop"),
						marginBottom: b.css("marginBottom"),
						marginLeft: b.css("marginLeft"),
						marginRight: b.css("marginRight"),
						float: b.css("float")
					}).outerWidth(b.outerWidth()).outerHeight(b.outerHeight()).addClass("ui-effects-placeholder"), b.data(k + "placeholder", c)), b.css({
						position: d,
						left: e.left,
						top: e.top
					}), c
				},
				removePlaceholder: function (a) {
					var b = k + "placeholder",
						c = a.data(b);
					c && (c.remove(), a.removeData(b))
				},
				cleanUp: function (b) {
					a.effects.restoreStyle(b), a.effects.removePlaceholder(b)
				},
				setTransition: function (b, c, d, e) {
					return e = e || {}, a.each(c, function (a, c) {
						var f = b.cssUnit(c);
						f[0] > 0 && (e[c] = f[0] * d + f[1])
					}), e
				}
			}), a.fn.extend({
				effect: function () {
					function c(b) {
						function c() {
							h.removeData(m), a.effects.cleanUp(h), "hide" === d.mode && h.hide(), g()
						}

						function g() {
							a.isFunction(i) && i.call(h[0]), a.isFunction(b) && b()
						}
						var h = a(this);
						d.mode = k.shift(), !1 === a.uiBackCompat || f ? "none" === d.mode ? (h[j](), g()) : e.call(h[0], d, c) : (h.is(":hidden") ? "hide" === j : "show" === j) ? (h[j](), g()) : e.call(h[0], d, g)
					}
					var d = b.apply(this, arguments),
						e = a.effects.effect[d.effect],
						f = e.mode,
						g = d.queue,
						h = g || "fx",
						i = d.complete,
						j = d.mode,
						k = [],
						l = function (b) {
							var c = a(this),
								d = a.effects.mode(c, j) || f;
							c.data(m, !0), k.push(d), f && ("show" === d || d === f && "hide" === d) && c.show(), f && "none" === d || a.effects.saveStyle(c), a.isFunction(b) && b()
						};
					return a.fx.off || !e ? j ? this[j](d.duration, i) : this.each(function () {
						i && i.call(this)
					}) : !1 === g ? this.each(l).each(c) : this.queue(h, l).queue(h, c)
				},
				show: function (a) {
					return function (d) {
						if (c(d)) return a.apply(this, arguments);
						var e = b.apply(this, arguments);
						return e.mode = "show", this.effect.call(this, e)
					}
				}(a.fn.show),
				hide: function (a) {
					return function (d) {
						if (c(d)) return a.apply(this, arguments);
						var e = b.apply(this, arguments);
						return e.mode = "hide", this.effect.call(this, e)
					}
				}(a.fn.hide),
				toggle: function (a) {
					return function (d) {
						if (c(d) || "boolean" == typeof d) return a.apply(this, arguments);
						var e = b.apply(this, arguments);
						return e.mode = "toggle", this.effect.call(this, e)
					}
				}(a.fn.toggle),
				cssUnit: function (b) {
					var c = this.css(b),
						d = [];
					return a.each(["em", "px", "%", "pt"], function (a, b) {
						c.indexOf(b) > 0 && (d = [parseFloat(c), b])
					}), d
				},
				cssClip: function (a) {
					return a ? this.css("clip", "rect(" + a.top + "px " + a.right + "px " + a.bottom + "px " + a.left + "px)") : d(this.css("clip"), this)
				},
				transfer: function (b, c) {
					var d = a(this),
						e = a(b.to),
						f = "fixed" === e.css("position"),
						g = a("body"),
						h = f ? g.scrollTop() : 0,
						i = f ? g.scrollLeft() : 0,
						j = e.offset(),
						k = {
							top: j.top - h,
							left: j.left - i,
							height: e.innerHeight(),
							width: e.innerWidth()
						},
						l = d.offset(),
						m = a("<div class='ui-effects-transfer'></div>").appendTo("body").addClass(b.className).css({
							top: l.top - h,
							left: l.left - i,
							height: d.innerHeight(),
							width: d.innerWidth(),
							position: f ? "fixed" : "absolute"
						}).animate(k, b.duration, b.easing, function () {
							m.remove(), a.isFunction(c) && c()
						})
				}
			}), a.fx.step.clip = function (b) {
				b.clipInit || (b.start = a(b.elem).cssClip(), "string" == typeof b.end && (b.end = d(b.end, b.elem)), b.clipInit = !0), a(b.elem).cssClip({
					top: b.pos * (b.end.top - b.start.top) + b.start.top,
					right: b.pos * (b.end.right - b.start.right) + b.start.right,
					bottom: b.pos * (b.end.bottom - b.start.bottom) + b.start.bottom,
					left: b.pos * (b.end.left - b.start.left) + b.start.left
				})
			}
		}(),
		function () {
			var b = {};
			a.each(["Quad", "Cubic", "Quart", "Quint", "Expo"], function (a, c) {
				b[c] = function (b) {
					return Math.pow(b, a + 2)
				}
			}), a.extend(b, {
				Sine: function (a) {
					return 1 - Math.cos(a * Math.PI / 2)
				},
				Circ: function (a) {
					return 1 - Math.sqrt(1 - a * a)
				},
				Elastic: function (a) {
					return 0 === a || 1 === a ? a : -Math.pow(2, 8 * (a - 1)) * Math.sin((80 * (a - 1) - 7.5) * Math.PI / 15)
				},
				Back: function (a) {
					return a * a * (3 * a - 2)
				},
				Bounce: function (a) {
					for (var b, c = 4;
						((b = Math.pow(2, --c)) - 1) / 11 > a;);
					return 1 / Math.pow(4, 3 - c) - 7.5625 * Math.pow((3 * b - 2) / 22 - a, 2)
				}
			}), a.each(b, function (b, c) {
				a.easing["easeIn" + b] = c, a.easing["easeOut" + b] = function (a) {
					return 1 - c(1 - a)
				}, a.easing["easeInOut" + b] = function (a) {
					return .5 > a ? c(2 * a) / 2 : 1 - c(-2 * a + 2) / 2
				}
			})
		}();
	a.effects;
	a.effects.define("blind", "hide", function (b, c) {
		var d = {
				up: ["bottom", "top"],
				vertical: ["bottom", "top"],
				down: ["top", "bottom"],
				left: ["right", "left"],
				horizontal: ["right", "left"],
				right: ["left", "right"]
			},
			e = a(this),
			f = b.direction || "up",
			g = e.cssClip(),
			h = {
				clip: a.extend({}, g)
			},
			i = a.effects.createPlaceholder(e);
		h.clip[d[f][0]] = h.clip[d[f][1]], "show" === b.mode && (e.cssClip(h.clip), i && i.css(a.effects.clipToBox(h)), h.clip = g), i && i.animate(a.effects.clipToBox(h), b.duration, b.easing), e.animate(h, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	}), a.effects.define("bounce", function (b, c) {
		var d, e, f, g = a(this),
			h = b.mode,
			i = "hide" === h,
			j = "show" === h,
			k = b.direction || "up",
			l = b.distance,
			m = b.times || 5,
			n = 2 * m + (j || i ? 1 : 0),
			o = b.duration / n,
			p = b.easing,
			q = "up" === k || "down" === k ? "top" : "left",
			r = "up" === k || "left" === k,
			s = 0,
			t = g.queue().length;
		for (a.effects.createPlaceholder(g), f = g.css(q), l || (l = g["top" === q ? "outerHeight" : "outerWidth"]() / 3), j && (e = {
				opacity: 1
			}, e[q] = f, g.css("opacity", 0).css(q, r ? 2 * -l : 2 * l).animate(e, o, p)), i && (l /= Math.pow(2, m - 1)), e = {}, e[q] = f; m > s; s++) d = {}, d[q] = (r ? "-=" : "+=") + l, g.animate(d, o, p).animate(e, o, p), l = i ? 2 * l : l / 2;
		i && (d = {
			opacity: 0
		}, d[q] = (r ? "-=" : "+=") + l, g.animate(d, o, p)), g.queue(c), a.effects.unshift(g, t, n + 1)
	}), a.effects.define("clip", "hide", function (b, c) {
		var d, e = {},
			f = a(this),
			g = b.direction || "vertical",
			h = "both" === g,
			i = h || "horizontal" === g,
			j = h || "vertical" === g;
		d = f.cssClip(), e.clip = {
			top: j ? (d.bottom - d.top) / 2 : d.top,
			right: i ? (d.right - d.left) / 2 : d.right,
			bottom: j ? (d.bottom - d.top) / 2 : d.bottom,
			left: i ? (d.right - d.left) / 2 : d.left
		}, a.effects.createPlaceholder(f), "show" === b.mode && (f.cssClip(e.clip), e.clip = d), f.animate(e, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	}), a.effects.define("drop", "hide", function (b, c) {
		var d, e = a(this),
			f = b.mode,
			g = "show" === f,
			h = b.direction || "left",
			i = "up" === h || "down" === h ? "top" : "left",
			j = "up" === h || "left" === h ? "-=" : "+=",
			k = "+=" === j ? "-=" : "+=",
			l = {
				opacity: 0
			};
		a.effects.createPlaceholder(e), d = b.distance || e["top" === i ? "outerHeight" : "outerWidth"](!0) / 2, l[i] = j + d, g && (e.css(l), l[i] = k + d, l.opacity = 1), e.animate(l, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	}), a.effects.define("explode", "hide", function (b, c) {
		function d() {
			t.push(this), t.length === l * m && e()
		}

		function e() {
			n.css({
				visibility: "visible"
			}), a(t).remove(), c()
		}
		var f, g, h, i, j, k, l = b.pieces ? Math.round(Math.sqrt(b.pieces)) : 3,
			m = l,
			n = a(this),
			o = b.mode,
			p = "show" === o,
			q = n.show().css("visibility", "hidden").offset(),
			r = Math.ceil(n.outerWidth() / m),
			s = Math.ceil(n.outerHeight() / l),
			t = [];
		for (f = 0; l > f; f++)
			for (i = q.top + f * s, k = f - (l - 1) / 2, g = 0; m > g; g++) h = q.left + g * r, j = g - (m - 1) / 2, n.clone().appendTo("body").wrap("<div></div>").css({
				position: "absolute",
				visibility: "visible",
				left: -g * r,
				top: -f * s
			}).parent().addClass("ui-effects-explode").css({
				position: "absolute",
				overflow: "hidden",
				width: r,
				height: s,
				left: h + (p ? j * r : 0),
				top: i + (p ? k * s : 0),
				opacity: p ? 0 : 1
			}).animate({
				left: h + (p ? 0 : j * r),
				top: i + (p ? 0 : k * s),
				opacity: p ? 1 : 0
			}, b.duration || 500, b.easing, d)
	}), a.effects.define("fade", "toggle", function (b, c) {
		var d = "show" === b.mode;
		a(this).css("opacity", d ? 0 : 1).animate({
			opacity: d ? 1 : 0
		}, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	}), a.effects.define("fold", "hide", function (b, c) {
		var d = a(this),
			e = b.mode,
			f = "show" === e,
			g = "hide" === e,
			h = b.size || 15,
			i = /([0-9]+)%/.exec(h),
			j = !!b.horizFirst,
			k = j ? ["right", "bottom"] : ["bottom", "right"],
			l = b.duration / 2,
			m = a.effects.createPlaceholder(d),
			n = d.cssClip(),
			o = {
				clip: a.extend({}, n)
			},
			p = {
				clip: a.extend({}, n)
			},
			q = [n[k[0]], n[k[1]]],
			r = d.queue().length;
		i && (h = parseInt(i[1], 10) / 100 * q[g ? 0 : 1]), o.clip[k[0]] = h, p.clip[k[0]] = h, p.clip[k[1]] = 0, f && (d.cssClip(p.clip), m && m.css(a.effects.clipToBox(p)), p.clip = n), d.queue(function (c) {
			m && m.animate(a.effects.clipToBox(o), l, b.easing).animate(a.effects.clipToBox(p), l, b.easing), c()
		}).animate(o, l, b.easing).animate(p, l, b.easing).queue(c), a.effects.unshift(d, r, 4)
	}), a.effects.define("highlight", "show", function (b, c) {
		var d = a(this),
			e = {
				backgroundColor: d.css("backgroundColor")
			};
		"hide" === b.mode && (e.opacity = 0), a.effects.saveStyle(d), d.css({
			backgroundImage: "none",
			backgroundColor: b.color || "#ffff99"
		}).animate(e, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	}), a.effects.define("size", function (b, c) {
		var d, e, f, g = a(this),
			h = ["fontSize"],
			i = ["borderTopWidth", "borderBottomWidth", "paddingTop", "paddingBottom"],
			j = ["borderLeftWidth", "borderRightWidth", "paddingLeft", "paddingRight"],
			k = b.mode,
			l = "effect" !== k,
			m = b.scale || "both",
			n = b.origin || ["middle", "center"],
			o = g.css("position"),
			p = g.position(),
			q = a.effects.scaledDimensions(g),
			r = b.from || q,
			s = b.to || a.effects.scaledDimensions(g, 0);
		a.effects.createPlaceholder(g), "show" === k && (f = r, r = s, s = f), e = {
			from: {
				y: r.height / q.height,
				x: r.width / q.width
			},
			to: {
				y: s.height / q.height,
				x: s.width / q.width
			}
		}, ("box" === m || "both" === m) && (e.from.y !== e.to.y && (r = a.effects.setTransition(g, i, e.from.y, r), s = a.effects.setTransition(g, i, e.to.y, s)), e.from.x !== e.to.x && (r = a.effects.setTransition(g, j, e.from.x, r), s = a.effects.setTransition(g, j, e.to.x, s))), ("content" === m || "both" === m) && e.from.y !== e.to.y && (r = a.effects.setTransition(g, h, e.from.y, r), s = a.effects.setTransition(g, h, e.to.y, s)), n && (d = a.effects.getBaseline(n, q), r.top = (q.outerHeight - r.outerHeight) * d.y + p.top, r.left = (q.outerWidth - r.outerWidth) * d.x + p.left, s.top = (q.outerHeight - s.outerHeight) * d.y + p.top, s.left = (q.outerWidth - s.outerWidth) * d.x + p.left), g.css(r), ("content" === m || "both" === m) && (i = i.concat(["marginTop", "marginBottom"]).concat(h), j = j.concat(["marginLeft", "marginRight"]), g.find("*[width]").each(function () {
			var c = a(this),
				d = a.effects.scaledDimensions(c),
				f = {
					height: d.height * e.from.y,
					width: d.width * e.from.x,
					outerHeight: d.outerHeight * e.from.y,
					outerWidth: d.outerWidth * e.from.x
				},
				g = {
					height: d.height * e.to.y,
					width: d.width * e.to.x,
					outerHeight: d.height * e.to.y,
					outerWidth: d.width * e.to.x
				};
			e.from.y !== e.to.y && (f = a.effects.setTransition(c, i, e.from.y, f), g = a.effects.setTransition(c, i, e.to.y, g)), e.from.x !== e.to.x && (f = a.effects.setTransition(c, j, e.from.x, f), g = a.effects.setTransition(c, j, e.to.x, g)), l && a.effects.saveStyle(c), c.css(f), c.animate(g, b.duration, b.easing, function () {
				l && a.effects.restoreStyle(c)
			})
		})), g.animate(s, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: function () {
				var b = g.offset();
				0 === s.opacity && g.css("opacity", r.opacity), l || (g.css("position", "static" === o ? "relative" : o).offset(b), a.effects.saveStyle(g)), c()
			}
		})
	}), a.effects.define("scale", function (b, c) {
		var d = a(this),
			e = b.mode,
			f = parseInt(b.percent, 10) || (0 === parseInt(b.percent, 10) ? 0 : "effect" !== e ? 0 : 100),
			g = a.extend(!0, {
				from: a.effects.scaledDimensions(d),
				to: a.effects.scaledDimensions(d, f, b.direction || "both"),
				origin: b.origin || ["middle", "center"]
			}, b);
		b.fade && (g.from.opacity = 1, g.to.opacity = 0), a.effects.effect.size.call(this, g, c)
	}), a.effects.define("puff", "hide", function (b, c) {
		var d = a.extend(!0, {}, b, {
			fade: !0,
			percent: parseInt(b.percent, 10) || 150
		});
		a.effects.effect.scale.call(this, d, c)
	}), a.effects.define("pulsate", "show", function (b, c) {
		var d = a(this),
			e = b.mode,
			f = "show" === e,
			g = "hide" === e,
			h = f || g,
			i = 2 * (b.times || 5) + (h ? 1 : 0),
			j = b.duration / i,
			k = 0,
			l = 1,
			m = d.queue().length;
		for ((f || !d.is(":visible")) && (d.css("opacity", 0).show(), k = 1); i > l; l++) d.animate({
			opacity: k
		}, j, b.easing), k = 1 - k;
		d.animate({
			opacity: k
		}, j, b.easing), d.queue(c), a.effects.unshift(d, m, i + 1)
	}), a.effects.define("shake", function (b, c) {
		var d = 1,
			e = a(this),
			f = b.direction || "left",
			g = b.distance || 20,
			h = b.times || 3,
			i = 2 * h + 1,
			j = Math.round(b.duration / i),
			k = "up" === f || "down" === f ? "top" : "left",
			l = "up" === f || "left" === f,
			m = {},
			n = {},
			o = {},
			p = e.queue().length;
		for (a.effects.createPlaceholder(e), m[k] = (l ? "-=" : "+=") + g, n[k] = (l ? "+=" : "-=") + 2 * g, o[k] = (l ? "-=" : "+=") + 2 * g, e.animate(m, j, b.easing); h > d; d++) e.animate(n, j, b.easing).animate(o, j, b.easing);
		e.animate(n, j, b.easing).animate(m, j / 2, b.easing).queue(c), a.effects.unshift(e, p, i + 1)
	}), a.effects.define("slide", "show", function (b, c) {
		var d, e, f = a(this),
			g = {
				up: ["bottom", "top"],
				down: ["top", "bottom"],
				left: ["right", "left"],
				right: ["left", "right"]
			},
			h = b.mode,
			i = b.direction || "left",
			j = "up" === i || "down" === i ? "top" : "left",
			k = "up" === i || "left" === i,
			l = b.distance || f["top" === j ? "outerHeight" : "outerWidth"](!0),
			m = {};
		a.effects.createPlaceholder(f), d = f.cssClip(), e = f.position()[j], m[j] = (k ? -1 : 1) * l + e, m.clip = f.cssClip(), m.clip[g[i][1]] = m.clip[g[i][0]], "show" === h && (f.cssClip(m.clip), f.css(j, m[j]), m.clip = d, m[j] = e), f.animate(m, {
			queue: !1,
			duration: b.duration,
			easing: b.easing,
			complete: c
		})
	});
	!1 !== a.uiBackCompat && a.effects.define("transfer", function (b, c) {
			a(this).transfer(b, c)
		}), a.ui.focusable = function (c, d) {
			var e, f, g, h, i, j = c.nodeName.toLowerCase();
			return "area" === j ? (e = c.parentNode, f = e.name, !(!c.href || !f || "map" !== e.nodeName.toLowerCase()) && (g = a("img[usemap='#" + f + "']"), g.length > 0 && g.is(":visible"))) : (/^(input|select|textarea|button|object)$/.test(j) ? (h = !c.disabled) && (i = a(c).closest("fieldset")[0]) && (h = !i.disabled) : h = "a" === j ? c.href || d : d, h && a(c).is(":visible") && b(a(c)))
		}, a.extend(a.expr[":"], {
			focusable: function (b) {
				return a.ui.focusable(b, null != a.attr(b, "tabindex"))
			}
		}), a.ui.focusable, a.fn.form = function () {
			return "string" == typeof this[0].form ? this.closest("form") : a(this[0].form)
		}, a.ui.formResetMixin = {
			_formResetHandler: function () {
				var b = a(this);
				setTimeout(function () {
					var c = b.data("ui-form-reset-instances");
					a.each(c, function () {
						this.refresh()
					})
				})
			},
			_bindFormResetHandler: function () {
				if (this.form = this.element.form(), this.form.length) {
					var a = this.form.data("ui-form-reset-instances") || [];
					a.length || this.form.on("reset.ui-form-reset", this._formResetHandler), a.push(this), this.form.data("ui-form-reset-instances", a)
				}
			},
			_unbindFormResetHandler: function () {
				if (this.form.length) {
					var b = this.form.data("ui-form-reset-instances");
					b.splice(a.inArray(this, b), 1), b.length ? this.form.data("ui-form-reset-instances", b) : this.form.removeData("ui-form-reset-instances").off("reset.ui-form-reset")
				}
			}
		}, "1.7" === a.fn.jquery.substring(0, 3) && (a.each(["Width", "Height"], function (b, c) {
			function d(b, c, d, f) {
				return a.each(e, function () {
					c -= parseFloat(a.css(b, "padding" + this)) || 0, d && (c -= parseFloat(a.css(b, "border" + this + "Width")) || 0), f && (c -= parseFloat(a.css(b, "margin" + this)) || 0)
				}), c
			}
			var e = "Width" === c ? ["Left", "Right"] : ["Top", "Bottom"],
				f = c.toLowerCase(),
				g = {
					innerWidth: a.fn.innerWidth,
					innerHeight: a.fn.innerHeight,
					outerWidth: a.fn.outerWidth,
					outerHeight: a.fn.outerHeight
				};
			a.fn["inner" + c] = function (b) {
				return void 0 === b ? g["inner" + c].call(this) : this.each(function () {
					a(this).css(f, d(this, b) + "px")
				})
			}, a.fn["outer" + c] = function (b, e) {
				return "number" != typeof b ? g["outer" + c].call(this, b) : this.each(function () {
					a(this).css(f, d(this, b, !0, e) + "px")
				})
			}
		}), a.fn.addBack = function (a) {
			return this.add(null == a ? this.prevObject : this.prevObject.filter(a))
		}), a.ui.keyCode = {
			BACKSPACE: 8,
			COMMA: 188,
			DELETE: 46,
			DOWN: 40,
			END: 35,
			ENTER: 13,
			ESCAPE: 27,
			HOME: 36,
			LEFT: 37,
			PAGE_DOWN: 34,
			PAGE_UP: 33,
			PERIOD: 190,
			RIGHT: 39,
			SPACE: 32,
			TAB: 9,
			UP: 38
		},
		a.ui.escapeSelector = function () {
			var a = /([!"#$%&'()*+,.\/:;<=>?@[\]^`{|}~])/g;
			return function (b) {
				return b.replace(a, "\\$1")
			}
		}(), a.fn.labels = function () {
			var b, c, d, e, f;
			return this[0].labels && this[0].labels.length ? this.pushStack(this[0].labels) : (e = this.eq(0).parents("label"), d = this.attr("id"), d && (b = this.eq(0).parents().last(), f = b.add(b.length ? b.siblings() : this.siblings()), c = "label[for='" + a.ui.escapeSelector(d) + "']", e = e.add(f.find(c).addBack(c))), this.pushStack(e))
		}, a.fn.scrollParent = function (b) {
			var c = this.css("position"),
				d = "absolute" === c,
				e = b ? /(auto|scroll|hidden)/ : /(auto|scroll)/,
				f = this.parents().filter(function () {
					var b = a(this);
					return (!d || "static" !== b.css("position")) && e.test(b.css("overflow") + b.css("overflow-y") + b.css("overflow-x"))
				}).eq(0);
			return "fixed" !== c && f.length ? f : a(this[0].ownerDocument || document)
		}, a.extend(a.expr[":"], {
			tabbable: function (b) {
				var c = a.attr(b, "tabindex"),
					d = null != c;
				return (!d || c >= 0) && a.ui.focusable(b, d)
			}
		}), a.fn.extend({
			uniqueId: function () {
				var a = 0;
				return function () {
					return this.each(function () {
						this.id || (this.id = "ui-id-" + ++a)
					})
				}
			}(),
			removeUniqueId: function () {
				return this.each(function () {
					/^ui-id-\d+$/.test(this.id) && a(this).removeAttr("id")
				})
			}
		}), a.widget("ui.accordion", {
			version: "1.12.1",
			options: {
				active: 0,
				animate: {},
				classes: {
					"ui-accordion-header": "ui-corner-top",
					"ui-accordion-header-collapsed": "ui-corner-all",
					"ui-accordion-content": "ui-corner-bottom"
				},
				collapsible: !1,
				event: "click",
				header: "> li > :first-child, > :not(li):even",
				heightStyle: "auto",
				icons: {
					activeHeader: "ui-icon-triangle-1-s",
					header: "ui-icon-triangle-1-e"
				},
				activate: null,
				beforeActivate: null
			},
			hideProps: {
				borderTopWidth: "hide",
				borderBottomWidth: "hide",
				paddingTop: "hide",
				paddingBottom: "hide",
				height: "hide"
			},
			showProps: {
				borderTopWidth: "show",
				borderBottomWidth: "show",
				paddingTop: "show",
				paddingBottom: "show",
				height: "show"
			},
			_create: function () {
				var b = this.options;
				this.prevShow = this.prevHide = a(), this._addClass("ui-accordion", "ui-widget ui-helper-reset"), this.element.attr("role", "tablist"), b.collapsible || !1 !== b.active && null != b.active || (b.active = 0), this._processPanels(), 0 > b.active && (b.active += this.headers.length), this._refresh()
			},
			_getCreateEventData: function () {
				return {
					header: this.active,
					panel: this.active.length ? this.active.next() : a()
				}
			},
			_createIcons: function () {
				var b, c, d = this.options.icons;
				d && (b = a("<span>"), this._addClass(b, "ui-accordion-header-icon", "ui-icon " + d.header), b.prependTo(this.headers), c = this.active.children(".ui-accordion-header-icon"), this._removeClass(c, d.header)._addClass(c, null, d.activeHeader)._addClass(this.headers, "ui-accordion-icons"))
			},
			_destroyIcons: function () {
				this._removeClass(this.headers, "ui-accordion-icons"), this.headers.children(".ui-accordion-header-icon").remove()
			},
			_destroy: function () {
				var a;
				this.element.removeAttr("role"), this.headers.removeAttr("role aria-expanded aria-selected aria-controls tabIndex").removeUniqueId(), this._destroyIcons(), a = this.headers.next().css("display", "").removeAttr("role aria-hidden aria-labelledby").removeUniqueId(), "content" !== this.options.heightStyle && a.css("height", "")
			},
			_setOption: function (a, b) {
				return "active" === a ? void this._activate(b) : ("event" === a && (this.options.event && this._off(this.headers, this.options.event), this._setupEvents(b)), this._super(a, b), "collapsible" !== a || b || !1 !== this.options.active || this._activate(0), void("icons" === a && (this._destroyIcons(), b && this._createIcons())))
			},
			_setOptionDisabled: function (a) {
				this._super(a), this.element.attr("aria-disabled", a), this._toggleClass(null, "ui-state-disabled", !!a), this._toggleClass(this.headers.add(this.headers.next()), null, "ui-state-disabled", !!a)
			},
			_keydown: function (b) {
				if (!b.altKey && !b.ctrlKey) {
					var c = a.ui.keyCode,
						d = this.headers.length,
						e = this.headers.index(b.target),
						f = !1;
					switch (b.keyCode) {
						case c.RIGHT:
						case c.DOWN:
							f = this.headers[(e + 1) % d];
							break;
						case c.LEFT:
						case c.UP:
							f = this.headers[(e - 1 + d) % d];
							break;
						case c.SPACE:
						case c.ENTER:
							this._eventHandler(b);
							break;
						case c.HOME:
							f = this.headers[0];
							break;
						case c.END:
							f = this.headers[d - 1]
					}
					f && (a(b.target).attr("tabIndex", -1), a(f).attr("tabIndex", 0), a(f).trigger("focus"), b.preventDefault())
				}
			},
			_panelKeyDown: function (b) {
				b.keyCode === a.ui.keyCode.UP && b.ctrlKey && a(b.currentTarget).prev().trigger("focus")
			},
			refresh: function () {
				var b = this.options;
				this._processPanels(), !1 === b.active && !0 === b.collapsible || !this.headers.length ? (b.active = !1, this.active = a()) : !1 === b.active ? this._activate(0) : this.active.length && !a.contains(this.element[0], this.active[0]) ? this.headers.length === this.headers.find(".ui-state-disabled").length ? (b.active = !1, this.active = a()) : this._activate(Math.max(0, b.active - 1)) : b.active = this.headers.index(this.active), this._destroyIcons(), this._refresh()
			},
			_processPanels: function () {
				var a = this.headers,
					b = this.panels;
				this.headers = this.element.find(this.options.header), this._addClass(this.headers, "ui-accordion-header ui-accordion-header-collapsed", "ui-state-default"), this.panels = this.headers.next().filter(":not(.ui-accordion-content-active)").hide(), this._addClass(this.panels, "ui-accordion-content", "ui-helper-reset ui-widget-content"), b && (this._off(a.not(this.headers)), this._off(b.not(this.panels)))
			},
			_refresh: function () {
				var b, c = this.options,
					d = c.heightStyle,
					e = this.element.parent();
				this.active = this._findActive(c.active), this._addClass(this.active, "ui-accordion-header-active", "ui-state-active")._removeClass(this.active, "ui-accordion-header-collapsed"), this._addClass(this.active.next(), "ui-accordion-content-active"), this.active.next().show(), this.headers.attr("role", "tab").each(function () {
					var b = a(this),
						c = b.uniqueId().attr("id"),
						d = b.next(),
						e = d.uniqueId().attr("id");
					b.attr("aria-controls", e), d.attr("aria-labelledby", c)
				}).next().attr("role", "tabpanel"), this.headers.not(this.active).attr({
					"aria-selected": "false",
					"aria-expanded": "false",
					tabIndex: -1
				}).next().attr({
					"aria-hidden": "true"
				}).hide(), this.active.length ? this.active.attr({
					"aria-selected": "true",
					"aria-expanded": "true",
					tabIndex: 0
				}).next().attr({
					"aria-hidden": "false"
				}) : this.headers.eq(0).attr("tabIndex", 0), this._createIcons(), this._setupEvents(c.event), "fill" === d ? (b = e.height(), this.element.siblings(":visible").each(function () {
					var c = a(this),
						d = c.css("position");
					"absolute" !== d && "fixed" !== d && (b -= c.outerHeight(!0))
				}), this.headers.each(function () {
					b -= a(this).outerHeight(!0)
				}), this.headers.next().each(function () {
					a(this).height(Math.max(0, b - a(this).innerHeight() + a(this).height()))
				}).css("overflow", "auto")) : "auto" === d && (b = 0, this.headers.next().each(function () {
					var c = a(this).is(":visible");
					c || a(this).show(), b = Math.max(b, a(this).css("height", "").height()), c || a(this).hide()
				}).height(b))
			},
			_activate: function (b) {
				var c = this._findActive(b)[0];
				c !== this.active[0] && (c = c || this.active[0], this._eventHandler({
					target: c,
					currentTarget: c,
					preventDefault: a.noop
				}))
			},
			_findActive: function (b) {
				return "number" == typeof b ? this.headers.eq(b) : a()
			},
			_setupEvents: function (b) {
				var c = {
					keydown: "_keydown"
				};
				b && a.each(b.split(" "), function (a, b) {
					c[b] = "_eventHandler"
				}), this._off(this.headers.add(this.headers.next())), this._on(this.headers, c), this._on(this.headers.next(), {
					keydown: "_panelKeyDown"
				}), this._hoverable(this.headers), this._focusable(this.headers)
			},
			_eventHandler: function (b) {
				var c, d, e = this.options,
					f = this.active,
					g = a(b.currentTarget),
					h = g[0] === f[0],
					i = h && e.collapsible,
					j = i ? a() : g.next(),
					k = f.next(),
					l = {
						oldHeader: f,
						oldPanel: k,
						newHeader: i ? a() : g,
						newPanel: j
					};
				b.preventDefault(), h && !e.collapsible || !1 === this._trigger("beforeActivate", b, l) || (e.active = !i && this.headers.index(g), this.active = h ? a() : g, this._toggle(l), this._removeClass(f, "ui-accordion-header-active", "ui-state-active"), e.icons && (c = f.children(".ui-accordion-header-icon"), this._removeClass(c, null, e.icons.activeHeader)._addClass(c, null, e.icons.header)), h || (this._removeClass(g, "ui-accordion-header-collapsed")._addClass(g, "ui-accordion-header-active", "ui-state-active"), e.icons && (d = g.children(".ui-accordion-header-icon"), this._removeClass(d, null, e.icons.header)._addClass(d, null, e.icons.activeHeader)), this._addClass(g.next(), "ui-accordion-content-active")))
			},
			_toggle: function (b) {
				var c = b.newPanel,
					d = this.prevShow.length ? this.prevShow : b.oldPanel;
				this.prevShow.add(this.prevHide).stop(!0, !0), this.prevShow = c, this.prevHide = d, this.options.animate ? this._animate(c, d, b) : (d.hide(), c.show(), this._toggleComplete(b)), d.attr({
					"aria-hidden": "true"
				}), d.prev().attr({
					"aria-selected": "false",
					"aria-expanded": "false"
				}), c.length && d.length ? d.prev().attr({
					tabIndex: -1,
					"aria-expanded": "false"
				}) : c.length && this.headers.filter(function () {
					return 0 === parseInt(a(this).attr("tabIndex"), 10)
				}).attr("tabIndex", -1), c.attr("aria-hidden", "false").prev().attr({
					"aria-selected": "true",
					"aria-expanded": "true",
					tabIndex: 0
				})
			},
			_animate: function (a, b, c) {
				var d, e, f, g = this,
					h = 0,
					i = a.css("box-sizing"),
					j = a.length && (!b.length || a.index() < b.index()),
					k = this.options.animate || {},
					l = j && k.down || k,
					m = function () {
						g._toggleComplete(c)
					};
				return "number" == typeof l && (f = l), "string" == typeof l && (e = l), e = e || l.easing || k.easing, f = f || l.duration || k.duration, b.length ? a.length ? (d = a.show().outerHeight(), b.animate(this.hideProps, {
					duration: f,
					easing: e,
					step: function (a, b) {
						b.now = Math.round(a)
					}
				}), void a.hide().animate(this.showProps, {
					duration: f,
					easing: e,
					complete: m,
					step: function (a, c) {
						c.now = Math.round(a), "height" !== c.prop ? "content-box" === i && (h += c.now) : "content" !== g.options.heightStyle && (c.now = Math.round(d - b.outerHeight() - h), h = 0)
					}
				})) : b.animate(this.hideProps, f, e, m) : a.animate(this.showProps, f, e, m)
			},
			_toggleComplete: function (a) {
				var b = a.oldPanel,
					c = b.prev();
				this._removeClass(b, "ui-accordion-content-active"), this._removeClass(c, "ui-accordion-header-active")._addClass(c, "ui-accordion-header-collapsed"), b.length && (b.parent()[0].className = b.parent()[0].className), this._trigger("activate", null, a)
			}
		}), a.ui.safeActiveElement = function (a) {
			var b;
			try {
				b = a.activeElement
			} catch (c) {
				b = a.body
			}
			return b || (b = a.body), b.nodeName || (b = a.body), b
		}, a.widget("ui.menu", {
			version: "1.12.1",
			defaultElement: "<ul>",
			delay: 300,
			options: {
				icons: {
					submenu: "ui-icon-caret-1-e"
				},
				items: "> *",
				menus: "ul",
				position: {
					my: "left top",
					at: "right top"
				},
				role: "menu",
				blur: null,
				focus: null,
				select: null
			},
			_create: function () {
				this.activeMenu = this.element, this.mouseHandled = !1, this.element.uniqueId().attr({
					role: this.options.role,
					tabIndex: 0
				}), this._addClass("ui-menu", "ui-widget ui-widget-content"), this._on({
					"mousedown .ui-menu-item": function (a) {
						a.preventDefault()
					},
					"click .ui-menu-item": function (b) {
						var c = a(b.target),
							d = a(a.ui.safeActiveElement(this.document[0]));
						!this.mouseHandled && c.not(".ui-state-disabled").length && (this.select(b), b.isPropagationStopped() || (this.mouseHandled = !0), c.has(".ui-menu").length ? this.expand(b) : !this.element.is(":focus") && d.closest(".ui-menu").length && (this.element.trigger("focus", [!0]), this.active && 1 === this.active.parents(".ui-menu").length && clearTimeout(this.timer)))
					},
					"mouseenter .ui-menu-item": function (b) {
						if (!this.previousFilter) {
							var c = a(b.target).closest(".ui-menu-item"),
								d = a(b.currentTarget);
							c[0] === d[0] && (this._removeClass(d.siblings().children(".ui-state-active"), null, "ui-state-active"), this.focus(b, d))
						}
					},
					mouseleave: "collapseAll",
					"mouseleave .ui-menu": "collapseAll",
					focus: function (a, b) {
						var c = this.active || this.element.find(this.options.items).eq(0);
						b || this.focus(a, c)
					},
					blur: function (b) {
						this._delay(function () {
							!a.contains(this.element[0], a.ui.safeActiveElement(this.document[0])) && this.collapseAll(b)
						})
					},
					keydown: "_keydown"
				}), this.refresh(), this._on(this.document, {
					click: function (a) {
						this._closeOnDocumentClick(a) && this.collapseAll(a), this.mouseHandled = !1
					}
				})
			},
			_destroy: function () {
				var b = this.element.find(".ui-menu-item").removeAttr("role aria-disabled"),
					c = b.children(".ui-menu-item-wrapper").removeUniqueId().removeAttr("tabIndex role aria-haspopup");
				this.element.removeAttr("aria-activedescendant").find(".ui-menu").addBack().removeAttr("role aria-labelledby aria-expanded aria-hidden aria-disabled tabIndex").removeUniqueId().show(), c.children().each(function () {
					var b = a(this);
					b.data("ui-menu-submenu-caret") && b.remove()
				})
			},
			_keydown: function (b) {
				var c, d, e, f, g = !0;
				switch (b.keyCode) {
					case a.ui.keyCode.PAGE_UP:
						this.previousPage(b);
						break;
					case a.ui.keyCode.PAGE_DOWN:
						this.nextPage(b);
						break;
					case a.ui.keyCode.HOME:
						this._move("first", "first", b);
						break;
					case a.ui.keyCode.END:
						this._move("last", "last", b);
						break;
					case a.ui.keyCode.UP:
						this.previous(b);
						break;
					case a.ui.keyCode.DOWN:
						this.next(b);
						break;
					case a.ui.keyCode.LEFT:
						this.collapse(b);
						break;
					case a.ui.keyCode.RIGHT:
						this.active && !this.active.is(".ui-state-disabled") && this.expand(b);
						break;
					case a.ui.keyCode.ENTER:
					case a.ui.keyCode.SPACE:
						this._activate(b);
						break;
					case a.ui.keyCode.ESCAPE:
						this.collapse(b);
						break;
					default:
						g = !1, d = this.previousFilter || "", f = !1, e = b.keyCode >= 96 && 105 >= b.keyCode ? "" + (b.keyCode - 96) : String.fromCharCode(b.keyCode), clearTimeout(this.filterTimer), e === d ? f = !0 : e = d + e, c = this._filterMenuItems(e), c = f && -1 !== c.index(this.active.next()) ? this.active.nextAll(".ui-menu-item") : c, c.length || (e = String.fromCharCode(b.keyCode), c = this._filterMenuItems(e)), c.length ? (this.focus(b, c), this.previousFilter = e, this.filterTimer = this._delay(function () {
							delete this.previousFilter
						}, 1e3)) : delete this.previousFilter
				}
				g && b.preventDefault()
			},
			_activate: function (a) {
				this.active && !this.active.is(".ui-state-disabled") && (this.active.children("[aria-haspopup='true']").length ? this.expand(a) : this.select(a))
			},
			refresh: function () {
				var b, c, d, e, f, g = this,
					h = this.options.icons.submenu,
					i = this.element.find(this.options.menus);
				this._toggleClass("ui-menu-icons", null, !!this.element.find(".ui-icon").length), d = i.filter(":not(.ui-menu)").hide().attr({
					role: this.options.role,
					"aria-hidden": "true",
					"aria-expanded": "false"
				}).each(function () {
					var b = a(this),
						c = b.prev(),
						d = a("<span>").data("ui-menu-submenu-caret", !0);
					g._addClass(d, "ui-menu-icon", "ui-icon " + h), c.attr("aria-haspopup", "true").prepend(d), b.attr("aria-labelledby", c.attr("id"))
				}), this._addClass(d, "ui-menu", "ui-widget ui-widget-content ui-front"), b = i.add(this.element), c = b.find(this.options.items), c.not(".ui-menu-item").each(function () {
					var b = a(this);
					g._isDivider(b) && g._addClass(b, "ui-menu-divider", "ui-widget-content")
				}), e = c.not(".ui-menu-item, .ui-menu-divider"), f = e.children().not(".ui-menu").uniqueId().attr({
					tabIndex: -1,
					role: this._itemRole()
				}), this._addClass(e, "ui-menu-item")._addClass(f, "ui-menu-item-wrapper"), c.filter(".ui-state-disabled").attr("aria-disabled", "true"), this.active && !a.contains(this.element[0], this.active[0]) && this.blur()
			},
			_itemRole: function () {
				return {
					menu: "menuitem",
					listbox: "option"
				}[this.options.role]
			},
			_setOption: function (a, b) {
				if ("icons" === a) {
					var c = this.element.find(".ui-menu-icon");
					this._removeClass(c, null, this.options.icons.submenu)._addClass(c, null, b.submenu)
				}
				this._super(a, b)
			},
			_setOptionDisabled: function (a) {
				this._super(a), this.element.attr("aria-disabled", a + ""), this._toggleClass(null, "ui-state-disabled", !!a)
			},
			focus: function (a, b) {
				var c, d, e;
				this.blur(a, a && "focus" === a.type), this._scrollIntoView(b), this.active = b.first(), d = this.active.children(".ui-menu-item-wrapper"), this._addClass(d, null, "ui-state-active"), this.options.role && this.element.attr("aria-activedescendant", d.attr("id")), e = this.active.parent().closest(".ui-menu-item").children(".ui-menu-item-wrapper"), this._addClass(e, null, "ui-state-active"), a && "keydown" === a.type ? this._close() : this.timer = this._delay(function () {
					this._close()
				}, this.delay), c = b.children(".ui-menu"), c.length && a && /^mouse/.test(a.type) && this._startOpening(c), this.activeMenu = b.parent(), this._trigger("focus", a, {
					item: b
				})
			},
			_scrollIntoView: function (b) {
				var c, d, e, f, g, h;
				this._hasScroll() && (c = parseFloat(a.css(this.activeMenu[0], "borderTopWidth")) || 0, d = parseFloat(a.css(this.activeMenu[0], "paddingTop")) || 0, e = b.offset().top - this.activeMenu.offset().top - c - d, f = this.activeMenu.scrollTop(), g = this.activeMenu.height(), h = b.outerHeight(), 0 > e ? this.activeMenu.scrollTop(f + e) : e + h > g && this.activeMenu.scrollTop(f + e - g + h))
			},
			blur: function (a, b) {
				b || clearTimeout(this.timer), this.active && (this._removeClass(this.active.children(".ui-menu-item-wrapper"), null, "ui-state-active"), this._trigger("blur", a, {
					item: this.active
				}), this.active = null)
			},
			_startOpening: function (a) {
				clearTimeout(this.timer), "true" === a.attr("aria-hidden") && (this.timer = this._delay(function () {
					this._close(), this._open(a)
				}, this.delay))
			},
			_open: function (b) {
				var c = a.extend({ of: this.active
				}, this.options.position);
				clearTimeout(this.timer), this.element.find(".ui-menu").not(b.parents(".ui-menu")).hide().attr("aria-hidden", "true"), b.show().removeAttr("aria-hidden").attr("aria-expanded", "true").position(c)
			},
			collapseAll: function (b, c) {
				clearTimeout(this.timer), this.timer = this._delay(function () {
					var d = c ? this.element : a(b && b.target).closest(this.element.find(".ui-menu"));
					d.length || (d = this.element), this._close(d), this.blur(b), this._removeClass(d.find(".ui-state-active"), null, "ui-state-active"), this.activeMenu = d
				}, this.delay)
			},
			_close: function (a) {
				a || (a = this.active ? this.active.parent() : this.element), a.find(".ui-menu").hide().attr("aria-hidden", "true").attr("aria-expanded", "false")
			},
			_closeOnDocumentClick: function (b) {
				return !a(b.target).closest(".ui-menu").length
			},
			_isDivider: function (a) {
				return !/[^\-\u2014\u2013\s]/.test(a.text())
			},
			collapse: function (a) {
				var b = this.active && this.active.parent().closest(".ui-menu-item", this.element);
				b && b.length && (this._close(), this.focus(a, b))
			},
			expand: function (a) {
				var b = this.active && this.active.children(".ui-menu ").find(this.options.items).first();
				b && b.length && (this._open(b.parent()), this._delay(function () {
					this.focus(a, b)
				}))
			},
			next: function (a) {
				this._move("next", "first", a)
			},
			previous: function (a) {
				this._move("prev", "last", a)
			},
			isFirstItem: function () {
				return this.active && !this.active.prevAll(".ui-menu-item").length
			},
			isLastItem: function () {
				return this.active && !this.active.nextAll(".ui-menu-item").length
			},
			_move: function (a, b, c) {
				var d;
				this.active && (d = "first" === a || "last" === a ? this.active["first" === a ? "prevAll" : "nextAll"](".ui-menu-item").eq(-1) : this.active[a + "All"](".ui-menu-item").eq(0)), d && d.length && this.active || (d = this.activeMenu.find(this.options.items)[b]()), this.focus(c, d)
			},
			nextPage: function (b) {
				var c, d, e;
				return this.active ? void(this.isLastItem() || (this._hasScroll() ? (d = this.active.offset().top, e = this.element.height(), this.active.nextAll(".ui-menu-item").each(function () {
					return c = a(this), 0 > c.offset().top - d - e
				}), this.focus(b, c)) : this.focus(b, this.activeMenu.find(this.options.items)[this.active ? "last" : "first"]()))) : void this.next(b)
			},
			previousPage: function (b) {
				var c, d, e;
				return this.active ? void(this.isFirstItem() || (this._hasScroll() ? (d = this.active.offset().top, e = this.element.height(), this.active.prevAll(".ui-menu-item").each(function () {
					return c = a(this), c.offset().top - d + e > 0
				}), this.focus(b, c)) : this.focus(b, this.activeMenu.find(this.options.items).first()))) : void this.next(b)
			},
			_hasScroll: function () {
				return this.element.outerHeight() < this.element.prop("scrollHeight")
			},
			select: function (b) {
				this.active = this.active || a(b.target).closest(".ui-menu-item");
				var c = {
					item: this.active
				};
				this.active.has(".ui-menu").length || this.collapseAll(b, !0), this._trigger("select", b, c)
			},
			_filterMenuItems: function (b) {
				var c = b.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&"),
					d = RegExp("^" + c, "i");
				return this.activeMenu.find(this.options.items).filter(".ui-menu-item").filter(function () {
					return d.test(a.trim(a(this).children(".ui-menu-item-wrapper").text()))
				})
			}
		}), a.widget("ui.autocomplete", {
			version: "1.12.1",
			defaultElement: "<input>",
			options: {
				appendTo: null,
				autoFocus: !1,
				delay: 300,
				minLength: 1,
				position: {
					my: "left top",
					at: "left bottom",
					collision: "none"
				},
				source: null,
				change: null,
				close: null,
				focus: null,
				open: null,
				response: null,
				search: null,
				select: null
			},
			requestIndex: 0,
			pending: 0,
			_create: function () {
				var b, c, d, e = this.element[0].nodeName.toLowerCase(),
					f = "textarea" === e,
					g = "input" === e;
				this.isMultiLine = f || !g && this._isContentEditable(this.element), this.valueMethod = this.element[f || g ? "val" : "text"], this.isNewMenu = !0, this._addClass("ui-autocomplete-input"), this.element.attr("autocomplete", "off"), this._on(this.element, {
					keydown: function (e) {
						if (this.element.prop("readOnly")) return b = !0, d = !0, void(c = !0);
						b = !1, d = !1, c = !1;
						var f = a.ui.keyCode;
						switch (e.keyCode) {
							case f.PAGE_UP:
								b = !0, this._move("previousPage", e);
								break;
							case f.PAGE_DOWN:
								b = !0, this._move("nextPage", e);
								break;
							case f.UP:
								b = !0, this._keyEvent("previous", e);
								break;
							case f.DOWN:
								b = !0, this._keyEvent("next", e);
								break;
							case f.ENTER:
								this.menu.active && (b = !0, e.preventDefault(), this.menu.select(e));
								break;
							case f.TAB:
								this.menu.active && this.menu.select(e);
								break;
							case f.ESCAPE:
								this.menu.element.is(":visible") && (this.isMultiLine || this._value(this.term), this.close(e), e.preventDefault());
								break;
							default:
								c = !0, this._searchTimeout(e)
						}
					},
					keypress: function (d) {
						if (b) return b = !1, void((!this.isMultiLine || this.menu.element.is(":visible")) && d.preventDefault());
						if (!c) {
							var e = a.ui.keyCode;
							switch (d.keyCode) {
								case e.PAGE_UP:
									this._move("previousPage", d);
									break;
								case e.PAGE_DOWN:
									this._move("nextPage", d);
									break;
								case e.UP:
									this._keyEvent("previous", d);
									break;
								case e.DOWN:
									this._keyEvent("next", d)
							}
						}
					},
					input: function (a) {
						return d ? (d = !1, void a.preventDefault()) : void this._searchTimeout(a)
					},
					focus: function () {
						this.selectedItem = null, this.previous = this._value()
					},
					blur: function (a) {
						return this.cancelBlur ? void delete this.cancelBlur : (clearTimeout(this.searching), this.close(a), void this._change(a))
					}
				}), this._initSource(), this.menu = a("<ul>").appendTo(this._appendTo()).menu({
					role: null
				}).hide().menu("instance"), this._addClass(this.menu.element, "ui-autocomplete", "ui-front"), this._on(this.menu.element, {
					mousedown: function (b) {
						b.preventDefault(), this.cancelBlur = !0, this._delay(function () {
							delete this.cancelBlur, this.element[0] !== a.ui.safeActiveElement(this.document[0]) && this.element.trigger("focus")
						})
					},
					menufocus: function (b, c) {
						var d, e;
						return this.isNewMenu && (this.isNewMenu = !1, b.originalEvent && /^mouse/.test(b.originalEvent.type)) ? (this.menu.blur(), void this.document.one("mousemove", function () {
							a(b.target).trigger(b.originalEvent)
						})) : (e = c.item.data("ui-autocomplete-item"), !1 !== this._trigger("focus", b, {
							item: e
						}) && b.originalEvent && /^key/.test(b.originalEvent.type) && this._value(e.value), void((d = c.item.attr("aria-label") || e.value) && a.trim(d).length && (this.liveRegion.children().hide(), a("<div>").text(d).appendTo(this.liveRegion))))
					},
					menuselect: function (b, c) {
						var d = c.item.data("ui-autocomplete-item"),
							e = this.previous;
						this.element[0] !== a.ui.safeActiveElement(this.document[0]) && (this.element.trigger("focus"), this.previous = e, this._delay(function () {
							this.previous = e, this.selectedItem = d
						})), !1 !== this._trigger("select", b, {
							item: d
						}) && this._value(d.value), this.term = this._value(), this.close(b), this.selectedItem = d
					}
				}), this.liveRegion = a("<div>", {
					role: "status",
					"aria-live": "assertive",
					"aria-relevant": "additions"
				}).appendTo(this.document[0].body), this._addClass(this.liveRegion, null, "ui-helper-hidden-accessible"), this._on(this.window, {
					beforeunload: function () {
						this.element.removeAttr("autocomplete")
					}
				})
			},
			_destroy: function () {
				clearTimeout(this.searching), this.element.removeAttr("autocomplete"), this.menu.element.remove(), this.liveRegion.remove()
			},
			_setOption: function (a, b) {
				this._super(a, b), "source" === a && this._initSource(), "appendTo" === a && this.menu.element.appendTo(this._appendTo()), "disabled" === a && b && this.xhr && this.xhr.abort()
			},
			_isEventTargetInWidget: function (b) {
				var c = this.menu.element[0];
				return b.target === this.element[0] || b.target === c || a.contains(c, b.target)
			},
			_closeOnClickOutside: function (a) {
				this._isEventTargetInWidget(a) || this.close()
			},
			_appendTo: function () {
				var b = this.options.appendTo;
				return b && (b = b.jquery || b.nodeType ? a(b) : this.document.find(b).eq(0)), b && b[0] || (b = this.element.closest(".ui-front, dialog")), b.length || (b = this.document[0].body), b
			},
			_initSource: function () {
				var b, c, d = this;
				a.isArray(this.options.source) ? (b = this.options.source, this.source = function (c, d) {
					d(a.ui.autocomplete.filter(b, c.term))
				}) : "string" == typeof this.options.source ? (c = this.options.source, this.source = function (b, e) {
					d.xhr && d.xhr.abort(), d.xhr = a.ajax({
						url: c,
						data: b,
						dataType: "json",
						success: function (a) {
							e(a)
						},
						error: function () {
							e([])
						}
					})
				}) : this.source = this.options.source
			},
			_searchTimeout: function (a) {
				clearTimeout(this.searching), this.searching = this._delay(function () {
					var b = this.term === this._value(),
						c = this.menu.element.is(":visible"),
						d = a.altKey || a.ctrlKey || a.metaKey || a.shiftKey;
					(!b || b && !c && !d) && (this.selectedItem = null, this.search(null, a))
				}, this.options.delay)
			},
			search: function (a, b) {
				return a = null != a ? a : this._value(), this.term = this._value(), a.length < this.options.minLength ? this.close(b) : !1 !== this._trigger("search", b) ? this._search(a) : void 0
			},
			_search: function (a) {
				this.pending++, this._addClass("ui-autocomplete-loading"), this.cancelSearch = !1, this.source({
					term: a
				}, this._response())
			},
			_response: function () {
				var b = ++this.requestIndex;
				return a.proxy(function (a) {
					b === this.requestIndex && this.__response(a), --this.pending || this._removeClass("ui-autocomplete-loading")
				}, this)
			},
			__response: function (a) {
				a && (a = this._normalize(a)), this._trigger("response", null, {
					content: a
				}), !this.options.disabled && a && a.length && !this.cancelSearch ? (this._suggest(a), this._trigger("open")) : this._close()
			},
			close: function (a) {
				this.cancelSearch = !0, this._close(a)
			},
			_close: function (a) {
				this._off(this.document, "mousedown"), this.menu.element.is(":visible") && (this.menu.element.hide(), this.menu.blur(), this.isNewMenu = !0, this._trigger("close", a))
			},
			_change: function (a) {
				this.previous !== this._value() && this._trigger("change", a, {
					item: this.selectedItem
				})
			},
			_normalize: function (b) {
				return b.length && b[0].label && b[0].value ? b : a.map(b, function (b) {
					return "string" == typeof b ? {
						label: b,
						value: b
					} : a.extend({}, b, {
						label: b.label || b.value,
						value: b.value || b.label
					})
				})
			},
			_suggest: function (b) {
				var c = this.menu.element.empty();
				this._renderMenu(c, b), this.isNewMenu = !0, this.menu.refresh(), c.show(), this._resizeMenu(), c.position(a.extend({ of: this.element
				}, this.options.position)), this.options.autoFocus && this.menu.next(), this._on(this.document, {
					mousedown: "_closeOnClickOutside"
				})
			},
			_resizeMenu: function () {
				var a = this.menu.element;
				a.outerWidth(Math.max(a.width("").outerWidth() + 1, this.element.outerWidth()))
			},
			_renderMenu: function (b, c) {
				var d = this;
				a.each(c, function (a, c) {
					d._renderItemData(b, c)
				})
			},
			_renderItemData: function (a, b) {
				return this._renderItem(a, b).data("ui-autocomplete-item", b)
			},
			_renderItem: function (b, c) {
				return a("<li>").append(a("<div>").text(c.label)).appendTo(b)
			},
			_move: function (a, b) {
				return this.menu.element.is(":visible") ? this.menu.isFirstItem() && /^previous/.test(a) || this.menu.isLastItem() && /^next/.test(a) ? (this.isMultiLine || this._value(this.term), void this.menu.blur()) : void this.menu[a](b) : void this.search(null, b)
			},
			widget: function () {
				return this.menu.element
			},
			_value: function () {
				return this.valueMethod.apply(this.element, arguments)
			},
			_keyEvent: function (a, b) {
				(!this.isMultiLine || this.menu.element.is(":visible")) && (this._move(a, b), b.preventDefault())
			},
			_isContentEditable: function (a) {
				if (!a.length) return !1;
				var b = a.prop("contentEditable");
				return "inherit" === b ? this._isContentEditable(a.parent()) : "true" === b
			}
		}), a.extend(a.ui.autocomplete, {
			escapeRegex: function (a) {
				return a.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&")
			},
			filter: function (b, c) {
				var d = RegExp(a.ui.autocomplete.escapeRegex(c), "i");
				return a.grep(b, function (a) {
					return d.test(a.label || a.value || a)
				})
			}
		}), a.widget("ui.autocomplete", a.ui.autocomplete, {
			options: {
				messages: {
					noResults: "No search results.",
					results: function (a) {
						return a + (a > 1 ? " results are" : " result is") + " available, use up and down arrow keys to navigate."
					}
				}
			},
			__response: function (b) {
				var c;
				this._superApply(arguments), this.options.disabled || this.cancelSearch || (c = b && b.length ? this.options.messages.results(b.length) : this.options.messages.noResults, this.liveRegion.children().hide(), a("<div>").text(c).appendTo(this.liveRegion))
			}
		}), a.ui.autocomplete;
	var o = /ui-corner-([a-z]){2,6}/g;
	a.widget("ui.controlgroup", {
		version: "1.12.1",
		defaultElement: "<div>",
		options: {
			direction: "horizontal",
			disabled: null,
			onlyVisible: !0,
			items: {
				button: "input[type=button], input[type=submit], input[type=reset], button, a",
				controlgroupLabel: ".ui-controlgroup-label",
				checkboxradio: "input[type='checkbox'], input[type='radio']",
				selectmenu: "select",
				spinner: ".ui-spinner-input"
			}
		},
		_create: function () {
			this._enhance()
		},
		_enhance: function () {
			this.element.attr("role", "toolbar"), this.refresh()
		},
		_destroy: function () {
			this._callChildMethod("destroy"), this.childWidgets.removeData("ui-controlgroup-data"), this.element.removeAttr("role"), this.options.items.controlgroupLabel && this.element.find(this.options.items.controlgroupLabel).find(".ui-controlgroup-label-contents").contents().unwrap()
		},
		_initWidgets: function () {
			var b = this,
				c = [];
			a.each(this.options.items, function (d, e) {
				var f, g = {};
				return e ? "controlgroupLabel" === d ? (f = b.element.find(e), f.each(function () {
					var b = a(this);
					b.children(".ui-controlgroup-label-contents").length || b.contents().wrapAll("<span class='ui-controlgroup-label-contents'></span>")
				}), b._addClass(f, null, "ui-widget ui-widget-content ui-state-default"), void(c = c.concat(f.get()))) : void(a.fn[d] && (g = b["_" + d + "Options"] ? b["_" + d + "Options"]("middle") : {
					classes: {}
				}, b.element.find(e).each(function () {
					var e = a(this),
						f = e[d]("instance"),
						h = a.widget.extend({}, g);
					if ("button" !== d || !e.parent(".ui-spinner").length) {
						f || (f = e[d]()[d]("instance")), f && (h.classes = b._resolveClassesValues(h.classes, f)), e[d](h);
						var i = e[d]("widget");
						a.data(i[0], "ui-controlgroup-data", f || e[d]("instance")), c.push(i[0])
					}
				}))) : void 0
			}), this.childWidgets = a(a.unique(c)), this._addClass(this.childWidgets, "ui-controlgroup-item")
		},
		_callChildMethod: function (b) {
			this.childWidgets.each(function () {
				var c = a(this),
					d = c.data("ui-controlgroup-data");
				d && d[b] && d[b]()
			})
		},
		_updateCornerClass: function (a, b) {
			var c = this._buildSimpleOptions(b, "label").classes.label;
			this._removeClass(a, null, "ui-corner-top ui-corner-bottom ui-corner-left ui-corner-right ui-corner-all"), this._addClass(a, null, c)
		},
		_buildSimpleOptions: function (a, b) {
			var c = "vertical" === this.options.direction,
				d = {
					classes: {}
				};
			return d.classes[b] = {
				middle: "",
				first: "ui-corner-" + (c ? "top" : "left"),
				last: "ui-corner-" + (c ? "bottom" : "right"),
				only: "ui-corner-all"
			}[a], d
		},
		_spinnerOptions: function (a) {
			var b = this._buildSimpleOptions(a, "ui-spinner");
			return b.classes["ui-spinner-up"] = "", b.classes["ui-spinner-down"] = "", b
		},
		_buttonOptions: function (a) {
			return this._buildSimpleOptions(a, "ui-button")
		},
		_checkboxradioOptions: function (a) {
			return this._buildSimpleOptions(a, "ui-checkboxradio-label")
		},
		_selectmenuOptions: function (a) {
			var b = "vertical" === this.options.direction;
			return {
				width: !!b && "auto",
				classes: {
					middle: {
						"ui-selectmenu-button-open": "",
						"ui-selectmenu-button-closed": ""
					},
					first: {
						"ui-selectmenu-button-open": "ui-corner-" + (b ? "top" : "tl"),
						"ui-selectmenu-button-closed": "ui-corner-" + (b ? "top" : "left")
					},
					last: {
						"ui-selectmenu-button-open": b ? "" : "ui-corner-tr",
						"ui-selectmenu-button-closed": "ui-corner-" + (b ? "bottom" : "right")
					},
					only: {
						"ui-selectmenu-button-open": "ui-corner-top",
						"ui-selectmenu-button-closed": "ui-corner-all"
					}
				}[a]
			}
		},
		_resolveClassesValues: function (b, c) {
			var d = {};
			return a.each(b, function (e) {
				var f = c.options.classes[e] || "";
				f = a.trim(f.replace(o, "")), d[e] = (f + " " + b[e]).replace(/\s+/g, " ")
			}), d
		},
		_setOption: function (a, b) {
			return "direction" === a && this._removeClass("ui-controlgroup-" + this.options.direction), this._super(a, b), "disabled" === a ? void this._callChildMethod(b ? "disable" : "enable") : void this.refresh()
		},
		refresh: function () {
			var b, c = this;
			this._addClass("ui-controlgroup ui-controlgroup-" + this.options.direction), "horizontal" === this.options.direction && this._addClass(null, "ui-helper-clearfix"), this._initWidgets(), b = this.childWidgets, this.options.onlyVisible && (b = b.filter(":visible")), b.length && (a.each(["first", "last"], function (a, d) {
				var e = b[d]().data("ui-controlgroup-data");
				if (e && c["_" + e.widgetName + "Options"]) {
					var f = c["_" + e.widgetName + "Options"](1 === b.length ? "only" : d);
					f.classes = c._resolveClassesValues(f.classes, e), e.element[e.widgetName](f)
				} else c._updateCornerClass(b[d](), d)
			}), this._callChildMethod("refresh"))
		}
	}), a.widget("ui.checkboxradio", [a.ui.formResetMixin, {
		version: "1.12.1",
		options: {
			disabled: null,
			label: null,
			icon: !0,
			classes: {
				"ui-checkboxradio-label": "ui-corner-all",
				"ui-checkboxradio-icon": "ui-corner-all"
			}
		},
		_getCreateOptions: function () {
			var b, c, d = this,
				e = this._super() || {};
			return this._readType(), c = this.element.labels(), this.label = a(c[c.length - 1]), this.label.length || a.error("No label found for checkboxradio widget"), this.originalLabel = "", this.label.contents().not(this.element[0]).each(function () {
					d.originalLabel += 3 === this.nodeType ? a(this).text() : this.outerHTML
				}), this.originalLabel && (e.label = this.originalLabel),
				b = this.element[0].disabled, null != b && (e.disabled = b), e
		},
		_create: function () {
			var a = this.element[0].checked;
			this._bindFormResetHandler(), null == this.options.disabled && (this.options.disabled = this.element[0].disabled), this._setOption("disabled", this.options.disabled), this._addClass("ui-checkboxradio", "ui-helper-hidden-accessible"), this._addClass(this.label, "ui-checkboxradio-label", "ui-button ui-widget"), "radio" === this.type && this._addClass(this.label, "ui-checkboxradio-radio-label"), this.options.label && this.options.label !== this.originalLabel ? this._updateLabel() : this.originalLabel && (this.options.label = this.originalLabel), this._enhance(), a && (this._addClass(this.label, "ui-checkboxradio-checked", "ui-state-active"), this.icon && this._addClass(this.icon, null, "ui-state-hover")), this._on({
				change: "_toggleClasses",
				focus: function () {
					this._addClass(this.label, null, "ui-state-focus ui-visual-focus")
				},
				blur: function () {
					this._removeClass(this.label, null, "ui-state-focus ui-visual-focus")
				}
			})
		},
		_readType: function () {
			var b = this.element[0].nodeName.toLowerCase();
			this.type = this.element[0].type, "input" === b && /radio|checkbox/.test(this.type) || a.error("Can't create checkboxradio on element.nodeName=" + b + " and element.type=" + this.type)
		},
		_enhance: function () {
			this._updateIcon(this.element[0].checked)
		},
		widget: function () {
			return this.label
		},
		_getRadioGroup: function () {
			var b, c = this.element[0].name,
				d = "input[name='" + a.ui.escapeSelector(c) + "']";
			return c ? (b = this.form.length ? a(this.form[0].elements).filter(d) : a(d).filter(function () {
				return 0 === a(this).form().length
			}), b.not(this.element)) : a([])
		},
		_toggleClasses: function () {
			var b = this.element[0].checked;
			this._toggleClass(this.label, "ui-checkboxradio-checked", "ui-state-active", b), this.options.icon && "checkbox" === this.type && this._toggleClass(this.icon, null, "ui-icon-check ui-state-checked", b)._toggleClass(this.icon, null, "ui-icon-blank", !b), "radio" === this.type && this._getRadioGroup().each(function () {
				var b = a(this).checkboxradio("instance");
				b && b._removeClass(b.label, "ui-checkboxradio-checked", "ui-state-active")
			})
		},
		_destroy: function () {
			this._unbindFormResetHandler(), this.icon && (this.icon.remove(), this.iconSpace.remove())
		},
		_setOption: function (a, b) {
			return "label" !== a || b ? (this._super(a, b), "disabled" === a ? (this._toggleClass(this.label, null, "ui-state-disabled", b), void(this.element[0].disabled = b)) : void this.refresh()) : void 0
		},
		_updateIcon: function (b) {
			var c = "ui-icon ui-icon-background ";
			this.options.icon ? (this.icon || (this.icon = a("<span>"), this.iconSpace = a("<span> </span>"), this._addClass(this.iconSpace, "ui-checkboxradio-icon-space")), "checkbox" === this.type ? (c += b ? "ui-icon-check ui-state-checked" : "ui-icon-blank", this._removeClass(this.icon, null, b ? "ui-icon-blank" : "ui-icon-check")) : c += "ui-icon-blank", this._addClass(this.icon, "ui-checkboxradio-icon", c), b || this._removeClass(this.icon, null, "ui-icon-check ui-state-checked"), this.icon.prependTo(this.label).after(this.iconSpace)) : void 0 !== this.icon && (this.icon.remove(), this.iconSpace.remove(), delete this.icon)
		},
		_updateLabel: function () {
			var a = this.label.contents().not(this.element[0]);
			this.icon && (a = a.not(this.icon[0])), this.iconSpace && (a = a.not(this.iconSpace[0])), a.remove(), this.label.append(this.options.label)
		},
		refresh: function () {
			var a = this.element[0].checked,
				b = this.element[0].disabled;
			this._updateIcon(a), this._toggleClass(this.label, "ui-checkboxradio-checked", "ui-state-active", a), null !== this.options.label && this._updateLabel(), b !== this.options.disabled && this._setOptions({
				disabled: b
			})
		}
	}]), a.ui.checkboxradio, a.widget("ui.button", {
		version: "1.12.1",
		defaultElement: "<button>",
		options: {
			classes: {
				"ui-button": "ui-corner-all"
			},
			disabled: null,
			icon: null,
			iconPosition: "beginning",
			label: null,
			showLabel: !0
		},
		_getCreateOptions: function () {
			var a, b = this._super() || {};
			return this.isInput = this.element.is("input"), a = this.element[0].disabled, null != a && (b.disabled = a), this.originalLabel = this.isInput ? this.element.val() : this.element.html(), this.originalLabel && (b.label = this.originalLabel), b
		},
		_create: function () {
			!this.option.showLabel & !this.options.icon && (this.options.showLabel = !0), null == this.options.disabled && (this.options.disabled = this.element[0].disabled || !1), this.hasTitle = !!this.element.attr("title"), this.options.label && this.options.label !== this.originalLabel && (this.isInput ? this.element.val(this.options.label) : this.element.html(this.options.label)), this._addClass("ui-button", "ui-widget"), this._setOption("disabled", this.options.disabled), this._enhance(), this.element.is("a") && this._on({
				keyup: function (b) {
					b.keyCode === a.ui.keyCode.SPACE && (b.preventDefault(), this.element[0].click ? this.element[0].click() : this.element.trigger("click"))
				}
			})
		},
		_enhance: function () {
			this.element.is("button") || this.element.attr("role", "button"), this.options.icon && (this._updateIcon("icon", this.options.icon), this._updateTooltip())
		},
		_updateTooltip: function () {
			this.title = this.element.attr("title"), this.options.showLabel || this.title || this.element.attr("title", this.options.label)
		},
		_updateIcon: function (b, c) {
			var d = "iconPosition" !== b,
				e = d ? this.options.iconPosition : c,
				f = "top" === e || "bottom" === e;
			this.icon ? d && this._removeClass(this.icon, null, this.options.icon) : (this.icon = a("<span>"), this._addClass(this.icon, "ui-button-icon", "ui-icon"), this.options.showLabel || this._addClass("ui-button-icon-only")), d && this._addClass(this.icon, null, c), this._attachIcon(e), f ? (this._addClass(this.icon, null, "ui-widget-icon-block"), this.iconSpace && this.iconSpace.remove()) : (this.iconSpace || (this.iconSpace = a("<span> </span>"), this._addClass(this.iconSpace, "ui-button-icon-space")), this._removeClass(this.icon, null, "ui-wiget-icon-block"), this._attachIconSpace(e))
		},
		_destroy: function () {
			this.element.removeAttr("role"), this.icon && this.icon.remove(), this.iconSpace && this.iconSpace.remove(), this.hasTitle || this.element.removeAttr("title")
		},
		_attachIconSpace: function (a) {
			this.icon[/^(?:end|bottom)/.test(a) ? "before" : "after"](this.iconSpace)
		},
		_attachIcon: function (a) {
			this.element[/^(?:end|bottom)/.test(a) ? "append" : "prepend"](this.icon)
		},
		_setOptions: function (a) {
			var b = void 0 === a.showLabel ? this.options.showLabel : a.showLabel,
				c = void 0 === a.icon ? this.options.icon : a.icon;
			b || c || (a.showLabel = !0), this._super(a)
		},
		_setOption: function (a, b) {
			"icon" === a && (b ? this._updateIcon(a, b) : this.icon && (this.icon.remove(), this.iconSpace && this.iconSpace.remove())), "iconPosition" === a && this._updateIcon(a, b), "showLabel" === a && (this._toggleClass("ui-button-icon-only", null, !b), this._updateTooltip()), "label" === a && (this.isInput ? this.element.val(b) : (this.element.html(b), this.icon && (this._attachIcon(this.options.iconPosition), this._attachIconSpace(this.options.iconPosition)))), this._super(a, b), "disabled" === a && (this._toggleClass(null, "ui-state-disabled", b), this.element[0].disabled = b, b && this.element.blur())
		},
		refresh: function () {
			var a = this.element.is("input, button") ? this.element[0].disabled : this.element.hasClass("ui-button-disabled");
			a !== this.options.disabled && this._setOptions({
				disabled: a
			}), this._updateTooltip()
		}
	}), !1 !== a.uiBackCompat && (a.widget("ui.button", a.ui.button, {
		options: {
			text: !0,
			icons: {
				primary: null,
				secondary: null
			}
		},
		_create: function () {
			this.options.showLabel && !this.options.text && (this.options.showLabel = this.options.text), !this.options.showLabel && this.options.text && (this.options.text = this.options.showLabel), this.options.icon || !this.options.icons.primary && !this.options.icons.secondary ? this.options.icon && (this.options.icons.primary = this.options.icon) : this.options.icons.primary ? this.options.icon = this.options.icons.primary : (this.options.icon = this.options.icons.secondary, this.options.iconPosition = "end"), this._super()
		},
		_setOption: function (a, b) {
			return "text" === a ? void this._super("showLabel", b) : ("showLabel" === a && (this.options.text = b), "icon" === a && (this.options.icons.primary = b), "icons" === a && (b.primary ? (this._super("icon", b.primary), this._super("iconPosition", "beginning")) : b.secondary && (this._super("icon", b.secondary), this._super("iconPosition", "end"))), void this._superApply(arguments))
		}
	}), a.fn.button = function (b) {
		return function () {
			return !this.length || this.length && "INPUT" !== this[0].tagName || this.length && "INPUT" === this[0].tagName && "checkbox" !== this.attr("type") && "radio" !== this.attr("type") ? b.apply(this, arguments) : (a.ui.checkboxradio || a.error("Checkboxradio widget missing"), 0 === arguments.length ? this.checkboxradio({
				icon: !1
			}) : this.checkboxradio.apply(this, arguments))
		}
	}(a.fn.button), a.fn.buttonset = function () {
		return a.ui.controlgroup || a.error("Controlgroup widget missing"), "option" === arguments[0] && "items" === arguments[1] && arguments[2] ? this.controlgroup.apply(this, [arguments[0], "items.button", arguments[2]]) : "option" === arguments[0] && "items" === arguments[1] ? this.controlgroup.apply(this, [arguments[0], "items.button"]) : ("object" == typeof arguments[0] && arguments[0].items && (arguments[0].items = {
			button: arguments[0].items
		}), this.controlgroup.apply(this, arguments))
	}), a.ui.button, a.extend(a.ui, {
		datepicker: {
			version: "1.12.1"
		}
	});
	var p;
	a.extend(d.prototype, {
		markerClassName: "hasDatepicker",
		maxRows: 4,
		_widgetDatepicker: function () {
			return this.dpDiv
		},
		setDefaults: function (a) {
			return g(this._defaults, a || {}), this
		},
		_attachDatepicker: function (b, c) {
			var d, e, f;
			d = b.nodeName.toLowerCase(), e = "div" === d || "span" === d, b.id || (this.uuid += 1, b.id = "dp" + this.uuid), f = this._newInst(a(b), e), f.settings = a.extend({}, c || {}), "input" === d ? this._connectDatepicker(b, f) : e && this._inlineDatepicker(b, f)
		},
		_newInst: function (b, c) {
			return {
				id: b[0].id.replace(/([^A-Za-z0-9_\-])/g, "\\\\$1"),
				input: b,
				selectedDay: 0,
				selectedMonth: 0,
				selectedYear: 0,
				drawMonth: 0,
				drawYear: 0,
				inline: c,
				dpDiv: c ? e(a("<div class='" + this._inlineClass + " ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all'></div>")) : this.dpDiv
			}
		},
		_connectDatepicker: function (b, c) {
			var d = a(b);
			c.append = a([]), c.trigger = a([]), d.hasClass(this.markerClassName) || (this._attachments(d, c), d.addClass(this.markerClassName).on("keydown", this._doKeyDown).on("keypress", this._doKeyPress).on("keyup", this._doKeyUp), this._autoSize(c), a.data(b, "datepicker", c), c.settings.disabled && this._disableDatepicker(b))
		},
		_attachments: function (b, c) {
			var d, e, f, g = this._get(c, "appendText"),
				h = this._get(c, "isRTL");
			c.append && c.append.remove(), g && (c.append = a("<span class='" + this._appendClass + "'>" + g + "</span>"), b[h ? "before" : "after"](c.append)), b.off("focus", this._showDatepicker), c.trigger && c.trigger.remove(), d = this._get(c, "showOn"), ("focus" === d || "both" === d) && b.on("focus", this._showDatepicker), ("button" === d || "both" === d) && (e = this._get(c, "buttonText"), f = this._get(c, "buttonImage"), c.trigger = a(this._get(c, "buttonImageOnly") ? a("<img/>").addClass(this._triggerClass).attr({
				src: f,
				alt: e,
				title: e
			}) : a("<button type='button'></button>").addClass(this._triggerClass).html(f ? a("<img/>").attr({
				src: f,
				alt: e,
				title: e
			}) : e)), b[h ? "before" : "after"](c.trigger), c.trigger.on("click", function () {
				return a.datepicker._datepickerShowing && a.datepicker._lastInput === b[0] ? a.datepicker._hideDatepicker() : a.datepicker._datepickerShowing && a.datepicker._lastInput !== b[0] ? (a.datepicker._hideDatepicker(), a.datepicker._showDatepicker(b[0])) : a.datepicker._showDatepicker(b[0]), !1
			}))
		},
		_autoSize: function (a) {
			if (this._get(a, "autoSize") && !a.inline) {
				var b, c, d, e, f = new Date(2009, 11, 20),
					g = this._get(a, "dateFormat");
				g.match(/[DM]/) && (b = function (a) {
					for (c = 0, d = 0, e = 0; a.length > e; e++) a[e].length > c && (c = a[e].length, d = e);
					return d
				}, f.setMonth(b(this._get(a, g.match(/MM/) ? "monthNames" : "monthNamesShort"))), f.setDate(b(this._get(a, g.match(/DD/) ? "dayNames" : "dayNamesShort")) + 20 - f.getDay())), a.input.attr("size", this._formatDate(a, f).length)
			}
		},
		_inlineDatepicker: function (b, c) {
			var d = a(b);
			d.hasClass(this.markerClassName) || (d.addClass(this.markerClassName).append(c.dpDiv), a.data(b, "datepicker", c), this._setDate(c, this._getDefaultDate(c), !0), this._updateDatepicker(c), this._updateAlternate(c), c.settings.disabled && this._disableDatepicker(b), c.dpDiv.css("display", "block"))
		},
		_dialogDatepicker: function (b, c, d, e, f) {
			var h, i, j, k, l, m = this._dialogInst;
			return m || (this.uuid += 1, h = "dp" + this.uuid, this._dialogInput = a("<input type='text' id='" + h + "' style='position: absolute; top: -100px; width: 0px;'/>"), this._dialogInput.on("keydown", this._doKeyDown), a("body").append(this._dialogInput), m = this._dialogInst = this._newInst(this._dialogInput, !1), m.settings = {}, a.data(this._dialogInput[0], "datepicker", m)), g(m.settings, e || {}), c = c && c.constructor === Date ? this._formatDate(m, c) : c, this._dialogInput.val(c), this._pos = f ? f.length ? f : [f.pageX, f.pageY] : null, this._pos || (i = document.documentElement.clientWidth, j = document.documentElement.clientHeight, k = document.documentElement.scrollLeft || document.body.scrollLeft, l = document.documentElement.scrollTop || document.body.scrollTop, this._pos = [i / 2 - 100 + k, j / 2 - 150 + l]), this._dialogInput.css("left", this._pos[0] + 20 + "px").css("top", this._pos[1] + "px"), m.settings.onSelect = d, this._inDialog = !0, this.dpDiv.addClass(this._dialogClass), this._showDatepicker(this._dialogInput[0]), a.blockUI && a.blockUI(this.dpDiv), a.data(this._dialogInput[0], "datepicker", m), this
		},
		_destroyDatepicker: function (b) {
			var c, d = a(b),
				e = a.data(b, "datepicker");
			d.hasClass(this.markerClassName) && (c = b.nodeName.toLowerCase(), a.removeData(b, "datepicker"), "input" === c ? (e.append.remove(), e.trigger.remove(), d.removeClass(this.markerClassName).off("focus", this._showDatepicker).off("keydown", this._doKeyDown).off("keypress", this._doKeyPress).off("keyup", this._doKeyUp)) : ("div" === c || "span" === c) && d.removeClass(this.markerClassName).empty(), p === e && (p = null))
		},
		_enableDatepicker: function (b) {
			var c, d, e = a(b),
				f = a.data(b, "datepicker");
			e.hasClass(this.markerClassName) && (c = b.nodeName.toLowerCase(), "input" === c ? (b.disabled = !1, f.trigger.filter("button").each(function () {
				this.disabled = !1
			}).end().filter("img").css({
				opacity: "1.0",
				cursor: ""
			})) : ("div" === c || "span" === c) && (d = e.children("." + this._inlineClass), d.children().removeClass("ui-state-disabled"), d.find("select.ui-datepicker-month, select.ui-datepicker-year").prop("disabled", !1)), this._disabledInputs = a.map(this._disabledInputs, function (a) {
				return a === b ? null : a
			}))
		},
		_disableDatepicker: function (b) {
			var c, d, e = a(b),
				f = a.data(b, "datepicker");
			e.hasClass(this.markerClassName) && (c = b.nodeName.toLowerCase(), "input" === c ? (b.disabled = !0, f.trigger.filter("button").each(function () {
				this.disabled = !0
			}).end().filter("img").css({
				opacity: "0.5",
				cursor: "default"
			})) : ("div" === c || "span" === c) && (d = e.children("." + this._inlineClass), d.children().addClass("ui-state-disabled"), d.find("select.ui-datepicker-month, select.ui-datepicker-year").prop("disabled", !0)), this._disabledInputs = a.map(this._disabledInputs, function (a) {
				return a === b ? null : a
			}), this._disabledInputs[this._disabledInputs.length] = b)
		},
		_isDisabledDatepicker: function (a) {
			if (!a) return !1;
			for (var b = 0; this._disabledInputs.length > b; b++)
				if (this._disabledInputs[b] === a) return !0;
			return !1
		},
		_getInst: function (b) {
			try {
				return a.data(b, "datepicker")
			} catch (a) {
				throw "Missing instance data for this datepicker"
			}
		},
		_optionDatepicker: function (b, c, d) {
			var e, f, h, i, j = this._getInst(b);
			return 2 === arguments.length && "string" == typeof c ? "defaults" === c ? a.extend({}, a.datepicker._defaults) : j ? "all" === c ? a.extend({}, j.settings) : this._get(j, c) : null : (e = c || {}, "string" == typeof c && (e = {}, e[c] = d), void(j && (this._curInst === j && this._hideDatepicker(), f = this._getDateDatepicker(b, !0), h = this._getMinMaxDate(j, "min"), i = this._getMinMaxDate(j, "max"), g(j.settings, e), null !== h && void 0 !== e.dateFormat && void 0 === e.minDate && (j.settings.minDate = this._formatDate(j, h)), null !== i && void 0 !== e.dateFormat && void 0 === e.maxDate && (j.settings.maxDate = this._formatDate(j, i)), "disabled" in e && (e.disabled ? this._disableDatepicker(b) : this._enableDatepicker(b)), this._attachments(a(b), j), this._autoSize(j), this._setDate(j, f), this._updateAlternate(j), this._updateDatepicker(j))))
		},
		_changeDatepicker: function (a, b, c) {
			this._optionDatepicker(a, b, c)
		},
		_refreshDatepicker: function (a) {
			var b = this._getInst(a);
			b && this._updateDatepicker(b)
		},
		_setDateDatepicker: function (a, b) {
			var c = this._getInst(a);
			c && (this._setDate(c, b), this._updateDatepicker(c), this._updateAlternate(c))
		},
		_getDateDatepicker: function (a, b) {
			var c = this._getInst(a);
			return c && !c.inline && this._setDateFromField(c, b), c ? this._getDate(c) : null
		},
		_doKeyDown: function (b) {
			var c, d, e, f = a.datepicker._getInst(b.target),
				g = !0,
				h = f.dpDiv.is(".ui-datepicker-rtl");
			if (f._keyEvent = !0, a.datepicker._datepickerShowing) switch (b.keyCode) {
				case 9:
					a.datepicker._hideDatepicker(), g = !1;
					break;
				case 13:
					return e = a("td." + a.datepicker._dayOverClass + ":not(." + a.datepicker._currentClass + ")", f.dpDiv), e[0] && a.datepicker._selectDay(b.target, f.selectedMonth, f.selectedYear, e[0]), c = a.datepicker._get(f, "onSelect"), c ? (d = a.datepicker._formatDate(f), c.apply(f.input ? f.input[0] : null, [d, f])) : a.datepicker._hideDatepicker(), !1;
				case 27:
					a.datepicker._hideDatepicker();
					break;
				case 33:
					a.datepicker._adjustDate(b.target, b.ctrlKey ? -a.datepicker._get(f, "stepBigMonths") : -a.datepicker._get(f, "stepMonths"), "M");
					break;
				case 34:
					a.datepicker._adjustDate(b.target, b.ctrlKey ? +a.datepicker._get(f, "stepBigMonths") : +a.datepicker._get(f, "stepMonths"), "M");
					break;
				case 35:
					(b.ctrlKey || b.metaKey) && a.datepicker._clearDate(b.target), g = b.ctrlKey || b.metaKey;
					break;
				case 36:
					(b.ctrlKey || b.metaKey) && a.datepicker._gotoToday(b.target), g = b.ctrlKey || b.metaKey;
					break;
				case 37:
					(b.ctrlKey || b.metaKey) && a.datepicker._adjustDate(b.target, h ? 1 : -1, "D"), g = b.ctrlKey || b.metaKey, b.originalEvent.altKey && a.datepicker._adjustDate(b.target, b.ctrlKey ? -a.datepicker._get(f, "stepBigMonths") : -a.datepicker._get(f, "stepMonths"), "M");
					break;
				case 38:
					(b.ctrlKey || b.metaKey) && a.datepicker._adjustDate(b.target, -7, "D"), g = b.ctrlKey || b.metaKey;
					break;
				case 39:
					(b.ctrlKey || b.metaKey) && a.datepicker._adjustDate(b.target, h ? -1 : 1, "D"), g = b.ctrlKey || b.metaKey, b.originalEvent.altKey && a.datepicker._adjustDate(b.target, b.ctrlKey ? +a.datepicker._get(f, "stepBigMonths") : +a.datepicker._get(f, "stepMonths"), "M");
					break;
				case 40:
					(b.ctrlKey || b.metaKey) && a.datepicker._adjustDate(b.target, 7, "D"), g = b.ctrlKey || b.metaKey;
					break;
				default:
					g = !1
			} else 36 === b.keyCode && b.ctrlKey ? a.datepicker._showDatepicker(this) : g = !1;
			g && (b.preventDefault(), b.stopPropagation())
		},
		_doKeyPress: function (b) {
			var c, d, e = a.datepicker._getInst(b.target);
			return a.datepicker._get(e, "constrainInput") ? (c = a.datepicker._possibleChars(a.datepicker._get(e, "dateFormat")), d = String.fromCharCode(null == b.charCode ? b.keyCode : b.charCode), b.ctrlKey || b.metaKey || " " > d || !c || c.indexOf(d) > -1) : void 0
		},
		_doKeyUp: function (b) {
			var c = a.datepicker._getInst(b.target);
			if (c.input.val() !== c.lastVal) try {
				a.datepicker.parseDate(a.datepicker._get(c, "dateFormat"), c.input ? c.input.val() : null, a.datepicker._getFormatConfig(c)) && (a.datepicker._setDateFromField(c), a.datepicker._updateAlternate(c), a.datepicker._updateDatepicker(c))
			} catch (a) {}
			return !0
		},
		_showDatepicker: function (b) {
			if (b = b.target || b, "input" !== b.nodeName.toLowerCase() && (b = a("input", b.parentNode)[0]), !a.datepicker._isDisabledDatepicker(b) && a.datepicker._lastInput !== b) {
				var d, e, f, h, i, j, k;
				d = a.datepicker._getInst(b), a.datepicker._curInst && a.datepicker._curInst !== d && (a.datepicker._curInst.dpDiv.stop(!0, !0), d && a.datepicker._datepickerShowing && a.datepicker._hideDatepicker(a.datepicker._curInst.input[0])), e = a.datepicker._get(d, "beforeShow"), !1 !== (f = e ? e.apply(b, [b, d]) : {}) && (g(d.settings, f), d.lastVal = null, a.datepicker._lastInput = b, a.datepicker._setDateFromField(d), a.datepicker._inDialog && (b.value = ""), a.datepicker._pos || (a.datepicker._pos = a.datepicker._findPos(b), a.datepicker._pos[1] += b.offsetHeight), h = !1, a(b).parents().each(function () {
					return !(h |= "fixed" === a(this).css("position"))
				}), i = {
					left: a.datepicker._pos[0],
					top: a.datepicker._pos[1]
				}, a.datepicker._pos = null, d.dpDiv.empty(), d.dpDiv.css({
					position: "absolute",
					display: "block",
					top: "-1000px"
				}), a.datepicker._updateDatepicker(d), i = a.datepicker._checkOffset(d, i, h), d.dpDiv.css({
					position: a.datepicker._inDialog && a.blockUI ? "static" : h ? "fixed" : "absolute",
					display: "none",
					left: i.left + "px",
					top: i.top + "px"
				}), d.inline || (j = a.datepicker._get(d, "showAnim"), k = a.datepicker._get(d, "duration"), d.dpDiv.css("z-index", c(a(b)) + 1), a.datepicker._datepickerShowing = !0, a.effects && a.effects.effect[j] ? d.dpDiv.show(j, a.datepicker._get(d, "showOptions"), k) : d.dpDiv[j || "show"](j ? k : null), a.datepicker._shouldFocusInput(d) && d.input.trigger("focus"), a.datepicker._curInst = d))
			}
		},
		_updateDatepicker: function (b) {
			this.maxRows = 4, p = b, b.dpDiv.empty().append(this._generateHTML(b)), this._attachHandlers(b);
			var c, d = this._getNumberOfMonths(b),
				e = d[1],
				g = b.dpDiv.find("." + this._dayOverClass + " a");
			g.length > 0 && f.apply(g.get(0)), b.dpDiv.removeClass("ui-datepicker-multi-2 ui-datepicker-multi-3 ui-datepicker-multi-4").width(""), e > 1 && b.dpDiv.addClass("ui-datepicker-multi-" + e).css("width", 17 * e + "em"), b.dpDiv[(1 !== d[0] || 1 !== d[1] ? "add" : "remove") + "Class"]("ui-datepicker-multi"), b.dpDiv[(this._get(b, "isRTL") ? "add" : "remove") + "Class"]("ui-datepicker-rtl"), b === a.datepicker._curInst && a.datepicker._datepickerShowing && a.datepicker._shouldFocusInput(b) && b.input.trigger("focus"), b.yearshtml && (c = b.yearshtml, setTimeout(function () {
				c === b.yearshtml && b.yearshtml && b.dpDiv.find("select.ui-datepicker-year:first").replaceWith(b.yearshtml), c = b.yearshtml = null
			}, 0))
		},
		_shouldFocusInput: function (a) {
			return a.input && a.input.is(":visible") && !a.input.is(":disabled") && !a.input.is(":focus")
		},
		_checkOffset: function (b, c, d) {
			var e = b.dpDiv.outerWidth(),
				f = b.dpDiv.outerHeight(),
				g = b.input ? b.input.outerWidth() : 0,
				h = b.input ? b.input.outerHeight() : 0,
				i = document.documentElement.clientWidth + (d ? 0 : a(document).scrollLeft()),
				j = document.documentElement.clientHeight + (d ? 0 : a(document).scrollTop());
			return c.left -= this._get(b, "isRTL") ? e - g : 0, c.left -= d && c.left === b.input.offset().left ? a(document).scrollLeft() : 0, c.top -= d && c.top === b.input.offset().top + h ? a(document).scrollTop() : 0, c.left -= Math.min(c.left, c.left + e > i && i > e ? Math.abs(c.left + e - i) : 0), c.top -= Math.min(c.top, c.top + f > j && j > f ? Math.abs(f + h) : 0), c
		},
		_findPos: function (b) {
			for (var c, d = this._getInst(b), e = this._get(d, "isRTL"); b && ("hidden" === b.type || 1 !== b.nodeType || a.expr.filters.hidden(b));) b = b[e ? "previousSibling" : "nextSibling"];
			return c = a(b).offset(), [c.left, c.top]
		},
		_hideDatepicker: function (b) {
			var c, d, e, f, g = this._curInst;
			!g || b && g !== a.data(b, "datepicker") || this._datepickerShowing && (c = this._get(g, "showAnim"), d = this._get(g, "duration"), e = function () {
				a.datepicker._tidyDialog(g)
			}, a.effects && (a.effects.effect[c] || a.effects[c]) ? g.dpDiv.hide(c, a.datepicker._get(g, "showOptions"), d, e) : g.dpDiv["slideDown" === c ? "slideUp" : "fadeIn" === c ? "fadeOut" : "hide"](c ? d : null, e), c || e(), this._datepickerShowing = !1, f = this._get(g, "onClose"), f && f.apply(g.input ? g.input[0] : null, [g.input ? g.input.val() : "", g]), this._lastInput = null, this._inDialog && (this._dialogInput.css({
				position: "absolute",
				left: "0",
				top: "-100px"
			}), a.blockUI && (a.unblockUI(), a("body").append(this.dpDiv))), this._inDialog = !1)
		},
		_tidyDialog: function (a) {
			a.dpDiv.removeClass(this._dialogClass).off(".ui-datepicker-calendar")
		},
		_checkExternalClick: function (b) {
			if (a.datepicker._curInst) {
				var c = a(b.target),
					d = a.datepicker._getInst(c[0]);
				(c[0].id !== a.datepicker._mainDivId && 0 === c.parents("#" + a.datepicker._mainDivId).length && !c.hasClass(a.datepicker.markerClassName) && !c.closest("." + a.datepicker._triggerClass).length && a.datepicker._datepickerShowing && (!a.datepicker._inDialog || !a.blockUI) || c.hasClass(a.datepicker.markerClassName) && a.datepicker._curInst !== d) && a.datepicker._hideDatepicker()
			}
		},
		_adjustDate: function (b, c, d) {
			var e = a(b),
				f = this._getInst(e[0]);
			this._isDisabledDatepicker(e[0]) || (this._adjustInstDate(f, c + ("M" === d ? this._get(f, "showCurrentAtPos") : 0), d), this._updateDatepicker(f))
		},
		_gotoToday: function (b) {
			var c, d = a(b),
				e = this._getInst(d[0]);
			this._get(e, "gotoCurrent") && e.currentDay ? (e.selectedDay = e.currentDay, e.drawMonth = e.selectedMonth = e.currentMonth, e.drawYear = e.selectedYear = e.currentYear) : (c = new Date, e.selectedDay = c.getDate(), e.drawMonth = e.selectedMonth = c.getMonth(), e.drawYear = e.selectedYear = c.getFullYear()), this._notifyChange(e), this._adjustDate(d)
		},
		_selectMonthYear: function (b, c, d) {
			var e = a(b),
				f = this._getInst(e[0]);
			f["selected" + ("M" === d ? "Month" : "Year")] = f["draw" + ("M" === d ? "Month" : "Year")] = parseInt(c.options[c.selectedIndex].value, 10), this._notifyChange(f), this._adjustDate(e)
		},
		_selectDay: function (b, c, d, e) {
			var f, g = a(b);
			a(e).hasClass(this._unselectableClass) || this._isDisabledDatepicker(g[0]) || (f = this._getInst(g[0]), f.selectedDay = f.currentDay = a("a", e).html(), f.selectedMonth = f.currentMonth = c, f.selectedYear = f.currentYear = d, this._selectDate(b, this._formatDate(f, f.currentDay, f.currentMonth, f.currentYear)))
		},
		_clearDate: function (b) {
			var c = a(b);
			this._selectDate(c, "")
		},
		_selectDate: function (b, c) {
			var d, e = a(b),
				f = this._getInst(e[0]);
			c = null != c ? c : this._formatDate(f), f.input && f.input.val(c), this._updateAlternate(f), d = this._get(f, "onSelect"), d ? d.apply(f.input ? f.input[0] : null, [c, f]) : f.input && f.input.trigger("change"), f.inline ? this._updateDatepicker(f) : (this._hideDatepicker(), this._lastInput = f.input[0], "object" != typeof f.input[0] && f.input.trigger("focus"), this._lastInput = null)
		},
		_updateAlternate: function (b) {
			var c, d, e, f = this._get(b, "altField");
			f && (c = this._get(b, "altFormat") || this._get(b, "dateFormat"), d = this._getDate(b), e = this.formatDate(c, d, this._getFormatConfig(b)), a(f).val(e))
		},
		noWeekends: function (a) {
			var b = a.getDay();
			return [b > 0 && 6 > b, ""]
		},
		iso8601Week: function (a) {
			var b, c = new Date(a.getTime());
			return c.setDate(c.getDate() + 4 - (c.getDay() || 7)), b = c.getTime(), c.setMonth(0), c.setDate(1), Math.floor(Math.round((b - c) / 864e5) / 7) + 1
		},
		parseDate: function (b, c, d) {
			if (null == b || null == c) throw "Invalid arguments";
			if ("" === (c = "object" == typeof c ? "" + c : c + "")) return null;
			var e, f, g, h, i = 0,
				j = (d ? d.shortYearCutoff : null) || this._defaults.shortYearCutoff,
				k = "string" != typeof j ? j : (new Date).getFullYear() % 100 + parseInt(j, 10),
				l = (d ? d.dayNamesShort : null) || this._defaults.dayNamesShort,
				m = (d ? d.dayNames : null) || this._defaults.dayNames,
				n = (d ? d.monthNamesShort : null) || this._defaults.monthNamesShort,
				o = (d ? d.monthNames : null) || this._defaults.monthNames,
				p = -1,
				q = -1,
				r = -1,
				s = -1,
				t = !1,
				u = function (a) {
					var c = b.length > e + 1 && b.charAt(e + 1) === a;
					return c && e++, c
				},
				v = function (a) {
					var b = u(a),
						d = "@" === a ? 14 : "!" === a ? 20 : "y" === a && b ? 4 : "o" === a ? 3 : 2,
						e = "y" === a ? d : 1,
						f = RegExp("^\\d{" + e + "," + d + "}"),
						g = c.substring(i).match(f);
					if (!g) throw "Missing number at position " + i;
					return i += g[0].length, parseInt(g[0], 10)
				},
				w = function (b, d, e) {
					var f = -1,
						g = a.map(u(b) ? e : d, function (a, b) {
							return [
								[b, a]
							]
						}).sort(function (a, b) {
							return -(a[1].length - b[1].length)
						});
					if (a.each(g, function (a, b) {
							var d = b[1];
							return c.substr(i, d.length).toLowerCase() === d.toLowerCase() ? (f = b[0], i += d.length, !1) : void 0
						}), -1 !== f) return f + 1;
					throw "Unknown name at position " + i
				},
				x = function () {
					if (c.charAt(i) !== b.charAt(e)) throw "Unexpected literal at position " + i;
					i++
				};
			for (e = 0; b.length > e; e++)
				if (t) "'" !== b.charAt(e) || u("'") ? x() : t = !1;
				else switch (b.charAt(e)) {
					case "d":
						r = v("d");
						break;
					case "D":
						w("D", l, m);
						break;
					case "o":
						s = v("o");
						break;
					case "m":
						q = v("m");
						break;
					case "M":
						q = w("M", n, o);
						break;
					case "y":
						p = v("y");
						break;
					case "@":
						h = new Date(v("@")), p = h.getFullYear(), q = h.getMonth() + 1, r = h.getDate();
						break;
					case "!":
						h = new Date((v("!") - this._ticksTo1970) / 1e4), p = h.getFullYear(), q = h.getMonth() + 1, r = h.getDate();
						break;
					case "'":
						u("'") ? x() : t = !0;
						break;
					default:
						x()
				}
			if (c.length > i && (g = c.substr(i), !/^\s+/.test(g))) throw "Extra/unparsed characters found in date: " + g;
			if (-1 === p ? p = (new Date).getFullYear() : 100 > p && (p += (new Date).getFullYear() - (new Date).getFullYear() % 100 + (k >= p ? 0 : -100)), s > -1)
				for (q = 1, r = s; !((f = this._getDaysInMonth(p, q - 1)) >= r);) q++, r -= f;
			if (h = this._daylightSavingAdjust(new Date(p, q - 1, r)), h.getFullYear() !== p || h.getMonth() + 1 !== q || h.getDate() !== r) throw "Invalid date";
			return h
		},
		ATOM: "yy-mm-dd",
		COOKIE: "D, dd M yy",
		ISO_8601: "yy-mm-dd",
		RFC_822: "D, d M y",
		RFC_850: "DD, dd-M-y",
		RFC_1036: "D, d M y",
		RFC_1123: "D, d M yy",
		RFC_2822: "D, d M yy",
		RSS: "D, d M y",
		TICKS: "!",
		TIMESTAMP: "@",
		W3C: "yy-mm-dd",
		_ticksTo1970: 864e9 * (718685 + Math.floor(492.5) - Math.floor(19.7) + Math.floor(4.925)),
		formatDate: function (a, b, c) {
			if (!b) return "";
			var d, e = (c ? c.dayNamesShort : null) || this._defaults.dayNamesShort,
				f = (c ? c.dayNames : null) || this._defaults.dayNames,
				g = (c ? c.monthNamesShort : null) || this._defaults.monthNamesShort,
				h = (c ? c.monthNames : null) || this._defaults.monthNames,
				i = function (b) {
					var c = a.length > d + 1 && a.charAt(d + 1) === b;
					return c && d++, c
				},
				j = function (a, b, c) {
					var d = "" + b;
					if (i(a))
						for (; c > d.length;) d = "0" + d;
					return d
				},
				k = function (a, b, c, d) {
					return i(a) ? d[b] : c[b]
				},
				l = "",
				m = !1;
			if (b)
				for (d = 0; a.length > d; d++)
					if (m) "'" !== a.charAt(d) || i("'") ? l += a.charAt(d) : m = !1;
					else switch (a.charAt(d)) {
						case "d":
							l += j("d", b.getDate(), 2);
							break;
						case "D":
							l += k("D", b.getDay(), e, f);
							break;
						case "o":
							l += j("o", Math.round((new Date(b.getFullYear(), b.getMonth(), b.getDate()).getTime() - new Date(b.getFullYear(), 0, 0).getTime()) / 864e5), 3);
							break;
						case "m":
							l += j("m", b.getMonth() + 1, 2);
							break;
						case "M":
							l += k("M", b.getMonth(), g, h);
							break;
						case "y":
							l += i("y") ? b.getFullYear() : (10 > b.getFullYear() % 100 ? "0" : "") + b.getFullYear() % 100;
							break;
						case "@":
							l += b.getTime();
							break;
						case "!":
							l += 1e4 * b.getTime() + this._ticksTo1970;
							break;
						case "'":
							i("'") ? l += "'" : m = !0;
							break;
						default:
							l += a.charAt(d)
					}
			return l
		},
		_possibleChars: function (a) {
			var b, c = "",
				d = !1,
				e = function (c) {
					var d = a.length > b + 1 && a.charAt(b + 1) === c;
					return d && b++, d
				};
			for (b = 0; a.length > b; b++)
				if (d) "'" !== a.charAt(b) || e("'") ? c += a.charAt(b) : d = !1;
				else switch (a.charAt(b)) {
					case "d":
					case "m":
					case "y":
					case "@":
						c += "0123456789";
						break;
					case "D":
					case "M":
						return null;
					case "'":
						e("'") ? c += "'" : d = !0;
						break;
					default:
						c += a.charAt(b)
				}
			return c
		},
		_get: function (a, b) {
			return void 0 !== a.settings[b] ? a.settings[b] : this._defaults[b]
		},
		_setDateFromField: function (a, b) {
			if (a.input.val() !== a.lastVal) {
				var c = this._get(a, "dateFormat"),
					d = a.lastVal = a.input ? a.input.val() : null,
					e = this._getDefaultDate(a),
					f = e,
					g = this._getFormatConfig(a);
				try {
					f = this.parseDate(c, d, g) || e
				} catch (a) {
					d = b ? "" : d
				}
				a.selectedDay = f.getDate(), a.drawMonth = a.selectedMonth = f.getMonth(), a.drawYear = a.selectedYear = f.getFullYear(), a.currentDay = d ? f.getDate() : 0, a.currentMonth = d ? f.getMonth() : 0, a.currentYear = d ? f.getFullYear() : 0, this._adjustInstDate(a)
			}
		},
		_getDefaultDate: function (a) {
			return this._restrictMinMax(a, this._determineDate(a, this._get(a, "defaultDate"), new Date))
		},
		_determineDate: function (b, c, d) {
			var e = null == c || "" === c ? d : "string" == typeof c ? function (c) {
				try {
					return a.datepicker.parseDate(a.datepicker._get(b, "dateFormat"), c, a.datepicker._getFormatConfig(b))
				} catch (a) {}
				for (var d = (c.toLowerCase().match(/^c/) ? a.datepicker._getDate(b) : null) || new Date, e = d.getFullYear(), f = d.getMonth(), g = d.getDate(), h = /([+\-]?[0-9]+)\s*(d|D|w|W|m|M|y|Y)?/g, i = h.exec(c); i;) {
					switch (i[2] || "d") {
						case "d":
						case "D":
							g += parseInt(i[1], 10);
							break;
						case "w":
						case "W":
							g += 7 * parseInt(i[1], 10);
							break;
						case "m":
						case "M":
							f += parseInt(i[1], 10), g = Math.min(g, a.datepicker._getDaysInMonth(e, f));
							break;
						case "y":
						case "Y":
							e += parseInt(i[1], 10), g = Math.min(g, a.datepicker._getDaysInMonth(e, f))
					}
					i = h.exec(c)
				}
				return new Date(e, f, g)
			}(c) : "number" == typeof c ? isNaN(c) ? d : function (a) {
				var b = new Date;
				return b.setDate(b.getDate() + a), b
			}(c) : new Date(c.getTime());
			return e = e && "Invalid Date" == "" + e ? d : e, e && (e.setHours(0), e.setMinutes(0), e.setSeconds(0), e.setMilliseconds(0)), this._daylightSavingAdjust(e)
		},
		_daylightSavingAdjust: function (a) {
			return a ? (a.setHours(a.getHours() > 12 ? a.getHours() + 2 : 0), a) : null
		},
		_setDate: function (a, b, c) {
			var d = !b,
				e = a.selectedMonth,
				f = a.selectedYear,
				g = this._restrictMinMax(a, this._determineDate(a, b, new Date));
			a.selectedDay = a.currentDay = g.getDate(), a.drawMonth = a.selectedMonth = a.currentMonth = g.getMonth(), a.drawYear = a.selectedYear = a.currentYear = g.getFullYear(), e === a.selectedMonth && f === a.selectedYear || c || this._notifyChange(a), this._adjustInstDate(a), a.input && a.input.val(d ? "" : this._formatDate(a))
		},
		_getDate: function (a) {
			return !a.currentYear || a.input && "" === a.input.val() ? null : this._daylightSavingAdjust(new Date(a.currentYear, a.currentMonth, a.currentDay))
		},
		_attachHandlers: function (b) {
			var c = this._get(b, "stepMonths"),
				d = "#" + b.id.replace(/\\\\/g, "\\");
			b.dpDiv.find("[data-handler]").map(function () {
				var b = {
					prev: function () {
						a.datepicker._adjustDate(d, -c, "M")
					},
					next: function () {
						a.datepicker._adjustDate(d, +c, "M")
					},
					hide: function () {
						a.datepicker._hideDatepicker()
					},
					today: function () {
						a.datepicker._gotoToday(d)
					},
					selectDay: function () {
						return a.datepicker._selectDay(d, +this.getAttribute("data-month"), +this.getAttribute("data-year"), this), !1
					},
					selectMonth: function () {
						return a.datepicker._selectMonthYear(d, this, "M"), !1
					},
					selectYear: function () {
						return a.datepicker._selectMonthYear(d, this, "Y"), !1
					}
				};
				a(this).on(this.getAttribute("data-event"), b[this.getAttribute("data-handler")])
			})
		},
		_generateHTML: function (a) {
			var b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O = new Date,
				P = this._daylightSavingAdjust(new Date(O.getFullYear(), O.getMonth(), O.getDate())),
				Q = this._get(a, "isRTL"),
				R = this._get(a, "showButtonPanel"),
				S = this._get(a, "hideIfNoPrevNext"),
				T = this._get(a, "navigationAsDateFormat"),
				U = this._getNumberOfMonths(a),
				V = this._get(a, "showCurrentAtPos"),
				W = this._get(a, "stepMonths"),
				X = 1 !== U[0] || 1 !== U[1],
				Y = this._daylightSavingAdjust(a.currentDay ? new Date(a.currentYear, a.currentMonth, a.currentDay) : new Date(9999, 9, 9)),
				Z = this._getMinMaxDate(a, "min"),
				$ = this._getMinMaxDate(a, "max"),
				_ = a.drawMonth - V,
				aa = a.drawYear;
			if (0 > _ && (_ += 12, aa--), $)
				for (b = this._daylightSavingAdjust(new Date($.getFullYear(), $.getMonth() - U[0] * U[1] + 1, $.getDate())), b = Z && Z > b ? Z : b; this._daylightSavingAdjust(new Date(aa, _, 1)) > b;) 0 > --_ && (_ = 11, aa--);
			for (a.drawMonth = _, a.drawYear = aa, c = this._get(a, "prevText"), c = T ? this.formatDate(c, this._daylightSavingAdjust(new Date(aa, _ - W, 1)), this._getFormatConfig(a)) : c, d = this._canAdjustMonth(a, -1, aa, _) ? "<a class='ui-datepicker-prev ui-corner-all' data-handler='prev' data-event='click' title='" + c + "'><span class='ui-icon ui-icon-circle-triangle-" + (Q ? "e" : "w") + "'>" + c + "</span></a>" : S ? "" : "<a class='ui-datepicker-prev ui-corner-all ui-state-disabled' title='" + c + "'><span class='ui-icon ui-icon-circle-triangle-" + (Q ? "e" : "w") + "'>" + c + "</span></a>", e = this._get(a, "nextText"), e = T ? this.formatDate(e, this._daylightSavingAdjust(new Date(aa, _ + W, 1)), this._getFormatConfig(a)) : e, f = this._canAdjustMonth(a, 1, aa, _) ? "<a class='ui-datepicker-next ui-corner-all' data-handler='next' data-event='click' title='" + e + "'><span class='ui-icon ui-icon-circle-triangle-" + (Q ? "w" : "e") + "'>" + e + "</span></a>" : S ? "" : "<a class='ui-datepicker-next ui-corner-all ui-state-disabled' title='" + e + "'><span class='ui-icon ui-icon-circle-triangle-" + (Q ? "w" : "e") + "'>" + e + "</span></a>", g = this._get(a, "currentText"), h = this._get(a, "gotoCurrent") && a.currentDay ? Y : P, g = T ? this.formatDate(g, h, this._getFormatConfig(a)) : g, i = a.inline ? "" : "<button type='button' class='ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all' data-handler='hide' data-event='click'>" + this._get(a, "closeText") + "</button>", j = R ? "<div class='ui-datepicker-buttonpane ui-widget-content'>" + (Q ? i : "") + (this._isInRange(a, h) ? "<button type='button' class='ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all' data-handler='today' data-event='click'>" + g + "</button>" : "") + (Q ? "" : i) + "</div>" : "", k = parseInt(this._get(a, "firstDay"), 10), k = isNaN(k) ? 0 : k, l = this._get(a, "showWeek"), m = this._get(a, "dayNames"), n = this._get(a, "dayNamesMin"), o = this._get(a, "monthNames"), p = this._get(a, "monthNamesShort"), q = this._get(a, "beforeShowDay"), r = this._get(a, "showOtherMonths"), s = this._get(a, "selectOtherMonths"), t = this._getDefaultDate(a), u = "", w = 0; U[0] > w; w++) {
				for (x = "", this.maxRows = 4, y = 0; U[1] > y; y++) {
					if (z = this._daylightSavingAdjust(new Date(aa, _, a.selectedDay)), A = " ui-corner-all", B = "", X) {
						if (B += "<div class='ui-datepicker-group", U[1] > 1) switch (y) {
							case 0:
								B += " ui-datepicker-group-first", A = " ui-corner-" + (Q ? "right" : "left");
								break;
							case U[1] - 1:
								B += " ui-datepicker-group-last", A = " ui-corner-" + (Q ? "left" : "right");
								break;
							default:
								B += " ui-datepicker-group-middle", A = ""
						}
						B += "'>"
					}
					for (B += "<div class='ui-datepicker-header ui-widget-header ui-helper-clearfix" + A + "'>" + (/all|left/.test(A) && 0 === w ? Q ? f : d : "") + (/all|right/.test(A) && 0 === w ? Q ? d : f : "") + this._generateMonthYearHeader(a, _, aa, Z, $, w > 0 || y > 0, o, p) + "</div><table class='ui-datepicker-calendar'><thead><tr>", C = l ? "<th class='ui-datepicker-week-col'>" + this._get(a, "weekHeader") + "</th>" : "", v = 0; 7 > v; v++) D = (v + k) % 7, C += "<th scope='col'" + ((v + k + 6) % 7 >= 5 ? " class='ui-datepicker-week-end'" : "") + "><span title='" + m[D] + "'>" + n[D] + "</span></th>";
					for (B += C + "</tr></thead><tbody>", E = this._getDaysInMonth(aa, _), aa === a.selectedYear && _ === a.selectedMonth && (a.selectedDay = Math.min(a.selectedDay, E)), F = (this._getFirstDayOfMonth(aa, _) - k + 7) % 7, G = Math.ceil((F + E) / 7), H = X && this.maxRows > G ? this.maxRows : G, this.maxRows = H, I = this._daylightSavingAdjust(new Date(aa, _, 1 - F)), J = 0; H > J; J++) {
						for (B += "<tr>", K = l ? "<td class='ui-datepicker-week-col'>" + this._get(a, "calculateWeek")(I) + "</td>" : "", v = 0; 7 > v; v++) L = q ? q.apply(a.input ? a.input[0] : null, [I]) : [!0, ""], M = I.getMonth() !== _, N = M && !s || !L[0] || Z && Z > I || $ && I > $, K += "<td class='" + ((v + k + 6) % 7 >= 5 ? " ui-datepicker-week-end" : "") + (M ? " ui-datepicker-other-month" : "") + (I.getTime() === z.getTime() && _ === a.selectedMonth && a._keyEvent || t.getTime() === I.getTime() && t.getTime() === z.getTime() ? " " + this._dayOverClass : "") + (N ? " " + this._unselectableClass + " ui-state-disabled" : "") + (M && !r ? "" : " " + L[1] + (I.getTime() === Y.getTime() ? " " + this._currentClass : "") + (I.getTime() === P.getTime() ? " ui-datepicker-today" : "")) + "'" + (M && !r || !L[2] ? "" : " title='" + L[2].replace(/'/g, "&#39;") + "'") + (N ? "" : " data-handler='selectDay' data-event='click' data-month='" + I.getMonth() + "' data-year='" + I.getFullYear() + "'") + ">" + (M && !r ? "&#xa0;" : N ? "<span class='ui-state-default'>" + I.getDate() + "</span>" : "<a class='ui-state-default" + (I.getTime() === P.getTime() ? " ui-state-highlight" : "") + (I.getTime() === Y.getTime() ? " ui-state-active" : "") + (M ? " ui-priority-secondary" : "") + "' href='#'>" + I.getDate() + "</a>") + "</td>", I.setDate(I.getDate() + 1), I = this._daylightSavingAdjust(I);
						B += K + "</tr>"
					}
					_++, _ > 11 && (_ = 0, aa++), B += "</tbody></table>" + (X ? "</div>" + (U[0] > 0 && y === U[1] - 1 ? "<div class='ui-datepicker-row-break'></div>" : "") : ""), x += B
				}
				u += x
			}
			return u += j, a._keyEvent = !1, u
		},
		_generateMonthYearHeader: function (a, b, c, d, e, f, g, h) {
			var i, j, k, l, m, n, o, p, q = this._get(a, "changeMonth"),
				r = this._get(a, "changeYear"),
				s = this._get(a, "showMonthAfterYear"),
				t = "<div class='ui-datepicker-title'>",
				u = "";
			if (f || !q) u += "<span class='ui-datepicker-month'>" + g[b] + "</span>";
			else {
				for (i = d && d.getFullYear() === c, j = e && e.getFullYear() === c, u += "<select class='ui-datepicker-month' data-handler='selectMonth' data-event='change'>", k = 0; 12 > k; k++)(!i || k >= d.getMonth()) && (!j || e.getMonth() >= k) && (u += "<option value='" + k + "'" + (k === b ? " selected='selected'" : "") + ">" + h[k] + "</option>");
				u += "</select>"
			}
			if (s || (t += u + (!f && q && r ? "" : "&#xa0;")), !a.yearshtml)
				if (a.yearshtml = "", f || !r) t += "<span class='ui-datepicker-year'>" + c + "</span>";
				else {
					for (l = this._get(a, "yearRange").split(":"), m = (new Date).getFullYear(), n = function (a) {
							var b = a.match(/c[+\-].*/) ? c + parseInt(a.substring(1), 10) : a.match(/[+\-].*/) ? m + parseInt(a, 10) : parseInt(a, 10);
							return isNaN(b) ? m : b
						}, o = n(l[0]), p = Math.max(o, n(l[1] || "")), o = d ? Math.max(o, d.getFullYear()) : o, p = e ? Math.min(p, e.getFullYear()) : p, a.yearshtml += "<select class='ui-datepicker-year' data-handler='selectYear' data-event='change'>"; p >= o; o++) a.yearshtml += "<option value='" + o + "'" + (o === c ? " selected='selected'" : "") + ">" + o + "</option>";
					a.yearshtml += "</select>", t += a.yearshtml, a.yearshtml = null
				}
			return t += this._get(a, "yearSuffix"), s && (t += (!f && q && r ? "" : "&#xa0;") + u), t += "</div>"
		},
		_adjustInstDate: function (a, b, c) {
			var d = a.selectedYear + ("Y" === c ? b : 0),
				e = a.selectedMonth + ("M" === c ? b : 0),
				f = Math.min(a.selectedDay, this._getDaysInMonth(d, e)) + ("D" === c ? b : 0),
				g = this._restrictMinMax(a, this._daylightSavingAdjust(new Date(d, e, f)));
			a.selectedDay = g.getDate(), a.drawMonth = a.selectedMonth = g.getMonth(), a.drawYear = a.selectedYear = g.getFullYear(), ("M" === c || "Y" === c) && this._notifyChange(a)
		},
		_restrictMinMax: function (a, b) {
			var c = this._getMinMaxDate(a, "min"),
				d = this._getMinMaxDate(a, "max"),
				e = c && c > b ? c : b;
			return d && e > d ? d : e
		},
		_notifyChange: function (a) {
			var b = this._get(a, "onChangeMonthYear");
			b && b.apply(a.input ? a.input[0] : null, [a.selectedYear, a.selectedMonth + 1, a])
		},
		_getNumberOfMonths: function (a) {
			var b = this._get(a, "numberOfMonths");
			return null == b ? [1, 1] : "number" == typeof b ? [1, b] : b
		},
		_getMinMaxDate: function (a, b) {
			return this._determineDate(a, this._get(a, b + "Date"), null)
		},
		_getDaysInMonth: function (a, b) {
			return 32 - this._daylightSavingAdjust(new Date(a, b, 32)).getDate()
		},
		_getFirstDayOfMonth: function (a, b) {
			return new Date(a, b, 1).getDay()
		},
		_canAdjustMonth: function (a, b, c, d) {
			var e = this._getNumberOfMonths(a),
				f = this._daylightSavingAdjust(new Date(c, d + (0 > b ? b : e[0] * e[1]), 1));
			return 0 > b && f.setDate(this._getDaysInMonth(f.getFullYear(), f.getMonth())), this._isInRange(a, f)
		},
		_isInRange: function (a, b) {
			var c, d, e = this._getMinMaxDate(a, "min"),
				f = this._getMinMaxDate(a, "max"),
				g = null,
				h = null,
				i = this._get(a, "yearRange");
			return i && (c = i.split(":"), d = (new Date).getFullYear(), g = parseInt(c[0], 10), h = parseInt(c[1], 10), c[0].match(/[+\-].*/) && (g += d), c[1].match(/[+\-].*/) && (h += d)), (!e || b.getTime() >= e.getTime()) && (!f || b.getTime() <= f.getTime()) && (!g || b.getFullYear() >= g) && (!h || h >= b.getFullYear())
		},
		_getFormatConfig: function (a) {
			var b = this._get(a, "shortYearCutoff");
			return b = "string" != typeof b ? b : (new Date).getFullYear() % 100 + parseInt(b, 10), {
				shortYearCutoff: b,
				dayNamesShort: this._get(a, "dayNamesShort"),
				dayNames: this._get(a, "dayNames"),
				monthNamesShort: this._get(a, "monthNamesShort"),
				monthNames: this._get(a, "monthNames")
			}
		},
		_formatDate: function (a, b, c, d) {
			b || (a.currentDay = a.selectedDay, a.currentMonth = a.selectedMonth, a.currentYear = a.selectedYear);
			var e = b ? "object" == typeof b ? b : this._daylightSavingAdjust(new Date(d, c, b)) : this._daylightSavingAdjust(new Date(a.currentYear, a.currentMonth, a.currentDay));
			return this.formatDate(this._get(a, "dateFormat"), e, this._getFormatConfig(a))
		}
	}), a.fn.datepicker = function (b) {
		if (!this.length) return this;
		a.datepicker.initialized || (a(document).on("mousedown", a.datepicker._checkExternalClick), a.datepicker.initialized = !0), 0 === a("#" + a.datepicker._mainDivId).length && a("body").append(a.datepicker.dpDiv);
		var c = Array.prototype.slice.call(arguments, 1);
		return "string" != typeof b || "isDisabled" !== b && "getDate" !== b && "widget" !== b ? "option" === b && 2 === arguments.length && "string" == typeof arguments[1] ? a.datepicker["_" + b + "Datepicker"].apply(a.datepicker, [this[0]].concat(c)) : this.each(function () {
			"string" == typeof b ? a.datepicker["_" + b + "Datepicker"].apply(a.datepicker, [this].concat(c)) : a.datepicker._attachDatepicker(this, b)
		}) : a.datepicker["_" + b + "Datepicker"].apply(a.datepicker, [this[0]].concat(c))
	}, a.datepicker = new d, a.datepicker.initialized = !1, a.datepicker.uuid = (new Date).getTime(), a.datepicker.version = "1.12.1", a.datepicker, a.ui.ie = !!/msie [\w.]+/.exec(navigator.userAgent.toLowerCase());
	var q = !1;
	a(document).on("mouseup", function () {
		q = !1
	}), a.widget("ui.mouse", {
		version: "1.12.1",
		options: {
			cancel: "input, textarea, button, select, option",
			distance: 1,
			delay: 0
		},
		_mouseInit: function () {
			var b = this;
			this.element.on("mousedown." + this.widgetName, function (a) {
				return b._mouseDown(a)
			}).on("click." + this.widgetName, function (c) {
				return !0 === a.data(c.target, b.widgetName + ".preventClickEvent") ? (a.removeData(c.target, b.widgetName + ".preventClickEvent"), c.stopImmediatePropagation(), !1) : void 0
			}), this.started = !1
		},
		_mouseDestroy: function () {
			this.element.off("." + this.widgetName), this._mouseMoveDelegate && this.document.off("mousemove." + this.widgetName, this._mouseMoveDelegate).off("mouseup." + this.widgetName, this._mouseUpDelegate)
		},
		_mouseDown: function (b) {
			if (!q) {
				this._mouseMoved = !1, this._mouseStarted && this._mouseUp(b), this._mouseDownEvent = b;
				var c = this,
					d = 1 === b.which,
					e = !("string" != typeof this.options.cancel || !b.target.nodeName) && a(b.target).closest(this.options.cancel).length;
				return !(d && !e && this._mouseCapture(b)) || (this.mouseDelayMet = !this.options.delay, this.mouseDelayMet || (this._mouseDelayTimer = setTimeout(function () {
					c.mouseDelayMet = !0
				}, this.options.delay)), this._mouseDistanceMet(b) && this._mouseDelayMet(b) && (this._mouseStarted = !1 !== this._mouseStart(b), !this._mouseStarted) ? (b.preventDefault(), !0) : (!0 === a.data(b.target, this.widgetName + ".preventClickEvent") && a.removeData(b.target, this.widgetName + ".preventClickEvent"), this._mouseMoveDelegate = function (a) {
					return c._mouseMove(a)
				}, this._mouseUpDelegate = function (a) {
					return c._mouseUp(a)
				}, this.document.on("mousemove." + this.widgetName, this._mouseMoveDelegate).on("mouseup." + this.widgetName, this._mouseUpDelegate), b.preventDefault(), q = !0, !0))
			}
		},
		_mouseMove: function (b) {
			if (this._mouseMoved) {
				if (a.ui.ie && (!document.documentMode || 9 > document.documentMode) && !b.button) return this._mouseUp(b);
				if (!b.which)
					if (b.originalEvent.altKey || b.originalEvent.ctrlKey || b.originalEvent.metaKey || b.originalEvent.shiftKey) this.ignoreMissingWhich = !0;
					else if (!this.ignoreMissingWhich) return this._mouseUp(b)
			}
			return (b.which || b.button) && (this._mouseMoved = !0), this._mouseStarted ? (this._mouseDrag(b), b.preventDefault()) : (this._mouseDistanceMet(b) && this._mouseDelayMet(b) && (this._mouseStarted = !1 !== this._mouseStart(this._mouseDownEvent, b), this._mouseStarted ? this._mouseDrag(b) : this._mouseUp(b)), !this._mouseStarted)
		},
		_mouseUp: function (b) {
			this.document.off("mousemove." + this.widgetName, this._mouseMoveDelegate).off("mouseup." + this.widgetName, this._mouseUpDelegate), this._mouseStarted && (this._mouseStarted = !1, b.target === this._mouseDownEvent.target && a.data(b.target, this.widgetName + ".preventClickEvent", !0), this._mouseStop(b)), this._mouseDelayTimer && (clearTimeout(this._mouseDelayTimer), delete this._mouseDelayTimer), this.ignoreMissingWhich = !1, q = !1, b.preventDefault()
		},
		_mouseDistanceMet: function (a) {
			return Math.max(Math.abs(this._mouseDownEvent.pageX - a.pageX), Math.abs(this._mouseDownEvent.pageY - a.pageY)) >= this.options.distance
		},
		_mouseDelayMet: function () {
			return this.mouseDelayMet
		},
		_mouseStart: function () {},
		_mouseDrag: function () {},
		_mouseStop: function () {},
		_mouseCapture: function () {
			return !0
		}
	}), a.ui.plugin = {
		add: function (b, c, d) {
			var e, f = a.ui[b].prototype;
			for (e in d) f.plugins[e] = f.plugins[e] || [], f.plugins[e].push([c, d[e]])
		},
		call: function (a, b, c, d) {
			var e, f = a.plugins[b];
			if (f && (d || a.element[0].parentNode && 11 !== a.element[0].parentNode.nodeType))
				for (e = 0; f.length > e; e++) a.options[f[e][0]] && f[e][1].apply(a.element, c)
		}
	}, a.ui.safeBlur = function (b) {
		b && "body" !== b.nodeName.toLowerCase() && a(b).trigger("blur")
	}, a.widget("ui.draggable", a.ui.mouse, {
		version: "1.12.1",
		widgetEventPrefix: "drag",
		options: {
			addClasses: !0,
			appendTo: "parent",
			axis: !1,
			connectToSortable: !1,
			containment: !1,
			cursor: "auto",
			cursorAt: !1,
			grid: !1,
			handle: !1,
			helper: "original",
			iframeFix: !1,
			opacity: !1,
			refreshPositions: !1,
			revert: !1,
			revertDuration: 500,
			scope: "default",
			scroll: !0,
			scrollSensitivity: 20,
			scrollSpeed: 20,
			snap: !1,
			snapMode: "both",
			snapTolerance: 20,
			stack: !1,
			zIndex: !1,
			drag: null,
			start: null,
			stop: null
		},
		_create: function () {
			"original" === this.options.helper && this._setPositionRelative(), this.options.addClasses && this._addClass("ui-draggable"), this._setHandleClassName(), this._mouseInit()
		},
		_setOption: function (a, b) {
			this._super(a, b), "handle" === a && (this._removeHandleClassName(), this._setHandleClassName())
		},
		_destroy: function () {
			return (this.helper || this.element).is(".ui-draggable-dragging") ? void(this.destroyOnClear = !0) : (this._removeHandleClassName(), void this._mouseDestroy())
		},
		_mouseCapture: function (b) {
			var c = this.options;
			return !(this.helper || c.disabled || a(b.target).closest(".ui-resizable-handle").length > 0) && (this.handle = this._getHandle(b), !!this.handle && (this._blurActiveElement(b), this._blockFrames(!0 === c.iframeFix ? "iframe" : c.iframeFix), !0))
		},
		_blockFrames: function (b) {
			this.iframeBlocks = this.document.find(b).map(function () {
				var b = a(this);
				return a("<div>").css("position", "absolute").appendTo(b.parent()).outerWidth(b.outerWidth()).outerHeight(b.outerHeight()).offset(b.offset())[0]
			})
		},
		_unblockFrames: function () {
			this.iframeBlocks && (this.iframeBlocks.remove(), delete this.iframeBlocks)
		},
		_blurActiveElement: function (b) {
			var c = a.ui.safeActiveElement(this.document[0]);
			a(b.target).closest(c).length || a.ui.safeBlur(c)
		},
		_mouseStart: function (b) {
			var c = this.options;
			return this.helper = this._createHelper(b), this._addClass(this.helper, "ui-draggable-dragging"), this._cacheHelperProportions(), a.ui.ddmanager && (a.ui.ddmanager.current = this), this._cacheMargins(), this.cssPosition = this.helper.css("position"), this.scrollParent = this.helper.scrollParent(!0), this.offsetParent = this.helper.offsetParent(), this.hasFixedAncestor = this.helper.parents().filter(function () {
				return "fixed" === a(this).css("position")
			}).length > 0, this.positionAbs = this.element.offset(), this._refreshOffsets(b), this.originalPosition = this.position = this._generatePosition(b, !1), this.originalPageX = b.pageX, this.originalPageY = b.pageY, c.cursorAt && this._adjustOffsetFromHelper(c.cursorAt), this._setContainment(), !1 === this._trigger("start", b) ? (this._clear(), !1) : (this._cacheHelperProportions(), a.ui.ddmanager && !c.dropBehaviour && a.ui.ddmanager.prepareOffsets(this, b), this._mouseDrag(b, !0), a.ui.ddmanager && a.ui.ddmanager.dragStart(this, b), !0)
		},
		_refreshOffsets: function (a) {
			this.offset = {
				top: this.positionAbs.top - this.margins.top,
				left: this.positionAbs.left - this.margins.left,
				scroll: !1,
				parent: this._getParentOffset(),
				relative: this._getRelativeOffset()
			}, this.offset.click = {
				left: a.pageX - this.offset.left,
				top: a.pageY - this.offset.top
			}
		},
		_mouseDrag: function (b, c) {
			if (this.hasFixedAncestor && (this.offset.parent = this._getParentOffset()), this.position = this._generatePosition(b, !0), this.positionAbs = this._convertPositionTo("absolute"), !c) {
				var d = this._uiHash();
				if (!1 === this._trigger("drag", b, d)) return this._mouseUp(new a.Event("mouseup", b)), !1;
				this.position = d.position
			}
			return this.helper[0].style.left = this.position.left + "px", this.helper[0].style.top = this.position.top + "px", a.ui.ddmanager && a.ui.ddmanager.drag(this, b), !1
		},
		_mouseStop: function (b) {
			var c = this,
				d = !1;
			return a.ui.ddmanager && !this.options.dropBehaviour && (d = a.ui.ddmanager.drop(this, b)), this.dropped && (d = this.dropped, this.dropped = !1), "invalid" === this.options.revert && !d || "valid" === this.options.revert && d || !0 === this.options.revert || a.isFunction(this.options.revert) && this.options.revert.call(this.element, d) ? a(this.helper).animate(this.originalPosition, parseInt(this.options.revertDuration, 10), function () {
				!1 !== c._trigger("stop", b) && c._clear()
			}) : !1 !== this._trigger("stop", b) && this._clear(), !1
		},
		_mouseUp: function (b) {
			return this._unblockFrames(), a.ui.ddmanager && a.ui.ddmanager.dragStop(this, b), this.handleElement.is(b.target) && this.element.trigger("focus"), a.ui.mouse.prototype._mouseUp.call(this, b)
		},
		cancel: function () {
			return this.helper.is(".ui-draggable-dragging") ? this._mouseUp(new a.Event("mouseup", {
				target: this.element[0]
			})) : this._clear(), this
		},
		_getHandle: function (b) {
			return !this.options.handle || !!a(b.target).closest(this.element.find(this.options.handle)).length
		},
		_setHandleClassName: function () {
			this.handleElement = this.options.handle ? this.element.find(this.options.handle) : this.element, this._addClass(this.handleElement, "ui-draggable-handle")
		},
		_removeHandleClassName: function () {
			this._removeClass(this.handleElement, "ui-draggable-handle")
		},
		_createHelper: function (b) {
			var c = this.options,
				d = a.isFunction(c.helper),
				e = d ? a(c.helper.apply(this.element[0], [b])) : "clone" === c.helper ? this.element.clone().removeAttr("id") : this.element;
			return e.parents("body").length || e.appendTo("parent" === c.appendTo ? this.element[0].parentNode : c.appendTo), d && e[0] === this.element[0] && this._setPositionRelative(), e[0] === this.element[0] || /(fixed|absolute)/.test(e.css("position")) || e.css("position", "absolute"), e
		},
		_setPositionRelative: function () {
			/^(?:r|a|f)/.test(this.element.css("position")) || (this.element[0].style.position = "relative")
		},
		_adjustOffsetFromHelper: function (b) {
			"string" == typeof b && (b = b.split(" ")), a.isArray(b) && (b = {
				left: +b[0],
				top: +b[1] || 0
			}), "left" in b && (this.offset.click.left = b.left + this.margins.left), "right" in b && (this.offset.click.left = this.helperProportions.width - b.right + this.margins.left), "top" in b && (this.offset.click.top = b.top + this.margins.top), "bottom" in b && (this.offset.click.top = this.helperProportions.height - b.bottom + this.margins.top)
		},
		_isRootNode: function (a) {
			return /(html|body)/i.test(a.tagName) || a === this.document[0]
		},
		_getParentOffset: function () {
			var b = this.offsetParent.offset(),
				c = this.document[0];
			return "absolute" === this.cssPosition && this.scrollParent[0] !== c && a.contains(this.scrollParent[0], this.offsetParent[0]) && (b.left += this.scrollParent.scrollLeft(), b.top += this.scrollParent.scrollTop()), this._isRootNode(this.offsetParent[0]) && (b = {
				top: 0,
				left: 0
			}), {
				top: b.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0),
				left: b.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)
			}
		},
		_getRelativeOffset: function () {
			if ("relative" !== this.cssPosition) return {
				top: 0,
				left: 0
			};
			var a = this.element.position(),
				b = this._isRootNode(this.scrollParent[0]);
			return {
				top: a.top - (parseInt(this.helper.css("top"), 10) || 0) + (b ? 0 : this.scrollParent.scrollTop()),
				left: a.left - (parseInt(this.helper.css("left"), 10) || 0) + (b ? 0 : this.scrollParent.scrollLeft())
			}
		},
		_cacheMargins: function () {
			this.margins = {
				left: parseInt(this.element.css("marginLeft"), 10) || 0,
				top: parseInt(this.element.css("marginTop"), 10) || 0,
				right: parseInt(this.element.css("marginRight"), 10) || 0,
				bottom: parseInt(this.element.css("marginBottom"), 10) || 0
			}
		},
		_cacheHelperProportions: function () {
			this.helperProportions = {
				width: this.helper.outerWidth(),
				height: this.helper.outerHeight()
			}
		},
		_setContainment: function () {
			var b, c, d, e = this.options,
				f = this.document[0];
			return this.relativeContainer = null, e.containment ? "window" === e.containment ? void(this.containment = [a(window).scrollLeft() - this.offset.relative.left - this.offset.parent.left, a(window).scrollTop() - this.offset.relative.top - this.offset.parent.top, a(window).scrollLeft() + a(window).width() - this.helperProportions.width - this.margins.left, a(window).scrollTop() + (a(window).height() || f.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]) : "document" === e.containment ? void(this.containment = [0, 0, a(f).width() - this.helperProportions.width - this.margins.left, (a(f).height() || f.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]) : e.containment.constructor === Array ? void(this.containment = e.containment) : ("parent" === e.containment && (e.containment = this.helper[0].parentNode), c = a(e.containment), void((d = c[0]) && (b = /(scroll|auto)/.test(c.css("overflow")), this.containment = [(parseInt(c.css("borderLeftWidth"), 10) || 0) + (parseInt(c.css("paddingLeft"), 10) || 0), (parseInt(c.css("borderTopWidth"), 10) || 0) + (parseInt(c.css("paddingTop"), 10) || 0), (b ? Math.max(d.scrollWidth, d.offsetWidth) : d.offsetWidth) - (parseInt(c.css("borderRightWidth"), 10) || 0) - (parseInt(c.css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left - this.margins.right, (b ? Math.max(d.scrollHeight, d.offsetHeight) : d.offsetHeight) - (parseInt(c.css("borderBottomWidth"), 10) || 0) - (parseInt(c.css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top - this.margins.bottom], this.relativeContainer = c))) : void(this.containment = null)
		},
		_convertPositionTo: function (a, b) {
			b || (b = this.position);
			var c = "absolute" === a ? 1 : -1,
				d = this._isRootNode(this.scrollParent[0]);
			return {
				top: b.top + this.offset.relative.top * c + this.offset.parent.top * c - ("fixed" === this.cssPosition ? -this.offset.scroll.top : d ? 0 : this.offset.scroll.top) * c,
				left: b.left + this.offset.relative.left * c + this.offset.parent.left * c - ("fixed" === this.cssPosition ? -this.offset.scroll.left : d ? 0 : this.offset.scroll.left) * c
			}
		},
		_generatePosition: function (a, b) {
			var c, d, e, f, g = this.options,
				h = this._isRootNode(this.scrollParent[0]),
				i = a.pageX,
				j = a.pageY;
			return h && this.offset.scroll || (this.offset.scroll = {
				top: this.scrollParent.scrollTop(),
				left: this.scrollParent.scrollLeft()
			}), b && (this.containment && (this.relativeContainer ? (d = this.relativeContainer.offset(), c = [this.containment[0] + d.left, this.containment[1] + d.top, this.containment[2] + d.left, this.containment[3] + d.top]) : c = this.containment, a.pageX - this.offset.click.left < c[0] && (i = c[0] + this.offset.click.left), a.pageY - this.offset.click.top < c[1] && (j = c[1] + this.offset.click.top), a.pageX - this.offset.click.left > c[2] && (i = c[2] + this.offset.click.left), a.pageY - this.offset.click.top > c[3] && (j = c[3] + this.offset.click.top)), g.grid && (e = g.grid[1] ? this.originalPageY + Math.round((j - this.originalPageY) / g.grid[1]) * g.grid[1] : this.originalPageY, j = c ? e - this.offset.click.top >= c[1] || e - this.offset.click.top > c[3] ? e : e - this.offset.click.top >= c[1] ? e - g.grid[1] : e + g.grid[1] : e, f = g.grid[0] ? this.originalPageX + Math.round((i - this.originalPageX) / g.grid[0]) * g.grid[0] : this.originalPageX, i = c ? f - this.offset.click.left >= c[0] || f - this.offset.click.left > c[2] ? f : f - this.offset.click.left >= c[0] ? f - g.grid[0] : f + g.grid[0] : f), "y" === g.axis && (i = this.originalPageX), "x" === g.axis && (j = this.originalPageY)), {
				top: j - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + ("fixed" === this.cssPosition ? -this.offset.scroll.top : h ? 0 : this.offset.scroll.top),
				left: i - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + ("fixed" === this.cssPosition ? -this.offset.scroll.left : h ? 0 : this.offset.scroll.left)
			}
		},
		_clear: function () {
			this._removeClass(this.helper, "ui-draggable-dragging"), this.helper[0] === this.element[0] || this.cancelHelperRemoval || this.helper.remove(), this.helper = null, this.cancelHelperRemoval = !1, this.destroyOnClear && this.destroy()
		},
		_trigger: function (b, c, d) {
			return d = d || this._uiHash(), a.ui.plugin.call(this, b, [c, d, this], !0), /^(drag|start|stop)/.test(b) && (this.positionAbs = this._convertPositionTo("absolute"), d.offset = this.positionAbs), a.Widget.prototype._trigger.call(this, b, c, d)
		},
		plugins: {},
		_uiHash: function () {
			return {
				helper: this.helper,
				position: this.position,
				originalPosition: this.originalPosition,
				offset: this.positionAbs
			}
		}
	}), a.ui.plugin.add("draggable", "connectToSortable", {
		start: function (b, c, d) {
			var e = a.extend({}, c, {
				item: d.element
			});
			d.sortables = [], a(d.options.connectToSortable).each(function () {
				var c = a(this).sortable("instance");
				c && !c.options.disabled && (d.sortables.push(c), c.refreshPositions(), c._trigger("activate", b, e))
			})
		},
		stop: function (b, c, d) {
			var e = a.extend({}, c, {
				item: d.element
			});
			d.cancelHelperRemoval = !1, a.each(d.sortables, function () {
				var a = this;
				a.isOver ? (a.isOver = 0, d.cancelHelperRemoval = !0, a.cancelHelperRemoval = !1, a._storedCSS = {
					position: a.placeholder.css("position"),
					top: a.placeholder.css("top"),
					left: a.placeholder.css("left")
				}, a._mouseStop(b), a.options.helper = a.options._helper) : (a.cancelHelperRemoval = !0, a._trigger("deactivate", b, e))
			})
		},
		drag: function (b, c, d) {
			a.each(d.sortables, function () {
				var e = !1,
					f = this;
				f.positionAbs = d.positionAbs, f.helperProportions = d.helperProportions, f.offset.click = d.offset.click, f._intersectsWith(f.containerCache) && (e = !0, a.each(d.sortables, function () {
					return this.positionAbs = d.positionAbs, this.helperProportions = d.helperProportions, this.offset.click = d.offset.click, this !== f && this._intersectsWith(this.containerCache) && a.contains(f.element[0], this.element[0]) && (e = !1), e
				})), e ? (f.isOver || (f.isOver = 1, d._parent = c.helper.parent(), f.currentItem = c.helper.appendTo(f.element).data("ui-sortable-item", !0), f.options._helper = f.options.helper, f.options.helper = function () {
					return c.helper[0]
				}, b.target = f.currentItem[0], f._mouseCapture(b, !0), f._mouseStart(b, !0, !0), f.offset.click.top = d.offset.click.top, f.offset.click.left = d.offset.click.left, f.offset.parent.left -= d.offset.parent.left - f.offset.parent.left, f.offset.parent.top -= d.offset.parent.top - f.offset.parent.top, d._trigger("toSortable", b), d.dropped = f.element, a.each(d.sortables, function () {
					this.refreshPositions()
				}), d.currentItem = d.element, f.fromOutside = d), f.currentItem && (f._mouseDrag(b), c.position = f.position)) : f.isOver && (f.isOver = 0, f.cancelHelperRemoval = !0, f.options._revert = f.options.revert, f.options.revert = !1, f._trigger("out", b, f._uiHash(f)), f._mouseStop(b, !0), f.options.revert = f.options._revert, f.options.helper = f.options._helper, f.placeholder && f.placeholder.remove(), c.helper.appendTo(d._parent), d._refreshOffsets(b), c.position = d._generatePosition(b, !0), d._trigger("fromSortable", b), d.dropped = !1, a.each(d.sortables, function () {
					this.refreshPositions()
				}))
			})
		}
	}), a.ui.plugin.add("draggable", "cursor", {
		start: function (b, c, d) {
			var e = a("body"),
				f = d.options;
			e.css("cursor") && (f._cursor = e.css("cursor")), e.css("cursor", f.cursor)
		},
		stop: function (b, c, d) {
			var e = d.options;
			e._cursor && a("body").css("cursor", e._cursor)
		}
	}), a.ui.plugin.add("draggable", "opacity", {
		start: function (b, c, d) {
			var e = a(c.helper),
				f = d.options;
			e.css("opacity") && (f._opacity = e.css("opacity")), e.css("opacity", f.opacity)
		},
		stop: function (b, c, d) {
			var e = d.options;
			e._opacity && a(c.helper).css("opacity", e._opacity)
		}
	}), a.ui.plugin.add("draggable", "scroll", {
		start: function (a, b, c) {
			c.scrollParentNotHidden || (c.scrollParentNotHidden = c.helper.scrollParent(!1)), c.scrollParentNotHidden[0] !== c.document[0] && "HTML" !== c.scrollParentNotHidden[0].tagName && (c.overflowOffset = c.scrollParentNotHidden.offset())
		},
		drag: function (b, c, d) {
			var e = d.options,
				f = !1,
				g = d.scrollParentNotHidden[0],
				h = d.document[0];
			g !== h && "HTML" !== g.tagName ? (e.axis && "x" === e.axis || (d.overflowOffset.top + g.offsetHeight - b.pageY < e.scrollSensitivity ? g.scrollTop = f = g.scrollTop + e.scrollSpeed : b.pageY - d.overflowOffset.top < e.scrollSensitivity && (g.scrollTop = f = g.scrollTop - e.scrollSpeed)), e.axis && "y" === e.axis || (d.overflowOffset.left + g.offsetWidth - b.pageX < e.scrollSensitivity ? g.scrollLeft = f = g.scrollLeft + e.scrollSpeed : b.pageX - d.overflowOffset.left < e.scrollSensitivity && (g.scrollLeft = f = g.scrollLeft - e.scrollSpeed))) : (e.axis && "x" === e.axis || (b.pageY - a(h).scrollTop() < e.scrollSensitivity ? f = a(h).scrollTop(a(h).scrollTop() - e.scrollSpeed) : a(window).height() - (b.pageY - a(h).scrollTop()) < e.scrollSensitivity && (f = a(h).scrollTop(a(h).scrollTop() + e.scrollSpeed))), e.axis && "y" === e.axis || (b.pageX - a(h).scrollLeft() < e.scrollSensitivity ? f = a(h).scrollLeft(a(h).scrollLeft() - e.scrollSpeed) : a(window).width() - (b.pageX - a(h).scrollLeft()) < e.scrollSensitivity && (f = a(h).scrollLeft(a(h).scrollLeft() + e.scrollSpeed)))), !1 !== f && a.ui.ddmanager && !e.dropBehaviour && a.ui.ddmanager.prepareOffsets(d, b)
		}
	}), a.ui.plugin.add("draggable", "snap", {
		start: function (b, c, d) {
			var e = d.options;
			d.snapElements = [], a(e.snap.constructor !== String ? e.snap.items || ":data(ui-draggable)" : e.snap).each(function () {
				var b = a(this),
					c = b.offset();
				this !== d.element[0] && d.snapElements.push({
					item: this,
					width: b.outerWidth(),
					height: b.outerHeight(),
					top: c.top,
					left: c.left
				})
			})
		},
		drag: function (b, c, d) {
			var e, f, g, h, i, j, k, l, m, n, o = d.options,
				p = o.snapTolerance,
				q = c.offset.left,
				r = q + d.helperProportions.width,
				s = c.offset.top,
				t = s + d.helperProportions.height;
			for (m = d.snapElements.length - 1; m >= 0; m--) i = d.snapElements[m].left - d.margins.left, j = i + d.snapElements[m].width, k = d.snapElements[m].top - d.margins.top, l = k + d.snapElements[m].height, i - p > r || q > j + p || k - p > t || s > l + p || !a.contains(d.snapElements[m].item.ownerDocument, d.snapElements[m].item) ? (d.snapElements[m].snapping && d.options.snap.release && d.options.snap.release.call(d.element, b, a.extend(d._uiHash(), {
				snapItem: d.snapElements[m].item
			})), d.snapElements[m].snapping = !1) : ("inner" !== o.snapMode && (e = p >= Math.abs(k - t), f = p >= Math.abs(l - s), g = p >= Math.abs(i - r), h = p >= Math.abs(j - q), e && (c.position.top = d._convertPositionTo("relative", {
				top: k - d.helperProportions.height,
				left: 0
			}).top), f && (c.position.top = d._convertPositionTo("relative", {
				top: l,
				left: 0
			}).top), g && (c.position.left = d._convertPositionTo("relative", {
				top: 0,
				left: i - d.helperProportions.width
			}).left), h && (c.position.left = d._convertPositionTo("relative", {
				top: 0,
				left: j
			}).left)), n = e || f || g || h, "outer" !== o.snapMode && (e = p >= Math.abs(k - s), f = p >= Math.abs(l - t), g = p >= Math.abs(i - q), h = p >= Math.abs(j - r), e && (c.position.top = d._convertPositionTo("relative", {
				top: k,
				left: 0
			}).top), f && (c.position.top = d._convertPositionTo("relative", {
				top: l - d.helperProportions.height,
				left: 0
			}).top), g && (c.position.left = d._convertPositionTo("relative", {
				top: 0,
				left: i
			}).left), h && (c.position.left = d._convertPositionTo("relative", {
				top: 0,
				left: j - d.helperProportions.width
			}).left)), !d.snapElements[m].snapping && (e || f || g || h || n) && d.options.snap.snap && d.options.snap.snap.call(d.element, b, a.extend(d._uiHash(), {
				snapItem: d.snapElements[m].item
			})), d.snapElements[m].snapping = e || f || g || h || n)
		}
	}), a.ui.plugin.add("draggable", "stack", {
		start: function (b, c, d) {
			var e, f = d.options,
				g = a.makeArray(a(f.stack)).sort(function (b, c) {
					return (parseInt(a(b).css("zIndex"), 10) || 0) - (parseInt(a(c).css("zIndex"), 10) || 0)
				});
			g.length && (e = parseInt(a(g[0]).css("zIndex"), 10) || 0, a(g).each(function (b) {
				a(this).css("zIndex", e + b)
			}), this.css("zIndex", e + g.length))
		}
	}), a.ui.plugin.add("draggable", "zIndex", {
		start: function (b, c, d) {
			var e = a(c.helper),
				f = d.options;
			e.css("zIndex") && (f._zIndex = e.css("zIndex")), e.css("zIndex", f.zIndex)
		},
		stop: function (b, c, d) {
			var e = d.options;
			e._zIndex && a(c.helper).css("zIndex", e._zIndex)
		}
	}), a.ui.draggable, a.widget("ui.resizable", a.ui.mouse, {
		version: "1.12.1",
		widgetEventPrefix: "resize",
		options: {
			alsoResize: !1,
			animate: !1,
			animateDuration: "slow",
			animateEasing: "swing",
			aspectRatio: !1,
			autoHide: !1,
			classes: {
				"ui-resizable-se": "ui-icon ui-icon-gripsmall-diagonal-se"
			},
			containment: !1,
			ghost: !1,
			grid: !1,
			handles: "e,s,se",
			helper: !1,
			maxHeight: null,
			maxWidth: null,
			minHeight: 10,
			minWidth: 10,
			zIndex: 90,
			resize: null,
			start: null,
			stop: null
		},
		_num: function (a) {
			return parseFloat(a) || 0
		},
		_isNumber: function (a) {
			return !isNaN(parseFloat(a))
		},
		_hasScroll: function (b, c) {
			if ("hidden" === a(b).css("overflow")) return !1;
			var d = c && "left" === c ? "scrollLeft" : "scrollTop",
				e = !1;
			return b[d] > 0 || (b[d] = 1, e = b[d] > 0, b[d] = 0, e)
		},
		_create: function () {
			var b, c = this.options,
				d = this;
			this._addClass("ui-resizable"), a.extend(this, {
				_aspectRatio: !!c.aspectRatio,
				aspectRatio: c.aspectRatio,
				originalElement: this.element,
				_proportionallyResizeElements: [],
				_helper: c.helper || c.ghost || c.animate ? c.helper || "ui-resizable-helper" : null
			}), this.element[0].nodeName.match(/^(canvas|textarea|input|select|button|img)$/i) && (this.element.wrap(a("<div class='ui-wrapper' style='overflow: hidden;'></div>").css({
				position: this.element.css("position"),
				width: this.element.outerWidth(),
				height: this.element.outerHeight(),
				top: this.element.css("top"),
				left: this.element.css("left")
			})), this.element = this.element.parent().data("ui-resizable", this.element.resizable("instance")), this.elementIsWrapper = !0, b = {
				marginTop: this.originalElement.css("marginTop"),
				marginRight: this.originalElement.css("marginRight"),
				marginBottom: this.originalElement.css("marginBottom"),
				marginLeft: this.originalElement.css("marginLeft")
			}, this.element.css(b), this.originalElement.css("margin", 0), this.originalResizeStyle = this.originalElement.css("resize"), this.originalElement.css("resize", "none"), this._proportionallyResizeElements.push(this.originalElement.css({
				position: "static",
				zoom: 1,
				display: "block"
			})), this.originalElement.css(b), this._proportionallyResize()), this._setupHandles(), c.autoHide && a(this.element).on("mouseenter", function () {
				c.disabled || (d._removeClass("ui-resizable-autohide"), d._handles.show())
			}).on("mouseleave", function () {
				c.disabled || d.resizing || (d._addClass("ui-resizable-autohide"), d._handles.hide())
			}), this._mouseInit()
		},
		_destroy: function () {
			this._mouseDestroy();
			var b, c = function (b) {
				a(b).removeData("resizable").removeData("ui-resizable").off(".resizable").find(".ui-resizable-handle").remove()
			};
			return this.elementIsWrapper && (c(this.element), b = this.element, this.originalElement.css({
				position: b.css("position"),
				width: b.outerWidth(),
				height: b.outerHeight(),
				top: b.css("top"),
				left: b.css("left")
			}).insertAfter(b), b.remove()), this.originalElement.css("resize", this.originalResizeStyle), c(this.originalElement), this
		},
		_setOption: function (a, b) {
			switch (this._super(a, b), a) {
				case "handles":
					this._removeHandles(), this._setupHandles()
			}
		},
		_setupHandles: function () {
			var b, c, d, e, f, g = this.options,
				h = this;
			if (this.handles = g.handles || (a(".ui-resizable-handle", this.element).length ? {
					n: ".ui-resizable-n",
					e: ".ui-resizable-e",
					s: ".ui-resizable-s",
					w: ".ui-resizable-w",
					se: ".ui-resizable-se",
					sw: ".ui-resizable-sw",
					ne: ".ui-resizable-ne",
					nw: ".ui-resizable-nw"
				} : "e,s,se"), this._handles = a(), this.handles.constructor === String)
				for ("all" === this.handles && (this.handles = "n,e,s,w,se,sw,ne,nw"), d = this.handles.split(","), this.handles = {}, c = 0; d.length > c; c++) b = a.trim(d[c]), e = "ui-resizable-" + b, f = a("<div>"), this._addClass(f, "ui-resizable-handle " + e), f.css({
					zIndex: g.zIndex
				}), this.handles[b] = ".ui-resizable-" + b, this.element.append(f);
			this._renderAxis = function (b) {
				var c, d, e, f;
				b = b || this.element;
				for (c in this.handles) this.handles[c].constructor === String ? this.handles[c] = this.element.children(this.handles[c]).first().show() : (this.handles[c].jquery || this.handles[c].nodeType) && (this.handles[c] = a(this.handles[c]), this._on(this.handles[c], {
					mousedown: h._mouseDown
				})), this.elementIsWrapper && this.originalElement[0].nodeName.match(/^(textarea|input|select|button)$/i) && (d = a(this.handles[c], this.element), f = /sw|ne|nw|se|n|s/.test(c) ? d.outerHeight() : d.outerWidth(), e = ["padding", /ne|nw|n/.test(c) ? "Top" : /se|sw|s/.test(c) ? "Bottom" : /^e$/.test(c) ? "Right" : "Left"].join(""), b.css(e, f), this._proportionallyResize()), this._handles = this._handles.add(this.handles[c])
			}, this._renderAxis(this.element), this._handles = this._handles.add(this.element.find(".ui-resizable-handle")), this._handles.disableSelection(), this._handles.on("mouseover", function () {
				h.resizing || (this.className && (f = this.className.match(/ui-resizable-(se|sw|ne|nw|n|e|s|w)/i)), h.axis = f && f[1] ? f[1] : "se")
			}), g.autoHide && (this._handles.hide(), this._addClass("ui-resizable-autohide"))
		},
		_removeHandles: function () {
			this._handles.remove()
		},
		_mouseCapture: function (b) {
			var c, d, e = !1;
			for (c in this.handles)((d = a(this.handles[c])[0]) === b.target || a.contains(d, b.target)) && (e = !0);
			return !this.options.disabled && e
		},
		_mouseStart: function (b) {
			var c, d, e, f = this.options,
				g = this.element;
			return this.resizing = !0, this._renderProxy(), c = this._num(this.helper.css("left")), d = this._num(this.helper.css("top")), f.containment && (c += a(f.containment).scrollLeft() || 0, d += a(f.containment).scrollTop() || 0), this.offset = this.helper.offset(), this.position = {
				left: c,
				top: d
			}, this.size = this._helper ? {
				width: this.helper.width(),
				height: this.helper.height()
			} : {
				width: g.width(),
				height: g.height()
			}, this.originalSize = this._helper ? {
				width: g.outerWidth(),
				height: g.outerHeight()
			} : {
				width: g.width(),
				height: g.height()
			}, this.sizeDiff = {
				width: g.outerWidth() - g.width(),
				height: g.outerHeight() - g.height()
			}, this.originalPosition = {
				left: c,
				top: d
			}, this.originalMousePosition = {
				left: b.pageX,
				top: b.pageY
			}, this.aspectRatio = "number" == typeof f.aspectRatio ? f.aspectRatio : this.originalSize.width / this.originalSize.height || 1, e = a(".ui-resizable-" + this.axis).css("cursor"), a("body").css("cursor", "auto" === e ? this.axis + "-resize" : e), this._addClass("ui-resizable-resizing"), this._propagate("start", b), !0
		},
		_mouseDrag: function (b) {
			var c, d, e = this.originalMousePosition,
				f = this.axis,
				g = b.pageX - e.left || 0,
				h = b.pageY - e.top || 0,
				i = this._change[f];
			return this._updatePrevProperties(), !!i && (c = i.apply(this, [b, g, h]), this._updateVirtualBoundaries(b.shiftKey), (this._aspectRatio || b.shiftKey) && (c = this._updateRatio(c, b)), c = this._respectSize(c, b), this._updateCache(c), this._propagate("resize", b), d = this._applyChanges(), !this._helper && this._proportionallyResizeElements.length && this._proportionallyResize(), a.isEmptyObject(d) || (this._updatePrevProperties(), this._trigger("resize", b, this.ui()), this._applyChanges()), !1)
		},
		_mouseStop: function (b) {
			this.resizing = !1;
			var c, d, e, f, g, h, i, j = this.options,
				k = this;
			return this._helper && (c = this._proportionallyResizeElements, d = c.length && /textarea/i.test(c[0].nodeName), e = d && this._hasScroll(c[0], "left") ? 0 : k.sizeDiff.height, f = d ? 0 : k.sizeDiff.width, g = {
				width: k.helper.width() - f,
				height: k.helper.height() - e
			}, h = parseFloat(k.element.css("left")) + (k.position.left - k.originalPosition.left) || null, i = parseFloat(k.element.css("top")) + (k.position.top - k.originalPosition.top) || null, j.animate || this.element.css(a.extend(g, {
				top: i,
				left: h
			})), k.helper.height(k.size.height), k.helper.width(k.size.width), this._helper && !j.animate && this._proportionallyResize()), a("body").css("cursor", "auto"), this._removeClass("ui-resizable-resizing"), this._propagate("stop", b), this._helper && this.helper.remove(), !1
		},
		_updatePrevProperties: function () {
			this.prevPosition = {
				top: this.position.top,
				left: this.position.left
			}, this.prevSize = {
				width: this.size.width,
				height: this.size.height
			}
		},
		_applyChanges: function () {
			var a = {};
			return this.position.top !== this.prevPosition.top && (a.top = this.position.top + "px"), this.position.left !== this.prevPosition.left && (a.left = this.position.left + "px"), this.size.width !== this.prevSize.width && (a.width = this.size.width + "px"), this.size.height !== this.prevSize.height && (a.height = this.size.height + "px"), this.helper.css(a), a
		},
		_updateVirtualBoundaries: function (a) {
			var b, c, d, e, f, g = this.options;
			f = {
				minWidth: this._isNumber(g.minWidth) ? g.minWidth : 0,
				maxWidth: this._isNumber(g.maxWidth) ? g.maxWidth : 1 / 0,
				minHeight: this._isNumber(g.minHeight) ? g.minHeight : 0,
				maxHeight: this._isNumber(g.maxHeight) ? g.maxHeight : 1 / 0
			}, (this._aspectRatio || a) && (b = f.minHeight * this.aspectRatio, d = f.minWidth / this.aspectRatio, c = f.maxHeight * this.aspectRatio, e = f.maxWidth / this.aspectRatio, b > f.minWidth && (f.minWidth = b), d > f.minHeight && (f.minHeight = d), f.maxWidth > c && (f.maxWidth = c), f.maxHeight > e && (f.maxHeight = e)), this._vBoundaries = f
		},
		_updateCache: function (a) {
			this.offset = this.helper.offset(), this._isNumber(a.left) && (this.position.left = a.left), this._isNumber(a.top) && (this.position.top = a.top), this._isNumber(a.height) && (this.size.height = a.height), this._isNumber(a.width) && (this.size.width = a.width)
		},
		_updateRatio: function (a) {
			var b = this.position,
				c = this.size,
				d = this.axis;
			return this._isNumber(a.height) ? a.width = a.height * this.aspectRatio : this._isNumber(a.width) && (a.height = a.width / this.aspectRatio), "sw" === d && (a.left = b.left + (c.width - a.width), a.top = null), "nw" === d && (a.top = b.top + (c.height - a.height), a.left = b.left + (c.width - a.width)), a
		},
		_respectSize: function (a) {
			var b = this._vBoundaries,
				c = this.axis,
				d = this._isNumber(a.width) && b.maxWidth && b.maxWidth < a.width,
				e = this._isNumber(a.height) && b.maxHeight && b.maxHeight < a.height,
				f = this._isNumber(a.width) && b.minWidth && b.minWidth > a.width,
				g = this._isNumber(a.height) && b.minHeight && b.minHeight > a.height,
				h = this.originalPosition.left + this.originalSize.width,
				i = this.originalPosition.top + this.originalSize.height,
				j = /sw|nw|w/.test(c),
				k = /nw|ne|n/.test(c);
			return f && (a.width = b.minWidth), g && (a.height = b.minHeight), d && (a.width = b.maxWidth), e && (a.height = b.maxHeight), f && j && (a.left = h - b.minWidth), d && j && (a.left = h - b.maxWidth), g && k && (a.top = i - b.minHeight), e && k && (a.top = i - b.maxHeight), a.width || a.height || a.left || !a.top ? a.width || a.height || a.top || !a.left || (a.left = null) : a.top = null, a
		},
		_getPaddingPlusBorderDimensions: function (a) {
			for (var b = 0, c = [], d = [a.css("borderTopWidth"), a.css("borderRightWidth"), a.css("borderBottomWidth"), a.css("borderLeftWidth")], e = [a.css("paddingTop"), a.css("paddingRight"), a.css("paddingBottom"), a.css("paddingLeft")]; 4 > b; b++) c[b] = parseFloat(d[b]) || 0, c[b] += parseFloat(e[b]) || 0;
			return {
				height: c[0] + c[2],
				width: c[1] + c[3]
			}
		},
		_proportionallyResize: function () {
			if (this._proportionallyResizeElements.length)
				for (var a, b = 0, c = this.helper || this.element; this._proportionallyResizeElements.length > b; b++) a = this._proportionallyResizeElements[b], this.outerDimensions || (this.outerDimensions = this._getPaddingPlusBorderDimensions(a)), a.css({
					height: c.height() - this.outerDimensions.height || 0,
					width: c.width() - this.outerDimensions.width || 0
				})
		},
		_renderProxy: function () {
			var b = this.element,
				c = this.options;
			this.elementOffset = b.offset(), this._helper ? (this.helper = this.helper || a("<div style='overflow:hidden;'></div>"), this._addClass(this.helper, this._helper), this.helper.css({
				width: this.element.outerWidth(),
				height: this.element.outerHeight(),
				position: "absolute",
				left: this.elementOffset.left + "px",
				top: this.elementOffset.top + "px",
				zIndex: ++c.zIndex
			}), this.helper.appendTo("body").disableSelection()) : this.helper = this.element
		},
		_change: {
			e: function (a, b) {
				return {
					width: this.originalSize.width + b
				}
			},
			w: function (a, b) {
				var c = this.originalSize;
				return {
					left: this.originalPosition.left + b,
					width: c.width - b
				}
			},
			n: function (a, b, c) {
				var d = this.originalSize;
				return {
					top: this.originalPosition.top + c,
					height: d.height - c
				}
			},
			s: function (a, b, c) {
				return {
					height: this.originalSize.height + c
				}
			},
			se: function (b, c, d) {
				return a.extend(this._change.s.apply(this, arguments), this._change.e.apply(this, [b, c, d]))
			},
			sw: function (b, c, d) {
				return a.extend(this._change.s.apply(this, arguments), this._change.w.apply(this, [b, c, d]))
			},
			ne: function (b, c, d) {
				return a.extend(this._change.n.apply(this, arguments), this._change.e.apply(this, [b, c, d]))
			},
			nw: function (b, c, d) {
				return a.extend(this._change.n.apply(this, arguments), this._change.w.apply(this, [b, c, d]))
			}
		},
		_propagate: function (b, c) {
			a.ui.plugin.call(this, b, [c, this.ui()]), "resize" !== b && this._trigger(b, c, this.ui())
		},
		plugins: {},
		ui: function () {
			return {
				originalElement: this.originalElement,
				element: this.element,
				helper: this.helper,
				position: this.position,
				size: this.size,
				originalSize: this.originalSize,
				originalPosition: this.originalPosition
			}
		}
	}), a.ui.plugin.add("resizable", "animate", {
		stop: function (b) {
			var c = a(this).resizable("instance"),
				d = c.options,
				e = c._proportionallyResizeElements,
				f = e.length && /textarea/i.test(e[0].nodeName),
				g = f && c._hasScroll(e[0], "left") ? 0 : c.sizeDiff.height,
				h = f ? 0 : c.sizeDiff.width,
				i = {
					width: c.size.width - h,
					height: c.size.height - g
				},
				j = parseFloat(c.element.css("left")) + (c.position.left - c.originalPosition.left) || null,
				k = parseFloat(c.element.css("top")) + (c.position.top - c.originalPosition.top) || null;
			c.element.animate(a.extend(i, k && j ? {
				top: k,
				left: j
			} : {}), {
				duration: d.animateDuration,
				easing: d.animateEasing,
				step: function () {
					var d = {
						width: parseFloat(c.element.css("width")),
						height: parseFloat(c.element.css("height")),
						top: parseFloat(c.element.css("top")),
						left: parseFloat(c.element.css("left"))
					};
					e && e.length && a(e[0]).css({
						width: d.width,
						height: d.height
					}), c._updateCache(d), c._propagate("resize", b)
				}
			})
		}
	}), a.ui.plugin.add("resizable", "containment", {
		start: function () {
			var b, c, d, e, f, g, h, i = a(this).resizable("instance"),
				j = i.options,
				k = i.element,
				l = j.containment,
				m = l instanceof a ? l.get(0) : /parent/.test(l) ? k.parent().get(0) : l;
			m && (i.containerElement = a(m), /document/.test(l) || l === document ? (i.containerOffset = {
				left: 0,
				top: 0
			}, i.containerPosition = {
				left: 0,
				top: 0
			}, i.parentData = {
				element: a(document),
				left: 0,
				top: 0,
				width: a(document).width(),
				height: a(document).height() || document.body.parentNode.scrollHeight
			}) : (b = a(m), c = [], a(["Top", "Right", "Left", "Bottom"]).each(function (a, d) {
				c[a] = i._num(b.css("padding" + d))
			}), i.containerOffset = b.offset(), i.containerPosition = b.position(), i.containerSize = {
				height: b.innerHeight() - c[3],
				width: b.innerWidth() - c[1]
			}, d = i.containerOffset, e = i.containerSize.height, f = i.containerSize.width, g = i._hasScroll(m, "left") ? m.scrollWidth : f, h = i._hasScroll(m) ? m.scrollHeight : e, i.parentData = {
				element: m,
				left: d.left,
				top: d.top,
				width: g,
				height: h
			}))
		},
		resize: function (b) {
			var c, d, e, f, g = a(this).resizable("instance"),
				h = g.options,
				i = g.containerOffset,
				j = g.position,
				k = g._aspectRatio || b.shiftKey,
				l = {
					top: 0,
					left: 0
				},
				m = g.containerElement,
				n = !0;
			m[0] !== document && /static/.test(m.css("position")) && (l = i), j.left < (g._helper ? i.left : 0) && (g.size.width = g.size.width + (g._helper ? g.position.left - i.left : g.position.left - l.left), k && (g.size.height = g.size.width / g.aspectRatio, n = !1), g.position.left = h.helper ? i.left : 0), j.top < (g._helper ? i.top : 0) && (g.size.height = g.size.height + (g._helper ? g.position.top - i.top : g.position.top), k && (g.size.width = g.size.height * g.aspectRatio, n = !1), g.position.top = g._helper ? i.top : 0), e = g.containerElement.get(0) === g.element.parent().get(0), f = /relative|absolute/.test(g.containerElement.css("position")), e && f ? (g.offset.left = g.parentData.left + g.position.left, g.offset.top = g.parentData.top + g.position.top) : (g.offset.left = g.element.offset().left, g.offset.top = g.element.offset().top), c = Math.abs(g.sizeDiff.width + (g._helper ? g.offset.left - l.left : g.offset.left - i.left)), d = Math.abs(g.sizeDiff.height + (g._helper ? g.offset.top - l.top : g.offset.top - i.top)), c + g.size.width >= g.parentData.width && (g.size.width = g.parentData.width - c, k && (g.size.height = g.size.width / g.aspectRatio, n = !1)), d + g.size.height >= g.parentData.height && (g.size.height = g.parentData.height - d, k && (g.size.width = g.size.height * g.aspectRatio, n = !1)), n || (g.position.left = g.prevPosition.left, g.position.top = g.prevPosition.top, g.size.width = g.prevSize.width, g.size.height = g.prevSize.height)
		},
		stop: function () {
			var b = a(this).resizable("instance"),
				c = b.options,
				d = b.containerOffset,
				e = b.containerPosition,
				f = b.containerElement,
				g = a(b.helper),
				h = g.offset(),
				i = g.outerWidth() - b.sizeDiff.width,
				j = g.outerHeight() - b.sizeDiff.height;
			b._helper && !c.animate && /relative/.test(f.css("position")) && a(this).css({
				left: h.left - e.left - d.left,
				width: i,
				height: j
			}), b._helper && !c.animate && /static/.test(f.css("position")) && a(this).css({
				left: h.left - e.left - d.left,
				width: i,
				height: j
			})
		}
	}), a.ui.plugin.add("resizable", "alsoResize", {
		start: function () {
			var b = a(this).resizable("instance"),
				c = b.options;
			a(c.alsoResize).each(function () {
				var b = a(this);
				b.data("ui-resizable-alsoresize", {
					width: parseFloat(b.width()),
					height: parseFloat(b.height()),
					left: parseFloat(b.css("left")),
					top: parseFloat(b.css("top"))
				})
			})
		},
		resize: function (b, c) {
			var d = a(this).resizable("instance"),
				e = d.options,
				f = d.originalSize,
				g = d.originalPosition,
				h = {
					height: d.size.height - f.height || 0,
					width: d.size.width - f.width || 0,
					top: d.position.top - g.top || 0,
					left: d.position.left - g.left || 0
				};
			a(e.alsoResize).each(function () {
				var b = a(this),
					d = a(this).data("ui-resizable-alsoresize"),
					e = {},
					f = b.parents(c.originalElement[0]).length ? ["width", "height"] : ["width", "height", "top", "left"];
				a.each(f, function (a, b) {
					var c = (d[b] || 0) + (h[b] || 0);
					c && c >= 0 && (e[b] = c || null)
				}), b.css(e)
			})
		},
		stop: function () {
			a(this).removeData("ui-resizable-alsoresize")
		}
	}), a.ui.plugin.add("resizable", "ghost", {
		start: function () {
			var b = a(this).resizable("instance"),
				c = b.size;
			b.ghost = b.originalElement.clone(), b.ghost.css({
				opacity: .25,
				display: "block",
				position: "relative",
				height: c.height,
				width: c.width,
				margin: 0,
				left: 0,
				top: 0
			}), b._addClass(b.ghost, "ui-resizable-ghost"), !1 !== a.uiBackCompat && "string" == typeof b.options.ghost && b.ghost.addClass(this.options.ghost), b.ghost.appendTo(b.helper)
		},
		resize: function () {
			var b = a(this).resizable("instance");
			b.ghost && b.ghost.css({
				position: "relative",
				height: b.size.height,
				width: b.size.width
			})
		},
		stop: function () {
			var b = a(this).resizable("instance");
			b.ghost && b.helper && b.helper.get(0).removeChild(b.ghost.get(0))
		}
	}), a.ui.plugin.add("resizable", "grid", {
		resize: function () {
			var b, c = a(this).resizable("instance"),
				d = c.options,
				e = c.size,
				f = c.originalSize,
				g = c.originalPosition,
				h = c.axis,
				i = "number" == typeof d.grid ? [d.grid, d.grid] : d.grid,
				j = i[0] || 1,
				k = i[1] || 1,
				l = Math.round((e.width - f.width) / j) * j,
				m = Math.round((e.height - f.height) / k) * k,
				n = f.width + l,
				o = f.height + m,
				p = d.maxWidth && n > d.maxWidth,
				q = d.maxHeight && o > d.maxHeight,
				r = d.minWidth && d.minWidth > n,
				s = d.minHeight && d.minHeight > o;
			d.grid = i, r && (n += j), s && (o += k), p && (n -= j), q && (o -= k), /^(se|s|e)$/.test(h) ? (c.size.width = n, c.size.height = o) : /^(ne)$/.test(h) ? (c.size.width = n, c.size.height = o, c.position.top = g.top - m) : /^(sw)$/.test(h) ? (c.size.width = n, c.size.height = o, c.position.left = g.left - l) : ((0 >= o - k || 0 >= n - j) && (b = c._getPaddingPlusBorderDimensions(this)), o - k > 0 ? (c.size.height = o, c.position.top = g.top - m) : (o = k - b.height, c.size.height = o, c.position.top = g.top + f.height - o), n - j > 0 ? (c.size.width = n, c.position.left = g.left - l) : (n = j - b.width, c.size.width = n, c.position.left = g.left + f.width - n))
		}
	}), a.ui.resizable, a.widget("ui.dialog", {
		version: "1.12.1",
		options: {
			appendTo: "body",
			autoOpen: !0,
			buttons: [],
			classes: {
				"ui-dialog": "ui-corner-all",
				"ui-dialog-titlebar": "ui-corner-all"
			},
			closeOnEscape: !0,
			closeText: "Close",
			draggable: !0,
			hide: null,
			height: "auto",
			maxHeight: null,
			maxWidth: null,
			minHeight: 150,
			minWidth: 150,
			modal: !1,
			position: {
				my: "center",
				at: "center",
				of: window,
				collision: "fit",
				using: function (b) {
					var c = a(this).css(b).offset().top;
					0 > c && a(this).css("top", b.top - c)
				}
			},
			resizable: !0,
			show: null,
			title: null,
			width: 300,
			beforeClose: null,
			close: null,
			drag: null,
			dragStart: null,
			dragStop: null,
			focus: null,
			open: null,
			resize: null,
			resizeStart: null,
			resizeStop: null
		},
		sizeRelatedOptions: {
			buttons: !0,
			height: !0,
			maxHeight: !0,
			maxWidth: !0,
			minHeight: !0,
			minWidth: !0,
			width: !0
		},
		resizableRelatedOptions: {
			maxHeight: !0,
			maxWidth: !0,
			minHeight: !0,
			minWidth: !0
		},
		_create: function () {
			this.originalCss = {
				display: this.element[0].style.display,
				width: this.element[0].style.width,
				minHeight: this.element[0].style.minHeight,
				maxHeight: this.element[0].style.maxHeight,
				height: this.element[0].style.height
			}, this.originalPosition = {
				parent: this.element.parent(),
				index: this.element.parent().children().index(this.element)
			}, this.originalTitle = this.element.attr("title"), null == this.options.title && null != this.originalTitle && (this.options.title = this.originalTitle), this.options.disabled && (this.options.disabled = !1), this._createWrapper(), this.element.show().removeAttr("title").appendTo(this.uiDialog), this._addClass("ui-dialog-content", "ui-widget-content"), this._createTitlebar(), this._createButtonPane(), this.options.draggable && a.fn.draggable && this._makeDraggable(), this.options.resizable && a.fn.resizable && this._makeResizable(), this._isOpen = !1, this._trackFocus()
		},
		_init: function () {
			this.options.autoOpen && this.open()
		},
		_appendTo: function () {
			var b = this.options.appendTo;
			return b && (b.jquery || b.nodeType) ? a(b) : this.document.find(b || "body").eq(0)
		},
		_destroy: function () {
			var a, b = this.originalPosition;
			this._untrackInstance(), this._destroyOverlay(), this.element.removeUniqueId().css(this.originalCss).detach(), this.uiDialog.remove(), this.originalTitle && this.element.attr("title", this.originalTitle), a = b.parent.children().eq(b.index), a.length && a[0] !== this.element[0] ? a.before(this.element) : b.parent.append(this.element)
		},
		widget: function () {
			return this.uiDialog
		},
		disable: a.noop,
		enable: a.noop,
		close: function (b) {
			var c = this;
			this._isOpen && !1 !== this._trigger("beforeClose", b) && (this._isOpen = !1, this._focusedElement = null, this._destroyOverlay(), this._untrackInstance(), this.opener.filter(":focusable").trigger("focus").length || a.ui.safeBlur(a.ui.safeActiveElement(this.document[0])), this._hide(this.uiDialog, this.options.hide, function () {
				c._trigger("close", b)
			}))
		},
		isOpen: function () {
			return this._isOpen
		},
		moveToTop: function () {
			this._moveToTop()
		},
		_moveToTop: function (b, c) {
			var d = !1,
				e = this.uiDialog.siblings(".ui-front:visible").map(function () {
					return +a(this).css("z-index")
				}).get(),
				f = Math.max.apply(null, e);
			return f >= +this.uiDialog.css("z-index") && (this.uiDialog.css("z-index", f + 1), d = !0), d && !c && this._trigger("focus", b), d
		},
		open: function () {
			var b = this;
			return this._isOpen ? void(this._moveToTop() && this._focusTabbable()) : (this._isOpen = !0, this.opener = a(a.ui.safeActiveElement(this.document[0])), this._size(), this._position(), this._createOverlay(), this._moveToTop(null, !0), this.overlay && this.overlay.css("z-index", this.uiDialog.css("z-index") - 1), this._show(this.uiDialog, this.options.show, function () {
				b._focusTabbable(), b._trigger("focus")
			}), this._makeFocusTarget(), void this._trigger("open"))
		},
		_focusTabbable: function () {
			var a = this._focusedElement;
			a || (a = this.element.find("[autofocus]")), a.length || (a = this.element.find(":tabbable")), a.length || (a = this.uiDialogButtonPane.find(":tabbable")), a.length || (a = this.uiDialogTitlebarClose.filter(":tabbable")), a.length || (a = this.uiDialog), a.eq(0).trigger("focus")
		},
		_keepFocus: function (b) {
			function c() {
				var b = a.ui.safeActiveElement(this.document[0]);
				this.uiDialog[0] === b || a.contains(this.uiDialog[0], b) || this._focusTabbable()
			}
			b.preventDefault(), c.call(this), this._delay(c)
		},
		_createWrapper: function () {
			this.uiDialog = a("<div>").hide().attr({
				tabIndex: -1,
				role: "dialog"
			}).appendTo(this._appendTo()), this._addClass(this.uiDialog, "ui-dialog", "ui-widget ui-widget-content ui-front"), this._on(this.uiDialog, {
				keydown: function (b) {
					if (this.options.closeOnEscape && !b.isDefaultPrevented() && b.keyCode && b.keyCode === a.ui.keyCode.ESCAPE) return b.preventDefault(), void this.close(b);
					if (b.keyCode === a.ui.keyCode.TAB && !b.isDefaultPrevented()) {
						var c = this.uiDialog.find(":tabbable"),
							d = c.filter(":first"),
							e = c.filter(":last");
						b.target !== e[0] && b.target !== this.uiDialog[0] || b.shiftKey ? b.target !== d[0] && b.target !== this.uiDialog[0] || !b.shiftKey || (this._delay(function () {
							e.trigger("focus")
						}), b.preventDefault()) : (this._delay(function () {
							d.trigger("focus")
						}), b.preventDefault())
					}
				},
				mousedown: function (a) {
					this._moveToTop(a) && this._focusTabbable()
				}
			}), this.element.find("[aria-describedby]").length || this.uiDialog.attr({
				"aria-describedby": this.element.uniqueId().attr("id")
			})
		},
		_createTitlebar: function () {
			var b;
			this.uiDialogTitlebar = a("<div>"), this._addClass(this.uiDialogTitlebar, "ui-dialog-titlebar", "ui-widget-header ui-helper-clearfix"), this._on(this.uiDialogTitlebar, {
				mousedown: function (b) {
					a(b.target).closest(".ui-dialog-titlebar-close") || this.uiDialog.trigger("focus")
				}
			}), this.uiDialogTitlebarClose = a("<button type='button'></button>").button({
				label: a("<a>").text(this.options.closeText).html(),
				icon: "ui-icon-closethick",
				showLabel: !1
			}).appendTo(this.uiDialogTitlebar), this._addClass(this.uiDialogTitlebarClose, "ui-dialog-titlebar-close"), this._on(this.uiDialogTitlebarClose, {
				click: function (a) {
					a.preventDefault(), this.close(a)
				}
			}), b = a("<span>").uniqueId().prependTo(this.uiDialogTitlebar), this._addClass(b, "ui-dialog-title"), this._title(b), this.uiDialogTitlebar.prependTo(this.uiDialog), this.uiDialog.attr({
				"aria-labelledby": b.attr("id")
			})
		},
		_title: function (a) {
			this.options.title ? a.text(this.options.title) : a.html("&#160;")
		},
		_createButtonPane: function () {
			this.uiDialogButtonPane = a("<div>"), this._addClass(this.uiDialogButtonPane, "ui-dialog-buttonpane", "ui-widget-content ui-helper-clearfix"), this.uiButtonSet = a("<div>").appendTo(this.uiDialogButtonPane), this._addClass(this.uiButtonSet, "ui-dialog-buttonset"), this._createButtons()
		},
		_createButtons: function () {
			var b = this,
				c = this.options.buttons;
			return this.uiDialogButtonPane.remove(), this.uiButtonSet.empty(), a.isEmptyObject(c) || a.isArray(c) && !c.length ? void this._removeClass(this.uiDialog, "ui-dialog-buttons") : (a.each(c, function (c, d) {
				var e, f;
				d = a.isFunction(d) ? {
					click: d,
					text: c
				} : d, d = a.extend({
					type: "button"
				}, d), e = d.click, f = {
					icon: d.icon,
					iconPosition: d.iconPosition,
					showLabel: d.showLabel,
					icons: d.icons,
					text: d.text
				}, delete d.click, delete d.icon, delete d.iconPosition, delete d.showLabel, delete d.icons, "boolean" == typeof d.text && delete d.text, a("<button></button>", d).button(f).appendTo(b.uiButtonSet).on("click", function () {
					e.apply(b.element[0], arguments)
				})
			}), this._addClass(this.uiDialog, "ui-dialog-buttons"), void this.uiDialogButtonPane.appendTo(this.uiDialog))
		},
		_makeDraggable: function () {
			function b(a) {
				return {
					position: a.position,
					offset: a.offset
				}
			}
			var c = this,
				d = this.options;
			this.uiDialog.draggable({
				cancel: ".ui-dialog-content, .ui-dialog-titlebar-close",
				handle: ".ui-dialog-titlebar",
				containment: "document",
				start: function (d, e) {
					c._addClass(a(this), "ui-dialog-dragging"), c._blockFrames(), c._trigger("dragStart", d, b(e))
				},
				drag: function (a, d) {
					c._trigger("drag", a, b(d))
				},
				stop: function (e, f) {
					var g = f.offset.left - c.document.scrollLeft(),
						h = f.offset.top - c.document.scrollTop();
					d.position = {
						my: "left top",
						at: "left" + (g >= 0 ? "+" : "") + g + " top" + (h >= 0 ? "+" : "") + h,
						of: c.window
					}, c._removeClass(a(this), "ui-dialog-dragging"), c._unblockFrames(), c._trigger("dragStop", e, b(f))
				}
			})
		},
		_makeResizable: function () {
			function b(a) {
				return {
					originalPosition: a.originalPosition,
					originalSize: a.originalSize,
					position: a.position,
					size: a.size
				}
			}
			var c = this,
				d = this.options,
				e = d.resizable,
				f = this.uiDialog.css("position"),
				g = "string" == typeof e ? e : "n,e,s,w,se,sw,ne,nw";
			this.uiDialog.resizable({
				cancel: ".ui-dialog-content",
				containment: "document",
				alsoResize: this.element,
				maxWidth: d.maxWidth,
				maxHeight: d.maxHeight,
				minWidth: d.minWidth,
				minHeight: this._minHeight(),
				handles: g,
				start: function (d, e) {
					c._addClass(a(this), "ui-dialog-resizing"), c._blockFrames(), c._trigger("resizeStart", d, b(e))
				},
				resize: function (a, d) {
					c._trigger("resize", a, b(d))
				},
				stop: function (e, f) {
					var g = c.uiDialog.offset(),
						h = g.left - c.document.scrollLeft(),
						i = g.top - c.document.scrollTop();
					d.height = c.uiDialog.height(), d.width = c.uiDialog.width(), d.position = {
						my: "left top",
						at: "left" + (h >= 0 ? "+" : "") + h + " top" + (i >= 0 ? "+" : "") + i,
						of: c.window
					}, c._removeClass(a(this), "ui-dialog-resizing"), c._unblockFrames(), c._trigger("resizeStop", e, b(f))
				}
			}).css("position", f)
		},
		_trackFocus: function () {
			this._on(this.widget(), {
				focusin: function (b) {
					this._makeFocusTarget(), this._focusedElement = a(b.target)
				}
			})
		},
		_makeFocusTarget: function () {
			this._untrackInstance(), this._trackingInstances().unshift(this)
		},
		_untrackInstance: function () {
			var b = this._trackingInstances(),
				c = a.inArray(this, b); - 1 !== c && b.splice(c, 1)
		},
		_trackingInstances: function () {
			var a = this.document.data("ui-dialog-instances");
			return a || (a = [], this.document.data("ui-dialog-instances", a)), a
		},
		_minHeight: function () {
			var a = this.options;
			return "auto" === a.height ? a.minHeight : Math.min(a.minHeight, a.height)
		},
		_position: function () {
			var a = this.uiDialog.is(":visible");
			a || this.uiDialog.show(), this.uiDialog.position(this.options.position), a || this.uiDialog.hide()
		},
		_setOptions: function (b) {
			var c = this,
				d = !1,
				e = {};
			a.each(b, function (a, b) {
				c._setOption(a, b), a in c.sizeRelatedOptions && (d = !0), a in c.resizableRelatedOptions && (e[a] = b)
			}), d && (this._size(), this._position()), this.uiDialog.is(":data(ui-resizable)") && this.uiDialog.resizable("option", e)
		},
		_setOption: function (b, c) {
			var d, e, f = this.uiDialog;
			"disabled" !== b && (this._super(b, c), "appendTo" === b && this.uiDialog.appendTo(this._appendTo()), "buttons" === b && this._createButtons(), "closeText" === b && this.uiDialogTitlebarClose.button({
				label: a("<a>").text("" + this.options.closeText).html()
			}), "draggable" === b && (d = f.is(":data(ui-draggable)"), d && !c && f.draggable("destroy"), !d && c && this._makeDraggable()), "position" === b && this._position(), "resizable" === b && (e = f.is(":data(ui-resizable)"), e && !c && f.resizable("destroy"), e && "string" == typeof c && f.resizable("option", "handles", c), e || !1 === c || this._makeResizable()), "title" === b && this._title(this.uiDialogTitlebar.find(".ui-dialog-title")))
		},
		_size: function () {
			var a, b, c, d = this.options;
			this.element.show().css({
				width: "auto",
				minHeight: 0,
				maxHeight: "none",
				height: 0
			}), d.minWidth > d.width && (d.width = d.minWidth), a = this.uiDialog.css({
				height: "auto",
				width: d.width
			}).outerHeight(), b = Math.max(0, d.minHeight - a), c = "number" == typeof d.maxHeight ? Math.max(0, d.maxHeight - a) : "none", "auto" === d.height ? this.element.css({
				minHeight: b,
				maxHeight: c,
				height: "auto"
			}) : this.element.height(Math.max(0, d.height - a)), this.uiDialog.is(":data(ui-resizable)") && this.uiDialog.resizable("option", "minHeight", this._minHeight())
		},
		_blockFrames: function () {
			this.iframeBlocks = this.document.find("iframe").map(function () {
				var b = a(this);
				return a("<div>").css({
					position: "absolute",
					width: b.outerWidth(),
					height: b.outerHeight()
				}).appendTo(b.parent()).offset(b.offset())[0]
			})
		},
		_unblockFrames: function () {
			this.iframeBlocks && (this.iframeBlocks.remove(), delete this.iframeBlocks)
		},
		_allowInteraction: function (b) {
			return !!a(b.target).closest(".ui-dialog").length || !!a(b.target).closest(".ui-datepicker").length
		},
		_createOverlay: function () {
			if (this.options.modal) {
				var b = !0;
				this._delay(function () {
					b = !1
				}), this.document.data("ui-dialog-overlays") || this._on(this.document, {
					focusin: function (a) {
						b || this._allowInteraction(a) || (a.preventDefault(), this._trackingInstances()[0]._focusTabbable())
					}
				}), this.overlay = a("<div>").appendTo(this._appendTo()), this._addClass(this.overlay, null, "ui-widget-overlay ui-front"), this._on(this.overlay, {
					mousedown: "_keepFocus"
				}), this.document.data("ui-dialog-overlays", (this.document.data("ui-dialog-overlays") || 0) + 1)
			}
		},
		_destroyOverlay: function () {
			if (this.options.modal && this.overlay) {
				var a = this.document.data("ui-dialog-overlays") - 1;
				a ? this.document.data("ui-dialog-overlays", a) : (this._off(this.document, "focusin"), this.document.removeData("ui-dialog-overlays")), this.overlay.remove(), this.overlay = null
			}
		}
	}), !1 !== a.uiBackCompat && a.widget("ui.dialog", a.ui.dialog, {
		options: {
			dialogClass: ""
		},
		_createWrapper: function () {
			this._super(), this.uiDialog.addClass(this.options.dialogClass)
		},
		_setOption: function (a, b) {
			"dialogClass" === a && this.uiDialog.removeClass(this.options.dialogClass).addClass(b), this._superApply(arguments)
		}
	}), a.ui.dialog, a.widget("ui.droppable", {
		version: "1.12.1",
		widgetEventPrefix: "drop",
		options: {
			accept: "*",
			addClasses: !0,
			greedy: !1,
			scope: "default",
			tolerance: "intersect",
			activate: null,
			deactivate: null,
			drop: null,
			out: null,
			over: null
		},
		_create: function () {
			var b, c = this.options,
				d = c.accept;
			this.isover = !1, this.isout = !0, this.accept = a.isFunction(d) ? d : function (a) {
				return a.is(d)
			}, this.proportions = function () {
				return arguments.length ? void(b = arguments[0]) : b || (b = {
					width: this.element[0].offsetWidth,
					height: this.element[0].offsetHeight
				})
			}, this._addToManager(c.scope), c.addClasses && this._addClass("ui-droppable")
		},
		_addToManager: function (b) {
			a.ui.ddmanager.droppables[b] = a.ui.ddmanager.droppables[b] || [], a.ui.ddmanager.droppables[b].push(this)
		},
		_splice: function (a) {
			for (var b = 0; a.length > b; b++) a[b] === this && a.splice(b, 1)
		},
		_destroy: function () {
			var b = a.ui.ddmanager.droppables[this.options.scope];
			this._splice(b)
		},
		_setOption: function (b, c) {
			if ("accept" === b) this.accept = a.isFunction(c) ? c : function (a) {
				return a.is(c)
			};
			else if ("scope" === b) {
				var d = a.ui.ddmanager.droppables[this.options.scope];
				this._splice(d), this._addToManager(c)
			}
			this._super(b, c)
		},
		_activate: function (b) {
			var c = a.ui.ddmanager.current;
			this._addActiveClass(), c && this._trigger("activate", b, this.ui(c))
		},
		_deactivate: function (b) {
			var c = a.ui.ddmanager.current;
			this._removeActiveClass(), c && this._trigger("deactivate", b, this.ui(c))
		},
		_over: function (b) {
			var c = a.ui.ddmanager.current;
			c && (c.currentItem || c.element)[0] !== this.element[0] && this.accept.call(this.element[0], c.currentItem || c.element) && (this._addHoverClass(), this._trigger("over", b, this.ui(c)))
		},
		_out: function (b) {
			var c = a.ui.ddmanager.current;
			c && (c.currentItem || c.element)[0] !== this.element[0] && this.accept.call(this.element[0], c.currentItem || c.element) && (this._removeHoverClass(),
				this._trigger("out", b, this.ui(c)))
		},
		_drop: function (b, c) {
			var d = c || a.ui.ddmanager.current,
				e = !1;
			return !(!d || (d.currentItem || d.element)[0] === this.element[0]) && (this.element.find(":data(ui-droppable)").not(".ui-draggable-dragging").each(function () {
				var c = a(this).droppable("instance");
				return c.options.greedy && !c.options.disabled && c.options.scope === d.options.scope && c.accept.call(c.element[0], d.currentItem || d.element) && r(d, a.extend(c, {
					offset: c.element.offset()
				}), c.options.tolerance, b) ? (e = !0, !1) : void 0
			}), !e && (!!this.accept.call(this.element[0], d.currentItem || d.element) && (this._removeActiveClass(), this._removeHoverClass(), this._trigger("drop", b, this.ui(d)), this.element)))
		},
		ui: function (a) {
			return {
				draggable: a.currentItem || a.element,
				helper: a.helper,
				position: a.position,
				offset: a.positionAbs
			}
		},
		_addHoverClass: function () {
			this._addClass("ui-droppable-hover")
		},
		_removeHoverClass: function () {
			this._removeClass("ui-droppable-hover")
		},
		_addActiveClass: function () {
			this._addClass("ui-droppable-active")
		},
		_removeActiveClass: function () {
			this._removeClass("ui-droppable-active")
		}
	});
	var r = a.ui.intersect = function () {
		function a(a, b, c) {
			return a >= b && b + c > a
		}
		return function (b, c, d, e) {
			if (!c.offset) return !1;
			var f = (b.positionAbs || b.position.absolute).left + b.margins.left,
				g = (b.positionAbs || b.position.absolute).top + b.margins.top,
				h = f + b.helperProportions.width,
				i = g + b.helperProportions.height,
				j = c.offset.left,
				k = c.offset.top,
				l = j + c.proportions().width,
				m = k + c.proportions().height;
			switch (d) {
				case "fit":
					return f >= j && l >= h && g >= k && m >= i;
				case "intersect":
					return f + b.helperProportions.width / 2 > j && l > h - b.helperProportions.width / 2 && g + b.helperProportions.height / 2 > k && m > i - b.helperProportions.height / 2;
				case "pointer":
					return a(e.pageY, k, c.proportions().height) && a(e.pageX, j, c.proportions().width);
				case "touch":
					return (g >= k && m >= g || i >= k && m >= i || k > g && i > m) && (f >= j && l >= f || h >= j && l >= h || j > f && h > l);
				default:
					return !1
			}
		}
	}();
	a.ui.ddmanager = {
		current: null,
		droppables: {
			default: []
		},
		prepareOffsets: function (b, c) {
			var d, e, f = a.ui.ddmanager.droppables[b.options.scope] || [],
				g = c ? c.type : null,
				h = (b.currentItem || b.element).find(":data(ui-droppable)").addBack();
			a: for (d = 0; f.length > d; d++)
				if (!(f[d].options.disabled || b && !f[d].accept.call(f[d].element[0], b.currentItem || b.element))) {
					for (e = 0; h.length > e; e++)
						if (h[e] === f[d].element[0]) {
							f[d].proportions().height = 0;
							continue a
						}
					f[d].visible = "none" !== f[d].element.css("display"), f[d].visible && ("mousedown" === g && f[d]._activate.call(f[d], c), f[d].offset = f[d].element.offset(), f[d].proportions({
						width: f[d].element[0].offsetWidth,
						height: f[d].element[0].offsetHeight
					}))
				}
		},
		drop: function (b, c) {
			var d = !1;
			return a.each((a.ui.ddmanager.droppables[b.options.scope] || []).slice(), function () {
				this.options && (!this.options.disabled && this.visible && r(b, this, this.options.tolerance, c) && (d = this._drop.call(this, c) || d), !this.options.disabled && this.visible && this.accept.call(this.element[0], b.currentItem || b.element) && (this.isout = !0, this.isover = !1, this._deactivate.call(this, c)))
			}), d
		},
		dragStart: function (b, c) {
			b.element.parentsUntil("body").on("scroll.droppable", function () {
				b.options.refreshPositions || a.ui.ddmanager.prepareOffsets(b, c)
			})
		},
		drag: function (b, c) {
			b.options.refreshPositions && a.ui.ddmanager.prepareOffsets(b, c), a.each(a.ui.ddmanager.droppables[b.options.scope] || [], function () {
				if (!this.options.disabled && !this.greedyChild && this.visible) {
					var d, e, f, g = r(b, this, this.options.tolerance, c),
						h = !g && this.isover ? "isout" : g && !this.isover ? "isover" : null;
					h && (this.options.greedy && (e = this.options.scope, f = this.element.parents(":data(ui-droppable)").filter(function () {
						return a(this).droppable("instance").options.scope === e
					}), f.length && (d = a(f[0]).droppable("instance"), d.greedyChild = "isover" === h)), d && "isover" === h && (d.isover = !1, d.isout = !0, d._out.call(d, c)), this[h] = !0, this["isout" === h ? "isover" : "isout"] = !1, this["isover" === h ? "_over" : "_out"].call(this, c), d && "isout" === h && (d.isout = !1, d.isover = !0, d._over.call(d, c)))
				}
			})
		},
		dragStop: function (b, c) {
			b.element.parentsUntil("body").off("scroll.droppable"), b.options.refreshPositions || a.ui.ddmanager.prepareOffsets(b, c)
		}
	}, !1 !== a.uiBackCompat && a.widget("ui.droppable", a.ui.droppable, {
		options: {
			hoverClass: !1,
			activeClass: !1
		},
		_addActiveClass: function () {
			this._super(), this.options.activeClass && this.element.addClass(this.options.activeClass)
		},
		_removeActiveClass: function () {
			this._super(), this.options.activeClass && this.element.removeClass(this.options.activeClass)
		},
		_addHoverClass: function () {
			this._super(), this.options.hoverClass && this.element.addClass(this.options.hoverClass)
		},
		_removeHoverClass: function () {
			this._super(), this.options.hoverClass && this.element.removeClass(this.options.hoverClass)
		}
	}), a.ui.droppable, a.widget("ui.progressbar", {
		version: "1.12.1",
		options: {
			classes: {
				"ui-progressbar": "ui-corner-all",
				"ui-progressbar-value": "ui-corner-left",
				"ui-progressbar-complete": "ui-corner-right"
			},
			max: 100,
			value: 0,
			change: null,
			complete: null
		},
		min: 0,
		_create: function () {
			this.oldValue = this.options.value = this._constrainedValue(), this.element.attr({
				role: "progressbar",
				"aria-valuemin": this.min
			}), this._addClass("ui-progressbar", "ui-widget ui-widget-content"), this.valueDiv = a("<div>").appendTo(this.element), this._addClass(this.valueDiv, "ui-progressbar-value", "ui-widget-header"), this._refreshValue()
		},
		_destroy: function () {
			this.element.removeAttr("role aria-valuemin aria-valuemax aria-valuenow"), this.valueDiv.remove()
		},
		value: function (a) {
			return void 0 === a ? this.options.value : (this.options.value = this._constrainedValue(a), void this._refreshValue())
		},
		_constrainedValue: function (a) {
			return void 0 === a && (a = this.options.value), this.indeterminate = !1 === a, "number" != typeof a && (a = 0), !this.indeterminate && Math.min(this.options.max, Math.max(this.min, a))
		},
		_setOptions: function (a) {
			var b = a.value;
			delete a.value, this._super(a), this.options.value = this._constrainedValue(b), this._refreshValue()
		},
		_setOption: function (a, b) {
			"max" === a && (b = Math.max(this.min, b)), this._super(a, b)
		},
		_setOptionDisabled: function (a) {
			this._super(a), this.element.attr("aria-disabled", a), this._toggleClass(null, "ui-state-disabled", !!a)
		},
		_percentage: function () {
			return this.indeterminate ? 100 : 100 * (this.options.value - this.min) / (this.options.max - this.min)
		},
		_refreshValue: function () {
			var b = this.options.value,
				c = this._percentage();
			this.valueDiv.toggle(this.indeterminate || b > this.min).width(c.toFixed(0) + "%"), this._toggleClass(this.valueDiv, "ui-progressbar-complete", null, b === this.options.max)._toggleClass("ui-progressbar-indeterminate", null, this.indeterminate), this.indeterminate ? (this.element.removeAttr("aria-valuenow"), this.overlayDiv || (this.overlayDiv = a("<div>").appendTo(this.valueDiv), this._addClass(this.overlayDiv, "ui-progressbar-overlay"))) : (this.element.attr({
				"aria-valuemax": this.options.max,
				"aria-valuenow": b
			}), this.overlayDiv && (this.overlayDiv.remove(), this.overlayDiv = null)), this.oldValue !== b && (this.oldValue = b, this._trigger("change")), b === this.options.max && this._trigger("complete")
		}
	}), a.widget("ui.selectable", a.ui.mouse, {
		version: "1.12.1",
		options: {
			appendTo: "body",
			autoRefresh: !0,
			distance: 0,
			filter: "*",
			tolerance: "touch",
			selected: null,
			selecting: null,
			start: null,
			stop: null,
			unselected: null,
			unselecting: null
		},
		_create: function () {
			var b = this;
			this._addClass("ui-selectable"), this.dragged = !1, this.refresh = function () {
				b.elementPos = a(b.element[0]).offset(), b.selectees = a(b.options.filter, b.element[0]), b._addClass(b.selectees, "ui-selectee"), b.selectees.each(function () {
					var c = a(this),
						d = c.offset(),
						e = {
							left: d.left - b.elementPos.left,
							top: d.top - b.elementPos.top
						};
					a.data(this, "selectable-item", {
						element: this,
						$element: c,
						left: e.left,
						top: e.top,
						right: e.left + c.outerWidth(),
						bottom: e.top + c.outerHeight(),
						startselected: !1,
						selected: c.hasClass("ui-selected"),
						selecting: c.hasClass("ui-selecting"),
						unselecting: c.hasClass("ui-unselecting")
					})
				})
			}, this.refresh(), this._mouseInit(), this.helper = a("<div>"), this._addClass(this.helper, "ui-selectable-helper")
		},
		_destroy: function () {
			this.selectees.removeData("selectable-item"), this._mouseDestroy()
		},
		_mouseStart: function (b) {
			var c = this,
				d = this.options;
			this.opos = [b.pageX, b.pageY], this.elementPos = a(this.element[0]).offset(), this.options.disabled || (this.selectees = a(d.filter, this.element[0]), this._trigger("start", b), a(d.appendTo).append(this.helper), this.helper.css({
				left: b.pageX,
				top: b.pageY,
				width: 0,
				height: 0
			}), d.autoRefresh && this.refresh(), this.selectees.filter(".ui-selected").each(function () {
				var d = a.data(this, "selectable-item");
				d.startselected = !0, b.metaKey || b.ctrlKey || (c._removeClass(d.$element, "ui-selected"), d.selected = !1, c._addClass(d.$element, "ui-unselecting"), d.unselecting = !0, c._trigger("unselecting", b, {
					unselecting: d.element
				}))
			}), a(b.target).parents().addBack().each(function () {
				var d, e = a.data(this, "selectable-item");
				return e ? (d = !b.metaKey && !b.ctrlKey || !e.$element.hasClass("ui-selected"), c._removeClass(e.$element, d ? "ui-unselecting" : "ui-selected")._addClass(e.$element, d ? "ui-selecting" : "ui-unselecting"), e.unselecting = !d, e.selecting = d, e.selected = d, d ? c._trigger("selecting", b, {
					selecting: e.element
				}) : c._trigger("unselecting", b, {
					unselecting: e.element
				}), !1) : void 0
			}))
		},
		_mouseDrag: function (b) {
			if (this.dragged = !0, !this.options.disabled) {
				var c, d = this,
					e = this.options,
					f = this.opos[0],
					g = this.opos[1],
					h = b.pageX,
					i = b.pageY;
				return f > h && (c = h, h = f, f = c), g > i && (c = i, i = g, g = c), this.helper.css({
					left: f,
					top: g,
					width: h - f,
					height: i - g
				}), this.selectees.each(function () {
					var c = a.data(this, "selectable-item"),
						j = !1,
						k = {};
					c && c.element !== d.element[0] && (k.left = c.left + d.elementPos.left, k.right = c.right + d.elementPos.left, k.top = c.top + d.elementPos.top, k.bottom = c.bottom + d.elementPos.top, "touch" === e.tolerance ? j = !(k.left > h || f > k.right || k.top > i || g > k.bottom) : "fit" === e.tolerance && (j = k.left > f && h > k.right && k.top > g && i > k.bottom), j ? (c.selected && (d._removeClass(c.$element, "ui-selected"), c.selected = !1), c.unselecting && (d._removeClass(c.$element, "ui-unselecting"), c.unselecting = !1), c.selecting || (d._addClass(c.$element, "ui-selecting"), c.selecting = !0, d._trigger("selecting", b, {
						selecting: c.element
					}))) : (c.selecting && ((b.metaKey || b.ctrlKey) && c.startselected ? (d._removeClass(c.$element, "ui-selecting"), c.selecting = !1, d._addClass(c.$element, "ui-selected"), c.selected = !0) : (d._removeClass(c.$element, "ui-selecting"), c.selecting = !1, c.startselected && (d._addClass(c.$element, "ui-unselecting"), c.unselecting = !0), d._trigger("unselecting", b, {
						unselecting: c.element
					}))), c.selected && (b.metaKey || b.ctrlKey || c.startselected || (d._removeClass(c.$element, "ui-selected"), c.selected = !1, d._addClass(c.$element, "ui-unselecting"), c.unselecting = !0, d._trigger("unselecting", b, {
						unselecting: c.element
					})))))
				}), !1
			}
		},
		_mouseStop: function (b) {
			var c = this;
			return this.dragged = !1, a(".ui-unselecting", this.element[0]).each(function () {
				var d = a.data(this, "selectable-item");
				c._removeClass(d.$element, "ui-unselecting"), d.unselecting = !1, d.startselected = !1, c._trigger("unselected", b, {
					unselected: d.element
				})
			}), a(".ui-selecting", this.element[0]).each(function () {
				var d = a.data(this, "selectable-item");
				c._removeClass(d.$element, "ui-selecting")._addClass(d.$element, "ui-selected"), d.selecting = !1, d.selected = !0, d.startselected = !0, c._trigger("selected", b, {
					selected: d.element
				})
			}), this._trigger("stop", b), this.helper.remove(), !1
		}
	}), a.widget("ui.selectmenu", [a.ui.formResetMixin, {
		version: "1.12.1",
		defaultElement: "<select>",
		options: {
			appendTo: null,
			classes: {
				"ui-selectmenu-button-open": "ui-corner-top",
				"ui-selectmenu-button-closed": "ui-corner-all"
			},
			disabled: null,
			icons: {
				button: "ui-icon-triangle-1-s"
			},
			position: {
				my: "left top",
				at: "left bottom",
				collision: "none"
			},
			width: !1,
			change: null,
			close: null,
			focus: null,
			open: null,
			select: null
		},
		_create: function () {
			var b = this.element.uniqueId().attr("id");
			this.ids = {
				element: b,
				button: b + "-button",
				menu: b + "-menu"
			}, this._drawButton(), this._drawMenu(), this._bindFormResetHandler(), this._rendered = !1, this.menuItems = a()
		},
		_drawButton: function () {
			var b, c = this,
				d = this._parseOption(this.element.find("option:selected"), this.element[0].selectedIndex);
			this.labels = this.element.labels().attr("for", this.ids.button), this._on(this.labels, {
				click: function (a) {
					this.button.focus(), a.preventDefault()
				}
			}), this.element.hide(), this.button = a("<span>", {
				tabindex: this.options.disabled ? -1 : 0,
				id: this.ids.button,
				role: "combobox",
				"aria-expanded": "false",
				"aria-autocomplete": "list",
				"aria-owns": this.ids.menu,
				"aria-haspopup": "true",
				title: this.element.attr("title")
			}).insertAfter(this.element), this._addClass(this.button, "ui-selectmenu-button ui-selectmenu-button-closed", "ui-button ui-widget"), b = a("<span>").appendTo(this.button), this._addClass(b, "ui-selectmenu-icon", "ui-icon " + this.options.icons.button), this.buttonItem = this._renderButtonItem(d).appendTo(this.button), !1 !== this.options.width && this._resizeButton(), this._on(this.button, this._buttonEvents), this.button.one("focusin", function () {
				c._rendered || c._refreshMenu()
			})
		},
		_drawMenu: function () {
			var b = this;
			this.menu = a("<ul>", {
				"aria-hidden": "true",
				"aria-labelledby": this.ids.button,
				id: this.ids.menu
			}), this.menuWrap = a("<div>").append(this.menu), this._addClass(this.menuWrap, "ui-selectmenu-menu", "ui-front"), this.menuWrap.appendTo(this._appendTo()), this.menuInstance = this.menu.menu({
				classes: {
					"ui-menu": "ui-corner-bottom"
				},
				role: "listbox",
				select: function (a, c) {
					a.preventDefault(), b._setSelection(), b._select(c.item.data("ui-selectmenu-item"), a)
				},
				focus: function (a, c) {
					var d = c.item.data("ui-selectmenu-item");
					null != b.focusIndex && d.index !== b.focusIndex && (b._trigger("focus", a, {
						item: d
					}), b.isOpen || b._select(d, a)), b.focusIndex = d.index, b.button.attr("aria-activedescendant", b.menuItems.eq(d.index).attr("id"))
				}
			}).menu("instance"), this.menuInstance._off(this.menu, "mouseleave"), this.menuInstance._closeOnDocumentClick = function () {
				return !1
			}, this.menuInstance._isDivider = function () {
				return !1
			}
		},
		refresh: function () {
			this._refreshMenu(), this.buttonItem.replaceWith(this.buttonItem = this._renderButtonItem(this._getSelectedItem().data("ui-selectmenu-item") || {})), null === this.options.width && this._resizeButton()
		},
		_refreshMenu: function () {
			var a, b = this.element.find("option");
			this.menu.empty(), this._parseOptions(b), this._renderMenu(this.menu, this.items), this.menuInstance.refresh(), this.menuItems = this.menu.find("li").not(".ui-selectmenu-optgroup").find(".ui-menu-item-wrapper"), this._rendered = !0, b.length && (a = this._getSelectedItem(), this.menuInstance.focus(null, a), this._setAria(a.data("ui-selectmenu-item")), this._setOption("disabled", this.element.prop("disabled")))
		},
		open: function (a) {
			this.options.disabled || (this._rendered ? (this._removeClass(this.menu.find(".ui-state-active"), null, "ui-state-active"), this.menuInstance.focus(null, this._getSelectedItem())) : this._refreshMenu(), this.menuItems.length && (this.isOpen = !0, this._toggleAttr(), this._resizeMenu(), this._position(), this._on(this.document, this._documentClick), this._trigger("open", a)))
		},
		_position: function () {
			this.menuWrap.position(a.extend({ of: this.button
			}, this.options.position))
		},
		close: function (a) {
			this.isOpen && (this.isOpen = !1, this._toggleAttr(), this.range = null, this._off(this.document), this._trigger("close", a))
		},
		widget: function () {
			return this.button
		},
		menuWidget: function () {
			return this.menu
		},
		_renderButtonItem: function (b) {
			var c = a("<span>");
			return this._setText(c, b.label), this._addClass(c, "ui-selectmenu-text"), c
		},
		_renderMenu: function (b, c) {
			var d = this,
				e = "";
			a.each(c, function (c, f) {
				var g;
				f.optgroup !== e && (g = a("<li>", {
					text: f.optgroup
				}), d._addClass(g, "ui-selectmenu-optgroup", "ui-menu-divider" + (f.element.parent("optgroup").prop("disabled") ? " ui-state-disabled" : "")), g.appendTo(b), e = f.optgroup), d._renderItemData(b, f)
			})
		},
		_renderItemData: function (a, b) {
			return this._renderItem(a, b).data("ui-selectmenu-item", b)
		},
		_renderItem: function (b, c) {
			var d = a("<li>"),
				e = a("<div>", {
					title: c.element.attr("title")
				});
			return c.disabled && this._addClass(d, null, "ui-state-disabled"), this._setText(e, c.label), d.append(e).appendTo(b)
		},
		_setText: function (a, b) {
			b ? a.text(b) : a.html("&#160;")
		},
		_move: function (a, b) {
			var c, d, e = ".ui-menu-item";
			this.isOpen ? c = this.menuItems.eq(this.focusIndex).parent("li") : (c = this.menuItems.eq(this.element[0].selectedIndex).parent("li"), e += ":not(.ui-state-disabled)"), d = "first" === a || "last" === a ? c["first" === a ? "prevAll" : "nextAll"](e).eq(-1) : c[a + "All"](e).eq(0), d.length && this.menuInstance.focus(b, d)
		},
		_getSelectedItem: function () {
			return this.menuItems.eq(this.element[0].selectedIndex).parent("li")
		},
		_toggle: function (a) {
			this[this.isOpen ? "close" : "open"](a)
		},
		_setSelection: function () {
			var a;
			this.range && (window.getSelection ? (a = window.getSelection(), a.removeAllRanges(), a.addRange(this.range)) : this.range.select(), this.button.focus())
		},
		_documentClick: {
			mousedown: function (b) {
				this.isOpen && (a(b.target).closest(".ui-selectmenu-menu, #" + a.ui.escapeSelector(this.ids.button)).length || this.close(b))
			}
		},
		_buttonEvents: {
			mousedown: function () {
				var a;
				window.getSelection ? (a = window.getSelection(), a.rangeCount && (this.range = a.getRangeAt(0))) : this.range = document.selection.createRange()
			},
			click: function (a) {
				this._setSelection(), this._toggle(a)
			},
			keydown: function (b) {
				var c = !0;
				switch (b.keyCode) {
					case a.ui.keyCode.TAB:
					case a.ui.keyCode.ESCAPE:
						this.close(b), c = !1;
						break;
					case a.ui.keyCode.ENTER:
						this.isOpen && this._selectFocusedItem(b);
						break;
					case a.ui.keyCode.UP:
						b.altKey ? this._toggle(b) : this._move("prev", b);
						break;
					case a.ui.keyCode.DOWN:
						b.altKey ? this._toggle(b) : this._move("next", b);
						break;
					case a.ui.keyCode.SPACE:
						this.isOpen ? this._selectFocusedItem(b) : this._toggle(b);
						break;
					case a.ui.keyCode.LEFT:
						this._move("prev", b);
						break;
					case a.ui.keyCode.RIGHT:
						this._move("next", b);
						break;
					case a.ui.keyCode.HOME:
					case a.ui.keyCode.PAGE_UP:
						this._move("first", b);
						break;
					case a.ui.keyCode.END:
					case a.ui.keyCode.PAGE_DOWN:
						this._move("last", b);
						break;
					default:
						this.menu.trigger(b), c = !1
				}
				c && b.preventDefault()
			}
		},
		_selectFocusedItem: function (a) {
			var b = this.menuItems.eq(this.focusIndex).parent("li");
			b.hasClass("ui-state-disabled") || this._select(b.data("ui-selectmenu-item"), a)
		},
		_select: function (a, b) {
			var c = this.element[0].selectedIndex;
			this.element[0].selectedIndex = a.index, this.buttonItem.replaceWith(this.buttonItem = this._renderButtonItem(a)), this._setAria(a), this._trigger("select", b, {
				item: a
			}), a.index !== c && this._trigger("change", b, {
				item: a
			}), this.close(b)
		},
		_setAria: function (a) {
			var b = this.menuItems.eq(a.index).attr("id");
			this.button.attr({
				"aria-labelledby": b,
				"aria-activedescendant": b
			}), this.menu.attr("aria-activedescendant", b)
		},
		_setOption: function (a, b) {
			if ("icons" === a) {
				var c = this.button.find("span.ui-icon");
				this._removeClass(c, null, this.options.icons.button)._addClass(c, null, b.button)
			}
			this._super(a, b), "appendTo" === a && this.menuWrap.appendTo(this._appendTo()), "width" === a && this._resizeButton()
		},
		_setOptionDisabled: function (a) {
			this._super(a), this.menuInstance.option("disabled", a), this.button.attr("aria-disabled", a), this._toggleClass(this.button, null, "ui-state-disabled", a), this.element.prop("disabled", a), a ? (this.button.attr("tabindex", -1), this.close()) : this.button.attr("tabindex", 0)
		},
		_appendTo: function () {
			var b = this.options.appendTo;
			return b && (b = b.jquery || b.nodeType ? a(b) : this.document.find(b).eq(0)), b && b[0] || (b = this.element.closest(".ui-front, dialog")), b.length || (b = this.document[0].body), b
		},
		_toggleAttr: function () {
			this.button.attr("aria-expanded", this.isOpen), this._removeClass(this.button, "ui-selectmenu-button-" + (this.isOpen ? "closed" : "open"))._addClass(this.button, "ui-selectmenu-button-" + (this.isOpen ? "open" : "closed"))._toggleClass(this.menuWrap, "ui-selectmenu-open", null, this.isOpen), this.menu.attr("aria-hidden", !this.isOpen)
		},
		_resizeButton: function () {
			var a = this.options.width;
			return !1 === a ? void this.button.css("width", "") : (null === a && (a = this.element.show().outerWidth(), this.element.hide()), void this.button.outerWidth(a))
		},
		_resizeMenu: function () {
			this.menu.outerWidth(Math.max(this.button.outerWidth(), this.menu.width("").outerWidth() + 1))
		},
		_getCreateOptions: function () {
			var a = this._super();
			return a.disabled = this.element.prop("disabled"), a
		},
		_parseOptions: function (b) {
			var c = this,
				d = [];
			b.each(function (b, e) {
				d.push(c._parseOption(a(e), b))
			}), this.items = d
		},
		_parseOption: function (a, b) {
			var c = a.parent("optgroup");
			return {
				element: a,
				index: b,
				value: a.val(),
				label: a.text(),
				optgroup: c.attr("label") || "",
				disabled: c.prop("disabled") || a.prop("disabled")
			}
		},
		_destroy: function () {
			this._unbindFormResetHandler(), this.menuWrap.remove(), this.button.remove(), this.element.show(), this.element.removeUniqueId(), this.labels.attr("for", this.ids.element)
		}
	}]), a.widget("ui.slider", a.ui.mouse, {
		version: "1.12.1",
		widgetEventPrefix: "slide",
		options: {
			animate: !1,
			classes: {
				"ui-slider": "ui-corner-all",
				"ui-slider-handle": "ui-corner-all",
				"ui-slider-range": "ui-corner-all ui-widget-header"
			},
			distance: 0,
			max: 100,
			min: 0,
			orientation: "horizontal",
			range: !1,
			step: 1,
			value: 0,
			values: null,
			change: null,
			slide: null,
			start: null,
			stop: null
		},
		numPages: 5,
		_create: function () {
			this._keySliding = !1, this._mouseSliding = !1, this._animateOff = !0, this._handleIndex = null, this._detectOrientation(), this._mouseInit(), this._calculateNewMax(), this._addClass("ui-slider ui-slider-" + this.orientation, "ui-widget ui-widget-content"), this._refresh(), this._animateOff = !1
		},
		_refresh: function () {
			this._createRange(), this._createHandles(), this._setupEvents(), this._refreshValue()
		},
		_createHandles: function () {
			var b, c, d = this.options,
				e = this.element.find(".ui-slider-handle"),
				f = [];
			for (c = d.values && d.values.length || 1, e.length > c && (e.slice(c).remove(), e = e.slice(0, c)), b = e.length; c > b; b++) f.push("<span tabindex='0'></span>");
			this.handles = e.add(a(f.join("")).appendTo(this.element)), this._addClass(this.handles, "ui-slider-handle", "ui-state-default"), this.handle = this.handles.eq(0), this.handles.each(function (b) {
				a(this).data("ui-slider-handle-index", b).attr("tabIndex", 0)
			})
		},
		_createRange: function () {
			var b = this.options;
			b.range ? (!0 === b.range && (b.values ? b.values.length && 2 !== b.values.length ? b.values = [b.values[0], b.values[0]] : a.isArray(b.values) && (b.values = b.values.slice(0)) : b.values = [this._valueMin(), this._valueMin()]), this.range && this.range.length ? (this._removeClass(this.range, "ui-slider-range-min ui-slider-range-max"), this.range.css({
				left: "",
				bottom: ""
			})) : (this.range = a("<div>").appendTo(this.element), this._addClass(this.range, "ui-slider-range")), ("min" === b.range || "max" === b.range) && this._addClass(this.range, "ui-slider-range-" + b.range)) : (this.range && this.range.remove(), this.range = null)
		},
		_setupEvents: function () {
			this._off(this.handles), this._on(this.handles, this._handleEvents), this._hoverable(this.handles), this._focusable(this.handles)
		},
		_destroy: function () {
			this.handles.remove(), this.range && this.range.remove(), this._mouseDestroy()
		},
		_mouseCapture: function (b) {
			var c, d, e, f, g, h, i, j = this,
				k = this.options;
			return !k.disabled && (this.elementSize = {
				width: this.element.outerWidth(),
				height: this.element.outerHeight()
			}, this.elementOffset = this.element.offset(), c = {
				x: b.pageX,
				y: b.pageY
			}, d = this._normValueFromMouse(c), e = this._valueMax() - this._valueMin() + 1, this.handles.each(function (b) {
				var c = Math.abs(d - j.values(b));
				(e > c || e === c && (b === j._lastChangedValue || j.values(b) === k.min)) && (e = c, f = a(this), g = b)
			}), !1 !== this._start(b, g) && (this._mouseSliding = !0, this._handleIndex = g, this._addClass(f, null, "ui-state-active"), f.trigger("focus"), h = f.offset(), i = !a(b.target).parents().addBack().is(".ui-slider-handle"), this._clickOffset = i ? {
				left: 0,
				top: 0
			} : {
				left: b.pageX - h.left - f.width() / 2,
				top: b.pageY - h.top - f.height() / 2 - (parseInt(f.css("borderTopWidth"), 10) || 0) - (parseInt(f.css("borderBottomWidth"), 10) || 0) + (parseInt(f.css("marginTop"), 10) || 0)
			}, this.handles.hasClass("ui-state-hover") || this._slide(b, g, d), this._animateOff = !0, !0))
		},
		_mouseStart: function () {
			return !0
		},
		_mouseDrag: function (a) {
			var b = {
					x: a.pageX,
					y: a.pageY
				},
				c = this._normValueFromMouse(b);
			return this._slide(a, this._handleIndex, c), !1
		},
		_mouseStop: function (a) {
			return this._removeClass(this.handles, null, "ui-state-active"), this._mouseSliding = !1, this._stop(a, this._handleIndex), this._change(a, this._handleIndex), this._handleIndex = null, this._clickOffset = null, this._animateOff = !1, !1
		},
		_detectOrientation: function () {
			this.orientation = "vertical" === this.options.orientation ? "vertical" : "horizontal"
		},
		_normValueFromMouse: function (a) {
			var b, c, d, e, f;
			return "horizontal" === this.orientation ? (b = this.elementSize.width, c = a.x - this.elementOffset.left - (this._clickOffset ? this._clickOffset.left : 0)) : (b = this.elementSize.height, c = a.y - this.elementOffset.top - (this._clickOffset ? this._clickOffset.top : 0)), d = c / b, d > 1 && (d = 1), 0 > d && (d = 0), "vertical" === this.orientation && (d = 1 - d), e = this._valueMax() - this._valueMin(), f = this._valueMin() + d * e, this._trimAlignValue(f)
		},
		_uiHash: function (a, b, c) {
			var d = {
				handle: this.handles[a],
				handleIndex: a,
				value: void 0 !== b ? b : this.value()
			};
			return this._hasMultipleValues() && (d.value = void 0 !== b ? b : this.values(a), d.values = c || this.values()), d
		},
		_hasMultipleValues: function () {
			return this.options.values && this.options.values.length
		},
		_start: function (a, b) {
			return this._trigger("start", a, this._uiHash(b))
		},
		_slide: function (a, b, c) {
			var d, e = this.value(),
				f = this.values();
			this._hasMultipleValues() && (d = this.values(b ? 0 : 1), e = this.values(b), 2 === this.options.values.length && !0 === this.options.range && (c = 0 === b ? Math.min(d, c) : Math.max(d, c)), f[b] = c), c !== e && !1 !== this._trigger("slide", a, this._uiHash(b, c, f)) && (this._hasMultipleValues() ? this.values(b, c) : this.value(c))
		},
		_stop: function (a, b) {
			this._trigger("stop", a, this._uiHash(b))
		},
		_change: function (a, b) {
			this._keySliding || this._mouseSliding || (this._lastChangedValue = b, this._trigger("change", a, this._uiHash(b)))
		},
		value: function (a) {
			return arguments.length ? (this.options.value = this._trimAlignValue(a), this._refreshValue(), void this._change(null, 0)) : this._value()
		},
		values: function (b, c) {
			var d, e, f;
			if (arguments.length > 1) return this.options.values[b] = this._trimAlignValue(c), this._refreshValue(), void this._change(null, b);
			if (!arguments.length) return this._values();
			if (!a.isArray(arguments[0])) return this._hasMultipleValues() ? this._values(b) : this.value();
			for (d = this.options.values, e = arguments[0], f = 0; d.length > f; f += 1) d[f] = this._trimAlignValue(e[f]), this._change(null, f);
			this._refreshValue()
		},
		_setOption: function (b, c) {
			var d, e = 0;
			switch ("range" === b && !0 === this.options.range && ("min" === c ? (this.options.value = this._values(0), this.options.values = null) : "max" === c && (this.options.value = this._values(this.options.values.length - 1), this.options.values = null)), a.isArray(this.options.values) && (e = this.options.values.length), this._super(b, c), b) {
				case "orientation":
					this._detectOrientation(), this._removeClass("ui-slider-horizontal ui-slider-vertical")._addClass("ui-slider-" + this.orientation), this._refreshValue(), this.options.range && this._refreshRange(c), this.handles.css("horizontal" === c ? "bottom" : "left", "");
					break;
				case "value":
					this._animateOff = !0, this._refreshValue(), this._change(null, 0), this._animateOff = !1;
					break;
				case "values":
					for (this._animateOff = !0, this._refreshValue(), d = e - 1; d >= 0; d--) this._change(null, d);
					this._animateOff = !1;
					break;
				case "step":
				case "min":
				case "max":
					this._animateOff = !0, this._calculateNewMax(), this._refreshValue(), this._animateOff = !1;
					break;
				case "range":
					this._animateOff = !0, this._refresh(), this._animateOff = !1
			}
		},
		_setOptionDisabled: function (a) {
			this._super(a), this._toggleClass(null, "ui-state-disabled", !!a)
		},
		_value: function () {
			var a = this.options.value;
			return a = this._trimAlignValue(a)
		},
		_values: function (a) {
			var b, c, d;
			if (arguments.length) return b = this.options.values[a], b = this._trimAlignValue(b);
			if (this._hasMultipleValues()) {
				for (c = this.options.values.slice(), d = 0; c.length > d; d += 1) c[d] = this._trimAlignValue(c[d]);
				return c
			}
			return []
		},
		_trimAlignValue: function (a) {
			if (this._valueMin() >= a) return this._valueMin();
			if (a >= this._valueMax()) return this._valueMax();
			var b = this.options.step > 0 ? this.options.step : 1,
				c = (a - this._valueMin()) % b,
				d = a - c;
			return 2 * Math.abs(c) >= b && (d += c > 0 ? b : -b), parseFloat(d.toFixed(5))
		},
		_calculateNewMax: function () {
			var a = this.options.max,
				b = this._valueMin(),
				c = this.options.step;
			a = Math.round((a - b) / c) * c + b, a > this.options.max && (a -= c), this.max = parseFloat(a.toFixed(this._precision()))
		},
		_precision: function () {
			var a = this._precisionOf(this.options.step);
			return null !== this.options.min && (a = Math.max(a, this._precisionOf(this.options.min))), a
		},
		_precisionOf: function (a) {
			var b = "" + a,
				c = b.indexOf(".");
			return -1 === c ? 0 : b.length - c - 1
		},
		_valueMin: function () {
			return this.options.min
		},
		_valueMax: function () {
			return this.max
		},
		_refreshRange: function (a) {
			"vertical" === a && this.range.css({
				width: "",
				left: ""
			}), "horizontal" === a && this.range.css({
				height: "",
				bottom: ""
			})
		},
		_refreshValue: function () {
			var b, c, d, e, f, g = this.options.range,
				h = this.options,
				i = this,
				j = !this._animateOff && h.animate,
				k = {};
			this._hasMultipleValues() ? this.handles.each(function (d) {
				c = (i.values(d) - i._valueMin()) / (i._valueMax() - i._valueMin()) * 100, k["horizontal" === i.orientation ? "left" : "bottom"] = c + "%", a(this).stop(1, 1)[j ? "animate" : "css"](k, h.animate), !0 === i.options.range && ("horizontal" === i.orientation ? (0 === d && i.range.stop(1, 1)[j ? "animate" : "css"]({
					left: c + "%"
				}, h.animate), 1 === d && i.range[j ? "animate" : "css"]({
					width: c - b + "%"
				}, {
					queue: !1,
					duration: h.animate
				})) : (0 === d && i.range.stop(1, 1)[j ? "animate" : "css"]({
					bottom: c + "%"
				}, h.animate), 1 === d && i.range[j ? "animate" : "css"]({
					height: c - b + "%"
				}, {
					queue: !1,
					duration: h.animate
				}))), b = c
			}) : (d = this.value(), e = this._valueMin(), f = this._valueMax(), c = f !== e ? (d - e) / (f - e) * 100 : 0, k["horizontal" === this.orientation ? "left" : "bottom"] = c + "%", this.handle.stop(1, 1)[j ? "animate" : "css"](k, h.animate), "min" === g && "horizontal" === this.orientation && this.range.stop(1, 1)[j ? "animate" : "css"]({
				width: c + "%"
			}, h.animate), "max" === g && "horizontal" === this.orientation && this.range.stop(1, 1)[j ? "animate" : "css"]({
				width: 100 - c + "%"
			}, h.animate), "min" === g && "vertical" === this.orientation && this.range.stop(1, 1)[j ? "animate" : "css"]({
				height: c + "%"
			}, h.animate), "max" === g && "vertical" === this.orientation && this.range.stop(1, 1)[j ? "animate" : "css"]({
				height: 100 - c + "%"
			}, h.animate))
		},
		_handleEvents: {
			keydown: function (b) {
				var c, d, e, f = a(b.target).data("ui-slider-handle-index");
				switch (b.keyCode) {
					case a.ui.keyCode.HOME:
					case a.ui.keyCode.END:
					case a.ui.keyCode.PAGE_UP:
					case a.ui.keyCode.PAGE_DOWN:
					case a.ui.keyCode.UP:
					case a.ui.keyCode.RIGHT:
					case a.ui.keyCode.DOWN:
					case a.ui.keyCode.LEFT:
						if (b.preventDefault(), !this._keySliding && (this._keySliding = !0, this._addClass(a(b.target), null, "ui-state-active"), !1 === this._start(b, f))) return
				}
				switch (e = this.options.step, c = d = this._hasMultipleValues() ? this.values(f) : this.value(), b.keyCode) {
					case a.ui.keyCode.HOME:
						d = this._valueMin();
						break;
					case a.ui.keyCode.END:
						d = this._valueMax();
						break;
					case a.ui.keyCode.PAGE_UP:
						d = this._trimAlignValue(c + (this._valueMax() - this._valueMin()) / this.numPages);
						break;
					case a.ui.keyCode.PAGE_DOWN:
						d = this._trimAlignValue(c - (this._valueMax() - this._valueMin()) / this.numPages);
						break;
					case a.ui.keyCode.UP:
					case a.ui.keyCode.RIGHT:
						if (c === this._valueMax()) return;
						d = this._trimAlignValue(c + e);
						break;
					case a.ui.keyCode.DOWN:
					case a.ui.keyCode.LEFT:
						if (c === this._valueMin()) return;
						d = this._trimAlignValue(c - e)
				}
				this._slide(b, f, d)
			},
			keyup: function (b) {
				var c = a(b.target).data("ui-slider-handle-index");
				this._keySliding && (this._keySliding = !1, this._stop(b, c), this._change(b, c), this._removeClass(a(b.target), null, "ui-state-active"))
			}
		}
	}), a.widget("ui.sortable", a.ui.mouse, {
		version: "1.12.1",
		widgetEventPrefix: "sort",
		ready: !1,
		options: {
			appendTo: "parent",
			axis: !1,
			connectWith: !1,
			containment: !1,
			cursor: "auto",
			cursorAt: !1,
			dropOnEmpty: !0,
			forcePlaceholderSize: !1,
			forceHelperSize: !1,
			grid: !1,
			handle: !1,
			helper: "original",
			items: "> *",
			opacity: !1,
			placeholder: !1,
			revert: !1,
			scroll: !0,
			scrollSensitivity: 20,
			scrollSpeed: 20,
			scope: "default",
			tolerance: "intersect",
			zIndex: 1e3,
			activate: null,
			beforeStop: null,
			change: null,
			deactivate: null,
			out: null,
			over: null,
			receive: null,
			remove: null,
			sort: null,
			start: null,
			stop: null,
			update: null
		},
		_isOverAxis: function (a, b, c) {
			return a >= b && b + c > a
		},
		_isFloating: function (a) {
			return /left|right/.test(a.css("float")) || /inline|table-cell/.test(a.css("display"))
		},
		_create: function () {
			this.containerCache = {}, this._addClass("ui-sortable"), this.refresh(), this.offset = this.element.offset(), this._mouseInit(), this._setHandleClassName(), this.ready = !0
		},
		_setOption: function (a, b) {
			this._super(a, b), "handle" === a && this._setHandleClassName()
		},
		_setHandleClassName: function () {
			var b = this;
			this._removeClass(this.element.find(".ui-sortable-handle"), "ui-sortable-handle"), a.each(this.items, function () {
				b._addClass(this.instance.options.handle ? this.item.find(this.instance.options.handle) : this.item, "ui-sortable-handle")
			})
		},
		_destroy: function () {
			this._mouseDestroy();
			for (var a = this.items.length - 1; a >= 0; a--) this.items[a].item.removeData(this.widgetName + "-item");
			return this
		},
		_mouseCapture: function (b, c) {
			var d = null,
				e = !1,
				f = this;
			return !this.reverting && (!this.options.disabled && "static" !== this.options.type && (this._refreshItems(b),
				a(b.target).parents().each(function () {
					return a.data(this, f.widgetName + "-item") === f ? (d = a(this), !1) : void 0
				}), a.data(b.target, f.widgetName + "-item") === f && (d = a(b.target)), !!d && (!(this.options.handle && !c && (a(this.options.handle, d).find("*").addBack().each(function () {
					this === b.target && (e = !0)
				}), !e)) && (this.currentItem = d, this._removeCurrentsFromItems(), !0))))
		},
		_mouseStart: function (b, c, d) {
			var e, f, g = this.options;
			if (this.currentContainer = this, this.refreshPositions(), this.helper = this._createHelper(b), this._cacheHelperProportions(), this._cacheMargins(), this.scrollParent = this.helper.scrollParent(), this.offset = this.currentItem.offset(), this.offset = {
					top: this.offset.top - this.margins.top,
					left: this.offset.left - this.margins.left
				}, a.extend(this.offset, {
					click: {
						left: b.pageX - this.offset.left,
						top: b.pageY - this.offset.top
					},
					parent: this._getParentOffset(),
					relative: this._getRelativeOffset()
				}), this.helper.css("position", "absolute"), this.cssPosition = this.helper.css("position"), this.originalPosition = this._generatePosition(b), this.originalPageX = b.pageX, this.originalPageY = b.pageY, g.cursorAt && this._adjustOffsetFromHelper(g.cursorAt), this.domPosition = {
					prev: this.currentItem.prev()[0],
					parent: this.currentItem.parent()[0]
				}, this.helper[0] !== this.currentItem[0] && this.currentItem.hide(), this._createPlaceholder(), g.containment && this._setContainment(), g.cursor && "auto" !== g.cursor && (f = this.document.find("body"), this.storedCursor = f.css("cursor"), f.css("cursor", g.cursor), this.storedStylesheet = a("<style>*{ cursor: " + g.cursor + " !important; }</style>").appendTo(f)), g.opacity && (this.helper.css("opacity") && (this._storedOpacity = this.helper.css("opacity")), this.helper.css("opacity", g.opacity)), g.zIndex && (this.helper.css("zIndex") && (this._storedZIndex = this.helper.css("zIndex")), this.helper.css("zIndex", g.zIndex)), this.scrollParent[0] !== this.document[0] && "HTML" !== this.scrollParent[0].tagName && (this.overflowOffset = this.scrollParent.offset()), this._trigger("start", b, this._uiHash()), this._preserveHelperProportions || this._cacheHelperProportions(), !d)
				for (e = this.containers.length - 1; e >= 0; e--) this.containers[e]._trigger("activate", b, this._uiHash(this));
			return a.ui.ddmanager && (a.ui.ddmanager.current = this), a.ui.ddmanager && !g.dropBehaviour && a.ui.ddmanager.prepareOffsets(this, b), this.dragging = !0, this._addClass(this.helper, "ui-sortable-helper"), this._mouseDrag(b), !0
		},
		_mouseDrag: function (b) {
			var c, d, e, f, g = this.options,
				h = !1;
			for (this.position = this._generatePosition(b), this.positionAbs = this._convertPositionTo("absolute"), this.lastPositionAbs || (this.lastPositionAbs = this.positionAbs), this.options.scroll && (this.scrollParent[0] !== this.document[0] && "HTML" !== this.scrollParent[0].tagName ? (this.overflowOffset.top + this.scrollParent[0].offsetHeight - b.pageY < g.scrollSensitivity ? this.scrollParent[0].scrollTop = h = this.scrollParent[0].scrollTop + g.scrollSpeed : b.pageY - this.overflowOffset.top < g.scrollSensitivity && (this.scrollParent[0].scrollTop = h = this.scrollParent[0].scrollTop - g.scrollSpeed), this.overflowOffset.left + this.scrollParent[0].offsetWidth - b.pageX < g.scrollSensitivity ? this.scrollParent[0].scrollLeft = h = this.scrollParent[0].scrollLeft + g.scrollSpeed : b.pageX - this.overflowOffset.left < g.scrollSensitivity && (this.scrollParent[0].scrollLeft = h = this.scrollParent[0].scrollLeft - g.scrollSpeed)) : (b.pageY - this.document.scrollTop() < g.scrollSensitivity ? h = this.document.scrollTop(this.document.scrollTop() - g.scrollSpeed) : this.window.height() - (b.pageY - this.document.scrollTop()) < g.scrollSensitivity && (h = this.document.scrollTop(this.document.scrollTop() + g.scrollSpeed)), b.pageX - this.document.scrollLeft() < g.scrollSensitivity ? h = this.document.scrollLeft(this.document.scrollLeft() - g.scrollSpeed) : this.window.width() - (b.pageX - this.document.scrollLeft()) < g.scrollSensitivity && (h = this.document.scrollLeft(this.document.scrollLeft() + g.scrollSpeed))), !1 !== h && a.ui.ddmanager && !g.dropBehaviour && a.ui.ddmanager.prepareOffsets(this, b)), this.positionAbs = this._convertPositionTo("absolute"), this.options.axis && "y" === this.options.axis || (this.helper[0].style.left = this.position.left + "px"), this.options.axis && "x" === this.options.axis || (this.helper[0].style.top = this.position.top + "px"), c = this.items.length - 1; c >= 0; c--)
				if (d = this.items[c], e = d.item[0], (f = this._intersectsWithPointer(d)) && d.instance === this.currentContainer && e !== this.currentItem[0] && this.placeholder[1 === f ? "next" : "prev"]()[0] !== e && !a.contains(this.placeholder[0], e) && ("semi-dynamic" !== this.options.type || !a.contains(this.element[0], e))) {
					if (this.direction = 1 === f ? "down" : "up", "pointer" !== this.options.tolerance && !this._intersectsWithSides(d)) break;
					this._rearrange(b, d), this._trigger("change", b, this._uiHash());
					break
				}
			return this._contactContainers(b), a.ui.ddmanager && a.ui.ddmanager.drag(this, b), this._trigger("sort", b, this._uiHash()), this.lastPositionAbs = this.positionAbs, !1
		},
		_mouseStop: function (b, c) {
			if (b) {
				if (a.ui.ddmanager && !this.options.dropBehaviour && a.ui.ddmanager.drop(this, b), this.options.revert) {
					var d = this,
						e = this.placeholder.offset(),
						f = this.options.axis,
						g = {};
					f && "x" !== f || (g.left = e.left - this.offset.parent.left - this.margins.left + (this.offsetParent[0] === this.document[0].body ? 0 : this.offsetParent[0].scrollLeft)), f && "y" !== f || (g.top = e.top - this.offset.parent.top - this.margins.top + (this.offsetParent[0] === this.document[0].body ? 0 : this.offsetParent[0].scrollTop)), this.reverting = !0, a(this.helper).animate(g, parseInt(this.options.revert, 10) || 500, function () {
						d._clear(b)
					})
				} else this._clear(b, c);
				return !1
			}
		},
		cancel: function () {
			if (this.dragging) {
				this._mouseUp(new a.Event("mouseup", {
					target: null
				})), "original" === this.options.helper ? (this.currentItem.css(this._storedCSS), this._removeClass(this.currentItem, "ui-sortable-helper")) : this.currentItem.show();
				for (var b = this.containers.length - 1; b >= 0; b--) this.containers[b]._trigger("deactivate", null, this._uiHash(this)), this.containers[b].containerCache.over && (this.containers[b]._trigger("out", null, this._uiHash(this)), this.containers[b].containerCache.over = 0)
			}
			return this.placeholder && (this.placeholder[0].parentNode && this.placeholder[0].parentNode.removeChild(this.placeholder[0]), "original" !== this.options.helper && this.helper && this.helper[0].parentNode && this.helper.remove(), a.extend(this, {
				helper: null,
				dragging: !1,
				reverting: !1,
				_noFinalSort: null
			}), this.domPosition.prev ? a(this.domPosition.prev).after(this.currentItem) : a(this.domPosition.parent).prepend(this.currentItem)), this
		},
		serialize: function (b) {
			var c = this._getItemsAsjQuery(b && b.connected),
				d = [];
			return b = b || {}, a(c).each(function () {
				var c = (a(b.item || this).attr(b.attribute || "id") || "").match(b.expression || /(.+)[\-=_](.+)/);
				c && d.push((b.key || c[1] + "[]") + "=" + (b.key && b.expression ? c[1] : c[2]))
			}), !d.length && b.key && d.push(b.key + "="), d.join("&")
		},
		toArray: function (b) {
			var c = this._getItemsAsjQuery(b && b.connected),
				d = [];
			return b = b || {}, c.each(function () {
				d.push(a(b.item || this).attr(b.attribute || "id") || "")
			}), d
		},
		_intersectsWith: function (a) {
			var b = this.positionAbs.left,
				c = b + this.helperProportions.width,
				d = this.positionAbs.top,
				e = d + this.helperProportions.height,
				f = a.left,
				g = f + a.width,
				h = a.top,
				i = h + a.height,
				j = this.offset.click.top,
				k = this.offset.click.left,
				l = "x" === this.options.axis || d + j > h && i > d + j,
				m = "y" === this.options.axis || b + k > f && g > b + k,
				n = l && m;
			return "pointer" === this.options.tolerance || this.options.forcePointerForContainers || "pointer" !== this.options.tolerance && this.helperProportions[this.floating ? "width" : "height"] > a[this.floating ? "width" : "height"] ? n : b + this.helperProportions.width / 2 > f && g > c - this.helperProportions.width / 2 && d + this.helperProportions.height / 2 > h && i > e - this.helperProportions.height / 2
		},
		_intersectsWithPointer: function (a) {
			var b, c, d = "x" === this.options.axis || this._isOverAxis(this.positionAbs.top + this.offset.click.top, a.top, a.height),
				e = "y" === this.options.axis || this._isOverAxis(this.positionAbs.left + this.offset.click.left, a.left, a.width);
			return !(!d || !e) && (b = this._getDragVerticalDirection(), c = this._getDragHorizontalDirection(), this.floating ? "right" === c || "down" === b ? 2 : 1 : b && ("down" === b ? 2 : 1))
		},
		_intersectsWithSides: function (a) {
			var b = this._isOverAxis(this.positionAbs.top + this.offset.click.top, a.top + a.height / 2, a.height),
				c = this._isOverAxis(this.positionAbs.left + this.offset.click.left, a.left + a.width / 2, a.width),
				d = this._getDragVerticalDirection(),
				e = this._getDragHorizontalDirection();
			return this.floating && e ? "right" === e && c || "left" === e && !c : d && ("down" === d && b || "up" === d && !b)
		},
		_getDragVerticalDirection: function () {
			var a = this.positionAbs.top - this.lastPositionAbs.top;
			return 0 !== a && (a > 0 ? "down" : "up")
		},
		_getDragHorizontalDirection: function () {
			var a = this.positionAbs.left - this.lastPositionAbs.left;
			return 0 !== a && (a > 0 ? "right" : "left")
		},
		refresh: function (a) {
			return this._refreshItems(a), this._setHandleClassName(), this.refreshPositions(), this
		},
		_connectWith: function () {
			var a = this.options;
			return a.connectWith.constructor === String ? [a.connectWith] : a.connectWith
		},
		_getItemsAsjQuery: function (b) {
			function c() {
				h.push(this)
			}
			var d, e, f, g, h = [],
				i = [],
				j = this._connectWith();
			if (j && b)
				for (d = j.length - 1; d >= 0; d--)
					for (f = a(j[d], this.document[0]), e = f.length - 1; e >= 0; e--)(g = a.data(f[e], this.widgetFullName)) && g !== this && !g.options.disabled && i.push([a.isFunction(g.options.items) ? g.options.items.call(g.element) : a(g.options.items, g.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), g]);
			for (i.push([a.isFunction(this.options.items) ? this.options.items.call(this.element, null, {
					options: this.options,
					item: this.currentItem
				}) : a(this.options.items, this.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), this]), d = i.length - 1; d >= 0; d--) i[d][0].each(c);
			return a(h)
		},
		_removeCurrentsFromItems: function () {
			var b = this.currentItem.find(":data(" + this.widgetName + "-item)");
			this.items = a.grep(this.items, function (a) {
				for (var c = 0; b.length > c; c++)
					if (b[c] === a.item[0]) return !1;
				return !0
			})
		},
		_refreshItems: function (b) {
			this.items = [], this.containers = [this];
			var c, d, e, f, g, h, i, j, k = this.items,
				l = [
					[a.isFunction(this.options.items) ? this.options.items.call(this.element[0], b, {
						item: this.currentItem
					}) : a(this.options.items, this.element), this]
				],
				m = this._connectWith();
			if (m && this.ready)
				for (c = m.length - 1; c >= 0; c--)
					for (e = a(m[c], this.document[0]), d = e.length - 1; d >= 0; d--)(f = a.data(e[d], this.widgetFullName)) && f !== this && !f.options.disabled && (l.push([a.isFunction(f.options.items) ? f.options.items.call(f.element[0], b, {
						item: this.currentItem
					}) : a(f.options.items, f.element), f]), this.containers.push(f));
			for (c = l.length - 1; c >= 0; c--)
				for (g = l[c][1], h = l[c][0], d = 0, j = h.length; j > d; d++) i = a(h[d]), i.data(this.widgetName + "-item", g), k.push({
					item: i,
					instance: g,
					width: 0,
					height: 0,
					left: 0,
					top: 0
				})
		},
		refreshPositions: function (b) {
			this.floating = !!this.items.length && ("x" === this.options.axis || this._isFloating(this.items[0].item)), this.offsetParent && this.helper && (this.offset.parent = this._getParentOffset());
			var c, d, e, f;
			for (c = this.items.length - 1; c >= 0; c--) d = this.items[c], d.instance !== this.currentContainer && this.currentContainer && d.item[0] !== this.currentItem[0] || (e = this.options.toleranceElement ? a(this.options.toleranceElement, d.item) : d.item, b || (d.width = e.outerWidth(), d.height = e.outerHeight()), f = e.offset(), d.left = f.left, d.top = f.top);
			if (this.options.custom && this.options.custom.refreshContainers) this.options.custom.refreshContainers.call(this);
			else
				for (c = this.containers.length - 1; c >= 0; c--) f = this.containers[c].element.offset(), this.containers[c].containerCache.left = f.left, this.containers[c].containerCache.top = f.top, this.containers[c].containerCache.width = this.containers[c].element.outerWidth(), this.containers[c].containerCache.height = this.containers[c].element.outerHeight();
			return this
		},
		_createPlaceholder: function (b) {
			b = b || this;
			var c, d = b.options;
			d.placeholder && d.placeholder.constructor !== String || (c = d.placeholder, d.placeholder = {
				element: function () {
					var d = b.currentItem[0].nodeName.toLowerCase(),
						e = a("<" + d + ">", b.document[0]);
					return b._addClass(e, "ui-sortable-placeholder", c || b.currentItem[0].className)._removeClass(e, "ui-sortable-helper"), "tbody" === d ? b._createTrPlaceholder(b.currentItem.find("tr").eq(0), a("<tr>", b.document[0]).appendTo(e)) : "tr" === d ? b._createTrPlaceholder(b.currentItem, e) : "img" === d && e.attr("src", b.currentItem.attr("src")), c || e.css("visibility", "hidden"), e
				},
				update: function (a, e) {
					(!c || d.forcePlaceholderSize) && (e.height() || e.height(b.currentItem.innerHeight() - parseInt(b.currentItem.css("paddingTop") || 0, 10) - parseInt(b.currentItem.css("paddingBottom") || 0, 10)), e.width() || e.width(b.currentItem.innerWidth() - parseInt(b.currentItem.css("paddingLeft") || 0, 10) - parseInt(b.currentItem.css("paddingRight") || 0, 10)))
				}
			}), b.placeholder = a(d.placeholder.element.call(b.element, b.currentItem)), b.currentItem.after(b.placeholder), d.placeholder.update(b, b.placeholder)
		},
		_createTrPlaceholder: function (b, c) {
			var d = this;
			b.children().each(function () {
				a("<td>&#160;</td>", d.document[0]).attr("colspan", a(this).attr("colspan") || 1).appendTo(c)
			})
		},
		_contactContainers: function (b) {
			var c, d, e, f, g, h, i, j, k, l, m = null,
				n = null;
			for (c = this.containers.length - 1; c >= 0; c--)
				if (!a.contains(this.currentItem[0], this.containers[c].element[0]))
					if (this._intersectsWith(this.containers[c].containerCache)) {
						if (m && a.contains(this.containers[c].element[0], m.element[0])) continue;
						m = this.containers[c], n = c
					} else this.containers[c].containerCache.over && (this.containers[c]._trigger("out", b, this._uiHash(this)), this.containers[c].containerCache.over = 0);
			if (m)
				if (1 === this.containers.length) this.containers[n].containerCache.over || (this.containers[n]._trigger("over", b, this._uiHash(this)), this.containers[n].containerCache.over = 1);
				else {
					for (e = 1e4, f = null, k = m.floating || this._isFloating(this.currentItem), g = k ? "left" : "top", h = k ? "width" : "height", l = k ? "pageX" : "pageY", d = this.items.length - 1; d >= 0; d--) a.contains(this.containers[n].element[0], this.items[d].item[0]) && this.items[d].item[0] !== this.currentItem[0] && (i = this.items[d].item.offset()[g], j = !1, b[l] - i > this.items[d][h] / 2 && (j = !0), e > Math.abs(b[l] - i) && (e = Math.abs(b[l] - i), f = this.items[d], this.direction = j ? "up" : "down"));
					if (!f && !this.options.dropOnEmpty) return;
					if (this.currentContainer === this.containers[n]) return void(this.currentContainer.containerCache.over || (this.containers[n]._trigger("over", b, this._uiHash()), this.currentContainer.containerCache.over = 1));
					f ? this._rearrange(b, f, null, !0) : this._rearrange(b, null, this.containers[n].element, !0), this._trigger("change", b, this._uiHash()), this.containers[n]._trigger("change", b, this._uiHash(this)), this.currentContainer = this.containers[n], this.options.placeholder.update(this.currentContainer, this.placeholder), this.containers[n]._trigger("over", b, this._uiHash(this)), this.containers[n].containerCache.over = 1
				}
		},
		_createHelper: function (b) {
			var c = this.options,
				d = a.isFunction(c.helper) ? a(c.helper.apply(this.element[0], [b, this.currentItem])) : "clone" === c.helper ? this.currentItem.clone() : this.currentItem;
			return d.parents("body").length || a("parent" !== c.appendTo ? c.appendTo : this.currentItem[0].parentNode)[0].appendChild(d[0]), d[0] === this.currentItem[0] && (this._storedCSS = {
				width: this.currentItem[0].style.width,
				height: this.currentItem[0].style.height,
				position: this.currentItem.css("position"),
				top: this.currentItem.css("top"),
				left: this.currentItem.css("left")
			}), (!d[0].style.width || c.forceHelperSize) && d.width(this.currentItem.width()), (!d[0].style.height || c.forceHelperSize) && d.height(this.currentItem.height()), d
		},
		_adjustOffsetFromHelper: function (b) {
			"string" == typeof b && (b = b.split(" ")), a.isArray(b) && (b = {
				left: +b[0],
				top: +b[1] || 0
			}), "left" in b && (this.offset.click.left = b.left + this.margins.left), "right" in b && (this.offset.click.left = this.helperProportions.width - b.right + this.margins.left), "top" in b && (this.offset.click.top = b.top + this.margins.top), "bottom" in b && (this.offset.click.top = this.helperProportions.height - b.bottom + this.margins.top)
		},
		_getParentOffset: function () {
			this.offsetParent = this.helper.offsetParent();
			var b = this.offsetParent.offset();
			return "absolute" === this.cssPosition && this.scrollParent[0] !== this.document[0] && a.contains(this.scrollParent[0], this.offsetParent[0]) && (b.left += this.scrollParent.scrollLeft(), b.top += this.scrollParent.scrollTop()), (this.offsetParent[0] === this.document[0].body || this.offsetParent[0].tagName && "html" === this.offsetParent[0].tagName.toLowerCase() && a.ui.ie) && (b = {
				top: 0,
				left: 0
			}), {
				top: b.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0),
				left: b.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)
			}
		},
		_getRelativeOffset: function () {
			if ("relative" === this.cssPosition) {
				var a = this.currentItem.position();
				return {
					top: a.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(),
					left: a.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft()
				}
			}
			return {
				top: 0,
				left: 0
			}
		},
		_cacheMargins: function () {
			this.margins = {
				left: parseInt(this.currentItem.css("marginLeft"), 10) || 0,
				top: parseInt(this.currentItem.css("marginTop"), 10) || 0
			}
		},
		_cacheHelperProportions: function () {
			this.helperProportions = {
				width: this.helper.outerWidth(),
				height: this.helper.outerHeight()
			}
		},
		_setContainment: function () {
			var b, c, d, e = this.options;
			"parent" === e.containment && (e.containment = this.helper[0].parentNode), ("document" === e.containment || "window" === e.containment) && (this.containment = [0 - this.offset.relative.left - this.offset.parent.left, 0 - this.offset.relative.top - this.offset.parent.top, "document" === e.containment ? this.document.width() : this.window.width() - this.helperProportions.width - this.margins.left, ("document" === e.containment ? this.document.height() || document.body.parentNode.scrollHeight : this.window.height() || this.document[0].body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]), /^(document|window|parent)$/.test(e.containment) || (b = a(e.containment)[0], c = a(e.containment).offset(), d = "hidden" !== a(b).css("overflow"), this.containment = [c.left + (parseInt(a(b).css("borderLeftWidth"), 10) || 0) + (parseInt(a(b).css("paddingLeft"), 10) || 0) - this.margins.left, c.top + (parseInt(a(b).css("borderTopWidth"), 10) || 0) + (parseInt(a(b).css("paddingTop"), 10) || 0) - this.margins.top, c.left + (d ? Math.max(b.scrollWidth, b.offsetWidth) : b.offsetWidth) - (parseInt(a(b).css("borderLeftWidth"), 10) || 0) - (parseInt(a(b).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left, c.top + (d ? Math.max(b.scrollHeight, b.offsetHeight) : b.offsetHeight) - (parseInt(a(b).css("borderTopWidth"), 10) || 0) - (parseInt(a(b).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top])
		},
		_convertPositionTo: function (b, c) {
			c || (c = this.position);
			var d = "absolute" === b ? 1 : -1,
				e = "absolute" !== this.cssPosition || this.scrollParent[0] !== this.document[0] && a.contains(this.scrollParent[0], this.offsetParent[0]) ? this.scrollParent : this.offsetParent,
				f = /(html|body)/i.test(e[0].tagName);
			return {
				top: c.top + this.offset.relative.top * d + this.offset.parent.top * d - ("fixed" === this.cssPosition ? -this.scrollParent.scrollTop() : f ? 0 : e.scrollTop()) * d,
				left: c.left + this.offset.relative.left * d + this.offset.parent.left * d - ("fixed" === this.cssPosition ? -this.scrollParent.scrollLeft() : f ? 0 : e.scrollLeft()) * d
			}
		},
		_generatePosition: function (b) {
			var c, d, e = this.options,
				f = b.pageX,
				g = b.pageY,
				h = "absolute" !== this.cssPosition || this.scrollParent[0] !== this.document[0] && a.contains(this.scrollParent[0], this.offsetParent[0]) ? this.scrollParent : this.offsetParent,
				i = /(html|body)/i.test(h[0].tagName);
			return "relative" !== this.cssPosition || this.scrollParent[0] !== this.document[0] && this.scrollParent[0] !== this.offsetParent[0] || (this.offset.relative = this._getRelativeOffset()), this.originalPosition && (this.containment && (b.pageX - this.offset.click.left < this.containment[0] && (f = this.containment[0] + this.offset.click.left), b.pageY - this.offset.click.top < this.containment[1] && (g = this.containment[1] + this.offset.click.top), b.pageX - this.offset.click.left > this.containment[2] && (f = this.containment[2] + this.offset.click.left), b.pageY - this.offset.click.top > this.containment[3] && (g = this.containment[3] + this.offset.click.top)), e.grid && (c = this.originalPageY + Math.round((g - this.originalPageY) / e.grid[1]) * e.grid[1], g = this.containment ? c - this.offset.click.top >= this.containment[1] && c - this.offset.click.top <= this.containment[3] ? c : c - this.offset.click.top >= this.containment[1] ? c - e.grid[1] : c + e.grid[1] : c, d = this.originalPageX + Math.round((f - this.originalPageX) / e.grid[0]) * e.grid[0], f = this.containment ? d - this.offset.click.left >= this.containment[0] && d - this.offset.click.left <= this.containment[2] ? d : d - this.offset.click.left >= this.containment[0] ? d - e.grid[0] : d + e.grid[0] : d)), {
				top: g - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + ("fixed" === this.cssPosition ? -this.scrollParent.scrollTop() : i ? 0 : h.scrollTop()),
				left: f - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + ("fixed" === this.cssPosition ? -this.scrollParent.scrollLeft() : i ? 0 : h.scrollLeft())
			}
		},
		_rearrange: function (a, b, c, d) {
			c ? c[0].appendChild(this.placeholder[0]) : b.item[0].parentNode.insertBefore(this.placeholder[0], "down" === this.direction ? b.item[0] : b.item[0].nextSibling), this.counter = this.counter ? ++this.counter : 1;
			var e = this.counter;
			this._delay(function () {
				e === this.counter && this.refreshPositions(!d)
			})
		},
		_clear: function (a, b) {
			function c(a, b, c) {
				return function (d) {
					c._trigger(a, d, b._uiHash(b))
				}
			}
			this.reverting = !1;
			var d, e = [];
			if (!this._noFinalSort && this.currentItem.parent().length && this.placeholder.before(this.currentItem), this._noFinalSort = null, this.helper[0] === this.currentItem[0]) {
				for (d in this._storedCSS)("auto" === this._storedCSS[d] || "static" === this._storedCSS[d]) && (this._storedCSS[d] = "");
				this.currentItem.css(this._storedCSS), this._removeClass(this.currentItem, "ui-sortable-helper")
			} else this.currentItem.show();
			for (this.fromOutside && !b && e.push(function (a) {
					this._trigger("receive", a, this._uiHash(this.fromOutside))
				}), !this.fromOutside && this.domPosition.prev === this.currentItem.prev().not(".ui-sortable-helper")[0] && this.domPosition.parent === this.currentItem.parent()[0] || b || e.push(function (a) {
					this._trigger("update", a, this._uiHash())
				}), this !== this.currentContainer && (b || (e.push(function (a) {
					this._trigger("remove", a, this._uiHash())
				}), e.push(function (a) {
					return function (b) {
						a._trigger("receive", b, this._uiHash(this))
					}
				}.call(this, this.currentContainer)), e.push(function (a) {
					return function (b) {
						a._trigger("update", b, this._uiHash(this))
					}
				}.call(this, this.currentContainer)))), d = this.containers.length - 1; d >= 0; d--) b || e.push(c("deactivate", this, this.containers[d])), this.containers[d].containerCache.over && (e.push(c("out", this, this.containers[d])), this.containers[d].containerCache.over = 0);
			if (this.storedCursor && (this.document.find("body").css("cursor", this.storedCursor), this.storedStylesheet.remove()), this._storedOpacity && this.helper.css("opacity", this._storedOpacity), this._storedZIndex && this.helper.css("zIndex", "auto" === this._storedZIndex ? "" : this._storedZIndex), this.dragging = !1, b || this._trigger("beforeStop", a, this._uiHash()), this.placeholder[0].parentNode.removeChild(this.placeholder[0]), this.cancelHelperRemoval || (this.helper[0] !== this.currentItem[0] && this.helper.remove(), this.helper = null), !b) {
				for (d = 0; e.length > d; d++) e[d].call(this, a);
				this._trigger("stop", a, this._uiHash())
			}
			return this.fromOutside = !1, !this.cancelHelperRemoval
		},
		_trigger: function () {
			!1 === a.Widget.prototype._trigger.apply(this, arguments) && this.cancel()
		},
		_uiHash: function (b) {
			var c = b || this;
			return {
				helper: c.helper,
				placeholder: c.placeholder || a([]),
				position: c.position,
				originalPosition: c.originalPosition,
				offset: c.positionAbs,
				item: c.currentItem,
				sender: b ? b.element : null
			}
		}
	}), a.widget("ui.spinner", {
		version: "1.12.1",
		defaultElement: "<input>",
		widgetEventPrefix: "spin",
		options: {
			classes: {
				"ui-spinner": "ui-corner-all",
				"ui-spinner-down": "ui-corner-br",
				"ui-spinner-up": "ui-corner-tr"
			},
			culture: null,
			icons: {
				down: "ui-icon-triangle-1-s",
				up: "ui-icon-triangle-1-n"
			},
			incremental: !0,
			max: null,
			min: null,
			numberFormat: null,
			page: 10,
			step: 1,
			change: null,
			spin: null,
			start: null,
			stop: null
		},
		_create: function () {
			this._setOption("max", this.options.max), this._setOption("min", this.options.min), this._setOption("step", this.options.step), "" !== this.value() && this._value(this.element.val(), !0), this._draw(), this._on(this._events), this._refresh(), this._on(this.window, {
				beforeunload: function () {
					this.element.removeAttr("autocomplete")
				}
			})
		},
		_getCreateOptions: function () {
			var b = this._super(),
				c = this.element;
			return a.each(["min", "max", "step"], function (a, d) {
				var e = c.attr(d);
				null != e && e.length && (b[d] = e)
			}), b
		},
		_events: {
			keydown: function (a) {
				this._start(a) && this._keydown(a) && a.preventDefault()
			},
			keyup: "_stop",
			focus: function () {
				this.previous = this.element.val()
			},
			blur: function (a) {
				return this.cancelBlur ? void delete this.cancelBlur : (this._stop(), this._refresh(), void(this.previous !== this.element.val() && this._trigger("change", a)))
			},
			mousewheel: function (a, b) {
				if (b) {
					if (!this.spinning && !this._start(a)) return !1;
					this._spin((b > 0 ? 1 : -1) * this.options.step, a), clearTimeout(this.mousewheelTimer), this.mousewheelTimer = this._delay(function () {
						this.spinning && this._stop(a)
					}, 100), a.preventDefault()
				}
			},
			"mousedown .ui-spinner-button": function (b) {
				function c() {
					this.element[0] === a.ui.safeActiveElement(this.document[0]) || (this.element.trigger("focus"), this.previous = d, this._delay(function () {
						this.previous = d
					}))
				}
				var d;
				d = this.element[0] === a.ui.safeActiveElement(this.document[0]) ? this.previous : this.element.val(), b.preventDefault(), c.call(this), this.cancelBlur = !0, this._delay(function () {
					delete this.cancelBlur, c.call(this)
				}), !1 !== this._start(b) && this._repeat(null, a(b.currentTarget).hasClass("ui-spinner-up") ? 1 : -1, b)
			},
			"mouseup .ui-spinner-button": "_stop",
			"mouseenter .ui-spinner-button": function (b) {
				return a(b.currentTarget).hasClass("ui-state-active") ? !1 !== this._start(b) && void this._repeat(null, a(b.currentTarget).hasClass("ui-spinner-up") ? 1 : -1, b) : void 0
			},
			"mouseleave .ui-spinner-button": "_stop"
		},
		_enhance: function () {
			this.uiSpinner = this.element.attr("autocomplete", "off").wrap("<span>").parent().append("<a></a><a></a>")
		},
		_draw: function () {
			this._enhance(), this._addClass(this.uiSpinner, "ui-spinner", "ui-widget ui-widget-content"), this._addClass("ui-spinner-input"), this.element.attr("role", "spinbutton"), this.buttons = this.uiSpinner.children("a").attr("tabIndex", -1).attr("aria-hidden", !0).button({
				classes: {
					"ui-button": ""
				}
			}), this._removeClass(this.buttons, "ui-corner-all"), this._addClass(this.buttons.first(), "ui-spinner-button ui-spinner-up"), this._addClass(this.buttons.last(), "ui-spinner-button ui-spinner-down"), this.buttons.first().button({
				icon: this.options.icons.up,
				showLabel: !1
			}), this.buttons.last().button({
				icon: this.options.icons.down,
				showLabel: !1
			}), this.buttons.height() > Math.ceil(.5 * this.uiSpinner.height()) && this.uiSpinner.height() > 0 && this.uiSpinner.height(this.uiSpinner.height())
		},
		_keydown: function (b) {
			var c = this.options,
				d = a.ui.keyCode;
			switch (b.keyCode) {
				case d.UP:
					return this._repeat(null, 1, b), !0;
				case d.DOWN:
					return this._repeat(null, -1, b), !0;
				case d.PAGE_UP:
					return this._repeat(null, c.page, b), !0;
				case d.PAGE_DOWN:
					return this._repeat(null, -c.page, b), !0
			}
			return !1
		},
		_start: function (a) {
			return !(!this.spinning && !1 === this._trigger("start", a)) && (this.counter || (this.counter = 1), this.spinning = !0, !0)
		},
		_repeat: function (a, b, c) {
			a = a || 500, clearTimeout(this.timer), this.timer = this._delay(function () {
				this._repeat(40, b, c)
			}, a), this._spin(b * this.options.step, c)
		},
		_spin: function (a, b) {
			var c = this.value() || 0;
			this.counter || (this.counter = 1), c = this._adjustValue(c + a * this._increment(this.counter)), this.spinning && !1 === this._trigger("spin", b, {
				value: c
			}) || (this._value(c), this.counter++)
		},
		_increment: function (b) {
			var c = this.options.incremental;
			return c ? a.isFunction(c) ? c(b) : Math.floor(b * b * b / 5e4 - b * b / 500 + 17 * b / 200 + 1) : 1
		},
		_precision: function () {
			var a = this._precisionOf(this.options.step);
			return null !== this.options.min && (a = Math.max(a, this._precisionOf(this.options.min))), a
		},
		_precisionOf: function (a) {
			var b = "" + a,
				c = b.indexOf(".");
			return -1 === c ? 0 : b.length - c - 1
		},
		_adjustValue: function (a) {
			var b, c, d = this.options;
			return b = null !== d.min ? d.min : 0, c = a - b, c = Math.round(c / d.step) * d.step, a = b + c, a = parseFloat(a.toFixed(this._precision())), null !== d.max && a > d.max ? d.max : null !== d.min && d.min > a ? d.min : a
		},
		_stop: function (a) {
			this.spinning && (clearTimeout(this.timer), clearTimeout(this.mousewheelTimer), this.counter = 0, this.spinning = !1, this._trigger("stop", a))
		},
		_setOption: function (a, b) {
			var c, d, e;
			return "culture" === a || "numberFormat" === a ? (c = this._parse(this.element.val()), this.options[a] = b, void this.element.val(this._format(c))) : (("max" === a || "min" === a || "step" === a) && "string" == typeof b && (b = this._parse(b)), "icons" === a && (d = this.buttons.first().find(".ui-icon"), this._removeClass(d, null, this.options.icons.up), this._addClass(d, null, b.up), e = this.buttons.last().find(".ui-icon"), this._removeClass(e, null, this.options.icons.down), this._addClass(e, null, b.down)), void this._super(a, b))
		},
		_setOptionDisabled: function (a) {
			this._super(a), this._toggleClass(this.uiSpinner, null, "ui-state-disabled", !!a), this.element.prop("disabled", !!a), this.buttons.button(a ? "disable" : "enable")
		},
		_setOptions: h(function (a) {
			this._super(a)
		}),
		_parse: function (a) {
			return "string" == typeof a && "" !== a && (a = window.Globalize && this.options.numberFormat ? Globalize.parseFloat(a, 10, this.options.culture) : +a), "" === a || isNaN(a) ? null : a
		},
		_format: function (a) {
			return "" === a ? "" : window.Globalize && this.options.numberFormat ? Globalize.format(a, this.options.numberFormat, this.options.culture) : a
		},
		_refresh: function () {
			this.element.attr({
				"aria-valuemin": this.options.min,
				"aria-valuemax": this.options.max,
				"aria-valuenow": this._parse(this.element.val())
			})
		},
		isValid: function () {
			var a = this.value();
			return null !== a && a === this._adjustValue(a)
		},
		_value: function (a, b) {
			var c;
			"" !== a && null !== (c = this._parse(a)) && (b || (c = this._adjustValue(c)), a = this._format(c)), this.element.val(a), this._refresh()
		},
		_destroy: function () {
			this.element.prop("disabled", !1).removeAttr("autocomplete role aria-valuemin aria-valuemax aria-valuenow"), this.uiSpinner.replaceWith(this.element)
		},
		stepUp: h(function (a) {
			this._stepUp(a)
		}),
		_stepUp: function (a) {
			this._start() && (this._spin((a || 1) * this.options.step), this._stop())
		},
		stepDown: h(function (a) {
			this._stepDown(a)
		}),
		_stepDown: function (a) {
			this._start() && (this._spin((a || 1) * -this.options.step), this._stop())
		},
		pageUp: h(function (a) {
			this._stepUp((a || 1) * this.options.page)
		}),
		pageDown: h(function (a) {
			this._stepDown((a || 1) * this.options.page)
		}),
		value: function (a) {
			return arguments.length ? void h(this._value).call(this, a) : this._parse(this.element.val())
		},
		widget: function () {
			return this.uiSpinner
		}
	}), !1 !== a.uiBackCompat && a.widget("ui.spinner", a.ui.spinner, {
		_enhance: function () {
			this.uiSpinner = this.element.attr("autocomplete", "off").wrap(this._uiSpinnerHtml()).parent().append(this._buttonHtml())
		},
		_uiSpinnerHtml: function () {
			return "<span>"
		},
		_buttonHtml: function () {
			return "<a></a><a></a>"
		}
	}), a.ui.spinner, a.widget("ui.tabs", {
		version: "1.12.1",
		delay: 300,
		options: {
			active: null,
			classes: {
				"ui-tabs": "ui-corner-all",
				"ui-tabs-nav": "ui-corner-all",
				"ui-tabs-panel": "ui-corner-bottom",
				"ui-tabs-tab": "ui-corner-top"
			},
			collapsible: !1,
			event: "click",
			heightStyle: "content",
			hide: null,
			show: null,
			activate: null,
			beforeActivate: null,
			beforeLoad: null,
			load: null
		},
		_isLocal: function () {
			var a = /#.*$/;
			return function (b) {
				var c, d;
				c = b.href.replace(a, ""), d = location.href.replace(a, "");
				try {
					c = decodeURIComponent(c)
				} catch (a) {}
				try {
					d = decodeURIComponent(d)
				} catch (a) {}
				return b.hash.length > 1 && c === d
			}
		}(),
		_create: function () {
			var b = this,
				c = this.options;
			this.running = !1, this._addClass("ui-tabs", "ui-widget ui-widget-content"), this._toggleClass("ui-tabs-collapsible", null, c.collapsible), this._processTabs(), c.active = this._initialActive(), a.isArray(c.disabled) && (c.disabled = a.unique(c.disabled.concat(a.map(this.tabs.filter(".ui-state-disabled"), function (a) {
				return b.tabs.index(a)
			}))).sort()), this.active = !1 !== this.options.active && this.anchors.length ? this._findActive(c.active) : a(), this._refresh(), this.active.length && this.load(c.active)
		},
		_initialActive: function () {
			var b = this.options.active,
				c = this.options.collapsible,
				d = location.hash.substring(1);
			return null === b && (d && this.tabs.each(function (c, e) {
				return a(e).attr("aria-controls") === d ? (b = c, !1) : void 0
			}), null === b && (b = this.tabs.index(this.tabs.filter(".ui-tabs-active"))), (null === b || -1 === b) && (b = !!this.tabs.length && 0)), !1 !== b && -1 === (b = this.tabs.index(this.tabs.eq(b))) && (b = !c && 0), !c && !1 === b && this.anchors.length && (b = 0), b
		},
		_getCreateEventData: function () {
			return {
				tab: this.active,
				panel: this.active.length ? this._getPanelForTab(this.active) : a()
			}
		},
		_tabKeydown: function (b) {
			var c = a(a.ui.safeActiveElement(this.document[0])).closest("li"),
				d = this.tabs.index(c),
				e = !0;
			if (!this._handlePageNav(b)) {
				switch (b.keyCode) {
					case a.ui.keyCode.RIGHT:
					case a.ui.keyCode.DOWN:
						d++;
						break;
					case a.ui.keyCode.UP:
					case a.ui.keyCode.LEFT:
						e = !1, d--;
						break;
					case a.ui.keyCode.END:
						d = this.anchors.length - 1;
						break;
					case a.ui.keyCode.HOME:
						d = 0;
						break;
					case a.ui.keyCode.SPACE:
						return b.preventDefault(), clearTimeout(this.activating), void this._activate(d);
					case a.ui.keyCode.ENTER:
						return b.preventDefault(), clearTimeout(this.activating), void this._activate(d !== this.options.active && d);
					default:
						return
				}
				b.preventDefault(), clearTimeout(this.activating), d = this._focusNextTab(d, e), b.ctrlKey || b.metaKey || (c.attr("aria-selected", "false"), this.tabs.eq(d).attr("aria-selected", "true"), this.activating = this._delay(function () {
					this.option("active", d)
				}, this.delay))
			}
		},
		_panelKeydown: function (b) {
			this._handlePageNav(b) || b.ctrlKey && b.keyCode === a.ui.keyCode.UP && (b.preventDefault(), this.active.trigger("focus"))
		},
		_handlePageNav: function (b) {
			return b.altKey && b.keyCode === a.ui.keyCode.PAGE_UP ? (this._activate(this._focusNextTab(this.options.active - 1, !1)), !0) : b.altKey && b.keyCode === a.ui.keyCode.PAGE_DOWN ? (this._activate(this._focusNextTab(this.options.active + 1, !0)), !0) : void 0
		},
		_findNextTab: function (b, c) {
			for (var d = this.tabs.length - 1; - 1 !== a.inArray(function () {
					return b > d && (b = 0), 0 > b && (b = d), b
				}(), this.options.disabled);) b = c ? b + 1 : b - 1;
			return b
		},
		_focusNextTab: function (a, b) {
			return a = this._findNextTab(a, b), this.tabs.eq(a).trigger("focus"), a
		},
		_setOption: function (a, b) {
			return "active" === a ? void this._activate(b) : (this._super(a, b), "collapsible" === a && (this._toggleClass("ui-tabs-collapsible", null, b), b || !1 !== this.options.active || this._activate(0)), "event" === a && this._setupEvents(b), void("heightStyle" === a && this._setupHeightStyle(b)))
		},
		_sanitizeSelector: function (a) {
			return a ? a.replace(/[!"$%&'()*+,.\/:;<=>?@\[\]\^`{|}~]/g, "\\$&") : ""
		},
		refresh: function () {
			var b = this.options,
				c = this.tablist.children(":has(a[href])");
			b.disabled = a.map(c.filter(".ui-state-disabled"), function (a) {
				return c.index(a)
			}), this._processTabs(), !1 !== b.active && this.anchors.length ? this.active.length && !a.contains(this.tablist[0], this.active[0]) ? this.tabs.length === b.disabled.length ? (b.active = !1, this.active = a()) : this._activate(this._findNextTab(Math.max(0, b.active - 1), !1)) : b.active = this.tabs.index(this.active) : (b.active = !1, this.active = a()), this._refresh()
		},
		_refresh: function () {
			this._setOptionDisabled(this.options.disabled), this._setupEvents(this.options.event), this._setupHeightStyle(this.options.heightStyle), this.tabs.not(this.active).attr({
				"aria-selected": "false",
				"aria-expanded": "false",
				tabIndex: -1
			}), this.panels.not(this._getPanelForTab(this.active)).hide().attr({
				"aria-hidden": "true"
			}), this.active.length ? (this.active.attr({
				"aria-selected": "true",
				"aria-expanded": "true",
				tabIndex: 0
			}), this._addClass(this.active, "ui-tabs-active", "ui-state-active"), this._getPanelForTab(this.active).show().attr({
				"aria-hidden": "false"
			})) : this.tabs.eq(0).attr("tabIndex", 0)
		},
		_processTabs: function () {
			var b = this,
				c = this.tabs,
				d = this.anchors,
				e = this.panels;
			this.tablist = this._getList().attr("role", "tablist"), this._addClass(this.tablist, "ui-tabs-nav", "ui-helper-reset ui-helper-clearfix ui-widget-header"), this.tablist.on("mousedown" + this.eventNamespace, "> li", function (b) {
				a(this).is(".ui-state-disabled") && b.preventDefault()
			}).on("focus" + this.eventNamespace, ".ui-tabs-anchor", function () {
				a(this).closest("li").is(".ui-state-disabled") && this.blur()
			}), this.tabs = this.tablist.find("> li:has(a[href])").attr({
				role: "tab",
				tabIndex: -1
			}), this._addClass(this.tabs, "ui-tabs-tab", "ui-state-default"), this.anchors = this.tabs.map(function () {
				return a("a", this)[0]
			}).attr({
				role: "presentation",
				tabIndex: -1
			}), this._addClass(this.anchors, "ui-tabs-anchor"), this.panels = a(), this.anchors.each(function (c, d) {
				var e, f, g, h = a(d).uniqueId().attr("id"),
					i = a(d).closest("li"),
					j = i.attr("aria-controls");
				b._isLocal(d) ? (e = d.hash, g = e.substring(1), f = b.element.find(b._sanitizeSelector(e))) : (g = i.attr("aria-controls") || a({}).uniqueId()[0].id, e = "#" + g, f = b.element.find(e), f.length || (f = b._createPanel(g), f.insertAfter(b.panels[c - 1] || b.tablist)), f.attr("aria-live", "polite")), f.length && (b.panels = b.panels.add(f)), j && i.data("ui-tabs-aria-controls", j), i.attr({
					"aria-controls": g,
					"aria-labelledby": h
				}), f.attr("aria-labelledby", h)
			}), this.panels.attr("role", "tabpanel"), this._addClass(this.panels, "ui-tabs-panel", "ui-widget-content"), c && (this._off(c.not(this.tabs)), this._off(d.not(this.anchors)), this._off(e.not(this.panels)))
		},
		_getList: function () {
			return this.tablist || this.element.find("ol, ul").eq(0)
		},
		_createPanel: function (b) {
			return a("<div>").attr("id", b).data("ui-tabs-destroy", !0)
		},
		_setOptionDisabled: function (b) {
			var c, d, e;
			for (a.isArray(b) && (b.length ? b.length === this.anchors.length && (b = !0) : b = !1), e = 0; d = this.tabs[e]; e++) c = a(d), !0 === b || -1 !== a.inArray(e, b) ? (c.attr("aria-disabled", "true"), this._addClass(c, null, "ui-state-disabled")) : (c.removeAttr("aria-disabled"), this._removeClass(c, null, "ui-state-disabled"));
			this.options.disabled = b, this._toggleClass(this.widget(), this.widgetFullName + "-disabled", null, !0 === b)
		},
		_setupEvents: function (b) {
			var c = {};
			b && a.each(b.split(" "), function (a, b) {
				c[b] = "_eventHandler"
			}), this._off(this.anchors.add(this.tabs).add(this.panels)), this._on(!0, this.anchors, {
				click: function (a) {
					a.preventDefault()
				}
			}), this._on(this.anchors, c), this._on(this.tabs, {
				keydown: "_tabKeydown"
			}), this._on(this.panels, {
				keydown: "_panelKeydown"
			}), this._focusable(this.tabs), this._hoverable(this.tabs)
		},
		_setupHeightStyle: function (b) {
			var c, d = this.element.parent();
			"fill" === b ? (c = d.height(), c -= this.element.outerHeight() - this.element.height(), this.element.siblings(":visible").each(function () {
				var b = a(this),
					d = b.css("position");
				"absolute" !== d && "fixed" !== d && (c -= b.outerHeight(!0))
			}), this.element.children().not(this.panels).each(function () {
				c -= a(this).outerHeight(!0)
			}), this.panels.each(function () {
				a(this).height(Math.max(0, c - a(this).innerHeight() + a(this).height()))
			}).css("overflow", "auto")) : "auto" === b && (c = 0, this.panels.each(function () {
				c = Math.max(c, a(this).height("").height())
			}).height(c))
		},
		_eventHandler: function (b) {
			var c = this.options,
				d = this.active,
				e = a(b.currentTarget),
				f = e.closest("li"),
				g = f[0] === d[0],
				h = g && c.collapsible,
				i = h ? a() : this._getPanelForTab(f),
				j = d.length ? this._getPanelForTab(d) : a(),
				k = {
					oldTab: d,
					oldPanel: j,
					newTab: h ? a() : f,
					newPanel: i
				};
			b.preventDefault(), f.hasClass("ui-state-disabled") || f.hasClass("ui-tabs-loading") || this.running || g && !c.collapsible || !1 === this._trigger("beforeActivate", b, k) || (c.active = !h && this.tabs.index(f), this.active = g ? a() : f, this.xhr && this.xhr.abort(), j.length || i.length || a.error("jQuery UI Tabs: Mismatching fragment identifier."), i.length && this.load(this.tabs.index(f), b), this._toggle(b, k))
		},
		_toggle: function (b, c) {
			function d() {
				f.running = !1, f._trigger("activate", b, c)
			}

			function e() {
				f._addClass(c.newTab.closest("li"), "ui-tabs-active", "ui-state-active"), g.length && f.options.show ? f._show(g, f.options.show, d) : (g.show(), d())
			}
			var f = this,
				g = c.newPanel,
				h = c.oldPanel;
			this.running = !0, h.length && this.options.hide ? this._hide(h, this.options.hide, function () {
				f._removeClass(c.oldTab.closest("li"), "ui-tabs-active", "ui-state-active"), e()
			}) : (this._removeClass(c.oldTab.closest("li"), "ui-tabs-active", "ui-state-active"), h.hide(), e()), h.attr("aria-hidden", "true"), c.oldTab.attr({
				"aria-selected": "false",
				"aria-expanded": "false"
			}), g.length && h.length ? c.oldTab.attr("tabIndex", -1) : g.length && this.tabs.filter(function () {
				return 0 === a(this).attr("tabIndex")
			}).attr("tabIndex", -1), g.attr("aria-hidden", "false"), c.newTab.attr({
				"aria-selected": "true",
				"aria-expanded": "true",
				tabIndex: 0
			})
		},
		_activate: function (b) {
			var c, d = this._findActive(b);
			d[0] !== this.active[0] && (d.length || (d = this.active), c = d.find(".ui-tabs-anchor")[0], this._eventHandler({
				target: c,
				currentTarget: c,
				preventDefault: a.noop
			}))
		},
		_findActive: function (b) {
			return !1 === b ? a() : this.tabs.eq(b)
		},
		_getIndex: function (b) {
			return "string" == typeof b && (b = this.anchors.index(this.anchors.filter("[href$='" + a.ui.escapeSelector(b) + "']"))), b
		},
		_destroy: function () {
			this.xhr && this.xhr.abort(), this.tablist.removeAttr("role").off(this.eventNamespace), this.anchors.removeAttr("role tabIndex").removeUniqueId(), this.tabs.add(this.panels).each(function () {
				a.data(this, "ui-tabs-destroy") ? a(this).remove() : a(this).removeAttr("role tabIndex aria-live aria-busy aria-selected aria-labelledby aria-hidden aria-expanded")
			}), this.tabs.each(function () {
				var b = a(this),
					c = b.data("ui-tabs-aria-controls");
				c ? b.attr("aria-controls", c).removeData("ui-tabs-aria-controls") : b.removeAttr("aria-controls")
			}), this.panels.show(), "content" !== this.options.heightStyle && this.panels.css("height", "")
		},
		enable: function (b) {
			var c = this.options.disabled;
			!1 !== c && (void 0 === b ? c = !1 : (b = this._getIndex(b), c = a.isArray(c) ? a.map(c, function (a) {
				return a !== b ? a : null
			}) : a.map(this.tabs, function (a, c) {
				return c !== b ? c : null
			})), this._setOptionDisabled(c))
		},
		disable: function (b) {
			var c = this.options.disabled;
			if (!0 !== c) {
				if (void 0 === b) c = !0;
				else {
					if (b = this._getIndex(b), -1 !== a.inArray(b, c)) return;
					c = a.isArray(c) ? a.merge([b], c).sort() : [b]
				}
				this._setOptionDisabled(c)
			}
		},
		load: function (b, c) {
			b = this._getIndex(b);
			var d = this,
				e = this.tabs.eq(b),
				f = e.find(".ui-tabs-anchor"),
				g = this._getPanelForTab(e),
				h = {
					tab: e,
					panel: g
				},
				i = function (a, b) {
					"abort" === b && d.panels.stop(!1, !0), d._removeClass(e, "ui-tabs-loading"), g.removeAttr("aria-busy"), a === d.xhr && delete d.xhr
				};
			this._isLocal(f[0]) || (this.xhr = a.ajax(this._ajaxSettings(f, c, h)), this.xhr && "canceled" !== this.xhr.statusText && (this._addClass(e, "ui-tabs-loading"), g.attr("aria-busy", "true"), this.xhr.done(function (a, b, e) {
				setTimeout(function () {
					g.html(a), d._trigger("load", c, h), i(e, b)
				}, 1)
			}).fail(function (a, b) {
				setTimeout(function () {
					i(a, b)
				}, 1)
			})))
		},
		_ajaxSettings: function (b, c, d) {
			var e = this;
			return {
				url: b.attr("href").replace(/#.*$/, ""),
				beforeSend: function (b, f) {
					return e._trigger("beforeLoad", c, a.extend({
						jqXHR: b,
						ajaxSettings: f
					}, d))
				}
			}
		},
		_getPanelForTab: function (b) {
			var c = a(b).attr("aria-controls");
			return this.element.find(this._sanitizeSelector("#" + c))
		}
	}), !1 !== a.uiBackCompat && a.widget("ui.tabs", a.ui.tabs, {
		_processTabs: function () {
			this._superApply(arguments), this._addClass(this.tabs, "ui-tab")
		}
	}), a.ui.tabs, a.widget("ui.tooltip", {
		version: "1.12.1",
		options: {
			classes: {
				"ui-tooltip": "ui-corner-all ui-widget-shadow"
			},
			content: function () {
				var b = a(this).attr("title") || "";
				return a("<a>").text(b).html()
			},
			hide: !0,
			items: "[title]:not([disabled])",
			position: {
				my: "left top+15",
				at: "left bottom",
				collision: "flipfit flip"
			},
			show: !0,
			track: !1,
			close: null,
			open: null
		},
		_addDescribedBy: function (b, c) {
			var d = (b.attr("aria-describedby") || "").split(/\s+/);
			d.push(c), b.data("ui-tooltip-id", c).attr("aria-describedby", a.trim(d.join(" ")))
		},
		_removeDescribedBy: function (b) {
			var c = b.data("ui-tooltip-id"),
				d = (b.attr("aria-describedby") || "").split(/\s+/),
				e = a.inArray(c, d); - 1 !== e && d.splice(e, 1), b.removeData("ui-tooltip-id"), d = a.trim(d.join(" ")), d ? b.attr("aria-describedby", d) : b.removeAttr("aria-describedby")
		},
		_create: function () {
			this._on({
				mouseover: "open",
				focusin: "open"
			}), this.tooltips = {}, this.parents = {}, this.liveRegion = a("<div>").attr({
				role: "log",
				"aria-live": "assertive",
				"aria-relevant": "additions"
			}).appendTo(this.document[0].body), this._addClass(this.liveRegion, null, "ui-helper-hidden-accessible"), this.disabledTitles = a([])
		},
		_setOption: function (b, c) {
			var d = this;
			this._super(b, c), "content" === b && a.each(this.tooltips, function (a, b) {
				d._updateContent(b.element)
			})
		},
		_setOptionDisabled: function (a) {
			this[a ? "_disable" : "_enable"]()
		},
		_disable: function () {
			var b = this;
			a.each(this.tooltips, function (c, d) {
				var e = a.Event("blur");
				e.target = e.currentTarget = d.element[0], b.close(e, !0)
			}), this.disabledTitles = this.disabledTitles.add(this.element.find(this.options.items).addBack().filter(function () {
				var b = a(this);
				return b.is("[title]") ? b.data("ui-tooltip-title", b.attr("title")).removeAttr("title") : void 0
			}))
		},
		_enable: function () {
			this.disabledTitles.each(function () {
				var b = a(this);
				b.data("ui-tooltip-title") && b.attr("title", b.data("ui-tooltip-title"))
			}), this.disabledTitles = a([])
		},
		open: function (b) {
			var c = this,
				d = a(b ? b.target : this.element).closest(this.options.items);
			d.length && !d.data("ui-tooltip-id") && (d.attr("title") && d.data("ui-tooltip-title", d.attr("title")), d.data("ui-tooltip-open", !0), b && "mouseover" === b.type && d.parents().each(function () {
				var b, d = a(this);
				d.data("ui-tooltip-open") && (b = a.Event("blur"), b.target = b.currentTarget = this, c.close(b, !0)), d.attr("title") && (d.uniqueId(), c.parents[this.id] = {
					element: this,
					title: d.attr("title")
				}, d.attr("title", ""))
			}), this._registerCloseHandlers(b, d), this._updateContent(d, b))
		},
		_updateContent: function (a, b) {
			var c, d = this.options.content,
				e = this,
				f = b ? b.type : null;
			return "string" == typeof d || d.nodeType || d.jquery ? this._open(b, a, d) : void((c = d.call(a[0], function (c) {
				e._delay(function () {
					a.data("ui-tooltip-open") && (b && (b.type = f), this._open(b, a, c))
				})
			})) && this._open(b, a, c))
		},
		_open: function (b, c, d) {
			function e(a) {
				j.of = a, g.is(":hidden") || g.position(j)
			}
			var f, g, h, i, j = a.extend({}, this.options.position);
			if (d) {
				if (f = this._find(c)) return void f.tooltip.find(".ui-tooltip-content").html(d);
				c.is("[title]") && (b && "mouseover" === b.type ? c.attr("title", "") : c.removeAttr("title")), f = this._tooltip(c), g = f.tooltip, this._addDescribedBy(c, g.attr("id")), g.find(".ui-tooltip-content").html(d), this.liveRegion.children().hide(), i = a("<div>").html(g.find(".ui-tooltip-content").html()), i.removeAttr("name").find("[name]").removeAttr("name"), i.removeAttr("id").find("[id]").removeAttr("id"), i.appendTo(this.liveRegion), this.options.track && b && /^mouse/.test(b.type) ? (this._on(this.document, {
					mousemove: e
				}), e(b)) : g.position(a.extend({ of: c
				}, this.options.position)), g.hide(), this._show(g, this.options.show), this.options.track && this.options.show && this.options.show.delay && (h = this.delayedShow = setInterval(function () {
					g.is(":visible") && (e(j.of), clearInterval(h))
				}, a.fx.interval)), this._trigger("open", b, {
					tooltip: g
				})
			}
		},
		_registerCloseHandlers: function (b, c) {
			var d = {
				keyup: function (b) {
					if (b.keyCode === a.ui.keyCode.ESCAPE) {
						var d = a.Event(b);
						d.currentTarget = c[0], this.close(d, !0)
					}
				}
			};
			c[0] !== this.element[0] && (d.remove = function () {
				this._removeTooltip(this._find(c).tooltip)
			}), b && "mouseover" !== b.type || (d.mouseleave = "close"), b && "focusin" !== b.type || (d.focusout = "close"), this._on(!0, c, d)
		},
		close: function (b) {
			var c, d = this,
				e = a(b ? b.currentTarget : this.element),
				f = this._find(e);
			return f ? (c = f.tooltip, void(f.closing || (clearInterval(this.delayedShow), e.data("ui-tooltip-title") && !e.attr("title") && e.attr("title", e.data("ui-tooltip-title")), this._removeDescribedBy(e), f.hiding = !0, c.stop(!0), this._hide(c, this.options.hide, function () {
				d._removeTooltip(a(this))
			}), e.removeData("ui-tooltip-open"), this._off(e, "mouseleave focusout keyup"), e[0] !== this.element[0] && this._off(e, "remove"), this._off(this.document, "mousemove"), b && "mouseleave" === b.type && a.each(this.parents, function (b, c) {
				a(c.element).attr("title", c.title), delete d.parents[b]
			}), f.closing = !0, this._trigger("close", b, {
				tooltip: c
			}), f.hiding || (f.closing = !1)))) : void e.removeData("ui-tooltip-open")
		},
		_tooltip: function (b) {
			var c = a("<div>").attr("role", "tooltip"),
				d = a("<div>").appendTo(c),
				e = c.uniqueId().attr("id");
			return this._addClass(d, "ui-tooltip-content"), this._addClass(c, "ui-tooltip", "ui-widget ui-widget-content"), c.appendTo(this._appendTo(b)), this.tooltips[e] = {
				element: b,
				tooltip: c
			}
		},
		_find: function (a) {
			var b = a.data("ui-tooltip-id");
			return b ? this.tooltips[b] : null
		},
		_removeTooltip: function (a) {
			a.remove(), delete this.tooltips[a.attr("id")]
		},
		_appendTo: function (a) {
			var b = a.closest(".ui-front, dialog");
			return b.length || (b = this.document[0].body), b
		},
		_destroy: function () {
			var b = this;
			a.each(this.tooltips, function (c, d) {
				var e = a.Event("blur"),
					f = d.element;
				e.target = e.currentTarget = f[0], b.close(e, !0), a("#" + c).remove(), f.data("ui-tooltip-title") && (f.attr("title") || f.attr("title", f.data("ui-tooltip-title")), f.removeData("ui-tooltip-title"))
			}), this.liveRegion.remove()
		}
	}), !1 !== a.uiBackCompat && a.widget("ui.tooltip", a.ui.tooltip, {
		options: {
			tooltipClass: null
		},
		_tooltip: function () {
			var a = this._superApply(arguments);
			return this.options.tooltipClass && a.tooltip.addClass(this.options.tooltipClass), a
		}
	}), a.ui.tooltip
}),
function (a, b) {
	if ("object" == typeof exports && "object" == typeof module) module.exports = b(require("jquery"), require("moment"));
	else if ("function" == typeof define && define.amd) define(["jquery", "moment"], b);
	else {
		var c = "object" == typeof exports ? b(require("jquery"), require("moment")) : b(a.jQuery, a.moment);
		for (var d in c)("object" == typeof exports ? exports : a)[d] = c[d]
	}
}(this, function (a, b) {
	return function (a) {
		function b(d) {
			if (c[d]) return c[d].exports;
			var e = c[d] = {
				i: d,
				l: !1,
				exports: {}
			};
			return a[d].call(e.exports, e, e.exports, b), e.l = !0, e.exports
		}
		var c = {};
		return b.m = a, b.c = c, b.d = function (a, c, d) {
			b.o(a, c) || Object.defineProperty(a, c, {
				configurable: !1,
				enumerable: !0,
				get: d
			})
		}, b.n = function (a) {
			var c = a && a.__esModule ? function () {
				return a.default
			} : function () {
				return a
			};
			return b.d(c, "a", c), c
		}, b.o = function (a, b) {
			return Object.prototype.hasOwnProperty.call(a, b)
		}, b.p = "", b(b.s = 72)
	}([function (a, b) {
		var c = function (a, b) {
			for (var c in b) b.hasOwnProperty(c) && (a[c] = b[c])
		};
		b.__extends = function (a, b) {
			function d() {
				this.constructor = a
			}
			c(a, b), a.prototype = null === b ? Object.create(b) : (d.prototype = b.prototype, new d)
		}
	}, function (b, c) {
		b.exports = a
	}, function (a, b, c) {
		function d(a, b) {
			b.left && a.css({
				"border-left-width": 1,
				"margin-left": b.left - 1
			}), b.right && a.css({
				"border-right-width": 1,
				"margin-right": b.right - 1
			})
		}

		function e(a) {
			a.css({
				"margin-left": "",
				"margin-right": "",
				"border-left-width": "",
				"border-right-width": ""
			})
		}

		function f() {
			na("body").addClass("fc-not-allowed")
		}

		function g() {
			na("body").removeClass("fc-not-allowed")
		}

		function h(a, b, c) {
			var d = Math.floor(b / a.length),
				e = Math.floor(b - d * (a.length - 1)),
				f = [],
				g = [],
				h = [],
				j = 0;
			i(a), a.each(function (b, c) {
				var i = b === a.length - 1 ? e : d,
					k = na(c).outerHeight(!0);
				k < i ? (f.push(c), g.push(k), h.push(na(c).height())) : j += k
			}), c && (b -= j, d = Math.floor(b / f.length), e = Math.floor(b - d * (f.length - 1))), na(f).each(function (a, b) {
				var c = a === f.length - 1 ? e : d,
					i = g[a],
					j = h[a],
					k = c - (i - j);
				i < c && na(b).height(k)
			})
		}

		function i(a) {
			a.height("")
		}

		function j(a) {
			var b = 0;
			return a.find("> *").each(function (a, c) {
				var d = na(c).outerWidth();
				d > b && (b = d)
			}), b++, a.width(b), b
		}

		function k(a, b) {
			var c, d = a.add(b);
			return d.css({
				position: "relative",
				left: -1
			}), c = a.outerHeight() - b.outerHeight(), d.css({
				position: "",
				left: ""
			}), c
		}

		function l(a) {
			var b = a.css("position"),
				c = a.parents().filter(function () {
					var a = na(this);
					return /(auto|scroll)/.test(a.css("overflow") + a.css("overflow-y") + a.css("overflow-x"))
				}).eq(0);
			return "fixed" !== b && c.length ? c : na(a[0].ownerDocument || document)
		}

		function m(a, b) {
			var c = a.offset(),
				d = c.left - (b ? b.left : 0),
				e = c.top - (b ? b.top : 0);
			return {
				left: d,
				right: d + a.outerWidth(),
				top: e,
				bottom: e + a.outerHeight()
			}
		}

		function n(a, b) {
			var c = a.offset(),
				d = p(a),
				e = c.left + t(a, "border-left-width") + d.left - (b ? b.left : 0),
				f = c.top + t(a, "border-top-width") + d.top - (b ? b.top : 0);
			return {
				left: e,
				right: e + a[0].clientWidth,
				top: f,
				bottom: f + a[0].clientHeight
			}
		}

		function o(a, b) {
			var c = a.offset(),
				d = c.left + t(a, "border-left-width") + t(a, "padding-left") - (b ? b.left : 0),
				e = c.top + t(a, "border-top-width") + t(a, "padding-top") - (b ? b.top : 0);
			return {
				left: d,
				right: d + a.width(),
				top: e,
				bottom: e + a.height()
			}
		}

		function p(a) {
			var b, c = a[0].offsetWidth - a[0].clientWidth,
				d = a[0].offsetHeight - a[0].clientHeight;
			return c = q(c), d = q(d), b = {
				left: 0,
				right: 0,
				top: 0,
				bottom: d
			}, r() && "rtl" == a.css("direction") ? b.left = c : b.right = c, b
		}

		function q(a) {
			return a = Math.max(0, a), a = Math.round(a)
		}

		function r() {
			return null === oa && (oa = s()), oa
		}

		function s() {
			var a = na("<div><div/></div>").css({
					position: "absolute",
					top: -1e3,
					left: 0,
					border: 0,
					padding: 0,
					overflow: "scroll",
					direction: "rtl"
				}).appendTo("body"),
				b = a.children(),
				c = b.offset().left > a.offset().left;
			return a.remove(), c
		}

		function t(a, b) {
			return parseFloat(a.css(b)) || 0
		}

		function u(a) {
			return 1 == a.which && !a.ctrlKey
		}

		function v(a) {
			var b = a.originalEvent.touches;
			return b && b.length ? b[0].pageX : a.pageX
		}

		function w(a) {
			var b = a.originalEvent.touches;
			return b && b.length ? b[0].pageY : a.pageY
		}

		function x(a) {
			return /^touch/.test(a.type)
		}

		function y(a) {
			a.addClass("fc-unselectable").on("selectstart", A)
		}

		function z(a) {
			a.removeClass("fc-unselectable").off("selectstart", A)
		}

		function A(a) {
			a.preventDefault()
		}

		function B(a, b) {
			var c = {
				left: Math.max(a.left, b.left),
				right: Math.min(a.right, b.right),
				top: Math.max(a.top, b.top),
				bottom: Math.min(a.bottom, b.bottom)
			};
			return c.left < c.right && c.top < c.bottom && c
		}

		function C(a, b) {
			return {
				left: Math.min(Math.max(a.left, b.left), b.right),
				top: Math.min(Math.max(a.top, b.top), b.bottom)
			}
		}

		function D(a) {
			return {
				left: (a.left + a.right) / 2,
				top: (a.top + a.bottom) / 2
			}
		}

		function E(a, b) {
			return {
				left: a.left - b.left,
				top: a.top - b.top
			}
		}

		function F(a) {
			var b, c, d = [],
				e = [];
			for ("string" == typeof a ? e = a.split(/\s*,\s*/) : "function" == typeof a ? e = [a] : na.isArray(a) && (e = a), b = 0; b < e.length; b++) c = e[b], "string" == typeof c ? d.push("-" == c.charAt(0) ? {
				field: c.substring(1),
				order: -1
			} : {
				field: c,
				order: 1
			}) : "function" == typeof c && d.push({
				func: c
			});
			return d
		}

		function G(a, b, c) {
			var d, e;
			for (d = 0; d < c.length; d++)
				if (e = H(a, b, c[d])) return e;
			return 0
		}

		function H(a, b, c) {
			return c.func ? c.func(a, b) : I(a[c.field], b[c.field]) * (c.order || 1)
		}

		function I(a, b) {
			return a || b ? null == b ? -1 : null == a ? 1 : "string" === na.type(a) || "string" === na.type(b) ? String(a).localeCompare(String(b)) : a - b : 0
		}

		function J(a, b) {
			return ma.duration({
				days: a.clone().stripTime().diff(b.clone().stripTime(), "days"),
				ms: a.time() - b.time()
			})
		}

		function K(a, b) {
			return ma.duration({
				days: a.clone().stripTime().diff(b.clone().stripTime(), "days")
			})
		}

		function L(a, b, c) {
			return ma.duration(Math.round(a.diff(b, c, !0)), c)
		}

		function M(a, c) {
			var d, e, f;
			for (d = 0; d < b.unitsDesc.length && (e = b.unitsDesc[d], !((f = O(e, a, c)) >= 1 && ja(f))); d++);
			return e
		}

		function N(a, b) {
			var c = M(a);
			return "week" === c && "object" == typeof b && b.days && (c = "day"), c
		}

		function O(a, b, c) {
			return null != c ? c.diff(b, a, !0) : ma.isDuration(b) ? b.as(a) : b.end.diff(b.start, a, !0)
		}

		function P(a, b, c) {
			var d;
			return S(c) ? (b - a) / c : (d = c.asMonths(), Math.abs(d) >= 1 && ja(d) ? b.diff(a, "months", !0) / d : b.diff(a, "days", !0) / c.asDays())
		}

		function Q(a, b) {
			var c, d;
			return S(a) || S(b) ? a / b : (c = a.asMonths(), d = b.asMonths(), Math.abs(c) >= 1 && ja(c) && Math.abs(d) >= 1 && ja(d) ? c / d : a.asDays() / b.asDays())
		}

		function R(a, b) {
			var c;
			return S(a) ? ma.duration(a * b) : (c = a.asMonths(), Math.abs(c) >= 1 && ja(c) ? ma.duration({
				months: c * b
			}) : ma.duration({
				days: a.asDays() * b
			}))
		}

		function S(a) {
			return Boolean(a.hours() || a.minutes() || a.seconds() || a.milliseconds())
		}

		function T(a) {
			return "[object Date]" === Object.prototype.toString.call(a) || a instanceof Date
		}

		function U(a) {
			return "string" == typeof a && /^\d+\:\d+(?:\:\d+\.?(?:\d{3})?)?$/.test(a)
		}

		function V() {
			for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
			var c = window.console;
			if (c && c.log) return c.log.apply(c, a)
		}

		function W() {
			for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
			var c = window.console;
			return c && c.warn ? c.warn.apply(c, a) : V.apply(null, a)
		}

		function X(a, b) {
			var c, d, e, f, g, h, i = {};
			if (b)
				for (c = 0; c < b.length; c++) {
					for (d = b[c], e = [], f = a.length - 1; f >= 0; f--)
						if ("object" == typeof (g = a[f][d])) e.unshift(g);
						else if (void 0 !== g) {
						i[d] = g;
						break
					}
					e.length && (i[d] = X(e))
				}
			for (c = a.length - 1; c >= 0; c--) {
				h = a[c];
				for (d in h) d in i || (i[d] = h[d])
			}
			return i
		}

		function Y(a, b) {
			for (var c in a) Z(a, c) && (b[c] = a[c])
		}

		function Z(a, b) {
			return pa.call(a, b)
		}

		function $(a, b, c) {
			if (na.isFunction(a) && (a = [a]), a) {
				var d, e;
				for (d = 0; d < a.length; d++) e = a[d].apply(b, c) || e;
				return e
			}
		}

		function _(a, b) {
			for (var c = 0, d = 0; d < a.length;) b(a[d]) ? (a.splice(d, 1), c++) : d++;
			return c
		}

		function aa(a, b) {
			for (var c = 0, d = 0; d < a.length;) a[d] === b ? (a.splice(d, 1), c++) : d++;
			return c
		}

		function ba(a, b) {
			var c, d = a.length;
			if (null == d || d !== b.length) return !1;
			for (c = 0; c < d; c++)
				if (a[c] !== b[c]) return !1;
			return !0
		}

		function ca() {
			for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
			for (var c = 0; c < a.length; c++)
				if (void 0 !== a[c]) return a[c]
		}

		function da(a) {
			return (a + "").replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/'/g, "&#039;").replace(/"/g, "&quot;").replace(/\n/g, "<br />")
		}

		function ea(a) {
			return a.replace(/&.*?;/g, "")
		}

		function fa(a) {
			var b = [];
			return na.each(a, function (a, c) {
				null != c && b.push(a + ":" + c)
			}), b.join(";")
		}

		function ga(a) {
			var b = [];
			return na.each(a, function (a, c) {
				null != c && b.push(a + '="' + da(c) + '"')
			}), b.join(" ")
		}

		function ha(a) {
			return a.charAt(0).toUpperCase() + a.slice(1)
		}

		function ia(a, b) {
			return a - b
		}

		function ja(a) {
			return a % 1 == 0
		}

		function ka(a, b) {
			var c = a[b];
			return function () {
				return c.apply(a, arguments)
			}
		}

		function la(a, b, c) {
			void 0 === c && (c = !1);
			var d, e, f, g, h, i = function () {
				var j = +new Date - g;
				j < b ? d = setTimeout(i, b - j) : (d = null, c || (h = a.apply(f, e), f = e = null))
			};
			return function () {
				f = this, e = arguments, g = +new Date;
				var j = c && !d;
				return d || (d = setTimeout(i, b)), j && (h = a.apply(f, e), f = e = null), h
			}
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var ma = c(3),
			na = c(1);
		b.compensateScroll = d, b.uncompensateScroll = e, b.disableCursor = f, b.enableCursor = g, b.distributeHeight = h, b.undistributeHeight = i, b.matchCellWidths = j, b.subtractInnerElHeight = k, b.getScrollParent = l, b.getOuterRect = m, b.getClientRect = n, b.getContentRect = o, b.getScrollbarWidths = p;
		var oa = null;
		b.isPrimaryMouseButton = u, b.getEvX = v, b.getEvY = w, b.getEvIsTouch = x, b.preventSelection = y, b.allowSelection = z, b.preventDefault = A, b.intersectRects = B, b.constrainPoint = C, b.getRectCenter = D, b.diffPoints = E, b.parseFieldSpecs = F, b.compareByFieldSpecs = G, b.compareByFieldSpec = H, b.flexibleCompare = I, b.dayIDs = ["sun", "mon", "tue", "wed", "thu", "fri", "sat"], b.unitsDesc = ["year", "month", "week", "day", "hour", "minute", "second", "millisecond"], b.diffDayTime = J, b.diffDay = K, b.diffByUnit = L, b.computeGreatestUnit = M, b.computeDurationGreatestUnit = N, b.divideRangeByDuration = P, b.divideDurationByDuration = Q, b.multiplyDuration = R, b.durationHasTime = S, b.isNativeDate = T, b.isTimeString = U, b.log = V, b.warn = W;
		var pa = {}.hasOwnProperty;
		b.mergeProps = X, b.copyOwnProps = Y, b.hasOwnProp = Z, b.applyAll = $, b.removeMatching = _, b.removeExact = aa, b.isArraysEqual = ba, b.firstDefined = ca, b.htmlEscape = da, b.stripHtmlEntities = ea, b.cssToStr = fa, b.attrsToStr = ga, b.capitaliseFirstLetter = ha, b.compareNumbers = ia, b.isInt = ja, b.proxy = ka, b.debounce = la
	}, function (a, c) {
		a.exports = b
	}, function (a, b, c) {
		function d(a, b) {
			return a.startMs - b.startMs
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(3),
			f = c(9),
			g = function () {
				function a(a, b) {
					this.isStart = !0, this.isEnd = !0, e.isMoment(a) && (a = a.clone().stripZone()), e.isMoment(b) && (b = b.clone().stripZone()), a && (this.startMs = a.valueOf()), b && (this.endMs = b.valueOf())
				}
				return a.prototype.intersect = function (b) {
					var c = this.startMs,
						d = this.endMs,
						e = null;
					return null != b.startMs && (c = null == c ? b.startMs : Math.max(c, b.startMs)), null != b.endMs && (d = null == d ? b.endMs : Math.min(d, b.endMs)), (null == c || null == d || c < d) && (e = new a(c, d), e.isStart = this.isStart && c === this.startMs, e.isEnd = this.isEnd && d === this.endMs), e
				}, a.prototype.intersectsWith = function (a) {
					return (null == this.endMs || null == a.startMs || this.endMs > a.startMs) && (null == this.startMs || null == a.endMs || this.startMs < a.endMs)
				}, a.prototype.containsRange = function (a) {
					return (null == this.startMs || null != a.startMs && a.startMs >= this.startMs) && (null == this.endMs || null != a.endMs && a.endMs <= this.endMs)
				}, a.prototype.containsDate = function (a) {
					var b = a.valueOf();
					return (null == this.startMs || b >= this.startMs) && (null == this.endMs || b < this.endMs)
				}, a.prototype.constrainDate = function (a) {
					var b = a.valueOf();
					return null != this.startMs && b < this.startMs && (b = this.startMs), null != this.endMs && b >= this.endMs && (b = this.endMs - 1), b
				}, a.prototype.equals = function (a) {
					return this.startMs === a.startMs && this.endMs === a.endMs
				}, a.prototype.clone = function () {
					var b = new a(this.startMs, this.endMs);
					return b.isStart = this.isStart, b.isEnd = this.isEnd, b
				}, a.prototype.getStart = function () {
					return null != this.startMs ? f.default.utc(this.startMs).stripZone() : null
				}, a.prototype.getEnd = function () {
					return null != this.endMs ? f.default.utc(this.endMs).stripZone() : null
				}, a.prototype.as = function (a) {
					return e.utc(this.endMs).diff(e.utc(this.startMs), a, !0)
				}, a.invertRanges = function (b, c) {
					var e, f, g = [],
						h = c.startMs;
					for (b.sort(d), e = 0; e < b.length; e++) f = b[e], f.startMs > h && g.push(new a(h, f.startMs)), f.endMs > h && (h = f.endMs);
					return h < c.endMs && g.push(new a(h, c.endMs)), g
				}, a
			}();
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(50),
			g = c(21),
			h = c(33),
			i = function (a) {
				function b(c) {
					var d = a.call(this) || this;
					return d.calendar = c, d.className = [], d.uid = String(b.uuid++), d
				}
				return d.__extends(b, a), b.prototype.fetch = function (a, b, c) {}, b.prototype.removeEventDefsById = function (a) {}, b.prototype.removeAllEventDefs = function () {}, b.prototype.getPrimitive = function (a) {}, b.prototype.parseEventDefs = function (a) {
					var b, c, d = [];
					for (b = 0; b < a.length; b++)(c = this.parseEventDef(a[b])) && d.push(c);
					return d
				}, b.prototype.parseEventDef = function (a) {
					var b = this.calendar.opt("eventDataTransform"),
						c = this.eventDataTransform;
					return b && (a = b(a)), c && (a = c(a)), h.default.parse(a, this)
				}, b.prototype.applyManualStandardProps = function (a) {
					return null != a.id && (this.id = b.normalizeId(a.id)), e.isArray(a.className) ? this.className = a.className : "string" == typeof a.className && (this.className = a.className.split(/\s+/)), !0
				}, b.parse = function (a, b) {
					var c = new this(b);
					return !("object" != typeof a || !c.applyProps(a)) && c
				}, b.normalizeId = function (a) {
					return a ? String(a) : null
				}, b.defineStandardProps = f.default.defineStandardProps, b.copyVerbatimStandardProps = f.default.copyVerbatimStandardProps, b.uuid = 0, b
			}(g.default);
		b.default = i, f.default.mixInto(i), i.defineStandardProps({
			id: !1,
			className: !1,
			color: !0,
			backgroundColor: !0,
			borderColor: !0,
			textColor: !0,
			editable: !0,
			startEditable: !0,
			durationEditable: !0,
			rendering: !0,
			overlap: !0,
			constraint: !0,
			allDayDefault: !0,
			eventDataTransform: !0
		})
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(12),
			g = 0,
			h = function (a) {
				function b() {
					var b = null !== a && a.apply(this, arguments) || this;
					return b.listenerId = null, b
				}
				return d.__extends(b, a), b.prototype.listenTo = function (a, b, c) {
					if ("object" == typeof b)
						for (var d in b) b.hasOwnProperty(d) && this.listenTo(a, d, b[d]);
					else "string" == typeof b && a.on(b + "." + this.getListenerNamespace(), e.proxy(c, this))
				}, b.prototype.stopListeningTo = function (a, b) {
					a.off((b || "") + "." + this.getListenerNamespace())
				}, b.prototype.getListenerNamespace = function () {
					return null == this.listenerId && (this.listenerId = g++), "_listener" + this.listenerId
				}, b
			}(f.default);
		b.default = h
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		}), b.default = {
			version: "<%= version %>",
			internalApiVersion: 12,
			touchMouseIgnoreWait: 500,
			dataAttrPrefix: "",
			views: {},
			locales: {}
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(12),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.on = function (a, b) {
					return e(this).on(a, this._prepareIntercept(b)), this
				}, b.prototype.one = function (a, b) {
					return e(this).one(a, this._prepareIntercept(b)), this
				}, b.prototype._prepareIntercept = function (a) {
					var b = function (b, c) {
						return a.apply(c.context || this, c.args || [])
					};
					return a.guid || (a.guid = e.guid++), b.guid = a.guid, b
				}, b.prototype.off = function (a, b) {
					return e(this).off(a, b), this
				}, b.prototype.trigger = function (a) {
					for (var b = [], c = 1; c < arguments.length; c++) b[c - 1] = arguments[c];
					return e(this).triggerHandler(a, {
						args: b
					}), this
				}, b.prototype.triggerWith = function (a, b, c) {
					return e(this).triggerHandler(a, {
						context: b,
						args: c
					}), this
				}, b.prototype.hasHandlers = function (a) {
					var b = e._data(this, "events");
					return b && b[a] && b[a].length > 0
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		function d(a, b) {
			return l.format.call(a, b)
		}

		function e(a, b, c) {
			void 0 === b && (b = !1), void 0 === c && (c = !1);
			var d, e, k, l, m = a[0],
				n = 1 == a.length && "string" == typeof m;
			return f.isMoment(m) || h.isNativeDate(m) || void 0 === m ? l = f.apply(null, a) : (d = !1, e = !1, n ? i.test(m) ? (m += "-01", a = [m], d = !0, e = !0) : (k = j.exec(m)) && (d = !k[5], e = !0) : g.isArray(m) && (e = !0), l = b || d ? f.utc.apply(f, a) : f.apply(null, a), d ? (l._ambigTime = !0, l._ambigZone = !0) : c && (e ? l._ambigZone = !0 : n && l.utcOffset(m))), l._fullCalendar = !0, l
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var f = c(3),
			g = c(1),
			h = c(2),
			i = /^\s*\d{4}-\d\d$/,
			j = /^\s*\d{4}-(?:(\d\d-\d\d)|(W\d\d$)|(W\d\d-\d)|(\d\d\d))((T| )(\d\d(:\d\d(:\d\d(\.\d+)?)?)?)?)?$/,
			k = f.fn;
		b.newMomentProto = k;
		var l = g.extend({}, k);
		b.oldMomentProto = l;
		var m = f.momentProperties;
		m.push("_fullCalendar"), m.push("_ambigTime"), m.push("_ambigZone"), b.oldMomentFormat = d;
		var n = function () {
			return e(arguments)
		};
		b.default = n, n.utc = function () {
			var a = e(arguments, !0);
			return a.hasTime() && a.utc(), a
		}, n.parseZone = function () {
			return e(arguments, !0, !0)
		}, k.week = k.weeks = function (a) {
			var b = this._locale._fullCalendar_weekCalc;
			return null == a && "function" == typeof b ? b(this) : "ISO" === b ? l.isoWeek.apply(this, arguments) : l.week.apply(this, arguments)
		}, k.time = function (a) {
			if (!this._fullCalendar) return l.time.apply(this, arguments);
			if (null == a) return f.duration({
				hours: this.hours(),
				minutes: this.minutes(),
				seconds: this.seconds(),
				milliseconds: this.milliseconds()
			});
			this._ambigTime = !1, f.isDuration(a) || f.isMoment(a) || (a = f.duration(a));
			var b = 0;
			return f.isDuration(a) && (b = 24 * Math.floor(a.asDays())), this.hours(b + a.hours()).minutes(a.minutes()).seconds(a.seconds()).milliseconds(a.milliseconds())
		}, k.stripTime = function () {
			return this._ambigTime || (this.utc(!0), this.set({
				hours: 0,
				minutes: 0,
				seconds: 0,
				ms: 0
			}), this._ambigTime = !0, this._ambigZone = !0), this
		}, k.hasTime = function () {
			return !this._ambigTime
		}, k.stripZone = function () {
			var a;
			return this._ambigZone || (a = this._ambigTime, this.utc(!0), this._ambigTime = a || !1, this._ambigZone = !0), this
		}, k.hasZone = function () {
			return !this._ambigZone
		}, k.local = function (a) {
			return l.local.call(this, this._ambigZone || a), this._ambigTime = !1, this._ambigZone = !1, this
		}, k.utc = function (a) {
			return l.utc.call(this, a), this._ambigTime = !1, this._ambigZone = !1, this
		}, k.utcOffset = function (a) {
			return null != a && (this._ambigTime = !1, this._ambigZone = !1), l.utcOffset.apply(this, arguments)
		}
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a, b) {
				this.isAllDay = !1, this.unzonedRange = a, this.isAllDay = b
			}
			return a.prototype.toLegacy = function (a) {
				return {
					start: a.msToMoment(this.unzonedRange.startMs, this.isAllDay),
					end: a.msToMoment(this.unzonedRange.endMs, this.isAllDay)
				}
			}, a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(22),
			f = c(51),
			g = c(15),
			h = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.buildInstances = function () {
					return [this.buildInstance()]
				}, b.prototype.buildInstance = function () {
					return new f.default(this, this.dateProfile)
				}, b.prototype.isAllDay = function () {
					return this.dateProfile.isAllDay()
				}, b.prototype.clone = function () {
					var b = a.prototype.clone.call(this);
					return b.dateProfile = this.dateProfile, b
				}, b.prototype.rezone = function () {
					var a = this.source.calendar,
						b = this.dateProfile;
					this.dateProfile = new g.default(a.moment(b.start), b.end ? a.moment(b.end) : null, a)
				}, b.prototype.applyManualStandardProps = function (b) {
					var c = a.prototype.applyManualStandardProps.call(this, b),
						d = g.default.parse(b, this.source);
					return !!d && (this.dateProfile = d, null != b.date && (this.miscProps.date = b.date), c)
				}, b
			}(e.default);
		b.default = h, h.defineStandardProps({
			start: !1,
			date: !1,
			end: !1,
			allDay: !1
		})
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a() {}
			return a.mixInto = function (a) {
				var b = this;
				Object.getOwnPropertyNames(this.prototype).forEach(function (c) {
					a.prototype[c] || (a.prototype[c] = b.prototype[c])
				})
			}, a.mixOver = function (a) {
				var b = this;
				Object.getOwnPropertyNames(this.prototype).forEach(function (c) {
					a.prototype[c] = b.prototype[c]
				})
			}, a
		}();
		b.default = c
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a) {
				this.view = a._getView(), this.component = a
			}
			return a.prototype.opt = function (a) {
				return this.view.opt(a)
			}, a.prototype.end = function () {}, a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(7),
			f = c(8),
			g = c(6),
			h = null,
			i = 0,
			j = function () {
				function a() {
					this.isTouching = !1, this.mouseIgnoreDepth = 0
				}
				return a.prototype.bind = function () {
					var a = this;
					this.listenTo(d(document), {
						touchstart: this.handleTouchStart,
						touchcancel: this.handleTouchCancel,
						touchend: this.handleTouchEnd,
						mousedown: this.handleMouseDown,
						mousemove: this.handleMouseMove,
						mouseup: this.handleMouseUp,
						click: this.handleClick,
						selectstart: this.handleSelectStart,
						contextmenu: this.handleContextMenu
					}), window.addEventListener("touchmove", this.handleTouchMoveProxy = function (b) {
						a.handleTouchMove(d.Event(b))
					}, {
						passive: !1
					}), window.addEventListener("scroll", this.handleScrollProxy = function (b) {
						a.handleScroll(d.Event(b))
					}, !0)
				}, a.prototype.unbind = function () {
					this.stopListeningTo(d(document)), window.removeEventListener("touchmove", this.handleTouchMoveProxy), window.removeEventListener("scroll", this.handleScrollProxy, !0)
				}, a.prototype.handleTouchStart = function (a) {
					this.stopTouch(a, !0), this.isTouching = !0, this.trigger("touchstart", a)
				}, a.prototype.handleTouchMove = function (a) {
					this.isTouching && this.trigger("touchmove", a)
				}, a.prototype.handleTouchCancel = function (a) {
					this.isTouching && (this.trigger("touchcancel", a), this.stopTouch(a))
				}, a.prototype.handleTouchEnd = function (a) {
					this.stopTouch(a)
				}, a.prototype.handleMouseDown = function (a) {
					this.shouldIgnoreMouse() || this.trigger("mousedown", a)
				}, a.prototype.handleMouseMove = function (a) {
					this.shouldIgnoreMouse() || this.trigger("mousemove", a)
				}, a.prototype.handleMouseUp = function (a) {
					this.shouldIgnoreMouse() || this.trigger("mouseup", a)
				}, a.prototype.handleClick = function (a) {
					this.shouldIgnoreMouse() || this.trigger("click", a)
				}, a.prototype.handleSelectStart = function (a) {
					this.trigger("selectstart", a)
				}, a.prototype.handleContextMenu = function (a) {
					this.trigger("contextmenu", a)
				}, a.prototype.handleScroll = function (a) {
					this.trigger("scroll", a)
				}, a.prototype.stopTouch = function (a, b) {
					void 0 === b && (b = !1), this.isTouching && (this.isTouching = !1, this.trigger("touchend", a), b || this.startTouchMouseIgnore())
				}, a.prototype.startTouchMouseIgnore = function () {
					var a = this,
						b = e.default.touchMouseIgnoreWait;
					b && (this.mouseIgnoreDepth++, setTimeout(function () {
						a.mouseIgnoreDepth--
					}, b))
				}, a.prototype.shouldIgnoreMouse = function () {
					return this.isTouching || Boolean(this.mouseIgnoreDepth)
				}, a.get = function () {
					return h || (h = new a, h.bind()), h
				}, a.needed = function () {
					a.get(), i++
				}, a.unneeded = function () {
					--i || (h.unbind(), h = null)
				}, a
			}();
		b.default = j, g.default.mixInto(j), f.default.mixInto(j)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(4),
			e = function () {
				function a(a, b, c) {
					this.start = a, this.end = b || null, this.unzonedRange = this.buildUnzonedRange(c)
				}
				return a.prototype.isAllDay = function () {
					return !(this.start.hasTime() || this.end && this.end.hasTime())
				}, a.prototype.buildUnzonedRange = function (a) {
					var b = this.start.clone().stripZone().valueOf(),
						c = this.getEnd(a).stripZone().valueOf();
					return new d.default(b, c)
				}, a.prototype.getEnd = function (a) {
					return this.end ? this.end.clone() : a.getDefaultEventEnd(this.isAllDay(), this.start)
				}, a.isStandardProp = function (a) {
					return "start" === a || "date" === a || "end" === a || "allDay" === a
				}, a.parse = function (b, c) {
					var d = b.start || b.date,
						e = b.end;
					if (!d) return !1;
					var f = c.calendar,
						g = f.moment(d),
						h = e ? f.moment(e) : null,
						i = b.allDay,
						j = f.opt("forceEventDuration");
					return !!g.isValid() && (!h || h.isValid() && h.isAfter(g) || (h = null), null == i && null == (i = c.allDayDefault) && (i = f.opt("allDayDefault")), !0 === i ? (g.stripTime(), h && h.stripTime()) : !1 === i && (g.hasTime() || g.time(0), h && !h.hasTime() && h.time(0)), !h && j && (h = f.getDefaultEventEnd(!g.hasTime(), g)), new a(g, h, f))
				}, a
			}();
		b.default = e
	}, function (a, b, c) {
		function d(a, b) {
			a.then = function (c) {
				return "function" == typeof c ? g.resolve(c(b)) : a
			}
		}

		function e(a) {
			a.then = function (b, c) {
				return "function" == typeof c && c(), a
			}
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var f = c(1),
			g = {
				construct: function (a) {
					var b = f.Deferred(),
						c = b.promise();
					return "function" == typeof a && a(function (a) {
						b.resolve(a), d(c, a)
					}, function () {
						b.reject(), e(c)
					}), c
				},
				resolve: function (a) {
					var b = f.Deferred().resolve(a),
						c = b.promise();
					return d(c, a), c
				},
				reject: function () {
					var a = f.Deferred().reject(),
						b = a.promise();
					return e(b), b
				}
			};
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(4),
			e = c(23),
			f = c(53),
			g = function () {
				function a(a) {
					this.eventInstances = a || []
				}
				return a.prototype.getAllEventRanges = function (a) {
					return a ? this.sliceNormalRenderRanges(a) : this.eventInstances.map(e.eventInstanceToEventRange)
				}, a.prototype.sliceRenderRanges = function (a) {
					return this.isInverse() ? this.sliceInverseRenderRanges(a) : this.sliceNormalRenderRanges(a)
				}, a.prototype.sliceNormalRenderRanges = function (a) {
					var b, c, d, e = this.eventInstances,
						g = [];
					for (b = 0; b < e.length; b++) c = e[b], (d = c.dateProfile.unzonedRange.intersect(a)) && g.push(new f.default(d, c.def, c));
					return g
				}, a.prototype.sliceInverseRenderRanges = function (a) {
					var b = this.eventInstances.map(e.eventInstanceToUnzonedRange),
						c = this.getEventDef();
					return b = d.default.invertRanges(b, a), b.map(function (a) {
						return new f.default(a, c)
					})
				}, a.prototype.isInverse = function () {
					return this.getEventDef().hasInverseRendering()
				}, a.prototype.getEventDef = function () {
					return this.explicitEventDef || this.eventInstances[0].def
				}, a
			}();
		b.default = g
	}, function (a, b, c) {
		function d(a, b) {
			return !a && !b || !(!a || !b) && a.component === b.component && e(a, b) && e(b, a)
		}

		function e(a, b) {
			for (var c in a)
				if (!/^(component|left|right|top|bottom)$/.test(c) && a[c] !== b[c]) return !1;
			return !0
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var f = c(0),
			g = c(2),
			h = c(39),
			i = function (a) {
				function b(b, c) {
					var d = a.call(this, c) || this;
					return d.component = b, d
				}
				return f.__extends(b, a), b.prototype.handleInteractionStart = function (b) {
					var c, d, e, f = this.subjectEl;
					this.component.hitsNeeded(), this.computeScrollBounds(), b ? (d = {
						left: g.getEvX(b),
						top: g.getEvY(b)
					}, e = d, f && (c = g.getOuterRect(f), e = g.constrainPoint(e, c)), this.origHit = this.queryHit(e.left, e.top), f && this.options.subjectCenter && (this.origHit && (c = g.intersectRects(this.origHit, c) || c), e = g.getRectCenter(c)), this.coordAdjust = g.diffPoints(e, d)) : (this.origHit = null, this.coordAdjust = null), a.prototype.handleInteractionStart.call(this, b)
				}, b.prototype.handleDragStart = function (b) {
					var c;
					a.prototype.handleDragStart.call(this, b), (c = this.queryHit(g.getEvX(b), g.getEvY(b))) && this.handleHitOver(c)
				}, b.prototype.handleDrag = function (b, c, e) {
					var f;
					a.prototype.handleDrag.call(this, b, c, e), f = this.queryHit(g.getEvX(e), g.getEvY(e)), d(f, this.hit) || (this.hit && this.handleHitOut(), f && this.handleHitOver(f))
				}, b.prototype.handleDragEnd = function (b) {
					this.handleHitDone(), a.prototype.handleDragEnd.call(this, b)
				}, b.prototype.handleHitOver = function (a) {
					var b = d(a, this.origHit);
					this.hit = a, this.trigger("hitOver", this.hit, b, this.origHit)
				}, b.prototype.handleHitOut = function () {
					this.hit && (this.trigger("hitOut", this.hit), this.handleHitDone(), this.hit = null)
				}, b.prototype.handleHitDone = function () {
					this.hit && this.trigger("hitDone", this.hit)
				}, b.prototype.handleInteractionEnd = function (b, c) {
					a.prototype.handleInteractionEnd.call(this, b, c), this.origHit = null, this.hit = null, this.component.hitsNotNeeded()
				}, b.prototype.handleScrollEnd = function () {
					a.prototype.handleScrollEnd.call(this), this.isDragging && (this.component.releaseHits(), this.component.prepareHits())
				}, b.prototype.queryHit = function (a, b) {
					return this.coordAdjust && (a += this.coordAdjust.left, b += this.coordAdjust.top), this.component.queryHit(a, b)
				}, b
			}(h.default);
		b.default = i
	}, function (a, b, c) {
		function d(a) {
			return e.mergeProps(a, f)
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(2);
		b.globalDefaults = {
			titleRangeSeparator: " – ",
			monthYearFormat: "MMMM YYYY",
			defaultTimedEventDuration: "02:00:00",
			defaultAllDayEventDuration: {
				days: 1
			},
			forceEventDuration: !1,
			nextDayThreshold: "09:00:00",
			columnHeader: !0,
			defaultView: "month",
			aspectRatio: 1.35,
			header: {
				left: "title",
				center: "",
				right: "today prev,next"
			},
			weekends: !0,
			weekNumbers: !1,
			weekNumberTitle: "W",
			weekNumberCalculation: "local",
			scrollTime: "06:00:00",
			minTime: "00:00:00",
			maxTime: "24:00:00",
			showNonCurrentDates: !0,
			lazyFetching: !0,
			startParam: "start",
			endParam: "end",
			timezoneParam: "timezone",
			timezone: !1,
			locale: null,
			isRTL: !1,
			buttonText: {
				prev: "prev",
				next: "next",
				prevYear: "prev year",
				nextYear: "next year",
				year: "year",
				today: "today",
				month: "month",
				week: "week",
				day: "day"
			},
			allDayText: "all-day",
			agendaEventMinHeight: 0,
			theme: !1,
			dragOpacity: .75,
			dragRevertDuration: 500,
			dragScroll: !0,
			unselectAuto: !0,
			dropAccept: "*",
			eventOrder: "title",
			eventLimit: !1,
			eventLimitText: "more",
			eventLimitClick: "popover",
			dayPopoverFormat: "LL",
			handleWindowResize: !0,
			windowResizeDelay: 100,
			longPressDelay: 1e3
		}, b.englishDefaults = {
			dayPopoverFormat: "dddd, MMMM D"
		}, b.rtlDefaults = {
			header: {
				left: "next,prev today",
				center: "",
				right: "title"
			},
			buttonIcons: {
				prev: "right-single-arrow",
				next: "left-single-arrow",
				prevYear: "right-double-arrow",
				nextYear: "left-double-arrow"
			},
			themeButtonIcons: {
				prev: "circle-triangle-e",
				next: "circle-triangle-w",
				nextYear: "seek-prev",
				prevYear: "seek-next"
			}
		};
		var f = ["header", "footer", "buttonText", "buttonIcons", "themeButtonIcons"];
		b.mergeOptions = d
	}, function (a, b, c) {
		function d(a, b, c) {
			var d = m[a] || (m[a] = {});
			d.isRTL = c.isRTL, d.weekNumberTitle = c.weekHeader, h.each(n, function (a, b) {
				d[a] = b(c)
			});
			var e = h.datepicker;
			e && (e.regional[b] = e.regional[a] = c, e.regional.en = e.regional[""], e.setDefaults(c))
		}

		function e(a, b) {
			var c, d;
			c = m[a] || (m[a] = {}), b && (c = m[a] = k.mergeOptions([c, b])), d = g(a), h.each(o, function (a, b) {
				null == c[a] && (c[a] = b(d, c))
			}), k.globalDefaults.locale = a
		}

		function f(a) {
			h.each(p, function (b, c) {
				null == a[b] && (a[b] = c(a))
			})
		}

		function g(a) {
			return i.localeData(a) || i.localeData("en")
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var h = c(1),
			i = c(3),
			j = c(7),
			k = c(19),
			l = c(2),
			m = j.default.locales;
		b.localeOptionHash = m, b.datepickerLocale = d, b.locale = e;
		var n = {
				buttonText: function (a) {
					return {
						prev: l.stripHtmlEntities(a.prevText),
						next: l.stripHtmlEntities(a.nextText),
						today: l.stripHtmlEntities(a.currentText)
					}
				},
				monthYearFormat: function (a) {
					return a.showMonthAfterYear ? "YYYY[" + a.yearSuffix + "] MMMM" : "MMMM YYYY[" + a.yearSuffix + "]"
				}
			},
			o = {
				dayOfMonthFormat: function (a, b) {
					var c = a.longDateFormat("l");
					return c = c.replace(/^Y+[^\w\s]*|[^\w\s]*Y+$/g, ""), b.isRTL ? c += " ddd" : c = "ddd " + c, c
				},
				mediumTimeFormat: function (a) {
					return a.longDateFormat("LT").replace(/\s*a$/i, "a")
				},
				smallTimeFormat: function (a) {
					return a.longDateFormat("LT").replace(":mm", "(:mm)").replace(/(\Wmm)$/, "($1)").replace(/\s*a$/i, "a")
				},
				extraSmallTimeFormat: function (a) {
					return a.longDateFormat("LT").replace(":mm", "(:mm)").replace(/(\Wmm)$/, "($1)").replace(/\s*a$/i, "t")
				},
				hourFormat: function (a) {
					return a.longDateFormat("LT").replace(":mm", "").replace(/(\Wmm)$/, "").replace(/\s*a$/i, "a")
				},
				noMeridiemTimeFormat: function (a) {
					return a.longDateFormat("LT").replace(/\s*a$/i, "")
				}
			},
			p = {
				smallDayDateFormat: function (a) {
					return a.isRTL ? "D dd" : "dd D"
				},
				weekFormat: function (a) {
					return a.isRTL ? "w[ " + a.weekNumberTitle + "]" : "[" + a.weekNumberTitle + " ]w"
				},
				smallWeekFormat: function (a) {
					return a.isRTL ? "w[" + a.weekNumberTitle + "]" : "[" + a.weekNumberTitle + "]w"
				}
			};
		b.populateInstanceComputableOptions = f, b.getMomentLocaleData = g, e("en", k.englishDefaults)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = function () {
				function a() {}
				return a.extend = function (a) {
					var b = function (a) {
						function b() {
							return null !== a && a.apply(this, arguments) || this
						}
						return d.__extends(b, a), b
					}(this);
					return e.copyOwnProps(a, b.prototype), b
				}, a.mixin = function (a) {
					e.copyOwnProps(a, this.prototype)
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(50),
			f = function () {
				function a(a) {
					this.source = a, this.className = [], this.miscProps = {}
				}
				return a.prototype.clone = function () {
					var b = new this.constructor(this.source);
					return b.id = this.id, b.rawId = this.rawId, b.uid = this.uid, a.copyVerbatimStandardProps(this, b), b.className = this.className.slice(), b.miscProps = d.extend({}, this.miscProps), b
				}, a.prototype.hasInverseRendering = function () {
					return "inverse-background" === this.getRendering()
				}, a.prototype.hasBgRendering = function () {
					var a = this.getRendering();
					return "inverse-background" === a || "background" === a
				}, a.prototype.getRendering = function () {
					return null != this.rendering ? this.rendering : this.source.rendering
				}, a.prototype.getConstraint = function () {
					return null != this.constraint ? this.constraint : null != this.source.constraint ? this.source.constraint : this.source.calendar.opt("eventConstraint")
				}, a.prototype.getOverlap = function () {
					return null != this.overlap ? this.overlap : null != this.source.overlap ? this.source.overlap : this.source.calendar.opt("eventOverlap")
				}, a.prototype.isStartExplicitlyEditable = function () {
					return null != this.startEditable ? this.startEditable : this.source.startEditable
				}, a.prototype.isDurationExplicitlyEditable = function () {
					return null != this.durationEditable ? this.durationEditable : this.source.durationEditable
				}, a.prototype.isExplicitlyEditable = function () {
					return null != this.editable ? this.editable : this.source.editable
				}, a.prototype.toLegacy = function () {
					var b = d.extend({}, this.miscProps);
					return b._id = this.uid, b.source = this.source, b.className = this.className.slice(), b.allDay = this.isAllDay(), null != this.rawId && (b.id = this.rawId), a.copyVerbatimStandardProps(this, b), b
				}, a.prototype.applyManualStandardProps = function (b) {
					return null != b.id ? this.id = a.normalizeId(this.rawId = b.id) : this.id = a.generateId(), null != b._id ? this.uid = String(b._id) : this.uid = a.generateId(), d.isArray(b.className) && (this.className = b.className), "string" == typeof b.className && (this.className = b.className.split(/\s+/)), !0
				}, a.prototype.applyMiscProps = function (a) {
					d.extend(this.miscProps, a)
				}, a.parse = function (a, b) {
					var c = new this(b);
					return !!c.applyProps(a) && c
				}, a.normalizeId = function (a) {
					return String(a)
				}, a.generateId = function () {
					return "_fc" + a.uuid++
				}, a.defineStandardProps = e.default.defineStandardProps, a.copyVerbatimStandardProps = e.default.copyVerbatimStandardProps, a.uuid = 0, a
			}();
		b.default = f, e.default.mixInto(f), f.defineStandardProps({
			_id: !1,
			id: !1,
			className: !1,
			source: !1,
			title: !0,
			url: !0,
			rendering: !0,
			constraint: !0,
			overlap: !0,
			editable: !0,
			startEditable: !0,
			durationEditable: !0,
			color: !0,
			backgroundColor: !0,
			borderColor: !0,
			textColor: !0
		})
	}, function (a, b, c) {
		function d(a, b) {
			var c, d = [];
			for (c = 0; c < a.length; c++) d.push.apply(d, a[c].buildInstances(b));
			return d
		}

		function e(a) {
			return new i.default(a.dateProfile.unzonedRange, a.def, a)
		}

		function f(a) {
			return new j.default(new k.default(a.unzonedRange, a.eventDef.isAllDay()), a.eventDef, a.eventInstance)
		}

		function g(a) {
			return a.dateProfile.unzonedRange
		}

		function h(a) {
			return a.componentFootprint
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var i = c(53),
			j = c(24),
			k = c(10);
		b.eventDefsToEventInstances = d, b.eventInstanceToEventRange = e, b.eventRangeToEventFootprint = f, b.eventInstanceToUnzonedRange = g, b.eventFootprintToComponentFootprint = h
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a, b, c) {
				this.componentFootprint = a, this.eventDef = b, c && (this.eventInstance = c)
			}
			return a.prototype.getEventLegacy = function () {
				return (this.eventInstance || this.eventDef).toLegacy()
			}, a
		}();
		b.default = c
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		}), b.default = {
			sourceClasses: [],
			registerClass: function (a) {
				this.sourceClasses.unshift(a)
			},
			parse: function (a, b) {
				var c, d, e = this.sourceClasses;
				for (c = 0; c < e.length; c++)
					if (d = e[c].parse(a, b)) return d
			}
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(2),
			e = c(15),
			f = c(22),
			g = c(35),
			h = c(11),
			i = function () {
				function a() {}
				return a.prototype.mutateSingle = function (a) {
					var b;
					return this.dateMutation && (b = a.dateProfile, a.dateProfile = this.dateMutation.buildNewDateProfile(b, a.source.calendar)), null != this.eventDefId && (a.id = f.default.normalizeId(a.rawId = this.eventDefId)), this.className && (a.className = this.className), this.verbatimStandardProps && h.default.copyVerbatimStandardProps(this.verbatimStandardProps, a), this.miscProps && a.applyMiscProps(this.miscProps), b ? function () {
						a.dateProfile = b
					} : function () {}
				}, a.prototype.setDateMutation = function (a) {
					a && !a.isEmpty() ? this.dateMutation = a : this.dateMutation = null
				}, a.prototype.isEmpty = function () {
					return !this.dateMutation
				}, a.createFromRawProps = function (b, c, h) {
					var i, j, k, l, m = b.def,
						n = {},
						o = {},
						p = {},
						q = {},
						r = null,
						s = null;
					for (i in c) e.default.isStandardProp(i) ? n[i] = c[i] : m.isStandardProp(i) ? o[i] = c[i] : m.miscProps[i] !== c[i] && (p[i] = c[i]);
					return j = e.default.parse(n, m.source), j && (k = g.default.createFromDiff(b.dateProfile, j, h)), o.id !== m.id && (r = o.id), d.isArraysEqual(o.className, m.className) || (s = o.className), f.default.copyVerbatimStandardProps(o, q), l = new a, l.eventDefId = r, l.className = s, l.verbatimStandardProps = q, l.miscProps = p, k && (l.dateMutation = k), l
				}, a
			}();
		b.default = i
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = function () {
				function a(a) {
					this.optionsManager = a, this.processIconOverride()
				}
				return a.prototype.processIconOverride = function () {
					this.iconOverrideOption && this.setIconOverride(this.optionsManager.get(this.iconOverrideOption))
				}, a.prototype.setIconOverride = function (a) {
					var b, c;
					if (d.isPlainObject(a)) {
						b = d.extend({}, this.iconClasses);
						for (c in a) b[c] = this.applyIconOverridePrefix(a[c]);
						this.iconClasses = b
					} else !1 === a && (this.iconClasses = {})
				}, a.prototype.applyIconOverridePrefix = function (a) {
					var b = this.iconOverridePrefix;
					return b && 0 !== a.indexOf(b) && (a = b + a), a
				}, a.prototype.getClass = function (a) {
					return this.classes[a] || ""
				}, a.prototype.getIconClass = function (a) {
					var b = this.iconClasses[a];
					return b ? this.baseIconClass + " " + b : ""
				}, a.prototype.getCustomButtonIconClass = function (a) {
					var b;
					return this.iconOverrideCustomButtonOption && (b = a[this.iconOverrideCustomButtonOption]) ? this.baseIconClass + " " + this.applyIconOverridePrefix(b) : ""
				}, a
			}();
		b.default = e, e.prototype.classes = {}, e.prototype.iconClasses = {}, e.prototype.baseIconClass = "", e.prototype.iconOverridePrefix = ""
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(21),
			h = function (a) {
				function b(b) {
					var c = a.call(this) || this;
					return b = b || {}, c.overflowX = b.overflowX || b.overflow || "auto", c.overflowY = b.overflowY || b.overflow || "auto", c
				}
				return d.__extends(b, a), b.prototype.render = function () {
					this.el = this.renderEl(), this.applyOverflow()
				}, b.prototype.renderEl = function () {
					return this.scrollEl = e('<div class="fc-scroller"></div>')
				}, b.prototype.clear = function () {
					this.setHeight("auto"), this.applyOverflow()
				}, b.prototype.destroy = function () {
					this.el.remove()
				}, b.prototype.applyOverflow = function () {
					this.scrollEl.css({
						"overflow-x": this.overflowX,
						"overflow-y": this.overflowY
					})
				}, b.prototype.lockOverflow = function (a) {
					var b = this.overflowX,
						c = this.overflowY;
					a = a || this.getScrollbarWidths(), "auto" === b && (b = a.top || a.bottom || this.scrollEl[0].scrollWidth - 1 > this.scrollEl[0].clientWidth ? "scroll" : "hidden"), "auto" === c && (c = a.left || a.right || this.scrollEl[0].scrollHeight - 1 > this.scrollEl[0].clientHeight ? "scroll" : "hidden"), this.scrollEl.css({
						"overflow-x": b,
						"overflow-y": c
					})
				}, b.prototype.setHeight = function (a) {
					this.scrollEl.height(a)
				}, b.prototype.getScrollTop = function () {
					return this.scrollEl.scrollTop()
				}, b.prototype.setScrollTop = function (a) {
					this.scrollEl.scrollTop(a)
				}, b.prototype.getClientWidth = function () {
					return this.scrollEl[0].clientWidth
				}, b.prototype.getClientHeight = function () {
					return this.scrollEl[0].clientHeight
				}, b.prototype.getScrollbarWidths = function () {
					return f.getScrollbarWidths(this.scrollEl)
				}, b
			}(g.default);
		b.default = h
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(61),
			h = c(14),
			i = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.segSelector = ".fc-event-container > *", d.dateSelectingClass && (d.dateClicking = new d.dateClickingClass(d)), d.dateSelectingClass && (d.dateSelecting = new d.dateSelectingClass(d)), d.eventPointingClass && (d.eventPointing = new d.eventPointingClass(d)), d.eventDraggingClass && d.eventPointing && (d.eventDragging = new d.eventDraggingClass(d, d.eventPointing)), d.eventResizingClass && d.eventPointing && (d.eventResizing = new d.eventResizingClass(d, d.eventPointing)), d.externalDroppingClass && (d.externalDropping = new d.externalDroppingClass(d)), d
				}
				return d.__extends(b, a), b.prototype.setElement = function (b) {
					a.prototype.setElement.call(this, b), this.dateClicking && this.dateClicking.bindToEl(b), this.dateSelecting && this.dateSelecting.bindToEl(b), this.bindAllSegHandlersToEl(b)
				}, b.prototype.removeElement = function () {
					this.endInteractions(), a.prototype.removeElement.call(this)
				}, b.prototype.executeEventUnrender = function () {
					this.endInteractions(), a.prototype.executeEventUnrender.call(this)
				}, b.prototype.bindGlobalHandlers = function () {
					a.prototype.bindGlobalHandlers.call(this), this.externalDropping && this.externalDropping.bindToDocument()
				}, b.prototype.unbindGlobalHandlers = function () {
					a.prototype.unbindGlobalHandlers.call(this), this.externalDropping && this.externalDropping.unbindFromDocument()
				}, b.prototype.bindDateHandlerToEl = function (a, b, c) {
					var d = this;
					this.el.on(b, function (a) {
						if (!e(a.target).is(d.segSelector + "," + d.segSelector + " *,.fc-more,a[data-goto]")) return c.call(d, a)
					})
				}, b.prototype.bindAllSegHandlersToEl = function (a) {
					[this.eventPointing, this.eventDragging, this.eventResizing].forEach(function (b) {
						b && b.bindToEl(a)
					})
				}, b.prototype.bindSegHandlerToEl = function (a, b, c) {
					var d = this;
					a.on(b, this.segSelector, function (a) {
						var b = e(a.currentTarget).data("fc-seg");
						if (b && !d.shouldIgnoreEventPointing()) return c.call(d, b, a)
					})
				}, b.prototype.shouldIgnoreMouse = function () {
					return h.default.get().shouldIgnoreMouse()
				}, b.prototype.shouldIgnoreTouch = function () {
					var a = this._getView();
					return a.isSelected || a.selectedEvent
				}, b.prototype.shouldIgnoreEventPointing = function () {
					return this.eventDragging && this.eventDragging.isDragging || this.eventResizing && this.eventResizing.isResizing
				}, b.prototype.canStartSelection = function (a, b) {
					return f.getEvIsTouch(b) && !this.canStartResize(a, b) && (this.isEventDefDraggable(a.footprint.eventDef) || this.isEventDefResizable(a.footprint.eventDef))
				}, b.prototype.canStartDrag = function (a, b) {
					return !this.canStartResize(a, b) && this.isEventDefDraggable(a.footprint.eventDef)
				}, b.prototype.canStartResize = function (a, b) {
					var c = this._getView(),
						d = a.footprint.eventDef;
					return (!f.getEvIsTouch(b) || c.isEventDefSelected(d)) && this.isEventDefResizable(d) && e(b.target).is(".fc-resizer")
				}, b.prototype.endInteractions = function () {
					[this.dateClicking, this.dateSelecting, this.eventPointing, this.eventDragging, this.eventResizing].forEach(function (a) {
						a && a.end()
					})
				}, b.prototype.isEventDefDraggable = function (a) {
					return this.isEventDefStartEditable(a)
				}, b.prototype.isEventDefStartEditable = function (a) {
					var b = a.isStartExplicitlyEditable();
					return null == b && null == (b = this.opt("eventStartEditable")) && (b = this.isEventDefGenerallyEditable(a)), b
				}, b.prototype.isEventDefGenerallyEditable = function (a) {
					var b = a.isExplicitlyEditable();
					return null == b && (b = this.opt("editable")), b
				}, b.prototype.isEventDefResizableFromStart = function (a) {
					return this.opt("eventResizableFromStart") && this.isEventDefResizable(a)
				}, b.prototype.isEventDefResizableFromEnd = function (a) {
					return this.isEventDefResizable(a)
				}, b.prototype.isEventDefResizable = function (a) {
					var b = a.isDurationExplicitlyEditable();
					return null == b && null == (b = this.opt("eventDurationEditable")) && (b = this.isEventDefGenerallyEditable(a)), b
				}, b.prototype.diffDates = function (a, b) {
					return this.largeUnit ? f.diffByUnit(a, b, this.largeUnit) : f.diffDayTime(a, b)
				}, b.prototype.isEventInstanceGroupAllowed = function (a) {
					var b, c = this._getView(),
						d = this.dateProfile,
						e = this.eventRangesToEventFootprints(a.getAllEventRanges());
					for (b = 0; b < e.length; b++)
						if (!d.validUnzonedRange.containsRange(e[b].componentFootprint.unzonedRange)) return !1;
					return c.calendar.constraints.isEventInstanceGroupAllowed(a)
				}, b.prototype.isExternalInstanceGroupAllowed = function (a) {
					var b, c = this._getView(),
						d = this.dateProfile,
						e = this.eventRangesToEventFootprints(a.getAllEventRanges());
					for (b = 0; b < e.length; b++)
						if (!d.validUnzonedRange.containsRange(e[b].componentFootprint.unzonedRange)) return !1;
					for (b = 0; b < e.length; b++)
						if (!c.calendar.constraints.isSelectionFootprintAllowed(e[b].componentFootprint)) return !1;
					return !0
				}, b
			}(g.default);
		b.default = i
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(3),
			g = c(2),
			h = c(60),
			i = c(62),
			j = c(29),
			k = c(14),
			l = c(4),
			m = function (a) {
				function b(b, c) {
					var d = a.call(this, null, c.options) || this;
					return d.batchRenderDepth = 0, d.isSelected = !1, d.calendar = b, d.viewSpec = c, d.type = c.type, d.name = d.type, d.initRenderQueue(), d.initHiddenDays(), d.dateProfileGenerator = new d.dateProfileGeneratorClass(d), d.bindBaseRenderHandlers(), d.eventOrderSpecs = g.parseFieldSpecs(d.opt("eventOrder")), d.initialize && d.initialize(), d
				}
				return d.__extends(b, a), b.prototype._getView = function () {
					return this
				}, b.prototype.opt = function (a) {
					return this.options[a]
				}, b.prototype.initRenderQueue = function () {
					this.renderQueue = new h.default({
						event: this.opt("eventRenderWait")
					}), this.renderQueue.on("start", this.onRenderQueueStart.bind(this)), this.renderQueue.on("stop", this.onRenderQueueStop.bind(this)), this.on("before:change", this.startBatchRender), this.on("change", this.stopBatchRender)
				}, b.prototype.onRenderQueueStart = function () {
					this.calendar.freezeContentHeight(), this.addScroll(this.queryScroll())
				}, b.prototype.onRenderQueueStop = function () {
					this.calendar.updateViewSize() && this.popScroll(), this.calendar.thawContentHeight()
				}, b.prototype.startBatchRender = function () {
					this.batchRenderDepth++ || this.renderQueue.pause()
				}, b.prototype.stopBatchRender = function () {
					--this.batchRenderDepth || this.renderQueue.resume()
				}, b.prototype.requestRender = function (a, b, c) {
					this.renderQueue.queue(a, b, c)
				}, b.prototype.whenSizeUpdated = function (a) {
					this.renderQueue.isRunning ? this.renderQueue.one("stop", a.bind(this)) : a.call(this)
				}, b.prototype.computeTitle = function (a) {
					var b;
					return b = /^(year|month)$/.test(a.currentRangeUnit) ? a.currentUnzonedRange : a.activeUnzonedRange, this.formatRange({
						start: this.calendar.msToMoment(b.startMs, a.isRangeAllDay),
						end: this.calendar.msToMoment(b.endMs, a.isRangeAllDay)
					}, a.isRangeAllDay, this.opt("titleFormat") || this.computeTitleFormat(a), this.opt("titleRangeSeparator"))
				}, b.prototype.computeTitleFormat = function (a) {
					var b = a.currentRangeUnit;
					return "year" == b ? "YYYY" : "month" == b ? this.opt("monthYearFormat") : a.currentUnzonedRange.as("days") > 1 ? "ll" : "LL"
				}, b.prototype.setDate = function (a) {
					var b = this.get("dateProfile"),
						c = this.dateProfileGenerator.build(a, void 0, !0);
					b && b.activeUnzonedRange.equals(c.activeUnzonedRange) || this.set("dateProfile", c)
				}, b.prototype.unsetDate = function () {
					this.unset("dateProfile")
				}, b.prototype.fetchInitialEvents = function (a) {
					var b = this.calendar,
						c = a.isRangeAllDay && !this.usesMinMaxTime;
					return b.requestEvents(b.msToMoment(a.activeUnzonedRange.startMs, c), b.msToMoment(a.activeUnzonedRange.endMs, c))
				}, b.prototype.bindEventChanges = function () {
					this.listenTo(this.calendar, "eventsReset", this.resetEvents)
				}, b.prototype.unbindEventChanges = function () {
					this.stopListeningTo(this.calendar, "eventsReset")
				}, b.prototype.setEvents = function (a) {
					this.set("currentEvents", a), this.set("hasEvents", !0)
				}, b.prototype.unsetEvents = function () {
					this.unset("currentEvents"), this.unset("hasEvents")
				}, b.prototype.resetEvents = function (a) {
					this.startBatchRender(), this.unsetEvents(), this.setEvents(a), this.stopBatchRender()
				}, b.prototype.requestDateRender = function (a) {
					var b = this;
					this.requestRender(function () {
						b.executeDateRender(a)
					}, "date", "init")
				}, b.prototype.requestDateUnrender = function () {
					var a = this;
					this.requestRender(function () {
						a.executeDateUnrender()
					}, "date", "destroy")
				}, b.prototype.executeDateRender = function (b) {
					a.prototype.executeDateRender.call(this, b), this.render && this.render(), this.trigger("datesRendered"), this.addScroll({
						isDateInit: !0
					}), this.startNowIndicator()
				}, b.prototype.executeDateUnrender = function () {
					this.unselect(), this.stopNowIndicator(), this.trigger("before:datesUnrendered"), this.destroy && this.destroy(), a.prototype.executeDateUnrender.call(this)
				}, b.prototype.bindBaseRenderHandlers = function () {
					var a = this;
					this.on("datesRendered", function () {
						a.whenSizeUpdated(a.triggerViewRender)
					}), this.on("before:datesUnrendered", function () {
						a.triggerViewDestroy()
					})
				}, b.prototype.triggerViewRender = function () {
					this.publiclyTrigger("viewRender", {
						context: this,
						args: [this, this.el]
					})
				}, b.prototype.triggerViewDestroy = function () {
					this.publiclyTrigger("viewDestroy", {
						context: this,
						args: [this, this.el]
					})
				}, b.prototype.requestEventsRender = function (a) {
					var b = this;
					this.requestRender(function () {
						b.executeEventRender(a), b.whenSizeUpdated(b.triggerAfterEventsRendered)
					}, "event", "init")
				}, b.prototype.requestEventsUnrender = function () {
					var a = this;
					this.requestRender(function () {
						a.triggerBeforeEventsDestroyed(), a.executeEventUnrender()
					}, "event", "destroy")
				}, b.prototype.requestBusinessHoursRender = function (a) {
					var b = this;
					this.requestRender(function () {
						b.renderBusinessHours(a)
					}, "businessHours", "init")
				}, b.prototype.requestBusinessHoursUnrender = function () {
					var a = this;
					this.requestRender(function () {
						a.unrenderBusinessHours()
					}, "businessHours", "destroy")
				}, b.prototype.bindGlobalHandlers = function () {
					a.prototype.bindGlobalHandlers.call(this), this.listenTo(k.default.get(), {
						touchstart: this.processUnselect,
						mousedown: this.handleDocumentMousedown
					})
				}, b.prototype.unbindGlobalHandlers = function () {
					a.prototype.unbindGlobalHandlers.call(this), this.stopListeningTo(k.default.get())
				}, b.prototype.startNowIndicator = function () {
					var a, b, c, d = this;
					this.opt("nowIndicator") && (a = this.getNowIndicatorUnit()) && (b = g.proxy(this, "updateNowIndicator"), this.initialNowDate = this.calendar.getNow(), this.initialNowQueriedMs = +new Date, c = this.initialNowDate.clone().startOf(a).add(1, a) - this.initialNowDate, this.nowIndicatorTimeoutID = setTimeout(function () {
						d.nowIndicatorTimeoutID = null, b(), c = +f.duration(1, a), c = Math.max(100, c),
							d.nowIndicatorIntervalID = setInterval(b, c)
					}, c))
				}, b.prototype.updateNowIndicator = function () {
					this.isDatesRendered && this.initialNowDate && (this.unrenderNowIndicator(), this.renderNowIndicator(this.initialNowDate.clone().add((new Date).valueOf() - this.initialNowQueriedMs)), this.isNowIndicatorRendered = !0)
				}, b.prototype.stopNowIndicator = function () {
					this.isNowIndicatorRendered && (this.nowIndicatorTimeoutID && (clearTimeout(this.nowIndicatorTimeoutID), this.nowIndicatorTimeoutID = null), this.nowIndicatorIntervalID && (clearInterval(this.nowIndicatorIntervalID), this.nowIndicatorIntervalID = null), this.unrenderNowIndicator(), this.isNowIndicatorRendered = !1)
				}, b.prototype.updateSize = function (b, c, d) {
					this.setHeight ? this.setHeight(b, c) : a.prototype.updateSize.call(this, b, c, d), this.updateNowIndicator()
				}, b.prototype.addScroll = function (a) {
					var b = this.queuedScroll || (this.queuedScroll = {});
					e.extend(b, a)
				}, b.prototype.popScroll = function () {
					this.applyQueuedScroll(), this.queuedScroll = null
				}, b.prototype.applyQueuedScroll = function () {
					this.queuedScroll && this.applyScroll(this.queuedScroll)
				}, b.prototype.queryScroll = function () {
					var a = {};
					return this.isDatesRendered && e.extend(a, this.queryDateScroll()), a
				}, b.prototype.applyScroll = function (a) {
					a.isDateInit && this.isDatesRendered && e.extend(a, this.computeInitialDateScroll()), this.isDatesRendered && this.applyDateScroll(a)
				}, b.prototype.computeInitialDateScroll = function () {
					return {}
				}, b.prototype.queryDateScroll = function () {
					return {}
				}, b.prototype.applyDateScroll = function (a) {}, b.prototype.reportEventDrop = function (a, b, c, d) {
					var e = this.calendar.eventManager,
						g = e.mutateEventsWithId(a.def.id, b, this.calendar),
						h = b.dateMutation;
					h && (a.dateProfile = h.buildNewDateProfile(a.dateProfile, this.calendar)), this.triggerEventDrop(a, h && h.dateDelta || f.duration(), g, c, d)
				}, b.prototype.triggerEventDrop = function (a, b, c, d, e) {
					this.publiclyTrigger("eventDrop", {
						context: d[0],
						args: [a.toLegacy(), b, c, e, {}, this]
					})
				}, b.prototype.reportExternalDrop = function (a, b, c, d, e, f) {
					b && this.calendar.eventManager.addEventDef(a, c), this.triggerExternalDrop(a, b, d, e, f)
				}, b.prototype.triggerExternalDrop = function (a, b, c, d, e) {
					this.publiclyTrigger("drop", {
						context: c[0],
						args: [a.dateProfile.start.clone(), d, e, this]
					}), b && this.publiclyTrigger("eventReceive", {
						context: this,
						args: [a.buildInstance().toLegacy(), this]
					})
				}, b.prototype.reportEventResize = function (a, b, c, d) {
					var e = this.calendar.eventManager,
						f = e.mutateEventsWithId(a.def.id, b, this.calendar);
					a.dateProfile = b.dateMutation.buildNewDateProfile(a.dateProfile, this.calendar), this.triggerEventResize(a, b.dateMutation.endDelta, f, c, d)
				}, b.prototype.triggerEventResize = function (a, b, c, d, e) {
					this.publiclyTrigger("eventResize", {
						context: d[0],
						args: [a.toLegacy(), b, c, e, {}, this]
					})
				}, b.prototype.select = function (a, b) {
					this.unselect(b), this.renderSelectionFootprint(a), this.reportSelection(a, b)
				}, b.prototype.renderSelectionFootprint = function (b) {
					this.renderSelection ? this.renderSelection(b.toLegacy(this.calendar)) : a.prototype.renderSelectionFootprint.call(this, b)
				}, b.prototype.reportSelection = function (a, b) {
					this.isSelected = !0, this.triggerSelect(a, b)
				}, b.prototype.triggerSelect = function (a, b) {
					var c = this.calendar.footprintToDateProfile(a);
					this.publiclyTrigger("select", {
						context: this,
						args: [c.start, c.end, b, this]
					})
				}, b.prototype.unselect = function (a) {
					void 0 === a && (a = null), this.isSelected && (this.isSelected = !1, this.destroySelection && this.destroySelection(), this.unrenderSelection(), this.publiclyTrigger("unselect", {
						context: this,
						args: [a, this]
					}))
				}, b.prototype.selectEventInstance = function (a) {
					this.selectedEventInstance && this.selectedEventInstance === a || (this.unselectEventInstance(), this.getEventSegs().forEach(function (b) {
						b.footprint.eventInstance === a && b.el && b.el.addClass("fc-selected")
					}), this.selectedEventInstance = a)
				}, b.prototype.unselectEventInstance = function () {
					this.selectedEventInstance && (this.getEventSegs().forEach(function (a) {
						a.el && a.el.removeClass("fc-selected")
					}), this.selectedEventInstance = null)
				}, b.prototype.isEventDefSelected = function (a) {
					return this.selectedEventInstance && this.selectedEventInstance.def.id === a.id
				}, b.prototype.handleDocumentMousedown = function (a) {
					g.isPrimaryMouseButton(a) && this.processUnselect(a)
				}, b.prototype.processUnselect = function (a) {
					this.processRangeUnselect(a), this.processEventUnselect(a)
				}, b.prototype.processRangeUnselect = function (a) {
					var b;
					this.isSelected && this.opt("unselectAuto") && ((b = this.opt("unselectCancel")) && e(a.target).closest(b).length || this.unselect(a))
				}, b.prototype.processEventUnselect = function (a) {
					this.selectedEventInstance && (e(a.target).closest(".fc-selected").length || this.unselectEventInstance())
				}, b.prototype.triggerBaseRendered = function () {
					this.publiclyTrigger("viewRender", {
						context: this,
						args: [this, this.el]
					})
				}, b.prototype.triggerBaseUnrendered = function () {
					this.publiclyTrigger("viewDestroy", {
						context: this,
						args: [this, this.el]
					})
				}, b.prototype.triggerDayClick = function (a, b, c) {
					var d = this.calendar.footprintToDateProfile(a);
					this.publiclyTrigger("dayClick", {
						context: b,
						args: [d.start, c, this]
					})
				}, b.prototype.isDateInOtherMonth = function (a, b) {
					return !1
				}, b.prototype.getUnzonedRangeOption = function (a) {
					var b = this.opt(a);
					if ("function" == typeof b && (b = b.apply(null, Array.prototype.slice.call(arguments, 1))), b) return this.calendar.parseUnzonedRange(b)
				}, b.prototype.initHiddenDays = function () {
					var a, b = this.opt("hiddenDays") || [],
						c = [],
						d = 0;
					for (!1 === this.opt("weekends") && b.push(0, 6), a = 0; a < 7; a++)(c[a] = -1 !== e.inArray(a, b)) || d++;
					if (!d) throw "invalid hiddenDays";
					this.isHiddenDayHash = c
				}, b.prototype.trimHiddenDays = function (a) {
					var b = a.getStart(),
						c = a.getEnd();
					return b && (b = this.skipHiddenDays(b)), c && (c = this.skipHiddenDays(c, -1, !0)), null === b || null === c || b < c ? new l.default(b, c) : null
				}, b.prototype.isHiddenDay = function (a) {
					return f.isMoment(a) && (a = a.day()), this.isHiddenDayHash[a]
				}, b.prototype.skipHiddenDays = function (a, b, c) {
					void 0 === b && (b = 1), void 0 === c && (c = !1);
					for (var d = a.clone(); this.isHiddenDayHash[(d.day() + (c ? b : 0) + 7) % 7];) d.add(b, "days");
					return d
				}, b
			}(j.default);
		b.default = m, m.prototype.usesMinMaxTime = !1, m.prototype.dateProfileGeneratorClass = i.default, m.watch("displayingDates", ["isInDom", "dateProfile"], function (a) {
			this.requestDateRender(a.dateProfile)
		}, function () {
			this.requestDateUnrender()
		}), m.watch("displayingBusinessHours", ["displayingDates", "businessHourGenerator"], function (a) {
			this.requestBusinessHoursRender(a.businessHourGenerator)
		}, function () {
			this.requestBusinessHoursUnrender()
		}), m.watch("initialEvents", ["dateProfile"], function (a) {
			return this.fetchInitialEvents(a.dateProfile)
		}), m.watch("bindingEvents", ["initialEvents"], function (a) {
			this.setEvents(a.initialEvents), this.bindEventChanges()
		}, function () {
			this.unbindEventChanges(), this.unsetEvents()
		}), m.watch("displayingEvents", ["displayingDates", "hasEvents"], function () {
			this.requestEventsRender(this.get("currentEvents"))
		}, function () {
			this.requestEventsUnrender()
		}), m.watch("title", ["dateProfile"], function (a) {
			return this.title = this.computeTitle(a.dateProfile)
		}), m.watch("legacyDateProps", ["dateProfile"], function (a) {
			var b = this.calendar,
				c = a.dateProfile;
			this.start = b.msToMoment(c.activeUnzonedRange.startMs, c.isRangeAllDay), this.end = b.msToMoment(c.activeUnzonedRange.endMs, c.isRangeAllDay), this.intervalStart = b.msToMoment(c.currentUnzonedRange.startMs, c.isRangeAllDay), this.intervalEnd = b.msToMoment(c.currentUnzonedRange.endMs, c.isRangeAllDay)
		})
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = function () {
				function a(a, b) {
					this.view = a._getView(), this.component = a, this.fillRenderer = b
				}
				return a.prototype.opt = function (a) {
					return this.view.opt(a)
				}, a.prototype.rangeUpdated = function () {
					var a, b;
					this.eventTimeFormat = this.opt("eventTimeFormat") || this.opt("timeFormat") || this.computeEventTimeFormat(), a = this.opt("displayEventTime"), null == a && (a = this.computeDisplayEventTime()), b = this.opt("displayEventEnd"), null == b && (b = this.computeDisplayEventEnd()), this.displayEventTime = a, this.displayEventEnd = b
				}, a.prototype.render = function (a) {
					var b, c, d, e = this.component._getDateProfile(),
						f = [],
						g = [];
					for (b in a) c = a[b], d = c.sliceRenderRanges(e.activeUnzonedRange), c.getEventDef().hasBgRendering() ? f.push.apply(f, d) : g.push.apply(g, d);
					this.renderBgRanges(f), this.renderFgRanges(g)
				}, a.prototype.unrender = function () {
					this.unrenderBgRanges(), this.unrenderFgRanges()
				}, a.prototype.renderFgRanges = function (a) {
					var b = this.component.eventRangesToEventFootprints(a),
						c = this.component.eventFootprintsToSegs(b);
					c = this.renderFgSegEls(c), !1 !== this.renderFgSegs(c) && (this.fgSegs = c)
				}, a.prototype.unrenderFgRanges = function () {
					this.unrenderFgSegs(this.fgSegs || []), this.fgSegs = null
				}, a.prototype.renderBgRanges = function (a) {
					var b = this.component.eventRangesToEventFootprints(a),
						c = this.component.eventFootprintsToSegs(b);
					!1 !== this.renderBgSegs(c) && (this.bgSegs = c)
				}, a.prototype.unrenderBgRanges = function () {
					this.unrenderBgSegs(), this.bgSegs = null
				}, a.prototype.getSegs = function () {
					return (this.bgSegs || []).concat(this.fgSegs || [])
				}, a.prototype.renderFgSegs = function (a) {
					return !1
				}, a.prototype.unrenderFgSegs = function (a) {}, a.prototype.renderBgSegs = function (a) {
					var b = this;
					if (!this.fillRenderer) return !1;
					this.fillRenderer.renderSegs("bgEvent", a, {
						getClasses: function (a) {
							return b.getBgClasses(a.footprint.eventDef)
						},
						getCss: function (a) {
							return {
								"background-color": b.getBgColor(a.footprint.eventDef)
							}
						},
						filterEl: function (a, c) {
							return b.filterEventRenderEl(a.footprint, c)
						}
					})
				}, a.prototype.unrenderBgSegs = function () {
					this.fillRenderer && this.fillRenderer.unrender("bgEvent")
				}, a.prototype.renderFgSegEls = function (a, b) {
					var c = this;
					void 0 === b && (b = !1);
					var e, f = this.view.hasPublicHandlers("eventRender"),
						g = "",
						h = [];
					if (a.length) {
						for (e = 0; e < a.length; e++) this.beforeFgSegHtml(a[e]), g += this.fgSegHtml(a[e], b);
						d(g).each(function (b, e) {
							var g = a[b],
								i = d(e);
							f && (i = c.filterEventRenderEl(g.footprint, i)), i && (i.data("fc-seg", g), g.el = i, h.push(g))
						})
					}
					return h
				}, a.prototype.beforeFgSegHtml = function (a) {}, a.prototype.fgSegHtml = function (a, b) {}, a.prototype.getSegClasses = function (a, b, c) {
					var d = ["fc-event", a.isStart ? "fc-start" : "fc-not-start", a.isEnd ? "fc-end" : "fc-not-end"].concat(this.getClasses(a.footprint.eventDef));
					return b && d.push("fc-draggable"), c && d.push("fc-resizable"), this.view.isEventDefSelected(a.footprint.eventDef) && d.push("fc-selected"), d
				}, a.prototype.filterEventRenderEl = function (a, b) {
					var c = a.getEventLegacy(),
						e = this.view.publiclyTrigger("eventRender", {
							context: c,
							args: [c, b, this.view]
						});
					return !1 === e ? b = null : e && !0 !== e && (b = d(e)), b
				}, a.prototype.getTimeText = function (a, b, c) {
					return this._getTimeText(a.eventInstance.dateProfile.start, a.eventInstance.dateProfile.end, a.componentFootprint.isAllDay, b, c)
				}, a.prototype._getTimeText = function (a, b, c, d, e) {
					return null == d && (d = this.eventTimeFormat), null == e && (e = this.displayEventEnd), this.displayEventTime && !c ? e && b ? this.view.formatRange({
						start: a,
						end: b
					}, !1, d) : a.format(d) : ""
				}, a.prototype.computeEventTimeFormat = function () {
					return this.opt("smallTimeFormat")
				}, a.prototype.computeDisplayEventTime = function () {
					return !0
				}, a.prototype.computeDisplayEventEnd = function () {
					return !0
				}, a.prototype.getBgClasses = function (a) {
					var b = this.getClasses(a);
					return b.push("fc-bgevent"), b
				}, a.prototype.getClasses = function (a) {
					var b, c = this.getStylingObjs(a),
						d = [];
					for (b = 0; b < c.length; b++) d.push.apply(d, c[b].eventClassName || c[b].className || []);
					return d
				}, a.prototype.getSkinCss = function (a) {
					return {
						"background-color": this.getBgColor(a),
						"border-color": this.getBorderColor(a),
						color: this.getTextColor(a)
					}
				}, a.prototype.getBgColor = function (a) {
					var b, c, d = this.getStylingObjs(a);
					for (b = 0; b < d.length && !c; b++) c = d[b].eventBackgroundColor || d[b].eventColor || d[b].backgroundColor || d[b].color;
					return c || (c = this.opt("eventBackgroundColor") || this.opt("eventColor")), c
				}, a.prototype.getBorderColor = function (a) {
					var b, c, d = this.getStylingObjs(a);
					for (b = 0; b < d.length && !c; b++) c = d[b].eventBorderColor || d[b].eventColor || d[b].borderColor || d[b].color;
					return c || (c = this.opt("eventBorderColor") || this.opt("eventColor")), c
				}, a.prototype.getTextColor = function (a) {
					var b, c, d = this.getStylingObjs(a);
					for (b = 0; b < d.length && !c; b++) c = d[b].eventTextColor || d[b].textColor;
					return c || (c = this.opt("eventTextColor")), c
				}, a.prototype.getStylingObjs = function (a) {
					var b = this.getFallbackStylingObjs(a);
					return b.unshift(a), b
				}, a.prototype.getFallbackStylingObjs = function (a) {
					return [a.source]
				}, a.prototype.sortEventSegs = function (a) {
					a.sort(e.proxy(this, "compareEventSegs"))
				}, a.prototype.compareEventSegs = function (a, b) {
					var c = a.footprint.componentFootprint,
						d = c.unzonedRange,
						f = b.footprint.componentFootprint,
						g = f.unzonedRange;
					return d.startMs - g.startMs || g.endMs - g.startMs - (d.endMs - d.startMs) || f.isAllDay - c.isAllDay || e.compareByFieldSpecs(a.footprint.eventDef, b.footprint.eventDef, this.view.eventOrderSpecs)
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(21),
			f = c(8),
			g = c(6),
			h = function (a) {
				function b() {
					var b = a.call(this) || this;
					return b._watchers = {}, b._props = {}, b.applyGlobalWatchers(), b.constructed(), b
				}
				return d.__extends(b, a), b.prototype.constructed = function () {}, b.prototype.applyGlobalWatchers = function () {
					var a, b = this._globalWatchArgs;
					for (a in b) this.watch.apply(this, [a].concat(b[a]))
				}, b.prototype.has = function (a) {
					return a in this._props
				}, b.prototype.get = function (a) {
					return void 0 === a ? this._props : this._props[a]
				}, b.prototype.set = function (a, b) {
					var c;
					"string" == typeof a ? (c = {}, c[a] = void 0 === b ? null : b) : c = a, this.setProps(c)
				}, b.prototype.reset = function (a) {
					var b, c = this._props,
						d = {};
					for (b in c) d[b] = void 0;
					for (b in a) d[b] = a[b];
					this.setProps(d)
				}, b.prototype.unset = function (a) {
					var b, c, d = {};
					for (b = "string" == typeof a ? [a] : a, c = 0; c < b.length; c++) d[b[c]] = void 0;
					this.setProps(d)
				}, b.prototype.setProps = function (a) {
					var b, c, d = {},
						e = 0;
					for (b in a) "object" != typeof (c = a[b]) && c === this._props[b] || (d[b] = c, e++);
					if (e) {
						this.trigger("before:batchChange", d);
						for (b in d) c = d[b], this.trigger("before:change", b, c), this.trigger("before:change:" + b, c);
						for (b in d) c = d[b], void 0 === c ? delete this._props[b] : this._props[b] = c, this.trigger("change:" + b, c), this.trigger("change", b, c);
						this.trigger("batchChange", d)
					}
				}, b.prototype.watch = function (a, b, c, d) {
					var e = this;
					this.unwatch(a), this._watchers[a] = this._watchDeps(b, function (b) {
						var d = c.call(e, b);
						d && d.then ? (e.unset(a), d.then(function (b) {
							e.set(a, b)
						})) : e.set(a, d)
					}, function (b) {
						e.unset(a), d && d.call(e, b)
					})
				}, b.prototype.unwatch = function (a) {
					var b = this._watchers[a];
					b && (delete this._watchers[a], b.teardown())
				}, b.prototype._watchDeps = function (a, b, c) {
					var d = this,
						e = 0,
						f = a.length,
						g = 0,
						h = {},
						i = [],
						j = !1,
						k = function (a, b, d) {
							1 == ++e && g === f && (j = !0, c(h), j = !1)
						},
						l = function (a, c, d) {
							void 0 === c ? (d || void 0 === h[a] || g--, delete h[a]) : (d || void 0 !== h[a] || g++, h[a] = c), --e || g === f && (j || b(h))
						},
						m = function (a, b) {
							d.on(a, b), i.push([a, b])
						};
					return a.forEach(function (a) {
						var b = !1;
						"?" === a.charAt(0) && (a = a.substring(1), b = !0), m("before:change:" + a, function (a) {
							k()
						}), m("change:" + a, function (c) {
							l(a, c, b)
						})
					}), a.forEach(function (a) {
						var b = !1;
						"?" === a.charAt(0) && (a = a.substring(1), b = !0), d.has(a) ? (h[a] = d.get(a), g++) : b && g++
					}), g === f && b(h), {
						teardown: function () {
							for (var a = 0; a < i.length; a++) d.off(i[a][0], i[a][1]);
							i = null, g === f && c()
						},
						flash: function () {
							g === f && (c(), b(h))
						}
					}
				}, b.prototype.flash = function (a) {
					var b = this._watchers[a];
					b && b.flash()
				}, b.watch = function (a) {
					for (var b = [], c = 1; c < arguments.length; c++) b[c - 1] = arguments[c];
					this.prototype.hasOwnProperty("_globalWatchArgs") || (this.prototype._globalWatchArgs = Object.create(this.prototype._globalWatchArgs)), this.prototype._globalWatchArgs[a] = b
				}, b
			}(e.default);
		b.default = h, h.prototype._globalWatchArgs = {}, f.default.mixInto(h), g.default.mixInto(h)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(3),
			e = c(2),
			f = c(11),
			g = c(52);
		b.default = {
			parse: function (a, b) {
				return e.isTimeString(a.start) || d.isDuration(a.start) || e.isTimeString(a.end) || d.isDuration(a.end) ? g.default.parse(a, b) : f.default.parse(a, b)
			}
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(16),
			h = c(5),
			i = c(11),
			j = function (a) {
				function b(b) {
					var c = a.call(this, b) || this;
					return c.eventDefs = [], c
				}
				return d.__extends(b, a), b.prototype.setRawEventDefs = function (a) {
					this.rawEventDefs = a, this.eventDefs = this.parseEventDefs(a)
				}, b.prototype.fetch = function (a, b, c) {
					var d, e = this.eventDefs;
					if (null != this.currentTimezone && this.currentTimezone !== c)
						for (d = 0; d < e.length; d++) e[d] instanceof i.default && e[d].rezone();
					return this.currentTimezone = c, g.default.resolve(e)
				}, b.prototype.addEventDef = function (a) {
					this.eventDefs.push(a)
				}, b.prototype.removeEventDefsById = function (a) {
					return f.removeMatching(this.eventDefs, function (b) {
						return b.id === a
					})
				}, b.prototype.removeAllEventDefs = function () {
					this.eventDefs = []
				}, b.prototype.getPrimitive = function () {
					return this.rawEventDefs
				}, b.prototype.applyManualStandardProps = function (b) {
					var c = a.prototype.applyManualStandardProps.call(this, b);
					return this.setRawEventDefs(b.events), c
				}, b.parse = function (a, b) {
					var c;
					return e.isArray(a.events) ? c = a : e.isArray(a) && (c = {
						events: a
					}), !!c && h.default.parse.call(this, c, b)
				}, b
			}(h.default);
		b.default = j, j.defineStandardProps({
			events: !1
		})
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(2),
			e = c(15),
			f = function () {
				function a() {
					this.clearEnd = !1, this.forceTimed = !1, this.forceAllDay = !1
				}
				return a.prototype.buildNewDateProfile = function (a, b) {
					var c = a.start.clone(),
						d = null,
						f = !1;
					return a.end && !this.clearEnd ? d = a.end.clone() : this.endDelta && !d && (d = b.getDefaultEventEnd(a.isAllDay(), c)), this.forceTimed ? (f = !0, c.hasTime() || c.time(0), d && !d.hasTime() && d.time(0)) : this.forceAllDay && (c.hasTime() && c.stripTime(), d && d.hasTime() && d.stripTime()), this.dateDelta && (f = !0, c.add(this.dateDelta), d && d.add(this.dateDelta)), this.endDelta && (f = !0, d.add(this.endDelta)), this.startDelta && (f = !0, c.add(this.startDelta)), f && (c = b.applyTimezone(c), d && (d = b.applyTimezone(d))), !d && b.opt("forceEventDuration") && (d = b.getDefaultEventEnd(a.isAllDay(), c)), new e.default(c, d, b)
				}, a.prototype.setDateDelta = function (a) {
					a && a.valueOf() ? this.dateDelta = a : this.dateDelta = null
				}, a.prototype.setStartDelta = function (a) {
					a && a.valueOf() ? this.startDelta = a : this.startDelta = null
				}, a.prototype.setEndDelta = function (a) {
					a && a.valueOf() ? this.endDelta = a : this.endDelta = null
				}, a.prototype.isEmpty = function () {
					return !(this.clearEnd || this.forceTimed || this.forceAllDay || this.dateDelta || this.startDelta || this.endDelta)
				}, a.createFromDiff = function (b, c, e) {
					function f(a, b) {
						return e ? d.diffByUnit(a, b, e) : c.isAllDay() ? d.diffDay(a, b) : d.diffDayTime(a, b)
					}
					var g, h, i, j, k = b.end && !c.end,
						l = b.isAllDay() && !c.isAllDay(),
						m = !b.isAllDay() && c.isAllDay();
					return g = f(c.start, b.start), c.end && (h = f(c.unzonedRange.getEnd(), b.unzonedRange.getEnd()), i = h.subtract(g)), j = new a, j.clearEnd = k, j.forceTimed = l, j.forceAllDay = m, j.setDateDelta(g), j.setEndDelta(i), j
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(55),
			e = c(56);
		b.default = {
			themeClassHash: {},
			register: function (a, b) {
				this.themeClassHash[a] = b
			},
			getThemeClass: function (a) {
				return a ? !0 === a ? e.default : this.themeClassHash[a] : d.default
			}
		}
	}, function (a, b, c) {
		function d(a) {
			return "en" !== a.locale() ? a.clone().locale("en") : a
		}

		function e(a, b) {
			return n(h(b).fakeFormatString, a)
		}

		function f(a, b, c, d, e) {
			var f;
			return a = r.default.parseZone(a), b = r.default.parseZone(b), f = a.localeData(), c = f.longDateFormat(c) || c, g(h(c), a, b, d || " - ", e)
		}

		function g(a, b, c, d, e) {
			var f, g, h, i = a.sameUnits,
				j = b.clone().stripZone(),
				k = c.clone().stripZone(),
				l = o(a.fakeFormatString, b),
				m = o(a.fakeFormatString, c),
				n = "",
				q = "",
				r = "",
				s = "",
				t = "";
			for (f = 0; f < i.length && (!i[f] || j.isSame(k, i[f])); f++) n += l[f];
			for (g = i.length - 1; g > f && (!i[g] || j.isSame(k, i[g])) && (g - 1 !== f || "." !== l[g]); g--) q = l[g] + q;
			for (h = f; h <= g; h++) r += l[h], s += m[h];
			return (r || s) && (t = e ? s + d + r : r + d + s), p(n + t + q)
		}

		function h(a) {
			return y[a] || (y[a] = i(a))
		}

		function i(a) {
			var b = j(a);
			return {
				fakeFormatString: l(b),
				sameUnits: m(b)
			}
		}

		function j(a) {
			for (var b, c = [], d = /\[([^\]]*)\]|\(([^\)]*)\)|(LTS|LT|(\w)\4*o?)|([^\w\[\(]+)/g; b = d.exec(a);) b[1] ? c.push.apply(c, k(b[1])) : b[2] ? c.push({
				maybe: j(b[2])
			}) : b[3] ? c.push({
				token: b[3]
			}) : b[5] && c.push.apply(c, k(b[5]));
			return c
		}

		function k(a) {
			return ". " === a ? [".", " "] : [a]
		}

		function l(a) {
			var b, c, d = [];
			for (b = 0; b < a.length; b++) c = a[b], "string" == typeof c ? d.push("[" + c + "]") : c.token ? c.token in w ? d.push(t + "[" + c.token + "]") : d.push(c.token) : c.maybe && d.push(u + l(c.maybe) + u);
			return d.join(s)
		}

		function m(a) {
			var b, c, d, e = [];
			for (b = 0; b < a.length; b++) c = a[b], c.token ? (d = x[c.token.charAt(0)], e.push(d ? d.unit : "second")) : c.maybe ? e.push.apply(e, m(c.maybe)) : e.push(null);
			return e
		}

		function n(a, b) {
			return p(o(a, b).join(""))
		}

		function o(a, b) {
			var c, d, e = [],
				f = r.oldMomentFormat(b, a),
				g = f.split(s);
			for (c = 0; c < g.length; c++) d = g[c], d.charAt(0) === t ? e.push(w[d.substring(1)](b)) : e.push(d);
			return e
		}

		function p(a) {
			return a.replace(v, function (a, b) {
				return b.match(/[1-9]/) ? b : ""
			})
		}

		function q(a) {
			var b, c, d, e, f = j(a);
			for (b = 0; b < f.length; b++) c = f[b], c.token && (d = x[c.token.charAt(0)]) && (!e || d.value > e.value) && (e = d);
			return e ? e.unit : null
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var r = c(9);
		r.newMomentProto.format = function () {
			return this._fullCalendar && arguments[0] ? e(this, arguments[0]) : this._ambigTime ? r.oldMomentFormat(d(this), "YYYY-MM-DD") : this._ambigZone ? r.oldMomentFormat(d(this), "YYYY-MM-DD[T]HH:mm:ss") : this._fullCalendar ? r.oldMomentFormat(d(this)) : r.oldMomentProto.format.apply(this, arguments)
		}, r.newMomentProto.toISOString = function () {
			return this._ambigTime ? r.oldMomentFormat(d(this), "YYYY-MM-DD") : this._ambigZone ? r.oldMomentFormat(d(this), "YYYY-MM-DD[T]HH:mm:ss") : this._fullCalendar ? r.oldMomentProto.toISOString.apply(d(this), arguments) : r.oldMomentProto.toISOString.apply(this, arguments)
		};
		var s = "\v",
			t = "",
			u = "",
			v = new RegExp(u + "([^" + u + "]*)" + u, "g"),
			w = {
				t: function (a) {
					return r.oldMomentFormat(a, "a").charAt(0)
				},
				T: function (a) {
					return r.oldMomentFormat(a, "A").charAt(0)
				}
			},
			x = {
				Y: {
					value: 1,
					unit: "year"
				},
				M: {
					value: 2,
					unit: "month"
				},
				W: {
					value: 3,
					unit: "week"
				},
				w: {
					value: 3,
					unit: "week"
				},
				D: {
					value: 4,
					unit: "day"
				},
				d: {
					value: 4,
					unit: "day"
				}
			};
		b.formatDate = e, b.formatRange = f;
		var y = {};
		b.queryMostGranularFormatUnit = q
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = function () {
				function a(a) {
					this.isHorizontal = !1, this.isVertical = !1, this.els = d(a.els), this.isHorizontal = a.isHorizontal, this.isVertical = a.isVertical, this.forcedOffsetParentEl = a.offsetParent ? d(a.offsetParent) : null
				}
				return a.prototype.build = function () {
					var a = this.forcedOffsetParentEl;
					!a && this.els.length > 0 && (a = this.els.eq(0).offsetParent()), this.origin = a ? a.offset() : null, this.boundingRect = this.queryBoundingRect(), this.isHorizontal && this.buildElHorizontals(), this.isVertical && this.buildElVerticals()
				}, a.prototype.clear = function () {
					this.origin = null, this.boundingRect = null, this.lefts = null, this.rights = null, this.tops = null, this.bottoms = null
				}, a.prototype.ensureBuilt = function () {
					this.origin || this.build()
				}, a.prototype.buildElHorizontals = function () {
					var a = [],
						b = [];
					this.els.each(function (c, e) {
						var f = d(e),
							g = f.offset().left,
							h = f.outerWidth();
						a.push(g), b.push(g + h)
					}), this.lefts = a, this.rights = b
				}, a.prototype.buildElVerticals = function () {
					var a = [],
						b = [];
					this.els.each(function (c, e) {
						var f = d(e),
							g = f.offset().top,
							h = f.outerHeight();
						a.push(g), b.push(g + h)
					}), this.tops = a, this.bottoms = b
				}, a.prototype.getHorizontalIndex = function (a) {
					this.ensureBuilt();
					var b, c = this.lefts,
						d = this.rights,
						e = c.length;
					for (b = 0; b < e; b++)
						if (a >= c[b] && a < d[b]) return b
				}, a.prototype.getVerticalIndex = function (a) {
					this.ensureBuilt();
					var b, c = this.tops,
						d = this.bottoms,
						e = c.length;
					for (b = 0; b < e; b++)
						if (a >= c[b] && a < d[b]) return b
				}, a.prototype.getLeftOffset = function (a) {
					return this.ensureBuilt(), this.lefts[a]
				}, a.prototype.getLeftPosition = function (a) {
					return this.ensureBuilt(), this.lefts[a] - this.origin.left
				}, a.prototype.getRightOffset = function (a) {
					return this.ensureBuilt(), this.rights[a]
				}, a.prototype.getRightPosition = function (a) {
					return this.ensureBuilt(), this.rights[a] - this.origin.left
				}, a.prototype.getWidth = function (a) {
					return this.ensureBuilt(), this.rights[a] - this.lefts[a]
				}, a.prototype.getTopOffset = function (a) {
					return this.ensureBuilt(), this.tops[a]
				}, a.prototype.getTopPosition = function (a) {
					return this.ensureBuilt(), this.tops[a] - this.origin.top
				}, a.prototype.getBottomOffset = function (a) {
					return this.ensureBuilt(), this.bottoms[a]
				}, a.prototype.getBottomPosition = function (a) {
					return this.ensureBuilt(), this.bottoms[a] - this.origin.top
				}, a.prototype.getHeight = function (a) {
					return this.ensureBuilt(), this.bottoms[a] - this.tops[a]
				}, a.prototype.queryBoundingRect = function () {
					var a;
					return this.els.length > 0 && (a = e.getScrollParent(this.els.eq(0)), !a.is(document)) ? e.getClientRect(a) : null
				}, a.prototype.isPointInBounds = function (a, b) {
					return this.isLeftInBounds(a) && this.isTopInBounds(b)
				}, a.prototype.isLeftInBounds = function (a) {
					return !this.boundingRect || a >= this.boundingRect.left && a < this.boundingRect.right
				}, a.prototype.isTopInBounds = function (a) {
					return !this.boundingRect || a >= this.boundingRect.top && a < this.boundingRect.bottom
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = c(6),
			g = c(14),
			h = function () {
				function a(a) {
					this.isInteracting = !1, this.isDistanceSurpassed = !1, this.isDelayEnded = !1, this.isDragging = !1, this.isTouch = !1, this.isGeneric = !1, this.shouldCancelTouchScroll = !0, this.scrollAlwaysKills = !1, this.isAutoScroll = !1, this.scrollSensitivity = 30, this.scrollSpeed = 200, this.scrollIntervalMs = 50, this.options = a || {}
				}
				return a.prototype.startInteraction = function (a, b) {
					if (void 0 === b && (b = {}), "mousedown" === a.type) {
						if (g.default.get().shouldIgnoreMouse()) return;
						if (!e.isPrimaryMouseButton(a)) return;
						a.preventDefault()
					}
					this.isInteracting || (this.delay = e.firstDefined(b.delay, this.options.delay, 0), this.minDistance = e.firstDefined(b.distance, this.options.distance, 0), this.subjectEl = this.options.subjectEl, e.preventSelection(d("body")), this.isInteracting = !0, this.isTouch = e.getEvIsTouch(a), this.isGeneric = "dragstart" === a.type, this.isDelayEnded = !1, this.isDistanceSurpassed = !1, this.originX = e.getEvX(a), this.originY = e.getEvY(a), this.scrollEl = e.getScrollParent(d(a.target)), this.bindHandlers(), this.initAutoScroll(), this.handleInteractionStart(a), this.startDelay(a), this.minDistance || this.handleDistanceSurpassed(a))
				}, a.prototype.handleInteractionStart = function (a) {
					this.trigger("interactionStart", a)
				}, a.prototype.endInteraction = function (a, b) {
					this.isInteracting && (this.endDrag(a), this.delayTimeoutId && (clearTimeout(this.delayTimeoutId), this.delayTimeoutId = null), this.destroyAutoScroll(), this.unbindHandlers(), this.isInteracting = !1, this.handleInteractionEnd(a, b), e.allowSelection(d("body")))
				}, a.prototype.handleInteractionEnd = function (a, b) {
					this.trigger("interactionEnd", a, b || !1)
				}, a.prototype.bindHandlers = function () {
					var a = g.default.get();
					this.isGeneric ? this.listenTo(d(document), {
						drag: this.handleMove,
						dragstop: this.endInteraction
					}) : this.isTouch ? this.listenTo(a, {
						touchmove: this.handleTouchMove,
						touchend: this.endInteraction,
						scroll: this.handleTouchScroll
					}) : this.listenTo(a, {
						mousemove: this.handleMouseMove,
						mouseup: this.endInteraction
					}), this.listenTo(a, {
						selectstart: e.preventDefault,
						contextmenu: e.preventDefault
					})
				}, a.prototype.unbindHandlers = function () {
					this.stopListeningTo(g.default.get()), this.stopListeningTo(d(document))
				}, a.prototype.startDrag = function (a, b) {
					this.startInteraction(a, b), this.isDragging || (this.isDragging = !0, this.handleDragStart(a))
				}, a.prototype.handleDragStart = function (a) {
					this.trigger("dragStart", a)
				}, a.prototype.handleMove = function (a) {
					var b = e.getEvX(a) - this.originX,
						c = e.getEvY(a) - this.originY,
						d = this.minDistance;
					this.isDistanceSurpassed || b * b + c * c >= d * d && this.handleDistanceSurpassed(a), this.isDragging && this.handleDrag(b, c, a)
				}, a.prototype.handleDrag = function (a, b, c) {
					this.trigger("drag", a, b, c), this.updateAutoScroll(c)
				}, a.prototype.endDrag = function (a) {
					this.isDragging && (this.isDragging = !1, this.handleDragEnd(a))
				}, a.prototype.handleDragEnd = function (a) {
					this.trigger("dragEnd", a)
				}, a.prototype.startDelay = function (a) {
					var b = this;
					this.delay ? this.delayTimeoutId = setTimeout(function () {
						b.handleDelayEnd(a)
					}, this.delay) : this.handleDelayEnd(a)
				}, a.prototype.handleDelayEnd = function (a) {
					this.isDelayEnded = !0, this.isDistanceSurpassed && this.startDrag(a)
				}, a.prototype.handleDistanceSurpassed = function (a) {
					this.isDistanceSurpassed = !0, this.isDelayEnded && this.startDrag(a)
				}, a.prototype.handleTouchMove = function (a) {
					this.isDragging && this.shouldCancelTouchScroll && a.preventDefault(), this.handleMove(a)
				}, a.prototype.handleMouseMove = function (a) {
					this.handleMove(a)
				}, a.prototype.handleTouchScroll = function (a) {
					this.isDragging && !this.scrollAlwaysKills || this.endInteraction(a, !0)
				}, a.prototype.trigger = function (a) {
					for (var b = [], c = 1; c < arguments.length; c++) b[c - 1] = arguments[c];
					this.options[a] && this.options[a].apply(this, b), this["_" + a] && this["_" + a].apply(this, b)
				}, a.prototype.initAutoScroll = function () {
					var a = this.scrollEl;
					this.isAutoScroll = this.options.scroll && a && !a.is(window) && !a.is(document), this.isAutoScroll && this.listenTo(a, "scroll", e.debounce(this.handleDebouncedScroll, 100))
				}, a.prototype.destroyAutoScroll = function () {
					this.endAutoScroll(), this.isAutoScroll && this.stopListeningTo(this.scrollEl, "scroll")
				}, a.prototype.computeScrollBounds = function () {
					this.isAutoScroll && (this.scrollBounds = e.getOuterRect(this.scrollEl))
				}, a.prototype.updateAutoScroll = function (a) {
					var b, c, d, f, g = this.scrollSensitivity,
						h = this.scrollBounds,
						i = 0,
						j = 0;
					h && (b = (g - (e.getEvY(a) - h.top)) / g, c = (g - (h.bottom - e.getEvY(a))) / g, d = (g - (e.getEvX(a) - h.left)) / g, f = (g - (h.right - e.getEvX(a))) / g, b >= 0 && b <= 1 ? i = b * this.scrollSpeed * -1 : c >= 0 && c <= 1 && (i = c * this.scrollSpeed), d >= 0 && d <= 1 ? j = d * this.scrollSpeed * -1 : f >= 0 && f <= 1 && (j = f * this.scrollSpeed)), this.setScrollVel(i, j)
				}, a.prototype.setScrollVel = function (a, b) {
					this.scrollTopVel = a, this.scrollLeftVel = b, this.constrainScrollVel(), !this.scrollTopVel && !this.scrollLeftVel || this.scrollIntervalId || (this.scrollIntervalId = setInterval(e.proxy(this, "scrollIntervalFunc"), this.scrollIntervalMs))
				}, a.prototype.constrainScrollVel = function () {
					var a = this.scrollEl;
					this.scrollTopVel < 0 ? a.scrollTop() <= 0 && (this.scrollTopVel = 0) : this.scrollTopVel > 0 && a.scrollTop() + a[0].clientHeight >= a[0].scrollHeight && (this.scrollTopVel = 0), this.scrollLeftVel < 0 ? a.scrollLeft() <= 0 && (this.scrollLeftVel = 0) : this.scrollLeftVel > 0 && a.scrollLeft() + a[0].clientWidth >= a[0].scrollWidth && (this.scrollLeftVel = 0)
				}, a.prototype.scrollIntervalFunc = function () {
					var a = this.scrollEl,
						b = this.scrollIntervalMs / 1e3;
					this.scrollTopVel && a.scrollTop(a.scrollTop() + this.scrollTopVel * b), this.scrollLeftVel && a.scrollLeft(a.scrollLeft() + this.scrollLeftVel * b), this.constrainScrollVel(), this.scrollTopVel || this.scrollLeftVel || this.endAutoScroll()
				}, a.prototype.endAutoScroll = function () {
					this.scrollIntervalId && (clearInterval(this.scrollIntervalId), this.scrollIntervalId = null, this.handleScrollEnd())
				}, a.prototype.handleDebouncedScroll = function () {
					this.scrollIntervalId || this.handleScrollEnd()
				}, a.prototype.handleScrollEnd = function () {}, a
			}();
		b.default = h, f.default.mixInto(h)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = c(12),
			g = function (a) {
				function b() {
					var b = null !== a && a.apply(this, arguments) || this;
					return b.breakOnWeeks = !1, b
				}
				return d.__extends(b, a), b.prototype.updateDayTable = function () {
					for (var a, b, c, d = this, e = d.view, f = e.calendar, g = f.msToUtcMoment(d.dateProfile.renderUnzonedRange.startMs, !0), h = f.msToUtcMoment(d.dateProfile.renderUnzonedRange.endMs, !0), i = -1, j = [], k = []; g.isBefore(h);) e.isHiddenDay(g) ? j.push(i + .5) : (i++, j.push(i), k.push(g.clone())), g.add(1, "days");
					if (this.breakOnWeeks) {
						for (b = k[0].day(), a = 1; a < k.length && k[a].day() != b; a++);
						c = Math.ceil(k.length / a)
					} else c = 1, a = k.length;
					this.dayDates = k, this.dayIndices = j, this.daysPerRow = a, this.rowCnt = c, this.updateDayTableCols()
				}, b.prototype.updateDayTableCols = function () {
					this.colCnt = this.computeColCnt(), this.colHeadFormat = this.opt("columnFormat") || this.computeColHeadFormat()
				}, b.prototype.computeColCnt = function () {
					return this.daysPerRow
				}, b.prototype.getCellDate = function (a, b) {
					return this.dayDates[this.getCellDayIndex(a, b)].clone()
				}, b.prototype.getCellRange = function (a, b) {
					var c = this.getCellDate(a, b);
					return {
						start: c,
						end: c.clone().add(1, "days")
					}
				}, b.prototype.getCellDayIndex = function (a, b) {
					return a * this.daysPerRow + this.getColDayIndex(b)
				}, b.prototype.getColDayIndex = function (a) {
					return this.isRTL ? this.colCnt - 1 - a : a
				}, b.prototype.getDateDayIndex = function (a) {
					var b = this.dayIndices,
						c = a.diff(this.dayDates[0], "days");
					return c < 0 ? b[0] - 1 : c >= b.length ? b[b.length - 1] + 1 : b[c]
				}, b.prototype.computeColHeadFormat = function () {
					return this.rowCnt > 1 || this.colCnt > 10 ? "ddd" : this.colCnt > 1 ? this.opt("dayOfMonthFormat") : "dddd"
				}, b.prototype.sliceRangeByRow = function (a) {
					var b, c, d, e, f, g = this.daysPerRow,
						h = this.view.computeDayRange(a),
						i = this.getDateDayIndex(h.start),
						j = this.getDateDayIndex(h.end.clone().subtract(1, "days")),
						k = [];
					for (b = 0; b < this.rowCnt; b++) c = b * g, d = c + g - 1, e = Math.max(i, c), f = Math.min(j, d), e = Math.ceil(e), f = Math.floor(f), e <= f && k.push({
						row: b,
						firstRowDayIndex: e - c,
						lastRowDayIndex: f - c,
						isStart: e === i,
						isEnd: f === j
					});
					return k
				}, b.prototype.sliceRangeByDay = function (a) {
					var b, c, d, e, f, g, h = this.daysPerRow,
						i = this.view.computeDayRange(a),
						j = this.getDateDayIndex(i.start),
						k = this.getDateDayIndex(i.end.clone().subtract(1, "days")),
						l = [];
					for (b = 0; b < this.rowCnt; b++)
						for (c = b * h, d = c + h - 1, e = c; e <= d; e++) f = Math.max(j, e), g = Math.min(k, e), f = Math.ceil(f), g = Math.floor(g), f <= g && l.push({
							row: b,
							firstRowDayIndex: f - c,
							lastRowDayIndex: g - c,
							isStart: f === j,
							isEnd: g === k
						});
					return l
				}, b.prototype.renderHeadHtml = function () {
					var a = this.view.calendar.theme;
					return '<div class="fc-row ' + a.getClass("headerRow") + '"><table class="' + a.getClass("tableGrid") + '"><thead>' + this.renderHeadTrHtml() + "</thead></table></div>"
				}, b.prototype.renderHeadIntroHtml = function () {
					return this.renderIntroHtml()
				}, b.prototype.renderHeadTrHtml = function () {
					return "<tr>" + (this.isRTL ? "" : this.renderHeadIntroHtml()) + this.renderHeadDateCellsHtml() + (this.isRTL ? this.renderHeadIntroHtml() : "") + "</tr>"
				}, b.prototype.renderHeadDateCellsHtml = function () {
					var a, b, c = [];
					for (a = 0; a < this.colCnt; a++) b = this.getCellDate(0, a), c.push(this.renderHeadDateCellHtml(b));
					return c.join("")
				}, b.prototype.renderHeadDateCellHtml = function (a, b, c) {
					var d = this,
						f = d.view,
						g = d.dateProfile.activeUnzonedRange.containsDate(a),
						h = ["fc-day-header", f.calendar.theme.getClass("widgetHeader")],
						i = e.htmlEscape(a.format(d.colHeadFormat));
					return 1 === d.rowCnt ? h = h.concat(d.getDayClasses(a, !0)) : h.push("fc-" + e.dayIDs[a.day()]), '<th class="' + h.join(" ") + '"' + (1 === (g && d.rowCnt) ? ' data-date="' + a.format("YYYY-MM-DD") + '"' : "") + (b > 1 ? ' colspan="' + b + '"' : "") + (c ? " " + c : "") + ">" + (g ? f.buildGotoAnchorHtml({
						date: a,
						forceOff: d.rowCnt > 1 || 1 === d.colCnt
					}, i) : i) + "</th>"
				}, b.prototype.renderBgTrHtml = function (a) {
					return "<tr>" + (this.isRTL ? "" : this.renderBgIntroHtml(a)) + this.renderBgCellsHtml(a) + (this.isRTL ? this.renderBgIntroHtml(a) : "") + "</tr>"
				}, b.prototype.renderBgIntroHtml = function (a) {
					return this.renderIntroHtml()
				}, b.prototype.renderBgCellsHtml = function (a) {
					var b, c, d = [];
					for (b = 0; b < this.colCnt; b++) c = this.getCellDate(a, b), d.push(this.renderBgCellHtml(c));
					return d.join("")
				}, b.prototype.renderBgCellHtml = function (a, b) {
					var c = this,
						d = c.view,
						e = c.dateProfile.activeUnzonedRange.containsDate(a),
						f = c.getDayClasses(a);
					return f.unshift("fc-day", d.calendar.theme.getClass("widgetContent")), '<td class="' + f.join(" ") + '"' + (e ? ' data-date="' + a.format("YYYY-MM-DD") + '"' : "") + (b ? " " + b : "") + "></td>"
				}, b.prototype.renderIntroHtml = function () {}, b.prototype.bookendCells = function (a) {
					var b = this.renderIntroHtml();
					b && (this.isRTL ? a.append(b) : a.prepend(b))
				}, b
			}(f.default);
		b.default = g
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a, b) {
				this.component = a, this.fillRenderer = b
			}
			return a.prototype.render = function (a) {
				var b = this.component,
					c = b._getDateProfile().activeUnzonedRange,
					d = a.buildEventInstanceGroup(b.hasAllDayBusinessHours, c),
					e = d ? b.eventRangesToEventFootprints(d.sliceRenderRanges(c)) : [];
				this.renderEventFootprints(e)
			}, a.prototype.renderEventFootprints = function (a) {
				var b = this.component.eventFootprintsToSegs(a);
				this.renderSegs(b), this.segs = b
			}, a.prototype.renderSegs = function (a) {
				this.fillRenderer && this.fillRenderer.renderSegs("businessHours", a, {
					getClasses: function (a) {
						return ["fc-nonbusiness", "fc-bgevent"]
					}
				})
			}, a.prototype.unrender = function () {
				this.fillRenderer && this.fillRenderer.unrender("businessHours"), this.segs = null
			}, a.prototype.getSegs = function () {
				return this.segs || []
			}, a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = function () {
				function a(a) {
					this.fillSegTag = "div", this.component = a, this.elsByFill = {}
				}
				return a.prototype.renderFootprint = function (a, b, c) {
					this.renderSegs(a, this.component.componentFootprintToSegs(b), c)
				}, a.prototype.renderSegs = function (a, b, c) {
					var d;
					return b = this.buildSegEls(a, b, c), d = this.attachSegEls(a, b), d && this.reportEls(a, d), b
				}, a.prototype.unrender = function (a) {
					var b = this.elsByFill[a];
					b && (b.remove(), delete this.elsByFill[a])
				}, a.prototype.buildSegEls = function (a, b, c) {
					var e, f = this,
						g = "",
						h = [];
					if (b.length) {
						for (e = 0; e < b.length; e++) g += this.buildSegHtml(a, b[e], c);
						d(g).each(function (a, e) {
							var g = b[a],
								i = d(e);
							c.filterEl && (i = c.filterEl(g, i)), i && (i = d(i), i.is(f.fillSegTag) && (g.el = i, h.push(g)))
						})
					}
					return h
				}, a.prototype.buildSegHtml = function (a, b, c) {
					var d = c.getClasses ? c.getClasses(b) : [],
						f = e.cssToStr(c.getCss ? c.getCss(b) : {});
					return "<" + this.fillSegTag + (d.length ? ' class="' + d.join(" ") + '"' : "") + (f ? ' style="' + f + '"' : "") + " />"
				}, a.prototype.attachSegEls = function (a, b) {}, a.prototype.reportEls = function (a, b) {
					this.elsByFill[a] ? this.elsByFill[a] = this.elsByFill[a].add(b) : this.elsByFill[a] = d(b)
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(11),
			e = c(24),
			f = c(5),
			g = function () {
				function a(a, b) {
					this.view = a._getView(), this.component = a, this.eventRenderer = b
				}
				return a.prototype.renderComponentFootprint = function (a) {
					this.renderEventFootprints([this.fabricateEventFootprint(a)])
				}, a.prototype.renderEventDraggingFootprints = function (a, b, c) {
					this.renderEventFootprints(a, b, "fc-dragging", c ? null : this.view.opt("dragOpacity"))
				}, a.prototype.renderEventResizingFootprints = function (a, b, c) {
					this.renderEventFootprints(a, b, "fc-resizing")
				}, a.prototype.renderEventFootprints = function (a, b, c, d) {
					var e, f = this.component.eventFootprintsToSegs(a),
						g = "fc-helper " + (c || "");
					for (f = this.eventRenderer.renderFgSegEls(f), e = 0; e < f.length; e++) f[e].el.addClass(g);
					if (null != d)
						for (e = 0; e < f.length; e++) f[e].el.css("opacity", d);
					this.helperEls = this.renderSegs(f, b)
				}, a.prototype.renderSegs = function (a, b) {}, a.prototype.unrender = function () {
					this.helperEls && (this.helperEls.remove(), this.helperEls = null)
				}, a.prototype.fabricateEventFootprint = function (a) {
					var b, c = this.view.calendar,
						g = c.footprintToDateProfile(a),
						h = new d.default(new f.default(c));
					return h.dateProfile = g, b = h.buildInstance(), new e.default(a, h, b)
				}, a
			}();
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(14),
			f = c(13),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.bindToEl = function (a) {
					var b = this.component;
					b.bindSegHandlerToEl(a, "click", this.handleClick.bind(this)), b.bindSegHandlerToEl(a, "mouseenter", this.handleMouseover.bind(this)), b.bindSegHandlerToEl(a, "mouseleave", this.handleMouseout.bind(this))
				}, b.prototype.handleClick = function (a, b) {
					!1 === this.component.publiclyTrigger("eventClick", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, this.view]
					}) && b.preventDefault()
				}, b.prototype.handleMouseover = function (a, b) {
					e.default.get().shouldIgnoreMouse() || this.mousedOverSeg || (this.mousedOverSeg = a, this.view.isEventDefResizable(a.footprint.eventDef) && a.el.addClass("fc-allow-mouse-resize"), this.component.publiclyTrigger("eventMouseover", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, this.view]
					}))
				}, b.prototype.handleMouseout = function (a, b) {
					this.mousedOverSeg && (this.mousedOverSeg = null, this.view.isEventDefResizable(a.footprint.eventDef) && a.el.removeClass("fc-allow-mouse-resize"), this.component.publiclyTrigger("eventMouseout", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b || {}, this.view]
					}))
				}, b.prototype.end = function () {
					this.mousedOverSeg && this.handleMouseout(this.mousedOverSeg)
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(12),
			f = c(82),
			g = c(66),
			h = c(44),
			i = c(65),
			j = c(64),
			k = c(63),
			l = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b
			}(e.default);
		b.default = l, l.prototype.dateClickingClass = f.default, l.prototype.dateSelectingClass = g.default, l.prototype.eventPointingClass = h.default, l.prototype.eventDraggingClass = i.default, l.prototype.eventResizingClass = j.default, l.prototype.externalDroppingClass = k.default
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(38),
			h = c(86),
			i = c(4),
			j = c(10),
			k = c(24),
			l = c(41),
			m = c(45),
			n = c(29),
			o = c(40),
			p = c(87),
			q = c(88),
			r = c(89),
			s = function (a) {
				function b(b) {
					var c = a.call(this, b) || this;
					return c.cellWeekNumbersVisible = !1, c.bottomCoordPadding = 0, c.isRigid = !1, c.hasAllDayBusinessHours = !0, c
				}
				return d.__extends(b, a), b.prototype.componentFootprintToSegs = function (a) {
					var b, c, d = this.sliceRangeByRow(a.unzonedRange);
					for (b = 0; b < d.length; b++) c = d[b], this.isRTL ? (c.leftCol = this.daysPerRow - 1 - c.lastRowDayIndex, c.rightCol = this.daysPerRow - 1 - c.firstRowDayIndex) : (c.leftCol = c.firstRowDayIndex, c.rightCol = c.lastRowDayIndex);
					return d
				}, b.prototype.renderDates = function (a) {
					this.dateProfile = a, this.updateDayTable(), this.renderGrid()
				}, b.prototype.unrenderDates = function () {
					this.removeSegPopover()
				}, b.prototype.renderGrid = function () {
					var a, b, c = this.view,
						d = this.rowCnt,
						e = this.colCnt,
						f = "";
					for (this.headContainerEl && this.headContainerEl.html(this.renderHeadHtml()), a = 0; a < d; a++) f += this.renderDayRowHtml(a, this.isRigid);
					for (this.el.html(f), this.rowEls = this.el.find(".fc-row"), this.cellEls = this.el.find(".fc-day, .fc-disabled-day"), this.rowCoordCache = new g.default({
							els: this.rowEls,
							isVertical: !0
						}), this.colCoordCache = new g.default({
							els: this.cellEls.slice(0, this.colCnt),
							isHorizontal: !0
						}), a = 0; a < d; a++)
						for (b = 0; b < e; b++) this.publiclyTrigger("dayRender", {
							context: c,
							args: [this.getCellDate(a, b), this.getCellEl(a, b), c]
						})
				}, b.prototype.renderDayRowHtml = function (a, b) {
					var c = this.view.calendar.theme,
						d = ["fc-row", "fc-week", c.getClass("dayRow")];
					return b && d.push("fc-rigid"), '<div class="' + d.join(" ") + '"><div class="fc-bg"><table class="' + c.getClass("tableGrid") + '">' + this.renderBgTrHtml(a) + '</table></div><div class="fc-content-skeleton"><table>' + (this.getIsNumbersVisible() ? "<thead>" + this.renderNumberTrHtml(a) + "</thead>" : "") + "</table></div></div>"
				}, b.prototype.getIsNumbersVisible = function () {
					return this.getIsDayNumbersVisible() || this.cellWeekNumbersVisible
				}, b.prototype.getIsDayNumbersVisible = function () {
					return this.rowCnt > 1
				}, b.prototype.renderNumberTrHtml = function (a) {
					return "<tr>" + (this.isRTL ? "" : this.renderNumberIntroHtml(a)) + this.renderNumberCellsHtml(a) + (this.isRTL ? this.renderNumberIntroHtml(a) : "") + "</tr>"
				}, b.prototype.renderNumberIntroHtml = function (a) {
					return this.renderIntroHtml()
				}, b.prototype.renderNumberCellsHtml = function (a) {
					var b, c, d = [];
					for (b = 0; b < this.colCnt; b++) c = this.getCellDate(a, b), d.push(this.renderNumberCellHtml(c));
					return d.join("")
				}, b.prototype.renderNumberCellHtml = function (a) {
					var b, c, d = this.view,
						e = "",
						f = this.dateProfile.activeUnzonedRange.containsDate(a),
						g = this.getIsDayNumbersVisible() && f;
					return g || this.cellWeekNumbersVisible ? (b = this.getDayClasses(a), b.unshift("fc-day-top"), this.cellWeekNumbersVisible && (c = "ISO" === a._locale._fullCalendar_weekCalc ? 1 : a._locale.firstDayOfWeek()), e += '<td class="' + b.join(" ") + '"' + (f ? ' data-date="' + a.format() + '"' : "") + ">", this.cellWeekNumbersVisible && a.day() == c && (e += d.buildGotoAnchorHtml({
						date: a,
						type: "week"
					}, {
						class: "fc-week-number"
					}, a.format("w"))), g && (e += d.buildGotoAnchorHtml(a, {
						class: "fc-day-number"
					}, a.date())), e += "</td>") : "<td/>"
				}, b.prototype.prepareHits = function () {
					this.colCoordCache.build(), this.rowCoordCache.build(), this.rowCoordCache.bottoms[this.rowCnt - 1] += this.bottomCoordPadding
				}, b.prototype.releaseHits = function () {
					this.colCoordCache.clear(), this.rowCoordCache.clear()
				}, b.prototype.queryHit = function (a, b) {
					if (this.colCoordCache.isLeftInBounds(a) && this.rowCoordCache.isTopInBounds(b)) {
						var c = this.colCoordCache.getHorizontalIndex(a),
							d = this.rowCoordCache.getVerticalIndex(b);
						if (null != d && null != c) return this.getCellHit(d, c)
					}
				}, b.prototype.getHitFootprint = function (a) {
					var b = this.getCellRange(a.row, a.col);
					return new j.default(new i.default(b.start, b.end), !0)
				}, b.prototype.getHitEl = function (a) {
					return this.getCellEl(a.row, a.col)
				}, b.prototype.getCellHit = function (a, b) {
					return {
						row: a,
						col: b,
						component: this,
						left: this.colCoordCache.getLeftOffset(b),
						right: this.colCoordCache.getRightOffset(b),
						top: this.rowCoordCache.getTopOffset(a),
						bottom: this.rowCoordCache.getBottomOffset(a)
					}
				}, b.prototype.getCellEl = function (a, b) {
					return this.cellEls.eq(a * this.colCnt + b)
				}, b.prototype.executeEventUnrender = function () {
					this.removeSegPopover(), a.prototype.executeEventUnrender.call(this)
				}, b.prototype.getOwnEventSegs = function () {
					return a.prototype.getOwnEventSegs.call(this).concat(this.popoverSegs || [])
				}, b.prototype.renderDrag = function (a, b, c) {
					var d;
					for (d = 0; d < a.length; d++) this.renderHighlight(a[d].componentFootprint);
					if (a.length && b && b.component !== this) return this.helperRenderer.renderEventDraggingFootprints(a, b, c), !0
				}, b.prototype.unrenderDrag = function () {
					this.unrenderHighlight(), this.helperRenderer.unrender()
				}, b.prototype.renderEventResize = function (a, b, c) {
					var d;
					for (d = 0; d < a.length; d++) this.renderHighlight(a[d].componentFootprint);
					this.helperRenderer.renderEventResizingFootprints(a, b, c)
				}, b.prototype.unrenderEventResize = function () {
					this.unrenderHighlight(), this.helperRenderer.unrender()
				}, b.prototype.removeSegPopover = function () {
					this.segPopover && this.segPopover.hide()
				}, b.prototype.limitRows = function (a) {
					var b, c, d = this.eventRenderer.rowStructs || [];
					for (b = 0; b < d.length; b++) this.unlimitRow(b), !1 !== (c = !!a && ("number" == typeof a ? a : this.computeRowLevelLimit(b))) && this.limitRow(b, c)
				}, b.prototype.computeRowLevelLimit = function (a) {
					function b(a, b) {
						f = Math.max(f, e(b).outerHeight())
					}
					var c, d, f, g = this.rowEls.eq(a),
						h = g.height(),
						i = this.eventRenderer.rowStructs[a].tbodyEl.children();
					for (c = 0; c < i.length; c++)
						if (d = i.eq(c).removeClass("fc-limited"), f = 0, d.find("> td > :first-child").each(b), d.position().top + f > h) return c;
					return !1
				}, b.prototype.limitRow = function (a, b) {
					var c, d, f, g, h, i, j, k, l, m, n, o, p, q, r, s = this,
						t = this.eventRenderer.rowStructs[a],
						u = [],
						v = 0,
						w = function (c) {
							for (; v < c;) i = s.getCellSegs(a, v, b), i.length && (l = d[b - 1][v], r = s.renderMoreLink(a, v, i), q = e("<div/>").append(r), l.append(q), u.push(q[0])), v++
						};
					if (b && b < t.segLevels.length) {
						for (c = t.segLevels[b - 1], d = t.cellMatrix, f = t.tbodyEl.children().slice(b).addClass("fc-limited").get(), g = 0; g < c.length; g++) {
							for (h = c[g], w(h.leftCol), k = [], j = 0; v <= h.rightCol;) i = this.getCellSegs(a, v, b), k.push(i), j += i.length, v++;
							if (j) {
								for (l = d[b - 1][h.leftCol], m = l.attr("rowspan") || 1, n = [], o = 0; o < k.length; o++) p = e('<td class="fc-more-cell"/>').attr("rowspan", m), i = k[o], r = this.renderMoreLink(a, h.leftCol + o, [h].concat(i)), q = e("<div/>").append(r), p.append(q), n.push(p[0]), u.push(p[0]);
								l.addClass("fc-limited").after(e(n)), f.push(l[0])
							}
						}
						w(this.colCnt), t.moreEls = e(u), t.limitedEls = e(f)
					}
				}, b.prototype.unlimitRow = function (a) {
					var b = this.eventRenderer.rowStructs[a];
					b.moreEls && (b.moreEls.remove(), b.moreEls = null), b.limitedEls && (b.limitedEls.removeClass("fc-limited"), b.limitedEls = null)
				}, b.prototype.renderMoreLink = function (a, b, c) {
					var d = this,
						f = this.view;
					return e('<a class="fc-more"/>').text(this.getMoreLinkText(c.length)).on("click", function (g) {
						var h = d.opt("eventLimitClick"),
							i = d.getCellDate(a, b),
							j = e(g.currentTarget),
							k = d.getCellEl(a, b),
							l = d.getCellSegs(a, b),
							m = d.resliceDaySegs(l, i),
							n = d.resliceDaySegs(c, i);
						"function" == typeof h && (h = d.publiclyTrigger("eventLimitClick", {
							context: f,
							args: [{
								date: i.clone(),
								dayEl: k,
								moreEl: j,
								segs: m,
								hiddenSegs: n
							}, g, f]
						})), "popover" === h ? d.showSegPopover(a, b, j, m) : "string" == typeof h && f.calendar.zoomTo(i, h)
					})
				}, b.prototype.showSegPopover = function (a, b, c, d) {
					var e, f, g = this,
						i = this.view,
						j = c.parent();
					e = 1 == this.rowCnt ? i.el : this.rowEls.eq(a), f = {
						className: "fc-more-popover " + i.calendar.theme.getClass("popover"),
						content: this.renderSegPopoverContent(a, b, d),
						parentEl: i.el,
						top: e.offset().top,
						autoHide: !0,
						viewportConstrain: this.opt("popoverViewportConstrain"),
						hide: function () {
							g.popoverSegs && g.triggerBeforeEventSegsDestroyed(g.popoverSegs), g.segPopover.removeElement(), g.segPopover = null, g.popoverSegs = null
						}
					}, this.isRTL ? f.right = j.offset().left + j.outerWidth() + 1 : f.left = j.offset().left - 1, this.segPopover = new h.default(f), this.segPopover.show(), this.bindAllSegHandlersToEl(this.segPopover.el), this.triggerAfterEventSegsRendered(d)
				}, b.prototype.renderSegPopoverContent = function (a, b, c) {
					var d, g = this.view,
						h = g.calendar.theme,
						i = this.getCellDate(a, b).format(this.opt("dayPopoverFormat")),
						j = e('<div class="fc-header ' + h.getClass("popoverHeader") + '"><span class="fc-close ' + h.getIconClass("close") + '"></span><span class="fc-title">' + f.htmlEscape(i) + '</span><div class="fc-clear"/></div><div class="fc-body ' + h.getClass("popoverContent") + '"><div class="fc-event-container"></div></div>'),
						k = j.find(".fc-event-container");
					for (c = this.eventRenderer.renderFgSegEls(c, !0), this.popoverSegs = c, d = 0; d < c.length; d++) this.hitsNeeded(), c[d].hit = this.getCellHit(a, b), this.hitsNotNeeded(), k.append(c[d].el);
					return j
				}, b.prototype.resliceDaySegs = function (a, b) {
					var c, d, f, g = b.clone(),
						h = g.clone().add(1, "days"),
						l = new i.default(g, h),
						m = [];
					for (c = 0; c < a.length; c++) d = a[c], (f = d.footprint.componentFootprint.unzonedRange.intersect(l)) && m.push(e.extend({}, d, {
						footprint: new k.default(new j.default(f, d.footprint.componentFootprint.isAllDay), d.footprint.eventDef, d.footprint.eventInstance),
						isStart: d.isStart && f.isStart,
						isEnd: d.isEnd && f.isEnd
					}));
					return this.eventRenderer.sortEventSegs(m), m
				}, b.prototype.getMoreLinkText = function (a) {
					var b = this.opt("eventLimitText");
					return "function" == typeof b ? b(a) : "+" + a + " " + b
				}, b.prototype.getCellSegs = function (a, b, c) {
					for (var d, e = this.eventRenderer.rowStructs[a].segMatrix, f = c || 0, g = []; f < e.length;) d = e[f][b], d && g.push(d), f++;
					return g
				}, b
			}(n.default);
		b.default = s, s.prototype.eventRendererClass = p.default, s.prototype.businessHourRendererClass = l.default, s.prototype.helperRendererClass = q.default, s.prototype.fillRendererClass = r.default, m.default.mixInto(s), o.default.mixInto(s)
	}, function (a, b, c) {
		function d(a) {
			return function (a) {
				function b() {
					var b = null !== a && a.apply(this, arguments) || this;
					return b.colWeekNumbersVisible = !1, b
				}
				return e.__extends(b, a), b.prototype.renderHeadIntroHtml = function () {
					var a = this.view;
					return this.colWeekNumbersVisible ? '<th class="fc-week-number ' + a.calendar.theme.getClass("widgetHeader") + '" ' + a.weekNumberStyleAttr() + "><span>" + g.htmlEscape(this.opt("weekNumberTitle")) + "</span></th>" : ""
				}, b.prototype.renderNumberIntroHtml = function (a) {
					var b = this.view,
						c = this.getCellDate(a, 0);
					return this.colWeekNumbersVisible ? '<td class="fc-week-number" ' + b.weekNumberStyleAttr() + ">" + b.buildGotoAnchorHtml({
						date: c,
						type: "week",
						forceOff: 1 === this.colCnt
					}, c.format("w")) + "</td>" : ""
				}, b.prototype.renderBgIntroHtml = function () {
					var a = this.view;
					return this.colWeekNumbersVisible ? '<td class="fc-week-number ' + a.calendar.theme.getClass("widgetContent") + '" ' + a.weekNumberStyleAttr() + "></td>" : ""
				}, b.prototype.renderIntroHtml = function () {
					var a = this.view;
					return this.colWeekNumbersVisible ? '<td class="fc-week-number" ' + a.weekNumberStyleAttr() + "></td>" : ""
				}, b.prototype.getIsNumbersVisible = function () {
					return k.default.prototype.getIsNumbersVisible.apply(this, arguments) || this.colWeekNumbersVisible
				}, b
			}(a)
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(0),
			f = c(1),
			g = c(2),
			h = c(28),
			i = c(30),
			j = c(69),
			k = c(46),
			l = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.dayGrid = d.instantiateDayGrid(), d.dayGrid.isRigid = d.hasRigidRows(), d.opt("weekNumbers") && (d.opt("weekNumbersWithinDays") ? (d.dayGrid.cellWeekNumbersVisible = !0, d.dayGrid.colWeekNumbersVisible = !1) : (d.dayGrid.cellWeekNumbersVisible = !1, d.dayGrid.colWeekNumbersVisible = !0)), d.addChild(d.dayGrid), d.scroller = new h.default({
						overflowX: "hidden",
						overflowY: "auto"
					}), d
				}
				return e.__extends(b, a), b.prototype.instantiateDayGrid = function () {
					return new(d(this.dayGridClass))(this)
				}, b.prototype.executeDateRender = function (b) {
					this.dayGrid.breakOnWeeks = /year|month|week/.test(b.currentRangeUnit), a.prototype.executeDateRender.call(this, b)
				}, b.prototype.renderSkeleton = function () {
					var a, b;
					this.el.addClass("fc-basic-view").html(this.renderSkeletonHtml()), this.scroller.render(), a = this.scroller.el.addClass("fc-day-grid-container"), b = f('<div class="fc-day-grid" />').appendTo(a), this.el.find(".fc-body > tr > td").append(a), this.dayGrid.headContainerEl = this.el.find(".fc-head-container"), this.dayGrid.setElement(b)
				}, b.prototype.unrenderSkeleton = function () {
					this.dayGrid.removeElement(), this.scroller.destroy()
				}, b.prototype.renderSkeletonHtml = function () {
					var a = this.calendar.theme;
					return '<table class="' + a.getClass("tableGrid") + '">' + (this.opt("columnHeader") ? '<thead class="fc-head"><tr><td class="fc-head-container ' + a.getClass("widgetHeader") + '">&nbsp;</td></tr></thead>' : "") + '<tbody class="fc-body"><tr><td class="' + a.getClass("widgetContent") + '"></td></tr></tbody></table>'
				}, b.prototype.weekNumberStyleAttr = function () {
					return null != this.weekNumberWidth ? 'style="width:' + this.weekNumberWidth + 'px"' : ""
				}, b.prototype.hasRigidRows = function () {
					var a = this.opt("eventLimit");
					return a && "number" != typeof a
				}, b.prototype.updateSize = function (b, c, d) {
					var e, f, h = this.opt("eventLimit"),
						i = this.dayGrid.headContainerEl.find(".fc-row");
					if (!this.dayGrid.rowEls) return void(c || (e = this.computeScrollerHeight(b), this.scroller.setHeight(e)));
					a.prototype.updateSize.call(this, b, c, d), this.dayGrid.colWeekNumbersVisible && (this.weekNumberWidth = g.matchCellWidths(this.el.find(".fc-week-number"))), this.scroller.clear(), g.uncompensateScroll(i), this.dayGrid.removeSegPopover(), h && "number" == typeof h && this.dayGrid.limitRows(h), e = this.computeScrollerHeight(b), this.setGridHeight(e, c), h && "number" != typeof h && this.dayGrid.limitRows(h), c || (this.scroller.setHeight(e), f = this.scroller.getScrollbarWidths(), (f.left || f.right) && (g.compensateScroll(i, f), e = this.computeScrollerHeight(b), this.scroller.setHeight(e)), this.scroller.lockOverflow(f))
				}, b.prototype.computeScrollerHeight = function (a) {
					return a - g.subtractInnerElHeight(this.el, this.scroller.el)
				}, b.prototype.setGridHeight = function (a, b) {
					b ? g.undistributeHeight(this.dayGrid.rowEls) : g.distributeHeight(this.dayGrid.rowEls, a, !0)
				}, b.prototype.computeInitialDateScroll = function () {
					return {
						top: 0
					}
				}, b.prototype.queryDateScroll = function () {
					return {
						top: this.scroller.getScrollTop()
					}
				}, b.prototype.applyDateScroll = function (a) {
					void 0 !== a.top && this.scroller.setScrollTop(a.top)
				}, b
			}(i.default);
		b.default = l, l.prototype.dateProfileGeneratorClass = j.default, l.prototype.dayGridClass = k.default
	}, function (a, b, c) {
		function d(a, b) {
			return null == b ? a : e.isFunction(b) ? a.filter(b) : (b += "", a.filter(function (a) {
				return a.id == b || a._id === b
			}))
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(1),
			f = c(3),
			g = c(2),
			h = c(74),
			i = c(14),
			j = c(8),
			k = c(6),
			l = c(75),
			m = c(76),
			n = c(77),
			o = c(49),
			p = c(20),
			q = c(9),
			r = c(4),
			s = c(10),
			t = c(15),
			u = c(78),
			v = c(54),
			w = c(25),
			x = c(33),
			y = c(11),
			z = c(26),
			A = c(5),
			B = c(36),
			C = function () {
				function a(a, b) {
					this.loadingLevel = 0, this.ignoreUpdateViewSize = 0, this.freezeContentHeightDepth = 0, i.default.needed(), this.el = a, this.viewsByType = {}, this.optionsManager = new m.default(this, b), this.viewSpecManager = new n.default(this.optionsManager, this), this.initMomentInternals(), this.initCurrentDate(), this.initEventManager(), this.constraints = new o.default(this.eventManager, this), this.constructed()
				}
				return a.prototype.constructed = function () {}, a.prototype.getView = function () {
					return this.view
				}, a.prototype.publiclyTrigger = function (a, b) {
					var c, d, f = this.opt(a);
					if (e.isPlainObject(b) ? (c = b.context, d = b.args) : e.isArray(b) && (d = b), null == c && (c = this.el[0]), d || (d = []), this.triggerWith(a, c, d), f) return f.apply(c, d)
				}, a.prototype.hasPublicHandlers = function (a) {
					return this.hasHandlers(a) || this.opt(a)
				}, a.prototype.option = function (a, b) {
					var c;
					if ("string" == typeof a) {
						if (void 0 === b) return this.optionsManager.get(a);
						c = {}, c[a] = b, this.optionsManager.add(c)
					} else "object" == typeof a && this.optionsManager.add(a)
				}, a.prototype.opt = function (a) {
					return this.optionsManager.get(a)
				}, a.prototype.instantiateView = function (a) {
					var b = this.viewSpecManager.getViewSpec(a);
					return new b.class(this, b)
				}, a.prototype.isValidViewType = function (a) {
					return Boolean(this.viewSpecManager.getViewSpec(a))
				}, a.prototype.changeView = function (a, b) {
					b && (b.start && b.end ? this.optionsManager.recordOverrides({
						visibleRange: b
					}) : this.currentDate = this.moment(b).stripZone()), this.renderView(a)
				}, a.prototype.zoomTo = function (a, b) {
					var c;
					b = b || "day", c = this.viewSpecManager.getViewSpec(b) || this.viewSpecManager.getUnitViewSpec(b), this.currentDate = a.clone(), this.renderView(c ? c.type : null)
				}, a.prototype.initCurrentDate = function () {
					var a = this.opt("defaultDate");
					this.currentDate = null != a ? this.moment(a).stripZone() : this.getNow()
				}, a.prototype.prev = function () {
					var a = this.view,
						b = a.dateProfileGenerator.buildPrev(a.get("dateProfile"));
					b.isValid && (this.currentDate = b.date, this.renderView())
				}, a.prototype.next = function () {
					var a = this.view,
						b = a.dateProfileGenerator.buildNext(a.get("dateProfile"));
					b.isValid && (this.currentDate = b.date, this.renderView())
				}, a.prototype.prevYear = function () {
					this.currentDate.add(-1, "years"), this.renderView()
				}, a.prototype.nextYear = function () {
					this.currentDate.add(1, "years"), this.renderView()
				}, a.prototype.today = function () {
					this.currentDate = this.getNow(), this.renderView()
				}, a.prototype.gotoDate = function (a) {
					this.currentDate = this.moment(a).stripZone(), this.renderView()
				}, a.prototype.incrementDate = function (a) {
					this.currentDate.add(f.duration(a)), this.renderView()
				}, a.prototype.getDate = function () {
					return this.applyTimezone(this.currentDate)
				}, a.prototype.pushLoading = function () {
					this.loadingLevel++ || this.publiclyTrigger("loading", [!0, this.view])
				}, a.prototype.popLoading = function () {
					--this.loadingLevel || this.publiclyTrigger("loading", [!1, this.view])
				}, a.prototype.render = function () {
					this.contentEl ? this.elementVisible() && (this.calcSize(), this.updateViewSize()) : this.initialRender()
				}, a.prototype.initialRender = function () {
					var a = this,
						b = this.el;
					b.addClass("fc"), b.on("click.fc", "a[data-goto]", function (b) {
						var c = e(b.currentTarget),
							d = c.data("goto"),
							f = a.moment(d.date),
							h = d.type,
							i = a.view.opt("navLink" + g.capitaliseFirstLetter(h) + "Click");
						"function" == typeof i ? i(f, b) : ("string" == typeof i && (h = i), a.zoomTo(f, h))
					}), this.optionsManager.watch("settingTheme", ["?theme", "?themeSystem"], function (c) {
						var d = B.default.getThemeClass(c.themeSystem || c.theme),
							e = new d(a.optionsManager),
							f = e.getClass("widget");
						a.theme = e, f && b.addClass(f)
					}, function () {
						var c = a.theme.getClass("widget");
						a.theme = null, c && b.removeClass(c)
					}), this.optionsManager.watch("settingBusinessHourGenerator", ["?businessHours"], function (b) {
						a.businessHourGenerator = new v.default(b.businessHours, a), a.view && a.view.set("businessHourGenerator", a.businessHourGenerator)
					}, function () {
						a.businessHourGenerator = null
					}), this.optionsManager.watch("applyingDirClasses", ["?isRTL", "?locale"], function (a) {
						b.toggleClass("fc-ltr", !a.isRTL), b.toggleClass("fc-rtl", a.isRTL)
					}), this.contentEl = e("<div class='fc-view-container'/>").prependTo(b), this.initToolbars(), this.renderHeader(), this.renderFooter(), this.renderView(this.opt("defaultView")), this.opt("handleWindowResize") && e(window).resize(this.windowResizeProxy = g.debounce(this.windowResize.bind(this), this.opt("windowResizeDelay")))
				}, a.prototype.destroy = function () {
					this.view && this.clearView(), this.toolbarsManager.proxyCall("removeElement"), this.contentEl.remove(), this.el.removeClass("fc fc-ltr fc-rtl"), this.optionsManager.unwatch("settingTheme"), this.optionsManager.unwatch("settingBusinessHourGenerator"), this.el.off(".fc"), this.windowResizeProxy && (e(window).unbind("resize", this.windowResizeProxy), this.windowResizeProxy = null), i.default.unneeded()
				}, a.prototype.elementVisible = function () {
					return this.el.is(":visible")
				}, a.prototype.bindViewHandlers = function (a) {
					var b = this;
					a.watch("titleForCalendar", ["title"], function (c) {
						a === b.view && b.setToolbarsTitle(c.title)
					}), a.watch("dateProfileForCalendar", ["dateProfile"], function (c) {
						a === b.view && (b.currentDate = c.dateProfile.date, b.updateToolbarButtons(c.dateProfile))
					})
				}, a.prototype.unbindViewHandlers = function (a) {
					a.unwatch("titleForCalendar"), a.unwatch("dateProfileForCalendar")
				}, a.prototype.renderView = function (a) {
					var b, c = this.view;
					this.freezeContentHeight(), c && a && c.type !== a && this.clearView(), !this.view && a && (b = this.view = this.viewsByType[a] || (this.viewsByType[a] = this.instantiateView(a)), this.bindViewHandlers(b), b.startBatchRender(), b.setElement(e("<div class='fc-view fc-" + a + "-view' />").appendTo(this.contentEl)), this.toolbarsManager.proxyCall("activateButton", a)), this.view && (this.view.get("businessHourGenerator") !== this.businessHourGenerator && this.view.set("businessHourGenerator", this.businessHourGenerator), this.view.setDate(this.currentDate), b && b.stopBatchRender()), this.thawContentHeight()
				}, a.prototype.clearView = function () {
					var a = this.view;
					this.toolbarsManager.proxyCall("deactivateButton", a.type), this.unbindViewHandlers(a), a.removeElement(), a.unsetDate(), this.view = null
				}, a.prototype.reinitView = function () {
					var a = this.view,
						b = a.queryScroll();
					this.freezeContentHeight(), this.clearView(), this.calcSize(), this.renderView(a.type), this.view.applyScroll(b), this.thawContentHeight()
				}, a.prototype.getSuggestedViewHeight = function () {
					return null == this.suggestedViewHeight && this.calcSize(), this.suggestedViewHeight
				}, a.prototype.isHeightAuto = function () {
					return "auto" === this.opt("contentHeight") || "auto" === this.opt("height")
				}, a.prototype.updateViewSize = function (a) {
					void 0 === a && (a = !1);
					var b, c = this.view;
					if (!this.ignoreUpdateViewSize && c) return a && (this.calcSize(), b = c.queryScroll()), this.ignoreUpdateViewSize++, c.updateSize(this.getSuggestedViewHeight(), this.isHeightAuto(), a), this.ignoreUpdateViewSize--, a && c.applyScroll(b), !0
				}, a.prototype.calcSize = function () {
					this.elementVisible() && this._calcSize()
				}, a.prototype._calcSize = function () {
					var a = this.opt("contentHeight"),
						b = this.opt("height");
					this.suggestedViewHeight = "number" == typeof a ? a : "function" == typeof a ? a() : "number" == typeof b ? b - this.queryToolbarsHeight() : "function" == typeof b ? b() - this.queryToolbarsHeight() : "parent" === b ? this.el.parent().height() - this.queryToolbarsHeight() : Math.round(this.contentEl.width() / Math.max(this.opt("aspectRatio"), .5))
				}, a.prototype.windowResize = function (a) {
					a.target === window && this.view && this.view.isDatesRendered && this.updateViewSize(!0) && this.publiclyTrigger("windowResize", [this.view])
				}, a.prototype.freezeContentHeight = function () {
					this.freezeContentHeightDepth++ || this.forceFreezeContentHeight()
				}, a.prototype.forceFreezeContentHeight = function () {
					this.contentEl.css({
						width: "100%",
						height: this.contentEl.height(),
						overflow: "hidden"
					})
				}, a.prototype.thawContentHeight = function () {
					this.freezeContentHeightDepth--, this.contentEl.css({
						width: "",
						height: "",
						overflow: ""
					}), this.freezeContentHeightDepth && this.forceFreezeContentHeight()
				}, a.prototype.initToolbars = function () {
					this.header = new l.default(this, this.computeHeaderOptions()), this.footer = new l.default(this, this.computeFooterOptions()), this.toolbarsManager = new h.default([this.header, this.footer])
				}, a.prototype.computeHeaderOptions = function () {
					return {
						extraClasses: "fc-header-toolbar",
						layout: this.opt("header")
					}
				}, a.prototype.computeFooterOptions = function () {
					return {
						extraClasses: "fc-footer-toolbar",
						layout: this.opt("footer")
					}
				}, a.prototype.renderHeader = function () {
					var a = this.header;
					a.setToolbarOptions(this.computeHeaderOptions()), a.render(), a.el && this.el.prepend(a.el)
				}, a.prototype.renderFooter = function () {
					var a = this.footer;
					a.setToolbarOptions(this.computeFooterOptions()), a.render(), a.el && this.el.append(a.el)
				}, a.prototype.setToolbarsTitle = function (a) {
					this.toolbarsManager.proxyCall("updateTitle", a)
				}, a.prototype.updateToolbarButtons = function (a) {
					var b = this.getNow(),
						c = this.view,
						d = c.dateProfileGenerator.build(b),
						e = c.dateProfileGenerator.buildPrev(c.get("dateProfile")),
						f = c.dateProfileGenerator.buildNext(c.get("dateProfile"));
					this.toolbarsManager.proxyCall(d.isValid && !a.currentUnzonedRange.containsDate(b) ? "enableButton" : "disableButton", "today"), this.toolbarsManager.proxyCall(e.isValid ? "enableButton" : "disableButton", "prev"), this.toolbarsManager.proxyCall(f.isValid ? "enableButton" : "disableButton", "next")
				}, a.prototype.queryToolbarsHeight = function () {
					return this.toolbarsManager.items.reduce(function (a, b) {
						return a + (b.el ? b.el.outerHeight(!0) : 0)
					}, 0)
				}, a.prototype.select = function (a, b) {
					this.view.select(this.buildSelectFootprint.apply(this, arguments))
				}, a.prototype.unselect = function () {
					this.view && this.view.unselect()
				}, a.prototype.buildSelectFootprint = function (a, b) {
					var c, d = this.moment(a).stripZone();
					return c = b ? this.moment(b).stripZone() : d.hasTime() ? d.clone().add(this.defaultTimedEventDuration) : d.clone().add(this.defaultAllDayEventDuration), new s.default(new r.default(d, c), !d.hasTime())
				}, a.prototype.initMomentInternals = function () {
					var a = this;
					this.defaultAllDayEventDuration = f.duration(this.opt("defaultAllDayEventDuration")), this.defaultTimedEventDuration = f.duration(this.opt("defaultTimedEventDuration")),
						this.optionsManager.watch("buildingMomentLocale", ["?locale", "?monthNames", "?monthNamesShort", "?dayNames", "?dayNamesShort", "?firstDay", "?weekNumberCalculation"], function (b) {
							var c, d = b.weekNumberCalculation,
								e = b.firstDay;
							"iso" === d && (d = "ISO");
							var f = Object.create(p.getMomentLocaleData(b.locale));
							b.monthNames && (f._months = b.monthNames), b.monthNamesShort && (f._monthsShort = b.monthNamesShort), b.dayNames && (f._weekdays = b.dayNames), b.dayNamesShort && (f._weekdaysShort = b.dayNamesShort), null == e && "ISO" === d && (e = 1), null != e && (c = Object.create(f._week), c.dow = e, f._week = c), "ISO" !== d && "local" !== d && "function" != typeof d || (f._fullCalendar_weekCalc = d), a.localeData = f, a.currentDate && a.localizeMoment(a.currentDate)
						})
				}, a.prototype.moment = function () {
					for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
					var c;
					return "local" === this.opt("timezone") ? (c = q.default.apply(null, a), c.hasTime() && c.local()) : c = "UTC" === this.opt("timezone") ? q.default.utc.apply(null, a) : q.default.parseZone.apply(null, a), this.localizeMoment(c), c
				}, a.prototype.msToMoment = function (a, b) {
					var c = q.default.utc(a);
					return b ? c.stripTime() : c = this.applyTimezone(c), this.localizeMoment(c), c
				}, a.prototype.msToUtcMoment = function (a, b) {
					var c = q.default.utc(a);
					return b && c.stripTime(), this.localizeMoment(c), c
				}, a.prototype.localizeMoment = function (a) {
					a._locale = this.localeData
				}, a.prototype.getIsAmbigTimezone = function () {
					return "local" !== this.opt("timezone") && "UTC" !== this.opt("timezone")
				}, a.prototype.applyTimezone = function (a) {
					if (!a.hasTime()) return a.clone();
					var b, c = this.moment(a.toArray()),
						d = a.time() - c.time();
					return d && (b = c.clone().add(d), a.time() - b.time() == 0 && (c = b)), c
				}, a.prototype.footprintToDateProfile = function (a, b) {
					var c, d = q.default.utc(a.unzonedRange.startMs);
					return b || (c = q.default.utc(a.unzonedRange.endMs)), a.isAllDay ? (d.stripTime(), c && c.stripTime()) : (d = this.applyTimezone(d), c && (c = this.applyTimezone(c))), new t.default(d, c, this)
				}, a.prototype.getNow = function () {
					var a = this.opt("now");
					return "function" == typeof a && (a = a()), this.moment(a).stripZone()
				}, a.prototype.humanizeDuration = function (a) {
					return a.locale(this.opt("locale")).humanize()
				}, a.prototype.parseUnzonedRange = function (a) {
					var b = null,
						c = null;
					return a.start && (b = this.moment(a.start).stripZone()), a.end && (c = this.moment(a.end).stripZone()), b || c ? b && c && c.isBefore(b) ? null : new r.default(b, c) : null
				}, a.prototype.initEventManager = function () {
					var a = this,
						b = new u.default(this),
						c = this.opt("eventSources") || [],
						d = this.opt("events");
					this.eventManager = b, d && c.unshift(d), b.on("release", function (b) {
						a.trigger("eventsReset", b)
					}), b.freeze(), c.forEach(function (c) {
						var d = w.default.parse(c, a);
						d && b.addSource(d)
					}), b.thaw()
				}, a.prototype.requestEvents = function (a, b) {
					return this.eventManager.requestEvents(a, b, this.opt("timezone"), !this.opt("lazyFetching"))
				}, a.prototype.getEventEnd = function (a) {
					return a.end ? a.end.clone() : this.getDefaultEventEnd(a.allDay, a.start)
				}, a.prototype.getDefaultEventEnd = function (a, b) {
					var c = b.clone();
					return a ? c.stripTime().add(this.defaultAllDayEventDuration) : c.add(this.defaultTimedEventDuration), this.getIsAmbigTimezone() && c.stripZone(), c
				}, a.prototype.rerenderEvents = function () {
					this.view.flash("displayingEvents")
				}, a.prototype.refetchEvents = function () {
					this.eventManager.refetchAllSources()
				}, a.prototype.renderEvents = function (a, b) {
					this.eventManager.freeze();
					for (var c = 0; c < a.length; c++) this.renderEvent(a[c], b);
					this.eventManager.thaw()
				}, a.prototype.renderEvent = function (a, b) {
					var c = this.eventManager,
						d = x.default.parse(a, a.source || c.stickySource);
					d && c.addEventDef(d, b)
				}, a.prototype.removeEvents = function (a) {
					var b, c, e = this.eventManager,
						f = [],
						g = {};
					if (null == a) e.removeAllEventDefs(!0);
					else {
						for (e.getEventInstances().forEach(function (a) {
								f.push(a.toLegacy())
							}), f = d(f, a), c = 0; c < f.length; c++) b = this.eventManager.getEventDefByUid(f[c]._id), g[b.id] = !0;
						e.freeze();
						for (c in g) e.removeEventDefsById(c, !0);
						e.thaw()
					}
				}, a.prototype.clientEvents = function (a) {
					var b = [];
					return this.eventManager.getEventInstances().forEach(function (a) {
						b.push(a.toLegacy())
					}), d(b, a)
				}, a.prototype.updateEvents = function (a) {
					this.eventManager.freeze();
					for (var b = 0; b < a.length; b++) this.updateEvent(a[b]);
					this.eventManager.thaw()
				}, a.prototype.updateEvent = function (a) {
					var b, c, d = this.eventManager.getEventDefByUid(a._id);
					d instanceof y.default && (b = d.buildInstance(), c = z.default.createFromRawProps(b, a, null), this.eventManager.mutateEventsWithId(d.id, c))
				}, a.prototype.getEventSources = function () {
					return this.eventManager.otherSources.slice()
				}, a.prototype.getEventSourceById = function (a) {
					return this.eventManager.getSourceById(A.default.normalizeId(a))
				}, a.prototype.addEventSource = function (a) {
					var b = w.default.parse(a, this);
					b && this.eventManager.addSource(b)
				}, a.prototype.removeEventSources = function (a) {
					var b, c, d = this.eventManager;
					if (null == a) this.eventManager.removeAllSources();
					else {
						for (b = d.multiQuerySources(a), d.freeze(), c = 0; c < b.length; c++) d.removeSource(b[c]);
						d.thaw()
					}
				}, a.prototype.removeEventSource = function (a) {
					var b, c = this.eventManager,
						d = c.querySources(a);
					for (c.freeze(), b = 0; b < d.length; b++) c.removeSource(d[b]);
					c.thaw()
				}, a.prototype.refetchEventSources = function (a) {
					var b, c = this.eventManager,
						d = c.multiQuerySources(a);
					for (c.freeze(), b = 0; b < d.length; b++) c.refetchSource(d[b]);
					c.thaw()
				}, a
			}();
		b.default = C, j.default.mixInto(C), k.default.mixInto(C)
	}, function (a, b, c) {
		function d(a, b, c) {
			var d;
			for (d = 0; d < a.length; d++)
				if (!b(a[d].eventInstance.toLegacy(), c ? c.toLegacy() : null)) return !1;
			return !0
		}

		function e(a, b) {
			var c, d, e, f, g = b.toLegacy();
			for (c = 0; c < a.length; c++) {
				if (d = a[c].eventInstance, e = d.def, !1 === (f = e.getOverlap())) return !1;
				if ("function" == typeof f && !f(d.toLegacy(), g)) return !1
			}
			return !0
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var f = c(4),
			g = c(10),
			h = c(33),
			i = c(5),
			j = c(23),
			k = function () {
				function a(a, b) {
					this.eventManager = a, this._calendar = b
				}
				return a.prototype.opt = function (a) {
					return this._calendar.opt(a)
				}, a.prototype.isEventInstanceGroupAllowed = function (a) {
					var b, c = a.getEventDef(),
						d = this.eventRangesToEventFootprints(a.getAllEventRanges()),
						e = this.getPeerEventInstances(c),
						f = e.map(j.eventInstanceToEventRange),
						g = this.eventRangesToEventFootprints(f),
						h = c.getConstraint(),
						i = c.getOverlap(),
						k = this.opt("eventAllow");
					for (b = 0; b < d.length; b++)
						if (!this.isFootprintAllowed(d[b].componentFootprint, g, h, i, d[b].eventInstance)) return !1;
					if (k)
						for (b = 0; b < d.length; b++)
							if (!1 === k(d[b].componentFootprint.toLegacy(this._calendar), d[b].getEventLegacy())) return !1;
					return !0
				}, a.prototype.getPeerEventInstances = function (a) {
					return this.eventManager.getEventInstancesWithoutId(a.id)
				}, a.prototype.isSelectionFootprintAllowed = function (a) {
					var b, c = this.eventManager.getEventInstances(),
						d = c.map(j.eventInstanceToEventRange),
						e = this.eventRangesToEventFootprints(d);
					return !(!this.isFootprintAllowed(a, e, this.opt("selectConstraint"), this.opt("selectOverlap")) || (b = this.opt("selectAllow")) && !1 === b(a.toLegacy(this._calendar)))
				}, a.prototype.isFootprintAllowed = function (a, b, c, f, g) {
					var h, i;
					if (null != c && (h = this.constraintValToFootprints(c, a.isAllDay), !this.isFootprintWithinConstraints(a, h))) return !1;
					if (i = this.collectOverlapEventFootprints(b, a), !1 === f) {
						if (i.length) return !1
					} else if ("function" == typeof f && !d(i, f, g)) return !1;
					return !(g && !e(i, g))
				}, a.prototype.isFootprintWithinConstraints = function (a, b) {
					var c;
					for (c = 0; c < b.length; c++)
						if (this.footprintContainsFootprint(b[c], a)) return !0;
					return !1
				}, a.prototype.constraintValToFootprints = function (a, b) {
					var c;
					return "businessHours" === a ? this.buildCurrentBusinessFootprints(b) : "object" == typeof a ? (c = this.parseEventDefToInstances(a), c ? this.eventInstancesToFootprints(c) : this.parseFootprints(a)) : null != a ? (c = this.eventManager.getEventInstancesWithId(a), this.eventInstancesToFootprints(c)) : void 0
				}, a.prototype.buildCurrentBusinessFootprints = function (a) {
					var b = this._calendar.view,
						c = b.get("businessHourGenerator"),
						d = b.dateProfile.activeUnzonedRange,
						e = c.buildEventInstanceGroup(a, d);
					return e ? this.eventInstancesToFootprints(e.eventInstances) : []
				}, a.prototype.eventInstancesToFootprints = function (a) {
					var b = a.map(j.eventInstanceToEventRange);
					return this.eventRangesToEventFootprints(b).map(j.eventFootprintToComponentFootprint)
				}, a.prototype.collectOverlapEventFootprints = function (a, b) {
					var c, d = [];
					for (c = 0; c < a.length; c++) this.footprintsIntersect(b, a[c].componentFootprint) && d.push(a[c]);
					return d
				}, a.prototype.parseEventDefToInstances = function (a) {
					var b = this.eventManager,
						c = h.default.parse(a, new i.default(this._calendar));
					return !!c && c.buildInstances(b.currentPeriod.unzonedRange)
				}, a.prototype.eventRangesToEventFootprints = function (a) {
					var b, c = [];
					for (b = 0; b < a.length; b++) c.push.apply(c, this.eventRangeToEventFootprints(a[b]));
					return c
				}, a.prototype.eventRangeToEventFootprints = function (a) {
					return [j.eventRangeToEventFootprint(a)]
				}, a.prototype.parseFootprints = function (a) {
					var b, c;
					return a.start && (b = this._calendar.moment(a.start), b.isValid() || (b = null)), a.end && (c = this._calendar.moment(a.end), c.isValid() || (c = null)), [new g.default(new f.default(b, c), b && !b.hasTime() || c && !c.hasTime())]
				}, a.prototype.footprintContainsFootprint = function (a, b) {
					return a.unzonedRange.containsRange(b.unzonedRange)
				}, a.prototype.footprintsIntersect = function (a, b) {
					return a.unzonedRange.intersectsWith(b.unzonedRange)
				}, a
			}();
		b.default = k
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = c(12),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.applyProps = function (a) {
					var b, c = this.standardPropMap,
						d = {},
						e = {};
					for (b in a) !0 === c[b] ? this[b] = a[b] : !1 === c[b] ? d[b] = a[b] : e[b] = a[b];
					return this.applyMiscProps(e), this.applyManualStandardProps(d)
				}, b.prototype.applyManualStandardProps = function (a) {
					return !0
				}, b.prototype.applyMiscProps = function (a) {}, b.prototype.isStandardProp = function (a) {
					return a in this.standardPropMap
				}, b.defineStandardProps = function (a) {
					var b = this.prototype;
					b.hasOwnProperty("standardPropMap") || (b.standardPropMap = Object.create(b.standardPropMap)), e.copyOwnProps(a, b.standardPropMap)
				}, b.copyVerbatimStandardProps = function (a, b) {
					var c, d = this.prototype.standardPropMap;
					for (c in d) null != a[c] && !0 === d[c] && (b[c] = a[c])
				}, b
			}(f.default);
		b.default = g, g.prototype.standardPropMap = {}
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a, b) {
				this.def = a, this.dateProfile = b
			}
			return a.prototype.toLegacy = function () {
				var a = this.dateProfile,
					b = this.def.toLegacy();
				return b.start = a.start.clone(), b.end = a.end ? a.end.clone() : null, b
			}, a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(3),
			g = c(22),
			h = c(51),
			i = c(15),
			j = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.isAllDay = function () {
					return !this.startTime && !this.endTime
				}, b.prototype.buildInstances = function (a) {
					for (var b, c, d, e = this.source.calendar, f = a.getStart(), g = a.getEnd(), j = []; f.isBefore(g);) this.dowHash && !this.dowHash[f.day()] || (b = e.applyTimezone(f), c = b.clone(), d = null, this.startTime ? c.time(this.startTime) : c.stripTime(), this.endTime && (d = b.clone().time(this.endTime)), j.push(new h.default(this, new i.default(c, d, e)))), f.add(1, "days");
					return j
				}, b.prototype.setDow = function (a) {
					this.dowHash || (this.dowHash = {});
					for (var b = 0; b < a.length; b++) this.dowHash[a[b]] = !0
				}, b.prototype.clone = function () {
					var b = a.prototype.clone.call(this);
					return b.startTime && (b.startTime = f.duration(this.startTime)), b.endTime && (b.endTime = f.duration(this.endTime)), this.dowHash && (b.dowHash = e.extend({}, this.dowHash)), b
				}, b
			}(g.default);
		b.default = j, j.prototype.applyProps = function (a) {
			var b = g.default.prototype.applyProps.call(this, a);
			return a.start && (this.startTime = f.duration(a.start)), a.end && (this.endTime = f.duration(a.end)), a.dow && this.setDow(a.dow), b
		}, j.defineStandardProps({
			start: !1,
			end: !1,
			dow: !1
		})
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a, b, c) {
				this.unzonedRange = a, this.eventDef = b, c && (this.eventInstance = c)
			}
			return a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(23),
			f = c(17),
			g = c(52),
			h = c(5),
			i = {
				start: "09:00",
				end: "17:00",
				dow: [1, 2, 3, 4, 5],
				rendering: "inverse-background"
			},
			j = function () {
				function a(a, b) {
					this.rawComplexDef = a, this.calendar = b
				}
				return a.prototype.buildEventInstanceGroup = function (a, b) {
					var c, d = this.buildEventDefs(a);
					if (d.length) return c = new f.default(e.eventDefsToEventInstances(d, b)), c.explicitEventDef = d[0], c
				}, a.prototype.buildEventDefs = function (a) {
					var b, c = this.rawComplexDef,
						e = [],
						f = !1,
						g = [];
					for (!0 === c ? e = [{}] : d.isPlainObject(c) ? e = [c] : d.isArray(c) && (e = c, f = !0), b = 0; b < e.length; b++) f && !e[b].dow || g.push(this.buildEventDef(a, e[b]));
					return g
				}, a.prototype.buildEventDef = function (a, b) {
					var c = d.extend({}, i, b);
					return a && (c.start = null, c.end = null), g.default.parse(c, new h.default(this.calendar))
				}, a
			}();
		b.default = j
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(27),
			f = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b
			}(e.default);
		b.default = f, f.prototype.classes = {
			widget: "fc-unthemed",
			widgetHeader: "fc-widget-header",
			widgetContent: "fc-widget-content",
			buttonGroup: "fc-button-group",
			button: "fc-button",
			cornerLeft: "fc-corner-left",
			cornerRight: "fc-corner-right",
			stateDefault: "fc-state-default",
			stateActive: "fc-state-active",
			stateDisabled: "fc-state-disabled",
			stateHover: "fc-state-hover",
			stateDown: "fc-state-down",
			popoverHeader: "fc-widget-header",
			popoverContent: "fc-widget-content",
			headerRow: "fc-widget-header",
			dayRow: "fc-widget-content",
			listView: "fc-widget-content"
		}, f.prototype.baseIconClass = "fc-icon", f.prototype.iconClasses = {
			close: "fc-icon-x",
			prev: "fc-icon-left-single-arrow",
			next: "fc-icon-right-single-arrow",
			prevYear: "fc-icon-left-double-arrow",
			nextYear: "fc-icon-right-double-arrow"
		}, f.prototype.iconOverrideOption = "buttonIcons", f.prototype.iconOverrideCustomButtonOption = "icon", f.prototype.iconOverridePrefix = "fc-icon-"
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(27),
			f = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b
			}(e.default);
		b.default = f, f.prototype.classes = {
			widget: "ui-widget",
			widgetHeader: "ui-widget-header",
			widgetContent: "ui-widget-content",
			buttonGroup: "fc-button-group",
			button: "ui-button",
			cornerLeft: "ui-corner-left",
			cornerRight: "ui-corner-right",
			stateDefault: "ui-state-default",
			stateActive: "ui-state-active",
			stateDisabled: "ui-state-disabled",
			stateHover: "ui-state-hover",
			stateDown: "ui-state-down",
			today: "ui-state-highlight",
			popoverHeader: "ui-widget-header",
			popoverContent: "ui-widget-content",
			headerRow: "ui-widget-header",
			dayRow: "ui-widget-content",
			listView: "ui-widget-content"
		}, f.prototype.baseIconClass = "ui-icon", f.prototype.iconClasses = {
			close: "ui-icon-closethick",
			prev: "ui-icon-circle-triangle-w",
			next: "ui-icon-circle-triangle-e",
			prevYear: "ui-icon-seek-prev",
			nextYear: "ui-icon-seek-next"
		}, f.prototype.iconOverrideOption = "themeButtonIcons", f.prototype.iconOverrideCustomButtonOption = "themeIcon", f.prototype.iconOverridePrefix = "ui-icon-"
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(16),
			g = c(5),
			h = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.fetch = function (a, b, c) {
					var d = this;
					return this.calendar.pushLoading(), f.default.construct(function (e) {
						d.func.call(d.calendar, a.clone(), b.clone(), c, function (a) {
							d.calendar.popLoading(), e(d.parseEventDefs(a))
						})
					})
				}, b.prototype.getPrimitive = function () {
					return this.func
				}, b.prototype.applyManualStandardProps = function (b) {
					var c = a.prototype.applyManualStandardProps.call(this, b);
					return this.func = b.events, c
				}, b.parse = function (a, b) {
					var c;
					return e.isFunction(a.events) ? c = a : e.isFunction(a) && (c = {
						events: a
					}), !!c && g.default.parse.call(this, c, b)
				}, b
			}(g.default);
		b.default = h, h.defineStandardProps({
			events: !1
		})
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(16),
			h = c(5),
			i = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.fetch = function (a, c, d) {
					var h = this,
						i = this.ajaxSettings,
						j = i.success,
						k = i.error,
						l = this.buildRequestParams(a, c, d);
					return this.calendar.pushLoading(), g.default.construct(function (a, c) {
						e.ajax(e.extend({}, b.AJAX_DEFAULTS, i, {
							url: h.url,
							data: l,
							success: function (b, d, g) {
								var i;
								h.calendar.popLoading(), b ? (i = f.applyAll(j, h, [b, d, g]), e.isArray(i) && (b = i), a(h.parseEventDefs(b))) : c()
							},
							error: function (a, b, d) {
								h.calendar.popLoading(), f.applyAll(k, h, [a, b, d]), c()
							}
						}))
					})
				}, b.prototype.buildRequestParams = function (a, b, c) {
					var d, f, g, h, i = this.calendar,
						j = this.ajaxSettings,
						k = {};
					return d = this.startParam, null == d && (d = i.opt("startParam")), f = this.endParam, null == f && (f = i.opt("endParam")), g = this.timezoneParam, null == g && (g = i.opt("timezoneParam")), h = e.isFunction(j.data) ? j.data() : j.data || {}, e.extend(k, h), k[d] = a.format(), k[f] = b.format(), c && "local" !== c && (k[g] = c), k
				}, b.prototype.getPrimitive = function () {
					return this.url
				}, b.prototype.applyMiscProps = function (a) {
					this.ajaxSettings = a
				}, b.parse = function (a, b) {
					var c;
					return "string" == typeof a.url ? c = a : "string" == typeof a && (c = {
						url: a
					}), !!c && h.default.parse.call(this, c, b)
				}, b.AJAX_DEFAULTS = {
					dataType: "json",
					cache: !1
				}, b
			}(h.default);
		b.default = i, i.defineStandardProps({
			url: !0,
			startParam: !0,
			endParam: !0,
			timezoneParam: !0
		})
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(8),
			e = function () {
				function a() {
					this.q = [], this.isPaused = !1, this.isRunning = !1
				}
				return a.prototype.queue = function () {
					for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
					this.q.push.apply(this.q, a), this.tryStart()
				}, a.prototype.pause = function () {
					this.isPaused = !0
				}, a.prototype.resume = function () {
					this.isPaused = !1, this.tryStart()
				}, a.prototype.getIsIdle = function () {
					return !this.isRunning && !this.isPaused
				}, a.prototype.tryStart = function () {
					!this.isRunning && this.canRunNext() && (this.isRunning = !0, this.trigger("start"), this.runRemaining())
				}, a.prototype.canRunNext = function () {
					return !this.isPaused && this.q.length
				}, a.prototype.runRemaining = function () {
					var a, b, c = this;
					do {
						if (a = this.q.shift(), (b = this.runTask(a)) && b.then) return void b.then(function () {
							c.canRunNext() && c.runRemaining()
						})
					} while (this.canRunNext());
					this.trigger("stop"), this.isRunning = !1, this.tryStart()
				}, a.prototype.runTask = function (a) {
					return a()
				}, a
			}();
		b.default = e, d.default.mixInto(e)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(59),
			f = function (a) {
				function b(b) {
					var c = a.call(this) || this;
					return c.waitsByNamespace = b || {}, c
				}
				return d.__extends(b, a), b.prototype.queue = function (a, b, c) {
					var d, e = {
						func: a,
						namespace: b,
						type: c
					};
					b && (d = this.waitsByNamespace[b]), this.waitNamespace && (b === this.waitNamespace && null != d ? this.delayWait(d) : (this.clearWait(), this.tryStart())), this.compoundTask(e) && (this.waitNamespace || null == d ? this.tryStart() : this.startWait(b, d))
				}, b.prototype.startWait = function (a, b) {
					this.waitNamespace = a, this.spawnWait(b)
				}, b.prototype.delayWait = function (a) {
					clearTimeout(this.waitId), this.spawnWait(a)
				}, b.prototype.spawnWait = function (a) {
					var b = this;
					this.waitId = setTimeout(function () {
						b.waitNamespace = null, b.tryStart()
					}, a)
				}, b.prototype.clearWait = function () {
					this.waitNamespace && (clearTimeout(this.waitId), this.waitId = null, this.waitNamespace = null)
				}, b.prototype.canRunNext = function () {
					if (!a.prototype.canRunNext.call(this)) return !1;
					if (this.waitNamespace) {
						for (var b = this.q, c = 0; c < b.length; c++)
							if (b[c].namespace !== this.waitNamespace) return !0;
						return !1
					}
					return !0
				}, b.prototype.runTask = function (a) {
					a.func()
				}, b.prototype.compoundTask = function (a) {
					var b, c, d = this.q,
						e = !0;
					if (a.namespace && "destroy" === a.type)
						for (b = d.length - 1; b >= 0; b--) switch (c = d[b], c.type) {
							case "init":
								e = !1;
							case "add":
							case "remove":
								d.splice(b, 1)
						}
					return e && d.push(a), e
				}, b
			}(e.default);
		b.default = f
	}, function (a, b, c) {
		function d(a) {
			var b, c, d, e = [];
			for (b in a)
				for (c = a[b].eventInstances, d = 0; d < c.length; d++) e.push(c[d].toLegacy());
			return e
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(0),
			f = c(1),
			g = c(3),
			h = c(2),
			i = c(9),
			j = c(37),
			k = c(80),
			l = c(23),
			m = function (a) {
				function b(c, d) {
					var e = a.call(this) || this;
					return e.isRTL = !1, e.hitsNeededDepth = 0, e.hasAllDayBusinessHours = !1, e.isDatesRendered = !1, c && (e.view = c), d && (e.options = d), e.uid = String(b.guid++), e.childrenByUid = {}, e.nextDayThreshold = g.duration(e.opt("nextDayThreshold")), e.isRTL = e.opt("isRTL"), e.fillRendererClass && (e.fillRenderer = new e.fillRendererClass(e)), e.eventRendererClass && (e.eventRenderer = new e.eventRendererClass(e, e.fillRenderer)), e.helperRendererClass && e.eventRenderer && (e.helperRenderer = new e.helperRendererClass(e, e.eventRenderer)), e.businessHourRendererClass && e.fillRenderer && (e.businessHourRenderer = new e.businessHourRendererClass(e, e.fillRenderer)), e
				}
				return e.__extends(b, a), b.prototype.addChild = function (a) {
					return !this.childrenByUid[a.uid] && (this.childrenByUid[a.uid] = a, !0)
				}, b.prototype.removeChild = function (a) {
					return !!this.childrenByUid[a.uid] && (delete this.childrenByUid[a.uid], !0)
				}, b.prototype.updateSize = function (a, b, c) {
					this.callChildren("updateSize", arguments)
				}, b.prototype.opt = function (a) {
					return this._getView().opt(a)
				}, b.prototype.publiclyTrigger = function () {
					for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
					var c = this._getCalendar();
					return c.publiclyTrigger.apply(c, a)
				}, b.prototype.hasPublicHandlers = function () {
					for (var a = [], b = 0; b < arguments.length; b++) a[b] = arguments[b];
					var c = this._getCalendar();
					return c.hasPublicHandlers.apply(c, a)
				}, b.prototype.executeDateRender = function (a) {
					this.dateProfile = a, this.renderDates(a), this.isDatesRendered = !0, this.callChildren("executeDateRender", arguments)
				}, b.prototype.executeDateUnrender = function () {
					this.callChildren("executeDateUnrender", arguments), this.dateProfile = null, this.unrenderDates(), this.isDatesRendered = !1
				}, b.prototype.renderDates = function (a) {}, b.prototype.unrenderDates = function () {}, b.prototype.getNowIndicatorUnit = function () {}, b.prototype.renderNowIndicator = function (a) {
					this.callChildren("renderNowIndicator", arguments)
				}, b.prototype.unrenderNowIndicator = function () {
					this.callChildren("unrenderNowIndicator", arguments)
				}, b.prototype.renderBusinessHours = function (a) {
					this.businessHourRenderer && this.businessHourRenderer.render(a), this.callChildren("renderBusinessHours", arguments)
				}, b.prototype.unrenderBusinessHours = function () {
					this.callChildren("unrenderBusinessHours", arguments), this.businessHourRenderer && this.businessHourRenderer.unrender()
				}, b.prototype.executeEventRender = function (a) {
					this.eventRenderer ? (this.eventRenderer.rangeUpdated(), this.eventRenderer.render(a)) : this.renderEvents && this.renderEvents(d(a)), this.callChildren("executeEventRender", arguments)
				}, b.prototype.executeEventUnrender = function () {
					this.callChildren("executeEventUnrender", arguments), this.eventRenderer ? this.eventRenderer.unrender() : this.destroyEvents && this.destroyEvents()
				}, b.prototype.getBusinessHourSegs = function () {
					var a = this.getOwnBusinessHourSegs();
					return this.iterChildren(function (b) {
						a.push.apply(a, b.getBusinessHourSegs())
					}), a
				}, b.prototype.getOwnBusinessHourSegs = function () {
					return this.businessHourRenderer ? this.businessHourRenderer.getSegs() : []
				}, b.prototype.getEventSegs = function () {
					var a = this.getOwnEventSegs();
					return this.iterChildren(function (b) {
						a.push.apply(a, b.getEventSegs())
					}), a
				}, b.prototype.getOwnEventSegs = function () {
					return this.eventRenderer ? this.eventRenderer.getSegs() : []
				}, b.prototype.triggerAfterEventsRendered = function () {
					this.triggerAfterEventSegsRendered(this.getEventSegs()), this.publiclyTrigger("eventAfterAllRender", {
						context: this,
						args: [this]
					})
				}, b.prototype.triggerAfterEventSegsRendered = function (a) {
					var b = this;
					this.hasPublicHandlers("eventAfterRender") && a.forEach(function (a) {
						var c;
						a.el && (c = a.footprint.getEventLegacy(), b.publiclyTrigger("eventAfterRender", {
							context: c,
							args: [c, a.el, b]
						}))
					})
				}, b.prototype.triggerBeforeEventsDestroyed = function () {
					this.triggerBeforeEventSegsDestroyed(this.getEventSegs())
				}, b.prototype.triggerBeforeEventSegsDestroyed = function (a) {
					var b = this;
					this.hasPublicHandlers("eventDestroy") && a.forEach(function (a) {
						var c;
						a.el && (c = a.footprint.getEventLegacy(), b.publiclyTrigger("eventDestroy", {
							context: c,
							args: [c, a.el, b]
						}))
					})
				}, b.prototype.showEventsWithId = function (a) {
					this.getEventSegs().forEach(function (b) {
						b.footprint.eventDef.id === a && b.el && b.el.css("visibility", "")
					}), this.callChildren("showEventsWithId", arguments)
				}, b.prototype.hideEventsWithId = function (a) {
					this.getEventSegs().forEach(function (b) {
						b.footprint.eventDef.id === a && b.el && b.el.css("visibility", "hidden")
					}), this.callChildren("hideEventsWithId", arguments)
				}, b.prototype.renderDrag = function (a, b, c) {
					var d = !1;
					return this.iterChildren(function (e) {
						e.renderDrag(a, b, c) && (d = !0)
					}), d
				}, b.prototype.unrenderDrag = function () {
					this.callChildren("unrenderDrag", arguments)
				}, b.prototype.renderEventResize = function (a, b, c) {
					this.callChildren("renderEventResize", arguments)
				}, b.prototype.unrenderEventResize = function () {
					this.callChildren("unrenderEventResize", arguments)
				}, b.prototype.renderSelectionFootprint = function (a) {
					this.renderHighlight(a), this.callChildren("renderSelectionFootprint", arguments)
				}, b.prototype.unrenderSelection = function () {
					this.unrenderHighlight(), this.callChildren("unrenderSelection", arguments)
				}, b.prototype.renderHighlight = function (a) {
					this.fillRenderer && this.fillRenderer.renderFootprint("highlight", a, {
						getClasses: function () {
							return ["fc-highlight"]
						}
					}), this.callChildren("renderHighlight", arguments)
				}, b.prototype.unrenderHighlight = function () {
					this.fillRenderer && this.fillRenderer.unrender("highlight"), this.callChildren("unrenderHighlight", arguments)
				}, b.prototype.hitsNeeded = function () {
					this.hitsNeededDepth++ || this.prepareHits(), this.callChildren("hitsNeeded", arguments)
				}, b.prototype.hitsNotNeeded = function () {
					this.hitsNeededDepth && !--this.hitsNeededDepth && this.releaseHits(), this.callChildren("hitsNotNeeded", arguments)
				}, b.prototype.prepareHits = function () {}, b.prototype.releaseHits = function () {}, b.prototype.queryHit = function (a, b) {
					var c, d, e = this.childrenByUid;
					for (c in e)
						if (d = e[c].queryHit(a, b)) break;
					return d
				}, b.prototype.getSafeHitFootprint = function (a) {
					var b = this.getHitFootprint(a);
					return this.dateProfile.activeUnzonedRange.containsRange(b.unzonedRange) ? b : null
				}, b.prototype.getHitFootprint = function (a) {}, b.prototype.getHitEl = function (a) {}, b.prototype.eventRangesToEventFootprints = function (a) {
					var b, c = [];
					for (b = 0; b < a.length; b++) c.push.apply(c, this.eventRangeToEventFootprints(a[b]));
					return c
				}, b.prototype.eventRangeToEventFootprints = function (a) {
					return [l.eventRangeToEventFootprint(a)]
				}, b.prototype.eventFootprintsToSegs = function (a) {
					var b, c = [];
					for (b = 0; b < a.length; b++) c.push.apply(c, this.eventFootprintToSegs(a[b]));
					return c
				}, b.prototype.eventFootprintToSegs = function (a) {
					var b, c, d, e = a.componentFootprint.unzonedRange;
					for (b = this.componentFootprintToSegs(a.componentFootprint), c = 0; c < b.length; c++) d = b[c], e.isStart || (d.isStart = !1), e.isEnd || (d.isEnd = !1), d.footprint = a;
					return b
				}, b.prototype.componentFootprintToSegs = function (a) {
					return []
				}, b.prototype.callChildren = function (a, b) {
					this.iterChildren(function (c) {
						c[a].apply(c, b)
					})
				}, b.prototype.iterChildren = function (a) {
					var b, c = this.childrenByUid;
					for (b in c) a(c[b])
				}, b.prototype._getCalendar = function () {
					var a = this;
					return a.calendar || a.view.calendar
				}, b.prototype._getView = function () {
					return this.view
				}, b.prototype._getDateProfile = function () {
					return this._getView().get("dateProfile")
				}, b.prototype.buildGotoAnchorHtml = function (a, b, c) {
					var d, e, g, j;
					return f.isPlainObject(a) ? (d = a.date, e = a.type, g = a.forceOff) : d = a, d = i.default(d), j = {
						date: d.format("YYYY-MM-DD"),
						type: e || "day"
					}, "string" == typeof b && (c = b, b = null), b = b ? " " + h.attrsToStr(b) : "", c = c || "", !g && this.opt("navLinks") ? "<a" + b + ' data-goto="' + h.htmlEscape(JSON.stringify(j)) + '">' + c + "</a>" : "<span" + b + ">" + c + "</span>"
				}, b.prototype.getAllDayHtml = function () {
					return this.opt("allDayHtml") || h.htmlEscape(this.opt("allDayText"))
				}, b.prototype.getDayClasses = function (a, b) {
					var c, d = this._getView(),
						e = [];
					return this.dateProfile.activeUnzonedRange.containsDate(a) ? (e.push("fc-" + h.dayIDs[a.day()]), d.isDateInOtherMonth(a, this.dateProfile) && e.push("fc-other-month"), c = d.calendar.getNow(), a.isSame(c, "day") ? (e.push("fc-today"), !0 !== b && e.push(d.calendar.theme.getClass("today"))) : a < c ? e.push("fc-past") : e.push("fc-future")) : e.push("fc-disabled-day"), e
				}, b.prototype.formatRange = function (a, b, c, d) {
					var e = a.end;
					return b && (e = e.clone().subtract(1)), j.formatRange(a.start, e, c, d, this.isRTL)
				}, b.prototype.currentRangeAs = function (a) {
					return this._getDateProfile().currentUnzonedRange.as(a)
				}, b.prototype.computeDayRange = function (a) {
					var b = this._getCalendar(),
						c = b.msToUtcMoment(a.startMs, !0),
						d = b.msToUtcMoment(a.endMs),
						e = +d.time(),
						f = d.clone().stripTime();
					return e && e >= this.nextDayThreshold && f.add(1, "days"), f <= c && (f = c.clone().add(1, "days")), {
						start: c,
						end: f
					}
				}, b.prototype.isMultiDayRange = function (a) {
					var b = this.computeDayRange(a);
					return b.end.diff(b.start, "days") > 1
				}, b.guid = 0, b
			}(k.default);
		b.default = m
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(3),
			e = c(2),
			f = c(4),
			g = function () {
				function a(a) {
					this._view = a
				}
				return a.prototype.opt = function (a) {
					return this._view.opt(a)
				}, a.prototype.trimHiddenDays = function (a) {
					return this._view.trimHiddenDays(a)
				}, a.prototype.msToUtcMoment = function (a, b) {
					return this._view.calendar.msToUtcMoment(a, b)
				}, a.prototype.buildPrev = function (a) {
					var b = a.date.clone().startOf(a.currentRangeUnit).subtract(a.dateIncrement);
					return this.build(b, -1)
				}, a.prototype.buildNext = function (a) {
					var b = a.date.clone().startOf(a.currentRangeUnit).add(a.dateIncrement);
					return this.build(b, 1)
				}, a.prototype.build = function (a, b, c) {
					void 0 === c && (c = !1);
					var e, f, g, h, i, j, k = !a.hasTime(),
						l = null,
						m = null;
					return e = this.buildValidRange(), e = this.trimHiddenDays(e), c && (a = this.msToUtcMoment(e.constrainDate(a), k)), f = this.buildCurrentRangeInfo(a, b), g = /^(year|month|week|day)$/.test(f.unit), h = this.buildRenderRange(this.trimHiddenDays(f.unzonedRange), f.unit, g), h = this.trimHiddenDays(h), i = h.clone(), this.opt("showNonCurrentDates") || (i = i.intersect(f.unzonedRange)), l = d.duration(this.opt("minTime")), m = d.duration(this.opt("maxTime")), i = this.adjustActiveRange(i, l, m), i = i.intersect(e), i && (a = this.msToUtcMoment(i.constrainDate(a), k)), j = f.unzonedRange.intersectsWith(e), {
						validUnzonedRange: e,
						currentUnzonedRange: f.unzonedRange,
						currentRangeUnit: f.unit,
						isRangeAllDay: g,
						activeUnzonedRange: i,
						renderUnzonedRange: h,
						minTime: l,
						maxTime: m,
						isValid: j,
						date: a,
						dateIncrement: this.buildDateIncrement(f.duration)
					}
				}, a.prototype.buildValidRange = function () {
					return this._view.getUnzonedRangeOption("validRange", this._view.calendar.getNow()) || new f.default
				}, a.prototype.buildCurrentRangeInfo = function (a, b) {
					var c, d = this._view.viewSpec,
						f = null,
						g = null,
						h = null;
					return d.duration ? (f = d.duration, g = d.durationUnit, h = this.buildRangeFromDuration(a, b, f, g)) : (c = this.opt("dayCount")) ? (g = "day", h = this.buildRangeFromDayCount(a, b, c)) : (h = this.buildCustomVisibleRange(a)) ? g = e.computeGreatestUnit(h.getStart(), h.getEnd()) : (f = this.getFallbackDuration(), g = e.computeGreatestUnit(f), h = this.buildRangeFromDuration(a, b, f, g)), {
						duration: f,
						unit: g,
						unzonedRange: h
					}
				}, a.prototype.getFallbackDuration = function () {
					return d.duration({
						days: 1
					})
				}, a.prototype.adjustActiveRange = function (a, b, c) {
					var d = a.getStart(),
						e = a.getEnd();
					return this._view.usesMinMaxTime && (b < 0 && d.time(0).add(b), c > 864e5 && e.time(c - 864e5)), new f.default(d, e)
				}, a.prototype.buildRangeFromDuration = function (a, b, c, g) {
					function h() {
						k = a.clone().startOf(n), l = k.clone().add(c), m = new f.default(k, l)
					}
					var i, j, k, l, m, n = this.opt("dateAlignment");
					return n || (i = this.opt("dateIncrement"), i ? (j = d.duration(i), n = j < c ? e.computeDurationGreatestUnit(j, i) : g) : n = g), c.as("days") <= 1 && this._view.isHiddenDay(k) && (k = this._view.skipHiddenDays(k, b), k.startOf("day")), h(), this.trimHiddenDays(m) || (a = this._view.skipHiddenDays(a, b), h()), m
				}, a.prototype.buildRangeFromDayCount = function (a, b, c) {
					var d, e = this.opt("dateAlignment"),
						g = 0,
						h = a.clone();
					e && h.startOf(e), h.startOf("day"), h = this._view.skipHiddenDays(h, b), d = h.clone();
					do {
						d.add(1, "day"), this._view.isHiddenDay(d) || g++
					} while (g < c);
					return new f.default(h, d)
				}, a.prototype.buildCustomVisibleRange = function (a) {
					var b = this._view.getUnzonedRangeOption("visibleRange", this._view.calendar.applyTimezone(a));
					return !b || null != b.startMs && null != b.endMs ? b : null
				}, a.prototype.buildRenderRange = function (a, b, c) {
					return a.clone()
				}, a.prototype.buildDateIncrement = function (a) {
					var b, c = this.opt("dateIncrement");
					return c ? d.duration(c) : (b = this.opt("dateAlignment")) ? d.duration(1, b) : a || d.duration({
						days: 1
					})
				}, a
			}();
		b.default = g
	}, function (a, b, c) {
		function d(a) {
			var b, c, d, e, i = h.default.dataAttrPrefix;
			return i && (i += "-"), b = a.data(i + "event") || null, b && (b = "object" == typeof b ? f.extend({}, b) : {}, c = b.start, null == c && (c = b.time), d = b.duration,
				e = b.stick, delete b.start, delete b.time, delete b.duration, delete b.stick), null == c && (c = a.data(i + "start")), null == c && (c = a.data(i + "time")), null == d && (d = a.data(i + "duration")), null == e && (e = a.data(i + "stick")), c = null != c ? g.duration(c) : null, d = null != d ? g.duration(d) : null, e = Boolean(e), {
				eventProps: b,
				startTime: c,
				duration: d,
				stick: e
			}
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(0),
			f = c(1),
			g = c(3),
			h = c(7),
			i = c(2),
			j = c(9),
			k = c(6),
			l = c(18),
			m = c(11),
			n = c(17),
			o = c(5),
			p = c(13),
			q = function (a) {
				function b() {
					var b = null !== a && a.apply(this, arguments) || this;
					return b.isDragging = !1, b
				}
				return e.__extends(b, a), b.prototype.end = function () {
					this.dragListener && this.dragListener.endInteraction()
				}, b.prototype.bindToDocument = function () {
					this.listenTo(f(document), {
						dragstart: this.handleDragStart,
						sortstart: this.handleDragStart
					})
				}, b.prototype.unbindFromDocument = function () {
					this.stopListeningTo(f(document))
				}, b.prototype.handleDragStart = function (a, b) {
					var c, d;
					this.opt("droppable") && (c = f((b ? b.item : null) || a.target), d = this.opt("dropAccept"), (f.isFunction(d) ? d.call(c[0], c) : c.is(d)) && (this.isDragging || this.listenToExternalDrag(c, a, b)))
				}, b.prototype.listenToExternalDrag = function (a, b, c) {
					var e, f = this,
						g = this.component,
						h = this.view,
						j = d(a);
					(this.dragListener = new l.default(g, {
						interactionStart: function () {
							f.isDragging = !0
						},
						hitOver: function (a) {
							var b, c = !0,
								d = a.component.getSafeHitFootprint(a);
							d ? (e = f.computeExternalDrop(d, j), e ? (b = new n.default(e.buildInstances()), c = j.eventProps ? g.isEventInstanceGroupAllowed(b) : g.isExternalInstanceGroupAllowed(b)) : c = !1) : c = !1, c || (e = null, i.disableCursor()), e && g.renderDrag(g.eventRangesToEventFootprints(b.sliceRenderRanges(g.dateProfile.renderUnzonedRange, h.calendar)))
						},
						hitOut: function () {
							e = null
						},
						hitDone: function () {
							i.enableCursor(), g.unrenderDrag()
						},
						interactionEnd: function (b) {
							e && h.reportExternalDrop(e, Boolean(j.eventProps), Boolean(j.stick), a, b, c), f.isDragging = !1, f.dragListener = null
						}
					})).startDrag(b)
				}, b.prototype.computeExternalDrop = function (a, b) {
					var c, d = this.view.calendar,
						e = j.default.utc(a.unzonedRange.startMs).stripZone();
					return a.isAllDay && (b.startTime ? e.time(b.startTime) : e.stripTime()), b.duration && (c = e.clone().add(b.duration)), e = d.applyTimezone(e), c && (c = d.applyTimezone(c)), m.default.parse(f.extend({}, b.eventProps, {
						start: e,
						end: c
					}), new o.default(d))
				}, b
			}(p.default);
		b.default = q, k.default.mixInto(q)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(26),
			h = c(35),
			i = c(18),
			j = c(13),
			k = function (a) {
				function b(b, c) {
					var d = a.call(this, b) || this;
					return d.isResizing = !1, d.eventPointing = c, d
				}
				return d.__extends(b, a), b.prototype.end = function () {
					this.dragListener && this.dragListener.endInteraction()
				}, b.prototype.bindToEl = function (a) {
					var b = this.component;
					b.bindSegHandlerToEl(a, "mousedown", this.handleMouseDown.bind(this)), b.bindSegHandlerToEl(a, "touchstart", this.handleTouchStart.bind(this))
				}, b.prototype.handleMouseDown = function (a, b) {
					this.component.canStartResize(a, b) && this.buildDragListener(a, e(b.target).is(".fc-start-resizer")).startInteraction(b, {
						distance: 5
					})
				}, b.prototype.handleTouchStart = function (a, b) {
					this.component.canStartResize(a, b) && this.buildDragListener(a, e(b.target).is(".fc-start-resizer")).startInteraction(b)
				}, b.prototype.buildDragListener = function (a, b) {
					var c, d, e = this,
						g = this.component,
						h = this.view,
						j = h.calendar,
						k = j.eventManager,
						l = a.el,
						m = a.footprint.eventDef,
						n = a.footprint.eventInstance;
					return this.dragListener = new i.default(g, {
						scroll: this.opt("dragScroll"),
						subjectEl: l,
						interactionStart: function () {
							c = !1
						},
						dragStart: function (b) {
							c = !0, e.eventPointing.handleMouseout(a, b), e.segResizeStart(a, b)
						},
						hitOver: function (c, i, l) {
							var n, o = !0,
								p = g.getSafeHitFootprint(l),
								q = g.getSafeHitFootprint(c);
							p && q ? (d = b ? e.computeEventStartResizeMutation(p, q, a.footprint) : e.computeEventEndResizeMutation(p, q, a.footprint), d ? (n = k.buildMutatedEventInstanceGroup(m.id, d), o = g.isEventInstanceGroupAllowed(n)) : o = !1) : o = !1, o ? d.isEmpty() && (d = null) : (d = null, f.disableCursor()), d && (h.hideEventsWithId(a.footprint.eventDef.id), h.renderEventResize(g.eventRangesToEventFootprints(n.sliceRenderRanges(g.dateProfile.renderUnzonedRange, j)), a))
						},
						hitOut: function () {
							d = null
						},
						hitDone: function () {
							h.unrenderEventResize(a), h.showEventsWithId(a.footprint.eventDef.id), f.enableCursor()
						},
						interactionEnd: function (b) {
							c && e.segResizeStop(a, b), d && h.reportEventResize(n, d, l, b), e.dragListener = null
						}
					})
				}, b.prototype.segResizeStart = function (a, b) {
					this.isResizing = !0, this.component.publiclyTrigger("eventResizeStart", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, {}, this.view]
					})
				}, b.prototype.segResizeStop = function (a, b) {
					this.isResizing = !1, this.component.publiclyTrigger("eventResizeStop", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, {}, this.view]
					})
				}, b.prototype.computeEventStartResizeMutation = function (a, b, c) {
					var d, e, f = c.componentFootprint.unzonedRange,
						i = this.component.diffDates(b.unzonedRange.getStart(), a.unzonedRange.getStart());
					return f.getStart().add(i) < f.getEnd() && (d = new h.default, d.setStartDelta(i), e = new g.default, e.setDateMutation(d), e)
				}, b.prototype.computeEventEndResizeMutation = function (a, b, c) {
					var d, e, f = c.componentFootprint.unzonedRange,
						i = this.component.diffDates(b.unzonedRange.getEnd(), a.unzonedRange.getEnd());
					return f.getEnd().add(i) > f.getStart() && (d = new h.default, d.setEndDelta(i), e = new g.default, e.setDateMutation(d), e)
				}, b
			}(j.default);
		b.default = k
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = c(26),
			g = c(35),
			h = c(39),
			i = c(18),
			j = c(81),
			k = c(13),
			l = function (a) {
				function b(b, c) {
					var d = a.call(this, b) || this;
					return d.isDragging = !1, d.eventPointing = c, d
				}
				return d.__extends(b, a), b.prototype.end = function () {
					this.dragListener && this.dragListener.endInteraction()
				}, b.prototype.getSelectionDelay = function () {
					var a = this.opt("eventLongPressDelay");
					return null == a && (a = this.opt("longPressDelay")), a
				}, b.prototype.bindToEl = function (a) {
					var b = this.component;
					b.bindSegHandlerToEl(a, "mousedown", this.handleMousedown.bind(this)), b.bindSegHandlerToEl(a, "touchstart", this.handleTouchStart.bind(this))
				}, b.prototype.handleMousedown = function (a, b) {
					this.component.canStartDrag(a, b) && this.buildDragListener(a).startInteraction(b, {
						distance: 5
					})
				}, b.prototype.handleTouchStart = function (a, b) {
					var c = this.component,
						d = {
							delay: this.view.isEventDefSelected(a.footprint.eventDef) ? 0 : this.getSelectionDelay()
						};
					c.canStartDrag(a, b) ? this.buildDragListener(a).startInteraction(b, d) : c.canStartSelection(a, b) && this.buildSelectListener(a).startInteraction(b, d)
				}, b.prototype.buildSelectListener = function (a) {
					var b = this,
						c = this.view,
						d = a.footprint.eventDef,
						e = a.footprint.eventInstance;
					if (this.dragListener) return this.dragListener;
					var f = this.dragListener = new h.default({
						dragStart: function (a) {
							f.isTouch && !c.isEventDefSelected(d) && e && c.selectEventInstance(e)
						},
						interactionEnd: function (a) {
							b.dragListener = null
						}
					});
					return f
				}, b.prototype.buildDragListener = function (a) {
					var b, c, d, f = this,
						g = this.component,
						h = this.view,
						k = h.calendar,
						l = k.eventManager,
						m = a.el,
						n = a.footprint.eventDef,
						o = a.footprint.eventInstance;
					if (this.dragListener) return this.dragListener;
					var p = this.dragListener = new i.default(h, {
						scroll: this.opt("dragScroll"),
						subjectEl: m,
						subjectCenter: !0,
						interactionStart: function (d) {
							a.component = g, b = !1, c = new j.default(a.el, {
								additionalClass: "fc-dragging",
								parentEl: h.el,
								opacity: p.isTouch ? null : f.opt("dragOpacity"),
								revertDuration: f.opt("dragRevertDuration"),
								zIndex: 2
							}), c.hide(), c.start(d)
						},
						dragStart: function (c) {
							p.isTouch && !h.isEventDefSelected(n) && o && h.selectEventInstance(o), b = !0, f.eventPointing.handleMouseout(a, c), f.segDragStart(a, c), h.hideEventsWithId(a.footprint.eventDef.id)
						},
						hitOver: function (b, i, j) {
							var m, o, q, r = !0;
							a.hit && (j = a.hit), m = j.component.getSafeHitFootprint(j), o = b.component.getSafeHitFootprint(b), m && o ? (d = f.computeEventDropMutation(m, o, n), d ? (q = l.buildMutatedEventInstanceGroup(n.id, d), r = g.isEventInstanceGroupAllowed(q)) : r = !1) : r = !1, r || (d = null, e.disableCursor()), d && h.renderDrag(g.eventRangesToEventFootprints(q.sliceRenderRanges(g.dateProfile.renderUnzonedRange, k)), a, p.isTouch) ? c.hide() : c.show(), i && (d = null)
						},
						hitOut: function () {
							h.unrenderDrag(a), c.show(), d = null
						},
						hitDone: function () {
							e.enableCursor()
						},
						interactionEnd: function (e) {
							delete a.component, c.stop(!d, function () {
								b && (h.unrenderDrag(a), f.segDragStop(a, e)), h.showEventsWithId(a.footprint.eventDef.id), d && h.reportEventDrop(o, d, m, e)
							}), f.dragListener = null
						}
					});
					return p
				}, b.prototype.segDragStart = function (a, b) {
					this.isDragging = !0, this.component.publiclyTrigger("eventDragStart", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, {}, this.view]
					})
				}, b.prototype.segDragStop = function (a, b) {
					this.isDragging = !1, this.component.publiclyTrigger("eventDragStop", {
						context: a.el[0],
						args: [a.footprint.getEventLegacy(), b, {}, this.view]
					})
				}, b.prototype.computeEventDropMutation = function (a, b, c) {
					var d = new f.default;
					return d.setDateMutation(this.computeEventDateMutation(a, b)), d
				}, b.prototype.computeEventDateMutation = function (a, b) {
					var c, d, e = a.unzonedRange.getStart(),
						f = b.unzonedRange.getStart(),
						h = !1,
						i = !1,
						j = !1;
					return a.isAllDay !== b.isAllDay && (h = !0, b.isAllDay ? (j = !0, e.stripTime()) : i = !0), c = this.component.diffDates(f, e), d = new g.default, d.clearEnd = h, d.forceTimed = i, d.forceAllDay = j, d.setDateDelta(c), d
				}, b
			}(k.default);
		b.default = l
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = c(18),
			g = c(10),
			h = c(4),
			i = c(13),
			j = function (a) {
				function b(b) {
					var c = a.call(this, b) || this;
					return c.dragListener = c.buildDragListener(), c
				}
				return d.__extends(b, a), b.prototype.end = function () {
					this.dragListener.endInteraction()
				}, b.prototype.getDelay = function () {
					var a = this.opt("selectLongPressDelay");
					return null == a && (a = this.opt("longPressDelay")), a
				}, b.prototype.bindToEl = function (a) {
					var b = this,
						c = this.component,
						d = this.dragListener;
					c.bindDateHandlerToEl(a, "mousedown", function (a) {
						b.opt("selectable") && !c.shouldIgnoreMouse() && d.startInteraction(a, {
							distance: b.opt("selectMinDistance")
						})
					}), c.bindDateHandlerToEl(a, "touchstart", function (a) {
						b.opt("selectable") && !c.shouldIgnoreTouch() && d.startInteraction(a, {
							delay: b.getDelay()
						})
					}), e.preventSelection(a)
				}, b.prototype.buildDragListener = function () {
					var a, b = this,
						c = this.component;
					return new f.default(c, {
						scroll: this.opt("dragScroll"),
						interactionStart: function () {
							a = null
						},
						dragStart: function (a) {
							b.view.unselect(a)
						},
						hitOver: function (d, f, g) {
							var h, i;
							g && (h = c.getSafeHitFootprint(g), i = c.getSafeHitFootprint(d), a = h && i ? b.computeSelection(h, i) : null, a ? c.renderSelectionFootprint(a) : !1 === a && e.disableCursor())
						},
						hitOut: function () {
							a = null, c.unrenderSelection()
						},
						hitDone: function () {
							e.enableCursor()
						},
						interactionEnd: function (c, d) {
							!d && a && b.view.reportSelection(a, c)
						}
					})
				}, b.prototype.computeSelection = function (a, b) {
					var c = this.computeSelectionFootprint(a, b);
					return !(c && !this.isSelectionFootprintAllowed(c)) && c
				}, b.prototype.computeSelectionFootprint = function (a, b) {
					var c = [a.unzonedRange.startMs, a.unzonedRange.endMs, b.unzonedRange.startMs, b.unzonedRange.endMs];
					return c.sort(e.compareNumbers), new g.default(new h.default(c[0], c[3]), a.isAllDay)
				}, b.prototype.isSelectionFootprintAllowed = function (a) {
					return this.component.dateProfile.validUnzonedRange.containsRange(a.unzonedRange) && this.view.calendar.constraints.isSelectionFootprintAllowed(a)
				}, b
			}(i.default);
		b.default = j
	}, function (a, b, c) {
		function d(a) {
			return function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return g.__extends(b, a), b.prototype.renderHeadIntroHtml = function () {
					var a, b = this.view,
						c = b.calendar,
						d = c.msToUtcMoment(this.dateProfile.renderUnzonedRange.startMs, !0);
					return this.opt("weekNumbers") ? (a = d.format(this.opt("smallWeekFormat")), '<th class="fc-axis fc-week-number ' + c.theme.getClass("widgetHeader") + '" ' + b.axisStyleAttr() + ">" + b.buildGotoAnchorHtml({
						date: d,
						type: "week",
						forceOff: this.colCnt > 1
					}, j.htmlEscape(a)) + "</th>") : '<th class="fc-axis ' + c.theme.getClass("widgetHeader") + '" ' + b.axisStyleAttr() + "></th>"
				}, b.prototype.renderBgIntroHtml = function () {
					var a = this.view;
					return '<td class="fc-axis ' + a.calendar.theme.getClass("widgetContent") + '" ' + a.axisStyleAttr() + "></td>"
				}, b.prototype.renderIntroHtml = function () {
					return '<td class="fc-axis" ' + this.view.axisStyleAttr() + "></td>"
				}, b
			}(a)
		}

		function e(a) {
			return function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return g.__extends(b, a), b.prototype.renderBgIntroHtml = function () {
					var a = this.view;
					return '<td class="fc-axis ' + a.calendar.theme.getClass("widgetContent") + '" ' + a.axisStyleAttr() + "><span>" + a.getAllDayHtml() + "</span></td>"
				}, b.prototype.renderIntroHtml = function () {
					return '<td class="fc-axis" ' + this.view.axisStyleAttr() + "></td>"
				}, b
			}(a)
		}

		function f(a) {
			var b, c = [],
				d = [];
			for (b = 0; b < a.length; b++) a[b].componentFootprint.isAllDay ? c.push(a[b]) : d.push(a[b]);
			return {
				allDay: c,
				timed: d
			}
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var g = c(0),
			h = c(3),
			i = c(1),
			j = c(2),
			k = c(28),
			l = c(30),
			m = c(68),
			n = c(46),
			o = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.usesMinMaxTime = !0, d.timeGrid = d.instantiateTimeGrid(), d.addChild(d.timeGrid), d.opt("allDaySlot") && (d.dayGrid = d.instantiateDayGrid(), d.addChild(d.dayGrid)), d.scroller = new k.default({
						overflowX: "hidden",
						overflowY: "auto"
					}), d
				}
				return g.__extends(b, a), b.prototype.instantiateTimeGrid = function () {
					return new(d(this.timeGridClass))(this)
				}, b.prototype.instantiateDayGrid = function () {
					return new(e(this.dayGridClass))(this)
				}, b.prototype.renderSkeleton = function () {
					var a, b;
					this.el.addClass("fc-agenda-view").html(this.renderSkeletonHtml()), this.scroller.render(), a = this.scroller.el.addClass("fc-time-grid-container"), b = i('<div class="fc-time-grid" />').appendTo(a), this.el.find(".fc-body > tr > td").append(a), this.timeGrid.headContainerEl = this.el.find(".fc-head-container"), this.timeGrid.setElement(b), this.dayGrid && (this.dayGrid.setElement(this.el.find(".fc-day-grid")), this.dayGrid.bottomCoordPadding = this.dayGrid.el.next("hr").outerHeight())
				}, b.prototype.unrenderSkeleton = function () {
					this.timeGrid.removeElement(), this.dayGrid && this.dayGrid.removeElement(), this.scroller.destroy()
				}, b.prototype.renderSkeletonHtml = function () {
					var a = this.calendar.theme;
					return '<table class="' + a.getClass("tableGrid") + '">' + (this.opt("columnHeader") ? '<thead class="fc-head"><tr><td class="fc-head-container ' + a.getClass("widgetHeader") + '">&nbsp;</td></tr></thead>' : "") + '<tbody class="fc-body"><tr><td class="' + a.getClass("widgetContent") + '">' + (this.dayGrid ? '<div class="fc-day-grid"/><hr class="fc-divider ' + a.getClass("widgetHeader") + '"/>' : "") + "</td></tr></tbody></table>"
				}, b.prototype.axisStyleAttr = function () {
					return null != this.axisWidth ? 'style="width:' + this.axisWidth + 'px"' : ""
				}, b.prototype.getNowIndicatorUnit = function () {
					return this.timeGrid.getNowIndicatorUnit()
				}, b.prototype.updateSize = function (b, c, d) {
					var e, f, g;
					if (a.prototype.updateSize.call(this, b, c, d), this.axisWidth = j.matchCellWidths(this.el.find(".fc-axis")), !this.timeGrid.colEls) return void(c || (f = this.computeScrollerHeight(b), this.scroller.setHeight(f)));
					var h = this.el.find(".fc-row:not(.fc-scroller *)");
					this.timeGrid.bottomRuleEl.hide(), this.scroller.clear(), j.uncompensateScroll(h), this.dayGrid && (this.dayGrid.removeSegPopover(), e = this.opt("eventLimit"), e && "number" != typeof e && (e = 5), e && this.dayGrid.limitRows(e)), c || (f = this.computeScrollerHeight(b), this.scroller.setHeight(f), g = this.scroller.getScrollbarWidths(), (g.left || g.right) && (j.compensateScroll(h, g), f = this.computeScrollerHeight(b), this.scroller.setHeight(f)), this.scroller.lockOverflow(g), this.timeGrid.getTotalSlatHeight() < f && this.timeGrid.bottomRuleEl.show())
				}, b.prototype.computeScrollerHeight = function (a) {
					return a - j.subtractInnerElHeight(this.el, this.scroller.el)
				}, b.prototype.computeInitialDateScroll = function () {
					var a = h.duration(this.opt("scrollTime")),
						b = this.timeGrid.computeTimeTop(a);
					return b = Math.ceil(b), b && b++, {
						top: b
					}
				}, b.prototype.queryDateScroll = function () {
					return {
						top: this.scroller.getScrollTop()
					}
				}, b.prototype.applyDateScroll = function (a) {
					void 0 !== a.top && this.scroller.setScrollTop(a.top)
				}, b.prototype.getHitFootprint = function (a) {
					return a.component.getHitFootprint(a)
				}, b.prototype.getHitEl = function (a) {
					return a.component.getHitEl(a)
				}, b.prototype.executeEventRender = function (a) {
					var b, c, d = {},
						e = {};
					for (b in a) c = a[b], c.getEventDef().isAllDay() ? d[b] = c : e[b] = c;
					this.timeGrid.executeEventRender(e), this.dayGrid && this.dayGrid.executeEventRender(d)
				}, b.prototype.renderDrag = function (a, b, c) {
					var d = f(a),
						e = !1;
					return e = this.timeGrid.renderDrag(d.timed, b, c), this.dayGrid && (e = this.dayGrid.renderDrag(d.allDay, b, c) || e), e
				}, b.prototype.renderEventResize = function (a, b, c) {
					var d = f(a);
					this.timeGrid.renderEventResize(d.timed, b, c), this.dayGrid && this.dayGrid.renderEventResize(d.allDay, b, c)
				}, b.prototype.renderSelectionFootprint = function (a) {
					a.isAllDay ? this.dayGrid && this.dayGrid.renderSelectionFootprint(a) : this.timeGrid.renderSelectionFootprint(a)
				}, b
			}(l.default);
		b.default = o, o.prototype.timeGridClass = m.default, o.prototype.dayGridClass = n.default
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(3),
			g = c(2),
			h = c(29),
			i = c(41),
			j = c(45),
			k = c(40),
			l = c(38),
			m = c(4),
			n = c(10),
			o = c(83),
			p = c(84),
			q = c(85),
			r = [{
				hours: 1
			}, {
				minutes: 30
			}, {
				minutes: 15
			}, {
				seconds: 30
			}, {
				seconds: 15
			}],
			s = function (a) {
				function b(b) {
					var c = a.call(this, b) || this;
					return c.processOptions(), c
				}
				return d.__extends(b, a), b.prototype.componentFootprintToSegs = function (a) {
					var b, c = this.sliceRangeByTimes(a.unzonedRange);
					for (b = 0; b < c.length; b++) this.isRTL ? c[b].col = this.daysPerRow - 1 - c[b].dayIndex : c[b].col = c[b].dayIndex;
					return c
				}, b.prototype.sliceRangeByTimes = function (a) {
					var b, c, d = [];
					for (c = 0; c < this.daysPerRow; c++)(b = a.intersect(this.dayRanges[c])) && d.push({
						startMs: b.startMs,
						endMs: b.endMs,
						isStart: b.isStart,
						isEnd: b.isEnd,
						dayIndex: c
					});
					return d
				}, b.prototype.processOptions = function () {
					var a, b = this.opt("slotDuration"),
						c = this.opt("snapDuration");
					b = f.duration(b), c = c ? f.duration(c) : b, this.slotDuration = b, this.snapDuration = c, this.snapsPerSlot = b / c, a = this.opt("slotLabelFormat"), e.isArray(a) && (a = a[a.length - 1]), this.labelFormat = a || this.opt("smallTimeFormat"), a = this.opt("slotLabelInterval"), this.labelInterval = a ? f.duration(a) : this.computeLabelInterval(b)
				}, b.prototype.computeLabelInterval = function (a) {
					var b, c, d;
					for (b = r.length - 1; b >= 0; b--)
						if (c = f.duration(r[b]), d = g.divideDurationByDuration(c, a), g.isInt(d) && d > 1) return c;
					return f.duration(a)
				}, b.prototype.renderDates = function (a) {
					this.dateProfile = a, this.updateDayTable(), this.renderSlats(), this.renderColumns()
				}, b.prototype.unrenderDates = function () {
					this.unrenderColumns()
				}, b.prototype.renderSkeleton = function () {
					var a = this.view.calendar.theme;
					this.el.html('<div class="fc-bg"></div><div class="fc-slats"></div><hr class="fc-divider ' + a.getClass("widgetHeader") + '" style="display:none" />'), this.bottomRuleEl = this.el.find("hr")
				}, b.prototype.renderSlats = function () {
					var a = this.view.calendar.theme;
					this.slatContainerEl = this.el.find("> .fc-slats").html('<table class="' + a.getClass("tableGrid") + '">' + this.renderSlatRowHtml() + "</table>"), this.slatEls = this.slatContainerEl.find("tr"), this.slatCoordCache = new l.default({
						els: this.slatEls,
						isVertical: !0
					})
				}, b.prototype.renderSlatRowHtml = function () {
					for (var a, b, c, d = this.view, e = d.calendar, h = e.theme, i = this.isRTL, j = this.dateProfile, k = "", l = f.duration(+j.minTime), m = f.duration(0); l < j.maxTime;) a = e.msToUtcMoment(j.renderUnzonedRange.startMs).time(l), b = g.isInt(g.divideDurationByDuration(m, this.labelInterval)), c = '<td class="fc-axis fc-time ' + h.getClass("widgetContent") + '" ' + d.axisStyleAttr() + ">" + (b ? "<span>" + g.htmlEscape(a.format(this.labelFormat)) + "</span>" : "") + "</td>", k += '<tr data-time="' + a.format("HH:mm:ss") + '"' + (b ? "" : ' class="fc-minor"') + ">" + (i ? "" : c) + '<td class="' + h.getClass("widgetContent") + '"/>' + (i ? c : "") + "</tr>", l.add(this.slotDuration), m.add(this.slotDuration);
					return k
				}, b.prototype.renderColumns = function () {
					var a = this.dateProfile,
						b = this.view.calendar.theme;
					this.dayRanges = this.dayDates.map(function (b) {
						return new m.default(b.clone().add(a.minTime), b.clone().add(a.maxTime))
					}), this.headContainerEl && this.headContainerEl.html(this.renderHeadHtml()), this.el.find("> .fc-bg").html('<table class="' + b.getClass("tableGrid") + '">' + this.renderBgTrHtml(0) + "</table>"), this.colEls = this.el.find(".fc-day, .fc-disabled-day"), this.colCoordCache = new l.default({
						els: this.colEls,
						isHorizontal: !0
					}), this.renderContentSkeleton()
				}, b.prototype.unrenderColumns = function () {
					this.unrenderContentSkeleton()
				}, b.prototype.renderContentSkeleton = function () {
					var a, b, c = "";
					for (a = 0; a < this.colCnt; a++) c += '<td><div class="fc-content-col"><div class="fc-event-container fc-helper-container"></div><div class="fc-event-container"></div><div class="fc-highlight-container"></div><div class="fc-bgevent-container"></div><div class="fc-business-container"></div></div></td>';
					b = this.contentSkeletonEl = e('<div class="fc-content-skeleton"><table><tr>' + c + "</tr></table></div>"), this.colContainerEls = b.find(".fc-content-col"), this.helperContainerEls = b.find(".fc-helper-container"), this.fgContainerEls = b.find(".fc-event-container:not(.fc-helper-container)"), this.bgContainerEls = b.find(".fc-bgevent-container"), this.highlightContainerEls = b.find(".fc-highlight-container"), this.businessContainerEls = b.find(".fc-business-container"), this.bookendCells(b.find("tr")), this.el.append(b)
				}, b.prototype.unrenderContentSkeleton = function () {
					this.contentSkeletonEl.remove(), this.contentSkeletonEl = null, this.colContainerEls = null, this.helperContainerEls = null, this.fgContainerEls = null, this.bgContainerEls = null, this.highlightContainerEls = null, this.businessContainerEls = null
				}, b.prototype.groupSegsByCol = function (a) {
					var b, c = [];
					for (b = 0; b < this.colCnt; b++) c.push([]);
					for (b = 0; b < a.length; b++) c[a[b].col].push(a[b]);
					return c
				}, b.prototype.attachSegsByCol = function (a, b) {
					var c, d, e;
					for (c = 0; c < this.colCnt; c++)
						for (d = a[c], e = 0; e < d.length; e++) b.eq(c).append(d[e].el)
				}, b.prototype.getNowIndicatorUnit = function () {
					return "minute"
				}, b.prototype.renderNowIndicator = function (a) {
					if (this.colContainerEls) {
						var b, c = this.componentFootprintToSegs(new n.default(new m.default(a, a.valueOf() + 1), !1)),
							d = this.computeDateTop(a, a),
							f = [];
						for (b = 0; b < c.length; b++) f.push(e('<div class="fc-now-indicator fc-now-indicator-line"></div>').css("top", d).appendTo(this.colContainerEls.eq(c[b].col))[0]);
						c.length > 0 && f.push(e('<div class="fc-now-indicator fc-now-indicator-arrow"></div>').css("top", d).appendTo(this.el.find(".fc-content-skeleton"))[0]), this.nowIndicatorEls = e(f)
					}
				}, b.prototype.unrenderNowIndicator = function () {
					this.nowIndicatorEls && (this.nowIndicatorEls.remove(), this.nowIndicatorEls = null)
				}, b.prototype.updateSize = function (b, c, d) {
					a.prototype.updateSize.call(this, b, c, d), this.slatCoordCache.build(), d && this.updateSegVerticals([].concat(this.eventRenderer.getSegs(), this.businessSegs || []))
				}, b.prototype.getTotalSlatHeight = function () {
					return this.slatContainerEl.outerHeight()
				}, b.prototype.computeDateTop = function (a, b) {
					return this.computeTimeTop(f.duration(a - b.clone().stripTime()))
				}, b.prototype.computeTimeTop = function (a) {
					var b, c, d = this.slatEls.length,
						e = this.dateProfile,
						f = (a - e.minTime) / this.slotDuration;
					return f = Math.max(0, f), f = Math.min(d, f), b = Math.floor(f), b = Math.min(b, d - 1), c = f - b, this.slatCoordCache.getTopPosition(b) + this.slatCoordCache.getHeight(b) * c
				}, b.prototype.updateSegVerticals = function (a) {
					this.computeSegVerticals(a), this.assignSegVerticals(a)
				}, b.prototype.computeSegVerticals = function (a) {
					var b, c, d, e = this.opt("agendaEventMinHeight");
					for (b = 0; b < a.length; b++) c = a[b], d = this.dayDates[c.dayIndex], c.top = this.computeDateTop(c.startMs, d), c.bottom = Math.max(c.top + e, this.computeDateTop(c.endMs, d))
				}, b.prototype.assignSegVerticals = function (a) {
					var b, c;
					for (b = 0; b < a.length; b++) c = a[b], c.el.css(this.generateSegVerticalCss(c))
				}, b.prototype.generateSegVerticalCss = function (a) {
					return {
						top: a.top,
						bottom: -a.bottom
					}
				}, b.prototype.prepareHits = function () {
					this.colCoordCache.build(), this.slatCoordCache.build()
				}, b.prototype.releaseHits = function () {
					this.colCoordCache.clear()
				}, b.prototype.queryHit = function (a, b) {
					var c = this.snapsPerSlot,
						d = this.colCoordCache,
						e = this.slatCoordCache;
					if (d.isLeftInBounds(a) && e.isTopInBounds(b)) {
						var f = d.getHorizontalIndex(a),
							g = e.getVerticalIndex(b);
						if (null != f && null != g) {
							var h = e.getTopOffset(g),
								i = e.getHeight(g),
								j = (b - h) / i,
								k = Math.floor(j * c),
								l = g * c + k,
								m = h + k / c * i,
								n = h + (k + 1) / c * i;
							return {
								col: f,
								snap: l,
								component: this,
								left: d.getLeftOffset(f),
								right: d.getRightOffset(f),
								top: m,
								bottom: n
							}
						}
					}
				}, b.prototype.getHitFootprint = function (a) {
					var b, c = this.getCellDate(0, a.col),
						d = this.computeSnapTime(a.snap);
					return c.time(d), b = c.clone().add(this.snapDuration), new n.default(new m.default(c, b), !1)
				}, b.prototype.computeSnapTime = function (a) {
					return f.duration(this.dateProfile.minTime + this.snapDuration * a)
				}, b.prototype.getHitEl = function (a) {
					return this.colEls.eq(a.col)
				}, b.prototype.renderDrag = function (a, b, c) {
					var d;
					if (b) {
						if (a.length) return this.helperRenderer.renderEventDraggingFootprints(a, b, c), !0
					} else
						for (d = 0; d < a.length; d++) this.renderHighlight(a[d].componentFootprint)
				}, b.prototype.unrenderDrag = function () {
					this.unrenderHighlight(), this.helperRenderer.unrender()
				}, b.prototype.renderEventResize = function (a, b, c) {
					this.helperRenderer.renderEventResizingFootprints(a, b, c)
				}, b.prototype.unrenderEventResize = function () {
					this.helperRenderer.unrender()
				}, b.prototype.renderSelectionFootprint = function (a) {
					this.opt("selectHelper") ? this.helperRenderer.renderComponentFootprint(a) : this.renderHighlight(a)
				}, b.prototype.unrenderSelection = function () {
					this.helperRenderer.unrender(), this.unrenderHighlight()
				}, b
			}(h.default);
		b.default = s, s.prototype.eventRendererClass = o.default, s.prototype.businessHourRendererClass = i.default, s.prototype.helperRendererClass = p.default, s.prototype.fillRendererClass = q.default, j.default.mixInto(s), k.default.mixInto(s)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(4),
			f = c(62),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.buildRenderRange = function (b, c, d) {
					var f = a.prototype.buildRenderRange.call(this, b, c, d),
						g = this.msToUtcMoment(f.startMs, d),
						h = this.msToUtcMoment(f.endMs, d);
					return /^(year|month)$/.test(c) && (g.startOf("week"), h.weekday() && h.add(1, "week").startOf("week")), new e.default(g, h)
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(3),
			f = c(2),
			g = c(4),
			h = c(47),
			i = c(69),
			j = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.buildRenderRange = function (b, c, d) {
					var e, f = a.prototype.buildRenderRange.call(this, b, c, d),
						h = this.msToUtcMoment(f.startMs, d),
						i = this.msToUtcMoment(f.endMs, d);
					return this.opt("fixedWeekCount") && (e = Math.ceil(i.diff(h, "weeks", !0)), i.add(6 - e, "weeks")), new g.default(h, i)
				}, b
			}(i.default),
			k = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.setGridHeight = function (a, b) {
					b && (a *= this.dayGrid.rowCnt / 6), f.distributeHeight(this.dayGrid.rowEls, a, !b)
				}, b.prototype.isDateInOtherMonth = function (a, b) {
					return a.month() !== e.utc(b.currentUnzonedRange.startMs).month()
				}, b
			}(h.default);
		b.default = k, k.prototype.dateProfileGeneratorClass = j
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(4),
			h = c(30),
			i = c(28),
			j = c(90),
			k = c(91),
			l = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.segSelector = ".fc-list-item", d.scroller = new i.default({
						overflowX: "hidden",
						overflowY: "auto"
					}), d
				}
				return d.__extends(b, a), b.prototype.renderSkeleton = function () {
					this.el.addClass("fc-list-view " + this.calendar.theme.getClass("listView")), this.scroller.render(), this.scroller.el.appendTo(this.el), this.contentEl = this.scroller.scrollEl
				}, b.prototype.unrenderSkeleton = function () {
					this.scroller.destroy()
				}, b.prototype.updateSize = function (a, b, c) {
					this.scroller.setHeight(this.computeScrollerHeight(a))
				}, b.prototype.computeScrollerHeight = function (a) {
					return a - f.subtractInnerElHeight(this.el, this.scroller.el)
				}, b.prototype.renderDates = function (a) {
					for (var b = this.calendar, c = b.msToUtcMoment(a.renderUnzonedRange.startMs, !0), d = b.msToUtcMoment(a.renderUnzonedRange.endMs, !0), e = [], f = []; c < d;) e.push(c.clone()), f.push(new g.default(c, c.clone().add(1, "day"))), c.add(1, "day");
					this.dayDates = e, this.dayRanges = f
				}, b.prototype.componentFootprintToSegs = function (a) {
					var b, c, d, e = this.dayRanges,
						f = [];
					for (b = 0; b < e.length; b++)
						if ((c = a.unzonedRange.intersect(e[b])) && (d = {
								startMs: c.startMs,
								endMs: c.endMs,
								isStart: c.isStart,
								isEnd: c.isEnd,
								dayIndex: b
							}, f.push(d), !d.isEnd && !a.isAllDay && b + 1 < e.length && a.unzonedRange.endMs < e[b + 1].startMs + this.nextDayThreshold)) {
							d.endMs = a.unzonedRange.endMs, d.isEnd = !0;
							break
						}
					return f
				}, b.prototype.renderEmptyMessage = function () {
					this.contentEl.html('<div class="fc-list-empty-wrap2"><div class="fc-list-empty-wrap1"><div class="fc-list-empty">' + f.htmlEscape(this.opt("noEventsMessage")) + "</div></div></div>")
				}, b.prototype.renderSegList = function (a) {
					var b, c, d, f = this.groupSegsByDay(a),
						g = e('<table class="fc-list-table ' + this.calendar.theme.getClass("tableList") + '"><tbody/></table>'),
						h = g.find("tbody");
					for (b = 0; b < f.length; b++)
						if (c = f[b])
							for (h.append(this.dayHeaderHtml(this.dayDates[b])), this.eventRenderer.sortEventSegs(c), d = 0; d < c.length; d++) h.append(c[d].el);
					this.contentEl.empty().append(g)
				}, b.prototype.groupSegsByDay = function (a) {
					var b, c, d = [];
					for (b = 0; b < a.length; b++) c = a[b], (d[c.dayIndex] || (d[c.dayIndex] = [])).push(c);
					return d
				}, b.prototype.dayHeaderHtml = function (a) {
					var b = this.opt("listDayFormat"),
						c = this.opt("listDayAltFormat");
					return '<tr class="fc-list-heading" data-date="' + a.format("YYYY-MM-DD") + '"><td class="' + this.calendar.theme.getClass("widgetHeader") + '" colspan="3">' + (b ? this.buildGotoAnchorHtml(a, {
						class: "fc-list-heading-main"
					}, f.htmlEscape(a.format(b))) : "") + (c ? this.buildGotoAnchorHtml(a, {
						class: "fc-list-heading-alt"
					}, f.htmlEscape(a.format(c))) : "") + "</td></tr>"
				}, b
			}(h.default);
		b.default = l, l.prototype.eventRendererClass = j.default, l.prototype.eventPointingClass = k.default
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(7),
			f = c(73),
			g = c(2),
			h = c(48);
		c(9), c(37), c(92), c(93), c(95), c(96), c(97), d.fullCalendar = d.extend(e.default, f), d.fn.fullCalendar = function (a) {
			var b = Array.prototype.slice.call(arguments, 1),
				c = this;
			return this.each(function (e, f) {
				var i, j = d(f),
					k = j.data("fullCalendar");
				"string" == typeof a ? "getCalendar" === a ? e || (c = k) : "destroy" === a ? k && (k.destroy(), j.removeData("fullCalendar")) : k ? d.isFunction(k[a]) ? (i = k[a].apply(k, b), e || (c = i), "destroy" === a && j.removeData("fullCalendar")) : g.warn("'" + a + "' is an unknown FullCalendar method.") : g.warn("Attempting to call a FullCalendar method on an element with no calendar.") : k || (k = new h.default(j, a), j.data("fullCalendar", k), k.render())
			}), c
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(48);
		b.Calendar = d.default;
		var e = c(19);
		d.default.defaults = e.globalDefaults, d.default.englishDefaults = e.englishDefaults, d.default.rtlDefaults = e.rtlDefaults;
		var f = c(2);
		b.applyAll = f.applyAll, b.debounce = f.debounce, b.isInt = f.isInt, b.htmlEscape = f.htmlEscape, b.cssToStr = f.cssToStr, b.proxy = f.proxy, b.capitaliseFirstLetter = f.capitaliseFirstLetter, b.getOuterRect = f.getOuterRect, b.getClientRect = f.getClientRect, b.getContentRect = f.getContentRect, b.getScrollbarWidths = f.getScrollbarWidths, b.preventDefault = f.preventDefault, b.parseFieldSpecs = f.parseFieldSpecs, b.compareByFieldSpecs = f.compareByFieldSpecs, b.compareByFieldSpec = f.compareByFieldSpec, b.flexibleCompare = f.flexibleCompare, b.computeGreatestUnit = f.computeGreatestUnit, b.divideRangeByDuration = f.divideRangeByDuration, b.divideDurationByDuration = f.divideDurationByDuration, b.multiplyDuration = f.multiplyDuration, b.durationHasTime = f.durationHasTime, b.log = f.log, b.warn = f.warn, b.removeExact = f.removeExact, b.intersectRects = f.intersectRects;
		var g = c(37);
		b.formatDate = g.formatDate, b.formatRange = g.formatRange, b.queryMostGranularFormatUnit = g.queryMostGranularFormatUnit;
		var h = c(20);
		b.datepickerLocale = h.datepickerLocale, b.locale = h.locale;
		var i = c(9);
		b.moment = i.default;
		var j = c(8);
		b.EmitterMixin = j.default;
		var k = c(6);
		b.ListenerMixin = k.default;
		var l = c(32);
		b.Model = l.default;
		var m = c(49);
		b.Constraints = m.default;
		var n = c(4);
		b.UnzonedRange = n.default;
		var o = c(10);
		b.ComponentFootprint = o.default;
		var p = c(54);
		b.BusinessHourGenerator = p.default;
		var q = c(22);
		b.EventDef = q.default;
		var r = c(26);
		b.EventDefMutation = r.default;
		var s = c(25);
		b.EventSourceParser = s.default;
		var t = c(5);
		b.EventSource = t.default;
		var u = c(36);
		b.ThemeRegistry = u.default;
		var v = c(17);
		b.EventInstanceGroup = v.default;
		var w = c(34);
		b.ArrayEventSource = w.default;
		var x = c(57);
		b.FuncEventSource = x.default;
		var y = c(58);
		b.JsonFeedEventSource = y.default;
		var z = c(24);
		b.EventFootprint = z.default;
		var A = c(21);
		b.Class = A.default;
		var B = c(12);
		b.Mixin = B.default;
		var C = c(38);
		b.CoordCache = C.default;
		var D = c(39);
		b.DragListener = D.default;
		var E = c(16);
		b.Promise = E.default;
		var F = c(59);
		b.TaskQueue = F.default;
		var G = c(60);
		b.RenderQueue = G.default;
		var H = c(28);
		b.Scroller = H.default;
		var I = c(27);
		b.Theme = I.default;
		var J = c(61);
		b.DateComponent = J.default;
		var K = c(29);
		b.InteractiveDateComponent = K.default;
		var L = c(30);
		b.View = L.default;
		var M = c(40);
		b.DayTableMixin = M.default;
		var N = c(41);
		b.BusinessHourRenderer = N.default;
		var O = c(31);
		b.EventRenderer = O.default;
		var P = c(42);
		b.FillRenderer = P.default;
		var Q = c(43);
		b.HelperRenderer = Q.default;
		var R = c(63);
		b.ExternalDropping = R.default;
		var S = c(64);
		b.EventResizing = S.default;
		var T = c(44);
		b.EventPointing = T.default;
		var U = c(65);
		b.EventDragging = U.default;
		var V = c(66);
		b.DateSelecting = V.default;
		var W = c(45);
		b.StandardInteractionsMixin = W.default;
		var X = c(67);
		b.AgendaView = X.default;
		var Y = c(68);
		b.TimeGrid = Y.default;
		var Z = c(46);
		b.DayGrid = Z.default;
		var $ = c(47);
		b.BasicView = $.default;
		var _ = c(70);
		b.MonthView = _.default;
		var aa = c(71);
		b.ListView = aa.default
	}, function (a, b) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var c = function () {
			function a(a) {
				this.items = a || []
			}
			return a.prototype.proxyCall = function (a) {
				for (var b = [], c = 1; c < arguments.length; c++) b[c - 1] = arguments[c];
				var d = [];
				return this.items.forEach(function (c) {
					d.push(c[a].apply(c, b))
				}), d
			}, a
		}();
		b.default = c
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = function () {
				function a(a, b) {
					this.el = null, this.viewsWithButtons = [], this.calendar = a, this.toolbarOptions = b
				}
				return a.prototype.setToolbarOptions = function (a) {
					this.toolbarOptions = a
				}, a.prototype.render = function () {
					var a = this.toolbarOptions.layout,
						b = this.el;
					a ? (b ? b.empty() : b = this.el = d("<div class='fc-toolbar " + this.toolbarOptions.extraClasses + "'/>"), b.append(this.renderSection("left")).append(this.renderSection("right")).append(this.renderSection("center")).append('<div class="fc-clear"/>')) : this.removeElement()
				}, a.prototype.removeElement = function () {
					this.el && (this.el.remove(), this.el = null)
				}, a.prototype.renderSection = function (a) {
					var b = this,
						c = this.calendar,
						f = c.theme,
						g = c.optionsManager,
						h = c.viewSpecManager,
						i = d('<div class="fc-' + a + '"/>'),
						j = this.toolbarOptions.layout[a],
						k = g.get("customButtons") || {},
						l = g.overrides.buttonText || {},
						m = g.get("buttonText") || {};
					return j && d.each(j.split(" "), function (a, g) {
						var j, n = d(),
							o = !0;
						d.each(g.split(","), function (a, g) {
							var i, j, p, q, r, s, t, u;
							"title" == g ? (n = n.add(d("<h2>&nbsp;</h2>")), o = !1) : ((i = k[g]) ? (p = function (a) {
								i.click && i.click.call(u[0], a)
							}, (q = f.getCustomButtonIconClass(i)) || (q = f.getIconClass(g)) || (r = i.text)) : (j = h.getViewSpec(g)) ? (b.viewsWithButtons.push(g), p = function () {
								c.changeView(g)
							}, (r = j.buttonTextOverride) || (q = f.getIconClass(g)) || (r = j.buttonTextDefault)) : c[g] && (p = function () {
								c[g]()
							}, (r = l[g]) || (q = f.getIconClass(g)) || (r = m[g])), p && (t = ["fc-" + g + "-button", f.getClass("button"), f.getClass("stateDefault")], r ? s = e.htmlEscape(r) : q && (s = "<span class='" + q + "'></span>"), u = d('<button type="button" class="' + t.join(" ") + '">' + s + "</button>").click(function (a) {
								u.hasClass(f.getClass("stateDisabled")) || (p(a), (u.hasClass(f.getClass("stateActive")) || u.hasClass(f.getClass("stateDisabled"))) && u.removeClass(f.getClass("stateHover")))
							}).mousedown(function () {
								u.not("." + f.getClass("stateActive")).not("." + f.getClass("stateDisabled")).addClass(f.getClass("stateDown"))
							}).mouseup(function () {
								u.removeClass(f.getClass("stateDown"))
							}).hover(function () {
								u.not("." + f.getClass("stateActive")).not("." + f.getClass("stateDisabled")).addClass(f.getClass("stateHover"))
							}, function () {
								u.removeClass(f.getClass("stateHover")).removeClass(f.getClass("stateDown"))
							}), n = n.add(u)))
						}), o && n.first().addClass(f.getClass("cornerLeft")).end().last().addClass(f.getClass("cornerRight")).end(), n.length > 1 ? (j = d("<div/>"), o && j.addClass(f.getClass("buttonGroup")), j.append(n), i.append(j)) : i.append(n)
					}), i
				}, a.prototype.updateTitle = function (a) {
					this.el && this.el.find("h2").text(a)
				}, a.prototype.activateButton = function (a) {
					this.el && this.el.find(".fc-" + a + "-button").addClass(this.calendar.theme.getClass("stateActive"))
				}, a.prototype.deactivateButton = function (a) {
					this.el && this.el.find(".fc-" + a + "-button").removeClass(this.calendar.theme.getClass("stateActive"))
				}, a.prototype.disableButton = function (a) {
					this.el && this.el.find(".fc-" + a + "-button").prop("disabled", !0).addClass(this.calendar.theme.getClass("stateDisabled"))
				}, a.prototype.enableButton = function (a) {
					this.el && this.el.find(".fc-" + a + "-button").prop("disabled", !1).removeClass(this.calendar.theme.getClass("stateDisabled"))
				}, a.prototype.getViewsWithButtons = function () {
					return this.viewsWithButtons
				}, a
			}();
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(2),
			g = c(19),
			h = c(20),
			i = c(32),
			j = function (a) {
				function b(b, c) {
					var d = a.call(this) || this;
					return d._calendar = b, d.overrides = e.extend({}, c), d.dynamicOverrides = {}, d.compute(), d
				}
				return d.__extends(b, a), b.prototype.add = function (a) {
					var b, c = 0;
					this.recordOverrides(a);
					for (b in a) c++;
					if (1 === c) {
						if ("height" === b || "contentHeight" === b || "aspectRatio" === b) return void this._calendar.updateViewSize(!0);
						if ("defaultDate" === b) return;
						if ("businessHours" === b) return;
						if ("timezone" === b) return void this._calendar.view.flash("initialEvents")
					}
					this._calendar.renderHeader(), this._calendar.renderFooter(), this._calendar.viewsByType = {}, this._calendar.reinitView()
				}, b.prototype.compute = function () {
					var a, b, c, d, e;
					a = f.firstDefined(this.dynamicOverrides.locale, this.overrides.locale), b = h.localeOptionHash[a], b || (a = g.globalDefaults.locale, b = h.localeOptionHash[a] || {}), c = f.firstDefined(this.dynamicOverrides.isRTL, this.overrides.isRTL, b.isRTL, g.globalDefaults.isRTL), d = c ? g.rtlDefaults : {}, this.dirDefaults = d, this.localeDefaults = b, e = g.mergeOptions([g.globalDefaults, d, b, this.overrides, this.dynamicOverrides]), h.populateInstanceComputableOptions(e), this.reset(e)
				}, b.prototype.recordOverrides = function (a) {
					var b;
					for (b in a) this.dynamicOverrides[b] = a[b];
					this._calendar.viewSpecManager.clearCache(), this.compute()
				}, b
			}(i.default);
		b.default = j
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(3),
			e = c(1),
			f = c(7),
			g = c(2),
			h = c(19),
			i = c(20),
			j = function () {
				function a(a, b) {
					this.optionsManager = a, this._calendar = b, this.clearCache()
				}
				return a.prototype.clearCache = function () {
					this.viewSpecCache = {}
				}, a.prototype.getViewSpec = function (a) {
					var b = this.viewSpecCache;
					return b[a] || (b[a] = this.buildViewSpec(a))
				}, a.prototype.getUnitViewSpec = function (a) {
					var b, c, d;
					if (-1 != e.inArray(a, g.unitsDesc))
						for (b = this._calendar.header.getViewsWithButtons(), e.each(f.default.views, function (a) {
								b.push(a)
							}), c = 0; c < b.length; c++)
							if ((d = this.getViewSpec(b[c])) && d.singleUnit == a) return d
				}, a.prototype.buildViewSpec = function (a) {
					for (var b, c, e, i, j, k = this.optionsManager.overrides.views || {}, l = [], m = [], n = [], o = a; o;) b = f.default.views[o], c = k[o], o = null, "function" == typeof b && (b = {
						class: b
					}), b && (l.unshift(b), m.unshift(b.defaults || {}), e = e || b.duration, o = o || b.type), c && (n.unshift(c), e = e || c.duration, o = o || c.type);
					return b = g.mergeProps(l), b.type = a, !!b.class && (e = e || this.optionsManager.dynamicOverrides.duration || this.optionsManager.overrides.duration, e && (i = d.duration(e), i.valueOf() && (j = g.computeDurationGreatestUnit(i, e), b.duration = i, b.durationUnit = j, 1 === i.as(j) && (b.singleUnit = j, n.unshift(k[j] || {})))), b.defaults = h.mergeOptions(m), b.overrides = h.mergeOptions(n), this.buildViewSpecOptions(b), this.buildViewSpecButtonText(b, a), b)
				}, a.prototype.buildViewSpecOptions = function (a) {
					var b = this.optionsManager;
					a.options = h.mergeOptions([h.globalDefaults, a.defaults, b.dirDefaults, b.localeDefaults, b.overrides, a.overrides, b.dynamicOverrides]), i.populateInstanceComputableOptions(a.options)
				}, a.prototype.buildViewSpecButtonText = function (a, b) {
					function c(c) {
						var d = c.buttonText || {};
						return d[b] || (a.buttonTextKey ? d[a.buttonTextKey] : null) || (a.singleUnit ? d[a.singleUnit] : null)
					}
					var d = this.optionsManager;
					a.buttonTextOverride = c(d.dynamicOverrides) || c(d.overrides) || a.overrides.buttonText, a.buttonTextDefault = c(d.localeDefaults) || c(d.dirDefaults) || a.defaults.buttonText || c(h.globalDefaults) || (a.duration ? this._calendar.humanizeDuration(a.duration) : null) || b
				}, a
			}();
		b.default = j
	}, function (a, b, c) {
		function d(a, b) {
			return a.getPrimitive() == b.getPrimitive()
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var e = c(1),
			f = c(2),
			g = c(79),
			h = c(34),
			i = c(5),
			j = c(25),
			k = c(11),
			l = c(17),
			m = c(8),
			n = c(6),
			o = function () {
				function a(a) {
					this.calendar = a, this.stickySource = new h.default(a), this.otherSources = []
				}
				return a.prototype.requestEvents = function (a, b, c, d) {
					return !d && this.currentPeriod && this.currentPeriod.isWithinRange(a, b) && c === this.currentPeriod.timezone || this.setPeriod(new g.default(a, b, c)), this.currentPeriod.whenReleased()
				}, a.prototype.addSource = function (a) {
					this.otherSources.push(a), this.currentPeriod && this.currentPeriod.requestSource(a)
				}, a.prototype.removeSource = function (a) {
					f.removeExact(this.otherSources, a), this.currentPeriod && this.currentPeriod.purgeSource(a)
				}, a.prototype.removeAllSources = function () {
					this.otherSources = [], this.currentPeriod && this.currentPeriod.purgeAllSources()
				}, a.prototype.refetchSource = function (a) {
					var b = this.currentPeriod;
					b && (b.freeze(), b.purgeSource(a), b.requestSource(a), b.thaw())
				}, a.prototype.refetchAllSources = function () {
					var a = this.currentPeriod;
					a && (a.freeze(), a.purgeAllSources(), a.requestSources(this.getSources()), a.thaw())
				}, a.prototype.getSources = function () {
					return [this.stickySource].concat(this.otherSources)
				}, a.prototype.multiQuerySources = function (a) {
					a ? e.isArray(a) || (a = [a]) : a = [];
					var b, c = [];
					for (b = 0; b < a.length; b++) c.push.apply(c, this.querySources(a[b]));
					return c
				}, a.prototype.querySources = function (a) {
					var b, c, f = this.otherSources;
					for (b = 0; b < f.length; b++)
						if ((c = f[b]) === a) return [c];
					return (c = this.getSourceById(i.default.normalizeId(a))) ? [c] : (a = j.default.parse(a, this.calendar), a ? e.grep(f, function (b) {
						return d(a, b)
					}) : void 0)
				}, a.prototype.getSourceById = function (a) {
					return e.grep(this.otherSources, function (b) {
						return b.id && b.id === a
					})[0]
				}, a.prototype.setPeriod = function (a) {
					this.currentPeriod && (this.unbindPeriod(this.currentPeriod), this.currentPeriod = null), this.currentPeriod = a, this.bindPeriod(a), a.requestSources(this.getSources())
				}, a.prototype.bindPeriod = function (a) {
					this.listenTo(a, "release", function (a) {
						this.trigger("release", a)
					})
				}, a.prototype.unbindPeriod = function (a) {
					this.stopListeningTo(a)
				}, a.prototype.getEventDefByUid = function (a) {
					if (this.currentPeriod) return this.currentPeriod.getEventDefByUid(a)
				}, a.prototype.addEventDef = function (a, b) {
					b && this.stickySource.addEventDef(a), this.currentPeriod && this.currentPeriod.addEventDef(a)
				}, a.prototype.removeEventDefsById = function (a) {
					this.getSources().forEach(function (b) {
						b.removeEventDefsById(a)
					}), this.currentPeriod && this.currentPeriod.removeEventDefsById(a)
				}, a.prototype.removeAllEventDefs = function () {
					this.getSources().forEach(function (a) {
						a.removeAllEventDefs()
					}), this.currentPeriod && this.currentPeriod.removeAllEventDefs()
				}, a.prototype.mutateEventsWithId = function (a, b) {
					var c, d = this.currentPeriod,
						e = [];
					return d ? (d.freeze(), c = d.getEventDefsById(a), c.forEach(function (a) {
						d.removeEventDef(a), e.push(b.mutateSingle(a)), d.addEventDef(a)
					}), d.thaw(), function () {
						d.freeze();
						for (var a = 0; a < c.length; a++) d.removeEventDef(c[a]), e[a](), d.addEventDef(c[a]);
						d.thaw()
					}) : function () {}
				}, a.prototype.buildMutatedEventInstanceGroup = function (a, b) {
					var c, d, e = this.getEventDefsById(a),
						f = [];
					for (c = 0; c < e.length; c++)(d = e[c].clone()) instanceof k.default && (b.mutateSingle(d), f.push.apply(f, d.buildInstances()));
					return new l.default(f)
				}, a.prototype.freeze = function () {
					this.currentPeriod && this.currentPeriod.freeze()
				}, a.prototype.thaw = function () {
					this.currentPeriod && this.currentPeriod.thaw()
				}, a.prototype.getEventDefsById = function (a) {
					return this.currentPeriod.getEventDefsById(a)
				}, a.prototype.getEventInstances = function () {
					return this.currentPeriod.getEventInstances()
				}, a.prototype.getEventInstancesWithId = function (a) {
					return this.currentPeriod.getEventInstancesWithId(a)
				}, a.prototype.getEventInstancesWithoutId = function (a) {
					return this.currentPeriod.getEventInstancesWithoutId(a)
				}, a
			}();
		b.default = o, m.default.mixInto(o), n.default.mixInto(o)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = c(16),
			g = c(8),
			h = c(4),
			i = c(17),
			j = function () {
				function a(a, b, c) {
					this.pendingCnt = 0, this.freezeDepth = 0, this.stuntedReleaseCnt = 0, this.releaseCnt = 0, this.start = a, this.end = b, this.timezone = c, this.unzonedRange = new h.default(a.clone().stripZone(), b.clone().stripZone()), this.requestsByUid = {}, this.eventDefsByUid = {}, this.eventDefsById = {}, this.eventInstanceGroupsById = {}
				}
				return a.prototype.isWithinRange = function (a, b) {
					return !a.isBefore(this.start) && !b.isAfter(this.end)
				}, a.prototype.requestSources = function (a) {
					this.freeze();
					for (var b = 0; b < a.length; b++) this.requestSource(a[b]);
					this.thaw()
				}, a.prototype.requestSource = function (a) {
					var b = this,
						c = {
							source: a,
							status: "pending",
							eventDefs: null
						};
					this.requestsByUid[a.uid] = c, this.pendingCnt += 1, a.fetch(this.start, this.end, this.timezone).then(function (a) {
						"cancelled" !== c.status && (c.status = "completed", c.eventDefs = a, b.addEventDefs(a), b.pendingCnt--, b.tryRelease())
					}, function () {
						"cancelled" !== c.status && (c.status = "failed", b.pendingCnt--, b.tryRelease())
					})
				}, a.prototype.purgeSource = function (a) {
					var b = this.requestsByUid[a.uid];
					b && (delete this.requestsByUid[a.uid], "pending" === b.status ? (b.status = "cancelled", this.pendingCnt--, this.tryRelease()) : "completed" === b.status && b.eventDefs.forEach(this.removeEventDef.bind(this)))
				}, a.prototype.purgeAllSources = function () {
					var a, b, c = this.requestsByUid,
						d = 0;
					for (a in c) b = c[a], "pending" === b.status ? b.status = "cancelled" : "completed" === b.status && d++;
					this.requestsByUid = {}, this.pendingCnt = 0, d && this.removeAllEventDefs()
				}, a.prototype.getEventDefByUid = function (a) {
					return this.eventDefsByUid[a]
				}, a.prototype.getEventDefsById = function (a) {
					var b = this.eventDefsById[a];
					return b ? b.slice() : []
				}, a.prototype.addEventDefs = function (a) {
					for (var b = 0; b < a.length; b++) this.addEventDef(a[b])
				}, a.prototype.addEventDef = function (a) {
					var b, c = this.eventDefsById,
						d = a.id,
						e = c[d] || (c[d] = []),
						f = a.buildInstances(this.unzonedRange);
					for (e.push(a), this.eventDefsByUid[a.uid] = a, b = 0; b < f.length; b++) this.addEventInstance(f[b], d)
				}, a.prototype.removeEventDefsById = function (a) {
					var b = this;
					this.getEventDefsById(a).forEach(function (a) {
						b.removeEventDef(a)
					})
				}, a.prototype.removeAllEventDefs = function () {
					var a = d.isEmptyObject(this.eventDefsByUid);
					this.eventDefsByUid = {}, this.eventDefsById = {}, this.eventInstanceGroupsById = {}, a || this.tryRelease()
				}, a.prototype.removeEventDef = function (a) {
					var b = this.eventDefsById,
						c = b[a.id];
					delete this.eventDefsByUid[a.uid], c && (e.removeExact(c, a), c.length || delete b[a.id], this.removeEventInstancesForDef(a))
				}, a.prototype.getEventInstances = function () {
					var a, b = this.eventInstanceGroupsById,
						c = [];
					for (a in b) c.push.apply(c, b[a].eventInstances);
					return c
				}, a.prototype.getEventInstancesWithId = function (a) {
					var b = this.eventInstanceGroupsById[a];
					return b ? b.eventInstances.slice() : []
				}, a.prototype.getEventInstancesWithoutId = function (a) {
					var b, c = this.eventInstanceGroupsById,
						d = [];
					for (b in c) b !== a && d.push.apply(d, c[b].eventInstances);
					return d
				}, a.prototype.addEventInstance = function (a, b) {
					var c = this.eventInstanceGroupsById;
					(c[b] || (c[b] = new i.default)).eventInstances.push(a), this.tryRelease()
				}, a.prototype.removeEventInstancesForDef = function (a) {
					var b, c = this.eventInstanceGroupsById,
						d = c[a.id];
					d && (b = e.removeMatching(d.eventInstances, function (b) {
						return b.def === a
					}), d.eventInstances.length || delete c[a.id], b && this.tryRelease())
				}, a.prototype.tryRelease = function () {
					this.pendingCnt || (this.freezeDepth ? this.stuntedReleaseCnt++ : this.release())
				}, a.prototype.release = function () {
					this.releaseCnt++, this.trigger("release", this.eventInstanceGroupsById)
				}, a.prototype.whenReleased = function () {
					var a = this;
					return this.releaseCnt ? f.default.resolve(this.eventInstanceGroupsById) : f.default.construct(function (b) {
						a.one("release", b)
					})
				}, a.prototype.freeze = function () {
					this.freezeDepth++ || (this.stuntedReleaseCnt = 0)
				}, a.prototype.thaw = function () {
					--this.freezeDepth || !this.stuntedReleaseCnt || this.pendingCnt || this.release()
				}, a
			}();
		b.default = j, g.default.mixInto(j)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(32),
			f = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.setElement = function (a) {
					this.el = a, this.bindGlobalHandlers(), this.renderSkeleton(), this.set("isInDom", !0)
				}, b.prototype.removeElement = function () {
					this.unset("isInDom"), this.unrenderSkeleton(), this.unbindGlobalHandlers(), this.el.remove()
				}, b.prototype.bindGlobalHandlers = function () {}, b.prototype.unbindGlobalHandlers = function () {}, b.prototype.renderSkeleton = function () {}, b.prototype.unrenderSkeleton = function () {}, b
			}(e.default);
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = c(6),
			g = function () {
				function a(a, b) {
					this.isFollowing = !1, this.isHidden = !1, this.isAnimating = !1, this.options = b = b || {}, this.sourceEl = a, this.parentEl = b.parentEl ? d(b.parentEl) : a.parent()
				}
				return a.prototype.start = function (a) {
					this.isFollowing || (this.isFollowing = !0, this.y0 = e.getEvY(a), this.x0 = e.getEvX(a), this.topDelta = 0, this.leftDelta = 0, this.isHidden || this.updatePosition(), e.getEvIsTouch(a) ? this.listenTo(d(document), "touchmove", this.handleMove) : this.listenTo(d(document), "mousemove", this.handleMove))
				}, a.prototype.stop = function (a, b) {
					var c = this,
						e = this.options.revertDuration,
						f = function () {
							c.isAnimating = !1, c.removeElement(), c.top0 = c.left0 = null, b && b()
						};
					this.isFollowing && !this.isAnimating && (this.isFollowing = !1, this.stopListeningTo(d(document)), a && e && !this.isHidden ? (this.isAnimating = !0, this.el.animate({
						top: this.top0,
						left: this.left0
					}, {
						duration: e,
						complete: f
					})) : f())
				}, a.prototype.getEl = function () {
					var a = this.el;
					return a || (a = this.el = this.sourceEl.clone().addClass(this.options.additionalClass || "").css({
						position: "absolute",
						visibility: "",
						display: this.isHidden ? "none" : "",
						margin: 0,
						right: "auto",
						bottom: "auto",
						width: this.sourceEl.width(),
						height: this.sourceEl.height(),
						opacity: this.options.opacity || "",
						zIndex: this.options.zIndex
					}), a.addClass("fc-unselectable"), a.appendTo(this.parentEl)), a
				}, a.prototype.removeElement = function () {
					this.el && (this.el.remove(), this.el = null)
				}, a.prototype.updatePosition = function () {
					var a, b;
					this.getEl(), null == this.top0 && (a = this.sourceEl.offset(), b = this.el.offsetParent().offset(), this.top0 = a.top - b.top, this.left0 = a.left - b.left), this.el.css({
						top: this.top0 + this.topDelta,
						left: this.left0 + this.leftDelta
					})
				}, a.prototype.handleMove = function (a) {
					this.topDelta = e.getEvY(a) - this.y0, this.leftDelta = e.getEvX(a) - this.x0, this.isHidden || this.updatePosition()
				}, a.prototype.hide = function () {
					this.isHidden || (this.isHidden = !0, this.el && this.el.hide())
				}, a.prototype.show = function () {
					this.isHidden && (this.isHidden = !1, this.updatePosition(), this.getEl().show())
				}, a
			}();
		b.default = g, f.default.mixInto(g)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(18),
			f = c(13),
			g = function (a) {
				function b(b) {
					var c = a.call(this, b) || this;
					return c.dragListener = c.buildDragListener(), c
				}
				return d.__extends(b, a), b.prototype.end = function () {
					this.dragListener.endInteraction()
				}, b.prototype.bindToEl = function (a) {
					var b = this.component,
						c = this.dragListener;
					b.bindDateHandlerToEl(a, "mousedown", function (a) {
						b.shouldIgnoreMouse() || c.startInteraction(a)
					}), b.bindDateHandlerToEl(a, "touchstart", function (a) {
						b.shouldIgnoreTouch() || c.startInteraction(a)
					})
				}, b.prototype.buildDragListener = function () {
					var a, b = this,
						c = this.component,
						d = new e.default(c, {
							scroll: this.opt("dragScroll"),
							interactionStart: function () {
								a = d.origHit
							},
							hitOver: function (b, c, d) {
								c || (a = null)
							},
							hitOut: function () {
								a = null
							},
							interactionEnd: function (d, e) {
								var f;
								!e && a && (f = c.getSafeHitFootprint(a)) && b.view.triggerDayClick(f, c.getHitEl(a), d)
							}
						});
					return d.shouldCancelTouchScroll = !1, d.scrollAlwaysKills = !0, d
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		function d(a) {
			var b, c, d, e = [];
			for (b = 0; b < a.length; b++) {
				for (c = a[b], d = 0; d < e.length && g(c, e[d]).length; d++);
				c.level = d, (e[d] || (e[d] = [])).push(c)
			}
			return e
		}

		function e(a) {
			var b, c, d, e, f;
			for (b = 0; b < a.length; b++)
				for (c = a[b], d = 0; d < c.length; d++)
					for (e = c[d], e.forwardSegs = [], f = b + 1; f < a.length; f++) g(e, a[f], e.forwardSegs)
		}

		function f(a) {
			var b, c, d = a.forwardSegs,
				e = 0;
			if (void 0 === a.forwardPressure) {
				for (b = 0; b < d.length; b++) c = d[b], f(c), e = Math.max(e, 1 + c.forwardPressure);
				a.forwardPressure = e
			}
		}

		function g(a, b, c) {
			void 0 === c && (c = []);
			for (var d = 0; d < b.length; d++) h(a, b[d]) && c.push(b[d]);
			return c
		}

		function h(a, b) {
			return a.bottom > b.top && a.top < b.bottom
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var i = c(0),
			j = c(2),
			k = c(31),
			l = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.timeGrid = b, d
				}
				return i.__extends(b, a), b.prototype.renderFgSegs = function (a) {
					this.renderFgSegsIntoContainers(a, this.timeGrid.fgContainerEls)
				}, b.prototype.renderFgSegsIntoContainers = function (a, b) {
					var c, d;
					for (c = this.timeGrid.groupSegsByCol(a), d = 0; d < this.timeGrid.colCnt; d++) this.updateFgSegCoords(c[d]);
					this.timeGrid.attachSegsByCol(c, b)
				}, b.prototype.unrenderFgSegs = function () {
					this.fgSegs && this.fgSegs.forEach(function (a) {
						a.el.remove()
					})
				}, b.prototype.computeEventTimeFormat = function () {
					return this.opt("noMeridiemTimeFormat")
				}, b.prototype.computeDisplayEventEnd = function () {
					return !0
				}, b.prototype.fgSegHtml = function (a, b) {
					var c, d, e, f = this.view,
						g = f.calendar,
						h = a.footprint.componentFootprint,
						i = h.isAllDay,
						k = a.footprint.eventDef,
						l = f.isEventDefDraggable(k),
						m = !b && a.isStart && f.isEventDefResizableFromStart(k),
						n = !b && a.isEnd && f.isEventDefResizableFromEnd(k),
						o = this.getSegClasses(a, l, m || n),
						p = j.cssToStr(this.getSkinCss(k));
					if (o.unshift("fc-time-grid-event", "fc-v-event"), f.isMultiDayRange(h.unzonedRange)) {
						if (a.isStart || a.isEnd) {
							var q = g.msToMoment(a.startMs),
								r = g.msToMoment(a.endMs);
							c = this._getTimeText(q, r, i), d = this._getTimeText(q, r, i, "LT"), e = this._getTimeText(q, r, i, null, !1)
						}
					} else c = this.getTimeText(a.footprint), d = this.getTimeText(a.footprint, "LT"), e = this.getTimeText(a.footprint, null, !1);
					return '<a class="' + o.join(" ") + '"' + (k.url ? ' href="' + j.htmlEscape(k.url) + '"' : "") + (p ? ' style="' + p + '"' : "") + '><div class="fc-content">' + (c ? '<div class="fc-time" data-start="' + j.htmlEscape(e) + '" data-full="' + j.htmlEscape(d) + '"><span>' + j.htmlEscape(c) + "</span></div>" : "") + (k.title ? '<div class="fc-title">' + j.htmlEscape(k.title) + "</div>" : "") + '</div><div class="fc-bg"/>' + (n ? '<div class="fc-resizer fc-end-resizer" />' : "") + "</a>"
				}, b.prototype.updateFgSegCoords = function (a) {
					this.timeGrid.computeSegVerticals(a), this.computeFgSegHorizontals(a), this.timeGrid.assignSegVerticals(a), this.assignFgSegHorizontals(a)
				}, b.prototype.computeFgSegHorizontals = function (a) {
					var b, c, g;
					if (this.sortEventSegs(a), b = d(a), e(b), c = b[0]) {
						for (g = 0; g < c.length; g++) f(c[g]);
						for (g = 0; g < c.length; g++) this.computeFgSegForwardBack(c[g], 0, 0)
					}
				}, b.prototype.computeFgSegForwardBack = function (a, b, c) {
					var d, e = a.forwardSegs;
					if (void 0 === a.forwardCoord)
						for (e.length ? (this.sortForwardSegs(e), this.computeFgSegForwardBack(e[0], b + 1, c), a.forwardCoord = e[0].backwardCoord) : a.forwardCoord = 1, a.backwardCoord = a.forwardCoord - (a.forwardCoord - c) / (b + 1), d = 0; d < e.length; d++) this.computeFgSegForwardBack(e[d], 0, a.forwardCoord)
				}, b.prototype.sortForwardSegs = function (a) {
					a.sort(j.proxy(this, "compareForwardSegs"))
				}, b.prototype.compareForwardSegs = function (a, b) {
					return b.forwardPressure - a.forwardPressure || (a.backwardCoord || 0) - (b.backwardCoord || 0) || this.compareEventSegs(a, b)
				}, b.prototype.assignFgSegHorizontals = function (a) {
					var b, c;
					for (b = 0; b < a.length; b++) c = a[b], c.el.css(this.generateFgSegHorizontalCss(c)), c.bottom - c.top < 30 && c.el.addClass("fc-short")
				}, b.prototype.generateFgSegHorizontalCss = function (a) {
					var b, c, d = this.opt("slotEventOverlap"),
						e = a.backwardCoord,
						f = a.forwardCoord,
						g = this.timeGrid.generateSegVerticalCss(a),
						h = this.timeGrid.isRTL;
					return d && (f = Math.min(1, e + 2 * (f - e))), h ? (b = 1 - f, c = e) : (b = e, c = 1 - f), g.zIndex = a.level + 1, g.left = 100 * b + "%", g.right = 100 * c + "%", d && a.forwardPressure && (g[h ? "marginLeft" : "marginRight"] = 20), g
				}, b
			}(k.default);
		b.default = l
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(43),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.renderSegs = function (a, b) {
					var c, d, f, g = [];
					for (this.eventRenderer.renderFgSegsIntoContainers(a, this.component.helperContainerEls), c = 0; c < a.length; c++) d = a[c], b && b.col === d.col && (f = b.el, d.el.css({
						left: f.css("left"),
						right: f.css("right"),
						"margin-left": f.css("margin-left"),
						"margin-right": f.css("margin-right")
					})), g.push(d.el[0]);
					return e(g)
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(42),
			f = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.attachSegEls = function (a, b) {
					var c, d = this.component;
					return "bgEvent" === a ? c = d.bgContainerEls : "businessHours" === a ? c = d.businessContainerEls : "highlight" === a && (c = d.highlightContainerEls), d.updateSegVerticals(b), d.attachSegsByCol(d.groupSegsByCol(b), c), b.map(function (a) {
						return a.el[0]
					})
				}, b
			}(e.default);
		b.default = f
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(1),
			e = c(2),
			f = c(6),
			g = function () {
				function a(a) {
					this.isHidden = !0, this.margin = 10, this.options = a || {}
				}
				return a.prototype.show = function () {
					this.isHidden && (this.el || this.render(), this.el.show(), this.position(), this.isHidden = !1, this.trigger("show"))
				}, a.prototype.hide = function () {
					this.isHidden || (this.el.hide(), this.isHidden = !0, this.trigger("hide"))
				}, a.prototype.render = function () {
					var a = this,
						b = this.options;
					this.el = d('<div class="fc-popover"/>').addClass(b.className || "").css({
						top: 0,
						left: 0
					}).append(b.content).appendTo(b.parentEl), this.el.on("click", ".fc-close", function () {
						a.hide()
					}), b.autoHide && this.listenTo(d(document), "mousedown", this.documentMousedown)
				}, a.prototype.documentMousedown = function (a) {
					this.el && !d(a.target).closest(this.el).length && this.hide()
				}, a.prototype.removeElement = function () {
					this.hide(), this.el && (this.el.remove(), this.el = null), this.stopListeningTo(d(document), "mousedown")
				}, a.prototype.position = function () {
					var a, b, c, f, g, h = this.options,
						i = this.el.offsetParent().offset(),
						j = this.el.outerWidth(),
						k = this.el.outerHeight(),
						l = d(window),
						m = e.getScrollParent(this.el);
					f = h.top || 0, g = void 0 !== h.left ? h.left : void 0 !== h.right ? h.right - j : 0, m.is(window) || m.is(document) ? (m = l, a = 0, b = 0) : (c = m.offset(), a = c.top, b = c.left), a += l.scrollTop(), b += l.scrollLeft(), !1 !== h.viewportConstrain && (f = Math.min(f, a + m.outerHeight() - k - this.margin), f = Math.max(f, a + this.margin), g = Math.min(g, b + m.outerWidth() - j - this.margin), g = Math.max(g, b + this.margin)), this.el.css({
						top: f - i.top,
						left: g - i.left
					})
				}, a.prototype.trigger = function (a) {
					this.options[a] && this.options[a].apply(this, Array.prototype.slice.call(arguments, 1))
				}, a
			}();
		b.default = g, f.default.mixInto(g)
	}, function (a, b, c) {
		function d(a, b) {
			var c, d;
			for (c = 0; c < b.length; c++)
				if (d = b[c], d.leftCol <= a.rightCol && d.rightCol >= a.leftCol) return !0;
			return !1
		}

		function e(a, b) {
			return a.leftCol - b.leftCol
		}
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var f = c(0),
			g = c(1),
			h = c(2),
			i = c(31),
			j = function (a) {
				function b(b, c) {
					var d = a.call(this, b, c) || this;
					return d.dayGrid = b, d
				}
				return f.__extends(b, a), b.prototype.renderBgRanges = function (b) {
					b = g.grep(b, function (a) {
						return a.eventDef.isAllDay()
					}), a.prototype.renderBgRanges.call(this, b)
				}, b.prototype.renderFgSegs = function (a) {
					var b = this.rowStructs = this.renderSegRows(a);
					this.dayGrid.rowEls.each(function (a, c) {
						g(c).find(".fc-content-skeleton > table").append(b[a].tbodyEl)
					})
				}, b.prototype.unrenderFgSegs = function () {
					for (var a, b = this.rowStructs || []; a = b.pop();) a.tbodyEl.remove();
					this.rowStructs = null
				}, b.prototype.renderSegRows = function (a) {
					var b, c, d = [];
					for (b = this.groupSegRows(a), c = 0; c < b.length; c++) d.push(this.renderSegRow(c, b[c]));
					return d
				}, b.prototype.renderSegRow = function (a, b) {
					function c(a) {
						for (; f < a;) k = (r[d - 1] || [])[f], k ? k.attr("rowspan", parseInt(k.attr("rowspan") || 1, 10) + 1) : (k = g("<td/>"), h.append(k)), q[d][f] = k, r[d][f] = k, f++
					}
					var d, e, f, h, i, j, k, l = this.dayGrid.colCnt,
						m = this.buildSegLevels(b),
						n = Math.max(1, m.length),
						o = g("<tbody/>"),
						p = [],
						q = [],
						r = [];
					for (d = 0; d < n; d++) {
						if (e = m[d], f = 0, h = g("<tr/>"), p.push([]), q.push([]), r.push([]), e)
							for (i = 0; i < e.length; i++) {
								for (j = e[i], c(j.leftCol), k = g('<td class="fc-event-container"/>').append(j.el), j.leftCol != j.rightCol ? k.attr("colspan", j.rightCol - j.leftCol + 1) : r[d][f] = k; f <= j.rightCol;) q[d][f] = k, p[d][f] = j, f++;
								h.append(k)
							}
						c(l), this.dayGrid.bookendCells(h), o.append(h)
					}
					return {
						row: a,
						tbodyEl: o,
						cellMatrix: q,
						segMatrix: p,
						segLevels: m,
						segs: b
					}
				}, b.prototype.buildSegLevels = function (a) {
					var b, c, f, g = [];
					for (this.sortEventSegs(a), b = 0; b < a.length; b++) {
						for (c = a[b], f = 0; f < g.length && d(c, g[f]); f++);
						c.level = f, (g[f] || (g[f] = [])).push(c)
					}
					for (f = 0; f < g.length; f++) g[f].sort(e);
					return g
				}, b.prototype.groupSegRows = function (a) {
					var b, c = [];
					for (b = 0; b < this.dayGrid.rowCnt; b++) c.push([]);
					for (b = 0; b < a.length; b++) c[a[b].row].push(a[b]);
					return c
				}, b.prototype.computeEventTimeFormat = function () {
					return this.opt("extraSmallTimeFormat")
				}, b.prototype.computeDisplayEventEnd = function () {
					return 1 === this.dayGrid.colCnt
				}, b.prototype.fgSegHtml = function (a, b) {
					var c, d, e = this.view,
						f = a.footprint.eventDef,
						g = a.footprint.componentFootprint.isAllDay,
						i = e.isEventDefDraggable(f),
						j = !b && g && a.isStart && e.isEventDefResizableFromStart(f),
						k = !b && g && a.isEnd && e.isEventDefResizableFromEnd(f),
						l = this.getSegClasses(a, i, j || k),
						m = h.cssToStr(this.getSkinCss(f)),
						n = "";
					return l.unshift("fc-day-grid-event", "fc-h-event"), a.isStart && (c = this.getTimeText(a.footprint)) && (n = '<span class="fc-time">' + h.htmlEscape(c) + "</span>"), d = '<span class="fc-title">' + (h.htmlEscape(f.title || "") || "&nbsp;") + "</span>", '<a class="' + l.join(" ") + '"' + (f.url ? ' href="' + h.htmlEscape(f.url) + '"' : "") + (m ? ' style="' + m + '"' : "") + '><div class="fc-content">' + (this.dayGrid.isRTL ? d + " " + n : n + " " + d) + "</div>" + (j ? '<div class="fc-resizer fc-start-resizer" />' : "") + (k ? '<div class="fc-resizer fc-end-resizer" />' : "") + "</a>"
				}, b
			}(i.default);
		b.default = j
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(43),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.renderSegs = function (a, b) {
					var c, d = [];
					return c = this.eventRenderer.renderSegRows(a), this.component.rowEls.each(function (a, f) {
						var g, h, i = e(f),
							j = e('<div class="fc-helper-skeleton"><table/></div>');
						b && b.row === a ? h = b.el.position().top : (g = i.find(".fc-content-skeleton tbody"), g.length || (g = i.find(".fc-content-skeleton table")), h = g.position().top), j.css("top", h).find("table").append(c[a].tbodyEl), i.append(j), d.push(j[0])
					}), e(d)
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(42),
			g = function (a) {
				function b() {
					var b = null !== a && a.apply(this, arguments) || this;
					return b.fillSegTag = "td", b
				}
				return d.__extends(b, a), b.prototype.attachSegEls = function (a, b) {
					var c, d, e, f = [];
					for (c = 0; c < b.length; c++) d = b[c], e = this.renderFillRow(a, d), this.component.rowEls.eq(d.row).append(e), f.push(e[0]);
					return f
				}, b.prototype.renderFillRow = function (a, b) {
					var c, d, f, g = this.component.colCnt,
						h = b.leftCol,
						i = b.rightCol + 1;
					return c = "businessHours" === a ? "bgevent" : a.toLowerCase(), d = e('<div class="fc-' + c + '-skeleton"><table><tr/></table></div>'), f = d.find("tr"), h > 0 && f.append('<td colspan="' + h + '"/>'), f.append(b.el.attr("colspan", i - h)), i < g && f.append('<td colspan="' + (g - i) + '"/>'), this.component.bookendCells(f), d
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(2),
			f = c(31),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.renderFgSegs = function (a) {
					a.length ? this.component.renderSegList(a) : this.component.renderEmptyMessage()
				}, b.prototype.fgSegHtml = function (a) {
					var b, c = this.view,
						d = c.calendar,
						f = d.theme,
						g = a.footprint,
						h = g.eventDef,
						i = g.componentFootprint,
						j = h.url,
						k = ["fc-list-item"].concat(this.getClasses(h)),
						l = this.getBgColor(h);
					return b = i.isAllDay ? c.getAllDayHtml() : c.isMultiDayRange(i.unzonedRange) ? a.isStart || a.isEnd ? e.htmlEscape(this._getTimeText(d.msToMoment(a.startMs), d.msToMoment(a.endMs), i.isAllDay)) : c.getAllDayHtml() : e.htmlEscape(this.getTimeText(g)), j && k.push("fc-has-url"),
						'<tr class="' + k.join(" ") + '">' + (this.displayEventTime ? '<td class="fc-list-item-time ' + f.getClass("widgetContent") + '">' + (b || "") + "</td>" : "") + '<td class="fc-list-item-marker ' + f.getClass("widgetContent") + '"><span class="fc-event-dot"' + (l ? ' style="background-color:' + l + '"' : "") + '></span></td><td class="fc-list-item-title ' + f.getClass("widgetContent") + '"><a' + (j ? ' href="' + e.htmlEscape(j) + '"' : "") + ">" + e.htmlEscape(h.title || "") + "</a></td></tr>"
				}, b.prototype.computeEventTimeFormat = function () {
					return this.opt("mediumTimeFormat")
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(1),
			f = c(44),
			g = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b.prototype.handleClick = function (b, c) {
					var d;
					a.prototype.handleClick.call(this, b, c), e(c.target).closest("a[href]").length || (d = b.footprint.eventDef.url) && !c.isDefaultPrevented() && (window.location.href = d)
				}, b
			}(f.default);
		b.default = g
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(25),
			e = c(34),
			f = c(57),
			g = c(58);
		d.default.registerClass(e.default), d.default.registerClass(f.default), d.default.registerClass(g.default)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(36),
			e = c(55),
			f = c(56),
			g = c(94);
		d.default.register("standard", e.default), d.default.register("jquery-ui", f.default), d.default.register("bootstrap3", g.default)
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(0),
			e = c(27),
			f = function (a) {
				function b() {
					return null !== a && a.apply(this, arguments) || this
				}
				return d.__extends(b, a), b
			}(e.default);
		b.default = f, f.prototype.classes = {
			widget: "fc-bootstrap3",
			tableGrid: "table-bordered",
			tableList: "table table-striped",
			buttonGroup: "btn-group",
			button: "btn btn-default",
			stateActive: "active",
			stateDisabled: "disabled",
			today: "alert alert-info",
			popover: "panel panel-default",
			popoverHeader: "panel-heading",
			popoverContent: "panel-body",
			headerRow: "panel-default",
			dayRow: "panel-default",
			listView: "panel panel-default"
		}, f.prototype.baseIconClass = "glyphicon", f.prototype.iconClasses = {
			close: "glyphicon-remove",
			prev: "glyphicon-chevron-left",
			next: "glyphicon-chevron-right",
			prevYear: "glyphicon-backward",
			nextYear: "glyphicon-forward"
		}, f.prototype.iconOverrideOption = "bootstrapGlyphicons", f.prototype.iconOverrideCustomButtonOption = "bootstrapGlyphicon", f.prototype.iconOverridePrefix = "glyphicon-"
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(7),
			e = c(47),
			f = c(70),
			g = d.default.views;
		g.basic = {
			class: e.default
		}, g.basicDay = {
			type: "basic",
			duration: {
				days: 1
			}
		}, g.basicWeek = {
			type: "basic",
			duration: {
				weeks: 1
			}
		}, g.month = {
			class: f.default,
			duration: {
				months: 1
			},
			defaults: {
				fixedWeekCount: !0
			}
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(7),
			e = c(67),
			f = d.default.views;
		f.agenda = {
			class: e.default,
			defaults: {
				allDaySlot: !0,
				slotDuration: "00:30:00",
				slotEventOverlap: !0
			}
		}, f.agendaDay = {
			type: "agenda",
			duration: {
				days: 1
			}
		}, f.agendaWeek = {
			type: "agenda",
			duration: {
				weeks: 1
			}
		}
	}, function (a, b, c) {
		Object.defineProperty(b, "__esModule", {
			value: !0
		});
		var d = c(7),
			e = c(71),
			f = d.default.views;
		f.list = {
			class: e.default,
			buttonTextKey: "list",
			defaults: {
				buttonText: "list",
				listDayFormat: "LL",
				noEventsMessage: "No events to display"
			}
		}, f.listDay = {
			type: "list",
			duration: {
				days: 1
			},
			defaults: {
				listDayFormat: "dddd"
			}
		}, f.listWeek = {
			type: "list",
			duration: {
				weeks: 1
			},
			defaults: {
				listDayFormat: "dddd",
				listDayAltFormat: "LL"
			}
		}, f.listMonth = {
			type: "list",
			duration: {
				month: 1
			},
			defaults: {
				listDayAltFormat: "dddd"
			}
		}, f.listYear = {
			type: "list",
			duration: {
				year: 1
			},
			defaults: {
				listDayAltFormat: "dddd"
			}
		}
	}])
});
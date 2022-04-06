webpackJsonp([49],{

/***/ 1163:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

__webpack_require__(772);

var _constant = __webpack_require__(774);

var _constant2 = _interopRequireDefault(_constant);

var _vuex = __webpack_require__(176);

var _vueLoadingOverlay = __webpack_require__(373);

var _vueLoadingOverlay2 = _interopRequireDefault(_vueLoadingOverlay);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "ordergetbyid",
    data: function data() {
        return {
            isLoading: false,
            fullPage: false,
            color: "#007bff",
            currentSort: "Id",
            currentSortDir: "asc",
            loading: true,
            OrderId: 0,
            TotalPrice: 0,
            order: {},
            orderDetail: [],
            orderDetailPromotion: [],
            products: [],
            customer: {}
        };
    },

    created: {},
    components: {
        Loading: _vueLoadingOverlay2.default
    },
    mounted: function mounted() {
        var _this = this;

        if (this.$route.params.id > 0) {
            this.isLoading = true;
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getOrder(this.$route.params.id).then(function (response) {
                debugger;
                _this.order = response;
                console.log(_this.order);
            });
            this.isLoading = false;
        };
    },


    computed: (0, _extends3.default)({}, (0, _vuex.mapGetters)(["Order"])),

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getOrder"]))
};

/***/ }),

/***/ 1429:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "", ""]);

// exports


/***/ }),

/***/ 1525:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "productadd"
  }, [_c('loading', {
    attrs: {
      "active": _vm.isLoading,
      "height": 35,
      "width": 35,
      "color": _vm.color,
      "is-full-page": _vm.fullPage
    },
    on: {
      "update:active": function($event) {
        _vm.isLoading = $event
      }
    }
  }), _vm._v(" "), _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-header"
  }, [_vm._v("\n                    Thông tin đơn hàng\n                ")]), _vm._v(" "), _c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-4"
  }, [_c('div', {
    staticClass: "table-responsive"
  }, [_c('div', {
    staticClass: "dataTables_wrapper dt-bootstrap4 no-footer"
  }, [_c('div', {
    staticClass: "clear"
  }), _vm._v(" "), _c('table', {
    staticClass: "table table-bordered table-mini-text table-style-custom no-margin"
  }, [_c('tbody', [_vm._m(0), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Mã đơn hàng")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.code))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Trạng thái")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.status))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Nguồn đơn hàng")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.src))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Ngày tạo")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.createDate))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Mã voucher")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.odderDetail[0].voucher))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Giá trị voucher")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.odderDetail[0].voucherPrice))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Tổng tiền")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.odderDetail[0].oderPrice))])])])])])])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-4"
  }, [_c('div', {
    staticClass: "table-responsive"
  }, [_c('div', {
    staticClass: "dataTables_wrapper dt-bootstrap4 no-footer"
  }, [_c('div', {
    staticClass: "clear"
  }), _vm._v(" "), _c('table', {
    staticClass: "table table-bordered table-mini-text table-style-custom no-margin"
  }, [_c('tbody', [_vm._m(1), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Họ và tên")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.customer.name))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Giới tính")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.customer.gender))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Số điện thoại")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.customer.phoneNumber))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Email")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.customer.email))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Địa chỉ")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.address))])]), _vm._v(" "), _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', [_vm._v("Ghi chú")]), _vm._v(" "), _c('td', [_vm._v(_vm._s(_vm.order.customer.note))])])])])])])])])])])])]), _vm._v(" "), _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-header"
  }, [_vm._v("\n                    Chi tiết đơn hàng\n                ")]), _vm._v(" "), _c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "table-responsive"
  }, [_c('div', {
    staticClass: "dataTables_wrapper dt-bootstrap4 no-footer"
  }, [_c('div', {
    staticClass: "clear"
  }), _vm._v(" "), _c('table', {
    staticClass: "table table-bordered table-mini-text table-style-custom no-margin"
  }, [_vm._m(2), _vm._v(" "), _c('tbody', _vm._l((_vm.order.odderDetail), function(item, index) {
    return _c('tr', {
      staticClass: "odd",
      attrs: {
        "role": "row"
      }
    }, [_c('td', [_vm._v(_vm._s(index + 1))]), _vm._v(" "), _c('td', [_vm._v(_vm._s(item.code))]), _vm._v(" "), _c('td', [_c('a', {
      attrs: {
        "href": item.url
      }
    }, [_vm._v(_vm._s(item.name))])]), _vm._v(" "), _c('td', [_vm._v(" " + _vm._s(item.number))]), _vm._v(" "), _c('td', [_vm._v(_vm._s(item.price))]), _vm._v(" "), _c('td', {
      domProps: {
        "innerHTML": _vm._s(item.strPromotion)
      }
    }), _vm._v(" "), _c('td', [_vm._v(_vm._s(item.totalPrice))])])
  }), 0)])])])])])])])])])], 1)
},staticRenderFns: [function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', {
    attrs: {
      "colspan": "2"
    }
  }, [_c('b', [_vm._v("Thông tin chính")])])])
},function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('tr', {
    staticClass: "odd",
    attrs: {
      "role": "row"
    }
  }, [_c('td', {
    attrs: {
      "colspan": "2"
    }
  }, [_c('b', [_vm._v("Thông tin khách hàng")])])])
},function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('thead', [_c('tr', [_c('th', [_vm._v("STT")]), _vm._v(" "), _c('th', [_vm._v("Mã sản phẩm")]), _vm._v(" "), _c('th', [_vm._v("Sản phẩm")]), _vm._v(" "), _c('th', [_vm._v("SL")]), _vm._v(" "), _c('th', [_vm._v("Giá bán")]), _vm._v(" "), _c('th', [_vm._v("Khuyến mại ")]), _vm._v(" "), _c('th', [_vm._v("Thành tiền")])])])
}]}

/***/ }),

/***/ 1575:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1429);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("4577a7a0", content, true);

/***/ }),

/***/ 740:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1575)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1163),
  /* template */
  __webpack_require__(1525),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 772:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(773);
if(typeof content === 'string') content = [[module.i, content, '']];
// add the styles to the DOM
var update = __webpack_require__(175)(content, {});
if(content.locals) module.exports = content.locals;
// Hot Module Replacement
if(false) {
	// When the styles change, update the <style> tags
	if(!content.locals) {
		module.hot.accept("!!../../css-loader/index.js!./vue-loading.css", function() {
			var newContent = require("!!../../css-loader/index.js!./vue-loading.css");
			if(typeof newContent === 'string') newContent = [[module.id, newContent, '']];
			update(newContent);
		});
	}
	// When the module is disposed, remove the <style> tags
	module.hot.dispose(function() { update(); });
}

/***/ }),

/***/ 773:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, ".vld-overlay {\n  bottom: 0;\n  left: 0;\n  position: absolute;\n  right: 0;\n  top: 0;\n  align-items: center;\n  display: none;\n  justify-content: center;\n  overflow: hidden;\n  z-index: 9999;\n}\n\n.vld-overlay.is-active {\n  display: flex;\n}\n\n.vld-overlay.is-full-page {\n  z-index: 9999;\n  position: fixed;\n}\n\n.vld-overlay .vld-background {\n  bottom: 0;\n  left: 0;\n  position: absolute;\n  right: 0;\n  top: 0;\n  background: #fff;\n  opacity: 0.5;\n}\n\n.vld-overlay .vld-icon, .vld-parent {\n  position: relative;\n}\n\n", ""]);

// exports


/***/ }),

/***/ 774:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
   value: true
});
var msgNotify = {};
exports.default = msgNotify;

/***/ }),

/***/ 777:
/***/ (function(module, exports, __webpack_require__) {

/*
  MIT License http://www.opensource.org/licenses/mit-license.php
  Author Tobias Koppers @sokra
  Modified by Evan You @yyx990803
*/

var hasDocument = typeof document !== 'undefined'

if (typeof DEBUG !== 'undefined' && DEBUG) {
  if (!hasDocument) {
    throw new Error(
    'vue-style-loader cannot be used in a non-browser environment. ' +
    "Use { target: 'node' } in your Webpack config to indicate a server-rendering environment."
  ) }
}

var listToStyles = __webpack_require__(786)

/*
type StyleObject = {
  id: number;
  parts: Array<StyleObjectPart>
}

type StyleObjectPart = {
  css: string;
  media: string;
  sourceMap: ?string
}
*/

var stylesInDom = {/*
  [id: number]: {
    id: number,
    refs: number,
    parts: Array<(obj?: StyleObjectPart) => void>
  }
*/}

var head = hasDocument && (document.head || document.getElementsByTagName('head')[0])
var singletonElement = null
var singletonCounter = 0
var isProduction = false
var noop = function () {}

// Force single-tag solution on IE6-9, which has a hard limit on the # of <style>
// tags it will allow on a page
var isOldIE = typeof navigator !== 'undefined' && /msie [6-9]\b/.test(navigator.userAgent.toLowerCase())

module.exports = function (parentId, list, _isProduction) {
  isProduction = _isProduction

  var styles = listToStyles(parentId, list)
  addStylesToDom(styles)

  return function update (newList) {
    var mayRemove = []
    for (var i = 0; i < styles.length; i++) {
      var item = styles[i]
      var domStyle = stylesInDom[item.id]
      domStyle.refs--
      mayRemove.push(domStyle)
    }
    if (newList) {
      styles = listToStyles(parentId, newList)
      addStylesToDom(styles)
    } else {
      styles = []
    }
    for (var i = 0; i < mayRemove.length; i++) {
      var domStyle = mayRemove[i]
      if (domStyle.refs === 0) {
        for (var j = 0; j < domStyle.parts.length; j++) {
          domStyle.parts[j]()
        }
        delete stylesInDom[domStyle.id]
      }
    }
  }
}

function addStylesToDom (styles /* Array<StyleObject> */) {
  for (var i = 0; i < styles.length; i++) {
    var item = styles[i]
    var domStyle = stylesInDom[item.id]
    if (domStyle) {
      domStyle.refs++
      for (var j = 0; j < domStyle.parts.length; j++) {
        domStyle.parts[j](item.parts[j])
      }
      for (; j < item.parts.length; j++) {
        domStyle.parts.push(addStyle(item.parts[j]))
      }
      if (domStyle.parts.length > item.parts.length) {
        domStyle.parts.length = item.parts.length
      }
    } else {
      var parts = []
      for (var j = 0; j < item.parts.length; j++) {
        parts.push(addStyle(item.parts[j]))
      }
      stylesInDom[item.id] = { id: item.id, refs: 1, parts: parts }
    }
  }
}

function createStyleElement () {
  var styleElement = document.createElement('style')
  styleElement.type = 'text/css'
  head.appendChild(styleElement)
  return styleElement
}

function addStyle (obj /* StyleObjectPart */) {
  var update, remove
  var styleElement = document.querySelector('style[data-vue-ssr-id~="' + obj.id + '"]')

  if (styleElement) {
    if (isProduction) {
      // has SSR styles and in production mode.
      // simply do nothing.
      return noop
    } else {
      // has SSR styles but in dev mode.
      // for some reason Chrome can't handle source map in server-rendered
      // style tags - source maps in <style> only works if the style tag is
      // created and inserted dynamically. So we remove the server rendered
      // styles and inject new ones.
      styleElement.parentNode.removeChild(styleElement)
    }
  }

  if (isOldIE) {
    // use singleton mode for IE9.
    var styleIndex = singletonCounter++
    styleElement = singletonElement || (singletonElement = createStyleElement())
    update = applyToSingletonTag.bind(null, styleElement, styleIndex, false)
    remove = applyToSingletonTag.bind(null, styleElement, styleIndex, true)
  } else {
    // use multi-style-tag mode in all other cases
    styleElement = createStyleElement()
    update = applyToTag.bind(null, styleElement)
    remove = function () {
      styleElement.parentNode.removeChild(styleElement)
    }
  }

  update(obj)

  return function updateStyle (newObj /* StyleObjectPart */) {
    if (newObj) {
      if (newObj.css === obj.css &&
          newObj.media === obj.media &&
          newObj.sourceMap === obj.sourceMap) {
        return
      }
      update(obj = newObj)
    } else {
      remove()
    }
  }
}

var replaceText = (function () {
  var textStore = []

  return function (index, replacement) {
    textStore[index] = replacement
    return textStore.filter(Boolean).join('\n')
  }
})()

function applyToSingletonTag (styleElement, index, remove, obj) {
  var css = remove ? '' : obj.css

  if (styleElement.styleSheet) {
    styleElement.styleSheet.cssText = replaceText(index, css)
  } else {
    var cssNode = document.createTextNode(css)
    var childNodes = styleElement.childNodes
    if (childNodes[index]) styleElement.removeChild(childNodes[index])
    if (childNodes.length) {
      styleElement.insertBefore(cssNode, childNodes[index])
    } else {
      styleElement.appendChild(cssNode)
    }
  }
}

function applyToTag (styleElement, obj) {
  var css = obj.css
  var media = obj.media
  var sourceMap = obj.sourceMap

  if (media) {
    styleElement.setAttribute('media', media)
  }

  if (sourceMap) {
    // https://developer.chrome.com/devtools/docs/javascript-debugging
    // this makes source maps inside style tags work properly in Chrome
    css += '\n/*# sourceURL=' + sourceMap.sources[0] + ' */'
    // http://stackoverflow.com/a/26603875
    css += '\n/*# sourceMappingURL=data:application/json;base64,' + btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap)))) + ' */'
  }

  if (styleElement.styleSheet) {
    styleElement.styleSheet.cssText = css
  } else {
    while (styleElement.firstChild) {
      styleElement.removeChild(styleElement.firstChild)
    }
    styleElement.appendChild(document.createTextNode(css))
  }
}


/***/ }),

/***/ 786:
/***/ (function(module, exports) {

/**
 * Translates the list format produced by css-loader into something
 * easier to manipulate.
 */
module.exports = function listToStyles (parentId, list) {
  var styles = []
  var newStyles = {}
  for (var i = 0; i < list.length; i++) {
    var item = list[i]
    var id = item[0]
    var css = item[1]
    var media = item[2]
    var sourceMap = item[3]
    var part = {
      id: parentId + ':' + i,
      css: css,
      media: media,
      sourceMap: sourceMap
    }
    if (!newStyles[id]) {
      styles.push(newStyles[id] = { id: id, parts: [part] })
    } else {
      newStyles[id].parts.push(part)
    }
  }
  return styles
}


/***/ })

});
//# sourceMappingURL=49.js.map
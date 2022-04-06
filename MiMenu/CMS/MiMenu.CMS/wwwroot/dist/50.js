webpackJsonp([50],{

/***/ 1158:
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
    name: "propertyaddedit",
    data: function data() {
        return {
            isLoading: false,
            fullPage: false,
            color: "#007bff",
            currentSort: "Id",
            currentSortDir: "asc",
            loading: true,
            locationId: 0,
            objRequest: {},
            objRequestDetail: {},
            objRequestDetails: [],
            langSelected: "",
            Languages: []
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
            this.getLocation(this.$route.params.id).then(function (respose) {
                _this.locationId = respose.data.id;
                _this.objRequest = respose.data;
                _this.objRequestDetails = respose.listData;
                if (respose.data.locationInLanguage != null) {
                    _this.objRequestDetails = respose.listData;
                    if (_this.objRequestDetails != null && _this.objRequestDetails.length > 0) {
                        _this.objRequestDetail = _this.objRequestDetails[0];
                    }
                }
                _this.langSelected = _this.objRequestDetail.languageCode.trim();
            });
            this.isLoading = false;
        };

        this.getAllLanguages().then(function (respose) {
            var lang = respose.listData;
            _this.Languages = lang.map(function (item) {
                return {
                    value: item.languageCode.trim(),
                    text: item.name.trim()
                };
            });
        });
    },


    computed: (0, _extends3.default)({}, (0, _vuex.mapGetters)(["location"])),

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getLocation", "addLocation", "updateLocation", "getAllLanguages", "addLocationInLanguage"]), {
        DoAddEdit: function DoAddEdit() {
            var _this2 = this;

            this.isLoading = true;
            if (this.objRequest.id > 0) {
                this.updateLocation(this.objRequest).then(function (response) {
                    if (response.success == true) {
                        _this2.$toast.success(response.message, {});
                        _this2.isLoading = false;
                    } else {
                        _this2.$toast.error(response.message, {});
                        _this2.isLoading = false;
                    }
                }).catch(function (e) {
                    _this2.$toast.error(_constant2.default.error + ". Error:" + e, {});
                    _this2.isLoading = false;
                });
            } else {
                this.objRequest.id = this.propertyId;
                this.addLocation(this.objRequest).then(function (response) {
                    if (response.success == true) {
                        _this2.$toast.success(response.message, {});
                        _this2.objRequest.id = response.data.locationId;
                        _this2.locationId = response.data.locationId;
                        _this2.$router.push({
                            path: "/admin/location/edit/" + response.data.locationId
                        });
                        _this2.isLoading = false;
                    } else {
                        _this2.$toast.error(response.message, {});
                        _this2.isLoading = false;
                    }
                }).catch(function (e) {
                    _this2.$toast.error(_constant2.default.error + ". Error:" + e, {});
                    _this2.isLoading = false;
                });
            }
        },
        onChangeSelectd: function onChangeSelectd() {
            if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                var lang = this.langSelected || "vi-VN";
                var lstObjLang = this.objRequestDetails.filter(function (item) {
                    return item.languageCode.trim() === lang.trim();
                });
                if (lstObjLang != null && lstObjLang != undefined && lstObjLang.length > 0) {
                    this.objRequestDetail = lstObjLang[0];
                } else {
                    this.objRequestDetail = {};
                    this.objRequestDetail.languageCode = lang;
                }
            } else {
                var _lang = this.langSelected;
                this.objRequestDetail = {};
                this.objRequestDetail.languageCode = _lang;
            }
        },
        DoAddDetail: function DoAddDetail() {
            var _this3 = this;

            this.objRequestDetail.LocationId = this.locationId;
            this.addLocationInLanguage(this.objRequestDetail).then(function (response) {
                if (response.success == true) {
                    if (!_this3.objRequestDetails.some(function (x) {
                        return x.languageCode == _this3.objRequestDetail.languageCode;
                    })) {
                        _this3.objRequestDetails.push(_this3.objRequestDetail);
                    }
                    _this3.$toast.success(response.message, {});
                } else {
                    _this3.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this3.$toast.error(_constant2.default.error + ". Error:" + e, {});
                _this3.isLoading = false;
            });
        }
    })
};

/***/ }),

/***/ 1435:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "", ""]);

// exports


/***/ }),

/***/ 1544:
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
    staticClass: "col-sm-6 col-md-6"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-header"
  }, [_vm._v("\n                    Thông tin chính\n                ")]), _vm._v(" "), _c('div', {
    staticClass: "card-body"
  }, [_c('b-form', {
    staticClass: "form-horizontal"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Mã khu vực"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Mã khu vực",
      "required": ""
    },
    model: {
      value: (_vm.objRequest.code),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "code", $$v)
      },
      expression: "objRequest.code"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Tên khu vực"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Tên khu vực",
      "required": ""
    },
    model: {
      value: (_vm.objRequest.name),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "name", $$v)
      },
      expression: "objRequest.name"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Ghi chú"
    }
  }, [_c('b-form-textarea', {
    attrs: {
      "placeholder": "Ghi chú",
      "required": ""
    },
    model: {
      value: (_vm.objRequest.note),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "note", $$v)
      },
      expression: "objRequest.note"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-3"
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form col-md-12 btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.DoAddEdit()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                            ")])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }, [_c('button', {
    staticClass: "btn btn-success col-md-12 btncus",
    attrs: {
      "type": "button"
    },
    on: {
      "click": function($event) {
        return _vm.DoRefesh()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-refresh"
  }), _vm._v(" Làm mới\n                            ")])])])], 1)])]), _vm._v(" "), (_vm.objRequest.id > 0) ? _c('div', {
    staticClass: "col-sm-6 col-md-6"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-header"
  }, [_vm._v("\n                    Thông tin bổ sung\n                ")]), _vm._v(" "), _c('div', {
    staticClass: "card-body"
  }, [_c('b-form', {
    staticClass: "form-horizontal"
  }, [_c('b-row', [_c('b-col', [_c('b-form-group', {
    attrs: {
      "label": "Ngôn ngữ"
    }
  }, [_c('b-form-select', {
    attrs: {
      "options": _vm.Languages
    },
    on: {
      "change": _vm.onChangeSelectd
    },
    model: {
      value: (_vm.langSelected),
      callback: function($$v) {
        _vm.langSelected = $$v
      },
      expression: "langSelected"
    }
  })], 1)], 1), _vm._v(" "), _c('b-col'), _vm._v(" "), _c('b-col')], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Tên theo ngôn ngữ"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Tên",
      "required": ""
    },
    model: {
      value: (_vm.objRequestDetail.name),
      callback: function($$v) {
        _vm.$set(_vm.objRequestDetail, "name", $$v)
      },
      expression: "objRequestDetail.name"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Tên theo ngôn ngữ"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Đường dẫn",
      "required": ""
    },
    model: {
      value: (_vm.objRequestDetail.url),
      callback: function($$v) {
        _vm.$set(_vm.objRequestDetail, "url", $$v)
      },
      expression: "objRequestDetail.url"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-3"
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-3"
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form col-md-12 btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.DoAddDetail()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                            ")])])])], 1)])]) : _vm._e()])], 1)
},staticRenderFns: []}

/***/ }),

/***/ 1581:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1435);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("1a9b68cc", content, true);

/***/ }),

/***/ 735:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1581)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1158),
  /* template */
  __webpack_require__(1544),
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
//# sourceMappingURL=50.js.map
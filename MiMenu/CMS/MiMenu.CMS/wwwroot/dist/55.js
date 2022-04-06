webpackJsonp([55],{

/***/ 1109:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _http = __webpack_require__(4);

var _http2 = _interopRequireDefault(_http);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var roleRepository = {
    assignPermissionRole: function assignPermissionRole(id, data) {
        return _http2.default.post('/api/permission/save-permissions?role=' + id, data).then(function (response) {
            console.log(response.data);
            return response.data;
        }).catch(function (e) {
            alert('ex found:' + e);
        });
    }
};

exports.default = roleRepository;

/***/ }),

/***/ 1189:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _regenerator = __webpack_require__(75);

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(74);

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

var _http = __webpack_require__(4);

var _http2 = _interopRequireDefault(_http);

var _roleRepository = __webpack_require__(1109);

var _roleRepository2 = _interopRequireDefault(_roleRepository);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: 'assignPermissionRole',
    data: function data() {
        return {
            functions: [],

            actionss: []
        };
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getListFunctions", "getListActions"]), {
        getFunction: function getFunction() {
            var _this = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee() {
                var response;
                return _regenerator2.default.wrap(function _callee$(_context) {
                    while (1) {
                        switch (_context.prev = _context.next) {
                            case 0:
                                _context.next = 2;
                                return _http2.default.get("/api/role/getallfunctions").catch(function (e) {
                                    alert('ex found:' + e);
                                });

                            case 2:
                                response = _context.sent;

                                console.log(response.data);
                                _this.functions = response.data;

                            case 5:
                            case "end":
                                return _context.stop();
                        }
                    }
                }, _callee, _this);
            }))();
        },
        getAction: function getAction() {
            var _this2 = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee2() {
                var id, response;
                return _regenerator2.default.wrap(function _callee2$(_context2) {
                    while (1) {
                        switch (_context2.prev = _context2.next) {
                            case 0:
                                if (!_this2.$route.params.id) {
                                    _context2.next = 7;
                                    break;
                                }

                                id = _this2.$route.params.id;
                                _context2.next = 4;
                                return _http2.default.get("/api/role/getallactions?roleid=" + id).catch(function (e) {
                                    alert('ex found:' + e);
                                });

                            case 4:
                                response = _context2.sent;

                                console.log(response.data);
                                _this2.actionss = response.data;

                            case 7:
                            case "end":
                                return _context2.stop();
                        }
                    }
                }, _callee2, _this2);
            }))();
        },
        assignPermission: function assignPermission() {
            var _this3 = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee3() {
                var id, data, result;
                return _regenerator2.default.wrap(function _callee3$(_context3) {
                    while (1) {
                        switch (_context3.prev = _context3.next) {
                            case 0:
                                if (!_this3.$route.params.id) {
                                    _context3.next = 10;
                                    break;
                                }

                                id = _this3.$route.params.id;
                                data = _this3.actionss;

                                console.log(data);
                                result = void 0;
                                _context3.next = 7;
                                return _roleRepository2.default.assignPermissionRole(id, data);

                            case 7:
                                result = _context3.sent;

                                console.log(result);
                                if (result.success == true) {
                                    _this3.$toast.success("tạo thành công", {});
                                    _this3.isLoading = false;
                                    _this3.$router.go(-1);
                                } else {
                                    _this3.$router.go(-1);
                                    _this3.$toast.error("cập nhật thất bại", {});
                                    _this3.isLoading = false;
                                }

                            case 10:
                            case "end":
                                return _context3.stop();
                        }
                    }
                }, _callee3, _this3);
            }))();
        }
    }),
    created: function created() {
        var _this4 = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee4() {
            return _regenerator2.default.wrap(function _callee4$(_context4) {
                while (1) {
                    switch (_context4.prev = _context4.next) {
                        case 0:
                            _context4.next = 2;
                            return _this4.getAction();

                        case 2:
                            _context4.next = 4;
                            return _this4.getFunction();

                        case 4:
                        case "end":
                            return _context4.stop();
                    }
                }
            }, _callee4, _this4);
        }))();
    }
};

/***/ }),

/***/ 1417:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, ".headerRow[data-v-4ebcd6fe]{padding:.75rem;background-color:#000;color:#fff;font-weight:700;vertical-align:bottom;border-bottom:2px solid #dee2e6}.bodyRow[data-v-4ebcd6fe]{padding:.75rem;border-top:1px solid #dee2e6}", ""]);

// exports


/***/ }),

/***/ 1497:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', [_c('b-row', [_c('b-col', [_c('b-container', {
    staticClass: "col-12"
  }, [_c('div', {
    attrs: {
      "id": "permissionsTable"
    }
  }, [_c('b-row', {
    staticClass: "headerRow"
  }, [_c('b-col', {
    attrs: {
      "cols": "3"
    }
  }, [_vm._v("Danh mục chức năng")]), _vm._v(" "), _vm._l((_vm.actionss), function(itemF) {
    return _c('b-col', {
      key: itemF.name
    }, [_vm._v(_vm._s(itemF.name))])
  })], 2), _vm._v(" "), _vm._l((_vm.functions), function(itemA) {
    return _c('b-row', {
      key: itemA.name,
      staticClass: "bodyRow"
    }, [(itemA.parentId == null) ? _c('b-col', {
      attrs: {
        "cols": "3"
      }
    }, [_vm._v(_vm._s(itemA.name))]) : _vm._e(), _vm._v(" "), (itemA.parentId != null) ? _c('b-col', {
      attrs: {
        "cols": "3"
      }
    }, [_vm._v("-- " + _vm._s(itemA.name))]) : _vm._e(), _vm._v(" "), _vm._l((_vm.actionss), function(itemF) {
      return _c('b-col', {
        key: itemF.name
      }, [_c('b-form-checkbox-group', {
        attrs: {
          "id": itemF.name,
          "name": itemF.name + 'Danh mục chức năng'
        },
        model: {
          value: (itemF.functions),
          callback: function($$v) {
            _vm.$set(itemF, "functions", $$v)
          },
          expression: "itemF.functions"
        }
      }, [_c('b-form-checkbox', {
        attrs: {
          "value": itemA.id
        }
      })], 1)], 1)
    })], 2)
  })], 2), _vm._v(" "), _c('div', {
    staticClass: "mt-3"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form btncus float-right",
    attrs: {
      "type": "button"
    },
    on: {
      "click": function($event) {
        return _vm.assignPermission()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                            ")])])])])])], 1)], 1)], 1)
},staticRenderFns: []}

/***/ }),

/***/ 1563:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1417);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("138bd7bf", content, true);

/***/ }),

/***/ 761:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1563)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1189),
  /* template */
  __webpack_require__(1497),
  /* scopeId */
  "data-v-4ebcd6fe",
  /* cssModules */
  null
)

module.exports = Component.exports


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
//# sourceMappingURL=55.js.map
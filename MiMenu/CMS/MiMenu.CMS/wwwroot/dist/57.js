webpackJsonp([57],{

/***/ 1195:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _regenerator = __webpack_require__(75);

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(74);

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

__webpack_require__(772);

var _constant = __webpack_require__(774);

var _constant2 = _interopRequireDefault(_constant);

var _vuex = __webpack_require__(176);

var _vueLoadingOverlay = __webpack_require__(373);

var _vueLoadingOverlay2 = _interopRequireDefault(_vueLoadingOverlay);

var _authenticationRepository = __webpack_require__(114);

var _router = __webpack_require__(185);

var _manufacturerRepository = __webpack_require__(998);

var _manufacturerRepository2 = _interopRequireDefault(_manufacturerRepository);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "EditUser",
    data: function data() {
        return {
            editorConfig: {
                allowedContent: true,
                extraPlugins: ""

            },
            disabled: false,
            isLoading: false,
            fullPage: false,
            color: "#007bff",
            objRequest: {},
            currentUser: null
        };
    },
    created: function created() {
        var _this = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee() {
            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            _context.next = 2;
                            return _this.GetById();

                        case 2:
                        case "end":
                            return _context.stop();
                    }
                }
            }, _callee, _this);
        }))();
    },

    components: {
        Loading: _vueLoadingOverlay2.default
    },

    mounted: function mounted() {},


    computed: {},
    watch: {},
    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["addUser", "getById", "updateUser"]), {
        GetById: function GetById() {
            var _this2 = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee2() {
                var id, response;
                return _regenerator2.default.wrap(function _callee2$(_context2) {
                    while (1) {
                        switch (_context2.prev = _context2.next) {
                            case 0:
                                if (!_this2.$route.params.id) {
                                    _context2.next = 8;
                                    break;
                                }

                                id = _this2.$route.params.id;
                                _context2.next = 4;
                                return _this2.getById(id);

                            case 4:
                                response = _context2.sent;

                                _this2.objRequest = response;
                                _this2.objRequest.Id = response.id;
                                _this2.isLoading = false;

                            case 8:
                            case "end":
                                return _context2.stop();
                        }
                    }
                }, _callee2, _this2);
            }))();
        },
        DoAddEdit: function DoAddEdit() {
            var _this3 = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee3() {
                var result;
                return _regenerator2.default.wrap(function _callee3$(_context3) {
                    while (1) {
                        switch (_context3.prev = _context3.next) {
                            case 0:
                                _this3.isLoading = true;

                                if (!_this3.objRequest.id) {
                                    _context3.next = 9;
                                    break;
                                }

                                _context3.next = 4;
                                return _this3.updateUser(_this3.objRequest);

                            case 4:
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
                                _context3.next = 14;
                                break;

                            case 9:
                                _context3.next = 11;
                                return _this3.addUser(_this3.objRequest);

                            case 11:
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

                            case 14:
                            case "end":
                                return _context3.stop();
                        }
                    }
                }, _callee3, _this3);
            }))();
        },
        DoRefesh: function DoRefesh() {
            this.objRequest.Title = "";
        }
    })
};

/***/ }),

/***/ 1504:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%"
    }
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
  }), _vm._v(" "), _c('b-tabs', {
    staticClass: "col-md-12",
    attrs: {
      "pills": ""
    }
  }, [_c('b-tab', {
    attrs: {
      "title": "1. Nhập thông tin tài khoản",
      "active": ""
    }
  }, [_c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('b-card', {
    staticClass: "mt-3 ",
    attrs: {
      "header": "Cập nhật tài khoản"
    }
  }, [_c('b-form', {
    staticClass: "form-horizontal"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Email"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Email"
    },
    model: {
      value: (_vm.objRequest.email),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "email", $$v)
      },
      expression: "objRequest.email"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Tên người dùng"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Tên người dùng"
    },
    model: {
      value: (_vm.objRequest.userName),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "userName", $$v)
      },
      expression: "objRequest.userName"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Tên đầy đủ"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Tên đầy đủ"
    },
    model: {
      value: (_vm.objRequest.fullName),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "fullName", $$v)
      },
      expression: "objRequest.fullName"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Địa chỉ"
    }
  }, [_c('b-form-input', {
    attrs: {
      "placeholder": "Địa chỉ"
    },
    model: {
      value: (_vm.objRequest.address),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "address", $$v)
      },
      expression: "objRequest.address"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "mt-3"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-4"
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form col-md-12 btncus",
    attrs: {
      "type": "button"
    },
    on: {
      "click": function($event) {
        return _vm.DoAddEdit()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                                                    ")])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-4"
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
  }), _vm._v(" Làm mới\n                                                    ")])])])])])])])])], 1)], 1)])])], 1)], 1)
},staticRenderFns: []}

/***/ }),

/***/ 767:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1195),
  /* template */
  __webpack_require__(1504),
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

/***/ 998:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _regenerator = __webpack_require__(75);

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = __webpack_require__(74);

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

var _http = __webpack_require__(4);

var _http2 = _interopRequireDefault(_http);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var manufacturersRepository = {
    getPageManufacturers: function getPageManufacturers(data) {
        var _this = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee() {
            var response;
            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
                            _context.next = 2;
                            return _http2.default.get('/api/Manufacturer/Get?pageIndex=' + data.pageIndex + '&pageSize=' + data.pageSize + '&keyword=' + data.title, {}).catch(function (e) {
                                alert('ex found:' + e);
                            });

                        case 2:
                            response = _context.sent;

                            console.log(response);
                            return _context.abrupt('return', response.data);

                        case 5:
                        case 'end':
                            return _context.stop();
                    }
                }
            }, _callee, _this);
        }))();
    },
    addManufacturers: function addManufacturers(data) {
        var _this2 = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee2() {
            var response;
            return _regenerator2.default.wrap(function _callee2$(_context2) {
                while (1) {
                    switch (_context2.prev = _context2.next) {
                        case 0:
                            _context2.next = 2;
                            return _http2.default.post('/api/Manufacturer/Add', data).catch(function (e) {
                                alert('ex found:' + e);
                            });

                        case 2:
                            response = _context2.sent;

                            console.log(response.data);
                            return _context2.abrupt('return', response.data);

                        case 5:
                        case 'end':
                            return _context2.stop();
                    }
                }
            }, _callee2, _this2);
        }))();
    },
    updateManufacturers: function updateManufacturers(data) {
        var _this3 = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee3() {
            var response;
            return _regenerator2.default.wrap(function _callee3$(_context3) {
                while (1) {
                    switch (_context3.prev = _context3.next) {
                        case 0:
                            _context3.next = 2;
                            return _http2.default.put('/api/Manufacturer/Update', data).catch(function (e) {
                                alert('ex found:' + e);
                            });

                        case 2:
                            response = _context3.sent;

                            console.log(response.data);
                            return _context3.abrupt('return', response.data);

                        case 5:
                        case 'end':
                            return _context3.stop();
                    }
                }
            }, _callee3, _this3);
        }))();
    },
    getManufacturersById: function getManufacturersById(id) {
        var _this4 = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee4() {
            var response;
            return _regenerator2.default.wrap(function _callee4$(_context4) {
                while (1) {
                    switch (_context4.prev = _context4.next) {
                        case 0:
                            _context4.next = 2;
                            return _http2.default.get('/api/Manufacturer/GetById?id=' + id).catch(function (e) {
                                alert('ex found:' + e);
                            });

                        case 2:
                            response = _context4.sent;
                            return _context4.abrupt('return', response.data);

                        case 4:
                        case 'end':
                            return _context4.stop();
                    }
                }
            }, _callee4, _this4);
        }))();
    },
    deleteManufacturers: function deleteManufacturers(data) {
        return _http2.default.post('/api/Manufacturer/UnPublish?id=' + data).then(function (response) {
            console.log(response.data);
            return response.data;
        }).catch(function (e) {
            alert('ex found:' + e);
        });
    },
    getAllLanguageOption: function getAllLanguageOption() {
        var _this5 = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee5() {
            var response;
            return _regenerator2.default.wrap(function _callee5$(_context5) {
                while (1) {
                    switch (_context5.prev = _context5.next) {
                        case 0:
                            _context5.next = 2;
                            return _http2.default.get('/api/Common/GetAllLanguageOptions').catch(function (e) {
                                alert('ex found:' + e);
                            });

                        case 2:
                            response = _context5.sent;
                            return _context5.abrupt('return', response.data);

                        case 4:
                        case 'end':
                            return _context5.stop();
                    }
                }
            }, _callee5, _this5);
        }))();
    }
};

exports.default = manufacturersRepository;

/***/ })

});
//# sourceMappingURL=57.js.map
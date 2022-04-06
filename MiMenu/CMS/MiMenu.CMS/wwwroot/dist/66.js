webpackJsonp([66],{

/***/ 1182:
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

var _vuex = __webpack_require__(176);

var _vueLoadingOverlay = __webpack_require__(373);

var _vueLoadingOverlay2 = _interopRequireDefault(_vueLoadingOverlay);

var _authenticationRepository = __webpack_require__(114);

var _router = __webpack_require__(185);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "Profile",
    data: function data() {
        return {
            disabled: false,
            isLoading: false,
            fullPage: false,
            color: "#007bff",
            objRequest: {},
            currentUser: null,
            errorMessage: '',
            activeColor: 'red'
        };
    },
    created: function created() {
        var _this = this;

        return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee() {
            return _regenerator2.default.wrap(function _callee$(_context) {
                while (1) {
                    switch (_context.prev = _context.next) {
                        case 0:
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
    watch: {},
    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["changePassword"]), {
        DoAddEdit: function DoAddEdit() {
            var _this2 = this;

            return (0, _asyncToGenerator3.default)(_regenerator2.default.mark(function _callee2() {
                var currentUser, result;
                return _regenerator2.default.wrap(function _callee2$(_context2) {
                    while (1) {
                        switch (_context2.prev = _context2.next) {
                            case 0:
                                _this2.isLoading = true;
                                currentUser = _authenticationRepository.authenticationRepository.currentUserValue;

                                _this2.objRequest.userId = currentUser.id;

                                if (!_this2.objRequest.userId) {
                                    _context2.next = 9;
                                    break;
                                }

                                _context2.next = 6;
                                return _this2.changePassword(_this2.objRequest);

                            case 6:
                                result = _context2.sent;

                                console.log(result);
                                if (result.success == true) {
                                    _authenticationRepository.authenticationRepository.logout();
                                    _this2.$toast.success("cập nhật thành công", {});
                                    _this2.isLoading = false;
                                    _this2.$router.go(-1);
                                } else {
                                    _this2.$router.go(-1);
                                    _this2.$toast.error("cập nhật thất bại", {});
                                    _this2.isLoading = false;
                                }

                            case 9:
                            case "end":
                                return _context2.stop();
                        }
                    }
                }, _callee2, _this2);
            }))();
        },
        DoRefesh: function DoRefesh() {
            this.objRequest.Title = "";
        },
        GetRouterChangePassword: function GetRouterChangePassword() {
            _router.router.push('/admin/profile/change-password');
        }
    })
};

/***/ }),

/***/ 1493:
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
      "title": "Thay đổi mật khẩu",
      "active": ""
    }
  }, [_c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('b-card', {
    staticClass: "mt-3 ",
    attrs: {
      "header": "Cập nhật mật khẩu"
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
      "label": "Mật khẩu hiện tại"
    }
  }, [_c('b-form-input', {
    attrs: {
      "type": "password",
      "placeholder": "Mật khẩu hiện tại"
    },
    model: {
      value: (_vm.objRequest.oldPassword),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "oldPassword", $$v)
      },
      expression: "objRequest.oldPassword"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Mật khẩu mới"
    }
  }, [_c('b-form-input', {
    attrs: {
      "type": "password",
      "placeholder": "Mật khẩu mới"
    },
    model: {
      value: (_vm.objRequest.password),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "password", $$v)
      },
      expression: "objRequest.password"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Nhập lại mật khẩu mới"
    }
  }, [_c('b-form-input', {
    attrs: {
      "type": "password",
      "placeholder": "Nhập lại mật khẩu mới"
    },
    model: {
      value: (_vm.objRequest.confirmPassword),
      callback: function($$v) {
        _vm.$set(_vm.objRequest, "confirmPassword", $$v)
      },
      expression: "objRequest.confirmPassword"
    }
  }), _vm._v(" "), (_vm.objRequest.password != _vm.objRequest.confirmPassword) ? _c('p', {
    style: ({
      color: _vm.activeColor
    })
  }, [_vm._v("Bạn nhập\n                                                không khớp mật khẩu")]) : _c('p')], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-12"
  }, [_c('div', {
    staticClass: "mt-3"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-12"
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form col-md-12 btncus",
    attrs: {
      "disabled": _vm.objRequest.password != _vm.objRequest.confirmPassword,
      "type": "button"
    },
    on: {
      "click": function($event) {
        return _vm.DoAddEdit()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                                                    ")])])])])])])])])], 1)], 1)])])], 1)], 1)
},staticRenderFns: []}

/***/ }),

/***/ 754:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1182),
  /* template */
  __webpack_require__(1493),
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


/***/ })

});
//# sourceMappingURL=66.js.map
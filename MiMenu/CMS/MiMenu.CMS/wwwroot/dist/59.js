webpackJsonp([59],{

/***/ 1192:
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
    name: "TagEdit",
    data: function data() {
        return {

            isLoading: false,
            fullPage: false,
            color: "#007bff",
            tagObj: {
                Id: 0,
                ParentId: 0,
                Name: "",
                Description: "",
                Invisibled: false,
                IsHotTag: false,
                Type: 0
            },
            Types: [{ value: 0, text: "Chọn loại" }, { value: 1, text: "Loại thiết bị" }, { value: 2, text: "Loại sàn gỗ" }]
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
            this.getTag(this.$route.params.id).then(function (respose) {
                _this.tagObj = respose.Data;
            });
            this.isLoading = false;
        }
    },


    computed: {},

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["addTag", "getTag", "editTag"]), {
        DoAddEdit: function DoAddEdit() {
            var _this2 = this;

            this.isLoading = true;
            var fromData = new FormData();

            if (this.tagObj.Id > 0) {
                this.editTag(this.tagObj).then(function (response) {
                    if (response.Success == true) {
                        _this2.$toast.success(response.Message, {});
                        _this2.isLoading = false;
                    } else {
                        _this2.$toast.error(response.Message, {});
                        _this2.isLoading = false;
                    }
                }).catch(function (e) {
                    _this2.$toast.error(_constant2.default.error + ". Error:" + e, {});
                    _this2.isLoading = false;
                });
            } else {
                console.log("Goi vao api");
                console.log(this.tagObj);

                this.addTag(this.tagObj).then(function (response) {
                    if (response.Success == true) {
                        _this2.$toast.success(response.Message, {});
                        _this2.isLoading = false;
                    } else {
                        _this2.$toast.error(response.Message, {});
                        _this2.isLoading = false;
                    }
                }).catch(function (e) {
                    _this2.$toast.error(_constant2.default.error + ". Error:" + e, {});
                    _this2.isLoading = false;
                });
            }
        },
        openUpload: function openUpload() {
            document.getElementById('file-field').click();
        },
        updatePreview: function updatePreview(e) {
            var _this3 = this;

            console.log('e', e);
            var reader,
                files = e.target.files;
            if (files.length === 0) {
                console.log('Empty');
            }
            this.selectedFile = files[0];
            reader = new FileReader();
            reader.onload = function (e) {
                _this3.preview = e.target.result;

                console.log(_this3.selectedFile);
            };
            reader.readAsDataURL(files[0]);
        }
    })
};

/***/ }),

/***/ 1516:
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
      "width": 35
    },
    on: {
      "update:active": function($event) {
        _vm.isLoading = $event
      }
    }
  }), _vm._v(" "), _c('div', {
    staticClass: "col-md-8"
  }, [_c('b-card', {
    staticClass: "mt-3",
    attrs: {
      "header": "Thêm / Sửa Tag"
    }
  }, [_c('b-form', {
    staticClass: "form-horizontal"
  }, [_c('b-form-group', {
    attrs: {
      "label": "Tên Tag:",
      "label-for": "input-1"
    }
  }, [_c('b-form-input', {
    attrs: {
      "id": "input-1",
      "type": "text",
      "required": "",
      "placeholder": "Tên Tag"
    },
    model: {
      value: (_vm.tagObj.Name),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "Name", $$v)
      },
      expression: "tagObj.Name"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Url"
    }
  }, [_c('b-form-input', {
    attrs: {
      "type": "text",
      "required": "",
      "placeholder": "Url"
    },
    model: {
      value: (_vm.tagObj.Url),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "Url", $$v)
      },
      expression: "tagObj.Url"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Loại"
    }
  }, [_c('b-form-select', {
    attrs: {
      "options": _vm.Types,
      "required": ""
    },
    model: {
      value: (_vm.tagObj.Type),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "Type", $$v)
      },
      expression: "tagObj.Type"
    }
  })], 1), _vm._v(" "), _c('b-form-group', {
    attrs: {
      "label": "Mô tả"
    }
  }, [_c('b-form-textarea', {
    attrs: {
      "type": "text",
      "required": "",
      "placeholder": "Mô tả"
    },
    model: {
      value: (_vm.tagObj.Description),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "Description", $$v)
      },
      expression: "tagObj.Description"
    }
  })], 1)], 1)], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-4"
  }, [_c('div', {
    staticClass: "mt-3",
    staticStyle: {
      "position": "fixed",
      "width": "25%"
    }
  }, [_c('b-card', {
    attrs: {
      "header": "Đăng bài"
    }
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-6"
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
  }), _vm._v(" Cập nhật\n                        ")])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-6"
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
  }), _vm._v(" Làm mới\n                        ")])])]), _vm._v(" "), _c('div', {
    staticClass: "row",
    staticStyle: {
      "margin-top": "20px"
    }
  }, [_c('div', {
    staticClass: "col-md-6 col-xs-12"
  }, [_c('b-form-checkbox', {
    model: {
      value: (_vm.tagObj.Invisibled),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "Invisibled", $$v)
      },
      expression: "tagObj.Invisibled"
    }
  }, [_vm._v("\n                            Hiển thị\n                        ")])], 1), _vm._v(" "), _c('div', {
    staticClass: "col-md-6 col-xs-12"
  }, [_c('b-form-checkbox', {
    model: {
      value: (_vm.tagObj.IsHotTag),
      callback: function($$v) {
        _vm.$set(_vm.tagObj, "IsHotTag", $$v)
      },
      expression: "tagObj.IsHotTag"
    }
  }, [_vm._v("\n                            Tag Hot\n                        ")])], 1)])])], 1)])], 1)
},staticRenderFns: []}

/***/ }),

/***/ 764:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1192),
  /* template */
  __webpack_require__(1516),
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

/***/ })

});
//# sourceMappingURL=59.js.map
webpackJsonp([61],{

/***/ 1185:
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
    name: "product",
    components: {
        Loading: _vueLoadingOverlay2.default
    },
    data: function data() {
        return {
            isLoading: false,
            _product: {},
            messeger: "",
            currentSort: "Id",
            currentSortDir: "asc",

            currentPage: 1,
            pageSize: 10,
            loading: true,

            SearchKeyword: "",
            SearchLanguageCode: "vi-VN     ",
            Language: [],
            searchStatus: 0,
            ListStatus: [{
                Id: 0,
                Text: "Tất cả"
            }, {
                Id: 1,
                Text: "Xuất bản"
            }, {
                Id: 2,
                Text: "Ẩn"
            }, {
                Id: 3,
                Text: "Đã xóa"
            }],

            bootstrapPaginationClasses: {
                ul: "pagination",
                li: "page-item",
                liActive: "active",
                liDisable: "disabled",
                button: "page-link"
            },
            customLabels: {
                first: "First",
                prev: "Previous",
                next: "Next",
                last: "Last"
            }
        };
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getPromotions", "deletePromotion", "getAllLanguages"]), {
        onChangePaging: function onChangePaging() {
            this.isLoading = true;
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getPromotions({
                keyword: this.SearchKeyword,
                languageCode: this.SearchLanguageCode || "vi-VN",
                pageIndex: this.currentPage,
                pageSize: this.pageSize,
                sortBy: this.currentSort,
                sortDir: this.currentSortDir,
                status: this.searchStatus
            });
            this.isLoading = false;
        },

        sortor: function sortor(s) {
            if (s === this.currentSort) {
                this.currentSortDir = this.currentSortDir === "asc" ? "desc" : "asc";
            }
            this.currentSort = s;
            this.onChangePaging();
        },
        remove: function remove(item, status) {
            var _this = this;

            item.status = status;
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.deletePromotion(item).then(function (response) {
                if (response.key == true) {
                    _this.$toast.success(response.value, {});
                    _this.onChangePaging();
                    _this.isLoading = false;
                } else {
                    _this.$toast.error(response.value, {});
                    _this.isLoading = false;
                }
            }).catch(function (e) {
                _this.$toast.error(_constant2.default.error + ". Error:" + e, {});
            });
        }
    }),
    computed: (0, _extends3.default)({}, (0, _vuex.mapGetters)(["promotions"])),
    mounted: function mounted() {
        this.onChangePaging();
    },

    watch: {
        currentPage: function currentPage(newVal) {
            this.currentPage = newVal;
            this.onChangePaging();
        },
        SearchLanguageCode: function SearchLanguageCode() {
            this.currentPage = 1;
            this.onChangePaging();
        },
        searchStatus: function searchStatus() {
            this.currentPage = 1;
            this.onChangePaging();
        }
    }
};

/***/ }),

/***/ 1524:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "list-data"
  }, [_c('b-card', {
    staticClass: "card-filter",
    attrs: {
      "header-tag": "header",
      "footer-tag": "footer"
    }
  }, [_c('div', [_c('b-col', {
    attrs: {
      "md": "12"
    }
  }, [_c('b-row', {
    staticClass: "form-group"
  }, [_c('b-col', {
    attrs: {
      "md": "4"
    }
  }, [_c('b-form-input', {
    attrs: {
      "type": "text",
      "placeholder": "Tìm kiếm theo tên"
    },
    on: {
      "keyup": function($event) {
        if (!$event.type.indexOf('key') && $event.keyCode !== 13) { return null; }
        return _vm.onChangePaging()
      }
    },
    model: {
      value: (_vm.SearchKeyword),
      callback: function($$v) {
        _vm.SearchKeyword = $$v
      },
      expression: "SearchKeyword"
    }
  })], 1), _vm._v(" "), _c('b-col', {
    attrs: {
      "md": "2"
    }
  }, [_c('select', {
    directives: [{
      name: "model",
      rawName: "v-model",
      value: (_vm.searchStatus),
      expression: "searchStatus"
    }],
    staticClass: "form-control",
    on: {
      "change": function($event) {
        var $$selectedVal = Array.prototype.filter.call($event.target.options, function(o) {
          return o.selected
        }).map(function(o) {
          var val = "_value" in o ? o._value : o.value;
          return val
        });
        _vm.searchStatus = $event.target.multiple ? $$selectedVal : $$selectedVal[0]
      }
    }
  }, _vm._l((_vm.ListStatus), function(item) {
    return _c('option', {
      domProps: {
        "value": item.Id
      }
    }, [_vm._v("\n                                " + _vm._s(item.Text) + "\n                            ")])
  }), 0)]), _vm._v(" "), _c('b-col', {
    attrs: {
      "md": "1"
    }
  }, [_c('b-btn', {
    staticClass: "col-lg-12",
    attrs: {
      "variant": "info"
    }
  }, [_c('i', {
    staticClass: "fa fa-search",
    attrs: {
      "aria-hidden": "true"
    }
  })])], 1), _vm._v(" "), _c('b-col', {
    attrs: {
      "md": "1"
    }
  }, [_c('b-btn', {
    staticClass: "col-lg-12",
    attrs: {
      "variant": "primary"
    }
  }, [_c('i', {
    staticClass: "fa fa-refresh",
    attrs: {
      "aria-hidden": "true"
    }
  })])], 1), _vm._v(" "), _c('b-col', {
    attrs: {
      "md": "2"
    }
  }, [_c('b-btn', {
    directives: [{
      name: "b-toggle",
      rawName: "v-b-toggle.collapse1",
      modifiers: {
        "collapse1": true
      }
    }],
    attrs: {
      "variant": "primary"
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-down",
    attrs: {
      "aria-hidden": "true"
    }
  })])], 1)], 1)], 1)], 1)]), _vm._v(" "), _c('div', {
    staticClass: "card card-data"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: " mb-2",
    attrs: {
      "role": "toolbar",
      "aria-label": "Toolbar with button groups and dropdown menu"
    }
  }, [_c('div', {
    staticClass: "mx-1 btn-group",
    attrs: {
      "role": "group"
    }
  }, [_c('router-link', {
    staticClass: "btn btn-success",
    attrs: {
      "to": {
        path: 'add'
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-plus"
  }), _vm._v(" Thêm mới")]), _vm._v(" "), _vm._m(0)], 1), _vm._v(" "), _c('b-dropdown', {
    staticClass: "mx-1",
    attrs: {
      "variant": "info",
      "right": "",
      "text": "Hành động",
      "icon": ""
    }
  }, [_c('b-dropdown-item', [_vm._v("Kích hoạt")]), _vm._v(" "), _c('b-dropdown-item', [_vm._v("Không kích hoạt")])], 1), _vm._v(" "), _c('div', {
    staticClass: "mx-1 btn-group mi-paging"
  }, [_c('b-pagination', {
    attrs: {
      "total-rows": _vm.promotions.total,
      "per-page": _vm.pageSize,
      "aria-controls": "_product"
    },
    model: {
      value: (_vm.currentPage),
      callback: function($$v) {
        _vm.currentPage = $$v
      },
      expression: "currentPage"
    }
  })], 1)], 1), _vm._v(" "), _c('div', {
    staticClass: "table-responsive"
  }, [_c('div', {
    staticClass: "dataTables_wrapper dt-bootstrap4 no-footer"
  }, [_c('div', {
    staticClass: "clear"
  }), _vm._v(" "), _c('table', {
    staticClass: "table data-thumb-view dataTable no-footer",
    attrs: {
      "role": "grid"
    }
  }, [_vm._m(1), _vm._v(" "), _c('tbody', _vm._l((_vm.promotions.listData), function(item) {
    return _c('tr', {
      staticClass: "odd",
      attrs: {
        "role": "row"
      }
    }, [_vm._m(2, true), _vm._v(" "), _c('td', [_c('p', [_vm._v(_vm._s(item.name))])]), _vm._v(" "), _c('td', {
      staticClass: "product-type sorting_1"
    }, [_vm._v(_vm._s(item.typeName))]), _vm._v(" "), _c('td', {
      staticClass: "product-isDiscountPrice"
    }, [(item.isDiscountPrice == true) ? [_vm._v("\n                                        Trừ tiền mặt\n                                    ")] : _vm._e(), _vm._v(" "), (item.isDiscountPrice == false) ? [_vm._v("\n                                        Không trừ tiền mặt\n                                    ")] : _vm._e()], 2), _vm._v(" "), _c('td', {
      staticClass: "product-value"
    }, [_vm._v(_vm._s(item.value))]), _vm._v(" "), _c('td', [_c('p', [_vm._v("Ngôn ngữ: " + _vm._s(item.langCount))])]), _vm._v(" "), _c('td', [_c('p', [_vm._v("NBĐ: " + _vm._s(item.startDate))]), _vm._v(" "), _c('p', [_vm._v("NKT: " + _vm._s(item.endDate))])]), _vm._v(" "), _c('td', {
      staticClass: "text-center"
    }, [(item.status == 2) ? _c('span', {
      staticClass: "badge bg-warning"
    }, [_vm._v("Chưa xuất bản")]) : _vm._e(), _vm._v(" "), (item.status == 1) ? _c('span', {
      staticClass: "badge bg-success"
    }, [_vm._v("Xuất bản")]) : _vm._e(), _vm._v(" "), (item.status == 3) ? _c('span', {
      staticClass: "badge bg-danger"
    }, [_vm._v("Đã xóa")]) : _vm._e()]), _vm._v(" "), _c('td', {
      staticClass: "product-action"
    }, [_c('router-link', {
      attrs: {
        "to": {
          path: 'edit/' + item.id
        }
      }
    }, [_c('span', {
      staticClass: "action-edit"
    }, [_c('i', {
      staticClass: "fa fa-edit"
    })])]), _vm._v(" "), (item.status == 2) ? _c('span', {
      staticClass: "action-show"
    }, [_c('a', {
      directives: [{
        name: "b-tooltip",
        rawName: "v-b-tooltip.hover",
        modifiers: {
          "hover": true
        }
      }],
      attrs: {
        "title": "Xuất bản"
      },
      on: {
        "click": function($event) {
          return _vm.remove(item, 1)
        }
      }
    }, [_c('i', {
      staticClass: "fa fa-check-circle",
      staticStyle: {
        "color": "green"
      }
    })])]) : _vm._e(), _vm._v(" "), (item.status == 1) ? _c('span', {
      staticClass: "action-hidden"
    }, [_c('a', {
      directives: [{
        name: "b-tooltip",
        rawName: "v-b-tooltip.hover",
        modifiers: {
          "hover": true
        }
      }],
      attrs: {
        "title": "Ẩn"
      },
      on: {
        "click": function($event) {
          return _vm.remove(item, 2)
        }
      }
    }, [_c('i', {
      staticClass: "fa fa-circle-o",
      staticStyle: {
        "color": "gold"
      }
    })])]) : _vm._e(), _vm._v(" "), _c('span', {
      staticClass: "action-delete"
    }, [_c('a', {
      directives: [{
        name: "b-tooltip",
        rawName: "v-b-tooltip.hover",
        modifiers: {
          "hover": true
        }
      }],
      attrs: {
        "title": "Xóa"
      },
      on: {
        "click": function($event) {
          return _vm.remove(item, 3)
        }
      }
    }, [_c('i', {
      staticClass: "fa fa-trash",
      staticStyle: {
        "color": "red"
      }
    })])])], 1)])
  }), 0)])])])])])], 1)
},staticRenderFns: [function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('button', {
    staticClass: "btn btn-danger",
    attrs: {
      "type": "button"
    }
  }, [_c('i', {
    staticClass: "fa fa-trash-o"
  }), _vm._v(" Xóa")])
},function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('thead', {
    staticClass: "thead-dark table table-centered table-nowrap"
  }, [_c('tr', {
    attrs: {
      "role": "row"
    }
  }, [_c('th'), _vm._v(" "), _c('th', [_vm._v("Tên khuyến mãi")]), _vm._v(" "), _c('th', [_vm._v("Loại khuyến mãi")]), _vm._v(" "), _c('th', [_vm._v("Trừ tiền")]), _vm._v(" "), _c('th', [_vm._v("Giá")]), _vm._v(" "), _c('th', [_vm._v("Ngôn ngữ")]), _vm._v(" "), _c('th', [_vm._v("Ngày tháng")]), _vm._v(" "), _c('th', [_vm._v("Trạng thái")]), _vm._v(" "), _c('th', [_vm._v("Thao tác")])])])
},function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('td', {
    staticClass: "dt-checkboxes-cell"
  }, [_c('input', {
    staticClass: "dt-checkboxes",
    attrs: {
      "type": "checkbox"
    }
  })])
}]}

/***/ }),

/***/ 757:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1185),
  /* template */
  __webpack_require__(1524),
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
//# sourceMappingURL=61.js.map
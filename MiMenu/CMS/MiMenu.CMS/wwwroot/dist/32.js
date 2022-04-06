webpackJsonp([32],{

/***/ 1176:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _toConsumableArray2 = __webpack_require__(996);

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _assign = __webpack_require__(374);

var _assign2 = _interopRequireDefault(_assign);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

var _vueTreeselect = __webpack_require__(925);

var _vueTreeselect2 = _interopRequireDefault(_vueTreeselect);

var _helper = __webpack_require__(926);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "comboproduct",
    components: {
        Treeselect: _vueTreeselect2.default
    },
    props: {
        productId: {
            type: Number,
            required: false,
            default: 0
        },
        actions: {
            type: Boolean,
            required: false,
            default: false
        },
        category: {
            type: Array
        }
    },
    data: function data() {
        return {
            ListProduct: [],
            ListValue: [],
            keyword: "",
            activeLeft: [],
            activeRight: [],
            searchZoneId: 0,
            pageSize: 20,
            currentPage: 1,
            total: 0
        };
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getProductForCombo", "addCombo", "getComboById"]), {
        pathImgs: function pathImgs(path) {
            return (0, _helper.pathImg)(path);
        },
        toggleActive: function toggleActive(item) {
            if (this.activeItem[item.id]) {
                this.removeActiveItem(item);

                return;
            }

            this.addActiveItem(item);
        },
        addActiveItem: function addActiveItem(item) {
            this.activeItem = (0, _assign2.default)({}, this.activeItem ? [item.id] : item);
        },
        removeActiveItem: function removeActiveItem(item) {
            delete this.activeItem[item.id];
            this.activeItem = (0, _assign2.default)({}, this.activeItem);
        },
        onLoadProduct: function onLoadProduct() {
            var _this = this;

            this.activeLeft = [];
            this.activeRight = [];
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getProductForCombo({
                keyword: this.keyword,
                idZone: this.searchZoneId || 0,
                pageSize: this.pageSize,
                pageIndex: this.currentPage

            }).then(function (respose) {
                _this.ListProduct = respose.listData.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
                _this.total = respose.total;
            });
        },
        onLoadProductCombo: function onLoadProductCombo() {
            var _this2 = this;

            this.activeLeft = [];
            this.activeRight = [];
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getComboById(this.productId).then(function (respose) {
                _this2.ListValue = respose.listData.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
            });
        },

        Add: function Add() {
            var _this3 = this;

            this.addCombo({
                Id: this.productId,
                Type: "com-bo",
                Combos: this.ListValue
            }).then(function (response) {
                if (response.success == true) {
                    _this3.$toast.success(response.message, {});
                } else {
                    _this3.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this3.$toast.error("Lỗi hệ thống");
            });
        },
        alltoRight: function alltoRight() {
            this.activeLeft = 0;
            for (var i = 0; i < this.ListLocation.length; i++) {
                this.ListValue.push(this.ListLocation[i]);
            }

            this.ListLocation = [];
        },
        onetoRight: function onetoRight() {
            var lstChose = this.ListProduct.filter(function (x) {
                return x.active == true;
            });
            this.ListProduct = this.ListProduct.map(function (item) {
                return (0, _extends3.default)({}, item, {
                    active: false
                });
            });
            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(lstChose));
        },
        onetoRemove: function onetoRemove() {
            this.ListValue = this.ListValue.filter(function (x) {
                return x.active != true;
            });
        },

        changeValueLeft: function changeValueLeft(id) {
            if (this.ListProduct[id].active == true) {
                this.ListProduct[id].active = false;
            } else {
                this.ListProduct[id].active = true;
            }
        },
        changeValueRight: function changeValueRight(id) {
            if (this.ListValue[id].active == true) {
                this.ListValue[id].active = false;
            } else {
                this.ListValue[id].active = true;
            }
        },
        ExistId: function ExistId(id) {}
    }),
    watch: {
        actions: function actions(newVal) {
            if (newVal == true) {
                this.onLoadProduct();
                this.onLoadProductCombo();
            }
        },
        currentPage: function currentPage(newVal) {
            this.currentPage = newVal;
            this.onLoadProduct();
        },
        searchZoneId: function searchZoneId() {
            this.currentPage = 1;
            this.onLoadProduct();
        }
    }
};

/***/ }),

/***/ 1177:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _toConsumableArray2 = __webpack_require__(996);

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _assign = __webpack_require__(374);

var _assign2 = _interopRequireDefault(_assign);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

var _vueTreeselect = __webpack_require__(925);

var _vueTreeselect2 = _interopRequireDefault(_vueTreeselect);

var _helper = __webpack_require__(926);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "comboproduct",
    components: {
        Treeselect: _vueTreeselect2.default
    },
    props: {
        productId: {
            type: Number,
            required: false,
            default: 0
        },
        actions: {
            type: Boolean,
            required: false,
            default: false
        },
        category: {
            type: Array
        }
    },
    data: function data() {
        return {
            ListProduct: [],
            ListValue: [],
            keyword: "",
            activeLeft: [],
            activeRight: [],
            searchZoneId: 0,
            pageSize: 20,
            currentPage: 1,
            total: 0
        };
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getProductForCombo", "addCombo", "getComboById"]), {
        pathImgs: function pathImgs(path) {
            return (0, _helper.pathImg)(path);
        },
        toggleActive: function toggleActive(item) {
            if (this.activeItem[item.id]) {
                this.removeActiveItem(item);

                return;
            }

            this.addActiveItem(item);
        },
        addActiveItem: function addActiveItem(item) {
            this.activeItem = (0, _assign2.default)({}, this.activeItem ? [item.id] : item);
        },
        removeActiveItem: function removeActiveItem(item) {
            delete this.activeItem[item.id];
            this.activeItem = (0, _assign2.default)({}, this.activeItem);
        },
        onLoadProduct: function onLoadProduct() {
            var _this = this;

            this.activeLeft = [];
            this.activeRight = [];
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getProductForCombo({
                keyword: this.keyword,
                idZone: this.searchZoneId || 0,
                pageSize: this.pageSize,
                pageIndex: this.currentPage

            }).then(function (respose) {
                _this.ListProduct = respose.listData.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
                _this.total = respose.total;
            });
        },
        onLoadProductCombo: function onLoadProductCombo() {
            var _this2 = this;

            this.activeLeft = [];
            this.activeRight = [];
            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getComboSameById(this.productId).then(function (respose) {
                _this2.ListValue = respose.listData.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
            });
        },

        Add: function Add() {
            var _this3 = this;

            this.addCombo({
                Id: this.productId,
                Type: "same",
                Combos: this.ListValue
            }).then(function (response) {
                if (response.success == true) {
                    _this3.$toast.success(response.message, {});
                } else {
                    _this3.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this3.$toast.error("Lỗi hệ thống");
            });
        },
        alltoRight: function alltoRight() {
            this.activeLeft = 0;
            for (var i = 0; i < this.ListLocation.length; i++) {
                this.ListValue.push(this.ListLocation[i]);
            }

            this.ListLocation = [];
        },
        onetoRight: function onetoRight() {
            var lstChose = this.ListProduct.filter(function (x) {
                return x.active == true;
            });
            this.ListProduct = this.ListProduct.map(function (item) {
                return (0, _extends3.default)({}, item, {
                    active: false
                });
            });
            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(lstChose));
        },
        onetoRemove: function onetoRemove() {
            this.ListValue = this.ListValue.filter(function (x) {
                return x.active != true;
            });
        },

        changeValueLeft: function changeValueLeft(id) {
            if (this.ListProduct[id].active == true) {
                this.ListProduct[id].active = false;
            } else {
                this.ListProduct[id].active = true;
            }
        },
        changeValueRight: function changeValueRight(id) {
            if (this.ListValue[id].active == true) {
                this.ListValue[id].active = false;
            } else {
                this.ListValue[id].active = true;
            }
        },
        ExistId: function ExistId(id) {}
    }),
    watch: {
        actions: function actions(newVal) {
            if (newVal == true) {
                this.onLoadProduct();
                this.onLoadProductCombo();
            }
        },
        currentPage: function currentPage(newVal) {
            this.currentPage = newVal;
            this.onLoadProduct();
        },
        searchZoneId: function searchZoneId() {
            this.currentPage = 1;
            this.onLoadProduct();
        }
    }
};

/***/ }),

/***/ 1178:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _toConsumableArray2 = __webpack_require__(996);

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "locationinproduct",
    components: {},
    props: {
        productId: {
            type: Number,
            required: false,
            default: 0
        },
        actions: {
            type: Boolean,
            required: false,
            default: false
        }
    },
    data: function data() {
        return {

            ListLocation: [],
            ListValue: [],

            activeLeft: 0,
            activeRight: 0
        };
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getPriceByLocations", "updateLocations"]), {
        onLoadLocation: function onLoadLocation(id) {
            var _this = this;

            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getPriceByLocations(id).then(function (respose) {
                _this.ListLocation = respose.listObj1.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
                _this.ListValue = respose.listObj2.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });;
            });
        },


        Add: function Add() {
            var _this2 = this;

            this.updateLocations({
                Id: this.productId,
                PriceInLocation: this.ListValue
            }).then(function (response) {
                if (response.success == true) {
                    _this2.$toast.success(response.message, {});
                } else {
                    _this2.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this2.$toast.error("Lỗi hệ thống");
            });;
        },
        changeValue: function changeValue(item) {
            item.salePrice = item.price - item.price * item.discountPercent / 100;
        },
        alltoRight: function alltoRight() {
            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(this.ListLocation));
            this.ListLocation = [];
        },
        onetoRight: function onetoRight() {
            var lstChose = this.ListLocation.filter(function (x) {
                return x.active == true;
            });
            this.ListLocation = this.ListLocation.filter(function (item) {
                return item.active == false;
            });

            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(lstChose));
        },
        alltoLeft: function alltoLeft() {

            this.ListLocation = [].concat((0, _toConsumableArray3.default)(this.ListLocation), (0, _toConsumableArray3.default)(this.ListValue));
            this.ListValue = [];
        },
        onetoLeft: function onetoLeft() {
            var lstChose = this.ListValue.filter(function (x) {
                return x.active == true;
            });
            this.ListValue = this.ListValue.filter(function (item) {
                return item.active == false;
            });
            this.ListLocation = [].concat((0, _toConsumableArray3.default)(this.ListLocation), (0, _toConsumableArray3.default)(lstChose));
        },
        changeValueLeft: function changeValueLeft(id) {
            if (this.ListLocation[id].active == true) {
                this.ListLocation[id].active = false;
            } else {
                this.ListLocation[id].active = true;
            }
        },
        changeValueRight: function changeValueRight(id) {
            if (this.ListValue[id].active == true) {
                this.ListValue[id].active = false;
            } else {
                this.ListValue[id].active = true;
            }
        },
        filterFunction: function filterFunction() {
            var input, filter, i, a, div;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            div = document.getElementById("list-1");
            a = div.getElementsByTagName("button");
            for (i = 0; i < a.length; i++) {
                var txtValue = a[i].textContent || a[i].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    a[i].style.display = "";
                } else {
                    a[i].style.display = "none";
                }
            }
        }

    }),
    watch: {
        productId: function productId(newVal) {
            if (newVal > 0) {
                this.onLoadLocation(newVal);
            }
        }
    }
};

/***/ }),

/***/ 1179:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _defineProperty2 = __webpack_require__(929);

var _defineProperty3 = _interopRequireDefault(_defineProperty2);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _name$components$data;

__webpack_require__(772);

var _vuex = __webpack_require__(176);

var _vueLoadingOverlay = __webpack_require__(373);

var _vueLoadingOverlay2 = _interopRequireDefault(_vueLoadingOverlay);

var _location = __webpack_require__(1462);

var _location2 = _interopRequireDefault(_location);

var _comboproduct = __webpack_require__(1460);

var _comboproduct2 = _interopRequireDefault(_comboproduct);

var _combosame = __webpack_require__(1461);

var _combosame2 = _interopRequireDefault(_combosame);

var _promotion = __webpack_require__(1463);

var _promotion2 = _interopRequireDefault(_promotion);

var _specification = __webpack_require__(1464);

var _specification2 = _interopRequireDefault(_specification);

__webpack_require__(928);

var _helper = __webpack_require__(926);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = (_name$components$data = {
    name: "mainproductextend",
    components: {},
    data: function data() {
        return {
            actionLocation: false,
            actionCombo: false,
            actionpromotion: false,
            actionspecification: false,
            isLoading: false,
            Id: 0,
            objProduct: {},
            ListZone: [],

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
    }
}, (0, _defineProperty3.default)(_name$components$data, "components", {
    Loading: _vueLoadingOverlay2.default,
    locationinproduct: _location2.default,
    comboproduct: _comboproduct2.default,
    promotioninproduct: _promotion2.default,
    specificationinproduct: _specification2.default,
    sameproduct: _combosame2.default
}), (0, _defineProperty3.default)(_name$components$data, "methods", (0, _extends3.default)({}, (0, _vuex.mapActions)(["getZones", "getNameProduct"]), {
    pathImgs: function pathImgs(path) {
        return (0, _helper.pathImg)(path);
    },
    loadComboSame: function loadComboSame() {
        this.actionComboSame = true;
    },
    loadCombo: function loadCombo() {
        this.actionCombo = true;
    },
    loadPromotion: function loadPromotion() {
        this.actionpromotion = true;
    },
    loadSpecification: function loadSpecification() {
        this.actionspecification = true;
    }
})), (0, _defineProperty3.default)(_name$components$data, "computed", {}), (0, _defineProperty3.default)(_name$components$data, "created", function created() {
    var _this = this;

    this.getZones(1).then(function (respose) {
        try {
            respose.listData.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
            var data = respose.listData;
            var dataLeft = (0, _helper.unflatten)(data);
            _this.ListZone = dataLeft;
        } catch (ex) {}
    });
    if (this.$route.params.id > 0) {
        this.getNameProduct(this.$route.params.id).then(function (respose) {
            try {

                _this.objProduct = respose.data;
            } catch (ex) {}
        });
    }
}), (0, _defineProperty3.default)(_name$components$data, "mounted", function mounted() {
    if (this.$route.params.id > 0) {
        this.isLoading = true;
        this.Id = parseInt(this.$route.params.id);
        this.actionLocation = true;
        this.isLoading = false;
    }
}), _name$components$data);

/***/ }),

/***/ 1180:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _toConsumableArray2 = __webpack_require__(996);

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "promotioninproduct",
    components: {},
    props: {
        productId: {
            type: Number,
            required: false,
            default: 0
        },
        actions: {
            type: Boolean,
            required: false,
            default: false
        }
    },
    data: function data() {
        return {

            ListPromotion: [],
            ListValue: [],

            activeLeft: 0,
            activeRight: 0
        };
    },


    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getPromotionByProducts", "addPromotions"]), {
        onLoadPromotion: function onLoadPromotion() {
            var _this = this;

            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getPromotionByProducts(this.productId).then(function (respose) {
                console.log(respose);
                _this.ListPromotion = respose.listObj1.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });
                _this.ListValue = respose.listObj2.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false
                    });
                });;
            });
        },

        AddPromotion: function AddPromotion() {
            var _this2 = this;

            this.addPromotions({
                Id: this.productId,
                Promotions: this.ListValue
            }).then(function (response) {
                if (response.success == true) {
                    _this2.$toast.success(response.message, {});
                } else {
                    _this2.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this2.$toast.error("Lỗi hệ thống");
            });;
        },
        alltoRight: function alltoRight() {
            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(this.ListPromotion));
        },
        onetoRight: function onetoRight() {
            var lstChose = this.ListPromotion.filter(function (x) {
                return x.active == true;
            });
            this.ListPromotion = this.ListPromotion.filter(function (item) {
                return item.active == false;
            });

            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(lstChose));
        },
        alltoLeft: function alltoLeft() {

            this.ListPromotion = [].concat((0, _toConsumableArray3.default)(this.ListPromotion), (0, _toConsumableArray3.default)(this.ListValue));
        },
        onetoLeft: function onetoLeft() {
            var lstChose = this.ListValue.filter(function (x) {
                return x.active == true;
            });
            this.ListValue = this.ListValue.filter(function (item) {
                return item.active == false;
            });
            this.ListPromotion = [].concat((0, _toConsumableArray3.default)(this.ListPromotion), (0, _toConsumableArray3.default)(lstChose));
        },
        changeValueLeft: function changeValueLeft(id) {
            if (this.ListPromotion[id].active == true) {
                this.ListPromotion[id].active = false;
            } else {
                this.ListPromotion[id].active = true;
            }
        },
        changeValueRight: function changeValueRight(id) {
            if (this.ListValue[id].active == true) {
                this.ListValue[id].active = false;
            } else {
                this.ListValue[id].active = true;
            }
        },
        filterFunction: function filterFunction() {
            var input, filter, i, a, div;
            input = document.getElementById("myInput2");
            filter = input.value.toUpperCase();
            div = document.getElementById("_list-1");
            a = div.getElementsByTagName("button");
            for (i = 0; i < a.length; i++) {
                var txtValue = a[i].textContent || a[i].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    a[i].style.display = "";
                } else {
                    a[i].style.display = "none";
                }
            }
        }

    }),
    watch: {
        actions: function actions(newVal) {
            if (newVal == true) {
                this.onLoadPromotion();
            }
        }
    }
};

/***/ }),

/***/ 1181:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _toConsumableArray2 = __webpack_require__(996);

var _toConsumableArray3 = _interopRequireDefault(_toConsumableArray2);

var _extends2 = __webpack_require__(8);

var _extends3 = _interopRequireDefault(_extends2);

var _vuex = __webpack_require__(176);

var _vueTreeselect = __webpack_require__(925);

var _vueTreeselect2 = _interopRequireDefault(_vueTreeselect);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
    name: "specificationinproduct",
    components: {
        Treeselect: _vueTreeselect2.default
    },
    props: {
        productId: {
            type: Number,
            required: false,
            default: 0
        },
        actions: {
            type: Boolean,
            required: false,
            default: false
        },
        category: {
            type: Array
        }
    },
    data: function data() {
        return {
            ListTemp: [],
            ListSpecifications: [],
            ListValue: [],
            searchLanguageCode: "vi-VN",
            searchZoneId: 0,
            searchKey: "",

            activeLeft: 0,
            activeRight: 0,
            Language: []
        };
    },
    created: function created() {
        var _this = this;

        this.getAllLanguages().then(function (respose) {
            _this.Language = respose.listData.map(function (item) {
                return {
                    value: item.languageCode.trim(),
                    text: item.name
                };
            });
        });
    },

    methods: (0, _extends3.default)({}, (0, _vuex.mapActions)(["getSpecificationByProducts", "addSpecifications", "getAllLanguages"]), {
        onLoadSpecification: function onLoadSpecification() {
            var _this2 = this;

            var initial = this.$route.query.initial;
            initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
            this.getSpecificationByProducts(this.productId).then(function (respose) {
                _this2.ListSpecifications = respose.ListObj1;
                _this2.ListValue = respose.ListObj2.map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false

                    });
                });
            });
        },

        AddPromotion: function AddPromotion() {
            var _this3 = this;

            this.addSpecifications({
                Id: this.productId,
                Specifications: this.ListValue
            }).then(function (response) {
                if (response.success == true) {
                    _this3.$toast.success(response.message, {});
                } else {
                    _this3.$toast.error(response.message, {});
                }
            }).catch(function (e) {
                _this3.$toast.error("Lỗi hệ thống");
            });;
        },
        alltoRight: function alltoRight() {
            var _this4 = this;

            var data = this.ListTemp.map(function (item) {
                return {
                    productId: _this4.productId,
                    value: "",
                    name: item.name,
                    isShowLayout: false,
                    languageCode: _this4.searchLanguageCode,
                    spectificationId: item.id,
                    active: false
                };
            });
            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(data));

            this.ListSpecifications = [];
        },
        onetoRight: function onetoRight() {
            var _this5 = this;

            var lstChose = this.ListTemp.filter(function (x) {
                return x.active == true && x.hidden == false;
            }).map(function (item) {
                return {
                    productId: _this5.productId,
                    value: "",
                    name: item.name,
                    isShowLayout: false,
                    languageCode: _this5.searchLanguageCode,
                    spectificationId: item.id,
                    active: false
                };
            });

            this.ListValue = [].concat((0, _toConsumableArray3.default)(this.ListValue), (0, _toConsumableArray3.default)(lstChose));
        },
        alltoLeft: function alltoLeft() {
            this.ListSpecifications = [].concat((0, _toConsumableArray3.default)(this.ListSpecifications), (0, _toConsumableArray3.default)(this.ListValue));
            this.ListValue = [];
        },
        LoadDanhMuc: function LoadDanhMuc() {
            return this.category.map(function (x) {
                return { id: x.id, label: x.label };
            });
        },

        onetoLeft: function onetoLeft() {

            this.ListValue = this.ListValue.filter(function (item) {
                return item.active == false;
            });
        },
        changeValueLeft: function changeValueLeft(id) {
            if (this.ListTemp[id].active == true) {
                this.ListTemp[id].active = false;
            } else {
                this.ListTemp[id].active = true;
            }
        },
        changeValueRight: function changeValueRight(id) {
            if (this.ListValue[id].active == true) {
                this.ListValue[id].active = false;
            } else {
                this.ListValue[id].active = true;
            }
        },

        filterFunction: function filterFunction() {
            this.ListSpecifications = this.ListSpecifications.map(function (x) {
                return (0, _extends3.default)({}, x, {
                    hidden: false
                });
            });
            var vm = this;

            if (this.searchKey != null && this.searchKey != undefined && this.searchKey.length > 0) {

                this.ListSpecifications = this.ListSpecifications.reduce(function (ListNew, obj) {
                    if (vm.searchKey.toUpperCase().indexOf(obj.name) <= -1) {
                        obj.hidden = true;
                        ListNew.push(obj);
                    }
                    return ListNew;
                }, []);
            }
        }

    }),
    watch: {
        actions: function actions(newVal) {

            if (newVal == true) {
                this.onLoadSpecification();
            }
        },
        searchKey: function searchKey(newVal) {

            this.filterFunction();
        },
        searchZoneId: function searchZoneId(newVal) {
            if (this.ListSpecifications.hasOwnProperty(newVal)) {
                this.ListTemp = this.ListSpecifications[newVal].map(function (item) {
                    return (0, _extends3.default)({}, item, {
                        active: false,
                        hidden: false
                    });
                });
            }
        },
        searchLanguageCode: function searchLanguageCode(newVal) {

            for (var i = 0; i < this.ListValue.length; i++) {
                if (this.ListValue[i].languageCode.trim() == newVal.trim()) {
                    this.ListValue[i].hidden = false;
                } else {
                    this.ListValue[i].hidden = true;
                }
                this.ListValue.active = false;
            }

            console.log(this.ListValue);
        }
    }
};

/***/ }),

/***/ 1406:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, ".productedit .form-control{height:35px}.panel-body ul li .thumb{float:left;height:30px;margin-right:10px;overflow:hidden;width:30px;border:1px solid #ddd}.panel-body ul li p.text-muted{font-size:11px;line-height:15px}.panel-body ul li p{margin-bottom:0}.panel-body ul li{border-bottom:1px solid #e6e6fa;cursor:pointer;list-style-type:none;padding:4px 0;padding-left:10px}::-webkit-scrollbar{width:10px}::-webkit-scrollbar-track{background:#f1f1f1}::-webkit-scrollbar-thumb{background:#888}::-webkit-scrollbar-thumb:hover{background:#555}#rlist .row ul li.active,.row ul li.active{background:#f1f7fd}.pagination .page-item .page-link{font-size:12px}.pagination{margin-top:10px}", ""]);

// exports


/***/ }),

/***/ 1424:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "", ""]);

// exports


/***/ }),

/***/ 1428:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "", ""]);

// exports


/***/ }),

/***/ 1430:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "", ""]);

// exports


/***/ }),

/***/ 1460:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1176),
  /* template */
  __webpack_require__(1535),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 1461:
/***/ (function(module, exports, __webpack_require__) {

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1177),
  /* template */
  __webpack_require__(1517),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 1462:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1576)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1178),
  /* template */
  __webpack_require__(1528),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 1463:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1570)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1180),
  /* template */
  __webpack_require__(1510),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 1464:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1574)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1181),
  /* template */
  __webpack_require__(1520),
  /* scopeId */
  null,
  /* cssModules */
  null
)

module.exports = Component.exports


/***/ }),

/***/ 1475:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "productadd"
  }, [_c('div', {
    staticClass: "card mt-3"
  }, [_c('div', {
    staticClass: "card-header"
  }, [_c('div', [_vm._v("Thêm thông số cho sản phẩm: "), _c('b', {
    staticClass: "text-danger"
  }, [_vm._v(_vm._s(_vm.objProduct.name))])])]), _vm._v(" "), _c('div', {
    staticClass: "card-body",
    staticStyle: {
      "padding": "0 !important"
    }
  }, [_c('b-tabs', {
    staticClass: "col-md-12",
    staticStyle: {
      "padding": "0"
    },
    attrs: {
      "pills": ""
    }
  }, [_c('b-tab', {
    attrs: {
      "title": "Giá sản phẩm"
    }
  }, [_c('locationinproduct', {
    attrs: {
      "productId": _vm.Id,
      "actions": _vm.actionLocation
    }
  })], 1), _vm._v(" "), _c('b-tab', {
    attrs: {
      "title": "Thường được mua cùng"
    },
    on: {
      "click": function($event) {
        return _vm.loadComboSame()
      }
    }
  }, [_c('sameproduct', {
    attrs: {
      "productId": _vm.Id,
      "category": _vm.ListZone,
      "actions": _vm.actionComboSame
    }
  })], 1), _vm._v(" "), _c('b-tab', {
    attrs: {
      "title": "Combo sản phẩm"
    },
    on: {
      "click": function($event) {
        return _vm.loadCombo()
      }
    }
  }, [_c('comboproduct', {
    attrs: {
      "productId": _vm.Id,
      "category": _vm.ListZone,
      "actions": _vm.actionCombo
    }
  })], 1), _vm._v(" "), _c('b-tab', {
    attrs: {
      "title": "Khuyến mại"
    },
    on: {
      "click": function($event) {
        return _vm.loadPromotion()
      }
    }
  }, [_c('promotioninproduct', {
    attrs: {
      "productId": _vm.Id,
      "actions": _vm.actionpromotion
    }
  })], 1), _vm._v(" "), _c('b-tab', {
    attrs: {
      "title": "Chi tiết kĩ thuật"
    },
    on: {
      "click": function($event) {
        return _vm.loadSpecification()
      }
    }
  }, [_c('specificationinproduct', {
    attrs: {
      "category": _vm.ListZone,
      "productId": _vm.Id,
      "actions": _vm.actionspecification
    }
  })], 1)], 1)], 1)])])
},staticRenderFns: []}

/***/ }),

/***/ 1510:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-sm-12 col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-5"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "20%",
      "padding-top": "8px"
    }
  }, [_vm._v("Tìm kiếm: ")]), _vm._v(" "), _c('div', {
    staticStyle: {
      "display": "flex",
      "width": "80%"
    }
  }, [_c('div', {
    staticClass: "input-group"
  }, [_c('input', {
    staticClass: "form-control",
    attrs: {
      "type": "text",
      "autocomplete": "off",
      "placeholder": "Tìm kiếm khuyến mại",
      "id": "myInput2"
    },
    on: {
      "keyup": function($event) {
        return _vm.filterFunction()
      }
    }
  }), _vm._v(" "), _c('span', {
    staticClass: "input-group-addon bg-primary",
    staticStyle: {
      "cursor": "pointer",
      "width": "45px"
    }
  }, [_c('i', {
    staticClass: "fa fa-search",
    staticStyle: {
      "padding-top": "10px",
      "padding-left": "15px"
    },
    on: {
      "click": function($event) {
        return _vm.filterFunction()
      }
    }
  })])])])]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%",
      "height": "300px",
      "overflow-x": "scroll"
    },
    attrs: {
      "id": "_list-2"
    }
  }, _vm._l((_vm.ListPromotion), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueLeft(index)
        }
      }
    }, [_c('p', {
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v(" " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted",
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v(" " + _vm._s(item.des))])])
  }), 0)]), _vm._v(" "), _c('div', {
    staticClass: "col-md-1"
  }, [_c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển tất cả bản ghi"
    },
    on: {
      "click": _vm.alltoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoLeft
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-left",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển tất cả bản ghi"
    },
    on: {
      "click": _vm.alltoLeft
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-left",
    attrs: {
      "aria-hidden": "true"
    }
  })])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-6"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border": "1px solid #ccc",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticClass: "col-md-8 text-center",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("Thông tin khuyến mại")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-4",
    staticStyle: {
      "padding-right": "0"
    }
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form pull-right btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.AddPromotion()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Lưu\n                                ")])])]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%",
      "margin-top": "0"
    }
  }, _vm._l((_vm.ListValue), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueRight(index)
        }
      }
    }, [_c('p', {
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v(" " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted",
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v(" " + _vm._s(item.des))])])
  }), 0)])])])])])])
},staticRenderFns: []}

/***/ }),

/***/ 1517:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-sm-12 col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-6"
  }, [_c('div', {
    staticClass: "panel panel-white"
  }, [_c('div', {
    staticClass: "panel-heading"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%"
    }
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "60%"
    }
  }, [_c('treeselect', {
    attrs: {
      "multiple": false,
      "options": _vm.category,
      "placeholder": "Xin mời bạn lựa chọn danh mục"
    },
    model: {
      value: (_vm.searchZoneId),
      callback: function($$v) {
        _vm.searchZoneId = $$v
      },
      expression: "searchZoneId"
    }
  })], 1), _vm._v(" "), _c('div', {
    staticClass: "input-group",
    staticStyle: {
      "display": "flex",
      "width": "40%"
    }
  }, [_c('input', {
    directives: [{
      name: "model",
      rawName: "v-model",
      value: (_vm.keyword),
      expression: "keyword"
    }],
    staticClass: "form-control",
    attrs: {
      "type": "text",
      "autocomplete": "off",
      "placeholder": "Tìm kiếm sản phẩm"
    },
    domProps: {
      "value": (_vm.keyword)
    },
    on: {
      "keyup": function($event) {
        if (!$event.type.indexOf('key') && _vm._k($event.keyCode, "enter", 13, $event.key, "Enter")) { return null; }
        return _vm.onLoadProduct()
      },
      "input": function($event) {
        if ($event.target.composing) { return; }
        _vm.keyword = $event.target.value
      }
    }
  }), _vm._v(" "), _vm._m(0)])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-body"
  }, [_c('div', {
    staticClass: "slimScrollDiv",
    staticStyle: {
      "position": "relative",
      "overflow-x": "scroll",
      "width": "auto",
      "height": "300px",
      "margin-top": "10px"
    }
  }, [_c('div', {
    staticClass: "row",
    staticStyle: {
      "width": "auto",
      "height": "300px"
    }
  }, [_c('ul', {
    staticStyle: {
      "padding": "20px",
      "padding-left": "10px",
      "width": "100%"
    }
  }, _vm._l((_vm.ListProduct), function(item, index) {
    return _c('li', {
      class: {
        'active': item.active
      },
      on: {
        "click": function($event) {
          return _vm.changeValueLeft(index)
        }
      }
    }, [_c('a', {
      staticClass: "thumb"
    }, [_c('img', {
      attrs: {
        "src": _vm.pathImgs(item.avatar),
        "width": "30"
      }
    })]), _vm._v(" "), _c('p', {
      staticStyle: {
        "font-size": "13px",
        "overflow": "hidden"
      }
    }, [_vm._v("  " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted"
    }, [_vm._v("Giá bản " + _vm._s(item.price))])])
  }), 0)])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-footer"
  }, [_c('b-pagination', {
    attrs: {
      "total-rows": _vm.total,
      "per-page": _vm.pageSize
    },
    model: {
      value: (_vm.currentPage),
      callback: function($$v) {
        _vm.currentPage = $$v
      },
      expression: "currentPage"
    }
  })], 1)])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-1",
    staticStyle: {
      "padding-top": "30px"
    }
  }, [_c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Thêm sản phẩm"
    },
    on: {
      "click": _vm.onetoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-danger btn-block mb-2",
    attrs: {
      "title": "Xóa sản phẩm"
    },
    on: {
      "click": _vm.onetoRemove
    }
  }, [_c('i', {
    staticClass: "fa fa-trash",
    attrs: {
      "aria-hidden": "true"
    }
  })])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-5"
  }, [_c('div', {
    staticClass: "panel panel-white"
  }, [_c('div', {
    staticClass: "panel-heading",
    staticStyle: {
      "width": "100%",
      "display": "flex",
      "border": "1px solid #ccc"
    }
  }, [_c('div', {
    staticClass: "col-md-8",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("\n                                    Danh sách sản phẩm\n                                ")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-4",
    staticStyle: {
      "padding-right": "0"
    }
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form pull-right btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.Add()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Lưu\n                                    ")])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-body"
  }, [_c('div', {
    staticClass: "slimScrollDiv",
    staticStyle: {
      "position": "relative",
      "overflow-x": "scroll",
      "width": "auto",
      "height": "300px",
      "margin-top": "10px"
    }
  }, [_c('div', {
    staticClass: "row",
    staticStyle: {
      "width": "auto",
      "height": "300px"
    }
  }, [_c('ul', {
    staticStyle: {
      "padding": "20px",
      "padding-left": "10px",
      "width": "100%"
    }
  }, _vm._l((_vm.ListValue), function(item, index) {
    return _c('li', {
      class: {
        'active': item.active
      },
      on: {
        "click": function($event) {
          return _vm.changeValueRight(index)
        }
      }
    }, [_c('div', {
      staticClass: "row"
    }, [_c('div', {
      staticClass: "col-8"
    }, [_c('a', {
      staticClass: "thumb"
    }, [_c('img', {
      attrs: {
        "src": _vm.pathImgs(item.avatar),
        "width": "30"
      }
    })]), _vm._v(" "), _c('p', {
      staticStyle: {
        "font-size": "13px",
        "overflow": "hidden"
      }
    }, [_vm._v("  " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted"
    }, [_vm._v("Giá bản " + _vm._s(item.price))])]), _vm._v(" "), _c('div', {
      staticClass: "col-4"
    }, [_c('p', {
      staticStyle: {
        "display": "none"
      }
    }, [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.configPrice),
        expression: "item.configPrice"
      }],
      attrs: {
        "type": "number",
        "placeholder": "Giá giảm"
      },
      domProps: {
        "value": (item.configPrice)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "configPrice", $event.target.value)
        }
      }
    })]), _vm._v(" "), _c('p', {
      staticStyle: {
        "display": "none"
      }
    }, [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.configNote),
        expression: "item.configNote"
      }],
      attrs: {
        "type": "text",
        "placeholder": "Ghi chú"
      },
      domProps: {
        "value": (item.configNote)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "configNote", $event.target.value)
        }
      }
    })])])])])
  }), 0)])])])])])])])])])])
},staticRenderFns: [function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('span', {
    staticClass: "input-group-addon bg-primary",
    staticStyle: {
      "cursor": "pointer",
      "width": "45px"
    }
  }, [_c('i', {
    staticClass: "fa fa-search",
    staticStyle: {
      "padding-top": "10px",
      "padding-left": "15px"
    }
  })])
}]}

/***/ }),

/***/ 1520:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-sm-12 col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-5"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%"
    }
  }, [_c('treeselect', {
    attrs: {
      "multiple": false,
      "options": _vm.LoadDanhMuc(),
      "placeholder": "Xin mời bạn lựa chọn danh mục",
      "default-expanded-level": Infinity
    },
    model: {
      value: (_vm.searchZoneId),
      callback: function($$v) {
        _vm.searchZoneId = $$v
      },
      expression: "searchZoneId"
    }
  })], 1)]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%"
    },
    attrs: {
      "id": "_list-3"
    }
  }, _vm._l((_vm.ListTemp), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }, {
        'hidden': item.hidden
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueLeft(index)
        }
      }
    }, [_c('p', {
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v("Tên thông số: " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted",
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v("Đơn vị: " + _vm._s(item.unit))])])
  }), 0)]), _vm._v(" "), _c('div', {
    staticClass: "col-md-1"
  }, [_c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển tất cả bản ghi"
    },
    on: {
      "click": _vm.alltoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-danger btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoLeft
    }
  }, [_c('i', {
    staticClass: "fa fa-trash",
    attrs: {
      "aria-hidden": "true"
    }
  })])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-6"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border": "1px solid #ccc",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticClass: "text-center",
    staticStyle: {
      "display": "flex",
      "width": "100%"
    }
  }, [_c('div', {
    staticClass: "col-md-6",
    staticStyle: {
      "padding-left": "0"
    }
  }, [_c('select', {
    directives: [{
      name: "model",
      rawName: "v-model",
      value: (_vm.searchLanguageCode),
      expression: "searchLanguageCode"
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
        _vm.searchLanguageCode = $event.target.multiple ? $$selectedVal : $$selectedVal[0]
      }
    }
  }, _vm._l((_vm.Language), function(item) {
    return _c('option', {
      domProps: {
        "value": item.value
      }
    }, [_vm._v("\n                                            " + _vm._s(item.text) + "\n                                        ")])
  }), 0)]), _vm._v(" "), _c('div', {
    staticClass: "col-md-6",
    staticStyle: {
      "padding-right": "0"
    }
  }, [_c('button', {
    staticClass: "btn pull-right btn-info btn-submit-form btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.AddPromotion()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Cập nhật\n                                    ")])])])]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%",
      "margin-top": "0"
    }
  }, _vm._l((_vm.ListValue), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }, {
        'hidden': item.hidden
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueRight(index)
        }
      }
    }, [_c('p', {
      staticStyle: {
        "margin-bottom": "5px"
      }
    }, [_vm._v("Tên thông số: " + _vm._s(item.name))]), _vm._v(" "), _c('div', {
      staticStyle: {
        "display": "flex",
        "width": "100%"
      }
    }, [_c('p', {
      staticClass: "text-muted",
      staticStyle: {
        "margin-bottom": "5px",
        "display": "flex",
        "width": "75px"
      }
    }, [_vm._v("Thông số : ")]), _vm._v(" "), _c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.value),
        expression: "item.value"
      }],
      staticClass: "col-md-4 form-control",
      staticStyle: {
        "height": "20px",
        "display": "flex",
        "width": "30%"
      },
      domProps: {
        "value": (item.value)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "value", $event.target.value)
        }
      }
    }), _vm._v(" "), _c('p', {
      staticStyle: {
        "display": "flex",
        "padding-left": "10px",
        "margin-bottom": "0px"
      }
    }, [_c('b-form-checkbox', {
      model: {
        value: (item.isShowLayout),
        callback: function($$v) {
          _vm.$set(item, "isShowLayout", $$v)
        },
        expression: "item.isShowLayout"
      }
    }, [_vm._v("Hiển thị trên menu")])], 1)])])
  }), 0)])])])])])])
},staticRenderFns: []}

/***/ }),

/***/ 1528:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-sm-12 col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-4"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "20%",
      "padding-top": "8px"
    }
  }, [_vm._v("Tìm kiếm: ")]), _vm._v(" "), _c('div', {
    staticStyle: {
      "display": "flex",
      "width": "80%"
    }
  }, [_c('div', {
    staticClass: "input-group"
  }, [_c('input', {
    staticClass: "form-control",
    attrs: {
      "type": "text",
      "autocomplete": "off",
      "placeholder": "Tìm kiếm địa chỉ",
      "id": "myInput"
    },
    on: {
      "keyup": function($event) {
        return _vm.filterFunction()
      }
    }
  }), _vm._v(" "), _c('span', {
    staticClass: "input-group-addon bg-primary",
    staticStyle: {
      "cursor": "pointer",
      "width": "45px"
    }
  }, [_c('i', {
    staticClass: "fa fa-search",
    staticStyle: {
      "padding-top": "10px",
      "padding-left": "15px"
    },
    on: {
      "click": function($event) {
        return _vm.filterFunction()
      }
    }
  })])])])]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%",
      "height": "300px",
      "overflow-x": "scroll"
    },
    attrs: {
      "id": "list-1"
    }
  }, _vm._l((_vm.ListLocation), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueLeft(index)
        }
      }
    }, [_vm._v(_vm._s(item.ten))])
  }), 0)]), _vm._v(" "), _c('div', {
    staticClass: "col-md-1"
  }, [_c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển tất cả bản ghi"
    },
    on: {
      "click": _vm.alltoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển các bản ghi đã chọn"
    },
    on: {
      "click": _vm.onetoLeft
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-left",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Chuyển tất cả bản ghi"
    },
    on: {
      "click": _vm.alltoLeft
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-double-left",
    attrs: {
      "aria-hidden": "true"
    }
  })])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-7"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%",
      "border": "1px solid #ccc",
      "border-bottom": "none"
    }
  }, [_c('div', {
    staticClass: "col-md-3 text-center",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("Khu vực")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-3  text-center",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("Giá ")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-2  text-center",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("Giảm %")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-2  text-center",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("Giá giảm")]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-info btn-submit-form col-md-2 btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.Add()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Lưu\n                            ")])]), _vm._v(" "), _c('div', {
    staticClass: "dropdown-menu",
    staticStyle: {
      "display": "block",
      "position": "unset",
      "width": "100%",
      "margin-top": "0"
    }
  }, _vm._l((_vm.ListValue), function(item, index) {
    return _c('button', {
      class: ['dropdown-item', {
        'active': item.active
      }],
      on: {
        "click": function($event) {
          return _vm.changeValueRight(index)
        }
      }
    }, [_c('div', {
      staticStyle: {
        "display": "flex",
        "width": "100%"
      }
    }, [_c('div', {
      staticClass: "col-md-3"
    }, [_vm._v("\n                                        " + _vm._s(item.ten) + "\n                                    ")]), _vm._v(" "), _c('div', {
      staticClass: "col-md-3"
    }, [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.price),
        expression: "item.price"
      }],
      staticClass: "form-control",
      staticStyle: {
        "height": "20px"
      },
      attrs: {
        "min": "0",
        "type": "number"
      },
      domProps: {
        "value": (item.price)
      },
      on: {
        "change": function($event) {
          return _vm.changeValue(item)
        },
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "price", $event.target.value)
        }
      }
    })]), _vm._v(" "), _c('div', {
      staticClass: "col-md-2"
    }, [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.discountPercent),
        expression: "item.discountPercent"
      }],
      staticClass: "form-control",
      staticStyle: {
        "height": "20px"
      },
      attrs: {
        "min": "0",
        "max": "100",
        "type": "number"
      },
      domProps: {
        "value": (item.discountPercent)
      },
      on: {
        "change": function($event) {
          return _vm.changeValue(item)
        },
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "discountPercent", $event.target.value)
        }
      }
    })]), _vm._v(" "), _c('div', {
      staticClass: "col-md-3"
    }, [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.salePrice),
        expression: "item.salePrice"
      }],
      staticClass: "form-control",
      staticStyle: {
        "height": "20px"
      },
      attrs: {
        "min": "0",
        "type": "number"
      },
      domProps: {
        "value": (item.salePrice)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "salePrice", $event.target.value)
        }
      }
    })])])])
  }), 0)])])])])])])
},staticRenderFns: []}

/***/ }),

/***/ 1535:
/***/ (function(module, exports) {

module.exports={render:function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('div', {
    staticClass: "row productedit"
  }, [_c('div', {
    staticClass: "col-sm-12 col-md-12"
  }, [_c('div', {
    staticClass: "card"
  }, [_c('div', {
    staticClass: "card-body"
  }, [_c('div', {
    staticClass: "row"
  }, [_c('div', {
    staticClass: "col-md-6"
  }, [_c('div', {
    staticClass: "panel panel-white"
  }, [_c('div', {
    staticClass: "panel-heading"
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "100%"
    }
  }, [_c('div', {
    staticStyle: {
      "display": "flex",
      "width": "60%"
    }
  }, [_c('treeselect', {
    attrs: {
      "multiple": false,
      "options": _vm.category,
      "placeholder": "Xin mời bạn lựa chọn danh mục"
    },
    model: {
      value: (_vm.searchZoneId),
      callback: function($$v) {
        _vm.searchZoneId = $$v
      },
      expression: "searchZoneId"
    }
  })], 1), _vm._v(" "), _c('div', {
    staticClass: "input-group",
    staticStyle: {
      "display": "flex",
      "width": "40%"
    }
  }, [_c('input', {
    directives: [{
      name: "model",
      rawName: "v-model",
      value: (_vm.keyword),
      expression: "keyword"
    }],
    staticClass: "form-control",
    attrs: {
      "type": "text",
      "autocomplete": "off",
      "placeholder": "Tìm kiếm sản phẩm"
    },
    domProps: {
      "value": (_vm.keyword)
    },
    on: {
      "keyup": function($event) {
        if (!$event.type.indexOf('key') && _vm._k($event.keyCode, "enter", 13, $event.key, "Enter")) { return null; }
        return _vm.onLoadProduct()
      },
      "input": function($event) {
        if ($event.target.composing) { return; }
        _vm.keyword = $event.target.value
      }
    }
  }), _vm._v(" "), _vm._m(0)])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-body"
  }, [_c('div', {
    staticClass: "slimScrollDiv",
    staticStyle: {
      "position": "relative",
      "overflow-x": "scroll",
      "width": "auto",
      "height": "300px",
      "margin-top": "10px"
    }
  }, [_c('div', {
    staticClass: "row",
    staticStyle: {
      "width": "auto",
      "height": "300px"
    }
  }, [_c('ul', {
    staticStyle: {
      "padding": "20px",
      "padding-left": "10px",
      "width": "100%"
    }
  }, _vm._l((_vm.ListProduct), function(item, index) {
    return _c('li', {
      class: {
        'active': item.active
      },
      on: {
        "click": function($event) {
          return _vm.changeValueLeft(index)
        }
      }
    }, [_c('a', {
      staticClass: "thumb"
    }, [_c('img', {
      attrs: {
        "src": _vm.pathImgs(item.avatar),
        "width": "30"
      }
    })]), _vm._v(" "), _c('p', {
      staticStyle: {
        "font-size": "13px",
        "overflow": "hidden"
      }
    }, [_vm._v("  " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted"
    }, [_vm._v("Giá bản " + _vm._s(item.price))])])
  }), 0)])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-footer"
  }, [_c('b-pagination', {
    attrs: {
      "total-rows": _vm.total,
      "per-page": _vm.pageSize
    },
    model: {
      value: (_vm.currentPage),
      callback: function($$v) {
        _vm.currentPage = $$v
      },
      expression: "currentPage"
    }
  })], 1)])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-1",
    staticStyle: {
      "padding-top": "30px"
    }
  }, [_c('button', {
    staticClass: "btn btn-primary btn-block mb-2",
    attrs: {
      "title": "Thêm sản phẩm"
    },
    on: {
      "click": _vm.onetoRight
    }
  }, [_c('i', {
    staticClass: "fa fa-angle-right",
    attrs: {
      "aria-hidden": "true"
    }
  })]), _vm._v(" "), _c('button', {
    staticClass: "btn btn-danger btn-block mb-2",
    attrs: {
      "title": "Xóa sản phẩm"
    },
    on: {
      "click": _vm.onetoRemove
    }
  }, [_c('i', {
    staticClass: "fa fa-trash",
    attrs: {
      "aria-hidden": "true"
    }
  })])]), _vm._v(" "), _c('div', {
    staticClass: "col-md-5"
  }, [_c('div', {
    staticClass: "panel panel-white"
  }, [_c('div', {
    staticClass: "panel-heading",
    staticStyle: {
      "width": "100%",
      "display": "flex",
      "border": "1px solid #ccc"
    }
  }, [_c('div', {
    staticClass: "col-md-8",
    staticStyle: {
      "padding-top": "5px"
    }
  }, [_vm._v("\n                                    Danh sách sản phẩm\n                                ")]), _vm._v(" "), _c('div', {
    staticClass: "col-md-4",
    staticStyle: {
      "padding-right": "0"
    }
  }, [_c('button', {
    staticClass: "btn btn-info btn-submit-form pull-right btncus",
    attrs: {
      "type": "submit"
    },
    on: {
      "click": function($event) {
        return _vm.Add()
      }
    }
  }, [_c('i', {
    staticClass: "fa fa-save"
  }), _vm._v(" Lưu\n                                    ")])])]), _vm._v(" "), _c('div', {
    staticClass: "panel-body"
  }, [_c('div', {
    staticClass: "slimScrollDiv",
    staticStyle: {
      "position": "relative",
      "overflow-x": "scroll",
      "width": "auto",
      "height": "300px",
      "margin-top": "10px"
    }
  }, [_c('div', {
    staticClass: "row",
    staticStyle: {
      "width": "auto",
      "height": "300px"
    }
  }, [_c('ul', {
    staticStyle: {
      "padding": "20px",
      "padding-left": "10px",
      "width": "100%"
    }
  }, _vm._l((_vm.ListValue), function(item, index) {
    return _c('li', {
      class: {
        'active': item.active
      },
      on: {
        "click": function($event) {
          return _vm.changeValueRight(index)
        }
      }
    }, [_c('div', {
      staticClass: "row"
    }, [_c('div', {
      staticClass: "col-8"
    }, [_c('a', {
      staticClass: "thumb"
    }, [_c('img', {
      attrs: {
        "src": _vm.pathImgs(item.avatar),
        "width": "30"
      }
    })]), _vm._v(" "), _c('p', {
      staticStyle: {
        "font-size": "13px",
        "overflow": "hidden"
      }
    }, [_vm._v("  " + _vm._s(item.name))]), _vm._v(" "), _c('p', {
      staticClass: "text-muted"
    }, [_vm._v("Giá bản " + _vm._s(item.price))])]), _vm._v(" "), _c('div', {
      staticClass: "col-4"
    }, [_c('p', [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.configPrice),
        expression: "item.configPrice"
      }],
      attrs: {
        "type": "number",
        "placeholder": "Giá giảm"
      },
      domProps: {
        "value": (item.configPrice)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "configPrice", $event.target.value)
        }
      }
    })]), _vm._v(" "), _c('p', [_c('input', {
      directives: [{
        name: "model",
        rawName: "v-model",
        value: (item.configNote),
        expression: "item.configNote"
      }],
      attrs: {
        "type": "text",
        "placeholder": "Ghi chú"
      },
      domProps: {
        "value": (item.configNote)
      },
      on: {
        "input": function($event) {
          if ($event.target.composing) { return; }
          _vm.$set(item, "configNote", $event.target.value)
        }
      }
    })])])])])
  }), 0)])])])])])])])])])])
},staticRenderFns: [function (){var _vm=this;var _h=_vm.$createElement;var _c=_vm._self._c||_h;
  return _c('span', {
    staticClass: "input-group-addon bg-primary",
    staticStyle: {
      "cursor": "pointer",
      "width": "45px"
    }
  }, [_c('i', {
    staticClass: "fa fa-search",
    staticStyle: {
      "padding-top": "10px",
      "padding-left": "15px"
    }
  })])
}]}

/***/ }),

/***/ 1552:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1406);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("5fd57241", content, true);

/***/ }),

/***/ 1570:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1424);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("bc4fa500", content, true);

/***/ }),

/***/ 1574:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1428);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("28114aae", content, true);

/***/ }),

/***/ 1576:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(1430);
if(typeof content === 'string') content = [[module.i, content, '']];
if(content.locals) module.exports = content.locals;
// add the styles to the DOM
var update = __webpack_require__(777)("235ff64a", content, true);

/***/ }),

/***/ 753:
/***/ (function(module, exports, __webpack_require__) {


/* styles */
__webpack_require__(1552)

var Component = __webpack_require__(371)(
  /* script */
  __webpack_require__(1179),
  /* template */
  __webpack_require__(1475),
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

/***/ 775:
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(22);
var core = __webpack_require__(20);
var LIBRARY = __webpack_require__(104);
var wksExt = __webpack_require__(776);
var defineProperty = __webpack_require__(54).f;
module.exports = function (name) {
  var $Symbol = core.Symbol || (core.Symbol = LIBRARY ? {} : global.Symbol || {});
  if (name.charAt(0) != '_' && !(name in $Symbol)) defineProperty($Symbol, name, { value: wksExt.f(name) });
};


/***/ }),

/***/ 776:
/***/ (function(module, exports, __webpack_require__) {

exports.f = __webpack_require__(23);


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

/***/ 778:
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(788), __esModule: true };

/***/ }),

/***/ 779:
/***/ (function(module, exports) {

function _arrayLikeToArray(arr, len) {
  if (len == null || len > arr.length) len = arr.length;

  for (var i = 0, arr2 = new Array(len); i < len; i++) {
    arr2[i] = arr[i];
  }

  return arr2;
}

module.exports = _arrayLikeToArray;

/***/ }),

/***/ 780:
/***/ (function(module, exports, __webpack_require__) {

var arrayLikeToArray = __webpack_require__(779);

function _unsupportedIterableToArray(o, minLen) {
  if (!o) return;
  if (typeof o === "string") return arrayLikeToArray(o, minLen);
  var n = Object.prototype.toString.call(o).slice(8, -1);
  if (n === "Object" && o.constructor) n = o.constructor.name;
  if (n === "Map" || n === "Set") return Array.from(n);
  if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return arrayLikeToArray(o, minLen);
}

module.exports = _unsupportedIterableToArray;

/***/ }),

/***/ 781:
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(945), __esModule: true };

/***/ }),

/***/ 782:
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(946), __esModule: true };

/***/ }),

/***/ 783:
/***/ (function(module, exports, __webpack_require__) {

// 19.1.2.7 / 15.2.3.4 Object.getOwnPropertyNames(O)
var $keys = __webpack_require__(382);
var hiddenKeys = __webpack_require__(181).concat('length', 'prototype');

exports.f = Object.getOwnPropertyNames || function getOwnPropertyNames(O) {
  return $keys(O, hiddenKeys);
};


/***/ }),

/***/ 784:
/***/ (function(module, exports, __webpack_require__) {

var baseTrim = __webpack_require__(956),
    isObject = __webpack_require__(58),
    isSymbol = __webpack_require__(962);

/** Used as references for various `Number` constants. */
var NAN = 0 / 0;

/** Used to detect bad signed hexadecimal string values. */
var reIsBadHex = /^[-+]0x[0-9a-f]+$/i;

/** Used to detect binary string values. */
var reIsBinary = /^0b[01]+$/i;

/** Used to detect octal string values. */
var reIsOctal = /^0o[0-7]+$/i;

/** Built-in method references without a dependency on `root`. */
var freeParseInt = parseInt;

/**
 * Converts `value` to a number.
 *
 * @static
 * @memberOf _
 * @since 4.0.0
 * @category Lang
 * @param {*} value The value to process.
 * @returns {number} Returns the number.
 * @example
 *
 * _.toNumber(3.2);
 * // => 3.2
 *
 * _.toNumber(Number.MIN_VALUE);
 * // => 5e-324
 *
 * _.toNumber(Infinity);
 * // => Infinity
 *
 * _.toNumber('3.2');
 * // => 3.2
 */
function toNumber(value) {
  if (typeof value == 'number') {
    return value;
  }
  if (isSymbol(value)) {
    return NAN;
  }
  if (isObject(value)) {
    var other = typeof value.valueOf == 'function' ? value.valueOf() : value;
    value = isObject(other) ? (other + '') : other;
  }
  if (typeof value != 'string') {
    return value === 0 ? value : +value;
  }
  value = baseTrim(value);
  var isBinary = reIsBinary.test(value);
  return (isBinary || reIsOctal.test(value))
    ? freeParseInt(value.slice(2), isBinary ? 2 : 8)
    : (reIsBadHex.test(value) ? NAN : +value);
}

module.exports = toNumber;


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


/***/ }),

/***/ 788:
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(789);
var $Object = __webpack_require__(20).Object;
module.exports = function defineProperty(it, key, desc) {
  return $Object.defineProperty(it, key, desc);
};


/***/ }),

/***/ 789:
/***/ (function(module, exports, __webpack_require__) {

var $export = __webpack_require__(32);
// 19.1.2.4 / 15.2.3.6 Object.defineProperty(O, P, Attributes)
$export($export.S + $export.F * !__webpack_require__(44), 'Object', { defineProperty: __webpack_require__(54).f });


/***/ }),

/***/ 925:
/***/ (function(module, exports, __webpack_require__) {

/* WEBPACK VAR INJECTION */(function(process) {/*!
 * vue-treeselect v0.4.0 | (c) 2017-2019 Riophae Lee
 * Released under the MIT License.
 * https://vue-treeselect.js.org/
 */
module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "/";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 16);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(938);

/***/ }),
/* 1 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(939);

/***/ }),
/* 2 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(933);

/***/ }),
/* 3 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(954);

/***/ }),
/* 4 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(964);

/***/ }),
/* 5 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(960);

/***/ }),
/* 6 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(969);

/***/ }),
/* 7 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(955);

/***/ }),
/* 8 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(966);

/***/ }),
/* 9 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(961);

/***/ }),
/* 10 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(959);

/***/ }),
/* 11 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(940);

/***/ }),
/* 12 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(963);

/***/ }),
/* 13 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(941);

/***/ }),
/* 14 */
/***/ (function(module, exports) {

module.exports = __webpack_require__(31);

/***/ }),
/* 15 */
/***/ (function(module, exports, __webpack_require__) {

// extracted by mini-css-extract-plugin

/***/ }),
/* 16 */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);

// EXTERNAL MODULE: external "@babel/runtime/helpers/slicedToArray"
var slicedToArray_ = __webpack_require__(0);
var slicedToArray_default = /*#__PURE__*/__webpack_require__.n(slicedToArray_);

// EXTERNAL MODULE: external "@babel/runtime/helpers/toConsumableArray"
var toConsumableArray_ = __webpack_require__(1);
var toConsumableArray_default = /*#__PURE__*/__webpack_require__.n(toConsumableArray_);

// EXTERNAL MODULE: external "@babel/runtime/helpers/defineProperty"
var defineProperty_ = __webpack_require__(2);
var defineProperty_default = /*#__PURE__*/__webpack_require__.n(defineProperty_);

// EXTERNAL MODULE: external "fuzzysearch"
var external_fuzzysearch_ = __webpack_require__(3);
var external_fuzzysearch_default = /*#__PURE__*/__webpack_require__.n(external_fuzzysearch_);

// EXTERNAL MODULE: external "lodash/noop"
var noop_ = __webpack_require__(4);
var noop_default = /*#__PURE__*/__webpack_require__.n(noop_);

// CONCATENATED MODULE: ./src/utils/noop.js

// CONCATENATED MODULE: ./src/utils/warning.js


var warning_warning = process.env.NODE_ENV === 'production' ? noop_default.a : function warning(checker, complainer) {
  if (!checker()) {
    var _console;

    var message = ['[Vue-Treeselect Warning]'].concat(complainer());

    (_console = console).error.apply(_console, toConsumableArray_default()(message));
  }
};
// CONCATENATED MODULE: ./src/utils/onLeftClick.js
function onLeftClick(mouseDownHandler) {
  return function onMouseDown(evt) {
    if (evt.type === 'mousedown' && evt.button === 0) {
      for (var _len = arguments.length, args = new Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
        args[_key - 1] = arguments[_key];
      }

      mouseDownHandler.call.apply(mouseDownHandler, [this, evt].concat(args));
    }
  };
}
// CONCATENATED MODULE: ./src/utils/scrollIntoView.js
function scrollIntoView($scrollingEl, $focusedEl) {
  var scrollingReact = $scrollingEl.getBoundingClientRect();
  var focusedRect = $focusedEl.getBoundingClientRect();
  var overScroll = $focusedEl.offsetHeight / 3;

  if (focusedRect.bottom + overScroll > scrollingReact.bottom) {
    $scrollingEl.scrollTop = Math.min($focusedEl.offsetTop + $focusedEl.clientHeight - $scrollingEl.offsetHeight + overScroll, $scrollingEl.scrollHeight);
  } else if (focusedRect.top - overScroll < scrollingReact.top) {
    $scrollingEl.scrollTop = Math.max($focusedEl.offsetTop - overScroll, 0);
  }
}
// EXTERNAL MODULE: external "lodash/debounce"
var debounce_ = __webpack_require__(5);
var debounce_default = /*#__PURE__*/__webpack_require__.n(debounce_);

// CONCATENATED MODULE: ./src/utils/debounce.js

// EXTERNAL MODULE: external "watch-size"
var external_watch_size_ = __webpack_require__(6);
var external_watch_size_default = /*#__PURE__*/__webpack_require__.n(external_watch_size_);

// CONCATENATED MODULE: ./src/utils/removeFromArray.js
function removeFromArray(arr, elem) {
  var idx = arr.indexOf(elem);
  if (idx !== -1) arr.splice(idx, 1);
}
// CONCATENATED MODULE: ./src/utils/watchSize.js


var intervalId;
var registered = [];
var INTERVAL_DURATION = 100;

function run() {
  intervalId = setInterval(function () {
    registered.forEach(test);
  }, INTERVAL_DURATION);
}

function stop() {
  clearInterval(intervalId);
  intervalId = null;
}

function test(item) {
  var $el = item.$el,
      listener = item.listener,
      lastWidth = item.lastWidth,
      lastHeight = item.lastHeight;
  var width = $el.offsetWidth;
  var height = $el.offsetHeight;

  if (lastWidth !== width || lastHeight !== height) {
    item.lastWidth = width;
    item.lastHeight = height;
    listener({
      width: width,
      height: height
    });
  }
}

function watchSizeForIE9($el, listener) {
  var item = {
    $el: $el,
    listener: listener,
    lastWidth: null,
    lastHeight: null
  };

  var unwatch = function unwatch() {
    removeFromArray(registered, item);
    if (!registered.length) stop();
  };

  registered.push(item);
  test(item);
  run();
  return unwatch;
}

function watchSize($el, listener) {
  var isIE9 = document.documentMode === 9;
  var locked = true;

  var wrappedListener = function wrappedListener() {
    return locked || listener.apply(void 0, arguments);
  };

  var implementation = isIE9 ? watchSizeForIE9 : external_watch_size_default.a;
  var removeSizeWatcher = implementation($el, wrappedListener);
  locked = false;
  return removeSizeWatcher;
}
// CONCATENATED MODULE: ./src/utils/setupResizeAndScrollEventListeners.js
function findScrollParents($el) {
  var $scrollParents = [];
  var $parent = $el.parentNode;

  while ($parent && $parent.nodeName !== 'BODY' && $parent.nodeType === document.ELEMENT_NODE) {
    if (isScrollElment($parent)) $scrollParents.push($parent);
    $parent = $parent.parentNode;
  }

  $scrollParents.push(window);
  return $scrollParents;
}

function isScrollElment($el) {
  var _getComputedStyle = getComputedStyle($el),
      overflow = _getComputedStyle.overflow,
      overflowX = _getComputedStyle.overflowX,
      overflowY = _getComputedStyle.overflowY;

  return /(auto|scroll|overlay)/.test(overflow + overflowY + overflowX);
}

function setupResizeAndScrollEventListeners($el, listener) {
  var $scrollParents = findScrollParents($el);
  window.addEventListener('resize', listener, {
    passive: true
  });
  $scrollParents.forEach(function (scrollParent) {
    scrollParent.addEventListener('scroll', listener, {
      passive: true
    });
  });
  return function removeEventListeners() {
    window.removeEventListener('resize', listener, {
      passive: true
    });
    $scrollParents.forEach(function ($scrollParent) {
      $scrollParent.removeEventListener('scroll', listener, {
        passive: true
      });
    });
  };
}
// CONCATENATED MODULE: ./src/utils/isNaN.js
function isNaN_isNaN(x) {
  return x !== x;
}
// EXTERNAL MODULE: external "is-promise"
var external_is_promise_ = __webpack_require__(7);
var external_is_promise_default = /*#__PURE__*/__webpack_require__.n(external_is_promise_);

// CONCATENATED MODULE: ./src/utils/isPromise.js

// EXTERNAL MODULE: external "lodash/once"
var once_ = __webpack_require__(8);
var once_default = /*#__PURE__*/__webpack_require__.n(once_);

// CONCATENATED MODULE: ./src/utils/once.js

// EXTERNAL MODULE: external "lodash/identity"
var identity_ = __webpack_require__(9);
var identity_default = /*#__PURE__*/__webpack_require__.n(identity_);

// CONCATENATED MODULE: ./src/utils/identity.js

// EXTERNAL MODULE: external "lodash/constant"
var constant_ = __webpack_require__(10);
var constant_default = /*#__PURE__*/__webpack_require__.n(constant_);

// CONCATENATED MODULE: ./src/utils/constant.js

// CONCATENATED MODULE: ./src/utils/createMap.js
var createMap = function createMap() {
  return Object.create(null);
};
// EXTERNAL MODULE: external "@babel/runtime/helpers/typeof"
var typeof_ = __webpack_require__(11);
var typeof_default = /*#__PURE__*/__webpack_require__.n(typeof_);

// CONCATENATED MODULE: ./src/utils/deepExtend.js


function isPlainObject(value) {
  if (value == null || typeof_default()(value) !== 'object') return false;
  return Object.getPrototypeOf(value) === Object.prototype;
}

function copy(obj, key, value) {
  if (isPlainObject(value)) {
    obj[key] || (obj[key] = {});
    deepExtend(obj[key], value);
  } else {
    obj[key] = value;
  }
}

function deepExtend(target, source) {
  if (isPlainObject(source)) {
    var keys = Object.keys(source);

    for (var i = 0, len = keys.length; i < len; i++) {
      copy(target, keys[i], source[keys[i]]);
    }
  }

  return target;
}
// EXTERNAL MODULE: external "lodash/last"
var last_ = __webpack_require__(12);
var last_default = /*#__PURE__*/__webpack_require__.n(last_);

// CONCATENATED MODULE: ./src/utils/last.js

// CONCATENATED MODULE: ./src/utils/includes.js
function includes(arrOrStr, elem) {
  return arrOrStr.indexOf(elem) !== -1;
}
// CONCATENATED MODULE: ./src/utils/find.js
function find(arr, predicate, ctx) {
  for (var i = 0, len = arr.length; i < len; i++) {
    if (predicate.call(ctx, arr[i], i, arr)) return arr[i];
  }

  return undefined;
}
// CONCATENATED MODULE: ./src/utils/quickDiff.js
function quickDiff(arrA, arrB) {
  if (arrA.length !== arrB.length) return true;

  for (var i = 0; i < arrA.length; i++) {
    if (arrA[i] !== arrB[i]) return true;
  }

  return false;
}
// CONCATENATED MODULE: ./src/utils/index.js



















// CONCATENATED MODULE: ./src/constants.js
var NO_PARENT_NODE = null;
var UNCHECKED = 0;
var INDETERMINATE = 1;
var CHECKED = 2;
var ALL_CHILDREN = 'ALL_CHILDREN';
var ALL_DESCENDANTS = 'ALL_DESCENDANTS';
var LEAF_CHILDREN = 'LEAF_CHILDREN';
var LEAF_DESCENDANTS = 'LEAF_DESCENDANTS';
var LOAD_ROOT_OPTIONS = 'LOAD_ROOT_OPTIONS';
var LOAD_CHILDREN_OPTIONS = 'LOAD_CHILDREN_OPTIONS';
var ASYNC_SEARCH = 'ASYNC_SEARCH';
var ALL = 'ALL';
var BRANCH_PRIORITY = 'BRANCH_PRIORITY';
var LEAF_PRIORITY = 'LEAF_PRIORITY';
var ALL_WITH_INDETERMINATE = 'ALL_WITH_INDETERMINATE';
var ORDER_SELECTED = 'ORDER_SELECTED';
var LEVEL = 'LEVEL';
var INDEX = 'INDEX';
var KEY_CODES = {
  BACKSPACE: 8,
  ENTER: 13,
  ESCAPE: 27,
  END: 35,
  HOME: 36,
  ARROW_LEFT: 37,
  ARROW_UP: 38,
  ARROW_RIGHT: 39,
  ARROW_DOWN: 40,
  DELETE: 46
};
var INPUT_DEBOUNCE_DELAY = process.env.NODE_ENV === 'testing' ? 10 : 200;
var MIN_INPUT_WIDTH = 5;
var MENU_BUFFER = 40;
// CONCATENATED MODULE: ./src/mixins/treeselectMixin.js




function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(source, true).forEach(function (key) { defineProperty_default()(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }





function sortValueByIndex(a, b) {
  var i = 0;

  do {
    if (a.level < i) return -1;
    if (b.level < i) return 1;
    if (a.index[i] !== b.index[i]) return a.index[i] - b.index[i];
    i++;
  } while (true);
}

function sortValueByLevel(a, b) {
  return a.level === b.level ? sortValueByIndex(a, b) : a.level - b.level;
}

function createAsyncOptionsStates() {
  return {
    isLoaded: false,
    isLoading: false,
    loadingError: ''
  };
}

function stringifyOptionPropValue(value) {
  if (typeof value === 'string') return value;
  if (typeof value === 'number' && !isNaN_isNaN(value)) return value + '';
  return '';
}

function match(enableFuzzyMatch, needle, haystack) {
  return enableFuzzyMatch ? external_fuzzysearch_default()(needle, haystack) : includes(haystack, needle);
}

function getErrorMessage(err) {
  return err.message || String(err);
}

var instanceId = 0;
/* harmony default export */ var treeselectMixin = ({
  provide: function provide() {
    return {
      instance: this
    };
  },
  props: {
    allowClearingDisabled: {
      type: Boolean,
      default: false
    },
    allowSelectingDisabledDescendants: {
      type: Boolean,
      default: false
    },
    alwaysOpen: {
      type: Boolean,
      default: false
    },
    appendToBody: {
      type: Boolean,
      default: false
    },
    async: {
      type: Boolean,
      default: false
    },
    autoFocus: {
      type: Boolean,
      default: false
    },
    autoLoadRootOptions: {
      type: Boolean,
      default: true
    },
    autoDeselectAncestors: {
      type: Boolean,
      default: false
    },
    autoDeselectDescendants: {
      type: Boolean,
      default: false
    },
    autoSelectAncestors: {
      type: Boolean,
      default: false
    },
    autoSelectDescendants: {
      type: Boolean,
      default: false
    },
    backspaceRemoves: {
      type: Boolean,
      default: true
    },
    beforeClearAll: {
      type: Function,
      default: constant_default()(true)
    },
    branchNodesFirst: {
      type: Boolean,
      default: false
    },
    cacheOptions: {
      type: Boolean,
      default: true
    },
    clearable: {
      type: Boolean,
      default: true
    },
    clearAllText: {
      type: String,
      default: 'Clear all'
    },
    clearOnSelect: {
      type: Boolean,
      default: false
    },
    clearValueText: {
      type: String,
      default: 'Clear value'
    },
    closeOnSelect: {
      type: Boolean,
      default: true
    },
    defaultExpandLevel: {
      type: Number,
      default: 0
    },
    defaultOptions: {
      default: false
    },
    deleteRemoves: {
      type: Boolean,
      default: true
    },
    delimiter: {
      type: String,
      default: ','
    },
    flattenSearchResults: {
      type: Boolean,
      default: false
    },
    disableBranchNodes: {
      type: Boolean,
      default: false
    },
    disabled: {
      type: Boolean,
      default: false
    },
    disableFuzzyMatching: {
      type: Boolean,
      default: false
    },
    flat: {
      type: Boolean,
      default: false
    },
    instanceId: {
      default: function _default() {
        return "".concat(instanceId++, "$$");
      },
      type: [String, Number]
    },
    joinValues: {
      type: Boolean,
      default: false
    },
    limit: {
      type: Number,
      default: Infinity
    },
    limitText: {
      type: Function,
      default: function limitTextDefault(count) {
        return "and ".concat(count, " more");
      }
    },
    loadingText: {
      type: String,
      default: 'Loading...'
    },
    loadOptions: {
      type: Function
    },
    matchKeys: {
      type: Array,
      default: constant_default()(['label'])
    },
    maxHeight: {
      type: Number,
      default: 300
    },
    multiple: {
      type: Boolean,
      default: false
    },
    name: {
      type: String
    },
    noChildrenText: {
      type: String,
      default: 'No sub-options.'
    },
    noOptionsText: {
      type: String,
      default: 'No options available.'
    },
    noResultsText: {
      type: String,
      default: 'No results found...'
    },
    normalizer: {
      type: Function,
      default: identity_default.a
    },
    openDirection: {
      type: String,
      default: 'auto',
      validator: function validator(value) {
        var acceptableValues = ['auto', 'top', 'bottom', 'above', 'below'];
        return includes(acceptableValues, value);
      }
    },
    openOnClick: {
      type: Boolean,
      default: true
    },
    openOnFocus: {
      type: Boolean,
      default: false
    },
    options: {
      type: Array
    },
    placeholder: {
      type: String,
      default: 'Select...'
    },
    required: {
      type: Boolean,
      default: false
    },
    retryText: {
      type: String,
      default: 'Retry?'
    },
    retryTitle: {
      type: String,
      default: 'Click to retry'
    },
    searchable: {
      type: Boolean,
      default: true
    },
    searchNested: {
      type: Boolean,
      default: false
    },
    searchPromptText: {
      type: String,
      default: 'Type to search...'
    },
    showCount: {
      type: Boolean,
      default: false
    },
    showCountOf: {
      type: String,
      default: ALL_CHILDREN,
      validator: function validator(value) {
        var acceptableValues = [ALL_CHILDREN, ALL_DESCENDANTS, LEAF_CHILDREN, LEAF_DESCENDANTS];
        return includes(acceptableValues, value);
      }
    },
    showCountOnSearch: null,
    sortValueBy: {
      type: String,
      default: ORDER_SELECTED,
      validator: function validator(value) {
        var acceptableValues = [ORDER_SELECTED, LEVEL, INDEX];
        return includes(acceptableValues, value);
      }
    },
    tabIndex: {
      type: Number,
      default: 0
    },
    value: null,
    valueConsistsOf: {
      type: String,
      default: BRANCH_PRIORITY,
      validator: function validator(value) {
        var acceptableValues = [ALL, BRANCH_PRIORITY, LEAF_PRIORITY, ALL_WITH_INDETERMINATE];
        return includes(acceptableValues, value);
      }
    },
    valueFormat: {
      type: String,
      default: 'id'
    },
    zIndex: {
      type: [Number, String],
      default: 999
    }
  },
  data: function data() {
    return {
      trigger: {
        isFocused: false,
        searchQuery: ''
      },
      menu: {
        isOpen: false,
        current: null,
        lastScrollPosition: 0,
        placement: 'bottom'
      },
      forest: {
        normalizedOptions: [],
        nodeMap: createMap(),
        checkedStateMap: createMap(),
        selectedNodeIds: this.extractCheckedNodeIdsFromValue(),
        selectedNodeMap: createMap()
      },
      rootOptionsStates: createAsyncOptionsStates(),
      localSearch: {
        active: false,
        noResults: true,
        countMap: createMap()
      },
      remoteSearch: createMap()
    };
  },
  computed: {
    selectedNodes: function selectedNodes() {
      return this.forest.selectedNodeIds.map(this.getNode);
    },
    internalValue: function internalValue() {
      var _this = this;

      var internalValue;

      if (this.single || this.flat || this.disableBranchNodes || this.valueConsistsOf === ALL) {
        internalValue = this.forest.selectedNodeIds.slice();
      } else if (this.valueConsistsOf === BRANCH_PRIORITY) {
        internalValue = this.forest.selectedNodeIds.filter(function (id) {
          var node = _this.getNode(id);

          if (node.isRootNode) return true;
          return !_this.isSelected(node.parentNode);
        });
      } else if (this.valueConsistsOf === LEAF_PRIORITY) {
        internalValue = this.forest.selectedNodeIds.filter(function (id) {
          var node = _this.getNode(id);

          if (node.isLeaf) return true;
          return node.children.length === 0;
        });
      } else if (this.valueConsistsOf === ALL_WITH_INDETERMINATE) {
        var _internalValue;

        var indeterminateNodeIds = [];
        internalValue = this.forest.selectedNodeIds.slice();
        this.selectedNodes.forEach(function (selectedNode) {
          selectedNode.ancestors.forEach(function (ancestor) {
            if (includes(indeterminateNodeIds, ancestor.id)) return;
            if (includes(internalValue, ancestor.id)) return;
            indeterminateNodeIds.push(ancestor.id);
          });
        });

        (_internalValue = internalValue).push.apply(_internalValue, indeterminateNodeIds);
      }

      if (this.sortValueBy === LEVEL) {
        internalValue.sort(function (a, b) {
          return sortValueByLevel(_this.getNode(a), _this.getNode(b));
        });
      } else if (this.sortValueBy === INDEX) {
        internalValue.sort(function (a, b) {
          return sortValueByIndex(_this.getNode(a), _this.getNode(b));
        });
      }

      return internalValue;
    },
    hasValue: function hasValue() {
      return this.internalValue.length > 0;
    },
    single: function single() {
      return !this.multiple;
    },
    visibleOptionIds: function visibleOptionIds() {
      var _this2 = this;

      var visibleOptionIds = [];
      this.traverseAllNodesByIndex(function (node) {
        if (!_this2.localSearch.active || _this2.shouldOptionBeIncludedInSearchResult(node)) {
          visibleOptionIds.push(node.id);
        }

        if (node.isBranch && !_this2.shouldExpand(node)) {
          return false;
        }
      });
      return visibleOptionIds;
    },
    hasVisibleOptions: function hasVisibleOptions() {
      return this.visibleOptionIds.length !== 0;
    },
    showCountOnSearchComputed: function showCountOnSearchComputed() {
      return typeof this.showCountOnSearch === 'boolean' ? this.showCountOnSearch : this.showCount;
    },
    hasBranchNodes: function hasBranchNodes() {
      return this.forest.normalizedOptions.some(function (rootNode) {
        return rootNode.isBranch;
      });
    },
    shouldFlattenOptions: function shouldFlattenOptions() {
      return this.localSearch.active && this.flattenSearchResults;
    }
  },
  watch: {
    alwaysOpen: function alwaysOpen(newValue) {
      if (newValue) this.openMenu();else this.closeMenu();
    },
    branchNodesFirst: function branchNodesFirst() {
      this.initialize();
    },
    disabled: function disabled(newValue) {
      if (newValue && this.menu.isOpen) this.closeMenu();else if (!newValue && !this.menu.isOpen && this.alwaysOpen) this.openMenu();
    },
    flat: function flat() {
      this.initialize();
    },
    internalValue: function internalValue(newValue, oldValue) {
      var hasChanged = quickDiff(newValue, oldValue);
      if (hasChanged) this.$emit('input', this.getValue(), this.getInstanceId());
    },
    matchKeys: function matchKeys() {
      this.initialize();
    },
    multiple: function multiple(newValue) {
      if (newValue) this.buildForestState();
    },
    options: {
      handler: function handler() {
        if (this.async) return;
        this.initialize();
        this.rootOptionsStates.isLoaded = Array.isArray(this.options);
      },
      deep: true,
      immediate: true
    },
    'trigger.searchQuery': function triggerSearchQuery() {
      if (this.async) {
        this.handleRemoteSearch();
      } else {
        this.handleLocalSearch();
      }

      this.$emit('search-change', this.trigger.searchQuery, this.getInstanceId());
    },
    value: function value() {
      var nodeIdsFromValue = this.extractCheckedNodeIdsFromValue();
      var hasChanged = quickDiff(nodeIdsFromValue, this.internalValue);
      if (hasChanged) this.fixSelectedNodeIds(nodeIdsFromValue);
    }
  },
  methods: {
    verifyProps: function verifyProps() {
      var _this3 = this;

      warning_warning(function () {
        return _this3.async ? _this3.searchable : true;
      }, function () {
        return 'For async search mode, the value of "searchable" prop must be true.';
      });

      if (this.options == null && !this.loadOptions) {
        warning_warning(function () {
          return false;
        }, function () {
          return 'Are you meant to dynamically load options? You need to use "loadOptions" prop.';
        });
      }

      if (this.flat) {
        warning_warning(function () {
          return _this3.multiple;
        }, function () {
          return 'You are using flat mode. But you forgot to add "multiple=true"?';
        });
      }

      if (!this.flat) {
        var propNames = ['autoSelectAncestors', 'autoSelectDescendants', 'autoDeselectAncestors', 'autoDeselectDescendants'];
        propNames.forEach(function (propName) {
          warning_warning(function () {
            return !_this3[propName];
          }, function () {
            return "\"".concat(propName, "\" only applies to flat mode.");
          });
        });
      }
    },
    resetFlags: function resetFlags() {
      this._blurOnSelect = false;
    },
    initialize: function initialize() {
      var options = this.async ? this.getRemoteSearchEntry().options : this.options;

      if (Array.isArray(options)) {
        var prevNodeMap = this.forest.nodeMap;
        this.forest.nodeMap = createMap();
        this.keepDataOfSelectedNodes(prevNodeMap);
        this.forest.normalizedOptions = this.normalize(NO_PARENT_NODE, options, prevNodeMap);
        this.fixSelectedNodeIds(this.internalValue);
      } else {
        this.forest.normalizedOptions = [];
      }
    },
    getInstanceId: function getInstanceId() {
      return this.instanceId == null ? this.id : this.instanceId;
    },
    getValue: function getValue() {
      var _this4 = this;

      if (this.valueFormat === 'id') {
        return this.multiple ? this.internalValue.slice() : this.internalValue[0];
      }

      var rawNodes = this.internalValue.map(function (id) {
        return _this4.getNode(id).raw;
      });
      return this.multiple ? rawNodes : rawNodes[0];
    },
    getNode: function getNode(nodeId) {
      warning_warning(function () {
        return nodeId != null;
      }, function () {
        return "Invalid node id: ".concat(nodeId);
      });
      if (nodeId == null) return null;
      return nodeId in this.forest.nodeMap ? this.forest.nodeMap[nodeId] : this.createFallbackNode(nodeId);
    },
    createFallbackNode: function createFallbackNode(id) {
      var raw = this.extractNodeFromValue(id);
      var label = this.enhancedNormalizer(raw).label || "".concat(id, " (unknown)");
      var fallbackNode = {
        id: id,
        label: label,
        ancestors: [],
        parentNode: NO_PARENT_NODE,
        isFallbackNode: true,
        isRootNode: true,
        isLeaf: true,
        isBranch: false,
        isDisabled: false,
        isNew: false,
        index: [-1],
        level: 0,
        raw: raw
      };
      return this.$set(this.forest.nodeMap, id, fallbackNode);
    },
    extractCheckedNodeIdsFromValue: function extractCheckedNodeIdsFromValue() {
      var _this5 = this;

      if (this.value == null) return [];

      if (this.valueFormat === 'id') {
        return this.multiple ? this.value.slice() : [this.value];
      }

      return (this.multiple ? this.value : [this.value]).map(function (node) {
        return _this5.enhancedNormalizer(node);
      }).map(function (node) {
        return node.id;
      });
    },
    extractNodeFromValue: function extractNodeFromValue(id) {
      var _this6 = this;

      var defaultNode = {
        id: id
      };

      if (this.valueFormat === 'id') {
        return defaultNode;
      }

      var valueArray = this.multiple ? Array.isArray(this.value) ? this.value : [] : this.value ? [this.value] : [];
      var matched = find(valueArray, function (node) {
        return node && _this6.enhancedNormalizer(node).id === id;
      });
      return matched || defaultNode;
    },
    fixSelectedNodeIds: function fixSelectedNodeIds(nodeIdListOfPrevValue) {
      var _this7 = this;

      var nextSelectedNodeIds = [];

      if (this.single || this.flat || this.disableBranchNodes || this.valueConsistsOf === ALL) {
        nextSelectedNodeIds = nodeIdListOfPrevValue;
      } else if (this.valueConsistsOf === BRANCH_PRIORITY) {
        nodeIdListOfPrevValue.forEach(function (nodeId) {
          nextSelectedNodeIds.push(nodeId);

          var node = _this7.getNode(nodeId);

          if (node.isBranch) _this7.traverseDescendantsBFS(node, function (descendant) {
            nextSelectedNodeIds.push(descendant.id);
          });
        });
      } else if (this.valueConsistsOf === LEAF_PRIORITY) {
        var map = createMap();
        var queue = nodeIdListOfPrevValue.slice();

        while (queue.length) {
          var nodeId = queue.shift();
          var node = this.getNode(nodeId);
          nextSelectedNodeIds.push(nodeId);
          if (node.isRootNode) continue;
          if (!(node.parentNode.id in map)) map[node.parentNode.id] = node.parentNode.children.length;
          if (--map[node.parentNode.id] === 0) queue.push(node.parentNode.id);
        }
      } else if (this.valueConsistsOf === ALL_WITH_INDETERMINATE) {
        var _map = createMap();

        var _queue = nodeIdListOfPrevValue.filter(function (nodeId) {
          var node = _this7.getNode(nodeId);

          return node.isLeaf || node.children.length === 0;
        });

        while (_queue.length) {
          var _nodeId = _queue.shift();

          var _node = this.getNode(_nodeId);

          nextSelectedNodeIds.push(_nodeId);
          if (_node.isRootNode) continue;
          if (!(_node.parentNode.id in _map)) _map[_node.parentNode.id] = _node.parentNode.children.length;
          if (--_map[_node.parentNode.id] === 0) _queue.push(_node.parentNode.id);
        }
      }

      var hasChanged = quickDiff(this.forest.selectedNodeIds, nextSelectedNodeIds);
      if (hasChanged) this.forest.selectedNodeIds = nextSelectedNodeIds;
      this.buildForestState();
    },
    keepDataOfSelectedNodes: function keepDataOfSelectedNodes(prevNodeMap) {
      var _this8 = this;

      this.forest.selectedNodeIds.forEach(function (id) {
        if (!prevNodeMap[id]) return;

        var node = _objectSpread({}, prevNodeMap[id], {
          isFallbackNode: true
        });

        _this8.$set(_this8.forest.nodeMap, id, node);
      });
    },
    isSelected: function isSelected(node) {
      return this.forest.selectedNodeMap[node.id] === true;
    },
    traverseDescendantsBFS: function traverseDescendantsBFS(parentNode, callback) {
      if (!parentNode.isBranch) return;
      var queue = parentNode.children.slice();

      while (queue.length) {
        var currNode = queue[0];
        if (currNode.isBranch) queue.push.apply(queue, toConsumableArray_default()(currNode.children));
        callback(currNode);
        queue.shift();
      }
    },
    traverseDescendantsDFS: function traverseDescendantsDFS(parentNode, callback) {
      var _this9 = this;

      if (!parentNode.isBranch) return;
      parentNode.children.forEach(function (child) {
        _this9.traverseDescendantsDFS(child, callback);

        callback(child);
      });
    },
    traverseAllNodesDFS: function traverseAllNodesDFS(callback) {
      var _this10 = this;

      this.forest.normalizedOptions.forEach(function (rootNode) {
        _this10.traverseDescendantsDFS(rootNode, callback);

        callback(rootNode);
      });
    },
    traverseAllNodesByIndex: function traverseAllNodesByIndex(callback) {
      var walk = function walk(parentNode) {
        parentNode.children.forEach(function (child) {
          if (callback(child) !== false && child.isBranch) {
            walk(child);
          }
        });
      };

      walk({
        children: this.forest.normalizedOptions
      });
    },
    toggleClickOutsideEvent: function toggleClickOutsideEvent(enabled) {
      if (enabled) {
        document.addEventListener('mousedown', this.handleClickOutside, false);
      } else {
        document.removeEventListener('mousedown', this.handleClickOutside, false);
      }
    },
    getValueContainer: function getValueContainer() {
      return this.$refs.control.$refs['value-container'];
    },
    getInput: function getInput() {
      return this.getValueContainer().$refs.input;
    },
    focusInput: function focusInput() {
      this.getInput().focus();
    },
    blurInput: function blurInput() {
      this.getInput().blur();
    },
    handleMouseDown: onLeftClick(function handleMouseDown(evt) {
      evt.preventDefault();
      evt.stopPropagation();
      if (this.disabled) return;
      var isClickedOnValueContainer = this.getValueContainer().$el.contains(evt.target);

      if (isClickedOnValueContainer && !this.menu.isOpen && (this.openOnClick || this.trigger.isFocused)) {
        this.openMenu();
      }

      if (this._blurOnSelect) {
        this.blurInput();
      } else {
        this.focusInput();
      }

      this.resetFlags();
    }),
    handleClickOutside: function handleClickOutside(evt) {
      if (this.$refs.wrapper && !this.$refs.wrapper.contains(evt.target)) {
        this.blurInput();
        this.closeMenu();
      }
    },
    handleLocalSearch: function handleLocalSearch() {
      var _this11 = this;

      var searchQuery = this.trigger.searchQuery;

      var done = function done() {
        return _this11.resetHighlightedOptionWhenNecessary(true);
      };

      if (!searchQuery) {
        this.localSearch.active = false;
        return done();
      }

      this.localSearch.active = true;
      this.localSearch.noResults = true;
      this.traverseAllNodesDFS(function (node) {
        if (node.isBranch) {
          var _this11$$set;

          node.isExpandedOnSearch = false;
          node.showAllChildrenOnSearch = false;
          node.isMatched = false;
          node.hasMatchedDescendants = false;

          _this11.$set(_this11.localSearch.countMap, node.id, (_this11$$set = {}, defineProperty_default()(_this11$$set, ALL_CHILDREN, 0), defineProperty_default()(_this11$$set, ALL_DESCENDANTS, 0), defineProperty_default()(_this11$$set, LEAF_CHILDREN, 0), defineProperty_default()(_this11$$set, LEAF_DESCENDANTS, 0), _this11$$set));
        }
      });
      var lowerCasedSearchQuery = searchQuery.trim().toLocaleLowerCase();
      var splitSearchQuery = lowerCasedSearchQuery.replace(/\s+/g, ' ').split(' ');
      this.traverseAllNodesDFS(function (node) {
        if (_this11.searchNested && splitSearchQuery.length > 1) {
          node.isMatched = splitSearchQuery.every(function (filterValue) {
            return match(false, filterValue, node.nestedSearchLabel);
          });
        } else {
          node.isMatched = _this11.matchKeys.some(function (matchKey) {
            return match(!_this11.disableFuzzyMatching, lowerCasedSearchQuery, node.lowerCased[matchKey]);
          });
        }

        if (node.isMatched) {
          _this11.localSearch.noResults = false;
          node.ancestors.forEach(function (ancestor) {
            return _this11.localSearch.countMap[ancestor.id][ALL_DESCENDANTS]++;
          });
          if (node.isLeaf) node.ancestors.forEach(function (ancestor) {
            return _this11.localSearch.countMap[ancestor.id][LEAF_DESCENDANTS]++;
          });

          if (node.parentNode !== NO_PARENT_NODE) {
            _this11.localSearch.countMap[node.parentNode.id][ALL_CHILDREN] += 1;
            if (node.isLeaf) _this11.localSearch.countMap[node.parentNode.id][LEAF_CHILDREN] += 1;
          }
        }

        if ((node.isMatched || node.isBranch && node.isExpandedOnSearch) && node.parentNode !== NO_PARENT_NODE) {
          node.parentNode.isExpandedOnSearch = true;
          node.parentNode.hasMatchedDescendants = true;
        }
      });
      done();
    },
    handleRemoteSearch: function handleRemoteSearch() {
      var _this12 = this;

      var searchQuery = this.trigger.searchQuery;
      var entry = this.getRemoteSearchEntry();

      var done = function done() {
        _this12.initialize();

        _this12.resetHighlightedOptionWhenNecessary(true);
      };

      if ((searchQuery === '' || this.cacheOptions) && entry.isLoaded) {
        return done();
      }

      this.callLoadOptionsProp({
        action: ASYNC_SEARCH,
        args: {
          searchQuery: searchQuery
        },
        isPending: function isPending() {
          return entry.isLoading;
        },
        start: function start() {
          entry.isLoading = true;
          entry.isLoaded = false;
          entry.loadingError = '';
        },
        succeed: function succeed(options) {
          entry.isLoaded = true;
          entry.options = options;
          if (_this12.trigger.searchQuery === searchQuery) done();
        },
        fail: function fail(err) {
          entry.loadingError = getErrorMessage(err);
        },
        end: function end() {
          entry.isLoading = false;
        }
      });
    },
    getRemoteSearchEntry: function getRemoteSearchEntry() {
      var _this13 = this;

      var searchQuery = this.trigger.searchQuery;

      var entry = this.remoteSearch[searchQuery] || _objectSpread({}, createAsyncOptionsStates(), {
        options: []
      });

      this.$watch(function () {
        return entry.options;
      }, function () {
        if (_this13.trigger.searchQuery === searchQuery) _this13.initialize();
      }, {
        deep: true
      });

      if (searchQuery === '') {
        if (Array.isArray(this.defaultOptions)) {
          entry.options = this.defaultOptions;
          entry.isLoaded = true;
          return entry;
        } else if (this.defaultOptions !== true) {
          entry.isLoaded = true;
          return entry;
        }
      }

      if (!this.remoteSearch[searchQuery]) {
        this.$set(this.remoteSearch, searchQuery, entry);
      }

      return entry;
    },
    shouldExpand: function shouldExpand(node) {
      return this.localSearch.active ? node.isExpandedOnSearch : node.isExpanded;
    },
    shouldOptionBeIncludedInSearchResult: function shouldOptionBeIncludedInSearchResult(node) {
      if (node.isMatched) return true;
      if (node.isBranch && node.hasMatchedDescendants && !this.flattenSearchResults) return true;
      if (!node.isRootNode && node.parentNode.showAllChildrenOnSearch) return true;
      return false;
    },
    shouldShowOptionInMenu: function shouldShowOptionInMenu(node) {
      if (this.localSearch.active && !this.shouldOptionBeIncludedInSearchResult(node)) {
        return false;
      }

      return true;
    },
    getControl: function getControl() {
      return this.$refs.control.$el;
    },
    getMenu: function getMenu() {
      var ref = this.appendToBody ? this.$refs.portal.portalTarget : this;
      var $menu = ref.$refs.menu.$refs.menu;
      return $menu && $menu.nodeName !== '#comment' ? $menu : null;
    },
    setCurrentHighlightedOption: function setCurrentHighlightedOption(node) {
      var _this14 = this;

      var scroll = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : true;
      var prev = this.menu.current;

      if (prev != null && prev in this.forest.nodeMap) {
        this.forest.nodeMap[prev].isHighlighted = false;
      }

      this.menu.current = node.id;
      node.isHighlighted = true;

      if (this.menu.isOpen && scroll) {
        var scrollToOption = function scrollToOption() {
          var $menu = _this14.getMenu();

          var $option = $menu.querySelector(".vue-treeselect__option[data-id=\"".concat(node.id, "\"]"));
          if ($option) scrollIntoView($menu, $option);
        };

        if (this.getMenu()) {
          scrollToOption();
        } else {
          this.$nextTick(scrollToOption);
        }
      }
    },
    resetHighlightedOptionWhenNecessary: function resetHighlightedOptionWhenNecessary() {
      var forceReset = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : false;
      var current = this.menu.current;

      if (forceReset || current == null || !(current in this.forest.nodeMap) || !this.shouldShowOptionInMenu(this.getNode(current))) {
        this.highlightFirstOption();
      }
    },
    highlightFirstOption: function highlightFirstOption() {
      if (!this.hasVisibleOptions) return;
      var first = this.visibleOptionIds[0];
      this.setCurrentHighlightedOption(this.getNode(first));
    },
    highlightPrevOption: function highlightPrevOption() {
      if (!this.hasVisibleOptions) return;
      var prev = this.visibleOptionIds.indexOf(this.menu.current) - 1;
      if (prev === -1) return this.highlightLastOption();
      this.setCurrentHighlightedOption(this.getNode(this.visibleOptionIds[prev]));
    },
    highlightNextOption: function highlightNextOption() {
      if (!this.hasVisibleOptions) return;
      var next = this.visibleOptionIds.indexOf(this.menu.current) + 1;
      if (next === this.visibleOptionIds.length) return this.highlightFirstOption();
      this.setCurrentHighlightedOption(this.getNode(this.visibleOptionIds[next]));
    },
    highlightLastOption: function highlightLastOption() {
      if (!this.hasVisibleOptions) return;
      var last = last_default()(this.visibleOptionIds);
      this.setCurrentHighlightedOption(this.getNode(last));
    },
    resetSearchQuery: function resetSearchQuery() {
      this.trigger.searchQuery = '';
    },
    closeMenu: function closeMenu() {
      if (!this.menu.isOpen || !this.disabled && this.alwaysOpen) return;
      this.saveMenuScrollPosition();
      this.menu.isOpen = false;
      this.toggleClickOutsideEvent(false);
      this.resetSearchQuery();
      this.$emit('close', this.getValue(), this.getInstanceId());
    },
    openMenu: function openMenu() {
      if (this.disabled || this.menu.isOpen) return;
      this.menu.isOpen = true;
      this.$nextTick(this.resetHighlightedOptionWhenNecessary);
      this.$nextTick(this.restoreMenuScrollPosition);
      if (!this.options && !this.async) this.loadRootOptions();
      this.toggleClickOutsideEvent(true);
      this.$emit('open', this.getInstanceId());
    },
    toggleMenu: function toggleMenu() {
      if (this.menu.isOpen) {
        this.closeMenu();
      } else {
        this.openMenu();
      }
    },
    toggleExpanded: function toggleExpanded(node) {
      var nextState;

      if (this.localSearch.active) {
        nextState = node.isExpandedOnSearch = !node.isExpandedOnSearch;
        if (nextState) node.showAllChildrenOnSearch = true;
      } else {
        nextState = node.isExpanded = !node.isExpanded;
      }

      if (nextState && !node.childrenStates.isLoaded) {
        this.loadChildrenOptions(node);
      }
    },
    buildForestState: function buildForestState() {
      var _this15 = this;

      var selectedNodeMap = createMap();
      this.forest.selectedNodeIds.forEach(function (selectedNodeId) {
        selectedNodeMap[selectedNodeId] = true;
      });
      this.forest.selectedNodeMap = selectedNodeMap;
      var checkedStateMap = createMap();

      if (this.multiple) {
        this.traverseAllNodesByIndex(function (node) {
          checkedStateMap[node.id] = UNCHECKED;
        });
        this.selectedNodes.forEach(function (selectedNode) {
          checkedStateMap[selectedNode.id] = CHECKED;

          if (!_this15.flat && !_this15.disableBranchNodes) {
            selectedNode.ancestors.forEach(function (ancestorNode) {
              if (!_this15.isSelected(ancestorNode)) {
                checkedStateMap[ancestorNode.id] = INDETERMINATE;
              }
            });
          }
        });
      }

      this.forest.checkedStateMap = checkedStateMap;
    },
    enhancedNormalizer: function enhancedNormalizer(raw) {
      return _objectSpread({}, raw, {}, this.normalizer(raw, this.getInstanceId()));
    },
    normalize: function normalize(parentNode, nodes, prevNodeMap) {
      var _this16 = this;

      var normalizedOptions = nodes.map(function (node) {
        return [_this16.enhancedNormalizer(node), node];
      }).map(function (_ref, index) {
        var _ref2 = slicedToArray_default()(_ref, 2),
            node = _ref2[0],
            raw = _ref2[1];

        _this16.checkDuplication(node);

        _this16.verifyNodeShape(node);

        var id = node.id,
            label = node.label,
            children = node.children,
            isDefaultExpanded = node.isDefaultExpanded;
        var isRootNode = parentNode === NO_PARENT_NODE;
        var level = isRootNode ? 0 : parentNode.level + 1;
        var isBranch = Array.isArray(children) || children === null;
        var isLeaf = !isBranch;
        var isDisabled = !!node.isDisabled || !_this16.flat && !isRootNode && parentNode.isDisabled;
        var isNew = !!node.isNew;

        var lowerCased = _this16.matchKeys.reduce(function (prev, key) {
          return _objectSpread({}, prev, defineProperty_default()({}, key, stringifyOptionPropValue(node[key]).toLocaleLowerCase()));
        }, {});

        var nestedSearchLabel = isRootNode ? lowerCased.label : parentNode.nestedSearchLabel + ' ' + lowerCased.label;

        var normalized = _this16.$set(_this16.forest.nodeMap, id, createMap());

        _this16.$set(normalized, 'id', id);

        _this16.$set(normalized, 'label', label);

        _this16.$set(normalized, 'level', level);

        _this16.$set(normalized, 'ancestors', isRootNode ? [] : [parentNode].concat(parentNode.ancestors));

        _this16.$set(normalized, 'index', (isRootNode ? [] : parentNode.index).concat(index));

        _this16.$set(normalized, 'parentNode', parentNode);

        _this16.$set(normalized, 'lowerCased', lowerCased);

        _this16.$set(normalized, 'nestedSearchLabel', nestedSearchLabel);

        _this16.$set(normalized, 'isDisabled', isDisabled);

        _this16.$set(normalized, 'isNew', isNew);

        _this16.$set(normalized, 'isMatched', false);

        _this16.$set(normalized, 'isHighlighted', false);

        _this16.$set(normalized, 'isBranch', isBranch);

        _this16.$set(normalized, 'isLeaf', isLeaf);

        _this16.$set(normalized, 'isRootNode', isRootNode);

        _this16.$set(normalized, 'raw', raw);

        if (isBranch) {
          var _this16$$set;

          var isLoaded = Array.isArray(children);

          _this16.$set(normalized, 'childrenStates', _objectSpread({}, createAsyncOptionsStates(), {
            isLoaded: isLoaded
          }));

          _this16.$set(normalized, 'isExpanded', typeof isDefaultExpanded === 'boolean' ? isDefaultExpanded : level < _this16.defaultExpandLevel);

          _this16.$set(normalized, 'hasMatchedDescendants', false);

          _this16.$set(normalized, 'hasDisabledDescendants', false);

          _this16.$set(normalized, 'isExpandedOnSearch', false);

          _this16.$set(normalized, 'showAllChildrenOnSearch', false);

          _this16.$set(normalized, 'count', (_this16$$set = {}, defineProperty_default()(_this16$$set, ALL_CHILDREN, 0), defineProperty_default()(_this16$$set, ALL_DESCENDANTS, 0), defineProperty_default()(_this16$$set, LEAF_CHILDREN, 0), defineProperty_default()(_this16$$set, LEAF_DESCENDANTS, 0), _this16$$set));

          _this16.$set(normalized, 'children', isLoaded ? _this16.normalize(normalized, children, prevNodeMap) : []);

          if (isDefaultExpanded === true) normalized.ancestors.forEach(function (ancestor) {
            ancestor.isExpanded = true;
          });

          if (!isLoaded && typeof _this16.loadOptions !== 'function') {
            warning_warning(function () {
              return false;
            }, function () {
              return 'Unloaded branch node detected. "loadOptions" prop is required to load its children.';
            });
          } else if (!isLoaded && normalized.isExpanded) {
            _this16.loadChildrenOptions(normalized);
          }
        }

        normalized.ancestors.forEach(function (ancestor) {
          return ancestor.count[ALL_DESCENDANTS]++;
        });
        if (isLeaf) normalized.ancestors.forEach(function (ancestor) {
          return ancestor.count[LEAF_DESCENDANTS]++;
        });

        if (!isRootNode) {
          parentNode.count[ALL_CHILDREN] += 1;
          if (isLeaf) parentNode.count[LEAF_CHILDREN] += 1;
          if (isDisabled) parentNode.hasDisabledDescendants = true;
        }

        if (prevNodeMap && prevNodeMap[id]) {
          var prev = prevNodeMap[id];
          normalized.isMatched = prev.isMatched;
          normalized.showAllChildrenOnSearch = prev.showAllChildrenOnSearch;
          normalized.isHighlighted = prev.isHighlighted;

          if (prev.isBranch && normalized.isBranch) {
            normalized.isExpanded = prev.isExpanded;
            normalized.isExpandedOnSearch = prev.isExpandedOnSearch;

            if (prev.childrenStates.isLoaded && !normalized.childrenStates.isLoaded) {
              normalized.isExpanded = false;
            } else {
              normalized.childrenStates = _objectSpread({}, prev.childrenStates);
            }
          }
        }

        return normalized;
      });

      if (this.branchNodesFirst) {
        var branchNodes = normalizedOptions.filter(function (option) {
          return option.isBranch;
        });
        var leafNodes = normalizedOptions.filter(function (option) {
          return option.isLeaf;
        });
        normalizedOptions = branchNodes.concat(leafNodes);
      }

      return normalizedOptions;
    },
    loadRootOptions: function loadRootOptions() {
      var _this17 = this;

      this.callLoadOptionsProp({
        action: LOAD_ROOT_OPTIONS,
        isPending: function isPending() {
          return _this17.rootOptionsStates.isLoading;
        },
        start: function start() {
          _this17.rootOptionsStates.isLoading = true;
          _this17.rootOptionsStates.loadingError = '';
        },
        succeed: function succeed() {
          _this17.rootOptionsStates.isLoaded = true;

          _this17.$nextTick(function () {
            _this17.resetHighlightedOptionWhenNecessary(true);
          });
        },
        fail: function fail(err) {
          _this17.rootOptionsStates.loadingError = getErrorMessage(err);
        },
        end: function end() {
          _this17.rootOptionsStates.isLoading = false;
        }
      });
    },
    loadChildrenOptions: function loadChildrenOptions(parentNode) {
      var _this18 = this;

      var id = parentNode.id,
          raw = parentNode.raw;
      this.callLoadOptionsProp({
        action: LOAD_CHILDREN_OPTIONS,
        args: {
          parentNode: raw
        },
        isPending: function isPending() {
          return _this18.getNode(id).childrenStates.isLoading;
        },
        start: function start() {
          _this18.getNode(id).childrenStates.isLoading = true;
          _this18.getNode(id).childrenStates.loadingError = '';
        },
        succeed: function succeed() {
          _this18.getNode(id).childrenStates.isLoaded = true;
        },
        fail: function fail(err) {
          _this18.getNode(id).childrenStates.loadingError = getErrorMessage(err);
        },
        end: function end() {
          _this18.getNode(id).childrenStates.isLoading = false;
        }
      });
    },
    callLoadOptionsProp: function callLoadOptionsProp(_ref3) {
      var action = _ref3.action,
          args = _ref3.args,
          isPending = _ref3.isPending,
          start = _ref3.start,
          succeed = _ref3.succeed,
          fail = _ref3.fail,
          end = _ref3.end;

      if (!this.loadOptions || isPending()) {
        return;
      }

      start();
      var callback = once_default()(function (err, result) {
        if (err) {
          fail(err);
        } else {
          succeed(result);
        }

        end();
      });
      var result = this.loadOptions(_objectSpread({
        id: this.getInstanceId(),
        instanceId: this.getInstanceId(),
        action: action
      }, args, {
        callback: callback
      }));

      if (external_is_promise_default()(result)) {
        result.then(function () {
          callback();
        }, function (err) {
          callback(err);
        }).catch(function (err) {
          console.error(err);
        });
      }
    },
    checkDuplication: function checkDuplication(node) {
      var _this19 = this;

      warning_warning(function () {
        return !(node.id in _this19.forest.nodeMap && !_this19.forest.nodeMap[node.id].isFallbackNode);
      }, function () {
        return "Detected duplicate presence of node id ".concat(JSON.stringify(node.id), ". ") + "Their labels are \"".concat(_this19.forest.nodeMap[node.id].label, "\" and \"").concat(node.label, "\" respectively.");
      });
    },
    verifyNodeShape: function verifyNodeShape(node) {
      warning_warning(function () {
        return !(node.children === undefined && node.isBranch === true);
      }, function () {
        return 'Are you meant to declare an unloaded branch node? ' + '`isBranch: true` is no longer supported, please use `children: null` instead.';
      });
    },
    select: function select(node) {
      if (this.disabled || node.isDisabled) {
        return;
      }

      if (this.single) {
        this.clear();
      }

      var nextState = this.multiple && !this.flat ? this.forest.checkedStateMap[node.id] === UNCHECKED : !this.isSelected(node);

      if (nextState) {
        this._selectNode(node);
      } else {
        this._deselectNode(node);
      }

      this.buildForestState();

      if (nextState) {
        this.$emit('select', node.raw, this.getInstanceId());
      } else {
        this.$emit('deselect', node.raw, this.getInstanceId());
      }

      if (this.localSearch.active && nextState && (this.single || this.clearOnSelect)) {
        this.resetSearchQuery();
      }

      if (this.single && this.closeOnSelect) {
        this.closeMenu();

        if (this.searchable) {
          this._blurOnSelect = true;
        }
      }
    },
    clear: function clear() {
      var _this20 = this;

      if (this.hasValue) {
        if (this.single || this.allowClearingDisabled) {
          this.forest.selectedNodeIds = [];
        } else {
            this.forest.selectedNodeIds = this.forest.selectedNodeIds.filter(function (nodeId) {
              return _this20.getNode(nodeId).isDisabled;
            });
          }

        this.buildForestState();
      }
    },
    _selectNode: function _selectNode(node) {
      var _this21 = this;

      if (this.single || this.disableBranchNodes) {
        return this.addValue(node);
      }

      if (this.flat) {
        this.addValue(node);

        if (this.autoSelectAncestors) {
          node.ancestors.forEach(function (ancestor) {
            if (!_this21.isSelected(ancestor) && !ancestor.isDisabled) _this21.addValue(ancestor);
          });
        } else if (this.autoSelectDescendants) {
          this.traverseDescendantsBFS(node, function (descendant) {
            if (!_this21.isSelected(descendant) && !descendant.isDisabled) _this21.addValue(descendant);
          });
        }

        return;
      }

      var isFullyChecked = node.isLeaf || !node.hasDisabledDescendants || this.allowSelectingDisabledDescendants;

      if (isFullyChecked) {
        this.addValue(node);
      }

      if (node.isBranch) {
        this.traverseDescendantsBFS(node, function (descendant) {
          if (!descendant.isDisabled || _this21.allowSelectingDisabledDescendants) {
            _this21.addValue(descendant);
          }
        });
      }

      if (isFullyChecked) {
        var curr = node;

        while ((curr = curr.parentNode) !== NO_PARENT_NODE) {
          if (curr.children.every(this.isSelected)) this.addValue(curr);else break;
        }
      }
    },
    _deselectNode: function _deselectNode(node) {
      var _this22 = this;

      if (this.disableBranchNodes) {
        return this.removeValue(node);
      }

      if (this.flat) {
        this.removeValue(node);

        if (this.autoDeselectAncestors) {
          node.ancestors.forEach(function (ancestor) {
            if (_this22.isSelected(ancestor) && !ancestor.isDisabled) _this22.removeValue(ancestor);
          });
        } else if (this.autoDeselectDescendants) {
          this.traverseDescendantsBFS(node, function (descendant) {
            if (_this22.isSelected(descendant) && !descendant.isDisabled) _this22.removeValue(descendant);
          });
        }

        return;
      }

      var hasUncheckedSomeDescendants = false;

      if (node.isBranch) {
        this.traverseDescendantsDFS(node, function (descendant) {
          if (!descendant.isDisabled || _this22.allowSelectingDisabledDescendants) {
            _this22.removeValue(descendant);

            hasUncheckedSomeDescendants = true;
          }
        });
      }

      if (node.isLeaf || hasUncheckedSomeDescendants || node.children.length === 0) {
        this.removeValue(node);
        var curr = node;

        while ((curr = curr.parentNode) !== NO_PARENT_NODE) {
          if (this.isSelected(curr)) this.removeValue(curr);else break;
        }
      }
    },
    addValue: function addValue(node) {
      this.forest.selectedNodeIds.push(node.id);
      this.forest.selectedNodeMap[node.id] = true;
    },
    removeValue: function removeValue(node) {
      removeFromArray(this.forest.selectedNodeIds, node.id);
      delete this.forest.selectedNodeMap[node.id];
    },
    removeLastValue: function removeLastValue() {
      if (!this.hasValue) return;
      if (this.single) return this.clear();
      var lastValue = last_default()(this.internalValue);
      var lastSelectedNode = this.getNode(lastValue);
      this.select(lastSelectedNode);
    },
    saveMenuScrollPosition: function saveMenuScrollPosition() {
      var $menu = this.getMenu();
      if ($menu) this.menu.lastScrollPosition = $menu.scrollTop;
    },
    restoreMenuScrollPosition: function restoreMenuScrollPosition() {
      var $menu = this.getMenu();
      if ($menu) $menu.scrollTop = this.menu.lastScrollPosition;
    }
  },
  created: function created() {
    this.verifyProps();
    this.resetFlags();
  },
  mounted: function mounted() {
    if (this.autoFocus) this.focusInput();
    if (!this.options && !this.async && this.autoLoadRootOptions) this.loadRootOptions();
    if (this.alwaysOpen) this.openMenu();
    if (this.async && this.defaultOptions) this.handleRemoteSearch();
  },
  destroyed: function destroyed() {
    this.toggleClickOutsideEvent(false);
  }
});
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/HiddenFields.vue?vue&type=script&lang=js&


function stringifyValue(value) {
  if (typeof value === 'string') return value;
  if (value != null && !isNaN_isNaN(value)) return JSON.stringify(value);
  return '';
}

/* harmony default export */ var HiddenFieldsvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--hidden-fields',
  inject: ['instance'],
  functional: true,
  render: function render(_, context) {
    var h = arguments[0];
    var instance = context.injections.instance;
    if (!instance.name || instance.disabled || !instance.hasValue) return null;
    var stringifiedValues = instance.internalValue.map(stringifyValue);
    if (instance.multiple && instance.joinValues) stringifiedValues = [stringifiedValues.join(instance.delimiter)];
    return stringifiedValues.map(function (stringifiedValue, i) {
      return h("input", {
        attrs: {
          type: "hidden",
          name: instance.name
        },
        domProps: {
          "value": stringifiedValue
        },
        key: 'hidden-field-' + i
      });
    });
  }
});
// CONCATENATED MODULE: ./src/components/HiddenFields.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_HiddenFieldsvue_type_script_lang_js_ = (HiddenFieldsvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./node_modules/vue-loader/lib/runtime/componentNormalizer.js
/* globals __VUE_SSR_CONTEXT__ */

// IMPORTANT: Do NOT use ES2015 features in this file (except for modules).
// This module is a runtime utility for cleaner component module output and will
// be included in the final webpack user bundle.

function normalizeComponent (
  scriptExports,
  render,
  staticRenderFns,
  functionalTemplate,
  injectStyles,
  scopeId,
  moduleIdentifier, /* server only */
  shadowMode /* vue-cli only */
) {
  // Vue.extend constructor export interop
  var options = typeof scriptExports === 'function'
    ? scriptExports.options
    : scriptExports

  // render functions
  if (render) {
    options.render = render
    options.staticRenderFns = staticRenderFns
    options._compiled = true
  }

  // functional template
  if (functionalTemplate) {
    options.functional = true
  }

  // scopedId
  if (scopeId) {
    options._scopeId = 'data-v-' + scopeId
  }

  var hook
  if (moduleIdentifier) { // server build
    hook = function (context) {
      // 2.3 injection
      context =
        context || // cached call
        (this.$vnode && this.$vnode.ssrContext) || // stateful
        (this.parent && this.parent.$vnode && this.parent.$vnode.ssrContext) // functional
      // 2.2 with runInNewContext: true
      if (!context && typeof __VUE_SSR_CONTEXT__ !== 'undefined') {
        context = __VUE_SSR_CONTEXT__
      }
      // inject component styles
      if (injectStyles) {
        injectStyles.call(this, context)
      }
      // register component module identifier for async chunk inferrence
      if (context && context._registeredComponents) {
        context._registeredComponents.add(moduleIdentifier)
      }
    }
    // used by ssr in case component is cached and beforeCreate
    // never gets called
    options._ssrRegister = hook
  } else if (injectStyles) {
    hook = shadowMode
      ? function () { injectStyles.call(this, this.$root.$options.shadowRoot) }
      : injectStyles
  }

  if (hook) {
    if (options.functional) {
      // for template-only hot-reload because in that case the render fn doesn't
      // go through the normalizer
      options._injectStyles = hook
      // register for functioal component in vue file
      var originalRender = options.render
      options.render = function renderWithStyleInjection (h, context) {
        hook.call(context)
        return originalRender(h, context)
      }
    } else {
      // inject component registration as beforeCreate hook
      var existing = options.beforeCreate
      options.beforeCreate = existing
        ? [].concat(existing, hook)
        : [hook]
    }
  }

  return {
    exports: scriptExports,
    options: options
  }
}

// CONCATENATED MODULE: ./src/components/HiddenFields.vue
var HiddenFields_render, staticRenderFns




/* normalize component */

var component = normalizeComponent(
  components_HiddenFieldsvue_type_script_lang_js_,
  HiddenFields_render,
  staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var api; }
component.options.__file = "src/components/HiddenFields.vue"
/* harmony default export */ var HiddenFields = (component.exports);
// EXTERNAL MODULE: external "babel-helper-vue-jsx-merge-props"
var external_babel_helper_vue_jsx_merge_props_ = __webpack_require__(13);
var external_babel_helper_vue_jsx_merge_props_default = /*#__PURE__*/__webpack_require__.n(external_babel_helper_vue_jsx_merge_props_);

// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Input.vue?vue&type=script&lang=js&



var keysThatRequireMenuBeingOpen = [KEY_CODES.ENTER, KEY_CODES.END, KEY_CODES.HOME, KEY_CODES.ARROW_LEFT, KEY_CODES.ARROW_UP, KEY_CODES.ARROW_RIGHT, KEY_CODES.ARROW_DOWN];
/* harmony default export */ var Inputvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--input',
  inject: ['instance'],
  data: function data() {
    return {
      inputWidth: MIN_INPUT_WIDTH,
      value: ''
    };
  },
  computed: {
    needAutoSize: function needAutoSize() {
      var instance = this.instance;
      return instance.searchable && !instance.disabled && instance.multiple;
    },
    inputStyle: function inputStyle() {
      return {
        width: this.needAutoSize ? "".concat(this.inputWidth, "px") : null
      };
    }
  },
  watch: {
    'instance.trigger.searchQuery': function instanceTriggerSearchQuery(newValue) {
      this.value = newValue;
    },
    value: function value() {
      if (this.needAutoSize) this.$nextTick(this.updateInputWidth);
    }
  },
  created: function created() {
    this.debouncedCallback = debounce_default()(this.updateSearchQuery, INPUT_DEBOUNCE_DELAY, {
      leading: true,
      trailing: true
    });
  },
  methods: {
    clear: function clear() {
      this.onInput({
        target: {
          value: ''
        }
      });
    },
    focus: function focus() {
      var instance = this.instance;

      if (!instance.disabled) {
        this.$refs.input && this.$refs.input.focus();
      }
    },
    blur: function blur() {
      this.$refs.input && this.$refs.input.blur();
    },
    onFocus: function onFocus() {
      var instance = this.instance;
      instance.trigger.isFocused = true;
      if (instance.openOnFocus) instance.openMenu();
    },
    onBlur: function onBlur() {
      var instance = this.instance;
      var menu = instance.getMenu();

      if (menu && document.activeElement === menu) {
        return this.focus();
      }

      instance.trigger.isFocused = false;
      instance.closeMenu();
    },
    onInput: function onInput(evt) {
      var value = evt.target.value;
      this.value = value;

      if (value) {
        this.debouncedCallback();
      } else {
        this.debouncedCallback.cancel();
        this.updateSearchQuery();
      }
    },
    onKeyDown: function onKeyDown(evt) {
      var instance = this.instance;
      var key = 'which' in evt ? evt.which : evt.keyCode;
      if (evt.ctrlKey || evt.shiftKey || evt.altKey || evt.metaKey) return;

      if (!instance.menu.isOpen && includes(keysThatRequireMenuBeingOpen, key)) {
        evt.preventDefault();
        return instance.openMenu();
      }

      switch (key) {
        case KEY_CODES.BACKSPACE:
          {
            if (instance.backspaceRemoves && !this.value.length) {
              instance.removeLastValue();
            }

            break;
          }

        case KEY_CODES.ENTER:
          {
            evt.preventDefault();
            if (instance.menu.current === null) return;
            var current = instance.getNode(instance.menu.current);
            if (current.isBranch && instance.disableBranchNodes) return;
            instance.select(current);
            break;
          }

        case KEY_CODES.ESCAPE:
          {
            if (this.value.length) {
              this.clear();
            } else if (instance.menu.isOpen) {
              instance.closeMenu();
            }

            break;
          }

        case KEY_CODES.END:
          {
            evt.preventDefault();
            instance.highlightLastOption();
            break;
          }

        case KEY_CODES.HOME:
          {
            evt.preventDefault();
            instance.highlightFirstOption();
            break;
          }

        case KEY_CODES.ARROW_LEFT:
          {
            var _current = instance.getNode(instance.menu.current);

            if (_current.isBranch && instance.shouldExpand(_current)) {
              evt.preventDefault();
              instance.toggleExpanded(_current);
            } else if (!_current.isRootNode && (_current.isLeaf || _current.isBranch && !instance.shouldExpand(_current))) {
              evt.preventDefault();
              instance.setCurrentHighlightedOption(_current.parentNode);
            }

            break;
          }

        case KEY_CODES.ARROW_UP:
          {
            evt.preventDefault();
            instance.highlightPrevOption();
            break;
          }

        case KEY_CODES.ARROW_RIGHT:
          {
            var _current2 = instance.getNode(instance.menu.current);

            if (_current2.isBranch && !instance.shouldExpand(_current2)) {
              evt.preventDefault();
              instance.toggleExpanded(_current2);
            }

            break;
          }

        case KEY_CODES.ARROW_DOWN:
          {
            evt.preventDefault();
            instance.highlightNextOption();
            break;
          }

        case KEY_CODES.DELETE:
          {
            if (instance.deleteRemoves && !this.value.length) {
              instance.removeLastValue();
            }

            break;
          }

        default:
          {
            instance.openMenu();
          }
      }
    },
    onMouseDown: function onMouseDown(evt) {
      if (this.value.length) {
        evt.stopPropagation();
      }
    },
    renderInputContainer: function renderInputContainer() {
      var h = this.$createElement;
      var instance = this.instance;
      var props = {};
      var children = [];

      if (instance.searchable && !instance.disabled) {
        children.push(this.renderInput());
        if (this.needAutoSize) children.push(this.renderSizer());
      }

      if (!instance.searchable) {
        deepExtend(props, {
          on: {
            focus: this.onFocus,
            blur: this.onBlur,
            keydown: this.onKeyDown
          },
          ref: 'input'
        });
      }

      if (!instance.searchable && !instance.disabled) {
        deepExtend(props, {
          attrs: {
            tabIndex: instance.tabIndex
          }
        });
      }

      return h("div", external_babel_helper_vue_jsx_merge_props_default()([{
        "class": "vue-treeselect__input-container"
      }, props]), [children]);
    },
    renderInput: function renderInput() {
      var h = this.$createElement;
      var instance = this.instance;
      return h("input", {
        ref: "input",
        "class": "vue-treeselect__input",
        attrs: {
          type: "text",
          autocomplete: "off",
          tabIndex: instance.tabIndex,
          required: instance.required && !instance.hasValue
        },
        domProps: {
          "value": this.value
        },
        style: this.inputStyle,
        on: {
          "focus": this.onFocus,
          "input": this.onInput,
          "blur": this.onBlur,
          "keydown": this.onKeyDown,
          "mousedown": this.onMouseDown
        }
      });
    },
    renderSizer: function renderSizer() {
      var h = this.$createElement;
      return h("div", {
        ref: "sizer",
        "class": "vue-treeselect__sizer"
      }, [this.value]);
    },
    updateInputWidth: function updateInputWidth() {
      this.inputWidth = Math.max(MIN_INPUT_WIDTH, this.$refs.sizer.scrollWidth + 15);
    },
    updateSearchQuery: function updateSearchQuery() {
      var instance = this.instance;
      instance.trigger.searchQuery = this.value;
    }
  },
  render: function render() {
    return this.renderInputContainer();
  }
});
// CONCATENATED MODULE: ./src/components/Input.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Inputvue_type_script_lang_js_ = (Inputvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Input.vue
var Input_render, Input_staticRenderFns




/* normalize component */

var Input_component = normalizeComponent(
  components_Inputvue_type_script_lang_js_,
  Input_render,
  Input_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Input_api; }
Input_component.options.__file = "src/components/Input.vue"
/* harmony default export */ var Input = (Input_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Placeholder.vue?vue&type=script&lang=js&
/* harmony default export */ var Placeholdervue_type_script_lang_js_ = ({
  name: 'vue-treeselect--placeholder',
  inject: ['instance'],
  render: function render() {
    var h = arguments[0];
    var instance = this.instance;
    var placeholderClass = {
      'vue-treeselect__placeholder': true,
      'vue-treeselect-helper-zoom-effect-off': true,
      'vue-treeselect-helper-hide': instance.hasValue || instance.trigger.searchQuery
    };
    return h("div", {
      "class": placeholderClass
    }, [instance.placeholder]);
  }
});
// CONCATENATED MODULE: ./src/components/Placeholder.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Placeholdervue_type_script_lang_js_ = (Placeholdervue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Placeholder.vue
var Placeholder_render, Placeholder_staticRenderFns




/* normalize component */

var Placeholder_component = normalizeComponent(
  components_Placeholdervue_type_script_lang_js_,
  Placeholder_render,
  Placeholder_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Placeholder_api; }
Placeholder_component.options.__file = "src/components/Placeholder.vue"
/* harmony default export */ var Placeholder = (Placeholder_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/SingleValue.vue?vue&type=script&lang=js&


/* harmony default export */ var SingleValuevue_type_script_lang_js_ = ({
  name: 'vue-treeselect--single-value',
  inject: ['instance'],
  methods: {
    renderSingleValueLabel: function renderSingleValueLabel() {
      var instance = this.instance;
      var node = instance.selectedNodes[0];
      var customValueLabelRenderer = instance.$scopedSlots['value-label'];
      return customValueLabelRenderer ? customValueLabelRenderer({
        node: node
      }) : node.label;
    }
  },
  render: function render() {
    var h = arguments[0];
    var instance = this.instance,
        renderValueContainer = this.$parent.renderValueContainer;
    var shouldShowValue = instance.hasValue && !instance.trigger.searchQuery;
    return renderValueContainer([shouldShowValue && h("div", {
      "class": "vue-treeselect__single-value"
    }, [this.renderSingleValueLabel()]), h(Placeholder), h(Input, {
      ref: "input"
    })]);
  }
});
// CONCATENATED MODULE: ./src/components/SingleValue.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_SingleValuevue_type_script_lang_js_ = (SingleValuevue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/SingleValue.vue
var SingleValue_render, SingleValue_staticRenderFns




/* normalize component */

var SingleValue_component = normalizeComponent(
  components_SingleValuevue_type_script_lang_js_,
  SingleValue_render,
  SingleValue_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var SingleValue_api; }
SingleValue_component.options.__file = "src/components/SingleValue.vue"
/* harmony default export */ var SingleValue = (SingleValue_component.exports);
// CONCATENATED MODULE: ./node_modules/vue-loader/lib/loaders/templateLoader.js??vue-loader-options!./node_modules/cache-loader/dist/cjs.js!./node_modules/vue-loader/lib??vue-loader-options!./src/components/icons/Delete.vue?vue&type=template&id=364b6320&
var Deletevue_type_template_id_364b6320_render = function() {
  var _vm = this
  var _h = _vm.$createElement
  var _c = _vm._self._c || _h
  return _c(
    "svg",
    {
      attrs: {
        xmlns: "http://www.w3.org/2000/svg",
        viewBox: "0 0 348.333 348.333"
      }
    },
    [
      _c("path", {
        attrs: {
          d:
            "M336.559 68.611L231.016 174.165l105.543 105.549c15.699 15.705 15.699 41.145 0 56.85-7.844 7.844-18.128 11.769-28.407 11.769-10.296 0-20.581-3.919-28.419-11.769L174.167 231.003 68.609 336.563c-7.843 7.844-18.128 11.769-28.416 11.769-10.285 0-20.563-3.919-28.413-11.769-15.699-15.698-15.699-41.139 0-56.85l105.54-105.549L11.774 68.611c-15.699-15.699-15.699-41.145 0-56.844 15.696-15.687 41.127-15.687 56.829 0l105.563 105.554L279.721 11.767c15.705-15.687 41.139-15.687 56.832 0 15.705 15.699 15.705 41.145.006 56.844z"
        }
      })
    ]
  )
}
var Deletevue_type_template_id_364b6320_staticRenderFns = []
Deletevue_type_template_id_364b6320_render._withStripped = true


// CONCATENATED MODULE: ./src/components/icons/Delete.vue?vue&type=template&id=364b6320&

// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/icons/Delete.vue?vue&type=script&lang=js&
/* harmony default export */ var Deletevue_type_script_lang_js_ = ({
  name: 'vue-treeselect--x'
});
// CONCATENATED MODULE: ./src/components/icons/Delete.vue?vue&type=script&lang=js&
 /* harmony default export */ var icons_Deletevue_type_script_lang_js_ = (Deletevue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/icons/Delete.vue





/* normalize component */

var Delete_component = normalizeComponent(
  icons_Deletevue_type_script_lang_js_,
  Deletevue_type_template_id_364b6320_render,
  Deletevue_type_template_id_364b6320_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Delete_api; }
Delete_component.options.__file = "src/components/icons/Delete.vue"
/* harmony default export */ var Delete = (Delete_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/MultiValueItem.vue?vue&type=script&lang=js&


/* harmony default export */ var MultiValueItemvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--multi-value-item',
  inject: ['instance'],
  props: {
    node: {
      type: Object,
      required: true
    }
  },
  methods: {
    handleMouseDown: onLeftClick(function handleMouseDown() {
      var instance = this.instance,
          node = this.node;
      instance.select(node);
    })
  },
  render: function render() {
    var h = arguments[0];
    var instance = this.instance,
        node = this.node;
    var itemClass = {
      'vue-treeselect__multi-value-item': true,
      'vue-treeselect__multi-value-item-disabled': node.isDisabled,
      'vue-treeselect__multi-value-item-new': node.isNew
    };
    var customValueLabelRenderer = instance.$scopedSlots['value-label'];
    var labelRenderer = customValueLabelRenderer ? customValueLabelRenderer({
      node: node
    }) : node.label;
    return h("div", {
      "class": "vue-treeselect__multi-value-item-container"
    }, [h("div", {
      "class": itemClass,
      on: {
        "mousedown": this.handleMouseDown
      }
    }, [h("span", {
      "class": "vue-treeselect__multi-value-label"
    }, [labelRenderer]), h("span", {
      "class": "vue-treeselect__icon vue-treeselect__value-remove"
    }, [h(Delete)])])]);
  }
});
// CONCATENATED MODULE: ./src/components/MultiValueItem.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_MultiValueItemvue_type_script_lang_js_ = (MultiValueItemvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/MultiValueItem.vue
var MultiValueItem_render, MultiValueItem_staticRenderFns




/* normalize component */

var MultiValueItem_component = normalizeComponent(
  components_MultiValueItemvue_type_script_lang_js_,
  MultiValueItem_render,
  MultiValueItem_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var MultiValueItem_api; }
MultiValueItem_component.options.__file = "src/components/MultiValueItem.vue"
/* harmony default export */ var MultiValueItem = (MultiValueItem_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/MultiValue.vue?vue&type=script&lang=js&




/* harmony default export */ var MultiValuevue_type_script_lang_js_ = ({
  name: 'vue-treeselect--multi-value',
  inject: ['instance'],
  methods: {
    renderMultiValueItems: function renderMultiValueItems() {
      var h = this.$createElement;
      var instance = this.instance;
      return instance.internalValue.slice(0, instance.limit).map(instance.getNode).map(function (node) {
        return h(MultiValueItem, {
          key: "multi-value-item-".concat(node.id),
          attrs: {
            node: node
          }
        });
      });
    },
    renderExceedLimitTip: function renderExceedLimitTip() {
      var h = this.$createElement;
      var instance = this.instance;
      var count = instance.internalValue.length - instance.limit;
      if (count <= 0) return null;
      return h("div", {
        "class": "vue-treeselect__limit-tip vue-treeselect-helper-zoom-effect-off",
        key: "exceed-limit-tip"
      }, [h("span", {
        "class": "vue-treeselect__limit-tip-text"
      }, [instance.limitText(count)])]);
    }
  },
  render: function render() {
    var h = arguments[0];
    var renderValueContainer = this.$parent.renderValueContainer;
    var transitionGroupProps = {
      props: {
        tag: 'div',
        name: 'vue-treeselect__multi-value-item--transition',
        appear: true
      }
    };
    return renderValueContainer(h("transition-group", external_babel_helper_vue_jsx_merge_props_default()([{
      "class": "vue-treeselect__multi-value"
    }, transitionGroupProps]), [this.renderMultiValueItems(), this.renderExceedLimitTip(), h(Placeholder, {
      key: "placeholder"
    }), h(Input, {
      ref: "input",
      key: "input"
    })]));
  }
});
// CONCATENATED MODULE: ./src/components/MultiValue.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_MultiValuevue_type_script_lang_js_ = (MultiValuevue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/MultiValue.vue
var MultiValue_render, MultiValue_staticRenderFns




/* normalize component */

var MultiValue_component = normalizeComponent(
  components_MultiValuevue_type_script_lang_js_,
  MultiValue_render,
  MultiValue_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var MultiValue_api; }
MultiValue_component.options.__file = "src/components/MultiValue.vue"
/* harmony default export */ var MultiValue = (MultiValue_component.exports);
// CONCATENATED MODULE: ./node_modules/vue-loader/lib/loaders/templateLoader.js??vue-loader-options!./node_modules/cache-loader/dist/cjs.js!./node_modules/vue-loader/lib??vue-loader-options!./src/components/icons/Arrow.vue?vue&type=template&id=11186cd4&
var Arrowvue_type_template_id_11186cd4_render = function() {
  var _vm = this
  var _h = _vm.$createElement
  var _c = _vm._self._c || _h
  return _c(
    "svg",
    {
      attrs: {
        xmlns: "http://www.w3.org/2000/svg",
        viewBox: "0 0 292.362 292.362"
      }
    },
    [
      _c("path", {
        attrs: {
          d:
            "M286.935 69.377c-3.614-3.617-7.898-5.424-12.848-5.424H18.274c-4.952 0-9.233 1.807-12.85 5.424C1.807 72.998 0 77.279 0 82.228c0 4.948 1.807 9.229 5.424 12.847l127.907 127.907c3.621 3.617 7.902 5.428 12.85 5.428s9.233-1.811 12.847-5.428L286.935 95.074c3.613-3.617 5.427-7.898 5.427-12.847 0-4.948-1.814-9.229-5.427-12.85z"
        }
      })
    ]
  )
}
var Arrowvue_type_template_id_11186cd4_staticRenderFns = []
Arrowvue_type_template_id_11186cd4_render._withStripped = true


// CONCATENATED MODULE: ./src/components/icons/Arrow.vue?vue&type=template&id=11186cd4&

// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/icons/Arrow.vue?vue&type=script&lang=js&
/* harmony default export */ var Arrowvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--arrow'
});
// CONCATENATED MODULE: ./src/components/icons/Arrow.vue?vue&type=script&lang=js&
 /* harmony default export */ var icons_Arrowvue_type_script_lang_js_ = (Arrowvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/icons/Arrow.vue





/* normalize component */

var Arrow_component = normalizeComponent(
  icons_Arrowvue_type_script_lang_js_,
  Arrowvue_type_template_id_11186cd4_render,
  Arrowvue_type_template_id_11186cd4_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Arrow_api; }
Arrow_component.options.__file = "src/components/icons/Arrow.vue"
/* harmony default export */ var Arrow = (Arrow_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Control.vue?vue&type=script&lang=js&





/* harmony default export */ var Controlvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--control',
  inject: ['instance'],
  computed: {
    shouldShowX: function shouldShowX() {
      var instance = this.instance;
      return instance.clearable && !instance.disabled && instance.hasValue && (this.hasUndisabledValue || instance.allowClearingDisabled);
    },
    shouldShowArrow: function shouldShowArrow() {
      var instance = this.instance;
      if (!instance.alwaysOpen) return true;
      return !instance.menu.isOpen;
    },
    hasUndisabledValue: function hasUndisabledValue() {
      var instance = this.instance;
      return instance.hasValue && instance.internalValue.some(function (id) {
        return !instance.getNode(id).isDisabled;
      });
    }
  },
  methods: {
    renderX: function renderX() {
      var h = this.$createElement;
      var instance = this.instance;
      var title = instance.multiple ? instance.clearAllText : instance.clearValueText;
      if (!this.shouldShowX) return null;
      return h("div", {
        "class": "vue-treeselect__x-container",
        attrs: {
          title: title
        },
        on: {
          "mousedown": this.handleMouseDownOnX
        }
      }, [h(Delete, {
        "class": "vue-treeselect__x"
      })]);
    },
    renderArrow: function renderArrow() {
      var h = this.$createElement;
      var instance = this.instance;
      var arrowClass = {
        'vue-treeselect__control-arrow': true,
        'vue-treeselect__control-arrow--rotated': instance.menu.isOpen
      };
      if (!this.shouldShowArrow) return null;
      return h("div", {
        "class": "vue-treeselect__control-arrow-container",
        on: {
          "mousedown": this.handleMouseDownOnArrow
        }
      }, [h(Arrow, {
        "class": arrowClass
      })]);
    },
    handleMouseDownOnX: onLeftClick(function handleMouseDownOnX(evt) {
      evt.stopPropagation();
      evt.preventDefault();
      var instance = this.instance;
      var result = instance.beforeClearAll();

      var handler = function handler(shouldClear) {
        if (shouldClear) instance.clear();
      };

      if (external_is_promise_default()(result)) {
        result.then(handler);
      } else {
        setTimeout(function () {
          return handler(result);
        }, 0);
      }
    }),
    handleMouseDownOnArrow: onLeftClick(function handleMouseDownOnArrow(evt) {
      evt.preventDefault();
      evt.stopPropagation();
      var instance = this.instance;
      instance.focusInput();
      instance.toggleMenu();
    }),
    renderValueContainer: function renderValueContainer(children) {
      var h = this.$createElement;
      return h("div", {
        "class": "vue-treeselect__value-container"
      }, [children]);
    }
  },
  render: function render() {
    var h = arguments[0];
    var instance = this.instance;
    var ValueContainer = instance.single ? SingleValue : MultiValue;
    return h("div", {
      "class": "vue-treeselect__control",
      on: {
        "mousedown": instance.handleMouseDown
      }
    }, [h(ValueContainer, {
      ref: "value-container"
    }), this.renderX(), this.renderArrow()]);
  }
});
// CONCATENATED MODULE: ./src/components/Control.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Controlvue_type_script_lang_js_ = (Controlvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Control.vue
var Control_render, Control_staticRenderFns




/* normalize component */

var Control_component = normalizeComponent(
  components_Controlvue_type_script_lang_js_,
  Control_render,
  Control_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Control_api; }
Control_component.options.__file = "src/components/Control.vue"
/* harmony default export */ var Control = (Control_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Tip.vue?vue&type=script&lang=js&
/* harmony default export */ var Tipvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--tip',
  functional: true,
  props: {
    type: {
      type: String,
      required: true
    },
    icon: {
      type: String,
      required: true
    }
  },
  render: function render(_, context) {
    var h = arguments[0];
    var props = context.props,
        children = context.children;
    return h("div", {
      "class": "vue-treeselect__tip vue-treeselect__".concat(props.type, "-tip")
    }, [h("div", {
      "class": "vue-treeselect__icon-container"
    }, [h("span", {
      "class": "vue-treeselect__icon-".concat(props.icon)
    })]), h("span", {
      "class": "vue-treeselect__tip-text vue-treeselect__".concat(props.type, "-tip-text")
    }, [children])]);
  }
});
// CONCATENATED MODULE: ./src/components/Tip.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Tipvue_type_script_lang_js_ = (Tipvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Tip.vue
var Tip_render, Tip_staticRenderFns




/* normalize component */

var Tip_component = normalizeComponent(
  components_Tipvue_type_script_lang_js_,
  Tip_render,
  Tip_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Tip_api; }
Tip_component.options.__file = "src/components/Tip.vue"
/* harmony default export */ var Tip = (Tip_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Option.vue?vue&type=script&lang=js&





var arrowPlaceholder, checkMark, minusMark;
var Option = {
  name: 'vue-treeselect--option',
  inject: ['instance'],
  props: {
    node: {
      type: Object,
      required: true
    }
  },
  computed: {
    shouldExpand: function shouldExpand() {
      var instance = this.instance,
          node = this.node;
      return node.isBranch && instance.shouldExpand(node);
    },
    shouldShow: function shouldShow() {
      var instance = this.instance,
          node = this.node;
      return instance.shouldShowOptionInMenu(node);
    }
  },
  methods: {
    renderOption: function renderOption() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      var optionClass = {
        'vue-treeselect__option': true,
        'vue-treeselect__option--disabled': node.isDisabled,
        'vue-treeselect__option--selected': instance.isSelected(node),
        'vue-treeselect__option--highlight': node.isHighlighted,
        'vue-treeselect__option--matched': instance.localSearch.active && node.isMatched,
        'vue-treeselect__option--hide': !this.shouldShow
      };
      return h("div", {
        "class": optionClass,
        on: {
          "mouseenter": this.handleMouseEnterOption
        },
        attrs: {
          "data-id": node.id
        }
      }, [this.renderArrow(), this.renderLabelContainer([this.renderCheckboxContainer([this.renderCheckbox()]), this.renderLabel()])]);
    },
    renderSubOptionsList: function renderSubOptionsList() {
      var h = this.$createElement;
      if (!this.shouldExpand) return null;
      return h("div", {
        "class": "vue-treeselect__list"
      }, [this.renderSubOptions(), this.renderNoChildrenTip(), this.renderLoadingChildrenTip(), this.renderLoadingChildrenErrorTip()]);
    },
    renderArrow: function renderArrow() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      if (instance.shouldFlattenOptions && this.shouldShow) return null;

      if (node.isBranch) {
        var transitionProps = {
          props: {
            name: 'vue-treeselect__option-arrow--prepare',
            appear: true
          }
        };
        var arrowClass = {
          'vue-treeselect__option-arrow': true,
          'vue-treeselect__option-arrow--rotated': this.shouldExpand
        };
        return h("div", {
          "class": "vue-treeselect__option-arrow-container",
          on: {
            "mousedown": this.handleMouseDownOnArrow
          }
        }, [h("transition", transitionProps, [h(Arrow, {
          "class": arrowClass
        })])]);
      }

      if (instance.hasBranchNodes) {
        if (!arrowPlaceholder) arrowPlaceholder = h("div", {
          "class": "vue-treeselect__option-arrow-placeholder"
        }, ["\xA0"]);
        return arrowPlaceholder;
      }

      return null;
    },
    renderLabelContainer: function renderLabelContainer(children) {
      var h = this.$createElement;
      return h("div", {
        "class": "vue-treeselect__label-container",
        on: {
          "mousedown": this.handleMouseDownOnLabelContainer
        }
      }, [children]);
    },
    renderCheckboxContainer: function renderCheckboxContainer(children) {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      if (instance.single) return null;
      if (instance.disableBranchNodes && node.isBranch) return null;
      return h("div", {
        "class": "vue-treeselect__checkbox-container"
      }, [children]);
    },
    renderCheckbox: function renderCheckbox() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      var checkedState = instance.forest.checkedStateMap[node.id];
      var checkboxClass = {
        'vue-treeselect__checkbox': true,
        'vue-treeselect__checkbox--checked': checkedState === CHECKED,
        'vue-treeselect__checkbox--indeterminate': checkedState === INDETERMINATE,
        'vue-treeselect__checkbox--unchecked': checkedState === UNCHECKED,
        'vue-treeselect__checkbox--disabled': node.isDisabled
      };
      if (!checkMark) checkMark = h("span", {
        "class": "vue-treeselect__check-mark"
      });
      if (!minusMark) minusMark = h("span", {
        "class": "vue-treeselect__minus-mark"
      });
      return h("span", {
        "class": checkboxClass
      }, [checkMark, minusMark]);
    },
    renderLabel: function renderLabel() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      var shouldShowCount = node.isBranch && (instance.localSearch.active ? instance.showCountOnSearchComputed : instance.showCount);
      var count = shouldShowCount ? instance.localSearch.active ? instance.localSearch.countMap[node.id][instance.showCountOf] : node.count[instance.showCountOf] : NaN;
      var labelClassName = 'vue-treeselect__label';
      var countClassName = 'vue-treeselect__count';
      var customLabelRenderer = instance.$scopedSlots['option-label'];
      if (customLabelRenderer) return customLabelRenderer({
        node: node,
        shouldShowCount: shouldShowCount,
        count: count,
        labelClassName: labelClassName,
        countClassName: countClassName
      });
      return h("label", {
        "class": labelClassName
      }, [node.label, shouldShowCount && h("span", {
        "class": countClassName
      }, ["(", count, ")"])]);
    },
    renderSubOptions: function renderSubOptions() {
      var h = this.$createElement;
      var node = this.node;
      if (!node.childrenStates.isLoaded) return null;
      return node.children.map(function (childNode) {
        return h(Option, {
          attrs: {
            node: childNode
          },
          key: childNode.id
        });
      });
    },
    renderNoChildrenTip: function renderNoChildrenTip() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      if (!node.childrenStates.isLoaded || node.children.length) return null;
      return h(Tip, {
        attrs: {
          type: "no-children",
          icon: "warning"
        }
      }, [instance.noChildrenText]);
    },
    renderLoadingChildrenTip: function renderLoadingChildrenTip() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      if (!node.childrenStates.isLoading) return null;
      return h(Tip, {
        attrs: {
          type: "loading",
          icon: "loader"
        }
      }, [instance.loadingText]);
    },
    renderLoadingChildrenErrorTip: function renderLoadingChildrenErrorTip() {
      var h = this.$createElement;
      var instance = this.instance,
          node = this.node;
      if (!node.childrenStates.loadingError) return null;
      return h(Tip, {
        attrs: {
          type: "error",
          icon: "error"
        }
      }, [node.childrenStates.loadingError, h("a", {
        "class": "vue-treeselect__retry",
        attrs: {
          title: instance.retryTitle
        },
        on: {
          "mousedown": this.handleMouseDownOnRetry
        }
      }, [instance.retryText])]);
    },
    handleMouseEnterOption: function handleMouseEnterOption(evt) {
      var instance = this.instance,
          node = this.node;
      if (evt.target !== evt.currentTarget) return;
      instance.setCurrentHighlightedOption(node, false);
    },
    handleMouseDownOnArrow: onLeftClick(function handleMouseDownOnOptionArrow() {
      var instance = this.instance,
          node = this.node;
      instance.toggleExpanded(node);
    }),
    handleMouseDownOnLabelContainer: onLeftClick(function handleMouseDownOnLabelContainer() {
      var instance = this.instance,
          node = this.node;

      if (node.isBranch && instance.disableBranchNodes) {
        instance.toggleExpanded(node);
      } else {
        instance.select(node);
      }
    }),
    handleMouseDownOnRetry: onLeftClick(function handleMouseDownOnRetry() {
      var instance = this.instance,
          node = this.node;
      instance.loadChildrenOptions(node);
    })
  },
  render: function render() {
    var h = arguments[0];
    var node = this.node;
    var indentLevel = this.instance.shouldFlattenOptions ? 0 : node.level;

    var listItemClass = defineProperty_default()({
      'vue-treeselect__list-item': true
    }, "vue-treeselect__indent-level-".concat(indentLevel), true);

    var transitionProps = {
      props: {
        name: 'vue-treeselect__list--transition'
      }
    };
    return h("div", {
      "class": listItemClass
    }, [this.renderOption(), node.isBranch && h("transition", transitionProps, [this.renderSubOptionsList()])]);
  }
};
/* harmony default export */ var Optionvue_type_script_lang_js_ = (Option);
// CONCATENATED MODULE: ./src/components/Option.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Optionvue_type_script_lang_js_ = (Optionvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Option.vue
var Option_render, Option_staticRenderFns




/* normalize component */

var Option_component = normalizeComponent(
  components_Optionvue_type_script_lang_js_,
  Option_render,
  Option_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Option_api; }
Option_component.options.__file = "src/components/Option.vue"
/* harmony default export */ var components_Option = (Option_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Menu.vue?vue&type=script&lang=js&




var directionMap = {
  top: 'top',
  bottom: 'bottom',
  above: 'top',
  below: 'bottom'
};
/* harmony default export */ var Menuvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--menu',
  inject: ['instance'],
  computed: {
    menuStyle: function menuStyle() {
      var instance = this.instance;
      return {
        maxHeight: instance.maxHeight + 'px'
      };
    },
    menuContainerStyle: function menuContainerStyle() {
      var instance = this.instance;
      return {
        zIndex: instance.appendToBody ? null : instance.zIndex
      };
    }
  },
  watch: {
    'instance.menu.isOpen': function instanceMenuIsOpen(newValue) {
      if (newValue) {
        this.$nextTick(this.onMenuOpen);
      } else {
        this.onMenuClose();
      }
    }
  },
  created: function created() {
    this.menuSizeWatcher = null;
    this.menuResizeAndScrollEventListeners = null;
  },
  mounted: function mounted() {
    var instance = this.instance;
    if (instance.menu.isOpen) this.$nextTick(this.onMenuOpen);
  },
  destroyed: function destroyed() {
    this.onMenuClose();
  },
  methods: {
    renderMenu: function renderMenu() {
      var h = this.$createElement;
      var instance = this.instance;
      if (!instance.menu.isOpen) return null;
      return h("div", {
        ref: "menu",
        "class": "vue-treeselect__menu",
        on: {
          "mousedown": instance.handleMouseDown
        },
        style: this.menuStyle
      }, [this.renderBeforeList(), instance.async ? this.renderAsyncSearchMenuInner() : instance.localSearch.active ? this.renderLocalSearchMenuInner() : this.renderNormalMenuInner(), this.renderAfterList()]);
    },
    renderBeforeList: function renderBeforeList() {
      var instance = this.instance;
      var beforeListRenderer = instance.$scopedSlots['before-list'];
      return beforeListRenderer ? beforeListRenderer() : null;
    },
    renderAfterList: function renderAfterList() {
      var instance = this.instance;
      var afterListRenderer = instance.$scopedSlots['after-list'];
      return afterListRenderer ? afterListRenderer() : null;
    },
    renderNormalMenuInner: function renderNormalMenuInner() {
      var instance = this.instance;

      if (instance.rootOptionsStates.isLoading) {
        return this.renderLoadingOptionsTip();
      } else if (instance.rootOptionsStates.loadingError) {
        return this.renderLoadingRootOptionsErrorTip();
      } else if (instance.rootOptionsStates.isLoaded && instance.forest.normalizedOptions.length === 0) {
        return this.renderNoAvailableOptionsTip();
      } else {
        return this.renderOptionList();
      }
    },
    renderLocalSearchMenuInner: function renderLocalSearchMenuInner() {
      var instance = this.instance;

      if (instance.rootOptionsStates.isLoading) {
        return this.renderLoadingOptionsTip();
      } else if (instance.rootOptionsStates.loadingError) {
        return this.renderLoadingRootOptionsErrorTip();
      } else if (instance.rootOptionsStates.isLoaded && instance.forest.normalizedOptions.length === 0) {
        return this.renderNoAvailableOptionsTip();
      } else if (instance.localSearch.noResults) {
        return this.renderNoResultsTip();
      } else {
        return this.renderOptionList();
      }
    },
    renderAsyncSearchMenuInner: function renderAsyncSearchMenuInner() {
      var instance = this.instance;
      var entry = instance.getRemoteSearchEntry();
      var shouldShowSearchPromptTip = instance.trigger.searchQuery === '' && !instance.defaultOptions;
      var shouldShowNoResultsTip = shouldShowSearchPromptTip ? false : entry.isLoaded && entry.options.length === 0;

      if (shouldShowSearchPromptTip) {
        return this.renderSearchPromptTip();
      } else if (entry.isLoading) {
        return this.renderLoadingOptionsTip();
      } else if (entry.loadingError) {
        return this.renderAsyncSearchLoadingErrorTip();
      } else if (shouldShowNoResultsTip) {
        return this.renderNoResultsTip();
      } else {
        return this.renderOptionList();
      }
    },
    renderOptionList: function renderOptionList() {
      var h = this.$createElement;
      var instance = this.instance;
      return h("div", {
        "class": "vue-treeselect__list"
      }, [instance.forest.normalizedOptions.map(function (rootNode) {
        return h(components_Option, {
          attrs: {
            node: rootNode
          },
          key: rootNode.id
        });
      })]);
    },
    renderSearchPromptTip: function renderSearchPromptTip() {
      var h = this.$createElement;
      var instance = this.instance;
      return h(Tip, {
        attrs: {
          type: "search-prompt",
          icon: "warning"
        }
      }, [instance.searchPromptText]);
    },
    renderLoadingOptionsTip: function renderLoadingOptionsTip() {
      var h = this.$createElement;
      var instance = this.instance;
      return h(Tip, {
        attrs: {
          type: "loading",
          icon: "loader"
        }
      }, [instance.loadingText]);
    },
    renderLoadingRootOptionsErrorTip: function renderLoadingRootOptionsErrorTip() {
      var h = this.$createElement;
      var instance = this.instance;
      return h(Tip, {
        attrs: {
          type: "error",
          icon: "error"
        }
      }, [instance.rootOptionsStates.loadingError, h("a", {
        "class": "vue-treeselect__retry",
        on: {
          "click": instance.loadRootOptions
        },
        attrs: {
          title: instance.retryTitle
        }
      }, [instance.retryText])]);
    },
    renderAsyncSearchLoadingErrorTip: function renderAsyncSearchLoadingErrorTip() {
      var h = this.$createElement;
      var instance = this.instance;
      var entry = instance.getRemoteSearchEntry();
      return h(Tip, {
        attrs: {
          type: "error",
          icon: "error"
        }
      }, [entry.loadingError, h("a", {
        "class": "vue-treeselect__retry",
        on: {
          "click": instance.handleRemoteSearch
        },
        attrs: {
          title: instance.retryTitle
        }
      }, [instance.retryText])]);
    },
    renderNoAvailableOptionsTip: function renderNoAvailableOptionsTip() {
      var h = this.$createElement;
      var instance = this.instance;
      return h(Tip, {
        attrs: {
          type: "no-options",
          icon: "warning"
        }
      }, [instance.noOptionsText]);
    },
    renderNoResultsTip: function renderNoResultsTip() {
      var h = this.$createElement;
      var instance = this.instance;
      return h(Tip, {
        attrs: {
          type: "no-results",
          icon: "warning"
        }
      }, [instance.noResultsText]);
    },
    onMenuOpen: function onMenuOpen() {
      this.adjustMenuOpenDirection();
      this.setupMenuSizeWatcher();
      this.setupMenuResizeAndScrollEventListeners();
    },
    onMenuClose: function onMenuClose() {
      this.removeMenuSizeWatcher();
      this.removeMenuResizeAndScrollEventListeners();
    },
    adjustMenuOpenDirection: function adjustMenuOpenDirection() {
      var instance = this.instance;
      if (!instance.menu.isOpen) return;
      var $menu = instance.getMenu();
      var $control = instance.getControl();
      var menuRect = $menu.getBoundingClientRect();
      var controlRect = $control.getBoundingClientRect();
      var menuHeight = menuRect.height;
      var viewportHeight = window.innerHeight;
      var spaceAbove = controlRect.top;
      var spaceBelow = window.innerHeight - controlRect.bottom;
      var isControlInViewport = controlRect.top >= 0 && controlRect.top <= viewportHeight || controlRect.top < 0 && controlRect.bottom > 0;
      var hasEnoughSpaceBelow = spaceBelow > menuHeight + MENU_BUFFER;
      var hasEnoughSpaceAbove = spaceAbove > menuHeight + MENU_BUFFER;

      if (!isControlInViewport) {
        instance.closeMenu();
      } else if (instance.openDirection !== 'auto') {
        instance.menu.placement = directionMap[instance.openDirection];
      } else if (hasEnoughSpaceBelow || !hasEnoughSpaceAbove) {
        instance.menu.placement = 'bottom';
      } else {
        instance.menu.placement = 'top';
      }
    },
    setupMenuSizeWatcher: function setupMenuSizeWatcher() {
      var instance = this.instance;
      var $menu = instance.getMenu();
      if (this.menuSizeWatcher) return;
      this.menuSizeWatcher = {
        remove: watchSize($menu, this.adjustMenuOpenDirection)
      };
    },
    setupMenuResizeAndScrollEventListeners: function setupMenuResizeAndScrollEventListeners() {
      var instance = this.instance;
      var $control = instance.getControl();
      if (this.menuResizeAndScrollEventListeners) return;
      this.menuResizeAndScrollEventListeners = {
        remove: setupResizeAndScrollEventListeners($control, this.adjustMenuOpenDirection)
      };
    },
    removeMenuSizeWatcher: function removeMenuSizeWatcher() {
      if (!this.menuSizeWatcher) return;
      this.menuSizeWatcher.remove();
      this.menuSizeWatcher = null;
    },
    removeMenuResizeAndScrollEventListeners: function removeMenuResizeAndScrollEventListeners() {
      if (!this.menuResizeAndScrollEventListeners) return;
      this.menuResizeAndScrollEventListeners.remove();
      this.menuResizeAndScrollEventListeners = null;
    }
  },
  render: function render() {
    var h = arguments[0];
    return h("div", {
      ref: "menu-container",
      "class": "vue-treeselect__menu-container",
      style: this.menuContainerStyle
    }, [h("transition", {
      attrs: {
        name: "vue-treeselect__menu--transition"
      }
    }, [this.renderMenu()])]);
  }
});
// CONCATENATED MODULE: ./src/components/Menu.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Menuvue_type_script_lang_js_ = (Menuvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Menu.vue
var Menu_render, Menu_staticRenderFns




/* normalize component */

var Menu_component = normalizeComponent(
  components_Menuvue_type_script_lang_js_,
  Menu_render,
  Menu_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Menu_api; }
Menu_component.options.__file = "src/components/Menu.vue"
/* harmony default export */ var Menu = (Menu_component.exports);
// EXTERNAL MODULE: external "vue"
var external_vue_ = __webpack_require__(14);
var external_vue_default = /*#__PURE__*/__webpack_require__.n(external_vue_);

// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/MenuPortal.vue?vue&type=script&lang=js&


function MenuPortalvue_type_script_lang_js_ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function MenuPortalvue_type_script_lang_js_objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { MenuPortalvue_type_script_lang_js_ownKeys(source, true).forEach(function (key) { defineProperty_default()(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { MenuPortalvue_type_script_lang_js_ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }




var PortalTarget = {
  name: 'vue-treeselect--portal-target',
  inject: ['instance'],
  watch: {
    'instance.menu.isOpen': function instanceMenuIsOpen(newValue) {
      if (newValue) {
        this.setupHandlers();
      } else {
        this.removeHandlers();
      }
    },
    'instance.menu.placement': function instanceMenuPlacement() {
      this.updateMenuContainerOffset();
    }
  },
  created: function created() {
    this.controlResizeAndScrollEventListeners = null;
    this.controlSizeWatcher = null;
  },
  mounted: function mounted() {
    var instance = this.instance;
    if (instance.menu.isOpen) this.setupHandlers();
  },
  methods: {
    setupHandlers: function setupHandlers() {
      this.updateWidth();
      this.updateMenuContainerOffset();
      this.setupControlResizeAndScrollEventListeners();
      this.setupControlSizeWatcher();
    },
    removeHandlers: function removeHandlers() {
      this.removeControlResizeAndScrollEventListeners();
      this.removeControlSizeWatcher();
    },
    setupControlResizeAndScrollEventListeners: function setupControlResizeAndScrollEventListeners() {
      var instance = this.instance;
      var $control = instance.getControl();
      if (this.controlResizeAndScrollEventListeners) return;
      this.controlResizeAndScrollEventListeners = {
        remove: setupResizeAndScrollEventListeners($control, this.updateMenuContainerOffset)
      };
    },
    setupControlSizeWatcher: function setupControlSizeWatcher() {
      var _this = this;

      var instance = this.instance;
      var $control = instance.getControl();
      if (this.controlSizeWatcher) return;
      this.controlSizeWatcher = {
        remove: watchSize($control, function () {
          _this.updateWidth();

          _this.updateMenuContainerOffset();
        })
      };
    },
    removeControlResizeAndScrollEventListeners: function removeControlResizeAndScrollEventListeners() {
      if (!this.controlResizeAndScrollEventListeners) return;
      this.controlResizeAndScrollEventListeners.remove();
      this.controlResizeAndScrollEventListeners = null;
    },
    removeControlSizeWatcher: function removeControlSizeWatcher() {
      if (!this.controlSizeWatcher) return;
      this.controlSizeWatcher.remove();
      this.controlSizeWatcher = null;
    },
    updateWidth: function updateWidth() {
      var instance = this.instance;
      var $portalTarget = this.$el;
      var $control = instance.getControl();
      var controlRect = $control.getBoundingClientRect();
      $portalTarget.style.width = controlRect.width + 'px';
    },
    updateMenuContainerOffset: function updateMenuContainerOffset() {
      var instance = this.instance;
      var $control = instance.getControl();
      var $portalTarget = this.$el;
      var controlRect = $control.getBoundingClientRect();
      var portalTargetRect = $portalTarget.getBoundingClientRect();
      var offsetY = instance.menu.placement === 'bottom' ? controlRect.height : 0;
      var left = Math.round(controlRect.left - portalTargetRect.left) + 'px';
      var top = Math.round(controlRect.top - portalTargetRect.top + offsetY) + 'px';
      var menuContainerStyle = this.$refs.menu.$refs['menu-container'].style;
      var transformVariations = ['transform', 'webkitTransform', 'MozTransform', 'msTransform'];
      var transform = find(transformVariations, function (t) {
        return t in document.body.style;
      });
      menuContainerStyle[transform] = "translate(".concat(left, ", ").concat(top, ")");
    }
  },
  render: function render() {
    var h = arguments[0];
    var instance = this.instance;
    var portalTargetClass = ['vue-treeselect__portal-target', instance.wrapperClass];
    var portalTargetStyle = {
      zIndex: instance.zIndex
    };
    return h("div", {
      "class": portalTargetClass,
      style: portalTargetStyle,
      attrs: {
        "data-instance-id": instance.getInstanceId()
      }
    }, [h(Menu, {
      ref: "menu"
    })]);
  },
  destroyed: function destroyed() {
    this.removeHandlers();
  }
};
var placeholder;
/* harmony default export */ var MenuPortalvue_type_script_lang_js_ = ({
  name: 'vue-treeselect--menu-portal',
  created: function created() {
    this.portalTarget = null;
  },
  mounted: function mounted() {
    this.setup();
  },
  destroyed: function destroyed() {
    this.teardown();
  },
  methods: {
    setup: function setup() {
      var el = document.createElement('div');
      document.body.appendChild(el);
      this.portalTarget = new external_vue_default.a(MenuPortalvue_type_script_lang_js_objectSpread({
        el: el,
        parent: this
      }, PortalTarget));
    },
    teardown: function teardown() {
      document.body.removeChild(this.portalTarget.$el);
      this.portalTarget.$el.innerHTML = '';
      this.portalTarget.$destroy();
      this.portalTarget = null;
    }
  },
  render: function render() {
    var h = arguments[0];
    if (!placeholder) placeholder = h("div", {
      "class": "vue-treeselect__menu-placeholder"
    });
    return placeholder;
  }
});
// CONCATENATED MODULE: ./src/components/MenuPortal.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_MenuPortalvue_type_script_lang_js_ = (MenuPortalvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/MenuPortal.vue
var MenuPortal_render, MenuPortal_staticRenderFns




/* normalize component */

var MenuPortal_component = normalizeComponent(
  components_MenuPortalvue_type_script_lang_js_,
  MenuPortal_render,
  MenuPortal_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var MenuPortal_api; }
MenuPortal_component.options.__file = "src/components/MenuPortal.vue"
/* harmony default export */ var MenuPortal = (MenuPortal_component.exports);
// CONCATENATED MODULE: ./node_modules/cache-loader/dist/cjs.js!./node_modules/babel-loader/lib!./node_modules/vue-loader/lib??vue-loader-options!./src/components/Treeselect.vue?vue&type=script&lang=js&





/* harmony default export */ var Treeselectvue_type_script_lang_js_ = ({
  name: 'vue-treeselect',
  mixins: [treeselectMixin],
  computed: {
    wrapperClass: function wrapperClass() {
      return {
        'vue-treeselect': true,
        'vue-treeselect--single': this.single,
        'vue-treeselect--multi': this.multiple,
        'vue-treeselect--searchable': this.searchable,
        'vue-treeselect--disabled': this.disabled,
        'vue-treeselect--focused': this.trigger.isFocused,
        'vue-treeselect--has-value': this.hasValue,
        'vue-treeselect--open': this.menu.isOpen,
        'vue-treeselect--open-above': this.menu.placement === 'top',
        'vue-treeselect--open-below': this.menu.placement === 'bottom',
        'vue-treeselect--branch-nodes-disabled': this.disableBranchNodes,
        'vue-treeselect--append-to-body': this.appendToBody
      };
    }
  },
  render: function render() {
    var h = arguments[0];
    return h("div", {
      ref: "wrapper",
      "class": this.wrapperClass
    }, [h(HiddenFields), h(Control, {
      ref: "control"
    }), this.appendToBody ? h(MenuPortal, {
      ref: "portal"
    }) : h(Menu, {
      ref: "menu"
    })]);
  }
});
// CONCATENATED MODULE: ./src/components/Treeselect.vue?vue&type=script&lang=js&
 /* harmony default export */ var components_Treeselectvue_type_script_lang_js_ = (Treeselectvue_type_script_lang_js_); 
// CONCATENATED MODULE: ./src/components/Treeselect.vue
var Treeselect_render, Treeselect_staticRenderFns




/* normalize component */

var Treeselect_component = normalizeComponent(
  components_Treeselectvue_type_script_lang_js_,
  Treeselect_render,
  Treeselect_staticRenderFns,
  false,
  null,
  null,
  null
  
)

/* hot reload */
if (false) { var Treeselect_api; }
Treeselect_component.options.__file = "src/components/Treeselect.vue"
/* harmony default export */ var Treeselect = (Treeselect_component.exports);
// EXTERNAL MODULE: ./src/style.less
var style = __webpack_require__(15);

// CONCATENATED MODULE: ./src/index.js
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VERSION", function() { return VERSION; });
/* concated harmony reexport Treeselect */__webpack_require__.d(__webpack_exports__, "Treeselect", function() { return Treeselect; });
/* concated harmony reexport treeselectMixin */__webpack_require__.d(__webpack_exports__, "treeselectMixin", function() { return treeselectMixin; });
/* concated harmony reexport LOAD_ROOT_OPTIONS */__webpack_require__.d(__webpack_exports__, "LOAD_ROOT_OPTIONS", function() { return LOAD_ROOT_OPTIONS; });
/* concated harmony reexport LOAD_CHILDREN_OPTIONS */__webpack_require__.d(__webpack_exports__, "LOAD_CHILDREN_OPTIONS", function() { return LOAD_CHILDREN_OPTIONS; });
/* concated harmony reexport ASYNC_SEARCH */__webpack_require__.d(__webpack_exports__, "ASYNC_SEARCH", function() { return ASYNC_SEARCH; });



/* harmony default export */ var src = __webpack_exports__["default"] = (Treeselect);


var VERSION = "0.4.0";

/***/ })
/******/ ]);
//# sourceMappingURL=vue-treeselect.cjs.js.map
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(26)))

/***/ }),

/***/ 926:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.unflatten = unflatten;
exports.unflatten2 = unflatten2;
exports.slug = slug;
exports.pathImg = pathImg;
exports.urlBase = urlBase;

var _vue = __webpack_require__(31);

var _vue2 = _interopRequireDefault(_vue);

var _jsTreeList = __webpack_require__(943);

var _jsTreeList2 = _interopRequireDefault(_jsTreeList);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function unflatten(arr) {

    var ltt = new _jsTreeList2.default.ListToTree(arr, {
        key_id: 'id',
        key_parent: 'parentId',
        key_child: "children"
    });

    var tree = ltt.GetTree();

    return tree;
};

function unflatten2(arr) {

    var tree = unflatten(arr);
    return tree;
};

function unflatten(arr) {
    var tree = [],
        mappedArr = {},
        arrElem,
        mappedElem;

    for (var i = 0, len = arr.length; i < len; i++) {
        arrElem = arr[i];
        mappedArr[arrElem.id] = arrElem;
        mappedArr[arrElem.id]['children'] = [];
    }
    for (var id in mappedArr) {
        if (mappedArr.hasOwnProperty(id)) {
            mappedElem = mappedArr[id];

            if (mappedElem.parentId) {
                try {
                    mappedArr[mappedElem['parentId']]['children'].push(mappedElem);
                } catch (ex) {
                    console.log(ex);
                }
            } else {
                    tree.push(mappedElem);
                }
        }
    }
    return tree;
};

function slug(title) {
    var slug = "";
    if (title != null && title != undefined && title.length > 0) {
        var titleLower = title.toLowerCase();

        slug = titleLower.replace(/e|é|è|ẽ|ẻ|ẹ|ê|ế|ề|ễ|ể|ệ/gi, 'e');

        slug = slug.replace(/a|á|à|ã|ả|ạ|ă|ắ|ằ|ẵ|ẳ|ặ|â|ấ|ầ|ẫ|ẩ|ậ/gi, 'a');

        slug = slug.replace(/o|ó|ò|õ|ỏ|ọ|ô|ố|ồ|ỗ|ổ|ộ|ơ|ớ|ờ|ỡ|ở|ợ/gi, 'o');

        slug = slug.replace(/u|ú|ù|ũ|ủ|ụ|ư|ứ|ừ|ữ|ử|ự/gi, 'u');

        slug = slug.replace(/đ/gi, 'd');

        slug = slug.replace(/\s*$/g, '');

        slug = slug.replace(/\s+/g, '-');
    }
    return slug;
};

function pathImg(title) {
    if (title != null && title != undefined && title.length > 0) {
        title = config.BASE_URLCms + "uploads/thumb" + title;
    }
    return title;
};

function urlBase(title) {
    if (title != null && title != undefined && title.length > 0) {
        title = config.BASE_URLWeb + title + ".html";
    }
    return title;
};

/***/ }),

/***/ 927:
/***/ (function(module, exports, __webpack_require__) {

var META = __webpack_require__(178)('meta');
var isObject = __webpack_require__(56);
var has = __webpack_require__(72);
var setDesc = __webpack_require__(54).f;
var id = 0;
var isExtensible = Object.isExtensible || function () {
  return true;
};
var FREEZE = !__webpack_require__(103)(function () {
  return isExtensible(Object.preventExtensions({}));
});
var setMeta = function (it) {
  setDesc(it, META, { value: {
    i: 'O' + ++id, // object ID
    w: {}          // weak collections IDs
  } });
};
var fastKey = function (it, create) {
  // return primitive with prefix
  if (!isObject(it)) return typeof it == 'symbol' ? it : (typeof it == 'string' ? 'S' : 'P') + it;
  if (!has(it, META)) {
    // can't set metadata to uncaught frozen object
    if (!isExtensible(it)) return 'F';
    // not necessary to add metadata
    if (!create) return 'E';
    // add missing metadata
    setMeta(it);
  // return object ID
  } return it[META].i;
};
var getWeak = function (it, create) {
  if (!has(it, META)) {
    // can't set metadata to uncaught frozen object
    if (!isExtensible(it)) return true;
    // not necessary to add metadata
    if (!create) return false;
    // add missing metadata
    setMeta(it);
  // return hash weak collections IDs
  } return it[META].w;
};
// add metadata on freeze-family methods calling
var onFreeze = function (it) {
  if (FREEZE && meta.NEED && isExtensible(it) && !has(it, META)) setMeta(it);
  return it;
};
var meta = module.exports = {
  KEY: META,
  NEED: false,
  fastKey: fastKey,
  getWeak: getWeak,
  onFreeze: onFreeze
};


/***/ }),

/***/ 928:
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__(953);
if(typeof content === 'string') content = [[module.i, content, '']];
// add the styles to the DOM
var update = __webpack_require__(175)(content, {});
if(content.locals) module.exports = content.locals;
// Hot Module Replacement
if(false) {
	// When the styles change, update the <style> tags
	if(!content.locals) {
		module.hot.accept("!!../../../css-loader/index.js!./vue-treeselect.css", function() {
			var newContent = require("!!../../../css-loader/index.js!./vue-treeselect.css");
			if(typeof newContent === 'string') newContent = [[module.id, newContent, '']];
			update(newContent);
		});
	}
	// When the module is disposed, remove the <style> tags
	module.hot.dispose(function() { update(); });
}

/***/ }),

/***/ 929:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _defineProperty = __webpack_require__(778);

var _defineProperty2 = _interopRequireDefault(_defineProperty);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (obj, key, value) {
  if (key in obj) {
    (0, _defineProperty2.default)(obj, key, {
      value: value,
      enumerable: true,
      configurable: true,
      writable: true
    });
  } else {
    obj[key] = value;
  }

  return obj;
};

/***/ }),

/***/ 931:
/***/ (function(module, exports) {

function _arrayWithHoles(arr) {
  if (Array.isArray(arr)) return arr;
}

module.exports = _arrayWithHoles;

/***/ }),

/***/ 932:
/***/ (function(module, exports, __webpack_require__) {

var arrayLikeToArray = __webpack_require__(779);

function _arrayWithoutHoles(arr) {
  if (Array.isArray(arr)) return arrayLikeToArray(arr);
}

module.exports = _arrayWithoutHoles;

/***/ }),

/***/ 933:
/***/ (function(module, exports) {

function _defineProperty(obj, key, value) {
  if (key in obj) {
    Object.defineProperty(obj, key, {
      value: value,
      enumerable: true,
      configurable: true,
      writable: true
    });
  } else {
    obj[key] = value;
  }

  return obj;
}

module.exports = _defineProperty;

/***/ }),

/***/ 934:
/***/ (function(module, exports) {

function _iterableToArray(iter) {
  if (typeof Symbol !== "undefined" && Symbol.iterator in Object(iter)) return Array.from(iter);
}

module.exports = _iterableToArray;

/***/ }),

/***/ 935:
/***/ (function(module, exports) {

function _iterableToArrayLimit(arr, i) {
  if (typeof Symbol === "undefined" || !(Symbol.iterator in Object(arr))) return;
  var _arr = [];
  var _n = true;
  var _d = false;
  var _e = undefined;

  try {
    for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) {
      _arr.push(_s.value);

      if (i && _arr.length === i) break;
    }
  } catch (err) {
    _d = true;
    _e = err;
  } finally {
    try {
      if (!_n && _i["return"] != null) _i["return"]();
    } finally {
      if (_d) throw _e;
    }
  }

  return _arr;
}

module.exports = _iterableToArrayLimit;

/***/ }),

/***/ 936:
/***/ (function(module, exports) {

function _nonIterableRest() {
  throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}

module.exports = _nonIterableRest;

/***/ }),

/***/ 937:
/***/ (function(module, exports) {

function _nonIterableSpread() {
  throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}

module.exports = _nonIterableSpread;

/***/ }),

/***/ 938:
/***/ (function(module, exports, __webpack_require__) {

var arrayWithHoles = __webpack_require__(931);

var iterableToArrayLimit = __webpack_require__(935);

var unsupportedIterableToArray = __webpack_require__(780);

var nonIterableRest = __webpack_require__(936);

function _slicedToArray(arr, i) {
  return arrayWithHoles(arr) || iterableToArrayLimit(arr, i) || unsupportedIterableToArray(arr, i) || nonIterableRest();
}

module.exports = _slicedToArray;

/***/ }),

/***/ 939:
/***/ (function(module, exports, __webpack_require__) {

var arrayWithoutHoles = __webpack_require__(932);

var iterableToArray = __webpack_require__(934);

var unsupportedIterableToArray = __webpack_require__(780);

var nonIterableSpread = __webpack_require__(937);

function _toConsumableArray(arr) {
  return arrayWithoutHoles(arr) || iterableToArray(arr) || unsupportedIterableToArray(arr) || nonIterableSpread();
}

module.exports = _toConsumableArray;

/***/ }),

/***/ 940:
/***/ (function(module, exports) {

function _typeof(obj) {
  "@babel/helpers - typeof";

  if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
    module.exports = _typeof = function _typeof(obj) {
      return typeof obj;
    };
  } else {
    module.exports = _typeof = function _typeof(obj) {
      return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj;
    };
  }

  return _typeof(obj);
}

module.exports = _typeof;

/***/ }),

/***/ 941:
/***/ (function(module, exports) {

var nestRE = /^(attrs|props|on|nativeOn|class|style|hook)$/

module.exports = function mergeJSXProps (objs) {
  return objs.reduce(function (a, b) {
    var aa, bb, key, nestedKey, temp
    for (key in b) {
      aa = a[key]
      bb = b[key]
      if (aa && nestRE.test(key)) {
        // normalize class
        if (key === 'class') {
          if (typeof aa === 'string') {
            temp = aa
            a[key] = aa = {}
            aa[temp] = true
          }
          if (typeof bb === 'string') {
            temp = bb
            b[key] = bb = {}
            bb[temp] = true
          }
        }
        if (key === 'on' || key === 'nativeOn' || key === 'hook') {
          // merge functions
          for (nestedKey in bb) {
            aa[nestedKey] = mergeFn(aa[nestedKey], bb[nestedKey])
          }
        } else if (Array.isArray(aa)) {
          a[key] = aa.concat(bb)
        } else if (Array.isArray(bb)) {
          a[key] = [aa].concat(bb)
        } else {
          for (nestedKey in bb) {
            aa[nestedKey] = bb[nestedKey]
          }
        }
      } else {
        a[key] = b[key]
      }
    }
    return a
  }, {})
}

function mergeFn (a, b) {
  return function () {
    a && a.apply(this, arguments)
    b && b.apply(this, arguments)
  }
}


/***/ }),

/***/ 943:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;

var _assign = __webpack_require__(374);

var _assign2 = _interopRequireDefault(_assign);

var _defineProperty = __webpack_require__(778);

var _defineProperty2 = _interopRequireDefault(_defineProperty);

var _iterator = __webpack_require__(782);

var _iterator2 = _interopRequireDefault(_iterator);

var _symbol = __webpack_require__(781);

var _symbol2 = _interopRequireDefault(_symbol);

var _typeof3 = __webpack_require__(944);

var _typeof4 = _interopRequireDefault(_typeof3);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

(function (global, factory) {
  ( false ? 'undefined' : (0, _typeof4.default)(exports)) === 'object' && typeof module !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),
				__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?
				(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :
				__WEBPACK_AMD_DEFINE_FACTORY__),
				__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : global['js-tree-list'] = factory();
})(undefined, function () {
  'use strict';

  var _typeof = typeof _symbol2.default === "function" && (0, _typeof4.default)(_iterator2.default) === "symbol" ? function (obj) {
    return typeof obj === 'undefined' ? 'undefined' : (0, _typeof4.default)(obj);
  } : function (obj) {
    return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj === 'undefined' ? 'undefined' : (0, _typeof4.default)(obj);
  };

  var classCallCheck = function classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  };

  var createClass = function () {
    function defineProperties(target, props) {
      for (var i = 0; i < props.length; i++) {
        var descriptor = props[i];
        descriptor.enumerable = descriptor.enumerable || false;
        descriptor.configurable = true;
        if ("value" in descriptor) descriptor.writable = true;
        (0, _defineProperty2.default)(target, descriptor.key, descriptor);
      }
    }

    return function (Constructor, protoProps, staticProps) {
      if (protoProps) defineProperties(Constructor.prototype, protoProps);
      if (staticProps) defineProperties(Constructor, staticProps);
      return Constructor;
    };
  }();

  var defineProperty = function defineProperty(obj, key, value) {
    if (key in obj) {
      (0, _defineProperty2.default)(obj, key, {
        value: value,
        enumerable: true,
        configurable: true,
        writable: true
      });
    } else {
      obj[key] = value;
    }

    return obj;
  };

  var Node = function () {
    function Node(content) {
      classCallCheck(this, Node);

      this.content = content;
      this.children = [];
      this.length = 0;
    }

    createClass(Node, [{
      key: 'get',
      value: function get$$1(fieldKey) {
        if (typeof this.content[fieldKey] !== 'undefined') {
          return this.content[fieldKey];
        }
      }
    }, {
      key: 'set',
      value: function set$$1(fieldKey, value) {
        return !!(this.content[fieldKey] = value);
      }
    }, {
      key: 'add',
      value: function add(child) {
        var node = child instanceof Node ? child : new Node(child);
        node.parent = this;
        this.length++;
        this.children.push(node);
        return node;
      }
    }, {
      key: 'remove',
      value: function remove(callback) {
        var index = this.children.findIndex(callback);
        if (index > -1) {
          var removeItems = this.children.splice(index, 1);
          this.length--;
          return removeItems;
        }
        return [];
      }
    }, {
      key: 'sort',
      value: function sort(compare) {
        return this.children.sort(compare);
      }
    }, {
      key: 'traversal',
      value: function traversal(criteria, callback) {
        criteria = criteria || function () {
          return true;
        };
        this.children.filter(criteria).forEach(callback);
      }
    }]);
    return Node;
  }();

  var removeEmptyChildren = function removeEmptyChildren(jTree) {
    var node = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;
    var options = arguments[2];
    var key_children = options.key_children;

    node = node || jTree[0];
    if (node[key_children].length === 0) {
      delete node[key_children];
    } else {
      node[key_children].forEach(function (item) {
        removeEmptyChildren(jTree, item, options);
      });
    }
  };

  var searchNode = function searchNode(tree, node, criteria, options) {
    var currentNode = node || tree.rootNode;
    if (criteria(currentNode)) {
      return currentNode;
    }
    var children = currentNode.children;
    var target = null;
    for (var i = 0; i < children.length; i++) {
      var item = children[i];
      target = searchNode(tree, item, criteria);
      if (target) {
        return target;
      }
    }
  };

  var traversalTree = function traversalTree(tree) {
    var node = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;
    var criteria = arguments[2];
    var callback = arguments[3];

    var currentNode = node || tree.rootNode;
    if (!node) {
      if (typeof criteria === 'function' && criteria(currentNode)) {
        callback(currentNode);
      } else if (criteria === null) {
        callback(currentNode);
      }
    }
    currentNode.traversal(criteria, callback);
    var children = currentNode.children;

    children.forEach(function (item) {
      traversalTree(tree, item, criteria, callback);
    });
  };

  var serializeTree = function serializeTree(tree) {
    var node = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;
    var target = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : [];
    var options = arguments[3];
    var key_children = options.key_children;

    node = node || tree.rootNode;
    if (!node) {
      return null;
    }
    var index = target.push((0, _assign2.default)(defineProperty({}, key_children, []), node.content));
    node.children.forEach(function (item) {
      serializeTree(tree, item, target[index - 1][key_children], options);
    });
    return target;
  };

  var Tree = function () {
    function Tree() {
      var object = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : undefined;
      classCallCheck(this, Tree);

      this.rootNode = null;
      if (object) {
        this.rootNode = new Node(object);
      }
    }

    createClass(Tree, [{
      key: 'get',
      value: function get$$1(path) {
        return this.rootNode.get(path);
      }

    }, {
      key: 'set',
      value: function set$$1(path, value) {
        this.rootNode.set(path, value);
      }
    }, {
      key: 'add',
      value: function add(callback, object) {
        var type = typeof callback === 'undefined' ? 'undefined' : _typeof(callback);
        if (type === 'string' && callback === 'root') {
          this.rootNode = new Node(object);
          return this;
        } else if (type === 'function') {
          var target = searchNode(this, null, callback);
          if (target && target.add(object)) {
            return this;
          } else {
            console.log('Warning', object);
          }
        }
      }
    }, {
      key: 'contains',
      value: function contains(criteria) {
        return searchNode(this, null, criteria);
      }
    }, {
      key: 'remove',
      value: function remove(criteria) {
        var targetNode = this.contains(criteria);
        if (targetNode) {
          return !!targetNode.parent.remove(criteria);
        }
        return false;
      }
    }, {
      key: 'move',
      value: function move(search, destination) {
        var targetNode = this.contains(search);
        if (targetNode && this.remove(search)) {
          var destinationNode = this.contains(destination);
          return !!destinationNode.add(targetNode);
        }
        return false;
      }
    }, {
      key: 'traversal',
      value: function traversal(criteria, callback) {
        traversalTree(this, null, criteria, callback);
      }
    }, {
      key: 'sort',
      value: function sort(compare) {
        this.traversal(null, function (currentNode) {
          currentNode.sort(compare);
        });
      }
    }, {
      key: 'toJson',
      value: function toJson() {
        var options = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};

        var optionsDefault = {
          key_children: 'children',
          empty_children: true
        };
        options = (0, _assign2.default)(optionsDefault, options);
        var result = serializeTree(this, null, [], options);

        if (!options.empty_children) {
          removeEmptyChildren(result, null, options);
        }

        if (result && result.length > 0) {
          return result[0];
        } else {
          return [];
        }
      }
    }]);
    return Tree;
  }();

  var defaultOptions = {
    key_id: 'id',
    key_parent: 'parent',
    key_child: 'child',
    key_last: null,
    uuid: false,
    empty_children: false
  };

  function sortBy(collection, propertyA, propertyB) {
    return collection.sort(function (a, b) {
      if (a[propertyB] < b[propertyB]) {
        if (a[propertyA] > b[propertyA]) {
          return 1;
        }
        return -1;
      } else {
        if (a[propertyA] < b[propertyA]) {
          return -1;
        }
        return 1;
      }
    });
  }

  var ListToTree = function () {
    function ListToTree(list) {
      var options = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};
      classCallCheck(this, ListToTree);

      var _list = list.map(function (item) {
        return item;
      });

      options = (0, _assign2.default)({}, defaultOptions, options);
      this.options = options;
      var _options = options,
          key_id = _options.key_id,
          key_parent = _options.key_parent,
          uuid = _options.uuid;

      if (uuid === false) {
        sortBy(_list, key_parent, key_id);
      }

      var tree = new Tree(defineProperty({}, key_id, 0));
      _list.forEach(function (item, index) {
        tree.add(function (parentNode) {
          return parentNode.get(key_id) === item[key_parent] || item[key_parent] === null;
        }, item);
      });

      this.tree = tree;
    }

    createClass(ListToTree, [{
      key: 'sort',
      value: function sort(criteria) {
        this.tree.sort(criteria);
      }
    }, {
      key: 'last',
      value: function last(val, key_id, key_last, key_child) {
        for (var n in val) {
          if (val[n][key_child] && val[n][key_child].length) {
            this.last(val[n][key_child], key_id, key_last, key_child);
          }
          if (val[n][key_last] !== 0) {
            if (n - 1 >= 0 && val[n - 1][key_id] !== val[n][key_last] || n - 1 < 0) {
              var tmp = val.splice(n, 1);
              val.splice(n + 1, 0, tmp[0]);
            }
          }
        }
      }
    }, {
      key: 'GetTree',
      value: function GetTree() {
        var _options2 = this.options,
            key_id = _options2.key_id,
            key_child = _options2.key_child,
            empty_children = _options2.empty_children,
            key_last = _options2.key_last;

        var json = this.tree.toJson({
          key_children: key_child,
          empty_children: empty_children
        })[key_child];

        if (key_last) {
          this.last(json, key_id, key_last, key_child);
        }
        return json;
      }
    }]);
    return ListToTree;
  }();

  var index = {
    ListToTree: ListToTree,
    Tree: Tree,
    Node: Node
  };

  return index;
});

/***/ }),

/***/ 944:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _iterator = __webpack_require__(782);

var _iterator2 = _interopRequireDefault(_iterator);

var _symbol = __webpack_require__(781);

var _symbol2 = _interopRequireDefault(_symbol);

var _typeof = typeof _symbol2.default === "function" && typeof _iterator2.default === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj; };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = typeof _symbol2.default === "function" && _typeof(_iterator2.default) === "symbol" ? function (obj) {
  return typeof obj === "undefined" ? "undefined" : _typeof(obj);
} : function (obj) {
  return obj && typeof _symbol2.default === "function" && obj.constructor === _symbol2.default && obj !== _symbol2.default.prototype ? "symbol" : typeof obj === "undefined" ? "undefined" : _typeof(obj);
};

/***/ }),

/***/ 945:
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(950);
__webpack_require__(384);
__webpack_require__(951);
__webpack_require__(952);
module.exports = __webpack_require__(20).Symbol;


/***/ }),

/***/ 946:
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(180);
__webpack_require__(378);
module.exports = __webpack_require__(776).f('iterator');


/***/ }),

/***/ 947:
/***/ (function(module, exports, __webpack_require__) {

// all enumerable object keys, includes symbols
var getKeys = __webpack_require__(177);
var gOPS = __webpack_require__(375);
var pIE = __webpack_require__(372);
module.exports = function (it) {
  var result = getKeys(it);
  var getSymbols = gOPS.f;
  if (getSymbols) {
    var symbols = getSymbols(it);
    var isEnum = pIE.f;
    var i = 0;
    var key;
    while (symbols.length > i) if (isEnum.call(it, key = symbols[i++])) result.push(key);
  } return result;
};


/***/ }),

/***/ 948:
/***/ (function(module, exports, __webpack_require__) {

var pIE = __webpack_require__(372);
var createDesc = __webpack_require__(105);
var toIObject = __webpack_require__(102);
var toPrimitive = __webpack_require__(376);
var has = __webpack_require__(72);
var IE8_DOM_DEFINE = __webpack_require__(379);
var gOPD = Object.getOwnPropertyDescriptor;

exports.f = __webpack_require__(44) ? gOPD : function getOwnPropertyDescriptor(O, P) {
  O = toIObject(O);
  P = toPrimitive(P, true);
  if (IE8_DOM_DEFINE) try {
    return gOPD(O, P);
  } catch (e) { /* empty */ }
  if (has(O, P)) return createDesc(!pIE.f.call(O, P), O[P]);
};


/***/ }),

/***/ 949:
/***/ (function(module, exports, __webpack_require__) {

// fallback for IE11 buggy Object.getOwnPropertyNames with iframe and window
var toIObject = __webpack_require__(102);
var gOPN = __webpack_require__(783).f;
var toString = {}.toString;

var windowNames = typeof window == 'object' && window && Object.getOwnPropertyNames
  ? Object.getOwnPropertyNames(window) : [];

var getWindowNames = function (it) {
  try {
    return gOPN(it);
  } catch (e) {
    return windowNames.slice();
  }
};

module.exports.f = function getOwnPropertyNames(it) {
  return windowNames && toString.call(it) == '[object Window]' ? getWindowNames(it) : gOPN(toIObject(it));
};


/***/ }),

/***/ 950:
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// ECMAScript 6 symbols shim
var global = __webpack_require__(22);
var has = __webpack_require__(72);
var DESCRIPTORS = __webpack_require__(44);
var $export = __webpack_require__(32);
var redefine = __webpack_require__(383);
var META = __webpack_require__(927).KEY;
var $fails = __webpack_require__(103);
var shared = __webpack_require__(182);
var setToStringTag = __webpack_require__(110);
var uid = __webpack_require__(178);
var wks = __webpack_require__(23);
var wksExt = __webpack_require__(776);
var wksDefine = __webpack_require__(775);
var enumKeys = __webpack_require__(947);
var isArray = __webpack_require__(380);
var anObject = __webpack_require__(38);
var isObject = __webpack_require__(56);
var toObject = __webpack_require__(109);
var toIObject = __webpack_require__(102);
var toPrimitive = __webpack_require__(376);
var createDesc = __webpack_require__(105);
var _create = __webpack_require__(381);
var gOPNExt = __webpack_require__(949);
var $GOPD = __webpack_require__(948);
var $GOPS = __webpack_require__(375);
var $DP = __webpack_require__(54);
var $keys = __webpack_require__(177);
var gOPD = $GOPD.f;
var dP = $DP.f;
var gOPN = gOPNExt.f;
var $Symbol = global.Symbol;
var $JSON = global.JSON;
var _stringify = $JSON && $JSON.stringify;
var PROTOTYPE = 'prototype';
var HIDDEN = wks('_hidden');
var TO_PRIMITIVE = wks('toPrimitive');
var isEnum = {}.propertyIsEnumerable;
var SymbolRegistry = shared('symbol-registry');
var AllSymbols = shared('symbols');
var OPSymbols = shared('op-symbols');
var ObjectProto = Object[PROTOTYPE];
var USE_NATIVE = typeof $Symbol == 'function' && !!$GOPS.f;
var QObject = global.QObject;
// Don't use setters in Qt Script, https://github.com/zloirock/core-js/issues/173
var setter = !QObject || !QObject[PROTOTYPE] || !QObject[PROTOTYPE].findChild;

// fallback for old Android, https://code.google.com/p/v8/issues/detail?id=687
var setSymbolDesc = DESCRIPTORS && $fails(function () {
  return _create(dP({}, 'a', {
    get: function () { return dP(this, 'a', { value: 7 }).a; }
  })).a != 7;
}) ? function (it, key, D) {
  var protoDesc = gOPD(ObjectProto, key);
  if (protoDesc) delete ObjectProto[key];
  dP(it, key, D);
  if (protoDesc && it !== ObjectProto) dP(ObjectProto, key, protoDesc);
} : dP;

var wrap = function (tag) {
  var sym = AllSymbols[tag] = _create($Symbol[PROTOTYPE]);
  sym._k = tag;
  return sym;
};

var isSymbol = USE_NATIVE && typeof $Symbol.iterator == 'symbol' ? function (it) {
  return typeof it == 'symbol';
} : function (it) {
  return it instanceof $Symbol;
};

var $defineProperty = function defineProperty(it, key, D) {
  if (it === ObjectProto) $defineProperty(OPSymbols, key, D);
  anObject(it);
  key = toPrimitive(key, true);
  anObject(D);
  if (has(AllSymbols, key)) {
    if (!D.enumerable) {
      if (!has(it, HIDDEN)) dP(it, HIDDEN, createDesc(1, {}));
      it[HIDDEN][key] = true;
    } else {
      if (has(it, HIDDEN) && it[HIDDEN][key]) it[HIDDEN][key] = false;
      D = _create(D, { enumerable: createDesc(0, false) });
    } return setSymbolDesc(it, key, D);
  } return dP(it, key, D);
};
var $defineProperties = function defineProperties(it, P) {
  anObject(it);
  var keys = enumKeys(P = toIObject(P));
  var i = 0;
  var l = keys.length;
  var key;
  while (l > i) $defineProperty(it, key = keys[i++], P[key]);
  return it;
};
var $create = function create(it, P) {
  return P === undefined ? _create(it) : $defineProperties(_create(it), P);
};
var $propertyIsEnumerable = function propertyIsEnumerable(key) {
  var E = isEnum.call(this, key = toPrimitive(key, true));
  if (this === ObjectProto && has(AllSymbols, key) && !has(OPSymbols, key)) return false;
  return E || !has(this, key) || !has(AllSymbols, key) || has(this, HIDDEN) && this[HIDDEN][key] ? E : true;
};
var $getOwnPropertyDescriptor = function getOwnPropertyDescriptor(it, key) {
  it = toIObject(it);
  key = toPrimitive(key, true);
  if (it === ObjectProto && has(AllSymbols, key) && !has(OPSymbols, key)) return;
  var D = gOPD(it, key);
  if (D && has(AllSymbols, key) && !(has(it, HIDDEN) && it[HIDDEN][key])) D.enumerable = true;
  return D;
};
var $getOwnPropertyNames = function getOwnPropertyNames(it) {
  var names = gOPN(toIObject(it));
  var result = [];
  var i = 0;
  var key;
  while (names.length > i) {
    if (!has(AllSymbols, key = names[i++]) && key != HIDDEN && key != META) result.push(key);
  } return result;
};
var $getOwnPropertySymbols = function getOwnPropertySymbols(it) {
  var IS_OP = it === ObjectProto;
  var names = gOPN(IS_OP ? OPSymbols : toIObject(it));
  var result = [];
  var i = 0;
  var key;
  while (names.length > i) {
    if (has(AllSymbols, key = names[i++]) && (IS_OP ? has(ObjectProto, key) : true)) result.push(AllSymbols[key]);
  } return result;
};

// 19.4.1.1 Symbol([description])
if (!USE_NATIVE) {
  $Symbol = function Symbol() {
    if (this instanceof $Symbol) throw TypeError('Symbol is not a constructor!');
    var tag = uid(arguments.length > 0 ? arguments[0] : undefined);
    var $set = function (value) {
      if (this === ObjectProto) $set.call(OPSymbols, value);
      if (has(this, HIDDEN) && has(this[HIDDEN], tag)) this[HIDDEN][tag] = false;
      setSymbolDesc(this, tag, createDesc(1, value));
    };
    if (DESCRIPTORS && setter) setSymbolDesc(ObjectProto, tag, { configurable: true, set: $set });
    return wrap(tag);
  };
  redefine($Symbol[PROTOTYPE], 'toString', function toString() {
    return this._k;
  });

  $GOPD.f = $getOwnPropertyDescriptor;
  $DP.f = $defineProperty;
  __webpack_require__(783).f = gOPNExt.f = $getOwnPropertyNames;
  __webpack_require__(372).f = $propertyIsEnumerable;
  $GOPS.f = $getOwnPropertySymbols;

  if (DESCRIPTORS && !__webpack_require__(104)) {
    redefine(ObjectProto, 'propertyIsEnumerable', $propertyIsEnumerable, true);
  }

  wksExt.f = function (name) {
    return wrap(wks(name));
  };
}

$export($export.G + $export.W + $export.F * !USE_NATIVE, { Symbol: $Symbol });

for (var es6Symbols = (
  // 19.4.2.2, 19.4.2.3, 19.4.2.4, 19.4.2.6, 19.4.2.8, 19.4.2.9, 19.4.2.10, 19.4.2.11, 19.4.2.12, 19.4.2.13, 19.4.2.14
  'hasInstance,isConcatSpreadable,iterator,match,replace,search,species,split,toPrimitive,toStringTag,unscopables'
).split(','), j = 0; es6Symbols.length > j;)wks(es6Symbols[j++]);

for (var wellKnownSymbols = $keys(wks.store), k = 0; wellKnownSymbols.length > k;) wksDefine(wellKnownSymbols[k++]);

$export($export.S + $export.F * !USE_NATIVE, 'Symbol', {
  // 19.4.2.1 Symbol.for(key)
  'for': function (key) {
    return has(SymbolRegistry, key += '')
      ? SymbolRegistry[key]
      : SymbolRegistry[key] = $Symbol(key);
  },
  // 19.4.2.5 Symbol.keyFor(sym)
  keyFor: function keyFor(sym) {
    if (!isSymbol(sym)) throw TypeError(sym + ' is not a symbol!');
    for (var key in SymbolRegistry) if (SymbolRegistry[key] === sym) return key;
  },
  useSetter: function () { setter = true; },
  useSimple: function () { setter = false; }
});

$export($export.S + $export.F * !USE_NATIVE, 'Object', {
  // 19.1.2.2 Object.create(O [, Properties])
  create: $create,
  // 19.1.2.4 Object.defineProperty(O, P, Attributes)
  defineProperty: $defineProperty,
  // 19.1.2.3 Object.defineProperties(O, Properties)
  defineProperties: $defineProperties,
  // 19.1.2.6 Object.getOwnPropertyDescriptor(O, P)
  getOwnPropertyDescriptor: $getOwnPropertyDescriptor,
  // 19.1.2.7 Object.getOwnPropertyNames(O)
  getOwnPropertyNames: $getOwnPropertyNames,
  // 19.1.2.8 Object.getOwnPropertySymbols(O)
  getOwnPropertySymbols: $getOwnPropertySymbols
});

// Chrome 38 and 39 `Object.getOwnPropertySymbols` fails on primitives
// https://bugs.chromium.org/p/v8/issues/detail?id=3443
var FAILS_ON_PRIMITIVES = $fails(function () { $GOPS.f(1); });

$export($export.S + $export.F * FAILS_ON_PRIMITIVES, 'Object', {
  getOwnPropertySymbols: function getOwnPropertySymbols(it) {
    return $GOPS.f(toObject(it));
  }
});

// 24.3.2 JSON.stringify(value [, replacer [, space]])
$JSON && $export($export.S + $export.F * (!USE_NATIVE || $fails(function () {
  var S = $Symbol();
  // MS Edge converts symbol values to JSON as {}
  // WebKit converts symbol values to JSON as null
  // V8 throws on boxed symbols
  return _stringify([S]) != '[null]' || _stringify({ a: S }) != '{}' || _stringify(Object(S)) != '{}';
})), 'JSON', {
  stringify: function stringify(it) {
    var args = [it];
    var i = 1;
    var replacer, $replacer;
    while (arguments.length > i) args.push(arguments[i++]);
    $replacer = replacer = args[1];
    if (!isObject(replacer) && it === undefined || isSymbol(it)) return; // IE8 returns string on undefined
    if (!isArray(replacer)) replacer = function (key, value) {
      if (typeof $replacer == 'function') value = $replacer.call(this, key, value);
      if (!isSymbol(value)) return value;
    };
    args[1] = replacer;
    return _stringify.apply($JSON, args);
  }
});

// 19.4.3.4 Symbol.prototype[@@toPrimitive](hint)
$Symbol[PROTOTYPE][TO_PRIMITIVE] || __webpack_require__(47)($Symbol[PROTOTYPE], TO_PRIMITIVE, $Symbol[PROTOTYPE].valueOf);
// 19.4.3.5 Symbol.prototype[@@toStringTag]
setToStringTag($Symbol, 'Symbol');
// 20.2.1.9 Math[@@toStringTag]
setToStringTag(Math, 'Math', true);
// 24.3.3 JSON[@@toStringTag]
setToStringTag(global.JSON, 'JSON', true);


/***/ }),

/***/ 951:
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(775)('asyncIterator');


/***/ }),

/***/ 952:
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(775)('observable');


/***/ }),

/***/ 953:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(53)();
// imports


// module
exports.push([module.i, "/*!\n * vue-treeselect v0.4.0 | (c) 2017-2019 Riophae Lee\n * Released under the MIT License.\n * https://vue-treeselect.js.org/\n */\n/**\n * Dependencies\n */\n/**\n * Variables\n */\n/**\n * Mixins\n */\n/**\n * Helpers\n */\n.vue-treeselect-helper-hide {\n  display: none;\n}\n.vue-treeselect-helper-zoom-effect-off {\n  -ms-transform: none !important;\n      transform: none !important;\n}\n/**\n * Animations\n */\n@keyframes vue-treeselect-animation-fade-in {\n  0% {\n    opacity: 0;\n  }\n}\n@keyframes vue-treeselect-animation-bounce {\n  0%,\n  100% {\n    transform: scale(0);\n  }\n  50% {\n    transform: scale(1);\n  }\n}\n@keyframes vue-treeselect-animation-rotate {\n  100% {\n    transform: rotate(360deg);\n  }\n}\n/**\n * Transitions\n */\n.vue-treeselect__multi-value-item--transition-enter-active,\n.vue-treeselect__multi-value-item--transition-leave-active {\n  transition-duration: 200ms;\n  transition-property: transform, opacity;\n}\n.vue-treeselect__multi-value-item--transition-enter-active {\n  transition-timing-function: cubic-bezier(0.075, 0.82, 0.165, 1);\n}\n.vue-treeselect__multi-value-item--transition-leave-active {\n  transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);\n  position: absolute;\n}\n.vue-treeselect__multi-value-item--transition-enter,\n.vue-treeselect__multi-value-item--transition-leave-to {\n  -ms-transform: scale(0.7);\n      transform: scale(0.7);\n  opacity: 0;\n}\n.vue-treeselect__multi-value-item--transition-move {\n  transition: 200ms transform cubic-bezier(0.165, 0.84, 0.44, 1);\n}\n/**\n * Namespace\n */\n.vue-treeselect {\n  position: relative;\n  text-align: left;\n}\n[dir=\"rtl\"] .vue-treeselect {\n  text-align: right;\n}\n.vue-treeselect div,\n.vue-treeselect span {\n  box-sizing: border-box;\n}\n.vue-treeselect svg {\n  fill: currentColor;\n}\n/**\n * Control\n */\n.vue-treeselect__control {\n  padding-left: 5px;\n  padding-right: 5px;\n  display: table;\n  table-layout: fixed;\n  width: 100%;\n  height: 36px;\n  border: 1px solid #ddd;\n  border-radius: 5px;\n  background: #fff;\n  transition-duration: 200ms;\n  transition-property: border-color, box-shadow, width, height, background-color, opacity;\n  transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);\n}\n.vue-treeselect:not(.vue-treeselect--disabled):not(.vue-treeselect--focused) .vue-treeselect__control:hover {\n  border-color: #cfcfcf;\n}\n.vue-treeselect--focused:not(.vue-treeselect--open) .vue-treeselect__control {\n  border-color: #039be5;\n  box-shadow: 0 0 0 3px rgba(3, 155, 229, 0.1);\n}\n.vue-treeselect--disabled .vue-treeselect__control {\n  background-color: #f9f9f9;\n}\n.vue-treeselect--open .vue-treeselect__control {\n  border-color: #cfcfcf;\n}\n.vue-treeselect--open.vue-treeselect--open-below .vue-treeselect__control {\n  border-bottom-left-radius: 0;\n  border-bottom-right-radius: 0;\n}\n.vue-treeselect--open.vue-treeselect--open-above .vue-treeselect__control {\n  border-top-left-radius: 0;\n  border-top-right-radius: 0;\n}\n.vue-treeselect__value-container,\n.vue-treeselect__multi-value {\n  width: 100%;\n  vertical-align: middle;\n}\n.vue-treeselect__value-container {\n  display: table-cell;\n  position: relative;\n}\n.vue-treeselect--searchable:not(.vue-treeselect--disabled) .vue-treeselect__value-container {\n  cursor: text;\n}\n.vue-treeselect__multi-value {\n  display: inline-block;\n}\n.vue-treeselect--has-value .vue-treeselect__multi-value {\n  margin-bottom: 5px;\n}\n.vue-treeselect__placeholder,\n.vue-treeselect__single-value {\n  overflow: hidden;\n  text-overflow: ellipsis;\n  white-space: nowrap;\n  padding-left: 5px;\n  padding-right: 5px;\n  position: absolute;\n  top: 0;\n  right: 0;\n  bottom: 0;\n  left: 0;\n  line-height: 34px;\n  -webkit-user-select: none;\n     -moz-user-select: none;\n      -ms-user-select: none;\n          user-select: none;\n  pointer-events: none;\n}\n.vue-treeselect__placeholder {\n  color: #bdbdbd;\n}\n.vue-treeselect__single-value {\n  color: #333;\n}\n.vue-treeselect--focused.vue-treeselect--searchable .vue-treeselect__single-value {\n  color: #bdbdbd;\n}\n.vue-treeselect--disabled .vue-treeselect__single-value {\n  position: static;\n}\n.vue-treeselect__multi-value-item-container {\n  display: inline-block;\n  padding-top: 5px;\n  padding-right: 5px;\n  vertical-align: top;\n}\n[dir=\"rtl\"] .vue-treeselect__multi-value-item-container {\n  padding-right: 0;\n  padding-left: 5px;\n}\n.vue-treeselect__multi-value-item {\n  cursor: pointer;\n  display: inline-table;\n  background: #e3f2fd;\n  padding: 2px 0;\n  border: 1px solid transparent;\n  border-radius: 2px;\n  color: #039be5;\n  font-size: 12px;\n  vertical-align: top;\n}\n.vue-treeselect:not(.vue-treeselect--disabled) .vue-treeselect__multi-value-item:not(.vue-treeselect__multi-value-item-disabled):hover .vue-treeselect__multi-value-item:not(.vue-treeselect__multi-value-item-new) .vue-treeselect__multi-value-item:not(.vue-treeselect__multi-value-item-new):hover {\n  cursor: pointer;\n  background: #e3f2fd;\n  color: #039be5;\n}\n.vue-treeselect__multi-value-item.vue-treeselect__multi-value-item-disabled {\n  cursor: default;\n  background: #f5f5f5;\n  color: #757575;\n}\n.vue-treeselect--disabled .vue-treeselect__multi-value-item {\n  cursor: default;\n  background: #fff;\n  border-color: #e5e5e5;\n  color: #555;\n}\n.vue-treeselect__multi-value-item.vue-treeselect__multi-value-item-new {\n  background: #e8f5e9;\n}\n.vue-treeselect__multi-value-item.vue-treeselect__multi-value-item-new:hover {\n  background: #e8f5e9;\n}\n.vue-treeselect__value-remove,\n.vue-treeselect__multi-value-label {\n  display: table-cell;\n  padding: 0 5px;\n  vertical-align: middle;\n}\n.vue-treeselect__value-remove {\n  color: #039be5;\n  padding-left: 5px;\n  border-left: 1px solid #fff;\n  line-height: 0;\n}\n[dir=\"rtl\"] .vue-treeselect__value-remove {\n  border-left: 0 none;\n  border-right: 1px solid #fff;\n}\n.vue-treeselect__multi-value-item:hover .vue-treeselect__value-remove {\n  color: #e53935;\n}\n.vue-treeselect--disabled .vue-treeselect__value-remove,\n.vue-treeselect__multi-value-item-disabled .vue-treeselect__value-remove {\n  display: none;\n}\n.vue-treeselect__value-remove > svg {\n  width: 6px;\n  height: 6px;\n}\n.vue-treeselect__multi-value-label {\n  padding-right: 5px;\n  white-space: pre-line;\n  -webkit-user-select: none;\n     -moz-user-select: none;\n      -ms-user-select: none;\n          user-select: none;\n}\n.vue-treeselect__limit-tip {\n  display: inline-block;\n  padding-top: 5px;\n  padding-right: 5px;\n  vertical-align: top;\n}\n[dir=\"rtl\"] .vue-treeselect__limit-tip {\n  padding-right: 0;\n  padding-left: 5px;\n}\n.vue-treeselect__limit-tip-text {\n  cursor: default;\n  display: block;\n  margin: 2px 0;\n  padding: 1px 0;\n  color: #bdbdbd;\n  font-size: 12px;\n  font-weight: 600;\n}\n.vue-treeselect__input-container {\n  display: block;\n  max-width: 100%;\n  outline: none;\n}\n.vue-treeselect--single .vue-treeselect__input-container {\n  font-size: inherit;\n  height: 100%;\n}\n.vue-treeselect--multi .vue-treeselect__input-container {\n  display: inline-block;\n  font-size: 12px;\n  vertical-align: top;\n}\n.vue-treeselect--searchable .vue-treeselect__input-container {\n  padding-left: 5px;\n  padding-right: 5px;\n}\n.vue-treeselect--searchable.vue-treeselect--multi.vue-treeselect--has-value .vue-treeselect__input-container {\n  padding-top: 5px;\n  padding-left: 0;\n}\n[dir=\"rtl\"] .vue-treeselect--searchable.vue-treeselect--multi.vue-treeselect--has-value .vue-treeselect__input-container {\n  padding-left: 5px;\n  padding-right: 0;\n}\n.vue-treeselect--disabled .vue-treeselect__input-container {\n  display: none;\n}\n.vue-treeselect__input,\n.vue-treeselect__sizer {\n  margin: 0;\n  line-height: inherit;\n  font-family: inherit;\n  font-size: inherit;\n}\n.vue-treeselect__input {\n  max-width: 100%;\n  margin: 0;\n  padding: 0;\n  border: 0;\n  outline: none;\n  box-sizing: content-box;\n  box-shadow: none;\n  background: none transparent;\n  line-height: 1;\n  vertical-align: middle;\n}\n.vue-treeselect__input::-ms-clear {\n  display: none;\n}\n.vue-treeselect--single .vue-treeselect__input {\n  width: 100%;\n  height: 100%;\n}\n.vue-treeselect--multi .vue-treeselect__input {\n  padding-top: 3px;\n  padding-bottom: 3px;\n}\n.vue-treeselect--has-value .vue-treeselect__input {\n  line-height: inherit;\n  vertical-align: top;\n}\n.vue-treeselect__sizer {\n  position: absolute;\n  top: 0;\n  left: 0;\n  visibility: hidden;\n  height: 0;\n  overflow: scroll;\n  white-space: pre;\n}\n.vue-treeselect__x-container {\n  display: table-cell;\n  vertical-align: middle;\n  width: 20px;\n  text-align: center;\n  line-height: 0;\n  cursor: pointer;\n  color: #ccc;\n  animation: 200ms vue-treeselect-animation-fade-in cubic-bezier(0.075, 0.82, 0.165, 1);\n}\n.vue-treeselect__x-container:hover {\n  color: #e53935;\n}\n.vue-treeselect__x {\n  width: 8px;\n  height: 8px;\n}\n.vue-treeselect__control-arrow-container {\n  display: table-cell;\n  vertical-align: middle;\n  width: 20px;\n  text-align: center;\n  line-height: 0;\n  cursor: pointer;\n}\n.vue-treeselect--disabled .vue-treeselect__control-arrow-container {\n  cursor: default;\n}\n.vue-treeselect__control-arrow {\n  width: 9px;\n  height: 9px;\n  color: #ccc;\n}\n.vue-treeselect:not(.vue-treeselect--disabled) .vue-treeselect__control-arrow-container:hover .vue-treeselect__control-arrow {\n  color: #616161;\n}\n.vue-treeselect--disabled .vue-treeselect__control-arrow {\n  opacity: 0.35;\n}\n.vue-treeselect__control-arrow--rotated {\n  -ms-transform: rotate(180deg);\n      transform: rotateZ(180deg);\n}\n/**\n * Menu\n */\n.vue-treeselect__menu-container {\n  position: absolute;\n  left: 0;\n  width: 100%;\n  overflow: visible;\n  transition: 0s;\n}\n.vue-treeselect--open-below:not(.vue-treeselect--append-to-body) .vue-treeselect__menu-container {\n  top: 100%;\n}\n.vue-treeselect--open-above:not(.vue-treeselect--append-to-body) .vue-treeselect__menu-container {\n  bottom: 100%;\n}\n.vue-treeselect__menu {\n  cursor: default;\n  padding-top: 5px;\n  padding-bottom: 5px;\n  display: block;\n  position: absolute;\n  overflow-x: hidden;\n  overflow-y: auto;\n  width: auto;\n  border: 1px solid #cfcfcf;\n  background: #fff;\n  line-height: 180%;\n  -webkit-overflow-scrolling: touch;\n}\n.vue-treeselect--open-below .vue-treeselect__menu {\n  border-bottom-left-radius: 5px;\n  border-bottom-right-radius: 5px;\n  top: 0;\n  margin-top: -1px;\n  border-top-color: #f2f2f2;\n  box-shadow: 0 1px 0 rgba(0, 0, 0, 0.06);\n}\n.vue-treeselect--open-above .vue-treeselect__menu {\n  border-top-left-radius: 5px;\n  border-top-right-radius: 5px;\n  bottom: 0;\n  margin-bottom: -1px;\n  border-bottom-color: #f2f2f2;\n}\n.vue-treeselect__indent-level-0 .vue-treeselect__option {\n  padding-left: 5px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-0 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 5px;\n}\n.vue-treeselect__indent-level-0 .vue-treeselect__tip {\n  padding-left: 25px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-0 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 25px;\n}\n.vue-treeselect__indent-level-1 .vue-treeselect__option {\n  padding-left: 25px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-1 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 25px;\n}\n.vue-treeselect__indent-level-1 .vue-treeselect__tip {\n  padding-left: 45px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-1 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 45px;\n}\n.vue-treeselect__indent-level-2 .vue-treeselect__option {\n  padding-left: 45px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-2 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 45px;\n}\n.vue-treeselect__indent-level-2 .vue-treeselect__tip {\n  padding-left: 65px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-2 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 65px;\n}\n.vue-treeselect__indent-level-3 .vue-treeselect__option {\n  padding-left: 65px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-3 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 65px;\n}\n.vue-treeselect__indent-level-3 .vue-treeselect__tip {\n  padding-left: 85px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-3 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 85px;\n}\n.vue-treeselect__indent-level-4 .vue-treeselect__option {\n  padding-left: 85px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-4 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 85px;\n}\n.vue-treeselect__indent-level-4 .vue-treeselect__tip {\n  padding-left: 105px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-4 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 105px;\n}\n.vue-treeselect__indent-level-5 .vue-treeselect__option {\n  padding-left: 105px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-5 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 105px;\n}\n.vue-treeselect__indent-level-5 .vue-treeselect__tip {\n  padding-left: 125px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-5 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 125px;\n}\n.vue-treeselect__indent-level-6 .vue-treeselect__option {\n  padding-left: 125px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-6 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 125px;\n}\n.vue-treeselect__indent-level-6 .vue-treeselect__tip {\n  padding-left: 145px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-6 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 145px;\n}\n.vue-treeselect__indent-level-7 .vue-treeselect__option {\n  padding-left: 145px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-7 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 145px;\n}\n.vue-treeselect__indent-level-7 .vue-treeselect__tip {\n  padding-left: 165px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-7 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 165px;\n}\n.vue-treeselect__indent-level-8 .vue-treeselect__option {\n  padding-left: 165px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-8 .vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 165px;\n}\n.vue-treeselect__indent-level-8 .vue-treeselect__tip {\n  padding-left: 185px;\n}\n[dir=\"rtl\"] .vue-treeselect__indent-level-8 .vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 185px;\n}\n.vue-treeselect__option {\n  padding-left: 5px;\n  padding-right: 5px;\n  display: table;\n  table-layout: fixed;\n  width: 100%;\n}\n.vue-treeselect__option--highlight {\n  background: #f5f5f5;\n}\n.vue-treeselect--single .vue-treeselect__option--selected {\n  background: #e3f2fd;\n  font-weight: 600;\n}\n.vue-treeselect--single .vue-treeselect__option--selected:hover {\n  background: #e3f2fd;\n}\n.vue-treeselect__option--hide {\n  display: none;\n}\n.vue-treeselect__option-arrow-container,\n.vue-treeselect__option-arrow-placeholder {\n  display: table-cell;\n  vertical-align: middle;\n  width: 20px;\n  text-align: center;\n  line-height: 0;\n}\n.vue-treeselect__option-arrow-container {\n  cursor: pointer;\n}\n.vue-treeselect__option-arrow {\n  display: inline-block;\n  width: 9px;\n  height: 9px;\n  color: #ccc;\n  vertical-align: middle;\n  transition: 200ms transform cubic-bezier(0.19, 1, 0.22, 1);\n  -ms-transform: rotate(-90deg);\n      transform: rotateZ(-90deg);\n}\n[dir=\"rtl\"] .vue-treeselect__option-arrow {\n  -ms-transform: rotate(90deg);\n      transform: rotateZ(90deg);\n}\n.vue-treeselect__option-arrow-container:hover .vue-treeselect__option-arrow,\n.vue-treeselect--branch-nodes-disabled .vue-treeselect__option:hover .vue-treeselect__option-arrow {\n  color: #616161;\n}\n.vue-treeselect__option-arrow--rotated {\n  -ms-transform: rotate(0);\n      transform: rotateZ(0);\n}\n[dir=\"rtl\"] .vue-treeselect__option-arrow--rotated {\n  -ms-transform: rotate(0);\n      transform: rotateZ(0);\n}\n.vue-treeselect__option-arrow--rotated.vue-treeselect__option-arrow--prepare-enter {\n  -ms-transform: rotate(-90deg) !important;\n      transform: rotateZ(-90deg) !important;\n}\n[dir=\"rtl\"] .vue-treeselect__option-arrow--rotated.vue-treeselect__option-arrow--prepare-enter {\n  -ms-transform: rotate(90deg) !important;\n      transform: rotateZ(90deg) !important;\n}\n.vue-treeselect__label-container {\n  display: table-cell;\n  vertical-align: middle;\n  cursor: pointer;\n  display: table;\n  width: 100%;\n  table-layout: fixed;\n  color: inherit;\n}\n.vue-treeselect__option--disabled .vue-treeselect__label-container {\n  cursor: not-allowed;\n  color: rgba(0, 0, 0, 0.25);\n}\n.vue-treeselect__checkbox-container {\n  display: table-cell;\n  width: 20px;\n  min-width: 20px;\n  height: 100%;\n  text-align: center;\n  vertical-align: middle;\n}\n.vue-treeselect__checkbox {\n  display: block;\n  margin: auto;\n  width: 12px;\n  height: 12px;\n  border-width: 1px;\n  border-style: solid;\n  border-radius: 2px;\n  position: relative;\n  transition: 200ms all cubic-bezier(0.075, 0.82, 0.165, 1);\n}\n.vue-treeselect__check-mark,\n.vue-treeselect__minus-mark {\n  display: block;\n  position: absolute;\n  left: 1px;\n  top: 1px;\n  background-repeat: no-repeat;\n  opacity: 0;\n  transition: 200ms all ease;\n}\n.vue-treeselect__minus-mark {\n  width: 8px;\n  height: 8px;\n  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAgMAAAC5YVYYAAAACVBMVEUAAAD///////9zeKVjAAAAAnRSTlMAuLMp9oYAAAAPSURBVAjXY4CDrJUgBAMAGaECJ9dz3BAAAAAASUVORK5CYII=);\n  background-size: 8px 8px;\n}\n@media (-webkit-min-device-pixel-ratio: 1.5), (min-resolution: 1.5dppx) {\n  .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAgMAAABinRfyAAAADFBMVEUAAAD///////////84wDuoAAAAA3RSTlMAyTzPIdReAAAAGUlEQVQI12PAD+b///+Nof7//79gAsLFCwAx/w4blADeeQAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 2), (min-resolution: 192dpi) {\n  .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAgMAAABinRfyAAAADFBMVEUAAAD///////////84wDuoAAAAA3RSTlMAyTzPIdReAAAAGUlEQVQI12PAD+b///+Nof7//79gAsLFCwAx/w4blADeeQAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 3), (min-resolution: 288dpi) {\n  .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAD1BMVEUAAAD///////////////+PQt5oAAAABHRSTlMAy2EFIuWxUgAAACRJREFUGNNjGBBgJOICBY7KDCoucODEAJSAS6FwUJShGjAQAADBPRGrK2/FhgAAAABJRU5ErkJggg==);\n  }\n}\n.vue-treeselect__checkbox--indeterminate > .vue-treeselect__minus-mark {\n  opacity: 1;\n}\n.vue-treeselect__checkbox--disabled .vue-treeselect__minus-mark {\n  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAgMAAAC5YVYYAAAACVBMVEUAAADi4uLh4eHOxeSRAAAAAnRSTlMAuLMp9oYAAAAPSURBVAjXY4CDrJUgBAMAGaECJ9dz3BAAAAAASUVORK5CYII=);\n}\n@media (-webkit-min-device-pixel-ratio: 1.5), (min-resolution: 1.5dppx) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAgMAAABinRfyAAAADFBMVEUAAADi4uLi4uLh4eE5RQaIAAAAA3RSTlMAyTzPIdReAAAAGUlEQVQI12PAD+b///+Nof7//79gAsLFCwAx/w4blADeeQAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 2), (min-resolution: 192dpi) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAgMAAABinRfyAAAADFBMVEUAAADi4uLi4uLh4eE5RQaIAAAAA3RSTlMAyTzPIdReAAAAGUlEQVQI12PAD+b///+Nof7//79gAsLFCwAx/w4blADeeQAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 3), (min-resolution: 288dpi) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__minus-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAD1BMVEUAAADh4eHg4ODNzc3h4eEYfw2wAAAABHRSTlMAy2EFIuWxUgAAACRJREFUGNNjGBBgJOICBY7KDCoucODEAJSAS6FwUJShGjAQAADBPRGrK2/FhgAAAABJRU5ErkJggg==);\n  }\n}\n.vue-treeselect__check-mark {\n  width: 8px;\n  height: 8px;\n  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAMAAADz0U65AAAAQlBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////8IX9KGAAAAFXRSTlMA8u24NxILB+Tawb6jiH1zRz0xIQIIP3GUAAAAMklEQVQI1y3FtQEAMQDDQD+EGbz/qkEVOpyEOP6PudKjZNSXn4Jm2CKRdBKzSLsFWl8fMG0Bl6Jk1rMAAAAASUVORK5CYII=);\n  background-size: 8px 8px;\n  -ms-transform: scaleY(0.125);\n      transform: scaleY(0.125);\n}\n@media (-webkit-min-device-pixel-ratio: 1.5), (min-resolution: 1.5dppx) {\n  .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAYFBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////98JRy6AAAAH3RSTlMAzu4sDenl38fBvo1OMyIdEQrj1cSihX5hYFpHNycIcQOASAAAAF9JREFUGNN9zEcOgDAMRFHTS0LvNfe/JRmHKAIJ/mqeLJn+k9uDtaeUeFnFziGsBucUTirrprfe81RqZ3Bb6hPWeuZwDFOHyf+ig9CCzQ7INBn7bG5kF+QSt13BHNJnF7AaCT4Y+CW7AAAAAElFTkSuQmCC);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 2), (min-resolution: 192dpi) {\n  .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAYFBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////98JRy6AAAAH3RSTlMAzu4sDenl38fBvo1OMyIdEQrj1cSihX5hYFpHNycIcQOASAAAAF9JREFUGNN9zEcOgDAMRFHTS0LvNfe/JRmHKAIJ/mqeLJn+k9uDtaeUeFnFziGsBucUTirrprfe81RqZ3Bb6hPWeuZwDFOHyf+ig9CCzQ7INBn7bG5kF+QSt13BHNJnF7AaCT4Y+CW7AAAAAElFTkSuQmCC);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 3), (min-resolution: 288dpi) {\n  .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAWlBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////9ZMre9AAAAHXRSTlMA/PiJhGNI9XlEHJB/b2ldV08+Oibk49vPp6QhAYgGBuwAAACCSURBVCjPrdHdDoIwDAXgTWAqCigo/+f9X5OwnoUwtis4V92XNWladUl+rzQPeQJAN2EHxoOnsPn7/oYk8fxBv08Rr/deOH/aZ2Nm8ZJ+s573QGfWKnNuZGzWm3+lv2V3pcU1XQ385/yjmBoM3Z+dXvlbYLLD3ujhTaOM3KaIXvNkFkuSEvYy1LqOAAAAAElFTkSuQmCC);\n  }\n}\n.vue-treeselect__checkbox--checked > .vue-treeselect__check-mark {\n  opacity: 1;\n  -ms-transform: scaleY(1);\n      transform: scaleY(1);\n}\n.vue-treeselect__checkbox--disabled .vue-treeselect__check-mark {\n  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAMAAADz0U65AAAAP1BMVEUAAADj4+Pf39/h4eHh4eHh4eHk5OTh4eHg4ODi4uLh4eHh4eHg4ODh4eHh4eHg4ODh4eHh4eHp6en////h4eFqcyvUAAAAFHRSTlMAOQfy7bgS5NrBvqOIfXNHMSELAgQ/iFsAAAA2SURBVAjXY4AANjYIzcjMAaVFuBkY+RkEWERYmRjYRXjANAOfiIgIFxNIAa8IpxBEi6AwiAQAK2MBd7xY8csAAAAASUVORK5CYII=);\n}\n@media (-webkit-min-device-pixel-ratio: 1.5), (min-resolution: 1.5dppx) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAXVBMVEUAAADh4eHh4eHh4eHi4uLb29vh4eHh4eHh4eHh4eHh4eHh4eHh4eHi4uLi4uLj4+Pi4uLk5OTo6Ojh4eHh4eHi4uLg4ODg4ODh4eHg4ODh4eHf39/g4OD////h4eEzIk+wAAAAHnRSTlMAzu6/LA3p5eLZx8ONTjYiHRIKooV+YWBaRzEnCANnm5rnAAAAZElEQVQY033P2wqAIAyA4VWaaWrnc/n+j5mbhBjUf7WPoTD47TJb4i5zTr/sRDRHuyFaoWX7uK/RlbctlPEuyI1f4WY9yQINEkf6rzzo8YIzmUFoCs7J1EjeIaa9bXIEmzl8dgOZEAj/+2IvzAAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 2), (min-resolution: 192dpi) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAXVBMVEUAAADh4eHh4eHh4eHi4uLb29vh4eHh4eHh4eHh4eHh4eHh4eHh4eHi4uLi4uLj4+Pi4uLk5OTo6Ojh4eHh4eHi4uLg4ODg4ODh4eHg4ODh4eHf39/g4OD////h4eEzIk+wAAAAHnRSTlMAzu6/LA3p5eLZx8ONTjYiHRIKooV+YWBaRzEnCANnm5rnAAAAZElEQVQY033P2wqAIAyA4VWaaWrnc/n+j5mbhBjUf7WPoTD47TJb4i5zTr/sRDRHuyFaoWX7uK/RlbctlPEuyI1f4WY9yQINEkf6rzzo8YIzmUFoCs7J1EjeIaa9bXIEmzl8dgOZEAj/+2IvzAAAAABJRU5ErkJggg==);\n  }\n}\n@media (-webkit-min-device-pixel-ratio: 3), (min-resolution: 288dpi) {\n  .vue-treeselect__checkbox--disabled .vue-treeselect__check-mark {\n    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAUVBMVEUAAADh4eHh4eHh4eHh4eHi4uLi4uLh4eHh4eHh4eHf39/j4+Ph4eHh4eHh4eHg4ODi4uLh4eHh4eHi4uLh4eHh4eHh4eHh4eHh4eH////h4eF3FMFTAAAAGnRSTlMA+/eJhGhfSHE9JBzz5KaQf3pXT0Xbz0I5AYDw8F0AAAB+SURBVCjPrdHbDoMgEEVRKAii1dZe9fz/hxplTiKIT7qfYCWTEEZdUvOwbckNAD2WHeh3brHW5f5EzGQ+iN+b1Gt6KPvtv16Dn6JX9M9ya3/A1yfu5dlyduL6Hec7mXY6ddXLPP2lpABGZ8PWXfYLTJxZekVhhl7eTX24zZPNKXoRC7zQLjUAAAAASUVORK5CYII=);\n  }\n}\n.vue-treeselect__checkbox--unchecked {\n  border-color: #e0e0e0;\n  background: #fff;\n}\n.vue-treeselect__label-container:hover .vue-treeselect__checkbox--unchecked {\n  border-color: #039be5;\n  background: #fff;\n}\n.vue-treeselect__checkbox--indeterminate {\n  border-color: #039be5;\n  background: #039be5;\n}\n.vue-treeselect__label-container:hover .vue-treeselect__checkbox--indeterminate {\n  border-color: #039be5;\n  background: #039be5;\n}\n.vue-treeselect__checkbox--checked {\n  border-color: #039be5;\n  background: #039be5;\n}\n.vue-treeselect__label-container:hover .vue-treeselect__checkbox--checked {\n  border-color: #039be5;\n  background: #039be5;\n}\n.vue-treeselect__checkbox--disabled {\n  border-color: #e0e0e0;\n  background-color: #f7f7f7;\n}\n.vue-treeselect__label-container:hover .vue-treeselect__checkbox--disabled {\n  border-color: #e0e0e0;\n  background-color: #f7f7f7;\n}\n.vue-treeselect__label {\n  overflow: hidden;\n  text-overflow: ellipsis;\n  white-space: nowrap;\n  display: table-cell;\n  padding-left: 5px;\n  max-width: 100%;\n  vertical-align: middle;\n  cursor: inherit;\n}\n[dir=\"rtl\"] .vue-treeselect__label {\n  padding-left: 0;\n  padding-right: 5px;\n}\n.vue-treeselect__count {\n  margin-left: 5px;\n  font-weight: 400;\n  opacity: 0.6;\n}\n[dir=\"rtl\"] .vue-treeselect__count {\n  margin-left: 0;\n  margin-right: 5px;\n}\n.vue-treeselect__tip {\n  padding-left: 5px;\n  padding-right: 5px;\n  display: table;\n  table-layout: fixed;\n  width: 100%;\n  color: #757575;\n}\n.vue-treeselect__tip-text {\n  display: table-cell;\n  vertical-align: middle;\n  padding-left: 5px;\n  padding-right: 5px;\n  overflow: hidden;\n  text-overflow: ellipsis;\n  white-space: nowrap;\n  width: 100%;\n  font-size: 12px;\n}\n.vue-treeselect__error-tip .vue-treeselect__retry {\n  cursor: pointer;\n  margin-left: 5px;\n  font-style: normal;\n  font-weight: 600;\n  text-decoration: none;\n  color: #039be5;\n}\n[dir=\"rtl\"] .vue-treeselect__error-tip .vue-treeselect__retry {\n  margin-left: 0;\n  margin-right: 5px;\n}\n.vue-treeselect__icon-container {\n  display: table-cell;\n  vertical-align: middle;\n  width: 20px;\n  text-align: center;\n  line-height: 0;\n}\n.vue-treeselect--single .vue-treeselect__icon-container {\n  padding-left: 5px;\n}\n[dir=\"rtl\"] .vue-treeselect--single .vue-treeselect__icon-container {\n  padding-left: 0;\n  padding-right: 5px;\n}\n.vue-treeselect__icon-warning {\n  display: block;\n  margin: auto;\n  border-radius: 50%;\n  position: relative;\n  width: 12px;\n  height: 12px;\n  background: #fb8c00;\n}\n.vue-treeselect__icon-warning::after {\n  display: block;\n  position: absolute;\n  content: \"\";\n  left: 5px;\n  top: 2.5px;\n  width: 2px;\n  height: 1px;\n  border: 0 solid #fff;\n  border-top-width: 5px;\n  border-bottom-width: 1px;\n}\n.vue-treeselect__icon-error {\n  display: block;\n  margin: auto;\n  border-radius: 50%;\n  position: relative;\n  width: 12px;\n  height: 12px;\n  background: #e53935;\n}\n.vue-treeselect__icon-error::before,\n.vue-treeselect__icon-error::after {\n  display: block;\n  position: absolute;\n  content: \"\";\n  background: #fff;\n  -ms-transform: rotate(45deg);\n      transform: rotate(45deg);\n}\n.vue-treeselect__icon-error::before {\n  width: 6px;\n  height: 2px;\n  left: 3px;\n  top: 5px;\n}\n.vue-treeselect__icon-error::after {\n  width: 2px;\n  height: 6px;\n  left: 5px;\n  top: 3px;\n}\n.vue-treeselect__icon-loader {\n  display: block;\n  margin: auto;\n  position: relative;\n  width: 12px;\n  height: 12px;\n  text-align: center;\n  animation: 1.6s vue-treeselect-animation-rotate linear infinite;\n}\n.vue-treeselect__icon-loader::before,\n.vue-treeselect__icon-loader::after {\n  border-radius: 50%;\n  position: absolute;\n  content: \"\";\n  left: 0;\n  top: 0;\n  display: block;\n  width: 100%;\n  height: 100%;\n  opacity: 0.6;\n  animation: 1.6s vue-treeselect-animation-bounce ease-in-out infinite;\n}\n.vue-treeselect__icon-loader::before {\n  background: #039be5;\n}\n.vue-treeselect__icon-loader::after {\n  background: #b3e5fc;\n  animation-delay: -0.8s;\n}\n/**\n * Menu Portal\n */\n.vue-treeselect__menu-placeholder {\n  display: none;\n}\n.vue-treeselect__portal-target {\n  position: absolute;\n  display: block;\n  left: 0;\n  top: 0;\n  height: 0;\n  width: 0;\n  padding: 0;\n  margin: 0;\n  border: 0;\n  overflow: visible;\n  box-sizing: border-box;\n}\n\n\n/*# sourceMappingURL=vue-treeselect.css.map*/", ""]);

// exports


/***/ }),

/***/ 954:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


function fuzzysearch (needle, haystack) {
  var tlen = haystack.length;
  var qlen = needle.length;
  if (qlen > tlen) {
    return false;
  }
  if (qlen === tlen) {
    return needle === haystack;
  }
  outer: for (var i = 0, j = 0; i < qlen; i++) {
    var nch = needle.charCodeAt(i);
    while (j < tlen) {
      if (haystack.charCodeAt(j++) === nch) {
        continue outer;
      }
    }
    return false;
  }
  return true;
}

module.exports = fuzzysearch;


/***/ }),

/***/ 955:
/***/ (function(module, exports) {

module.exports = isPromise;

function isPromise(obj) {
  return !!obj && (typeof obj === 'object' || typeof obj === 'function') && typeof obj.then === 'function';
}


/***/ }),

/***/ 956:
/***/ (function(module, exports, __webpack_require__) {

var trimmedEndIndex = __webpack_require__(957);

/** Used to match leading whitespace. */
var reTrimStart = /^\s+/;

/**
 * The base implementation of `_.trim`.
 *
 * @private
 * @param {string} string The string to trim.
 * @returns {string} Returns the trimmed string.
 */
function baseTrim(string) {
  return string
    ? string.slice(0, trimmedEndIndex(string) + 1).replace(reTrimStart, '')
    : string;
}

module.exports = baseTrim;


/***/ }),

/***/ 957:
/***/ (function(module, exports) {

/** Used to match a single whitespace character. */
var reWhitespace = /\s/;

/**
 * Used by `_.trim` and `_.trimEnd` to get the index of the last non-whitespace
 * character of `string`.
 *
 * @private
 * @param {string} string The string to inspect.
 * @returns {number} Returns the index of the last non-whitespace character.
 */
function trimmedEndIndex(string) {
  var index = string.length;

  while (index-- && reWhitespace.test(string.charAt(index))) {}
  return index;
}

module.exports = trimmedEndIndex;


/***/ }),

/***/ 958:
/***/ (function(module, exports, __webpack_require__) {

var toInteger = __webpack_require__(968);

/** Error message constants. */
var FUNC_ERROR_TEXT = 'Expected a function';

/**
 * Creates a function that invokes `func`, with the `this` binding and arguments
 * of the created function, while it's called less than `n` times. Subsequent
 * calls to the created function return the result of the last `func` invocation.
 *
 * @static
 * @memberOf _
 * @since 3.0.0
 * @category Function
 * @param {number} n The number of calls at which `func` is no longer invoked.
 * @param {Function} func The function to restrict.
 * @returns {Function} Returns the new restricted function.
 * @example
 *
 * jQuery(element).on('click', _.before(5, addContactToList));
 * // => Allows adding up to 4 contacts to the list.
 */
function before(n, func) {
  var result;
  if (typeof func != 'function') {
    throw new TypeError(FUNC_ERROR_TEXT);
  }
  n = toInteger(n);
  return function() {
    if (--n > 0) {
      result = func.apply(this, arguments);
    }
    if (n <= 1) {
      func = undefined;
    }
    return result;
  };
}

module.exports = before;


/***/ }),

/***/ 959:
/***/ (function(module, exports) {

/**
 * Creates a function that returns `value`.
 *
 * @static
 * @memberOf _
 * @since 2.4.0
 * @category Util
 * @param {*} value The value to return from the new function.
 * @returns {Function} Returns the new constant function.
 * @example
 *
 * var objects = _.times(2, _.constant({ 'a': 1 }));
 *
 * console.log(objects);
 * // => [{ 'a': 1 }, { 'a': 1 }]
 *
 * console.log(objects[0] === objects[1]);
 * // => true
 */
function constant(value) {
  return function() {
    return value;
  };
}

module.exports = constant;


/***/ }),

/***/ 960:
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(58),
    now = __webpack_require__(965),
    toNumber = __webpack_require__(784);

/** Error message constants. */
var FUNC_ERROR_TEXT = 'Expected a function';

/* Built-in method references for those with the same name as other `lodash` methods. */
var nativeMax = Math.max,
    nativeMin = Math.min;

/**
 * Creates a debounced function that delays invoking `func` until after `wait`
 * milliseconds have elapsed since the last time the debounced function was
 * invoked. The debounced function comes with a `cancel` method to cancel
 * delayed `func` invocations and a `flush` method to immediately invoke them.
 * Provide `options` to indicate whether `func` should be invoked on the
 * leading and/or trailing edge of the `wait` timeout. The `func` is invoked
 * with the last arguments provided to the debounced function. Subsequent
 * calls to the debounced function return the result of the last `func`
 * invocation.
 *
 * **Note:** If `leading` and `trailing` options are `true`, `func` is
 * invoked on the trailing edge of the timeout only if the debounced function
 * is invoked more than once during the `wait` timeout.
 *
 * If `wait` is `0` and `leading` is `false`, `func` invocation is deferred
 * until to the next tick, similar to `setTimeout` with a timeout of `0`.
 *
 * See [David Corbacho's article](https://css-tricks.com/debouncing-throttling-explained-examples/)
 * for details over the differences between `_.debounce` and `_.throttle`.
 *
 * @static
 * @memberOf _
 * @since 0.1.0
 * @category Function
 * @param {Function} func The function to debounce.
 * @param {number} [wait=0] The number of milliseconds to delay.
 * @param {Object} [options={}] The options object.
 * @param {boolean} [options.leading=false]
 *  Specify invoking on the leading edge of the timeout.
 * @param {number} [options.maxWait]
 *  The maximum time `func` is allowed to be delayed before it's invoked.
 * @param {boolean} [options.trailing=true]
 *  Specify invoking on the trailing edge of the timeout.
 * @returns {Function} Returns the new debounced function.
 * @example
 *
 * // Avoid costly calculations while the window size is in flux.
 * jQuery(window).on('resize', _.debounce(calculateLayout, 150));
 *
 * // Invoke `sendMail` when clicked, debouncing subsequent calls.
 * jQuery(element).on('click', _.debounce(sendMail, 300, {
 *   'leading': true,
 *   'trailing': false
 * }));
 *
 * // Ensure `batchLog` is invoked once after 1 second of debounced calls.
 * var debounced = _.debounce(batchLog, 250, { 'maxWait': 1000 });
 * var source = new EventSource('/stream');
 * jQuery(source).on('message', debounced);
 *
 * // Cancel the trailing debounced invocation.
 * jQuery(window).on('popstate', debounced.cancel);
 */
function debounce(func, wait, options) {
  var lastArgs,
      lastThis,
      maxWait,
      result,
      timerId,
      lastCallTime,
      lastInvokeTime = 0,
      leading = false,
      maxing = false,
      trailing = true;

  if (typeof func != 'function') {
    throw new TypeError(FUNC_ERROR_TEXT);
  }
  wait = toNumber(wait) || 0;
  if (isObject(options)) {
    leading = !!options.leading;
    maxing = 'maxWait' in options;
    maxWait = maxing ? nativeMax(toNumber(options.maxWait) || 0, wait) : maxWait;
    trailing = 'trailing' in options ? !!options.trailing : trailing;
  }

  function invokeFunc(time) {
    var args = lastArgs,
        thisArg = lastThis;

    lastArgs = lastThis = undefined;
    lastInvokeTime = time;
    result = func.apply(thisArg, args);
    return result;
  }

  function leadingEdge(time) {
    // Reset any `maxWait` timer.
    lastInvokeTime = time;
    // Start the timer for the trailing edge.
    timerId = setTimeout(timerExpired, wait);
    // Invoke the leading edge.
    return leading ? invokeFunc(time) : result;
  }

  function remainingWait(time) {
    var timeSinceLastCall = time - lastCallTime,
        timeSinceLastInvoke = time - lastInvokeTime,
        timeWaiting = wait - timeSinceLastCall;

    return maxing
      ? nativeMin(timeWaiting, maxWait - timeSinceLastInvoke)
      : timeWaiting;
  }

  function shouldInvoke(time) {
    var timeSinceLastCall = time - lastCallTime,
        timeSinceLastInvoke = time - lastInvokeTime;

    // Either this is the first call, activity has stopped and we're at the
    // trailing edge, the system time has gone backwards and we're treating
    // it as the trailing edge, or we've hit the `maxWait` limit.
    return (lastCallTime === undefined || (timeSinceLastCall >= wait) ||
      (timeSinceLastCall < 0) || (maxing && timeSinceLastInvoke >= maxWait));
  }

  function timerExpired() {
    var time = now();
    if (shouldInvoke(time)) {
      return trailingEdge(time);
    }
    // Restart the timer.
    timerId = setTimeout(timerExpired, remainingWait(time));
  }

  function trailingEdge(time) {
    timerId = undefined;

    // Only invoke if we have `lastArgs` which means `func` has been
    // debounced at least once.
    if (trailing && lastArgs) {
      return invokeFunc(time);
    }
    lastArgs = lastThis = undefined;
    return result;
  }

  function cancel() {
    if (timerId !== undefined) {
      clearTimeout(timerId);
    }
    lastInvokeTime = 0;
    lastArgs = lastCallTime = lastThis = timerId = undefined;
  }

  function flush() {
    return timerId === undefined ? result : trailingEdge(now());
  }

  function debounced() {
    var time = now(),
        isInvoking = shouldInvoke(time);

    lastArgs = arguments;
    lastThis = this;
    lastCallTime = time;

    if (isInvoking) {
      if (timerId === undefined) {
        return leadingEdge(lastCallTime);
      }
      if (maxing) {
        // Handle invocations in a tight loop.
        clearTimeout(timerId);
        timerId = setTimeout(timerExpired, wait);
        return invokeFunc(lastCallTime);
      }
    }
    if (timerId === undefined) {
      timerId = setTimeout(timerExpired, wait);
    }
    return result;
  }
  debounced.cancel = cancel;
  debounced.flush = flush;
  return debounced;
}

module.exports = debounce;


/***/ }),

/***/ 961:
/***/ (function(module, exports) {

/**
 * This method returns the first argument it receives.
 *
 * @static
 * @since 0.1.0
 * @memberOf _
 * @category Util
 * @param {*} value Any value.
 * @returns {*} Returns `value`.
 * @example
 *
 * var object = { 'a': 1 };
 *
 * console.log(_.identity(object) === object);
 * // => true
 */
function identity(value) {
  return value;
}

module.exports = identity;


/***/ }),

/***/ 962:
/***/ (function(module, exports, __webpack_require__) {

var baseGetTag = __webpack_require__(76),
    isObjectLike = __webpack_require__(60);

/** `Object#toString` result references. */
var symbolTag = '[object Symbol]';

/**
 * Checks if `value` is classified as a `Symbol` primitive or object.
 *
 * @static
 * @memberOf _
 * @since 4.0.0
 * @category Lang
 * @param {*} value The value to check.
 * @returns {boolean} Returns `true` if `value` is a symbol, else `false`.
 * @example
 *
 * _.isSymbol(Symbol.iterator);
 * // => true
 *
 * _.isSymbol('abc');
 * // => false
 */
function isSymbol(value) {
  return typeof value == 'symbol' ||
    (isObjectLike(value) && baseGetTag(value) == symbolTag);
}

module.exports = isSymbol;


/***/ }),

/***/ 963:
/***/ (function(module, exports) {

/**
 * Gets the last element of `array`.
 *
 * @static
 * @memberOf _
 * @since 0.1.0
 * @category Array
 * @param {Array} array The array to query.
 * @returns {*} Returns the last element of `array`.
 * @example
 *
 * _.last([1, 2, 3]);
 * // => 3
 */
function last(array) {
  var length = array == null ? 0 : array.length;
  return length ? array[length - 1] : undefined;
}

module.exports = last;


/***/ }),

/***/ 964:
/***/ (function(module, exports) {

/**
 * This method returns `undefined`.
 *
 * @static
 * @memberOf _
 * @since 2.3.0
 * @category Util
 * @example
 *
 * _.times(2, _.noop);
 * // => [undefined, undefined]
 */
function noop() {
  // No operation performed.
}

module.exports = noop;


/***/ }),

/***/ 965:
/***/ (function(module, exports, __webpack_require__) {

var root = __webpack_require__(25);

/**
 * Gets the timestamp of the number of milliseconds that have elapsed since
 * the Unix epoch (1 January 1970 00:00:00 UTC).
 *
 * @static
 * @memberOf _
 * @since 2.4.0
 * @category Date
 * @returns {number} Returns the timestamp.
 * @example
 *
 * _.defer(function(stamp) {
 *   console.log(_.now() - stamp);
 * }, _.now());
 * // => Logs the number of milliseconds it took for the deferred invocation.
 */
var now = function() {
  return root.Date.now();
};

module.exports = now;


/***/ }),

/***/ 966:
/***/ (function(module, exports, __webpack_require__) {

var before = __webpack_require__(958);

/**
 * Creates a function that is restricted to invoking `func` once. Repeat calls
 * to the function return the value of the first invocation. The `func` is
 * invoked with the `this` binding and arguments of the created function.
 *
 * @static
 * @memberOf _
 * @since 0.1.0
 * @category Function
 * @param {Function} func The function to restrict.
 * @returns {Function} Returns the new restricted function.
 * @example
 *
 * var initialize = _.once(createApplication);
 * initialize();
 * initialize();
 * // => `createApplication` is invoked once
 */
function once(func) {
  return before(2, func);
}

module.exports = once;


/***/ }),

/***/ 967:
/***/ (function(module, exports, __webpack_require__) {

var toNumber = __webpack_require__(784);

/** Used as references for various `Number` constants. */
var INFINITY = 1 / 0,
    MAX_INTEGER = 1.7976931348623157e+308;

/**
 * Converts `value` to a finite number.
 *
 * @static
 * @memberOf _
 * @since 4.12.0
 * @category Lang
 * @param {*} value The value to convert.
 * @returns {number} Returns the converted number.
 * @example
 *
 * _.toFinite(3.2);
 * // => 3.2
 *
 * _.toFinite(Number.MIN_VALUE);
 * // => 5e-324
 *
 * _.toFinite(Infinity);
 * // => 1.7976931348623157e+308
 *
 * _.toFinite('3.2');
 * // => 3.2
 */
function toFinite(value) {
  if (!value) {
    return value === 0 ? value : 0;
  }
  value = toNumber(value);
  if (value === INFINITY || value === -INFINITY) {
    var sign = (value < 0 ? -1 : 1);
    return sign * MAX_INTEGER;
  }
  return value === value ? value : 0;
}

module.exports = toFinite;


/***/ }),

/***/ 968:
/***/ (function(module, exports, __webpack_require__) {

var toFinite = __webpack_require__(967);

/**
 * Converts `value` to an integer.
 *
 * **Note:** This method is loosely based on
 * [`ToInteger`](http://www.ecma-international.org/ecma-262/7.0/#sec-tointeger).
 *
 * @static
 * @memberOf _
 * @since 4.0.0
 * @category Lang
 * @param {*} value The value to convert.
 * @returns {number} Returns the converted integer.
 * @example
 *
 * _.toInteger(3.2);
 * // => 3
 *
 * _.toInteger(Number.MIN_VALUE);
 * // => 0
 *
 * _.toInteger(Infinity);
 * // => 1.7976931348623157e+308
 *
 * _.toInteger('3.2');
 * // => 3
 */
function toInteger(value) {
  var result = toFinite(value),
      remainder = result % 1;

  return result === result ? (remainder ? result - remainder : result) : 0;
}

module.exports = toInteger;


/***/ }),

/***/ 969:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
var index = (function (element, listener) {
	var expand = document.createElement('_');
	var shrink = expand.appendChild(document.createElement('_'));
	var expandChild = expand.appendChild(document.createElement('_'));
	var shrinkChild = shrink.appendChild(document.createElement('_'));

	var lastWidth = void 0,
	    lastHeight = void 0;

	shrink.style.cssText = expand.style.cssText = 'height:100%;left:0;opacity:0;overflow:hidden;pointer-events:none;position:absolute;top:0;transition:0s;width:100%;z-index:-1';
	shrinkChild.style.cssText = expandChild.style.cssText = 'display:block;height:100%;transition:0s;width:100%';
	shrinkChild.style.width = shrinkChild.style.height = '200%';

	element.appendChild(expand);

	test();

	return stop;

	function test() {
		unbind();

		var width = element.offsetWidth;
		var height = element.offsetHeight;

		if (width !== lastWidth || height !== lastHeight) {
			lastWidth = width;
			lastHeight = height;

			expandChild.style.width = width * 2 + 'px';
			expandChild.style.height = height * 2 + 'px';

			expand.scrollLeft = expand.scrollWidth;
			expand.scrollTop = expand.scrollHeight;
			shrink.scrollLeft = shrink.scrollWidth;
			shrink.scrollTop = shrink.scrollHeight;

			listener({ width: width, height: height });
		}

		shrink.addEventListener('scroll', test);
		expand.addEventListener('scroll', test);
	}

	function unbind() {
		shrink.removeEventListener('scroll', test);
		expand.removeEventListener('scroll', test);
	}

	function stop() {
		unbind();

		element.removeChild(expand);
	}
});

/* harmony default export */ __webpack_exports__["default"] = (index);


/***/ }),

/***/ 971:
/***/ (function(module, exports, __webpack_require__) {

module.exports = { "default": __webpack_require__(386), __esModule: true };

/***/ }),

/***/ 996:
/***/ (function(module, exports, __webpack_require__) {

"use strict";


exports.__esModule = true;

var _from = __webpack_require__(971);

var _from2 = _interopRequireDefault(_from);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = function (arr) {
  if (Array.isArray(arr)) {
    for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) {
      arr2[i] = arr[i];
    }

    return arr2;
  } else {
    return (0, _from2.default)(arr);
  }
};

/***/ })

});
//# sourceMappingURL=32.js.map
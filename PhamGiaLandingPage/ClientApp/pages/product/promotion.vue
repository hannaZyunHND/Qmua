<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-header">
                    Chương trình khuyễn mại
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex;width:100%">
                                        <div class="col-md-7" style="padding-left:0px">
                                            <treeselect :multiple="false"
                                                        :options="unflattenBase(ListZone)"
                                                        placeholder="Xin mời bạn lựa chọn danh mục"
                                                        v-model="SearchZoneId"
                                                        :default-expanded-level="Infinity"/>
                                        </div>
                                        <div class="input-group" style="display:flex;width:40%">
                                            <input type="text" autocomplete="off" v-model="keyword"
                                                   placeholder="Tìm kiếm sản phẩm" v-on:keyup.enter="onLoadProduct()"
                                                   class="form-control"> <span @click="onLoadProduct()"
                                                                               class="input-group-addon bg-primary"
                                                                               style="cursor: pointer; width: 45px;">
                                                <i class="fa fa-search"
                                                   style="padding-top: 10px; padding-left: 15px;"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv"
                                         style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li>
                                                    <div style="font-size: 13px">
                                                        <b-form-checkbox v-model="this.isAllProduct"
                                                                         @change="filterAllProduct">
                                                            Chọn tất cả
                                                        </b-form-checkbox>
                                                    </div>
                                                </li>
                                                <li v-for="(item,index) in ListProduct" @click="changeValueLeft(index)"
                                                    :class="{'active':item.active==true}">
                                                    <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                    <p style="font-size:13px;overflow:hidden"> {{item.name}}</p>
                                                    <p class="text-muted">Giá bản {{item.price}}</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <b-pagination v-model="currentPage"
                                                  :total-rows="total"
                                                  :per-page="pageSize"></b-pagination>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1" style="padding-top:30px">
                            <button class="btn btn-primary btn-block mb-2" title="Thêm sản phẩm" @click="onetoRight">
                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                            </button>
                            <button class="btn btn-danger btn-block mb-2" title="Xóa sản phẩm" @click="onetoRemove">
                                <i class="fa fa-trash" aria-hidden="true"></i>
                            </button>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex">
                                        <div class="input-group" style="display:flex;width:40%">
                                            <input type="text" autocomplete="off" v-model="keywordPromotion"
                                                   placeholder="Tìm kiếm chương trình khuyễn mại"
                                                   v-on:keyup.enter="onLoadPromotion()" class="form-control"> <span
                                            @click="onLoadPromotion()" class="input-group-addon bg-primary"
                                            style="cursor: pointer; width: 45px;">
                                                <i class="fa fa-search"
                                                   style="padding-top: 10px; padding-left: 15px;"></i>
                                            </span>
                                        </div>
                                        <div style="display:flex;width:15%;padding-left:2%">
                                            <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit"
                                                    @click="AddPromotionWithProduct()">
                                                <i class="fa fa-save"></i> Lưu
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv"
                                         style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li>
                                                    <div style="font-size: 13px">
                                                        <b-form-checkbox v-model="this.isAll" @change="filter">
                                                            Chọn tất cả
                                                        </b-form-checkbox>
                                                    </div>
                                                </li>
                                                <li v-for="(item,index) in ListPromotion"
                                                    @click="changeValueRight(index)"
                                                    :class="{'active':item.isChoose==true}">
                                                    <div style="font-size:13px">
                                                        <b-form-checkbox v-model="item.isChoose" :disabled="disabled">
                                                            {{item.name}}
                                                        </b-form-checkbox>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <b-pagination v-model="currentPage"
                                                  :total-rows="total"
                                                  :per-page="pageSize"></b-pagination>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <cmodal></cmodal>
    </div>
</template>

<script>

    import {mapActions} from "vuex";
    import Treeselect from '@riophae/vue-treeselect';
    import {unflatten, pathImg} from '../../plugins/helper';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import cmodal from './../../components/fileManager/list';
    import EventBus from "./../../common/eventBus";

    export default {
        name: "productinregion",
        components: {
            Treeselect,
            'cmodal': cmodal,
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
            }
        },
        data() {
            return {
                SelectedRegion: "",
                SearchZoneId: 0,
                ChoseZoneId: 0,
                ListZone: [],
                ListProduct: [],
                ListPromotion: [],
                ListValue: [],
                ListRegion: [],
                ListZoneRight: [],
                keyword: "",
                keywordPromotion: "",
                activeLeft: [],
                activeRight: [],
                pageSize: 20,
                currentPage: 1,
                total: 0,
                isAll: false,
                isAllProduct: false,
                disabled: false,
                ListResult: [],
                ListPromotionResult: [],
                ListPushAPI: [],
                ListChooseProduct: [],
                ListPromotionChoose: []
            };
        },
        mounted: function () {


        },
        methods: {
            ...mapActions(["getProductForCombo", "addPromotionWithProduct", "getAllRegion", "getProductByRegion", "getZones", "addProductByRegion", "getListPromotion", "getListPromotionByProductId"]),
            pathImgs(path) {
                return pathImg(path);
            },
            filter() {
                debugger
                if (this.isAll) {
                    this.isAll = false;
                    this.disabled = false
                } else {
                    this.isAll = true;
                    this.disabled = true;
                    var result = [];
                    this.onLoadPromotion()
                    var listPromotionResult = this.ListPromotion
                    this.ListChooseProduct.forEach(function (entry) {
                        listPromotionResult.forEach(function (entryj) {
                            var promotionInProduct = {};
                            promotionInProduct.productId = entry
                            promotionInProduct.id = entryj.id
                            result.push(promotionInProduct);
                        })
                    })
                    this.ListPushAPI = [];
                    for (let i = 0; i < result.length; i++) {
                        let resultObject = {}
                        resultObject.productId = result[i].productId
                        resultObject.id = result[i].id
                        this.ListPushAPI.push(resultObject)
                    }
                    // this.ListPushAPI=result
                    console.log(this.ListPushAPI);

                }
            },
            filterAllProduct() {
                console.log(this.ListResult)
                if (this.isAllProduct) {
                    this.isAllProduct = false;
                } else {
                    this.isAllProduct = true;
                    this.onLoadPromotion();
                    var result = []
                    this.ListProduct.forEach(function (entry) {
                        var promotionInProduct = {};
                        promotionInProduct.productId = entry.id
                        result.push(promotionInProduct);
                    })
                    this.ListResult = result
                    console.log(this.ListResult);
                }
            },
            openImg(img) {
                this.choseImg = img;
                EventBus.$emit('FileManagerOpen', '');
            },
            toggleActive(item) {
                if (this.activeItem[item.id]) {
                    this.removeActiveItem(item);
                    return;
                }

                this.addActiveItem(item);
            },

            addActiveItem(item) {
                this.activeItem = Object.assign({},
                    this.activeItem ? [item.id] : item,
                );
            },
            removeActiveItem(item) {
                delete this.activeItem[item.id];
                this.activeItem = Object.assign({}, this.activeItem);
            },
            onLoadPromotion() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getListPromotion({
                    keyword: this.keywordPromotion,
                    pageSize: this.pageSize,
                    pageIndex: this.currentPage,

                }).then(respose => {
                    this.ListPromotion = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));
                    console.log(this.ListPromotion);
                    this.total = respose.total;
                })
            }
            ,
            onLoadProduct() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProductForCombo({
                    keyword: this.keyword,
                    idZone: this.SearchZoneId || 0,
                    pageSize: this.pageSize,
                    pageIndex: this.currentPage,

                }).then(respose => {
                    this.ListProduct = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));
                    console.log(this.ListProduct);
                    this.total = respose.total;
                })
            },
            onLoadProductByRegion() {
                //debugger-
                this.getProductByRegion(
                    {
                        zoneId: this.ChoseZoneId || 0
                    }).then(respose => {
                    this.ListValue = respose.map(item => ({
                        ...item,
                        active: false
                    }));
                    console.log(this.ListValue);
                })
            },
            onLoadProductCombo() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getComboById(this.productId).then(respose => {
                    this.ListValue = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));

                })
            },
            AddPromotionWithProduct: function () {
                //debugger-
                // if (this.ChoseZoneId > 0) {
                //     this.addPromotionWithProduct(this.ListPushAPI).then(response => {
                //         if (response.success == true) {
                //             this.$toast.success(response.message, {});
                //
                //         } else {
                //             this.$toast.error(response.message, {});
                //         }
                //     }).catch(e => {
                //         this.$toast.error("Lỗi hệ thống");
                //
                //     });
                // } else {
                //     this.$toast.error("Chưa chọn vùng");
                // }
                if (!this.isAllProduct && !this.isAll) {
                    var result=[]
                    var listPromotionChoose = this.ListPromotionChoose
                    this.ListChooseProduct.forEach(function (entry) {
                        listPromotionChoose.forEach(function (entryj) {
                            var promotionInProduct={}
                            promotionInProduct.productId = entry;
                            promotionInProduct.id=entryj;
                            result.push(promotionInProduct)
                        })
                    })
                    this.ListPushAPI = [];
                    for (let i = 0; i < result.length; i++) {
                        let resultObject = {}
                        resultObject.productId = result[i].productId
                        resultObject.id = result[i].id
                        this.ListPushAPI.push(resultObject)
                    }
                }
                this.addPromotionWithProduct(this.ListPushAPI).then(response => {
                    if (response.success == true) {
                        this.$toast.success(response.message, {});
                        this.ListPushAPI = []
                    } else {
                        this.$toast.error(response.message, {});
                    }
                }).catch(e => {
                    this.$toast.error("Lỗi hệ thống");

                });


            },

            onetoRight: function () {

                if (this.ChoseZoneId > 0) {
                    var vm = this;
                    var data = vm.ListZone;
                    var lstChose = this.ListProduct.filter(x => x.active == true).map(item => ({
                        productId: item.id,
                        isHot: false,
                        productName: item.name,
                        avatar: item.avatar,
                        zoneId: vm.ChoseZoneId,
                        bigThumb: "",
                        active: false,
                        zoneName: vm.ListZoneRight.filter(x => x.id == vm.ChoseZoneId)[0].label
                    }));

                    this.ListProduct = this.ListProduct.map(item => ({
                        ...item,
                        active: false
                    }))

                    this.ListValue = [...this.ListValue, ...lstChose];

                } else {
                    this.$toast.error("Chưa chọn vùng", {});
                }

            },
            onetoRemove: function () {
                //debugger-
                this.ListValue = this.ListValue.filter(x => x.active != true);
            },

            changeValueLeft: function (id) {
                if (!this.isAllProduct) {
                    this.getListPromotionByProductId(this.ListProduct[id].id).then(response => {
                        this.ListPromotion = response;
                    });
                    this.ListChooseProduct.push(this.ListProduct[id].id)
                    this
                    if (this.ListProduct[id].active == true) {
                        this.ListProduct[id].active = false;
                    } else {
                        this.ListProduct[id].active = true;
                    }
                } else {
                    this.onLoadPromotion()
                }
            },
            changeValueRight: function (id) {
                debugger
                this.ListPromotionResult.push(this.ListPromotion[id])
                var listPromotionResult = this.ListPromotionResult
                if (this.isAllProduct) {
                    var result = []
                    this.ListResult.forEach(function (entry) {
                        var promotionInProduct = {};
                        listPromotionResult.forEach(function (entryj) {
                            promotionInProduct.productId = entry.productId
                            promotionInProduct.id = entryj.id
                        })
                        result.push(promotionInProduct);
                    })
                    for (let i = 0; i < result.length; i++) {
                        let resultObject = {}
                        resultObject.productId = result[i].productId
                        resultObject.id = result[i].id
                        this.ListPushAPI.push(resultObject)
                    }
                    // this.ListPushAPI=result
                    console.log(this.ListPushAPI);
                } else {
                    this.ListPromotionChoose.push(this.ListPromotion[id].id)
                }
            },
            ExistId(id) {

            },
            unflattenBase(data) {
                return unflatten(data);
            },
        },
        created() {
            var vm = this;
            EventBus.$on('FileSelected', value => {
                for (let i = 0; i < vm.ListValue.length; i++) {
                    if (vm.ListValue[i].active == true) {
                        vm.ListValue[i].bigThumb = value[0].path;
                    }
                }

                console.log(vm.ListValue);
            })

            this.getAllRegion().then(respose => {
                this.ListRegion = respose;
            });

            this.getZones(0).then(respose => {
                try {
                    //debugger-
                    respose.listData.push({id: 0, label: "Chọn danh mục cha", parentId: 0});
                    var data = respose.listData;
                    var dataRight = data.filter(x => x.type == 7 || x.id == 0);
                    var dataLeft = data.filter(x => x.type == 1 || x.id == 0);
                    this.ListZone = dataLeft;
                    this.ListZoneRight = dataRight;
                } catch (ex) {

                }

            });
            // this.onLoadProduct();
            this.onLoadPromotion();
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onLoadProduct();
            },

            ChoseZoneId: function (newVal) {

                //debugger-

                this.onLoadProductByRegion();
            },
            SearchZoneId: function (newVal) {
                this.onLoadProduct();
            },
        }
    };
</script>

<style>

    .productedit .form-control {
        height: 35px;
    }


    .panel-body ul li .thumb {
        float: left;
        height: 30px;
        margin-right: 10px;
        overflow: hidden;
        width: 30px;
        border: 1px solid #ddd;
    }


    .panel-body ul li p.text-muted {
        font-size: 11px;
        line-height: 15px;
    }

    .panel-body ul li p {
        margin-bottom: 0px;
    }

    .panel-body ul li {
        border-bottom: 1px solid #e6e6fa;
        cursor: pointer;
        list-style-type: none;
        padding: 4px 0;
        padding-left: 10px;
    }

    /* width */
    ::-webkit-scrollbar {
        width: 10px;
    }

    /* Track */
    ::-webkit-scrollbar-track {
        background: #f1f1f1;
    }

    /* Handle */
    ::-webkit-scrollbar-thumb {
        background: #888;
    }

    /* Handle on hover */
    ::-webkit-scrollbar-thumb:hover {
        background: #555;
    }

    .row ul li.active, #rlist .row ul li.active {
        background: #f1f7fd;
    }


    .pagination .page-item .page-link {
        font-size: 12px;
    }

    .pagination {
        margin-top: 10px
    }
</style>

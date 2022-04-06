<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-header">
                    Sản phẩm Hot trong danh mục
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex;width:100%">
                                        <div class="col-md-7" style="padding-left:0px">
                                            <treeselect :multiple="false"
                                                        :options="unflattenBase(ListZone)"
                                                        placeholder="Xin mời bạn lựa chọn danh mục"
                                                        v-model="SearchZoneId"
                                                        :default-expand-level="Infinity" />
                                        </div>
                                        <div class="input-group" style="display:flex;width:40%">
                                            <input type="text" autocomplete="off" v-model="keyword" placeholder="Tìm kiếm sản phẩm" v-on:keyup.enter="onLoadProduct()" class="form-control"> <span @click="onLoadProduct()" class="input-group-addon bg-primary" style="cursor: pointer; width: 45px;"><i class="fa fa-search" style="padding-top: 10px; padding-left: 15px;"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv">
                                        <div class="row">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li v-for="(item,index) in ListProduct" class="row">
                                                    <div class="col-md-5">
                                                        <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                        <p style="font-size:13px;overflow:hidden">Mã: {{item.code}}</p>
                                                        <p class="text-muted">Tên: {{item.name}}</p>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <b-form-checkbox v-model="item.isHot" style="font-size:13px;">
                                                            Hot
                                                        </b-form-checkbox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <img @click="openImg(item.productId)" v-if="item.bigThumb != null && item.bigThumb.length >0" style="width:50px;height:20px" :src="pathImgs(item.bigThumb)" alt="Ảnh lỗi" />
                                                        <i @click="openImg(item.productId)" v-else style="font-size:30px" class="fa fa-picture-o"></i>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="btn btn-success" @click="SaveProductHot(item)"><i style="color:#fff" class="fa fa-save"></i></a>
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
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>
</template>

<script>

    import { mapActions } from "vuex";
    import Treeselect from '@riophae/vue-treeselect';
    import { unflatten, pathImg } from '../../plugins/helper';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import FileManager from './../../components/fileManager/list'
    import EventBus from "./../../common/eventBus";

    export default {
        name: "productinzone",
        components: {
            Treeselect,
            FileManager
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
                mikey1: 'mikey1',

                SelectedRegion: "",

                SearchZoneId: 0,
                ChoseZoneId: 0,
                ListZone: [],
                ListProduct: [],
                ListValue: [],

                ListRegion: [],

                ListZoneRight: [],


                keyword: "",
                activeLeft: [],
                activeRight: [],

                pageSize: 20,
                currentPage: 1,
                total: 0,
                choseImg: 0,
            };
        },
        mounted: function () {


        },
        methods: {
            ...mapActions(["getProductByZone", "getZones", "addProductHotInZone"]),
            pathImgs(path) {
                return pathImg(path);
            },

            openImg(img) {
                this.choseImg = img;
                EventBus.$emit(this.mikey1, '');
            },

            DoAttackFile(value) {
                let vm = this;
                for (let i = 0; i < vm.ListProduct.length; i++) {
                    if (vm.ListProduct[i].productId == this.choseImg) {
                        vm.ListProduct[i].bigThumb = value[0].path;
                    }
                }
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
            onLoadProduct() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProductByZone({
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


            SaveProductHot: function (item) {
                //debugger-

                this.addProductHotInZone(item).then(response => {
                    //debugger
                    if (response.success == true) {
                        this.$toast.success(response.message, {});

                    } else {
                        this.$toast.error(response.message, {});
                    }
                }).catch(e => {
                    this.$toast.error("Lỗi hệ thống");

                });

            },


            unflattenBase(data) {
                return unflatten(data);
            },
        },
        created() {
            var vm = this;
            EventBus.$on('FileSelected', value => {
                for (let i = 0; i < vm.ListProduct.length; i++) {
                    if (vm.ListProduct[i].productId == this.choseImg) {
                        vm.ListProduct[i].bigThumb = value[0].path;
                    }
                }
            })



            this.getZones(1).then(respose => {
                try {
                    //debugger-
                    respose.listData.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                    var data = respose.listData;
                    this.ListZone = data;

                }
                catch (ex) {

                }
            });

        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onLoadProduct();
            },

            SearchZoneId: function (newVal) {
                this.currentPage = 1;
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

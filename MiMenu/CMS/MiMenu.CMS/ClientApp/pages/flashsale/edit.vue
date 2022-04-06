<template>
    <div class="productadd">
        <div class="row productedit">
            <div class="col-sm-12 col-md-12">
                <div class="card">
                    <div class="card-header">
                        Thông tin chính
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <div class="row">
                                <div class="col-md-7">
                                    <b-form-group label="Tên chiến dịch">
                                        <b-form-input v-model="objRequest.name" placeholder="Tên chiến dịch" required></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-3" style="margin-top:32px">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus" type="button" @click="addFlashSale()">
                                        <i class="fa fa-save"></i> Cập nhật
                                    </button>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <b-form-group label="Ngày bắt đầu">
                                       
                                        <b-form-input v-model="objRequest.startTime" placeholder="Ngày bắt đầu" required type="date"></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-4">
                                    <b-form-group label="Ngày kết thúc">
                                        <b-form-input v-model="objRequest.endTime" placeholder="Ngày kết thúc" required type="date"></b-form-input>
                                    </b-form-group>
                                </div>
                            </div>
                        </b-form>
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
                                                            :default-expanded-level="Infinity" />
                                            </div>
                                            <div class="input-group" style="display:flex;width:40%">
                                                <input type="text" autocomplete="off" v-model="keyword" placeholder="Tìm kiếm sản phẩm" v-on:keyup.enter="onLoadProduct()" class="form-control"> <span @click="onLoadProduct()" class="input-group-addon bg-primary" style="cursor: pointer; width: 45px;"><i class="fa fa-search" style="padding-top: 10px; padding-left: 15px;"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                            <div class="row" style="width: auto; height: 300px;">
                                                <ul style="padding:20px;padding-left:10px ;width:100%">
                                                    <li v-for="(item,index) in ListProduct" @click="changeValueLeft(index)" :class="{'active':item.active==true}">
                                                        <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                        <p style="font-size:13px;overflow:hidden">Mã:  {{item.code}}</p>
                                                        <p class="text-muted">Tên: {{item.name}}</p>
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
                                <button class="btn btn-primary btn-block mb-2" title="Thêm sản phẩm" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                                <button class="btn btn-danger btn-block mb-2" title="Xóa sản phẩm" @click="onetoRemove"><i class="fa fa-trash" aria-hidden="true"></i></button>
                            </div>
                            <div class="col-md-6">
                                <div class="panel panel-white">
                                    <div class="panel-heading">
                                        <div style="display:flex">
                                            <div style="display:flex;width:15%;padding-left:2%;min-height:30px">

                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                            <div class="row" style="width: auto; height: 300px;">
                                                <ul style="padding:20px;padding-left:10px ;width:100%">
                                                    <li v-for="(item,index) in ListValue" @click="changeValueRight(index)" style="padding-bottom:10px;width:100%;display:flex" :class="{'active':item.active==true}">
                                                        <div class="col-md-6">
                                                            <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                            <p style="font-size:13px;overflow:hidden">Mã:  {{item.code}}</p>
                                                            <p class="text-muted">Tên: {{item.name}}</p>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="row">
                                                                <div class="col-md-7">
                                                                    <input class="form-control" v-model="item.productPriceInFlashSale" type="number" placeholder="Giá tiền" />
                                                                </div>
                                                                <div class="col-md-5">
                                                                    <input class="form-control" v-model="item.quantity" type="number" placeholder="Số lượng " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</template>


<script>

    import "vue-loading-overlay/dist/vue-loading.css";
    import Treeselect from '@riophae/vue-treeselect';
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import moment from 'moment';
    import { unflatten, pathImg } from '../../plugins/helper';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    export default {
        name: "editFlashSale",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                objRequest: {
                    startTime: moment(String(new Date())).format('YYYY-MM-DD'),
                    endTime: moment(String(new Date())).format('YYYY-MM-DD')
                },
                objRequestDetail: {},
                ListValue: [],
                langSelected: "",

                ListZone: [],
                ListProduct: [],
                SearchZoneId: 0,
                keyword: "",

                pageSize: 20,
                currentPage: 1,
                total: 0,

                objUser: {

                },
            };
        },
        created() {
            this.objUser = this.getUser();
        },
        components: {
            Loading,
            Treeselect
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getFlashSale(this.$route.params.id).then(respose => {
                    this.objRequest = respose.data;
                    this.objRequest.startTime = moment(String(this.objRequest.startTime)).format('YYYY-MM-DD');
                    this.objRequest.endTime = moment(String(this.objRequest.endTime)).format('YYYY-MM-DD');
                    if (respose.listData != null && respose.listData.length > 0) {
                        this.ListValue = respose.listData.map(item => ({
                            ...item,
                            active: false
                        }));
                    }
                });
                this.isLoading = false;
            };

            this.getZones(1).then(respose => {
                try {
                    respose.listData.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                    var data = respose.listData;
                    this.ListZone = data;
                }
                catch (ex) {
                    console.log(ex);
                }

            });
            this.onLoadProduct();
        },

        computed: {
            ...mapGetters(["promotion"]),
        },

        methods: {
            ...mapActions([
                "getFlashSale",
                "getZones",
                "getProductForCombo",
                "createFlashSale"
            ]),
            getUser() {
                //debugger-
                var data = JSON.parse(localStorage.getItem('currentUser'));
                return JSON.parse(localStorage.getItem('currentUser'));
            },
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
                  
                    this.total = respose.total;
                })
            },
            unflattenBase(data) {
                return unflatten(data);
            },
            pathImgs(path) {
                return pathImg(path);
            },
            onetoRight: function () {

                var vm = this;

                var lstChose = this.ListProduct.filter(x => x.active == true).map(item => ({
                    active: false,
                    avatar: item.avatar,
                    code: item.code,
                    flashSaleId: this.objRequest.id,
                    name: item.name,
                    productId: item.id,
                    productPriceInFlashSale: 0,
                    quantity: 0,
                }));

                this.ListProduct = this.ListProduct.map(item => ({
                    ...item,
                    active: false
                }))
                
                this.ListValue = [...lstChose, ...this.ListValue];
            },
            onetoRemove: function () {
                //debugger-
                this.ListValue = this.ListValue.filter(x => x.active != true);
            },

            changeValueLeft: function (id) {
                if (this.ListProduct[id].active == true) {
                    this.ListProduct[id].active = false;
                } else {
                    this.ListProduct[id].active = true;
                }
            },
            changeValueRight: function (id) {

                if (this.ListValue[id].active == true) {
                    this.ListValue[id].active = false;
                } else {
                    this.ListValue[id].active = true;
                }
            },

            addFlashSale: function () {
                //debugger
                this.objRequest.modifiedBy = this.objUser.userName;

                this.createFlashSale({
                    Flash: this.objRequest,
                    ListProduct: this.ListValue,
                }).then(response => {
                    if (response.key == true) {
                        this.$toast.success(response.value, {});

                    } else {
                        this.$toast.error(response.value, {});
                    }
                }).catch(e => {
                    this.$toast.error("Lỗi hệ thống");

                });
            },
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
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

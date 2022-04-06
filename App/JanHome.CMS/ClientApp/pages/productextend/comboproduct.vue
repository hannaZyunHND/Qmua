<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex;width:100%">
                                        <!--<div class="col-md-6" style="padding-left:0px">
                                            <v-select></v-select>
                                        </div>-->
                                        <div style="display:flex;width:60%">
                                            <treeselect :multiple="false"
                                                        :options="category"
                                                        placeholder="Xin mời bạn lựa chọn danh mục"
                                                        v-model="searchZoneId" />
                                        </div>
                                        <div class="input-group" style="display:flex;width:40%">
                                            <input type="text" autocomplete="off" v-model="keyword" placeholder="Tìm kiếm sản phẩm" v-on:keyup.enter="onLoadProduct()" class="form-control"> <span class="input-group-addon bg-primary" style="cursor: pointer; width: 45px;"><i class="fa fa-search" style="padding-top: 10px; padding-left: 15px;"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li v-for="(item,index) in ListProduct" @click="changeValueLeft(index)" :class="{'active':item.active}">
                                                    <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                    <p style="font-size:13px;overflow:hidden">  {{item.name}}</p>
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
                            <button class="btn btn-primary btn-block mb-2" title="Thêm sản phẩm" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                            <button class="btn btn-danger btn-block mb-2" title="Xóa sản phẩm" @click="onetoRemove"><i class="fa fa-trash" aria-hidden="true"></i></button>
                        </div>
                        <div class="col-md-5">
                            <div class="panel panel-white">
                                <div class="panel-heading" style="width:100%;display:flex;border:1px solid #ccc">
                                    <div class="col-md-8" style="padding-top:5px">
                                        Danh sách sản phẩm
                                    </div>
                                    <div class="col-md-4" style="padding-right:0">
                                        <button class="btn btn-info btn-submit-form pull-right btncus" type="submit" @click="Add()">
                                            <i class="fa fa-save"></i> Lưu
                                        </button>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li v-for="(item,index) in ListValue" @click="changeValueRight(index)" :class="{'active':item.active}">
                                                    <div class="row">
                                                        <div class="col-8">
                                                            <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                            <p style="font-size:13px;overflow:hidden">  {{item.name}}</p>
                                                            <p class="text-muted">Giá bản {{item.price}}</p>
                                                        </div>
                                                        <div class="col-4">
                                                            <p><input type="number" v-model="item.configPrice" placeholder="Giá giảm" /></p>
                                                            <p><input type="text" v-model="item.configNote" placeholder="Ghi chú" /></p>
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

</template>

<script>

    import { mapActions } from "vuex";
    // Import component
    import Treeselect from '@riophae/vue-treeselect';
    import { unflatten, pathImg } from '../../plugins/helper';
    export default {
        name: "comboproduct",
        components: {
            Treeselect
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
        data() {
            return {
                ListProduct: [],
                ListValue: [],
                keyword: "",
                activeLeft: [],
                activeRight: [],
                searchZoneId: 0,
                pageSize: 20,
                currentPage: 1,
                total: 0,
            };
        },
        methods: {
            ...mapActions(["getProductForCombo", "addCombo", "getComboById"]),
            pathImgs(path) {
                return pathImg(path);
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
                this.getProductForCombo({
                    keyword: this.keyword,
                    idZone: this.searchZoneId || 0,
                    pageSize: this.pageSize,
                    pageIndex: this.currentPage,

                }).then(respose => {
                    //debugger-
                    //this.ListProduct = respose.listData;
                    this.ListProduct = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));
                    this.total = respose.total;
                    //this.ListValue = respose.listObj2;
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
                    //this.ListValue = respose.listObj2;
                })
            },
            Add: function () {
                //console.log(this.ListValue);
                this.addCombo({
                    Id: this.productId,
                    Type: "com-bo",
                    Combos: this.ListValue
                }).then(response => {
                    if (response.success == true) {
                        this.$toast.success(response.message, {});

                    } else {
                        this.$toast.error(response.message, {});

                    }

                }).catch(e => {
                    this.$toast.error("Lỗi hệ thống");

                });
            },
            alltoRight: function () {
                this.activeLeft = 0;
                for (let i = 0; i < this.ListLocation.length; i++) {
                    this.ListValue.push(this.ListLocation[i]);
                }

                this.ListLocation = [];
            },
            onetoRight: function () {
                let lstChose = this.ListProduct.filter(x => x.active == true);
                this.ListProduct = this.ListProduct.map(item => ({
                    ...item,
                    active: false
                }))
                this.ListValue = [...this.ListValue, ...lstChose];

            },
            onetoRemove: function () {
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
            ExistId(id) {

            },
        },
        watch: {
            actions: function (newVal) {
                //debugger-
                if (newVal == true) {
                    this.onLoadProduct();
                    this.onLoadProductCombo();
                }
            },
            currentPage: function (newVal) {
                //debugger-
                this.currentPage = newVal;
                this.onLoadProduct();

            },
            searchZoneId: function () {
                this.currentPage = 1;
                this.onLoadProduct();
            }
        }
    };
</script>


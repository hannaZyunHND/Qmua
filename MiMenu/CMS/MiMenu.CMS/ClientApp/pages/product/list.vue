
<template>
    <div class="list-data">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="false"></loading>
        <b-card header-tag="header" class="card-filter"
                footer-tag="footer">
            <div>
                <b-row class="form-group">
                    <b-col md="3">
                        <b-form-input type="text" v-model="searchKey" v-on:keyup.enter="onChangePaging()" placeholder="Tìm kiếm theo tên"></b-form-input>
                    </b-col>
                    <b-col md="2">
                        <select class="form-control" v-model="searchStatus">
                            <option :value="item.key" v-for="item in ListStatus">{{item.value}}</option>
                        </select>
                    </b-col>
                    <b-col md="3">
                        <b-form-input type="text" v-model="voucherKey" v-on:keyup.enter="onChangePaging()" placeholder="Tìm kiếm mã voucher"></b-form-input>
                    </b-col>
                    <b-col md="3">
                        <select v-model="SearchPromotionId" class="form-control">
                            <option value="0">Chọn khuyến mại</option>
                            <option v-for="item in Promotions" :value="item.id">{{item.name}}</option>
                        </select>
                    </b-col>
                    <b-col md="1">
                        <b-btn v-b-toggle.collapse2 variant="primary">
                            <i class="fa fa-angle-double-down" aria-hidden="true"></i>
                        </b-btn>
                    </b-col>
                    <b-collapse id="collapse2" class="mt-2 col-md-12">
                        <b-card>
                            <p class="card-text">Thêm mã Voucher cho sản phẩm</p>
                            <b-row>
                                <b-col md="5">
                                    <select v-model="SearchParrentVoucher" class="form-control">
                                        <option value="0">Chọn nhóm Voucher</option>
                                        <option v-for="item in ListParrentVoucher" :value="item.id">{{item.code}}-{{item.name}}</option>
                                    </select>
                                </b-col>
                                <b-col md="7" v-if="SearchParrentVoucher != 0">
                                    <treeselect :multiple="true"
                                                :options="ListVoucher"
                                                placeholder=" Chọn mã voucher"
                                                :value-consists-of="LEAF_PRIORITY"
                                                v-model="ListVoucherChecked"
                                                :default-expanded-level="Infinity" />
                                </b-col>
                                <b-col md="12" class="mt-3">
                                    <button @click="AddVoucherByZone()" class="btn btn-info">Thêm cho danh mục đã chọn</button>
                                    <button @click="AddVoucherByProduct()" class="btn btn-info">Thêm cho sản phẩm đã chọn</button>
                                </b-col>
                            </b-row>
                        </b-card>
                    </b-collapse>
                </b-row>
                <b-row class="form-group">
                    <b-col md="7">
                        <treeselect :multiple="true"
                                    :options="ListZone"
                                    placeholder=" Chọn danh mục"
                                    :value-consists-of="valueConsistsOf"
                                    v-model="SearchZoneId"
                                    :default-expanded-level="Infinity" />
                    </b-col>
                    <b-col md="3" style="padding-top:5px">
                        <input type="radio" value="ALL" v-model="valueConsistsOf"><label>Tất cả</label>
                        <input type="radio" value="BRANCH_PRIORITY" v-model="valueConsistsOf"><label>Chỉ một</label>
                    </b-col>
                    <b-col md="2" style="padding-top:5px">
                        <b-form-checkbox v-model="IsInstallment" style="padding-bottom:7px">
                            Trả góp
                        </b-form-checkbox>
                    </b-col>
                </b-row>
            </div>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class=" mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="btn-group">
                        <router-link class="btn btn-success" :to="{ path: 'add'}" target='_blank'><i class="fa fa-plus"></i> Thêm mới</router-link>
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>
                    <b-dropdown class="mx-1" variant="info" right text="Xuất DL" icon>
                        <b-dropdown-item @click="exportPriceLocation()">Giá theo tỉnh thành</b-dropdown-item>
                        <b-dropdown-item @click="exportSpectification()">Chi tiết kĩ thuật</b-dropdown-item>
                    </b-dropdown>
                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="products.total"
                                      :per-page="pageSize"></b-pagination>
                        <div class="mx-1" style="padding-top:5px">Số lượng : {{products.total}}</div>
                    </div>
                </div>

                <div style="margin-top:10px;margin-bottom:10px">
                    <div v-for="(item,index) in ListProductChecked" style="margin-right:3px" class="vue-treeselect__multi-value-item">
                        <span class="vue-treeselect__multi-value-label">{{item.code}}</span>
                        <span class="vue-treeselect__icon vue-treeselect__value-remove" @click="RemoveItem(index)">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 348.333 348.333">
                                <path d="M336.559 68.611L231.016 174.165l105.543 105.549c15.699 15.705 15.699 41.145 0 56.85-7.844 7.844-18.128 11.769-28.407 11.769-10.296 0-20.581-3.919-28.419-11.769L174.167 231.003 68.609 336.563c-7.843 7.844-18.128 11.769-28.416 11.769-10.285 0-20.563-3.919-28.413-11.769-15.699-15.698-15.699-41.139 0-56.85l105.54-105.549L11.774 68.611c-15.699-15.699-15.699-41.145 0-56.844 15.696-15.687 41.127-15.687 56.829 0l105.563 105.554L279.721 11.767c15.705-15.687 41.139-15.687 56.832 0 15.705 15.699 15.705 41.145.006 56.844z"></path>
                            </svg>
                        </span>
                    </div>
                </div>
                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer" role="grid">
                            <thead class="table table-centered table-nowrap">
                                <tr role="row">
                                    <th style="padding-left:15px"><b-form-checkbox></b-form-checkbox></th>
                                    <th class="sorting">Hình ảnh</th>
                                    <th class="sorting">Mã sản phẩm</th>
                                    <th class="sorting_desc">Tên sản phẩm</th>
                                    <th class="sorting">Danh mục</th>
                                    <th class="">Ngôn ngữ</th>
                                    <th @click="sortor('SortOrder')" class="">Sắp xếp <i class="fa fa-angle-double-down"></i></th>
                                    <th class="sorting">Trạng thái</th>
                                    <th class="sorting">Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr role="row" class="odd" v-for="item in products.listData">
                                    <td class="dt-checkboxes-cell"><b-form-checkbox v-model="ListProductChecked" :value="item"></b-form-checkbox></td>
                                    <td class="product-img">
                                        <img style="width:100px;height:auto" class="img-thumbnail" :src="pathImgs(item.avatar)" alt="Ảnh lỗi">
                                    </td>
                                    <td class="product-code">
                                        {{item.code}}
                                    </td>
                                    <td style="width:200px;height:auto">
                                        <p>{{item.name}}</p>
                                    </td>
                                    <td class="product-category" v-html="item.category"></td>
                                    <td width="200px">
                                        <b-button :id="`popover-1-${item.id}`" variant="primary"><p><i class="fa fa-eye"></i> Ngôn ngữ: {{item.lang}}</p></b-button>
                                        <b-popover :target="`popover-1-${item.id}`"
                                                   placement="top"
                                                   title="Xem trước"
                                                   variant="danger"
                                                   triggers="click">
                                            <p v-for="urlLang in item.baseUrl">
                                                <a target="_blank" :href="urlLang.value"> Link: {{urlLang.key}}</a>
                                            </p>
                                        </b-popover>
                                    </td>
                                    <td style="width:150px">
                                        <label style="float:left">{{item.sortOrder}} => </label>
                                        <input style="width:50px;float:left" :value="item.sortOrderNew" v-on:keyup.enter="updateSorts($event,item.id)" />
                                    </td>
                                    <td class="text-center">
                                        <span v-if="item.status ==2" class="badge bg-warning">Chưa xuất bản</span>
                                        <span v-if="item.status ==1" class="badge bg-success">Xuất bản</span>
                                        <span v-if="item.status ==3" class="badge bg-danger">Đã xóa</span>
                                        <p>Lượt xem: {{item.viewCount}}</p>
                                    </td>
                                    <td class="product-action">
                                        <span v-if="item.status ==2" class="action-show"><a v-b-tooltip.hover title="Xuất bản" @click="remove(item,1)"><i style="color:green" class="fa fa-check-circle"></i></a></span>
                                        <span v-if="item.status ==1" class="action-hidden"><a v-b-tooltip.hover title="Hạ sản phẩm" @click="remove(item,2)"><i style="color:gold" class="fa fa-circle-o"></i></a></span>
                                        <router-link v-b-tooltip.hover title="Sửa sản phẩm" :to="{path: 'edit/'+ item.id}" target='_blank'>
                                            <span class="action-edit"><i class="fa fa-edit"></i></span>
                                        </router-link>
                                        <router-link v-b-tooltip.hover title="Thông tin bổ xung" :to="{path: 'productextent/'+ item.id}" target='_blank'>
                                            <span class="action-edit"><i style="color:brown" class="fa fa-newspaper-o"></i></span>
                                        </router-link>
                                        <span class="action-delete"><a v-b-tooltip.hover title="Xóa sản phẩm" @click="remove(item,3)"><i style="color:red" class="fa fa-trash"></i></a></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { unflatten, pathImg } from "../../plugins/helper";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    import Treeselect from '@riophae/vue-treeselect';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css';
    const fields = [
        { key: "id", label: "Id" },
        { key: "avatar", label: "Hình ảnh" },
        { key: "name", label: "Tên sản phẩm" },
        { key: "category", label: "Danh mục" },
        { key: "price", label: "Giá sản phẩm" },
        { key: "Is", label: "Thao tác" }
    ];

    export default {
        name: "product",
        components: {
            Loading,
            Treeselect
        },
        data() {
            return {
                valueConsistsOf: "BRANCH_PRIORITY",
                isLoading: false,
                fields,

                messeger: "",
                currentSort: "Id",
                currentSortDir: "desc",
                searchKey: "",
                searchStatus: 0,
                SearchLanguageCode: "vi-VN     ",
                ListStatus: [],
                SearchZoneId: [],
                SearchPromotionId: 0,
                SearchParrentVoucher: 0,
                ListZone: [],
                Language: [

                ],
                Promotions: [],
                voucherKey: "",

                IsInstallment: false,

                ListParrentVoucher: [],
                ListVoucher: [],

                ListVoucherChecked: [],
                ListProductChecked: [],

                currentPage: 1,
                pageSize: 10,
                color: "#007bff",
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
        methods: {
            ...mapActions(["getProducts", "addListVoucherByZone", "addListVoucherByProduct", "GetByCouponsChildParrentId", "deleteProduct", "supportsProduct", "getZones", "getAllLanguages", "getNamePromotion", "getAllCoupon", "exportPriceInLocation", "exportSpectifications", "updateSort"]),

            pathImgs(path) {
                return pathImg(path);
            },
            getAllParrentVoucher() {
                this.getAllCoupon().then(response => {
                    this.ListParrentVoucher = response;
                });
            },
            RemoveItem(index) {
                this.ListProductChecked.splice(index, 1)
            },
            AddVoucherByZone() {
                var data = {};
                data.ListKey = this.SearchZoneId;
                data.ListVoucher = this.ListVoucherChecked;
                if (this.SearchZoneId.length > 0 && this.ListVoucherChecked.length > 0) {
                    this.addListVoucherByZone(data).then(response => {
                        if (response.key == true) {
                            this.$toast.success(response.value, {});
                        } else {
                            this.$toast.error(response.value, {});
                        }
                    });
                } else {
                    this.$toast.error("Kiểm tra lại thông tin về voucher và sản phẩm", {});
                }

            },
            updateSorts(event, id) {
                debugger
                var sort = event.target.value;
                var obj = {};
                obj.id = id;
                obj.sortNew = sort;
                this.updateSort(obj).then(response => {
                    if (response.success == true) {
                        this.$toast.success(response.message, {});
                        //this.onChangePaging();
                        this.isLoading = false;
                    } else {
                        this.$toast.error(response.message, {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});
                });
            },
            AddVoucherByProduct() {
                var lstKey = this.ListProductChecked.map(x => x.id);

                var data = {};
                data.ListKey = lstKey;
                data.ListVoucher = this.ListVoucherChecked;
                if (lstKey.length > 0 && this.ListVoucherChecked.length > 0) {
                    this.addListVoucherByProduct(data).then(response => {
                        if (response.key == true) {
                            this.$toast.success(response.value, {});
                        } else {
                            this.$toast.error(response.value, {});
                        }
                    })
                } else {
                    this.$toast.error("Kiểm tra lại thông tin về voucher và sản phẩm", {});
                }


            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProducts({
                    languageCode: this.SearchLanguageCode || "vi-VN",
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    keyword: this.searchKey,
                    trangThai: this.searchStatus,
                    idZones: this.SearchZoneId,
                    voucher: this.voucherKey,
                    idPromotion: this.SearchPromotionId,
                    isInstallment: this.IsInstallment,
                    sortDir: this.currentSortDir,
                    sortBy: this.currentSort,
                });
                this.isLoading = false;
            },
            exportPriceLocation() {
                this.isLoading = true;
                var protocol = location.protocol;
                var slashes = protocol.concat("//");
                var host = slashes.concat(window.location.hostname);
                var port = location.port;
                if (port != 0 && port !== "") {
                    host = host.concat(":").concat(port);
                    console.log(host);
                }
                //let initial = this.$route.query.initial;
                //initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.exportPriceInLocation({
                    languageCode: this.SearchLanguageCode || "vi-VN",
                    pageIndex: 1,
                    pageSize: 1000000,
                    keyword: this.searchKey,
                    trangThai: this.searchStatus,
                    idZones: this.SearchZoneId,
                    voucher: this.voucherKey,
                    idPromotion: this.SearchPromotionId,
                    isInstallment: this.IsInstallment,
                  

                }).then(response => {

                    window.open(host + '/' + response.data, "_blank");
                });
                this.isLoading = false;
            },
            //exportSpectification
            exportSpectification() {
                this.isLoading = true;
                var protocol = location.protocol;
                var slashes = protocol.concat("//");
                var host = slashes.concat(window.location.hostname);
                var port = location.port;
                if (port != 0 && port !== "") {
                    host = host.concat(":").concat(port);
                    console.log(host);
                }
                //let initial = this.$route.query.initial;
                //initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.exportSpectifications({
                    languageCode: this.SearchLanguageCode || "vi-VN",
                    pageIndex: 1,
                    pageSize: 1000000,
                    keyword: this.searchKey,
                    trangThai: this.searchStatus,
                    idZones: this.SearchZoneId,
                    voucher: this.voucherKey,
                    idPromotion: this.SearchPromotionId,
                    isInstallment: this.IsInstallment
                }).then(response => {
                    //console.log(response.data);

                    window.open(host + '/' + response.data, "_blank");
                    this.isLoading = false;
                });

            },
            GetDataCouponsChildParrentId() {
                debugger
                if (this.SearchParrentVoucher > 0) {
                    this.GetByCouponsChildParrentId(this.SearchParrentVoucher).then(response => {
                        this.ListVoucher = response;
                    });
                }
            },
            sortor: function (s) {
                if (s === this.currentSort) {
                    this.currentSortDir = this.currentSortDir === "asc" ? "desc" : "asc";
                }
                this.currentSort = s;
                this.onChangePaging();
            },
            remove: function (item, status) {
                if (confirm("Bạn có thực sự muốn thay đổi trạng thái ?")) {
                    let obj = Object.assign({}, item)
                    obj.status = status;
                    this.deleteProduct(obj)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.onChangePaging();
                                this.isLoading = false;
                            } else {
                                this.$toast.error(response.message, {});
                                this.isLoading = false;
                            }
                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        });
                }
            }
        },
        computed: {
            ...mapGetters(["products"])
        },
        created() {
            this.getAllParrentVoucher();
            this.getNamePromotion().then(response => {
                try {
                    this.Promotions = response;
                }
                catch (ex) {

                }
            });

            this.supportsProduct().then(response => {
                try {
                    this.ListStatus = response.listStatus
                }
                catch (ex) {

                }
            });
            this.getZones(1).then(response => {
                try {
                    var data = response.listData;
                    data.push({ id: 0, label: "Chọn danh mục", parentId: 0 });
                    this.ListZone = unflatten(data);
                }
                catch (ex) {

                }
            })
            this.onChangePaging();
            //this.exportPriceLocation();
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            searchStatus: function (newVal) {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchZoneId: function (newVal) {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchLanguageCode: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchPromotionId: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            IsInstallment: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchParrentVoucher: function () {

                this.GetDataCouponsChildParrentId();
            }
        }
    };
</script>

<style>
</style>

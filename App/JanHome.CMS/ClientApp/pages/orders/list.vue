
<template>
    <div class="list-data">
        <b-card header-tag="header" class="card-filter"
                footer-tag="footer">
            <div>
                <b-col md="12">
                    <b-row class="form-group">
                        <b-col md="4">
                            <b-form-input @keyup.13="onChangePaging()" v-model="SearchKeyword" type="text" placeholder="Tìm kiếm theo tên"></b-form-input>
                        </b-col>
                        <b-col md="2">
                            <select class="form-control" v-model="SearchOrderStatus">
                                <option value="">Chọn trạng thái </option>
                                <option :value="item.value" v-for="item in OrderStatus"> {{item.text}}</option>
                            </select>
                        </b-col>

                        <b-col md="2">
                            <select class="form-control" v-model="SearchOrderType">
                                <option :value="item.value" v-for="item in OrderType"> {{item.text}}</option>
                            </select>
                        </b-col>
                        <b-col md="2">
                            <b-form-input type="date" v-model="SearchStartDate" placeholder="Ngày bắt đầu"></b-form-input>
                        </b-col>
                        <b-col md="2">
                            <b-form-input type="date" v-model="SearchEndDate" placeholder="Ngày kết thúc"></b-form-input>
                        </b-col>

                    </b-row>
                </b-col>
              
            </div>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class=" mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <!--<router-link class="btn btn-success" :to="{ path: 'add' }"><i class="fa fa-plus"></i> Thêm mới</router-link>-->
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>
                    
                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="orders.total"
                                      :per-page="pageSize"
                                      aria-controls="_product"></b-pagination>
                    </div>
                </div>
                
                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer table-bordered" role="grid">
                            <thead class="table table-centered table-nowrap">
                                <tr role="row">
                                    <th><!--<input type="checkbox">--></th>
                                    <th>Mã đơn hàng</th>
                                    <th>Khách hàng</th>
                                    <th>Sản phẩm</th>
                                    <th>Tóm tắt</th>
                                    <th>Ngày tạo</th>

                                    <th>Trạng thái</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr role="row" class="odd" v-for="item in orders.listData">
                                    <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                    <td class="product-thumb sorting_1">
                                        <div>
                                            <i class="fa fa-qrcode"></i>
                                            <router-link :to="{path: 'listdetail/'+ item.id}">
                                                {{item.orderCode}}
                                            </router-link>
                                        </div>
                                        <div>
                                            Loại đơn: {{item.type}}
                                        </div>
                                        <div>
                                            <i class="glyphicon glyphicon-time"></i> {{item.createDate}}
                                        </div>
                                    </td>
                                    <td class="product-thumb sorting_1">
                                        <div>
                                            <i class="fa fa-user"></i> {{item.custumerName}}
                                        </div>
                                        <div>
                                            <i class="fa fa-venus"></i> {{item.custumerGender}}
                                        </div>
                                        <div>
                                            <i class="fa fa-phone"></i> {{item.custumerPhone}}
                                        </div>
                                        <div>
                                            <i class="fa fa-envelope"></i> {{item.custumerEmail}}
                                        </div>
                                    </td>
                                    <td class="product-thumb sorting_1">
                                        <div v-for="product in item.orderDetail">
                                            <p><i class="fa fa-qrcode"></i> {{product.code}} / SL: {{product.quantity}}</p>
                                            <p>{{product.name}} : {{product.logPrice}}</p>
                                        </div>
                                    </td>

                                    <td class="product-thumb sorting_1">
                                        <div>Số bàn: {{item.voucherDetail.soBan}}</div>
                                        <div>Mã voucher: {{item.voucherDetail.voucherCode}}</div>
                                        <div>GT voucher: {{item.voucherDetail.voucherGiaTri}}</div>
                                        <div>Tổng tiền: {{item.voucherDetail.tongTien}}</div>
                                        <div>Giờ đặt: {{item.voucherDetail.thoiGianGiao}}</div>


                                    </td>
                                    <td>
                                        <p>{{item.createdDate}} </p>
                                    </td>
                                    <td class="product-thumb sorting_1">
                                        <b-form-select v-model="item.status" @change="onChangeSelected($event,item.id)" :options="OrderStatus"></b-form-select>
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
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import moment from 'moment';
    export default {
        name: "order",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                _product: {

                },
                SearchStartDate: moment(String(new Date("2000-01-01"))).format('YYYY-MM-DD'),
                SearchEndDate: moment(String(new Date("2030-01-01"))).format('YYYY-MM-DD'),
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",
                SearchKeyword: "",
                SearchOrderStatus: "",
                SearchOrderType: 0,
                currentPage: 1,
                pageSize: 10,
                loading: true,

                OrderStatus: [],
                OrderType: [],
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
            ...mapActions(["getOrders", "getOrderStatus", "updateOrderStatus", "getAllTypeOrder"]),
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getOrders({
                    keyword: this.SearchKeyword,
                    orderStatus: this.SearchOrderStatus,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    startDate: this.SearchStartDate,
                    endDate: this.SearchEndDate
                });
                this.isLoading = false;
            },
            sortor: function (s) {
                if (s === this.currentSort) {
                    this.currentSortDir = this.currentSortDir === "asc" ? "desc" : "asc";
                }
                this.currentSort = s;
                this.onChangePaging();
            },
            onChangeSelected(status, id) {
                this.isLoading = true;
                let objData = {};
                objData.Status = status;
                objData.Id = id;
                this.updateOrderStatus(objData).then(res => {
                    if (res.success == true) {
                        this.$toast.success(res.message, {});
                        this.isLoading = false;
                    }
                    else {
                        this.$toast.error(res.message, {});
                        this.isLoading = false;
                    }

                }).catch(ex => {
                    this.$toast.error(msgNotify.error + ". Error:" + ex, {});
                });
            },
        },


        computed: {
            ...mapGetters(["orders"])
        },
        mounted() {
            this.onChangePaging();
            this.getOrderStatus().then(respose => {
                this.OrderStatus = respose.listData.map(function (item) {
                    return {
                        value: item.key,
                        text: item.value
                    };
                });
            });
            this.getAllTypeOrder().then(respose => {
                this.OrderType = respose.listData.map(function (item) {
                    return {
                        value: item.key,
                        text: item.value
                    };
                });
            });
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            SearchOrderStatus: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchStartDate: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchEndDate: function () {
                this.currentPage = 1;
                this.onChangePaging();
            }
        }
    };
</script>


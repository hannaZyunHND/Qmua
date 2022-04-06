<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <div class="row productedit">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        Thông tin đơn hàng
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="table-responsive">
                                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                                        <div class="clear"></div>
                                        <table class="table table-bordered table-mini-text table-style-custom no-margin">
                                            <tbody>
                                                <tr role="row" class="odd">
                                                    <td colspan="2"><b>Thông tin chính</b></td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Mã đơn hàng</td>
                                                    <td>{{order.code}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Trạng thái</td>
                                                    <td>{{order.status}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Nguồn đơn hàng</td>
                                                    <td>{{order.src}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Ngày tạo</td>
                                                    <td>{{order.createDate}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Mã voucher</td>
                                                    <td>{{order.odderDetail[0].voucher}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Giá trị voucher</td>
                                                    <td>{{order.odderDetail[0].voucherPrice}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Tổng tiền</td>
                                                    <td>{{order.odderDetail[0].oderPrice}}</td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="table-responsive">
                                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                                        <div class="clear"></div>
                                        <table class="table table-bordered table-mini-text table-style-custom no-margin">
                                            <tbody>
                                                <tr role="row" class="odd">
                                                    <td colspan="2"><b>Thông tin khách hàng</b></td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Họ và tên</td>
                                                    <td>{{order.customer.name}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Giới tính</td>
                                                    <td>{{order.customer.gender}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Số điện thoại</td>
                                                    <td>{{order.customer.phoneNumber}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Email</td>
                                                    <td>{{order.customer.email}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Địa chỉ</td>
                                                    <td>{{order.address}}</td>
                                                </tr>
                                                <tr role="row" class="odd">
                                                    <td>Ghi chú</td>
                                                    <td>{{order.customer.note}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row productedit">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        Chi tiết đơn hàng
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                                        <div class="clear"></div>
                                        <table class="table table-bordered table-mini-text table-style-custom no-margin">
                                            <thead>
                                                <tr>
                                                    <th>STT</th>
                                                    <th>Mã sản phẩm</th>
                                                    <th>Sản phẩm</th>
                                                    <th>SL</th>
                                                    <th>Giá bán</th>
                                                    <!--<th>Voucher</th>-->
                                                    <th>Khuyến mại </th>
                                                    <th>Thành tiền</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr role="row" class="odd" v-for="(item,index) in order.odderDetail">
                                                    <td>{{index + 1 }}</td>
                                                    <td>{{item.code}}</td>
                                                    <td> <a :href="item.url">{{item.name}}</a></td>
                                                    <td> {{item.number}}</td>
                                                    <td>{{item.price}}</td>
                                                    <!--<td v-html="item.strVoucher"></td>-->
                                                    <td v-html="item.strPromotion"></td>
                                                    <td>{{item.totalPrice}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
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
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    export default {
        name: "ordergetbyid",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                OrderId: 0,
                TotalPrice: 0,
                order: {

                },
                orderDetail: [],
                orderDetailPromotion: [],
                products: [],
                customer: {}
            };
        },
        created: {},
        components: {
            Loading
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getOrder(this.$route.params.id).then(response => {
                    debugger
                    this.order = response;
                    console.log(this.order);
                });
                this.isLoading = false;
            };
        },

        computed: {
            ...mapGetters(["Order"]),

        },

        methods: {
            ...mapActions([
                "getOrder",
            ]),
        }
    };

</script>
<style>
</style>

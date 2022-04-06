<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-primary">
                    <b-card-body class="pb-0">
                        <p class="text-center">TỔNG ĐƠN</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalOrder}}</h2>
                    </b-card-body>
                    <cardline1chartexample chartId="card-chart-01" class="chart-wrapper px-3" style="height:70px;" :height="70" />
                </b-card>
            </b-col>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-info">
                    <b-card-body class="pb-0">
                        <p class="text-center">ĐƠN MỚI</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalNewOrder}}</h2>
                    </b-card-body>
                    <cardline2chartexample chartId="card-chart-02" class="chart-wrapper px-3" style="height:70px;" :height="70" />
                </b-card>
            </b-col>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-warning">
                    <b-card-body class="pb-0">
                        <p class="text-center">ĐANG XÁC NHẬN</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalProcessingOrder}}</h2>
                    </b-card-body>
                    <cardline3chartexample chartId="card-chart-03" class="chart-wrapper" style="height:70px;" height="70" />
                </b-card>
            </b-col>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-success">
                    <b-card-body class="pb-0">
                        <p class="text-center">ĐÃ XÁC NHẬN</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalApprovedOrder}}</h2>
                    </b-card-body>
                    <cardline1chartexample chartId="card-chart-04" class="chart-wrapper px-3" style="height:70px;" height="70" />
                </b-card>
            </b-col>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-success" style="background-color: #6f42c1 !important">
                    <b-card-body class="pb-0">
                        <p class="text-center">THÀNH CÔNG</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalSuccessOrder}}</h2>
                    </b-card-body>
                    <cardline2chartexample chartId="card-chart-05" class="chart-wrapper px-3" style="height:70px;" height="70" />
                </b-card>
            </b-col>
            <b-col sm="6" lg="2">
                <b-card no-body class="bg-danger">
                    <b-card-body class="pb-0">
                        <p class="text-center">HỦY</p>
                        <h2 class="mb-0 text-center">{{DashBoardData1.totalCancleOrder}}</h2>
                    </b-card-body>
                    <cardline3chartexample chartId="card-chart-06" class="chart-wrapper px-3" style="height:70px;" height="70" />
                </b-card>
            </b-col>
        </b-row>
        <b-card>
            <b-row>
                <b-col sm="5">
                    <h4 id="traffic" class="card-title mb-0">Thống kê đơn hàng trong tháng</h4>
                </b-col>
                <b-col sm="7" class="d-none d-md-block">
                    <b-button-toolbar class="float-right">
                        <!-- @change="onChangeYear()"-->
                        <b-form-select v-model="yearselected" :options="Years" @change="onChangeYear()"></b-form-select>
                    </b-button-toolbar>
                    <b-button-toolbar class="float-right">
                        <b-form-select v-model="monthselected" :options="Months" @change="onChangeMonth()"></b-form-select>
                    </b-button-toolbar>
                </b-col>
            </b-row>
            <MainChartExample chartId="main-chart-01" class="chart-wrapper" style="height:300px;margin-top:40px;" :actions="actionChart"></MainChartExample>
        </b-card>
        <b-card>
            <b-row>
                <b-col sm="5">
                    <h4 class="card-title mb-0">Danh sách 10 đơn hàng gần nhất</h4>
                </b-col>
                <b-col sm="7">
                    <h6 class="card-title mb-0 pull-right"><a href="/admin/orders/list">Xem thêm...</a></h6>
                </b-col>
            </b-row>
            <table class="table data-thumb-view dataTable no-footer table-bordered" role="grid" style="margin-top:30px !important">
                <thead class="table table-centered table-nowrap">
                    <tr role="row">
                        <th class="text-center">Mã đơn hàng</th>
                        <th class="text-center">Ngày tạo</th>
                        <th class="text-center">Thông tin khách hàng</th>
                        <th class="text-center">Địa chỉ</th>
                        <th class="text-center">Ghi chú</th>
                        <th class="text-center">VAT</th>
                        <th class="text-center">Trạng thái</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in DashBoardData3">
                        <td class="product-thumb sorting_1" scope="row">{{item.orderCode}}</td>
                        <td class="product-thumb sorting_1">{{item.createdDateString}}</td>
                        <td class="product-thumb sorting_1">Tên: {{item.cusName}} | SDT: {{item.cusPhoneNumber}} | Địa chỉ: {{item.cusAddress}} </td>
                        <td class="product-thumb sorting_1">{{item.address}}</td>
                        <td class="product-thumb sorting_1">{{item.note}}</td>
                        <td class="product-thumb sorting_1">{{item.vat}}</td>
                        <td class="product-thumb sorting_1">{{item.statusName}}</td>
                    </tr>
                </tbody>
            </table>
        </b-card>
        <b-card>
            <b-row>
                <b-col sm="5">
                    <h4 class="card-title mb-0">Danh sách 10 comment gần nhất</h4>
                </b-col>
                <b-col sm="7">
                    <h6 class="card-title mb-0 pull-right"><a href="/admin/comment/list">Xem thêm...</a></h6>
                </b-col>
            </b-row>
            <table class="table data-thumb-view dataTable no-footer table-bordered" role="grid" style="margin-top:30px !important">
                <thead class="table table-centered table-nowrap">
                    <tr role="row">
                        <th class="text-center">Comment</th>
                        <th class="text-center">Tên khách hàng</th>
                        <th class="text-center">SDT/Email</th>
                        <th class="text-center">Loại</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in DashBoardComment">
                        <td class="product-thumb sorting_1" scope="row">{{item.content}}</td>
                        <td class="product-thumb sorting_1" scope="row">{{item.name}}</td>
                        <td class="product-thumb sorting_1" scope="row">{{item.phoneOrMail}}</td>
                        <td class="product-thumb sorting_1" scope="row">{{item.type}}</td>
                    </tr>
                </tbody>
            </table>
        </b-card>
    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css"
    import Loading from "vue-loading-overlay"
    import { mapGetters, mapActions } from "vuex"
    import MainChartExample from '../components/dashboard/MainChartExample.vue'
    import cardline1chartexample from '../components/dashboard/CardLine1ChartExample.vue'
    import cardline2chartexample from '../components/dashboard/CardLine2ChartExample.vue'
    import cardline3chartexample from '../components/dashboard/CardLine3ChartExample.vue'
   
    //import { Bar } from 'vue-chartjs'
    const DateNow = new Date();

    export default {
        name: 'dashboard',
        //extends: Bar,
        components: {
            Loading,
            MainChartExample,
            cardline1chartexample,
            cardline2chartexample,
            cardline3chartexample,
           
        },
        data() {
            return {
                isLoading: false,
                monthselected: DateNow.getMonth() + 1,
                yearselected: DateNow.getFullYear(),
                actionChart: false,
                DashBoardData1: {},
                DashBoardData2: [],
                DashBoardData3: [],
                DashBoardComment: [],
                Months: [
                    { value: "1", text: "Tháng 1" },
                    { value: "2", text: "Tháng 2" },
                    { value: "3", text: "Tháng 3" },
                    { value: "4", text: "Tháng 4" },
                    { value: "5", text: "Tháng 5" },
                    { value: "6", text: "Tháng 6" },
                    { value: "7", text: "Tháng 7" },
                    { value: "8", text: "Tháng 8" },
                    { value: "9", text: "Tháng 9" },
                    { value: "10", text: "Tháng 10" },
                    { value: "11", text: "Tháng 11" },
                    { value: "12", text: "Tháng 12" }
                ],
                Years: [
                ],
            }
        },
        //prop: {
        //    chartdata: {
        //        type: Object,
        //        default: null
        //    },
        //    options: {
        //        type: Object,
        //        default: null
        //    }
        //},
        methods: {
            ...mapActions(["getDashBoard1", "getDashBoard2", "getDashBoard3", "getCommentData"]),
            onLoadPage() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getDashBoard1().then(res => {
                    this.DashBoardData1 = res.data;
                });
                this.getDashBoard3().then(res => {
                    this.DashBoardData3 = res.listData;
                });
                this.getCommentData().then(res => {
                    this.DashBoardComment = res.listData;
                });
                this.isLoading = false;
            },

            onChangeMonth() {
                this.actionChart = true;
                //this.MainChartExample.onLoad();
            },

            onChangeYear() {
                this.actionChart = true;
                //this.MainChartExample.onLoad();
            }
        },
        computed: {
            ...mapGetters(["dashboards"])
        },
        mounted() {
            this.onLoadPage();
            let yNow = DateNow.getFullYear();
            for (let index = 2019; index <= yNow; index++) {
                let item = {};
                item.value = index;
                item.text = index;
                this.Years.push(item);
            }
        },
        watch: {
            monthselected: function (newVal) {
                this.onChangeMonth();
            },
            yearselected: function (newVal) {
                this.onChangeYear();
            },
        }
    }
</script>
<style>

    /* IE fix */
    #card-chart-01, #card-chart-02 {
        width: 100% !important;
    }
</style>

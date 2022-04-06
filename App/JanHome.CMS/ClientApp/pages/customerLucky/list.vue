<template>
    <div>
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <!--<loading :active.sync="isLoading"
                     :height="35"
                     :width="35"
                     :color="color"
                     :is-full-page="fullPage"></loading>-->
            <div>
                <b-col md="12">
                    <b-row class="form-group">
                        <b-col md="4">
                            <b-form-input v-model="keyword" type="text" v-on:keyup.enter="onChangePaging()" placeholder="Số điện thoại"></b-form-input>
                        </b-col>
                        
                        <b-col md="2">
                            <b-btn variant="info" class="col-lg-12" @click="onChangePaging()"><i class="fa fa-search" aria-hidden="true"></i></b-btn>
                        </b-col>
                    </b-row>
                </b-col>
                
            </div>
        </b-card>
      
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class=" mb-2" aria-label="Toolbar with button groups and dropdown menu">
                   
                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="cslucky.totalRow"
                                      :per-page="pageSize"
                                      ></b-pagination>
                    </div>
                </div>
                <table class="table">
                    <thead class="thead-dark table table-centered table-nowrap">
                        <tr>
                            <th v-for="field in fields"
                                class="text-center"
                                style="max-width:150px"
                                scope="col">{{field.label}}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in cslucky.listData" >
                            <td class="text-left">{{item.phoneNumber}}</td>
                            <td class="text-left">{{item.luckySpinName}}</td>
                            <td class="text-center">{{format_date(item.createdDate)}}</td>

                        </tr>

                    </tbody>
                </table>
            </div>

        </div>
    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import moment from 'moment'
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    const fields = [
        //{ key: "id", label: "Mã" },
        { key: "phoneNumber", label: "Điện thoại" },
        { key: "luckySpinName", label: "Giải" },
        { key: "createdDate", label: "Thời gian" }
        //{ key: "SortOrder", label: "Sắp xếp", sortable: true },
        //{ key: "Thumb", label: "Thumb" },
        //{ key: "IsEnable", label: "Kích Hoạt" },
        //{ key: "Is", label: "Thao tác" }
    ];

    export default {
        name: "ads",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                fields,
                keyword: '',
               
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",

                currentPage: 1,
                pageSize: 10,
                loading: true,
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
            ...mapActions(["customerLuckyGetAll"]),

            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },


            onChangePaging() {
                this.isLoading = true;
                this.customerLuckyGetAll({
                    keyword: this.keyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize
                   
                });
                this.isLoading = false;
            },
            sortor: function (s) {
               
            },

        },
        computed: {
            ...mapGetters(["cslucky"])
        },
        mounted() {
            this.onChangePaging();
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            }
        }
    };
</script>

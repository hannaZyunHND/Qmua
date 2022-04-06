
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
                            <select v-model="searchStatus" class="form-control">
                                <option v-for="item in ListStatus" :value="item.Id">
                                    {{item.Text}}
                                </option>
                            </select>


                        </b-col>

                        <b-col md="1">
                            <b-btn variant="info" class="col-lg-12"><i class="fa fa-search" aria-hidden="true"></i></b-btn>
                        </b-col>
                        <b-col md="1">
                            <b-btn variant="primary" class="col-lg-12"><i class="fa fa-refresh" aria-hidden="true"></i></b-btn>
                        </b-col>
                        <b-col md="2">
                            <b-btn v-b-toggle.collapse1 variant="primary"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                        </b-col>
                    </b-row>
                </b-col>

            </div>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class=" mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <router-link class="btn btn-success" :to="{ path: 'add' }"><i class="fa fa-plus"></i> Thêm mới</router-link>
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>

                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="promotions.total"
                                      :per-page="pageSize"
                                      aria-controls="_product"></b-pagination>
                    </div>
                </div>

                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer" role="grid">
                            <thead class="thead-dark table table-centered table-nowrap">
                                <tr role="row">
                                    <th></th>
                                    <th>Tên khuyến mãi</th>
                                    <th>Loại khuyến mãi</th>
                                    <th>Trừ tiền</th>
                                    <th>Giá</th>
                                    <th>Ngôn ngữ</th>
                                    <th>Ngày tháng</th>
                                    <th>Trạng thái</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr role="row" class="odd" v-for="item in promotions.listData">
                                    <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                    <td>
                                        <p>{{item.name}}</p>

                                    </td>
                                    <td class="product-type sorting_1">{{item.typeName}}</td>
                                    <td class="product-isDiscountPrice">
                                        <template v-if="item.isDiscountPrice==true">
                                            Trừ tiền mặt
                                        </template>
                                        <template v-if="item.isDiscountPrice==false">
                                            Không trừ tiền mặt
                                        </template>
                                    </td>
                                    <td class="product-value">{{item.value}}</td>
                                    <td>
                                        <p>Ngôn ngữ: {{item.langCount}}</p>
                                    </td>
                                    <td>
                                        <p>NBĐ: {{item.startDate}}</p>
                                        <p>NKT: {{item.endDate}}</p>
                                    </td>
                                    <td class="text-center">
                                        <span v-if="item.status ==2" class="badge bg-warning">Chưa xuất bản</span>
                                        <span v-if="item.status ==1" class="badge bg-success">Xuất bản</span>
                                        <span v-if="item.status ==3" class="badge bg-danger">Đã xóa</span>
                                    </td>
                                    <td class="product-action">
                                        <router-link :to="{path: 'edit/'+ item.id}">
                                            <span class="action-edit"><i class="fa fa-edit"></i></span>
                                        </router-link>

                                        <span v-if="item.status ==2" class="action-show"><a v-b-tooltip.hover title="Xuất bản" @click="remove(item,1)"><i style="color:green" class="fa fa-check-circle"></i></a></span>
                                        <span v-if="item.status ==1" class="action-hidden"><a v-b-tooltip.hover title="Ẩn" @click="remove(item,2)"><i style="color:gold" class="fa fa-circle-o"></i></a></span>
                                        <span class="action-delete"><a v-b-tooltip.hover title="Xóa" @click="remove(item,3)"><i style="color:red" class="fa fa-trash"></i></a></span>
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

    export default {
        name: "product",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                _product: {

                },
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",

                currentPage: 1,
                pageSize: 10,
                loading: true,

                SearchKeyword: "",
                SearchLanguageCode: "vi-VN     ",
                Language: [],
                searchStatus: 0,
                ListStatus: [
                    {
                        Id: 0,
                        Text: "Tất cả",
                    },
                    {
                        Id: 1,
                        Text: "Xuất bản",
                    },
                    {
                        Id: 2,
                        Text: "Ẩn",
                    },
                    {
                        Id: 3,
                        Text: "Đã xóa",
                    },
                ],

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
            ...mapActions(["getPromotions", "deletePromotion", "getAllLanguages"]),
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getPromotions({
                    keyword: this.SearchKeyword,
                    languageCode: this.SearchLanguageCode || "vi-VN",
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    status: this.searchStatus,
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
            remove: function (item, status) {
                item.status = status;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.deletePromotion(item)
                    .then(response => {
                        if (response.key == true) {
                            this.$toast.success(response.value, {});
                            this.onChangePaging();
                            this.isLoading = false;
                        } else {
                            this.$toast.error(response.value, {});
                            this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    });
            }
        },
        computed: {
            ...mapGetters(["promotions"])
        },
        mounted() {
            this.onChangePaging();
            //this.getAllLanguages().then(respose => {
            //    this.Language = respose.listData.map(function (item) {
            //        return {
            //            value: item.languageCode,
            //            text: item.name
            //        };
            //    });
            //});
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            SearchLanguageCode: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            searchStatus: function () {
                this.currentPage = 1;
                this.onChangePaging();
            }
        }
    };
</script>



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
                        <b-col md="1">
                            <b-btn variant="info" class="col-lg-12"><i class="fa fa-search" aria-hidden="true"></i></b-btn>
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
                                      :total-rows="objRequests.total"
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
                                    <th><!--<input type="checkbox">--></th>
                                    <th class="text-center">STT</th>
                                    <th>Tên chiến dịch</th>
                                    <th>Ngày bắt đầu</th>
                                    <th>Ngày kết thúc</th>
                                    <th class="text-center">Trạng thái</th>
                                    <th class="text-center">Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item,index) in objRequests.listData" role="row" class="odd">
                                    <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                    <td class="text-center">{{index++}}</td>
                                    <td>
                                        {{item.name}}
                                    </td>
                                    <td style="max-width:300px">
                                        {{item.startTime}}
                                    </td>
                                    <td style="max-width:300px">
                                        {{item.endTime}}
                                    </td>
                                    <td class="text-center">
                                        <template v-if="item.status==1">
                                            <span class="badge bg-warning">Sắp bán</span>
                                        </template>
                                        <template v-if="item.status==2">
                                            <span class="badge bg-success">Đang bán</span>
                                        </template>
                                        <template v-if="item.status==3">
                                            <span class="badge bg-danger">Kết thúc</span>
                                        </template>
                                    </td>
                                    <td class="text-center">
                                        <router-link :to="{path: 'edit/'+item.id}">
                                            <a v-b-tooltip.hover title="Sửa chiến dịch">
                                                <i style="color:gold" class="fa fa-edit"></i>
                                            </a>
                                        </router-link>
                                        <template v-if="item.status!=1">
                                            <span class="badge bg-warning" @click="updateStatus(item,1)">Sắp bán</span>
                                        </template>
                                        <template v-if="item.status!=2">
                                            <span class="badge bg-success" @click="updateStatus(item,2)">Đang bán</span>
                                        </template>
                                        <template v-if="item.status!=3">
                                            <span class="badge bg-danger" @click="updateStatus(item,3)">Kết thúc</span>
                                        </template>
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
    import moment from 'moment'
    export default {
        name: "comments",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",
                SearchKeyword: "",
                SearchType: 0,
                SearchStatus: 1,
                currentPage: 1,
                pageSize: 1000,
                loading: true,
                searchLanguageCode: "vi-VN     ",
                objRequests: {

                },
                objRequest: {
                    code: "#000000",
                    languageCode: "vi-VN     "
                },
                objUser: {

                },
                ListStatus: [],
                ListType: [],
                Languages: [],
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
        created() {


            this.objUser = this.getUser();

        },

        methods: {
            ...mapActions(["getFlashSales", "createFlashSale", "removeFlashSale", "updateStatusFlashSale"]),
            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getFlashSales({
                    keyword: this.SearchKeyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                }).then(response => {
                    debugger
                    this.objRequests = response;
                });
                this.isLoading = false;
            },
            getUser() {
                //debugger-
                var data = JSON.parse(localStorage.getItem('currentUser'));
                return JSON.parse(localStorage.getItem('currentUser'));
            },
            edit(obj) {
                //debugger-
                this.objRequest = Object.assign({}, obj);;

                this.$bvModal.show('edit');
            },
            create(item) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.createFlashSale(item).then(response => {
                    //debugger-
                    if (response.Success == true) {
                        this.$toast.success(response.Message, {});
                        this.onChangePaging();
                        this.isLoading = false;
                    } else {
                        this.$toast.error(response.Message, {});
                        this.isLoading = false;
                    }
                });
            },


            toggeter(id) {
                //debugger-
                var str = "tr[data-x-id='" + id + "']";
                var menu = document.querySelectorAll(str).forEach(function (item) {
                    item.classList.toggle('hidden');
                });
            }
            ,
            sortor: function (s) {
                if (s === this.currentSort) {
                    this.currentSortDir = this.currentSortDir === "asc" ? "desc" : "asc";
                }
                this.currentSort = s;
                this.onChangePaging();
            },
            updateStatus: function (item, status) {
                //debugger-
                item.status = status;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.updateStatusFlashSale(item)
                    .then(response => {
                        if (response.key == true) {
                            this.$toast.success(response.value, {});
                            this.onChangePaging();
                            this.isLoading = false;
                        } else {
                            this.$toast.error(response.Message, {});
                            this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    });
            }
        },
        computed: {

        },
        mounted() {
            this.onChangePaging();

        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            searchLanguageCode: function () {

                this.onChangePaging();
            },
        }
    };
</script>


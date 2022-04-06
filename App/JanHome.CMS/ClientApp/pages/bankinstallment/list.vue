
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
                            <select v-model="SearchType" class="form-control">
                                <option v-for="item in ListType" :value="item.key">
                                    {{item.value}}
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
                <b-collapse id="collapse1" class="mt-2">
                    <b-card>
                        <p class="card-text">Collapse contents Here</p>
                        <b-btn v-b-toggle.collapse1_inner size="sm">Toggle Inner Collapse</b-btn>
                        <b-collapse id=collapse1_inner class="mt-2">
                            <b-card>Hello!</b-card>
                        </b-collapse>
                    </b-card>
                </b-collapse>
            </div>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class=" mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <router-link :to="{ path: 'add' }" class="btn btn-success"><i class="fa fa-plus"></i> Thêm mới</router-link>
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>
                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="bankinstallments.total"
                                      :per-page="pageSize"
                                      aria-controls="_bankinstallment"></b-pagination>
                    </div>
                </div>

                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer" role="grid">
                            <thead class="thead-dark table table-centered table-nowrap">
                                <tr role="row">
                                    <th></th>
                                    <th>Ngân hàng</th>
                                    <th>Hình ảnh</th>
                                    <th>Loại thẻ</th>
                                    <th>Phí quẹt thẻ</th>
                                    <th>Ngày tạo</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="item in bankinstallments">
                                    <tr role="row" class="odd">
                                        <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                        <td style="max-width:300px">
                                            <p>{{item.name}}</p>
                                        </td>
                                        <td>
                                            <img style="width:100px;height:auto" class="img-thumbnail" :src="pathImgs(item.avatar)" alt="Ảnh lỗi">
                                        </td>
                                        <td v-if="item.type == 1">
                                            <p>Thẻ thường</p>
                                        </td>
                                        <td v-else>
                                            <p>Thẻ Visa</p>
                                        </td>
                                        <td>
                                            <p>{{item.charge}}</p>
                                        </td>
                                        <td v-html="format_date(item.createdDate)"></td>
                                        <td>
                                            <router-link v-b-tooltip.hover title="Sửa nội dung" :to="{path: 'edit/'+ item.id}" class="action-edit"> <i class="fa fa-edit"> </i></router-link>
                                            <span class="action-delete"><a @click="remove(item)"><i class="fa fa-trash"></i></a></span>
                                        </td>
                                    </tr>
                                </template>
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
        name: "bankinstallment",
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
                SearchSource: 0,
                currentPage: 1,
                pageSize: 10,
                loading: true,

                objRequest: {

                },
                objUser: {

                },
                ListSource: [],
                ListType: [],

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

            this.getCardType().then(respose => {
                this.ListType = respose;
            });
            this.objUser = this.getUser();

        },

        methods: {
            ...mapActions(["getBankInstallments", "getBankInstallment", "removeBankInstallment", "createBankInstallment", "getCardType"]),
            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getBankInstallments({
                    keyword: this.SearchKeyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    type: this.SearchType
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
            remove: function (item) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                if (item.id > 0) {
                    this.removeBankInstallment(item.id)
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
                else {
                    this.$toast.error('Bản ghi không tồn tại.', {});
                }
            },
            pathImgs(path) {
                let url = 'https://cmsenterbuy.migroup.asia/' + "uploads/thumb" + path;
                return url;
            }
        },
        computed: {
            ...mapGetters(["bankinstallments"])
        },
        mounted() {
            this.onChangePaging();

        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            SearchType: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
        }
    };
</script>


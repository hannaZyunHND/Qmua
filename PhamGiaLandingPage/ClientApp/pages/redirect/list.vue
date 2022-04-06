
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
                    <b-col md="12">
                        <b-row class="form-group">
                            <b-col md="4">
                                <input type="text" class="form-control" v-model="objRequest.urlOld" placeholder="Url cũ" />
                            </b-col>
                            <b-col md="4">
                                <b-form-input v-model="objRequest.urlNew" type="text" placeholder="Url mới"></b-form-input>
                            </b-col>
                            <b-col md="2">
                                <select class="form-control" v-model="objRequest.urlType">
                                    <option value="301">Redirect 301</option>
                                    <option value="302">Redirect 302</option>
                                </select>
                            </b-col>
                            <b-col md="2">
                                <button type="button" @click="create(objRequest)" class="btn btn-success"><i class="fa fa-plus"></i> Lưu</button>
                            </b-col>
                        </b-row>
                    </b-col>
                </div>
                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer" role="grid">
                            <thead class="thead-dark table table-centered table-nowrap">
                                <tr role="row">
                                    <th></th>
                                    <th>Url cũ</th>
                                    <th>Url mới</th>
                                    <th>Loại</th>
                                    <th class="text-center">Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="item in objRequests.listData">
                                    <tr role="row" class="odd">
                                        <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                        <td>
                                            {{item.urlOld}}
                                        </td>
                                        <td>
                                            {{item.urlNew}}
                                        </td>
                                        <td>
                                            {{item.urlType}}
                                        </td>
                                        <td class="text-center">
                                            <span class="action-hidden"><a v-b-tooltip.hover title="Sửa" @click="edit(item)"><i style="color:blue" class="fa fa-edit"></i></a></span>
                                            <span class="action-show"><a v-b-tooltip.hover title="Xóa" @click="remove(item)"><i style="color:red" class="fa fa-trash"></i></a></span>
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
        name: "redirects",
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
                objRequests: [],
                objRequest: {
                    urlType: 301,
                    status: false,
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
            ...mapActions(["getRedirects", "createRedirects", "removeRedirects", "getAllLanguages"]),
            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getRedirects({
                    keyword: this.SearchKeyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    languageCode: this.searchLanguageCode || "vi-VN     ",
                }).then(response => {
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
                this.objRequest = obj;

            },
            create(item) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.createRedirects(item).then(response => {
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
            remove(item) {
                if (confirm("Bạn có thực sự muốn xóa màu này ?")) {
                    this.removeRedirects(item).then(response => {
                        if (response.Success == true) {
                            this.$toast.success(response.Message, {});
                            this.onChangePaging();
                            this.isLoading = false;
                        } else {
                            this.$toast.error(response.Message, {});
                            this.isLoading = false;
                        }
                    });
                }
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
                this.updateStatusContact(item)
                    .then(response => {
                        if (response.Success == true) {
                            this.$toast.success(response.Message, {});
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
            ...mapGetters(["colors"])
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


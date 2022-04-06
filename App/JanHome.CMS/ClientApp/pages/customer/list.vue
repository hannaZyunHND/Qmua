
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
                            <select v-model="SearchSource" class="form-control">
                                <option v-for="item in ListSource" :value="item.key">
                                    {{item.value}}
                                </option>
                            </select>
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
                        <button type="button" @click="create()" class="btn btn-success"><i class="fa fa-plus"></i> Thêm mới</button>
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>
                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="customers.total"
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
                                    <th>Thông tin</th>
                                    <th>Liên hệ</th>
                                    <th>Ngày tạo</th>
                                    <th>Ghi chú</th>
                                    <th>Nguồn</th>
                                    <th>Loại</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="item in customers.listData">
                                    <tr role="row" class="odd">
                                        <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                        <td style="max-width:300px">
                                            <p>Tên: {{item.name}}</p>
                                            <p>Giới tính: {{item.gender}}</p>
                                        </td>
                                        <td>
                                            <p>Contact: {{item.phoneNumber}}</p>
                                            <p>Mail: {{item.email}}</p>
                                        </td>
                                        <td v-html="format_date(item.createdDate)"></td>
                                        <td style="max-width:300px">
                                            <p>
                                                {{item.note}}
                                            </p>
                                        </td>
                                        <td class="text"><span class="badge bg-info"> {{item.sourceName}}</span>  </td>
                                        <td class="text">
                                            <span v-if="item.type==1" class="badge bg-info">Mới</span>
                                            <span v-if="item.type==2" class="badge bg-success">Tiềm năng</span>
                                            <span v-if="item.type==3" class="badge bg-danger">Danh sách đen</span>
                                        </td>
                                        <td class="text-center">
                                            <!--<span v-if="item.status !=2" class="action-show"><a v-b-tooltip.hover title="Xuất bản" @click="updateStatus(item,2)"><i style="color:green" class="fa fa-check-circle"></i></a></span>
                                            <span v-if="item.status !=3" class="action-hidden"><a v-b-tooltip.hover title="Hạ sản phẩm" @click="updateStatus(item,3)"><i style="color:gold" class="fa fa-circle-o"></i></a></span>-->
                                            <span v-b-tooltip.hover title="Trả lời" @click="edit(item)" class="action-edit"> <i class="fa fa-edit"> </i></span>
                                        </td>
                                    </tr>
                                </template>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <b-modal size="xl" id="edit" title="Thông tin khách hàng" hide-footer>

            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <b-form-group label="Tên">
                        <b-form-input v-model="objRequest.name" type="text" required placeholder="Họ tên hiển thị"></b-form-input>
                    </b-form-group>
                </div>
                <div class="col-md-4 col-xs-12">
                    <b-form-group label="Giới tính">
                        <select v-model="objRequest.gender" class="form-control">
                            <option value="male">Nam</option>
                            <option value="female">Nữ</option>
                        </select>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <b-form-group label="Số điện thoại">
                        <b-form-input v-model="objRequest.phoneNumber" type="text" required placeholder="Số điện thoại"></b-form-input>
                    </b-form-group>
                </div>
                <div class="col-md-6 col-xs-12">
                    <b-form-group label="Hòm thư">
                        <b-form-input v-model="objRequest.email" type="text" required placeholder="Email"></b-form-input>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <b-form-group label="Nguồn khách hàng">
                        <select v-model="objRequest.source" class="form-control">
                            <option v-for="item in ListSource" :value="item.key">
                                {{item.value}}
                            </option>
                        </select>
                    </b-form-group>
                </div>
                <div class="col-md-6 col-xs-12">
                    <b-form-group label="Loại khách hàng">
                        <select v-model="objRequest.type" class="form-control">
                            <option v-for="item in ListType" :value="item.key">
                                {{item.value}}
                            </option>
                        </select>
                    </b-form-group>
                </div>
            </div>
            <b-form-group label="Địa chỉ">
                <b-form-input v-model="objRequest.address" type="text" required placeholder="Địa chỉ"></b-form-input>
            </b-form-group>
            <b-form-group label="Ghi chú">
                <b-form-textarea v-model="objRequest.note" :rows="3" :max-rows="4" type="text" required placeholder="Ghi chú"></b-form-textarea>
            </b-form-group>
            <button @click="CreateCustomer(objRequest)" class="mt-3 btn btn-success">Cập nhật</button>
        </b-modal>
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
            this.getAllTypeCustomer().then(respose => {
                this.ListType = respose;
            });

            this.getAllSourceCustomer().then(response => {
                try {
                    this.ListSource = response;
                }
                catch (ex) {

                }
            });

            this.objUser = this.getUser();

        },

        methods: {
            ...mapActions(["getCustomers", "updateStatusComment", "getAllTypeCustomer", "getAllSourceCustomer", "createCustomer", "removeComment"]),
            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getCustomers({
                    keyword: this.SearchKeyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    type: this.SearchType,
                    source: this.SearchSource,
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
            create() {
                this.objRequest = {};
                this.objRequest.type = 1;
                this.objRequest.source = 2;
                this.objRequest.gender = "male";
                this.$bvModal.show('edit');
            },
            CreateCustomer(item) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.createCustomer(item).then(response => {
                    //debugger-
                    if (response.Success == true) {
                        this.$toast.success(response.Message, {});
                        this.$bvModal.hide('edit');
                        this.pageIndex = 1;
                        this.onChangePaging();
                        this.isLoading = false;
                    } else {
                        this.$toast.error(response.Message, {});
                        this.isLoading = false;
                    }
                });
            },
            removeComment(item) {
                this.removeComment(item).then(response => {
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
                this.updateStatusComment(item)
                    .then(response => {
                        if (response.Success == true) {
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
        },
        computed: {
            ...mapGetters(["customers"])
        },
        mounted() {
            this.onChangePaging();

        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            SearchSource: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
            SearchType: function () {
                this.currentPage = 1;
                this.onChangePaging();
            },
        }
    };
</script>


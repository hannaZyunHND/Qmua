
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
                            <select v-model="SearchStatus" class="form-control">
                                <option v-for="item in ListStatus" :value="item.key">
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
                        <!--<router-link class="btn btn-success" :to="{ path: 'add' }"><i class="fa fa-plus"></i> Thêm mới</router-link>-->
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>

                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="comments.total"
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
                                    <th>Người tạo</th>
                                    <th>Nội dung</th>
                                    <th>Ngày tạo</th>
                                    <th>Url đường dẫn</th>
                                    <th class="text-center">Loại</th>
                                    <th class="text-center">Trạng thái</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="item in comments.listData">
                                    <tr role="row" class="odd">
                                        <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                        <td style="max-width:300px">
                                            <p>Tên: {{item.name}}</p>
                                            <p>Contact: {{item.phoneOrMail}}</p>
                                        </td>
                                        <td style="max-width:400px">
                                            <p>
                                                {{item.content}}
                                            </p>
                                        </td>
                                        <td v-html="format_date(item.createdDate)"></td>
                                        <td>
                                            <!--<a href="{{item.url}}">Xem</a>-->
                                            <a :href="item.url" target="_blank">Xem</a>
                                        </td>
                                        <td class="text-center">
                                            <p class="badge bg-success">{{item.type}}</p>
                                            <p vi-if="item.type==rating"><i v-for="n in item.rate" style="color:gold" class="fa fa-star" aria-hidden="true"></i></p>
                                        </td>

                                        <td class="text-center">
                                            <span v-if="item.status ==1" class="badge bg-info">Mới</span>
                                            <span v-if="item.status ==2" class="badge bg-success">Xuất bản</span>
                                            <span v-if="item.status ==3" class="badge bg-warning">Loại bỏ</span>
                                        </td>
                                        <td>
                                            <span v-b-tooltip.hover title="Xem tin trả lời" @click="toggeter(item.id)" class="action-edit">{{item.child.length}} <i class="fa fa-comment"> </i></span>
                                            <span v-if="item.status !=2" class="action-show"><a v-b-tooltip.hover title="Duyệt" @click="updateStatus(item,2)"><i style="color:green" class="fa fa-check-circle"></i></a></span>
                                            <span v-if="item.status !=3" class="action-hidden"><a v-b-tooltip.hover title="Không duyệt" @click="updateStatus(item,3)"><i style="color:gold" class="fa fa-circle-o"></i></a></span>
                                            <span v-b-tooltip.hover title="Trả lời" @click="edit(item)" class="action-edit"> <i class="fa fa-edit"> </i></span>
                                            <span class="action-delete"><a v-b-tooltip.hover title="Xóa comment" @click="RemoveComment(item)"><i style="color:red" class="fa fa-trash-o"></i></a></span>
                                        </td>

                                    </tr>
                                    <template v-for="itemChild in item.child">
                                        <tr :data-x-id="item.id" role="row" :class="['info','hidden']">
                                            <td class="dt-checkboxes-cell"></td>
                                            <td style="max-width:300px;padding-left:50px">
                                                <p>Tên: {{itemChild.name}}</p>
                                                <p>Contact: {{itemChild.phoneOrMail}}</p>
                                            </td>
                                            <td style="max-width:400px">
                                                <p>
                                                    {{itemChild.content}}
                                                </p>
                                            </td>
                                            <td v-html="format_date(itemChild.createdDate)"></td>
                                            <td class="text-center"><p class="badge bg-success">{{itemChild.type}}</p></td>
                                            <td class="text-center">
                                                <span v-if="itemChild.status ==1" class="badge bg-info">Mới</span>
                                                <span v-if="itemChild.status ==2" class="badge bg-success">Xuất bản</span>
                                                <span v-if="itemChild.status ==3" class="badge bg-warning">Loại bỏ</span>
                                            </td>
                                            <td>
                                                <span v-if="itemChild.status !=2" class="action-show"><a v-b-tooltip.hover title="Xuất bản" @click="updateStatus(itemChild,2)"><i style="color:green" class="fa fa-check-circle"></i></a></span>
                                                <span v-if="itemChild.status !=3" class="action-hidden"><a v-b-tooltip.hover title="Hạ sản phẩm" @click="updateStatus(itemChild,3)"><i style="color:gold" class="fa fa-circle-o"></i></a></span>
                                                <span class="action-delete"><a v-b-tooltip.hover title="Xóa comment" @click="RemoveComment(itemChild)"><i style="color:red" class="fa fa-trash-o"></i></a></span>
                                            </td>
                                        </tr>
                                    </template>
                                </template>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <b-modal id="edit" title="Trả lời tin nhắn" hide-footer>
            <b-form-group label="Tên hiển thị">
                <b-form-input v-model="objRequest.name" type="text" required placeholder="Họ tên hiển thị"></b-form-input>
            </b-form-group>
            <b-form-textarea :rows="3" :max-rows="6" v-model="objRequest.content"
                             placeholder="Trả lời tin "
                             required></b-form-textarea>
            <button @click="CreateComment(objRequest)" class="mt-3 btn btn-success">Trả lời tin nhắn</button>
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
                SearchStatus: 1,
                currentPage: 1,
                pageSize: 10,
                loading: true,

                objRequest: {

                },
                objUser: {

                },
                ListStatus: [],
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
            this.getAllTypeComment().then(respose => {
                this.ListType = respose.listData;
            });

            this.getAllStatusComment().then(response => {
                try {
                    this.ListStatus = response.listData;
                }
                catch (ex) {

                }
            });

            this.objUser = this.getUser();

        },

        methods: {
            ...mapActions(["getComments", "updateStatusComment", "getAllTypeComment", "getAllStatusComment", "createComment", "removeComment"]),
            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getComments({
                    keyword: this.SearchKeyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir,
                    type: this.SearchType,
                    status: this.SearchStatus
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
                this.objRequest = {};
                this.objRequest.id = 0;
                this.objRequest.parentId = obj.id;
                this.objRequest.createdByAdminId = 'admin';
                this.objRequest.objectType = obj.objectType;
                this.objRequest.objectId = obj.objectId;
                this.objRequest.name = this.objUser.fullName;
                this.objRequest.phoneOrMail = this.objUser.email;
                this.objRequest.type = "comment";
                this.objRequest.content = "";
                this.$bvModal.show('edit');
            },
            CreateComment(item) {
                //debugger-
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.createComment(item).then(response => {
                    //debugger-
                    if (response.Success == true) {
                        this.$toast.success(response.message, {});
                        this.$bvModal.hide('edit');
                        this.pageIndex = 1;
                        this.onChangePaging();
                        this.isLoading = false;
                    } else {
                        this.$toast.error(response.message, {});
                        this.isLoading = false;
                    }
                });
            },
            RemoveComment(item) {
                //debugger-
                this.removeComment(item).then(response => {
                    if (response.Success == true) {
                        this.$toast.success(response.Message, {});
                        this.pageIndex = 1;
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
            ...mapGetters(["comments"])
        },
        mounted() {
            this.onChangePaging();

        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            SearchStatus: function () {
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


<template>
    <div class="list-data">
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <div>
                <b-col md="12">
                    <b-row class="form-group">
                        <b-col md="4">
                            <b-form-input type="text" v-model="searchTitle" v-on:keyup.enter="onChangePaging()"
                                          placeholder="Tìm kiếm theo tên"></b-form-input>
                        </b-col>
                        <b-col md="2">
                            <b-button variant="outline-primary" @click="this.onChangePaging">Tìm kiếm</b-button>
                        </b-col>
                    </b-row>
                </b-col>
            </div>
        </b-card>
        <b-card>
        </b-card>
        <b-card>
            <div role="toolbar" aria-label="Toolbar with button groups and dropdown menu" class="mb-2">
                <div role="group" class="mx-1 btn-group">
                    <router-link :to="{ path: 'add' }">
                        <b-button class="btn btn-success" v-b-tooltip.hover title="Thêm mới nhóm người dùng">
                            <i class="fa fa-plus"></i> Thêm mới
                        </b-button>
                    </router-link>
                </div>
                <div class="mx-1 btn-group mi-paging">
                    <b-pagination class="pagination b-pagination pagination-md" v-model="currentPage"
                                  :total-rows="this.totalRow"
                                  :per-page="pageSize"></b-pagination>
                </div>
            </div>

            <div class="table-responsive">
                <table class="table table-centered data-thumb-view dataTable no-footer table-nowrap">
                    <thead class="thead-dark table table-centered table-nowrap">
                    <tr>
                        <th v-for="field in fields"
                            @click="field.sortable ? sortor(field.key) : null"
                            class="text-center"
                            style="max-width:150px"
                            scope="col">{{field.label}}
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr v-for="item in this.items">
                        <td class="text-center">{{item.name}}</td>
                        <td class="text-center">
                            <router-link :to="{path: 'edit/'+item.id}" v-if="item.name!='Admin'">
                                <span v-b-tooltip.hover title="Sửa nhóm người dùng">
                                    <i style="color:gold" class="fa fa-edit"></i>
                                </span>
                            </router-link>
                            <router-link :to="{path: 'assignPermissionRole/'+item.id}" v-if="item.name!='Admin'">
                                <span  v-b-tooltip.hover title="Thêm quyền nhóm người dùng">
                                    <i style="color:green" class="fa fa-users"></i>
                                </span>
                            </router-link>
                            <a  v-b-tooltip.hover title="Hạ nhóm người dùng"
                                      v-if="item.name!='Admin'"
                                      @click="remove(item.id)">
                                <i style="color:red" class="fa fa-trash-o"></i>
                            </a>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>

        </b-card>

    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import Loading from "vue-loading-overlay";
    import {mapActions} from "vuex";
    import Treeselect from '@riophae/vue-treeselect'
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";
    import msgNotify from "../../common/constant";
    import {authenticationRepository} from "../../repository/authentication/authenticationRepository";
    import functionsConstant from "../../constant/functionsConstant";
    import actionsConstant from "../../constant/actionsConstant";

    const fields = [
        {key: "Name", label: "Tên nhóm người dùng", sortable: true},
        {key: "Action", label: "Thao tác"}
    ];
    export default {
        name: "Role",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                fields,
                searchTitle: "",
                searchStatus: 0,
                searchType: 0,
                messager: "",
                currentSort: "Id",
                currrentSortDir: "asc",
                currentPage: 1,
                pageSize: 10,
                totalRow: null,
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
                },
                listRole: null,
                items: null,
                currentUser: null,
                permissionAdd: false,
                permissionDelete: false,
            };
        },
        methods: {
            ...mapActions(["getPageRole", "deleteRole"]),
            async onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.listRole = await this.getPageRole({
                    initial: initial,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    title: this.searchTitle,
                });
                console.log(this.listRole);
                this.totalRow = this.listRole.totalRow;
                this.items = this.listRole.items;
                this.isLoading = false;
            },
            remove(item) {
                if (confirm("Bạn có thực sự muốn xoá?")) {
                    this.deleteRole(item)
                        .then(response => {
                            console.log(response);
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.isLoading = false;
                                this.onChangePaging();
                            } else {
                                this.$toast.success(response.message, {});
                                this.isLoading = false;
                            }
                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        })
                }
            },
            async checkPermissionAdd() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.SYSTEM_ROLE + '_' + actionsConstant.CREATE;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionAdd = true
                }
            },
            async checkRole() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                if (this.currentUser.roles.includes("Admin")) {
                    this.permissionDelete = true;
                    this.permissionAdd = true;

                }
            }
        },
        watch: {
            currentPage: function (val) {
                this.onChangePaging();
            }
        },
        created() {
            this.onChangePaging();
        },
        components: {
            Treeselect
        }

    }
</script>

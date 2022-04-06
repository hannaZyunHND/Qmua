
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
                        <!--<b-col md="2">
                            <b-select :options="Language" v-model="SearchLanguageCode"></b-select>
                        </b-col>-->
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
                        <router-link class="btn btn-success" :to="{ path: 'add' }"><i class="fa fa-plus"></i> Thêm mới</router-link>
                        <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>
                </div>

                <div class="table-responsive">
                    <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                        <div class="clear"></div>
                        <table class="table data-thumb-view dataTable no-footer" role="grid">
                            <thead class="table table-centered table-nowrap">
                                <tr role="row">
                                    <th><!--<input type="checkbox">--></th>
                                    <th class="text-center">STT</th>
                                    <th class="text-center">Mã chính sách</th>
                                    <th class="text-center">Ngôn ngữ</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item,index) in banneradss">
                                    <td class="dt-checkboxes-cell"><input type="checkbox" class="dt-checkboxes"></td>
                                    <td class="text-center">{{index}}</td>
                                    <td class="text-center">
                                        {{item.key}}
                                    </td>
                                    <td class="text-center">
                                        <p>Ngôn ngữ: {{item.value}}</p>
                                    </td>
                                    <td>
                                        <b-row>
                                            <div style="padding:5px">
                                                <router-link :to="{path: 'edit/'+ item.key}">
                                                    <span class="action-edit"><i class="fa fa-edit"></i></span>
                                                </router-link>
                                            </div>
                                            <div style="padding:5px">
                                                <span class="action-delete"><a @click="remove(item)"><i style="color:red" class="fa fa-trash"></i></a></span>
                                            </div>
                                        </b-row>
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
        name: "policy",
        components: {
            Loading
        },
        data() {
            return {
                editedItem: null,
                editMode: false,
                isLoading: false,
                _product: {

                },
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",
                SearchKeyword: "",
                SearchLanguageCode: "vi-VN     ",
                SearchLocation: 0,
                currentPage: 1,
                pageSize: 10,
                loading: true,
                item: {},
                Language: [],
                Location: [],

            };
        },
        methods: {
            ...mapActions(["getBannerAdss", "removeBannerAds"]),
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getBannerAdss({
                    keyword: this.SearchKeyword,
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
            remove: function (item) {
                if (confirm("Bạn có thực sự muốn xoá?")) {
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    this.removeBannerAds(item.key)
                        .then(response => {
                            debugger
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
            saveData(item) {
                if (item.id > 0) {
                    this.updateDepartment(item)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.isLoading = false;
                                this.editedItem = null;
                            }
                            else {
                                this.$toast.error(response.message, {});
                                this.isLoading = false;
                            }

                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                            this.isLoading = false;
                        });
                }
            },
            editData(item) {
                this.beforEditCache = item
                this.editedItem = item
            }
        },
        computed: {
            ...mapGetters(["banneradss"])
        },
        mounted() {
            this.onChangePaging();


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
            SearchLocation: function () {
                this.currentPage = 1;
                this.onChangePaging();
            }
        }
    };
</script>
<style>
    [v-cloak] {
        display: none;
    }

    .edit {
        display: none;
    }

    .editing .edit {
        display: block
    }

    .editing .view {
        display: none;
    }
</style>


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
            <div role="toolbar" aria-label="Toolbar with button groups and dropdown menu" class="mb-2">
                <div role="group" class="mx-1 btn-group">
                    <router-link :to="{ path: 'add' }">
                        <b-button class="btn btn-success" v-b-tooltip.hover title="Thêm mới nhà cung cấp">
                            <i class="fa fa-plus"></i> Thêm mới
                        </b-button>
                    </router-link>
                    <button type="button" class="btn btn-danger"><i class="fa fa-trash-o"></i> Xóa</button>
                </div>
                <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                    <b-dropdown-item>Kích hoạt</b-dropdown-item>
                    <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                </b-dropdown>
                <div class="mx-1 btn-group mi-paging">
                    <b-pagination class="pagination b-pagination pagination-md" v-model="currentPage"
                                  :total-rows="this.totalRow"
                                  :per-page="pageSize"></b-pagination>
                </div>
            </div>
            <div class="table-responsive">
                <table class="table table-centered table-nowrap data-thumb-view dataTable no-footer">
                    <thead class="thead-dark table table-centered table-nowrap">
                        <tr>
                            <th v-for="field in fields"
                                @click="field.sortable ? sortor(field.key) : null"
                                class="text-center"
                                style="max-width:150px"
                                scope="col">
                                {{field.label}}
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,index) in this.items">
                            <td><b-form-checkbox></b-form-checkbox></td>
                            <td class="text-center">
                                {{index+1}}
                            </td>
                            <td class="text-center"> <img style="width:50px;height:60px" class="img-thumbnail" :src="pathImgs(item.avatar)" alt="Ảnh lỗi"></td>

                            <td class="text-center">{{item.name}}</td>
                            <td class="text-center">
                                <p>Ngôn ngữ: {{item.langCount}}</p>
                            </td>
                            <td class="text-center">
                                <router-link :to="{path: 'edit/'+item.id}">
                                    <a v-b-tooltip.hover title="Sửa nhà cung cấp">
                                        <i style="color:gold" class="fa fa-edit"></i>
                                    </a>
                                </router-link>
                                <a v-b-tooltip.hover title="Hạ nhà cung cấp" @click="remove(item.id)">
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
    import Treeselect from '@riophae/vue-treeselect'
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'
    import { unflatten, pathImg } from "../../plugins/helper";
    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";
    import msgNotify from "../../common/constant";

    const fields = [
        { key: "Check", label: "" },
        { key: "Id", label: "STT" },
        { key: "Avatar", label: "Hình ảnh" },
        { key: "Title", label: "Tên nhà cung cấp", sortable: true },
        { key: "Language", label: "Ngôn ngữ", sortable: true },
        { key: "Action", label: "Thao tác" }
    ];
    export default {
        name: "Manufacturers",
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
                listManufacturer: null,
                items: null
            };
        },
        methods: {
            async onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.listManufacturer = await manufacturersRepository.getPageManufacturers({
                    initial: initial,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    title: this.searchTitle,
                });
                console.log(this.listManufacturer);
                this.totalRow = this.listManufacturer.totalRow;
                this.items = this.listManufacturer.items;
                this.isLoading = false;
            },
            pathImgs(path) {
                return pathImg(path);
            },
            remove(item) {
                if (confirm("Bạn có thực sự muốn xoá?")) {
                    manufacturersRepository.deleteManufacturers(item)
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

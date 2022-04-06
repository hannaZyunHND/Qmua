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
                            <b-form-input v-model="keyword" type="text" v-on:keyup.enter="onChangePaging()" placeholder="Tìm kiếm theo tên"></b-form-input>
                        </b-col>
                        <b-col md="2">
                            <b-form-select id="basicSelect"
                                           :plain="true"
                                           :options="['Chọn danh mục','Option 1', 'Option 2', 'Option 3']"
                                           value="Chọn danh mục"></b-form-select>
                        </b-col>
                        <b-col md="2">
                            <b-btn v-b-toggle.collapse1 variant="primary">
                                <i class="fa fa-angle-double-down" aria-hidden="true"></i>
                            </b-btn>
                        </b-col>
                    </b-row>
                </b-col>
                <b-collapse id="collapse1" class="mt-2">
                    <b-card>
                        <p class="card-text">Collapse contents Here</p>
                        <b-btn v-b-toggle.collapse1_inner size="sm">Toggle Inner Collapse</b-btn>
                        <b-collapse id="collapse1_inner" class="mt-2">
                            <b-card>Hello!</b-card>
                        </b-collapse>
                    </b-card>
                </b-collapse>
            </div>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class="mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <router-link class="btn btn-success" :to="{ path: 'add' }">
                            <i class="fa fa-plus"></i> Thêm mới
                        </router-link>
                        <button type="button" class="btn btn-danger">
                            <i class="fa fa-trash-o"></i> Xóa
                        </button>
                    </div>
                    <b-dropdown class="mx-1" variant="info" right text="Hành động" icon>
                        <b-dropdown-item>Kích hoạt</b-dropdown-item>
                        <b-dropdown-item>Không kích hoạt</b-dropdown-item>
                    </b-dropdown>

                    <div class="mx-1 btn-group mi-paging">
                        <b-pagination v-model="currentPage"
                                      :total-rows="adss.Total"
                                      :per-page="pageSize"
                                      aria-controls="_ads"></b-pagination>
                    </div>
                </div>
                <table class="table">
                    <thead class="thead-dark table table-centered table-nowrap">
                        <tr>
                            <th v-for="field in fields"
                                @click="field.sortable ? sortor(field.key) : null"
                                class="text-center"
                                style="max-width:150px"
                                scope="col">{{field.label}}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in adss.ListData">
                            <td class="text-center" scope="row">{{item.Id}}</td>
                            <td class="text-center">{{item.Name}}</td>
                            <td class="text-center">{{item.Type}}</td>
                            <td class="text-center">{{item.Position}}</td>
                            <td class="text-center" style="max-width:150px"><a :href="item.Url" :title="item.Url">Link</a></td>
                            <td class="text-center">{{item.SortOrder}}</td>
                            <td class="text-center" style="max-width:150px"><img style="width:80px" :src="item.Thumb" class="thumbnail" /></td>
                            <td class="text-center">{{item.IsEnable}}</td>
                            <td class="text-center">
                                <router-link :to="{path: 'edit/'+item.Id}" class="btn btn-warning">
                                    <i class="fa fa-edit"></i>
                                </router-link>
                                <button class="btn btn-xs btn-danger" @click="remove(item)">
                                    <i class="fa fa-minus-circle"></i>
                                </button>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
       
        </div>
    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    const fields = [
        { key: "Id", label: "Mã" },
        { key: "Name", label: "Tên", sortable: true },
        { key: "Type", label: "Loại", sortable: true },
        { key: "Position", label: "Vị trí" },
        { key: "Url", label: "URL" },
        { key: "SortOrder", label: "Sắp xếp", sortable: true },
        { key: "Thumb", label: "Thumb" },
        { key: "IsEnable", label: "Kích Hoạt" },
        { key: "Is", label: "Thao tác" }
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
                keyword:'',
                _ads: {
                    Id: 0,
                    Name: "",
                    Content: "",
                    Type: 0,
                    Position: 0,
                    Url: "",
                    SortOrder: 0,
                    IsEnable: 0,
                    Thumb: ""
                },
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
            ...mapActions(["getAdss", "removeAds"]),

            

            onKeyUp: function () {

            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getAdss({
                    initial: initial,
                    keyword: this.keyword,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    sortBy: this.currentSort,
                    sortDir: this.currentSortDir
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
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.removeAds(item)
                    .then(response => {
                        if (response.Success == true) {
                            this.$toast.success(response.Message, {});
                            this.isLoading = false;
                            this.onChangePaging();
                        } else {
                            this.$toast.success(response.Message, {});
                            this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    });
            }
        },
        computed: {
            ...mapGetters(["adss"])
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

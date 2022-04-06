<template>
    <div>
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <!--<loading :active.sync="isLoading"
                     :height="35"
                     :width="35"
                     :color="color"
                     :is-full-page="fullPage"></loading>-->
            <!--<div>
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
            </div>-->
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class="mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <router-link class="btn btn-success" :to="{ path: 'add' }">
                            <i class="fa fa-plus"></i> Thêm mới
                        </router-link>

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

                            <tr v-for="item in data.listData">
                                <td class="text-left">{{item.name}}</td>
                                <td class="text-center" v-html="format_date(item.createdDate)">{{item.createdDate}}</td>
                                <td class="text-center">{{item.ratio}}</td>
                                <td class="text-center">{{item.enable?'Hiển thị':'Đóng'}}</td>
                                <td class="text-center">
                                    <router-link :to="{path: 'edit/'+item.id}" class="btn btn-warning">
                                        <i class="fa fa-edit"></i>
                                    </router-link>
                                    <button class="btn btn-xs btn-danger" @click="remove(item.id)">
                                        <i class="fa fa-minus-circle"></i>
                                    </button>
                                </td>
                            </tr>

                        </tbody>
                    </table>
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
    const fields = [
        { key: "name", label: "Tên", sortable: true },
        { key: "createdDate", label: "Ngày tạo", sortable: true },
        { key: "ratio", label: "Tỉ lệ" },
        { key: "enable", label: "Kích Hoạt" },
        { key: "value", label: "Chức năng" }
    ];

    export default {
        name: "spin",
        components: {
            Loading
        },
        data() {
            return {
                isLoading: false,
                fields,
                keyword: '',
            
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
            ...mapActions(["getAll", "removeSpin"]),

            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },


            sortor: function (s) {
                if (s === this.currentSort) {
                    this.currentSortDir = this.currentSortDir === "asc" ? "desc" : "asc";
                }
                this.currentSort = s;
                
            },
            remove: function (id) {
                let $this = this;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.removeSpin(id)
                    .then(response => {
                        if (response.success) {
                           
                            this.$toast.success(response.message, {});
                            this.isLoading = false;
                            $this.$router.push('/admin/luckyspin/list');
                           
                        } else {
                            this.$toast.success(response.message, {});
                            this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    });
            }
        },
        computed: {
            ...mapGetters(["data"])
        },
        mounted() {

        },
        created() {
            this.getAll()
        },
        watch: {
            //currentPage: function (newVal) {
            //    this.currentPage = newVal;
            //    this.onChangePaging();
            //}
        }
    };
</script>

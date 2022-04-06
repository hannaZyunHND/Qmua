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
                        <b-col md="4">
                            <!--<p>Xin mời bạn lựa chọn trạng thái</p>-->
                            <treeselect :options="StatusOptions"
                                        :disable-branch-nodes="true"
                                        v-model="searchStatus"
                                        :default-expanded-level="Infinity"
                                        
                                        placeholder="Xin mời bạn lựa chọn trạng thái" />
                        </b-col>
                        <b-col md="4">
                            <!--<p>Xin mời bạn chọn loại bài viết</p>-->
                            <treeselect :options="TypeOptions"
                                        :disable-branch-nodes="true"
                                        v-model="searchType"
                                        :default-expanded-level="Infinity"
                                        placeholder="Xin mời bạn lựa chọn loại bài viết" />
                        </b-col>
                    </b-row>
                </b-col>
                <div class="pt-1">
                    <b-col md="12">
                        <b-row class="form-group">
                            <b-col md="7">
                                <treeselect :options="ZoneOptions"
                                            placeholder="Xin mời bạn lựa chọn danh mục"
                                            v-model="ZoneValues"
                                            :value-consists-of="valueConsistsOf"
                                            :multiple="true"
                                            :default-expanded-level="Infinity" />

                            </b-col>
                            <b-col md="2" style="padding-top:5px">
                                <input type="radio" value="ALL" v-model="valueConsistsOf"><label>Tất cả</label>
                                <input type="radio" value="BRANCH_PRIORITY" v-model="valueConsistsOf"><label>Chỉ một</label>
                            </b-col>
                            <b-col md="3">
                                <b-button class="btn btn-success float-right" @click="ExportExcel()"
                                          title="Thống kê lượt xem">
                                    <i class="fa fa-plus"></i> Thống kê lượt xem
                                </b-button>
                                <b-button-toolbar class="float-right">
                                    <!-- @change="onChangeYear()"-->
                                    <b-form-select v-model="yearselected" :options="Years"></b-form-select>
                                </b-button-toolbar>
                                <!--<b-button-toolbar class="float-right">
                                    <b-form-select v-model="monthselected" :options="Months" @change="onChangeMonth()"></b-form-select>
                                </b-button-toolbar>-->

                            </b-col>
                        </b-row>
                    </b-col>
                </div>
            </div>
        </b-card>
        <hr>
        <b-card>
            <div role="toolbar" aria-label="Toolbar with button groups and dropdown menu" class="mb-2">
                <div role="group" class="mx-1 btn-group">
                    <router-link  target="_blank" :to="{ path: 'add' }" v-if="this.permissionAdd">
                        <b-button class="btn btn-success" v-b-tooltip.hover
                                  title="Thêm mới bài viết">
                            <i class="fa fa-plus"></i> Thêm mới
                        </b-button>
                    </router-link>
                </div>
                <div class="mx-1 btn-group mi-paging">
                    <b-pagination class="pagination b-pagination pagination-md" v-model="currentPage"
                                  :total-rows="articless.total"
                                  :per-page="pageSize"></b-pagination>
                    <div class="mx-1" style="padding-top:5px">Số lượng : {{articless.total}}</div>
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

                        <tr v-for="item in articless.listData">
                            <td class="text-center" scope="row">
                                <b-form-checkbox>{{item.id}}</b-form-checkbox>
                            </td>
                            <td style="max-width:200px">{{item.name}}</td>

                            <td class="text-left" v-html="item.category"></td>

                            <td class="text-center" v-html="format_date(item.createdDate)"></td>
                            <td class="text-center" width="250px">
                                <b-button :id="`popover-1-${item.id}`" variant="primary"><p><i class="fa fa-eye"></i> Ngôn ngữ: {{item.lang}}</p></b-button>
                                <b-popover :target="`popover-1-${item.id}`"
                                           placement="top"
                                           title="Xem trước"
                                           variant="danger"
                                           triggers="click">
                                    <p v-for="urlLang in item.baseUrl">
                                        <a target="_blank" :href="urlLang.value"> Link: {{urlLang.key}}</a>
                                    </p>
                                </b-popover>
                            </td>
                            <td class="text-center">
                                <span v-if="item.status ==1" class="badge bg-warning">Chưa xuất bản</span>
                                <span v-if="item.status ==2" class="badge bg-success">Xuất bản</span>
                                <span v-if="item.status ==3" class="badge bg-danger">Đã xóa</span>
                                <p>Lượt xem: {{item.viewCount}}</p>
                            </td>
                            <td class="text-center">
                                <router-link :to="{path: 'edit/'+item.id}" v-if="permissionUpdate">
                                    <i style="color:gold" class="fa fa-edit"></i>
                                </router-link>
                                <a v-if="permissionUnPublish && item.status==2" v-b-tooltip.hover
                                   title="Hạ bài viết"
                                   @click="unpublish(item.id)">
                                    <i style="color:gold" class="fa fa-circle-o"></i>
                                </a>
                                <a v-if="permissionPublish && item.status==1" v-b-tooltip.hover
                                   title="Đăng bài viết"
                                   @click="publish(item.id)">
                                    <i style="color:green" class="fa fa-check-circle"></i>
                                </a>
                                <a v-if="permissionDelete" v-b-tooltip.hover
                                   title="Xóa bài viết"
                                   @click="deleteArticles(item.id)">
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
    import msgNotify from "./../../common/constant";
    import { mapActions, mapGetters } from "vuex";
    import Loading from "vue-loading-overlay";
    import Treeselect from '@riophae/vue-treeselect'
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'
    import { authenticationRepository } from "../../repository/authentication/authenticationRepository";
    import functionsConstant from "../../constant/functionsConstant";
    import actionsConstant from "../../constant/actionsConstant";
    import moment from 'moment'
    import DateRangePicker from 'vue2-daterange-picker'
    const DateNow = new Date();
    //import 'vue2-daterange-picker/dist/vue2-daterange-picker.css'




    const fields =
        [
            { key: "Check", label: "" },
            { key: "Name", label: "Tên bài viết" },

            { key: "CategoryName", label: "Tên danh mục" },
           
            { key: "CreatedDate", label: "Thời gian tạo" },
            { key: "CountLanguage", label: "Ngôn ngữ" },
            { key: "Status", label: "Trạng thái" },
            { key: "Action", label: "Thao tác" }
        ];
    export default {
        name: "Articles",
        components: {
            Loading,
            'date-range-picker' : DateRangePicker
        },
        data() {
            return {
                
                valueConsistsOf: "BRANCH_PRIORITY",
                isLoading: false,
                fields,
                searchTitle: "",
                searchStatus: 0,
                searchType: 0,
                StatusOptions: [],
                TypeOptions: [],
                ZoneValues: [],
                ZoneOptions: [],
                _Articles: {
                    Id: 0,
                    Title: "",
                    Description: "",
                    Body: "",
                    Avatar: "",
                    Author: "",
                    Status: "",
                    Source: "",
                    Type: 0,
                    CreatedDate: "",
                    LastModifiedDate: "",
                    DistributionDate: "",
                    CreatedBy: "",
                    LastModifiedBy: "",
                    PublishedBy: "",
                    WordCount: "",
                    ViewCount: "",
                    LikeCount: 0,
                    VoteCount: 0,
                    IsAllowComment: false,
                    Url: "",
                    SocialTitle: "",
                    SocialDescription: "",
                    SocialImage: "",
                    LanguageCode: "",
                    ZoneId: 0,
                    Zones: [],
                },
                currentUser: null,
                permissionAdd: false,
                permissionUpdate: false,
                permissionPublish: false,
                permissionUnPublish: false,
                permissionDelete: false,
                messager: "",
                currentSort: "Id",
                currrentSortDir: "asc",
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
                },
                Months: [
                    { value: "1", text: "Tháng 1" },
                    { value: "2", text: "Tháng 2" },
                    { value: "3", text: "Tháng 3" },
                    { value: "4", text: "Tháng 4" },
                    { value: "5", text: "Tháng 5" },
                    { value: "6", text: "Tháng 6" },
                    { value: "7", text: "Tháng 7" },
                    { value: "8", text: "Tháng 8" },
                    { value: "9", text: "Tháng 9" },
                    { value: "10", text: "Tháng 10" },
                    { value: "11", text: "Tháng 11" },
                    { value: "12", text: "Tháng 12" }
                ],
                Years: [
                ],
                monthselected: DateNow.getMonth() + 1,
                yearselected: DateNow.getFullYear(),
                dateRange: {
                    startDate: '2019-12-26',
                    endDate: '2019-12-28',
                },

            };
        },
        methods: {
            ...mapActions(["getArticle", "getPageArticle", "publishArticle", "unpublishArticle", "deleteArticle", "getAllStatusOption", "getZoneArticle", "getAllTypeOption",'ExportExcelViewCount']),

            format_date(value) {
                if (value) {
                    return moment(String(value)).format('DD/MM/YYYY HH:mm')
                }
            },
            dateFormat(classes, date) {
                if (!classes.disabled) {
                    classes.disabled = date.getTime() < new Date()
                }
                return classes
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getPageArticle({
                    initial: initial,
                    keyword: this.searchTitle,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    status: this.searchStatus,
                    title: this.searchTitle,
                    type: this.searchType,

                    zoneIds: this.ZoneValues
                });
                this.isLoading = false;
            },
            deleteArticles(item) {
                if (confirm("Bạn có thực sự muốn xóa bài viết?")) {
                    this.deleteArticle(item)
                        .then(response => {
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
            unpublish(item) {
                if (confirm("Bạn có thực sự muốn hạ bài viết?")) {
                    this.unpublishArticle(item)
                        .then(response => {
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
            publish(item) {
                if (confirm("Bạn có thực sự muốn đăng bài viết?")) {
                    this.publishArticle(item)
                        .then(response => {
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
            ExportExcel() {
                this.isLoading = true;
                var protocol = location.protocol;
                var slashes = protocol.concat("//");
                var host = slashes.concat(window.location.hostname);
                var port = location.port;
                if (port != 0 && port !== "") {
                    host = host.concat(":").concat(port);
                    console.log(host);
                }
                //let initial = this.$route.query.initial;
                //initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.ExportExcelViewCount({
                    //initial: initial,
                    keyword: this.searchTitle,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    status: this.searchStatus,
                    title: this.searchTitle,
                    type: this.searchType,
                    year: this.yearselected,
                    zoneIds: this.ZoneValues
                }).then(response => {
                    console.log(response.data);
                    window.open(host + '/' + response.data, "_blank");
                });
                this.isLoading = false;
            },
            async getAllStatusOptions() {
                this.StatusOptions = await this.getAllStatusOption();
            },
            async getZoneArticles() {
                this.ZoneOptions = await this.getZoneArticle();

                try {
                    this.ZoneOptions.push({
                        id: 0,
                        label: "Chọn danh mục"
                    });
                }
                catch (ex) {
                    console.log(ex);
                }

            },
            async getAllTypeOptions() {
                this.TypeOptions = await this.getAllTypeOption();
            },
            async checkPermissionAdd() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.ARTICLE + '_' + actionsConstant.CREATE;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionAdd = true
                }
            },

            async checkPermissionUpdate() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.ARTICLE + '_' + actionsConstant.UPDATE;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionUpdate = true
                }
            },
            async checkPermissionPublish() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.ARTICLE + '_' + actionsConstant.PUBLISH;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionPublish = true
                }
            },
            async checkPermissionUnPublish() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.ARTICLE + '_' + actionsConstant.UNPUBLISH;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionUnPublish = true
                }
            },
            async checkPermissionDelete() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                let permissions = functionsConstant.ARTICLE + '_' + actionsConstant.DELETE;
                if (this.currentUser.permissions.includes(permissions)) {
                    this.permissionDelete = true
                }
            },
            async checkRole() {
                this.currentUser = await authenticationRepository.getCurrentUser();
                if (this.currentUser.roles.includes("Admin")) {
                    this.permissionDelete = true;
                    this.permissionUnPublish = true;
                    this.permissionPublish = true;
                    this.permissionAdd = true;
                    this.permissionUpdate = true;
                }
            }

        },
        watch: {
            currentPage: function (val) {
                this.onChangePaging();
            },
            ZoneValues: function (val) {
                this.currentPage = 1;
                this.onChangePaging();
            },
            searchType: function (val) {
                this.currentPage = 1;
                this.onChangePaging();
            },
            searchStatus: function (val) {
                this.currentPage = 1;
                this.onChangePaging();
            }
            
        },
        computed: {
            ...mapGetters(["articless"])
        },
        mounted() {
            //this.onLoadPage();
            let yNow = DateNow.getFullYear();
            for (let index = 2019; index <= yNow; index++) {
                let item = {};
                item.value = index;
                item.text = index;
                this.Years.push(item);
            }
        },
        async created() {
            await this.getZoneArticles();
            await this.getAllStatusOptions();
            await this.getAllTypeOptions();
            await this.checkPermissionAdd();
            await this.checkPermissionUpdate();
            await this.checkPermissionPublish();
            await this.checkPermissionUnPublish();
            await this.checkPermissionDelete();
            await this.checkRole();
            this.onChangePaging();

        },
        components: {
            Treeselect
        }

    }
</script>

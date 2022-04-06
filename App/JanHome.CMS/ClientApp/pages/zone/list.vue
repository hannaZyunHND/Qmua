<template>
    <div>
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <b-col md="12">
                <b-row class="form-group">
                    <b-col md="4">
                        <b-form-input v-model="keyword" v-on:keyup.enter="onChangePaging()" type="text" placeholder="Tìm kiếm theo tên"></b-form-input>
                    </b-col>
                    <!--<b-col md="2">
                        <b-select :options="Language" v-model="SearchLanguageCode"></b-select>
                    </b-col>-->
                    <b-col md="2">
                        <b-select :options="Types" v-model="SearchTypes"></b-select>
                    </b-col>
                    <b-col md="2">
                        <b-select :options="Status" v-model="SearchStatus"></b-select>
                    </b-col>
                </b-row>
            </b-col>
        </b-card>
        <div class="card card-data">
            <div class="card-body">
                <div role="toolbar" class="mb-2" aria-label="Toolbar with button groups and dropdown menu">
                    <div role="group" class="mx-1 btn-group">
                        <router-link class="btn btn-success" :to="{ path: 'add' }" target='_blank'>
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
                </div>
            </div>
            <vue-ads-table :columns="columns"
                           :rows="unflatten(rows)">
                <template slot="lang" slot-scope="props">
                    <div align="center">
                        <p>Ngôn ngữ: {{props.row.lang}}</p>
                    </div>
                </template>
                <template slot="sortOrder" slot-scope="props">
                    <div align="center">
                        <input style="width:100px" type="number" class="form-control text-center" v-model="props.row.sortOrder" />
                    </div>
                </template>
                <template slot="isShowHome" slot-scope="props">
                    <div align="center">
                        <span v-if="props.row.isShowHome == true" class="badge bg-success"> Trang chủ</span>
                        <span v-if="props.row.isShowSearch == true" class="badge bg-success"> Gợi ý Search</span>
                        <span v-if="props.row.isShowMenu == true" class="badge bg-success"> Menu</span>
                    </div>
                </template>
                <template slot="action" slot-scope="props">
                    <div class="text-center action-item">
                        <a v-if="props.row.isShowHome == false" v-b-tooltip.hover title="Hiển thị trang chủ" @click="showLayout(props.row)">
                            <i style="color:blue" class="fa fa-home"></i>
                        </a>
                        <a v-if="props.row.isShowHome == true" v-b-tooltip.hover title="Hạ hiển thị trang chủ" @click="showLayout(props.row)">
                            <i style="color:blue" class="fa fa-exclamation-circle"></i>
                        </a>
                        <a v-b-tooltip.hover title="Hiển thị gợi ý Seach" @click="showSearch(props.row)">
                            <i style="color:blue" class="fa fa-search"></i>
                        </a>
                        <router-link v-b-tooltip.hover title="Sửa bài viết" target='_blank' :to="{path: 'edit/'+ props.row.id}">
                            <i style="color:gold" class="fa fa-edit"></i>
                        </router-link>
                        <a v-b-tooltip.hover title="Xóa bài viêt" @click="remove(props.row)">
                            <i style="color:red" class="fa fa-minus-circle"></i>
                        </a>
                        <a v-b-tooltip.hover title="Cập nhật vị trí" @click="updateSort(props.row)">
                            <i style="color:forestgreen" class="fa fa-save"></i>
                        </a>
                    </div>

                </template>
            </vue-ads-table>

        </div>

    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import VueAdsTable from '../../components/Table';
    import { unflatten2 } from "../../plugins/helper";
    let columns = [
        //{
        //    property: 'id',
        //    title: 'Mã danh mục',
        //    direction: null,
        //    filterable: true,
        //},
        {
            property: 'name',
            title: 'Tên danh mục',
            direction: null,
            filterable: true,
        },
        {
            property: 'lang',
            title: 'Ngôn ngữ',
            direction: null,
            filterable: true,
        },
        {
            property: 'sortOrder',
            title: 'Sắp xếp',
            direction: null,
            filterable: true,
        },
        {
            property: 'isShowHome',
            title: 'Hiển thị',
            direction: null,
            filterable: true,
        },
        {
            property: 'action',
            title: 'Thao tác',
            direction: null,
            filterable: false,
        },
    ];

    //const fields = [
    //    { key: "Id", label: "Id" },
    //    { key: "Name", label: "Name", sortable: true },
    //    { key: "Avatar", label: "Avatar" },
    //    { key: "Background", label: "Background" },
    //    { key: "Type", label: "Type" },
    //    { key: "Banner", label: "Banner" },
    //    { key: "BannerLink", label: "BannerLink" },
    //    { key: "LanguageCode", label: "LanguageCode" },
    //    { key: "SortOrder", label: "SortOrder", sortable: true },
    //    { key: "Is", label: "Thao tác" }
    //];
    let classes = {
        group: {
            'vue-ads-font-bold': true,
            'vue-ads-border-b': true,
            'vue-ads-italic': true,
        },
        '0/all': {
            'vue-ads-py-3': true,
            'vue-ads-px-2': true,
        },
        'even/': {
            'vue-ads-bg-blue-lighter': true,
        },
        'odd/': {
            'vue-ads-bg-blue-lightest': true,
        },
        '0/': {
            'vue-ads-bg-blue-lighter': false,
            'vue-ads-bg-blue-dark': true,
            'vue-ads-text-white': true,
            'vue-ads-font-bold': true,
        },
        '1_/': {
            'hover:vue-ads-bg-red-lighter': true,
        },
        '1_/0': {
            'leftAlign': true
        }
    };
    export default {
        name: "location",
        components: {
            Loading,
            VueAdsTable,
        },
        data() {
            return {
                columns,
                classes,
                filter: '',
                start: 0,
                end: 500,
                rows: [],

                isLoading: false,
                LeftSelect: 0,
                ListLocation: [
                    {
                        id: 1,
                        name: "Hà Nội",
                        price: 0,
                        salePrice: 0,
                    }, {
                        id: 2,
                        name: "TP Hồ Chí Minh",
                        price: 0,
                        salePrice: 0,
                    },
                    {
                        id: 3,
                        name: "Thanh Hóa",
                        price: 0,
                        salePrice: 0,
                    },
                    {
                        id: 4,
                        name: "Hà Tĩnh",
                        price: 0,
                        salePrice: 0,
                    }

                ],
                ListValue: [
                    {
                        id: 1,
                        name: "Hà Nội",
                        price: 0,
                        salePrice: 0,
                    }, {
                        id: 2,
                        name: "TP Hồ Chí Minh",
                        price: 0,
                        salePrice: 0,
                    },
                ],

                SearchTypes: 0,
                SearchStatus: 0,
                SearchLanguageCode: "vi-VN     ",
                messeger: "",
                currentSort: "Id",
                currentSortDir: "asc",
                keyword: "",
                currentPage: 1,
                pageSize: 10,
                loading: true,

                Language: [

                ],
                Types: [

                ],
                Status: [

                ],

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
            ...mapActions(["getZones", "getAllZone", "deleteZone", "getAllLanguages", "supportsZone", "updateSortZone", "updateShowLayout", "updateShowSearch"]),
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getAllZone({
                    tuKhoa: this.keyword || "",
                    languageCode: this.SearchLanguageCode || "vi-VN",
                    type: this.SearchTypes,
                    status: this.SearchStatus
                }).then(respose => {
                    this.rows = respose.listData;
                });
                this.isLoading = false;
            },
            showLayout(item) {
                this.updateShowLayout(item)
                    .then(response => {
                        if (response.success == true) {
                            this.onChangePaging();
                            this.$toast.success(response.message, {});
                            //this.isLoading = false;
                        } else {
                            this.$toast.error(response.message, {});
                            //this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(response.message + ". Error:" + e, {});
                    });
            },
            showSearch(item) {
                this.updateShowSearch(item)
                    .then(response => {
                        if (response.success == true) {
                            this.onChangePaging();
                            this.$toast.success(response.message, {});
                            //this.isLoading = false;
                        } else {
                            this.$toast.error(response.message, {});
                            //this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(response.message + ". Error:" + e, {});
                    });
            },
            changeValue: function (id) {
                var obj = this.ListLocation.filter(word => word.id === id);
                console.log(this.ListValue);
                this.ListValue.push({
                    id: obj[0].id,
                    name: obj[0].name,
                    price: 0,
                    salePrice: 0,
                });

                this.ListLocation.splice(obj, 1);
            },
            remove: function (item) {
                //var vm = this;
                if (confirm("Bạn có thực sự muốn xoá?")) {
                    this.deleteZone(item)
                        .then(response => {
                            if (response.success == true) {
                                this.onChangePaging();
                                this.$toast.success(response.message, {});
                                //this.isLoading = false;

                            } else {
                                this.$toast.error(response.message, {});
                                //this.isLoading = false;
                            }
                        })
                        .catch(e => {
                            this.$toast.error(response.message + ". Error:" + e, {});
                        });

                    //vm.$confirm('Confirm Title', 'Confirm Message', function (e) {
                    //    //debugger-

                    //}, function () {

                    //})
                }
            },
            updateSort: function (item) {
                //var vm = this;

                this.updateSortZone(item)
                    .then(response => {
                        if (response.success == true) {
                            //this.onChangePaging();
                            this.$toast.success(response.message, {});
                            ////this.isLoading = false;

                        } else {
                            this.$toast.error(response.message, {});
                            //this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(response.message + ". Error:" + e, {});
                    });

                //vm.$confirm('Confirm Title', 'Confirm Message', function (e) {
                //    //debugger-

                //}, function () {

                //})

            },
            unflatten: function (arr) {
                var tree = [],
                    mappedArr = {},
                    arrElem,
                    mappedElem;
                // First map the nodes of the array to an object -> create a hash table.
                for (var i = 0, len = arr.length; i < len; i++) {
                    arrElem = arr[i];
                    mappedArr[arrElem.id] = arrElem;
                    mappedArr[arrElem.id]['_children'] = [];
                }
                for (var id in mappedArr) {
                    if (mappedArr.hasOwnProperty(id)) {
                        mappedElem = mappedArr[id];
                        // If the element is not at the root level, add it to its parent array of children.
                        if (mappedElem.parentId) {
                            try {
                                mappedArr[mappedElem['parentId']]['_children'].push(mappedElem);
                            }
                            catch (ex) {
                                console.log(ex);
                            }
                        }
                        // If the element is at the root level, add it to first level elements array.
                        else {
                            tree.push(mappedElem);
                        }
                    }
                }
                return tree;
            },
        },
        computed: {
            ...mapGetters(["zones"])
        },
        mounted() {
            this.supportsZone().then(respose => {
                this.Types = respose.listTypes.map(function (item) {
                    return {
                        value: item.key,
                        text: item.value
                    }
                });
                this.Status = respose.listStatus.map(function (item) {
                    return {
                        value: item.key,
                        text: item.value
                    }
                });
            });
            this.onChangePaging();
        },
        watch: {
            SearchLanguageCode: function () {
                this.onChangePaging();
            },
            SearchTypes: function () {
                this.onChangePaging();
            },
            SearchStatus: function () {
                this.onChangePaging();
            },
        }
    };
</script>


<style>
    .vue-ads-cursor-pointer i {
        color: green;
        padding-right: 15px;
    }

    th .vue-ads-cursor-pointer i {
        color: green;
        padding-left: 15px;
    }

    th .vue-ads-flex {
        text-align: center;
    }

    td vue-ads-text-sm {
        text-align: center;
    }

    .vue-ads-p-2 {
        padding-right: 50px;
    }

    .vue-ads-cursor-pointer .action-item i {
        color: green;
        padding-right: 2px;
    }
</style>

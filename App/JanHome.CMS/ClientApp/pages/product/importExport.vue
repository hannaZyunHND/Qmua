
<template>
    <div class="productadd">

        <b-card header-tag="header" class="card-filter"
                footer-tag="footer">
            <b-col md="2">
                <b-select :options="Language" v-model="SearchLanguageCode"></b-select>
            </b-col>
        </b-card>
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <b-row>
                <b-col md="2">
                    Import Excel Giá theo tỉnh thành
                </b-col>
                <b-col md="10">
                    <b-form-file id="file-field-productlocation" size="sm"
                                 v-on:change="uploadExcelPriceLocation($event)"></b-form-file>
                </b-col>
            </b-row>
            <b-row>
                <b-col md="2">
                    Import Excel Chi tiết kỹ thuật
                </b-col>
                <b-col md="10">
                    <b-form-file id="file-field-productspectification" size="sm"
                                 v-on:change="uploadExcelSpectification($event)"></b-form-file>
                </b-col>
            </b-row>
        </b-card>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>
</template>
<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    import FileManager from './../../components/fileManager/list'
    import EventBus from "./../../common/eventBus";
    import InputTag from 'vue-input-tag'
    import { unflatten, slug, pathImg } from "../../plugins/helper";

    export default {
        name: "configs",
        components: {
            Loading,
            FileManager
        },
        data() {
            return {
                isLoading: false,
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                mikey1: 'mikey1',
                vmkey: "",
                vmindex: 0,
                SearchLanguageCode: "vi-VN     ",
                messeger: "",
                currentSort: "ConfigName",
                currentSortDir: "asc",

                ListConfigs: [],
                Language: [],
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
            ...mapActions(["getConfigs", "deleteConfig", "getAllLanguages", "editConfigInLanguages", "fmFileUpload", "fmFileUploadExcel","fmFileUploadExcel_Spectification"]),
            openImg(key, index) {

                this.vmkey = key;
                this.vmindex = index;
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
            DoAttackFile(value) {
                let vm = this;
                vm.ListConfigs[vm.vmkey][vm.vmindex].content = value[0].path;
            },

            pathImgs(path) {
                return pathImg(path);
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getConfigs({
                    languageCode: this.SearchLanguageCode || "vi-VN",
                }).then(response => {
                    var data = response.listData;
                    this.ListConfigs = data.reduce((r, a) => {
                        r[a.page] = [...r[a.page] || [], a];
                        return r;
                    }, {});
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
                this.deleteConfig(item)
                    .then(response => {
                        if (response.success == true) {
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
            },
            updateByKey(key) {
                try {
                    let lstValue = this.ListConfigs[key];
                    if (lstValue != null && lstValue != undefined && lstValue.length > 0) {
                        var slt = lstValue.map(x => {
                            return {
                                content: x.content,
                                languageCode: x.languageCode,
                                configName: x.configName
                            }
                        });
                        this.editConfigInLanguages(slt).then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.isLoading = false;
                            }
                            else {
                                this.$toast.error(response.message, {});
                                this.isLoading = false;
                            }
                        });
                    } else {
                        this.$toast.error("Không tìm thấy dữ liệu", {});
                    }
                }
                catch (ex) {
                    this.$toast.error("Không tìm thấy dữ liệu", {});
                }
            },

            updatePreview(e, key, index) {
                var reader, files = e.target.files;
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("files" + x, files[x]);
                }
                this.fmFileUpload(data).then(response => {
                    if (response.success == true) {
                        this.ListConfigs[key][index].content = response.data[0].path;
                        this.$toast.success("Thành công", {});
                    } else {
                        this.$toast.error(msgNotify.error + ". Error:", {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:", {});
                    this.isLoading = false;
                });
            },

            //uploadExcelSpectification
            uploadExcelPriceLocation(e) {
               
                this.isLoading = true;
                var reader, files = e.target.files;
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("files", files[x]);
                }
             
                this.fmFileUploadExcel(data).then(response => {
                    if (response.success == true) {
                        this.$toast.success("Thành công", {})
                        this.isLoading = false;
                    } else {
                        this.$toast.error(msgNotify.error + ". Error:", {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:", {});
                    this.isLoading = false;
                });
            },
            uploadExcelSpectification(e) {

                this.isLoading = true;
                var reader, files = e.target.files;
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("files", files[x]);
                }

                this.fmFileUploadExcel_Spectification(data).then(response => {
                    if (response.success == true) {
                        this.$toast.success("Thành công", {})
                        this.isLoading = false;
                    } else {
                        this.$toast.error(msgNotify.error + ". Error:", {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:", {});
                    this.isLoading = false;
                });
            },
        },

        created() {
            let vm = this;
            EventBus.$on('FileSelected', value => {

                vm.ListConfigs[vm.vmkey][vm.vmindex].content = value[0].path;

            });
            this.getAllLanguages().then(respose => {
                this.Language = respose.listData.map(function (item) {
                    return {
                        value: item.languageCode,
                        text: item.name
                    };
                });
            });
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        computed: {
            ...mapGetters(["configs"])
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
                this.onChangePaging();
            },
        }
    };
</script>
<style>
    .productadd .nav.nav-pills .nav-item .nav-link {
        padding: 10px 15px !important
    }

    @media (min-width: 768px) {
        .form-horizontal .control-label {
            text-align: right;
            margin-bottom: 0;
            padding-top: 7px;
        }
    }

    .ace-file-input .ace-file-container {
        display: block;
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        height: 30px;
        background-color: #fff;
        border: 1px solid #d5d5d5;
        cursor: pointer;
        -webkit-box-shadow: none;
        box-shadow: none;
        -webkit-transition: all .15s;
        -o-transition: all .15s;
        transition: all .15s;
    }
</style>

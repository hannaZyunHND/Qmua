
<template>
    <div class="productadd">

        <b-card header-tag="header" class="card-filter"
                footer-tag="footer">
            <b-col md="2">
                <b-select :options="Language" v-model="SearchLanguageCode"></b-select>
            </b-col>
        </b-card>
        <b-card header-tag="header" class="card-filter" footer-tag="footer">
            <form class="form-horizontal">
                <b-tabs class="col-md-12" pills>
                    <b-tab v-for="group,page in ListConfigs" :title="page">
                        <div style="display:flex;width:100%" v-for="(item,index) in group">
                            <div class="form-group" style="width:100%;display:flex">
                                <label class="col-md-2 col-sm-2 control-label"> {{item.configLabel}} </label>
                                <div v-if="item.configValueType == 1" class="col-md-8 col-sm-8">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <input type="text" :value="item.content" disabled autocomplete="off" class="form-control" placeholder="Chọn hình ảnh">
                                                <span @click="openImg(page,index)" class="input-group-addon bg-primary" style="cursor: pointer;width:45px"><i style="padding-top:10px;padding-left:15px" class="fa fa-picture-o"></i></span>
                                            </div>
                                        </div>
                                        <div @click="openImg(page,index)" class="col-md-4 gallery-upload-file ui-sortable">
                                            <div style="padding:0;margin:0;width:auto;height:auto" class="col-md-12 col-xs-12  r-queue-item ui-sortable-handle">
                                                <div v-if="item.content != null && item.content != undefined &&  item.content.length > 0">
                                                    <img alt="Ảnh lỗi" style="max-height:100px;width:100%" :src="pathImgs(item.content)" class="preview-image img-thumbnail">
                                                </div>
                                                <div v-else>
                                                    <i class="fa fa-picture-o"></i>
                                                    <p>[Chọn ảnh]</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="item.configValueType == 2" class="col-md-8 col-sm-8">
                                    <input placeholder="Nhập thông tin" type="text" class="col-xs-10 col-sm-12 form-control" v-model="item.content"
                                           aria-invalid="false">
                                </div>
                                <div v-if="item.configValueType ==  3" class="col-md-8 col-sm-8">
                                    <ckeditor tag-name="textarea"
                                              v-model="item.content" :rows="3" :config="editorConfig">
                                    </ckeditor>
                                </div>
                                <div v-if="item.configValueType ==  4" class="col-md-8 col-sm-8">
                                    <textarea placeholder="Nhập thông tin" :rows="3" :max-rows="6" type="text" class="col-xs-10 col-sm-12 form-control" v-model="item.content"></textarea>
                                </div>
                                <div v-if="item.configValueType ==  5" class="col-md-3 col-sm-3">
                                    <input placeholder="Nhập thông tin kiểu number" type="number" class="col-xs-10 col-sm-12 form-control" v-model="item.content">
                                </div>
                                <div v-if="item.configValueType ==  6" class="col-md-8 col-sm-8">
                                    <b-form-file id="file-field" size="sm"
                                                 v-on:change="updatePreview($event,page,index)"></b-form-file>
                                    <a target="_blank" v-if="item.content != null &&  item.content.length > 0" :href="/uploads/+ item.content"><i class="fa fa-file-o"></i> Xem file mới up lên</a>
                                </div>
                                <div v-if="item.configValueType ==  7" class="col-md-4 col-sm-8">
                                    <input placeholder="Nhập thông tin ngày tháng" type="time" class="col-xs-10 col-sm-12 form-control" v-model="item.content">
                                </div>
                            </div>
                        </div>
                        <div style="display:flex;width:100%">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-6">
                                <button type="button" class="btn btn-success" @click="updateByKey(page)">Lưu thông tin</button>
                            </div>
                        </div>
                    </b-tab>
                </b-tabs>
            </form>
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
            ...mapActions(["getConfigs", "deleteConfig", "getAllLanguages", "editConfigInLanguages", "fmFileUpload_2"]),
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
                this.fmFileUpload_2(data).then(response => {
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
    .productadd .nav.nav-pills .nav-item .nav-link { padding:10px 15px !important }

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

<template>

    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>

        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Nhập thông tin nhà cung cấp" active>
                <div class="row productedit">
                    <div class="col-md-12">
                        <b-card class="mt-3 " header="Thêm / Sửa nhà cung cấp">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-5 col-xs-12">
                                        <b-form-group label="Tên nhà cung cấp">
                                            <b-form-input v-model="objRequest.url" placeholder="Tên nhà cung cấp"
                                                          v-on:keyup.13="slugM"
                                                          required></b-form-input>
                                        </b-form-group>

                                        <b-form-group label="Hình đại diện">
                                            <a @click="openImg('avatar')">
                                                <div style="width:30%;display:flex" class="gallery-upload-file ui-sortable">
                                                    <div style="width:100%;height:auto;margin:0" class=" r-queue-item ui-sortable-handle">
                                                        <div style="width:100%" v-if="objRequest.avatar != null && objRequest.avatar != undefined &&  objRequest.avatar.length > 0">
                                                            <img alt="Ảnh lỗi" style="height:auto;width:100%" :src="pathImgs(objRequest.avatar)" class="preview-image img-thumbnail-full">
                                                        </div>
                                                        <div v-else>
                                                            <i class="fa fa-picture-o"></i>
                                                            <p>[Chọn ảnh]</p>
                                                        </div>
                                                    </div>

                                                </div>
                                            </a>

                                        </b-form-group>
                                    </div>
                                    <div class="col-md-7">
                                        <div class="col-md-8">
                                            <b-form-group label="Chọn ngôn ngữ">
                                                <treeselect :options="LanguageCodes"
                                                            :disable-branch-nodes="true"
                                                            @select="getSelectedLanguge"
                                                            v-model="LanguageValues"
                                                            :default-expanded-level="Infinity"
                                                            :disabled="disabled"
                                                            placeholder="Xin mời bạn lựa chọn ngôn ngữ" />

                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Tên theo ngôn ngữ">
                                                <b-form-input v-model="objRequest.name" placeholder="Tên nhà cung cấp"
                                                              v-on:keyup.13="slugM"
                                                              required></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Đường dẫn">
                                                <b-form-input v-model="objRequest.urlInLanguage" placeholder="Url"
                                                              required></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="mt-3">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <button class="btn btn-info btn-submit-form col-md-12 btncus" type="button"
                                                                @click="DoAddEdit()">
                                                            <i class="fa fa-save"></i> Cập nhật
                                                        </button>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <button class="btn btn-success col-md-12 btncus" type="button"
                                                                @click="DoRefesh()">
                                                            <i class="fa fa-refresh"></i> Làm mới
                                                        </button>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </b-form>
                        </b-card>
                    </div>

                </div>
            </b-tab>

        </b-tabs>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>


</template>


<script>
    import axios from 'axios';
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    // import 'vue-select/dist/vue-select.css';
    //import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";
    import productSpecificationTemplateRepository
        from "../../repository/productSpecificationTemplate/productSpecificationTemplateRepository";
    import { unflatten, slug, pathImg } from "../../plugins/helper";

    import FileManager from './../../components/fileManager/list'
    import EventBus from "./../../common/eventBus";
    // Import component

    export default {
        name: "articleaddedit",
        data() {
            return {
                mikey1: 'mikey1',

                //editor: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                disabled: false,
                selectedFile: null,
                preview: '/assets/img/unnamed.jpg',
                previewImageFacebook: '/assets/img/unnamed.jpg',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {
                    Id: 0,
                    urlInLanguage: "",
                    name: "",
                    avatar: ""
                },
                LanguageCodes: [],
                LanguageValues: null,
                listManufacturersInLanguage: null,
            };
        },
        async created() {

            let vm = this;
            EventBus.$on('FileSelected', value => {
                if (this.choseImg == "avatar") {
                    vm.objRequest.avatar = value[0].path;
                }
            });
            this.LanguageCodes = await productSpecificationTemplateRepository.getAllLanguageOption();
            this.GetById();
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        components: {
            Loading,
            Treeselect,
            FileManager,
        },

        mounted() {
        },

        computed: {
            ...mapGetters(["article", "isOR", "fileName"]),
            slugM: function () {
                //debugger-
                if (this.objRequest != null && this.objRequest != undefined) {
                    this.objRequest.urlInLanguage = slug(this.objRequest.name);
                }

            }
        },
        watch: {
            article: function (val) {
                this.objRequest = this.article
            }
        },
        methods: {
            ...mapActions(["updateArticle", "addArticle", "getArticle", "uploadFile"]),
            pathImgs(path) {
                return pathImg(path);
            },
            async  openImg(img) {
                this.choseImg = img;

                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file

            },

            DoAttackFile(value) {
                let vm = this;
                if (this.choseImg == "avatar") {
                    vm.objRequest.avatar = value[0].path;
                }
            },

            async GetById() {
                if (this.$route.params.id > 0) {
                    var id = this.$route.params.id;
                    console.log(id);
                    this.isLoading = true;
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    var response = await manufacturersRepository.getManufacturersById(id);
                    console.log(response);
                    this.objRequest.Id = response.manufacturers.id;
                    this.objRequest.url = response.manufacturers.name;
                    if (response.manufacturers.avatar != null && response.manufacturers.avatar != "") {
                        this.preview = response.manufacturers.avatar;
                        this.objRequest.avatar = response.manufacturers.avatar;
                    }
                    var listManufacturersInLanguage = response.manufacturersInLanguages;
                    this.listManufacturersInLanguage = listManufacturersInLanguage;
                    for (var i = 0; i < listManufacturersInLanguage.length; i++) {
                        this.LanguageValues = listManufacturersInLanguage[0].languageCode;
                        this.objRequest.urlInLanguage = listManufacturersInLanguage[0].url;
                        this.objRequest.name = listManufacturersInLanguage[0].name;
                        this.objRequest.LanguageCode = listManufacturersInLanguage[0].languageCode;
                    }
                    this.isLoading = false;
                }
            },
            async DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.Id > 0) {
                    var result = await manufacturersRepository.updateManufacturers(this.objRequest);
                    console.log(result);
                    if (result.success == true) {
                        this.$toast.success("tạo thành công", {});
                        this.isLoading = false;
                        this.$router.go(-1)
                    } else {
                        this.$router.go(-1);
                        this.$toast.error("cập nhật thất bại", {});
                        this.isLoading = false;
                    }
                } else {
                    var result = await manufacturersRepository.addManufacturers(this.objRequest);
                    console.log(result);
                    if (result.success == true) {
                        this.$toast.success("tạo thành công", {});
                        this.isLoading = false;
                        this.$router.go(-1)
                    } else {
                        this.$router.go(-1);
                        this.$toast.error("cập nhật thất bại", {});
                        this.isLoading = false;
                    }
                }
            },
            DoRefesh() {
                this.objRequest.Title = ""
            },
            openUpload() {
                document.getElementById('file-field').click()
            },
            updatePreview(e) {
                var reader, files = e.target.files;
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }
                this.uploadFile(data).then(response => {
                    if (response.success == true) {
                        reader.onload = (e) => {
                            this.preview = e.target.result;
                            console.log(this.selectedFile);
                            this.objRequest.avatar = response.linkImage;
                        };
                        reader.readAsDataURL(files[0])
                    } else {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    this.isLoading = false;
                });
            },
            getSelectedUser(node, id) {
                this.objRequest.ZoneId = node.id;
            },
            getSelectedLanguge(node, id) {
                this.LanguageValues = node.id;
                this.objRequest.LanguageCode = node.id;
                if (this.listManufacturersInLanguage != null) {
                    for (var i = 0; i < this.listManufacturersInLanguage.length; i++) {
                        if (node.id == this.listManufacturersInLanguage[i].languageCode) {
                            this.LanguageValues = this.listManufacturersInLanguage[i].languageCode;
                            this.objRequest.urlInLanguage = this.listManufacturersInLanguage[i].url;
                            this.objRequest.name = this.listManufacturersInLanguage[i].name;
                            this.objRequest.LanguageCode = this.listManufacturersInLanguage[i].languageCode;
                            break;
                        }
                        else {
                            this.objRequest.urlInLanguage = "";
                            this.objRequest.name = "";
                            this.objRequest.LanguageCode = node.id;
                        }
                    }
                }
                console.log(this.LanguageValues)
            },
            onChangeList: function ({ source, destination }) {
                this.ProductOptionsSource = source;
                this.ProductOptionsDestination = destination;
            }
        }
    };
</script>

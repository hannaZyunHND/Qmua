<template>

    <div style="display:flex;width:100%">
        <div class="row productedit">
            <div class="col-md-8">
                <b-card class="mt-3 " header="Thêm / Sửa nhà cung cấp">
                    <b-form class="form-horizontal">
                        <div class="row">

                            <div class="col-md-6">
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
                                <b-form-group label="Chọn danh mục">
                                    <treeselect :multiple="true"
                                                :flat="true"
                                                :options="ZoneOptions"
                                                placeholder="Xin mời bạn lựa chọn danh mục"
                                                @select="getSelectedUser"
                                                v-model="ZoneValues"
                                                :default-expanded-level="Infinity" />
                                </b-form-group>
                            </div>
                            <div class="col-md-12">
                                <b-form-group label="Tên thông số kỹ thuật">
                                    <b-form-input v-model="objRequest.name"
                                                  placeholder="Tên thông số kỹ thuật"
                                                  required></b-form-input>

                                </b-form-group>
                            </div>


                            <div class="col-md-4">
                                <b-form-group label="Đơn vị">
                                    <b-form-input v-model="objRequest.unit" placeholder="Đơn vị"
                                                  required></b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-8">
                                <b-form-group label="Url">
                                    <b-form-input v-model="objRequest.url" placeholder="Url"
                                                  ></b-form-input>
                                </b-form-group>

                            </div>
                        </div>
                    </b-form>
                </b-card>

            </div>
            <div class="col-md-4">
                <b-card class="mt-3 " header="Thêm / Sửa nhà cung cấp">
                    <div class="row">
                        <div class="col-md-6">
                            <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                    type="submit"
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
                    </div> <div class="row" style="padding-top:20px">
                        <div class="col-md-12">
                            <b-form-checkbox v-model="objRequest.isForAllProduct">
                                Hiển thị tất cả sản phẩm trong danh mục
                            </b-form-checkbox>
                        </div>
                        <div class="col-md-12">
                            <b-form-checkbox v-model="objRequest.isFilter">
                                Cho phép tìm kiếm
                            </b-form-checkbox>
                        </div>
                    </div>
                </b-card>
            </div>
        </div>
    </div>
</template>


<script>
    import axios from 'axios';
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapActions, mapGetters } from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import 'vue-select/dist/vue-select.css';
    //import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
    import productSpecificationTemplateRepository
        from "../../repository/productSpecificationTemplate/productSpecificationTemplateRepository";
    // Import component
    import { unflatten, slug } from "../../plugins/helper";

    export default {
        name: "ProductSpecificationTemplateAddEdit",
        data() {
            return {
                //editor: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                disabled: false,
                selectedFile: null,
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {
                    Id: 0
                },
                LanguageCodes: [],
                LanguageValues: null,
                listProductSpecificationTemplateInLanguage: null,
                ZoneValues: null,
                ZoneOptions: [],
            };
        },
        async created() {
            this.LanguageCodes = await productSpecificationTemplateRepository.getAllLanguageOption();
            await this.getZones(1).then(response => {
                try {
                    var data = response.listData;
                    data.push({ id: 0, label: "Chọn danh mục", parentId: 0 });
                    this.ZoneOptions = unflatten(data);
                } catch (ex) {

                }
            })
            // this.ZoneOptions = await productSpecificationTemplateRepository.getZoneArticle();
            this.GetById();
        },
        components: {
            Loading,
            Treeselect
        },

        mounted() {
        },

        computed: {
            ...mapGetters(["article", "isOR", "fileName"])
        },
        watch: {
            article: function (val) {
                this.objRequest = this.article
            }
        },
        methods: {
            ...mapActions(["updateArticle", "addArticle", "getArticle", "uploadFile", "getZones"]),
            async GetById() {
                if (this.$route.params.id > 0) {
                    var id = this.$route.params.id;
                    console.log(id);
                    this.isLoading = true;
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    var response = await productSpecificationTemplateRepository.getProductSpecificationTemplateById(id);
                    console.log(response);
                    this.ZoneValues = Object.values(response.listZoneIds);
                    this.objRequest.Id = response.productSpecificationTemplate.id;
                    this.listProductSpecificationTemplateInLanguage = response.productSpecificationTemplateInLanguage;
                    for (var i = 0; i < this.listProductSpecificationTemplateInLanguage.length; i++) {
                        this.LanguageValues = this.listProductSpecificationTemplateInLanguage[0].languageCode;
                        this.objRequest.unit = this.listProductSpecificationTemplateInLanguage[0].unit;
                        this.objRequest.name = this.listProductSpecificationTemplateInLanguage[0].name;
                        this.objRequest.LanguageCode = this.listProductSpecificationTemplateInLanguage[0].languageCode;
                        this.objRequest.url = this.listProductSpecificationTemplateInLanguage[0].url;
                    }
                    this.objRequest.isForAllProduct = response.productSpecificationTemplates.isForAllProduct;
                    this.objRequest.isFilter = response.productSpecificationTemplates.isFilter;
                    this.isLoading = false;
                }
            },
            async DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.Id > 0) {
                    console.log(this.ZoneValues.toString());
                    this.objRequest.ZoneIds = this.ZoneValues.toString();
                    var result = await productSpecificationTemplateRepository.updateProductSpecificationTemplate(this.objRequest);
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
                    if (this.ZoneValues != null) {
                        console.log(this.ZoneValues.toString());
                        this.objRequest.ZoneIds = this.ZoneValues.toString();
                    }
                    var result = await productSpecificationTemplateRepository.addProductSpecificationTemplate(this.objRequest);
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
            getSelectedUser(node, id) {
                this.objRequest.ZoneId = node.id;
            },
            DoRefesh() {
                this.objRequest.Title = ""
            },
            getSelectedUser(node, id) {
                this.objRequest.ZoneId = node.id;
            },
            getSelectedLanguge(node, id) {
                this.LanguageValues = node.id;
                this.objRequest.LanguageCode = node.id;
                if (this.listProductSpecificationTemplateInLanguage != null) {
                    for (var i = 0; i < this.listProductSpecificationTemplateInLanguage.length; i++) {
                        if (node.id == this.listProductSpecificationTemplateInLanguage[i].languageCode) {
                            this.LanguageValues = this.listProductSpecificationTemplateInLanguage[i].languageCode;
                            this.objRequest.name = this.listProductSpecificationTemplateInLanguage[i].name;
                            this.objRequest.unit = this.listProductSpecificationTemplateInLanguage[i].unit;
                            this.objRequest.LanguageCode = this.listProductSpecificationTemplateInLanguage[i].languageCode;
                            break;
                        } else {
                            this.objRequest.name = "";
                            this.objRequest.unit = "";
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

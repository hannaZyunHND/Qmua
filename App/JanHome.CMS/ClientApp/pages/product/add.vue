<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <b-tabs class="col-md-12" pills vertical>
            <b-tab title="1. Thông tin chung" active>
                <div class="row productedit">
                    <div class="col-sm-6 col-md-8">
                        <div class="card">
                            <div class="card-header">
                                Thông tin chính
                            </div>
                            <div class="card-body">
                                <b-form class="form-horizontal">
                                    <b-form-group label="Tiêu đề" label-for="input-1">
                                        <b-form-input id="input-1"
                                                      v-model="objRequest.title"
                                                      type="text"
                                                      required
                                                      placeholder="Tiêu đề"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mã sản phẩm">
                                        <b-form-input v-model="objRequest.code" placeholder="Mã sản phẩm" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Đường dẫn">
                                        <b-form-input v-model="objRequest.url" placeholder="Đường dẫn" required></b-form-input>
                                    </b-form-group>
                                </b-form>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                Giá và các phiên bản sản phẩm
                            </div>
                            <div class="card-body">
                                <b-row>
                                    <b-col>
                                        <b-form-group label="Giá">
                                            <b-form-input v-model="objRequest.price" typeof="number" placeholder="Giá" required></b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col>
                                        <b-form-group label="Giá bán">
                                            <b-form-input v-model="objRequest.piscountPrice" typeof="number" placeholder="Giá bán" required></b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-form-group label="Bảo hành">
                                    <b-form-input v-model="objRequest.warranty" placeholder="Bảo hành" required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Thông tin khuyến mại">
                                    <b-form-input v-model="objRequest.promotionInfo" placeholder="Thông tin khuyến mại" required></b-form-input>
                                </b-form-group>
                            </div>
                        </div>


                        <div class="card">
                            <div class="card-header">
                                SEO Analysis
                                <b-col md="2">
                                    <b-btn v-b-toggle.collapseSEO variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                </b-col>
                            </div>
                            <b-collapse id="collapseSEO" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Tiêu đề SEO">
                                        <b-form-input v-model="objRequest.metaTitle" placeholder="Tiêu đề SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Từ khóa SEO">
                                        <b-form-input v-model="objRequest.metaKeyword" placeholder="Từ khóa SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <b-form-input v-model="objRequest.metaDescription" placeholder="Mô tả" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </b-collapse>
                        </div>

                        <div class="card">
                            <div class="card-header">
                                Social Share
                                <b-col md="2">
                                    <b-btn v-b-toggle.collapseSocial variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                </b-col>
                            </div>
                            <b-collapse id="collapseSocial" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Ảnh mô tả">
                                        <b-form-input v-model="objRequest.socialImage" placeholder="Ảnh mô tả" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Tiêu đề">
                                        <b-form-input v-model="objRequest.socialTitle" placeholder="Tiêu đề" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <b-form-input v-model="objRequest.socialDescription" placeholder="Mô tả" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </b-collapse>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="card">
                            <div class="card-header">
                                Đăng bài
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEdit()">
                                            <i class="fa fa-save"></i> Cập nhật
                                        </button>
                                    </div>
                                    <div class="col-md-6">
                                        <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                            <i class="fa fa-refresh"></i> Làm mới
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header">
                                Phân loại sản phẩm
                            </div>
                            <div class="card-body">
                                <b-form-group label="Nhà cung cấp">
                                    <v-select v-model="objRequest.manufacturerId" :options="ManufacturerIds" :reduce="x=>x.id" label="url"></v-select>
                                </b-form-group>

                                <b-form-group label="Đơn vị">
                                    <b-form-input v-model="objRequest.unit" placeholder="Đơn vị" required type="number"></b-form-input>
                                </b-form-group>

                                <b-form-group label="Số lượng">
                                    <b-form-input v-model="objRequest.quantity" placeholder="Số lượng" required type="number"></b-form-input>
                                </b-form-group>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                Ảnh
                                <b-col md="2">
                                    <b-btn v-b-toggle.collapseAnh variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                </b-col>
                            </div>
                            <b-collapse id="collapseAnh" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Ảnh đại diện">
                                        <b-form-group>
                                            <b-form-file v-model="objRequest.avatar" id="file-field" size="sm" v-on:change="updatePreview" placeholder="Ảnh đại diện"></b-form-file>

                                            <img style="margin-top:10px" :src="preview" class="preview-image img-thumbnail" v-on:click="openUpload">
                                        </b-form-group>
                                    </b-form-group>
                                    <b-form-group label="Danh sách ảnh chi tiết">
                                        <b-form-group>
                                            <b-form-file v-model="objRequest.avatarArray" id="files-field" size="sm" v-on:change="updatePreviews" placeholder="Danh sách ảnh chi tiết"></b-form-file>

                                            <img style="margin-top:10px" :src="previews" class="preview-image img-thumbnail" v-on:click="openUploads">
                                        </b-form-group>
                                    </b-form-group>
                                </div>
                            </b-collapse>
                        </div>
                    </div>
                </div>
            </b-tab>
            <b-tab title="2. Thông tin bổ sung">
                <div class="row productedit">
                    <div class="col-sm-6 col-md-8">
                        <div class="card">
                            <div class="card-header">
                                Thông tin bổ sung
                            </div>
                            <div class="card-body">
                                <b-form class="form-horizontal">
                                    <b-row>
                                        <b-col>
                                            <b-form-group label="Ngôn ngữ">
                                                <v-select c v-model="objRequestDetail.languageCode" :options="LanguageCodes" :reduce="x => x.langugeCode" label="name"></v-select>
                                            </b-form-group>
                                        </b-col>
                                        <b-col></b-col>
                                        <b-col></b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <b-form-group>
                                                <label class="typo__label">Chọn tag</label>
                                                <multiselect v-model="ListTags" tag-placeholder="Thêm tags" placeholder="Tìm và thêm tag" label="name" track-by="id" :options="Tags" :multiple="true" :taggable="true"></multiselect>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-form-group label="Tiêu đề" label-for="input-1">
                                        <b-form-input id="input-1"
                                                      v-model="objRequestDetail.title"
                                                      type="text"
                                                      required
                                                      placeholder="Tiêu đề"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mã sản phẩm">
                                        <b-form-input v-model="objRequestDetail.code" placeholder="Mã sản phẩm" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <textarea class="col-md-12 form-control" :editor="editor" v-model="objRequestDetail.description" :config="editorConfig" placeholder="Mô tả"></textarea>
                                    </b-form-group>
                                    <b-form-group label="Nội dung">
                                        <textarea class="col-md-12 form-control" :editor="editor1" v-model="objRequestDetail.content" :config="editorConfig" placeholder="Nội dung"></textarea>
                                    </b-form-group>
                                    <b-form-group label="Đường dẫn">
                                        <b-form-input v-model="objRequestDetail.url" placeholder="Đường dẫn" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Thông tin khuyến mại">
                                        <b-form-input v-model="objRequestDetail.promotionInfo" placeholder="Thông tin khuyến mại" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mục lục">
                                        <b-form-input v-model="objRequestDetail.catalog" placeholder="Mục lục" required></b-form-input>
                                    </b-form-group>

                                </b-form>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header">
                                SEO Analysis
                                <b-col md="2">
                                    <b-btn v-b-toggle.collapseSEO variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                </b-col>
                            </div>
                            <b-collapse id="collapseSEO" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Tiêu đề SEO">
                                        <b-form-input v-model="objRequestDetail.metaTitle" placeholder="Tiêu đề SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Từ khóa SEO">
                                        <b-form-input v-model="objRequestDetail.metaKeyword" placeholder="Từ khóa SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <b-form-input v-model="objRequestDetail.metaDescription" placeholder="Mô tả" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </b-collapse>
                        </div>

                        <div class="card">
                            <div class="card-header">
                                Social Share
                                <b-col md="2">
                                    <b-btn v-b-toggle.collapseSocial variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                </b-col>
                            </div>
                            <b-collapse id="collapseSocial" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Ảnh mô tả">
                                        <b-form-input v-model="objRequestDetail.socialImage" placeholder="Ảnh mô tả" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Tiêu đề">
                                        <b-form-input v-model="objRequestDetail.socialTitle" placeholder="Tiêu đề" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <b-form-input v-model="objRequestDetail.socialDescription" placeholder="Mô tả" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </b-collapse>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="card">
                            <div class="card-header">
                                Thao tác
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddDetail()">
                                            <i class="fa fa-save"></i> Cập nhật
                                        </button>
                                    </div>
                                    <div class="col-md-6">
                                        <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                            <i class="fa fa-refresh"></i> Làm mới
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </b-tab>
        </b-tabs>
    </div>

</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
 
    // Import component

    export default {
        name: "productaddedit",
        components: {
    
        },
        data() {
            return {
                isLoading: false,
                fullPage: false,
                preview: '/assets/img/unnamed.jpg',
                previews: '/assets/img/unnamed.jpg',
                color: "#007bff",
                editor: ClassicEditor,
                editor1: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                objRequest: {
                    Id: 0,
                    Code: '',
                    Status: 1,
                    Url: '',
                    Avatar: '',
                    AvatarArray: '',
                    Price: 0,
                    DiscountPrice: 0,
                    Warranty: '',
                    MetaTitle: '',
                    MetaKeyword: '',
                    MetaDescription: '',
                    SocialImage: '',
                    SocialTitle: '',
                    SocialDescription: '',
                    ManufacturerId: 0,
                    Unit: '',
                    Quantity: '',
                    PropertyId: 0
                },
                objRequestDetail: {
                    Title: '',
                    Description: '',
                    Content: '',
                    ViewCount: '',
                    Url: '',
                    PromotionInfo: '',
                    MetaTitle: '',
                    MetaKeyword: '',
                    MetaDescription: '',
                    SocialImage: '',
                    SocialTitle: '',
                    SocialDescription: '',
                    LanguageCode: '',
                    Catalog: '',
                    ProductId: 0
                },
                ListTags: [],
                Tags: [

                ],
                LanguageCodes: [

                ],
                ManufacturerIds: [

                ]
            };
        },
        created: {},
        components: {
            Loading
        },

        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProduct(this.$route.params.id).then(respose => {
                    this.objRequest = respose.data;
                });
                this.isLoading = false;
            }

            this.getAllLanguages().then(respose => {
                this.LanguageCodes = respose.listData;

            });
            this.getTagAll().then(respose => {
                this.Tags = respose.listData;

            });
            this.getAllManufacturers().then(response => {
                this.ManufacturerIds = response.listData;
            });
        },

        computed: {
            ...mapGetters(["product"], ["fileName"]),
        },

        methods: {
            ...mapActions(["updateProduct",
                "addProduct",
                "getProduct",
                "uploadFile",
                "getAllManufacturers",
                "getTagAll",
                "getAllLanguages",
                "addProductInLanguage"
            ]),
            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.Id > 0) {
                    this.updateProduct(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.isLoading = false;
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
                } else {
                    let objProduct = {};
                    objProduct = this.objRequest;
                    //let tagsData = [];
                    //let lstTags = this.ListTags;
                    //for (let i = 0; i < lstTags.length; i++) {
                    //    let datatag = {};
                    //    datatag.TagId = lstTags[i].id;
                    //    datatag.TagMode = 0;
                    //    tagsData.push(datatag);
                    //}
                    //objProduct.TagInProduct = tagsData;
                    this.addProduct(objProduct)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequestDetail.ProductId = response.data.productID;
                                this.isLoading = false;
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
            DoAddDetail() {
                let check = this.objRequestDetail;
                //debugger-;
                this.addProductInLanguage(this.objRequestDetail)
                    .then(response => {
                        if (response.success == true) {
                            this.$toast.success(response.message, {});
                            this.isLoading = false;
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
            },
            openUpload() {
                document.getElementById('file-field').click()
            },
            updatePreview(e) {
                var reader, files = e.target.files
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
                    if (response.Success == true) {
                        reader.onload = (e) => {
                            this.preview = e.target.result;
                            this.objRequest.Avatar = response.data[0].name;
                            console.log(this.selectedFile);
                        }
                        reader.readAsDataURL(files[0])
                    }
                    else {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    this.isLoading = false;
                });

            },
            openUploads() {
                document.getElementById('files-field').click()
            },
            updatePreviews(e) {
                var reader, files = e.target.files
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
                    if (response.Success == true) {
                        reader.onload = (e) => {
                            this.previews = e.target.result;
                            this.objRequest.AvatarArray = response.data[0].name;
                            console.log(this.selectedFile);
                        }
                        reader.readAsDataURL(files[0])
                    }
                    else {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    this.isLoading = false;
                });
            }
        }
    };
</script>
<style>
</style>

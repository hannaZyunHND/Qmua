<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Thông tin chung" active>
                <div class="row productedit">
                    <div class="col-sm-6 col-md-8">
                        <div class="card">
                            <div class="card-header">
                                Thông tin chính
                            </div>
                            <div class="card-body">
                                <b-form class="form-horizontal">
                                    <b-form-group label="Tiêu đề">
                                        <b-form-input v-model="objRequest.name" placeholder="Tên sản phẩm" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Chọn danh mục">
                                        <treeselect :multiple="true"
                                                    :flat="true"
                                                    :options="ZoneOptions"
                                                    placeholder="Xin mời bạn lựa chọn danh mục"
                                                    @select="getSelectedUser"
                                                    v-model="ZoneValues"
                                                    :default-expanded-level="Infinity" />
                                    </b-form-group>
                                    <b-form-group label="Mã sản phẩm">
                                        <b-form-input v-model="objRequest.code" placeholder="Mã sản phẩm" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Danh sách ảnh">
                                        <div class="notice-upload-image">
                                            <p>
                                                <b style="font-size: 14px;">Đăng nhiều ảnh để công cụ tìm kiếm dễ thấy bạn hơn!</b><br><i style="font-size: 11px; padding-top: 5px; color: #666;">
                                                    Kéo ảnh lên vị trí đầu tiên để chọn làm
                                                    Thumbnail.
                                                </i>
                                            </p>
                                        </div>
                                        <div class="row gallery-upload-file ui-sortable">
                                            <div style="padding:0" class="col-md-2 col-xs-4 r-queue-item added ui-sortable-handle" v-for="(itemimg,index) in ListImg">
                                                <div class="item"><img style="width:100%;height:100px" alt="Ảnh lỗi" :src="pathImgs(itemimg)"></div><i @click="removeImg(index)" class="fa fa-times"></i>
                                            </div>

                                            <div class="col-md-2 col-xs-4 _library ui-sortable-handle">
                                                <i class="fa fa-folder-open" @click="openImg('ListImg')"></i>
                                                <p>[Thư viện ảnh]</p>
                                            </div>
                                        </div>
                                    </b-form-group>
                                    <b-form-group>
                                        <b-form-checkbox v-model="IsChoseFileDowload" style="padding-bottom:7px">
                                            File Dowload
                                        </b-form-checkbox>

                                        <b-form-input v-if="!IsChoseFileDowload" v-model="FileMeta.FileDowload" placeholder="File dowload" required></b-form-input>
                                        <template v-if="IsChoseFileDowload">
                                            <button @click="openImg('filedowload')" type="button" class="btn btn-success">Chọn file</button>
                                            <a v-if="FileMeta.FileDowload != null && FileMeta.FileDowload.length > 0" target="_blank" href="FileMeta.FileDowload"><i class="fa fa-file-o"></i> {{FileMeta.FileDowload}}</a>
                                        </template>
                                    </b-form-group>
                                    <b-form-group>
                                        <b-form-checkbox v-model="IsChoseImg360" style="padding-bottom:7px">
                                            Hình 360
                                        </b-form-checkbox>
                                        <b-form-input v-if="!IsChoseImg360" v-model="FileMeta.File360" placeholder="Hình ảnh 360" required></b-form-input>
                                        <template v-if="IsChoseImg360">
                                            <button @click="openImg('file360')" type="button" class="btn btn-success">Chọn file</button>
                                            <a v-if="FileMeta.File360 != null && FileMeta.File360.length > 0" target="_blank" href="FileMeta.File360"><i class="fa fa-file-o"></i> {{FileMeta.File360}}</a>
                                        </template>
                                    </b-form-group>
                                    <b-form-group>
                                        <b-form-checkbox v-model="IsChoseVideo" style="padding-bottom:7px">
                                            File Video
                                        </b-form-checkbox>
                                        <b-form-input v-if="!IsChoseVideo" v-model="FileMeta.FileVideo" placeholder="File Video" required></b-form-input>
                                        <template v-if="IsChoseVideo">
                                            <button @click="openImg('fileVideo')" type="button" class="btn btn-success">Chọn file</button>
                                            <a v-if="FileMeta.FileVideo != null && FileMeta.FileVideo.length > 0" target="_blank" href="FileMeta.FileVideo"><i class="fa fa-file-o"></i> {{FileMeta.FileVideo}}</a>
                                        </template>
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
                                        <b-form-group label="Giá sản phẩm">
                                            <b-form-input @change="sumTotal" :min="0" v-model="objRequest.price" type="number" placeholder="Giá sản phẩm" required></b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col>
                                        <b-form-group label="Phần trăm">
                                            <b-form-input @change="sumTotal" :min="0" :max="100" v-model="objRequest.discountPercent" type="number" placeholder="Phần trăm" required></b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col>
                                        <b-form-group label="Giảm giá">
                                            <b-form-input v-model="objRequest.discountPrice" :min="0" type="number" placeholder="Giảm giá" required></b-form-input>
                                        </b-form-group>
                                    </b-col>

                                </b-row>
                                <b-form-group label="Bảo hành">
                                    <b-form-input v-model="objRequest.warranty" placeholder="Bảo hành" required></b-form-input>
                                </b-form-group>
                            </div>
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
                                    <v-select v-model="objRequest.manufacturerId" :options="ManufacturerIds" :reduce="x=>x.id" label="name" placeholder="Chọn nhà cung cấp"></v-select>
                                </b-form-group>
                                <b-form-group label="Đơn vị">
                                    <b-form-input v-model="objRequest.unit" placeholder="Đơn vị" required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Số lượng">
                                    <b-form-input v-model="objRequest.quantity" placeholder="Số lượng" required type="number"></b-form-input>
                                </b-form-group>
                                <b-form-group label="Mã Voucher">
                                    <b-form-input v-model="objRequest.voucher" placeholder="Mã Voucher" required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Ngày hết hạn khuyến mại">
                                    <b-form-input type="date" v-model="objRequest.exprirePromotion" placeholder="Ngày hết hạn khuyến mại"></b-form-input>
                                </b-form-group>
                                <b-form-group>
                                    <b-form-checkbox v-model="objRequest.isInstallment"> Trả góp</b-form-checkbox>
                                </b-form-group>
                                <b-form-group>
                                    <label class="typo__label">Chọn thuộc tính</label>
                                    <treeselect :multiple="true"
                                                :flat="true"
                                                :options="Properties"
                                                placeholder="Xin mời bạn lựa chọn thuộc tính"
                                                v-model="ListProperty" />
                                </b-form-group>
                                <b-form-group label="Chính sách bảo hành">
                                    <select v-model="objRequest.guarantee" class="form-control">
                                        <option value="">Chọn chính sách</option>
                                        <option :value="item.key" v-for="item in ListGuarantee">
                                            {{item.value}}
                                        </option>
                                    </select>
                                </b-form-group>
                                <b-form-group label="Loại vật liệu">
                                    <select v-model="objRequest.materialType" class="form-control">
                                        <option :value="item.key" v-for="item in ListTypeMaterial">
                                            {{item.value}}
                                        </option>
                                    </select>
                                </b-form-group>
                                <b-form-group label="Màu sắc">
                                    <treeselect :multiple="true"
                                                :flat="true"
                                                :options="Colors"
                                                placeholder="Xin mời bạn lựa chọn thuộc tính"
                                                v-model="ListColors" />
                                </b-form-group>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                <b-row>
                                    <b-col md="10">
                                        Ảnh
                                    </b-col>
                                </b-row>
                            </div>
                            <div class="card-body">
                                <b-form-group label="Ảnh đại diện">
                                    <b-form-group>
                                        <a @click="openImg('Img')">
                                            <div style="width:150px;display:flex" class=" gallery-upload-file ui-sortable">
                                                <div style="padding:0" class="col-md-12 col-xs-12  r-queue-item ui-sortable-handle">
                                                    <div v-if="objRequest.avatar != null && objRequest.avatar != undefined &&  objRequest.avatar.length > 0">
                                                        <img alt="Ảnh lỗi" style="height:150px;width:150px" :src="pathImgs(objRequest.avatar)" class="preview-image img-thumbnail">
                                                    </div>
                                                    <div v-else>
                                                        <i class="fa fa-picture-o"></i>
                                                        <p>[Chọn ảnh]</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </b-form-group>
                                </b-form-group>
                            </div>
                        </div>
                    </div>
                </div>
            </b-tab>
            <b-tab @click="StartLoadLang()" title="2. Thông tin ngôn ngữ">
                <div>
                    <div class="row productedit">
                        <div class="col-sm-6 col-md-8">
                            <div class="card">
                                <div class="card-header">
                                    Thông tin ngôn ngữ
                                </div>
                                <div class="card-body">
                                    <b-form class="form-horizontal">
                                        <b-row>
                                            <b-col>
                                                <b-form-group label="Ngôn ngữ">
                                                    <b-form-select v-model="langSelected" @change="onChangeSelectd" :options="Languages"></b-form-select>
                                                </b-form-group>
                                            </b-col>
                                            <!--<b-col title="Xem trên website" style="margin-top:25px;font-size:30px">
                                                <a target="_blank" :href="urlBases(objRequestDetail.url)"><i class="fa fa-eye"></i></a>
                                            </b-col>-->
                                            <b-col></b-col>
                                        </b-row>
                                        <b-form-group label="Tiêu đề theo ngôn ngữ" label-for="input-1">
                                            <b-form-input id="input-1"
                                                          v-model="objRequestDetail.title"
                                                          type="text"
                                                          required
                                                          v-on:keyup.13="slugM()"
                                                          placeholder="Tiêu đề"></b-form-input>
                                        </b-form-group>

                                        <b-form-group label="Đường dẫn">
                                            <b-form-input v-model="objRequestDetail.url" placeholder="Đường dẫn" required></b-form-input>
                                        </b-form-group>
                                        <b-form-group label="Đường dẫn cũ">
                                            <b-form-input v-model="objRequestDetail.urlOld" placeholder="Đường dẫn" required></b-form-input>
                                        </b-form-group>
                                        <b-form-group label="Mô tả">
                                            <ckeditor tag-name="textarea"
                                                      v-model="objRequestDetail.description" :rows="3" :config="editorConfig">
                                            </ckeditor>
                                        </b-form-group>
                                        <b-form-group label="Nội dung">
                                            <MIEditor :contentEditor="objRequestDetail.content" v-on:handleEditorInput="getOrSetData"></MIEditor>
                                        </b-form-group>
                                        <b-row>
                                            <b-col>
                                                <b-form-group>
                                                    <label class="typo__label">Thêm tag</label>
                                                    <v-tags-input element-id="tags" v-model="Tags" :typeahead="true" :existingTags="ListAllTags"></v-tags-input>
                                                    
                                                </b-form-group>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <b-row>
                                        <b-col md="11">
                                            SEO Analysis
                                        </b-col>
                                        <b-col md="1">
                                            <b-btn v-b-toggle.collapseSEO2 variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                        </b-col>
                                    </b-row>
                                </div>
                                <b-collapse id="collapseSEO2" class="mt-2">
                                    <div class="card-body">
                                        <b-form-group label="Tiêu đề SEO">
                                            <b-form-input v-model="objRequestDetail.metaTitle" placeholder="Tiêu đề SEO" required></b-form-input>
                                        </b-form-group>
                                        <b-form-group label="Từ khóa SEO">
                                            <b-form-input v-model="objRequestDetail.metaKeyword" placeholder="Từ khóa SEO" required></b-form-input>
                                        </b-form-group>
                                        <b-form-group label="Mô tả SEO">
                                            <b-form-textarea rows="3" max-rows="6" v-model="objRequestDetail.metaDescription" placeholder="Mô tả" required></b-form-textarea>
                                        </b-form-group>
                                        <b-form-group label="Script WebPage">
                                            <b-form-textarea rows="3" max-rows="6" v-model="objRequestDetail.metaWebPage" placeholder="WebPage" required></b-form-textarea>
                                        </b-form-group>
                                        <b-form-group label="">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <b-form-checkbox placeholder="Canonical" v-model="statusCanonical" required>Chặn trùng lặp nội dung (Canonical)</b-form-checkbox>
                                                </div>
                                                <div class="col-md-6">
                                                    <b-form-checkbox placeholder="NoIndex" v-model="statusNoindex" required>Chặn lập chỉ mục (NoIndex)</b-form-checkbox>
                                                </div>
                                            </div>
                                        </b-form-group>


                                        <b-form-group label="Giá trị (Canonical)">
                                            <b-form-input placeholder="Canonical" v-model="valueCanonical" required></b-form-input>
                                        </b-form-group>


                                    </div>

                                </b-collapse>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <b-row>
                                        <b-col md="11">
                                            Social Share
                                        </b-col>
                                        <b-col md="1">
                                            <b-btn v-b-toggle.collapseSocial2 variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>
                                        </b-col>
                                    </b-row>
                                </div>
                                <b-collapse id="collapseSocial2" class="mt-2">
                                    <div class="card-body">
                                        <b-form-group label="Ảnh mô tả">
                                            <a @click="openImg('ImgS')">
                                                <div class="row gallery-upload-file ui-sortable">
                                                    <div style="width:200px;height:200px" class="r-queue-item ui-sortable-handle">
                                                        <div v-if="objRequestDetail.socialImage != null && objRequestDetail.socialImage != undefined &&  objRequestDetail.socialImage.length > 0">
                                                            <img alt="Ảnh lỗi" :src="pathImgs(objRequestDetail.socialImage)" style="height:200px;width:200px" />
                                                        </div>
                                                        <div v-else>
                                                            <i class="fa fa-picture-o"></i>
                                                            <p>[Chọn ảnh]</p>
                                                        </div>
                                                    </div>

                                                </div>
                                            </a>
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
                </div>
            </b-tab>
        </b-tabs>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>
</template>

<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    //import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import 'vue-select/dist/vue-select.css';
    import { unflatten, slug, pathImg, urlBase } from "../../plugins/helper";

    import EventBus from "./../../common/eventBus";
    import moment from 'moment';
    import VoerroTagsInput from './VoerroTagsInput';
    import FileManager from './../../components/fileManager/list'
    import MIEditor from '../../components/editor/MIEditor';
    // Import component

    export default {
        name: "productaddedit",
        data() {
            return {

                IsChoseImg360: false,
                IsChoseFileDowload: false,
                IsChoseVideo: false,

                mikey1: 'mikey1',

                content: '',
                contentContent: '',

                choseImg: "",

                isLoading: false,
                fullPage: false,
                color: "#007bff",
                isLoadLang: false,


                preview: '/assets/img/unnamed.jpg',
                previews: '/assets/img/unnamed.jpg',

                //editor: ClassicEditor,
                //editor1: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                currentSort: "Id",
                currentSortDir: "asc",
                productName: "",
                currentPage: 1,
                pageSize: 10,
                loading: true,
                langSelected: "",
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
                selected: [],
                objLocations: {
                    locationId: 0,
                },
                objCombo: {
                    name: ''
                },
                Locations: [],
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                statusNoindex: false,
                statusCanonical: false,
                valueCanonical: "",
                objRequest: {
                    avatar: "",
                    status: 2,
                    materialType: 0,
                    guarantee: "",
                    price: 0,
                    discountPrice: 0,
                    discountPercent: 0,
                    exprirePromotion: moment(String(new Date())).format('YYYY-MM-DD'),
                },
                objRequestDetail: {
                    url: "",
                    title: "",
                    content: "",
                    description: ""
                },

                objRequestDetails: [],
                Tag: "",
                Tags: [],
                ListImg: [],//Danh sách hình ảnh

                Languages: [],//Danh sách ngôn ngữ
                ManufacturerIds: [],//Danh sách nhà cung cấp
                ZoneOptions: [],
                ZoneValues: [],
                ProductUnit: [],
                Properties: [],
                ListProperty: [],
                Colors: [],
                ListColors: [],

                ListAllTags: [],
                ListGuarantee: [],
                ListTypeMaterial: [],
                objUser: {},
                FileMeta: {
                    File360: "",
                    FileDowload: "",
                    FileVideo: "",
                }
            };
        },
        created: {},
        components: {
            MIEditor,
            Loading,

            Treeselect,

            FileManager,
            "v-tags-input": VoerroTagsInput
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProduct(this.$route.params.id).then(respose => {
                    this.objRequest = respose.data;
                    //this.objRequest.id = 0;
                    if (this.objRequest != null && this.objRequest.exprirePromotion != null && this.objRequest.exprirePromotion != undefined)
                        this.objRequest.exprirePromotion = moment(String(this.objRequest.exprirePromotion)).format('YYYY-MM-DD');
                    else
                        this.objRequest.exprirePromotion = moment(String(new Date())).format('YYYY-MM-DD');
                    try {
                        this.FileMeta = JSON.parse(this.objRequest.metaFile);
                    }
                    catch (ex) {

                    }
                    this.ListProperty = respose.listProp;
                    this.ListColors = respose.listColor;
                    this.ZoneValues = respose.listCat;
                    this.ListImg = this.objRequest.avatarArray.split(",");
                });
                this.isLoading = false;
            }
        },

        computed: {
            ...mapGetters(["product", "fileName"]),
        },
        created() {
            let vm = this;
            EventBus.$on('FileSelected', value => {

            });

            this.objUser = this.getUser();

            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode,
                        text: item.name
                    }
                });
            });
            this.getAllColors().then(respose => {
                this.Colors = respose;
            });
            this.getAllManufacturers().then(response => {

                this.ManufacturerIds = response.listData;
            });

            this.productUnitGet().then(response => {
                this.ProductUnit = response.listData;
            });

            this.getPropertyProduct().then(response => {

                this.Properties = response;
            });
            this.getTagAll().then(response => {
                try {
                    this.ListAllTags = this.convertTags(response.listData);
                }
                catch (ex) {
                    console.log(ex);
                }
            });
            this.getGuarantee().then(response => {
                try {
                    this.ListGuarantee = response;
                }
                catch (ex) {
                    console.log(ex);
                }
            });
            this.getAllTypeMaterial().then(response => {
                try {
                    this.ListTypeMaterial = response.listData;
                }
                catch (ex) {
                    console.log(ex);
                }
            });
            this.onChangePaging();
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        methods: {
            ...mapActions(["updateProduct",
                "addProduct",
                "getProduct",
                "getAllManufacturers",
                "getAllLanguages",
                "addProductInLanguage",
                "getZones",
                "deleteProduct",
                "productUnitGet",
                "getPropertyProduct",
                "getLanguageById",
                "getTagAll",
                "getGuarantee",
                "getAllColors",
                "getAllTypeMaterial"
            ]),
            getOrSetData(value) {
                this.objRequestDetail.content = value;
            },
            sumTotal() {
                this.objRequest.discountPrice = this.objRequest.price - (this.objRequest.discountPercent * (this.objRequest.price / 100))
            },
            pathImgs(path) {
                return pathImg(path);
            },
            urlBases(path) {
                return urlBase(path);
            },
            getUser() {
                //debugger-
                var data = JSON.parse(localStorage.getItem('currentUser'));
                return JSON.parse(localStorage.getItem('currentUser'));
            },

            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getZones(1).then(respose => {
                    var data = respose.listData;

                    this.ZoneOptions = unflatten(data);
                });
                this.isLoading = false;
            },
            slugM: function () {
                //debugger-
                if (this.objRequestDetail != null && this.objRequestDetail != undefined) {
                    //this.objRequestDetail.url = "";

                    this.objRequestDetail.url = slug(this.objRequestDetail.title);


                }

            },

            defaultObj: function () {
                this.objRequestDetail = Object.assign({}, this.objRequestDetail);
                this.objRequestDetail.id = "00000000-0000-0000-0000-000000000000";
                this.objRequestDetail.title = "";
                this.objRequestDetail.url = "";
                this.objRequestDetail.content = "";
                this.objRequestDetail.description = "";
                this.Tags = [];
            },

            onLoadLang() {
                //debugger-
                this.isLoading = true;
                this.getLanguageById(this.objRequest.id).then(respose => {
                    if (respose.listData != null && respose.listData.length > 0) {
                        //debugger-
                        this.objRequestDetails = respose.listData;
                        if (this.objRequestDetails != null && this.objRequestDetails != undefined && this.objRequestDetails.length > 0) {
                            var obj = this.objRequestDetails.filter(x => x.languageCode == 'vi-VN     ');
                            if (obj != null && obj.length > 0) {
                                this.objRequestDetail = obj[0];
                                //
                                if (obj[0].metaNoIndex != null) {
                                    let objNoIndex = JSON.parse(obj[0].metaNoIndex);
                                    this.statusNoindex = objNoIndex.Status;
                                }
                                if (obj[0].metaCanonical != null) {
                                    let objCanonical = JSON.parse(obj[0].metaCanonical);
                                    this.statusCanonical = objCanonical.Status;
                                    this.valueCanonical = objCanonical.Value;
                                }
                                //
                            } else {
                                this.objRequestDetail = this.objRequestDetails[0];
                                //
                                if (this.objRequestDetails[0].metaNoIndex != null) {
                                    let objNoIndex = JSON.parse(this.objRequestDetails[0].metaNoIndex);
                                    this.statusNoindex = objNoIndex.Status;
                                }
                                if (this.objRequestDetails[0].metaCanonical != null) {
                                    let objCanonical = JSON.parse(this.objRequestDetails[0].metaCanonical);
                                    this.statusCanonical = objCanonical.Status;
                                    this.valueCanonical = objCanonical.Value;
                                }
                                //
                            }

                            this.Tags = this.convertTags(this.objRequestDetail.listTagName);
                        } else {
                            this.defaultObj();
                            this.statusNoindex = false;
                            this.statusCanonical = false;
                            this.valueCanonical = "";
                        }
                    }
                    this.langSelected = this.objRequestDetail.languageCode;
                });
                this.contentContent = this.objRequestDetail.content;

                this.isLoading = false;
            },
            StartLoadLang() {
                this.isLoadLang = true;
            },
            removeImg(index) {
                this.ListImg.splice(index, 1);
            },
            DoAddEdit() {
                //debugger-
                this.isLoading = true;
                this.objRequest.metaFile = JSON.stringify(this.FileMeta);
                this.objRequest.avatarArray = this.ListImg.join();
                this.objRequest.propertyId = this.ListProperty.join();
                this.objRequest.color = this.ListColors.join();
                this.objRequest.modifyBy = this.objUser.userName;

                this.objRequest.ProductInZone = this.ZoneValues.map(x => {
                    return {
                        ZoneId: x,
                        ProductId: this.objRequest.id,
                        IsPrimary: false,
                        IsHot: false,
                        BigThumb: ""
                    }
                });

                //this.objRequest.avatar = this.Img;
                if (this.objRequest.id > 0) {
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

                    this.addProduct(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.id;
                                this.objRequestDetail.ProductId = response.id;
                                this.$router.push({
                                    path: "/admin/product/edit/" + response.id,
                                });
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
            getSelectedUser(node, id) {
                this.objRequest.ZoneId = node.id;
            },
            convertTags(value) {
                if (value != null && value != undefined && value.length > 0) {
                    return value.map(x => {
                        return { key: slug(x), value: x }
                    });
                }
                return value;
            },
            onChangeSelectd() {
                console.log(this.objRequestDetails)
                if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                    //debugger-;
                    let lang = this.langSelected || "vi-VN     ";
                    let lstObjLang = this.objRequestDetails.filter(function (item) {
                        return item.languageCode.trim() === lang.trim()
                    });
                    if (lstObjLang != null && lstObjLang != undefined && lstObjLang.length > 0) {
                        this.objRequestDetail = lstObjLang[0];
                        //
                        if (lstObjLang[0].metaNoIndex != null) {
                            let objNoIndex = JSON.parse(lstObjLang[0].metaNoIndex);
                            this.statusNoindex = objNoIndex.Status;
                        }
                        if (lstObjLang[0].metaCanonical != null) {
                            let objCanonical = JSON.parse(lstObjLang[0].metaCanonical);
                            this.statusCanonical = objCanonical.Status;
                            this.valueCanonical = objCanonical.Value;
                        }
                        //
                        this.Tags = this.convertTags(this.objRequestDetail.listTagName);
                    } else {
                        this.defaultObj();
                        this.statusNoindex = false;
                        this.statusCanonical = false;
                        this.valueCanonical = "";
                    }
                }
                this.objRequestDetail.languageCode = this.langSelected;

            },

            DoAddDetail() {
                this.objRequestDetail.productId = this.objRequest.id;
                //
                let objMetaNoIndex = {
                    Value: "",
                    Status: this.statusNoindex
                }
                let objMetaCanonical = {
                    Value: this.valueCanonical,
                    Status: this.statusCanonical
                }
                this.objRequestDetail.metaNoIndex = JSON.stringify(objMetaNoIndex);
                this.objRequestDetail.metaCanonical = JSON.stringify(objMetaCanonical);
                //
                this.objRequestDetail.listTagName = this.Tags.map(x => {
                    return x.value
                });

                this.addProductInLanguage(this.objRequestDetail)
                    .then(response => {
                        if (response.success == true) {
                            if (response.data != null && response.data != undefined) {
                                this.objRequestDetail.id = response.data.Id;
                                if (!this.objRequestDetails.some(x => x.languageCode == this.objRequestDetail.languageCode)) {
                                    this.objRequestDetails.push(this.objRequestDetail);
                                }
                            }
                            this.$toast.success(response.message, {});
                        }
                        else {
                            this.$toast.error(response.message, {});
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    });
            },
            DoAttackFile(value) {
                let vm = this;
                if (this.choseImg == "ListImg") {
                    value.map(x => {
                        vm.ListImg.push(x.path)
                    });
                } else if (this.choseImg == "Img") {
                    vm.objRequest.avatar = value[0].path;
                }
                else if (this.choseImg == "ImgS") {
                    vm.objRequestDetail.socialImage = value[0].path;
                }
                else if (this.choseImg == "fileVideo") {
                    vm.FileMeta.FileVideo = value[0].path;
                }
                else if (this.choseImg == "file360") {
                    vm.FileMeta.File360 = value[0].path;
                }
                else if (this.choseImg == "filedowload") {
                    vm.FileMeta.FileDowload = value[0].path;
                }
            },
            openImg(img) {
                this.choseImg = img;
                if (img == "ListImg") {
                    //EventBus.$emit("FileSelected", );
                    EventBus.$emit(this.mikey1, 'multi'); // '': select one, 'multi': select multi file
                } else {
                    //EventBus.$emit("FileSelected", this.Img);
                    EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
                }
            },

        },
        watch: {
            isLoadLang: function (val) {
                this.onLoadLang();
            },

        },
    };
</script>
<style>
    /* The input */
    .tags-input {
        display: flex;
        flex-wrap: wrap;
        align-items: center;
    }

        .tags-input input {
            flex: 1;
            background: transparent;
            border: none;
        }

            .tags-input input:focus {
                outline: none;
            }

            .tags-input input[type="text"] {
                color: #495057;
            }

    .tags-input-wrapper-default {
        padding: .5em .25em;
        background: #fff;
        border: 1px solid transparent;
        border-radius: .25em;
        border-color: #dbdbdb;
    }

        .tags-input-wrapper-default.active {
            border: 1px solid #8bbafe;
            box-shadow: 0 0 0 0.2em rgba(13, 110, 253, 0.25);
            outline: 0 none;
        }

    /* The tag badges & the remove icon */
    .tags-input span {
        margin-right: 0.3em;
    }

    .tags-input-remove {
        cursor: pointer;
        position: absolute;
        display: inline-block;
        right: 0.3em;
        top: 0.3em;
        padding: 0.5em;
        overflow: hidden;
    }

        .tags-input-remove:focus {
            outline: none;
        }

        .tags-input-remove:before, .tags-input-remove:after {
            content: '';
            position: absolute;
            width: 75%;
            left: 0.15em;
            background: #5dc282;
            height: 2px;
            margin-top: -1px;
        }

        .tags-input-remove:before {
            transform: rotate(45deg);
        }

        .tags-input-remove:after {
            transform: rotate(-45deg);
        }

    /* Tag badge styles */
    .tags-input-badge {
        position: relative;
        display: inline-block;
        padding: 0.25em 0.4em;
        font-size: 75%;
        font-weight: 700;
        line-height: 1;
        text-align: center;
        white-space: nowrap;
        vertical-align: baseline;
        border-radius: 0.25em;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .tags-input-badge-pill {
        padding-right: 1.25em;
        padding-left: 0.6em;
        border-radius: 10em;
    }

    .tags-input-badge-selected-default {
        color: #212529;
        background-color: #f0f1f2;
    }

    /* Typeahead */
    .typeahead-hide-btn {
        color: #999 !important;
        font-style: italic;
    }

    /* Typeahead - badges */
    .typeahead-badges > span {
        cursor: pointer;
        margin-right: 0.3em;
    }

    /* Typeahead - dropdown */
    .typeahead-dropdown {
        list-style-type: none;
        padding: 0;
        margin: 0;
        position: absolute;
        width: 100%;
        z-index: 1000;
    }

        .typeahead-dropdown li {
            padding: .25em 1em;
            cursor: pointer;
        }

    /* Typeahead elements style/theme */
    .tags-input-typeahead-item-default {
        color: #fff;
        background-color: #343a40;
    }

    .tags-input-typeahead-item-highlighted-default {
        color: #fff;
        background-color: #007bff;
    }
</style>

<template>

    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>

        <b-tabs class="col-md-12" pills v-model="tabIndex">
            <b-tab href="#1" title="1. Nhập thông tin bài viết" active>
                <div class="row productedit">
                    <div class="col-md-12">
                        <b-card class="mt-3 " header="Thêm / Sửa bài viết">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <b-form-group label="Tên bài viết" label-for="input-1">
                                            <b-form-input id="input-1"
                                                          v-model="objRequest.name"
                                                          type="text"
                                                          required
                                                          v-on:keyup.13="slugM"
                                                          placeholder="Tên bài viết"></b-form-input>
                                        </b-form-group>
                                    </div>

                                    <div class="col-md-6">
                                        <b-form-group label="Trạng thái">
                                            <treeselect :options="StatusOptions"
                                                        :disable-branch-nodes="true"
                                                        v-model="objRequest.status"
                                                        :default-expanded-level="Infinity"
                                                        placeholder="Xin mời bạn lựa chọn trạng thái" />
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-6">
                                        <b-form-group label="Kiểu">
                                            <treeselect :options="TypeOptions"
                                                        :disable-branch-nodes="true"
                                                        v-model="objRequest.type"
                                                        :default-expanded-level="Infinity"
                                                        placeholder="Xin mời bạn lựa chọn loại bài viết" />
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
                                        <b-form-group>
                                            <b-form-checkbox v-model="objRequest.isShowHome">
                                                Cho phép hiển thị trang chủ
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Hình đại diện">
                                            <a @click="openImg('avatar')">
                                                <div style="width:200px;height:200px;display:flex"
                                                     class="gallery-upload-file ui-sortable">
                                                    <div style="width:100%;height:auto;margin:0"
                                                         class=" r-queue-item ui-sortable-handle">
                                                        <div style="width:100%"
                                                             v-if="objRequest.avatar != null && objRequest.avatar != undefined &&  objRequest.avatar.length > 0">
                                                            <img alt="Ảnh lỗi" style="height:200px;width:200px"
                                                                 :src="pathImgs(objRequest.avatar)">
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
                                    <div class="col-md-12">
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
                                    </div>
                                </div>
                            </b-form>
                        </b-card>
                    </div>
                </div>
            </b-tab>
            <b-tab href="#2" title="2. Nhập thông tin ngôn ngữ">
                <div class="row productedit">
                    <div class="col-md-8">
                        <b-card class="mt-3 " header="Thêm / Sửa bài viết">
                            <loading :active.sync="isLoading"
                                     :height="35"
                                     :width="35"
                                     :color="color"
                                     :is-full-page="fullPage"></loading>
                            <div class="row">
                                <div class="col-md-4 col-xs-10">
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

                            </div>
                            <b-form-group label="Tiêu đề" label-for="input-1">
                                <b-form-input id="input-1"
                                              v-model="objRequest.title"
                                              type="text"
                                              required
                                              v-on:keyup.13="slugM"
                                              placeholder="Tiêu đề"></b-form-input>
                            </b-form-group>
                            <b-form class="form-horizontal">
                                <b-form-group label="Đường dẫn">
                                    <b-form-input v-model="objRequest.url" placeholder="Url" required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Đường dẫn cũ">
                                    <b-form-input v-model="objRequest.urlOld" placeholder="Url" required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Tác giả">
                                    <b-form-input v-model="objRequest.author" placeholder="Tác giả"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Nguồn">
                                    <b-form-input v-model="objRequest.source" placeholder="Nguồn"
                                                  required></b-form-input>
                                </b-form-group>

                                <b-form-group label="Mô tả">
                                    <ckeditor tag-name="textarea"
                                              v-model="objRequest.description" :config="editorConfig">
                                    </ckeditor>
                                </b-form-group>
                                <b-form-group label="Nội dung">
                                    <button type="button" @click="autoGenIndex()" class="btn btn-success">
                                        Tạo chỉ mục
                                    </button>
                                    <button type="button" @click="removeIndex()" class="btn btn-danger">
                                        Xóa chỉ mục
                                    </button>
                                    <div class="row">
                                        <div class="col-md-12" v-html="objRequest.indexing"></div>
                                    </div>
                                    <MIEditor :contentEditor="objRequest.body" v-on:handleEditorInput="getOrSetData"></MIEditor>
                                </b-form-group>
                                <div class="col-md-12">
                                    <b-form-group>
                                        <label class="typo__label" style="margin-left: -16px;">Thêm tag</label>
                                        <vue-tags-input v-model="tag"
                                                        :tags="tags"
                                                        :autocomplete-items="filteredItems"
                                                        @tags-changed="newTags => tags = newTags"
                                                        :allow-edit-tags=true placeholder="Thêm tag"
                                                        class="tags-input" />
                                    </b-form-group>
                                </div>
                                <b-form-group>
                                    <b-form-checkbox v-model="IsChoseFileDowload" style="padding-bottom:7px">
                                        File Dowload DLC
                                    </b-form-checkbox>
                                    <b-form-input v-if="!IsChoseFileDowload" v-model="objRequest.fileAttachment" placeholder="File dowload DLC" required></b-form-input>
                                    <template v-if="IsChoseFileDowload">
                                        <button @click="openImg('filedowload')" type="button" class="btn btn-success">Chọn file</button>
                                        <a v-if="objRequest.fileAttachment != null && objRequest.fileAttachment.length > 0" target="_blank" href="objRequest.fileAttachment"><i class="fa fa-file-o"></i> {{objRequest.fileAttachment}}</a>
                                    </template>
                                </b-form-group>
                                <b-form-group>
                                    <b-form-checkbox v-model="IsChoseFileDowload" style="padding-bottom:7px">
                                        File Dowload DLT
                                    </b-form-checkbox>
                                    <b-form-input v-if="!IsChoseFileDowload" v-model="objRequest.fileAttachmentMinify" placeholder="File dowload DLT" required></b-form-input>
                                    <template v-if="IsChoseFileDowload">
                                        <button @click="openImg('filedowload')" type="button" class="btn btn-success">Chọn file</button>
                                        <a v-if="objRequest.fileAttachmentMinify != null && objRequest.fileAttachmentMinify.length > 0" target="_blank" href="objRequest.fileAttachmentMinify"><i class="fa fa-file-o"></i> {{objRequest.fileAttachmentMinify}}</a>
                                    </template>
                                </b-form-group>
                            </b-form>
                        </b-card>
                        <b-card class="mt-3 " header="SEO Analysis">
                            <b-form class="form-horizontal">
                                <b-form-group label="Tiêu đề SEO">
                                    <b-form-input v-model="objRequest.metaTitle" placeholder="Tiêu đề Seo"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Mô tả SEO">
                                    <b-form-textarea rows="3" max-rows="6" v-model="objRequest.metaDescription"
                                                     placeholder="Mô tả SEO"
                                                     required></b-form-textarea>
                                </b-form-group>
                                <b-form-group label="Từ khóa SEO">
                                    <b-form-input v-model="objRequest.metaKeyword" placeholder="Từ khóa SEO"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <b-form-checkbox placeholder="Canonical" v-model="statusCanonical" required>Chặn trùng lặp nội dung (Canonical)</b-form-checkbox>
                                        </div>
                                        <div class="col-md-6">
                                            <b-form-checkbox placeholder="NoIndex" v-model="statusNoindex" required>Chặn lập chỉ mục (NoIndex)</b-form-checkbox>
                                        </div>
                                    </div>
                                </b-form-group>
                                <b-form-group>

                                    <b-form-group label="Giá trị (Canonical)">
                                        <b-form-input placeholder="Canonical" v-model="valueCanonical" required></b-form-input>
                                    </b-form-group>
                                </b-form-group>
                            </b-form>
                        </b-card>
                        <b-card class="mt-3 " header=" Social Share">
                            <b-form-group label="Tiêu để  Facebook">
                                <b-form-input v-model="objRequest.socialTitle" placeholder="Tiêu để mạng xã hội"
                                              required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Mô tả Facebook">
                                <b-form-textarea rows="3" max-rows="6" v-model="objRequest.socialDescription"
                                                 placeholder="Mô tả xã hội"
                                                 required></b-form-textarea>
                            </b-form-group>
                            <b-form-group label="Script WebPage">
                                <b-form-textarea rows="3" max-rows="6" v-model="objRequest.metaWebPage" placeholder="WebPage" required></b-form-textarea>
                            </b-form-group>
                            <div class="row">
                                <div class="col-md-4 col-xs-12">
                                    <b-form-group label="Ảnh Facebook">
                                        <a @click="openImg('facebook')">
                                            <div style="width:200px;height:200px;display:flex"
                                                 class="gallery-upload-file ui-sortable">
                                                <div style="width:100%;height:auto;margin:0"
                                                     class=" r-queue-item ui-sortable-handle">
                                                    <div style="width:100%"
                                                         v-if="objRequest.socialImage != null && objRequest.socialImage != undefined &&  objRequest.socialImage.length > 0">
                                                        <img alt="Ảnh lỗi" style="height:200px;width:200px"
                                                             :src="pathImgs(objRequest.socialImage)">
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
                            </div>
                        </b-card>
                    </div>
                    <div class="col-md-4">
                        <div class="mt-3">
                            <b-card header="Đăng bài">
                                <div class="row">
                                    <div class="col-md-6">
                                        <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit"
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
                            </b-card>
                        </div>
                        <div class="mt-3">
                            <b-card header="Thông tin dự án">
                                <b-form-group label="Khách hàng">
                                    <b-form-input v-model="objRequest.ttkh " placeholder="Khách hàng"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Khu vực">
                                    <b-form-input v-model="objRequest.ttdc" placeholder="Khu vực"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Lĩnh vực">
                                    <b-form-input v-model="objRequest.ttlv" placeholder="Lĩnh vực"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Dịch vụ">
                                    <b-form-input v-model="objRequest.ttdv" placeholder="Dịch vụ"
                                                  required></b-form-input>
                                </b-form-group>
                               
                            </b-card>
                        </div>
                        <div class="mt-3">
                            <b-card header="Thông tin bài viết tuyển dụng">
                                <b-form-group label="Vị trí">
                                    <b-form-input v-model="objRequest.position " placeholder="Vị trí"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Địa chỉ">
                                    <b-form-input v-model="objRequest.address" placeholder="Địa chỉ"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Số lượng">
                                    <b-form-input type="number" v-model="objRequest.count" placeholder="Số lượng"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Mức lương">
                                    <b-form-input v-model="objRequest.salary" placeholder="Mức lương"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-group label="Ngày bắt đầu">

                                    <b-form-input v-model="objRequest.startDate" type="date" placeholder="Ngày bắt đầu"
                                                  required></b-form-input>
                                    <!--<date-time-picker v-model="startDate"></date-time-picker>-->
                                </b-form-group>
                                <b-form-group label="Ngày kết thúc">

                                    <b-form-input v-model="objRequest.endDate" type="date" placeholder="Ngày kết thúc"
                                                  required></b-form-input>
                                    <!--<date-time-picker v-model="endDate"></date-time-picker>-->
                                </b-form-group>
                            </b-card>
                        </div>
                        <div class="mt-3">
                            <b-card header="Thông tin thêm">
                                <b-form-group label="Lượt thích">
                                    <b-form-input type="number" v-model="objRequest.likeCount"
                                                  placeholder="Lượt thích"
                                                  required></b-form-input>
                                </b-form-group>
                                <!--<b-form-group label="Lượt xem">
                                    <b-form-input type="number" v-model="objRequest.viewCount"
                                                  placeholder="Lượt thích"
                                                  required></b-form-input>
                                </b-form-group>-->
                                <b-form-group label="Số lượng vote">
                                    <b-form-input type="number" v-model="objRequest.voteCount"
                                                  placeholder="Số lượng vote"
                                                  required></b-form-input>
                                </b-form-group>
                                <b-form-checkbox v-model="objRequest.isAllowComment">
                                    Cho phép comment
                                </b-form-checkbox>
                            </b-card>
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
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import 'vue-select/dist/vue-select.css';

    import FileManager from './../../components/fileManager/list'
    import { slug, pathImg, unflatten, urlBase } from "../../plugins/helper";
    // Import component
    // Import component

    import EventBus from "./../../common/eventBus";
    import VueTagsInput from '@johmun/vue-tags-input';
    import { router } from "../../router";
    import moment from 'moment';
    import MIEditor from '../../components/editor/MIEditor';
    export default {
        name: "articleaddedit",
        data() {
            return {
                //editor: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",
                },

                mikey1: 'mikey1',

                IsChoseFileDowload: false,
                disabled: false,
                selectedFile: null,
                preview: '/assets/img/unnamed.jpg',
                previewImageFacebook: '/assets/img/unnamed.jpg',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                statusNoindex: false,
                statusCanonical: false,
                valueCanonical: "",
                objRequest: {
                    id: 0,
                    title: "",
                    url: "",
                    avatar: "",
                    socialImage: "",
                    type: 0,
                    fileAttachment: "",
                    fileAttachmentMinify: "",
                    startDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    endDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    indexing: "",
                    urlOld: ""
                },
                isProduct: false,
                LanguageCodes: [],
                LanguageValues: null,
                Manufacturers: [],
                ZoneValues: [],
                ZoneOptions: [],
                ProductOptionsSource: [],
                ProductOptionsDestination: [],
                ProductSelectedStateOption: [],
                ProductIdSelected: "",
                ZoneArticleIdSelected: "",
                ListArticleInLanguage: [],
                StatusOptions: [],
                TypeOptions: [],
                tag: '',
                tags: [],
                tabIndex: 0,
                tabs: ['#1', '#2'],
                autocompleteItems: [],
                //startDate: new Date().toLocaleString(),
                //endDate: new Date().toLocaleString(),
                fileSample: null,

            };
        },
        async created() {
            let vm = this;

            await this.getZoneArticles();
            await this.getAllStatusOptions();
            await this.getAllTypeOptions();
            await this.getProductAtArticles();
            await this.getAllLanguageOptions();
            await this.getArticleByIds();
            await this.getAllTags();
        },
        components: {
            Loading,
            Treeselect,

            'FileManager': FileManager,
            VueTagsInput,

            MIEditor
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        mounted() {
            this.tabIndex = this.tabs.findIndex(tab => tab === this.$route.hash)
        },

        computed: {
            ...mapGetters(["article", "isOR", "fileName"]),

            filteredItems() {
                // for (var i = 0; i < this.autocompleteItems.length; i++){
                //     if (i.text.toLowerCase().indexOf(this.tag.toLowerCase())){
                //
                //     }
                // }
                this.autocompleteItems = Array.from(this.autocompleteItems);
                return this.autocompleteItems.filter(i => {
                    if (i.text != null) {
                        return i.text.toLowerCase().indexOf(this.tag.toLowerCase()) !== -1;
                    }
                });
            }
        },
        watch: {
            article: function (val) {
                this.objRequest = this.article
            }
        },
        methods: {
            ...mapActions(["updateArticle", "getAllTag", "addArticle", "getArticle", "uploadFile", "getAllStatusOption", "getZoneArticle", "getAllTypeOption", "getProductAtArticle", "getAllLanguageOption", "getArticleById", "autoGenIndexing"]),
            openImg(img) {
                this.choseImg = img;
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
            DoAttackFile(value) {
                let vm = this;
                if (this.choseImg == "ListImg") {
                    vm.ListImg = value.map(x => {
                        return x.path
                    });
                } else if (this.choseImg == "avatar") {
                    vm.objRequest.avatar = value[0].path;
                }
                else if (this.choseImg == "facebook") {
                    vm.objRequest.socialImage = value[0].path;
                }
                else if (this.choseImg == "filedowload") {
                    vm.objRequest.fileAttachment = value[0].path;
                }
            },
            getOrSetData(value) {
                this.objRequest.body = value;
            },
            slugM: function () {

                if (this.objRequest != null && this.objRequest != undefined) {
                    this.objRequest.url = slug(this.objRequest.title);
                }
            },

            autoGenIndex() {
                var obj = {};
                debugger
                obj.indexing = "";
                obj.html = this.objRequest.body;
                obj.language = this.objRequest.languageCode;
                this.autoGenIndexing(obj).then(response => {
                    debugger
                    this.objRequest.indexing = response.indexing;
                    this.objRequest.body = response.html;
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});

                });
            },
            removeIndex() {
                this.objRequest.indexing = "";
            },
            urlBases(path) {
                return urlBase(path);
            },

            pathImgs(path) {
                return pathImg(path);
            },
            getTextFileAttach() {
                return this.objRequest.fileAttachment;
            },
            tabClicked(selectedTab) {
                console.log('Current tab re-clicked:' + selectedTab.tab.name);
            },
            tabChanged(selectedTab) {
                console.log('Tab changed to:' + selectedTab.tab.name);
            },

            DoAddEdit() {
                if (this.ZoneValues != null) {
                    this.objRequest.ZoneIds = this.ZoneValues.toString();
                    console.log(this.ZoneValues.toString());
                }
                var result = Array.prototype.map.call(this.ProductOptionsDestination, function (item) {
                    return item.id;
                }).join(",");
                console.log(result);
                var tagName = '';
                this.tags.forEach(function (entry) {
                    tagName += entry.text;
                    tagName += ','
                });

                if (tagName != null) {
                    this.objRequest.TagNames = tagName.slice(0, -1)
                }
                //this.objRequest.avatarArray = this.ListImg.join();
                this.objRequest.avatarArray = "";
                this.objRequest.ProductIds = result;
                this.isLoading = true;
                //
                let objMetaNoIndex = {
                    Value: "",
                    Status: this.statusNoindex
                }
                let objMetaCanonical = {
                    Value: this.valueCanonical,
                    Status: this.statusCanonical
                }
                this.objRequest.metaNoIndex = JSON.stringify(objMetaNoIndex);
                this.objRequest.metaCanonical = JSON.stringify(objMetaCanonical);
                //
                if (this.objRequest.Id > 0) {
                    //this.objRequest.startDate = this.startDate;
                    //this.objRequest.endDate = this.endDate;
                    this.updateArticle(this.objRequest)
                        .then(response => {
                            console.log(response);
                            if (response.success == true) {
                                this.$toast.success("cập nhật thành công", {});
                                this.isLoading = false;
                                this.$router.go(-1)
                            } else {
                                this.$router.go(-1);
                                this.$toast.error("cập nhật thất bại", {});
                                this.isLoading = false;
                            }
                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                            this.isLoading = false;
                        });
                } else {
                    //this.objRequest.startDate = this.startDate;
                    //this.objRequest.endDate = this.endDate;
                    this.addArticle(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success("đăng bài thành công", {});
                                let id = response.id;
                                this.isLoading = false;
                                router.push({ path: `/admin/article/edit/${id}#2` });
                                // router.go(0);
                                // this.isLoading = false;
                                // this.$router.go(-1)
                            } else {
                                this.$router.go(-1);
                                this.$toast.error("cập nhật thất bại", {});
                                this.isLoading = false;
                            }
                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                            this.isLoading = false;
                        });
                }
            },
            removeImg(index) {
                this.ListImg.splice(index, 1);
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
                this.objRequest.languageCode = node.id;
                console.log(this.ListArticleInLanguage)
                if (this.ListArticleInLanguage != null) {
                    for (var i = 0; i < this.ListArticleInLanguage.length; i++) {

                        if (node.id == this.ListArticleInLanguage[i].languageCode) {
                            console.log(this.ListArticleInLanguage[i])
                            this.objRequest.url = this.ListArticleInLanguage[i].url;
                            this.objRequest.urlOld = this.ListArticleInLanguage[i].urlOld;
                            this.objRequest.author = this.ListArticleInLanguage[i].author;
                            this.objRequest.source = this.ListArticleInLanguage[i].source;
                            this.objRequest.title = this.ListArticleInLanguage[i].title;
                            this.objRequest.description = this.ListArticleInLanguage[i].description || "";
                            this.objRequest.body = this.ListArticleInLanguage[i].body || "";
                            this.objRequest.metaTitle = this.ListArticleInLanguage[i].metaTitle || "";
                            this.objRequest.metaDescription = this.ListArticleInLanguage[i].metaDescription || "";
                            this.objRequest.metaKeyword = this.ListArticleInLanguage[i].metaKeyword || "";
                            //
                            if (this.ListArticleInLanguage[i].metaNoIndex != null) {
                                let objNoIndex = JSON.parse(this.ListArticleInLanguage[i].metaNoIndex);
                                this.statusNoindex = objNoIndex.Status || "";
                            }
                            if (this.ListArticleInLanguage[i].metaCanonical != null) {
                                let objCanonical = JSON.parse(this.ListArticleInLanguage[i].metaCanonical);
                                this.statusCanonical = objCanonical.Status;
                                this.valueCanonical = objCanonical.Value || "";
                            }
                            //
                            this.objRequest.socialTitle = this.ListArticleInLanguage[i].socialTitle;
                            this.objRequest.socialDescription = this.ListArticleInLanguage[i].socialDescription;
                            this.objRequest.socialImage = this.ListArticleInLanguage[i].socialImage;
                            this.objRequest.wordCount = this.ListArticleInLanguage[i].wordCount || 0;
                            this.objRequest.likeCount = this.ListArticleInLanguage[i].likeCount || 0;
                            this.objRequest.viewCount = this.ListArticleInLanguage[i].viewCount || 0;
                            this.objRequest.voteCount = this.ListArticleInLanguage[i].voteCount || 0;
                            this.objRequest.position = this.ListArticleInLanguage[i].position || "";
                            this.objRequest.metaWebPage = this.ListArticleInLanguage[i].metaWebPage || "";

                            this.objRequest.address = this.ListArticleInLanguage[i].address || "";
                            this.objRequest.count = this.ListArticleInLanguage[i].count || "0";
                            this.objRequest.ttkh = this.ListArticleInLanguage[i].ttkh || "";
                            this.objRequest.ttdc = this.ListArticleInLanguage[i].ttdc || "";
                            this.objRequest.ttlv = this.ListArticleInLanguage[i].ttlv || "";
                            this.objRequest.ttdv = this.ListArticleInLanguage[i].ttdv || "";
                            this.objRequest.salary = this.ListArticleInLanguage[i].salary || "";
                            
                            this.objRequest.indexing = this.ListArticleInLanguage[i].indexing;
                            this.objRequest.startDate = moment(String(this.ListArticleInLanguage[i].startDate)).format('YYYY-MM-DD');
                            this.objRequest.endDate = moment(String(this.ListArticleInLanguage[i].endDate)).format('YYYY-MM-DD');
                            this.tags = this.ListArticleInLanguage[i].tagsViewModels;

                            this.objRequest.fileAttachmentUrl = this.ListArticleInLanguage[i].fileAttachmentUrl;
                            this.objRequest.fileAttachmen = this.ListArticleInLanguage[i].fileAttachment;
                            this.objRequest.fileAttachmentMinify = this.ListArticleInLanguage[i].fileAttachmentMinify;
                            this.objRequest.languageCode = this.ListArticleInLanguage[i].languageCode;
                            break;
                        } else {
                            this.objRequest.url = "";
                            this.objRequest.urlOld = "";
                            this.objRequest.author = "";
                            this.objRequest.source = "";
                            this.objRequest.title = "";
                            this.objRequest.description = "";
                            this.objRequest.body = "";
                            this.objRequest.metaTitle = "";
                            this.objRequest.metaDescription = "";
                            this.objRequest.metaKeyword = "";
                            this.objRequest.socialTitle = "";
                            this.objRequest.socialDescription = "";
                            this.objRequest.socialImage = "";
                            this.objRequest.wordCount = "";
                            this.objRequest.likeCount = "";
                            this.objRequest.viewCount = "";
                            this.objRequest.voteCount = "";
                            this.objRequest.indexing = "";
                            this.objRequest.metaWebPage = "";
                            this.statusNoindex = false;
                            this.statusCanonical = false;
                            this.valueCanonical = "";
                            this.tags = [];
                            this.objRequest.startDate = moment(String(new Date())).format('YYYY-MM-DD'),
                                this.objRequest.endDate = moment(String(new Date())).format('YYYY-MM-DD'),
                                this.objRequest.languageCode = node.id;
                        }
                    }
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

            onChangeList: function ({ source, destination }) {
                this.ProductOptionsSource = source;
                this.ProductOptionsDestination = destination;
            },
            async getAllTags() {
                this.autocompleteItems = await this.getAllTag();
            },
            async getAllStatusOptions() {
                this.StatusOptions = await this.getAllStatusOption();
            },
            async getZoneArticles() {
                this.ZoneOptions = await this.getZoneArticle();
            },
            async getAllTypeOptions() {
                this.TypeOptions = await this.getAllTypeOption();
            },
            async getProductAtArticles() {
                this.ProductOptionsSource = await this.getProductAtArticle();
            },
            async getAllLanguageOptions() {
                this.LanguageCodes = await this.getAllLanguageOption();
            },
            async getArticleByIds() {
                if (this.$route.params.id > 0) {
                    //debugger
                    var id = this.$route.params.id;
                    this.isLoading = true;
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    var result = await this.getArticleById(id);
                    console.log(result.data.articles.avatar);
                    console.log(result.data.articleInLanguageViewModels);
                    this.objRequest.avatar = result.data.articles.avatar;
                    this.objRequest.status = result.data.articles.status;
                    this.objRequest.type = result.data.articles.type;
                    this.objRequest.name = result.data.articles.name;
                    this.objRequest.isShowHome = result.data.articles.isShowHome;
                    this.objRequest.isAllowComment = result.data.articles.isAllowComment;
                    this.ProductOptionsDestination = result.data.productAtArticles;
                    this.ZoneValues = Object.values(result.data.listZoneIds);
                    this.objRequest.Id = result.data.articles.id;
                    //
                    
                    //


                    if (result.data.articles.avatar != null && result.data.articles.avatar != "") {
                        this.preview = result.data.articles.avatar;
                    }
                    var listArticleInLanguage = result.data.articleInLanguageViewModels;
                    console.log(listArticleInLanguage)
                    this.ListArticleInLanguage = listArticleInLanguage;
                    for (var i = 0; i < listArticleInLanguage.length; i++) {
                        this.LanguageValues = listArticleInLanguage[0].languageCode;
                        this.objRequest.indexing = listArticleInLanguage[0].indexing;
                        this.objRequest.url = listArticleInLanguage[0].url;
                        this.objRequest.urlOld = listArticleInLanguage[0].urlOld;
                        this.objRequest.author = listArticleInLanguage[0].author;
                        this.objRequest.source = listArticleInLanguage[0].source;
                        this.objRequest.title = listArticleInLanguage[0].title;
                        this.objRequest.description = listArticleInLanguage[0].description || "";
                        this.objRequest.body = listArticleInLanguage[0].body || "";
                        this.objRequest.metaTitle = listArticleInLanguage[0].metaTitle;
                        this.objRequest.metaDescription = listArticleInLanguage[0].metaDescription;
                        this.objRequest.metaKeyword = listArticleInLanguage[0].metaKeyword;
                        //
                        if (listArticleInLanguage[0].metaNoIndex != null) {
                            let objNoIndex = JSON.parse(listArticleInLanguage[0].metaNoIndex);
                            this.statusNoindex = objNoIndex.Status;
                        }
                        if (listArticleInLanguage[0].metaCanonical != null) {
                            let objCanonical = JSON.parse(listArticleInLanguage[0].metaCanonical);
                            this.statusCanonical = objCanonical.Status;
                            this.valueCanonical = objCanonical.Value;
                        }
                        //
                        this.objRequest.socialTitle = listArticleInLanguage[0].socialTitle;
                        this.objRequest.socialDescription = listArticleInLanguage[0].socialDescription;
                        this.objRequest.socialImage = listArticleInLanguage[0].socialImage;
                        this.tags = listArticleInLanguage[0].tagsViewModels;
                        this.previewImageFacebook = listArticleInLanguage[0].socialImage;
                        this.objRequest.wordCount = listArticleInLanguage[0].wordCount || 0;
                        this.objRequest.likeCount = listArticleInLanguage[0].likeCount || 0;
                        this.objRequest.viewCount = listArticleInLanguage[0].viewCount || 0;
                        this.objRequest.voteCount = listArticleInLanguage[0].voteCount || 0;
                        this.objRequest.position = listArticleInLanguage[0].position || "";
                        this.objRequest.address = listArticleInLanguage[0].address || "";
                        this.objRequest.count = listArticleInLanguage[0].count || "0";

                        var mt = JSON.parse(listArticleInLanguage[0].metaData);

                        this.objRequest.ttkh = mt.ttkh || "";
                        this.objRequest.ttdc = mt.ttdc || "";
                        this.objRequest.ttlv = mt.ttlv || "";
                        this.objRequest.ttdv = mt.ttdv || "";
                        
                        if(mt.AvatarArray === undefined){
                            console.log(1);
                            this.ListImg = [];
                        }                            
                        else
                            this.ListImg = mt.AvatarArray.split(",") || [];
                        this.objRequest.salary = listArticleInLanguage[0].salary || "";
                        this.objRequest.startDate = moment(String(listArticleInLanguage[0].startDate)).format('YYYY-MM-DD');
                        this.objRequest.endDate = moment(String(listArticleInLanguage[0].endDate)).format('YYYY-MM-DD');
                        this.objRequest.languageCode = listArticleInLanguage[0].languageCode;
                        this.objRequest.fileAttachment = listArticleInLanguage[0].fileAttachment;
                        this.objRequest.fileAttachmentMinify = listArticleInLanguage[0].fileAttachmentMinify;
                        this.objRequest.fileAttachmentUrl = listArticleInLanguage[0].fileAttachmentUrl;
                        this.objRequest.metaWebPage = listArticleInLanguage[0].metaWebPage || "";

                    }
                    this.isLoading = false;
                }
            },
            updateSocial(e) {
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
                            this.objRequest.socialImage = response.linkImage;
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
        }
    };
</script>
<style>

    .tags-input {
        max-width: 100% !important;
        position: relative;
        background-color: #fff;
        margin-left: -15px;
    }


    #toc_container {
        background: #f9f9f9;
        border: 1px solid #aaa;
        padding: 10px;
        margin-bottom: 1em;
        width: auto;
        display: table;
        font-size: 95%;
    }

        #toc_container p.toc_title {
            text-align: center;
            font-weight: 700;
            margin: 0;
            padding: 0;
        }

        #toc_container.no_bullets li, #toc_container.no_bullets ul, #toc_container.no_bullets ul li, .toc_widget_list.no_bullets, .toc_widget_list.no_bullets li {
            background: 0 0;
            list-style-type: none;
            list-style: none;
        }

    .container-fluid .btn {
        border-radius: 0;
    }

    #toc_container.no_bullets li, #toc_container.no_bullets ul, #toc_container.no_bullets ul li, .toc_widget_list.no_bullets, .toc_widget_list.no_bullets li {
        background: 0 0;
        list-style-type: none;
        list-style: none;
    }
</style>

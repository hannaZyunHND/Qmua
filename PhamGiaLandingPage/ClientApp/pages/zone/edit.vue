<template>
    <div class="productadd">
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Thông tin chung" active>
                <div class="row">
                    <div class="col-sm-6 col-md-8">
                        <b-card class="mt-3" header="Thông tin danh mục">
                            <b-form-group label="Tiêu đề">
                                <b-form-input v-model="objRequest.name" placeholder="Tiêu đề" required></b-form-input>
                            </b-form-group>
                            <div class="row">
                                <div class="col-md-6">
                                    <b-form-group label="Loại">
                                        <b-form-select v-model="Type" :options="Types"></b-form-select>
                                    </b-form-group>
                                </div>
                                <div class="col-md-6">
                                    <b-form-group label="Danh mục cha">
                                        <treeselect :multiple="false"
                                                    :options="ZoneByType"
                                                    placeholder="Xin mời bạn lựa chọn danh mục"
                                                    v-model="objRequest.parentId"
                                                    :default-expanded-level="Infinity" />
                                    </b-form-group>
                                </div>
                            </div>
                            <b-form-group label="Hình nền">
                                <b-form-group>
                                    <a @click="openImg('bg')">
                                        <div style="width:50%;display:flex" class="gallery-upload-file ui-sortable">
                                            <div style="width:100%;display:flex;margin:0" class=" r-queue-item ui-sortable-handle">
                                                <div style="width:100%" v-if="objRequest.background != null && objRequest.background != undefined &&  objRequest.background.length > 0">
                                                    <img alt="Ảnh lỗi" style="height:100px;width:100%" :src="pathImgs(objRequest.background)" class="preview-image img-thumbnail-full">
                                                </div>
                                                <div v-else>
                                                    <i class="fa fa-picture-o"></i>
                                                    <p>[Chọn ảnh]</p>
                                                </div>
                                            </div>

                                        </div>
                                    </a>
                                    <!--<b-form-file id="file-field1" size="sm" v-on:change="updatePreview2"></b-form-file>-->
                                </b-form-group>
                            </b-form-group>
                            <b-form-group label="Banner">
                                <b-form-group>
                                    <!--<b-form-file id="file-field2" size="sm" v-on:change="updatePreview3"></b-form-file>-->
                                    <a @click="openImg('banner')">
                                        <div style="width:50%;display:flex" class="gallery-upload-file ui-sortable">
                                            <div style="width:100%;display:flex;margin:0" class=" r-queue-item ui-sortable-handle">
                                                <div style="width:100%" v-if="objRequest.banner != null && objRequest.banner != undefined &&  objRequest.banner.length > 0">
                                                    <img alt="Ảnh lỗi" style="height:100px;width:100%" :src="pathImgs(objRequest.banner)" class="preview-image img-thumbnail-full">
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

                            <b-form-group label="Chọn làm danh mục tìm kiếm">
                                <b-col md="2">
                                    <b-select :options="SearchTypes" v-model="objRequest.zoneSearchType"></b-select>
                                </b-col>
                            </b-form-group>
                            <b-form-group label="Tùy chọn lọc giá">
                                <b-form-input v-model="objRequest.fillter" placeholder="Tạo bộ lọc giá theo định dạng : 0-10000,10000-1000000...." required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Nhà cung cấp">
                                <treeselect :multiple="true"
                                            :options="Manufacturer"
                                            placeholder="Xin mời bạn lựa chọn nhãn hiệu"
                                            v-model="objRequest.manufacturerIds"
                                            :default-expanded-level="Infinity" />
                            </b-form-group>
                        </b-card>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <b-card class="mt-3" header="Thông tin phụ">
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
                        </b-card>
                        <b-card header="Cấu hình">
                            <b-form-group>
                                <b-form-checkbox v-model="objRequest.isShowMenu">Hiển thị trên menu</b-form-checkbox>
                            </b-form-group>
                            <b-form-group label="Thứ tự">
                                <b-form-input v-model="objRequest.sortOrder" placeholder="Thứ tự" required></b-form-input>
                            </b-form-group>

                            <b-form-group label="Trạng thái">
                                <b-form-select v-model="objRequest.status" :options="Status"></b-form-select>
                            </b-form-group>
                        </b-card>
                        <b-card header="Hình ảnh">
                            <b-form-group label="Icon">
                                <b-form-group>
                                    <a @click="openImg('icon')">
                                        <div style="width:80px;height:80px;display:flex" class="gallery-upload-file ui-sortable">
                                            <div style="width:100%;height:100%;display:flex;margin:0" class=" r-queue-item ui-sortable-handle">
                                                <div style="width:100%" v-if="objRequest.icon != null && objRequest.icon != undefined &&  objRequest.icon.length > 0">
                                                    <img alt="Ảnh lỗi" style="height:100%;width:100%" :src="pathImgs(objRequest.icon)" class="preview-image img-thumbnail-full">
                                                </div>
                                                <div v-else>
                                                    <i class="fa fa-picture-o"></i>
                                                    <p>[Chọn ảnh]</p>
                                                </div>
                                            </div>

                                        </div>
                                        <!--<img style="margin-top:10px;width:30px;height:30px" :src="objRequest.icon" class="preview-image img-thumbnail">-->
                                    </a>
                                    <!--<b-form-file size="sm" v-on:change="updatePreviewIcon"></b-form-file>-->

                                </b-form-group>
                            </b-form-group>
                            <b-form-group label="Avatar">
                                <b-form-group>
                                    <a @click="openImg('avatar')">
                                        <div style="width:150px;display:flex" class="gallery-upload-file ui-sortable">
                                            <div style="width:100%;display:flex;margin:0" class=" r-queue-item ui-sortable-handle">
                                                <div style="width:100%" v-if="objRequest.avatar != null && objRequest.avatar != undefined &&  objRequest.avatar.length > 0">
                                                    <img alt="Ảnh lỗi" style="height:150px;width:150px" :src="pathImgs(objRequest.avatar)" class="preview-image img-thumbnail-full">
                                                </div>
                                                <div v-else>
                                                    <i class="fa fa-picture-o"></i>
                                                    <p>[Chọn ảnh]</p>
                                                </div>
                                            </div>

                                        </div>
                                        <!--<b-form-file id="file-field" size="sm" v-on:change="updatePreview"></b-form-file>-->
                                        <!--<img style="margin-top:10px" :src="objRequest.avatar" class="preview-image img-thumbnail">-->
                                    </a>
                                </b-form-group>
                            </b-form-group>
                        </b-card>
                    </div>
                </div>
            </b-tab>
            <b-tab v-if="objRequest.id > 0" title="2. Thông tin ngôn ngữ">
                <div class="row">
                    <div class="col-sm-6 col-md-8">
                        <b-card class="mt-3" header="Thông tin danh mục">
                            <b-form-group label="Ngôn ngữ">

                                <b-form-select v-model="langSelected" @change="onChangeSelectd" :options="Languages"></b-form-select>
                            </b-form-group>
                            <b-form-group label="Tên theo ngôn ngữ">
                                <b-form-input v-model="objRequestLanguage.name" placeholder="Tên danh mục" v-on:keyup.13="slugM()" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Đường dẫn">
                                <b-form-input v-model="objRequestLanguage.url" placeholder="Đường dẫn" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Đường dẫn cũ">
                                <b-form-input v-model="objRequestLanguage.urlOld" placeholder="Đường dẫn cũ" required></b-form-input>
                            </b-form-group>
                            <div class="row">
                                <div class="col-md-6">
                                    <b-form-group label="Breadcrumb thay thế">
                                        <b-form-input v-model="objRequestLanguage.breadcrumbFirst" placeholder="Dùng để thay thế text breadcrumb" required></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-6">
                                    <b-form-group label="Url Breadcrumb thay thế">
                                        <b-form-input v-model="objRequestLanguage.breadcrumUrl" placeholder="Dùng để thay thế url breadcrumb" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </div>

                            <b-form-group label="Mô tả">
                                <ckeditor tag-name="textarea"
                                          v-model="objRequestLanguage.description" :config="editorConfig">
                                </ckeditor>
                            </b-form-group>
                            <b-form-group label="Nội dung">
                                <b-form-group label="Nội dung">
                                    <MIEditor :contentEditor="objRequestLanguage.content" v-on:handleEditorInput="getOrSetData"></MIEditor>
                                </b-form-group>
                            </b-form-group>

                            <b-form-group label="Banner Link">
                                <b-form-input v-model="objRequestLanguage.bannerLink" placeholder="Đường dẫn khi kích vào BannerLink" required></b-form-input>
                            </b-form-group>
                        </b-card>
                        <div class="card">
                            <div class="card-header">
                                SEO Analysis
                                <b-btn v-b-toggle.collapseSEO variant="primary" class="pull-right"><i class="fa fa-angle-double-down" aria-hidden="true"></i></b-btn>

                            </div>
                            <b-collapse id="collapseSEO" class="mt-2">
                                <div class="card-body">
                                    <b-form-group label="Tiêu đề SEO">
                                        <b-form-input v-model="objRequestLanguage.metaTitle" placeholder="Tiêu đề SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Từ khóa SEO">
                                        <b-form-input v-model="objRequestLanguage.metaKeyword" placeholder="Từ khóa SEO" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Mô tả">
                                        <b-form-textarea v-model="objRequestLanguage.metaDescription" placeholder="Mô tả" rows="3" max-rows="6" required></b-form-textarea>
                                    </b-form-group>
                                    <b-form-group label="Script WebPage">
                                        <b-form-textarea rows="3" max-rows="6" v-model="objRequestLanguage.metaWebPage" placeholder="WebPage" required></b-form-textarea>
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
                                    <b-form-group label="Giá trị (Canonical)">
                                        <b-form-input placeholder="Canonical" v-model="valueCanonical" required></b-form-input>
                                    </b-form-group>


                                </div>
                            </b-collapse>
                        </div>
                    </div>

                    <div class="col-sm-6 col-md-4">
                        <b-card class="mt-3" header="Thông tin phụ">
                            <div class="row">
                                <div class="col-md-6">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEditLang()">
                                        <i class="fa fa-save"></i> Cập nhật
                                    </button>
                                </div>
                                <div class="col-md-6">
                                    <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                        <i class="fa fa-refresh"></i> Làm mới
                                    </button>
                                </div>
                            </div>
                        </b-card>

                    </div>
                </div>

            </b-tab>
        </b-tabs>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>
</template>


<script>

    import "vue-loading-overlay/dist/vue-loading.css";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    //import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import 'vue-select/dist/vue-select.css';
    import { unflatten, slug, pathImg, unflatten2 } from "../../plugins/helper";
    import FileManager from './../../components/fileManager/list'
    import MIEditor from '../../components/editor/MIEditor';
    import EventBus from "./../../common/eventBus";

    export default {
        name: "zoneedit",
        data() {
            return {
                //editor: ClassicEditor,
                mikey1: 'mikey1',
                isLoading: false,
                statusNoindex: false,
                statusCanonical: false,
                valueCanonical: "",
                //preview: '/assets/img/unnamed.jpg',
                objRequest: {
                    id: 0,
                    type: 0,
                    status: 1,
                    avatar: '',
                    background: '',
                    banner: '',
                    icon: '',
                    manufacturerId: '',
                    manufacturerIds: [],
                    zoneSearchType : 0
                    

                },
                Type: 0,
                
                SearchTypes: [
                    { value: 0, text: 'Tất cả' },
                    { value: 1, text: 'Theo thương hiệu' },
                    { value: 2, text: 'Theo khoảng giá' }
                    
                ],
                langSelected: "",
                objRequestLanguage: {
                    name: "",
                    url: ""
                },
                objRequestLanguages: [],
                Languages: [],
                Types: [

                ],
                Status: [

                ],
                ZoneOptions: [

                ],
                ZoneByType: [],
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },

                Manufacturer: []
            };
        },
        created() {

            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                debugger
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode.trim(),
                        text: item.name
                    }
                });
            });

            this.getAllManufacturers().then(response => {
                debugger
                this.Manufacturer = response.listData.map(x => ({
                    id: x.id,
                    label: x.name
                }));
            });

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



        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        components: {
            Loading,
            Treeselect,
            FileManager,
            MIEditor,
        },
        computed: {


        },
        mounted() {
            this.onChangePaging();


            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getZone(this.$route.params.id).then(respose => {
                    if (respose.data != null) {
                        this.objRequest = respose.data;
                        if (this.objRequest != null) {
                            this.Type = this.objRequest.type;
                            if (this.objRequest.manufacturerId != null) {
                                this.objRequest.manufacturerIds = this.objRequest.manufacturerId.split(",");
                            }

                        }

                    }
                    if (respose.listData != null) {
                        this.objRequestLanguages = respose.listData;
                        if (this.objRequestLanguages != null && this.objRequestLanguages.length > 0) {
                            this.objRequestLanguage = this.objRequestLanguages[0];
                            if (this.objRequestLanguage.metaNoIndex != null) {
                                let objNoIndex = JSON.parse(this.objRequestLanguage.metaNoIndex);
                                this.statusNoindex = objNoIndex.Status;
                            }
                            if (this.objRequestLanguage.metaCanonical != null) {
                                let objCanonical = JSON.parse(this.objRequestLanguage.metaCanonical);
                                this.statusCanonical = objCanonical.Status;
                                this.valueCanonical = objCanonical.Value;
                            }
                        } else {
                            this.defaultObj();
                            this.statusNoindex = false;
                            this.statusCanonical = false;
                            this.valueCanonical = "";
                            this.objRequestLanguage.languageCode = this.langSelected || "vi-VN"
                        }
                    }
                    debugger
                    this.langSelected = this.objRequestLanguage.languageCode.trim();

                });



                this.isLoading = false;
            }
        },

        methods: {
            ...mapActions(["updateZone", "getAllManufacturers", "addZone", "getZone", "getAllLanguages", "getZones", "mergeZoneLang", "supportsZone"]),
            pathImgs(path) {
                return pathImg(path);
            },
            getOrSetData(value) {
                this.objRequestLanguage.content = value;
            },
            openImg(img) {
                this.choseImg = img;

                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file

            },

            DoAttackFile(value) {
                let vm = this;
                if (this.choseImg == "avatar") {
                    vm.objRequest.avatar = value[0].path;
                } else if (this.choseImg == "icon") {

                    vm.objRequest.icon = value[0].path;
                }
                else if (this.choseImg == "bg") {
                    //debugger-
                    vm.objRequest.background = value[0].path;
                }
                else if (this.choseImg == "banner") {
                    vm.objRequest.banner = value[0].path;
                }
            },
            slugM: function () {
                //debugger-
                if (this.objRequestLanguage != null && this.objRequestLanguage != undefined) {
                    this.objRequestLanguage.url = slug(this.objRequestLanguage.name);
                }

            },
            onLoadZoneByType(type) {
                //debugger-
                this.ZoneByType = [];
                this.ZoneByType.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                if (type != null && type != undefined && type > 0) {
                    let vm = this;
                    var data = this.ZoneOptions.filter(x => x.type == type);
                    if (data !== null && data != undefined && data.length > 0) {
                        data.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                        var utu = data.reduce(function (ListNew, obj) {
                            ListNew.push(obj);
                            return ListNew;
                        }, []);
                        vm.ZoneByType = unflatten(utu);
                    }
                }
                this.objRequest.type = type;
            },
            onChangePaging() {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getZones(0).then(respose => {
                    try {
                        var data = respose.listData;
                        //data.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                        this.ZoneOptions = data;
                    }
                    catch (ex) {

                    }
                });


                this.isLoading = false;
            },

            onChangeSelectd() {

                if (this.objRequestLanguages != null && this.objRequestLanguages.length > 0) {
                    let lang = this.langSelected || "vi-VN";
                    let lstObjLang = this.objRequestLanguages.filter(function (item) {
                        return item.languageCode.trim() === lang.trim()
                    });
                    if (lstObjLang != null && lstObjLang != undefined && lstObjLang.length > 0) {
                        this.objRequestLanguage = lstObjLang[0];
                        let objNoIndex = JSON.parse(this.objRequestLanguage.metaNoIndex);
                        this.statusNoindex = objNoIndex.Status;
                        let objCanonical = JSON.parse(this.objRequestLanguage.metaCanonical);
                        this.statusCanonical = objCanonical.Status;
                        this.valueCanonical = objCanonical.Value;
                    } else {
                        this.defaultObj();
                        this.statusNoindex = false;
                        this.statusCanonical = false;
                        this.valueCanonical = "";
                    }
                    this.objRequestLanguage.languageCode = lang;
                }
            },
            defaultObj: function () {
                this.objRequestLanguage = Object.assign({}, this.objRequestLanguage);

                this.objRequestLanguage.name = "";
                this.objRequestLanguage.url = "";
                this.objRequestLanguage.content = "";
                this.objRequestLanguage.description = "";
            },


            DoAddEdit() {
                this.objRequest.manufacturerId = this.objRequest.manufacturerIds.join();

                if (this.objRequest.id > 0) {
                    this.updateZone(this.objRequest)
                        .then(response => {
                            //debugger-
                            if (response.success == true) {
                                this.$toast.success(response.message, {});

                            }
                            else {
                                this.$toast.error(response.message, {});

                            }
                        })
                        .catch(e => {
                            this.$toast.error(response.error + ". Error:" + e, {});

                        });
                } else {
                    this.addZone(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.id;
                                this.$router.push({
                                    path: "/admin/zone/edit/" + response.id,
                                });
                            }
                            else {
                                this.$toast.error(response.message, {});

                            }
                        })
                        .catch(e => {
                            this.$toast.error(response.error + ". Error:" + e, {});

                        });
                }
            },
            DoAddEditLang() {
                this.objRequestLanguage.zoneId = this.objRequest.id;
                let objMetaNoIndex = {
                    Value: "",
                    Status: this.statusNoindex
                }
                let objMetaCanonical = {
                    Value: this.valueCanonical,
                    Status: this.statusCanonical
                }
                this.objRequestLanguage.metaNoIndex = JSON.stringify(objMetaNoIndex);
                this.objRequestLanguage.metaCanonical = JSON.stringify(objMetaCanonical);
                this.mergeZoneLang(this.objRequestLanguage)
                    .then(response => {
                        if (response.success == true) {
                            this.$toast.success(response.message, {});
                            //add lại vào
                            if (!this.objRequestLanguages.some(x => x.languageCode.trim() == this.objRequestLanguage.languageCode.trim())) {
                                this.objRequestLanguages.push(this.objRequestLanguage);
                            }
                        }
                        else {
                            this.$toast.error(response.message, {});

                        }
                    })
                    .catch(e => {
                        this.$toast.error(response.error + ". Error:" + e, {});

                    });
            },
        },

        watch: {
            Type: function (newVal) {
                //debugger-

                this.onLoadZoneByType(newVal);
            },
            ZoneOptions: function () {
                this.onLoadZoneByType(this.Type);
            }

        }
    };
</script>
<style>
</style>

<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <div class="row productedit">
            <div class="col-sm-12 col-md-12">
                <div class="card">
                    <div class="card-header">
                        Thông tin cấu hình Banner
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b-form-group label="Ngôn ngữ">
                                        <b-form-select v-model="objRequest.languageCode" :options="Languages"></b-form-select>
                                    </b-form-group>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <b-form-checkbox v-model="IsChose" style="padding-bottom:7px">
                                            Tên Banner (Tích chọn để thêm tên banner)
                                        </b-form-checkbox>
                                        <template v-if="IsChose==false">
                                            <treeselect :multiple="false"
                                                        :options="ListCodeBanger"
                                                        placeholder="Chọn mã banner"
                                                        v-model="objRequest.code"
                                                        :default-expand-level="Infinity" />

                                        </template>
                                        <template v-if="IsChose==true">
                                            <input class="form-control" v-model="objRequest.code" />
                                        </template>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Loại:</label>
                                        <select v-model="objRequest.type" class="form-control">
                                            <option value="1">Ảnh Banner</option>
                                            <option value="2">Ảnh Slide</option>

                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-2 col-xs-12">
                                    <button class="btn btn-info btn-submit-form" style="margin-top:32px" type="button" @click="DoAddEdit()">
                                        <i class="fa fa-save"></i> Cập nhật
                                    </button>
                                </div>
                            </div>

                            <template v-if="objRequest.type > 1">
                                <div class="row" style="margin-bottom:20px">
                                    <div class="col-md-12">
                                        <button @click="DoAdd()" type="button" class="btn btn-success">
                                            Thêm mới
                                        </button>
                                    </div>
                                </div>
                                <div style="position:relative ;border:2px dashed #dcd6d6;padding:20px;margin:0" class="row" v-for="(item,index) in objRequestDetails">
                                    <i @click="removeSlide(index)" style=" cursor:pointer ;font-size:30px; color:red;position:absolute;top:0;left:5px" class="fa fa-remove"></i>
                                    <div class="col-md-5">
                                        <b-form-group label="Tiêu đề">
                                            <b-form-input label="Tiêu đề" v-model="item.Title"
                                                          type="text"
                                                          placeholder="Tiêu đề"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Đường dẫn">
                                            <b-form-input v-model="item.Url"
                                                          type="text"
                                                          placeholder="Đường dẫn"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-2">
                                        <b-form-group label="Thứ tự">
                                            <b-form-input v-model="item.Order"
                                                          type="number"
                                                          placeholder="Thứ tự"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-1">
                                        <label>Hiển thị</label>
                                        <label class="switch switch-outline-primary-alt" style="margin-top:5px">
                                            <input v-model="item.Show" type="checkbox" class="switch-input" checked>
                                            <span class="switch-slider"></span>
                                        </label>
                                    </div>
                                    <div class="col-md-10">
                                        <b-form-group label="Mô tả ngắn">
                                            <ckeditor tag-name="textarea"
                                                 v-model="item.Description" :rows="3" :config="editorConfig">
                                            </ckeditor>
                                            <!--<MIEditor :contentEditor="item.Description" v-on:handleEditorInput="getOrSetData"></MIEditor>-->
                                            <!--<b-form-textarea v-model="objRequestDetail.Description" rows="2" max-rows="4" placeholder="Tiêu đề">
                                            </b-form-textarea>-->
                                        </b-form-group>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <b-form-group label="Thời gian bắt đầu">
                                                    <b-form-input type="date" v-model="item.DateStart"></b-form-input>
                                                </b-form-group>
                                            </div>
                                            <div class="col-md-6">
                                                <b-form-group label="Thời gian kết thúc">
                                                    <b-form-input type="date" v-model="item.DateEnd"></b-form-input>
                                                </b-form-group>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div style="cursor:pointer" @click="openImg(index)">
                                            <div style="width:100%;display:flex"
                                                 class="gallery-upload-file ui-sortable">
                                                <div style="width:100%;height:auto;margin:0"
                                                     class=" r-queue-item ui-sortable-handle">
                                                    <div style="width:100%"
                                                         v-if="item.Image != null && item.Image != undefined &&  item.Image.length > 0">
                                                        <img alt="Ảnh lỗi" style="height:100px;width:100%"
                                                             :src="pathImgs(item.Image)">
                                                    </div>
                                                    <div v-else>
                                                        <i class="fa fa-picture-o"></i>
                                                        <p>[Chọn ảnh]</p>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </template>
                            <template v-if="objRequest.type <= 1">
                                <div style="position:relative ;border:2px dashed #dcd6d6;padding:20px;margin:0" class="row">
                                    <div class="col-md-5">
                                        <b-form-group label="Tiêu đề">
                                            <b-form-input label="Tiêu đề" v-model="objRequestDetail.Title"
                                                          type="text"
                                                          placeholder="Tiêu đề"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Đường dẫn">
                                            <b-form-input v-model="objRequestDetail.Url"
                                                          type="text"
                                                          placeholder="Đường dẫn"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-2">
                                        <b-form-group label="Thứ tự">
                                            <b-form-input v-model="objRequestDetail.Order"
                                                          type="number"
                                                          placeholder="Thứ tự"></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-1">
                                        <label>Hiển thị</label>
                                        <label class="switch switch-outline-primary-alt" style="margin-top:5px">
                                            <input v-model="objRequestDetail.Show" type="checkbox" class="switch-input" checked>
                                            <span class="switch-slider"></span>
                                        </label>
                                    </div>
                                    <div class="col-md-10">
                                        <b-form-group label="Mô tả ngắn">
                                            <ckeditor tag-name="textarea"
                                                      v-model="objRequestDetail.Description" :rows="3" :config="editorConfig">
                                            </ckeditor>
                                            <!-- <MIEditor :contentEditor="objRequestDetail.Description" v-on:handleEditorInput="getOrSetData"></MIEditor> -->
                                             <!--<b-form-textarea v-model="objRequestDetail.Description" rows="2" max-rows="4" placeholder="Tiêu đề">
                                             </b-form-textarea>--> 
                                        </b-form-group>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <b-form-group label="Thời gian bắt đầu">
                                                    <b-form-input type="date" v-model="objRequestDetail.DateStart"></b-form-input>
                                                </b-form-group>
                                            </div>
                                            <div class="col-md-6">
                                                <b-form-group label="Thời gian kết thúc">
                                                    <b-form-input type="date" v-model="objRequestDetail.DateEnd"></b-form-input>
                                                </b-form-group>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div style="cursor:pointer" @click="openImg(99999)">
                                            <div style="width:100%;display:flex"
                                                 class="gallery-upload-file ui-sortable">
                                                <div style="width:100%;height:auto;margin:0"
                                                     class=" r-queue-item ui-sortable-handle">
                                                    <div style="width:100%"
                                                         v-if="objRequestDetail.Image != null && objRequestDetail.Image != undefined &&  objRequestDetail.Image.length > 0">
                                                        <img alt="Ảnh lỗi" style="height:100px;width:100%"
                                                             :src="pathImgs(objRequestDetail.Image)">
                                                    </div>
                                                    <div v-else>
                                                        <i class="fa fa-picture-o"></i>
                                                        <p>[Chọn ảnh]</p>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </template>
                        </b-form>
                        <div class="row">
                            <div class="col-md-3">

                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />

    </div>
</template>
<script>

    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import { slug, pathImg } from "../../plugins/helper";
    import FileManager from './../../components/fileManager/list'

    import EventBus from "./../../common/eventBus";

    import '@riophae/vue-treeselect/dist/vue-treeselect.css';
    import Treeselect from '@riophae/vue-treeselect';
    import MIEditor from '../../components/editor/MIEditor';
    export default {
        name: "configaddedit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                searchLanguageCode: "vi-VN",
                searchKeyCode: "",
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                configId: "",
                objRequest: {
                    type: 1,
                    languageCode: 'vi-VN',
                    code: ""
                },
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                mikey1: 'mikey1',

                IsChose: false,

                objRequestDetail: {
                    Order: 255,
                    Image: "",
                    Title: "",
                    Url: "",
                    Description: "",
                    DateStart: null,
                    DateEnd: null
                },
                objRequestDetails: [],
                langSelected: "",
                Languages: [],
                ConfigValueTypes: [],
                KeyImg: -1,
                ListCodeBanger: [
                ]
            };
        },
        created() {
            let vm = this;
            EventBus.$on('FileSelected', value => {
               
            });

            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode,
                        text: item.name
                    }
                });
            });
        },
        components: {
            Loading,
            Treeselect,
            FileManager,
            MIEditor
        },
        mounted() {
            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode.trim(),
                        text: item.name.trim()
                    }
                });
            });

            this.getBannerCode().then(response => {
                this.ListCodeBanger = response.map(x => ({
                    id: x.key,
                    label: x.value
                }));
                if (this.ListCodeBanger.length > 0) {

                    this.objRequest.code = this.ListCodeBanger[0].id;
                    this.onChange();
                }
            });
        },

        computed: {
            ...mapGetters(["config"]),
        },

        methods: {
            ...mapActions([
                "getBannerAds",
                "createBannerAds",
                "getAllLanguages",
                "getBannerCode"
            ]),

            removeSlide(index) {
                //debugger
                this.objRequestDetails.splice(index, 1);
            },
            DoAdd() {
                this.objRequestDetails.push({
                    Order: 255,
                    Image: "",
                    Description: ""
                })
            },
            pathImgs(path) {
                return pathImg(path);
            },
            openImg(img) {
                
                this.KeyImg = img;
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
          
            DoAttackFile(value) {
                let vm = this;
                if (this.KeyImg == 99999) {
                    //debugger
                    vm.objRequestDetail.Image = value[0].path;
                } else {
                    if (this.KeyImg >= 0) {
                        vm.objRequestDetails[this.KeyImg].Image = value[0].path;
                    }
                }
            },
            // getOrSetData(value) {
            //     //this.objRequestDetail.content = value;
            //     objRequestDetail.Description = value;
            // },
            onChange() {                
                let key = `${this.objRequest.languageCode}_${this.objRequest.code}`;
                this.getBannerAds(key).then(response => {
                    var data = response;
                    data.languageCode = this.objRequest.languageCode;
                    data.code = this.objRequest.code;
                    console.log(data.metaData)
                    //debugger
                    if (data.type == 1) {
                        this.objRequestDetail = JSON.parse(data.metaData);
                    } else {
                        this.objRequestDetails = JSON.parse(data.metaData);
                    }
                    this.objRequest = data;
                });
            },
            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.type > 0) {
                    if (this.objRequest.type > 1) {
                        this.objRequest.metaData = JSON.stringify(this.objRequestDetails);
                    } else {
                        this.objRequest.metaData = JSON.stringify(this.objRequestDetail);
                    }
                    this.createBannerAds(this.objRequest)
                        .then(response => {
                            //debugger
                            if (response.key == true) {
                                if (this.IsChose) {
                                    if (this.ListCodeBanger.some(x => x.key != this.objRequest.code)) {
                                        this.ListCodeBanger.push({
                                            key: this.objRequest.code,
                                            value: this.objRequest.code
                                        })
                                    }
                                }

                                this.$toast.success(response.value, {});
                                this.onChange();
                                this.isLoading = false;
                            }
                            else {
                                this.$toast.error(response.value, {});
                                this.isLoading = false;
                            }

                        })
                        .catch(e => {
                            this.$toast.error(msgNotify.error + ". Error:" + e, {});
                            this.isLoading = false;
                        });
                } else {
                    this.$toast.error("Bạn chưa chọn loại", {});
                    this.isLoading = false;
                }
            },
            onChangeSelectd() {
                if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                    let lang = this.langSelected || "vi-VN";
                    let lstObjLang = this.objRequestDetails.filter(function (item) {
                        return item.languageCode.trim() === lang.trim()
                    });
                    if (lstObjLang != null && lstObjLang != undefined && lstObjLang.length > 0) {
                        this.objRequestDetail = lstObjLang[0];
                    } else {
                        this.objRequestDetail = {};
                        this.objRequestDetail.languageCode = lang;
                    }
                }
                else {
                    let lang = this.langSelected;
                    this.objRequestDetail = {};
                    this.objRequestDetail.languageCode = lang;
                }
            },
            DoAddDetail() {
                this.objRequestDetail.ConfigGroupKey = this.configId;
                this.addConfigInLanguage(this.objRequestDetail)
                    .then(response => {
                        if (response.success == true) {

                            if (!this.objRequestDetails.some(x => x.languageCode == this.objRequestDetail.languageCode)) {
                                this.objRequestDetails.push(this.objRequestDetail);
                            }
                            this.$toast.success(response.message, {});
                            this.onChange();
                        }
                        else {
                            this.$toast.error(response.message, {});
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    });
            }
        },

        watch: {
            'objRequest.languageCode': function (newVal) {
                this.onChange();
            },
            'objRequest.code': function (newVal, oldVal) {
                this.onChange();
            }
        }
    };

</script>
<style>
</style>

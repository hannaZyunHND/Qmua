<template>
    <div class="productadd">
        <div class="row productedit">
            <div class="col-sm-6 col-md-6">
                <div class="card">
                    <div class="card-header">
                        Thông tin chính
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <b-form-group v-if="objRequest.id > 0" label="Mã thuộc tính (Nhập số)">
                                <b-form-input type="number" v-model="propertyId" placeholder="Mã thuộc tính" required disabled></b-form-input>
                            </b-form-group>
                            <b-form-group v-else label="Mã thuộc tính (Nhập số)">
                                <b-form-input type="number" v-model="propertyId" placeholder="Mã thuộc tính" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Tên thuộc tính">
                                <b-form-input v-model="objRequest.name" placeholder="Tên thuộc tính" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Loại">
                                <select v-model="objRequest.groupId" class="form-control">
                                    <option :value="item.key" v-for="item in GroupIds">
                                        {{item.value}}
                                    </option>
                                </select>
                            </b-form-group>
                            <b-form-group label="Vị trí hiển thị">
                                <select class="form-control" v-model="objRequest.position">
                                    <option value="">Chọn vị trí</option>
                                    <option v-for="item in Positions" :value="item.key">{{item.value}}</option>
                                </select>

                                <!--<v-select v-model="objRequest.position" :options="Positions" :reduce="x=>x.key" label="value" placeholder="Chọn loại"></v-select>-->
                            </b-form-group>
                            <b-form-group label="Thumb">
                                <a @click="openImg('thumb')">
                                    <div style="width:30%;display:flex" class="gallery-upload-file ui-sortable">
                                        <div style="width:100%;height:auto;margin:0" class=" r-queue-item ui-sortable-handle">
                                            <div style="width:100%" v-if="objRequest.thumb != null && objRequest.thumb != undefined &&  objRequest.thumb.length > 0">
                                                <img alt="Ảnh lỗi" style="height:auto;width:100%" :src="pathImgs(objRequest.thumb)" class="preview-image img-thumbnail-full">
                                            </div>
                                            <div v-else>
                                                <i class="fa fa-picture-o"></i>
                                                <p>[Chọn ảnh]</p>
                                            </div>
                                        </div>

                                    </div>
                                </a>

                            </b-form-group>
                        </b-form>
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3">
                                <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEdit()">
                                    <i class="fa fa-save"></i> Cập nhật
                                </button>
                            </div>
                            <div class="col-md-3">
                                <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                    <i class="fa fa-refresh"></i> Làm mới
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="objRequest.id > 0" class="col-sm-6 col-md-6">
                <div class="card">
                    <div class="card-header">
                        Thông tin bổ xung
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <b-row>
                                <b-col>
                                    <b-form-group label="Ngôn ngữ">
                                        <b-form-select v-model="langSelected" @change="onChangeSelectd" :options="Languages"></b-form-select>
                                    </b-form-group>
                                </b-col>
                                <b-col></b-col>
                                <b-col></b-col>
                            </b-row>
                            <b-form-group label="Tên">
                                <b-form-input v-model="objRequestDetail.name" placeholder="Tên" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Mô tả">
                                <b-form-textarea v-model="objRequestDetail.content" :rows="3" placeholder="Mô tả" required></b-form-textarea>
                            </b-form-group>
                        </b-form>
                        <div class="row">
                            <div class="col-lg-offset-9"></div>

                            <div class="col-md-3">
                                <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddDetail()">
                                    <i class="fa fa-save"></i> Cập nhật
                                </button>
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
    import { mapGetters, mapActions } from "vuex";
  
    import EventBus from "./../../common/eventBus";
    import { unflatten, pathImg } from '../../plugins/helper';
    import FileManager from './../../components/fileManager/list'
    export default {
        name: "propertyedit",
        data() {
            return {
                mikey1: 'mikey1',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                propertyId: 0,
                objRequest: {
                    position: "",
                    id: 0
                },
                objRequestDetail: {
                    languageCode:"vi-VN     "
                },
                objRequestDetails: [],
                langSelected: "",
                Languages: [],
                GroupIds: [],
                Positions: []
            };
        },
        created() {
            let vm = this;
            EventBus.$on('FileSelected', value => {
                
            })
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        components: {
            FileManager
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getProperty(this.$route.params.id).then(respose => {
                    this.propertyId = respose.data.id;
                    this.objRequest = respose.data;
                    this.objRequestDetails = respose.listData;
                    if (respose.data.propertyLanguage != null) {
                        this.objRequestDetails = respose.listData;
                        if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                            this.objRequestDetail = this.objRequestDetails[0];
                        }
                    }
                    this.langSelected = this.objRequestDetail.languageCode.trim();
                });
                this.isLoading = false;
            };

            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode.trim(),
                        text: item.name.trim()
                    }
                });
            });
            this.propertyGetGroupId().then(reponse => {
                this.GroupIds = reponse.listData;
            });
            this.propertyGetPosition().then(reponse => {
                this.Positions = reponse.listData;
            });
        },

        computed: {
            ...mapGetters(["property"]),
        },

        methods: {
            ...mapActions([
                "getProperty",
                "addProperty",
                "updateProperty",
                "getAllLanguages",
                "addPropertyLanguage",
                "propertyGetGroupId",
                "propertyGetPosition"
            ]),
            pathImgs(path) {
                return pathImg(path);
            },


            async  openImg(img) {
                this.choseImg = img;
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
            DoAttackFile(value) {
                let vm = this;
                if (this.choseImg == "thumb") {
                    vm.objRequest.thumb = value[0].path;
                }
            },

            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id > 0) {
                    this.updateProperty(this.objRequest)
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
                    this.objRequest.id = this.propertyId;
                    this.addProperty(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.data.propertyId;
                                this.id = response.data.propertyId;
                                this.$router.push({
                                    path: "/admin/property/edit/" + response.data.propertyId,
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
                this.objRequestDetail.PropertyId = this.propertyId;

                this.addPropertyLanguage(this.objRequestDetail)
                    .then(response => {
                        if (response.success == true) {
                            if (!this.objRequestDetails.some(x => x.languageCode == this.objRequestDetail.languageCode)) {
                                this.objRequestDetails.push(this.objRequestDetail);
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
            }
        }
    };

</script>

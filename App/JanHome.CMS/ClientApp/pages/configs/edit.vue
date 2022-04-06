<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <div class="row productedit">
            <div class="col-sm-6 col-md-6">
                <div class="card">
                    <div class="card-header">
                        Thông tin chính
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <template v-if="objRequest.configGroupKey != null">
                                <b-form-group label="Mã cấu hình">
                                    <b-form-input v-model="configId" placeholder="Mã thuộc tính" required disabled></b-form-input>
                                </b-form-group>
                            </template>
                            <template v-else>
                                <b-form-group label="Mã cấu hình">
                                    <b-form-input v-model="configId" placeholder="Mã thuộc tính" required></b-form-input>
                                </b-form-group>
                            </template>
                            <b-form-group label="Tên cấu hình">
                                <b-form-input v-model="objRequest.configName" placeholder="Tên cấu hình" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Nhãn cấu hình">
                                <b-form-input v-model="objRequest.configLabel" placeholder="Nhãn cấu hình" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Giá trị cấu hình">
                                <b-form-input v-model="objRequest.configValue" placeholder="Giá trị cấu hình" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Loại">
                                <v-select v-model="objRequest.configValueType" :options="ConfigValueTypes" :reduce="x=>x.key" label="value" placeholder="Chọn loại"></v-select>
                            </b-form-group>
                            <b-form-group label="Trang">
                                <b-form-textarea v-model="objRequest.page" placeholder="Trang" required></b-form-textarea>
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
            <div v-if="objRequest.configGroupKey != null" class="col-sm-6 col-md-6">
                <div class="card">
                    <div class="card-header">
                        Thông tin bổ sung
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
                            <b-form-group label="Mô tả">
                                <b-form-textarea v-model="objRequestDetail.content" placeholder="Mô tả" required></b-form-textarea>
                            </b-form-group>
                        </b-form>
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
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
    </div>
</template>


<script>

    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";

    export default {
        name: "configaddedit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                configId: "",
                objRequest: {

                },
                objRequestDetail: {},
                objRequestDetails: [],
                langSelected: "",
                Languages: [],
                ConfigValueTypes: []
            };
        },
        created: {},
        components: {
            Loading
        },
        mounted() {
            if (this.$route.params.configGroupKey != "") {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getConfig(this.$route.params.configGroupKey).then(respose => {
                    this.configId = respose.data.configGroupKey;
                    this.objRequest = respose.data;
                    this.objRequestDetails = respose.listData;
                    if (respose.data.configInLanguage != null) {
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
            this.configTypeValueGet().then(reponse => {
                this.ConfigValueTypes = reponse.listData;
            });
        },

        computed: {
            ...mapGetters(["config"]),
        },

        methods: {
            ...mapActions([
                "getConfig",
                "addConfig",
                "updateConfig",
                "getAllLanguages",
                "addConfigInLanguage",
                "configTypeValueGet"
            ]),
            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.configGroupKey !== '' && this.objRequest.configGroupKey !== undefined) {
                    this.objRequest.configGroupKey = this.configId;
                    this.updateConfig(this.objRequest)
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
                    this.objRequest.ConfigGroupKey = this.configId;
                    this.addConfig(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.configGroupKey = response.data.configGroupKey;
                                this.$router.push({
                                    path: "/admin/config/edit/" + response.data.configGroupKey,
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
                this.objRequestDetail.ConfigGroupKey = this.configId;
                this.addConfigInLanguage(this.objRequestDetail)
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
<style>
</style>

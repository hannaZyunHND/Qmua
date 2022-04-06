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
                            <b-form-group label="Mã khu vực">
                                <b-form-input v-model="objRequest.code" placeholder="Mã khu vực" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Tên khu vực">
                                <b-form-input v-model="objRequest.name" placeholder="Tên khu vực" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Ghi chú">
                                <b-form-textarea v-model="objRequest.note" placeholder="Ghi chú" required></b-form-textarea>
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
                            <b-form-group label="Tên theo ngôn ngữ">
                                <b-form-input v-model="objRequestDetail.name" placeholder="Tên" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Tên theo ngôn ngữ">
                                <b-form-input v-model="objRequestDetail.url" placeholder="Đường dẫn" required></b-form-input>
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
        name: "propertyaddedit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                locationId: 0,
                objRequest: {

                },
                objRequestDetail: {},
                objRequestDetails: [],
                langSelected: "",
                Languages: []
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
                this.getLocation(this.$route.params.id).then(respose => {
                    this.locationId = respose.data.id;
                    this.objRequest = respose.data;
                    this.objRequestDetails = respose.listData;
                    if (respose.data.locationInLanguage != null) {
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
        },

        computed: {
            ...mapGetters(["location"]),
        },

        methods: {
            ...mapActions([
                "getLocation",
                "addLocation",
                "updateLocation",
                "getAllLanguages",
                "addLocationInLanguage",
            ]),
            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id > 0) {
                    this.updateLocation(this.objRequest)
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
                    this.addLocation(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.data.locationId;
                                this.locationId = response.data.locationId;
                                this.$router.push({
                                    path: "/admin/location/edit/" + response.data.locationId,
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
                this.objRequestDetail.LocationId = this.locationId;
                this.addLocationInLanguage(this.objRequestDetail)
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

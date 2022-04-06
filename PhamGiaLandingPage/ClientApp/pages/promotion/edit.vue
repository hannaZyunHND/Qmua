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
                            <b-form-group label="Tên khuyến mãi">
                                <b-form-input v-model="objRequest.name" placeholder="Mã sản phẩm" required></b-form-input>
                            </b-form-group>

                            <div class="row">
                                <div class="col-md-6">
                                    <b-form-group label="Số tiền">
                                        <b-form-input v-model="objRequest.value" placeholder="Số tiền" required type="number"></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-6">
                                    <b-form-group label="Loại">
                                        <v-select v-model="objRequest.type" :options="Promotions" :reduce="x=>x.key" label="value" placeholder="Chọn loại"></v-select>
                                    </b-form-group>
                                </div>
                            </div>
                            <b-form-group>
                                <b-form-checkbox v-model="objRequest.isDiscountPrice">Trừ tiền mặt</b-form-checkbox>
                            </b-form-group>
                            <div class="row">
                                <div class="col-md-6">
                                    <b-form-group label="Ngày bắt đầu">
                                        <b-form-input v-model="objRequest.startDate" placeholder="Ngày bắt đầu" type="date"></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-6">
                                    <b-form-group label="Ngày kết thúc">
                                        <b-form-input v-model="objRequest.endDate" placeholder="Ngày kết thúc" type="date"></b-form-input>
                                    </b-form-group>
                                </div>
                            </div>
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
                            <b-form-group label="Tên khuyến mãi">
                                <b-form-input v-model="objRequestDetail.name" placeholder="Tên khuyến mãi" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Mô tả">
                                <b-form-input v-model="objRequestDetail.description" placeholder="Mô tả" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Đường dẫn">
                                <b-form-input v-model="objRequestDetail.url" placeholder="Đường dẫn"></b-form-input>
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

    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
   
    import moment from 'moment';
    export default {
        name: "promotionaddedit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                loading: true,
                objRequest: {
                    isDiscountPrice: false,
                    startDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    endDate: moment(String(new Date())).format('YYYY-MM-DD'),
                },
                objRequestDetail: {},
                objRequestDetails: [],
                langSelected: "",
                Languages: [],
                Promotions: []
            };
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getPromotion(this.$route.params.id).then(respose => {
                    this.objRequest = respose.data;
                    this.objRequest.startDate = moment(respose.data.startDate).format('YYYY-MM-DD');
                    this.objRequest.endDate = moment(respose.data.endDate).format('YYYY-MM-DD');
                    if (respose.listData != null && respose.listData.length > 0) {
                        this.objRequestDetails = respose.listData;
                        if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                            this.objRequestDetail = this.objRequestDetails[0];
                        }
                    }
                    this.langSelected = this.objRequestDetail.languageCode;
                });
                this.isLoading = false;
            };

            this.getAllLanguages().then(respose => {
                let lang = respose.listData;
                this.Languages = lang.map(function (item) {
                    return {
                        value: item.languageCode,
                        text: item.name.trim()
                    }
                });
            });
            this.promotiontTypeGet().then(reponse => {
                this.Promotions = reponse.listData;
            });
        },

        methods: {
            ...mapActions([
                "getPromotion",
                "addPromotion",
                "updatePromotion",
                "getAllLanguages",
                "addPromotionInLanguage",
                "promotiontTypeGet"
            ]),
            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id > 0) {
                    this.updatePromotion(this.objRequest)
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
                    this.addPromotion(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.data.promotionId;
                                this.$router.push({
                                    path: "/admin/promotion/edit/" + response.data.promotionId,
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
                    let lang = this.langSelected || "vi-VN     ";
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
                this.objRequestDetail.PromotionId = this.objRequest.id;

                this.addPromotionInLanguage(this.objRequestDetail)
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

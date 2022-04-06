<template>
    <div class="productadd">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <div class="row productedit">
            <div class="col-md-12 col-sd-12">
                <div class="card">
                    <div class="card-header">
                        Thông tin chính
                    </div>
                    <div class="card-body">
                        <b-form class="form-horizontal">
                            <div class="row">
                                <div class="col-md-2 col-sm-3">
                                    <b-form-group label="Hình ảnh">
                                        <div style="cursor:pointer" @click="openImg(99999)">
                                            <div style="width:100%;display:flex"
                                                 class="gallery-upload-file ui-sortable">
                                                <div style="width:100%;height:auto;margin:0"
                                                     class=" r-queue-item ui-sortable-handle">
                                                    <div style="width:100%"
                                                         v-if="objRequest.avatar != null && objRequest.avatar != undefined &&  objRequest.avatar.length > 0">
                                                        <img alt="Ảnh lỗi" style="height:100px;width:100%"
                                                             :src="pathImgs(objRequest.avatar)">
                                                    </div>
                                                    <div v-else>
                                                        <i class="fa fa-picture-o"></i>
                                                        <p>[Chọn ảnh]</p>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </b-form-group>
                                </div>
                                <div class="col-md-10 col-sm-10">
                                    <b-form-group label="Tên ngân hàng">
                                        <b-form-input v-model="objRequest.name" placeholder="Tên ngân hàng" required></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Đường dẫn">
                                        <b-form-input v-model="objRequest.url" placeholder="Đường dẫn" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </div>
                            <b-row>
                                <div class="col-md-3 col-sm-3">
                                    <b-form-group label="Loại thẻ">
                                        <v-select v-model="objRequest.type" :options="ListType" :reduce="x=>x.key" label="value" placeholder="Chọn loại thẻ"></v-select>
                                    </b-form-group>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <b-form-group label="Phí quẹt thẻ">
                                        <b-form-input v-model="objRequest.charge" placeholder="Phí quẹt thẻ" required></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <b-form-group label="Số tháng trả góp">
                                        <v-select v-model="infoCardDataDetail.monthNumber" :options="ListMonthNumber" :reduce="x=>x.key" label="value" placeholder="Chọn số tháng" :on-change="MonthNumberChange"></v-select>
                                    </b-form-group>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <b-form-group label="Phần trăm lãi suất">
                                        <b-form-input v-model="infoCardDataDetail.interestRate" placeholder="Phần trăm lãi suất" required></b-form-input>
                                    </b-form-group>
                                </div>
                            </b-row>
                        </b-form>
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEdit()">
                                    <i class="fa fa-save"></i> Cập nhật
                                </button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                    <i class="fa fa-refresh"></i> Làm mới
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

    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import FileManager from './../../components/fileManager/list'
    import EventBus from "./../../common/eventBus";
    import { validBreakpoints } from "../../common/classes";
import debounce from "../../services/debounce";

    export default {
        name: "bankinstalledit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                currentSort: "Id",
                currentSortDir: "asc",
                monthNumber: "",
                interestRate: "",
                loading: true,
                locationId: 0,
                objRequest: {
                    avatar: ""
                },
                langSelected: 1,
                ListType: [],
                KeyImg: -1,
                mikey1: 'mikey1',
                ListMonthNumber: [
                    { key: 3, value: 3 },
                    { key: 6, value: 6 },
                    { key: 9, value: 9 },
                    { key: 12, value: 12 }
                ],
                infoCardDataDetail: {},
                InfoCardData: {
                    InfoCard: [

                    ]
                }
            };
        },
        created() {
            let vm = this;
            EventBus.$on('FileSelected', value => {

            });
        },
        components: {
            Loading,
            FileManager
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getBankInstallment(this.$route.params.id).then(respose => {
                    this.objRequest = respose;
                    this.InfoCardData = JSON.parse(respose.infoCard);
                    console.log(this.InfoCardData);
                });
                this.isLoading = false;
            };

            this.getCardType().then(respose => {
                this.ListType = respose
            });
        },

        computed: {
            ...mapGetters(["bankinstallment"]),
        },

        methods: {
            ...mapActions([
                "getBankInstallment",
                "createBankInstallment",
                "getCardType"
            ]),
            DoAddEdit() {
                this.isLoading = true;
                let dataInput = this.infoCardDataDetail;
                if (this.InfoCardData.InfoCard.length > 0) {
                    for (let index = 0; index < this.InfoCardData.InfoCard.length; index++) {
                        if (this.InfoCardData.InfoCard[index].MonthNumber == dataInput.monthNumber) {
                            this.InfoCardData.InfoCard[index].InterestRate = dataInput.interestRate;
                        } 
                    }
                } else {
                    this.InfoCardData.InfoCard.push(this.infoCardDataDetail);
                }
                this.objRequest.infoCard = JSON.stringify(this.InfoCardData);
                this.createBankInstallment(this.objRequest)
                    .then(response => {
                        if (response.key == true) {
                            this.$toast.success(response.value, {});
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
            openImg(img) {

                this.KeyImg = img;
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
            DoAttackFile(value) {
                let vm = this;
                if (this.KeyImg == 99999) {
                    vm.objRequest.avatar = value[0].path;
                } else {
                    if (this.KeyImg >= 0) {
                        vm.objRequest[this.KeyImg].avatar = value[0].path;
                    }
                }
            },
            pathImgs(path) {
                let url = 'https://cmsenterbuy.migroup.asia/' + "uploads/thumb" + path;
                return url;
            },
            MonthNumberChange() {
                let dataInfoCard = this.InfoCardData;
                let data = this.infoCardDataDetail;
                let dataCheck = dataInfoCard.InfoCard;
                if (dataCheck.length > 0) {
                    for (let index = 0; index < dataCheck.length; index++) {
                        if (dataCheck[index].MonthNumber == data.monthNumber) {
                            this.infoCardDataDetail.interestRate = dataCheck[index].InterestRate;
                        } else {
                            this.infoCardDataDetail.interestRate = 0;
                        }
                    }
                }
            }
        },
        watch: {
            'infoCardDataDetail.monthNumber': function (newVal) {
                this.MonthNumberChange();
            }
        }
    };

</script>
<style>
</style>

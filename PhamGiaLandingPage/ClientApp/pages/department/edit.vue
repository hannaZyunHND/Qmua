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
                            <b-row>
                                <b-col>
                                    <b-form-group label="Chọn vị trí">
                                        <select class="form-control" v-model="objRequest.locationId">
                                            <option value="0">Chọn khu vực</option>
                                            <option :value="item.id" v-for="item in Locations">{{item.name}}</option>
                                        </select>

                                        <!--<v-select :options="Locations" :reduce="x=>x.id" label="name" placeholder="Chọn vị trí"></v-select>-->
                                    </b-form-group>
                                </b-col>
                                <b-col></b-col>
                                <b-col></b-col>
                            </b-row>
                            <b-form-group label="Tên">
                                <b-form-input v-model="objRequest.name" placeholder="Tên hiển thị" required></b-form-input>
                            </b-form-group>

                            <div class="row">
                                <div class="col-md-6">
                                    <b-form-group label="Kinh độ">
                                        <b-form-input v-model="objRequest.longitude" placeholder="Kinh độ" required type="number"></b-form-input>
                                    </b-form-group>
                                </div>
                                <div class="col-md-6">
                                    <b-form-group label="Vĩ độ">
                                        <b-form-input v-model="objRequest.latitude" placeholder="Vĩ độ" required type="number"></b-form-input>
                                    </b-form-group>
                                </div>
                            </div>
                            <b-form-group label="Email">
                                <b-form-input v-model="objRequest.email" placeholder="Email" required type="text"></b-form-input>
                            </b-form-group>
                            <b-form-group label="Hotline">
                                <b-form-input v-model="objRequest.hotline" placeholder="Số điện thoại" required type="text"></b-form-input>
                            </b-form-group>
                            <div class="row">
                                <div class="col-md-3 col-xs-12">
                                    <b-form-group label="Sắp xếp">
                                        <b-form-input v-model="objRequest.sortOrder" placeholder="Sắp xếp" required type="number"></b-form-input>
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
                            <b-form-group label="Tên theo ngôn ngữ">
                                <b-form-input v-model="objRequestDetail.name" v-on:keyup.13="slugM()" placeholder="Tên" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Đường dẫn">
                                <b-form-input v-model="objRequestDetail.url" placeholder="Đường dẫn" required></b-form-input>
                            </b-form-group>
                            <b-form-group label="Địa chỉ">
                                <b-form-input v-model="objRequestDetail.address" placeholder="Địa chỉ" required></b-form-input>
                            </b-form-group>
                            <iframe width="500" height="300" :src="mapgoogle()" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe>
                        </b-form>
                        <div class="row">
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
    import { unflatten, slug, pathImg } from "../../plugins/helper";
    import { Function } from "core-js";
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
                departmentId: 0,
                objRequest: {
                    locationId: 0
                },
                objRequestDetail: {},
                objRequestDetails: [],
                langSelected: "",
                Languages: [],
                Locations: []
            };
        },

        components: {
            Loading
        },
        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getDepartment(this.$route.params.id).then(respose => {
                    this.departmentId = respose.data.id;
                    this.objRequest = respose.data;
                    this.objRequestDetails = respose.listData;
                    if (respose.data.departmentInLanguage != null) {
                        this.objRequestDetails = respose.listData;
                        if (this.objRequestDetails != null && this.objRequestDetails.length > 0) {
                            this.objRequestDetail = this.objRequestDetails[0];
                        } else {
                            this.defaultObj();
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

            this.getLocationAll().then(response => {
                this.Locations = response.listData;
            });
        },

        computed: {
            ...mapGetters(["departments"]),
        },

        methods: {
            ...mapActions([
                "getDepartment",
                "addDepartment",
                "updateDepartment",
                "getAllLanguages",
                "addDepartmentInLanguage",
                "getLocationAll"
            ]),

            mapgoogle: function () {
                let address = this.objRequestDetail.address.replace(" ", "+");
                return `https://maps.google.com/maps?width=100%&height=300&hl=en&q=(${address})&ie=UTF8&t=&z=14&iwloc=B&output=embed`;
            },
            defaultObj: function () {

                this.objRequestDetail = Object.assign({}, this.objRequestDetail);
                this.objRequestDetail.name = "";
                this.objRequestDetail.url = "";
                this.objRequestDetail.address = "";
            },
            slugM: function () {
                //debugger-
                if (this.objRequestDetail != null && this.objRequestDetail != undefined) {
                    //this.objRequestDetail.url = "";
                    this.objRequestDetail.url = slug(this.objRequestDetail.name);
                }

            },

            DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id > 0) {
                    this.updateDepartment(this.objRequest)
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
                    this.objRequest.id = this.departmentId;
                    this.addDepartment(this.objRequest)
                        .then(response => {
                            if (response.success == true) {
                                this.$toast.success(response.message, {});
                                this.objRequest.id = response.data.departmentId;
                                this.departmentId = response.data.departmentId;
                                this.$router.push({
                                    path: "/admin/department/edit/" + response.data.departmentId,
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
                        this.defaultObj();
                        this.objRequestDetail.languageCode = lang;
                    }
                }
                else {
                    let lang = this.langSelected;
                    this.defaultObj();
                    this.objRequestDetail.languageCode = lang;
                }
            },
            DoAddDetail() {
                this.objRequestDetail.DepartmentId = this.departmentId;

                this.addDepartmentInLanguage(this.objRequestDetail)
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

        },
        watch: {
            'objRequestDetail.address': function (newVal) {
                this.mapgoogle();
            },

        }
    };

</script>
<style>
</style>

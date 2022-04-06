<template>

    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>

        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Nhập thông tin tài khoản" active>
                <div class="row productedit">
                    <div class="col-md-12">
                        <b-card class="mt-3 " header="Cập nhật tài khoản">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <b-form-group label="Email">
                                                <b-form-input v-model="objRequest.email" placeholder="Email"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Tên người dùng">
                                                <b-form-input v-model="objRequest.userName" placeholder="Tên người dùng"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Tên đầy đủ">
                                                <b-form-input v-model="objRequest.fullName" placeholder="Tên đầy đủ"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Địa chỉ">
                                                <b-form-input v-model="objRequest.address"
                                                              placeholder="Địa chỉ"></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="mt-3">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                                                type="button"
                                                                @click="DoAddEdit()">
                                                            <i class="fa fa-save"></i> Cập nhật
                                                        </button>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <button class="btn btn-success col-md-12 btncus" type="button"
                                                                @click="DoRefesh()">
                                                            <i class="fa fa-refresh"></i> Làm mới
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </b-form>
                        </b-card>
                    </div>
                </div>
            </b-tab>

        </b-tabs>
    </div>


</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import {mapGetters, mapActions} from "vuex";
    import Loading from "vue-loading-overlay";
    import {authenticationRepository} from "../../repository/authentication/authenticationRepository";
    import {router} from "../../router";
    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";

    export default {
        name: "EditUser",
        data() {
            return {
                //editor: ClassicEditor,
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                disabled: false,
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {},
                currentUser: null
            };
        },
        async created() {
            await this.GetById();
        },
        components: {
            Loading,
        },

        mounted() {
        },

        computed: {},
        watch: {},
        methods: {
            ...mapActions(["addUser","getById","updateUser"]),
            async GetById() {
                if (this.$route.params.id) {
                    var id = this.$route.params.id;
                    var response = await this.getById(id);
                    this.objRequest=response;
                    this.objRequest.Id = response.id;
                    this.isLoading = false;
                }

            },
            async DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id) {
                    var result = await this.updateUser(this.objRequest);
                    console.log(result);
                    if (result.success == true) {
                        this.$toast.success("tạo thành công", {});
                        this.isLoading = false;
                        this.$router.go(-1)
                    } else {
                        this.$router.go(-1);
                        this.$toast.error("cập nhật thất bại", {});
                        this.isLoading = false;
                    }
                } else {
                    var result = await this.addUser(this.objRequest);
                    console.log(result);
                    if (result.success == true) {
                        this.$toast.success("tạo thành công", {});
                        this.isLoading = false;
                        this.$router.go(-1)
                    } else {
                        this.$router.go(-1);
                        this.$toast.error("cập nhật thất bại", {});
                        this.isLoading = false;
                    }
                }
            },
            DoRefesh() {
                this.objRequest.Title = ""
            },
        }
    };
</script>

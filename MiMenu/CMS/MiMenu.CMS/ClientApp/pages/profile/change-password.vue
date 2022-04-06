<template>
    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage">

        </loading>
        <b-tabs class="col-md-12" pills>
            <b-tab title="Thay đổi mật khẩu" active>
                <div class="row productedit">
                    <div class="col-md-12">
                        <b-card class="mt-3 " header="Cập nhật mật khẩu">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <b-form-group label="Mật khẩu hiện tại">
                                                <b-form-input type="password" v-model="objRequest.oldPassword"
                                                              placeholder="Mật khẩu hiện tại"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Mật khẩu mới">
                                                <b-form-input type="password" v-model="objRequest.password"
                                                              placeholder="Mật khẩu mới"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Nhập lại mật khẩu mới">
                                                <b-form-input type="password"
                                                              v-model="objRequest.confirmPassword"
                                                              placeholder="Nhập lại mật khẩu mới"
                                                ></b-form-input>
                                                <p v-bind:style="{color:activeColor}"
                                                   v-if="objRequest.password != objRequest.confirmPassword">Bạn nhập
                                                    không khớp mật khẩu</p>
                                                <p v-else></p>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="mt-3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <button
                                                            :disabled="objRequest.password != objRequest.confirmPassword"
                                                            class="btn btn-info btn-submit-form col-md-12 btncus"
                                                            type="button"
                                                            @click="DoAddEdit()">
                                                            <i class="fa fa-save"></i> Cập nhật
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
    import {mapActions} from "vuex";
    import Loading from "vue-loading-overlay";
    import {authenticationRepository} from "../../repository/authentication/authenticationRepository";
    import {router} from "../../router";

    export default {
        name: "Profile",
        data() {
            return {
                disabled: false,
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {},
                currentUser: null,
                errorMessage: '',
                activeColor: 'red'
            };
        },
        async created() {
        },
        components: {
            Loading,
        },
        watch: {},
        methods: {
            ...mapActions(["changePassword"]),
            async DoAddEdit() {
                this.isLoading = true;
                const currentUser = authenticationRepository.currentUserValue;
                this.objRequest.userId = currentUser.id;
                if (this.objRequest.userId) {
                    var result = await this.changePassword(this.objRequest);
                    console.log(result);
                    if (result.success == true) {
                        authenticationRepository.logout();
                        this.$toast.success("cập nhật thành công", {});
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
            GetRouterChangePassword() {
                router.push('/admin/profile/change-password');
            }
        }
    };
</script>


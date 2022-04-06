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
                                    <div class="col-md-4">
                                        <b-form-group>
                                            <label>Ảnh đại diện</label>
                                            <b-form-file v-model="objRequest.avatar" id="file-field" size="sm"
                                                         v-on:change="updatePreview"></b-form-file>
                                            <img style="margin-top:10px" :src="preview"
                                                 class="preview-image img-thumbnail"
                                                 v-on:click="openUpload">
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="col-md-12">
                                            <b-form-group label="Email">
                                                <b-form-input v-model="objRequest.email" placeholder="Email"
                                                              readonly></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Tên người dùng">
                                                <b-form-input v-model="objRequest.userName" placeholder="Tên người dùng"
                                                              readonly></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Tên đầy đủ">
                                                <b-form-input v-model="objRequest.fullName" placeholder="Tên đầy đủ"
                                                ></b-form-input>
                                            </b-form-group>
                                        </div>
                                        <div class="col-md-12">
                                            <b-form-group label="Nhóm người dùng">
                                                <b-form-input v-model="objRequest.roles" placeholder="Nhóm người dùng"
                                                              readonly></b-form-input>
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
                                                    <div class="col-md-4">
                                                        <button class="btn btn-warning col-md-12 btncus" type="button"
                                                                @click="GetRouterChangePassword()">
                                                            <i class="fa fa-refresh"></i> Thay đổi mật khẩu
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

    export default {
        name: "Profile",
        data() {
            return {
                //editor: ClassicEditor,
                editorData: '<p>Content of the editor.</p>',
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: "",

                },
                disabled: false,
                selectedFile: null,
                preview: '/assets/img/unnamed.jpg',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {},
                LanguageCodes: [],
                LanguageValues: null,
                currentUser: null
            };
        },
        async created() {
            this.GetById();
        },
        components: {
            Loading,
        },

        mounted() {
        },

        computed: {
            ...mapGetters(["fileName"])
        },
        watch: {},
        methods: {
            ...mapActions(["uploadFile", "updateProfile"]),
            async GetById() {
                this.objRequest = await authenticationRepository.getCurrentUser();
                console.log(this.objRequest);
                if (this.objRequest.avatar) {
                    this.preview = this.objRequest.avatar;
                }
                this.isLoading = false;
            },
            async DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.id) {
                    var result = await this.updateProfile(this.objRequest);
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
            GetRouterChangePassword() {
                router.push('/admin/profile/change-password');
            },
            openUpload() {
                document.getElementById('file-field').click()
            },
            updatePreview(e) {
                var reader, files = e.target.files;
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }
                this.uploadFile(data).then(response => {
                    if (response.success == true) {
                        reader.onload = (e) => {
                            this.preview = e.target.result;
                            console.log(this.selectedFile);
                            this.objRequest.avatar = response.linkImage;
                        };
                        reader.readAsDataURL(files[0])
                    } else {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    }
                }).catch(e => {
                    this.$toast.error(msgNotify.error + ". Error:" + e, {});
                    this.isLoading = false;
                });
            },
        }
    };
</script>

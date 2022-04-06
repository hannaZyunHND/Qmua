<template>

    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Chỉnh sửa thông tin">
                <div class="row productedit">
                    <div class="col-md-12">
                        <b-card class="mt-3" header="Chọn nhóm người dùng">
                            <b-form-group label="Chọn nhóm người dùng">
                                <treeselect :options="Roles"
                                            :multiple="true"
                                            :disable-branch-nodes="true"
                                            v-model="roleValues"
                                            :default-expanded-level="Infinity"
                                            placeholder="Xin mời bạn lựa chọn nhóm người dùng"/>
                               
                            </b-form-group>
                        </b-card>
                    </div>
                    <div class="col-md-12">
                        <div class="mt-3">
                            <div class="row">
                                <div class="col-md-12">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit"
                                            @click="DoAddEdit()">
                                        <i class="fa fa-save"></i> Cập nhật
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </b-tab>
        </b-tabs>
    </div>
</template>


<script>
    import axios from 'axios';
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import {mapActions} from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'

    import 'vue-select/dist/vue-select.css';
    // Import component

    export default {
        name: "assignToRole",
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
                previewImageFacebook: '/assets/img/unnamed.jpg',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {},
                Roles: null,
                roleValues: []


            };
        },
        async created() {
            await this.getAllRoles();
            await this.getAllRoleName();
        },
        components: {
            Loading,
            Treeselect
        },

        mounted() {
        },

        computed: {},
        watch: {},
        methods: {
            ...mapActions(["getAllRole", "assignToRole", "getRoleName"]),
            DoAddEdit() {
                this.objRequest.id = this.$route.params.id;
                this.objRequest.roleName = this.roleValues;
                this.assignToRole(this.objRequest)
                    .then(response => {
                        if (response.success == true) {
                            this.$toast.success("đăng thành công", {});
                            this.isLoading = false;
                            this.$router.go(-1)
                        } else {
                            this.$router.go(-1);
                            this.$toast.error("cập nhật thất bại", {});
                            this.isLoading = false;
                        }
                    })
                    .catch(e => {
                        this.$toast.error(msgNotify.error + ". Error:" + e, {});
                        this.isLoading = false;
                    });
            },
            DoRefesh() {
                this.objRequest.Title = ""
            },
            async getAllRoles() {
                this.Roles = await this.getAllRole();
            },
            async getAllRoleName(){
                var data=this.$route.params.id;
                this.roleValues=await this.getRoleName(data);
            }
    }
    }
    ;
</script>

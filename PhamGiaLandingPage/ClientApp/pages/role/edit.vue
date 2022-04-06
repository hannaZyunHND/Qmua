<template>

    <div style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <div class="col-md-12">
            <b-card class="mt-3 " header="Thêm / Sửa nhóm người dùng">
                <b-form class="form-horizontal">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <b-form-group label="Tên nhà cung cấp">
                                    <b-form-input v-model="objRequest.name" placeholder="Tên nhóm người dùng"
                                                  required></b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-12">
                                <div class="mt-3">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <button class="btn btn-info btn-submit-form col-md-12 btncus" type="button"
                                                    @click="DoAddEdit()">
                                                <i class="fa fa-save"></i> Cập nhật
                                            </button>
                                        </div>
                                        <div class="col-md-6">
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


</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import {mapGetters, mapActions} from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'
   
    // Import component

    export default {
        name: "roleaddedit",
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
                objRequest: {
                    Id: null,
                    name: ""
                },
            };
        },
        async created() {
            await this.GetById()
        },
        components: {
            Loading,
            Treeselect
        },

        mounted() {
        },

        computed: {
            ...mapGetters(["article", "isOR", "fileName"])
        },
        watch: {
        },
        methods: {
            ...mapActions(["addRole", "getRoleById","updateRole"]),
            async GetById() {
                if (this.$route.params.id) {
                    let id = this.$route.params.id;
                    this.isLoading = true;
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    let response = await this.getRoleById(id);
                    console.log(response);
                    this.objRequest.Id = response.id;
                    this.objRequest.name = response.name;
                    this.isLoading = false;
                }
            },
            async DoAddEdit() {
                this.isLoading = true;
                if (this.objRequest.Id) {
                    var result = await this.updateRole(this.objRequest);
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
                    var result = await this.addRole(this.objRequest);
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

<template>
    <div style="display:flex;width:100%">

        <div class="col-md-8">
            <b-card class="mt-3" header="Thêm / Sửa ngôn ngữ">
                <loading :active.sync="isLoading"
                         :height="35"
                         :width="35"
                         :color="color"
                         :is-full-page="fullPage"></loading>
                <b-form class="form-horizontal">
                    <b-form-group label="Mã ngôn ngữ" label-for="input-1">
                        <b-form-input id="input-1"
                                      v-model="objRequest.LanguageCode"
                                      type="text"
                                      required
                                      placeholder="Mã ngôn ngữ"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Tên">
                        <b-form-input v-model="objRequest.Name" placeholder="Name" required></b-form-input>
                    </b-form-group>
                </b-form>
            </b-card>

        </div>
        <div class="col-md-4">
            <div class="mt-3" style="position:fixed;width:25%">
                <b-card header="Thao tác">
                    <div class="row">
                        <div class="col-md-6">
                            <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEdit()">
                                <i class="fa fa-save"></i> Cập nhật
                            </button>
                        </div>
                        <div class="col-md-6">
                            <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                <i class="fa fa-refresh"></i> Làm mới
                            </button>
                        </div>
                    </div>
                    <b-form-group style="margin-top:10px">
                        <b-form-checkbox v-model="objRequest.SetDefault">Kích hoạt</b-form-checkbox>
                    </b-form-group>
                </b-card>
                
            </div>
        </div>
    </div>
</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    // Import component

    export default {
        name: "languagedit",
        data() {
            return {
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {
                    SetDefault: false
                }
            };
        },
        created: {},
        components: {
            Loading
        },

        mounted() {
            if (this.$route.params.id.length > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getLanguage(this.$route.params.id).then(respose => {
                    this.objRequest = respose.Data;
                });
                this.isLoading = false;
            }

            
        },

        computed: {
            ...mapGetters(["language"]),

        },

        methods: {
            ...mapActions(["updateLanguage", "addLanguage", "getLanguage"]),
            DoAddEdit() {
                this.isLoading = true;
                this.addLanguage(this.objRequest)
                    .then(response => {
                        if (response.Success == true) {
                            this.$toast.success(response.Message, {});
                            this.isLoading = false;
                        }
                        else {
                            this.$toast.error(response.Message, {});
                            this.isLoading = false;
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

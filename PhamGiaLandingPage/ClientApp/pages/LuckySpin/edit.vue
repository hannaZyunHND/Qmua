<template>
    <div class="row" style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"></loading>
        <div class="col-md-8">
            <b-card class="mt-3" header="Thêm / Sửa ">
                <b-form class="form-horizontal">
                    <b-form-group label="Tên:" label-for="input-1">
                        <b-form-input id="input-1"
                                      v-model="spinObj.name"
                                      type="text"
                                      required
                                      placeholder="Tên giải thưởng"></b-form-input>
                    </b-form-group>


                    <!--<b-form-group label="Giá trị">
                        <b-form-input v-model="spinObj.value" type="text"  required placeholder=""></b-form-input>
                    </b-form-group>-->
                    <b-form-group label="Trạng thái">
                        <b-form-select v-model="spinObj.enable" :options="Types" required></b-form-select>
                    </b-form-group>
                    <b-form-group label="Tỉ lệ:" label-for="input-2">
                        <b-form-input id="input-2"
                                      v-model="spinObj.ratio"
                                      type="number"
                                      required
                                      placeholder="Tỉ lệ"></b-form-input>
                    </b-form-group>

                </b-form>
            </b-card>

        </div>
        <div class="col-md-4">
            <div class="mt-3" style="position:fixed;width:25%">
                <b-card header="Đăng bài">

                    <div class="row">
                        <div class="col-md-6">
                            <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" @click="DoAddEdit()">
                                <i class="fa fa-save"></i> Lưu
                            </button>
                        </div>
                        <div class="col-md-6">
                            <button class="btn btn-success col-md-12 btncus" type="button" @click="DoRefesh()">
                                <i class="fa fa-refresh"></i> Làm mới
                            </button>
                        </div>
                    </div>
                    <b-form-group style="margin-top:10px">
                        <b-form-checkbox v-model="spinObj.IsEnable">Kích hoạt</b-form-checkbox>
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
    

    export default {
        name: "AdsEdit",
        data() {
            return {
              
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                spinObj: {
                    enable: true,
                    value:0
                },
                Types: [
                    { value: true, text: "Hiển thị" },
                    { value: false, text: "Đóng" }
                  
                ]
            };
        },

        created() {

        },
        destroyed() {
       
        },
        components: {
            Loading,
           

        },

        mounted() {

            if (this.$route.params.id) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getObj(this.$route.params.id).then(respose => {
                
                    this.spinObj = respose.data;
                  
                });
                this.isLoading = false;
            }
        },

        computed: {
            ...mapGetters(["obj"]),
        },

        methods: {
            ...mapActions(["addSpin", "editSpin", "getObj"]),

          
            DoAddEdit() {
               
                this.isLoading = true;
                if (this.spinObj.id) {
                   
                    this.editSpin(this.spinObj)
                        .then(response => {
                           
                            if (response.success) {
                              
                                this.$toast.success(response.message, {});
                                this.spinObj = {};
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
                    this.addSpin(this.spinObj)
                        .then(response => {
                            if (response.success) {
                                this.spinObj = {};
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
                }
            },
           
        },
        watch: {
            ads: function () {
                this.spinObj = Object.assign({}, this.$store.getters.ads);
            }
        },
    };
</script>
<style>
</style>

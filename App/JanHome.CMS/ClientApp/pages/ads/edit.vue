<template>
    <div class="row" style="display:flex;width:100%">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"></loading>
        <div class="col-md-8">
            <b-card header="Đăng hình ảnh">
                <b-form-group>
                    <img style="margin-top:10px" :src="preview" class="preview-image img-thumbnail" v-on:click="FileManagerOpen1">
                </b-form-group>
                <b-form-group>
                    <img style="margin-top:10px" :src="preview1" class="preview-image img-thumbnail" v-on:click="FileManagerOpen2">
                </b-form-group>
            </b-card>
            <b-card class="mt-3" header="Thêm / Sửa quảng cáo">
                <b-form class="form-horizontal">
                    <b-form-group label="Tên chiến dịch:" label-for="input-1">
                        <b-form-input id="input-1"
                                      v-model="adsObj.Name"
                                      type="text"
                                      required
                                      placeholder="Tên chiến dịch"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Loại">
                        <b-form-select v-model="adsObj.Type" :options="Types" required></b-form-select>
                    </b-form-group>

                    <b-form-group label="Vị trí">
                        <b-form-input v-model="adsObj.Position" type="number" required placeholder="Vị trí"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Đường dẫn">
                        <b-form-input v-model="adsObj.Url" type="text" required placeholder="Đường dẫn"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Thứ tự">
                        <b-form-input v-model="adsObj.SortOrder" type="number" required placeholder="Thứ tự"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Nội dung">
                        <b-form-textarea v-model="adsObj.Content" placeholder="Nội dung" rows="3" max-rows="6"></b-form-textarea>
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
                        <b-form-checkbox v-model="adsObj.IsEnable">Kích hoạt</b-form-checkbox>
                    </b-form-group>
                </b-card>
              
            </div>


        </div>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1"/>
        <FileManager v-on:handleAttackFile="DoAttackFile2" :miKey="mikey2"/>
    </div>
</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import msgNotify from "./../../common/constant";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import FileManager from './../../components/fileManager/list'
    import EventBus from "./../../common/eventBus";
    // Import component

    export default {
        name: "AdsEdit",
        data() {
            return {
                mikey1:'mikey1',
                mikey2:'mikey2',
                selectedFile: null,
                preview: '/assets/img/unnamed.jpg',
                preview1: '/assets/img/unnamed.jpg',
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                adsObj: {
                    Id: 0,
                    Type: 0,
                    IsEnable: false
                },
                Types: [
                    { value: 0, text: "Chọn loại" },
                    { value: 1, text: "Loại thiết bị" },
                    { value: 2, text: "Loại sàn gỗ" }
                ]
            };
        },

        created() {
          
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        components: {
            Loading,
            'FileManager': FileManager,

        },

        mounted() {

            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getAds(this.$route.params.id).then(respose => {
                    this.adsObj = respose.Data;
                    if (this.adsObj.Thumb != null && this.adsObj.Thumb != undefined && this.adsObj.Thumb.length > 0) {
                        this.preview = this.adsObj.Thumb;
                    }
                });
                this.isLoading = false;
            }
        },

        computed: {
            ...mapGetters(["ads"]),
        },

        methods: {
            ...mapActions(["addAds", "editAds", "getAds"]),

            FileManagerOpen1() {
                EventBus.$emit(this.mikey1, ''); // '': select one, 'multi': select multi file
            },
            FileManagerOpen2() {
                EventBus.$emit(this.mikey2, ''); // '': select one, 'multi': select multi file
            },
            DoAttackFile(value) {
                this.preview = '/uploads' + value[0].path
            },
            DoAttackFile2(value) {
                this.preview1 = '/uploads' + value[0].path
            },
            DoAddEdit() {
                this.isLoading = true;
                var fromData = new FormData();
                fromData.append('image', this.selectedFile);
                fromData.append('ads', JSON.stringify(this.adsObj));
                if (this.adsObj.Id > 0) {
                    this.editAds(fromData)
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
                } else {
                    this.addAds(fromData)
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
            },
            openUpload() {
                document.getElementById('file-field').click()
            },
            updatePreview(e) {
                console.log('e', e)
                var reader, files = e.target.files
                if (files.length === 0) {
                    console.log('Empty')
                }
                this.selectedFile = files[0];
                reader = new FileReader();
                reader.onload = (e) => {
                    this.preview = e.target.result;

                    console.log(this.selectedFile);
                }
                reader.readAsDataURL(files[0])
            }
        },
        //watch: {
        //    ads: function () {
        //        this.adsObj = Object.assign({}, this.$store.getters.ads);
        //    }
        //},
    };
</script>
<style>
</style>

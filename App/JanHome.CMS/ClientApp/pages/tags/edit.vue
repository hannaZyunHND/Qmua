<template>
    <div style="display:flex;width:100%">

        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"></loading>
        <div class="col-md-8">
            <b-card class="mt-3" header="Thêm / Sửa Tag">
                <b-form class="form-horizontal">
                    <b-form-group label="Tên Tag:" label-for="input-1">
                        <b-form-input id="input-1"
                                      v-model="tagObj.Name"
                                      type="text"
                                      required
                                      placeholder="Tên Tag"></b-form-input>
                    </b-form-group>
                    <!--<b-form-group label="Tag Cha">
                        <v-select :options="TagParentId" v-model="tagObj.ParentId" :reduce="parent => parent.Id" label="Name"></v-select>

                    </b-form-group>-->


                    <b-form-group label="Url">
                        <b-form-input v-model="tagObj.Url" type="text" required placeholder="Url"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Loại">
                        <b-form-select v-model="tagObj.Type" :options="Types" required></b-form-select>
                        <!--<b-form-input v-model="tagObj.Type" type="number" required placeholder="Loại"></b-form-input>-->
                    </b-form-group>
                    <b-form-group label="Mô tả">
                        <b-form-textarea v-model="tagObj.Description" type="text" required placeholder="Mô tả"></b-form-textarea>
                    </b-form-group>

                    <!--<b-form-group label="Thứ tự">
                        <b-form-input v-model="departmentObj.SortOrder" type="number" required placeholder="Thứ tự"></b-form-input>
                    </b-form-group>

                    <b-form-group label="Nội dung">
                        <b-form-textarea v-model="departmentObj.Content" placeholder="Nội dung" rows="3" max-rows="6"></b-form-textarea>
                    </b-form-group>-->

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
                    <div class="row" style="margin-top:20px">
                        <div class="col-md-6 col-xs-12">
                            <b-form-checkbox v-model="tagObj.Invisibled">
                                Hiển thị
                            </b-form-checkbox>
                        </div>
                        <div class="col-md-6 col-xs-12">
                            <b-form-checkbox v-model="tagObj.IsHotTag">
                                Tag Hot
                            </b-form-checkbox>
                        </div>
                    </div>
                </b-card>
                <!--<b-card header="Thông tin thêm">
                    <b-form-group label="Ngôn ngữ">
                        <v-select :options="Language" v-model="departmentObj.LanguageCode" :reduce="location => location.LanguageCode" label="Name"></v-select>
                    </b-form-group>

                </b-card>-->

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
    // Import component

    export default {
        name: "TagEdit",
        data() {
            return {

                //selectedFile: null,

                //preview: '/assets/img/unnamed.jpg',


                isLoading: false,
                fullPage: false,
                color: "#007bff",
                tagObj: {
                    Id: 0,
                    ParentId: 0,
                    Name: "",
                    Description: "",
                    Invisibled: false,
                    IsHotTag: false,
                    Type: 0
                },
                Types: [
                    { value: 0, text: "Chọn loại" },
                    { value: 1, text: "Loại thiết bị" },
                    { value: 2, text: "Loại sàn gỗ" }
                ]
            };
        },
        created: {},
        components: {
            Loading
            //FileFields
        },

        mounted() {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getTag(this.$route.params.id).then(respose => {
                    this.tagObj = respose.Data;
                });
                this.isLoading = false;
            }
        },

        computed: {
            //...mapGetters(["ads"]),
        },

        methods: {
            ...mapActions(["addTag", "getTag", "editTag"]),
            DoAddEdit() {
                this.isLoading = true;
                var fromData = new FormData();
                //fromData.append('image', this.selectedFile);
                //fromData.append('tag', JSON.stringify(this.tagObj) );
                if (this.tagObj.Id > 0) {
                    this.editTag(this.tagObj)
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
                    console.log("Goi vao api");
                    console.log(this.tagObj);

                    this.addTag(this.tagObj)
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
        //        this.tagObj = Object.assign({}, this.$store.getters.ads);
        //    }
        //},
    };
</script>

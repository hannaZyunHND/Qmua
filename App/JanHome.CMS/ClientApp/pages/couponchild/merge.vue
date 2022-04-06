<template>
    <div style="display:flex;width:100%">
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Nhập thông tin mã giảm giá" active>
                <div class="row productedit">
                    <div class="col-md-8">
                        <b-card class="mt-3" header="Thêm / Sửa mã giảm giá">
                            <div class="form-group">
                                <select v-model="parrent" class="form-control">
                                    <option value="0">Chọn nhóm</option>
                                    <option :value="item.id" v-for="item in ListCoupon">{{item.code}} - {{item.name}}</option>
                                </select>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
                                </div>
                                <div class="col-md-3">
                                    <a class="btn btn-warning" href="/assets/DemoVoucher.xlsx">Tải File Mẫu</a>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <div class="dataTables_wrapper dt-bootstrap4 no-footer">
                                    <table class="table table-bordered table-hover" role="grid">
                                        <thead>
                                            <tr>
                                                <td>Mã</td>
                                                <td>Tên</td>
                                                <td>Ngày tạo</td>
                                                <td>Ngày hết hạn</td>
                                                <td>Email</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="item in tableData">
                                                <td>
                                                    <input class="form-control" v-model="item.Code" />
                                                </td>
                                                <td>
                                                    <input class="form-control" v-model="item.Name" />
                                                </td>
                                                <td>
                                                    <input class="form-control" type="date" v-model="item.CreateDate" />
                                                </td>
                                                <td>
                                                    <input class="form-control" type="date" v-model="item.ExprireDate" />
                                                </td>
                                                <td>
                                                    <input class="form-control" v-model="item.Email" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </b-card>
                    </div>
                    <div class="col-md-4">
                        <b-card class="mt-3" header="Cập nhật">
                            <div class="row">
                                <div class="col-md-6">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                            type="button"
                                            @click="AddList()">
                                        <i class="fa fa-save"></i> Cập nhật
                                    </button>
                                </div>
                                <div class="col-md-6">
                                    <button class="btn btn-success col-md-12 btncus"
                                            type="button"
                                            @click="DoRefesh()">
                                        <i class="fa fa-refresh"></i> Làm mới
                                    </button>
                                </div>
                            </div>
                        </b-card>
                    </div>
                </div>
            </b-tab>
        </b-tabs>
    </div>
</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";

    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    // import the component
    import Treeselect from "@riophae/vue-treeselect";
    // import the styles
    import "@riophae/vue-treeselect/dist/vue-treeselect.css";
    import moment from "moment";
    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";
    import productSpecificationTemplateRepository from "../../repository/productSpecificationTemplate/productSpecificationTemplateRepository";
    import { unflatten, slug, pathImg } from "../../plugins/helper";

    import EventBus from "./../../common/eventBus";
    // Import component
    import UploadExcelComponent from "../../components/uploadexcel/index.vue";
    export default {
        name: "counpon",

        data() {
            return {
                editorData: "<p>Content of the editor.</p>",
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: ""
                },
                lstChose: [],

                parrent: 0,

                isVoucher: false,
                valueConsistsOf: "ALL",
                ListProduct: [],
                SearchZoneId: [],

                disabled: false,
                selectedFile: null,
                isLoading: false,
                fullPage: false,
                color: "#007bff",

                ListCoupon: [],

                pageSize: 20,
                currentPage: 1,
                total: 0,
                keyword: "",

                tableData: [],
                tableHeader: []
            };
        },

        destroyed() {
            EventBus.$off("FileSelected");
        },
        components: {
            Loading,
            Treeselect,
            UploadExcelComponent
        },

        mounted() {
            this.getAll();
        },

        computed: {
            slugM: function () {
                if (this.objRequest != null && this.objRequest != undefined) {
                    this.objRequest.urlInLanguage = slug(this.objRequest.name);
                }
            }
        },
        watch: {
            SearchZoneId: function (newVal) {
                this.currentPage = 1;
                this.onLoadProduct();
            }
        },
        methods: {
            ...mapActions(["getAllCoupon", "MergeListCoupon"]),
            getAll() {
                this.getAllCoupon().then(response => {
                    this.ListCoupon = response;
                });
            },

            AddList() {

                if (this.parrent == 0) {
                    this.$toast.error("Mời bạn chọn nhóm .", {});
                } else {

                    if (this.tableData != undefined && this.tableData != null && this.tableData.length > 0) {
                        let lstObj = this.tableData.map(item => ({
                            ...item,
                            Ma: item.Code,
                            Parrent: this.parrent
                        }));
                        this.MergeListCoupon(lstObj)
                            .then(response => {
                                if (response.key == true) {
                                    debugger
                                    this.$toast.success("cập nhật thành công", {});
                                } else {
                                    this.$toast.error(response.value, {});
                                }
                            })
                            .catch(e => {
                                this.$toast.error("Lỗi" + ". Error:" + e, {});
                            });
                    } else {
                        this.$toast.error("Bạn chưa chọn bản ghi .", {});
                    }
                }
            },
            priceFormat(value) {
                return "$ " + value;
            },
            beforeUpload(file) {
                const isLt1M = file.size / 1024 / 1024 < 1;

                if (isLt1M) {
                    return true;
                }
                this.$message({
                    message: "Please do not upload files larger than 1m in size.",
                    type: "warning"
                });
                return false;
            },
            handleSuccess({ results, header }) {
                let lst = [];
                console.log(results)
                results.forEach(function (item) {
                    lst.push({
                        Code: item.ma,
                        Name: item.name,
                        CreateDate: moment(item.createDate).format("YYYY-MM-DD"),
                        ExprireDate: moment(item.exprireDate).format("YYYY-MM-DD"),
                        Email: item.Email
                    });
                });
                this.tableData = lst;
                this.tableHeader = header;
            },

            pathImgs(path) {
                return pathImg(path);
            },
            openImg(img) {
                this.choseImg = img;

                EventBus.$emit("FileManagerOpen", ""); // '': select one, 'multi': select multi file
            },
            unflattenBase(data) {
                return unflatten(data);
            },
            DoRefesh() {
                this.objRequest.Title = "";
            },
            openUpload() {
                document.getElementById("file-field").click();
            }
        }
    };
</script>

<style>
    .table td {
        padding: 5px;
    }

    .table th {
        text-align: center;
    }
</style>

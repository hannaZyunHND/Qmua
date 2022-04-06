<template>
    <div style="display:flex;width:100%">
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Nhập thông tin mã giảm giá" active>
                <div class="row productedit">
                    <div class="col-md-8">
                        <b-card class="mt-3" header="Thêm / Sửa mã giảm giá">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <b-form-group label="Nhóm khuyến mại">
                                            <select v-model="objRequest.parrent" class="form-control">
                                                <option value="0">Chọn nhóm</option>
                                                <option :value="item.id" v-for="item in ListCoupon">{{item.code}} - {{item.name}}</option>
                                            </select>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Mã khuyến mại">
                                            <b-form-input v-model="objRequest.ma" placeholder="Nhập mã (Chú ý nếu sửa thì không nên chỉnh mã)" required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-8 col-xs-12">
                                        <b-form-group label="Tên mã giảm giá">
                                            <b-form-input v-model="objRequest.name"
                                                          placeholder="Tên mã giảm giá"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>

                                    <div class="col-md-6">
                                        <b-form-group label="Ngày bắt đầu">
                                            <b-form-input type="date"
                                                          v-model="objRequest.createDate"
                                                          placeholder="Ngày bắt đầu"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-6">
                                        <b-form-group label="Ngày kết thúc">
                                            <b-form-input type="date"
                                                          v-model="objRequest.exprireDate"
                                                          placeholder="Ngày kết thúc"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-12">
                                        <b-form-group label="Email quản lý">
                                            <b-form-input type="text"
                                                          v-model="objRequest.email"
                                                          placeholder="Nhập email của người quản lý voucher"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                </div>
                            </b-form>
                        </b-card>
                    </div>
                    <div class="col-md-4">
                        <b-card class="mt-3" header="Cập nhật">
                            <div class="row">
                                <div class="col-md-6">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                            type="button"
                                            @click="DoAddEdit()">
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
    import moment from 'moment';
    import manufacturersRepository from "../../repository/manufacturer/manufacturerRepository";
    import productSpecificationTemplateRepository from "../../repository/productSpecificationTemplate/productSpecificationTemplateRepository";
    import { unflatten, slug, pathImg } from "../../plugins/helper";
    import EventBus from "./../../common/eventBus";
    // Import component

    export default {
        name: "counpons",
        data() {
            return {

                editorData: "<p>Content of the editor.</p>",
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: ""
                },
                lstChose: [],


                isVoucher: false,
                valueConsistsOf: "ALL",
                ListProduct: [],

                SearchProductId: [],

                disabled: false,
                selectedFile: null,
                preview: "/assets/img/unnamed.jpg",
                previewImageFacebook: "/assets/img/unnamed.jpg",
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {
                    parrent: 0,
                    ma: "",
                    name: "",
                    createDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    exprireDate: moment(String(new Date())).format('YYYY-MM-DD'),
                },
                ListCoupon: [],

                ListProductInCoupon: [],

                CouponCodes: [
                    {
                        id: 1,
                        label: "Giảm giá phần trăm"
                    },
                    {
                        id: 2,
                        label: "Giảm giá tiền"
                    }
                ],
            };
        },

        destroyed() {
            EventBus.$off("FileSelected");
        },
        components: {
            Loading,
            Treeselect,
        },

        mounted() {
            this.getAll();
            this.GetById();
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
            ...mapActions([
                "MergeCouponChild",
                "DeleteCouponChild",
                "GetCouponsChildId",
                "getAllCoupon",

            ]),
            getAll() {
                this.getAllCoupon().then(response => {
                    this.ListCoupon = response;
                });
            },



            pathImgs(path) {
                return pathImg(path);
            },
            async openImg(img) {
                this.choseImg = img;

                EventBus.$emit("FileManagerOpen", "");
            },
            unflattenBase(data) {
                return unflatten(data);
            },

            GetById() {
                debugger
                if (this.$route.params.id != null && this.$route.params.id.length > 0) {
                    this.GetCouponsChildId(this.$route.params.id).then(response => {
                        this.objRequest = response;
                        this.objRequest.createDate = moment(response.createDate).format('YYYY-MM-DD');
                        this.objRequest.exprireDate = moment(response.exprireDate).format('YYYY-MM-DD');
                    });
                }
            },
            DoAddEdit() {
                this.isLoading = true;
                this.MergeCouponChild(this.objRequest).then(response => {
                    if (response.key == true) {
                        this.$toast.success(response.value, {});
                    } else {
                        this.$toast.error(response.value, {});
                    }
                }).catch(e => {
                    this.$toast.error("Lỗi" + ". Error:" + e, {});
                });
            },
            DoRefesh() {
                this.objRequest.Title = "";
            },
            openUpload() {
                document.getElementById("file-field").click();
            },
            getSelectedCoupon(node, id) {
                this.CouponValues = node.id;
                this.objRequest.discountOption = node.id;
                console.log(this.CouponValues);
            },
            onChangeList: function ({ source, destination }) {
                this.ProductOptionsSource = source;
                this.ProductOptionsDestination = destination;
            },

        },
        watch: {

        }
    };
</script>

<style>
    .productedit .form-control {
        height: 35px;
    }


    .panel-body ul li .thumb {
        float: left;
        height: 30px;
        margin-right: 10px;
        overflow: hidden;
        width: 30px;
        border: 1px solid #ddd;
    }



    .panel-body ul li p.text-muted {
        font-size: 11px;
        line-height: 15px;
    }

    .panel-body ul li p {
        margin-bottom: 0px;
    }

    .panel-body ul li {
        border-bottom: 1px solid #e6e6fa;
        cursor: pointer;
        list-style-type: none;
        padding: 4px 0;
        padding-left: 10px;
    }
    /* width */
    ::-webkit-scrollbar {
        width: 10px;
    }

    /* Track */
    ::-webkit-scrollbar-track {
        background: #f1f1f1;
    }

    /* Handle */
    ::-webkit-scrollbar-thumb {
        background: #888;
    }

        /* Handle on hover */
        ::-webkit-scrollbar-thumb:hover {
            background: #555;
        }

    .row ul li.active, #rlist .row ul li.active {
        background: #f1f7fd;
    }


    .pagination .page-item .page-link {
        font-size: 12px;
    }

    .pagination {
        margin-top: 10px
    }
</style>

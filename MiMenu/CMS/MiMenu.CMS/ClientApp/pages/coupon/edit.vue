<template>
    <div style="display:flex;width:100%">
        <b-tabs class="col-md-12" pills>
            <b-tab title="1. Nhập thông tin mã giảm giá" active>
                <div class="row productedit">
                    <div class="col-md-8">
                        <b-card class="mt-3" header="Thêm / Sửa mã giảm giá">
                            <b-form class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-6 col-xs-12">
                                        <b-form-group label="Mô tả mã giảm giá">
                                            <b-form-input v-model="objRequest.name"
                                                          placeholder="Tên mã giảm giá"
                                                          v-on:keyup.13="slugM"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-6">
                                        <b-form-group label="Chọn loại mã giảm giá">
                                            <treeselect :options="CouponCodes"
                                                        :disable-branch-nodes="true"
                                                        @select="getSelectedCoupon"
                                                        v-model="CouponValues"
                                                        :default-expanded-level="Infinity"
                                                        :disabled="disabled"
                                                        placeholder="Xin mời bạn lựa chọn" />
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-6">
                                        <b-form-group label="Mã giảm giá">
                                            <b-form-input v-model="objRequest.code" placeholder="Code" required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-6">
                                        <b-form-group label="Số lượng">
                                            <b-form-input type="number"
                                                          v-model="objRequest.quantityDiscount"
                                                          placeholder="Số lượng"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Giá trị">
                                            <b-form-input type="number"
                                                          v-model="objRequest.valueDiscount"
                                                          placeholder="Giá trị"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Ngày bắt đầu">
                                            <b-form-input type="date"
                                                          v-model="objRequest.startDate"
                                                          placeholder="Ngày bắt đầu"
                                                          required></b-form-input>
                                        </b-form-group>
                                    </div>
                                    <div class="col-md-4">
                                        <b-form-group label="Ngày kết thúc">
                                            <b-form-input type="date"
                                                          v-model="objRequest.endDate"
                                                          placeholder="Ngày kết thúc"
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
                <div class="row productedit" style="display:none">
                    <div class="col-md-8">
                        <b-card class="mt-3" header="Danh sách sản phẩm áp dụng 1">
                            <div class="row">
                                <div class="col-md-9">

                                    <treeselect :multiple="true"
                                                :flat="true"
                                                :options="ListProduct"
                                                placeholder="Mời bạn chọn sản phẩm"
                                                v-model="SearchProductId" />
                                </div>
                                <div class="col-md-3">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                            type="button" @click="addProduct()">
                                        <i class="fa fa-plus"></i> Thêm mới
                                    </button>
                                </div>
                            </div>

                            <table class="table table-centered table-nowrap data-thumb-view dataTable no-footer">
                                <thead class="thead-dark table table-centered table-nowrap text-center">
                                    <tr>
                                        <th> Tên sản phẩm sds </th>
                                        <th> Giá trị áp dụng </th>
                                        <th> Loại </th>
                                        <th> XXX </th>
                                    </tr>
                                </thead>
                                <tbody class="text-center">
                                    <tr v-for="(item,index) in ListProductInCoupon">
                                        <td>{{item.name}}</td>
                                        <td><input v-model="item.value" /> </td>
                                        <td>
                                            <select v-model="item.type">
                                                <option v-for="i in CouponCodes" :value="i.id">
                                                    {{i.label}}
                                                </option>
                                            </select>
                                        </td>
                                        <td>
                                            <span class="action-delete"><a v-b-tooltip.hover title="Xóa sản phẩm" @click="removeProduct(index)"><i style="color:red" class="fa fa-trash"></i></a></span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </b-card>
                    </div>
                    <div class="col-md-4">
                        <b-card class="mt-3" header="Áp dụng cho sản phẩm">
                            <div class="row">
                                <div class="col-md-6">
                                    <button class="btn btn-info btn-submit-form col-md-12 btncus"
                                            type="button"
                                            @click="addProductInCoupons()()">
                                        <i class="fa fa-save"></i> Thêm mã giảm giá cho sản phẩm
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
        name: "counpon",
        data() {
            return {

                editorData: "<p>Content of the editor.</p>",
                editorConfig: {
                    allowedContent: true,
                    extraPlugins: ""
                },
                lstChose: [],
                SearchProductId: [],

                isVoucher: false,
                valueConsistsOf: "ALL",



                disabled: false,
                selectedFile: null,
                preview: "/assets/img/unnamed.jpg",
                previewImageFacebook: "/assets/img/unnamed.jpg",
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                objRequest: {
                    id: 0,
                    urlInLanguage: "",
                    name: "",
                    avatar: "",
                    startDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    endDate: moment(String(new Date())).format('YYYY-MM-DD'),
                    isCategory: false,
                    listProduct: [],
                },
                LanguageCodes: [],
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
                LanguageValues: null,
                CouponValues: 2,

                pageSize: 20,
                currentPage: 1,
                total: 0,
                keyword: "",

                ListProduct: [],


                ListCoupon: [],

                ListProductInCoupon: [],

            };
        },
        async created() {
            this.LanguageCodes = await productSpecificationTemplateRepository.getAllLanguageOption();
            this.GetById();
        },
        destroyed() {
            EventBus.$off("FileSelected");
        },
        components: {
            Loading,
            Treeselect,
        },


        computed: {
            slugM: function () {

                if (this.objRequest != null && this.objRequest != undefined) {
                    this.objRequest.urlInLanguage = slug(this.objRequest.name);
                }
            }
        },
        watch: {


        },
        methods: {
            ...mapActions([
                "addCoupon",
                "updateCoupon",
                "getCouponById",
                "uploadFile",
                "getProductInCoupon",
                "addProductInCoupon"
            ]),
            removeProduct(index) {
                this.ListProductInCoupon.splice(index, 1);
            },
            addProduct() {
                for (var i = 0; i < this.SearchProductId.length; i++) {
                    var searItem = this.SearchProductId[i];
                    var obj = this.ListProduct.filter(x => x.id == searItem);
                    this.ListProductInCoupon.push({
                        productId: obj[0].id,
                        name: obj[0].label,
                        couponId: this.objRequest.id,
                        value: 0,
                        type: 2
                    })
                }
            },
            addProductInCoupons() {
                var obj = {};
                obj.lstObj = this.ListProductInCoupon;
                obj.id = this.objRequest.id;
                this.addProductInCoupon(obj).then(response => {
                    if (response.key == true) {
                        this.$toast.success(response.value, {});
                    } else {
                        this.$toast.error(response.value, {});
                    }
                }).catch(e => {
                    this.$toast.error("Lỗi" + ". Error:" + e, {});
                });
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

            getProduct(ma) {
                this.getProductInCoupon(ma).then(response => {

                    this.ListProductInCoupon = response.ListData;
                    this.ListProduct = response.ListProduct;
                });
            },


            async GetById() {
                if (this.$route.params.id > 0) {
                    var id = this.$route.params.id;
                    debugger
                    this.isLoading = true;
                    let initial = this.$route.query.initial;
                    initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                    var response = await this.getCouponById(id);

                    this.objRequest = response;
                    this.objRequest.id = response.id;
                    this.CouponValues = response.discountOption;
                    this.objRequest.startDate = moment(response.startDate).format('YYYY-MM-DD');
                    this.objRequest.endDate = moment(response.endDate).format('YYYY-MM-DD');

                    this.getProduct(id);
                    this.isLoading = false;
                }
            },
            async DoAddEdit() {
                this.isLoading = true;
                this.objRequest.category = this.SearchZoneId;
                if (this.objRequest.id > 0) {

                    var result = await this.updateCoupon(this.objRequest);
                    if (result.success == true) {
                        this.$toast.success("Tạo thành công", {});
                        this.isLoading = false;
                        //this.$router.go(-1);
                    } else {
                        //this.$router.go(-1);
                        this.$toast.error(result.message, {});
                        this.isLoading = false;
                    }
                } else {
                    var result = await this.addCoupon(this.objRequest);
             
                    if (result.success == true) {
                        this.$toast.success("Tạo thành công", {});
                        this.isLoading = false;
                        this.objRequest.id = result.id;
                        this.$router.push({
                            path: "/admin/coupon/edit/" + result.id,
                        });
                        this.getProduct(result.id);
                       
                    } else {

                        this.$toast.error(result.message, {});
                        this.isLoading = false;
                    }
                }
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

            },
            onChangeList: function ({ source, destination }) {
                this.ProductOptionsSource = source;
                this.ProductOptionsDestination = destination;
            }
        }
    };
</script>

<style>
    .productedit .form-control {
        height: 35px;
    }

    .hidents{
        display:none
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

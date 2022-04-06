<template>
    <div class="productadd">


        <div class="card mt-3">
            <div class="card-header"><div>Thêm thông số cho sản phẩm: <b class="text-danger">{{objProduct.name}}</b> </div></div>
            <div class="card-body" style="padding:0 !important">
             
                <b-tabs style="padding:0" class="col-md-12" pills>
                    <b-tab title="Giá sản phẩm">
                        <locationinproduct :productId="Id" :actions="actionLocation">
                        </locationinproduct>
                    </b-tab>
                    <b-tab title="Thường được mua cùng" @click="loadComboSame()">
                        <sameproduct :productId="Id" :category="ListZone" :actions="actionComboSame">
                        </sameproduct>
                    </b-tab>
                    <b-tab title="Combo sản phẩm" @click="loadCombo()">
                        <comboproduct :productId="Id" :category="ListZone" :actions="actionCombo">
                        </comboproduct>
                    </b-tab>
                    <b-tab title="Khuyến mại" @click="loadPromotion()">
                        <promotioninproduct :productId="Id" :actions="actionpromotion">
                        </promotioninproduct>
                    </b-tab>
                    <b-tab title="Chi tiết kĩ thuật" @click="loadSpecification()">
                        <specificationinproduct :category="ListZone" :productId="Id" :actions="actionspecification">
                        </specificationinproduct>
                    </b-tab>
                </b-tabs>


            </div>

        </div>

    </div>

</template>


<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import { mapGetters, mapActions } from "vuex";
    import Loading from "vue-loading-overlay";
    import locationinproduct from "./location";
    import comboproduct from "./comboproduct";
    import sameproduct from "./combosame";
    import promotioninproduct from "./promotion";
    import specificationinproduct from "./specification";
    import '@riophae/vue-treeselect/dist/vue-treeselect.css';
    import { unflatten, pathImg } from '../../plugins/helper';

    // Import component
    export default {
        name: "mainproductextend",
        components: {

        },
        data() {
            return {
                actionLocation: false,
                actionCombo: false,
                actionpromotion: false,
                actionspecification: false,
                isLoading: false,
                Id: 0,
                objProduct: {
                   
                },
                ListZone: [],
                
                bootstrapPaginationClasses: {
                    ul: "pagination",
                    li: "page-item",
                    liActive: "active",
                    liDisable: "disabled",
                    button: "page-link"
                },
                customLabels: {
                    first: "First",
                    prev: "Previous",
                    next: "Next",
                    last: "Last"
                }
            };
        },

        components: {
            Loading,
            locationinproduct,
            comboproduct,
            promotioninproduct,
            specificationinproduct,
            sameproduct
        },
        methods: {
            ...mapActions(["getZones", "getNameProduct"]),
            pathImgs(path) {
                return pathImg(path);
            },
            loadComboSame() {
                this.actionComboSame = true;
            },
            loadCombo() {
                this.actionCombo = true;
            },
            loadPromotion() {
                this.actionpromotion = true;
            },
            loadSpecification() {
                this.actionspecification = true;
            }
        },
        computed: {
        },
        created() {
            this.getZones(1).then(respose => {
                try {
                    respose.listData.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                    var data = respose.listData;
                    var dataLeft = unflatten(data);
                    this.ListZone = dataLeft;
                }
                catch (ex) {

                }
            });
            if (this.$route.params.id > 0) {
                this.getNameProduct(this.$route.params.id).then(respose => {
                    try {

                        this.objProduct = respose.data;

                    }
                    catch (ex) {

                    }
                });
            }

        },
        mounted: function () {
            if (this.$route.params.id > 0) {
                this.isLoading = true;
                this.Id = parseInt(this.$route.params.id);
                this.actionLocation = true;
                this.isLoading = false;
            }

        },
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

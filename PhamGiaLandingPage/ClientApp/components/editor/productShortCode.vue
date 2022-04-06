<template>
    <div class="file-manager">
        <loading :active.sync="isLoading"
                 :height="35"
                 :width="35"
                 :color="color"
                 :is-full-page="fullPage"></loading>
        <b-modal ref="file-manager-modal" id="file-manager" fbody="xxx" hide-footer hide-header size="xl">
            <div id="fm-container" class="container-fluid">
                <b-row id="fm-toolbar">
                    <b-col lg="2" md="2" sm="12">
                        <b>Sản phẩm</b>
                    </b-col>
                    <b-col>
                        <b-row align-h="between">
                            <b-col cols="4">
                                <treeselect v-model="SearchZoneId"
                                            :options="ZoneOptions"
                                            placeholder="Danh mục"
                                            :default-expanded-level="Infinity">
                                </treeselect>
                            </b-col>
                            <b-col cols="4" class="bd-0">
                                <b-pagination v-model="currentPage" align="right" size="sm" :limit="4"
                                              :total-rows="products.total"
                                              :per-page="pageSize"></b-pagination>
                            </b-col>

                        </b-row>

                    </b-col>

                </b-row>
                <b-row id="fm-main">
                    <b-col id="fm-content" lg="9" dm="12" sm="12">
                        <div id="fm-data-wrapper">
                            <div class="fm-list-wrapper">
                                <div class="fm-list">
                                    <ul id="fm-grid" v-if="products.listData">
                                        <li v-for="item in products.listData" :key="item.id" class="item" :class="{ _active: isActive}" @click="SelectFile($event,{name:item.name,id:item.id,avatar:item.avatar,code:item.code})">
                                            <img :src="pathImgs(item.avatar)" alt="">
                                            <i class="fa fa-check"></i>
                                            <div class="info">
                                                <p class="name">{{item.name}}</p>
                                                <p class="dimensions">{{item.code}}</p>
                                            </div>
                                        </li>
                                    </ul>
                                    <p v-else class="text-center">Không tìm thấy !</p>
                                </div>
                            </div>
                        </div>

                    </b-col>
                    <b-col id="fm-content-selected" lg="3" dm="2" sm="12">
                        <p class="text-center"><b>Danh sách đã chọn</b></p>
                        <ul style="padding:0" v-if="selectedProduct">
                            <li v-for="pro in selectedProduct" :key="pro.id" style="border-bottom:1px solid #ddd;padding:4px 0;display:inline-block;min-height:50px">
                                <img style="width:45px;float:left;padding-right:4px;clear:both" :src="pathImgs(pro.avatar)" alt="">{{pro.name}}-{{pro.code}}
                            </li>
                        </ul>
                        <p v-else class="text-center">trống !</p>
                    </b-col>
                </b-row>
                <b-row align-h="end" id="fm-footer">
                    <b-col cols="4">
                        <div class="btn-attack">
                            <button type="button" id="btn-attack" @click="attackFile">Đính kèm</button>
                            <!--<button type="button" id="btn-attack" @click="attackFileSelected">Đính kèm2</button>-->
                            <button type="button" id="dl-close" @click="hideModal" class="iclose">Đóng</button>
                        </div>
                    </b-col>
                </b-row>
            </div>
        </b-modal>
    </div>
</template>

<script>
    import "vue-loading-overlay/dist/vue-loading.css";
    import { mapActions, mapGetters } from "vuex";
    import Loading from "vue-loading-overlay";
    import EventBus from "./../../common/eventBus";
    // import the component
    import Treeselect from '@riophae/vue-treeselect'
    // import the styles
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'
    import { unflatten, pathImg } from "../../plugins/helper";
    export default {
        name: "FileManager",
        components: {
            Loading,
            Treeselect
        },
        props: {
            pskey: { type: String },
        },
        data() {
            return {
                SearchZoneId: 0,
                isLoading: false,
                fullPage: false,
                color: "#007bff",
                isLoadLang: false,
                currentPage: 1,
                pageSize: 24,
                isLoading: false,
                selectedProduct: [],
                isActive: false,
                selectType: '',
                ZoneOptions: [],
            };
        },
        created() {

        },
        destroyed() {
            // Stop listening the event hello with handler
            EventBus.$off(this.pskey + 'attack');

        },
        computed: {
            files() {
                return this.$store.getters.files
            },
            ...mapGetters(["products"])
        },
        methods: {
            ...mapActions(["getProducts", "getZones"]),
            pathImgs(path) {
                return pathImg(path);
            },
            SelectFile(event, file) {
                if (this.selectType == 'multi') {
                    if (event.currentTarget.classList.contains('_active')) {
                        event.currentTarget.classList.remove('_active');
                        this.selectedProduct = this.selectedProduct.filter(obj => obj.id != file.id)


                    }
                    else {

                        event.currentTarget.classList.add("_active");
                        this.selectedProduct.push(file)
                    }
                } else {

                    this.selectedFile = [];
                    if (event.currentTarget.classList.contains('_active')) {
                        event.currentTarget.classList.remove('_active');

                    }
                    else {
                        let items = document.querySelectorAll('.item');
                        items.forEach(function (item) {
                            item.classList.remove('_active');
                        });
                        event.currentTarget.classList.add("_active");
                        this.selectedProduct.push(file)
                    }


                }


            },

            ProductManagerOpen(param) {

                this.selectType = param;
                this.$refs["file-manager-modal"].show();
                this.getZones(1).then(respose => {
                    var data = respose.listData;
                    data.push({ id: 0, label: "Danh mục", parentId: 0 });
                    this.ZoneOptions = unflatten(data);
                });
                this.getProducts({
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    idZone: this.SearchZoneId
                });
            },

            hideModal() {
                this.$refs["file-manager-modal"].hide();
            },
            attackFile() {
                EventBus.$emit(this.pskey + 'attack', this.selectedProduct);
                this.$refs["file-manager-modal"].hide();
                this.selectedProduct = [];
            },

            toggleModal() {

                this.$refs["file-manager-modal"].toggle("#toggle-btn");
            }
        },
        mounted() {
            //  EventBus.$on(this.pskey + 'modal', this.ProductManagerOpen);
        },
        watch: {
            currentPage() {
                this.getProducts({
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    idZone: this.SearchZoneId
                });
            },
            SearchZoneId: function(val) {
                console.log(val);
                this.isLoadLang = true;
                this.getProducts({
                    pageIndex: 1,
                    pageSize: this.pageSize,
                    idZone: val
                }).then(res => {
                    this.isLoadLang = false;
                });
            },
        }
    };
</script>
<style scoped>
    #file-manager, #fm-content-selected {
        padding: 0;
    }

    #file-manager___BV_modal_body_ {
        padding: 0;
    }

    #fm-content-selected ul li {
        list-style-type: none
    }

    #fm-container-dialog {
        padding: 0;
        overflow: hidden !important;
    }

    #fm-container {
        font-size: 12px;
        font-family: sans-serif;
        padding: 0
    }

    #fm-toolbar ul {
        float: left;
        margin: 3.5px 0 3px 10px;
        padding: 1px 2px;
        border-radius: 0
    }

        #fm-toolbar ul li {
            border: 1px solid #fff;
            cursor: pointer;
            float: left;
            line-height: 26px;
            list-style-type: none;
            padding: 0 8px;
            text-align: center;
        }

            #fm-toolbar ul li:hover {
                background: #eaeaea;
            }

            #fm-toolbar ul li.active {
                background: #eaeaea;
            }

    #fm-container {
    }

    #fm-toolbar {
    }

    #fm-footer {
        border-top: 1px solid #eaeaea;
        padding-top: 10px
    }

    #fm-toolbar .tool-items {
        clear: both;
        padding-right: 6px;
    }

    #fm-toolbar .tools {
        float: left;
    }

    #fm-main {
        margin-top: 8px;
        border-top: 1px solid #eaeaea;
    }

    #fm-sidebar .fm-header {
        height: 35px;
        line-height: 35px;
        background-color: #39c;
        overflow: hidden;
        font-size: 17px;
        color: #ecf3f9;
        text-align: center;
    }

    #fm-sidebar {
        border-right: 1px solid #eaeaea
    }

    #fm-content {
        background-color: white;
        cursor: default; /*z-index: 0; box-shadow: 0 2px 4px rgba(0,0,0,0.04), 0 1px 4px rgba(0,0,0,0.12);*/
        height: 100%;
    }

    #fm-data-wrapper {
        overflow: hidden;
    }

    #fm-file-view {
        border-left: 1px solid #eaeaea;
        float: right;
        height: 100%;
        width: 250px;
        padding: 9px;
        background: #fff;
    }

        #fm-file-view .file-thumb {
            text-align: center;
            max-height: 250px;
            max-width: 230px;
            overflow: hidden;
        }

        #fm-file-view .header {
            font-weight: bold;
            margin-bottom: 12px;
            padding: 2px 0;
            text-align: center;
            text-transform: uppercase;
        }

        #fm-file-view .details {
            padding-top: 16px;
        }

            #fm-file-view .details div {
                line-height: 21px;
            }

            #fm-file-view .details .uploaded,
            #fm-file-view .details .file-size,
            #fm-file-view .details .dimensions {
                line-height: 21px;
            }

        #fm-file-view .file-thumb img {
            border: 1px solid #eaeaea;
            padding: 4px; /*max-height: 220px; max-width: 228px;*/
        }

    .attachment-info .filename {
        font-weight: 600;
        color: #444;
        word-wrap: break-word;
    }

    .btn-attack {
        background: #fff none repeat scroll 0 0;
        padding: 3px 3px 3px 0;
        width: 230px;
        float: right;
        clear: both;
    }

        .btn-attack button#btn-attack {
            color: #fff;
            background: #0085ba;
            border: 0;
            padding: 4px 6px;
        }

        .btn-attack button#dl-close {
            background: #eaeaea;
            border: 0;
            padding: 4px 6px;
            float: right;
            clear: both;
        }

    #fm-content table._list {
        width: 100%;
    }

        #fm-content table._list tr {
        }

            #fm-content table._list tr th {
                text-align: center;
                font-weight: bold;
            }

            #fm-content table._list tr td {
                padding: 5px 4px;
            }

            #fm-content table._list tr._active {
                background: #48adff;
            }

    #fm-toolbar ul li i {
        background-repeat: no-repeat;
        display: inline-block;
        height: 16px;
        vertical-align: middle;
        width: 16px;
    }

    #fm-toolbar ul li i {
    }

    li i.create-folder {
        background-image: url(./../../assets/img/icon/folder_add.png);
    }

    li i.upload {
        background-image: url("./../../assets/img/icon/upload.png");
    }

    li i.list {
        background-image: url("./../../assets/img/icon/application_view_list.png");
    }

    li i.grid {
        background-image: url("./../../assets/img/icon/application_view_icons.png");
    }

    li i.iclose {
        background-image: url("./../../assets/img/icon/reset.png");
    }

    li i.crop {
        background-image: url("./../../assets/img/icon/crop.png");
    }

    li i.remove {
        background-image: url("./../../assets/img/icon/bin_closed.png");
    }

    li i.close:hover {
    }

    #fm-footer {
        min-height: 32px;
    }

    #fm-data-wrapper {
        height: 100%;
    }

    .clear {
        clear: both;
    }

    #_list ._active {
        background: #48adff;
    }

    #fm-content {
    }

        #fm-content table._list table thead tr th {
            text-align: center;
        }

        #fm-content table._list tr {
            font-size: 11px;
        }

            #fm-content table._list tr:hover {
                background: #c1edff;
                cursor: pointer;
            }

            #fm-content table._list tr td {
            }

        #fm-content table._list tbody tr td.name {
            display: inline-flex;
        }

            #fm-content table._list tbody tr td.name .list-icon {
                display: inline-block;
                float: left;
                padding-right: 20px;
            }
    /* #fm-content table._list tbody tr td.name .png, #fm-content table tbody tr td.name .jpg, #fm-content table tbody tr td.name .jpeg, #fm-content table tbody tr td.name .gif,
    #fm-content table._list tbody tr td.name .bmp { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/picture.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name .doc, #fm-content table tbody tr td.name .docx { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/doc.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name .xlsx, #fm-content table tbody tr td.name .xls { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/xls.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name .zip, #fm-content table tbody tr td.name .rar { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/zip.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name .txt { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/txt.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name .pdf { background: url(/CMS/Modules/FileManager/Libs/Images/filetree/pdf.png) no-repeat; width: 16px; height: 16px; }
    #fm-content table._list tbody tr td.name { width: 274px; display: inline-block; overflow: hidden; }
    #fm-content table._list tbody tr td.name .fname { display: inline-flex; }
    #fm-content table._list tbody tr td.type, #fm-content table tbody tr td.size, #fm-content table tbody tr td.dimensions, #fm-content table tbody tr td.date { overflow: hidden; width: 98px; }
    #fm-content table._list tbody tr td.size { width: 70px; }
    #fm-content table._list tbody tr td.dimensions { width: 80px; }
    #fm-content table._list tbody tr td.date { width: 115px; } */

    #fm-grid, #fm-folder ul, #fm-grid li .info p {
        padding: 0;
        margin: 0;
    }

        #fm-grid li {
            position: relative;
            vertical-align: middle;
            display: table-cell;
            border: 1px solid #eaeaea;
            cursor: pointer;
            float: left;
            height: 100px;
            min-width: 114px;
            list-style-type: none;
            margin: 4px;
            padding: 2px;
            width: 14.99%;
            position: relative;
            overflow: hidden;
            text-align: center;
        }

            #fm-grid li .info {
                font-size: 10px;
                background: rgba(109, 105, 105, 0.87);
                color: #fff;
                position: absolute;
                bottom: 0;
                width: 100%;
                height: 0
            }

            #fm-grid li:hover .info {
                height: auto
            }

    .fm-list li img {
        width: 100%
    }

    .fm-list li.not_img img {
        width: 50%;
        padding-top: 10px
    }

    #fm-grid li img.thumb {
        width: 60%;
        margin: 0 auto;
    }

    #fm-grid li i {
        position: absolute;
        z-index: 999;
        right: 4px;
        background: #fff;
        border-radius: 11px;
        color: #0085ba;
        padding: 1px;
        top: 3px;
        display: none;
    }

    #fm-grid li._active i {
        display: block;
    }

    #fm-grid li._active {
        border: 1px solid #0085ba;
    }

    #fm-folder ul li {
        list-style-type: none;
        padding: 8px 0;
        cursor: pointer
    }

        #fm-folder ul li i {
            background-repeat: no-repeat;
            display: inline-block;
            height: 16px;
            vertical-align: middle;
            width: 16px;
        }

    .plupload_filelist_header {
        height: 20px;
    }

    .plupload_filelist_footer {
        padding: 6px 20px;
        height: 33px;
    }

    .plupload_scroll .plupload_filelist {
        height: 172px !important;
    }

    .plupload_filelist_footer {
        padding: 6px 20px;
    }

    .plupload_filelist:empty,
    .plupload_filelist li.plupload_droptext {
        height: 140px;
    }

        .plupload_filelist:empty::before,
        .plupload_filelist li.plupload_droptext::before {
            font-size: 52px;
            height: 75px;
            left: 49%;
            margin-left: -40px;
            padding-top: 43px;
        }

    .items-action {
        padding-top: 20px;
        display: inline-block;
        width: 100%;
    }

        .items-action .file-action {
            border: 0 none;
            color: #fff;
            padding: 4px 10px;
        }

        .items-action .file-action {
            background: rosybrown;
            float: left;
            clear: both;
        }
    /*.items-action .file-action:last-child { background: #0085ba;float: right;}*/
    #fm-toolbar .Mi.ipagination a:first-child {
        border: 0;
        border-radius: 0;
    }

    #fm-toolbar .Mi.ipagination input {
        border: medium none;
        float: left;
        font-family: arial;
        font-size: 11px;
        height: 30px;
        margin: 0;
        outline: medium none;
        padding: 0;
        text-align: center;
        vertical-align: middle;
        width: 84px;
    }

    #fm-toolbar .Mi.ipagination a {
        border: 0;
        border-radius: 0;
        background: #fff;
    }

    #fm-toolbar .ipagination.iweb {
        margin-top: 3.45px;
    }

    #frowInTotals {
        display: inline-block;
        float: left;
        line-height: 39px;
        padding-right: 3px;
        vertical-align: baseline;
    }

    label#progressall {
        display: none;
        padding: 7px 7px;
    }

    .Cdiv {
        padding-left: 12px;
        height: 17px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        width: 100%;
        display: inline-block;
        font-weight: normal;
    }

    #btn-fm-upload label {
        margin-bottom: 0 !important;
    }
    /* Extra small devices (phones, 600px and down) */

    @media only screen and (max-width: 600px) {
        #fm-grid li {
            width: 47.65%;
        }
    }

    /* Small devices (portrait tablets and large phones, 600px and up) */

    @media only screen and (min-width: 600px) {
    }

    /* Medium devices (landscape tablets, 768px and up) */

    @media only screen and (min-width: 768px) {
    }

    /* Large devices (laptops/desktops, 992px and up) */

    @media only screen and (min-width: 992px) {
    }

    /* Extra large devices (large laptops and desktops, 1200px and up) */

    @media only screen and (min-width: 1200px) {
    }
</style>

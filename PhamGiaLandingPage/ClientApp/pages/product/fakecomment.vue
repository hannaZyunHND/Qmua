<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-header">
                    Chọn bài viết hoặc sản phẩm để Fake Comment
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex;width:100%">
                                        <div class="col-md-7" style="padding-left:0px">
                                            <treeselect :multiple="false"
                                                        :options="unflattenBase(ListZone)"
                                                        placeholder="Xin mời bạn lựa chọn danh mục"
                                                        v-model="SearchZoneId"
                                                        :default-expanded-level="Infinity" />
                                        </div>
                                        <div class="input-group" style="display:flex;width:40%">
                                            <input type="text" autocomplete="off" v-model="keyword" placeholder="Tìm kiếm sản phẩm" v-on:keyup.enter="onLoadProduct()" class="form-control"> <span @click="onLoadProduct()" class="input-group-addon bg-primary" style="cursor: pointer; width: 45px;"><i class="fa fa-search" style="padding-top: 10px; padding-left: 15px;"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">
                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <li v-for="(item,index) in ListProduct" @click="changeValueLeft(index)" :class="{'active':item.active==true}">
                                                    <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                    <p style="font-size:13px;overflow:hidden">Mã: {{item.code}}</p>
                                                    <p class="text-muted">Tên: {{item.name}}</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <b-pagination v-model="currentPage"
                                                  :total-rows="total"
                                                  :per-page="pageSize"></b-pagination>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1" style="padding-top:30px">
                            <button class="btn btn-primary btn-block mb-2" title="Thêm sản phẩm" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                            <button class="btn btn-danger btn-block mb-2" title="Xóa sản phẩm" @click="onetoRemove"><i class="fa fa-trash" aria-hidden="true"></i></button>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <div style="display:flex">
                                        <!--<div style="display:flex;width:30%">
                                            <select v-model="SelectedRegion" class="form-control">
                                                <option v-for="item in ListRegion" :value="item.key">
                                                    {{item.value}}
                                                </option>
                                            </select>
                                        </div>-->
                                        <div style="display:none;width:40%;padding-left:2%">
                                            <treeselect :multiple="false"
                                                        :options="unflattenBase(ListZoneRight)"
                                                        placeholder="Xin mời bạn lựa chọn danh mục"
                                                        v-model="ChoseZoneId"
                                                        :default-expanded-level="Infinity" />
                                        </div>
                                        <div style="display:flex;width:15%;padding-left:2%">
                                            <button class="btn btn-info btn-submit-form col-md-12 btncus" type="submit" v-on:click="AddFakeComment()">
                                                <i class="fa fa-save"></i> Lưu
                                            </button>
                                        </div>
                                        <div style="display:none;width:10%;padding-left:2%">
                                            <button title="Chọn ảnh" class="btn btn-danger btn-submit-form col-md-12 btncus" @click="openImg('img')" type="submit">
                                                <i class="fa fa-picture-o"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="slimScrollDiv" style="position: relative; overflow-x: scroll; width: auto; height: 300px;margin-top:10px">
                                        <div class="row" style="width: auto; height: 300px;">

                                            <ul style="padding:20px;padding-left:10px ;width:100%">
                                                <draggable v-model="ListValue" group="people" @start="drag=true" @end="drag=false">
                                                    <li v-for="(item,index) in ListValue" @click="changeValueRight(index)" style="padding-bottom:10px" :class="{'active':item.active==true}">
                                                        <div style="display:flex;width:100%">
                                                            <div style="display:flex;width:10%">
                                                                <a class="thumb"><img :src="pathImgs(item.avatar)" width="30"></a>
                                                            </div>
                                                            <div style="display:flex;width:70%;flex-wrap:wrap">
                                                                <span style="font-size:13px">  {{item.productName}}</span><p v-if="item.zoneId > 0" style="color:#0026ff;font-size:13px;padding-right:10px"> / {{item.zoneName}}</p>
                                                                <p style="font-size:13px"> <b-form-checkbox style="display:none" v-model="item.isHot">Sản phẩm Hot</b-form-checkbox></p>
                                                            </div>
                                                            <div style="display:flex;width:20%">
                                                                <div class="col-md-6">
                                                                    <!--<input type="button" v-model="item.bigThumb"  />-->
                                                                    <img v-if="(item.bigThumb !=null && item.bigThumb.length>0)" style="width:100px;height:40px" :src="pathImgs(item.bigThumb)" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </li>
                                                </draggable>

                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-10">
                            Chi tiết của Comment
                        </div>
                        <div class="col-2">
                            <button type="button" class="btn btn-success" v-on:click="addNewCommentItem">+</button>
                        </div>
                    </div>
                    
                </div>
                <div class="card-body">
                    <div class="row" v-for="(it, index) in CommentFakes" style="margin-bottom:5px">    
                        <div class="col-md-2">
                            <b-form-select v-model="it.Type" :options="CommentType"></b-form-select>
                        </div>
                        <div class="col-md-4">
                            <b-form-input v-model="it.Name" placeholder="Nhập tên comment"></b-form-input>
                        </div>
                        <div class="col-md-5">
                            <b-form-textarea id="textarea"
                                             v-model="it.Description"
                                             placeholder="Enter something..."   
                                             rows="3"
                                             max-rows="6"></b-form-textarea>
                        </div>
                        <div class="col-md-1">
                            <b-form-input>
                                <b-form-input v-model="it.Rating" typeof="number" min="0" max="5"  placeholder="Nhập Rating"></b-form-input>
                            </b-form-input>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <FileManager v-on:handleAttackFile="DoAttackFile" :miKey="mikey1" />
    </div>
</template>

<script>

    import { mapActions } from "vuex";
    import Treeselect from '@riophae/vue-treeselect';
    import { unflatten, pathImg } from '../../plugins/helper';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css'
    import FileManager from './../../components/fileManager/list'

    import EventBus from "./../../common/eventBus";
    import draggable from 'vuedraggable';

    export default {
        name: "productinregion",
        components: {
            Treeselect,
            FileManager,
            draggable,
        },
        props: {
            productId: {
                type: Number,
                required: false,
                default: 0
            },
            actions: {
                type: Boolean,
                required: false,
                default: false
            }
        },
        data() {
            return {

                mikey1: 'mikey1',
                SelectedRegion: "",

                SearchZoneId: 0,
                ChoseZoneId: 0,
                ListZone: [],
                ListProduct: [],
                ListValue: [],

                ListRegion: [],

                ListZoneRight: [],
                TransferObject: [],


                

                keyword: "",
                activeLeft: [],
                activeRight: [],
                CommentFakes: [],
                ItemCommnet: {
                    Type: "",
                    Name: "",
                    Description: "",
                    Rating:0
                },
                CommentTypeSelected : null,

                CommentType: [
                    
                    { value: null, text: "Chọn loại Comment" },
                    { value: 'comment', text: "comment" },
                    { value: 'rating', text: "rating" }
                ],

                pageSize: 20,
                currentPage: 1,
                total: 0,
            };
        },
        mounted: function () {
            this.CommentFakes.push(this.ItemCommnet);

        },
        methods: {
            ...mapActions(["getProductForCombo", "getAllRegion", "getProductByRegion", "getZones", "addProductByRegion", "getItemAllForCombo","createCommentFake"]),
            pathImgs(path) {
                return pathImg(path);
            },
            AddFakeComment() {
                var listObject = this.ListValue;
                console.log(listObject);
                var obj = [];
                for (let i = 0; i < listObject.length; i++) {
                    var p = { id: listObject[i].productId, type: listObject[i].type }
                    obj.push(p)
                }
                var cmd = this.CommentFakes;
                var data = {
                    objs: listObject,
                    comments: cmd
                }
                console.log(data);
                this.createCommentFake(data).then(function (el) {
                    alert(el);
                })


            },
            addNewCommentItem() {
                var a = {
                    Type: "",
                    Name: "",
                    Description: "",
                    Rating: 0
                }
                this.CommentFakes.push(a);
            },
            openImg(img) {
                this.choseImg = img;
                EventBus.$emit(this.mikey1, '');
            },

            DoAttackFile(value) {
                let vm = this;
                for (let i = 0; i < vm.ListValue.length; i++) {
                    if (vm.ListValue[i].active == true) {
                        vm.ListValue[i].bigThumb = value[0].path;
                    }
                }
            },
            toggleActive(item) {
                if (this.activeItem[item.id]) {
                    this.removeActiveItem(item);
                    return;
                }

                this.addActiveItem(item);
            },
            
            addActiveItem(item) {
                this.activeItem = Object.assign({},
                    this.activeItem ? [item.id] : item,
                );
            },
            removeActiveItem(item) {
                delete this.activeItem[item.id];
                this.activeItem = Object.assign({}, this.activeItem);
            },
            onLoadProduct() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getItemAllForCombo({
                    keyword: this.keyword,
                    idZone: this.SearchZoneId || 0,
                    pageSize: this.pageSize,
                    pageIndex: this.currentPage,

                }).then(respose => {
                    this.ListProduct = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));
                    console.log(this.ListProduct);
                    this.total = respose.total;
                })
            },
            onLoadProductByRegion() {
                //debugger-
                this.getProductByRegion(
                    {
                        zoneId: this.ChoseZoneId || 0
                    }).then(respose => {
                        this.ListValue = respose.map(item => ({
                            ...item,
                            active: false
                        }));
                        console.log(this.ListValue);
                    })
            },
            onLoadProductCombo() {
                this.activeLeft = [];
                this.activeRight = [];
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getComboById(this.productId).then(respose => {
                    this.ListValue = respose.listData.map(item => ({
                        ...item,
                        active: false
                    }));

                })
            },
            AddProductByRegion: function () {
                //debugger-
                if (this.ChoseZoneId > 0) {
                    this.addProductByRegion({
                        zoneId: this.ChoseZoneId || 0,
                        Regions: this.ListValue,
                    }).then(response => {
                        if (response.success == true) {
                            this.$toast.success(response.message, {});

                        } else {
                            this.$toast.error(response.message, {});
                        }
                    }).catch(e => {
                        this.$toast.error("Lỗi hệ thống");

                    });
                } else {
                    this.$toast.error("Chưa chọn vùng");
                }

            },

            onetoRight: function () {
                var vm = this;
                var data = vm.ListZone;
                var lstChose = this.ListProduct.filter(x => x.active == true).map(item => ({
                    productId: item.id,
                    isHot: false,
                    productName: item.name,
                    avatar: item.avatar,
                    zoneId: vm.ChoseZoneId,
                    bigThumb: "",
                    active: false,
                    type: item.type,
                    zoneName: vm.ListZoneRight.filter(x => x.id == vm.ChoseZoneId)[0].label
                }));

                this.ListProduct = this.ListProduct.map(item => ({
                    ...item,
                    active: false
                }))

                this.ListValue = [...this.ListValue, ...lstChose];



                //if (this.ChoseZoneId > 0) {
                    

                //} else {
                //    this.$toast.error("Chưa chọn vùng", {});
                //}

            },
            onetoRemove: function () {
                //debugger-
                this.ListValue = this.ListValue.filter(x => x.active != true);
            },

            changeValueLeft: function (id) {
                if (this.ListProduct[id].active == true) {
                    this.ListProduct[id].active = false;
                } else {
                    this.ListProduct[id].active = true;
                }
            },
            changeValueRight: function (id) {
                if (this.ListValue[id].active == true) {
                    this.ListValue[id].active = false;
                } else {
                    this.ListValue[id].active = true;
                }
            },
            ExistId(id) {

            },
            unflattenBase(data) {
                return unflatten(data);
            },
        },
        created() {
            var vm = this;
            EventBus.$on('FileSelected', value => {
                //debugger-


                console.log(vm.ListValue);
            })

            this.getAllRegion().then(respose => {
                this.ListRegion = respose;
            });

            this.getZones(0).then(respose => {
                try {
                    //debugger-
                    respose.listData.push({ id: 0, label: "Chọn danh mục cha", parentId: 0 });
                    var data = respose.listData;
                    var dataRight = data.filter(x => x.type == 7 || x.id == 0);
                    var bv = data.filter(x => x.type != 1 || x.type != 7);

                    var dataLeft = data.filter(x => x.type == 1 || x.id == 0);
                    dataLeft.push(...bv);
                    this.ListZone = dataLeft;
                    this.ListZoneRight = dataRight;
                }
                catch (ex) {

                }

            });
            this.onLoadProduct();
        },
        destroyed() {
            EventBus.$off('FileSelected');
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onLoadProduct();
            },

            ChoseZoneId: function (newVal) {

                //debugger-

                this.onLoadProductByRegion();
            },
            SearchZoneId: function (newVal) {
                this.onLoadProduct();
            },
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
        font-size: 15px;
    }

    .pagination {
        margin-top: 10px
    }
</style>

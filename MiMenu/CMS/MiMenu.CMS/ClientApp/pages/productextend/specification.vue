<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <div style="display:flex;width:100%;border-bottom:none">
                                <div style="display:flex;width:100%">
                                    <treeselect :multiple="false"
                                                :options="LoadDanhMuc()"
                                                placeholder="Xin mời bạn lựa chọn danh mục"
                                                v-model="searchZoneId"
                                                :default-expanded-level="Infinity" />
                                </div>

                                <!--<div style="display:flex;width:40%">
                                    <div class="input-group">
                                        <input type="text" v-model="searchKey" autocomplete="off" class="form-control" placeholder="Tìm kiếm thông số">
                                        <span class="input-group-addon bg-primary" style="cursor: pointer;width:45px"><i style="padding-top:10px;padding-left:15px" class="fa fa-search"></i></span>
                                    </div>
                                </div>-->
                            </div>
                            <div class="dropdown-menu" id="_list-3" style="display:block;position:unset;width:100%;">
                                <button v-for="(item,index) in ListTemp" @click="changeValueLeft(index)" :class="['dropdown-item',{'active':item.active},{'hidden':item.hidden}]">
                                    <p style="margin-bottom:5px">Tên thông số: {{item.name}}</p>
                                    <p style="margin-bottom:5px" class="text-muted">Đơn vị: {{item.unit}}</p>
                                </button>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoRight"><i class="fa fa-angle-double-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-primary btn-block mb-2" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-danger btn-block mb-2" @click="onetoLeft"><i class="fa fa-trash" aria-hidden="true"></i></button>
                            <!--<button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoLeft"><i class="fa fa-angle-double-left" aria-hidden="true"></i></button>-->
                        </div>
                        <div class="col-md-6">
                            <div style="display:flex;width:100%;border:1px solid #ccc;border-bottom:none">
                                <div style="display:flex;width:100%" class="text-center">
                                    <div style="padding-left:0" class="col-md-6">
                                        <select v-model="searchLanguageCode" class="form-control">
                                            <option :value="item.value" v-for="item in Language">
                                                {{item.text}}
                                            </option>
                                        </select>

                                    </div>
                                    <div class="col-md-6" style="padding-right:0">
                                        <button class="btn pull-right btn-info btn-submit-form btncus" type="submit" @click="AddPromotion()">
                                            <i class="fa fa-save"></i> Cập nhật
                                        </button>
                                    </div>
                                </div>

                            </div>

                            <div class="dropdown-menu" style="display:block;position:unset;width:100%;margin-top:0">
                                <button :class="['dropdown-item',{'active':item.active},{'hidden':item.hidden}]" @click="changeValueRight(index)" v-for="(item,index) in ListValue">
                                    <p style="margin-bottom:5px">Tên thông số: {{item.name}}</p>
                                    <div style="display:flex;width:100%">
                                        <p style="margin-bottom:5px;display:flex;width:75px" class="text-muted">Thông số : </p>
                                        <input style="height: 20px;display:flex;width:30%" class="col-md-4 form-control" v-model="item.value" />
                                        <p style="display:flex;padding-left:10px;margin-bottom:0px;"> <b-form-checkbox v-model="item.isShowLayout">Hiển thị trên menu</b-form-checkbox></p>
                                    </div>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>

</template>

<script>

    import { mapActions } from "vuex";
    import Treeselect from '@riophae/vue-treeselect';
    // Import component

    export default {
        name: "specificationinproduct",
        components: {
            Treeselect
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
            },
            category: {
                type: Array
            }
        },
        data() {
            return {
                ListTemp: [],
                ListSpecifications: [],
                ListValue: [],
                searchLanguageCode: "vi-VN",
                searchZoneId: 0,
                searchKey: "",

                activeLeft: 0,
                activeRight: 0,
                Language: [],
            };
        },
        created() {
            this.getAllLanguages().then(respose => {
                this.Language = respose.listData.map(function (item) {
                    return {
                        value: item.languageCode.trim(),
                        text: item.name
                    };
                });
            });
        },
        methods: {
            ...mapActions(["getSpecificationByProducts", "addSpecifications", "getAllLanguages"]),
            onLoadSpecification() {
                //debugger-
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getSpecificationByProducts(this.productId).then(respose => {
                    this.ListSpecifications = respose.ListObj1;
                    this.ListValue = respose.ListObj2.map(item => ({
                        ...item,
                        active: false,
                       
                    }));

                })
            },
            AddPromotion: function () {
                //debugger-
                this.addSpecifications({
                    Id: this.productId,
                    Specifications: this.ListValue
                }).then(response => {
                    if (response.success == true) {
                        this.$toast.success(response.message, {});

                    } else {
                        this.$toast.error(response.message, {});

                    }

                }).catch(e => {
                    this.$toast.error("Lỗi hệ thống");

                });;
            },
            alltoRight: function () {
                var data = this.ListTemp.map(item => ({
                    productId: this.productId,
                    value: "",
                    name: item.name,
                    isShowLayout: false,
                    languageCode: this.searchLanguageCode,
                    spectificationId: item.id,
                    active: false
                }));
                this.ListValue = [...this.ListValue, ...data];

                this.ListSpecifications = [];
            },
            onetoRight: function () {
                //debugger-
                let lstChose = this.ListTemp.filter(x => x.active == true && x.hidden == false).map(item => ({
                    productId: this.productId,
                    value: "",
                    name: item.name,
                    isShowLayout: false,
                    languageCode: this.searchLanguageCode,
                    spectificationId: item.id,
                    active: false
                }));

                this.ListValue = [...this.ListValue, ...lstChose];



            },
            alltoLeft: function () {
                this.ListSpecifications = [...this.ListSpecifications, ...this.ListValue];
                this.ListValue = [];
            },
            LoadDanhMuc() {
                return this.category.map(x => {
                    return { id: x.id, label: x.label }
                })
            },
            onetoLeft: function () {

                this.ListValue = this.ListValue.filter(item => item.active == false);
            },
            changeValueLeft: function (id) {
                if (this.ListTemp[id].active == true) {
                    this.ListTemp[id].active = false;
                } else {
                    this.ListTemp[id].active = true;
                }
            },
            changeValueRight: function (id) {
                if (this.ListValue[id].active == true) {
                    this.ListValue[id].active = false;
                } else {
                    this.ListValue[id].active = true;
                }
            },

            filterFunction: function () {
                this.ListSpecifications = this.ListSpecifications.map(x => ({
                    ...x,
                    hidden: false,
                }));
                let vm = this;

                //debugger-
                if (this.searchKey != null && this.searchKey != undefined && this.searchKey.length > 0) {
                    //debugger-

                    this.ListSpecifications = this.ListSpecifications.reduce(function (ListNew, obj) {
                        if (vm.searchKey.toUpperCase().indexOf(obj.name) <= -1) {
                            obj.hidden = true;
                            ListNew.push(obj)
                        }
                        return ListNew;
                    }, [])
                }
            }

        },
        watch: {
            actions: function (newVal) {

                if (newVal == true) {
                    this.onLoadSpecification();
                }
            },
            searchKey: function (newVal) {

                this.filterFunction();
            },
            searchZoneId: function (newVal) {
                if (this.ListSpecifications.hasOwnProperty(newVal)) {
                    this.ListTemp = this.ListSpecifications[newVal].map(item => ({
                        ...item,
                        active: false,
                        hidden: false
                    }));
                }
               
            },
            searchLanguageCode: function (newVal) {

                for (let i = 0; i < this.ListValue.length; i++) {
                    if (this.ListValue[i].languageCode.trim() == newVal.trim()) {
                        this.ListValue[i].hidden = false;
                    } else {
                        this.ListValue[i].hidden = true;
                    }
                    this.ListValue.active = false;
                }
                
                console.log(this.ListValue);
            }
        }
    };
</script>
<style>
</style>

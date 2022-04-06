<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <div style="display:flex;width:100%;border-bottom:none">
                                <div style="display:flex;width:20%;padding-top:8px">Tìm kiếm: </div>
                                <div style="display:flex;width:80%">
                                    <div class="input-group">
                                        <input type="text" autocomplete="off" class="form-control" placeholder="Tìm kiếm khuyến mại" id="myInput2" v-on:keyup="filterFunction()">
                                        <span class="input-group-addon bg-primary" style="cursor: pointer;width:45px"><i style="padding-top:10px;padding-left:15px" @click="filterFunction()" class="fa fa-search"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown-menu" id="_list-2" style="display:block;position:unset;width:100%;height:300px;overflow-x:scroll">
                                <button v-for="(item,index) in ListPromotion" @click="changeValueLeft(index)" :class="['dropdown-item',{'active':item.active}]">
                                    <p style="margin-bottom:5px"> {{item.name}}</p>
                                    <p style="margin-bottom:5px" class="text-muted"> {{item.des}}</p>
                                </button>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoRight"><i class="fa fa-angle-double-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-primary btn-block mb-2" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-primary btn-block mb-2" @click="onetoLeft"><i class="fa fa-angle-left" aria-hidden="true"></i></button>
                            <button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoLeft"><i class="fa fa-angle-double-left" aria-hidden="true"></i></button>
                        </div>
                        <div class="col-md-6">
                            <div style="display:flex;width:100%;border:1px solid #ccc;border-bottom:none">
                                <div style="padding-top:5px" class="col-md-8 text-center">Thông tin khuyến mại</div>
                                <div class="col-md-4" style="padding-right:0">
                                    <button class="btn btn-info btn-submit-form pull-right btncus" type="submit" @click="AddPromotion()">
                                        <i class="fa fa-save"></i> Lưu
                                    </button>
                                </div>
                            </div>
                            <div class="dropdown-menu" style="display:block;position:unset;width:100%;margin-top:0">
                                <button :class="['dropdown-item',{'active':item.active}]" @click="changeValueRight(index)" v-for="(item,index) in ListValue">
                                    <p style="margin-bottom:5px"> {{item.name}}</p>
                                    <p style="margin-bottom:5px" class="text-muted"> {{item.des}}</p>
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



    // Import component

    export default {
        name: "promotioninproduct",
        components: {

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

                ListPromotion: [],
                ListValue: [],

                activeLeft: 0,
                activeRight: 0,
            };
        },

        methods: {
            ...mapActions(["getPromotionByProducts", "addPromotions"]),
            onLoadPromotion() {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getPromotionByProducts(this.productId).then(respose => {
                    console.log(respose);
                    this.ListPromotion = respose.listObj1.map(item => ({
                        ...item,
                        active: false
                    }));
                    this.ListValue = respose.listObj2.map(item => ({
                        ...item,
                        active: false
                    }));;
                })
            },
            AddPromotion: function () {
                //debugger-
                this.addPromotions({
                    Id: this.productId,
                    Promotions: this.ListValue
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
                this.ListValue = [...this.ListValue, ...this.ListPromotion];
            },
            onetoRight: function () {
                //debugger-
                let lstChose = this.ListPromotion.filter(x => x.active == true);
                this.ListPromotion = this.ListPromotion.filter(item => item.active == false);

                this.ListValue = [...this.ListValue, ...lstChose];

                //if (this.activeLeft > 0) {
                //    //debugger-
                //    var obj = this.ListPromotion.filter(word => word.locationId === this.activeLeft);
                //    this.ListValue.push(obj[0]);

                //    this.ListPromotion = this.ListPromotion.filter(word => word.locationId != this.activeLeft);
                //    this.activeLeft = 0;
                //}

            },
            alltoLeft: function () {
                //let lstChose = this.ListValue.filter(x => x.active == true);
                //this.ListValue = this.ListValue.filter(item => item.active == false);

                this.ListPromotion = [...this.ListPromotion, ...this.ListValue];

                //this.activeRight = 0;

                //for (let i = 0; i < this.ListValue.length; i++) {
                //    this.ListPromotion.push(this.ListValue[i]);
                //}

                //this.ListValue = [];
            },
            onetoLeft: function () {
                //debugger-
                let lstChose = this.ListValue.filter(x => x.active == true);
                this.ListValue = this.ListValue.filter(item => item.active == false);
                this.ListPromotion = [...this.ListPromotion, ...lstChose];

                //if (this.activeRight > 0) {
                //    var obj = this.ListValue.filter(word => word.locationId === this.activeRight);
                //    this.ListPromotion.push(obj[0]);
                //    this.ListValue = this.ListValue.filter(word => word.locationId !== this.activeRight);
                //    this.activeRight = 0;
                //}

            },
            changeValueLeft: function (id) {
                if (this.ListPromotion[id].active == true) {
                    this.ListPromotion[id].active = false;
                } else {
                    this.ListPromotion[id].active = true;
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
                //debugger-
                var input, filter, i, a, div;
                input = document.getElementById("myInput2");
                filter = input.value.toUpperCase();
                div = document.getElementById("_list-1");
                a = div.getElementsByTagName("button");
                for (i = 0; i < a.length; i++) {
                    //debugger-
                    var txtValue = a[i].textContent || a[i].innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        a[i].style.display = "";
                    } else {
                        a[i].style.display = "none";
                    }
                }
            }

        },
        watch: {
            actions: function (newVal) {
                //debugger-
                if (newVal == true) {
                    this.onLoadPromotion();
                }
            }
        }
    };
</script>
<style>
    .productedit .form-control {
    }
</style>

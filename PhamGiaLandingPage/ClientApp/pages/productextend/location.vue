<template>
    <div class="row productedit">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div style="display:flex;width:100%;border-bottom:none">
                                <div style="display:flex;width:20%;padding-top:8px">Tìm kiếm: </div>
                                <div style="display:flex;width:80%">
                                    <div class="input-group">
                                        <input type="text" autocomplete="off" class="form-control" placeholder="Tìm kiếm địa chỉ" id="myInput" v-on:keyup="filterFunction()">
                                        <span class="input-group-addon bg-primary" style="cursor: pointer;width:45px"><i style="padding-top:10px;padding-left:15px" @click="filterFunction()" class="fa fa-search"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown-menu" id="list-1" style="display:block;position:unset;width:100%;height:300px;overflow-x:scroll">
                                <button v-for="(item,index) in ListLocation" @click="changeValueLeft(index)" :class="['dropdown-item',{'active':item.active}]">{{item.ten}}</button>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoRight"><i class="fa fa-angle-double-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-primary btn-block mb-2" @click="onetoRight"><i class="fa fa-angle-right" aria-hidden="true"></i></button>
                            <button title="Chuyển các bản ghi đã chọn" class="btn btn-primary btn-block mb-2" @click="onetoLeft"><i class="fa fa-angle-left" aria-hidden="true"></i></button>
                            <button title="Chuyển tất cả bản ghi" class="btn btn-primary btn-block mb-2" @click="alltoLeft"><i class="fa fa-angle-double-left" aria-hidden="true"></i></button>
                        </div>
                        <div class="col-md-7">
                            <div style="display:flex;width:100%;border:1px solid #ccc;border-bottom:none">
                                <div style="padding-top:5px" class="col-md-3 text-center">Khu vực</div>
                                <div style="padding-top:5px" class="col-md-3  text-center">Giá </div>
                                <div style="padding-top:5px" class="col-md-2  text-center">Giảm %</div>
                                <div style="padding-top:5px" class="col-md-2  text-center">Giá giảm</div>
                                <button class="btn btn-info btn-submit-form col-md-2 btncus" type="submit" @click="Add()">
                                    <i class="fa fa-save"></i> Lưu
                                </button>
                            </div>

                            <div class="dropdown-menu" style="display:block;position:unset;width:100%;margin-top:0">
                                <button :class="['dropdown-item',{'active':item.active}]" @click="changeValueRight(index)" v-for="(item,index) in ListValue">
                                    <div style="display:flex;width:100%">
                                        <div class="col-md-3">
                                            {{item.ten}}
                                        </div>
                                        <div class="col-md-3">
                                            <input style="height:20px" @change="changeValue(item)" min="0" type="number" class="form-control" v-model="item.price" />
                                        </div>
                                        <div class="col-md-2">
                                            <input style="height:20px" @change="changeValue(item)" min="0" max="100" type="number" class="form-control" v-model="item.discountPercent" />
                                        </div>
                                        <div class="col-md-3">
                                            <input style="height:20px" min="0" type="number" class="form-control" v-model="item.salePrice" />
                                        </div>
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

    export default {
        name: "locationinproduct",
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

                ListLocation: [],
                ListValue: [],

                activeLeft: 0,
                activeRight: 0,
            };
        },
        methods: {
            ...mapActions(["getPriceByLocations", "updateLocations"]),

            onLoadLocation(id) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getPriceByLocations(id).then(respose => {
                    this.ListLocation = respose.listObj1.map(item => ({
                        ...item,
                        active: false
                    }));
                    this.ListValue = respose.listObj2.map(item => ({
                        ...item,
                        active: false
                    }));;
                })
            },


            Add: function () {
                //debugger-
                this.updateLocations({
                    Id: this.productId,
                    PriceInLocation: this.ListValue
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
            changeValue: function (item) {
                item.salePrice = item.price - (item.price * item.discountPercent / 100)
            },
            alltoRight: function () {
                this.ListValue = [...this.ListValue, ...this.ListLocation];
                this.ListLocation = [];
            },
            onetoRight: function () {
                //debugger-
                let lstChose = this.ListLocation.filter(x => x.active == true);
                this.ListLocation = this.ListLocation.filter(item => item.active == false);

                this.ListValue = [...this.ListValue, ...lstChose];

                //if (this.activeLeft > 0) {
                //    //debugger-
                //    var obj = this.ListLocation.filter(word => word.locationId === this.activeLeft);
                //    this.ListValue.push(obj[0]);

                //    this.ListLocation = this.ListLocation.filter(word => word.locationId != this.activeLeft);
                //    this.activeLeft = 0;
                //}
            },
            alltoLeft: function () {
                //let lstChose = this.ListValue.filter(x => x.active == true);
                //this.ListValue = this.ListValue.filter(item => item.active == false);

                this.ListLocation = [...this.ListLocation, ...this.ListValue];
                this.ListValue = [];
                //this.activeRight = 0;

                //for (let i = 0; i < this.ListValue.length; i++) {
                //    this.ListLocation.push(this.ListValue[i]);
                //}

                //this.ListValue = [];
            },
            onetoLeft: function () {
                //debugger-
                let lstChose = this.ListValue.filter(x => x.active == true);
                this.ListValue = this.ListValue.filter(item => item.active == false);
                this.ListLocation = [...this.ListLocation, ...lstChose];

                //if (this.activeRight > 0) {
                //    var obj = this.ListValue.filter(word => word.locationId === this.activeRight);
                //    this.ListLocation.push(obj[0]);
                //    this.ListValue = this.ListValue.filter(word => word.locationId !== this.activeRight);
                //    this.activeRight = 0;
                //}

            },
            changeValueLeft: function (id) {
                if (this.ListLocation[id].active == true) {
                    this.ListLocation[id].active = false;
                } else {
                    this.ListLocation[id].active = true;
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
                var input, filter, i, a, div;
                input = document.getElementById("myInput");
                filter = input.value.toUpperCase();
                div = document.getElementById("list-1");
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
            productId: function (newVal) {
                //debugger-
                if (newVal > 0) {
                    this.onLoadLocation(newVal);
                }
            }
        }
    };
</script>
<style>
    .productedit .form-control {
    }
</style>

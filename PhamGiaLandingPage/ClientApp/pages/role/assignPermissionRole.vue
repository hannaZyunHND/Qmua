<template>
    <div>
        <b-row>
            <b-col>
                <b-container class="col-12">
                    <div id='permissionsTable'>
                        <b-row class='headerRow'>
                            <b-col cols='3'>Danh mục chức năng</b-col>
                            <b-col v-for="itemF in actionss" v-bind:key="itemF.name">{{itemF.name}}</b-col>
                        </b-row>
                        <b-row v-for="itemA in functions" v-bind:key="itemA.name" class="bodyRow">
                            <b-col cols='3' v-if="itemA.parentId==null">{{itemA.name}}</b-col>
                            <b-col cols='3' v-if="itemA.parentId!=null">-- {{itemA.name}}</b-col>
                            <b-col v-for="itemF in actionss" v-bind:key="itemF.name">
                                <b-form-checkbox-group v-bind:id="itemF.name"
                                                       v-bind:name="itemF.name + 'Danh mục chức năng'"
                                                       v-model="itemF.functions">
                                    <b-form-checkbox v-bind:value="itemA.id"></b-form-checkbox>
                                </b-form-checkbox-group>
                            </b-col>
                        </b-row>
                    </div>
                    <div class="mt-3">
                        <div class="row">
                            <div class="col-md-12">
                                <button class="btn btn-info btn-submit-form btncus float-right" type="button"
                                        @click="assignPermission()">
                                    <i class="fa fa-save"></i> Cập nhật
                                </button>
                            </div>
                        </div>
                    </div>
                </b-container>
            </b-col>
        </b-row>
    </div>

</template>


<script>
 
    import {mapActions} from "vuex";
    import HttpService from "../../plugins/http";
    import roleRepository from "../../repository/roleRepository/roleRepository";

    export default {
        name: 'assignPermissionRole',
        data() {
            return {
                functions: [],

                actionss: [],
            };
        },
        methods: {
            ...mapActions(["getListFunctions", "getListActions"]),
            async getFunction() {
                let response = await HttpService.get(`/api/role/getallfunctions`
                ).catch(e => {
                    alert('ex found:' + e)
                });
                console.log(response.data);
                this.functions = response.data;
            },
            async getAction() {
                if (this.$route.params.id) {
                    let id = this.$route.params.id;
                    let response = await HttpService.get(`/api/role/getallactions?roleid=${id}`
                    ).catch(e => {
                        alert('ex found:' + e)
                    });
                    console.log(response.data);
                    this.actionss = response.data;
                }

            },
            async assignPermission() {
                if (this.$route.params.id) {
                    let id = this.$route.params.id;
                    let data = this.actionss;
                    console.log(data);
                    let result;
                    result = await roleRepository.assignPermissionRole(id, data);
                    console.log(result);
                    if (result.success == true) {
                        this.$toast.success("tạo thành công", {});
                        this.isLoading = false;
                        this.$router.go(-1)
                    } else {
                        this.$router.go(-1);
                        this.$toast.error("cập nhật thất bại", {});
                        this.isLoading = false;
                    }
                }
            }

        },
        async created() {
            await this.getAction();
            await this.getFunction();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .headerRow {
        padding: .75rem;
        background-color: #000000;
        color: #ffffff;
        font-weight: bold;
        vertical-align: bottom;
        border-bottom: 2px solid #dee2e6;
    }

    .bodyRow {
        padding: .75rem;
        border-top: 1px solid #dee2e6;
    }
</style>

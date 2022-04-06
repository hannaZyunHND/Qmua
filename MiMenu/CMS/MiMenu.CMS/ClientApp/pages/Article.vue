<template>
    <div>
        <table class="table">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Tiêu đề</th>
                    <th scope="col">Tác giả</th>
                    <th scope="col">Nguồn</th>
                    <th scope="col">Thao tác</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in articless">
                    <th scope="row">{{item.Id}}</th>
                    <td>{{item.Title}}</td>
                    <td>{{item.Author}}</td>
                    <td>{{item.Source}}</td>
                    <td>
                        <button class="btn btn-xs btn-warning" @click="openModal(item.Id)"><i class="fa fa-edit"></i></button>
                        <button class="btn btn-xs btn-danger"><i class="fa fa-minus-circle"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>

        <b-pagination v-model="currentPage"
                      :total-rows="total"
                      :per-page="pageSize"
                      aria-controls="_article" @change></b-pagination>


        <!--<b-modal id="modal-center-add-content" style="width: 120px"
                 centered class="h-50 d-inline-block min-vw-100"
                 ok-only ok-title="Create">
            <add-content></add-content>
        </b-modal>-->

        <b-modal id="modal-article" hide-footer title="Thêm/sửa bài viết">
            <form>

                <fieldset class="form-group">
                    <input type="text"
                           class="form-control"
                           :value="article.Title"
                           @input="updateUserObj('Title',$event)"
                           placeholder="Tiêu đề" />
                </fieldset>
                <fieldset class="form-group">
                    <input type="text"
                           class="form-control"
                           :value="article.Description"
                           @input="updateUserObj('Description',$event)"
                           placeholder="What's this article about?" />
                </fieldset>
                <fieldset class="form-group">
                    <textarea class="form-control"
                              rows="8"
                              :value="article.Body"
                              @input="updateUserObj('Body',$event)"
                              placeholder="Write your article (in markdown)">
                </textarea>
                </fieldset>
            </form>
            <b-button class="col-md-5" variant="outline-warning" @click="onPublish(article.Id)">Lưu</b-button>

        </b-modal>
    </div>
</template>
<script>

    import { mapGetters, mapActions } from "vuex";

    export default {
        name: "Articles",
        computed: {
            ...mapGetters(["articless", "total", "article"])


            //article() {
            //    return this.$store.state.article
            //}
        },
        data() {
            return {

                userObj: {},

                currentPage: 1,
                pageSize: 10,
                loading: true,
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
        methods: {
            ...mapActions(["getArticles", "getArticle", "updateArticle"]),

            onChangePaging() {
                console.log(this.currentPage);
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getArticles({
                    initial: initial,
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize
                });
            },
            openModal(id) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                this.getArticle({
                    initial: initial,
                    Id: id
                });



                this.$bvModal.show("modal-article");
            },
            onPublish(id) {
                let initial = this.$route.query.initial;
                initial = typeof initial != "undefined" ? initial.toLowerCase() : "";
                if (id > 0) {
                    var finalUserObj = Object.assign({}, this.article, this.userObj)
                    this.updateArticle({
                        initial: initial,
                        article: finalUserObj
                    });
                }
                else {
                    console.log("Add");
                    this.addArticle({
                        initial: initial,
                        article: this.article
                    });
                }
                this.$bvModal.hide("modal-article");
            },
            updateUserObj(attribute, e) {

                this.userObj[attribute] = e.target.value;
                console.log(this.userObj);

            },
        },

        mounted() {
            this.onChangePaging();
        },
        watch: {
            currentPage: function (newVal) {
                this.currentPage = newVal;
                this.onChangePaging();
            },
            //Article: {
            //    handler: _.debounce(function (Article) {
            //        console.log(Article);
            //        this.$store.commit("UPDATE_ARTICLE_CLONE", Article);
            //    }, 500), deep: true
            //}
        },

    };
</script>

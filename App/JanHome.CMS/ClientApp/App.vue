<template>
    <div id="app-main">
        <router-view></router-view>
    </div>
</template>
<script>
    export default {
        name: "app"
    };
</script>
<style lang="scss">
    @import "~@coreui/icons/css/coreui-icons.min.css";
    $fa-font-path: "~font-awesome/fonts/";
    @import "~font-awesome/scss/font-awesome.scss";
    $simple-line-font-path: "~simple-line-icons/fonts/";
    @import "~simple-line-icons/scss/simple-line-icons.scss";
    @import "./assets/scss/tool-arraw.scss";
    @import "~bootstrap-vue/dist/bootstrap-vue.css";
    @import "~vue-select/src/scss/vue-select.scss";
    @import "./assets/scss/style";
</style>

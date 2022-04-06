<template>
    <div class="app">
        <DefaultHeader/>
        <div class="app-body">
            <AppSidebar fixed>
                <SidebarHeader/>
                <SidebarForm/>
                <SidebarNav :navItems="nav"></SidebarNav>
                <SidebarFooter/>
                <SidebarMinimizer/>
            </AppSidebar>
            <main class="main">
                <Breadcrumb :list="list"/>
                <div class="container-fluid">
                    <router-view></router-view>
                    <!--<transition name="slide-fade">

            </transition>-->
                </div>
            </main>
           
        </div>
        <!--<DefaultFooter/>-->
    </div>
</template>

<script>
    //import nav from './shared/_nav'
    import {
        Sidebar as AppSidebar,
        SidebarFooter,
        SidebarForm,
        SidebarHeader,
        SidebarMinimizer,
        SidebarNav,
        Aside as AppAside,
        Breadcrumb
    } from "@coreui/vue";
    //import DefaultAside from './DefaultAside'
    import DefaultHeaderDropdownAccnt from "./DefaultHeaderDropdownAccnt";
    import DefaultHeader from "./DefaultHeader";
    import DefaultFooter from "./DefaultFooter";
    import HttpService from "../plugins/http";
    import {router} from "../router";

    export default {
        name: "DefaultContainer",
        components: {
            AppSidebar,
            AppAside,
            Breadcrumb,
            //DefaultAside,
            DefaultHeaderDropdownAccnt,
            SidebarForm,
            SidebarFooter,
            SidebarHeader,
            SidebarNav,
            SidebarMinimizer,
            DefaultFooter,
            DefaultHeader
        },
        data() {
            return {
                // nav: nav.items
                nav: []
            };
        },
        created() {
            const getAllFunction = async () => {
                const response = await HttpService.get(
                    `/api/permission/functions-view`
                ).catch(e => {
                    localStorage.removeItem('currentUser');
                    router.push('/admin/login');
                });
                
                this.nav = response.data;
                return response.data;
            };
            getAllFunction();
        },
        computed: {
            name() {
                return this.$route.name;
            },
            list() {
                return this.$route.matched.filter(
                    route => route.name || route.meta.label
                );
            }
        }
    };
</script>

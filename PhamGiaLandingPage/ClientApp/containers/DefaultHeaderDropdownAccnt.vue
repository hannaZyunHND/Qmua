<template>
    <AppHeaderDropdown right no-caret>
        <template slot="header">
            <img
                src="./../assets/img/avatars/6.jpg"
                class="img-avatar"
                alt="admin@bootstrapmaster.com"/>
        </template>

        <template slot="dropdown">
            <b-dropdown-item>
                <i class="fa fa-user"/>
                <a @click="profile">Hồ sơ người dùng</a>
            </b-dropdown-item>
            <b-dropdown-divider/>
            <b-dropdown-item>
                '<i class="fa fa-lock"/>
                <a @click="logout">Đăng xuất</a>
            </b-dropdown-item>
        </template>
    </AppHeaderDropdown>
</template>

<script>
    import {HeaderDropdown as AppHeaderDropdown} from '@coreui/vue'
    import {authenticationRepository} from "../repository/authentication/authenticationRepository";
    import {router} from "../router/index";

    export default {
        name: 'DefaultHeaderDropdownAccnt',
        components: {
            AppHeaderDropdown
        },
        data: () => {
            return {itemsCount: 42}
        },
        methods: {
            logout() {
                authenticationRepository.logout();
                router.push('/admin/login');
            },
            profile(){
                router.push('/admin/profile');
            }
        }
    }
</script>

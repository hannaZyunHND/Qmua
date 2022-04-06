<template>
    <div class="app flex-row align-items-center">
        <div class="container">
            <b-row class="justify-content-center">
                <b-col md="8">
                    <b-card-group>
                        <b-card no-body class="p-4">
                            <b-card-body>
                                <b-form @submit.prevent="DoLogin">
                                    <h1>Đăng nhập</h1>
                                    <p class="text-muted">Đăng nhập vào tài khoản của bạn</p>
                                    <b-input-group class="mb-3">


                                        <b-input-group-prepend>
                                            <b-input-group-text><i class="icon-user"></i></b-input-group-text>
                                        </b-input-group-prepend>
                                        <b-form-input type="text" v-model.trim="$v.username.$model"
                                                      name="username"
                                                      placeholder="Email đăng nhập"
                                                      class="form-control"
                                                      :class="{ 'is-invalid': submitted && $v.username.$error }"
                                                      autocomplete="username email"/>
                                        <div v-if="submitted && !$v.username.required" class="invalid-feedback">Bắt buộc
                                            phải nhập Email
                                        </div>
                                    </b-input-group>
                                    <b-input-group class="mb-4">
                                        <b-input-group-prepend>
                                            <b-input-group-text><i class="icon-lock"></i></b-input-group-text>
                                        </b-input-group-prepend>
                                        <b-form-input type="password" v-model.trim="$v.password.$model" name="password"
                                                      class="form-control"
                                                      :class="{ 'is-invalid': submitted && $v.password.$error }"
                                                      placeholder="Mật khẩu"
                                                      autocomplete="current-password"/>
                                        <div v-if="submitted && !$v.password.required" class="invalid-feedback">Bắt buộc
                                            phải nhập mật khẩu
                                        </div>
                                    </b-input-group>
                                    <b-row>
                                        <b-col cols="6">
                                            <b-button :disabled="loading" variant="primary" class="px-4"
                                                      v-on:click="DoLogin">
                                                <span class="spinner-border spinner-border-sm" v-show="loading"></span>
                                                <span>Đăng nhập</span>
                                            </b-button>
                                        </b-col>
                                    </b-row>
                                    <div v-if="error" class="alert alert-danger">{{error}}</div>
                                </b-form>
                            </b-card-body>
                        </b-card>
                    </b-card-group>
                </b-col>
            </b-row>
        </div>
    </div>
</template>

<script>
    import {authenticationRepository} from "../repository/authentication/authenticationRepository";
    import {router} from "../router/index";
    import {required} from 'vuelidate/lib/validators';

    export default {
        name: 'Login',
        data() {
            return {
                username: '',
                password: '',
                submitted: false,
                loading: false,
                returnUrl: '',
                error: ''
            };
        },
        validations: {
            username: {required},
            password: {required}
        },
        created() {
            // redirect to home if already logged in
            console.log(authenticationRepository.currentUserValue);
            if (authenticationRepository.currentUserValue) {
                return router.push('/');
            }
            // get return url from route parameters or default to '/'
            this.returnUrl = this.$route.query.returnUrl || '/';
        },
        methods: {
            DoLogin() {
                this.submitted = true;
                // stop here if form is invalid
                this.$v.$touch();
                if (this.$v.$invalid) {
                    return;
                }
                this.loading = true;
                authenticationRepository.login(this.username, this.password)
                    .then(
                        user => router.push(this.returnUrl),
                        error => {
                            this.error = error;
                            this.loading = false;
                        }
                    );
            }
        }
    }
</script>

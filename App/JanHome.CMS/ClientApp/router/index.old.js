import Vue from 'vue'
import Router from 'vue-router'
import { authenticationRepository } from "./repository/authentication/authenticationRepository";
// home
const DefaultContainer = () => import('./../containers/DefaultContainer');
const Dashboard = () => import('./../pages/Dashboard');


const Products = () => import('./../pages/product/list');

const ProductEdit = () => import('./../pages/product/edit');
const ProductExtent = () => import('./../pages/productextend/main');
const ProductRegion = () => import('./../pages/product/region');


const Promotions = () => import('./../pages/promotion/list');
const PromotionEdit = () => import('./../pages/promotion/edit');

const Propertys = () => import('./../pages/property/list');
const PropertyEdit = () => import('./../pages/property/edit');


const Forms = () => import('./../pages/Forms');
const Page404 = () => import('./../pages/Page404');
const Login = () => import('./../pages/Login');

const Config = () => import('./../pages/configs/list');
const ConfigEdit = () => import('./../pages/configs/edit');


const Zone = () => import('./../pages/zone/list');
const ZoneEdit = () => import('./../pages/zone/edit');


const Location = () => import('./../pages/location/list');
const LocationEdit = () => import('./../pages/location/edit');

const Language = () => import('./../pages/language/list');
const LanguageEdit = () => import('./../pages/language/edit');

const Ads = () => import('./../pages/ads/list');
const AdsEdit = () => import('./../pages/ads/edit');

const DepartmentEdit = () => import('./../pages/department/edit');
const Department = () => import('./../pages/department/list');
//Tag
const Tag = () => import('./../pages/tags/list');
const TagEdit = () => import('./../pages/tags/edit');

const Article = () => import('./../pages/Article/list');
const ArticleEdit = () => import('./../pages/Article/edit');


const Manufacturers = () => import('./../pages/manufacturer/list');
const ManufacturersEdit = () => import('./../pages/manufacturer/edit');

const ProductSpecificationTemplates = () => import('./../pages/productSpecificationTemplate/list');
const ProductSpecificationTemplatesEdit = () => import('./../pages/productSpecificationTemplate/edit');

const Role = () => import('./../pages/role/list');
const RoleEdit = () => import('./../pages/role/edit');
const RoleAssignPermission = () => import('./../pages/role/assignPermissionRole');

const Comment = () => import('./../pages/comment/list');

const Order = () => import('./../pages/orders/list');
const OrderDetail = () => import('./../pages/orders/listdetail');

const Customer = () => import('./../pages/customer/list');

Vue.use(Router);


export let router = new Router({
    mode: 'history',
    hashbang: false,
    history: true,
    linkActiveClass: "active",
    routes: [
        {
            path: '/',
            redirect: '/admin/dashboard',
            component: DefaultContainer,
            display: 'Home',
            style: 'glyphicon glyphicon-home',
            children: [
                {
                    path: '/admin/dashboard',
                    name: 'T???ng quan',
                    component: Dashboard,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/product/list',
                    name: 'Danh s??ch s???n ph???m',
                    component: Products,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/add',
                    name: 'Th??m m???i s???n ph???m',
                    component: ProductEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/edit/:id',
                    name: 'S???a s???n ph???m',
                    component: ProductEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/productextent/:id',
                    name: 'Ph???n m??? r???ng',
                    component: ProductExtent,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/region',
                    name: 'V??ng hi???n th???',
                    component: ProductRegion,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/config/list',
                    name: 'C???u h??nh',
                    component: Config,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/config/add',
                    name: 'Th??m m???i c???u h??nh',
                    component: ConfigEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/config/edit/:configGroupKey',
                    name: 'S???a c???u h??nh',
                    component: ConfigEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/comment/list',
                    name: 'Comment',
                    component: Comment,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/customer/list',
                    name: 'Kh??ch h??ng ti???m n??ng',
                    component: Customer,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/zone/list',
                    name: 'Nh??m b??i vi???t',
                    component: Zone,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/zone/add',
                    name: 'Th??m m???i nh??m b??i vi???t',
                    component: ZoneEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/zone/edit/:id',
                    name: 'S???a nh??m b??i vi???t',
                    component: ZoneEdit,
                    meta: { authorize: [] }

                },


                {
                    path: '/admin/location/list',
                    name: 'V??? tr??',
                    component: Location,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/location/add',
                    name: 'Th??m m???i v??? tr??',
                    component: LocationEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/location/edit/:id',
                    name: 'S???a v??? tr??',
                    component: LocationEdit,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/language/list',
                    name: 'Ng??n ng???',
                    component: Language,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/language/add',
                    name: 'Th??m ng??n ng???',
                    component: LanguageEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/language/edit/:id',
                    name: 'S???a ng??n ng???',
                    component: LanguageEdit,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/ads/list',
                    name: 'Qu???ng c??o',
                    component: Ads,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/ads/add',
                    name: 'Th??m m???i qu???ng c??o',
                    component: AdsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/ads/edit/:id',
                    name: 'S???a qu???ng c??o',
                    component: AdsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/list',
                    name: 'Danh s??ch ph??ng',
                    component: Department,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/add',
                    name: 'Th??m m???i ph??ng',
                    component: DepartmentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/edit/:id',
                    name: 'S???a ph??ng',
                    component: DepartmentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/list',
                    name: 'Danh s??ch Tags',
                    component: Tag,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/add',
                    name: 'Th??m m???i Tags',
                    component: TagEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/edit/:id',
                    name: 'S???a Tags',
                    component: TagEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/article/list',
                    name: 'Danh s??ch b??i vi???t',
                    component: Article,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/article/add',
                    name: 'Th??m m???i b??i vi???t',
                    component: ArticleEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/article/edit/:id',
                    name: 'S???a b??i vi???t',
                    component: ArticleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/list',
                    name: 'Danh s??ch nh?? cung c???p',
                    component: Manufacturers,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/add',
                    name: 'Th??m m???i nh?? cung c???p',
                    component: ManufacturersEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/edit/:id',
                    name: 'S???a nh?? cung c???p',
                    component: ManufacturersEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/list',
                    name: 'Danh s??ch khuy???n m??i',
                    component: Promotions,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/add',
                    name: 'Th??m m???i khuy???n m??i',
                    component: PromotionEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/edit/:id',
                    name: 'S???a khuy???n m??i',
                    component: PromotionEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/list',
                    name: 'Danh s??ch thu???c t??nh',
                    component: Propertys,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/add',
                    name: 'Th??m m???i thu???c t??nh',
                    component: PropertyEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/edit/:id',
                    name: 'S???a thu???c t??nh',
                    component: PropertyEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/list',
                    name: 'Danh s??ch th??ng s??? k??? thu???t',
                    component: ProductSpecificationTemplates,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/add',
                    name: 'Th??m m???i th??ng s??? k??? thu???t',
                    component: ProductSpecificationTemplatesEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/edit/:id',
                    name: 'S???a th??ng s??? k??? thu???t',
                    component: ProductSpecificationTemplatesEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/list',
                    name: 'Danh s??ch nh??m ng?????i d??ng',
                    component: Role,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/add',
                    name: 'Th??m m???i nh??m ng?????i d??ng',
                    component: RoleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/edit/:id',
                    name: 'S???a nh??m ng?????i d??ng',
                    component: RoleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/orders/list',
                    name: 'Danh s??ch ????n h??ng',
                    component: Order,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/orders/listdetail/:id',
                    name: 'Chi ti???t ????n h??ng',
                    component: OrderDetail,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/assignPermissionRole/:id',
                    name: 'Phan quyen cho nhom nguoi dung',
                    component: RoleAssignPermission,
                    meta: { authorize: [] }
                }
            ]

        },
        {
            path: '/pages',
            redirect: '/pages/404',
            name: 'Pages',
            component: {
                render(c) {
                    return c('router-view')
                }
            },
            children: [
                {
                    path: '404',
                    name: 'Page404',
                    component: Page404
                },

            ]
        },
        {
            path: '/admin/login',
            name: 'Login',
            component: Login
        }
    ]
});

router.beforeEach((to, from, next) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const { authorize } = to.meta;
    const currentUser = authenticationRepository.currentUserValue;
    if (to.path !== '/admin/login' && currentUser != null) {
        authenticationRepository.getCurrentUser().then(response => {

            if (!response.permissionUrl.includes(to.path)) {
                alert('B???n kh??ng c?? quy???n v??o link n??y');
                return next({ path: '/' });
            }
        });
    }
    authenticationRepository.getCurrentUser().then(response => {
        if (response.roles.includes("Admin")) {
            return;
        }
        if (!response.permissionUrl.includes(to.path)) {
            alert('B???n kh??ng c?? quy???n v??o link n??y');
            return next({ path: '/' });
        }
    });

    if (authorize) {
        if (!currentUser) {
            // not logged in so redirect to login page with the return url
            return next({ path: '/admin/login', query: { returnUrl: to.path } });
        }

        // check if route is restricted by role
        if (authorize.length && !authorize.includes(currentUser.role)) {
            // role not authorised so redirect to home page
            return next({ path: '/' });
        }
    }
    next();
});



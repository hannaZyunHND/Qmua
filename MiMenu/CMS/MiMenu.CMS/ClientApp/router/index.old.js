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
                    name: 'Tổng quan',
                    component: Dashboard,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/product/list',
                    name: 'Danh sách sản phẩm',
                    component: Products,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/add',
                    name: 'Thêm mới sản phẩm',
                    component: ProductEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/edit/:id',
                    name: 'Sửa sản phẩm',
                    component: ProductEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/productextent/:id',
                    name: 'Phần mở rộng',
                    component: ProductExtent,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/region',
                    name: 'Vùng hiển thị',
                    component: ProductRegion,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/config/list',
                    name: 'Cấu hình',
                    component: Config,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/config/add',
                    name: 'Thêm mới cấu hình',
                    component: ConfigEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/config/edit/:configGroupKey',
                    name: 'Sửa cấu hình',
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
                    name: 'Khách hàng tiềm năng',
                    component: Customer,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/zone/list',
                    name: 'Nhóm bài viết',
                    component: Zone,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/zone/add',
                    name: 'Thêm mới nhóm bài viết',
                    component: ZoneEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/zone/edit/:id',
                    name: 'Sửa nhóm bài viết',
                    component: ZoneEdit,
                    meta: { authorize: [] }

                },


                {
                    path: '/admin/location/list',
                    name: 'Vị trí',
                    component: Location,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/location/add',
                    name: 'Thêm mới vị trí',
                    component: LocationEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/location/edit/:id',
                    name: 'Sửa vị trí',
                    component: LocationEdit,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/language/list',
                    name: 'Ngôn ngữ',
                    component: Language,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/language/add',
                    name: 'Thêm ngôn ngữ',
                    component: LanguageEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/language/edit/:id',
                    name: 'Sửa ngôn ngữ',
                    component: LanguageEdit,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/ads/list',
                    name: 'Quảng cáo',
                    component: Ads,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/ads/add',
                    name: 'Thêm mới quảng cáo',
                    component: AdsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/ads/edit/:id',
                    name: 'Sửa quảng cáo',
                    component: AdsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/list',
                    name: 'Danh sách phòng',
                    component: Department,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/add',
                    name: 'Thêm mới phòng',
                    component: DepartmentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/department/edit/:id',
                    name: 'Sửa phòng',
                    component: DepartmentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/list',
                    name: 'Danh sách Tags',
                    component: Tag,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/add',
                    name: 'Thêm mới Tags',
                    component: TagEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/tags/edit/:id',
                    name: 'Sửa Tags',
                    component: TagEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/article/list',
                    name: 'Danh sách bài viết',
                    component: Article,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/article/add',
                    name: 'Thêm mới bài viết',
                    component: ArticleEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/article/edit/:id',
                    name: 'Sửa bài viết',
                    component: ArticleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/list',
                    name: 'Danh sách nhà cung cấp',
                    component: Manufacturers,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/add',
                    name: 'Thêm mới nhà cung cấp',
                    component: ManufacturersEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/manufacturers/edit/:id',
                    name: 'Sửa nhà cung cấp',
                    component: ManufacturersEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/list',
                    name: 'Danh sách khuyến mãi',
                    component: Promotions,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/add',
                    name: 'Thêm mới khuyến mãi',
                    component: PromotionEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/promotion/edit/:id',
                    name: 'Sửa khuyến mãi',
                    component: PromotionEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/list',
                    name: 'Danh sách thuộc tính',
                    component: Propertys,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/add',
                    name: 'Thêm mới thuộc tính',
                    component: PropertyEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/property/edit/:id',
                    name: 'Sửa thuộc tính',
                    component: PropertyEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/list',
                    name: 'Danh sách thông số kỹ thuật',
                    component: ProductSpecificationTemplates,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/add',
                    name: 'Thêm mới thông số kỹ thuật',
                    component: ProductSpecificationTemplatesEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/productSpecificationTemplate/edit/:id',
                    name: 'Sửa thông số kỹ thuật',
                    component: ProductSpecificationTemplatesEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/list',
                    name: 'Danh sách nhóm người dùng',
                    component: Role,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/add',
                    name: 'Thêm mới nhóm người dùng',
                    component: RoleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/role/edit/:id',
                    name: 'Sửa nhóm người dùng',
                    component: RoleEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/orders/list',
                    name: 'Danh sách đơn hàng',
                    component: Order,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/orders/listdetail/:id',
                    name: 'Chi tiết đơn hàng',
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
                alert('Bạn không có quyền vào link này');
                return next({ path: '/' });
            }
        });
    }
    authenticationRepository.getCurrentUser().then(response => {
        if (response.roles.includes("Admin")) {
            return;
        }
        if (!response.permissionUrl.includes(to.path)) {
            alert('Bạn không có quyền vào link này');
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



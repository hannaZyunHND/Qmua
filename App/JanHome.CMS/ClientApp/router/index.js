import Vue from 'vue'
import Router from 'vue-router'
import { authenticationRepository } from "../repository/authentication/authenticationRepository";
// home
const DefaultContainer = () => import('./../containers/DefaultContainer');
const Dashboard = () => import('./../pages/Dashboard');


const Products = () => import('./../pages/product/list');

const ProductEdit = () => import('./../pages/product/edit');
const ProductClone = () => import('./../pages/product/clone');
const ProductExtent = () => import('./../pages/productextend/main');
const ProductRegion = () => import('./../pages/product/region');
const ProductPromotion = () => import('./../pages/product/promotion');
const ProductZone = () => import('./../pages/product/zone');
const ProductImportExport = () => import('./../pages/product/importExport');
const CloneComment = () => import('./../pages/product/fakecomment');

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

const Coupons = () => import('./../pages/coupon/list');
const CouponEdit = () => import('./../pages/coupon/edit');


const CouponChilds = () => import('./../pages/couponchild/list');
const CouponChildsAdd = () => import('./../pages/couponchild/merge');
const CouponChildsEdit = () => import('./../pages/couponchild/edit');

const ProductSpecificationTemplates = () => import('./../pages/productSpecificationTemplate/list');
const ProductSpecificationTemplatesEdit = () => import('./../pages/productSpecificationTemplate/edit');

const Role = () => import('./../pages/role/list');
const RoleEdit = () => import('./../pages/role/edit');
const RoleAssignPermission = () => import('./../pages/role/assignPermissionRole');

const Profile = () => import('./../pages/profile/edit');
const ChangePassword = () => import('./../pages/profile/change-password');

const User = () => import('./../pages/user/list');
const UserEdit = () => import('./../pages/user/edit');
const AssignToRole = () => import('./../pages/user/assignToRole');

const Comment = () => import('./../pages/comment/list');
const Customer = () => import('./../pages/customer/list');
const Order = () => import('./../pages/orders/list');
const OrderDetail = () => import('./../pages/orders/listdetail');

const Contact = () => import('./../pages/contact/list');
const Color = () => import('./../pages/Color/list');

const BannerEdit = () => import('./../pages/banner/edit');

const Policy = () => import('./../pages/policy/list');
const PolicyEdit = () => import('./../pages/policy/edit');

const FlashSale = () => import('./../pages/flashsale/list');

const FlashSaleEdit = () => import('./../pages/flashsale/edit');

const BankInstallMent = () => import('./../pages/bankinstallment/list');

const BankInstallMentEdit = () => import('./../pages/bankinstallment/edit');

const LuckySpinEdit = () => import('./../pages/LuckySpin/edit');
const LuckySpinList = () => import('./../pages/LuckySpin/list');
const CustomerLuckyList = () => import('./../pages/CustomerLucky/list');

const RedirectList = () => import('./../pages/redirect/list');

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
                    path: '/admin/banner/setting',
                    name: 'Cấu hình Banner',
                    component: BannerEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/banner/edit:id',
                    name: 'Sửa banner',
                    component: BannerEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/product/list',
                    name: 'Danh sách sản phẩm',
                    component: Products,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/importExport',
                    name: 'Import sản phẩm',
                    component: ProductImportExport,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/product/fakecomment',
                    name: 'Thêm Comment Giả',
                    component: CloneComment,
                    meta: { authorize: [] }

                },
                //const ProductImportExport = () => import('./../pages/product/importExport');
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
                    path: '/admin/product/clone/:id',
                    name: 'Clone sản phẩm',
                    component: ProductClone,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/flashsale/list',
                    name: 'FlashSale',
                    component: FlashSale,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/flashsale/add',
                    name: 'Thêm mới chiến dịch FlashSale',
                    component: FlashSaleEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/flashsale/edit/:id',
                    name: 'Sửa chiến dịch FlashSale',
                    component: FlashSaleEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/policy/list',
                    name: 'Chính sách bảo hành',
                    component: Policy,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/policy/edit/:code',
                    name: 'Sửa chính sách',
                    component: PolicyEdit,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/policy/add',
                    name: 'Thêm chính sách',
                    component: PolicyEdit,
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
                    path: '/admin/product/zone',
                    name: 'Chọn sản phẩm Hot theo danh mục',
                    component: ProductZone,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/product/promotion',
                    name: 'Khuyến mại',
                    component: ProductPromotion,
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
                    path: '/admin/contact/list',
                    name: 'Danh sách liên hệ',
                    component: Contact,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/color/list',
                    name: 'Danh sách màu sắc',
                    component: Color,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/redirect/list',
                    name: 'Danh sách Link Redirect',
                    component: RedirectList,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/comment/list',
                    name: 'Danh sách comment',
                    component: Comment,
                    meta: { authorize: [] }

                },
                {
                    path: '/admin/orders/list',
                    name: 'Đơn hàng',
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
                    path: '/admin/coupon/list',
                    name: 'Danh mục mã giảm giá',
                    component: Coupons,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/coupon/add',
                    name: 'Thêm mới danh mục mã giảm giá',
                    component: CouponEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/coupon/edit/:id',
                    name: 'Sửa danh mục Mã giảm giá',
                    component: CouponEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/couponchild/list',
                    name: 'Danh sách mã giảm giá',
                    component: CouponChilds,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/couponchild/add',
                    name: 'Thêm mới mã giảm giá',
                    component: CouponChildsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/couponchild/edit/:id',
                    name: 'Sửa Mã giảm giá',
                    component: CouponChildsEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/couponchild/merge',
                    name: 'Thêm mới mã giảm giá',
                    component: CouponChildsAdd,
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
                    path: '/admin/system/role/assignPermissionRole/:id',
                    name: 'Phan quyen cho nhom nguoi dung',
                    component: RoleAssignPermission,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/profile',
                    name: 'Sửa thông tin người dùng',
                    component: Profile,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/profile/change-password',
                    name: 'Thay đổi mật khẩu',
                    component: ChangePassword,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/user/list',
                    name: 'Danh sách người dùng',
                    component: User,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/user/edit/:id',
                    name: 'Sửa  người dùng',
                    component: UserEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/user/add',
                    name: 'Thêm mới người dùng',
                    component: UserEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/system/user/assignRole/:id',
                    name: 'Thêm Role',
                    component: AssignToRole,
                    meta: { authorize: [] }
                },

                {
                    path: '/admin/bankinstallment/list',
                    name: 'Trả góp',
                    component: BankInstallMent,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/bankinstallment/add',
                    name: 'Thêm mới cài đặt trả góp',
                    component: BankInstallMentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/bankinstallment/edit/:id',
                    name: 'Sửa cài đặt trả góp',
                    component: BankInstallMentEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/luckyspin/edit/:id',
                    name: 'Vòng quay may mắn',
                    component: LuckySpinEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/luckyspin/add',
                    name: 'Vòng quay may mắn',
                    component: LuckySpinEdit,
                    meta: { authorize: [] }
                },
                {
                    path: '/admin/luckyspin/list',
                    name: 'Quản lý vòng quay',
                    component: LuckySpinList,
                    meta: { authorize: [] }
                }, {
                    path: '/admin/customer-lucky/list',
                    name: 'Khách hàng may mắn',
                    component: CustomerLuckyList,
                    meta: { authorize: [] }
                },

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
            if (response.roles.includes("Admin")) {
                return;
            }
            console.log(response.permissionUrl);
            console.log(to.path);
            if (to.path === "/admin/dashboard" && !response.permissionUrl.includes(to.path)) {

                return next({ path: response.permissionUrl[0].url });
            }
        });
    }
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



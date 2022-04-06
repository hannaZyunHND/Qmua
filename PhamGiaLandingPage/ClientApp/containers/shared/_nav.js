export default {
    items: [
        {
            name: 'Tổng quan',
            url: '/admin/dashboard',
            icon: 'icon-speedometer',
            badge: {
                variant: 'primary'
            }
        },
        {
            name: 'Sản phẩm',
            url: '/product',
            icon: 'icon-drop',
            children: [
                {
                    name: 'Danh sách',
                    url: '/admin/product/list',
                    icon: 'icon-puzzle'
                },
                {
                    name: 'Vùng hiển thị',
                    url: '/product/region',
                    icon: 'icon-puzzle'
                },
                {
                    name: 'Thuộc tính',
                    url: '/property/list',
                    icon: 'icon-puzzle'
                },
                {
                    name: 'Khuyến mãi',
                    url: '/promotion/list',
                    icon: 'icon-puzzle'
                },
                {
                    name: 'Nhãn hiệu',
                    url: '/manufacturers/list',
                    icon: 'icon-puzzle',
                },
                {
                    name: 'Chi tiết kĩ thuật',
                    url: '/productSpecificationTemplate/list',
                    icon: 'icon-puzzle',
                },
                {
                    name: 'Import - Export File',
                    url: '/admin/product/importExport',
                    icon: 'icon-puzzle',
                },
            ]
        },

        {
            name: 'Bài viết',
            url: '/article/list',
            icon: 'icon-puzzle'
        },
        
        {
            name: 'Vòng quay may mắn',
            url: '/lucky-spin',
            icon: 'icon-drop',
            children: [

                {
                    name: 'Vòng quay',
                    url: '/lucky-spin/list',
                    icon: 'icon-puzzle',
                },

                {
                    name: 'Khách hàng may mắn',
                    url: '/lucky-spin/customer',
                    icon: 'icon-puzzle',
                },

              
            ]
        },
        {
            name: 'Cấu hình',
            url: '/config',
            icon: 'icon-drop',
            children: [

                {
                    name: 'Cấu hình',
                    url: '/config/list',
                    icon: 'icon-puzzle',
                },

                {
                    name: 'Danh mục',
                    url: '/zone/list',
                    icon: 'icon-puzzle',
                },

                {
                    name: 'Khu vực',
                    url: '/location/list',
                    icon: 'icon-puzzle',

                },
                {
                    name: 'Chi nhánh',
                    url: '/department/list',
                    icon: 'icon-puzzle',

                }
                
            ]
        }

    ]
}

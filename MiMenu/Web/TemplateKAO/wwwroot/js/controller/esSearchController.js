R.EsSearch = {
    Init: function () {
        //R.EsSearch.Index = 1;
        //R.EsSearch.Size = 24;
        var x = sessionStorage.getItem("esSearch");
        if (x != null) {
            var p = JSON.parse(x);
            p.index = 1;
            sessionStorage.setItem("esSeach", JSON.stringify(p));
        }
        R.EsSearch.IndexQR = 1;
        R.EsSearch.IndexES = 1;
        R.EsSearch.location_id = R.CurrentLocationId();
        R.EsSearch.FirstLoad();
        R.EsSearch.RegisterEvent();
    },
    RegisterEvent: function () {
        $('._view-more-product').off('click').on('click', function () {
            //alert('an nut');
            R.EsSearch.LoadSearch('append');
        })
        $('._view-more-product-query-db').off('click').on('click', function () {
            R.EsSearch.IndexQR++;
            R.EsSearch.LoadQuerySearch('append');
        })
        $('.filter-picking').off('click').on('click', function () {
            var el = $(this);
            el.closest('.container').find('.filter-picking').each(function (el) {
                $(this).removeClass('picking-active');
            })
            el.addClass('picking-active');
            R.EsSearch.ChangeButtonClass();
            R.EsSearch.IndexQR = 1;
            R.EsSearch.Filter_QR(R.EsSearch.IndexQR);
        });
        $('.filter-select').off('change').on('change', function () {
            //console.log($(this).val());
            R.EsSearch.ChangeButtonClass();
            R.EsSearch.IndexQR = 1;
            R.EsSearch.Filter_QR(R.EsSearch.IndexQR);
        });
    },
    ChangeButtonClass: function () {
        var el = $('._view-more-product');
        el.removeClass('_view-more-product').addClass('_view-more-product-query-db');
    },
    FirstLoad: function () {
       
        var path = window.location.pathname;
        console.log(path);
        if (path == '/tim-kiem') {
            console.log("es");
            R.EsSearch.LoadSearch('renew');
        }
            
        if (path == '/tim-kiem-nhanh') {
            console.log("query");
            R.EsSearch.LoadQuerySearch('renew');
        }
            
    },
    LoadSearch: function (cker) {
        var x = sessionStorage.getItem("esSearch");
        if (x != null) {
            var search_paramas = JSON.parse(x);
            var index = search_paramas.index;
            var size = search_paramas.size;
            var kw = search_paramas.keyword;
            console.log(x);
            $.get("/Product/ESSeachResultComponent?keyword=" + kw + "&index=" + index + "&size=" + size + "", function (response) {
                //console.log(response);
                if (cker == 'append') {
                    $('._binding_product_query_db').append(response);
                }
                if (cker == 'renew') {
                    $('._binding_product_query_db').html('').html(response);
                }
                R.Extra.BindingExtraToProduct();
                //index++;
                var research = {
                    index: index,
                    size: size,
                    keyword: kw
                }
                sessionStorage.setItem("esSearch", JSON.stringify(research));
                R.EsSearch.LoadInfomationPage();
                R.EsSearch.SetFilterES(R.EsSearch.IndexQR);
                R.EsSearch.RegisterEvent();
            })
        }
        
    },
    LoadQuerySearch: function (cker) {
        var x = sessionStorage.getItem("homeQuerySearch");
        if (x != null) {
            console.log(x);
            console.log(cker);
            var x_parse = JSON.parse(x);
            //$.post()
            $.post("/Product/HomeQuerySearchComponent", x_parse, function (response) {
                if (cker == "append") {
                    $('._binding_product_query_db').append(response);
                    R.Extra.BindingExtraToProduct();
                    R.EsSearch.LoadInfomationPage();
                    x_parse.pageNumber = x_parse.pageNumber + 1;
                    sessionStorage.setItem("homeQuerySearch", JSON.stringify(x_parse));
                    R.EsSearch.RegisterEvent();
                }
                if (cker == "renew") {
                    $('._binding_product_query_db').html('').html(response);
                    R.Extra.BindingExtraToProduct();
                    R.EsSearch.LoadInfomationPage();
                    x_parse.pageNumber = x_parse.pageNumber + 1;
                    sessionStorage.setItem("homeQuerySearch", JSON.stringify(x_parse));
                    R.EsSearch.RegisterEvent();
                }
                
            })
        }
    },
    LoadInfomationPage: function () {
        var el = $('._get-info').first();
        var total = el.data('total');
        var kw = el.data('keyword');
        $('.tk-keyword').text(kw);
        $('.nuber-kq').text(total);
    },
    Filter_QR: function (index) {
        //Get thuong hieu
        var x = sessionStorage.getItem("homeQuerySearch");
        if (x != null) {
            var r = JSON.parse(x);
            r.pageNumber = index;
            var parent_id = r.parentId;
            var manu_id = $('.picking-active').data('manu-id');
            var range_price = $('.range-price').val();
            var min_price = 0;
            var max_price = 0
            if (range_price != "") {
                var arr = range_price.split('-');
                min_price = arr[0];
                max_price = arr[1];
            }
            var fp = [];
            $('.dynamic-filter').each(function (element) {
                var filter_item = {
                    SpectificationId: $(this).data('spec-id'),
                    Value: $(this).val()
                }
                fp.push(filter_item);
            })
            var color_code = $('.color').val();
            var extra = $('.extra').val();
            var sort_price = 0;
            var sort_rate = 0;
            if (extra == "2")
                sort_price = 1;
            if (extra == "3")
                sort_rate = 1;
            var locationId = R.EsSearch.location_id;
            var params = {
                parentId: parent_id,
                lang_code: 'vi-VN',
                locationId: locationId,
                manufacture_id: manu_id,
                min_price: parseInt(min_price),
                max_price: parseInt(max_price),
                sort_price: sort_price,
                sort_rate: sort_rate,
                color_code: color_code,
                filter: fp,
                filter_text: '',
                material_type: 0,
                pageNumber: index,
                pageSize: 24
            }
            sessionStorage.setItem("homeQuerySearch", JSON.stringify(params));
            R.EsSearch.IndexQR++;
            R.EsSearch.LoadQuerySearch('renew');
        }

    },
    SetFilterES: function (index) {
        var parent_id = 0;
        var manu_id = $('.picking-active').data('manu-id');
        var range_price = $('.range-price').val();
        var min_price = 0;
        var max_price = 0
        if (range_price != "") {
            var arr = range_price.split('-');
            min_price = arr[0];
            max_price = arr[1];
        }
        var fp = [];
        $('.dynamic-filter').each(function (element) {
            var filter_item = {
                SpectificationId: $(this).data('spec-id'),
                Value: $(this).val()
            }
            fp.push(filter_item);
        })
        var color_code = $('.color').val();
        var extra = $('.extra').val();
        var sort_price = 0;
        var sort_rate = 0;
        if (extra == "2")
            sort_price = 1;
        if (extra == "3")
            sort_rate = 1;
        var locationId = R.EsSearch.location_id;
        var params = {
            parentId: parent_id,
            lang_code: 'vi-VN',
            locationId: locationId,
            manufacture_id: manu_id,
            min_price: parseInt(min_price),
            max_price: parseInt(max_price),
            sort_price: sort_price,
            sort_rate: sort_rate,
            color_code: color_code,
            filter: fp,
            filter_text: '',
            material_type: 0,
            pageNumber: index,
            pageSize: 24
        }
        console.log(params);
        sessionStorage.setItem("homeQuerySearch", JSON.stringify(params));
    },
    CheckBack: function () {
        var isBack = false;
        var x = sessionStorage.getItem("esSearch");
        if (x != null) {
            var p = JSON.parse(x);
            if (p.index > 1)
                isBack = true;
            if (isBack) {
                p.index = 1;
                sessionStorage.setItem("esSeach", JSON.stringify(p));
            }
            
        }
    }
}

$(function () {
    R.EsSearch.Init()
})


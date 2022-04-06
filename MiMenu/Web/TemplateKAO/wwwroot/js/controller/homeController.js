R.Home = {
    Init: function() {
        R.Home.RegisterEvent();
        R.Home.culture = R.Culture();
        R.Home.currentLocationId = R.CurrentLocationId();
        R.Home.BindingTotal();
        R.Home.CountDownFlashSale('.flash-sale');

    },
    RegisterEvent: function () {
        $('._switch_region').off('click').on('click', function () {
            $(this).closest('ul').find('.active').removeClass('active');
            $(this).addClass('active');
            var id = $(this).data('region-id');
            R.Home.SwitchRegion($(this), id)
        })
        $('.switch-du-an').off('click').on('click', function () {
            //clear active
            $('.switch-du-an').each(function (e) {
                $(this).removeClass('uk-active');
            })
            $(this).addClass('uk-active');
            var data = $(this).data('value');
            R.Home.ChangeDuAn(data);
        })
        $('.tab-region').off('click').on('click', function() {
            $(this).closest('.swiper-wrapper').find('.swiper-slide-active').removeClass('swiper-slide-active');
            $(this).closest('.swiper-slide').addClass('swiper-slide-active');
            $(this).closest('.menu-op').find('.active').removeClass('active');
            $(this).addClass("active");

            var region_id = $(this).data('region-id');
            R.Home.SwitchRegion($(this), region_id)
        });
        $('.view-remain-product').off('click').on('click', function() {
           
        })
        $('.view-more-region-nbv').off('click').on('click', function () {
            var id = $(this).data('zone-id');
            var idex = $(this).data('index');
            var size = $(this).data('size');
            var loc = $(this).data('loc');
            var el = $('._binding_product');
            var skip = idex * size;
            R.Home.ViewMoreRegion(id, el, skip, size, idex);

        })
        $('.filter-home').off('click').on('click', function () {
            var el = $(this);
            //var pr = $(this).closest('.main-fast-search');
            var parent_id = 0;
            //var parent_id = $('#zone-current').data('id');
            var manu_id = $(this).data('manu-id');
            //var range_price = pr.find('.range-price').val();
            var min_price = 0;
            var max_price = 0
            //if (range_price != "") {
            //    var arr = range_price.split('-');
            //    min_price = arr[0];
            //    max_price = arr[1];
            //}
            var fp = [];
            $('.dynamic-filter').each(function (element) {
                var filter_item = {
                    SpectificationId: $(this).data('spec-id'),
                    Value: $(this).val()
                }
                fp.push(filter_item);
            })
            //var color_code = $('.color').val();
            var extra = $('.extra').val();
            var sort_price = 0;
            var sort_rate = 0;
            if (extra == "2")
                sort_price = 0;
            if (extra == "3")
                sort_rate = 1;
            var locationId = R.Home.currentLocationId;
            var params = {
                parentId: parent_id,
                lang_code: 'vi-VN',
                locationId: locationId,
                manufacture_id: manu_id == null ? 0 : manu_id,
                min_price: parseInt(min_price),
                max_price: parseInt(max_price),
                sort_price: sort_price,
                sort_rate: sort_rate,
                color_code: '',
                filter: null,
                filter_text: '',
                material_type: 0,
                pageNumber: 1,
                pageSize: 24
            }

            sessionStorage.setItem("homeQuerySearch", JSON.stringify(params));
            window.location.href = "/tim-kiem-nhanh";
            //load ajax
            //R.Home.BindingProductFilterd(params, el);
        })
    },
    ChangeDuAn: function (data) {
        var banner_code = "Cooken_TrangChu_DuAn" + data;
        var param = {
            lang_code: "vi-VN",
            banner_code : banner_code
        }
        var url = "/Extra/GetBannerAdsByCode";
        $.post(url, param, function (response) {
            console.log(response);
            $('.binding-du-an').html('').html(response);
            R.Home.RegisterEvent();
        })
    },
    SwitchRegion: function(el, region_id) {
        var url = R.Home.culture + "/Home/SwitchRegion";
        var params = {
            region_id: region_id
        }
        $.post(url, params, function(response) {
            el.closest('.container').find("._binding_product").html('').html(response);
            var total = el.closest('.container').find('.set-total').data('total');

            console.log(total);
            var url = el.closest('.container').find('.set-total').data('url');
            if (total < 20) {
                el.closest('.container').find('.view-more-region-nbv').css('display', 'none');
            }
            else {
                el.closest('.container').find('.view-more-region-nbv').css('display', 'block');
                el.closest('.container').find('.view-more-region-nbv').css('display', '');
                el.closest('.container').find('.view-more-region-nbv').data('index', 1);
                el.closest('.container').find('.view-more-region-nbv').data('size', 20);
                el.closest('.container').find('.view-more-region-nbv').data('zone-id', region_id);    

                //window.location.href = url;
            }
            //var total_product = el.closest('.container').find("._binding_product").data('total-product');
            //var page_size = el.closest('.container').find(".remain-product").data("page-size");
            //remain-product
            R.Home.BindingUrlOld();
            R.Home.BindingTotal();
            R.Extra.BindingExtraToProduct();
        })
    },
    BindingUrlOld: function () {
        $('.set-url-old-link').each(function (element) {
            var url = $(this).data('link');
            $(this).closest('.container').find('.url-old-link').attr('href',url);

        })
        $(this).closest('.container').find('.remain-product').closest('.view-remain-product').show();
    },
    BindingProductFilterd: function (params, el) {
        //FilterShomepectificationInZoneListProductList
        $.post(R.Home.culture + '/Product/FilterSpectificationInZoneListProductList', params, function (response) {
            console.log(response);
            //el.closest('.container').find('._binding_product').html(response);
            //var new_element = el.closest('.container').find('._binding_product');
            //R.Extra.BindingExtraToProductInElement(new_element);
            //R.Product.BindingTotal();
            //R.Product.RegisterEvent();
        })
    },
    ViewMoreRegion: function(id, el, skip, size, indexer) {
        //int zone_parent_id, int locationId, int skip, int size
        var params = {
            zone_parent_id: id,
            locationId: R.Home.currentLocationId,
            skip: size,
            size: size
        }
        $.post(R.Home.culture + '/Home/ViewMoreRegion', params, function(response) {
            el.closest('.container').find('._binding_product').append(response);
            $('.view-more-region-nbv').data('index', indexer + 1);
            //el.closest('.container').find('.view-remain-product').hide();
            //el.hide();
            //R.Home.BindingTotal();
            R.Extra.BindingExtraToProduct();
            R.Home.RegisterEvent();
        })
    },
    BindingTotal: function() {
        $('.set-total').each(function(el) {
            var total = $(this).data('total');
            var id = $(this).data('id');
            //console.log(total);
            if (total <= 9)
                $(this).closest('.container').find('.remain-product').html("<b>" + 0 + "</b>");
            else {
                var remain = total - 9;
                $(this).closest('.container').find('.remain-product').html("<b>" + remain + "</b>");
                $(this).closest('.container').find('.remain-product').closest('.view-remain-product').show();
                $(this).closest('.container').find('.remain-product').closest('.view-remain-product').data('size', total - 9);
                $(this).closest('.container').find('.remain-product').closest('.view-remain-product').data('id', id);
            }

        })
    },
    CountDownFlashSale: function(el) {
        var end_time = $(el).data('end-time');
        R.CountDown(end_time, el);

    }


}



$(function() {
    R.Home.Init()
})

//R.Home = {
//    Init: function () {
//        R.Home.RegisterEvent();
//        R.Home.culture = R.Culture();
//        R.Home.currentLocationId = R.CurrentLocationId();
//        R.Home.BindingTotal();
//        R.Home.CountDownFlashSale('.flash-sale');

//    },
//    RegisterEvent: function () {
//        $('.tab-region').off('click').on('click', function () {
//            $(this).closest('.swiper-wrapper').find('.swiper-slide-active').removeClass('swiper-slide-active');
//            $(this).closest('.swiper-slide').addClass('swiper-slide-active');
//            var region_id = $(this).data('region-id');
//            R.Home.SwitchRegion($(this), region_id)
//        });
//        $('.view-remain-product').off('click').on('click', function () {
//            console.log(1);
//            var el = $(this);
//            var id = $(this).data('id');
//            var skip = $(this).data('skip');
//            var size = $(this).data('size');
//            R.Home.ViewMoreRegion(id, el, skip, size);
//        })
//    },
//    SwitchRegion: function (el, region_id) {
//        var url = R.Home.culture + "/Home/SwitchRegion";
//        var params = {
//            region_id : region_id
//        }
//        $.post(url, params, function (response) {
//            el.closest('.container').find("._binding_product").replaceWith(response);
//            var total_product = el.closest('.container').find("._binding_product").data('total-product');
//            var page_size = el.closest('.container').find(".remain-product").data("page-size");
//            //remain-product
//            R.Home.BindingTotal();
//            R.Extra.BindingExtraToProduct();
//        })
//    },
//    ViewMoreRegion: function (id, el, skip, size) {
//        //int zone_parent_id, int locationId, int skip, int size
//        var params = {
//            zone_parent_id: id,
//            locationId: R.Home.currentLocationId,
//            skip: skip,
//            size: size
//        }
//        $.post(R.Home.culture + '/Home/ViewMoreRegion', params, function (response) {
//            el.closest('.container').find('._binding_product').append(response);
//            el.hide();
//            //R.Home.BindingTotal();
//            R.Extra.BindingExtraToProduct();
//            R.Home.RegisterEvent();
//        })
//    },
//    BindingTotal: function () {
//        $('.set-total').each(function (el) {
//            var total = $(this).data('total');
//            var id = $(this).data('id');
//            console.log(total);
//            if (total <= 9)
//                $(this).closest('.container').find('.remain-product').html("<b>" + 0 + "</b>");
//            else {
//                var remain = total - 9;
//                $(this).closest('.container').find('.remain-product').html("<b>"+remain+"</b>");
//                $(this).closest('.container').find('.remain-product').parent().show();
//                $(this).closest('.container').find('.remain-product').parent().data('size', total - 9);
//                $(this).closest('.container').find('.remain-product').parent().data('id', id);
//            }

//        })
//    },
//    CountDownFlashSale: function (el) {
//        var end_time = $(el).data('end-time');
//        R.CountDown(end_time, el);

//    }


//}



//$(function () {
//    R.Home.Init()
//})
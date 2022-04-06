R.Product = {
    Init: function() {
        //R.Test();
        R.Product.BindingTotal();
        R.Product.RegisterEvent();
        R.Product.culture = R.Culture();
        R.Product.location_id = R.CurrentLocationId();
        R.Extra.BindingExtraToProduct();
        R.Product.FirstBindFilterZone();
        R.Product.CheckBack();
        //var source_url = R.Product.culture + "/FilterProduct/FilterProductBySpectification"
        //var source_total = parseInt($('.paging-container').data('total'));
        //var source_size = parseInt($('.paging-container').data('size'));
        //var source_current = parseInt($('.paging-container').data('currentpage'));
        R.Product.Filter_v2(1);
        //R.Product.Filter_v2();
    },
    RegisterEvent: function () {
        $('#filter-text').keyup(function (e) {
            if (e.keyCode == 13) {
                R.Product.Filter_v2();
            }
        });
        $('.nav-link').off('click').on('click', function () {
            $('.nav-link').each(function (i, v) {
                $(v).removeClass('active');
            })
            $(this).addClass('active');
            R.Product.Filter_v2();
        })
        $('.dynamic-filter').off('change').on('change', function () {
            R.Product.Filter_v2_Mobile();
        })
        $('input[type="checkbox"]').off('change').on('change', function () {
            //var par = $(this).closest('.filter-item');
            R.Product.Filter_v2();
            //$(this).closest('.filter-item').find('input[type = "checkbox"]').not(this).prop('checked', false);
        });
        $('.select-zone').off('click').on('click', function() {
            $(this).closest('.swiper-wrapper').find('.swiper-slide-active').removeClass('swiper-slide-active');
            $(this).closest('.swiper-slide').addClass('swiper-slide-active');
            $(this).closest('.menu-op').find('.active').removeClass('active');
            $(this).addClass("active");
            var el = $(this);
            var zone_id = $(this).data('id');
            //load ajax
            R.Product.BindingProduct(zone_id, el);
        })
        //$('.select-zone-filterd').off('click').on('click', function() {
        //    $(this).closest('.swiper-wrapper').find('.swiper-slide-active').removeClass('swiper-slide-active');
        //    $(this).closest('.swiper-slide').addClass('swiper-slide-active');
        //    var el = $(this);
        //    var parent_id = $(this).data('id');
        //    //var parent_id = $('#zone-current').data('id');
        //    var manu_id = $('.picking-active').data('manu-id');
        //    var range_price = $('.range-price').val();
        //    var min_price = 0;
        //    var max_price = 0
        //    if (range_price != "") {
        //        var arr = range_price.split('-');
        //        min_price = arr[0];
        //        max_price = arr[1];
        //    }
        //    var fp = [];
        //    $('.dynamic-filter').each(function(element) {
        //        var filter_item = {
        //            SpectificationId: $(this).data('spec-id'),
        //            Value: $(this).val()
        //        }
        //        fp.push(filter_item);
        //    })
        //    var color_code = $('.color').val();
        //    var extra = $('.extra').val();
        //    var sort_price = 0;
        //    var sort_rate = 0;
        //    if (extra == "2")
        //        sort_price = 1;
        //    if (extra == "3")
        //        sort_rate = 1;
        //    var locationId = R.Product.location_id;
        //    var params = {
        //            parentId: parent_id,
        //            lang_code: 'vi-VN',
        //            locationId: locationId,
        //            manufacture_id: manu_id,
        //            min_price: parseInt(min_price),
        //            max_price: parseInt(max_price),
        //            sort_price: sort_price,
        //            sort_rate: sort_rate,
        //            color_code: color_code,
        //            filter: fp,
        //            filter_text: '',
        //            material_type: 0,
        //            pageNumber: 1,
        //            pageSize: 20
        //        }
        //        //load ajax
        //    R.Product.BindingProductFilterd(params, el);
        //})

        $('.view-more').off('click').on('click', function() {
            console.log(1);
            var el = $(this);
            var id = $(this).data('id');
            var skip = $(this).closest('.container').find('.set-total').data('is-rendered');
            var size = $(this).data('size');
            R.Product.ViewMore(id, el, skip, size);
        });
        $('.button-view-more').off('click').on('click', function() {
            console.log(1);
            var el = $(this);
            var id = $(this).data('id');
            var skip = $(this).closest('.container').find('.set-total').data('is-rendered');
            var size = $(this).data('size');
            R.Product.ViewMore(id, el, skip, size);
        });
        $(".star-rating i").off('click').on('click', function() {
            $(this).parent().find(".star-rating i").removeClass("checked");
            $(this).addClass("checked");
            $(this).prevAll().addClass("checked");
            $(this).nextAll().removeClass("checked");
            var type = 2;
            var zone_id = $(this).closest('.star-rating').data('id');
            //Dem so phan tu co class active
            R.Product.Rating(zone_id, type);
        });
        $('.filter-picking').off('click').on('click', function() {
            var el = $(this);
            el.closest('.container').find('.filter-picking').each(function(el) {
                $(this).removeClass('picking-active');
            })
            el.addClass('picking-active');
            R.Product.Filter_v1(1);
        });
        //$('.filter-select').off('change').on('change', function () {
        //    //console.log($(this).val());
        //    R.Product.Filter_v1(1);
        //});
        $('.filterd-view-more').off('click').on('click', function () {
            var current_index = $(this).data('index');
            var index = current_index + 1;
            $(this).data('index', index);
            R.Product.Filter_v1(index);
        })
    },
    FirstBindFilterZone: function () {
        console.log(sessionStorage.getItem('filterZone'));
        if (sessionStorage.getItem('filterZone') != null) {
            console.log(sessionStorage.getItem('filterZone'));
            var fil = JSON.parse(sessionStorage.getItem('filterZone'));
            var type = fil.type;
            
            if (type === 1) {
                var value = fil.value.toString().split(',');
                console.log(1);
                $('.filter-item.manufacture').find('input[type=checkbox]').each(function (element) {
                    var t = $(this).data('value').toString();
                    console.log(t);
                    if (value.includes(t))
                        $(this).prop('checked', true);
                })
            }
            if (type == 2) {
                var filter_price = fil.value.toString().split(',')[0];
                var min_filter = 0;
                var max_filter = 0
                if (filter_price != null) {
                    min_filter = parseInt(filter_price.split('-')[0]);
                    max_filter = parseInt(filter_price.split('-')[1]);
                }
                $('.filter-item.price').find('input[type=checkbox]').each(function (element) {
                    var t = $(this).data('value').toString();
                    var max_value = parseInt(t.split('-')[1]);
                    var min_value = parseInt(t.split('-')[0]);
                    if ((max_filter > min_value && max_filter > max_value) || (min_value <= min_filter && max_value >= max_filter)) {
                        $(this).prop('checked', true);
                    }
                    
                })
            }
            R.Product.Filter_v2();
            sessionStorage.removeItem('filterZone')
        }
    },
    BindingTotal: function() {
        $('.set-total').each(function(el) {
            var total = $(this).data('total');
            var id = $(this).data('id');
            var isRendered = $(this).data('is-rendered');
            console.log(total, isRendered);
            if (total <= isRendered) {
                //button-view-more
                $(this).closest('.container').find('.tong_so_sp').parent().hide();
                $(this).closest('.container').find('.button-view-more').parent().hide();
            } else {
                $(this).closest('.container').find('.tong_so_sp').text(total - isRendered);
                $(this).closest('.container').find('.tong_so_sp').parent().show();
                $(this).closest('.container').find('.tong_so_sp').parent().data('size', 20);
                $(this).closest('.container').find('.tong_so_sp').parent().data('id', id);
                $(this).closest('.container').find('.button-view-more').parent().show();
                $(this).closest('.container').find('.button-view-more').data('size', 20);
                $(this).closest('.container').find('.button-view-more').data('id', id);
            }

        })
    },
    BindingProduct: function(id, el) {
        console.log(id);
        var params = {
            zone_id: id,
            location_id: 3
        }
        $.post(R.Product.culture + '/Product/GetProductByZoneId', params, function(response) {
            console.log(response);
            el.closest('.container').find('._binding_product').html(response);
            var new_element = el.closest('.container').find('._binding_product');
            R.Extra.BindingExtraToProductInElement(new_element);
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
    },
    BindingProductFilterd: function(params, el) {
        //FilterSpectificationInZoneListProductList
        $.post(R.Product.culture + '/Product/FilterSpectificationInZoneListProductList', params, function(response) {
            console.log(response);
            el.closest('.container').find('._binding_product').html(response);
            var new_element = el.closest('.container').find('._binding_product');
            R.Extra.BindingExtraToProductInElement(new_element);
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
    },
    ViewMore: function(id, el, skip, size) {
        //int zone_parent_id, int locationId, int skip, int size
        var params = {
            zone_parent_id: id,
            locationId: R.CurrentLocationId(),
            skip: skip,
            size: size
        }
        $.post(R.Product.culture + '/Product/ViewMore', params, function(response) {
            el.closest('.container').find('._binding_product').append(response);
            var new_element = el.closest('.container').find('._binding_product');
            //Update Isrendered
            var isItemRenderd = skip + size;
            el.closest('.container').find('.set-total').data('is-rendered', isItemRenderd);
            R.Extra.BindingExtraToProductInElement(new_element);
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
    },
    Rating: function(id, type) {
        //Dem so sao
        var count = 0;
        $('.star-rating').find('.checked').each(function(element) {
            count++;
        });
        console.log(count);
        var url = R.Product.culture + "/Extra/CreateRating";
        //int objectId, int objectType, int rate
        var params = {
            objectId: id,
            objectType: type,
            rate: count
        }
        $.post(url, params, function(response) {
            console.log(response);
            $(".star-rating i").off('click');
        })
    },
    Filter: function() {
        //Get thuong hieu
        var parent_id = $('#zone-current').data('id');
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
        $('.dynamic-filter').each(function(element) {
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
        var locationId = R.Product.location_id;
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
            pageNumber: 1,
            pageSize: 20
        }
        var url = R.Product.culture + "/Product/FilterSpectificationInZone"
        $.post(url, params, function(response) {
            console.log(response);
            R.Extra.PagePaging(true, )
                $('#append-filter').html('').html(response);
                slidermenu();
                R.Extra.BindingExtraToProduct();
                R.Product.BindingTotal();
                R.Product.RegisterEvent();
            })
            //R.Estimates.RegisterEvent();

        //console.log(params);
    },
    Filter_v1: function (index) {
        //Get thuong hieu
        var parent_id = $('#zone-current').data('id');
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
        var locationId = R.Product.location_id;
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
            pageSize: 18
        }
        var url = R.Product.culture + "/FilterProduct/FilterProductBySpectification"
        $.post(url, params, function (response) {
            console.log(response);
            if (index == 1) {
                $('#_binding_tim_kiem').html('').html(response);
                $('#_binding_tim_kiem').css('display', 'block');
                $('._title_tim_kiem').css('display', 'block');

            }
            if (index > 1) {
                $('.filter-view-more').append(response);
                $('.filter-view-more').find('.filterd-view-more').parent().hide();
            }
            
            slidermenu();
            R.Extra.BindingExtraToProduct();
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
            //R.Estimates.RegisterEvent();

        //console.log(params);
    },
    Filter_v2_Mobile: function (index) {
        var parent_id = $('#zone-current').data('id');
        var filter_t = $('#filter-text').val();

        var manuList = [];
        $('.filter-item.manufacture').find('input[type="checkbox"]').each(function (element) {
            if ($(this).is(':checked'))
                manuList.push($(this).data('value'))
        })

        var list_range = [];



        //$('.filter-item.price').find('input[type="checkbox"]').each(function (element) {
        //    if ($(this).is(':checked')) {
        //        var x = $(this).data('value').split('-');
        //        x.forEach(function (e) {
        //            var ss = parseInt(e);
        //            list_range.push(ss);
        //        })
        //    }
        //})
        var min_price = 0;
        var max_price = 0;
        var slt_range = $('.dynamic-filter-range-price').val();
        if (slt_range != "") {
            var x = slt_range.split('-');
            x.forEach(function (e) {
                var ss = parseInt(e);
                list_range.push(ss);
            })
        }


        if (list_range.length > 0) {
            var list_range_min = Math.min.apply(null, list_range);
            var list_range_max = Math.max.apply(null, list_range);
            min_price = list_range_min;
            max_price = list_range_max;
        }



        var fp = [];

        $('.dynamic-filter-spectification').each(function (e) {
            if ($(this).val() != "") {
                var filter_item = {
                    SpectificationId: $(this).data('spec-id'),
                    Value: $(this).val()
                }
                fp.push(filter_item);
            }
            
        })

        //$('.filter-item.dynamic-filter').each(function (e) {
        //    $(this).find('input[type="checkbox"]').each(function (element) {
        //        if ($(this).is(':checked')) {
        //            var filter_item = {
        //                SpectificationId: $(this).data('spec'),
        //                Value: $(this).data('value')
        //            }
        //            fp.push(filter_item);
        //        }
        //    })
        //})

        var idx = $('.paginationjs-page.active').data('num');
        var sort_price = 0;
        var sort_rate = 0;
        if ($('.sort-rate').hasClass('active')) {
            sort_rate = parseInt($('.sort-rate').data('value'));
        }
        $('.sort-price').each(function (i, v) {
            if ($(v).hasClass('active'))
                sort_price = parseInt($(v).data('value'));
        })
        var locationId = R.Product.location_id;
        var params = {
            parentId: parent_id,
            lang_code: 'vi-VN',
            locationId: locationId,
            manufacture_id: manuList.toString(),
            min_price: parseInt(min_price),
            max_price: parseInt(max_price),
            sort_price: sort_price,
            sort_rate: sort_rate,
            color_code: '',
            filter: fp,
            filter_text: filter_t,
            material_type: 0,
            pageNumber: typeof (idx) === 'undefined' ? 1 : idx,
            pageSize: 16
        }

        console.log(params);

        var url = R.Product.culture + "/FilterProduct/FilterProductBySpectification"
        $.post(url, params, function (response) {
            //console.log(response);
            var el = $('#append-filter').find('._binding_product');

            $('#append-filter').find('._binding_product').html('').html(response);
            R.Extra.PagePaging(true, url, params, el);
            //document.addEventListener("DOMContentLoaded", yall);

            //R.Extra.PagePaging();

            R.Extra.BindingExtraToProduct();
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
    },
    Filter_v2: function (index) {
        //Get thuong hieu
        var parent_id = $('#zone-current').data('id');
        var filter_t = $('#filter-text').val();

        var manuList = [];
        $('.filter-item.manufacture').find('input[type="checkbox"]').each(function (element) {
            if ($(this).is(':checked'))
                manuList.push($(this).data('value'))
        })

        var list_range = [];
        $('.filter-item.price').find('input[type="checkbox"]').each(function (element) {
            if ($(this).is(':checked')) {
                var x = $(this).data('value').split('-');
                x.forEach(function (e) {
                    var ss = parseInt(e);
                    list_range.push(ss);
                })
            }
        })
        var min_price = 0;
        var max_price = 0;

        if (list_range.length > 0) {
            var list_range_min = Math.min.apply(null, list_range);
            var list_range_max = Math.max.apply(null, list_range);
            min_price = list_range_min;
            max_price = list_range_max;
        }



        var fp = [];

        $('.filter-item.dynamic-filter').each(function (e) {
            $(this).find('input[type="checkbox"]').each(function (element) {
                if ($(this).is(':checked')) {
                    var filter_item = {
                        SpectificationId: $(this).data('spec'),
                        Value: $(this).data('value')
                    }
                    fp.push(filter_item);
                }
            })
        })

        var idx = $('.paginationjs-page.active').data('num');
        var sort_price = 0;
        var sort_rate = 0;
        if ($('.sort-rate').hasClass('active')) {
            sort_rate = parseInt($('.sort-rate').data('value'));
        }
        $('.sort-price').each(function (i,v) {
            if ($(v).hasClass('active')) 
                sort_price = parseInt($(v).data('value'));
        })
        var locationId = R.Product.location_id;
        var params = {
            parentId: parent_id,
            lang_code: 'vi-VN',
            locationId: locationId,
            manufacture_id: manuList.toString(),
            min_price: parseInt(min_price),
            max_price: parseInt(max_price),
            sort_price: sort_price,
            sort_rate: sort_rate,
            color_code: '',
            filter: fp,
            filter_text: filter_t,
            material_type: 0,
            pageNumber: typeof(idx) === 'undefined' ? 1 : idx,
            pageSize: 16
        }

        console.log(params);

        var url = R.Product.culture + "/FilterProduct/FilterProductBySpectification"
        $.post(url, params, function (response) {
            //console.log(response);
            var el = $('#append-filter').find('._binding_product');
            
            $('#append-filter').find('._binding_product').html('').html(response);
            R.Extra.PagePaging(true, url, params, el);
            //document.addEventListener("DOMContentLoaded", yall);
            
            //R.Extra.PagePaging();
            
            R.Extra.BindingExtraToProduct();
            R.Product.BindingTotal();
            R.Product.RegisterEvent();
        })
        //R.Estimates.RegisterEvent();

        //console.log(params);
    },
    CheckBack: function () {
        var isBack = false;
        if ($('#filter-text').val() != "") {
            isBack = true;
        }
        $('.main-boloc input[type=checkbox]').each(function (element) {
            if ($(this).is(":checked"))
                isBack = true;
        })
        if (isBack) {
            R.Product.Filter_v2();
            //R.Product.Filter_v2_Mobile();
        }
            

    }
}
$(function() {
    R.Product.Init();
})
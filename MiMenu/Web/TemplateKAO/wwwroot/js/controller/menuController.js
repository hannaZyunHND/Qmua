R.Menu = {
    Init: function () {
        R.Menu.culture = R.Culture();
        R.Menu.FirstLoadLocation();
        R.Menu.RegisterEvent();
        R.Menu.current_location = R.CurrentLocationId();
        //R.Menu.GetDeptByLocationId(R.CurrentLocationId());
    },
    RegisterEvent: function () {
        //$('#find-product').keyup(function () {
        //    R.Menu.FindProductAutocomplete($(this));
        //});
        $('.zone-have-filter').off('click').on('click', function () {
            var params = {
                type : $(this).data('ftype'),
                value : $(this).data('fval')
            }
            sessionStorage.setItem('filterZone', JSON.stringify(params))
            var url = $(this).data('tarurl');
            console.log(url);
            window.location.href = url;
        })
        $('._btn-lien-he-bh').off('click').on('click', function () {
            R.Menu.GetDeptByLocationId(R.CurrentLocationId());
            $('#modal-lien-he').modal('show');
        })
        $('.department-location').off('change').on('change', function () {
            //console.log(1);
            var id = $(this).val();
            R.Menu.GetDeptByLocationId(id);
        });
        $('._choosen').off('change').on('change', function () {
            var add = $("input[name='department']:checked").parent('label').text();
            //var add = $(this).parent().find('_address').text();
            //console.log(add);
            $('#dropdownMenu1-1').html('').html(add);
        })
        $('.dropdown-cart-trigger').off('click').on('click', function () {
            R.Menu.LoadDropdownCart();
            //R.Order.LoadOrderDetail();
        }); 
        $('.dropdown-cart-trigger-mobile').off('click').on('click', function () {
            R.Menu.LoadDropdownCartMoblie();
        })
        $('.small-cart-plus').off('click').on('click', function () {
            var affected_id = $(this).closest('.item-cart').data('id');
            var quantity = parseInt($(this).closest('.item-cart').find('.small-cart-quantity').text());
            $(this).closest('.item-cart').find('.small-cart-quantity').text(quantity + 1);
            //resave-local
            var r = JSON.parse(localStorage.getItem("arrProduct"));
            r.find(function (element) {
                if (element.product_id == affected_id) {
                    element.quantity = parseInt(quantity) + 1;
                }

            })
            /////console.log(r);
            //re-save local
            localStorage.setItem("arrProduct", JSON.stringify(r));
            R.LoadCart();
            R.Menu.DropdownOrderDetailInLocalStorage();
        });
        $(document).mouseup(function (e) {
            var container = $(".dropdown-cart");
            var container_mobile = $('.dropdown-cart-mobile');
            var last_seen_container = $('.spvx');

            // if the target of the click isn't the container nor a descendant of the container
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                container.hide();
            }
            if (!container_mobile.is(e.target) && container_mobile.has(e.target).length === 0) {
                container_mobile.hide();
            }
            if (!last_seen_container.is(e.target) && last_seen_container.has(e.target).length === 0 && last_seen_container.hasClass('show')) {
                last_seen_container.toggleClass('show');
                //alert(1);
            }
        });
        $('.small-cart-minus').off('click').on('click', function () {

            var affected_id = $(this).closest('.item-cart').data('id');
            var quantity = parseInt($(this).closest('.item-cart').find('.small-cart-quantity').text());
            $(this).closest('.item-cart').find('.small-cart-quantity').text(quantity - 1);
            //resave-local
            var r = JSON.parse(localStorage.getItem("arrProduct"));
            r.find(function (element) {
                if (element.product_id == affected_id) {
                    element.quantity = parseInt(quantity) - 1;
                }

            })
            /////console.log(r);
            //re-save local
            localStorage.setItem("arrProduct", JSON.stringify(r));
            R.LoadCart();
            R.Menu.DropdownOrderDetailInLocalStorage();
        });
        $('.last-seen-link').off('click').on('click', function () {
            R.Menu.LoadLastSeenProduct();
        })
        $('.select-language').off('click').on('click', function () {
            var lang = $(this).data('lang');
            R.Menu.SelectLanguage(lang);
        })
        $('#modal-lien-he').off('submit').on('submit', function () {
            //alert(1);
            /*
             <div class="form-group">
                            <input type="text" id="txt-name-bh" class="form-control" placeholder="Họ và tên (bắt buộc)" />
                        </div>
                        <div class="form-group">
                            <input type="text" id="txt-phone-bh" class="form-control" placeholder="Số điện thoại" />
                        </div>
                        <div class="form-group">
                            <input type="text" id="txt-yc-bh" class="form-control" placeholder="Yêu cầu" />
                        </div>
                        <div class="form-group">
                            <textarea type="text" rows="5" class="form-control" id="txt-nd-bh" placeholder="Nội dung chi tiết"></textarea>
                        </div>
             */
            var name = $('#txt-name-bh').val();
            var phone = $('#txt-phone-bh').val();
            var yc = $('#txt-yc-bh').val();
            var add = $('#dropdownMenu1-1').html();
            var nd = "Địa điểm: " + add + "";
            nd += $('#txt-nd-bh').val();
            var add = $('#dropdownMenu1-1').html();
            var params = {
                Name: name,
                Phone: phone,
                Address: '',
                Title: yc,
                Content: nd,
                Type: 1,
                Source: 'web'
            }

            R.Menu.CreateServicesTicket(params);
            return false;

        })
        $('.small-cart-delte').off('click').on('click', function () {
            var affected_id = $(this).closest('.item-cart').data('id');
            var r = JSON.parse(localStorage.getItem("arrProduct"));
            r.find(function (element) {
                if (element.product_id == affected_id) {
                    R.RemoveCartItem(affected_id);
                }
            })
            $(this).closest('.item-cart').remove();
            R.LoadCart();
            R.Menu.DropdownOrderDetailInLocalStorage();
        })

        /////console.log(r);
        //re-save local
        //localStorage.setItem("arrProduct", JSON.stringify(r));
        //R.LoadCart();
        //R.Menu.DropdownOrderDetailInLocalStorage();
    },
    GetDeptByLocationId: function (id) {
        var params = {
            id: id
        }
        var url = "/Store/ListDeptByLocId?id=" + id + "";
        $.get(url, function (response) {
            $('.department-location').val(id);
            ////console.log(response);
            $('#your-custom-id').html('').html(response);
            R.Menu.RegisterEvent();
            ////console.log(JSON.parse(response));
        })
    },
    CreateServicesTicket: function (params) {
        var url = R.Menu.culture + '/Extra/CreateServiceTicket';
        $.post(url, params, function (response) {
            if (response > 0) {
                $('#txt-name-bh').val('');
                $('#txt-phone-bh').val('');
                $('#txt-yc-bh').val('');
                $('#txt-nd-bh').val('');
                $('#modal-lien-he').modal('hide');
                $('#modal-xn').modal('show');
                return false;
            }

        })
        return false;
    },
    SelectLanguage: function (lang) {
        var url = 'Home/SetLanguage';
        var params = {
            culture: lang
        }
        $.post(url, params, function (response) {
            //console.log('Change language success!');
            var host = window.location.hostname;
            window.location.href = "/";
        })
    },
    LoadDropdownCart: function () {
        var product_ids = $('#link-target-cart').data('product_ids')
        var params = {
            product_ids: product_ids
        }
        //Load ajax
        $.post(R.Menu.culture + "/Order/LoadDropdonwCart", params, function (response) {
            var htmlOriginal = $.fn.html;

            // redefine the `.html()` function to accept a callback
            $.fn.html = function (html, callback) {
                // run the old `.html()` function with the first parameter
                var ret = htmlOriginal.apply(this, arguments);
                // run the callback (if it is defined)
                if (typeof callback == "function") {
                    callback();
                }
                // make sure chaining is not broken
                return ret;
            }
            $('._binding_dropdown_cart').html('').html(response);
            var r = JSON.parse(localStorage.getItem("arrProduct"));
            var product_ids = [];

            r.forEach(function (element) {
                var product_id = element.product_id;
                product_ids.push(product_id);
                var el = "#item-dropdown-cart" + product_id;
                ////console.log(el);
                $(el).find('.small-cart-quantity').text(element.quantity);

            })
            var quantity_cart = r.length;
            //alert(quantity_cart);
            var count_small_cart = 0;
            $('.dropdown-cart').find('.item-cart').each(function (element) {
                count_small_cart++;
            });
            var remain = quantity_cart - count_small_cart;
            //var remain = quantity_cart - count_small_cart;
            //alert(remain);
            if (quantity_cart > 0) {
                $('.small-total-cart').parent().show();
                $('.dropdown-quantity-cart').parent().show();
                $('.dropdown-quantity-cart').text(remain);
                $('.dropdown-cart').toggle();
            }
            //Bind total
            R.Menu.DropdownOrderDetailInLocalStorage();
            //small-total-cart

            R.Menu.RegisterEvent();

        })
    },
    LoadDropdownCartMoblie: function () {
        var product_ids = $('#link-target-cart-mobile').data('product_ids')
        var params = {
            product_ids: product_ids
        }
        //Load ajax
        $.post(R.Menu.culture + "/Order/LoadDropdonwCart", params, function (response) {
            var htmlOriginal = $.fn.html;

            // redefine the `.html()` function to accept a callback
            $.fn.html = function (html, callback) {
                // run the old `.html()` function with the first parameter
                var ret = htmlOriginal.apply(this, arguments);
                // run the callback (if it is defined)
                if (typeof callback == "function") {
                    callback();
                }
                // make sure chaining is not broken
                return ret;
            }
            $('.cart-mobile ._binding_dropdown_cart').html('').html(response);
            var r = JSON.parse(localStorage.getItem("arrProduct"));
            var product_ids = [];

            r.forEach(function (element) {
                var product_id = element.product_id;
                product_ids.push(product_id);
                var el = "#item-dropdown-cart" + product_id;
                ////console.log(el);
                $(el).find('.small-cart-quantity').text(element.quantity);

            })
            var quantity_cart = r.length;
            //alert(quantity_cart);
            var count_small_cart = 0;
            $('.dropdown-cart-mobile').find('.item-cart').each(function (element) {
                count_small_cart++;
            });
            var remain = quantity_cart - count_small_cart;
            //var remain = quantity_cart - count_small_cart;
            //alert(remain);
            if (quantity_cart > 0) {
                $('.small-total-cart').parent().show();
                $('.dropdown-quantity-cart').parent().show();
                $('.dropdown-quantity-cart').text(remain);
                $('.dropdown-cart-mobile').toggle();
            }
            //Bind total
            R.Menu.DropdownOrderDetailInLocalStorage();
            //small-total-cart

            R.Menu.RegisterEvent();

        })
    },
    DropdownOrderDetailInLocalStorage: function () {
        var product_ids = $('#link-target-cart').data('product_ids')
        var params = {
            product_ids: product_ids.toString()
        }
        var url = R.Menu.culture + "/Order/LoadOrderDetailJson";
        $.post(url, params, function (data) {
            //console.log(data);
            //Match vào localstorage
            var a = JSON.parse(localStorage.getItem("arrProduct"));
            data.forEach(function (d) {
                a.find(function (item) {
                    if (item.product_id == d.productId)
                        if (d.flashSaleId > 0)
                            item.price = d.productPriceInFlashSale
                        else
                            item.price = d.salePrice

                })
            })
            //console.log(a);
            var total_price = 0;
            a.forEach(function (item) {

                total_price += item.price * parseInt(item.quantity);
            })
            $('.small-total-cart').html(R.FormatNumber(total_price) + 'đ');
            R.Order.RegisterEvent();
        })



    },
    //FindProductAutocomplete: function (dom) {
    //    dom.autocompleter({

    //    })
    //},
    LoadLastSeenProduct: function () {
        console.log(localStorage.getItem("lastSeen"));
        var r = JSON.parse(localStorage.getItem("lastSeen")).slice(0, 6);
        if (r == null)
            r == "";
        console.log(r);
        var product_ids = "";
        if (r != null) {
            product_ids = r.slice(0, 6).toString();
            var params = {
                product_ids: product_ids
            }
            $.post(R.Menu.culture + '/Product/VLastSeen', params, function (response) {
                //alert("success");
                $('._binding_last_seen_product').html('').html(response);
                R.Extra.BindingExtraToProduct();
                $(".menu-fix .spvx").toggleClass("show");
            })
        }



    },
    FirstLoadLocation: function () {
        var r = sessionStorage.getItem("locationFirstLoad");
        if (r == "1") {
            var el = $('.only-first-load-location');
            el.css('display', 'inline');
            el.css({
                'display': 'inline',
                'position': 'fixed',
                'width': '100%',
                'background-color': 'rgba(255, 255, 255, 0.7)',
                'z-index': '9999',
            })
            //$("body").css({
            //    "background-color": "rgba(0,0,0,0.5)",
            //    "z-index": "999",
            //    "position": "relative",
            //});
            //el.css({
            //    //"background-color": "rgba(200,200,200,1)",
            //    "position": "relative",
            //    "z-index": "9919",
            //    "opacity": "1"
            //});
            var isLoad = "0";
            sessionStorage.setItem("locationFirstLoad", isLoad);
        }
    }
}
//only-first-load-location


$(function () {
    R.Menu.Init()
})
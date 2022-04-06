R.ProductDetail = {
    Init: function () {
        R.ProductDetail.RegisterEvent();
        R.ProductDetail.culture = R.Culture();
        R.ProductDetail.CountDownFlashSale('.timer');
        R.ProductDetail.AddLastSeenProduct();
        R.ProductDetail.AddViewCountProduct();
        
        R.ProductDetail.current_location = R.CurrentLocationId();
        R.ProductDetail.GetDeptByLocationId(R.CurrentLocationId());
        //$('#txt-time-input').date
    },
    RegisterEvent: function () {
        $('#modal-showromm-ganhat').off('submit').on('submit', function () {
            var current_url = window.location.href;
            var name = $('#txt-name-bh-1').val();
            var phone = $('#txt-phone-bh-1').val();
            var add = $('._picking_address').text();
            var booking = $('#txt-time-input').val();
            var booking_time = moment(booking, 'DD/MM/YYYY').format('YYYY-MM-DD');
            var yc = "Kiểm tra siêu thị có hàng trưng bày";
            var nd = 'link: ' + current_url + '';
            nd += 'Thời gian: ' + booking + '';
            nd += 'Địa điểm: ' + add + '';
            var params = {
                Name: name,
                Phone: phone,
                Address: '',
                Title: yc,
                Content: nd,
                Type: 2,
                Source: 'web',
                BookingTime: booking_time
            }
            console.log(params);
            R.ProductDetail.BookingProductDetail(params);
            return false;

        })
        $('._choosen').off('change').on('change', function () {
            var add = $("input[name='department']:checked").parent('label').text();
            //var add = $(this).parent().find('_address').text();
            console.log(add);
            $('._picking_address').text('').text(add);
        })
        $('.find-showroom').off('click').on('click', function () {
            $('#modal-showromm-ganhat').modal('show');
            R.ProductDetail.GetDeptByLocationId(R.CurrentLocationId());
            //$('#modal-showromm-ganhat').modal('show');

        })
        $('.location-item').off('click').on('click', function () {
            //Get text
            var text = $(this).text();
            $(this).closest(".select-local").find(".dropdown-toggle").text(text);
            //Get gia
            var normal_price = $(this).data('price');
            var sale_price = $(this).data('sale-price');
            var unit = $(this).data('unit');
            console.log(normal_price, sale_price);
            $('#price-sale').text(R.FormatNumber(sale_price) + ' đ/');
            $('#default_price').text(R.FormatNumber(sale_price) + ' đ/');
            $('#default-sale-price').data('sale-price', sale_price);
            $('#price-normal').text(R.FormatNumber(normal_price) + 'đ');
            if (normal_price == 0) {
                $('#price-normal').hide();
            }
            //Goi lay gia cua combo
            var product_id = $(this).data('product-id');
            var location_id = $(this).data('location-id');

            R.ProductDetail.GetComboInProduct(product_id, location_id);
        });
        $('#add-cart-combo-remake').off('click').on('click', function () {
            var id = $(this).data('combo-id');
            R.ProductDetail.AddToCartComboV2(id);
        })
        $('.choose_combo').change(function() {
            R.ProductDetail.InfomationCombo();
        });
        $('#plus').off('click').on('click', function() {
            console.log('plus');
            var quantity = parseInt($('#quantity').val()) + 1;
            $('#quantity').val(quantity);
        });
        $('#minus').off('click').on('click', function() {
            console.log('minus');
            var quantity = parseInt($('#quantity').val()) - 1;
            if (quantity <= 1)
                quantity = 1;
            $('#quantity').val(quantity);
        });
        $('#add-to-cart').off('click').on('click', function() {
            var ls = $(this).closest('.product-detail').find('#list_combo');
            var thumb = ls.find('.image-product');
            var cardElm = $('.dropdown-cart-trigger');
            if (thumb.length > 0)
                flyToElement(thumb[0], $(cardElm));

            R.ProductDetail.AddToCart();
        });
        //buy-now
        $('#buy-now').off('click').on('click', function () {
            if (parseInt($(this).data('quantit')) > 0)
                R.ProductDetail.AddToCart();
            //dan sang trang gio hang
            //var url = R.ProductDetail.culture + '/cart.html';
            //window.location.href = url;

        });
        $('#add-cart-combo').off('click').on('click', function() {
            R.ProductDetail.AddToCartCombo();
        });
        
        $('#rating-form').off('submit').on('submit', function() {

            //int objectId, int objectType, string name, string phoneOrEmail, string avatar, string content, string type, int rate
            /*
             $("#btn-upload-sitemap").click(function (evt) {
            var fileUpload = $("#upload-xml-sitemap").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "/UploadSEOFile.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) { alert(result); },
                error: function (err) {
                    alert(err.statusText)
                }
            });

            evt.preventDefault();
        });
             */
            var type = $(this).data('type');
            var object_id = $(this).data('object-id');
            var object_type = $(this).data('object-type');
            var name = $(this).find('.txt_name').val();
            var phoneOrEmail = $(this).find('.txt_phoneOrEmail').val();
            var avatar = $(this).find('.input-choose-file').data('target-url');
            var content = $(this).find('.txt_content').val();
            var rate = 0;
            //Getrate
            $(this).find('.star-rating-dynamic').find('.checked').each(function(element) {
                    rate++;
                })
                //alert(rate);
                //uploadfile
            var fileUpload = $(this).find(".input-choose-file").get(0);
            var files = fileUpload.files;
            var params = {
                objectId: object_id,
                objectType: object_type,
                name: name,
                phoneOrEmail: phoneOrEmail,
                avatar: avatar,
                content: content,
                type: type,
                rate: rate
            }
            R.ProductDetail.CreateComment(files, params);
            return false;
        });
        $('#picking-estimates').off('click').on('click', function() {
            var id = $(this).data('id');
            var material = $(this).data('material');
            var quantity = parseInt($('#quantity').val())
            var price = $(this).data('price');
            var sale_price = $(this).data('sale-price');
            var avatar = $(this).data('avatar');
            var SpecName = $(this).data('spec-name');
            var SpecValue = $(this).data('spec-value');
            var title = $(this).data('title');
            var item_obj = {
                Id: id,
                Title: title,
                SalePrice: sale_price,
                Price: price,
                MaterialType: material,
                Avatar: avatar,
                SpecName: SpecName,
                SpecValue: SpecValue
            }
            R.ProductDetail.AddEstimatesLocal(item_obj);
        });
        $('.star-rating-dynamic i ').off('click').on('click', function() {
            $(this).parent().find(".star-rating-dynamic i").removeClass("checked");
            $(this).addClass("checked");
            $(this).prevAll().addClass("checked");
            $(this).nextAll().removeClass("checked");
        });
        $('.department-location').off('change').on('change', function () {
            console.log(1);
            var id = $(this).val();
            R.ProductDetail.GetDeptByLocationId(id);
        });
    },
    BookingProductDetail: function (params) {
        var url = "/Extra/CreateServiceTicketWBookingTime";
        $.post(url, params, function (response) {
            console.log(response);
            $('#modal-xn').modal('show');
            R.ProductDetail.CloseModalAndClearText();
            return false;
        })

    },
    InfomationCombo: function() {
        var count = 0;
        var sale_price = 0;
        $('.choose_combo').each(function(element) {
            if (this.checked) {
                count++;
                sale_price += parseInt($(this).data('sale-price'));
            }
        });
        $('#count_combo').text(count);
        $('#total_combo_price').text(R.FormatNumber(sale_price) + "đ");
        R.ProductDetail.RegisterEvent();
    },
    GetComboInProduct: function(product_id, location_id) {
        var params = {
            product_id: product_id,
            location_id: location_id
        }
        $.post(R.ProductDetail.culture + '/Product/GetComboByLocationId', params, function(response) {
            $('#_binding_combo').html(response);
            R.ProductDetail.InfomationCombo();
            R.ProductDetail.RegisterEvent();
        })
    },
    AddToCart: function() {
        //    {
        //        product_id: 1,
        //        quantity: 1,
        //        add_time: getdate(),
        //        promotion: [
        //            {
        //                id: 1,
        //                checked: true
        //            },
        //            {
        //                id: 2,
        //                checked: false
        //            }
        //        ]
        //    }

        var product_id = $('#add-to-cart').data('product-id')
        var quantity = $('#quantity').val();
        var list_promotion = [];
        $('.choose_promotion').each(function(element) {
            var id = $(this).data('promotion-id');
            var checked = false;
            if (this.checked)
                checked = true;
            var obj = {
                id: id,
                checked: checked
            }
            list_promotion.push(obj);
        })
        var cart_item = {
                product_id: product_id,
                quantity: parseInt(quantity),
                add_time: Date.parse(new Date()),
                promotion: list_promotion
            }
            //Add to localStorage
        var arrProduct = JSON.parse(localStorage.getItem("arrProduct"));
        arrProduct.push(cart_item);
        localStorage.setItem("arrProduct", JSON.stringify(arrProduct));
        R.LoadCart();
    },
    AddToCartCombo: function() {
        console.log(1);
        var arrProduct = JSON.parse(localStorage.getItem("arrProduct"));
        $('.choose_combo').each(function(element) {
            if (this.checked) {
                var product_id = $(this).data('product-id');
                var quantity = 1;
                var promotions = [];
                var cart_item = {
                    product_id: product_id,
                    quantity: quantity,
                    add_time: Date.parse(new Date()),
                    promotion: promotions
                }
                arrProduct.push(cart_item);
            }
        })
        localStorage.setItem("arrProduct", JSON.stringify(arrProduct));
        R.LoadCart();
    },
    AddToCartComboV2: function (id) {
        console.log(id);
        var arrProduct = JSON.parse(localStorage.getItem("arrProduct"));
        var product_id = id;
        var quantity = 1;
        var promotions = [];
        var cart_item = {
            product_id: product_id,
            quantity: quantity,
            add_time: Date.parse(new Date()),
            promotion: promotions
        }
        arrProduct.push(cart_item);
        localStorage.setItem("arrProduct", JSON.stringify(arrProduct));
        R.LoadCart();
    },
    CreateComment: function(files, params) {
        //Kiem tra files
        //if (files != null) {
        //    var dataFile = new FormData();
        //    for (var i = 0; i < files.length; i++) {
        //        dataFile.append(files[i].name, files[i]);
        //    }
        //    $.ajax({
        //        url: "https://jhcms.migroup.asia/api/UploadFile/UploadImage",
        //        type: "POST",
        //        data: dataFile,
        //        contentType: false,
        //        processData: false,
        //        success: function (result) { alert(result); },
        //        error: function (err) {
        //            alert(err.statusText)
        //        }
        //    });
        //}

        var url = R.ProductDetail.culture + "/Extra/CreateComment";
        $.post(url, params, function(response) {
            $('#modal-xn').modal('show');
            R.ProductDetail.CloseModalAndClearText();
            return false;
        })
    },
    CloseModalAndClearText: function() {
        $('.txt_name').val('');
        $('.txt_phoneOrEmail').val('');
        $('.txt_content').val('');
        $('#modal-danh-gia').modal('hide');
        /*
         var name = $('#txt-name-bh-1').val();
            var phone = $('#txt-phone-bh-1').val();
            var add = $('._picking_address').text();
         */
        $('#txt-name-bh-1').val('');
        $('#txt-phone-bh-1').val('');
        $('._picking_address').text('');
        $('#modal-showromm-ganhat').modal('hide');
    },
    AddLastSeenProduct: function() {

        var id_san_pham = $('.product-detail').data('id');
        var arrDaXem = [];
        var a = localStorage.getItem('lastSeen');
        if (a == null) {
            console.log("lastSeen Null");
            //Tao ra mang arrDaXem va add mang nay vao session
            arrDaXem.push(id_san_pham);
            localStorage.setItem('lastSeen', JSON.stringify(arrDaXem));
        }
        if (a != null) {
            arrDaXem = JSON.parse(a);
            //Kiem tra trong mang nay co san pham da xem chua
            var r = 0;
            for (var i = 0; i < arrDaXem.length; i++) {
                if (arrDaXem[i] == id_san_pham)
                    r = i;
            }
            if (r <= 0) {
                arrDaXem.unshift(id_san_pham);
            }
            localStorage.setItem('lastSeen', JSON.stringify(arrDaXem));

        }
    },
    AddViewCountProduct: function() {

        var id_san_pham = $('.product-detail').data('id');
        var url = "/Extra/CreateViewCount";
        var params = {
            objectId: id_san_pham,
            type: 'product'
        }
        $.post(url, params, function(response) {
            console.log(response);
        })

    },

    AddEstimatesLocal: function(item_obj) {
        var r = localStorage.getItem("arrEstimates");

        if (r == null) {
            var arr = [];
            arr.push(item_obj);
            localStorage.setItem("arrEstimates", JSON.stringify(arr));
            var url = "/du-toan-cong-trinh";
            window.location.href = url;
        }
        if (r != null) {
            var arr = JSON.parse(r);
            var flag = false;
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].MaterialType == item_obj.MaterialType) {
                    arr.splice(i, 1)
                    arr.push(item_obj);
                    flag = true;
                }

            }
            if (flag == false) {
                arr.push(item_obj);
            }
            localStorage.setItem("arrEstimates", JSON.stringify(arr));
            var url = "/du-toan-cong-trinh";
            window.location.href = url;

        }

    },
    CountDownFlashSale: function(el) {
        var end_time = $(el).data('end-time');
        R.CountDown(end_time, el);

    },
    GetCurrentLocation: function () {
        var loc = R.ProductDetail.current_location;
        $('.department-location').val(loc);
    },
    GetDeptByLocationId: function (id) {
        var params = {
            id: id
        }
        var url = "/Store/ListDeptByLocId?id="+id+"";
        $.get(url, function (response) {
            $('.department-location').val(id);
            console.log(response);
            $('#modal-showromm-ganhat').find('.cust-bg-mdal').html('').html(response);
            R.ProductDetail.RegisterEvent();
            //console.log(JSON.parse(response));
        })
    }

}


$(function() {
    R.ProductDetail.Init();
})

function flyToElement(flyer, flyingTo) {
    var $func = $(this);

    // Nhân bản đối tượng(hình ảnh) sẽ bay vào giỏ hàng
    var flyerClone = $(flyer).clone();

    // Thiết lập đối tượng nhân bản này trùng với đối tượng thực tế 
    $(flyerClone).css({
        position: 'absolute',
        top: $(flyer).offset().top + "px",
        left: $(flyer).offset().left + "px",
        opacity: 1,
        'z-index': 1000
    }).appendTo($('body'));

    // Lấy về tọa độ của giỏ hàng
    var gotoX = $(flyingTo).offset().left;
    var gotoY = $(flyingTo).offset().top;

    // Hiệu ứng bay vào giỏ hàng
    $(flyerClone).animate({
            opacity: 0.4,
            left: gotoX,
            top: gotoY,
            width: $(flyingTo).width(),
            height: $(flyingTo).height()
        }, 700,
        function() {
            // Hiệu ứng rung lắc khi sản phẩm đã bay vào giỏ hàng
            //$(flyingTo).effect("shake", function() {
                
            //});
            // Ẩn đối tượng sản phẩm bay vào giỏ hàng và delete nó
            $(flyerClone).fadeOut('fast', function () {
                $(flyerClone).remove();
            });
        });
}
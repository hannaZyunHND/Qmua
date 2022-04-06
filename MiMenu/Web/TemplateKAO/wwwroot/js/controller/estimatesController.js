R.Estimates = {
    Init: function () {
        R.Estimates.location_id = R.CurrentLocationId();
        R.Estimates.culture = R.Culture();
        R.Estimates.RegisterEvent();
        R.Estimates.RenderEstimatesItemFromLocal();
        R.Estimates.default_loading_page = 1;
        R.Estimates.SortByPrice = 0;
    },
    RegisterEvent: function () {
        $('.infomation-room input').off('focusout').on('focusout', function () {
            R.Estimates.CalculateSpaceRoom();
            R.Estimates.CalculateQuantityInItemChoose();
        })
        $('.choose-material-type').off('click').on('click', function () {
            var material_type = $(this).data('material-type');
            R.Estimates.BindingMaterialModel(material_type);
        });
        $(".list-filter-modal .heading").click(function () {
            //$(this).closest(".menu.sub").find('.spectification-value').removeClass('active');
            $(this).closest(".menu.sub").toggleClass("show");
        });
        $('.spectification-value').off('click').on('click', function () {

            //Remove all in 1 parent

            //Set active
            if ($(this).hasClass('active')) {
                //console.log('is active');
                $(this).removeClass('active');
            }

            else {
                $(this).parent().find('.spectification-value').removeClass('active');
                //console.log('nhay vao day');
                $(this).addClass('active');
            }

            R.Estimates.ChooseSpectificationValue();
        })
        $('.product-pick-item').off('click').on('click', function () {
            var material_type = $('.list-filter-modal').data('material-type');
            var item_id = $(this).data('id');
            var item_title = $(this).data('title');
            var item_sale_price = $(this).data('sale-price');
            var item_price = $(this).data('price');
            var item_avatar = $(this).data('avatar');
            var item_spec_name = $(this).data('spec-name');
            var item_spec_value = $(this).data('spec-value');
            var item_obj = {
                Id: item_id,
                Title: item_title,
                SalePrice: item_sale_price,
                Price: item_price,
                MaterialType: material_type,
                Avatar: item_avatar,
                SpecName: item_spec_name,
                SpecValue: item_spec_value
            }
            console.log(item_obj)
            R.Estimates.PickingEstimatesItem(item_obj)
        })
        $('.item-selected-edit-button').off('click').on('click', function () {
            var material_type = $(this).closest('.item-choose').data('material-type');
            R.Estimates.BindingMaterialModel(material_type);
        });
        $('.item-selected-del-button').off('click').on('click', function () {
            var materialType = $(this).closest('.item-choose').data('material-type');
            //alert(materialType);
            $(this).closest('.item-choose').find('.material-item-selected').data('is-rendered', false);
            $(this).closest('.item-choose').find('.material-item-selected').hide();
            $(this).closest('.item-choose').find('.btn-choose').show();

            var acreage = $('#acreage').val() == null ? 0 : $('#acreage').val();
            var perimeter = $('#perimeter').val() == null ? 0 : $('#perimeter').val();
            //console.log(acreage, perimeter);
            //R.Estimates.CalculateTotalInItemChoose($(this), data_price);
            var r = localStorage.getItem("arrEstimates");
            if (r != null) {
                var arr = JSON.parse(r);
                for (var i = 0; i < arr.length; i++)
                    if (arr[i].MaterialType == materialType)
                        arr.splice(i, 1);
                localStorage.setItem("arrEstimates", JSON.stringify(arr));
            }
            R.Estimates.FillValueToCalculatedCard(acreage, perimeter);
        });
        $('.item-selected-minus-button').off('click').on('click', function () {
            //console.log('minus');

            var unit = $(this).closest('.item-choose').data('unit');
            var data_price = $(this).closest('.material-item-selected').data('sale-price');
            if (unit == 'm' || unit == 'm2') {
                var r = parseFloat($(this).closest('.item-choose').find('.item-selected-quantity').val());
                r -= 0.1;
                $(this).closest('.item-choose').find('.item-selected-quantity').val(r.toFixed(1));
            } else {
                var r = parseFloat($(this).closest('.item-choose').find('.item-selected-quantity').val());
                r -= 1;
                $(this).closest('.item-choose').find('.item-selected-quantity').val(r.toFixed(1));
            }
            var acreage = $('#acreage').val() == null ? 0 : $('#acreage').val();
            var perimeter = $('#perimeter').val() == null ? 0 : $('#perimeter').val();
            console.log(acreage, perimeter);
            R.Estimates.CalculateTotalInItemChoose($(this), data_price);
            R.Estimates.FillValueToCalculatedCard(acreage, perimeter);



        });
        $('.item-selected-plus-button').off('click').on('click', function () {
            console.log('minus');
            var unit = $(this).closest('.item-choose').data('unit');
            var data_price = $(this).closest('.material-item-selected').data('sale-price');
            if (unit == 'm' || unit == 'm2') {
                var r = parseFloat($(this).closest('.item-choose').find('.item-selected-quantity').val());
                r += 0.1;
                $(this).closest('.item-choose').find('.item-selected-quantity').val(r.toFixed(1));
            } else {
                var r = parseFloat($(this).closest('.item-choose').find('.item-selected-quantity').val());
                r += 1;
                $(this).closest('.item-choose').find('.item-selected-quantity').val(r.toFixed(1));
            }
            var acreage = $('#acreage').val() == '' ? 0 : $('#acreage').val();
            var perimeter = $('#perimeter').val() == '' ? 0 : $('#perimeter').val();
            console.log(acreage, perimeter);
            R.Estimates.CalculateTotalInItemChoose($(this), data_price);
            R.Estimates.FillValueToCalculatedCard(acreage, perimeter);
        });
        $('.contruction-estimates-form').off('submit').on('submit', function () {
            //alert('submited');
            var tinh = $(this).find('.tinh-thanh option:selected').html();
            var quan = $(this).find('.quan-huyen option:selected').html();
            var phuong = $(this).find('.phuong-xa option:selected').html();
            var nha = $(this).find('.so-nha').val();
            var address = nha + " - " + phuong + " - " + quan + " - " + tinh;
            var gender = $("input[name='gender-radio']:checked").val();
            var customer_infomation = {
                Gender: gender,
                Name: $(this).find('.name').val(),
                PhoneNumber: $(this).find('.phone-number').val(),
                Note: $(this).find('.note').val(),
                Address: address,
                Id: 0
            }
            var extra_infomation = [];
            $(this).find('.extra-infomation').each(function (element) {
                if ($(this).is(':checked'))
                    extra_infomation.push($(this).data('content'));
            })
            var product_infomation = [];
            $('.material-item-selected').each(function (element) {

                var el = $(this);
                if (el.data('is-rendered') == true) {
                    var product_infomation_item = {
                        ProductId: el.data('id'),
                        Name: el.data('name'),
                        LogPrice: el.data('sale-price'),
                        Quantity: parseFloat(el.find('.item-selected-quantity').val()),
                        Promotions: []
                    }
                    product_infomation.push(product_infomation_item);
                }

            });
            console.log(customer_infomation);
            console.log(extra_infomation);
            console.log(product_infomation);
            R.Estimates.RenderModalConfirmOrder(customer_infomation, extra_infomation, product_infomation);
            return false;
        });
        $('.tinh-thanh').off('change').on('change', function () {
            var locationType = 'quan_huyen';
            var parent = $(this).val();
            R.Estimates.PickingProvince(locationType, parent);
        });
        $('.quan-huyen').off('change').on('change', function () {
            var locationType = 'phuong_xa';
            var parent = $(this).val();
            R.Estimates.PickingProvince(locationType, parent);
        });
        $('.list-product-modal-2').unbind('scroll').bind('scroll', function () {
            console.log('scrool vao day');
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 20) {
                R.Estimates.default_loading_page = R.Estimates.default_loading_page + 1;
                R.Estimates.ChooseSpectificationValuePaging(R.Estimates.default_loading_page)
            }
        });
        $('.filter-text').off('focusout').on('focusout', function () {
            R.Estimates.ChooseSpectificationValue();
        });
        $('._sort_price').off('click').on('click', function () {
            //console.log(R.Estimates.SortByPrice);
            if (R.Estimates.SortByPrice == 0) {
                R.Estimates.SortByPrice = R.Estimates.SortByPrice + 1;
                R.Estimates.ChooseSpectificationValue();
                return null;
            }
            if (R.Estimates.SortByPrice == 1) {
                R.Estimates.SortByPrice = R.Estimates.SortByPrice - 1;
                R.Estimates.ChooseSpectificationValue();
                return null;
            }
                
            console.log(R.Estimates.SortByPrice);
            
        })
    },
    BindingMaterialModel: function (material_type) {
        var url = R.Estimates.culture + '/Estimates/GetSpectificationMenuByMaterialType';
        var params = {
            material_type: material_type
        }
        //alert(url);
        //alert(params);
        $.post(url, params, function (response) {
            $("#material-model").find('.modal-content').html('').html(response);
            $("#material-model").modal('show');
            R.Estimates.RegisterEvent();
        })
        R.Estimates.RegisterEvent();
    },
    ChooseSpectificationValuePaging: function (index) {
        var material_type = $('.list-filter-modal').data('material-type');
        var fp = [];
        $('.list-filter-modal').find('.spectification-value').each(function (element) {
            if ($(this).hasClass('active')) {
                var f = {
                    SpectificationId: $(this).data('spectification-id'),
                    Value: $(this).data('spectification-value')
                }
                fp.push(f);
            }
        })
        var manufacture_id = 0;
        var locationId = R.Estimates.location_id;
        var lang_code = 'vi-VN';
        var min_price = 0;
        var max_price = 0;
        var sort_price = 0;
        var sort_rate = 0;
        var color_code = "";
        var filer_text = $('.filter-text').val();
        var pageNumber = index;
        var pageSize = 10;

        var params = {
            parentId: 0,
            lang_code: lang_code,
            locationId: locationId,
            manufacture_id: manufacture_id,
            min_price: min_price,
            max_price: max_price,
            sort_price: R.Estimates.SortByPrice,
            sort_rate: sort_rate,
            color_code: color_code,
            filter: fp,
            filter_text: filer_text,
            material_type: material_type,
            pageNumber: pageNumber,
            pageSize: pageSize
        }
        var url = R.Estimates.culture + "/Estimates/GetProductInSpectificationSearch"
        $.post(url, params, function (response) {
            $('.list-product-modal-2').append(response);
            R.Estimates.default_loading_page = R.Estimates.default_loading_page + 1;
            R.Estimates.RegisterEvent();
        })
        R.Estimates.RegisterEvent();
    },
    ChooseSpectificationValue: function () {
        var material_type = $('.list-filter-modal').data('material-type');
        var fp = [];
        $('.list-filter-modal').find('.spectification-value').each(function (element) {
            if ($(this).hasClass('active')) {
                var f = {
                    SpectificationId: $(this).data('spectification-id'),
                    Value: $(this).data('spectification-value')
                }
                fp.push(f);
            }
        })
        var manufacture_id = 0;
        var locationId = R.Estimates.location_id;
        var lang_code = 'vi-VN';
        var min_price = 0;
        var max_price = 0;
        var sort_price = R.Estimates.SortByPrice;
        var sort_rate = 0;
        var color_code = "";
        var filer_text = $('.filter-text').val();
        var pageNumber = 1;
        var pageSize = 10;

        var params = {
            parentId: 0,
            lang_code: 'vi-VN',
            locationId: locationId,
            manufacture_id: manufacture_id,
            min_price: min_price,
            max_price: max_price,
            sort_price: sort_price,
            sort_rate: sort_rate,
            color_code: color_code,
            filter: fp,
            filter_text: filer_text,
            material_type: material_type,
            pageNumber: pageNumber,
            pageSize: pageSize
        }
        var url = R.Estimates.culture + "/Estimates/GetProductInSpectificationSearch"
        $.post(url, params, function (response) {
            $('.list-product-modal-2').html('').html(response);
            R.Estimates.default_loading_page = pageNumber + 1;
            R.Estimates.RegisterEvent();
        })
        R.Estimates.RegisterEvent();
    },
    PickingEstimatesItem: function (item_obj) {

        var r = localStorage.getItem("arrEstimates");
        if (r == null) {
            var arr = [];
            arr.push(item_obj);
            localStorage.setItem("arrEstimates", JSON.stringify(arr));
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
        }
        //console.log(localStorage.getItem("arrEstimates"));
        var length_room = parseFloat(parseFloat($('#length').val() == null ? 0 : $('#length').val()).toFixed(1));
        var width_room = parseFloat(parseFloat($('#width').val() == null ? 0 : $('#width').val()).toFixed(1));
        var count_door = parseFloat(parseFloat($('#count-door').val() == null ? 0 : $('#count-door').val()).toFixed(1));
        var length_door = parseFloat(parseFloat($('#length-door').val() == null ? 0 : $('#length-door').val()).toFixed(1));
        //console.log("length room: " + length_room + ", type: " + typeof (length_room));
        //console.log("width room: " + width_room + ", type: " + typeof (width_room));
        //console.log("count door: " + count_door + ", type: " + typeof (count_door));
        //console.log("length door: " + length_door + ", type: " + typeof (length_door));
        //=>
        acreage = length_room * width_room;
        var p = length_room + width_room;
        var e = length_door * count_door;
        var perimeter = p * 2 - e;

        acreage = parseFloat(acreage.toFixed(1));
        perimeter = parseFloat(perimeter.toFixed(1));

        $('.item-choose').each(function (element) {
            if ($(this).find('.btn-choose').data('material-type') == item_obj.MaterialType) {
                $(this).find('.btn-choose').hide();
                $(this).find('.material-item-selected').hide().show();
                $(this).find('.material-item-selected').data('id', item_obj.Id);
                $(this).find('.material-item-selected').data('name', item_obj.Title);
                $(this).find('.material-item-selected').data('sale-price', item_obj.SalePrice <= 0 || item_obj.SalePrice == item_obj.Price ? item_obj.Price : item_obj.SalePrice);
                $(this).find('.material-item-selected').data('is-rendered', true);
                $(this).find('.material-item-selected .item-selected-avatar').attr("src", "").attr('src', item_obj.Avatar);
                $(this).find('.material-item-selected .item-selected-title').text("").text(item_obj.Title);
                $(this).find('.material-item-selected .item-selected-spec-name').text("").text(item_obj.SpecName);
                $(this).find('.material-item-selected .item-selected-spec-value').text("").text(item_obj.SpecValue);

                var total_price = 0;
                if ($(this).data('unit') == 'm2') {
                    $(this).find('.material-item-selected .item-selected-quantity').val(acreage);
                    total_price = acreage * item_obj.SalePrice;
                }

                if ($(this).data('unit') == 'm') {
                    $(this).find('.material-item-selected .item-selected-quantity').val(perimeter);
                    total_price = perimeter * item_obj.SalePrice;
                }

                var rs_price = total_price.toFixed(0);
                $(this).find('.material-item-selected .item-selected-sale-price').text("").text(R.FormatNumber(rs_price) + ' đ');
                $(this).find('.material-item-selected').data('sale-price', item_obj.SalePrice);

            }
        })

        $("#material-model").modal('hide');
        R.Estimates.CheckedLabor(acreage);
        R.Estimates.FillValueToCalculatedCard(acreage, perimeter);
        R.Estimates.RegisterEvent();
    },
    CalculateSpaceRoom: function () {
        var acreage = 0;
        //var perimeter = 0;
        var length_room = parseFloat(parseFloat($('#length').val() == null ? 0 : $('#length').val()).toFixed(1));
        var width_room = parseFloat(parseFloat($('#width').val() == null ? 0 : $('#width').val()).toFixed(1));
        var count_door = parseFloat(parseFloat($('#count-door').val() == null ? 0 : $('#count-door').val()).toFixed(1));
        var length_door = parseFloat(parseFloat($('#length-door').val() == null ? 0 : $('#length-door').val()).toFixed(1));
        //console.log("length room: " + length_room + ", type: " + typeof (length_room));
        //console.log("width room: " + width_room + ", type: " + typeof (width_room));
        //console.log("count door: " + count_door + ", type: " + typeof (count_door));
        //console.log("length door: " + length_door + ", type: " + typeof (length_door));
        //=>
        acreage = length_room * width_room;
        var p = length_room + width_room;
        var e = length_door * count_door;
        var perimeter = p * 2 - e;
        //alert(acreage);
        //alert(perimeter);
        console.log(1);
        acreage = parseFloat(acreage.toFixed(1));
        perimeter = parseFloat(perimeter.toFixed(1));

        $('#acreage').val(acreage);
        $('#perimeter').val(perimeter);

        $('.room-acreage-woodfloor').text(acreage);
        $('.room-acreage-woodfloor').data('value', acreage);

        $('.room-acreage-styrofoam').text(acreage);
        $('.room-acreage-styrofoam').data('value', acreage);

        $('.room-length-splint').text(e);
        $('.room-length-splint').data('value', e);

        $('.room-length-walling').text(perimeter);
        $('.room-length-walling').data('value', perimeter);
        R.Estimates.CheckedLabor(acreage);
        R.Estimates.FillValueToCalculatedCard(acreage, perimeter);

        //R.Estimates.FillValueToCalculatedCard(acreage, perimeter);
        R.Estimates.RegisterEvent();
    },
    CalculateQuantityInItemChoose: function () {
        var length_room = parseFloat(parseFloat($('#length').val() == null ? 0 : $('#length').val()).toFixed(1));
        var width_room = parseFloat(parseFloat($('#width').val() == null ? 0 : $('#width').val()).toFixed(1));
        var count_door = parseFloat(parseFloat($('#count-door').val() == null ? 0 : $('#count-door').val()).toFixed(1));
        var length_door = parseFloat(parseFloat($('#length-door').val() == null ? 0 : $('#length-door').val()).toFixed(1));
        //console.log("length room: " + length_room + ", type: " + typeof (length_room));
        //console.log("width room: " + width_room + ", type: " + typeof (width_room));
        //console.log("count door: " + count_door + ", type: " + typeof (count_door));
        //console.log("length door: " + length_door + ", type: " + typeof (length_door));
        //=>
        acreage = length_room * width_room;
        var p = length_room + width_room;
        var e = length_door * count_door;
        var perimeter = p * 2 - e;
        var df = 1;
        acreage = parseFloat(acreage.toFixed(1));
        perimeter = parseFloat(perimeter.toFixed(1));
        $('.item-choose').each(function (element) {
            var data_price = $(this).find('.material-item-selected').data('sale-price');
            var el = $(this).find('.item-selected-plus-button');
            if ($(this).data('unit') == 'm2') {
                $(this).find('.material-item-selected .item-selected-quantity').val(acreage);
                R.Estimates.CalculateTotalInItemChoose(el, data_price);
                //total_price = acreage * item_obj.SalePrice;
            }

            if ($(this).data('unit') == 'm') {
                $(this).find('.material-item-selected .item-selected-quantity').val(perimeter);
                R.Estimates.CalculateTotalInItemChoose(el, data_price);
            }
            if ($(this).data('unit') == 'm-door') {
                $(this).find('.material-item-selected .item-selected-quantity').val(e);
                R.Estimates.CalculateTotalInItemChoose(el, data_price);
            }
            if ($(this).data('unit') == 'box') {
                $(this).find('.material-item-selected .item-selected-quantity').val(df);
                R.Estimates.CalculateTotalInItemChoose(el, data_price);
            }
        })
    },
    CheckedLabor: function (acreage) {

        $('.labor').each(function (element) {
            var min = parseFloat($(this).data('min-area'));
            var max = parseFloat($(this).data('max-area'));
            if (acreage >= min && acreage <= max) {
                $('.labor').prop('checked', false);
                $(this).prop('checked', true);
            }

        })
        R.Estimates.RegisterEvent();
    },
    FillValueToCalculatedCard: function (acreage, perimeter) {
        console.log(acreage, perimeter);
        var caled_material = 0;
        var caled_labor = 0;
        $('.item-choose').each(function (element) {
            var q = $(this).find('.item-selected-quantity').val() == '' ? 0 : $(this).find('.item-selected-quantity').val();

            var quantity = parseFloat(q);
            console.log(q, typeof (q));
            //console.log($(this).find('.material-item-selected').data('sale-price'))
            caled_material += $(this).find('.material-item-selected').data('sale-price') * quantity;

        })

        $('.labor').each(function (element) {
            if ($(this).is(":checked") && $(this).data('is-per-metter')==0)
                caled_labor = $(this).data('item-price');
            if ($(this).is(":checked") && $(this).data('is-per-metter') == 1)
                caled_labor = parseInt($(this).data('item-price')) * acreage;
        })
        var caled_total = parseInt(caled_material) + parseInt(caled_labor);
        $('.caled-acreage').text(acreage);
        $('.caled-acreage').data('value', acreage);

        $('.caled-labor').text(R.FormatNumber(parseInt(caled_labor)) + "đ");
        $('.caled-labor').data('value', parseInt(caled_labor));

        $('.caled-material').text(R.FormatNumber(parseInt(caled_material)));
        $('.caled-material').data('value', parseInt(caled_material));

        $('.caled-total').text(R.FormatNumber(caled_total));
        $('.caled-total').data('value', caled_total);
        R.Estimates.RegisterEvent();
    },
    CalculateTotalInItemChoose: function (el, price) {
        var quantity = el.closest('.material-item-selected').find('.item-selected-quantity').val();
        var cal_price = parseInt(parseFloat(price) * parseFloat(quantity));
        el.closest('.material-item-selected').find('.item-selected-sale-price').text("").text(R.FormatNumber(cal_price) + ' đ');
    },
    RenderModalConfirmOrder: function (customer_infomation, extra_infomation, product_infomation) {
        event.preventDefault();
        var modals = $('#modal-don-hang');
        //Xoa toan bo class 
        modals.find('.table-report-item-append').each(function (element) {
            $(this).remove();
        })
        //Fill thong tin nguoi nhan
        modals.find('.customer-name').text(customer_infomation.Name);
        modals.find('.customer-phone').text(customer_infomation.PhoneNumber);
        modals.find('.customer-address').text(customer_infomation.Address);
        //Fill thong tin bo sung
        var ttbs = '';
        extra_infomation.forEach(function (element) {
            ttbs += '<li>' + element + '</li>';
        })
        modals.find('.list_extra_infomation').empty().append(ttbs);

        //Fill thong tin don hang
        //var tthh = '';
        product_infomation.forEach(function (element) {
            console.log(element.Quantity, typeof (element.Quantity))
            $('.table-report-item').last().clone().insertAfter('.table-report-item:last');
            var row = $('.table-report-item').last();
            row.find('.product-id').text(element.ProductId);
            row.find('.product-name').text(element.Name);
            row.find('.product-quantity').text(element.Quantity);
            row.find('.product-price').text(R.FormatNumber(element.LogPrice));
            var tp = element.LogPrice * element.Quantity
            row.find('.product-total-price').text(R.FormatNumber(parseInt(tp)));
            var promo_html = '';
            element.Promotions.forEach(function (p) {
                promo_html += '<li>' + p.LogName + '</li>';
            })
            row.find('.product-promotion-ul').empty().append(promo_html);
            row.css('display', 'table-row');
            row.addClass('table-report-item-append');
        })
        //$('#order-price').data('price', order_price);
        modals.find('.total-payment').html(R.FormatNumber($('.caled-total').data('value')) + 'vnd');
        $('#modal-don-hang').modal('show');

        modals.find('.btn-save').off('click').on('click', function () {
            var orders = {
                OrderCode: "random.org - 1",
                Customer: customer_infomation,
                Products: product_infomation,
                Extras: extra_infomation
            }
            var params = {
                order: JSON.stringify(orders)
            };
            var url = R.Order.culture + '/Order/CreateOrderStringtify';
            $.post(url, params, function (response) {
                alert(response);
            })
        })
        return false;
    },
    PickingProvince: function (locationType, parent) {
        var el_name = "." + locationType.replace('_', '-');
        var params = {
            //(string locationType,string parent
            locationType: locationType,
            parent: parent
        }
        var url = '/Order/GetQuanHuyen'
        $.post(url, params, function (response) {
            var result = JSON.parse(response);
            console.log(result);
            console.log(el_name);
            $(el_name).prop('disabled', false);
            var htm = ''
            result.forEach(function (element) {
                htm += '<option value="' + element.Key + '">' + element.Value.name_with_type + '</option>'
            })
            $(el_name).html('').html(htm);
        })


    },
    RenderEstimatesItemFromLocal: function () {
        var r = localStorage.getItem("arrEstimates");
        console.log(r);
        if (r != null) {
            var _list = JSON.parse(r);
            _list.forEach(function (element) {
                R.Estimates.PickingEstimatesItem(element);
            })

        }
    },
    ReplaceEstimatesFromLocal: function () {

    }
}



$(function () {
    R.Estimates.Init()
})
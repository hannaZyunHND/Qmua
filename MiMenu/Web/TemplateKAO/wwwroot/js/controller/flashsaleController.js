R.FlashSale = {
    Init: function () {
        R.FlashSale.culture = R.Culture();
        R.Extra.BindingExtraToProduct();
        R.FlashSale.CountDownFlashSale('.timer-flash-sale');
        R.FlashSale.RegisterEvent();
    },
    RegisterEvent: function () {
        $(".tab-flash-sale .link-tab-flash-sale").click(function () {
            $(".tab-flash-sale .link-tab-flash-sale").removeClass("active");
            $(this).addClass("active");
            var f_id = $(this).data('id');
            var f_start_time = $(this).data('start-time');
            var f_end_time = $(this).data('end-time');
            var f_status = $(this).data('status');
            R.FlashSale.SwitchFlashSale(f_id, f_start_time, f_end_time, f_status);
        });
        $('.flash-sale-view-more').off('click').on('click', function () {
            var affected_id = 0;
            $(".link-tab-flash-sale").each(function (element) {
                if ($(this).hasClass('active'))
                    affected_id = $(this).data('id');
            })
            var pageIndex = $(this).data('page-index');
            var pageSize = $(this).data('page-size');
            R.FlashSale.ViewMore(affected_id, pageIndex, pageSize);
        })
    },
    SwitchFlashSale: function (id, start_time, end_time, status) {
        //int fSaleId, int? pageIndex, int? pageSize
        var params = {
            fSaleId : id
        }
        var url = "/FlashSale/SwitchFlashSaleActive";
        $.post(url, params, function (response) {
            $('._binding_flashsale').html('').html(response);
            if (status == 1) {
                $('.timer-flash-sale label').text("Bắt đầu trong");
                $('.timer-flash-sale').data('end-time', start_time);
                R.FlashSale.CountDownFlashSale('.timer-flash-sale');
            }
            if (status == 2) {
                $('.timer-flash-sale label').text("Kết thúc trong");
                $('.timer-flash-sale').data('end-time', end_time);
                R.FlashSale.CountDownFlashSale('.timer-flash-sale');
            }
            R.Extra.BindingExtraToProduct();
            $('.flash-sale-view-more').data('page-index', 1);
            R.FlashSale.RegisterEvent();
        })
    },
    CountDownFlashSale: function (el) {
        var end_time = $(el).data('end-time');
        R.CountDown(end_time, el);

    },
    ViewMore: function (id, page_index, page_size) {
        //int fSaleId, int pageIndex, int pageSize
        var params = {
            fSaleId: id,
            pageIndex: page_index+1,
            pageSize: page_size
        }
        var url = "/FlashSale/SwitchFlashSaleActive";
        $.post(url, params, function (response) {
            console.log(response);
            $('._binding_flashsale').append(response);
            $('.flash-sale-view-more').data('page-index', page_index + 1);
            $('.flash-sale-view-more').data('page-size', page_size);
            R.Extra.BindingExtraToProduct();
            R.FlashSale.RegisterEvent();
        })
    }
}

$(function () {
    R.FlashSale.Init();
})
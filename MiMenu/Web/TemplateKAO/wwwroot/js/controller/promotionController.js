R.Promotion = {
    Init: function () {
        R.Promotion.location_id = R.CurrentLocationId();
        R.Promotion.culture = R.Culture();
        R.Promotion.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.paging-data').off('change').on('change', function () {
            var pageIndex = $(this).val();
            window.location.href = R.Promotion.culture + '/promotion.html?pageIndex=' + pageIndex;
        });
        $('.btn-paging-pre').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) - 1;
            window.location.href = R.Promotion.culture + '/promotion.html?pageIndex=' + pIndex;
        });
        $('.btn-paging-next').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) + 1;
            window.location.href = R.Promotion.culture + '/promotion.html?pageIndex=' + pIndex;
        });
    },
}



$(function () {
    R.Promotion.Init()
})
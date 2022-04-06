R.Quotation = {
    Init: function () {
        R.Quotation.location_id = R.CurrentLocationId();
        R.Quotation.culture = R.Culture();
        R.Quotation.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.paging-data').off('change').on('change', function () {
            var pageIndex = $(this).val();
            window.location.href = R.Quotation.culture + '/quotation.html?pageIndex=' + pageIndex;
        });
        $('.btn-paging-pre').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) - 1;
            window.location.href = R.Quotation.culture + '/quotation.html?pageIndex=' + pIndex;
        });
        $('.btn-paging-next').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) + 1;
            window.location.href = R.Quotation.culture + '/quotation.html?pageIndex=' + pIndex;
        });
    },
}



$(function () {
    R.Quotation.Init()
})
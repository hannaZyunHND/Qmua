R.Recruitment = {
    Init: function () {
        R.Recruitment.location_id = R.CurrentLocationId();
        R.Recruitment.culture = R.Culture();
        R.Recruitment.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.paging-data').off('change').on('change', function () {
            var pageIndex = $(this).val();
            window.location.href = R.Recruitment.culture + '/recruitment.html?pageIndex=' + pageIndex;
        });
        $('.btn-paging-pre').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) - 1;
            window.location.href = R.Recruitment.culture + '/recruitment.html?pageIndex=' + pIndex;
        });
        $('.btn-paging-next').off('click').on('click', function () {
            var pageIndex = $('.paging-data').val();
            var pIndex = parseInt(pageIndex) + 1;
            window.location.href = R.Recruitment.culture + '/recruitment.html?pageIndex=' + pIndex;
        });
    },
}



$(function () {
    R.Recruitment.Init()
})
R.CategoriesList1 = {
    Init: function () {
        R.CategoriesList1.culture = R.Culture();
        var selected = $('.select-page').data('page-index');
        //alert(selected);
        $('.select-page').val(selected);
        R.CategoriesList1.RegisterEvent();
    },
    RegisterEvent: function () {

        $('.preview_page').off('click').on('click', function () {
            var zone_id = $(this).data('zone-id');
            var page_index = $(this).data('page-index');
            var to_page = page_index - 1;
            var page_size = $(this).data('page-size');
            var min_page = $(this).data('min-page');
            var type = $(this).data('type');
            var alias = $(this).data('alias')
            if (page_index > min_page) {
                R.CategoriesList1.ViewMore(zone_id, to_page, page_size, type, alias);
            }


        });
        $('.next_page').off('click').on('click', function () {
            var zone_id = $(this).data('zone-id');
            var page_index = $(this).data('page-index');
            var to_page = page_index + 1;
            var page_size = $(this).data('page-size');
            var max_page = $(this).data('max-page');
            var type = $(this).data('type');
            var alias = $(this).data('alias');
            if (page_index < max_page) {
                R.CategoriesList1.ViewMore(zone_id, to_page, page_size, type, alias);
            }


        });
        $('.select-page').off('change').on('change', function () {
            var zone_id = $(this).data('zone-id');
            var to_page = $(this).val();
            var page_size = $(this).data('page-size');
            var type = $(this).data('type');
            var alias = $(this).data('alias');
            R.CategoriesList1.ViewMore(zone_id, to_page, page_size, type, alias);
        })

    },
    ViewMore: function (zone_id, to_page, page_size, type, alias) {
        //var url = R.BlogList2.culture + "/Blog/MoreBlogs";
        ////int zone_id, int type, string filter, int page_index, int page_size
        //var params = {
        //    zone_id: zone_id,
        //    type: type,
        //    filter: "",
        //    page_index: to_page,
        //    page_size: page_size
        //}
        //$.post(url, params, function (response) {
        //    console.log(response);
        //    $('.binding-blog').html(response);
        //    $('.preview_page').data('page-index', to_page);
        //    $('.next_page').data('page-index', to_page);
        //    $('.select-page').val(to_page);
        //})
        var url = R.CategoriesList1.culture + "/" + alias + ".dc" + zone_id + ".html";
        var queryStirng = "?pageIndex=" + to_page;
        window.location.href = url + queryStirng;
    }

}

$(function () {
    R.CategoriesList1.Init();
})
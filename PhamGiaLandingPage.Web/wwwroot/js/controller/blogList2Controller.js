R.BlogList2 = {
    Init: function () {
        R.BlogList2.culture = R.Culture();
        R.Extra.PagePaging(false);
        //var selected = $('.select-page').data('page-index');
        ////alert(selected);
        //$('.select-page').val(selected);
        R.BlogList2.RegisterEvent();
        R.BlogList2.SetActive();
    },
    RegisterEvent: function () {

        //$('.preview_page').off('click').on('click', function () {
        //    var zone_id = $(this).data('zone-id');
        //    var page_index = $(this).data('page-index');
        //    var to_page = page_index - 1;
        //    var page_size = $(this).data('page-size');
        //    var min_page = $(this).data('min-page');
        //    var type = $(this).data('type');
        //    var alias = $(this).data('alias')
        //    if (page_index > min_page) {
        //        R.BlogList2.ViewMore(zone_id, to_page, page_size, type,alias);
        //    }


        //});
        //$('.next_page').off('click').on('click', function () {
        //    var zone_id = $(this).data('zone-id');
        //    var page_index = $(this).data('page-index');
        //    var to_page = page_index + 1;
        //    var page_size = $(this).data('page-size');
        //    var max_page = $(this).data('max-page');
        //    var type = $(this).data('type');
        //    var alias = $(this).data('alias');
        //    if (page_index < max_page) {
        //        R.BlogList2.ViewMore(zone_id, to_page, page_size, type,alias);
        //    }


        //});
        $('.select-page').off('change').on('change', function () {
            var url = $(this).val();
            window.location.href = url;
        })

    },
    ViewMore : function (zone_id, to_page, page_size, type, alias) {
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
        var url = R.BlogList2.culture + "/" + alias + ".b" + zone_id + ".html";
        var queryStirng = "?pageIndex=" + to_page;
        window.location.href = url + queryStirng;
    },
    SetActive: function () {
        var _a = $('.blog__btn');
        _a.each(function (element) {
            
            var path = window.location.pathname.split("/")[1];
            //console.log(path)
            if ($(this).attr("href") == "/"+path) {
                $(this).addClass("uk-active");
            }
        })
    }

}

$(function () {
    R.BlogList2.Init();
})
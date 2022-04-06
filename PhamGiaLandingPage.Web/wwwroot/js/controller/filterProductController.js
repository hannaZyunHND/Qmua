R.FilterProduct = {
    Init: function () {
        
        R.FilterProduct.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.region-view-more').off('click').on('click', function () {
            var alias = $(this).data('url');
            var to_page = $(this).data('index') + 1;
            R.FilterProduct.ViewMore(alias, to_page);
        })

    },
    ViewMore: function (alias, to_page) {
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
        var controller = "/FilterProduct/FilterProductByRegion";
        var params = {
            region: alias,
            pageIndex: to_page
        }
        $.post(controller, params, function (response) {
            console.log(response);
            $('._binding_product').append(response);
            $('.region-view-more').data('index', to_page);
            R.Extra.BindingExtraToProduct();
        })
        //var url = R.CategoriesList1.culture + "/" + alias + ".dc" + zone_id + ".html";
        //var queryStirng = "?pageIndex=" + to_page;
        //window.location.href = url + queryStirng;
    }

}

$(function () {
    R.FilterProduct.Init();
})
R.BlogDetail = {
    Init: function() {
        R.BlogDetail.location_id = R.CurrentLocationId();
        R.BlogDetail.culture = R.Culture();
        R.BlogDetail.LoadListProduct();
        R.BlogDetail.AddViewCountArticle();
        R.BlogDetail.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.toc_title').off('click').on('click', function () {
            $('.toc_list').toggle();
        })
        $('.form-thu-thap').off('submit').on('submit', function () {
            var current_url = window.location.href;
            var name = $('._lien_he_name').val();
            var phone = $('._lien_he_phone').val();
            var add = $('._lien_he_email').val();
            //var booking = $('#txt-time-input').val();
            //var booking_time = moment(booking, 'DD/MM/YYYY').format('YYYY-MM-DD');
            var yc = "Liên hệ dự án TemplateKAO";
            var nd = "";
            //var nd = $('._lien_he_nd').val();
            nd += "Thông tin: " + $('.select-thu-thap').val();
            nd += "Ghi chú: " + $('._lien_he_ghichu').val();
            var params = {
                Name: name,
                Phone: phone,
                Address: add,
                Title: yc,
                Content: nd,
                Type: 3,
                Source: 'web',
                
            }
            R.BlogDetail.SendContact(params);
            return false;
        })

    },
    LoadListProduct: function() {

        $('product').each(function(element) {
            var el = $(this);
            var product_list = $(this).data('id-list');
            var params = {
                product_ids: product_list,
                location_id: R.BlogDetail.location_id
            }
            var url = R.BlogDetail.culture + "/Blog/ProductsInArticle";
            $.post(url, params, function(response) {
                console.log(response);
                el.replaceWith(response);
                R.Extra.BindingExtraToProduct();
                R.BlogDetail.RegisterEvent();
            })
        })
    },
    AddViewCountArticle: function() {

        var id_san_pham = $('.detail-container').data('id');
        var url = "/Extra/CreateViewCount";
        var params = {
            objectId: id_san_pham,
            type: 'article'
        }
        $.post(url, params, function(response) {
            console.log(response);
        })

    },
    SendContact: function (params) {
        var url = "/Extra/CreateServiceTicket";
        $.post(url, params, function (response) {
            console.log(response);
            alert("Cám ơn! Chúng tôi sẽ liên lạc với bạn trong thời gian sớm nhất")
            // $('#modal-xn').modal('show');
            // R.Contact.CloseModalAndClearText();
            return false;
        })
    }
}



$(function() {
    R.BlogDetail.Init()
})

R.Contact = {
    Init: function () {
        R.Contact.RegisterEvent();
    },
    RegisterEvent: function () {
        $('._picking_location').off('click').on('click', function () {
            console.log(1);
            var loc_id = $(this).data('id');
            var _post = $(this).data('position');
            //Dong tat ca cac binding
            $('._binding_departments').hide();
            //Lam border
            $('._picking_location').find('.cust-tinh').css('border-bottom', '');
            //$('._picking_location').find('.tinht').css('color', '');
            $(this).find('.cust-tinh').css('border-bottom', '1px solid orange');
            //$(this).find('.tinht').css('color', 'orange');
            R.Contact.ShowDepartment(loc_id, _post)
        })
        $('#form-lien-he').off('submit').on('submit', function () {
            var current_url = window.location.href;
            var name = $('._lien_he_name').val();
            var phone = $('._lien_he_phone').val();
            var add = $('._lien_he_email').val();
            //var booking = $('#txt-time-input').val();
            //var booking_time = moment(booking, 'DD/MM/YYYY').format('YYYY-MM-DD');
            var yc = "Liên hệ TemplateKAO";
            var nd = "";
            var nd = $('._lien_he_nd').val() === undefined ? "" : $('._lien_he_nd').val();
            //nd += "Lĩnh vực: " + $('._lien_he_linhvuc').val();
            //nd += "Ngân sách dự kiến: " + $('._lien_he_ngansach').val();
            var params = {
                Name: name,
                Phone: phone,
                Address: add,
                Title: yc,
                Content: nd,
                Type: 3,
                Source: 'web',
                
            }
            R.Contact.SendContact(params);
            return false;
        })
    },
    ShowDepartment: function (id, post) {
        var url = "/Contact/GetDepartments"
        var params = {
            loc_id: id
        }
        $.post(url, params, function (response) {
            $('._binding_departments').each(function (element) {
                if ($(this).data('position') == post) {
                    $(this).css('display', 'block');
                    $(this).html('').html(response);
                }
            })
        })
    },
    SendContact: function (params) {
        var url = "/Extra/CreateServiceTicket";
        $.post(url, params, function (response) {
            console.log(response);
            alert("Cám ơn! Chúng tôi sẽ liên lạc với bạn trong thời gian sớm nhất")
            return false;
        })
    },
    CloseModalAndClearText: function () {
        $('._lien_he_name').val('');
        $('._lien_he_phone').val('');
        $('._lien_he_email').val('');
        $('._lien_he_nd').val('');
    }
}



$(function () {
    R.Contact.Init()
})
R.PreMenu = {
    Init: function () {
        R.PreMenu.LoadSessionObject();
        R.PreMenu.culture = R.Culture();
        R.PreMenu.RegisterEvent();
    },
    RegisterEvent: function () {
        $('.them_san_pham').off('click').on('click', function () {
            var ten_sp = $(this).data('ten');
            var gia_sp = $(this).data('giasp');
            var id_sp = $(this).data('idsp');
            var sanphamObj = {
                Ten: ten_sp,
                Gia: gia_sp,
                Id: id_sp
            }
            R.PreMenu.HienThiGioHang(sanphamObj);
        })
        $('.cong_them_sp').off('click').on('click', function () {
            var x = parseInt($(this).closest('.isClone').find('.so_luong').text());
            $(this).closest('.isClone').find('.so_luong').text(x + 1);
            R.PreMenu.TinhGiaTriOrder();
        })
        $('.tru_di_sp').off('click').on('click', function () {
            var x = parseInt($(this).closest('.isClone').find('.so_luong').text());
            if (x > 1) {
                $(this).closest('.isClone').find('.so_luong').text(x - 1);

            }
            if (x == 1) {
                var cloned = $(this).closest('.isClone');
                R.PreMenu.XoaSanPham(cloned);
            }
            R.PreMenu.TinhGiaTriOrder();

        })
        $('.xoa_sp').off('click').on('click', function () {
            var cloned = $(this).closest('.isClone');
            R.PreMenu.XoaSanPham(cloned);
            R.PreMenu.TinhGiaTriOrder();
        })
        $('.ap_dung_ma_khuyen_mai').off('click').on('click', function () {
            R.PreMenu.InsertMaKhuyenMai();
            R.PreMenu.TinhGiaTriOrder();
            R.PreMenu.RegisterEvent();
        })
        $('.form-dat-hang').off('submit').on('submit', function () {
            R.PreMenu.ConfirmDatHang();
            return false;
        })
        $('.tim-kiem-san-pham').keydown(function(e){
            var data = [];
            $('.them_san_pham').each(function(element){
                data.push({
                    TenSanPham: $(this).data('ten'),
                    IdDOM: $(this).closest('.container-san-pham').find('.container-ten-san-pham').attr('id')
                })
            })
            $(this).autocomplete({
                delay: 300,
                source: function(request, response){
                    var dt = [];
                    data.forEach(function(i,v){
                        
                        if(i.TenSanPham.toLowerCase().includes(request.term.toLowerCase())) dt.push(i);
                        response(dt);
                    })
                    console.log(dt);
                },
                minlength: 1,
                select: function(event, ui){
                    console.log(ui)
                }
                
            })
            .autocomplete( "instance" )._renderItem = function (ul, item) {
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a href=\"#"+item.IdDOM+"\">" + item.TenSanPham + "</a>")
                    .appendTo(ul);
            };
            // .autocomplete("instance")._renderItem = function(div, item){
            //     console.log(item);
            // }
        })
    },
    XoaSanPham: function (element) {
        element.remove();
        R.PreMenu.RegisterEvent();
    },
    HienThiGioHang: function (sanphamObj) {
        var checker = R.PreMenu.KiemTraTrungDuLieu(sanphamObj);
        // alert(checker);
        if (!checker) {
            var root = $('.root');
            var cloned = root.last().clone();

            cloned.removeClass('root').addClass('isClone');

            cloned.find('.ten_sp').text(sanphamObj.Ten);
            cloned.find('.gia_sp').text(R.FormatNumber(sanphamObj.Gia) + " đ");
            cloned.find('.gia_sp').attr('gia', sanphamObj.Gia);
            cloned.attr('sp_id', sanphamObj.Id);
            // cloned.prop('id', sanphamObj.Id);
            //$(this).find('.so_luong').text();
            cloned.find('.so_luong').text(sanphamObj.SoLuong);
            cloned.css('display', '');

            root.closest('tbody').prepend(cloned);
            R.PreMenu.RegisterEvent();
        }
        R.PreMenu.TinhGiaTriOrder();
    },
    KiemTraTrungDuLieu: function (sanphamObj) {
        var id = sanphamObj.Id;
        var flag = false;
        // alert(id);
        $('.isClone').each(function (element) {
            var x = parseInt($(this).attr('sp_id'));
            // alert(x);
            if (x == sanphamObj.Id) {
                // alert(id);
                var sl = parseInt($(this).find('.so_luong').text());
                console.log(sl);
                $(this).find('.so_luong').text(sl + 1);
                R.PreMenu.RegisterEvent();
                flag = true;
            }
        })
        return flag;
    },
    TinhGiaTriOrder: function () {
        var so_mon = 0;
        var tien_gachngang = 0;
        var tien_giam_1 = 0;
        var gtKm = parseInt($('.so_tien_giam_gia').data('gia_tri_km') == null ? "0" : $('.so_tien_giam_gia').data('gia_tri_km'));
        var optionKm = parseInt($('.so_tien_giam_gia').data('option_khuyen_mai') == null ? "0" : $('.so_tien_giam_gia').data('option_khuyen_mai'));
        var tien_tt = 0;
        $('.isClone').each(function (element) {
            var sl = parseInt($(this).find('.so_luong').text());
            var gia = $(this).find('.gia_sp').attr('gia');
            so_mon += sl;
            tien_tt += gia * sl
        })
        tien_gachngang = tien_tt;
        if (optionKm == 1)
            tien_giam_1 = tien_tt * gtKm / 100;
        if (optionKm != 1)
            tien_giam_1 = gtKm;
        tien_tt = tien_tt - tien_giam_1;
        $('.dh_so_mon span').text(so_mon);
        $('.dh_tien_gachngang del').text(R.FormatNumber(tien_gachngang) + " đ");
        $('.dh_tien_tt').text(R.FormatNumber(tien_tt) + " đ");
        R.PreMenu.SaveSessionObject();
        R.PreMenu.RegisterEvent();
    },
    SaveSessionWithHTML: function () {
        var innerHtmlGioHang = $('.html-gio-hang').html();
        sessionStorage.setItem("htmlGioHang", innerHtmlGioHang);
    },
    KiemTraSessionHTML: function () {
        var ss = sessionStorage.getItem("htmlGioHang");
        if (ss != null) {
            console.log(ss);
        } else {
            console.log('Khong co session');
        }
    },
    LoadHtmlSession: function () {
        var ss = sessionStorage.getItem("htmlGioHang");
        if (ss != null) {
            $('.html-gio-hang').html('').html(ss);
        } else {
            console.log('Khong co session');
        }
    },
    SaveSessionObject: function () {
        var result = [];
        $('.isClone').each(function (e) {
            var ten = $(this).find('.ten_sp').text();
            var gia_sp = $(this).find('.gia_sp').attr('gia');
            var id_sp = $(this).attr('sp_id');
            var so_luong = $(this).find('.so_luong').text();
            var p = {
                Ten: ten,
                Gia: gia_sp,
                Id: id_sp,
                SoLuong: so_luong
            }
            result.push(p);
        })

        //Save phan voucher



        sessionStorage.setItem("gioHangObject", JSON.stringify(result));
    },
    InsertMaKhuyenMai: function () {
        //var df_ma = ["VANPHONGVUI", "MIGROUP"];
        //var df_gia_tri_km = 20000;
        var url = "/Order/GetCouPonByCode";
        var ma_inputed = $('.ma_khuyen_mai').val();
        if (ma_inputed != "") {
            url += "?code=" + ma_inputed;
            console.log(url)
            $.get(url, function (response) {
                console.log(response)
                if (response != null) {
                    var df_ma = [];
                    df_ma.push({
                        Ma: response.name,
                        GiaTri: response.valueDiscount,
                        Option: response.discountOption
                    })
                    var df_ma_choosen = df_ma.filter((e) => { if (e.Ma == ma_inputed) return e; })[0];
                    if (df_ma_choosen != null) {
                        R.PreMenu.LoadMaKhuyenMai(df_ma_choosen);
                        sessionStorage.setItem("ma_khuyen_mai", JSON.stringify(df_ma_choosen));
                    }
                    R.PreMenu.TinhGiaTriOrder();
                    R.PreMenu.RegisterEvent();
                }

            })
        }

    },
    LoadMaKhuyenMai: function (df_ma_choosen) {
        $('.tb_giam_gia').css('display', '');
        var op = df_ma_choosen.Option;
        var quantity = "";
        if (op == 1)
            quantity = "%";
        else
            quantity = "đ"
        $('.so_tien_giam_gia').text(R.FormatNumber(df_ma_choosen.GiaTri) + " " + quantity);
        $('.so_tien_giam_gia').data('gia_tri_km', df_ma_choosen.GiaTri);
        $('.so_tien_giam_gia').data('option_khuyen_mai', df_ma_choosen.Option);
        $('.ma_khuyen_mai').val(df_ma_choosen.Ma);
        R.PreMenu.TinhGiaTriOrder();
        R.PreMenu.RegisterEvent()
    },
    LoadSessionObject: function () {
        var r = sessionStorage.getItem("gioHangObject");
        if (r != null) {
            var rs = JSON.parse(r);
            //console.log(rs);
            //Nghiep vu in ra 
            rs.forEach(function (e) {
                R.PreMenu.HienThiGioHang(e);
            })
        } else {
            console.log('Khong co san pham');
        }
        var m = sessionStorage.getItem("ma_khuyen_mai");
        if (m != null) {
            var mkm = JSON.parse(m);
            R.PreMenu.LoadMaKhuyenMai(mkm);
        } else {
            console.log('Khong co ma khuyen mai')
        }
    },
    ConfirmDatHang: function () {
        var isNgoiTaiQuan = $('input[name=radio2]:checked').val();
        console.log('isNgoiTaiQuan', isNgoiTaiQuan);
        var soBan = $('.so-ban').val();
        var diachi_giaohang = $('.dia-chi-giao-hang').val();
        var nguoi_nhan = $('.nguoi-nhan').val();
        var dien_thaoi = $('.dien-thoai').val();
        var e_mail = $('.e-mail').val();
        var ghi_chu = $('.ghi-chu').val();
        var hh_thanhtoan = $('input[name=radio1]:checked').parent().find('.order__boxForm__label2').val();
        var thoi_gian_giao = $('.thoi-gian-giao').val().replace("T"," ");
        console.log(thoi_gian_giao);

        //Get san pham
        var sp = [];
        if (sessionStorage.getItem('gioHangObject') != null) {
            var xxx = sessionStorage.getItem('gioHangObject');
            sp = JSON.parse(xxx);
        }
        //GetVoucher
        var vc = "";
        var gt_vc = 0;
        var voucersession = sessionStorage.getItem("ma_khuyen_mai");
        if (voucersession != null) {
            vc = JSON.parse(voucersession).Ma;
            var optionVoucher = JSON.parse(voucersession).Option;
            if (optionVoucher == 1) {
                gt_vc = JSON.parse(voucersession).GiaTri;
            }
            gt_vc = JSON.parse(voucersession).GiaTri;
        }
        //Build ViewModel
        var OrderCode = "";
        if (parseInt(isNgoiTaiQuan) == 1) OrderCode = "Ngồi tại quán";
        if (parseInt(isNgoiTaiQuan) == 0) OrderCode = "Ship đi";
        var Customer = {
            Id: 0,
            Name: nguoi_nhan,
            PhoneNumber: dien_thaoi,
            Address: diachi_giaohang,
            Note: "",
            Gender: ""
        }
        var SanPham = [];
        var tongTien = 0;
        sp.forEach(function (e) {
            var p = {
                ProductId: e.Id,
                Name: e.Ten,
                LogPrice: e.Gia,
                Quantity: e.SoLuong,
                OrderSourceType: 0,
                OrderSourceId: 0,
                Voucher: "",
                VoucherPrice: 0,
                VoucherType: 0
            }
            SanPham.push(p);
            tongTien += parseInt(e.Gia) * parseInt(e.SoLuong);
        })
        //GetVoucher
        var vc = "";
        var gt_vc = 0;
        var voucersession = sessionStorage.getItem("ma_khuyen_mai");
        if (voucersession != null) {
            vc = JSON.parse(voucersession).Ma;
            var optionVoucher = JSON.parse(voucersession).Option;
            if (optionVoucher == 1) {
                gt_vc = tongTien * JSON.parse(voucersession).GiaTri/ 100;
            }
            else {
                gt_vc = JSON.parse(voucersession).GiaTri;
            }
            //gt_vc = JSON.parse(voucersession).GiaTri;
        }
        var MetaData = {
            IsNgoiTaiQuan: parseInt(isNgoiTaiQuan) || 0,
            SoBan: soBan || "",
            GhiChu: ghi_chu || "",
            HHThanhToan: hh_thanhtoan || "",
            ThoiGianGiao: thoi_gian_giao || "",
            MaVoucher: vc,
            GiaTriVoucher: gt_vc,
            TongTien: tongTien - gt_vc
        }
        var mailBody = "";
        mailBody += "<ul>";

        mailBody += "<li>Loại: " + OrderCode + "</li>"
        if (MetaData.IsNgoiTaiQuan == 1)
            mailBody += "<li> Số bàn: " + MetaData.SoBan + "</li>"
        else {

            mailBody += "<li>";
            mailBody += "<ul>";
            mailBody += "<li>KH: " + Customer.Name + "</li>";
            mailBody += "<li>Phone: " + Customer.PhoneNumber + "</li>";
            mailBody += "<li>Địa chỉ: " + Customer.Address + "</li>";
            mailBody += "</ul>";
            mailBody += "</li>";
        }
        mailBody += "<li>Sản phẩm: </li>";
        mailBody += "<li>";
        mailBody += "<ul>";
        SanPham.forEach(function (e) {
            mailBody += "<li>Sản phẩm: " + e.Name + "</li>";
            mailBody += "<li>Số lượng: " + e.Quantity + "</li>";

        })
        mailBody += "</ul>";
        mailBody += "</li>";
        if (vc != "") {
            mailBody += "<li>Voucher: " + vc + " (Giá trị: " + R.FormatNumber(gt_vc) + " )</li>";

        }

        mailBody += "<li>Tổng tiền: " + R.FormatNumber(MetaData.TongTien) + "</li>";
        mailBody += "</ul>";
        var parameters = {
            OrderCode: OrderCode,
            Customer: Customer,
            Products: SanPham,
            Extras: JSON.stringify(MetaData),
            MailBody: mailBody
        }
        console.log(parameters);
        

        //Lưu vào  databse
        var url = '/Order/CreateOrder';
        $.post(url, parameters, function (response) {
            //alert(response);
            //console.log(response);
            //$('#modal-success').modal('show');
            UIkit.modal("#modal-success").show();
            
            //alert("Tạo đơn hàng thành công");
            return false;
        })
        return false;

    },

    Test: function () {
        var a = 100000;
        alert(a);
        alert(R.FormatNumber(a));

    }
}
R.PreMenu.Init();

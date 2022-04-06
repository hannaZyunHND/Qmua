R.Extra = {
    Init: function () {
        R.Extra.RegisterEvent();
        R.Extra.culture = R.Culture();
        R.Extra.GetPropertyDetails();
        R.Extra.BindingExtraToProduct();
        R.Extra.GenerateAvatarCircle();
        R.Extra.CommentPaging();
        R.Extra.PagePaging();
        R.Extra.Url = "https://cms.janhome.vn";
        R.Extra.BeforePaging = "";
        //R.Extra.GenerateAvatarCircle();
    },
    RegisterEvent: function () {
        $('#comment-form').off('submit').on('submit', function () {

            var type = $(this).data('type');
            var object_id = $(this).data('object-id');
            var object_type = $(this).data('object-type');
            var name = $(this).find('.txt_name').val();
            var phoneOrEmail = $(this).find('.txt_phoneOrEmail').val();
            var avatar = $(this).find('.input-choose-file').data('target-url');
            var content = $(this).find('.txt_content').val();
            var rate = 0;
            //uploadfile
            var fileUpload = $(this).find(".input-choose-file").get(0);
            var files = fileUpload.files;
            var params = {
                objectId: object_id,
                objectType: object_type,
                name: name,
                phoneOrEmail: phoneOrEmail,
                avatar: avatar,
                content: content,
                type: type,
                rate: rate
            }
            R.Extra.CreateComment(files, params);
            return false;
        });
        $(".star-rating i").off('click').on('click', function () {
            $(this).parent().find(".star-rating i").removeClass("checked");
            $(this).addClass("checked");
            $(this).prevAll().addClass("checked");
            $(this).nextAll().removeClass("checked");
            var type = $(this).closest('.star-rating').data('type');
            var object_id = $(this).closest('.star-rating').data('id');
            var el = $(this).parent();
            //var el = $(this).parent();
            //Dem so phan tu co class active
            R.Extra.Rating(el, object_id, type);
        });
        $('.reply_comment').off('click').on('click', function () {
            var el = $(this);
            var id = $(this).closest('.comment_infomation').data('id');
            var obj_id = $(this).closest('.comment_infomation').data('object-id');
            var obj_type = $(this).closest('.comment_infomation').data('object-type');
            $('._comment_reply').each(function (element) {
                $(this).html('');
            })
            R.Extra.RenderReplyComment(el, id, obj_id, obj_type);

        });
        $('.send-comment-reply').off('click').on('click', function () {
            var parent_comment = $(this).data('parent')
            var obj_id = $(this).data('object-id');
            var obj_type = $(this).data('object-type');
            var type = $(this).data('type');
            var lang = $(this).data('lang');
            var name = $(this).closest('.reply').find('.txt_name').val();
            var phoneOrEmail = $(this).closest('.reply').find('.txt_phoneOrEmail').val();
            var avatar = $(this).closest('.reply').find('.input-choose-file').data('target-url');
            var content = $(this).closest('.reply').find('.txt_content').val();
            var rate = 0;
            //uploadfile
            var fileUpload = $(this).closest('.reply').find(".input-choose-file").get(0);
            var files = fileUpload.files;
            var params = {
                objectId: obj_id,
                objectType: obj_type,
                name: name,
                phoneOrEmail: phoneOrEmail,
                avatar: avatar,
                content: content,
                type: type,
                rate: rate,
                parentId: parent_comment
            }
            R.Extra.CreateComment(files, params);
        });
        $('.input-choose-file').off('change').on('change', function () {
            var parent_el = $(this).closest('.upload-parent');
            readURL(this, parent_el);
        });
        $('._place-img-close').off('click').on('click', function () {
            $(this).parent().parent().find('._place-img').attr("src", "");
            $(this).parent().parent().hide();
            $('.input-choose-file').val('');
        })
        $('.btn-email-collection').off('click').on('click', function () {
            //alert('.email-collection');
            //
            var current_url = window.location.href;
            var name = $('.email-collection').val();
            var phone = 'No Infomation';
            var add = 'No Infomation';
            //var booking = $('#txt-time-input').val();
            //var booking_time = moment(booking, 'DD/MM/YYYY').format('YYYY-MM-DD');
            var yc = "Liên hệ Janhome";
            var nd = 'No Infomation';
            var params = {
                Name: name,
                Phone: phone,
                Address: add,
                Title: yc,
                Content: nd,
                Type: 3,
                Source: 'web',

            }
            R.Extra.SendContact(params);
            return false;
        })
    },
    SendContact: function (params) {
        var url = "/Extra/CreateServiceTicket";
        $.post(url, params, function (response) {
            //console.log(response);
            $('#modal-xn').modal('show');
            R.Contact.CloseModalAndClearText();
            return false;
        })
    },
    GetPropertyDetails: function () {
        //Kiem tra session co chua
        var extraSession = sessionStorage.getItem("properties");
        if (extraSession == null) {
            var url = R.Extra.culture + "/Extra/GetPropertyDetails"
            $.post(url, null, function (response) {
                sessionStorage.setItem("properties", response);
            })
        }
    },
    BindingExtraToProduct: function () {

        $('.item-product').each(function (element) {
            var properties = $(this).data('properties');
            if (properties !== undefined && properties != "") {
                var p = properties.toString();
                var el = $(this);
                if (p != "") {
                    var list_property = p.split(',');
                    BindExtraTop($(this), list_property);

                }
            }
            else {
                $(this).find('.tag-1').hide();
            }
        })

        $('.product-detail').each(function (element) {
            var properties = $(this).data('properties');
            ////console.log(properties, typeof (properties))
            if (properties !== undefined && properties != "") {
                var p = properties.toString();
                var el = $(this);
                if (p != "") {
                    var list_property = p.split(',');
                    BindExtraTop($(this), list_property);

                }
            }
            else {
                $(this).find('.tag').hide();
            }
        })
    },
    BindingExtraToProductInElement: function (el) {
        $(el).find('.item-product').each(function (element) {
            var properties = $(this).data('properties');
            if (properties !== undefined) {
                var list_property = [];
                if (!properties || typeof properties === "string" && properties.includes(',') == true)
                    list_property = properties.split(',');
                if (!properties || typeof properties === "string" && properties.includes(',') == false && properties != null)
                    list_property.push(properties)
                BindExtraTop($(this), list_property);
            }


        })
    },
    CreateComment: function (files, params) {
        ////console.log(files);
        ////console.log(params);
        //Kiem tra files
        if (files != null) {
            var dataFile = new FormData();
            for (var i = 0; i < files.length; i++) {
                dataFile.append(files[i].name, files[i]);
            }
            $.ajax({
                url: R.Extra.Url + "/api/UploadFile/UploadImage",
                type: "POST",
                data: dataFile,
                contentType: false,
                processData: false,
                success: function (result) {
                    //console.log(result);
                    params.avatar = result.linkImage;
                    var url = R.Extra.culture + "/Extra/CreateComment";
                    var send_mail = R.Extra.culture + "/Extra/SendMail";
                    var params_mail = {
                        type: "comment",
                        body: params.content
                    }
                    $.post(url, params, function (response) {
                        //$('#modal-xn').modal('show');
                        $.post(send_mail, params_mail, function (response) {
                            R.Extra.CloseModalAndClearText();
                            return false;
                        })
                        
                    })
                },
                error: function (err) {
                    alert(err.statusText)
                }
            });
        }
        else {
            var url = R.Extra.culture + "/Extra/CreateComment";
            $.post(url, params, function (response) {
                $('#modal-xn').modal('show');
                R.Extra.CloseModalAndClearText();
                return false;
            })
        }
    },
    CloseModalAndClearText: function () {
        $('.txt_name').val('');
        $('.txt_phoneOrEmail').val('');
        $('.txt_content').val('');
        $('.modal-danh-gia').modal(hide);
        $('._comment_reply').each(function (element) {
            $(this).html('');
        })
    },
    Rating: function (el, id, type) {
        ////console.log(el);
        //Dem so sao
        var count = 0;
        el.find('.checked').each(function (element) {
            count++;
        });
        ////console.log("dem sao " + count);
        var url = R.Extra.culture + "/Extra/CreateRating";
        //int objectId, int objectType, int rate
        var params = {
            objectId: id,
            objectType: type,
            rate: count
        }
        $.post(url, params, function (response) {
            //console.log("khoi tao rate " + response);
            //console.log(el);
            el.find('i').off('click');
        })
    },
    GenerateAvatarCircle: function () {
        $('.avatar').each(function (element) {
            var name = $(this).data('name');
            //console.log(name, typeof (name));
            if (name != null && name.indexOf(" ") > -1) {
                var cutted_name = name.split(" ");
                var first = "";
                var second = "";
                first = cutted_name[0].charAt(0);
                if (cutted_name.length >= 2) {
                    second = cutted_name[1].charAt(0);
                }
                var avt = $(this).text(first + " " + second);
            }
            else {
                var first = name.charAt(0);
                var avt = $(this).text(first);
            }


        })
    },
    RenderReplyComment: function (el, id, obj_id, obj_type) {
        var url = R.Extra.culture + "/Extra/GetReplyComment";
        var params = {
            id: id,
            obj_id: obj_id,
            obj_type: obj_type
        }
        $.post(url, params, function (response) {
            el.closest('.media-body').find('._comment_reply').first().html('').html(response);
            R.Extra.RegisterEvent();
        })
    },
    CommentPaging: function () {
        if ($('.comment-paging').length > 0) {
            var total = parseInt($('.comment-paging').data('total'));
            var size = parseInt($('.comment-paging').data('size'));
            var obj_id = parseInt($('.comment-paging').data('object-id'));
            var obj_type = parseInt($('.comment-paging').data('object-type'));
            var arr = [];
            for (var i = 0; i < total; i++) {
                arr.push(i);
            }
            $('.comment-paging').pagination({
                dataSource: arr,
                pageSize: size,
                showPrevious: false,
                showNext: false,
                callback: function (data, pagination) {
                    //console.log(pagination.pageNumber);
                    var page = pagination.pageNumber;
                    var url = R.Extra.culture + "/Extra/GetCommentList";
                    var params = {
                        //int object_id, int object_type, int? pageIndex
                        object_id: obj_id,
                        object_type: obj_type,
                        pageIndex: page
                    }
                    $.post(url, params, function (response) {
                        $('.comment_binding').html('').html(response);
                        R.Extra.GenerateAvatarCircle();
                        R.Extra.RegisterEvent();
                    })

                }
            })
        }
    },
    PagePaging: function (isAjax, url, params, binding) {
        if (isAjax == false) {
            if ($('.pagin').length > 0) {
                var total = parseInt($('.paging-container').data('total'));
                var size = parseInt($('.paging-container').data('size'));
                var current = parseInt($('.paging-container').data('currentpage'));
                var arr = [];
                for (var i = 0; i < total; i++) {
                    arr.push(i);
                }
                $('.pagin').pagination({
                    dataSource: arr,
                    pageSize: size,
                    showPrevious: true,
                    showNext: true,
                    pageNumber: current,
                    afterPageOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        var url = window.location.pathname;
                        var a = url.split('/');
                        R.Extra.BeforePaging = "/" + a[1];
                        url = R.Extra.BeforePaging + '/page/' + page;
                        //console.log(url);
                        window.location.href = url;
                    },
                    afterPreviousOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        
                        var url = window.location.pathname;
                        var a = url.split('/');
                        R.Extra.BeforePaging = "/" + a[1];
                        url = R.Extra.BeforePaging + '/page/' + page;
                        //console.log(url);
                        window.location.href = url;
                    },
                    afterNextOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        var url = window.location.pathname;
                        var a = url.split('/');
                        R.Extra.BeforePaging = "/" + a[1];
                        url = R.Extra.BeforePaging + '/page/' + page;
                        //console.log(url);
                        window.location.href = url;
                    },
                    callback: function (data, pagination) {
                        $('.pagin').find('.paginationjs').first().addClass('number-page');
                    }
                })
            }
        }
        if (isAjax == true) {
            if ($('.pagin').length > 0) {
                var total = parseInt($('.paging-container').data('total'));
                var size = parseInt($('.paging-container').data('size'));
                var current = parseInt($('.paging-container').data('currentpage'));
                var arr = [];
                for (var i = 0; i < total; i++) {
                    arr.push(i);
                }
                $('.pagin').pagination({
                    dataSource: arr,
                    pageSize: size,
                    showPrevious: true,
                    showNext: true,
                    pageNumber: current,
                    afterPageOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        params.pageNumber = parseInt(page)
                        $.post(url, params, function (response) {
                            console.log(response);
                            binding.html('').html(response);
                            slidermenu();
                            R.Extra.BindingExtraToProduct();
                            R.Product.BindingTotal();
                            R.Product.RegisterEvent();
                        })
                    },
                    afterPreviousOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        params.pageNumber = parseInt(page)
                        $.post(url, params, function (response) {
                            console.log(response);
                            binding.html('').html(response);
                            slidermenu();
                            R.Extra.BindingExtraToProduct();
                            R.Product.BindingTotal();
                            R.Product.RegisterEvent();
                        })
                    },
                    afterNextOnClick: function () {
                        var el = $('.paginationjs-page.active');
                        ////console.log(el.text());
                        var page = el.text();
                        params.pageNumber = parseInt(page)
                        $.post(url, params, function (response) {
                            console.log(response);
                            binding.html('').html(response);
                            slidermenu();
                            R.Extra.BindingExtraToProduct();
                            R.Product.BindingTotal();
                            R.Product.RegisterEvent();
                        })
                    },
                    callback: function (data, pagination) {
                        $('.pagin').find('.paginationjs').first().addClass('number-page');
                    }
                })
            }
        }
        
    }
}



$(function () {
    R.Extra.Init()
})

function BindExtraTop(element, list_property) {

    var propertySession = sessionStorage.getItem("properties");
    if (propertySession == null) {
        var url = R.Extra.culture + "/Extra/GetPropertyDetails"
        $.post(url, null, function (response) {
            //console.log(response);
            sessionStorage.setItem("properties", response);
            var property_list = JSON.parse(response);
            ////console.log(property_list, element, list_property);
            var isDoneTop = false;
            var isDoneBottom = false;

            if (property_list.length != null) {
                property_list.forEach(function (i1) {
                    if (list_property != null) {
                        list_property.find(function (e) {
                            if (parseInt(e) == i1.Id && i1.Position == 'top') {
                                $(element).find('.tag-1').html(i1.Name);
                                $(element).find('.tag').html(i1.Name);
                                isDoneTop = true;

                            }
                            if (parseInt(e) == i1.Id && i1.Position == 'bottom') {
                                ////console.log($(element))
                                var src = R.StoreFilePath(true) + i1.Thumb;
                                //$(element).find('.tag-2-thumb').attr("src", src);
                                $(element).find('.deal').find(' img').attr("src", src);
                                $(element).find('.deal').find('.text-deal').html(i1.Content);
                                isDoneBottom = true;

                            }
                        })
                    }
                })
            }
            if (isDoneTop == false)
                $(element).find('.tag-1').hide();
        })
    } else {
        var property_list = JSON.parse(propertySession);
        ////console.log(property_list, element, list_property);
        var isDoneTop = false;
        var isDoneBottom = false;
        ////console.log(property_list);
        if (property_list.length != null) {
            property_list.forEach(function (i1) {
                if (list_property != null) {
                    list_property.find(function (e) {
                        if (parseInt(e) == i1.Id && i1.Position == 'top') {
                            $(element).find('.tag-1').html(i1.Name);
                            $(element).find('.tag').html(i1.Name);
                            isDoneTop = true;
                            ////console.log($(element))
                        }
                        if (parseInt(e) == i1.Id && i1.Position == 'bottom') {
                            ////console.log($(element))
                            var src = R.StoreFilePath(true) + i1.Thumb;
                            //$(element).find('.tag-2-thumb').attr("src", src); // Doi domain se thay anh
                            $(element).find('.deal').find('.text-deal').html(i1.Content);
                            isDoneBottom = true;
                        }
                    })
                }
            })
        }
        if (isDoneTop == false) {
            $(element).find('.tag-1').hide();
            $(element).find('.tag').hide();
        }

    }





    //if (property_list != null) {
    //    list_property.find(function (e) {

    //        if (parseInt() == e.Id && element.Position == 'top') {
    //            $(element).find('.tag-1').html(e.Name);
    //            //console.log("tag 1 " + $(element))
    //        }
    //        if (parseInt(e) == e.Id && element.Position == 'bottom') {
    //            ////console.log("Tag 2 " + $(element))
    //            var src = R.StoreFilePath(true) + e.Thumb;
    //            $(element).find('.tag-2-thumb').attr("src", src);
    //        }
    //    })
    //}


    //if (property_list != null) {
    //    property_list.forEach(function (item) {
    //        list_property.forEach(function (p) {
    //            ////console.log(p);
    //            if (parseInt(p) == item.Id && item.Position == 'top' && isDoneTop == false) {
    //                //console.log(item.Name)
    //                $(element).find('.tag-1').html(item.Name);
    //                isDoneTop == true;
    //                ////console.log(item.Content);
    //                //Bind ra tag-1
    //            }
    //            //else {
    //            //    $(element).find('.tag-1').hide();
    //            //}
    //            if (parseInt(p) == item.Id && item.Position == 'bottom' && isDoneBottom == false) {
    //                var src = R.StoreFilePath(true) + item.Thumb;
    //                $(element).find('.tag-2-thumb').attr("src", src);
    //                isDoneBottom = true;
    //            }
    //            //console.log(isDoneTop, isDoneBottom);
    //            //if (isDoneTop == false)
    //            //    $(element).find('.tag-1').hide();
    //        })
    //    })
    //}

    //if (typeof callback == "function")
    //    callback(element); 


};
function readURL(input, el) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            el.find('._binding_pickup_img').show();
            el.find('._place-img').attr('src', e.target.result).css("max-width","300").css("max-height","200");
            //$('._binding_pickup_img').css("positon", "relative");
            //$('._close_customize').css("position", "absolute").css("top","0").css("right","0").css("z-index","99");
            
            R.Extra.RegisterEvent();
            //$('place-img')
        }
        reader.readAsDataURL(input.files[0]);
    }
};
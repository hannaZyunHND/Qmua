$(document).ready(function () {
    sessionStorage.setItem("current-js-tree", localStorage.getItem("jstree"));
    $('.tree-menu-mobile').jstree({
        "core": {
            "animation": 100,
            "check_callback": true,
            "multiple": false, // no multiselection
            "themes": {
                "dots": false, // no connecting dots between dots
                "icons": false

            },
        },
        "plugins": ["state", "wholerow", "types"],
        'types': {
            'default': {
                'icon': 'fa fa-angle-right fa-fw'
            },
            'f-open': {
                'icon': 'fa fa-folder-open fa-fw'
            },
            'f-closed': {
                'icon': 'fa fa-folder fa-fw'
            }
        }
    });
    if (window.location.pathname == "" || window.location.pathname === "/") {
        localStorage.setItem("jstree", "");
    }

    //var jstreelocal = JSON.parse(localStorage.getItem("jstree"));
    //if (jstreelocal != "" || jstreelocal !== 'undefinde') {

    //}
    //var current_selected_node = 
    $('.tree-menu-mobile').on("select_node.jstree", function (e, data) {
        var r = data.node;
        //console.log(r);
        var nodesToKeepOpen = [];

        // get all parent nodes to keep open
        $('#' + data.node.id).parents('.jstree-node').each(function () {
            nodesToKeepOpen.push(this.id);
        });

        // add current node to keep open
        nodesToKeepOpen.push(data.node.id);

        // close all other nodes
        $('.jstree-node').each(function () {
            if (nodesToKeepOpen.indexOf(this.id) === -1) {
                $(".tree-menu-mobile").jstree().close_node(this.id);
            }
        })
        if (r.state.opened == false && r.children.length > 0) {
            $('.tree-menu-mobile').jstree("toggle_node", data.node);
            return false;
        }
        if (r.state.opened == true || r.children.length == 0) {
            var id = r.id;
            var url = $('#' + id).find('.span-tree-node').data('url');
            var old_node = $(id).find('.jstree-clicked');
            console.log(sessionStorage.getItem('current-js-tree'));
            if (sessionStorage.getItem('current-js-tree') == "" || sessionStorage.getItem('current-js-tree') === "undefined") {
                window.location.href = url;
            } else {
                var ss = JSON.parse(sessionStorage.getItem('current-js-tree'));

                var seleced_current = ss.state.core.selected;
                if (seleced_current.length > 0) {
                    if (seleced_current[0] != id) {
                        //console.log("Khac roi nay");
                        //sessionStorage.setItem("current-js-tree", localStorage.getItem("jstree"));
                        window.location.href = url;
                    }
                    if (seleced_current[0] == id) {
                        //console.log("Trung roi nay")
                    }
                }
            }


            //if (ss == "1") {

            //}
            return false;
        }
    });

    $('#txt-time-input').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        //minYear: 1901,
        //maxYear: parseInt(moment().format('YYYY'), 10),
        locale: {
            "format": "DD/MM/YYYY"
        }
        //}, function (start, end, label) {
        //    var years = moment().diff(start, 'years');
        //    alert("You are " + years + " years old!");
    });

    var url = window.location.pathname;
    $('.menu-cate-mobile div a[href="' + url + '"]').addClass('active');

    $('.find-product').keydown(function () {

        if ($(this).val().length < 3) {
            $(".suggest-text.suggest-text-2").hide();
            $(".suggest-text.suggest-text-1").show();
        } else {

            $(".suggest-text.suggest-text-2").show();
            $(".suggest-text.suggest-text-1").hide();


            $(this).autocomplete({
                delay: 300,
                source: function (request, response) {
                    $.ajax({
                        url: "/Product/Get",
                        dataType: "json",
                        data: {
                            keyword: request.term
                        },
                        success: function (data) {
                            debugger
                            response(data);
                        }
                    });
                },
                minLength: 2,
            }).autocomplete("instance")._renderItem = function (div, item) {
                debugger
                $(".suggest-text.suggest-text-2").empty();
                var lstItem = "";
                lstItem += "<div class='h6 px-3'>Sản phẩm gợi ý</div>"
                $.each(item, function (index, im) {
                    lstItem += '<div class="item-cart"><div class="image-product"><img src="' + im.avatar + '" class="" /></div>';
                    lstItem += '<div class="text px-2 "> <h6 class="name-product mb-1"><a href="' + im.url + '">' + im.name + '</a></h6>';
                    if (im.discountPrice.length > 0) {
                        lstItem += '<div class="price d-inline-block">' + im.discountPrice + '</div>';
                    }
                    if (im.discountPrice.price > 0) {
                        lstItem += ' <div class=" price-old d-inline-block">' + im.price + '</div>';
                    }
                    lstItem += "</div></div>";
                });
                $(div).removeClass("ui-menu");
                return $(lstItem).appendTo(".suggest-text.suggest-text-2");
            };
        }
    })



    $('.find-product').on("focusout", function () {
        setTimeout(function () {
            $(".suggest-text.suggest-text-2").hide();
            $(".suggest-text.suggest-text-1").hide();
        }, 1000);
    });
    $('.find-product').on("focus", function () {
        if ($(this).val().length < 3) {
            $(".suggest-text.suggest-text-1").show();
        }
    });
    $('.cust-ag').off('click').on('click', function () {
        var url_image = $(this).data('original');
        //var url_image = $(this).parent().data('url');
        $('#img01').attr('src', url_image);

        $('.modal-img').modal('show');
        $('.modal-backdrop').replaceWith("");
    })
    $('.close-load-img').off('click').on('click', function () {
        $('.modal-img').modal('hide');
    })
    $('#modal-id').on('shown.bs.modal', function () {
        $(".modal-backdrop.in").hide();
    })
})
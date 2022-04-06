/*
FSelect
Author: Tú Káo
Release Date: 24/09/2014
Last Updated: 20/8/2015
Last Updated: 17/3/2016. hiephv
        - autoResize ,hideNodeLevel,allowSelectNodeChild

Depenencies: tipsy, jScrollPane (Nếu không có sẽ fallback về mặc định)
*/
(function ($) {
    $.fn.fselect = function (userOptions) {
        var defaults = {
            highlightSearch: false, //Nếu bằng true, sẽ có nền vàng highlight ở các item có chứa từ khóa
            dropDownWidth: 0, //Nếu = 0 sẽ lấy width bằng width của thẻ select
            dropDownHeight: 300,
            showChildNodeOnSearch: true,
            multiple: false,
            searchUnicode: true,
            placeholder: 'Chọn giá trị...',
            showCheckBox: false, //chỉ dạng multiple
            closeOnSelect: true, //,
            autoResize: false,//chỉ dạng multiple
            hideNodeLevel: 0,// hide node level >= thứ tự child truyền vào
            allowSelectNodeChild: true // ẩn các child node khi node parent selected
        };
        var settings = $.extend({}, defaults, userOptions);
        return this.each(function () {
            fselect(this, settings);
        });
    };

    function fselect(selectField, settings) {
        if (typeof (isSmartPhone) != "undefined") {
            if (isSmartPhone) return;
        }
        var $selectField = $(selectField);
        settings.multiple = (typeof ($selectField.attr('multiple')) != "undefined");
        if ($selectField.next().hasClass('FSelectWrapper')) {
            return;
        }
        var $options = $('option', $(selectField));
        var selectList = [];
        $options.each(function (index) {
            var val = $(this).attr('value');
            //for tipsy
            var title = $(this).attr('title');
            if (typeof (title) == "undefined") title = $(this).attr('original-title');
            if (typeof (title) == "undefined") title = '';

            var txt = $(this).text();
            if ($.trim(txt) == '') txt = 'Chọn giá trị...';
            var displayText = txt;
            if (typeof (val) != "undefined" && val != '') {
                //(displayText.replace(/^-+/, '')).length > 0 là để check trường hợp text kiểu như -----
                if (typeof (val) != "undefined" && val != '' && (parseFloat(val) > 0) && (displayText.replace(/^-+/, '')).length > 0) displayText = displayText.replace(/^-+/, '');
            }
            $(this).text(displayText);
            var dataParent = $(this).attr('data-parent');
            var additionCls = [];
            var parentTxt = '';
            var currentLevel = 0;
            function getLevel(dataParent) {
                if (dataParent > 0) {
                    if ($('option[value="' + dataParent + '"]', $selectField).length > 0) {
                        var myParent = $('option[value="' + dataParent + '"]', $selectField).attr('data-parent');
                        if (myParent != null) {
                            if (myParent > 0) {
                                return currentLevel += getLevel(myParent);
                            } else {
                                return 1;
                            }
                        }
                    }
                }
            }
            if (dataParent != null) {
                //Thêm class nếu như là mục cha để style
                if (dataParent <= 0) {
                    additionCls.push('FParentNode');
                } else {
                    currentLevel = 1;
                    getLevel(dataParent);
                    additionCls.push('FChildNode');

                    additionCls.push('FChildNodeLevel' + currentLevel);
                    parentTxt = $('option[value="' + dataParent + '"]', $selectField).text();
                }
            }
            var displayText = txt;

            if (typeof (val) != "undefined" && val != '' && (parseFloat(val) > 0) && (displayText.replace(/^-+/, '')).length > 0) displayText = displayText.replace(/^-+/, '');
            var chk = '';
            if (settings.showCheckBox) {
                additionCls.push('FHasCheckbox');
                var index2 = $('select').index($selectField);
                chk = '<div class="FSelectCheckboxWrapper"><input class="FSelectCheckbox" id="FSelectCheckBox' + index + index2 + '" type="checkbox"><label class="FSelectCheckboxLabel" for="FSelectCheckBox' + index + index2 + '"></label></div>';
            }
            if (settings.hideNodeLevel > 0) {
                if (currentLevel < settings.hideNodeLevel) {
                    selectList.push('<li title="' + title + '" data-value="' + val + '" data-parent="' + dataParent + '" class="' + additionCls.join(' ') + '">' + chk + '<span>' + displayText + '</span><i>' + parentTxt + '</i></li>');
                }
            } else {
                selectList.push('<li title="' + title + '" data-value="' + val + '" data-parent="' + dataParent + '" class="' + additionCls.join(' ') + '">' + chk + '<span>' + displayText + '</span><i>' + parentTxt + '</i></li>');
            }
        });
        var $template = null;
        if (settings.multiple) {
            $template = $('<div class="FSelectWrapper"> \
                            <div class="FSelectWrapperIn">\
                                <ul class="FSelectMultipleValues"><input class="FSelectMultipleValueSearch" type="text" placeholder="' + settings.placeholder + '" /><i></i></ul>\
                                <div class="FSelectDropDown">\
                                    <div class="FSelectSearchBoxWrapper"><input type="text" placeholder="Tìm kiếm..." class="FSelectSearchBox"/></div>\
                                    <ul class="FSelectList"></ul>\
                                </div> \
                            </div> \
                        <div>');
        } else {
            $template = $('<div class="FSelectWrapper"> \
                            <div class="FSelectWrapperIn">\
                                <span class="FSelectedValue"><b></b><i></i></span>\
                                <div class="FSelectDropDown">\
                                    <div class="FSelectSearchBoxWrapper"><input type="text" placeholder="Tìm kiếm..." class="FSelectSearchBox"/></div>\
                                    <ul class="FSelectList"></ul>\
                                </div> \
                            </div> \
                        <div>');
        }

        var $wrapper = $template;
        var $searchBox = $('.FSelectSearchBox', $template);
        var $list = $('.FSelectList', $template);
        var $selected = $('.FSelectedValue', $template);
        var $selectedText = $('.FSelectedValue b', $template);
        var $dropdown = $('.FSelectDropDown', $template);
        var $multipleSelected = $('.FSelectMultipleValues', $template);
        var $multipleSearchField = $('.FSelectMultipleValueSearch', $template);

        $list.html(selectList.join(' '));

        function buildPlaceHolder() {
            if (!$.trim(settings.placeholder)) {
                //placeholder
            }
        }

        var $li = $('.FSelectList li', $template);
        $li.off('click').on('click', function () {
            if (settings.multiple) {
                if ($(this).hasClass('disabled')) {
                    //Nếu đã chọn thì thôi
                    return false;
                } else {
                    $multipleSearchField.before('<li data-value="' + $(this).attr('data-value') + '"><span>' + $(this).find('span').text().trim() + '</span><span class="FMultiSelectRemove"></span></li>');
                    $('.FMultiSelectRemove', $multipleSelected).off('click').on('click', function () {
                        var $currentSelected = $(this).parents('li:first');
                        $li.filter('[data-value="' + $currentSelected.attr('data-value') + '"]').removeClass('disabled');

                        if (settings.allowSelectNodeChild) {
                            // lấy gia trị parrent;
                            var parrentValue = $currentSelected.attr('data-value');
                            $(this).closest('.FSelectWrapperIn').find('.FSelectList li[data-parent=' + parrentValue + ']').removeClass('disabled');

                            // check selected xem còn thằng con nào ko mới active cha

                            $('.FSelectMultipleValues li').each(function () {

                            });

                            var dataParent = $('.FSelectDropDown').find('li[data-value=' + parrentValue + ']').attr('data-parent');
                            $('.FSelectDropDown').find('li.FParentNode[data-value=' + dataParent + ']').removeClass('disabled');

                        }


                        $currentSelected.remove();
                        reStyleMultiple();
                        updateMultiSelectValue();
                    });

                    $(this).addClass('disabled');
                    // Nếu chọn con thì ko cho chọn cha.
                    // check nếu chọn chính nó.
                    // if (settings.multiple) {
                    if (!$(this).hasClass('.FParentNode')) {
                        var parrentId = $(this).attr('data-parent');
                        $(this).closest('.FSelectDropDown').find('li.FParentNode[data-value=' + parrentId + ']').addClass('disabled');
                    }
                    //  }


                    // tìm tất cả cac con của node
                    if (settings.allowSelectNodeChild) {
                        // lấy gia trị parrent;
                        var parrentValue = $(this).attr('data-value');
                        // ẩn các node là con 
                        $(this).parent().find('li[data-parent=' + parrentValue + ']').addClass('disabled');
                    }
                }
                updateMultiSelectValue();
                reStyleMultiple();
                if (settings.closeOnSelect) {
                    $dropdown.hide();
                    $searchBox.val('').trigger('keyup');
                    $selected.removeClass('drop');
                }
            } else {
                $li.filter('.selected').removeClass('selected');
                $(this).addClass('selected');
                $selected.attr('data-value', $(this).attr('data-value'));
                $selectedText.text($(this).find('span').text().trim());
                $selectField.val($(this).attr('data-value'));
                $selectField.trigger('change');
                $dropdown.hide();
                $searchBox.val('').trigger('keyup');
                $selected.removeClass('drop');
            }
        });

        $li.off('hover').on('hover', function () {
            $li.filter('.selected').removeClass('selected');
            $(this).addClass('selected');
        });

        //Gán lại giá trị khi load lên       
        if (settings.multiple) {
            var vals = $selectField.val();
            if (vals != null) {
                $.each(vals, function (i, item) {
                    $li.filter('[data-value="' + item + '"]').trigger('click');
                });
            }
        } else {
            var $selectedItem = $selectField.find('option:selected');
            if ($selectedItem.length == 0) {
                $selectedItem = $selectField.find('option:first');
            }
            $('li[data-value="' + $selectedItem.attr('value') + '"]', $dropdown).addClass('selected');
            $selected.attr('data-value', $selectedItem.attr('value'));
            $selectedText.text($selectedItem.text());
        }

        if (jQuery().tipsy) {
            $li.tipsy({ html: true, live: true, gravity: 'e', opacity: 1 });
        }

        if (settings.autoResize) {
            var w = $selectField.parent().width();//    
            $selected.css({
                width: w
            });

            $multipleSelected.css({
                width: w
            });
            $selectedText.css({
                width: w - 18
            });
            $wrapper.css({
                width: w
            });
          
            $(window).resize(function () {
              var  rw = $selectField.parent().width();
                setTimeout(function () {
                    $selected.css({
                        width: rw
                    });

                    $multipleSelected.css({
                        width: rw
                    });
                    $selectedText.css({
                        width: rw - 18
                    });
                    $wrapper.css({
                        width: rw
                    });
                }, 200)
               
            });

        }
        else
        {
          
            var w = $selectField.parent().innerWidth() - (($selectField.parent().innerWidth() - $selectField.parent().width()) / 2);//   

            if (w > 20) {

            } else {
                $selectField.parent().width();
            }
            $selected.css({
                width: w
            });

            $multipleSelected.css({
                width: w
            });
            $selectedText.css({
                width: w - 18
            });
            $wrapper.css({
                width: w
            });
        }

        
        if (settings.dropDownHeight > 0) {
            $list.css({
                height: settings.dropDownHeight
            });
        }
        if (settings.dropDownWidth > 0) {

            $dropdown.css({
                width: settings.dropDownWidth
            });
        } else {
            $dropdown.css({
                width: '100%'
            });
        }
        $selectField.after($template);
        //Ẩn sau khi đã tính toán xong width, height. nếu ẩn trước sẽ không lấy được kích thước chính xác
        $selectField.addClass('FSelectField');

        function openSelectList() {
            if ($dropdown.is(':visible')) {
                closeSelect();
            } else {
                if ($li.length < 10) {
                    //Tự ẩn ô tìm kiếm nếu số lượng item ít.
                    $searchBox.parents('.FSelectSearchBoxWrapper').hide();
                }
                $dropdown.show();
                $searchBox.focus();
                $selected.addClass('drop');
                $list.scrollTop($('li.selected', $wrapper).scrollTop());
                checkDropDownHeight();

                if (jQuery().jScrollPane) {
                    var opts = {};
                    if (settings.dropDownWidth > 0) {
                        opts = {
                            width: settings.dropDownWidth
                        };
                    }
                    $list.jScrollPane(opts);
                }
            }
        }
        //select item
        $selected.off('click').on('click', function () {
            openSelectList();
        });
        //multiple value selected item
        $multipleSelected.off('click').on('click', function (e) {
            if (!$(e.target).hasClass('FMultiSelectRemove')) {
                openSelectList();
            }
        });
        function closeSelect() {
            $dropdown.hide();
            $selected.removeClass('drop');
        }
        function updateMultiSelectValue() {
            var vals = [];
            $('li', $multipleSelected).each(function () {
                var val = $(this).attr('data-value');
                vals.push(val);
            });
            $selectField.val(vals);
            $selectField.trigger('change');
        }
        function checkMultipleHasValue() {
            if ($('li', $multipleSelected).length == 0) {
                $multipleSearchField.show();
                $multipleSelected.addClass('noItemSelected');
            } else {
                //comment tạm thời
                // $multipleSearchField.hide();
                $multipleSelected.removeClass('noItemSelected');
            }
        }
        function reStyleMultiple() {
            checkMultipleHasValue();
            var h = $multipleSelected.height();
            $dropdown.css({
                top: h
            });
        }

        reStyleMultiple();
        //search
        function removeUnicode(s) {
            if (settings.searchUnicode) {
                uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
                KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
                retVal = "";
                for (i = 0; i < s.length; i++) {
                    pos = uniChars.indexOf(s.charAt(i));
                    if (pos >= 0) {
                        retVal += KoDauChars.charAt(pos);
                    } else {
                        retVal += s.charAt(i);
                    }
                }
                return retVal;
            } else {
                return s;
            }
        }

        //close on click outside
        $(document).click(function (evt) {
            if (($(evt.target).parents('.FSelectWrapper').length == 0) && $dropdown.is(':visible')) {
                closeSelect();
            } else if ($(evt.target).parents('.FSelectWrapper').find($dropdown).length == 0) {
                //click on other dropdown
                closeSelect();
            }
        });

        $searchBox.off('keyup').on('keyup', function (evt) {
            try {
                var api = $list.data('jsp');
                api.scrollTo(0, 0);
            } catch (e) {

            }
            if (evt.keyCode == 40) {
                //down
                var $currentSelectedLi = $('.FSelectList li.selected:visible:first', $dropdown);
                var $next = $currentSelectedLi.nextAll('li:visible:first');
                if ($next.length == 0) {
                    $next = $('li:visible:first');
                }
                if ($next.length > 0) {
                    $li.removeClass('selected');
                    $next.addClass('selected');
                    $list.scrollTop($('.FSelectList li.selected', $wrapper).scrollTop());
                }
            } else if (evt.keyCode == 38) {
                //up
                var $currentSelectedLi = $('.FSelectList li.selected:visible:first', $dropdown);
                var $prev = $currentSelectedLi.prevAll('li:visible:first');
                if ($prev.length == 0) {
                    $prev = $('li:visible:last', $dropdown);
                }
                if ($prev.length > 0) {
                    $li.removeClass('selected');
                    $prev.addClass('selected');
                }
            } else if (evt.keyCode == 13) {
                //enter
                var $currentSelectedLi = $('.FSelectList li.selected:visible:first', $dropdown);
                if ($currentSelectedLi.length > 0) {
                    $currentSelectedLi.trigger('click');
                }
            }
            else {
                var kw = $(this).val();
                if (settings.highlightSearch) {
                    $li.find('.FSelectSearchHighLight').replaceWith(function () {
                        return $(this).text();
                    });
                }
                if (kw != '') {
                    kw = kw.toLowerCase();
                    var showParents = [];
                    $li.each(function () {
                        var txt = $(this).find('span').text().toLowerCase();
                        if (txt.lastIndexOf(kw) != -1 || removeUnicode(txt).lastIndexOf(removeUnicode(kw)) != -1) {
                            $(this).show();
                            if (settings.highlightSearch) {
                                $(this).find('span').html(makeHighLight(kw, $(this).find('span').text()));
                            }
                            showParents.push($(this).attr('data-value'));
                        } else {
                            $(this).hide();
                        }
                    });
                    if (settings.showChildNodeOnSearch) {
                        showChildNode(showParents);
                    }
                } else {
                    $li.show();
                }
                $li.removeClass('selected');
                $('.FSelectList li:visible:first', $template).addClass('selected');
            }
        });

        function showChildNode(showParents) {
            $.each(showParents, function (i, item) {
                $('.FSelectList li[data-parent="' + item + '"]', $template).show();
            });
        }

        function makeHighLight(keyword, text) {
            var reg = new RegExp(keyword, 'gi');
            text = text.replace(reg, function (_m) {
                return '<i class="FSelectSearchHighLight">' + _m + '</i>';
            });
            return text;
        }

        function checkDropDownHeight() {
            //Hàm này để tính trường hợp nếu dropdown mà dài quá (số item ít ==> k có thanh cuộn) thì lấy height của dropdown = height của tổng items.
            //chưa lấy đc height của các items.
            var itemHeight = $('li:eq(0)', $list).outerHeight(true);
            var liHeight = $li.length * itemHeight;
            if (liHeight < settings.dropDownHeight) {
                $list.css({
                    height: liHeight
                });
            } else {
                $list.css({
                    height: settings.dropDownHeight
                });
            }
        }
        //trigger - overide chosen
        function resetFselect() {
            //phải show lại để tính width đúng
            $selectField.removeClass('FSelectField');
            $selectField.next().remove();
            fselect(selectField, settings);
        }
        $selectField.off('liszt:updated chosen:updated fselect:updated').on('liszt:updated chosen:updated fselect:updated', function () {
            resetFselect();
        });
    }
})(jQuery);

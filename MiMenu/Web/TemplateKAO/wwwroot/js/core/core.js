var R = {
    Init: function () {
        //R.ClearCart();
        R.LoadCart();
        R.FirstLoad();
    },
    LoadCart: function () {
        var r = localStorage.getItem("arrProduct");
        if (r == null) {
            var arrProduct = [];
            localStorage.setItem('arrProduct', JSON.stringify(arrProduct));
        }
        //console.log(r);
        var arrProduct = JSON.parse(localStorage.getItem("arrProduct"));
        var grouped = {};
        if (arrProduct.length > 0)
            grouped = R.GroupBy(arrProduct, 'product_id');
        ////console.log(arrProduct);
        ////console.log(grouped);

        //Che bien Grouped
        var groupped_after = [];
        //Lay danh sach key cua grouped
        var keys = Object.keys(grouped);
        ////console.log(keys);
        keys.forEach(function (element) {
            ////console.log("key" + element, grouped[element]);
            var result_product_id = parseInt(element);
            var arr_value = grouped[element];
            var sum_quantity = 0;
            var get_time = 0;
            var promotion = [];
            arr_value.forEach(function (a) {
                sum_quantity += a.quantity;
                //Lay pomotion, để làm sau
                var arr_promotion = a.promotion;
                var arr_convert = a.promotion.reduce(function (map, obj) {
                    map[obj.id] = map[obj.id] || obj.check;
                    return map;
                }, {});
                //Lay time
                //console.log(a.add_time);
                var time = a.add_time;
                if (time > get_time)
                    get_time = time;
                
            })
            ////console.log("sum: " + sum_quantity);
            var item_cart_converted = {
                product_id: result_product_id,
                quantity: sum_quantity,
                add_time: get_time,
                promotion : []
            }
            groupped_after.push(item_cart_converted);
        })
        localStorage.setItem('arrProduct', JSON.stringify(groupped_after));
        arrProduct = JSON.parse(localStorage.getItem("arrProduct"));
        arrProduct.sort((a, b) => (a.add_time > b.add_time) ? 1 : -1);
        //console.log(arrProduct);
        //Lay gia tien theo dia danh trong gio hang

        
        //Dem so luong san pham trong gio hang
        var count_cart = arrProduct.length;
        $('#cart-count').text(count_cart);
        $('#cart-count-mobile').text(count_cart);
        if (count_cart > 9)
            $('#cart-count').text('9+');
        //Danh sach productIds:
        var product_ids = [];
        arrProduct.forEach(function (element) {
            product_ids.push(element.product_id);
        })
        //alert(product_ids);
        var base_culture = R.Culture();
        var default_target_cart = base_culture + '/gio-hang';
        var queryString = "?product_ids=" + product_ids;
        //$('#link-target-cart').attr("href", default_target_cart + queryString);
        //$('#buy-now').attr("href", default_target_cart + queryString);
        $('#link-target-cart').attr("href", default_target_cart);
        $('#link-target-cart').data('product_ids', product_ids.toString());
        $('#link-target-cart-mobile').data('product_ids', product_ids.toString());
        $('#buy-now').attr("href", default_target_cart);
    },
    Test: function () {
        var test = 1;
        //console.log(test);
    },
    Culture: function () {
        //var culture = window.location.pathname.split('/')[1];
        //return ("/" + culture);
        return ("");
    },
    FormatNumber: function (num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
    }, 
    ClearCart: function () {
        var arrProduct = [];
        localStorage.setItem('arrProduct', JSON.stringify(arrProduct));
    },
    GroupBy: function (xs, key) {
        return xs.reduce(function (rv, x) {
            (rv[x[key]] = rv[x[key]] || []).push(x);
            return rv;
        }, {});
    },
    CurrentLocationId: function () {
        var location_id = $('#current-local').data('id');
        return location_id;
    },
    RemoveCartItem: function (id) {
        var r = JSON.parse(localStorage.getItem("arrProduct"));
        for (var i = 0; i < r.length; i++) {
            if (r[i].product_id == id)
                r.splice(i,1);
        }
        localStorage.setItem('arrProduct', JSON.stringify(r));

    },
    StoreFilePath: function (isThumb) {
        // string store_url = "https://jhcms.migroup.asia", string root = "/uploads"
        var store_url = "https://jhcms.migroup.asia";
        var root = "/uploads";
        if (isThumb)
            return store_url + root + "/thumb";
        else
            return store_url + root;
    },
    FirstLoad: function () {
        var r = sessionStorage.getItem("locationFirstLoad");
        if (r == null)
            sessionStorage.setItem("locationFirstLoad", "1");
    },
    CountDown: function (date, el) {
        var countDownDate = new Date(date).getTime();

        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get today's date and time
            var now = new Date().getTime();
            //console.log(countDownDate);

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            $(el).find('#hour').text(hours + 'h');
            $(el).find('#minute').text(minutes + 'm');
            $(el).find('#second').text(seconds + 's');

        }, 1000);
    },
    
}
$(function () {
    R.Init();
})

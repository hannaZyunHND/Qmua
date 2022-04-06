var markers = [];
var map;
var directionsDisplay;
var directionsService;
var infoWindow;
var isNotSp = false;
var isPickupZoom = 6;
R.Location = {
    InitMap: function () {

        R.Location.currentLat = 17.464886; //center VietNam
        R.Location.currentLng = 106.434782;
        R.Location.culture = R.Culture();
        directionsDisplay = new google.maps.DirectionsRenderer;
        directionsService = new google.maps.DirectionsService;

        infoWindow = new google.maps.InfoWindow();
        map = new google.maps.Map(document.getElementById('map'), {
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        // infoWindow = new google.maps.InfoWindow({ map: map });
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {

                var _lat = position.coords.latitude, _lng = position.coords.longitude;
                var pos = { lat: _lat, lng: _lng };

                map.setCenter(pos);
                map.setZoom(6);
                R.Location.addCurrentMarker(pos);
                directionsDisplay.setMap(map);
                R.Location.currentLat = _lat;
                R.Location.currentLng = _lng;
                isNotSp = false;
                R.Location.LoadLocation();
                R.Location.PickupLocation();

            }, function () {
                //alert('Trình duyệt không hỗ trợ');
                // load bản đồ mặc đinh;
                var pos = { lat: R.Location.currentLat, lng: R.Location.currentLng };
                map.setCenter(pos);
                map.setZoom(6);
                R.Location.addCurrentMarker(pos);
                directionsDisplay.setMap(map);
                //R.Location.currentLat = _lat;
                //R.Location.currentLng = _lng;
                isNotSp = false;
                R.Location.LoadLocation();
                R.Location.PickupLocation();
                //$.confirm({
                //    title: 'Thông báo !',
                //    //  type: 'red',
                //    content: "Vị trí hiện tại không xác định. Hãy chọn một vị trí.",
                //    animation: 'scaleY',
                //    closeAnimation: 'scaleY',
                //    theme: 'supervan',
                //    type: 'orange',
                //    buttons: {
                //        cancel: {
                //            text: 'Đóng',
                //            btnClass: 're-btn re-btn-default'
                //        },
                //        yes: {
                //            isHidden: true, // hide the button
                //            keys: ['ESC'],
                //            action: function () {

                //            }
                //        }

                //    }

                //});
                map.setOptions({ draggableCursor: 'crosshair' });// thay doi con tro chuot
                //map.addListener('click', function (event) {
                //    isNotSp = true;
                //    isPickupZoom = 6;
                //    R.Location.clearMarkers();
                //    R.Location.addCurrentMarker(event.latLng);
                //    directionsDisplay.setMap(map);
                //    var position = event.latLng.toUrlValue().split(',');
                //    R.Location.currentLat = position[0];
                //    R.Location.currentLng = position[1];
                //    R.Location.RegisterEvents();

                //});


            }, { timeout: 10000 });
        } else {
            R.Location.LoadLocation();
        }
        R.Location.Init();
    },
    Init: function () {
        $('#map').css('height', $(window).height() - 45);
        R.Location.RegisterEvents();
        R.Location.resizeResult();
    },
    RegisterEvents: function () {
        $('#current-location').off('click').on('click', function () {
            map.setOptions({ draggableCursor: 'crosshair' });
            map.addListener('click', function (event) {
                R.Location.clearMarkers();
                R.Location.addCurrentMarker(event.latLng);
                directionsDisplay.setMap(map);
                var position = event.latLng.toUrlValue().split(',');
                R.Location.currentLat = position[0];
                R.Location.currentLng = position[1];
                R.Location.RegisterEvents();
            });
            isNotSp = true;
        });

        //search data 
        $('#txtSearchLocation').off('change').on('change', function () {

        });

        // tab result
        $('#map-sidebar-left .nav-shop-result ul.header li:not(.active)').off('click').on('click', function () {
            var $el = $(this);
            if (!$el.hasClass('active')) {
                $el.closest('ul').find('li').removeClass('active').find('.gw-headerpanel-tab-pin').remove();
                $el.addClass('active').append('<div class="gw-headerpanel-tab-pin"><div></div></div>');
                R.Location.RegisterEvents();
            }
        });
        //zoom to feature
        $('#shop-result-content li').off('click').on('click', function () {
            $(this).parent().children('li').removeClass('active')
            $(this).addClass('active');
            var pos = { lat: parseFloat($(this).attr('data-lat')), lng: parseFloat($(this).attr('data-lng')) }
            R.Location.zoomToFeature(pos);
            var index = $('#shop-result-content li').index($(this));
            google.maps.event.trigger(markers[index], "click");
        });

    },
    SeachLocation: function () {
        var searchData = $('#txtSearchLocation').val().toUpperCase();
        var div = document.getElementById("shop-result-content");
        var item = div.getElementsByClassName("item");
        for (var i = 0; i < item.length; i++) {
            var a = item[i].getElementsByTagName("a");
            var txtValue = a[0].innerHTML;
            if (txtValue.toUpperCase().indexOf(searchData) > -1) {
                item[i].style.display = "";
            } else {
                item[i].style.display = "none";
            }
        }
    },

    ShowLocation: function (e) {
        var pos = { lat: parseFloat($(e).attr('data-lat')), lng: parseFloat($(e).attr('data-lng')) }
        R.Location.zoomToFeature(pos);
        var index = $('#shop-result-content .item').index($(this));
        google.maps.event.trigger(markers[index], "click");
    },
    handleLocationError: function (browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(browserHasGeolocation ?
            'Error: The Geolocation service failed.' :
            'Error: Your browser doesn\'t support geolocation.');
    },
    ShowPopup: function () {
        //$('#modal').modal('view', {
        //    target: '#modal',
        //    animation: 'top',
        //    position: 'center',
        //    overlayClose: true,
        //    show: true
        //})
    },
    PickupLocation: function () {
        map.setOptions({ draggableCursor: 'crosshair' });

        //map.addListener('click', function (event) {
        //    if (!$("input[name='rDistance']:checked").val()) {
        //        $.confirm({
        //            title: 'Thông báo !',
        //            // type: 'red',
        //            content: "Chọn khoảng cách !.",
        //            animation: 'scaleY',
        //            closeAnimation: 'scaleY',
        //            theme: 'supervan',
        //            type: 'orange',
        //            buttons: {
        //                cancel: {
        //                    text: 'Đóng',
        //                    btnClass: 're-btn re-btn-default'
        //                },
        //                yes: {
        //                    isHidden: true, // hide the button
        //                    keys: ['ESC'],
        //                    action: function () {

        //                    }
        //                }

        //            }

        //        });
        //        return false;
        //    }


        //    R.Location.clearMarkers();
        //    R.Location.addCurrentMarker(event.latLng);
        //    directionsDisplay.setMap(map);
        //    var position = event.latLng.toUrlValue().split(',');
        //    R.Location.currentLat = position[0];
        //    R.Location.currentLng = position[1];

        //    R.Location.LoadLocation();
        //});
    },
    LoadLocation: function () {



        var distance = $("input[name='rDistance']:checked").val() ? $("input[name='rDistance']:checked").val() : -1;
        var dataPost = {
            Latitude: parseFloat(R.Location.currentLat),
            Longitude: parseFloat(R.Location.currentLng),
            Distance: distance,
            LanguageCode: "vi-VN",
            SortOrder: 1

        };
        R.Location.SearchNearLocation(dataPost);
    },
    SearchNearLocation: function (dataPost) {

        //$('#text-result').html('<img src="../images/ajax-loader.gif"/>');
        var url = R.Location.culture + '/Store/NearLocation';
        $.post(url, dataPost, function (res) {
            if (res.length > 0) {
                // Xóa Marker old
                if (markers.length > 0) {
                    R.Location.deleteMarkers();
                }
                //---
                //var json = $.parseJSON(res.Data);

                var features = [];
                var rowHtml = '';
                var currentFeature = {
                    "title": '',
                    "lat": parseFloat(dataPost.Latitude),
                    "lng": parseFloat(dataPost.Longitude),
                    "address": '',
                    "mobile": '',
                    "icon": '/images/marker-icon.png',
                    "direction": "Vị trí hiện tại"
                };
                if (isNotSp) {
                    features.push(currentFeature);
                }

                $.each(res, function (i, v) {
                    rowHtml += '<div class="item">';
                    rowHtml += '<div class="name" > <a onclick="R.Location.ShowLocation(this)" href="javascripts:void(0)" data-lat="' + v.latitude + '" data-lng="' + v.longitude + '">' + v.name + '</a></div>';
                    rowHtml += '<div class="d-flex "><div class="icon"><img src="/images/icon-store-1.svg?v=5A4pR0aLOzwJtrkhudyAYFxk-oTNF0GmGRalbrl62Rg" class="img-fluid"></div>';
                    rowHtml += '<div class="text">' + v.address + '</div></div>';
                    rowHtml += '<div class="d-flex "><div class="icon"><img src = "/images/icon-store-2.svg?v=nuiAqW-Wgnn18OSZRw2zN5XR_ospp17aa-9KEZ2oeAo" class="img-fluid"></div>';
                    rowHtml += '<div class="text">' + v.email + '</div></div>';
                    rowHtml += '<div class="d-flex">';
                    rowHtml += '<div class="icon">';
                    rowHtml += '<img src="/images/icon-store-3.svg?v=6EeySJWAq8vfAQP19g76W-PZsgingm7xcQy0bvM2k7g" class="img-fluid"></div>';
                    rowHtml += '<div class="text color-EE7D22 font-weight-600 ">' + v.hotline + '</div></div></div>';
                    //rowHtml += '<li data-lat="' + v.Latitude + '" data-lng="' + v.Longitude + '" ><h2>' + v.Name + '</h2>' +
                    //    '<p class="vote"><span>Đánh giá:</span><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i></p>' +
                    //    '<address><span>Đ/c</span>: ' + v.Address + '</address>' +
                    //    '<p class="phone"><span>Điện thoại</span>:' + mobile + '</p><p class="profile">bởi: <a href="#">' + v.CreatedBy + '</a> - <span><time>' + JSONDate(v.CreatedDate) + '</time></span></p></li>';

                    var feature = {
                        "title": v.name + '(' + v.distance + 'km)',
                        "lat": v.latitude,
                        "lng": v.longitude,
                        'icon': '/images/janhome-marker.png',
                        "address": "<p class='address'><b>Đc</b> :" + v.address + "</p>",
                        "mobile": "<p><b>ĐT</b>:" + v.hotline + "</p>",
                        "direction": '<p><b>Đường đi</b>:' +
                            '<a class="btn-derection" onclick="R.Location.Direction(' + v.latitude + ',' + v.longitude + ',1)" href="javascript:void(0)">Lái xe</a>, ' +
                            //'<a class="btn-derection" onclick="R.Location.Direction(' + v.Latitude + ',' + v.Longitude + ',2)" href="javascript:void(0)">Xe máy</a>, ' +
                            '<a class="btn-derection" onclick="R.Location.Direction(' + v.latitude + ',' + v.longitude + ',3)" href="javascript:void(0)">Đi bộ</a>' +
                            '</p>'
                    }
                    features.push(feature);
                });

                var url1 = R.Location.culture + '/Store/NearLocationHTML';
                $.post(url1, null, function (res1) {
                    $('#shop-result-content').html(res1);
                    $('.list-add-store').mCustomScrollbar({
                        theme: "dark",
                        autoHideScrollbar: true
                    });


                    var pos = {
                        lat: parseFloat(dataPost.Latitude),
                        lng: parseFloat(dataPost.Longitude)
                    };
                    $('#text-result').html('');
                    //if (dataPost.top == 0) {


                    R.Location.AddMarkerAndZoomCenter(features, pos);

                })
                //} else {
                //    R.Location.AddMarkerAndZoomBound(features, pos);
                //}


            } else {
                $('#text-result').html('Không tìm thấy !');
            }
            R.Location.RegisterEvents();
        });
    },


    AddMarkerAndZoomCenter: function (features, pop) {
        directionsDisplay.set('directions', null); // clear direction

        //  var latlngbounds = new google.maps.LatLngBounds(); // Create bounds 
        for (var i = 0; i < features.length; i++) {
            var data = features[i];
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: data.icon
            });
            (function (marker, data) {
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent("<div class=\"windows-base\" style = 'width:200px;min-height:40px'><p><b class='title'>" + data.title + "</b></p>" + data.address + data.mobile + data.direction + "</div>");
                    infoWindow.open(map, marker);
                });
            })(marker, data);
            markers.push(marker);
            //  latlngbounds.extend(marker.position);
        }
        // var bounds = new google.maps.LatLngBounds();

        var curentLtLng = new google.maps.LatLng(pop.lat, pop.lng);
        map.setCenter(curentLtLng);
        map.setZoom(isPickupZoom);

        //map.fitBounds(latlngbounds);
        $('.gmnoprint,.gm-style-cc,.gmnoprint').remove();
        R.Location.RegisterEvents();
    },

    AddMarkerAndZoomBound: function (features, pop) {
        directionsDisplay.set('directions', null); // clear direction

        //  var latlngbounds = new google.maps.LatLngBounds(); // Create bounds 
        for (var i = 0; i < features.length; i++) {
            var data = features[i];

            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: data.icon
            });
            (function (marker, data) {
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent("<div class=\"windows-base\" style = 'width:200px;min-height:40px'><p><b class='title'>" + data.title + "</b></p>" + data.address + data.mobile + data.direction + "</div>");
                    infoWindow.open(map, marker);
                });
            })(marker, data);
            markers.push(marker);
            // latlngbounds.extend(marker.position);
        }
        //  var bounds = new google.maps.LatLngBounds();

        var curentLtLng = new google.maps.LatLng(pop.lat, pop.lng);
        map.setCenter(curentLtLng);
        map.setZoom(10);
        map.fitBounds(map.getBounds());
        //  map.fitBounds(bounds);
        //   $('.gmnoprint,.gm-style-cc,.gmnoprint').remove();
        R.Location.RegisterEvents();
    },
    Direction: function (latEnd, lngEnd, mode) {
        var modeTravel = null;
        switch (mode) {
            case 1:
                modeTravel = google.maps.TravelMode.DRIVING;
                break;
            case 2:
                modeTravel = google.maps.TravelMode.BICYCLING;
                break;
            case 3:
                modeTravel = google.maps.TravelMode.WALKING;
                break;
            default:
        }

        directionsService.route({
            origin: {
                lat: R.Location.currentLat,
                lng: R.Location.currentLng,
            }, // Haight.

            destination: { lat: parseFloat(latEnd), lng: parseFloat(lngEnd) },
            travelMode: modeTravel,
            unitSystem: google.maps.UnitSystem.IMPERIAL

        }, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            } else {
                $.confirm({
                    title: 'Thông báo !',
                    theme: 'supervan',
                    type: 'orange',
                    content: "Phương tiện duy chuyển không phù hợp.",
                    animation: 'scaleY',
                    closeAnimation: 'scaleY',
                    buttons: {
                        cancel: {
                            text: 'Đóng',
                            btnClass: 're-btn re-btn-default'
                        },
                        yes: {
                            isHidden: true, // hide the button
                            keys: ['ESC'],
                            action: function () {

                            }
                        }

                    }

                });
            }
        });

        directionsDisplay.setOptions({
            suppressMarkers: true,
            polylineOptions: {
                strokeColor: "#2249a3",
                strokeOpacity: 0.7,
                strokeWeight: 8
            }
        });
    },
    addCurrentMarker: function (location) {

        var marker = new google.maps.Marker({
            position: location,
            map: map,
            icon: '/images/marker-icon.png'
        });
        markers.push(marker);
    },
    // Sets the map on all featurs in the array.
    setMapOnAll: function (map) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    },
    // Removes the featurs from the map, but keeps them in the array.
    clearMarkers: function () {
        R.Location.setMapOnAll(null);
    },
    // Shows any featurs currently in the array.
    showMarkers: function () {
        R.Location.setMapOnAll(map);
    },
    // Deletes all featurs in the array by removing references to them.
    deleteMarkers: function () {

        R.Location.clearMarkers();
        markers = [];
    },
    zoomToFeature: function (pos) {
        map.setZoom(16);
        map.setCenter(pos);
        R.Location.RegisterEvents();
    },
    resizeResult: function () {
        var h = $('#map').outerHeight();
        $('#map-sidebar-left .list-add-store').css({
            'height': h

        });
        $('.list-add-store').mCustomScrollbar({
            theme: "dark",
            autoHideScrollbar: true
        });
    },
    myclick: function (i) {
        google.maps.event.trigger(markers[i], "click");
    }


}
$(function () {
    $('#map').css('height', $(window).height())
    $('#map').css('width', $(window).width())


});

$(window).resize(function () {
    //$('.shop-result-content').mCustomScrollbar({
    //    theme: "dark",
    //    autoHideScrollbar: true
    //});
    R.Location.resizeResult();
});
function JSONDate(dateStr) {
    var m, day;
    var jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (day + '/' + m + '/' + d.getFullYear())
}
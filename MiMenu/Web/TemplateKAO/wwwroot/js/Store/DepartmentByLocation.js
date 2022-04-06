var markers = [];
var map;
var directionsDisplay;
var directionsService;
var infoWindow;
var isNotSp = false;
R.DepartmentByLocation = {
    InitMap: function () {
       
        this.currentLat = 21.0281106;
        this.currentLng = 105.8359394;
        R.DepartmentByLocation.culture = R.Culture();
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
                R.DepartmentByLocation.addCurrentMarker(pos);
                directionsDisplay.setMap(map);
                R.DepartmentByLocation.currentLat = _lat;
                R.DepartmentByLocation.currentLng = _lng;
                isNotSp = false;
            }, function () {
                //alert('Trình duyệt không hỗ trợ');
                //// load bản đồ mặc đinh;
                var pos = { lat: R.DepartmentByLocation.currentLat, lng: R.DepartmentByLocation.currentLng };
                map.setCenter(pos);
                map.setZoom(6);
                //$.confirm({
                //    title: 'Thông báo !',
                //    type: 'red',
                //    content: "Vị trí hiện tại không xác định. Hãy chọn một vị trí.",
                //    animation: 'scaleY',
                //    closeAnimation: 'scaleY',
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
                    map.addListener('click', function (event) {

                    R.DepartmentByLocation.clearMarkers();
                    R.DepartmentByLocation.addCurrentMarker(event.latLng);
                    directionsDisplay.setMap(map);
                    var position = event.latLng.toUrlValue().split(',');
                    R.DepartmentByLocation.currentLat = position[0];
                    R.DepartmentByLocation.currentLng = position[1];
                    R.DepartmentByLocation.RegisterEvents();
                    R.DepartmentByLocation.LoadLocation();
                    isNotSp = true;
                });
            }, { timeout: 10000 });
        } else {
          
            R.DepartmentByLocation.LoadLocationDefault();
        }

    },
    Init: function () {
        $('#map').css('height', $(window).height() - 45);
        R.DepartmentByLocation.RegisterEvents();
        R.DepartmentByLocation.resizeResult();
    },
    RegisterEvents: function () {
        $('#current-location').off('click').on('click', function () {
            map.setOptions({ draggableCursor: 'crosshair' });
            map.addListener('click', function (event) {
                R.DepartmentByLocation.clearMarkers();
                R.DepartmentByLocation.addCurrentMarker(event.latLng);
                directionsDisplay.setMap(map);
                var position = event.latLng.toUrlValue().split(',');
                R.DepartmentByLocation.currentLat = position[0];
                R.DepartmentByLocation.currentLng = position[1];
                R.DepartmentByLocation.RegisterEvents();
            });
            isNotSp = true;
        });


        // tab result
        $('#map-sidebar-left .nav-shop-result ul.header li:not(.active)').off('click').on('click', function () {
            var $el = $(this);
            if (!$el.hasClass('active')) {
                $el.closest('ul').find('li').removeClass('active').find('.gw-headerpanel-tab-pin').remove();
                $el.addClass('active').append('<div class="gw-headerpanel-tab-pin"><div></div></div>');
                R.DepartmentByLocation.RegisterEvents();
            }
        });
        //zoom to feature
        $('#shop-result-content li').off('click').on('click', function () {
            $(this).parent().children('li').removeClass('active')
            $(this).addClass('active');
            var pos = { lat: parseFloat($(this).attr('data-lat')), lng: parseFloat($(this).attr('data-lng')) }
            R.DepartmentByLocation.zoomToFeature(pos);
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
        R.DepartmentByLocation.zoomToFeature(pos);
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
    LoadLocationDefault: function () {
        var dataPost = {
            Latitude: R.DepartmentByLocation.currentLat,
            Longitude: R.DepartmentByLocation.currentLng,
            Distance: -1,
            LanguageCode: "vi-VN",
            SortOrder: 1
        };
        R.DepartmentByLocation.SearchNearLocation(dataPost);
    },
    LoadLocation: function () {
        var dataPost = {
            Latitude: R.DepartmentByLocation.currentLat,
            Longitude: R.DepartmentByLocation.currentLng,
            Distance: $("input[name='rDistance']:checked").val() ? $("input[name='rDistance']:checked").val() : -1,
            LanguageCode: "vi-VN",
            SortOrder: 1
        };
        R.DepartmentByLocation.NearLocation(dataPost);
    },
    SearchNearLocation: function (dataPost) {
       
        //$('#text-result').html('<img src="../images/ajax-loader.gif"/>');
        let url = R.DepartmentByLocation.culture + '/Store/NearLocation';
        $.post(url, dataPost, function (res) {
            if (res.length > 0) {
                // Xóa Marker old
                if (markers.length > 0) {
                    R.DepartmentByLocation.deleteMarkers();
                }
                //---
                //var json = $.parseJSON(res.Data);

                var features = [];
                var rowHtml = '';
                $.each(res, function (i, v) {
                    rowHtml += '<div class="item">';
                    rowHtml += '<div class="name" > <a onclick="R.DepartmentByLocation.ShowLocation(this)" href="javascripts:void(0)" data-lat="' + v.latitude + '" data-lng="' + v.longitude + '">' + v.name + '</a></div>';
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
                        "address": "<p class='address'><b>Đc</b> :" + v.address + "</p>",
                        "mobile": "<p><b>ĐT</b>:" + v.hotline + "</p>",
                        "direction": '<p><b>Đường đi</b>:' +
                            '<a class="btn-derection" onclick="R.DepartmentByLocation.Direction(' + v.latitude + ',' + v.longitude + ',1)" href="javascript:void(0)">Lái xe</a>, ' +
                            //'<a class="btn-derection" onclick="R.Location.Direction(' + v.Latitude + ',' + v.Longitude + ',2)" href="javascript:void(0)">Xe máy</a>, ' +
                            '<a class="btn-derection" onclick="R.DepartmentByLocation.Direction(' + v.latitude + ',' + v.longitude + ',3)" href="javascript:void(0)">Đi bộ</a>' +
                            '</p>'
                    }
                    features.push(feature);
                });

                $('#shop-result-content').html(rowHtml);
                $('.list-add-store').mCustomScrollbar({
                    theme: "dark",
                    autoHideScrollbar: true
                });
                var currentFeature = {
                    "title": '',
                    "lat": parseFloat(dataPost.Latitude),
                    "lng": parseFloat(dataPost.Longitude),
                    "address": '',
                    "mobile": '',
                    "icon": '/images/marker-icon.png',
                    "direction": "Vị trí hiện tại"
                }

                var pos = {
                    lat: parseFloat(dataPost.Latitude),
                    lng: parseFloat(dataPost.Longitude)
                };
                $('#text-result').html('');
                //if (dataPost.top == 0) {
                features.push(currentFeature);
                R.DepartmentByLocation.AddMarkerAndZoomCenterForDefault(features, pos);
                //} else {
                //    R.Location.AddMarkerAndZoomBound(features, pos);
                //}

            } else {
                $('#text-result').html('Không tìm thấy !');
            }
            R.DepartmentByLocation.RegisterEvents();
        });
    },
    NearLocation: function (dataPost) {
        //$('#text-result').html('<img src="../images/ajax-loader.gif"/>');
        let url = R.DepartmentByLocation.culture + '/Store/NearLocation';
        $.post(url, dataPost, function (res) {
            if (res.length > 0) {
                // Xóa Marker old
                if (markers.length > 0) {
                    R.DepartmentByLocation.deleteMarkers();
                }
                //---
                //var json = $.parseJSON(res.Data);

                var features = [];
                var rowHtml = '';
                $.each(res, function (i, v) {
                    rowHtml += '<div class="item">';
                    rowHtml += '<div class="name" > <a onclick="R.DepartmentByLocation.ShowLocation(this)" href="javascripts:void(0)" href="javascripts:void(0)" data-lat="' + v.latitude + '" data-lng="' + v.longitude + '">' + v.name + '</a></div>';
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
                        "address": "<p class='address'><b>Đc</b> :" + v.address + "</p>",
                        "mobile": "<p><b>ĐT</b>:" + v.hotline + "</p>",
                        "direction": '<p><b>Đường đi</b>:' +
                            '<a class="btn-derection" onclick="R.DepartmentByLocation.Direction(' + v.latitude + ',' + v.longitude + ',1)" href="javascript:void(0)">Lái xe</a>, ' +
                            //'<a class="btn-derection" onclick="R.Location.Direction(' + v.Latitude + ',' + v.Longitude + ',2)" href="javascript:void(0)">Xe máy</a>, ' +
                            '<a class="btn-derection" onclick="R.DepartmentByLocation.Direction(' + v.latitude + ',' + v.longitude + ',3)" href="javascript:void(0)">Đi bộ</a>' +
                            '</p>'
                    }
                    features.push(feature);
                });

                $('#shop-result-content').html(rowHtml);
                $('.list-add-store').mCustomScrollbar({
                    theme: "dark",
                    autoHideScrollbar: true
                });
                var currentFeature = {
                    "title": '',
                    "lat": parseFloat(dataPost.Latitude),
                    "lng": parseFloat(dataPost.Longitude),
                    "address": '',
                    "mobile": '',
                    "icon": '/images/marker-icon.png',
                    "direction": "Vị trí hiện tại"
                }

                var pos = {
                    lat: parseFloat(dataPost.Latitude),
                    lng: parseFloat(dataPost.Longitude)
                };


                var count = res.length;
                $('#text-result').html('');
                //if (dataPost.top == 0) {
                features.push(currentFeature);
                R.DepartmentByLocation.AddMarkerAndZoomCenter(features, pos);
                $('#text-result').html('<strong>' + count + '</strong> Kết quả.');
                //} else {
                //    R.Location.AddMarkerAndZoomBound(features, pos);
                //}

            } else {
                $('#text-result').html('Không tìm thấy !');
            }
            R.DepartmentByLocation.RegisterEvents();
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
                icon: '/images/marker-icon.png'
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
        map.setZoom(10);

        //map.fitBounds(latlngbounds);
        $('.gmnoprint,.gm-style-cc,.gmnoprint').remove();
        R.DepartmentByLocation.RegisterEvents();
    },
    AddMarkerAndZoomCenterForDefault: function (features, pop) {
        directionsDisplay.set('directions', null); // clear direction

        //  var latlngbounds = new google.maps.LatLngBounds(); // Create bounds 
        for (var i = 0; i < features.length; i++) {
            var data = features[i];
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: '/images/marker-icon.png'
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

        var curentLtLng = new google.maps.LatLng(15.6322608, 103.182669);
        map.setCenter(curentLtLng);
        map.setZoom(6);

        //map.fitBounds(latlngbounds);
        $('.gmnoprint,.gm-style-cc,.gmnoprint').remove();
        R.DepartmentByLocation.RegisterEvents();
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
        map.setZoom(14);
        map.fitBounds(map.getBounds());
        //  map.fitBounds(bounds);
        //   $('.gmnoprint,.gm-style-cc,.gmnoprint').remove();
        R.DepartmentByLocation.RegisterEvents();
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
                lat: R.DepartmentByLocation.currentLat,
                lng: R.DepartmentByLocation.currentLng,
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
                    type: 'red',
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
        R.DepartmentByLocation.setMapOnAll(null);
    },
    // Shows any featurs currently in the array.
    showMarkers: function () {
        R.DepartmentByLocation.setMapOnAll(map);
    },
    // Deletes all featurs in the array by removing references to them.
    deleteMarkers: function () {

        R.DepartmentByLocation.clearMarkers();
        markers = [];
    },
    zoomToFeature: function (pos) {
        map.setZoom(16);
        map.setCenter(pos);
        R.DepartmentByLocation.RegisterEvents();
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
    R.DepartmentByLocation.Init();
    //   R.Location.InitMap();
});

$(window).resize(function () {
    //$('.shop-result-content').mCustomScrollbar({
    //    theme: "dark",
    //    autoHideScrollbar: true
    //});
    // R.Location.resizeResult();
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
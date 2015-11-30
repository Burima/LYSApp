$(document).ready(function () {


    var geocoder;
    var map;
    var infowindow;
    var latlng;
    var icon;
    var name;


    var options = {
        zoom: 14,
        mapTypeId: 'Styled',
        disableDefaultUI: true,
        mapTypeControlOptions: {
            mapTypeIds: ['Styled']
        }
    };
    var styles = [{
        stylers: [{
            hue: "#cccccc"
        }, {
            saturation: -100
        }]
    }, {
        featureType: "road",
        elementType: "geometry",
        stylers: [{
            lightness: 100
        }, {
            visibility: "simplified"
        }]
    }, {
        featureType: "road",
        elementType: "labels",
        stylers: [{
            visibility: "on"
        }]
    }, {
        featureType: "poi",
        stylers: [{
            visibility: "off"
        }]
    }];

    var newMarker = null;
    var markers = [];
    // custom infowindow object for property
    var propertyInfoBox = new InfoBox({
        disableAutoPan: false,
        maxWidth: 202,
        pixelOffset: new google.maps.Size(-101, -285),
        zIndex: null,
        boxStyle: {
            background: "url('../Images/infobox-bg.png') no-repeat",
            opacity: 1,
            width: "202px",
            height: "245px"
        },
        closeBoxMargin: "28px 26px 0px 0px",
        closeBoxURL: "",
        infoBoxClearance: new google.maps.Size(1, 1),
        pane: "floatPane",
        enableEventPropagation: false
    });
    // custom infowindow object
    var infobox = new InfoBox({
        disableAutoPan: false,
        maxWidth: 202,
        pixelOffset: new google.maps.Size(-101, -120),
        zIndex: null,
        boxStyle: {
            background: "url('../Images/infobox-bg.png') no-repeat",
            opacity: 1,
            width: "202px",
            height: "70px"
        },
        closeBoxMargin: "28px 26px 0px 0px",
        closeBoxURL: "",
        infoBoxClearance: new google.maps.Size(1, 1),
        pane: "floatPane",
        enableEventPropagation: false
    });
    // function that adds the markers on map
    var addMarkers = function (props, map) {
        //alert(props.length);
        $.each(props, function (i, prop) {
            //alert("lat : "+prop.position.lat+"   long : "+ prop.position.long);
            latlng = new google.maps.LatLng(prop.position.lat, prop.position.long);
            map.setCenter(latlng);
            map.setZoom(15);
            var image = '../Images/marker-green.png';
            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                icon: image,
                draggable: false,

            });
            var infoboxContent = '<div class="infoW">' +
                                   '<div class="propImg">' +
                                       '<img src="images/prop/' + prop.image + '">' +
                                       '<div class="propBg">' +
                                           '<div class="propPrice">' + prop.price + '</div>' +
                                       '</div>' +
                                   '</div>' +
                                   '<div class="paWrapper">' +
                                       '<div class="propTitle">' + prop.title + '</div>' +
                                       '<div class="propAddress">' + prop.address + '</div>' +
                                   '</div>' +
                                   //'<div class="propRating">' +
                                   //    '<span class="fa fa-star"></span>' +
                                   //    '<span class="fa fa-star"></span>' +
                                   //    '<span class="fa fa-star"></span>' +
                                   //    '<span class="fa fa-star"></span>' +
                                   //    '<span class="fa fa-star-o"></span>' +
                                   //'</div>' +
                                   '<div class="clearfix"></div>' +
                                   '<div class="infoButtons">' +
                                       '<a class="btn btn-sm btn-round btn-gray btn-o closeInfo">Close</a>' +
                                       '<a href="single.html" class="btn btn-sm btn-round btn-green viewInfo">View</a>' +
                                   '</div>' +
                                '</div>';

           google.maps.event.addListener(marker, 'click', (function(marker, i) {
               return function() {
                   propertyInfoBox.setContent(infoboxContent);
                   propertyInfoBox.open(map, marker);
               }
           })(marker, i));

            $(document).on('click', '.closeInfo', function () {
                propertyInfoBox.open(null, null);
            });

            markers.push(marker);
        });
    }

    var map;

    setTimeout(function () {
        $('body').removeClass('notransition');

        map = new google.maps.Map(document.getElementById('mapView'), options);
        var styledMapType = new google.maps.StyledMapType(styles, {
            name: 'Styled'
        });

        map.mapTypes.set('Styled', styledMapType);
        map.setCenter(new google.maps.LatLng(20.1885251, 64.4458764));
        map.setZoom(15);
        //alert("js"+props.length);
        //console.log("js" + props.length);
        addMarkers(props, map);
    }, 300);
    $("#connectivity-bus").click(function () {
        codeAddress();
    });

    $("#connectivity-railstation").click(function () {
        codeAddress();
    });

    $("#amenity-store").click(function () {
        codeAddress();
    });

    $("#amenity-salon").click(function () {
        codeAddress();
    });

    $("#amenity-bank").click(function () {
        codeAddress();
    });

    $("#amenity-atm").click(function () {
        codeAddress();
    });

    $("#amenity-petrol").click(function () {
        codeAddress();
    });

    $("#amenity-police").click(function () {
        codeAddress();
    });


    $("#healthcare-hospital").click(function () {
        codeAddress();
    });

    $("#healthcare-pharmacy").click(function () {
        codeAddress();
    });

    $("#entertainment-mall").click(function () {
        codeAddress();
    });
    $("#entertainment-theatre").click(function () {
        codeAddress();
    });
    $("#food-restaurant").click(function () {
        codeAddress();
    });
    $("#food-bar").click(function () {
        codeAddress();
    });
    $("#education-school").click(function () {
        codeAddress();
    });
    $("#education-college").click(function () {
        codeAddress();
    });
    $("#education-daycare").click(function () {
        codeAddress();
    });

    $("#entertainment-park").click(function () {
        codeAddress();
    });


    function codeAddress() {
        refreshMap();

        var selectedVal = "";
        var selected = $("input[type='radio'][name='commongroup']:checked");
        if (selected.length > 0) {

            selectedVal = selected.val();
            if (selectedVal == "Bus") {
                icon = '/Images/Map/bus.png';
            } else if (selectedVal == "Railway") {
                icon = '/Images/Map/trainstation.png';
            } else if (selectedVal == "Supermarket") {
                icon = '/Images/Map/store.png';
            } else if (selectedVal == "Salon and Spa") {
                icon = '/Images/Map/salon.png';
            } else if (selectedVal == "Bank") {
                icon = '/Images/Map/bank.png';
            } else if (selectedVal == "ATM") {
                icon = '/Images/Map/ATM.png';
            } else if (selectedVal == "Petrol Station") {
                icon = '/Images/Map/petrol.png';
            } else if (selectedVal == "Police Station") {
                icon = '/Images/Map/police.png';
            } else if (selectedVal == "Hospitals") {
                icon = '/Images/Map/hospital.png';
            } else if (selectedVal == "Pharmacy") {
                icon = '/Images/Map/pharmacy.png';
            } else if (selectedVal == "Mall") {
                icon = '/Images/Map/store.png';
            } else if (selectedVal == "Movie Theater") {
                icon = '/Images/Map/theatre.png';
            } else if (selectedVal == "Amusement Park") {
                icon = '/Images/Map/amusementpark.png';
            } else if (selectedVal == "Restaurants") {
                icon = '/Images/Map/Restaurant.png';
            } else if (selectedVal == "Bars and NightClubs") {
                icon = '/Images/Map/disc.png';
            } else if (selectedVal == "School") {
                icon = '/Images/Map/school.png';
            } else if (selectedVal == "Day Care") {
                icon = '/Images/marker-red.png';
            } else if (selectedVal == "College and University") {
                icon = '/Images/Map/college.png';
            }
        }

        var request = {
            location: latlng,
            radius: '5000',
            query: selectedVal

        };

        var service = new google.maps.places.PlacesService(map);
        service.textSearch(request, callback);
    }

    function callback(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                createMarker(results[i]);
            }
        }
    }

    function createMarker(place) {

        var placeLoc = place.geometry.location

        var marker = new google.maps.Marker({
            map: map,
            title: place.name,
            position: place.geometry.location,
            icon: icon
        });

        var infoboxContent = '<div class="infoW">' +

                                    '<div class="paWrapper">' +
                                        '<div class="propTitle">' + place.name + '</div>' +
                                        '<div class="propAddress">' + place.name + '</div>' +
                                    '</div>' +
                                 '</div>';

        google.maps.event.addListener(marker, 'click', (function (marker) {
            return function () {
                infobox.setContent(infoboxContent);
                infobox.open(map, marker);
            }
        })(marker));

    }

    function refreshMap() {
        $('body').removeClass('notransition');

        map = new google.maps.Map(document.getElementById('mapView'), options);
        var styledMapType = new google.maps.StyledMapType(styles, {
            name: 'Styled'
        });

        map.mapTypes.set('Styled', styledMapType);
        map.setCenter(new google.maps.LatLng(20.1885251, 64.4458764));
        map.setZoom(15);

        addMarkers(props, map);
    }

    $("#btnViewAmenities").click(function () {

        if (document.getElementById('divAmenities').style.display == 'none') {
            document.getElementById('divAmenities').style.display = 'block';
            $("#btnViewAmenities").text('Hide Amenities');
        } else {
            document.getElementById('divAmenities').style.display = 'none';
            $("#btnViewAmenities").text('View Amenities');
        }
    });
    $("#commute").click(function () {
        name = 'connectivity';
        $('#commute').addClass('active-map');
        $('#amenity').removeClass("active-map");
        $('#healthcare').removeClass("active-map");
        $('#entertainment').removeClass("active-map");
        $('#food').removeClass("active-map");
        $('#education').removeClass("active-map");

        $('#connectivity').removeClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");

    });

    $("#amenity").click(function () {
        $('#amenity').addClass('active-map');
        $('#healthcare').removeClass("active-map");
        $('#entertainment').removeClass("active-map");
        $('#food').removeClass("active-map");
        $('#education').removeClass("active-map");
        $('#commute').removeClass("active-map");


        $('#basicamenity').removeClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
    });

    $("#healthcare").click(function () {
        $('#healthcare').addClass('active-map');
        $('#entertainment').removeClass("active-map");
        $('#food').removeClass("active-map");
        $('#education').removeClass("active-map");
        $('#commute').removeClass("active-map");
        $('#amenity').removeClass('active-map');

        $('#healthcares').removeClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
    });

    $("#entertainment").click(function () {
        $('#entertainment').addClass('active-map');
        $('#healthcare').removeClass('active-map');
        $('#food').removeClass("active-map");
        $('#education').removeClass("active-map");
        $('#commute').removeClass("active-map");
        $('#amenity').removeClass('active-map');

        $('#entertainments').removeClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
    });

    $("#food").click(function () {
        $('#food').addClass('active-map');
        $('#entertainment').removeClass('active-map');
        $('#healthcare').removeClass('active-map');
        $('#education').removeClass("active-map");
        $('#commute').removeClass("active-map");
        $('#amenity').removeClass('active-map');

        $('#foods').removeClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
    });

    $("#education").click(function () {
        $('#food').removeClass('active-map');
        $('#entertainment').removeClass('active-map');
        $('#healthcare').removeClass('active-map');
        $('#commute').removeClass("active-map");
        $('#amenity').removeClass('active-map');
        $('#education').addClass('active-map');

        $('#educations').removeClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
    });

});



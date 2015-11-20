var geocoder;
var map;
var infowindow;
var latlng;
var icon;
var name;
$(document).ready(function () {

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
    // custom infowindow object
    var infobox = new InfoBox({
        disableAutoPan: false,
        maxWidth: 202,
       
        zIndex: null,
        boxStyle: {
            
            width: "202px",
            height: "245px"
        },
        closeBoxMargin: "28px 26px 0px 0px",
        closeBoxURL: "",
        infoBoxClearance: new google.maps.Size(1, 1),
        pane: "floatPane",
        enableEventPropagation: false
    });

    // function that adds the markers on map
    var addMarkers = function (props, map) {

        $.each(props, function (i, prop) {
            latlng = new google.maps.LatLng(prop.position.lat, prop.position.lng);
            map.setCenter(latlng);
            map.setZoom(15);
            var image = '/Images/marker-green.png';
            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                icon: image,
                draggable: false,
              
            });
           

            $(document).on('click', '.closeInfo', function () {
                infobox.open(null, null);
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
    $("#education-university").click(function () {
        codeAddress();
    });

    $("#education-park").click(function () {
        codeAddress();
    });


    function codeAddress() {
        refreshMap();
       
        var selectedVal = "";
        var selected = $("input[type='radio'][name='commongroup']:checked");
        if (selected.length > 0) {
            alert(selected.val());
            selectedVal = selected.val();
            if (selectedVal == "Bus") {
                icon = '/Images/bus.png';
            } else if (selectedVal == "Railway") {
                icon = '/Images/railway.png';
            } else if (selectedVal == "Amusement Park") {
                icon = '/Images/tree.png';
            } else {
                icon = '/Images/marker-red.png';
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
                                    '<div class="propImg">' +
                                        '<img src="images/prop/2-1.png'+ '">' +
                                        
                                    '</div>' +
                                    '<div class="paWrapper">' +
                                        '<div class="propTitle">' + place.name + '</div>' +
                                        '<div class="propAddress">' + place.name + '</div>' +
                                    '</div>' +
                                    '<div class="propRating">' +
                                        '<span class="fa fa-star"></span>' +
                                        '<span class="fa fa-star"></span>' +
                                        '<span class="fa fa-star"></span>' +
                                        '<span class="fa fa-star"></span>' +
                                        '<span class="fa fa-star-o"></span>' +
                                    '</div>' +
                                    '<ul class="propFeat">' +
                                        '<li><span class="fa fa-moon-o"></span> ' + place.name + '</li>' +
                                        '<li><span class="icon-drop"></span> ' + place.name + '</li>' +
                                        '<li><span class="icon-frame"></span> ' + place.name + '</li>' +
                                    '</ul>' +
                                    '<div class="clearfix"></div>' +
                                    '<div class="infoButtons">' +
                                        '<a class="btn btn-sm btn-round btn-gray btn-o closeInfo">Close</a>' +
                                        '<a href="single.html" class="btn btn-sm btn-round btn-green viewInfo">View</a>' +
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
        $('#commute').toggleClass('active');
        $('#connectivity').removeClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
    });

    $("#amenity").click(function () {
        $('#amenity').toggleClass('active');
        $('#basicamenity').removeClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
    });

    $("#healthcare").click(function () {
        $('#healthcare').toggleClass('active');
        $('#healthcares').removeClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
    });

    $("#shopping").click(function () {
        $('#shopping').toggleClass('active');
        $('#entertainments').removeClass("hidden");
        $('#foods').addClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
    });

    $("#food").click(function () {
        $('#food').toggleClass('active');
        $('#foods').removeClass("hidden");
        $('#educations').addClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
    });

    $("#education").click(function () {
        $('#education').toggleClass('active');
        $('#educations').removeClass("hidden");
        $('#connectivity').addClass("hidden");
        $('#basicamenity').addClass("hidden");
        $('#healthcares').addClass("hidden");
        $('#entertainments').addClass("hidden");
        $('#foods').addClass("hidden");
    });

   
});



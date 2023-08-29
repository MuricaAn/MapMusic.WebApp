let map;
let service;
let infowindow;

function initMapFromCreateLocation () {
    var options = {
        zoom: 12,
        center: { lat: 44.426 , lng : 26.102 }
    }
    var map = new google.maps.Map(document.getElementById('map'), options);

    var marker = new google.maps.Marker({
        position: { lat: 44.4562, lng: 26.0569 },
        map: map,
        icon: src="../img/football-field.png"
    });

    var searchInput = document.getElementById('search-input');
    var searchBox = new google.maps.places.SearchBox(searchInput);
    var latitude = document.getElementById('Latitude');
    var longitude = document.getElementById('Longitude');
    var address = document.getElementById('Address');

    var currentMarker = null;

    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();
        if (places.length === 0) {
            return;
        }

        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                return;
            }
            bounds.extend(place.geometry.location);
        });
        map.fitBounds(bounds);
        map.setZoom(15);

        if (currentMarker) {
            currentMarker.setMap(null);
        }

        var selectedPlace = places[0];
        currentMarker = new google.maps.Marker({
            map: map,
            position: selectedPlace.geometry.location,
            title: selectedPlace.name
        });
        searchInput.value = "";

        var markerPosition = currentMarker.getPosition();
        var lat = markerPosition.lat();
        var lng = markerPosition.lng();
        latitude.value = lat;
        longitude.value = lng;
        address.value = "sdsadsdsa";
        getAddressFromMarker(currentMarker, function (addressValue) {
            address.value = addressValue;
        });

    });

}

function getAddressFromMarker(marker, callback) {
    var geocoder = new google.maps.Geocoder();

    geocoder.geocode({ 'location': marker.getPosition() },  function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                console.log("Formatted Address:", results[0].formatted_address);
                callback(results[0].formatted_address);
            } else {
                console.log("No results found");
            }
        } else {
            console.log("Geocoder failed due to: " + status);
        }
    });
}



function createMarker(place) {
    if (!place.geometry || !place.geometry.location) return;

    const marker = new google.maps.Marker({
        map,
        position: place.geometry.location,
    });

    google.maps.event.addListener(marker, "click", () => {
        infowindow.setContent(place.name || "");
        infowindow.open(map);
    });
}

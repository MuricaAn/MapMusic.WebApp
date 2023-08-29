let map;
let service;
let infowindow;

function initMapFromShowLocation() {
    var options = {
        zoom: 15,
        center: { lat: lat, lng: lng }
    }
    var map = new google.maps.Map(document.getElementById('map'), options);

    var marker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: map
    });
}
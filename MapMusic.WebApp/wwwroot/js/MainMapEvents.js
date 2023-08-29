let map;
let service;
let infowindow;

function initMapFromMainMap() {
    var options = {
        zoom: 12,
        center: new google.maps.LatLng(44.426, 26.102) 
    }

    var map = new google.maps.Map(document.getElementById('map'), options);
    fetch(`/Event/GetUpcomingEvents`, {
        method: "get",
    }).then(response => response.json())
        .then(events => {
            console.log(events);
            let musicType = {
                Rap: "Rap",
                Techno: "Techno",
                Rock: "Rock"
            }
            for (var i = 0; i < events.length; i++) {
                let iconPath = "";
                if (events[i].musicType == musicType.Rap) {
                    iconPath = "../img/rapper.png";
                }
                if (events[i].musicType == musicType.Techno) {
                    iconPath = "../img/techno.png";
                }
                if (events[i].musicType == musicType.Rock) {
                    iconPath = "../img/maloik.png";
                }

                let marker = new google.maps.Marker({
                    position: { lat: events[i].location.latitude, lng: events[i].location.longitude },
                    map: map,
                    icon: src = iconPath
                });

                const isoString = events[i].startDate;
                const date = new Date(isoString);

                const options = {
                    day: '2-digit',
                    month: '2-digit',
                    hour: '2-digit',
                    minute: '2-digit',
                    hour12: false
                };

                const formatter = new Intl.DateTimeFormat('default', options);
                const formattedDate = formatter.format(date);

                let eventId = encodeURIComponent(events[i].id);
                let content = `<a href="/Event/ShowEvent?eventId=${eventId}"><h6>${events[i].name}</h6>
                    <div>At ${events[i].location.name}</div>
                    <img src="data:image/png;base64,${events[i].profilePhoto}" style ="width: 75px; height: 150px;" />
                    <div> Starts on:</div>
                    <h6>${formattedDate}</h6>
                    <div>${events[i].musicType} show</div></a>`;

                let infoWindow = new google.maps.InfoWindow({
                    content: content
                });


                marker.addListener('click', function () {
                    infoWindow.open(map, marker);
                });

                //infoWindow.addListener('click', function () {
                //    fetch(`/Event/ShowEvent?eventId=${events[i].id}`, {
                //        method: "post"
                //    })
                //});

            }
        })
}


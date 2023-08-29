function ShowUpcomingEvents(page, locationId) {
    var grid = document.getElementById("grid");
    const roleType = {
        User : 1,
        Admin : 2,
        Artist : 3,
        Organizer : 4
    }
    fetch(`/Location/GetLocationUpcomingEvents?page=${page}&locationId=${locationId}`,
        {
            method: "get",
        }
    ).then(response => response.json())
        .then(events => {
            for (var item in events) {
        var elementWrapper = new DOMParser().parseFromString(
            `<div class="grid-item">
                    <div class="box">
                        <div id="${item.id}" class="fire-button">
                        </div>
                        @if (${currentUser.isLoggedIn})
                        {
                            if (${currentUser.roleId} == ${roleType.User})
                            {
                                <script>
                                    addFavouriteButton${item.id}, ${currentUser.id});
                                </script>
                            }
                        }
                        else
                        {
                            <div class="fire-button">
                                <a href="https://localhost:7275/Account/Login">
                                    <img src="~/img/empty-fire.png">
                                </a>
                            </div>
                        }
                        <h2 class="title">${events[item].name}</h2>
                        <div class="poster">
                            <img src="data:image/png;base64,@Convert.ToBase64String(${item.profilePhoto})" />
                        </div>
                        <p>${events[item].musicType} show, at : <a asp-controller="Location" asp-action="ShowLocation" asp-route-locationId="${events[item].location.id}">${events[item].location.name}</a> ,
                            by <a asp-controller="Account" asp-action="ShowOrganizer" asp-route-organizerId="${events[item].organizer.id}">${events[item].organizer.name}</a>
                        </p>
                        <p class="details">
                            On <b>${events[item].startDate}.ToString("dd.MM.yyyy HH:mm")</b> till <b>${events[item].EndDate}.ToString("dd.MM.yyyy HH:mm")</b>
                        </p>
                        <p>
                            Artists:
                            @if (${events[item].artists} != null)
                            {
                                @foreach (artist in ${events[item].artistsForEvent})
                                {
                                    <a asp-controller="Artist" asp-action="ShowArtist" asp-route-artistId="${events[item].artistsForEvent[artist].id}">${events[item].artistsForEvent[artist].name} ,</a>
                                }
                            }
                        </p>
                            <a asp-controller="Event" asp-action="ShowEvent" asp-route-eventId="${events[item].id}" class="action-button">More about</a>
                        @if (${currentUser.roleId} == ${roleType.Organizer} && ${currentUser.id} == ${events[item].organizer.id})
                        {
                            <a asp-controller="Event" asp-action="EditEvent" asp-route-eventId="${events[item].Id}" class="action-button">Edit</a>
                            <a asp-controller="Event" asp-action="DeleteEvent" asp-route-eventId="${events[item].Id}" class="action-button">Delete</a>
                        }
                    </div>
                </div>`
            , 'text/html').document.getElementsByClassName("grid-item")[0];
        grid.appendChild(elementWrapper);
            }
    })
}


function buttonEvents() {
    var btnUpcomingEvents = document.getElementById("upcomingEvents");
    var btnPastEvents = document.getElementById("pastEvents");
    btnPastEvents.addEventListener("click", function () {
        showUpcomingEvents(1);
    });
}
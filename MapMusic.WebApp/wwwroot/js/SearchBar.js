function formatEvent(event) {
    var $event = $(
        `
        <p style="position: absolute; right: 10px;" >${event.entityType}</p>
        <span><img src="data:image/png;base64,${event.profilePhoto}" style ="width: 40px; height: 50px;" />  ${event.text} </span> 
        `
    );
    return $event;
};

$(document).ready(function () {
    $('.searchBar').select2({   
        ajax: {
            url: 'https://localhost:7275/Event/GetEntitiesForSearchBar',
            data: function (params) {
                var query = {
                    search: params.term
                }

                // Query parameters will be ?search=[term]&type=public
                return query;
            },
            processResults: function (data) {
                // Transforms the top-level key of the response object from 'items' to 'results'
                return {
                    results: data
                };
            }
        },
        placeholder: 'Search ...',
        templateResult: formatEvent,
        dropdownCssClass : "searchBar"
    });
    $('.searchBar').on("select2:select", function (e) {
        if (e.params.data.entityType == "Event") {
            location.href = `/Event/ShowEvent?eventId=${e.params.data.id}`;
        }
        else {
            location.href = `/Artist/ShowArtist?artistId=${e.params.data.id}`;
        }
    });
});
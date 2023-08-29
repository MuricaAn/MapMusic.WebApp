function addFavouriteButton(eventId, userId) {
    let a = document.createElement("a");
    let img = document.createElement("img");
    let div = document.getElementById(eventId);
    let addFunction = () => {
        div.removeChild(div.children[0]);
        let a = document.createElement("a");
        let img = document.createElement("img");
        img.src = "../img/full-fire.png";
        $.ajax({
            url: '/Event/AddFavouriteEvent',
            type: 'POST',
            data: { eventId: eventId, userId: userId }
        });
        a.appendChild(img);
        a.addEventListener("click", removeFunction);
        div.appendChild(a);
    };
    let removeFunction = () => {
        div.removeChild(div.children[0]);
        let a = document.createElement("a");
        let img = document.createElement("img");
        img.src = "../img/empty-fire.png";
        $.ajax({
            url: '/Event/RemoveFavouriteEvent',
            type: 'POST',
            data: { eventId: eventId, userId: userId }
        });
        a.appendChild(img);
        a.addEventListener("click", addFunction);
        div.appendChild(a);

    };
    fetch("https://localhost:7275/Event/IsfavouriteEvent/" + userId + "/" + eventId,
        {
            method: "GET"
        }).then(response => response.json())
        .then(isFavourite => {
            if (isFavourite == true) {
                img.src = "../img/full-fire.png";
                a.addEventListener("click", removeFunction);

            }
            else {
                img.src = "../img/empty-fire.png";
                a.addEventListener("click", addFunction);
            }
            a.appendChild(img);
            div.appendChild(a);
        })
}
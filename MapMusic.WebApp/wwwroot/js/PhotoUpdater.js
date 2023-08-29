function PhotoUpdate(inputId, divPhoto) {
    var input = document.getElementById(inputId);
    input.addEventListener("change", function () {
        var div = document.getElementById(divPhoto);
        var img = document.createElement("img");
        var file = document.getElementById("newPhoto").files[0];
        if (div.firstChild) {
            div.removeChild(div.firstChild);
        }
        var reader = new FileReader();
        img.width = 50; 
        img.height = 50; 

        reader.onload = function (event) {
            img.src = event.target.result;
            div.appendChild(img);
        };

        if (file) {
            reader.readAsDataURL(file);
        }

    });
}
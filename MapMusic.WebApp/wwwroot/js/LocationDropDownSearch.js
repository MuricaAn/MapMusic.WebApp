function LocationDropDownSearch() {
    fetch(`/Location/GetLocation`,
        {
            method: "get",
        }
    ).then(response => response.json())
        .then(locations => {
            div = document.getElementById("myDropdown");
            let deleteAllChildren = (parent) => {
                while (parent.firstChild) {
                    parent.removeChild(parent.firstChild);
                }
            }
            deleteAllChildren(div);
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            for (var location in locations) {
                var a = document.createElement("a");
                a.innerHTML = locations[location].name;
                a.value = locations[location].id;
                a.addEventListener("click", (e) => {
                    var input = document.getElementById("myInput");
                    var forForm = document.getElementById("forForm");
                    forForm.value = e.target.value;
                    input.value = e.target.innerHTML;
                        deleteAllChildren(div);
                })
                txtValue = locations[location].name;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    div.appendChild(a);
                }
            }
        })
}
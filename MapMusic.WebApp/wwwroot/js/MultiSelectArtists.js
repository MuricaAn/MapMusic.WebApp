function MultiselectDropdown(containerId, artistsId) {
    fetch(`/Event/GetArtists`, {
        method: "get"
    }).then(response => response.json())
        .then(artists => {
            if (artistsId != null) {
                artists = artists.filter(a => !artistsId.includes(a.id));
            }
            var options = [];
            for (var i = 0; i < artists.length; i++) {
                options.push({
                    key: artists[i].id,
                    value: artists[i].stageName,
                    selected: false
                });
            }
            //let select = document.getElementById("Artists");
            let select = document.createElement("select");
            let container = document.getElementById(containerId);
            let x = () => {
                const deleteAllChildren = (parent) => {
                    while (parent.firstChild) {
                        parent.removeChild(parent.firstChild);
                    }
                }
                let createOptions = () => {
                    deleteAllChildren(select);
                    let selectedIdsDiv = document.getElementById("SelectedArtistsIds");
                    deleteAllChildren(selectedIdsDiv);
                    let placeholderOption = document.createElement("option");
                    placeholderOption.disabled = true;
                    placeholderOption.selected = true;
                    placeholderOption.value = "";
                    placeholderOption.text = "Select..."
                    select.appendChild(placeholderOption);
                    filter = input.value.toUpperCase();
                    options.forEach(o => {
                        txtValue = o.value;
                        if (o.selected == false && txtValue.toUpperCase().indexOf(filter) > -1) {
                            let option = document.createElement("option");
                            option.value = o.key;
                            option.text = o.value;
                            select.appendChild(option);
                        }
                        if (o.selected == true) {
                            let divhidden = document.createElement("input");
                            divhidden.type = "hidden";
                            divhidden.value = o.key;
                            divhidden.textContent = o.key;
                            divhidden.name = "ArtistsId";
                            selectedIdsDiv.appendChild(divhidden);
                        }
                    });
                }
                createOptions();
                select.onchange = (e) => {
                    options.forEach(o => o.selected = o.key == e.currentTarget.value ? true : o.selected);
                    createOptions();
                    updateSelectedOptionsDiv();
                    select.click();
                }

                let updateSelectedOptionsDiv = () => {
                    selectedOptionsContainer.innerHTML = null;
                    options.filter(o => o.selected).forEach(o => {
                        let selectedOptionSpan = document.createElement("span");
                        selectedOptionSpan.textContent = o.value;
                        selectedOptionSpan.value = o.key;
                        selectedOptionSpan.onclick = e => {
                            options.forEach(o => o.selected = o.key == e.currentTarget.value ? false : o.selected);
                            createOptions();
                            updateSelectedOptionsDiv();
                        }
                        selectedOptionsContainer.appendChild(selectedOptionSpan);
                    });
                }
                let wrapper = document.createElement("div");
                wrapper.className = "dropdown-multiple";
                wrapper.appendChild(select);


                let selectedOptionsContainer = document.getElementById("SelectedArtists");
                selectedOptionsContainer.className = "dropdown-multiple-selected-options";
                //wrapper.appendChild(selectedOptionsContainer);
                container.appendChild(wrapper);
            }
            let input = document.createElement("input");
            input.type = "text";
            input.placeholder = "choose the artists...";
            input.addEventListener("keyup", () => {
                x();
                select.click();
            });
            container.appendChild(input);
            
        })
}

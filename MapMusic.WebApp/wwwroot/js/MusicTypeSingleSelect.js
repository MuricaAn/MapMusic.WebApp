function SingleDropdown(divId, selectedID) {
    fetch(`/Event/GetMusicTypes`,
        {
            method: "get",
        }
    ).then(response => response.json())
        .then(categories => {
            var divMain = document.createElement('div');
            var h5 = document.createElement('h5');
            h5.innerHTML = 'Select Music Type:'
            divMain.appendChild(h5)
            var body = document.getElementById(divId);
            let select = document.getElementById("MusicTypeId");
            for (var i = 0; i < categories.length; i++) {
                let option = document.createElement('option');
                option.value = categories[i].id;
                option.text = categories[i].name;
                if (selectedID != null) {
                    if (categories[i].id == selectedID) {
                        option.selected = true;
                    }
                }
                select.appendChild(option);

            }
            divMain.appendChild(select);

            this.divMain = divMain;
            body.appendChild(this.divMain);
        })
}
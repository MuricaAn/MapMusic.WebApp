function changeDateFormat(dateString) {
    var parts = dateString.split("-");
    if (parts.length === 3) {
        return parts[1] + "/" + parts[2] + "/" + parts[0];
    } else {
        return dateString;
    }
}


function createInputs() {
    const urlParams = new URLSearchParams(window.location.search);
    const maxPriceForFilter = 500;
    var maxPriceFromUrl = maxPriceForFilter;
    var x = urlParams.get('maxPrice');
    var inputText = "No limit";
    if (urlParams.get('maxPrice') != "null" && urlParams.get('maxPrice') != null ) {
        maxPriceFromUrl = urlParams.get('maxPrice');
        inputText = maxPriceFromUrl.toString();
    }
    var startDateFromUrl = urlParams.get('startDate')
    var endDateFromUrl = urlParams.get('endDate')
    var musicTypesFromUrl = urlParams.get('musicTypesString')
    var newEvents = urlParams.get('newEvents');

    if (newEvents != false && newEvents != "false") {
        newEvents = true;
    }

    var startDateWrapper = new DOMParser().parseFromString(`<div><label>Start of the period</label><input class="input" type="date" name="startPeriod" id="startPeriod" value="${startDateFromUrl}"></div>`, "text/html").documentElement.getElementsByTagName("div")[0];
    var endDateWrapper = new DOMParser().parseFromString(`<div><label>End of the period</label><input class="input" type="date" name="endPeriod" id="endPeriod" value="${endDateFromUrl}"></div>`, "text/html").documentElement.getElementsByTagName("div")[0];
    var priceMaxWrapper = new DOMParser().parseFromString(`<div><label class="form-label">Max price</label><input type="range" class="form-range" min="0" max="${maxPriceForFilter}" step="10" value="${maxPriceFromUrl}" id="max-price"><span id="rangeValue">${inputText}</span></div>` , "text/html").documentElement.getElementsByTagName("div")[0];
    var buttonMaxWrapper = new DOMParser().parseFromString(`<div class="form-group">
                                <button class="btn btn-primary" id="filterbutton">Filter</button>
                            </div>` , "text/html").documentElement.getElementsByTagName("div")[0];


    var div = document.getElementsByClassName("filter-menu")[0];

    var startDateInput = startDateWrapper.getElementsByTagName("input")[0];
    var endDateInput = endDateWrapper.getElementsByTagName("input")[0];

    if (urlParams.get('startDate') != "null" && urlParams.get('startDate') != null) {
        endDateInput.min = startDateInput.value;
    }

    if (urlParams.get('endDate') != "null" && urlParams.get('endDate') != null) {
        startDateInput.max = endDateInput.value;
    }
    startDateInput.addEventListener("change", function (e) {
        startDateInput.value = e.target.value;
        endDateInput.min = startDateInput.value;
    });

    endDateInput.addEventListener("change", function (e) {
        endDateInput.value = e.target.value;
        startDateInput.max = e.target.value;
    });

    let divCreateCheckbox = document.createElement('div');
    divCreateCheckbox.innerHTML = "Choose music types:"
    let divMusicTypes = document.createElement('div');


    div.appendChild(startDateWrapper);
    div.appendChild(endDateWrapper);
    div.appendChild(priceMaxWrapper);
    div.appendChild(divCreateCheckbox);
    div.appendChild(divMusicTypes);
    div.appendChild(buttonMaxWrapper);

    divCreateCheckbox.addEventListener("click", function () {
        if (divMusicTypes.innerHTML == "") {
            fetch(`/Event/GetMusicTypes`,
                {
                    method: "get",
                }
            ).then(response => response.json())
                .then(categories => {
                    for (var category in categories) {
                        var divCheckbox = document.createElement("div");
                        var input = document.createElement("input");
                        input.name = "musicTypes";
                        input.type = "checkbox";
                        input.value = categories[category].id;
                        var label = document.createElement("label");
                        label.innerHTML = categories[category].name;
                        divCheckbox.appendChild(input);
                        divCheckbox.appendChild(label);
                        divMusicTypes.appendChild(divCheckbox);
                    }
                });
        }
        else {
            divMusicTypes.innerHTML = "";
        }
    });

    if (musicTypesFromUrl != '' && musicTypesFromUrl!=null) {
        var musicTypesInts = musicTypesFromUrl.split(',').map(Number);
        fetch(`/Event/GetMusicTypes`,
            {
                method: "get",
            }
        ).then(response => response.json())
            .then(categories => {
                for (var category in categories) {
                    var divCheckbox = document.createElement("div");
                    var input = document.createElement("input");
                    input.name = "musicTypes";
                    input.type = "checkbox";
                    input.value = categories[category].id;
                    if (musicTypesInts.includes(categories[category].id)) {
                        input.checked = true;
                    }
                    var label = document.createElement("label");
                    label.innerHTML = categories[category].name;
                    divCheckbox.appendChild(input);
                    divCheckbox.appendChild(label);
                    divMusicTypes.appendChild(divCheckbox);
                }
            });
    }
    var rangeinput = document.getElementById("max-price");
    var rangevaluespan = document.getElementById("rangeValue");
    var button = document.getElementById("filterbutton");

    rangeinput.addEventListener("input", function () {
        if (rangeinput.value == maxPriceForFilter) {
            rangevaluespan.innerHTML = "No limit";
        }
        else {
            rangevaluespan.innerHTML = rangeinput.value;
        }
    });

    var filterFunction = function () {
        var maxPrce = rangeinput.value;
        if (maxPrce == maxPriceForFilter) {
            maxPrce = null;
        }

        var url = `/Event/ShowEventsList?newEvents=${newEvents}&page=1&musicTypesString=`

        var inputsCheckbox = document.getElementsByName("musicTypes");
        var isElementBefore = false;
        inputsCheckbox.forEach(function (input) {
            if (input.checked) {
                if (isElementBefore) {
                    url += `,`;
                }
                url += `${input.value}`;
                isElementBefore = true;
            }

        });

        url += `&maxPrice=${maxPrce}&startDate=${startDateInput.value}&endDate=${endDateInput.value}`;


        window.location.href = url;
    }

    button.addEventListener("click", filterFunction);

    var keepFilterOptions = function () {
        var url = `/Event/ShowEventsList?newEvents=${newEvents}&page=1&musicTypesString=${musicTypesFromUrl}&maxPrice=${maxPriceFromUrl}&startDate=${startDateFromUrl}&endDate=${endDateFromUrl}`;
        window.location.href = url;
    }

    var upcomingEventsButton = document.getElementById("upcomingEvents");

    var changeNewEventsFlag = function (flag) {
        newEvents = flag;
        keepFilterOptions();
    }
    upcomingEventsButton.addEventListener("click", () => changeNewEventsFlag(true));

    var pastEventsButton = document.getElementById("pastEvents");
    pastEventsButton.addEventListener("click", () => changeNewEventsFlag(false));

    if (newEvents != false && newEvents != "false") {
        upcomingEventsButton.checked = true;
    }
    else {
        pastEventsButton.checked = true;
    }

}


function generatePageButtons(isNextPage) {
    const urlParams = new URLSearchParams(window.location.search);
    var maxPriceFromUrl = urlParams.get('maxPrice');
    var currentPage = urlParams.get('page');
    var startDateFromUrl = urlParams.get('startDate')
    var endDateFromUrl = urlParams.get('endDate')
    var musicTypesFromUrl = urlParams.get('musicTypesString')
    var newEvents = urlParams.get('newEvents');
    var divButtons = document.getElementById("page-buttons");
    if (currentPage == null) {
        currentPage = 1;
    }
    if (currentPage != 1) {
        var backButton = document.createElement("button");
        backButton.innerHTML = `${currentPage - 1}`;
        backButton.addEventListener("click", function () {
            var url = `/Event/ShowEventsList?newEvents=${newEvents}&page=${currentPage - 1}&musicTypesString=${musicTypesFromUrl}&maxPrice=${maxPriceFromUrl}&startDate=${startDateFromUrl}&endDate=${endDateFromUrl}`;

            window.location.href = url;
        });
        divButtons.appendChild(backButton);
    }
    var spanPage = document.createElement("span");
    spanPage.innerHTML = `${currentPage}`;
    divButtons.appendChild(spanPage);
    if (isNextPage) {
        var nextButton = document.createElement("button");
        nextButton.innerHTML = `${parseInt(currentPage) + 1}`;
        nextButton.addEventListener("click", function () {
            var url = `/Event/ShowEventsList?newEvents=${newEvents}&page=${parseInt(currentPage) + 1}&musicTypesString=${musicTypesFromUrl}&maxPrice=${maxPriceFromUrl}&startDate=${startDateFromUrl}&endDate=${endDateFromUrl}`;

            window.location.href = url;
        });
        divButtons.appendChild(nextButton);
    }

}
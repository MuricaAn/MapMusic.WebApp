function voteStars(divId, inputName, vote) {
    const deleteAllChildren = (parent) => {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
    let div = document.getElementById(divId);
    deleteAllChildren(div);
    let inputhidden = document.createElement("input");
    inputhidden.type = "hidden";
    inputhidden.value = vote;
    inputhidden.textContent = vote;
    inputhidden.name = inputName;
    inputhidden.id = `${divId}i`;
    div.appendChild(inputhidden);
    for (let i = 1; i <= vote; i++) {
        let st = document.createElement("span");
        st.className = "full-star material-symbols";
        st.innerHTML = "star";
        st.addEventListener("click", function () { voteStars(divId, inputName, i) });
        div.appendChild(st);

    }
    for (let i = vote + 1; i <= 5; i++) {
        let st = document.createElement("span");
        st.className = "empty-star material-symbols";
        st.innerHTML = "star";
        div.appendChild(st);
        st.addEventListener("click", function () { voteStars(divId, inputName, i) });
    }

    cancelButton = document.createElement("button");
    cancelButton.addEventListener("click", function () { createStars(divId, inputName) });
    cancelButton.innerHTML = "Cancel vote";
    div.appendChild(cancelButton);
}

function createStars(divId, inputName) {
    const deleteAllChildren = (parent) => {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
    let div = document.getElementById(divId);
    deleteAllChildren(div);
    let inputhidden = document.createElement("input");
    inputhidden.type = "hidden";
    inputhidden.value = 0;
    inputhidden.textContent = 0;
    inputhidden.name = inputName;
    inputhidden.id = `${divId}i`;
    div.appendChild(inputhidden);
    let st1 = document.createElement("span");
    st1.className = "empty-star material-symbols";
    st1.innerHTML = "star";
    st1.addEventListener("click", function () { voteStars(divId, inputName, 1) });
    let st2 = document.createElement("span");
    st2.className = "empty-star material-symbols";
    st2.innerHTML = "star";
    st2.addEventListener("click", function () { voteStars(divId, inputName, 2) });
    let st3 = document.createElement("span");
    st3.className = "empty-star material-symbols";
    st3.innerHTML = "star";
    st3.addEventListener("click", function () { voteStars(divId, inputName, 3) });
    let st4 = document.createElement("span");
    st4.className = "empty-star material-symbols";
    st4.innerHTML = "star";
    st4.addEventListener("click", function () { voteStars(divId, inputName, 4) });
    let st5 = document.createElement("span");
    st5.className = "empty-star material-symbols";
    st5.innerHTML = "star";
    st5.addEventListener("click", function () { voteStars(divId, inputName, 5) });
    div.appendChild(st1);
    div.appendChild(st2);
    div.appendChild(st3);
    div.appendChild(st4);
    div.appendChild(st5);
}


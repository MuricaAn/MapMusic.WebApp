function ShowOrganizersRequest(divName) {
    fetch('/GetAccountOrganizerRequests',
        {
            method : 'GET',
        }
    ).then(response => response.json())
        .then(organizersRequests => {
            let div = document.getElementById(divName);
            let allRequestsButton = document.createElement("button");
            let pendingRequestsButton = document.createElement("button");
            let rejectedRequestsButton = document.createElement("button");
            let status = {
                pending : 1,
                accepted : 2,
                rejected : 3
            }
            let statusColor = {
                [status.pending]: "pending",
                [status.accepted]: "accepted",
                [status.rejected] : "rejected"
            }
            let deleteAllChildren = (parent) => {
                while (parent.firstChild) {
                    parent.removeChild(parent.firstChild);
                }
            }
            let showElements = (elementsId) => {
                let div = document.getElementById("organizer-container");
                deleteAllChildren(div);
                for (let i = 0; i < organizersRequests.length; i++) {
                    if (elementsId.includes(organizersRequests[i].organizerRequestStatusId)) {
                        let organizerRequest = document.createElement("div");
                        organizerRequest.className = "organizer-card";
                        organizerRequest.classList.add(statusColor[organizersRequests[i].organizerRequestStatusId]);

                        let pId = document.createElement("p");
                        pId.innerHTML = organizersRequests[i].id;
                        organizerRequest.appendChild(pId);

                        let pName = document.createElement("p");
                        pName.innerHTML = organizersRequests[i].fullName;
                        organizerRequest.appendChild(pName);

                        let pEmail = document.createElement("p");
                        pEmail.innerHTML = organizersRequests[i].email;
                        organizerRequest.appendChild(pEmail);
                        
                        let pDescription = document.createElement("p");
                        pDescription.innerHTML = organizersRequests[i].description;
                        organizerRequest.appendChild(pDescription);

                        if (organizersRequests[i].organizerRequestStatusId == status.pending) {
                            let divButtons = document.createElement("div");
                            divButtons.className = "button-container";

                            let formAccept = document.createElement("form");
                            //formAccept.method = "post";
                            //formAccept.setAttribute("asp-controller", "Admin")
                            //formAccept.setAttribute("asp-action", "AcceptOrganizerRequest")

                            //formAccept.aspaction = "AcceptOrganizerRequest";
                            let inputAccept = document.createElement("input");
                            inputAccept.type = "hidden";
                            //inputAccept.name = "organizerRequestId";
                            //inputAccept.value = organizersRequests[i].id;
                            let buttonAccept = document.createElement("button");
                            //buttonAccept.type = "submit";
                            buttonAccept.innerHTML = "Accept";
                            buttonAccept.addEventListener("click", () => {
                                fetch(`/Admin/AcceptOrganizerRequest?organizerRequestId=${organizersRequests[i].id}`, {
                                    method: 'POST'

                                }).then(response => response.json())
                                    .then(response => { debugger; })

                            })
                            formAccept.appendChild(inputAccept);
                            formAccept.appendChild(buttonAccept);

                            let formReject = document.createElement("form");
                            formReject.method = "post";
                            formReject.setAttribute("asp-controller" , "Admin")
                            formReject.aspaction = "RejectOrganizerRequest";
                            let inputReject = document.createElement("input");
                            inputReject.type = "hidden";
                            inputReject.name = "organizerRequestId";
                            inputReject.value = organizersRequests[i].id;
                            let buttonReject = document.createElement("button");
                            //buttonReject.type = "submit";
                            buttonReject.innerHTML = "Reject";
                            buttonReject.addEventListener("click", () => {
                                fetch(`/Admin/RejectOrganizerRequest?organizerRequestId=${organizersRequests[i].id}`, {
                                    method: 'POST'
                                })
                            })                                    ;
                            formReject.appendChild(inputReject);
                            formReject.appendChild(buttonReject);
                            divButtons.appendChild(formAccept);
                            divButtons.appendChild(formReject);

                            organizerRequest.appendChild(divButtons);
                        }
                        div.appendChild(organizerRequest);
                    }
                }
            }
            showElements([status.pending, status.accepted, status.rejected]);
            let divButtons = document.getElementById("filter-buttons");
            allRequestsButton.innerHTML = "All Requests";
            pendingRequestsButton.innerHTML = "Pending Requests";
            rejectedRequestsButton.innerHTML = "Rejected Requests";
            allRequestsButton.addEventListener("click", () => showElements([status.pending, status.accepted, status.rejected]));
            pendingRequestsButton.addEventListener("click", () => showElements([status.pending]));
            rejectedRequestsButton.addEventListener("click", () => showElements([status.rejected]));
            divButtons.appendChild(allRequestsButton);
            divButtons.appendChild(pendingRequestsButton);
            divButtons.appendChild(rejectedRequestsButton);
            
        })

}
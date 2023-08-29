function ShowArtistInvitations(divName) {
    fetch('/Artist/ShowArtistInvitations',
        {
            method: 'GET',
        }
    ).then(response => response.json())
        .then(artistInvitations => {
            let div = document.getElementById(divName);
            let allRequestsButton = document.createElement("button");
            let pendingRequestsButton = document.createElement("button");
            let rejectedRequestsButton = document.createElement("button");
            let status = {
                pending: 1,
                accepted: 2,
                rejected: 3
            }
            let statusColor = {
                [status.pending]: "pending",
                [status.accepted]: "accepted",
                [status.rejected]: "rejected"
            }
            let deleteAllChildren = (parent) => {
                while (parent.firstChild) {
                    parent.removeChild(parent.firstChild);
                }
            }
            let showElements = (elementsId) => {
                let div = document.getElementById("organizer-container");
                deleteAllChildren(div);
                for (let i = 0; i < artistInvitations.length; i++) {
                    if (elementsId.includes(artistInvitations[i].organizerArtistInvitationStatusId)) {
                        let organizerRequest = document.createElement("div");
                        organizerRequest.className = "organizer-card";
                        organizerRequest.classList.add(statusColor[artistInvitations[i].organizerArtistInvitationStatusId]);

                        let pId = document.createElement("p");
                        pId.innerHTML = artistInvitations[i].id;
                        organizerRequest.appendChild(pId);

                        let pEventName = document.createElement("p");
                        pEventName.innerHTML = `You are invited at ${artistInvitations[i].eventName}`;
                        organizerRequest.appendChild(pEventName);

                        let pOrganizerName = document.createElement("p");
                        pOrganizerName.innerHTML = `Organized by ${artistInvitations[i].organizerFullName}`;
                        organizerRequest.appendChild(pOrganizerName);

                        let pDate = document.createElement("p");

                        const isoString = artistInvitations[i].startDate;
                        const date = new Date(isoString);

                        const options = {
                            day: '2-digit',
                            month: '2-digit',
                            hour: '2-digit',
                            minute: '2-digit',
                            hour12: false
                        };
                        const formatter = new Intl.DateTimeFormat('default', options);
                        const formattedDate = formatter.format(date);

                        pDate.innerHTML = `On ${formattedDate}`;
                        organizerRequest.appendChild(pDate);

                        if (artistInvitations[i].organizerArtistInvitationStatusId == status.pending) {
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
                                fetch(`/Artist/AcceptOrganizerArtistInvitation?organizerRequestId=${artistInvitations[i].id}`, {
                                    method: 'POST'

                                }).then(response => response.json())
                                    .then(response => { debugger; })

                            })
                            formAccept.appendChild(inputAccept);
                            formAccept.appendChild(buttonAccept);

                            let formReject = document.createElement("form");
                            formReject.method = "post";
                            formReject.setAttribute("asp-controller", "Admin")
                            formReject.aspaction = "RejectOrganizerRequest";
                            let inputReject = document.createElement("input");
                            inputReject.type = "hidden";
                            inputReject.name = "organizerRequestId";
                            inputReject.value = artistInvitations[i].id;
                            let buttonReject = document.createElement("button");
                            //buttonReject.type = "submit";
                            buttonReject.innerHTML = "Reject";
                            buttonReject.addEventListener("click", () => {
                                fetch(`/Artist/RejectOrganizerArtistInvitation?organizerRequestId=${artistInvitations[i].id}`, {
                                    method: 'POST'
                                })
                            });
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
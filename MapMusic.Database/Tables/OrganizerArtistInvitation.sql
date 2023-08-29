CREATE TABLE OrganizerArtistInvitation (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    OrganizerId INT NOT NULL,
    ArtistId INT NOT NULL,
    EventId INT NOT NULL,
    OrganizerArtistInvitationStatusId INT NOT NULL,

    FOREIGN KEY (OrganizerId) REFERENCES Organizer (Id),
    FOREIGN KEY (ArtistId) REFERENCES Artist (Id),
    FOREIGN KEY (EventId) REFERENCES Event (Id),
    FOREIGN KEY (OrganizerArtistInvitationStatusId) REFERENCES OrganizerArtistInvitationStatus (Id)
);

GO

CREATE NONCLUSTERED INDEX ix_OrganizerArtistInvitation1
ON OrganizerArtistInvitation (OrganizerId);

GO

CREATE NONCLUSTERED INDEX ix_OrganizerArtistInvitation2
ON OrganizerArtistInvitation (ArtistId);

GO

CREATE NONCLUSTERED INDEX ix_OrganizerArtistInvitation3
ON OrganizerArtistInvitation (EventId);

GO

CREATE NONCLUSTERED INDEX ix_OrganizerArtistInvitation4
ON OrganizerArtistInvitation (OrganizerArtistInvitationStatusId);
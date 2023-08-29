CREATE TABLE OrganizerArtistInvitationStatus (
    Id int PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT Unique_OrganizerArtistInvitationStatus UNIQUE (Name)
);
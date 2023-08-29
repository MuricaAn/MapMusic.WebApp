CREATE TABLE Event (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    OrganizerId INT NOT NULL,
    LocationId INT NOT NULL,
    MusicTypeId INT NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(2500),
    Price DECIMAL(10, 2) NOT NULL,
    StartDate DATETIMEOFFSET NOT NULL,
    EndDate DATETIMEOFFSET NOT NULL,
    IsDeleted BIT DEFAULT 0 NOT NULL,

    FOREIGN KEY (OrganizerId) REFERENCES Organizer (Id),
    FOREIGN KEY (LocationId) REFERENCES Location (Id),
    FOREIGN KEY (MusicTypeId) REFERENCES MusicType (Id)
);

GO
CREATE NONCLUSTERED INDEX IX_EventName
ON Event (Name);
GO
CREATE NONCLUSTERED INDEX IX_EventOrganizer
ON Event (OrganizerId);
GO
CREATE NONCLUSTERED INDEX IX_EventLocation
ON Event (LocationId);
GO
CREATE NONCLUSTERED INDEX IX_EventMusic
ON Event (MusicTypeId);
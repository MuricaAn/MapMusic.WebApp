CREATE TABLE ArtistEvent (
    ArtistId INT NOT NULL,
    EventId INT NOT NULL,

    CONSTRAINT PK_ArtistEvent PRIMARY KEY (ArtistId, EventId),
    FOREIGN KEY (ArtistId) REFERENCES Artist (Id),
    FOREIGN KEY (EventId) REFERENCES Event (Id)
);
GO
CREATE NONCLUSTERED INDEX IX_ArtistEvent1
ON ArtistEvent (ArtistId);
GO
CREATE NONCLUSTERED INDEX IX_ArtistEvent2
ON ArtistEvent (EventId);

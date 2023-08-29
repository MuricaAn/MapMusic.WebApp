CREATE TABLE PhotoEvent (
    EventId INT,
    PhotoId INT,
    Description NVARCHAR(50),

    CONSTRAINT PK_PhotoEvent PRIMARY KEY (EventId, PhotoId),
    FOREIGN KEY (EventId) REFERENCES Event (Id) ON DELETE CASCADE,
    FOREIGN KEY (PhotoId) REFERENCES Photo (Id) ON DELETE CASCADE
);
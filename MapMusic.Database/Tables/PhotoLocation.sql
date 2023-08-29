CREATE TABLE PhotoLocation (
    LocationId INT,
    PhotoId INT,
    Description NVARCHAR(50),

    CONSTRAINT PK_PhotoLocation PRIMARY KEY (LocationId, PhotoId),
    FOREIGN KEY (LocationId) REFERENCES Location (Id) ON DELETE CASCADE,
    FOREIGN KEY (PhotoId) REFERENCES Photo (Id) ON DELETE CASCADE
);
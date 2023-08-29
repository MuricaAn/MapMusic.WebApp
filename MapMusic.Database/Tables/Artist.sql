CREATE TABLE Artist (
    ID INT IDENTITY(1, 1) PRIMARY KEY,
    CredentialID INT NOT NULL,
    PhotoID INT,
    ArtistTypeID INT NOT NULL,
    StageName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    IsDeleted BIT DEFAULT 0 NOT NULL,

    FOREIGN KEY (CredentialID) REFERENCES Credential (ID),
    FOREIGN KEY (PhotoID) REFERENCES Photo (ID),
    FOREIGN KEY (ArtistTypeID) REFERENCES ArtistType (ID)
);
GO

CREATE NONCLUSTERED INDEX IX_ArtistName
ON Artist (StageName);
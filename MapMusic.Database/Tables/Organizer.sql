CREATE TABLE Organizer (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    CredentialId INT NOT NULL,
    PhotoId INT,
    FullName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    IsDeleted BIT DEFAULT 0 NOT NULL,

    FOREIGN KEY (CredentialId) REFERENCES Credential (Id),
    FOREIGN KEY (PhotoId) REFERENCES Photo (Id)
);
GO
CREATE NONCLUSTERED INDEX ix_Organizer
ON Organizer (CredentialId);
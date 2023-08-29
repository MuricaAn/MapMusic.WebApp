CREATE TABLE [User] (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    CredentialId INT NOT NULL,
    PhotoId INT,
    RoleId INT NOT NULL,
    BirthDate DATE,
    IsDeleted BIT DEFAULT 0 NOT NULL,

    FOREIGN KEY (CredentialId) REFERENCES Credential (Id),
    FOREIGN KEY (PhotoId) REFERENCES Photo (Id),
    FOREIGN KEY (RoleId) REFERENCES Role (Id)
);
GO
CREATE NONCLUSTERED INDEX ix_User1
ON [User] (CredentialId);
GO
CREATE NONCLUSTERED INDEX ix_User2
ON [User] (RoleId);
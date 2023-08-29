CREATE TABLE Credential (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Email NVARCHAR(200) NOT NULL,
    PasswordHash NVARCHAR(200) NOT NULL,
    IsDeleted BIT DEFAULT 0 NOT NULL,
    
    CONSTRAINT Unique_Email UNIQUE (Email)
);
GO
CREATE NONCLUSTERED INDEX IX_CredentialsEmail
ON Credential (Email);
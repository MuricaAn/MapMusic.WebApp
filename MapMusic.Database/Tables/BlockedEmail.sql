CREATE TABLE BlockedEmail (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Email NVARCHAR(50),

    CONSTRAINT UC_EmailBlocked UNIQUE (Email)
);
GO
CREATE NONCLUSTERED INDEX IX_BlockedEmail
ON BlockedEmail (Email);
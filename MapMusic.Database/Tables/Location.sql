CREATE TABLE Location (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    Description NVARCHAR(1000),
    Latitude DECIMAL(5, 5) ,
    Longitude DECIMAL(5, 5) ,
    IsDeleted BIT DEFAULT 0 NOT NULL,

    CONSTRAINT Unique_Name UNIQUE (Name)
);
GO
CREATE NONCLUSTERED INDEX ix_Location
ON Location (Name);

CREATE TABLE OrganizerRequest (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    OrganizerRequestStatusId INT NOT NULL,
    FullName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    Email NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,

    FOREIGN KEY (OrganizerRequestStatusId) REFERENCES OrganizerRequestStatus (Id)
);
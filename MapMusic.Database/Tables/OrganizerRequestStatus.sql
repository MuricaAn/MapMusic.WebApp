CREATE TABLE OrganizerRequestStatus (
    Id int PRIMARY KEY,
    Name NVARCHAR(50),

    CONSTRAINT Unique_OrganizerRequestName UNIQUE (Name)
);
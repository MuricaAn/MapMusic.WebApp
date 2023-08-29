CREATE TABLE Rating (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    UserId INT NOT NULL,
    EventId INT NOT NULL,
    RatingLocation INT NOT NULL,
    RatingOrganization INT NOT NULL,
    Comment NVARCHAR(500),

    CONSTRAINT UC_UserId_EventId UNIQUE (UserId, EventId),
    FOREIGN KEY (EventId) REFERENCES Event (Id),
    FOREIGN KEY (UserId) REFERENCES [User] (Id)
);

GO

CREATE NONCLUSTERED INDEX ix_Rating1
ON Rating (UserId);

GO

CREATE NONCLUSTERED INDEX ix_Rating2
ON Rating (EventId);
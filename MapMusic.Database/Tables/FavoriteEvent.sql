CREATE TABLE FavoriteEvent (
    UserId INT,
    EventId INT,

    CONSTRAINT PK_Grade PRIMARY KEY (UserId, EventId),
    FOREIGN KEY (UserId) REFERENCES [User] (Id),
    FOREIGN KEY (EventId) REFERENCES Event (Id)
);

GO

CREATE NONCLUSTERED INDEX favoriteevent1
ON FavoriteEvent (UserId);

GO

CREATE NONCLUSTERED INDEX favoriteevent
ON FavoriteEvent (EventId);
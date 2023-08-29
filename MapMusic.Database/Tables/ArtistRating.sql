CREATE TABLE ArtistRating (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    RatingId INT NOT NULL,
    ArtistId INT NOT NULL,
    Rating INT NOT NULL,

    CONSTRAINT UC_ArtistRating UNIQUE (RatingId, ArtistId),
    FOREIGN KEY (RatingId) REFERENCES Rating (Id) ON DELETE CASCADE,
    FOREIGN KEY (ArtistId) REFERENCES Artist (Id)
);
GO
CREATE NONCLUSTERED INDEX IX_ArtistRating
ON ArtistRating (ArtistId);


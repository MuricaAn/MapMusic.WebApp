﻿CREATE TABLE Role (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    CONSTRAINT Unique_Role UNIQUE (Name)
);
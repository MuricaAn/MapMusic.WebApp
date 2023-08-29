CREATE NONCLUSTERED INDEX IX_BlockedEmail
ON Blocked (Email);

Create nonclustered INDEX IX_ArtistName
on Artist (StageNAme);

Create nonclustered INDEX IX_ArtistEvent1
on ArtistEvent (ArtistId);

Create nonclustered INDEX IX_ArtistEvent2
on ArtistEvent (EventId);

Create nonclustered INDEX IX_ArtistRating
on ArtistRating (ArtistId);

CREATE NONCLUSTERED INDEX IX_CredentialsEmail
ON Credential (Email);

Create nonclustered INDEX IX_EventName
on event (name);
 

Create nonclustered INDEX IX_EventOrganizer
on event (OrganizerId);

Create nonclustered INDEX IX_EventLocation
on event (LocationId);

Create nonclustered INDEX IX_EventMusic
on event (MusicTypeId);

Create nonclustered INDEX favoriteevent1
on favoriteevent (UserId);

Create nonclustered INDEX favoriteevent
on favoriteevent (EventId);

Create nonclustered INDEX ix_Location
on Location (name);

Create nonclustered INDEX ix_Organizer
on Organizer (CredentialID);

Create nonclustered INDEX ix_OrganizerArtistInvitation1
on OrganizerArtistInvitation (OrganizerId);

Create nonclustered INDEX ix_OrganizerArtistInvitation2
on OrganizerArtistInvitation (ArtistId);

Create nonclustered INDEX ix_OrganizerArtistInvitation3
on OrganizerArtistInvitation (EventId);

Create nonclustered INDEX ix_OrganizerArtistInvitation4
on OrganizerArtistInvitation (OrganizerArtistInvitationStatusId);


Create nonclustered INDEX ix_Rating1
on Rating (UserId);

Create nonclustered INDEX ix_Rating2
on Rating (EventId);

Create nonclustered INDEX ix_User1
on [User] (CredentialId);

Create nonclustered INDEX ix_User2
on [User] (RoleId);
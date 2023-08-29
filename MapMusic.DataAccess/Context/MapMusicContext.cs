using System;
using System.Collections.Generic;
using MapMusic.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace MapMusic.Entities;

public partial class MapMusicContext : DbContext
{
    public MapMusicContext()
    {
    }

    public MapMusicContext(DbContextOptions<MapMusicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<ArtistRating> ArtistRatings { get; set; }

    public virtual DbSet<ArtistType> ArtistTypes { get; set; }

    public virtual DbSet<BlockedEmail> BlockedEmails { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MusicType> MusicTypes { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<OrganizerArtistInvitation> OrganizerArtistInvitations { get; set; }

    public virtual DbSet<OrganizerArtistInvitationStatus> OrganizerArtistInvitationStatuses { get; set; }

    public virtual DbSet<OrganizerRequest> OrganizerRequests { get; set; }

    public virtual DbSet<OrganizerRequestStatus> OrganizerRequestStatuses { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhotoEvent> PhotoEvents { get; set; }

    public virtual DbSet<PhotoLocation> PhotoLocations { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwSearcheableEntity> VwSearcheableEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Trusted_connection=True;Database=MapMusic;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Artist__3214EC27C11384EA");

            entity.ToTable("Artist");

            entity.HasIndex(e => e.StageName, "IX_ArtistName");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ArtistTypeId).HasColumnName("ArtistTypeID");
            entity.Property(e => e.CredentialId).HasColumnName("CredentialID");
            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.StageName).HasMaxLength(50);

            entity.HasOne(d => d.ArtistType).WithMany(p => p.Artists)
                .HasForeignKey(d => d.ArtistTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Artist__ArtistTy__6EF57B66");

            entity.HasOne(d => d.Credential).WithMany(p => p.Artists)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Artist__Credenti__6D0D32F4");

            entity.HasOne(d => d.Photo).WithMany(p => p.Artists)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Artist__PhotoID__6E01572D");

            entity.HasMany(d => d.Events).WithMany(p => p.Artists)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistEvent",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK__ArtistEve__Event__70DDC3D8"),
                    l => l.HasOne<Artist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArtistEve__Artis__6FE99F9F"),
                    j =>
                    {
                        j.HasKey("ArtistId", "EventId");
                        j.ToTable("ArtistEvent");
                        j.HasIndex(new[] { "ArtistId" }, "IX_ArtistEvent1");
                        j.HasIndex(new[] { "EventId" }, "IX_ArtistEvent2");
                    });
        });

        modelBuilder.Entity<ArtistRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ArtistRa__3214EC072398A0FF");

            entity.ToTable("ArtistRating");

            entity.HasIndex(e => e.ArtistId, "IX_ArtistRating");

            entity.HasIndex(e => new { e.RatingId, e.ArtistId }, "UC_ArtistRating").IsUnique();

            entity.HasOne(d => d.Artist).WithMany(p => p.ArtistRatings)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ArtistRat__Artis__72C60C4A");

            entity.HasOne(d => d.RatingNavigation).WithMany(p => p.ArtistRatings)
                .HasForeignKey(d => d.RatingId)
                .HasConstraintName("FK__ArtistRat__Ratin__71D1E811");
        });

        modelBuilder.Entity<ArtistType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ArtistTy__3214EC07E1DD9B75");

            entity.ToTable("ArtistType");

            entity.HasIndex(e => e.Name, "Unique_ArtistType").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<BlockedEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlockedE__3214EC07458A0CF3");

            entity.ToTable("BlockedEmail");

            entity.HasIndex(e => e.Email, "IX_BlockedEmail");

            entity.HasIndex(e => e.Email, "UC_EmailBlocked").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Credenti__3214EC071BDF32D7");

            entity.ToTable("Credential");

            entity.HasIndex(e => e.Email, "IX_CredentialsEmail");

            entity.HasIndex(e => e.Email, "Unique_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC07B330ECBB");

            entity.ToTable("Event");

            entity.HasIndex(e => e.LocationId, "IX_EventLocation");

            entity.HasIndex(e => e.MusicTypeId, "IX_EventMusic");

            entity.HasIndex(e => e.Name, "IX_EventName");

            entity.HasIndex(e => e.OrganizerId, "IX_EventOrganizer");

            entity.Property(e => e.Description).HasMaxLength(2500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Location).WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__LocationI__74AE54BC");

            entity.HasOne(d => d.MusicType).WithMany(p => p.Events)
                .HasForeignKey(d => d.MusicTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__MusicType__75A278F5");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__Organizer__73BA3083");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07A357E26F");

            entity.ToTable("Location");

            entity.HasIndex(e => e.Name, "Unique_Name").IsUnique();

            entity.HasIndex(e => e.Name, "ix_Location");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MusicType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MusicTyp__3214EC07C376C1EB");

            entity.ToTable("MusicType");

            entity.HasIndex(e => e.Name, "Unique_MusicType").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC075A76A35A");

            entity.ToTable("Organizer");

            entity.HasIndex(e => e.CredentialId, "ix_Organizer");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FullName).HasMaxLength(50);

            entity.HasOne(d => d.Credential).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Crede__787EE5A0");

            entity.HasOne(d => d.Photo).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Photo__797309D9");
        });

        modelBuilder.Entity<OrganizerArtistInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC0790FB6B6E");

            entity.ToTable("OrganizerArtistInvitation");

            entity.HasIndex(e => e.OrganizerId, "ix_OrganizerArtistInvitation1");

            entity.HasIndex(e => e.ArtistId, "ix_OrganizerArtistInvitation2");

            entity.HasIndex(e => e.EventId, "ix_OrganizerArtistInvitation3");

            entity.HasIndex(e => e.OrganizerArtistInvitationStatusId, "ix_OrganizerArtistInvitation4");

            entity.HasOne(d => d.Artist).WithMany(p => p.OrganizerArtistInvitations)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Artis__7B5B524B");

            entity.HasOne(d => d.Event).WithMany(p => p.OrganizerArtistInvitations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Organizer__Event__7C4F7684");

            entity.HasOne(d => d.OrganizerArtistInvitationStatus).WithMany(p => p.OrganizerArtistInvitations)
                .HasForeignKey(d => d.OrganizerArtistInvitationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Organ__7D439ABD");

            entity.HasOne(d => d.Organizer).WithMany(p => p.OrganizerArtistInvitations)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Organ__7A672E12");
        });

        modelBuilder.Entity<OrganizerArtistInvitationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC078AE97160");

            entity.ToTable("OrganizerArtistInvitationStatus");

            entity.HasIndex(e => e.Name, "Unique_OrganizerArtistInvitationStatus").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<OrganizerRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC075D136134");

            entity.ToTable("OrganizerRequest");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.OrganizerRequestStatus).WithMany(p => p.OrganizerRequests)
                .HasForeignKey(d => d.OrganizerRequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Organ__7E37BEF6");
        });

        modelBuilder.Entity<OrganizerRequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC07ED9D4855");

            entity.ToTable("OrganizerRequestStatus");

            entity.HasIndex(e => e.Name, "Unique_OrganizerRequestName").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Photo__3214EC07F9820EF6");

            entity.ToTable("Photo");

            entity.Property(e => e.CreatedOn).HasColumnType("date");
        });

        modelBuilder.Entity<PhotoEvent>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.PhotoId });

            entity.ToTable("PhotoEvent");

            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.PhotoEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__PhotoEven__Event__7F2BE32F");

            entity.HasOne(d => d.Photo).WithMany(p => p.PhotoEvents)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__PhotoEven__Photo__00200768");
        });

        modelBuilder.Entity<PhotoLocation>(entity =>
        {
            entity.HasKey(e => new { e.LocationId, e.PhotoId });

            entity.ToTable("PhotoLocation");

            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasOne(d => d.Location).WithMany(p => p.PhotoLocations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__PhotoLoca__Locat__01142BA1");

            entity.HasOne(d => d.Photo).WithMany(p => p.PhotoLocations)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__PhotoLoca__Photo__02084FDA");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC07BA9CFD27");

            entity.ToTable("Rating");

            entity.HasIndex(e => new { e.UserId, e.EventId }, "UC_UserId_EventId").IsUnique();

            entity.HasIndex(e => e.UserId, "ix_Rating1");

            entity.HasIndex(e => e.EventId, "ix_Rating2");

            entity.Property(e => e.Comment).HasMaxLength(500);

            entity.HasOne(d => d.Event).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Rating__EventId__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__UserId__03F0984C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC078BE6E846");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "Unique_Role").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07833A32B0");

            entity.ToTable("User");

            entity.HasIndex(e => e.CredentialId, "ix_User1");

            entity.HasIndex(e => e.RoleId, "ix_User2");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Credential).WithMany(p => p.Users)
                .HasForeignKey(d => d.CredentialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Credential__04E4BC85");

            entity.HasOne(d => d.Photo).WithMany(p => p.Users)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__PhotoId__05D8E0BE");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__06CD04F7");

            entity.HasMany(d => d.Events).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "FavoriteEvent",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK__FavoriteE__Event__778AC167"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FavoriteE__UserI__76969D2E"),
                    j =>
                    {
                        j.HasKey("UserId", "EventId").HasName("PK_Grade");
                        j.ToTable("FavoriteEvent");
                        j.HasIndex(new[] { "EventId" }, "favoriteevent");
                        j.HasIndex(new[] { "UserId" }, "favoriteevent1");
                    });
        });

        modelBuilder.Entity<VwSearcheableEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSearcheableEntities");

            entity.Property(e => e.EntityType)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

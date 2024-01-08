using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MAL2018_Assessment2.Models
{
    public partial class COMP2001MAL_CJiasinContext : DbContext
    {
        public COMP2001MAL_CJiasinContext()
        {
        }

        public COMP2001MAL_CJiasinContext(DbContextOptions<COMP2001MAL_CJiasinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookMark> BookMarks { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<ProfileMark> ProfileMarks { get; set; } = null!;
        public virtual DbSet<Trail> Trails { get; set; } = null!;
        public virtual DbSet<TrailActivity> TrailActivities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001MAL_CJiasin;User Id=CJiasin;Password=BgeX333*;Encrypt=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookMark>(entity =>
            {
                entity.ToTable("BookMark", "CW1");

                entity.Property(e => e.BookMarkId).HasColumnName("BookMarkID");

                entity.Property(e => e.BookMarkDescription).HasColumnType("text");

                entity.Property(e => e.TimePeriod)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.TrailActivitiesId).HasColumnName("TrailActivitiesID");

                entity.HasOne(d => d.TrailActivities)
                    .WithMany(p => p.BookMarks)
                    .HasForeignKey(d => d.TrailActivitiesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookMark__TrailA__5E54FF49");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "CW1");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.ProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Profile__UserID__5B78929E");
            });

            modelBuilder.Entity<ProfileMark>(entity =>
            {
                entity.ToTable("ProfileMark", "CW1");

                entity.Property(e => e.ProfileMarkId).HasColumnName("ProfileMarkID");

                entity.Property(e => e.BookMarkId).HasColumnName("BookMarkID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.HasOne(d => d.BookMark)
                    .WithMany(p => p.ProfileMarks)
                    .HasForeignKey(d => d.BookMarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProfileMa__BookM__6225902D");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileMarks)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProfileMa__Profi__61316BF4");
            });

            modelBuilder.Entity<Trail>(entity =>
            {
                entity.ToTable("Trail", "CW1");

                entity.Property(e => e.TrailId).HasColumnName("TrailID");

                entity.Property(e => e.EstimatedTime)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrailLength)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrailLocation)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrailActivity>(entity =>
            {
                entity.HasKey(e => e.TrailActivitiesId)
                    .HasName("PK__TrailAct__0AB848254FC3314E");

                entity.ToTable("TrailActivities", "CW1");

                entity.Property(e => e.TrailActivitiesId).HasColumnName("TrailActivitiesID");

                entity.Property(e => e.ActivitiesDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ActivitiesType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.TrailId).HasColumnName("TrailID");

                entity.HasOne(d => d.Trail)
                    .WithMany(p => p.TrailActivities)
                    .HasForeignKey(d => d.TrailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrailActi__Activ__57A801BA");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "CW1");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534267F6060")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

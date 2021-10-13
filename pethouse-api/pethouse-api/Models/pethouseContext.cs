using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Internal;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pethouse_api.Models
{
    public partial class pethouseContext : DbContext
    {
        public pethouseContext()
        {
        }

        public pethouseContext(DbContextOptions<pethouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Breeds> Breeds { get; set; }
        public virtual DbSet<Grooming> Grooming { get; set; }
        public virtual DbSet<Medications> Medications { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Races> Races { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vaccines> Vaccines { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:sqlservermyn56mvi6a25y.database.windows.net,1433;Initial Catalog=pethouse;Persist Security Info=False;User ID=sakling;Password=!OmaAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breeds>(entity =>
            {
                entity.HasKey(e => e.BreedId)
                    .HasName("PK__Breeds__648FABF338BC06B8");

                entity.Property(e => e.BreedId).HasColumnName("Breed_id");

                entity.Property(e => e.Breedname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RaceId).HasColumnName("Race_id");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.Breeds)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Breeds__Race_id__286302EC");
            });

            modelBuilder.Entity<Grooming>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GroomDate).HasColumnType("date");

                entity.Property(e => e.GroomExpDate).HasColumnType("date");

                entity.Property(e => e.Groomname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PetId).HasColumnName("Pet_id");

                entity.HasOne(d => d.Pet)
                    .WithMany()
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grooming__Pet_id__32E0915F");
            });

            modelBuilder.Entity<Medications>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.MedDate).HasColumnType("date");

                entity.Property(e => e.MedExpDate).HasColumnType("date");

                entity.Property(e => e.Medname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PetId).HasColumnName("Pet_id");

                entity.HasOne(d => d.Pet)
                    .WithMany()
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Medicatio__Pet_i__30F848ED");
            });

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.HasKey(e => e.PetId)
                    .HasName("PK__Pets__157F25C6A4EF0C1C");

                entity.Property(e => e.PetId).HasColumnName("Pet_id");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.BreedId).HasColumnName("Breed_id");

                entity.Property(e => e.Petname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RaceId).HasColumnName("Race_id");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.BreedId)
                    .HasConstraintName("FK__Pets__Breed_id__2D27B809");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.RaceId)
                    .HasConstraintName("FK__Pets__Race_id__2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pets__User_id__2B3F6F97");
            });

            modelBuilder.Entity<Races>(entity =>
            {
                entity.HasKey(e => e.RaceId)
                    .HasName("PK__Races__BF0D0C4F81280BAF");

                entity.Property(e => e.RaceId)
                    .HasColumnName("Race_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Racename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__206A9DF864D32BCD");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vaccines>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.PetId).HasColumnName("Pet_id");

                entity.Property(e => e.VacDate).HasColumnType("date");

                entity.Property(e => e.VacExpDate).HasColumnType("date");

                entity.Property(e => e.Vacname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pet)
                    .WithMany()
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vaccines__Pet_id__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project2.Models
{
    public partial class Project2Context : DbContext
    {
        public Project2Context()
        {
        }

        public Project2Context(DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Elan> Elans { get; set; }
        public virtual DbSet<Nov> Novs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Rayon> Rayons { get; set; }
        public virtual DbSet<Rey> Reys { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Seher> Sehers { get; set; }
        public virtual DbSet<Sekil> Sekils { get; set; }
        public virtual DbSet<Sk> Sks { get; set; }
        public virtual DbSet<Tip> Tips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-DE842EDO3NN\\SQLEXPRESS;Database=Project2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Elan>(entity =>
            {
                entity.ToTable("Elan");

                entity.Property(e => e.ElanSkid).HasColumnName("ElanSKId");

                entity.Property(e => e.ElanUnvan).HasMaxLength(100);

                entity.Property(e => e.ElanVaxt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ElanNov)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanNovId)
                    .HasConstraintName("FK__Elan__ElanNovId__778AC167");

                entity.HasOne(d => d.ElanPerson)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Elan__ElanPerson__787EE5A0");

                entity.HasOne(d => d.ElanRayon)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanRayonId)
                    .HasConstraintName("FK__Elan__ElanRayonI__7C4F7684");

                entity.HasOne(d => d.ElanSeher)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanSeherId)
                    .HasConstraintName("FK__Elan__ElanSeherI__7B5B524B");

                entity.HasOne(d => d.ElanSk)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanSkid)
                    .HasConstraintName("FK__Elan__ElanSKId__797309D9");

                entity.HasOne(d => d.ElanTip)
                    .WithMany(p => p.Elans)
                    .HasForeignKey(d => d.ElanTipId)
                    .HasConstraintName("FK__Elan__ElanTipId__7A672E12");
            });

            modelBuilder.Entity<Nov>(entity =>
            {
                entity.ToTable("Nov");

                entity.Property(e => e.NovAd).HasMaxLength(10);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonAd).HasMaxLength(20);

                entity.Property(e => e.PersonEmail).HasMaxLength(30);

                entity.Property(e => e.PersonIstifadeciAdi).HasMaxLength(30);

                entity.Property(e => e.PersonNomre).HasMaxLength(20);

                entity.Property(e => e.PersonParol).HasMaxLength(30);

                entity.Property(e => e.PersonSoyad).HasMaxLength(30);

                entity.HasOne(d => d.PersonRol)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.PersonRolId)
                    .HasConstraintName("FK__Person__PersonRo__74AE54BC");
            });

            modelBuilder.Entity<Rayon>(entity =>
            {
                entity.ToTable("Rayon");

                entity.Property(e => e.RayonAd).HasMaxLength(20);

                entity.HasOne(d => d.RayonSeher)
                    .WithMany(p => p.Rayons)
                    .HasForeignKey(d => d.RayonSeherId)
                    .HasConstraintName("FK__Rayon__RayonSehe__6C190EBB");
            });

            modelBuilder.Entity<Rey>(entity =>
            {
                entity.ToTable("Rey");

                entity.HasOne(d => d.ReyElan)
                    .WithMany(p => p.Reys)
                    .HasForeignKey(d => d.ReyElanId)
                    .HasConstraintName("FK__Rey__ReyElanId__02FC7413");

                entity.HasOne(d => d.ReyPerson)
                    .WithMany(p => p.Reys)
                    .HasForeignKey(d => d.ReyPersonId)
                    .HasConstraintName("FK__Rey__ReyPersonId__03F0984C");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.RolAd).HasMaxLength(20);
            });

            modelBuilder.Entity<Seher>(entity =>
            {
                entity.ToTable("Seher");

                entity.Property(e => e.SeherAd).HasMaxLength(20);
            });

            modelBuilder.Entity<Sekil>(entity =>
            {
                entity.ToTable("Sekil");

                entity.Property(e => e.SekilAd).HasMaxLength(30);

                entity.HasOne(d => d.SekilElan)
                    .WithMany(p => p.Sekils)
                    .HasForeignKey(d => d.SekilElanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Sekil__SekilElan__00200768");
            });

            modelBuilder.Entity<Sk>(entity =>
            {
                entity.ToTable("SK");

                entity.Property(e => e.Skid).HasColumnName("SKId");

                entity.Property(e => e.Skad)
                    .HasMaxLength(10)
                    .HasColumnName("SKAd");
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.ToTable("Tip");

                entity.Property(e => e.TipAd).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using GisServerProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Gestionnaire> Gestionnaires { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Vol> Vols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");

            // ✅ Configuration explicite des noms de tables (correspondant à PostgreSQL)

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");
                entity.HasKey(e => e.Id).HasName("clients_pkey");
            });

            modelBuilder.Entity<Gestionnaire>(entity =>
            {
                entity.ToTable("gestionnaires");
                entity.HasKey(e => e.Id).HasName("gestionnaires_pkey");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservations");

                entity.HasKey(e => e.Id).HasName("reservations_pkey");

                entity.Property(e => e.DateReservation).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("reservations_client_id_fkey");

                entity.HasOne(d => d.Vol)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.VolId)
                    .HasConstraintName("reservations_vol_id_fkey");
            });

            modelBuilder.Entity<Vol>(entity =>
            {
                entity.ToTable("vols");

                entity.HasKey(e => e.Id).HasName("vols_pkey");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NomVol).HasColumnName("nom_vol");
                entity.Property(e => e.NbPlacesMax).HasColumnName("nb_places_max");
                entity.Property(e => e.Destination).HasColumnName("destination");
                entity.Property(e => e.Depart).HasColumnName("depart");
                entity.Property(e => e.Prix).HasColumnName("prix");
                entity.Property(e => e.Geom).HasColumnName("geom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

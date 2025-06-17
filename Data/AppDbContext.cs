using System;
using System.Collections.Generic;
using GisServerProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GisServerProject.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<client> clients { get; set; }

    public virtual DbSet<gestionnaire> gestionnaires { get; set; }

    public virtual DbSet<reservation> reservations { get; set; }

    public virtual DbSet<vol> vols { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<client>(entity =>
        {
            entity.HasKey(e => e.id).HasName("clients_pkey");
        });

        modelBuilder.Entity<gestionnaire>(entity =>
        {
            entity.HasKey(e => e.id).HasName("gestionnaires_pkey");
        });

        modelBuilder.Entity<reservation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("reservations_pkey");

            entity.Property(e => e.date_reservation).HasDefaultValueSql("now()");

            entity.HasOne(d => d.client).WithMany(p => p.reservations).HasConstraintName("reservations_client_id_fkey");

            entity.HasOne(d => d.vol).WithMany(p => p.reservations).HasConstraintName("reservations_vol_id_fkey");
        });

        modelBuilder.Entity<vol>(entity =>
        {
            entity.HasKey(e => e.id).HasName("vols_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

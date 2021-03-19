using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TravelprojectApi.DataAccess.Models
{
    public partial class TravelDBContext : DbContext
    {
        public TravelDBContext()
        {
        }

        public TravelDBContext(DbContextOptions<TravelDBContext> options)
            : base(options)
        {    
        }

        public virtual DbSet<FlightItinerary> FlightItineraries { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<TravelDatum> TravelData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=TravelDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FlightItinerary>(entity =>
            {
                entity.ToTable("FlightItinerary");

                entity.Property(e => e.Aeroline)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationAirport)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FlightCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OriginAirpot)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.TravelData)
                    .WithMany(p => p.FlightItineraries)
                    .HasForeignKey(d => d.TravelDataId)
                    .HasConstraintName("FK_FlightItinerary_TravelData");
            });

            modelBuilder.Entity<Travel>(entity =>
            {
                entity.ToTable("Travel");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TravelDatum>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Passport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Travel)
                    .WithMany(p => p.TravelData)
                    .HasForeignKey(d => d.TravelId)
                    .HasConstraintName("FK_TravelData_Travel");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BusinessEntities;

namespace DataAccess.DB
{
    public partial class FlightPlannerDBContext : IdentityDbContext<User, GroupMembership, string>
    {
        //public FlightPlannerDBContext()
        //{
        //}

        public FlightPlannerDBContext(DbContextOptions<FlightPlannerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<GroupMembership> GroupMembership { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.ToTable("aircraft");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConsumptionUnit)
                    .HasColumnName("consumption_unit")
                    .HasMaxLength(50);

                entity.Property(e => e.FuelConsumption).HasColumnName("fuel_consumption");

                entity.Property(e => e.FuelConsumptionTakeoff).HasColumnName("fuel_consumption_takeoff");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("airport");

                entity.HasIndex(e => new { e.Code, e.Name, e.CityCode })
                    .HasName("UQ_airport_code_name_citycode")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CityCode)
                    .HasColumnName("city_code")
                    .HasMaxLength(100);

                entity.Property(e => e.CityName)
                    .HasColumnName("city_name")
                    .HasMaxLength(200);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(100);

                entity.Property(e => e.CountryCode)
                    .HasColumnName("country_code")
                    .HasMaxLength(200);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("country_name")
                    .HasMaxLength(200);

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasMaxLength(100);

                entity.Property(e => e.Lon)
                    .HasColumnName("lon")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.NumAirports).HasColumnName("num_airports");

                entity.Property(e => e.TimeZone)
                    .HasColumnName("time_zone")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("flight");

                entity.HasIndex(e => e.FkAircraft);

                entity.HasIndex(e => e.FkArrival);

                entity.HasIndex(e => e.FkDeparture);

                entity.HasIndex(e => e.FkUser);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Distance).HasColumnName("distance");

                entity.Property(e => e.DistanceUnit)
                    .HasColumnName("distance_unit")
                    .HasMaxLength(50);

                entity.Property(e => e.FkAircraft).HasColumnName("fk_aircraft");

                entity.Property(e => e.FkArrival).HasColumnName("fk_arrival");

                entity.Property(e => e.FkDeparture).HasColumnName("fk_departure");

                entity.Property(e => e.FkUser).HasColumnName("fk_user");

                entity.Property(e => e.TripFuelConsumption).HasColumnName("trip_fuel_consumption");

                entity.HasOne(d => d.FkAircraftNavigation)
                    .WithMany(p => p.Flight)
                    .HasForeignKey(d => d.FkAircraft)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_flight_aircraft");

                entity.HasOne(d => d.FkArrivalNavigation)
                    .WithMany(p => p.FlightFkArrivalNavigation)
                    .HasForeignKey(d => d.FkArrival)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_flight_airport_arrival");

                entity.HasOne(d => d.FkDepartureNavigation)
                    .WithMany(p => p.FlightFkDepartureNavigation)
                    .HasForeignKey(d => d.FkDeparture)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_flight_airport_departure");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.Flight)
                    .HasForeignKey(d => d.FkUser)
                    .HasConstraintName("FK_flight_user");
            });

            modelBuilder.Entity<GroupMembership>(entity =>
            {
                entity.ToTable("group_membership");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}

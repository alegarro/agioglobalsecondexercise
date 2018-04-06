﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using PruebaUnoAgioGlobal.Infrastructure.Contexts;
using System;

namespace PruebaUnoAgioGlobal.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180404123308_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("PruebaUnoAgioGlobal.Core.Entities.Airline", b =>
                {
                    b.Property<int>("AirlineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("AirlineId");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("PruebaUnoAgioGlobal.Core.Entities.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Country");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("AirportId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("PruebaUnoAgioGlobal.Core.Entities.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AirlineId");

                    b.Property<string>("Code");

                    b.Property<int>("DestinationAirportId");

                    b.Property<int>("Distance");

                    b.Property<int>("FuelConsumption");

                    b.Property<int>("OriginAirportId");

                    b.HasKey("FlightId");

                    b.HasIndex("AirlineId");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("OriginAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("PruebaUnoAgioGlobal.Core.Entities.Flight", b =>
                {
                    b.HasOne("PruebaUnoAgioGlobal.Core.Entities.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PruebaUnoAgioGlobal.Core.Entities.Airport", "DestinationAirport")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PruebaUnoAgioGlobal.Core.Entities.Airport", "OriginAirport")
                        .WithMany()
                        .HasForeignKey("OriginAirportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using post_office_back.Data;

#nullable disable

namespace post_office_back.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240524065501_addDestinationCountryColumn")]
    partial class addDestinationCountryColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("post_office_back.Models.Bag", b =>
                {
                    b.Property<string>("BagNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("ShipmentNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BagNumber");

                    b.HasIndex("ShipmentNumber");

                    b.ToTable("Bags");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Bag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("post_office_back.Models.Parcel", b =>
                {
                    b.Property<string>("ParcelNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DestinationCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParcelBagBagNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasPrecision(18, 3)
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("ParcelNumber");

                    b.HasIndex("ParcelBagBagNumber");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("post_office_back.Models.Shipment", b =>
                {
                    b.Property<string>("ShipmentNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DestinationAirport")
                        .HasColumnType("int");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("bit");

                    b.HasKey("ShipmentNumber");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("post_office_back.Models.LetterBag", b =>
                {
                    b.HasBaseType("post_office_back.Models.Bag");

                    b.Property<long>("CountOfLetters")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Weight")
                        .HasPrecision(18, 3)
                        .HasColumnType("decimal(18,3)");

                    b.ToTable("Bags");

                    b.HasDiscriminator().HasValue("LETTERBAG");
                });

            modelBuilder.Entity("post_office_back.Models.ParcelBag", b =>
                {
                    b.HasBaseType("post_office_back.Models.Bag");

                    b.ToTable("Bags");

                    b.HasDiscriminator().HasValue("PARCELBAG");
                });

            modelBuilder.Entity("post_office_back.Models.Bag", b =>
                {
                    b.HasOne("post_office_back.Models.Shipment", null)
                        .WithMany("Bags")
                        .HasForeignKey("ShipmentNumber");
                });

            modelBuilder.Entity("post_office_back.Models.Parcel", b =>
                {
                    b.HasOne("post_office_back.Models.ParcelBag", null)
                        .WithMany("Parcels")
                        .HasForeignKey("ParcelBagBagNumber");
                });

            modelBuilder.Entity("post_office_back.Models.Shipment", b =>
                {
                    b.Navigation("Bags");
                });

            modelBuilder.Entity("post_office_back.Models.ParcelBag", b =>
                {
                    b.Navigation("Parcels");
                });
#pragma warning restore 612, 618
        }
    }
}

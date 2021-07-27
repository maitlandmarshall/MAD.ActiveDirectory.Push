﻿// <auto-generated />
using System;
using MAD.ActiveDirectory.Push.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MAD.ActiveDirectory.Push.Migrations
{
    [DbContext(typeof(ADDbContext))]
    partial class ADDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MAD.ActiveDirectory.Push.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("C")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Co")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistinguishedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionAttribute1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionAttribute2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionAttribute3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionAttribute4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtensionAttribute5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExtractedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalDeliveryOfficeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPrincipalName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}

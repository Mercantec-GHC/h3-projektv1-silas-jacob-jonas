﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241016120724_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.DistanceObject", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SitSmartDeviceid")
                        .HasColumnType("integer");

                    b.Property<int>("length")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("SitSmartDeviceid");

                    b.ToTable("DistanceObjects");
                });

            modelBuilder.Entity("API.Models.Movement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SitSmartDeviceid")
                        .HasColumnType("integer");

                    b.Property<float>("xValue")
                        .HasColumnType("real");

                    b.Property<float>("yValue")
                        .HasColumnType("real");

                    b.Property<float>("zValue")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("SitSmartDeviceid");

                    b.ToTable("MovementObjects");
                });

            modelBuilder.Entity("API.Models.SitSmartDevice", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("SitSmartDevice");
                });

            modelBuilder.Entity("API.Models.TempHumidity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SitSmartDeviceid")
                        .HasColumnType("integer");

                    b.Property<int>("humidity")
                        .HasColumnType("integer");

                    b.Property<int>("temp")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("SitSmartDeviceid");

                    b.ToTable("TempHumidityObjects");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SitSmartDeviceid")
                        .HasColumnType("integer");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("realPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("SitSmartDeviceid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.DistanceObject", b =>
                {
                    b.HasOne("API.Models.SitSmartDevice", null)
                        .WithMany("distanceObjects")
                        .HasForeignKey("SitSmartDeviceid");
                });

            modelBuilder.Entity("API.Models.Movement", b =>
                {
                    b.HasOne("API.Models.SitSmartDevice", null)
                        .WithMany("movements")
                        .HasForeignKey("SitSmartDeviceid");
                });

            modelBuilder.Entity("API.Models.TempHumidity", b =>
                {
                    b.HasOne("API.Models.SitSmartDevice", null)
                        .WithMany("tempHumiditys")
                        .HasForeignKey("SitSmartDeviceid");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.HasOne("API.Models.SitSmartDevice", null)
                        .WithMany("user")
                        .HasForeignKey("SitSmartDeviceid");
                });

            modelBuilder.Entity("API.Models.SitSmartDevice", b =>
                {
                    b.Navigation("distanceObjects");

                    b.Navigation("movements");

                    b.Navigation("tempHumiditys");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}

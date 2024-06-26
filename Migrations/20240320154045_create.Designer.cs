﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineCasino.DatabaseContext;

#nullable disable

namespace OnlineCasino.Migrations
{
    [DbContext(typeof(OnlineCasinoContext))]
    [Migration("20240320154045_create")]
    partial class create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GameGameCollections", b =>
                {
                    b.Property<int>("CollectionsId")
                        .HasColumnType("int");

                    b.Property<int>("GamesId")
                        .HasColumnType("int");

                    b.HasKey("CollectionsId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameCollectionsGames", (string)null);
                });

            modelBuilder.Entity("OnlineCasino.DatabaseContext.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AvailableDevices")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayIndex")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameCategory")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableDevices = "Desktop",
                            DisplayIndex = 1,
                            DisplayName = "Game A",
                            GameCategory = 0,
                            ReleaseDate = new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6754),
                            Thumbnail = ""
                        },
                        new
                        {
                            Id = 2,
                            AvailableDevices = "Desktop",
                            DisplayIndex = 2,
                            DisplayName = "Game B",
                            GameCategory = 0,
                            ReleaseDate = new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6823),
                            Thumbnail = ""
                        },
                        new
                        {
                            Id = 3,
                            AvailableDevices = "Desktop",
                            DisplayIndex = 1,
                            DisplayName = "Game C",
                            GameCategory = 2,
                            ReleaseDate = new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6826),
                            Thumbnail = ""
                        },
                        new
                        {
                            Id = 4,
                            AvailableDevices = "Desktop",
                            DisplayIndex = 2,
                            DisplayName = "Game D",
                            GameCategory = 2,
                            ReleaseDate = new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6829),
                            Thumbnail = ""
                        },
                        new
                        {
                            Id = 5,
                            AvailableDevices = "Desktop",
                            DisplayIndex = 1,
                            DisplayName = "Game E",
                            GameCategory = 0,
                            ReleaseDate = new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6832),
                            Thumbnail = ""
                        });
                });

            modelBuilder.Entity("OnlineCasino.DatabaseContext.Entities.GameCollections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayIndex")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCollectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCollectionId");

                    b.ToTable("GameCollections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayIndex = 1,
                            DisplayName = "Featured Games"
                        },
                        new
                        {
                            Id = 2,
                            DisplayIndex = 2,
                            DisplayName = "Top Rated"
                        },
                        new
                        {
                            Id = 3,
                            DisplayIndex = 1,
                            DisplayName = "Classic Slots",
                            ParentCollectionId = 1
                        },
                        new
                        {
                            Id = 4,
                            DisplayIndex = 2,
                            DisplayName = "Table Games",
                            ParentCollectionId = 3
                        },
                        new
                        {
                            Id = 5,
                            DisplayIndex = 1,
                            DisplayName = "Roulette",
                            ParentCollectionId = 4
                        });
                });

            modelBuilder.Entity("OnlineCasino.DatabaseContext.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameGameCollections", b =>
                {
                    b.HasOne("OnlineCasino.DatabaseContext.Entities.GameCollections", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCasino.DatabaseContext.Entities.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineCasino.DatabaseContext.Entities.GameCollections", b =>
                {
                    b.HasOne("OnlineCasino.DatabaseContext.Entities.GameCollections", null)
                        .WithMany("SubCollections")
                        .HasForeignKey("ParentCollectionId");
                });

            modelBuilder.Entity("OnlineCasino.DatabaseContext.Entities.GameCollections", b =>
                {
                    b.Navigation("SubCollections");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using CatalogFilms.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatalogFilms.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221031090755_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CatalogFilms.Data.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AgeLimit")
                        .HasColumnType("int");

                    b.Property<int>("Budget")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Time")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("bit");

                    b.Property<Guid>("MovieToReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MovieToReviewId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.Property<Guid>("FavoriteMoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavoriteMoviesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MovieUser");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.Genre", b =>
                {
                    b.HasOne("CatalogFilms.Data.Models.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.Review", b =>
                {
                    b.HasOne("CatalogFilms.Data.Models.User", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogFilms.Data.Models.Movie", "MovieToReview")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieToReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("MovieToReview");
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.HasOne("CatalogFilms.Data.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("FavoriteMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogFilms.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.Movie", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("CatalogFilms.Data.Models.User", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
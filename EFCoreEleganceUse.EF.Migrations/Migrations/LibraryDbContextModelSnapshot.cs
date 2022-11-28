﻿// <auto-generated />
using System;
using EFCoreEleganceUse.EF.Mysql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreEleganceUse.EF.Migrations.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4 ");

            modelBuilder.Entity("EFCoreEleganceUse.Domain.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SN")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("Author");

                    b.HasIndex("SN")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("EFCoreEleganceUse.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = "090213204",
                            Birthday = new DateTime(1996, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "Bruce"
                        });
                });

            modelBuilder.Entity("EFCoreEleganceUse.Domain.Entities.Book", b =>
                {
                    b.HasOne("EFCoreEleganceUse.Domain.Entities.User", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EFCoreEleganceUse.Domain.Entities.User", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Postter.Infrastructure.Context;

#nullable disable

namespace Postter.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Postter.Infrastructure.DAO.AccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasMaxLength(140)
                        .HasColumnType("character varying(140)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ec4e0eea-6df1-4892-8fde-be8334066942"),
                            DateAdded = new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2075),
                            Email = "admin@gmail.com",
                            HashPassword = "554F8AC9FB309CE8385D473E683952F6",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Admin Adminskiy",
                            RoleId = 1,
                            Salt = "$t3*l0aNDebRhuY22#yy"
                        },
                        new
                        {
                            Id = new Guid("3087656c-0a96-4b90-a4f3-cc071478435f"),
                            DateAdded = new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2115),
                            Email = "user@gmail.com",
                            HashPassword = "AA1F76ABBDD756030D2245FE00DC701A",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "User Prostetskiy",
                            RoleId = 2,
                            Salt = "KJ@ZG&34_de!3QP5BKJ#"
                        },
                        new
                        {
                            Id = new Guid("b24ee270-f4a4-431a-ae16-6c150dc117c7"),
                            DateAdded = new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2118),
                            Email = "moder@gmail.com",
                            HashPassword = "7AAE39726A38A1FD2C0B7DD94EBA9379",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Moderator Zloy i Derzkiy",
                            RoleId = 3,
                            Salt = "368fV5ZBb4se+25*5(RN"
                        });
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Moder"
                        });
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.AccountEntity", b =>
                {
                    b.HasOne("Postter.Infrastructure.DAO.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}

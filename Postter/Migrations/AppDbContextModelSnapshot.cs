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
                            Id = new Guid("8c67adfd-a27c-4212-95a3-e16e8af1d47a"),
                            DateAdded = new DateTime(2022, 10, 8, 10, 28, 27, 735, DateTimeKind.Utc).AddTicks(151),
                            Email = "admin@gmail.com",
                            HashPassword = "BF56F4B77EA7868B792B52605837401B",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Admin Adminskiy",
                            RoleId = 1,
                            Salt = "6*KKO9iqbI8(2ZWugv8T"
                        },
                        new
                        {
                            Id = new Guid("aee566b6-1814-46cd-8d7c-afcc035bbeb5"),
                            DateAdded = new DateTime(2022, 10, 8, 10, 28, 27, 735, DateTimeKind.Utc).AddTicks(177),
                            Email = "user@gmail.com",
                            HashPassword = "60A3A344B1E3EBC0D967E826D05526EF",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "User Prostetskiy",
                            RoleId = 2,
                            Salt = "oZ!2A_MPvDm*RPc_NMbu"
                        },
                        new
                        {
                            Id = new Guid("7e72d42b-b55f-4e49-b564-f16aacea2b83"),
                            DateAdded = new DateTime(2022, 10, 8, 10, 28, 27, 735, DateTimeKind.Utc).AddTicks(179),
                            Email = "moder@gmail.com",
                            HashPassword = "91C2EF5472FC966356373C13A62C5595",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Moderator Zloy i Derzkiy",
                            RoleId = 3,
                            Salt = "Bc06Ju&RlWBWq2L8apSl"
                        });
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.CommentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("character varying(140)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.LikeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.PostEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("character varying(140)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.RoleEntity", b =>
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
                    b.HasOne("Postter.Infrastructure.DAO.RoleEntity", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.CommentEntity", b =>
                {
                    b.HasOne("Postter.Infrastructure.DAO.AccountEntity", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Postter.Infrastructure.DAO.PostEntity", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.LikeEntity", b =>
                {
                    b.HasOne("Postter.Infrastructure.DAO.AccountEntity", "Author")
                        .WithMany("Likes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Postter.Infrastructure.DAO.PostEntity", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.PostEntity", b =>
                {
                    b.HasOne("Postter.Infrastructure.DAO.AccountEntity", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.AccountEntity", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.PostEntity", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Postter.Infrastructure.DAO.RoleEntity", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}

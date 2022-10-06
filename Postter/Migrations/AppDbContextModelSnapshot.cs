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
                            Id = new Guid("5937eb5f-4278-467e-977b-e7205ed5fd9b"),
                            DateAdded = new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(625),
                            Email = "admin@gmail.com",
                            HashPassword = "C7284652D08DCF8A1EC9AE5B6FD46E65",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Admin Adminskiy",
                            RoleId = 1,
                            Salt = "t(quhPhBQuXC9amhy(hk"
                        },
                        new
                        {
                            Id = new Guid("ff99309a-8e95-469d-b454-4d5b45819314"),
                            DateAdded = new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(647),
                            Email = "user@gmail.com",
                            HashPassword = "87753F78463D2521ECDA7B9B8DD32366",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "User Prostetskiy",
                            RoleId = 2,
                            Salt = "oB#G%h3n^21et9!q(7ok"
                        },
                        new
                        {
                            Id = new Guid("8b1d22ce-e48e-4108-bf00-24c34ee6228b"),
                            DateAdded = new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(649),
                            Email = "moder@gmail.com",
                            HashPassword = "1F533E735DB40963C78494A1E988DC07",
                            ImageUri = "testlink.com/testdirectory/testimage.png",
                            IsActive = true,
                            Name = "Moderator Zloy i Derzkiy",
                            RoleId = 3,
                            Salt = "&iJgwfjwAE!Hp35wEy+e"
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

                    b.ToTable("CommentEntity");
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

                    b.ToTable("LikeEntity");
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

                    b.ToTable("PostEntity");
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

using Microsoft.EntityFrameworkCore;
using Postter.Common.Helpers;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;

namespace Postter.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AccountEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.Email)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.Name)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.Salt)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.HashPassword)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.IsActive)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .Property(x => x.RoleId)
            .IsRequired();
        modelBuilder.Entity<AccountEntity>()
            .HasOne(p => p.Role)
            .WithMany(b => b.Accounts)
            .HasForeignKey(x => x.RoleId);
        
        modelBuilder.Entity<CommentEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<CommentEntity>()
            .Property(x => x.AuthorId)
            .IsRequired();
        modelBuilder.Entity<CommentEntity>()
            .Property(x => x.PostId)
            .IsRequired();
        modelBuilder.Entity<CommentEntity>()
            .Property(x => x.Text)
            .IsRequired();
        modelBuilder.Entity<CommentEntity>()
            .HasOne(p => p.Post)
            .WithMany(b => b.Comments)
            .HasForeignKey(x => x.PostId);
        modelBuilder.Entity<CommentEntity>()
            .HasOne(p => p.Author)
            .WithMany(b => b.Comments)
            .HasForeignKey(x => x.AuthorId);
        
        modelBuilder.Entity<LikeEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<LikeEntity>()
            .Property(x => x.AuthorId)
            .IsRequired();
        modelBuilder.Entity<LikeEntity>()
            .Property(x => x.PostId)
            .IsRequired();
        modelBuilder.Entity<LikeEntity>()
            .HasOne(p => p.Post)
            .WithMany(b => b.Likes)
            .HasForeignKey(x => x.PostId);
        modelBuilder.Entity<LikeEntity>()
            .HasOne(p => p.Author)
            .WithMany(b => b.Likes)
            .HasForeignKey(x => x.AuthorId);
        
        modelBuilder.Entity<PostEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<PostEntity>()
            .Property(x => x.AuthorId)
            .IsRequired();
        modelBuilder.Entity<PostEntity>()
            .Property(x => x.Text)
            .IsRequired();
        modelBuilder.Entity<PostEntity>()
            .HasOne(p => p.Author)
            .WithMany(b => b.Posts)
            .HasForeignKey(x => x.AuthorId);
        
        modelBuilder.Entity<RoleEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<RoleEntity>()
            .Property(x => x.Name)
            .IsRequired();


        #region Инициализация тестовых данных
        RegistrationHelper registrationHelper = new();

        string adminEmail = "admin@gmail.com";
        string userEmail = "user@gmail.com";
        string moderEmail = "moder@gmail.com";

        string adminName = "Admin Adminskiy";
        string userName = "User Prostetskiy";
        string moderName = "Moderator Zloy i Derzkiy";

        string adminSalt = registrationHelper.generateSalt();
        string userSalt = registrationHelper.generateSalt();
        string moderSalt = registrationHelper.generateSalt();

        string adminPassword = registrationHelper.generateHashPass("admin", adminSalt);
        string userPassword = registrationHelper.generateHashPass("user", userSalt);
        string moderPassword = registrationHelper.generateHashPass("moder", moderSalt);

        RoleEntity adminRoleEntity = new() { Id = (int)RolesEnum.Admin, Name = RolesEnum.Admin.ToString() };
        RoleEntity userRoleEntity = new() { Id = (int)RolesEnum.User, Name = RolesEnum.User.ToString() };
        RoleEntity moderRoleEntity = new() { Id = (int)RolesEnum.Moder, Name = RolesEnum.Moder.ToString() };
        AccountEntity adminUser = new()
        {
            Id = Guid.NewGuid(),
            Email = adminEmail,
            Name = adminName,
            IsActive = true,
            HashPassword = adminPassword,
            RoleId = adminRoleEntity.Id, 
            Salt = adminSalt
        };
        AccountEntity user = new()
        {
            Id = Guid.NewGuid(),
            Email = userEmail,
            Name = userName,
            IsActive = true,
            HashPassword = userPassword,
            RoleId = userRoleEntity.Id,
            Salt = userSalt
        };
        AccountEntity moderUser = new()
        {
            Id = Guid.NewGuid(),
            Email = moderEmail,
            Name = moderName,
            IsActive = true,
            HashPassword = moderPassword,
            RoleId = moderRoleEntity.Id,
            Salt = moderSalt
        };

        modelBuilder.Entity<RoleEntity>().HasData(new[] { adminRoleEntity, userRoleEntity, moderRoleEntity });
        modelBuilder.Entity<AccountEntity>().HasData(new[] { adminUser, user, moderUser });
        #endregion
    }


    public virtual DbSet<AccountEntity> Person { get; set; }
    public virtual DbSet<RoleEntity> Role { get; set; }
    public virtual DbSet<PostEntity> Post { get; set; }
    public virtual DbSet<CommentEntity> Comment { get; set; }
    public virtual DbSet<LikeEntity> Like { get; set; }
}
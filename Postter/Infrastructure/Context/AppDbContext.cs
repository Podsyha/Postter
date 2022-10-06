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
            .HasOne(p => p.RoleEntity)
            .WithMany(b => b.Accounts);


        modelBuilder.Entity<RoleEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<RoleEntity>()
            .Property(x => x.Name)
            .IsRequired();


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
    }


    public virtual DbSet<AccountEntity> Person { get; set; }
    public virtual DbSet<RoleEntity> Role { get; set; }
}
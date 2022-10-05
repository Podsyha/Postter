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
            .WithMany(b => b.Accounts);


        modelBuilder.Entity<Role>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Role>()
            .Property(x => x.Name)
            .IsRequired();




        RegistrationHelper registrationHelper = new();

        string adminEmail = "admin@gmail.com";
        string userEmail = "user@gmail.com";
        string moderEmail = "moder@gmail.com";

        string adminSalt = registrationHelper.generateSalt();
        string userSalt = registrationHelper.generateSalt();
        string moderSalt = registrationHelper.generateSalt();

        string adminPassword = registrationHelper.generateHashPass("admin", adminSalt);
        string userPassword = registrationHelper.generateHashPass("user", userSalt);
        string moderPassword = registrationHelper.generateHashPass("moder", moderSalt);
        
        Role adminRole = new(){ Id = (int)RolesEnum.Admin, Name = RolesEnum.Admin.ToString() };
        Role userRole = new(){ Id = (int)RolesEnum.User, Name = RolesEnum.User.ToString() };
        Role moderRole = new() { Id = (int)RolesEnum.Moder, Name = RolesEnum.Moder.ToString() };
        AccountEntity adminUser = new(){ Id = Guid.NewGuid(), Email = adminEmail, HashPassword = adminPassword, RoleId = adminRole.Id, Salt = adminSalt};
        AccountEntity user = new(){ Id = Guid.NewGuid(), Email = userEmail, HashPassword = userPassword, RoleId = userRole.Id, Salt = userSalt};
        AccountEntity moderUser = new(){ Id = Guid.NewGuid(), Email = moderEmail, HashPassword = moderPassword, RoleId = moderRole.Id, Salt = moderSalt};
 
        modelBuilder.Entity<Role>().HasData(new[] { adminRole, userRole, moderRole});
        modelBuilder.Entity<AccountEntity>().HasData(new[] { adminUser, user, moderUser });
    }

    
    public virtual DbSet<AccountEntity> Person { get; set; }
    public virtual DbSet<Role> Role { get; set; }
}
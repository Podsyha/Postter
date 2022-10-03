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
        Person adminUser = new(){ Id = Guid.NewGuid(), Email = adminEmail, HashPassword = adminPassword, RoleId = adminRole.Id, Salt = adminSalt};
        Person user = new(){ Id = Guid.NewGuid(), Email = userEmail, HashPassword = userPassword, RoleId = userRole.Id, Salt = userSalt};
        Person moderUser = new(){ Id = Guid.NewGuid(), Email = moderEmail, HashPassword = moderPassword, RoleId = moderRole.Id, Salt = moderSalt};
 
        modelBuilder.Entity<Role>().HasData(new[] { adminRole, userRole, moderRole});
        modelBuilder.Entity<Person>().HasData(new[] { adminUser, user, moderUser });
    }

    
    public virtual DbSet<Person> Person { get; set; }
    public virtual DbSet<Role> Role { get; set; }
}
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
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
        MD5 md5 = MD5.Create();
        
        string adminEmail = "admin@gmail.com";
        string userEmail = "user@gmail.com";
        string moderEmail = "moder@gmail.com";

        string adminSalt = registrationHelper.generateSalt();
        string userSalt = registrationHelper.generateSalt();
        string moderSalt = registrationHelper.generateSalt();

        string adminPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(adminSalt + "admin")).ToString();
        string userPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(userSalt + "user")).ToString();
        string moderPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(moderSalt + "moder")).ToString();
        
        Role adminRole = new(){ Id = (int)RolesEnum.Admin, Name = RolesEnum.Admin.GetDisplayName() };
        Role userRole = new(){ Id = (int)RolesEnum.User, Name = RolesEnum.User.GetDisplayName() };
        Role moderRole = new() { Id = (int)RolesEnum.Moder, Name = RolesEnum.Moder.GetDisplayName() };
        Person adminUser = new(){ Id = Guid.NewGuid(), Email = adminEmail, HashPassword = adminPassword, RoleId = adminRole.Id, Salt = adminSalt};
        Person user = new(){ Id = Guid.NewGuid(), Email = userEmail, HashPassword = userPassword, RoleId = userRole.Id, Salt = userSalt};
        Person moderUser = new(){ Id = Guid.NewGuid(), Email = moderEmail, HashPassword = moderPassword, RoleId = moderRole.Id, Salt = moderSalt};
 
        modelBuilder.Entity<Role>().HasData(new[] { adminRole, userRole, moderRole});
        modelBuilder.Entity<Person>().HasData(new[] { adminUser, user, moderUser });
    }

    
    public virtual DbSet<Person> Person { get; set; }
    public virtual DbSet<Role> Role { get; set; }
}
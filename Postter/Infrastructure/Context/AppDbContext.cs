using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Postter.Common.Helpers;
using Postter.Infrastructure.DAO;

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

        RandomUtils randomUtils = new();
        MD5 md5 = MD5.Create();
        
        string adminRoleName = "admin";
        string userRoleName = "user";
        string moderRoleName = "moder";
 
        string adminEmail = "admin@gmail.com";
        string userEmail = "user@gmail.com";
        string moderEmail = "moder@gmail.com";

        string adminSalt = randomUtils.generateChars(20).ToString();
        string userSalt = randomUtils.generateChars(20).ToString();
        string moderSalt = randomUtils.generateChars(20).ToString();

        string adminPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(adminSalt + "admin")).ToString();
        string userPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(userSalt + "user")).ToString();
        string moderPassword = md5.ComputeHash(Encoding.ASCII.GetBytes(moderSalt + "moder")).ToString();
        
        Role adminRole = new(){ Id = 1, Name = adminRoleName };
        Role userRole = new(){ Id = 2, Name = userRoleName };
        Role moderRole = new() { Id = 3, Name = moderRoleName };
        Person adminUser = new(){ Id = Guid.NewGuid(), Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id, Salt = adminSalt};
        Person user = new(){ Id = Guid.NewGuid(), Email = userEmail, Password = userPassword, RoleId = userRole.Id, Salt = userSalt};
        Person moderUser = new(){ Id = Guid.NewGuid(), Email = moderEmail, Password = moderPassword, RoleId = moderRole.Id, Salt = moderSalt};
 
        modelBuilder.Entity<Role>().HasData(new[] { adminRole, userRole, moderRole});
        modelBuilder.Entity<Person>().HasData(new[] { adminUser, user, moderUser });
    }

    
    public virtual DbSet<Person> Person { get; set; }
    public virtual DbSet<Role> Role { get; set; }
}
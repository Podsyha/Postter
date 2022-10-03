using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
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
        
        string adminRoleName = "admin";
        string userRoleName = "user";
        string moderRoleName = "moder";
 
        string adminEmail = "admin@gmail.com";
        string adminPassword = "a";

        string userEmail = "user@gmail.com";
        string userPassword = "u";
        
        string moderEmail = "moder@gmail.com";
        string moderPassword = "m";
        
        Role adminRole = new(){ Id = 1, Name = adminRoleName };
        Role userRole = new(){ Id = 2, Name = userRoleName };
        Role moderRole = new() { Id = 3, Name = moderRoleName };
        Person adminUser = new(){ Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
        Person user = new(){ Id = 2, Email = userEmail, Password = userPassword, RoleId = userRole.Id };
        Person moderUser = new(){ Id = 3, Email = moderEmail, Password = moderPassword, RoleId = moderRole.Id };
 
        modelBuilder.Entity<Role>().HasData(new[] { adminRole, userRole, moderRole});
        modelBuilder.Entity<Person>().HasData(new[] { adminUser, user, moderUser });
    }

    
    public virtual DbSet<Person> Person { get; set; }
    public virtual DbSet<Role> Role { get; set; }
}
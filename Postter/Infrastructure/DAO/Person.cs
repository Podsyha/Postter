﻿namespace Postter.Infrastructure.DAO;

public class Person
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? RoleId { get; set; }
    public Role Role { get; set; }
}
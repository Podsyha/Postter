namespace Postter.Infrastructure.DAO;

/// <summary>
/// Аккуратно с изменениями. На сущности завязан enum RolesEnum
/// </summary>
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Person> Persons { get; set; }
    public Role()
    {
        Persons = new List<Person>();
    }
}
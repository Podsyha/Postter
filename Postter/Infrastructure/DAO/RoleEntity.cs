namespace Postter.Infrastructure.DAO;

/// <summary>
/// Аккуратно с изменениями. На сущности завязан enum RolesEnum
/// </summary>
public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AccountEntity> Accounts { get; set; }
}
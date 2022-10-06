using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.Roles;

public interface IRoleRepository
{
    /// <summary>
    /// Получить лист всех ролей
    /// </summary>
    public Task<List<RoleEntity>> GetAllRoles();
}
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.RoleRepository;

public interface IRoleRepository
{
    /// <summary>
    /// Получить лист всех ролей
    /// </summary>
    public Task<List<RoleEntity>> GetAllRoles();
}
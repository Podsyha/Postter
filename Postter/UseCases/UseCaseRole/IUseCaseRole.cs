using Postter.Infrastructure.DAO;

namespace Postter.UseCases.UseCaseRole;

public interface IUseCaseRole
{
    /// <summary>
    /// Получить лист всех ролей
    /// </summary>
    /// <returns></returns>
    Task<ICollection<RoleEntity>> GetAllRoles();
}
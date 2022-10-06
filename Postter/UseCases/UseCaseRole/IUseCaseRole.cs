using Postter.Infrastructure.DAO;

namespace Postter.UseCases.UseCaseRole;

public interface IUseCaseRole
{
    Task<List<RoleEntity>> GetAllRoles();
}
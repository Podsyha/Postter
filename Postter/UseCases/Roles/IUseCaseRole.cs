using Postter.Infrastructure.DAO;

namespace Postter.UseCases.Roles;

public interface IUseCaseRole
{
    Task<List<Role>> GetAllRoles();
}
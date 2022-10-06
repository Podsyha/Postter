using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.Roles;

namespace Postter.UseCases.Roles;

public class UseCaseRole : IUseCaseRole
{
    public UseCaseRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    private readonly IRoleRepository _roleRepository;



    public async Task<List<RoleEntity>> GetAllRoles() => 
        await _roleRepository.GetAllRoles();
}
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.RoleRepository;

namespace Postter.UseCases.UseCaseRole;

public class UseCaseRole : IUseCaseRole
{
    public UseCaseRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    private readonly IRoleRepository _roleRepository;



    public async Task<ICollection<RoleEntity>> GetAllRoles() => 
        await _roleRepository.GetAllRoles();
}
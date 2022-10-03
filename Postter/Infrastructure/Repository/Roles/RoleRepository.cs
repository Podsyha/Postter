using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.Roles;

public class RoleRepository : AppDbFunc, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
    }
    

    private readonly IAssert _assert;
    
    
    /// <summary>
    /// Получить лист всех ролей
    /// </summary>
    public async Task<List<Role>> GetAllRoles()
    {
        List<Role> roles = await _dbContext.Role.ToListAsync();
        
        _assert.EmptyCollection(roles);

        return roles;
    }
}
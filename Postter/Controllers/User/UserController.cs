using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postter.Infrastructure.Context;

namespace Postter.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly AppDbContext _dbContext;
    
    
    [HttpGet]
    public Task<Infrastructure.DAO.User> Get(Guid id)
    {
        return _dbContext.User.FirstAsync(x => x.Id == id);
    }
}
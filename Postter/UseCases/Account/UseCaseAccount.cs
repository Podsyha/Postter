using System.Security.Claims;
using Postter.Common.Exceptions;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DTO;
using Postter.Infrastructure.Repository;

namespace Postter.UseCases.Account;

public class UseCaseAccount : IUseCaseAccount
{
    public UseCaseAccount(AppDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    private readonly AppDbContext _dbContext;
    private readonly IUserRepository _userRepository;


    
    
}
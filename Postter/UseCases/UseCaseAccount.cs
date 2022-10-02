using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Postter.Common.Exceptions;
using Postter.Controllers.User.Model;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
using Postter.Infrastructure.Repository;

namespace Postter.UseCases;

public class UseCaseAccount
{
    public UseCaseAccount(AppDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    private readonly AppDbContext _dbContext;
    private readonly IUserRepository _userRepository;


    
    public async Task Login(LoginModel model)
    {
        UserDto user = await _userRepository.GetUserAsync(model.Password, model.Email);
        if (user != null)
            Authenticate(model.Email);
        else
            throw new RequestLogicException("Некорректные логин и(или) пароль");
    }

    public async Task Register(RegisterModel model)
    {
        UserDto user = await _userRepository.GetUserAsync(model.Password, model.Email);
        if (user == null)
        {
            await _userRepository.AddUserAsync(user.Email, user.Password);

            Authenticate(model.Email);
        }
        else
            throw new RequestLogicException("Некорректные логин и(или) пароль");
    }

    private static ClaimsIdentity Authenticate(string userName)
    {
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, userName)
        };

        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return id;
    }
}
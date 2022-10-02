using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Postter.Common.Exceptions;
using Postter.Controllers.User.Model;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.UseCases;

public class UseCaseAccount
{
    public UseCaseAccount(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly AppDbContext _dbContext;


    
    public async Task Login(LoginModel model)
    {
        User user = await _dbContext.User.FirstOrDefaultAsync(u =>
            u.Email == model.Email && u.Password == model.Password);
        if (user != null)
            Authenticate(model.Email);
        else
            throw new RequestLogicException("Некорректные логин и(или) пароль");
    }

    public async Task Register(RegisterModel model)
    {
        User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            _dbContext.User.Add(new User { Email = model.Email, Password = model.Password });
            await _dbContext.SaveChangesAsync();

            Authenticate(model.Email);
        }
        else
            throw new RequestLogicException("Некорректные логин и(или) пароль");
    }

    private ClaimsIdentity Authenticate(string userName)
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
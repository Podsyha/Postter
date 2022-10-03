using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Auth;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.Persons;

namespace Postter.UseCases.Account;

public class UseCaseAccount : IUseCaseAccount
{
    public UseCaseAccount(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    private readonly IPersonRepository _personRepository;

    
    public async Task<JwtSecurityToken> GetToken(string email, string password, ClaimsIdentity identity)
    {
        DateTime now = DateTime.UtcNow;
        
        JwtSecurityToken jwt = new(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return jwt;
    }
    
    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        Person person = await _personRepository.GetPersonAsync(email,password);

        if (person == null) return null;

        List<Claim> claims = new()
        {
            new(ClaimsIdentity.DefaultNameClaimType, person.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
        };

        ClaimsIdentity claimsIdentity =
            new(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
        
        return claimsIdentity;
    }
}
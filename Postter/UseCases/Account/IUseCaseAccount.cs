using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Postter.UseCases.Account;

public interface IUseCaseAccount
{
    Task<JwtSecurityToken> GetToken(string email, string password, ClaimsIdentity identity);
    Task<ClaimsIdentity> GetIdentity(string email, string password);
}
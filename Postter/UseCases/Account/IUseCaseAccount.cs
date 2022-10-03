using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Postter.UseCases.Account;

public interface IUseCaseAccount
{
    Task<JwtSecurityToken> GetToken(string username, string password, ClaimsIdentity identity);
    Task<ClaimsIdentity> GetIdentity(string username, string password);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Postter.UseCases.Account;

public interface IUseCaseAccount
{
    JwtSecurityToken GetToken(string email, string password, ClaimsIdentity identity);
    Task<ClaimsIdentity> GetIdentity(string email, string password);
    Task GiveTheUserARole(string email, string role);
}
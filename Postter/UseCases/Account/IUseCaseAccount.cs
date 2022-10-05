using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Postter.Controllers.Account.Models;

namespace Postter.UseCases.Account;

public interface IUseCaseAccount
{
    JwtSecurityToken GetToken(ClaimsIdentity identity);
    Task<ClaimsIdentity> GetIdentity(string email, string password);
    Task GiveTheUserARole(string email, string role);
    Task Registration(RegistrationModel model);
    Task DeleteAccount(Guid accountId);
    Task UpdateAccountInfo(UpdateAccountInfoModel model);
}
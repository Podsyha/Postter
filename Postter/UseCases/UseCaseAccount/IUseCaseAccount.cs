using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Postter.Controllers.Account.Models;

namespace Postter.UseCases.UseCaseAccount;

public interface IUseCaseAccount
{
    ///<summary>
    /// Получить UI модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    Task<AccountUi> GetPersonUiAsync(Guid id);
    JwtSecurityToken GetToken(ClaimsIdentity identity);
    Task<ClaimsIdentity> GetIdentity(string email, string password);
    Task GiveTheUserARole(string email, string role);
    Task<AccountUi> Registration(RegistrationModel model);
    Task DeleteAccount(Guid accountId);
    Task UpdateAccountInfo(UpdateAccountInfoModel model);
}
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
    /// <summary>
    /// Получить токен
    /// </summary>
    /// <param name="identity"></param>
    /// <returns></returns>
    JwtSecurityToken GetToken(ClaimsIdentity identity);
    /// <summary>
    /// Получить claims
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<ClaimsIdentity> GetIdentity(string email, string password);
    /// <summary>
    /// Выдать роль пользователя
    /// </summary>
    /// <param name="email"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task GiveTheUserARole(string email, string role);
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<AccountUi> Registration(RegistrationModel model);
    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task DeleteAccount(Guid accountId);
    /// <summary>
    /// Обновить базовую информацию о пользователе
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task UpdateAccountInfo(UpdateAccountInfoModel model);
}
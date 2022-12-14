using Postter.Controllers.Account.Models;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.AccountRepository;

public interface IAccountRepository
{
    ///<summary>
    /// Получить UI модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    Task<AccountUi> GetPersonUiAsync(Guid id);
    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    Task<AccountEntity> GetPersonAsync(string password, string email);
    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    Task<AccountEntity> GetPersonAsync(string email);
    /// <summary>
    /// Обновить сущность пользователя
    /// </summary>
    /// <param name="accountEntity"></param>
    Task UpdatePersonInfo(AccountEntity accountEntity);
    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    Task<AccountEntity> FindPersonAsync(Guid id);
    /// <summary>
    /// Проверить уникальность почты
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns>True - пользователя с постой нет в системе</returns>
    Task<bool> CheckMailUniqueness(string email);
    /// <summary>
    /// Добавить сущность пользователя
    /// </summary>
    /// <param name="newAccountEntity">Пользователь</param>
    Task<AccountUi> AddPerson(AccountEntity newAccountEntity);
    /// <summary>
    /// Отметить аккаунт удалённым
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task DeleteAccount(Guid accountId);
}
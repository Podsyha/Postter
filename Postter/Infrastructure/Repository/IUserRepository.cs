using Postter.Infrastructure.DTO;

namespace Postter.Infrastructure.Repository;

public interface IUserRepository
{
    /// <summary>
    /// Получить DTO модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    Task<UserDto> GetUserAsync(string password, string email);

    /// <summary>
    /// Найти DTO модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<UserDto> FindUserAsync(string password, string email);

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    Task AddUserAsync(string email, string password);
 }
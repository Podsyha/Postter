using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.Persons;

public interface IPersonRepository
{
    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(string password, string email);

    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(string email);

    /// <summary>
    /// Обновить сущность пользователя
    /// </summary>
    /// <param name="person"></param>
    Task UpdatePersonInfo(Person person);

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Person> FindPersonAsync(string password, string email);

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Person> FindPersonAsync(string email);
}
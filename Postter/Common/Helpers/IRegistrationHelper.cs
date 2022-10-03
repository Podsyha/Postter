namespace Postter.Common.Helpers;

public interface IRegistrationHelper
{
    /// <summary>
    /// Сгенерировать соль для пароля
    /// </summary>
    /// <returns>Рандомная строка из выборки</returns>
    string generateSalt();

    /// <summary>
    /// Сгенерировать хеш пароля на основе пароля и соли
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="salt">Соль</param>
    /// <returns>Хеш пароля</returns>
    string generateHashPass(string password, string salt);
}
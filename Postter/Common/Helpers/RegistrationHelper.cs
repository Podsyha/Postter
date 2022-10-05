using System.Security.Cryptography;
using System.Text;

namespace Postter.Common.Helpers;

/// <summary>
/// Класс-утилита с компонентами-хелперами для регистрации
/// </summary>
public sealed class RegistrationHelper : IRegistrationHelper
{
    private static readonly string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                              "abcdefghijklmnopqrstuvwxyz" + 
                                              "0123456789" +
                                              "!@#$%^&*()_+";

    /// <summary>
    /// Сгенерировать соль для пароля
    /// </summary>
    /// <returns>Рандомная строка из выборки</returns>
    public string generateSalt()
    {
        int length = 20;
        
        Random random = new Random();
        char[] result = new char[length];

        for (int i = 0; i < length; ++i)
        {
            int index = random.Next(ALPHABET.Length);

            result[i] = ALPHABET[index];
        }

        return new string(result);
    }

    /// <summary>
    /// Сгенерировать хеш пароля на основе пароля и соли
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="salt">Соль</param>
    /// <returns>Хеш пароля</returns>
    public string generateHashPass(string password, string salt)
    {
        MD5 md5 = MD5.Create();
        
        return Convert.ToHexString(md5.ComputeHash(Encoding.ASCII.GetBytes(salt + password)));
    }
}
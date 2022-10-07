using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Postter.Common.Auth;

/// <summary>
/// Класс-настройка для формирования JWT
/// </summary>
public static class AuthOptions
{
    public const string ISSUER = "PostterServer";
    public const string AUDIENCE = "http://localhost:44325/";
    public const int LIFETIME = 1; // стоковое значение 5 минут
    const string KEY = "Min16SymbolForHmacSha256";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new (Encoding.ASCII.GetBytes(KEY));
}
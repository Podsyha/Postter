using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Postter.Common.Auth;

public class AuthOptions
{
    public const string ISSUER = "PostterServer"; // издатель токена
    public const string AUDIENCE = "http://localhost:44325/"; // потребитель токена
    public const int LIFETIME = 1; // время жизни токена - 1 минута
    const string KEY = "16symbolForHmacSha256";   // ключ для шифрации
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
}
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Postter.Common.Auth;

public static class AuthOptions
{
    public const string ISSUER = "PostterServer";
    public const string AUDIENCE = "http://localhost:44325/";
    public const int LIFETIME = 5;
    const string KEY = "Min16SymbolForHmacSha256";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
}
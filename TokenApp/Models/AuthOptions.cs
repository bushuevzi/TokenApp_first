using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenApp.Models
{
    public class AuthOptions
    {
        //издатель токена
        public const string ISSUER = "MyAuthServer";
        //потребитель токена
        public const string AUDIENCE = "http://localhost:51884";
        //ключ для шифрации
        const string KEY = "mysupersecret_secret!123";
        public const int LIFETIME = 1; //время жизни токена - 1 минута
        //симетричный ключ шифрования
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

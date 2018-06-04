using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAintFunny.Core.Web.Jwt
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }

}

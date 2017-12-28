using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.UI.api.Helper
{
    public static class apiSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}

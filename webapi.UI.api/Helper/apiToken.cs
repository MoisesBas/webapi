using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.UI.api.Helper
{
    public sealed class apiToken
    {
        private JwtSecurityToken token;

        internal apiToken(JwtSecurityToken token)
        {
            this.token = token;
        }
        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}

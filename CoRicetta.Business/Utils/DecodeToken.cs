using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Utils
{
    public class DecodeToken
    {
        private JwtSecurityTokenHandler _tokenHandler;

        public DecodeToken()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string DecodeText(string token, string nameClaim)
        {
            var claim = _tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            return claim.Value;
        }
    }
}

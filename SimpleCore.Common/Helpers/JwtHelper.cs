using Microsoft.IdentityModel.Tokens;
using SimpleCore.Common.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Helpers
{
    public class JwtHelper
    {
        public static string CreateJwToken(Claim[] claims)
        {
            var salt = AppSettingsHelper.GetSection<SaltSetting>("Salt");
            var now = DateTime.Now;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(salt.Secret));
            var issu = salt.Issuer;
            var aud = salt.Audience;
            var jwt = new JwtSecurityToken(
                issuer: issu,
                audience: aud,
                claims: claims,
                notBefore: now,
                expires: DateTime.Now.AddSeconds(3600),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public static List<Claim> CreateJwtTokenClaims(string userName, string userId, string userRolesStr)
        {
            var salt = AppSettingsHelper.GetSection<SaltSetting>("Salt");
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var iat = Convert.ToInt64(ts.TotalSeconds);


            var claims = new List<Claim>
            {
                new Claim("scope", salt.Scope),
                new Claim(JwtRegisteredClaimNames.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti,userId),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
            };
            claims.AddRange(userRolesStr.Split(",").Select(s => new Claim(ClaimTypes.Role, s.ToString())));

            return claims;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.HttpContext
{
    public class SysUser : ISysUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SysUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public string Name => GetName();

        public long ID => GetClaimValueByType("jti").FirstOrDefault() is string idStr && long.TryParse(idStr, out var id) ? id : 0;


        private string GetName()
        {
            if (IsAuthenticated() && !string.IsNullOrWhiteSpace(_contextAccessor?.HttpContext?.User?.Identity?.Name))
            {
                return _contextAccessor.HttpContext.User.Identity.Name;
            }

            return GetClaimValueByType("name").FirstOrDefault() ?? string.Empty;
        }

        public List<string> GetClaimValueByType(string claimType)
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            return claims
                .Where(c => c.Type == claimType)
                .Select(c => c.Value)
                .ToList();
        }
    }

}

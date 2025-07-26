using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.HttpContext
{
    public interface ISysUser
    {
        string Name { get; }
        long ID { get; }
        List<string> GetClaimValueByType(string ClaimType);

    }
}

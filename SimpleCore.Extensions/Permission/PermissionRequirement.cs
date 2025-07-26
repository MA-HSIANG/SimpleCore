using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
    }
}

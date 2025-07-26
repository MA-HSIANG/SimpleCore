using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.Permission
{
    public class PermissionItem
    {
        public virtual string RoleName { get; set; } = string.Empty;
        public virtual string Url { get; set; }
    }
}

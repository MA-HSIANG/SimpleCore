using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Model.Dtos
{
    public class AccountIninfoDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LoginName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPwd { get; set; } = string.Empty;
    }
}

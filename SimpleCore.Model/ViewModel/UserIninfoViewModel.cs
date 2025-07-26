using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Model.ViewModel
{
    public class UserIninfoViewModel
    {
        public long Id { get; set; }
        public string LoginName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}

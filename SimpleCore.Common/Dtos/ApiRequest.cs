using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Dtos
{
    public class ApiRequest<T>
    {
        public T? Data { get; set; }
    }
}

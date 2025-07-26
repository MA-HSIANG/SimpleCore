using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Model.RootKey
{
    public class RootKey<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}

using SimpleCore.Model.RootKey;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Model
{
    [Table("Role")]
    public class RoleModel:RootKey<long>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        /// <summary>
        /// 標誌 Admin、User...
        /// </summary>
        public string Code { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

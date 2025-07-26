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
    [Table("UserRole")]
    public class UserRoleModel:RootKey<long>
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

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
    [Table("RoleMenuModule")]
    public class RoleMenuModuleModel:RootKey<long>
    {
        [Required]
        public long MenuId { get; set; }
        [Required]
        public long RoleId { get; set; }
        /// <summary>
        /// 接口ID
        /// </summary>
        [Required]
        public long ModuleId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }

        [NotMapped]
        public RoleModel Role { get; set; }
        [NotMapped]
        public ModuleModel Module { get; set; }
    }
}

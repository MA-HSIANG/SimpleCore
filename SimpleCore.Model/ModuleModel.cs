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
    [Table("Module")]
    public class ModuleModel :RootKey<long>
    {
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// API Url
        /// </summary>
        [Required]
        [MaxLength(3000)]
        public string LinkUrl { get; set; }
        [Required]
        [MaxLength(100)]
        public string Controller { get; set; }
        [Required]
        [MaxLength(100)]
        public string Action { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}

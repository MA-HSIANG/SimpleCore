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
    [Table("Menu")]
    public class MenuModel:RootKey<long>
    {
        [Required]
        [MaxLength(30)]
        /// <summary>
        /// 路由名稱
        /// </summary>
        public string Name { get; set; }
        [MaxLength(50)]
        /// <summary>
        /// 中文名稱
        /// </summary>
        public string? Title { get; set; }
        [MaxLength(100)]
        /// <summary>
        /// 對應前端Route Parh 
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 父選單
        /// </summary>
        [Required]
        public long ParentId { get; set; }
        /// <summary>
        /// 介面id
        /// </summary>
        public long ModuleId { get; set; }
        public string? Icon { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

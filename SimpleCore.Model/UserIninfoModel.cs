using SimpleCore.Model.RootKey;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCore.Model
{
    [Table("UserIninfo")]
    public class UserIninfoModel : RootKey<long>
    {
        public string Name { get; set; } = string.Empty;
        public string LoginName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; }
        [Required]
        public long CreateId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

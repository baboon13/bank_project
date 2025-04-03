using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    [Table("LikeListData")]
    public class LikeListData
    {
        [Key] // 明确指定主键
        public int SN { get; set; }

        [Required]
        [StringLength(10)]
        public string OrderName { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        public int? TotalFee { get; set; } // 允許 NULL

        public int? TotalAmount { get; set; }
    }
}


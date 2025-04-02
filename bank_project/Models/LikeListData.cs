using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    [Table("LikeListData")]
    public class LikeListData
    {
        [Key] // 明确指定主键
        [Column("SN")]
        public int SN { get; set; }

        [Required]
        [Column("OrderName")]
        [StringLength(10)]
        public string OrderName { get; set; }

        [Required]
        [Column("Account")]
        [StringLength(50)]
        public string Account { get; set; }

        [Column("TotalFee")]
        public int? TotalFee { get; set; } // 允許 NULL

        [Column("TotalAmount")]
        public int? TotalAmount { get; set; }
    }
}


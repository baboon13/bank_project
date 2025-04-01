using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    public class LikeList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SN { get; set; }  //流水序號

        [ForeignKey("UserID")]
        // ✅ 單數導航屬性
        public User User { get; set; }
        public int UserId { get; set; }       

        [ForeignKey("No")]
        public Product Product { get; set; }
        public int No { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        public string OrderName { get; set; }  // 購買數量
        public int? OrderNUM { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalFee { get; set; } // 改為 decimal 類型

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // 改為 decimal 類型

    }
}

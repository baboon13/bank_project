using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using bank_project.Models;

namespace bank_project.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { get; set; } //產品流水序號

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal FeeRate { get; set; } // 以百分比儲存，例如 10.00 表示 10%

        public ICollection<LikeList> LikeList { get; set; }
    }
}
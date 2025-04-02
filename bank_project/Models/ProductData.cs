using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using bank_project.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_project.Models
{

    [Table("ProductData")]
    public class ProductData
    {
        [Key] 
        [Column("No")]
        [StringLength(20)]
        public string No { get; set; }

        [Required]
        [Column("Product Name")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column("Price")]
        public int Price { get; set; }

        [Column("Fee rate")]
        [Precision(18, 2)] // 指定精度
        public decimal FeeRate { get; set; }

        //public ICollection<LikeList> LikeLists { get; set; }
    }

}

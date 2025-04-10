using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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

        // 設定外鍵，關聯到 Product 的 No
        [Required]
        [StringLength(20)]
        public string No { get; set; }  // 商品編號

        // 設定外鍵關聯
        [ForeignKey("No")]
        public ProductData Product { get; set; }  // 這會讓我們可以直接訪問商品資料

        // 使用 IdentityUser 的 Id 作為外鍵
        [Required]
        [ForeignKey("Id")]
        public string UserId { get; set; }  // 外鍵：UserId

        // 關聯到 UserData（即 IdentityUser）
        public UserData User { get; set; }
    }
}

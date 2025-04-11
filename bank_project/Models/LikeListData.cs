using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bank_project.Models
{
    [Table("LikeListData")]
    public class LikeListData
    {
        [Key] // 明确指定主键
        public int SN { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        public int? TotalFee { get; set; } // 允許 NULL

        public int? TotalAmount { get; set; }

        // ➕ 新增商品數量
        // ✅ 新增的欄位：數量
        [Required]
        [Range(1, 1000, ErrorMessage = "數量必須介於 1 到 1000")]
        public int Quantity { get; set; } = 1;  // ← 預設值 1


        // 👉 移除 OrderName（若已存在）

        // 設定外鍵，關聯到 Product 的 No
        [Required]
        [StringLength(20)]
        public string No { get; set; }  // 商品編號

        [ValidateNever] // ← 加這行會跳過 MVC 的 model binding 驗證
        [ForeignKey("No")]
        public ProductData Product { get; set; }  // 這會讓我們可以直接訪問商品資料

        // 使用 IdentityUser 的 Id 作為外鍵
        [Required]
        [ForeignKey("Id")]
        public string UserId { get; set; }  // 外鍵：UserId

        // 關聯到 UserData（即 IdentityUser）
        [ValidateNever]
        public UserData User { get; set; } // ← 加這行會跳過 MVC 的 model binding 驗證

    }
}

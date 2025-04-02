using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    public class LikeListCreateModel
    {
        [Required(ErrorMessage = "請選擇產品")]
        [Display(Name = "產品編號")]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "請輸入購買數量")]
        [Range(1, 100, ErrorMessage = "數量必須介於1-100")]
        [Display(Name = "購買數量")]
        public int OrderName { get; set; }

        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件")]
        public string? Email { get; set; } // 可選欄位
        
    }
}

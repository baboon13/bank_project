using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "使用者名稱只能包含英文字母與數字")]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入帳號編號")]
        [Display(Name = "帳號編號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "密碼至少 3 個字元")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請再次輸入密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不一致")]
        [Display(Name = "確認密碼")]
        public string? ConfirmPassword { get; set; }
    }
}

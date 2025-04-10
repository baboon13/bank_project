using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "記住我")]
        public bool RememberMe { get; set; }
    }
}

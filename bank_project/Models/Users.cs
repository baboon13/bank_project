using bank_project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Users
{
    [Key]
    public int UserID { get; set; }

    [Required(ErrorMessage = "使用者名稱必填")]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "電子郵件必填")]
    [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
    [StringLength(100)]
    public string Email { get; set; }

    [Required(ErrorMessage = "扣款帳號必填")]
    [StringLength(50)]
    public string Account { get; set; }

    [Required(ErrorMessage = "密碼必填")]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password1 { get; set; }


    // 密碼鹽值 (用於加密)
    public string Salt { get; set; }
    public ICollection<LikeList> LikeList { get; set; }
}

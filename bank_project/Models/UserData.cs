using bank_project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace bank_project.Models;

[Table("UserData")] // 對應 SQL Server 中的表名
public class UserData : IdentityUser
{
    // 不需要定義 UserID，因為 IdentityUser 已經提供 Id 屬性
    //[Key]
    //[StringLength(20)]
    //public string UserID { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [EmailAddress] 
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Account { get; set; }

    // 注意：不應該直接存儲明文密碼
    // 應該使用 Identity 的 PasswordHash
    [NotMapped]  // 標記為不映射到資料庫
    public string password1 { get; set; }

}

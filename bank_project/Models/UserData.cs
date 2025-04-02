using bank_project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace bank_project.Models;

[Table("UserData")] // 對應 SQL Server 中的表名
public class UserData
{
    [Key]
    [Column("UserID")]
    [StringLength(20)]
    public string UserID { get; set; }

    [Required]
    [Column("UserName")]
    [StringLength(50)]
    public string UserName { get; set; }

    [EmailAddress] 
    [Column("Email")]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [Column("Account")]
    [StringLength(50)]
    public string Account { get; set; }


    public string password1 { get; set; }


}

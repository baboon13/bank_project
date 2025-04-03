using bank_project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace bank_project.Models;

[Table("UserData")] // 對應 SQL Server 中的表名
public class UserData
{
    [Key]
    [StringLength(20)]
    public string UserID { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [EmailAddress] 
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Account { get; set; }



}

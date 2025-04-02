using System.ComponentModel.DataAnnotations;

namespace bank_project.Models
{
    public class LikeListViewModel
    {
        public int SN { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "價格")]
        public int Price { get; set; }

        [Display(Name = "手續費率")]
        public decimal FeeRate { get; set; }

        [Display(Name = "購買數量")]
        public int OrderName { get; set; }

        [Display(Name = "總金額")]
        public int TotalAmount { get; set; }

        [Display(Name = "總手續費")]
        public int TotalFee { get; set; }

        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Display(Name = "扣款帳號")]
        public string Account { get; set; }
    }
}
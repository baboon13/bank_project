using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace bank_project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LikeListController : ControllerBase
    {
        private readonly ILikeListService _likeListService;
        private readonly ILogger<LikeListController> _logger;

        public LikeListController(
            ILikeListService likeListService,
            ILogger<LikeListController> logger)
        {
            _likeListService = likeListService;
            _logger = logger;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAvailableProducts()
        {
            var products = await _likeListService.GetAvailableProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToLikeList([FromBody] LikeListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var likeList = new LikeList
                {
                    No = request.No,
                    Account = request.Account,
                    OrderNUM = request.OrderNum
                };

                await _likeListService.AddToLikeListAsync(userId, likeList);
                return Ok(new { Message = "已成功加入喜好清單" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加至喜好清單失敗");
                return StatusCode(500, new { Message = "添加失敗，請稍後再試" });
            }
        }

        // 其他 API 端點...
    }

    public class LikeListRequest
    {
        [Required]
        public int No { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [Range(1, 100)]
        public int OrderNum { get; set; }
    }
}

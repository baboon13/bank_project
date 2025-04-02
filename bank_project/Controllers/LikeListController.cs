using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Mvc;
using System;

public class LikeListController : Controller
{
    private readonly DapperService _dapper;
    private readonly ApplicationDbContext _context;

    public LikeListController(DapperService dapper, ApplicationDbContext context)
    {
        _dapper = dapper;
        _context = context;
    }

    // 查詢喜好清單
    public async Task<IActionResult> Index()
    {
        var likeLists = await _dapper.QueryStoredProcedure<LikeListData>(
            "sp_GetLikeLists",
            new { Account = User.Identity.Name });

        return View(likeLists);
    }

    // 新增喜好商品
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LikeListCreateModel model)
    {
        if (ModelState.IsValid)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 使用交易確保多表操作一致性
                    await _dapper.ExecuteStoredProcedure("sp_AddLikeList", new
                    {
                        model.ProductNo,
                        model.OrderName,
                        Account = User.Identity.Name
                    });

                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        return View(model);
    }

    // 其他動作方法...
}
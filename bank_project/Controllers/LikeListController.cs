using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Mvc;
using System;

public class LikeListController : Controller
{
    private readonly ApplicationDbContext context;

    public LikeListController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // 喜好清單
    public IActionResult Index()
    {
        var likeList = context.LikeLists.ToList();

        return View(likeList);
    }

}
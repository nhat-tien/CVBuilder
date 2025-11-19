using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
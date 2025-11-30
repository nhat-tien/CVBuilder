using CVBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Areas.User.Controllers;

[Area("User")]
public class DashboardController : Controller
{
    // GET: Dashboard
    public ActionResult Index()
    {
        var model = new DashboardViewModel
        {
            UserName = User.Identity?.Name ?? "User",
            TotalCVs = 5,
            LastLogin = DateTime.Now.AddDays(-1)
        };

        return View(model);
    }
}

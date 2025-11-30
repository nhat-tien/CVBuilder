using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Areas.User.Controllers;

[Area("User")]
public class PartialController: Controller
{
    [HttpGet("Partial/ChooseTemplate")]
    public IActionResult ChooseTemplate()
    {
        return PartialView("_ChooseTemplate");
    }
}


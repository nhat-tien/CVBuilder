using CVBuilder.Models;

namespace CVBuilder.Areas.User.ViewModels.CV;

public class CVIndexViewModel
{
    public ICollection<Models.CV> CVs { get; set; } = [];
    public ICollection<Template> Templates { get; set; } = [];
}


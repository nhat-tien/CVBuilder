using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Models;
public class Template
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string PreviewImageUrl { get; set; } = "";

    public string HtmlContent { get; set; } = "";

    public string? UserId { get; set; }
    public User? User { get; set; }

    public ICollection<CV> CVs { get; set; } = null!;
}


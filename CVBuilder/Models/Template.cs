using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Models;
public class Template
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string PreviewImageUrl { get; set; } = "https://placehold.co/300x400?text=template";
  
    public string HtmlContent { get; set; } = "";

    public string? UserId { get; set; }
    public User? User { get; set; }

    public ICollection<CV> CVs { get; set; } = [];
}


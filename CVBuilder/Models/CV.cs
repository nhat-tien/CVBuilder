namespace CVBuilder.Models;

public class CV
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;

    public int TemplateId { get; set; }
    public Template Template { get; set; } = null!;

    public string FileName { get; set; } = "MyCV.pdf";

    public string? Title { get; set; }

    public string? ThemeColor { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}


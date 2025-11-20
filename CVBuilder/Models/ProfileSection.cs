namespace CVBuilder.Models;

public class ProfileSection
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
    public string Title { get; set; } = "";
    public string Subsection { get; set; } = "";
}


namespace CVBuilder.Models;
public class ProfileLink
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
    public string Title {get; set; } = "";
    public string Href {get; set; } = "";
    public string Icon {get; set; } = "";
}


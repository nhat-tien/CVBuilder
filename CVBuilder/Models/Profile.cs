using System.ComponentModel;

namespace CVBuilder.Models;

public class Profile
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [DefaultValue(false)]
    public bool IsDefault {get; set; }

    public string FullName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Summary { get; set; } = "";

    public string Email { get; set; } = null!;
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";

    public ICollection<ProfileLink> ProfileLink { get; set; } = null!;
    public ICollection<ProfileSection> ProfileSections { get; set; } = null!;
}

using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Models;

public class User: IdentityUser
{
        [PersonalData]
        public string Name { get; set; } = "";
        [PersonalData]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Profile> Profiles { get; set; } = null!;
        public ICollection<CV> CVs { get; set; } = null!;
}


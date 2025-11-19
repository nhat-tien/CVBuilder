using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Models;

public class User: IdentityUser
{
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
}


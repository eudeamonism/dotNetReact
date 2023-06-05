using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        //Additional properties
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
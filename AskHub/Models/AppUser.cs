using Microsoft.AspNetCore.Identity;

namespace AskHub.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}

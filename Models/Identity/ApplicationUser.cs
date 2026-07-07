using Microsoft.AspNetCore.Identity;

namespace MiniCMS.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
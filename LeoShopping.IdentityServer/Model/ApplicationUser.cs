using Microsoft.AspNetCore.Identity;

namespace LeoShopping.IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace BulkyBookWeb.Models
{
    public class MyAppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}

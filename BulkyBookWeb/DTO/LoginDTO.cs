using Microsoft.Build.Framework;

namespace BulkyBookWeb.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string PassWord { get; set; }
    }
}

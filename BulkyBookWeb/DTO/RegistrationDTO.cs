//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.DTO
{
    public class RegistrationDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]  
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage ="minimun length is 8 and must contain  lowercase,  uppercase,  digit and  special character" )]
        public string PassWord { get; set; }
        public string ConfirmedPassWord { get; set; }
        [Required]  
        public string UserName { get; set; }
        [Required]  
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
        [Required]
        public string Gender { get; set; } 

    }
}

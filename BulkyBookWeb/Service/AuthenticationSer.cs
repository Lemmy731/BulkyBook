using BulkyBookWeb.DTO;
using BulkyBookWeb.IService;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BulkyBookWeb.Service
{
    public class AuthenticationSer : IAuthenticationSer
    {
        private readonly SignInManager<MyAppUser> _signInManager;
        private readonly UserManager<MyAppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationSer(SignInManager<MyAppUser> signInManager, UserManager<MyAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager; 
            _userManager = userManager; 
            _roleManager = roleManager; 
        }
        public async Task<StatusDTO> LoginAsync(LoginDTO loginDTO)
        {
            StatusDTO statusDTO = new StatusDTO();
            var result = await _userManager.FindByNameAsync(loginDTO.UserName);
            if(result == null)
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "user does not exist";
                return statusDTO;
            }
            if(!await _userManager.CheckPasswordAsync(result, loginDTO.PassWord))
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "invalid password";
                return statusDTO;   
            }
            var signInResult = await _signInManager.PasswordSignInAsync(result, loginDTO.PassWord, false, true);
            if(signInResult.Succeeded) 
            {
                var userRoles = await _userManager.GetRolesAsync(result);
                var authclaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authclaim.Add(new Claim(ClaimTypes.Role, userRole));
                }
                statusDTO.StatusCode = 1;
                statusDTO.Message = "user signin sucessfully";
                return statusDTO;
            }
            else if(signInResult.IsLockedOut)
                {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "user lockout";
                return statusDTO;
                }
            else
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "error login in";
                return statusDTO;
            }
            
        }

        public async Task LogoutAsync()
        {
           await _signInManager.SignOutAsync();
        }

        public async Task<StatusDTO> RegistrationAsync(RegistrationDTO registrationDTO)
        {
            StatusDTO statusDTO = new StatusDTO();
            var userExist = await _userManager.FindByNameAsync(registrationDTO.UserName);
            if(userExist != null)
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "user already exist";
                return statusDTO;
            }
            MyAppUser myAppUser = new MyAppUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
                Email = registrationDTO.Email,
                UserName = registrationDTO.UserName,
                PhoneNumber = registrationDTO.PhoneNumber,
                Gender= registrationDTO.Gender,
                


            };
            var result = await _userManager.CreateAsync(myAppUser, registrationDTO.PassWord);
            if(!result.Succeeded) 
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "fail to create";
                return statusDTO;   
            }
            //role management
            if (!await _roleManager.RoleExistsAsync(registrationDTO.Role))
                await _roleManager.CreateAsync(new IdentityRole(registrationDTO.Role));

            if(await _roleManager.RoleExistsAsync(registrationDTO.Role))
            {
                await _userManager.AddToRoleAsync(myAppUser, registrationDTO.Role);
            }
            statusDTO.StatusCode = 1;
            statusDTO.Message = "user has register sucessfully";
            return statusDTO;   

        }
    }
}

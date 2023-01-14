using BulkyBookWeb.DTO;
using BulkyBookWeb.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IAuthenticationSer _authenticationSer;

        public UserAuthenticationController(IAuthenticationSer authenticationSer)
        {
            _authenticationSer = authenticationSer;
        }
        public IActionResult Registration()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDTO registrationDTO)
        {
            if(!ModelState.IsValid)
            {
                return View(registrationDTO);
            }
            registrationDTO.Role = "user";
            var result = await _authenticationSer.RegistrationAsync(registrationDTO);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var result = await _authenticationSer.LoginAsync(loginDTO);
            if(result.StatusCode==1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
           await _authenticationSer.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationDTO
            {
                FirstName = "victory",
                LastName = "Paul",
                Email = "vic@gmail.com",
                PassWord = "Eye126755@",
                UserName = "greater",
                PhoneNumber = "08157575498",
                Gender = "Male"
            };
            model.Role = "user";
            var result = await _authenticationSer.RegistrationAsync(model);
            return Ok(result);
        }

    }
}

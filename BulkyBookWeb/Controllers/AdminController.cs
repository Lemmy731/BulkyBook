﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Authorize(Roles ="Admin")]
    
    public class AdminController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }
    }
}

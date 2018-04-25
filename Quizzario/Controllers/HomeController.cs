using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Models;
using Microsoft.AspNetCore.Identity;
using Quizzario.Data.Entities;

namespace Quizzario.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Quizes");
            return View();
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Models;
using Quizzario.Models.QuizViewModels;
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
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("MyQuizes", "Quizes");
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public ViewResult Searching()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Searching(SearchingModel searchingModel)
        {
            return View("SearchingByName",searchingModel);
        }
    }
}

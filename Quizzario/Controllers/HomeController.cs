using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Models;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Identity;
using Quizzario.Data.Entities;
using Quizzario.Services;
using Quizzario.BusinessLogic.Abstract;
using System.Linq;

namespace Quizzario.Controllers
{
    public class HomeController : Controller
    {
        private readonly int PageSize = 2;
        private IQuizService quizService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(SignInManager<ApplicationUser> signInManager, IQuizService quizService)
        {
            _signInManager = signInManager;
            this.quizService = quizService;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("MyQuizes", "Quizes");
            return View();
        }

   
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // 06.06
        // Pagination will not work, because it generatates not compatibile routing
        [HttpGet]
        public ActionResult Search(string q, int p = 1)
        {
            ViewData["HideFooter"] = true;
            PagingInfoService pagingInfoService = new PagingInfoService();
           
            var quizesCollection = quizService.SearchByName(q);
            string address = "?q=" + q;
            ViewBag.Address = address;
            foreach (var qu in quizesCollection)
            {
                string s = qu.Description;
            }
            SearchingByNameModel model = new SearchingByNameModel
            {
                Quizes = quizesCollection.
                OrderBy(o => o.Title).
                Skip((p - 1) * PageSize).
                Take(PageSize),
                PagingInfo = pagingInfoService.GetMetaData(quizesCollection.Count(),
                p, PageSize),
                allQuizes = quizesCollection.Count()
            };

            return View(model);
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
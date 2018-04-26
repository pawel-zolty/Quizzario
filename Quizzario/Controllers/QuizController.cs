using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Services;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        public int PageSize = 2;
        private IQuizService quizService;
        private PagingInfoService pagingInfoService = new PagingInfoService();

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public ViewResult List(int p = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);            
            var quizesCollection = quizService.GetAllUserQuizes(userId);

            QuizListViewModel model = new QuizListViewModel
            {
                Quizes = quizesCollection.                    
                    OrderBy(q => q.Title).
                    Skip((p - 1) * PageSize).
                    Take(PageSize),
                PagingInfo = pagingInfoService.GetMetaData(quizesCollection.Count(),
                p, PageSize)
            };

            return View(model);
        }

        public ViewResult Favourite(int p = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var quizesCollection = quizService.GetUserFavouriteQuizes(userId);

            QuizListViewModel model = new QuizListViewModel
            {
                Quizes = quizesCollection.
                    OrderBy(q => q.Title).
                    Skip((p - 1) * PageSize).
                    Take(PageSize),
                PagingInfo = pagingInfoService.GetMetaData(quizesCollection.Count(),
                p, PageSize)
            };

            return View(model);
        }

        [HttpGet]
        public ViewResult Searching()
        {

            return View();
        }

        [HttpPost]
        public ViewResult Searching(SearchingModel searchingModel, string returnUrl = null, int p=1)
        {
            ViewData["ReturnUrl"] = returnUrl;
            string title = "User's 1 Quiz";
            PagingInfoService pagingInfoService = new PagingInfoService();
            var quizesCollection = quizService.SearchByName(searchingModel.Name);
            if (quizesCollection.ElementAt(0) == null)
            {
                return View("SearchingByName");
            }
            else
            {
                SearchingByNameModel model = new SearchingByNameModel
                {
                    Quizes = quizesCollection.
                   OrderBy(q => q.Title).
                   Skip((p - 1) * PageSize).
                   Take(PageSize),
                    PagingInfo = pagingInfoService.GetMetaData(quizesCollection.Count(),
               p, PageSize)
                };

                return View("SearchingByName", model);
            }
        }
        public ViewResult EditQuiz()
        {
            return View(quizService.Quizes);
        }
        public ViewResult Edit(string Id)
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id == Id);
            return View(quizDTO);
        }
        [HttpPost]
        public  ActionResult Edit(QuizDTO quizDTO)
        {
            if(ModelState.IsValid)
            {
                quizService.SaveQuiz(quizDTO);
                TempData["message"] = string.Format("Zapisano {0}", quizDTO.Title);
                return RedirectToAction("MyQuizes");
            }
            return View(quizDTO);
        }
        public ViewResult Create()
        {
            return View("Edit", new QuizDTO());
        }
    }

}
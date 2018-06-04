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
        public ViewResult Searching(SearchingModel searchingModel, string returnUrl = null, int p = 1)
        {
            ViewData["ReturnUrl"] = returnUrl;
            // string title = "User's 1 Quiz";
            PagingInfoService pagingInfoService = new PagingInfoService();
            var quizesCollection = quizService.SearchByName(searchingModel.Name);
            if (quizesCollection.Count == 0)
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

    }

}
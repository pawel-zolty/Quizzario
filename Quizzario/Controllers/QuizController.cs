using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Services;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
    }
}
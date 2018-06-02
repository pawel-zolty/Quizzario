using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Services;
using System.Security.Claims;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.Controllers
{
    [Authorize]
    public class QuizesController : Controller
    {
        public int PageSize = 2;
        private IQuizService quizService;
        private PagingInfoService pagingInfoService = new PagingInfoService();

        public QuizesController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public ViewResult MyQuizes(int p = 1)
        {
            QuizListViewModel model = CreateQuizViewModelWithPagination(p);
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

        [HttpPost]
        public ViewResult AddToFavourite(QuizDTO model)
        {
            return null;
        }

        public IActionResult Index(int p = 1)
        {
            QuizListViewModel model = CreateQuizViewModelWithPagination(p);
            return View(model);
        }

        private QuizListViewModel CreateQuizViewModelWithPagination(int p)
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
            return model;
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
        public ActionResult Edit(QuizDTO quizDTO)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (quizDTO.ApplicationUserId == null)
                quizDTO.ApplicationUserId = userId;
            if (ModelState.IsValid)
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
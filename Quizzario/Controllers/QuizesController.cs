using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Services;
using System.Security.Claims;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;
using Quizzario.Extensions;
using Quizzario.BusinessLogic.Factories;
using Quizzario.BusinessLogic.Abstracts;

namespace Quizzario.Controllers
{
    [Authorize]
    public class QuizesController : Controller
    {
        public int PageSize = 4;
        private IQuizService quizService;
        private IApplicationUserDTOFactory userFactory;
        private PagingInfoService pagingInfoService = new PagingInfoService();

        public QuizesController(IQuizService quizService, IApplicationUserDTOFactory userFactory)
        {
            this.quizService = quizService;
            this.userFactory = userFactory;
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
        public void AddToFavourite(string quizId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            quizService.AddQuizToFavourite(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromFavourite(string quizId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            quizService.RemoveQuizFromFavourite(userId, quizId);
        }
        public ViewResult Summary(string Id)
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id == Id);
            return View(quizDTO);
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
            var model = new CreateQuizViewModel();
            return View("Create", model);
        }
        [HttpPost]
        public StatusCodeResult Create([FromForm] CreateQuizViewModel quizViewModel)
        {
            var userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userFactory.CreateUserWithId(userid);

            quizViewModel.Questions.Add(new CreateQuestionViewModel
            {
                Question = "First question",
                Answers = new List<CreateAnswerViewModel>()
            });

            quizViewModel.Questions.Add(new CreateQuestionViewModel
            {
                Question = "Second question",
                Answers = new List<CreateAnswerViewModel>()
            });

            foreach(var subViewModel in quizViewModel.Questions)
            {
                subViewModel.Answers.Add(new CreateAnswerViewModel
                {
                    Answer = "1st answer",
                    isCorrect = true
                });

                subViewModel.Answers.Add(new CreateAnswerViewModel
                {
                    Answer = "2nd answer",
                    isCorrect = false
                });
            }

            quizService.CreateQuiz(DTOMapper.Map(quizViewModel, user));
            return StatusCode(200);
        }

        [HttpPost]
        public PartialViewResult AddQuestion([FromBody]CreateQuizViewModel model)
        {
            model.Questions.Add(new CreateQuestionViewModel());
            return PartialView("CreateQuizQuestionPartialView", model.Questions);
        }

        [HttpPost]
        public PartialViewResult AddAnswer([FromBody]List<CreateAnswerViewModel> models)
        {
            models.Add(new CreateAnswerViewModel());
            return PartialView("CreateQuizAnswerPartialView", models);
        }

        /// <summary>
        /// Full version of action will require at least 2 GET parameters: quiz ID and question ID / number
        /// </summary>
        /// <returns></returns>
        public ViewResult Solving()
        {
            return View();
        }

        /// <summary>
        /// Full version of action will require passing result data
        /// </summary>
        /// <returns></returns>
        public ViewResult Results()
        {
            return View();
        }
    }
}
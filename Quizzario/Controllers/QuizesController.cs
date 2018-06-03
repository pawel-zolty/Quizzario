using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Services;
using System.Security.Claims;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Quizzario.Controllers
{
    [Authorize]
    public class QuizesController : Controller
    {
        private readonly int PageSize = 4;
        private IQuizService quizService;
        private IPagingInfoService pagingInfoService;
        private string userId;

        public QuizesController(IQuizService quizService, IPagingInfoService pagingInfoService)
        {
            this.quizService = quizService;
            this.pagingInfoService = pagingInfoService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Get user id   
            this.userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Index(int p = 1)
        {
            QuizListViewModel model = GetMyQuizesModel(p);
            return View("MyQuizes", model);
        }

        public ViewResult MyQuizes(int p = 1)
        {
            QuizListViewModel model = GetMyQuizesModel(p);
            return View(model);
        }

        public ViewResult Favourite(int p = 1)
        {
            var quizesCollection = quizService.GetUserFavouriteQuizes(userId);
            QuizListViewModel model = CreateQuizViewModelWithPagination(p, quizesCollection);
            return View(model);
        }

        /// <summary>
        /// Full version of action will require passing quiz data, to which user is assigned
        /// </summary>
        /// <returns></returns>
        public ViewResult Assigned(int p = 1)
        {
            // This is a placeholder code, all of it will be replaced
            //TO DO 
            /*var myQuizesCollection = quizService.GetAllUserAssignedQuizes(userId);*/
            var myAssignedQuizesCollection = quizService.GetAllUserQuizes(userId);
            QuizListViewModel model = CreateQuizViewModelWithPagination(p, myAssignedQuizesCollection);
            return View(model);
        }

        [HttpPost]
        public void AddToFavourite(string quizId)
        {
            quizService.AddQuizToFavourite(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromFavourite(string quizId)
        {
            quizService.RemoveQuizFromFavourite(userId, quizId);
        }

        //public bool IsFavourite(string quizId)
        //{
        //    return quizService.IsQuizFavourite(userId, quizId);
        //}

        public ViewResult Create()
        {
            var model = new CreateQuizViewModel();
            return View("Create", model);
            //return View("Edit", new QuizDTO());
        }

        public ViewResult Edit(string quizId)
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id.Equals(quizId));
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

        public ViewResult Summary(string Id)
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id.Equals(Id));
            var isFavourite = quizService.IsQuizFavourite(userId, Id);
            ViewBag.IsFavourite = isFavourite;
            return View(quizDTO);
            /* KUBA TO TWOJE CHYBA brakuje jakiegos question view modelu 
            var model = new CreateQuizViewModel();
            return View("Create", model);*/
        }

        [HttpPost]
        public RedirectToActionResult Create([FromForm] CreateQuizViewModel quizViewModel)
        {
            return RedirectToAction("MyQuizes");
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

        private QuizListViewModel CreateQuizViewModelWithPagination(int p, IEnumerable<QuizDTO> quizesCollection)
        {
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

        private QuizListViewModel GetMyQuizesModel(int p)
        {
            var myQuizesCollection = quizService.GetAllUserQuizes(userId);
            QuizListViewModel model = CreateQuizViewModelWithPagination(p, myQuizesCollection);
            return model;
        }
    }
}
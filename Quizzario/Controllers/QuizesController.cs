<<<<<<< HEAD
﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Services;
using System.Security.Claims;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;
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
            var myAssignedQuizesCollection = quizService.GetUserAssignedToPrivateQuizes(userId);
            QuizListViewModel model = CreateQuizViewModelWithPagination(p, myAssignedQuizesCollection);
            return View(model);
        }

        [HttpPost]
        public void AddToFavourite(string quizId)
        {
            quizService.AddQuizToFavourite(userId, quizId);
        }

        [HttpPost]
        public void AddToPrivateAssigned(string quizId)
        {
            quizService.AddQuizToPrivateAssigned(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromFavourite(string quizId)
        {
            quizService.RemoveQuizFromFavourite(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromPrivateAssigned(string quizId)
        {
            quizService.RemoveQuizFromPrivateAssigned(userId, quizId);
        }        

        public ViewResult Create()
        {
            var model = new CreateQuizViewModel();
            return View("Create", model);
            //return View("Edit", new QuizDTO());
        }

        public ViewResult Edit(string Id)
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id.Equals(Id));
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

                return View("MyQuizes", model);
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
=======
﻿using System.Linq;
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
>>>>>>> quiz-creation
}
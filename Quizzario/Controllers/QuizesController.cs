using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Services;
using System.Security.Claims;
using Quizzario.Models.QuizViewModels;
using Microsoft.AspNetCore.Authorization;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Quizzario.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Quizzario.Controllers
{
    [Authorize]
    public class QuizesController : Controller
    {
        private readonly int PageSize = 4;
        private IQuizService quizService;
        private IUserService userService;
        private IPagingInfoService pagingInfoService;
        private string userId;
        private IApplicationUserDTOMapper userMapper;
        private IQuizDTOMapperFromViewModel quizDTOMapperFromViewModel;
        private int x = 0;
        public QuizesController(IQuizService quizService,
            IUserService userService,
            IPagingInfoService pagingInfoService,
            IApplicationUserDTOMapper userMapper,
            IQuizDTOMapperFromViewModel quizDTOMapperFromViewModel)
        {
            this.quizService = quizService;
            this.userService = userService; ;
            this.userMapper = userMapper;
            this.pagingInfoService = pagingInfoService;
            this.quizDTOMapperFromViewModel = quizDTOMapperFromViewModel;
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

        private QuizListViewModel GetMyQuizesModel(int p)
        {
            var myQuizesCollection = quizService.GetAllUserQuizes(userId);
            QuizListViewModel model = CreateQuizViewModelWithPagination(p, myQuizesCollection);
            x++;
            return model;
        }

        public ViewResult MyQuizes(int p = 1)
        {
            QuizListViewModel model = GetMyQuizesModel(p);
            x++;
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
        public void AddToPrivateAssigned(string userName, string quizId)
        {
            var userId = userService.GetUserIdByName(userName);
            if (userId != null)
                quizService.AddQuizToPrivateAssigned(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromFavourite(string quizId)
        {
            quizService.RemoveQuizFromFavourite(userId, quizId);
        }

        [HttpPost]
        public void RemoveFromPrivateAssigned(string userName, string quizId)
        {
            var userId = userService.GetUserIdByName(userName);
            if (userId != null)
                quizService.RemoveQuizFromPrivateAssigned(userId, quizId);
        }


        public ViewResult Edit(string Id)
        {
            CreateQuizViewModel model = null;
            if (TempData.Peek("QuizInCreation") != null)
            {
                string fromTemp = (string)TempData["QuizInCreation"];
                model = JsonConvert.DeserializeObject<CreateQuizViewModel>(fromTemp);
            }
            else
            {
                model = new CreateQuizViewModel();
                QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id.Equals(Id));
                model = quizDTOMapperFromViewModel.CreateQuizViewModelFromQuizDTO(quizDTO);
            }

            TempData["QuizInCreation"] = JsonConvert.SerializeObject(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([FromBody]CreateQuizViewModel quizViewModel)
        {
            TempData.Remove("QuizInCreation");
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);//
            var user = this.userMapper.CreateUserWithId(userId);
            QuizDTO quizDTO = this.quizDTOMapperFromViewModel.Map(quizViewModel, user);
            if (quizDTO.ApplicationUserId == null)//
                quizDTO.ApplicationUserId = userId;//TE LINIE MOGA BYC DO WYWALENIA 
            quizDTO.FilePath = quizDTO.FilePath;
            quizService.SaveQuiz(quizDTO);
            return Json(new { status = "OK" });
        }

        public ViewResult EditQuiz()
        {
            return View(quizService.Quizes);//KTOS WIE CZY POTRZEBNE
        }

        public ViewResult Summary(string Id)//quizId
        {
            QuizDTO quizDTO = quizService.Quizes.FirstOrDefault(p => p.Id.Equals(Id));
            var isFavourite = quizService.IsQuizFavourite(userId, Id);
            var bestScore = quizService.GetBestScore(userId, Id);
            ViewBag.bestScore = bestScore;
            var attpemps = quizService.GetUserAttemps(userId, Id);
            ViewBag.attemps = attpemps;
            ViewBag.IsFavourite = isFavourite;
            var assignedUsers = quizService.GetAssignedToPrivateQuizUsers(Id);//quizId
            ViewBag.AssignedUsers = assignedUsers;
            return View(quizDTO);
            /* KUBA TO TWOJE CHYBA brakuje jakiegos question view modelu 
             * Nope, nie moje
            var model = new CreateQuizViewModel();
            return View("Create", model);*/
        }

        public ViewResult Create()
        {
            CreateQuizViewModel model = null;
            if (TempData.Peek("QuizInCreation") != null)
            {
                string fromTemp = (string)TempData["QuizInCreation"];
                model = JsonConvert.DeserializeObject<CreateQuizViewModel>(fromTemp);
            }
            else
            {
                model = new CreateQuizViewModel();
            }

            TempData["QuizInCreation"] = JsonConvert.SerializeObject(model);
            return View("Create", model);
        }        
        
        [HttpPost]
        public JsonResult Create([FromBody]CreateQuizViewModel quizViewModel)
        {            
            TempData.Remove("QuizInCreation");          
            var user = this.userMapper.CreateUserWithId(userId);
            var quiz = quizDTOMapperFromViewModel.Map(quizViewModel, user);
            
            quizService.CreateQuiz(quiz);
            return Json(new { status = "OK" });
        }

        [HttpPost]
        public PartialViewResult AddQuestion([FromBody]CreateQuizViewModel model)
        {
            model.Questions.Add(new CreateQuestionViewModel());
            TempData["QuizInCreation"] = JsonConvert.SerializeObject(model);
            return PartialView("QuestionsPartialView", model);
        }    

        [HttpPost]
        public PartialViewResult AddAnswer([FromBody]CreateQuizViewModel model)
        {
            if (model.Questions.Count == 0)
            {
                model.Questions.Add(new CreateQuestionViewModel());
            }

            int addedToQuestionIndex = 0;
            for(int i = 0; i < model.Questions.Count; i++)
            {
                if(model.Questions[i].NewAnswerRequested)
                {
                    model.Questions[i].Answers.Add(new CreateAnswerViewModel());
                    addedToQuestionIndex = i;
                }
            }
            TempData["QuizInCreation"] = JsonConvert.SerializeObject(model);
            return PartialView("AnswersPartialView", model.Questions[addedToQuestionIndex]);
        }        

        /// <summary>
        /// Should return model for a first question to the view or something
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Solving(string Id)
        {
            QuizDTO quizDTO = this.quizService.GetQuiz(Id);

            if(quizDTO == null)
            {
                return View("NotFoundErrorPage");
            }
            SolveDTO solveDTO = new SolveDTO(quizDTO);
            solveDTO.UserId = userId;
            HttpContext.Session.SetString("QuizAttempt-" + solveDTO.quizID, JsonConvert.SerializeObject(solveDTO));

            // Total number of questions. Used to generate content in left panel and links
            ViewData["TotalNumberOfQuestions"] = quizDTO.Questions.Count;
            SolvingQuizQuestionViewModel question = new SolvingQuizQuestionViewModel(quizDTO.Questions.ElementAt(0), 0, Id);
            
            return View(question);
        }

        /// <summary>
        /// Returns question of said number from model stored in session
        /// </summary>
        /// <param name="number">Number of a question stored in session</param>
        /// <returns>Partial view for question</returns>
        [HttpPost]
        public PartialViewResult SolvingGetQuestion(int number, string quizId)
        {
            string solveDTOString = HttpContext.Session.GetString("QuizAttempt-" + quizId);
            var quizDTO = this.quizService.GetQuiz(quizId);
            if(quizDTO == null)
            {
                return PartialView("NotFoundError");
            }
            if(number >= quizDTO.Questions.Count)
            {
                return PartialView("NotFoundError");
            }
            SolvingQuizQuestionViewModel question = new SolvingQuizQuestionViewModel(quizDTO.Questions[number], number, quizId);

            return PartialView("_SolvingQuizQuestionPartial", question);
        }

        /// <summary>
        /// Retrieves data about selected answers and updates session model
        /// </summary>
        /// <param name="model">Selected answers model</param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult SolvingUpdateAnswer([FromBody] SolvingQuizUpdateAnswerViewModel model)
        {
            string quizId = model.QuizId;
            var quiz = quizService.GetQuiz(quizId);
            var question = quiz.Questions.ElementAt(model.QuestionNumber);

            string solveDTOString = HttpContext.Session.GetString("QuizAttempt-" + model.QuizId);
            SolveDTO solveDTO = JsonConvert.DeserializeObject<SolveDTO>(solveDTOString);
            //solveDTO.Answers.Insert(model.QuestionNumber, new UserAnswerDTO(question));
            var userAnswer = solveDTO.Answers.ElementAt(model.QuestionNumber);
            userAnswer.userAnswers = new List<int>();
            foreach (var a in model.SelectedAnswersNumbers)
            {
                userAnswer.userAnswers.Add(a);
            }            
            HttpContext.Session.SetString("QuizAttempt-" + solveDTO.quizID, JsonConvert.SerializeObject(solveDTO));
            return Content(JsonConvert.SerializeObject(model).ToString());
        }

        /// <summary>
        /// Full version of action will require passing result data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Results(string quizId)
        {
            var solvingDTOJson = HttpContext.Session.GetString("QuizAttempt-" + quizId);
            var solvingDTO = JsonConvert.DeserializeObject<SolveDTO>(solvingDTOJson);
            var result = this.quizService.AddResult(solvingDTO);
            HttpContext.Session.Remove("QuizAttempt-" + quizId);
            ViewBag.Result = result;
            ViewBag.QuizId = quizId;
            var title = quizService.GetQuiz(quizId).Title;
            ViewBag.QuizTitle = title;
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


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Services;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Models.QuizViewModels;

namespace Quizzario.Controllers
{
    public class QuizController : Controller
    {
        public int PageSize = 3;
        IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public ViewResult List(int page = 1)
        {
            PagingInfoService pagingInfoService = new PagingInfoService();
            var quizesCollection = quizService.GetAllQuizes();

            QuizListViewModel model = new QuizListViewModel
            {
                Quizes = quizesCollection.
                    OrderBy(q => q.Title).
                    Skip((page - 1) * PageSize).
                    Take(PageSize),
                PagingInfo = pagingInfoService.GetMetaData(quizesCollection.Count(),
                page, PageSize)
            };

            return View(model);
        }
    }
}
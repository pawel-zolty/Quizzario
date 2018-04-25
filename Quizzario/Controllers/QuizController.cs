using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quizzario.Services;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.Models.QuizViewModels;
using Quizzario.Data.DTOs;

namespace Quizzario.Controllers
{
    public class QuizController : Controller
    {
        public int PageSize = 2;
        IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public ViewResult List(int p = 1)
        {

            PagingInfoService pagingInfoService = new PagingInfoService();
            var quizesCollection = quizService.GetAllQuizes();

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

    }

}
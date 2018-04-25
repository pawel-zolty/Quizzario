using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quizzario.Controllers
{
    public class QuizesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
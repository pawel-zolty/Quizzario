using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Models;
using Quizzario.Data.Abstracts;
using Quizzario.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private IQuizDTOFactory factory;

        public QuizService(IQuizDTOFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<QuizDTO> GetAllQuizes()
        {         
            IEnumerable<QuizDTO> quizes = factory.CreateAllQuizes();
            return quizes;
        }

        public IEnumerable<QuizDTO> SearchByName(string name)
        {
            IEnumerable<QuizDTO> quizes = factory.SearchByName(name);
            return quizes;
        }
    }
}

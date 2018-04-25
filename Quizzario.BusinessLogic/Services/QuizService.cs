using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private IQuizDTOFactory factory;

        public QuizService(IQuizDTOFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<QuizDTO> GetAllUserQuizes(string userId)
        {         
            IEnumerable<QuizDTO> quizes = factory.CreateAllUserQuizes(userId);
            return quizes;
        }
    }
}

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

        public IEnumerable<QuizDTO> GetUserFavouriteQuizes(string userId)
        {
            IEnumerable<QuizDTO> quizes = factory.CreateUserFavouriteQuizes(userId);
            return quizes;
        }

        public IEnumerable<QuizDTO> GetAllUserQuizes(string userId)
        {         
            IEnumerable<QuizDTO> quizes = factory.CreateAllUserQuizes(userId);
            return quizes;
        }

        public IEnumerable<QuizDTO> SearchByName(string name)
        {
            IEnumerable<QuizDTO> quizes = factory.SearchByName(name);
            return quizes;
        }
    }
}

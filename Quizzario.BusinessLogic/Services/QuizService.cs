using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private IQuizDTOFactory factory;
        
        public QuizService(IQuizDTOFactory factory)
        {
            this.factory = factory;
        }

        public List<QuizDTO> Quizes => factory.Quizes;

        public List<QuizDTO> GetUserFavouriteQuizes(string userId)
        {
            List<QuizDTO> quizes = factory.CreateUserFavouriteQuizes(userId);
            return quizes;
        }

        public List<QuizDTO> GetAllUserQuizes(string userId)
        {         
            List<QuizDTO> quizes = factory.CreateAllUserQuizes(userId);
            return quizes;
        }

        public void AddQuizToFavourite(string userId, string quizId)
        {
            factory.AddQuizToFavourite(userId, quizId);
        }

        public void RemoveQuizFromFavourite(string userId, string quizId)
        {
            factory.RemoveQuizFromFavourite(userId, quizId);
        }

        public List<QuizDTO> SearchByName(string name)
        {
            List<QuizDTO> quizes = factory.SearchByName(name);
            return quizes;
        }

        public List<QuizDTO> GetAllQuizes()
        {
            List<QuizDTO> quizes = factory.GetAllQuizes();
            return quizes;
        }

        public void SaveQuiz(QuizDTO quiz)
        {
            factory.SaveQuiz(quiz);
        }
    }
}

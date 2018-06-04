using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
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

        public IEnumerable<QuizDTO> Quizes => factory.Quizes;

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

        public void AddQuizToFavourite(string userId, string quizId)
        {
            factory.AddQuizToFavourite(userId, quizId);
        }

        public void RemoveQuizFromFavourite(string userId, string quizId)
        {
            factory.RemoveQuizFromFavourite(userId, quizId);
        }

        public IEnumerable<QuizDTO> SearchByName(string name)
        {
            IEnumerable<QuizDTO> quizes = factory.SearchByName(name);
            return quizes;
        }

        public IEnumerable<QuizDTO> GetAllQuizes()
        {
            IEnumerable<QuizDTO> quizes = factory.GetAllQuizes();
            return quizes;
        }

        public void SaveQuiz(QuizDTO quiz)
        {
            factory.SaveQuiz(quiz);
        }

        public void CreateQuiz(QuizDTO quiz)
        {
            factory.SaveQuiz(quiz);
        }
    }
}

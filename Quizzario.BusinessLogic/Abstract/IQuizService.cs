using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllUserQuizes(string userId);
        IEnumerable<QuizDTO> GetUserFavouriteQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);
        void SaveQuiz(QuizDTO quiz);
        IEnumerable<QuizDTO> Quizes { get; }
        IEnumerable<QuizDTO> SearchByName(string name);
        void CreateQuiz(QuizDTO quizViewModel);
    }
}

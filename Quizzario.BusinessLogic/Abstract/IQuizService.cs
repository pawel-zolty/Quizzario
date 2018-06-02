using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        List<QuizDTO> GetAllUserQuizes(string userId);
        List<QuizDTO> GetUserFavouriteQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);
        void SaveQuiz(QuizDTO quiz);
        List<QuizDTO> Quizes { get; }
        List<QuizDTO> SearchByName(string name);
    }
}

using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllUserQuizes(string userId);
        IEnumerable<QuizDTO> GetUserFavouriteQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);
    }
}

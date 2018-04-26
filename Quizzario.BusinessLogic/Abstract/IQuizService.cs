using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllUserQuizes(string userId);
        IEnumerable<QuizDTO> GetUserFavouriteQuizes(string userId);
        IEnumerable<QuizDTO> SearchByName(string name);
        IEnumerable<QuizDTO> Quizes { get; }
        void SaveQuiz(QuizDTO quiz);
    }
}

using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstracts
{
    public interface IQuizDTOFactory
    {
        QuizDTO Create(string id);
        IEnumerable<QuizDTO> CreateAllUserQuizes(string userId);
        IEnumerable<QuizDTO> CreateUserFavouriteQuizes(string userId);
        IEnumerable<QuizDTO> SearchByName(string name);
        IEnumerable<QuizDTO> GetAllQuizes();
        IEnumerable<QuizDTO> Quizes { get; }
        void SaveQuiz(QuizDTO quiz);
    }
}

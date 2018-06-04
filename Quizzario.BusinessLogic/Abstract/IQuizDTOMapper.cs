using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizDTOMapper
    {
        QuizDTO Create(string id);
        List<QuizDTO> CreateAllUserQuizes(string userId);
        List<QuizDTO> CreateUserFavouriteQuizes(string userId);
        List<QuizDTO> CreateUserAssignedToPrivateQuizes(string userId);
        List<QuizDTO> SearchByName(string name);
        List<QuizDTO> GetAllQuizes();
        List<QuizDTO> Quizes { get; }
        void SaveQuiz(QuizDTO quiz);
    }
}

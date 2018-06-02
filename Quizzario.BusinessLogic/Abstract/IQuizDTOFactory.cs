using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstracts
{
    public interface IQuizDTOFactory
    {
        QuizDTO Create(string id);
        List<QuizDTO> CreateAllUserQuizes(string userId);
        List<QuizDTO> CreateUserFavouriteQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);
        List<QuizDTO> SearchByName(string name);
        List<QuizDTO> GetAllQuizes();
        List<QuizDTO> Quizes { get; }
        void SaveQuiz(QuizDTO quiz);
    }
}

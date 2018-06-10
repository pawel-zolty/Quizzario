using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        List<QuizDTO> Quizes { get; }
        List<QuizDTO> GetAllUserQuizes(string userId);
        List<QuizDTO> GetUserFavouriteQuizes(string userId);
        List<QuizDTO> GetUserAssignedToPrivateQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void AddQuizToPrivateAssigned(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);

        List<ApplicationUserDTO> GetAssignedToPrivateQuizUsers(string id);

        void RemoveQuizFromPrivateAssigned(string userId, string quizId);
        bool IsQuizFavourite(string userId, string quizId);
        void SaveQuiz(QuizDTO quiz);        
        List<QuizDTO> SearchByName(string name);        
        void CreateQuiz(QuizDTO quizViewModel);
        float? GetBestScore(string userId, string quizId);
        float? GetLastScore(string userId, string quizId);
        int GetUserAttemps(string userId, string quizId);
        QuizDTO GetQuiz(string quizId);
        void AddResult(SolveDTO solvingDTO);
    }
}

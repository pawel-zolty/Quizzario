using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizEntityMapper
    {
        Quiz CreateQuizEntity(QuizDTO quiz);
        void SaveQuiz(QuizDTO quiz);
    }
}

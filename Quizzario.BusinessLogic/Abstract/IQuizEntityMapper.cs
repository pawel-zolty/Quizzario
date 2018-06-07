using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizEntityMapper
    {
        //Quiz CreateQuiz(QuizDTO quiz);
        void Update(QuizDTO quiz);
        void AddNewQuiz(QuizDTO quizDTo);
    }
}

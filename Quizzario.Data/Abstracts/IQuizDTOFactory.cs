using Quizzario.Data.DTOs;

namespace Quizzario.Data.Abstracts
{
    public interface IQuizDTOFactory
    {
        QuizDTO Create(string id);
    }
}

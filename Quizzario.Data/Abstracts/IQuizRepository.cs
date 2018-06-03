using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> Quizes { get;}
        Quiz GetById(string id);
        List<Quiz> GetQuiz();
        void Update(Quiz quiz);
    }
}

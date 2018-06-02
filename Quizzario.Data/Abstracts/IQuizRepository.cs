using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IQuizRepository
    {
        List<Quiz> Quizes { get;}
        Quiz GetById(string id);
        List<Quiz> GetQuiz();
        void SaveQuiz(Quiz quiz);
    }
}

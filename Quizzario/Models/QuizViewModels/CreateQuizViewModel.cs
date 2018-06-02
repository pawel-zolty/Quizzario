using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    public class CreateQuizViewModel
    {
        public CreateQuizViewModel()
        {
            Questions = new List<CreateQuestionViewModel>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CreateQuestionViewModel> Questions { get; set; }
    }
    public class CreateQuestionViewModel
    {
        public CreateQuestionViewModel()
        {
            Answers = new List<CreateAnswerViewModel>();
        }
        public string Question { get; set; }
        public List<CreateAnswerViewModel> Answers { get; set; }
    }
    public class CreateAnswerViewModel
    {
        public string Answer { get; set; }
        public bool isCorrect { get; set; }

        public CreateAnswerViewModel()
        {
            isCorrect = false;
        }

    }
}

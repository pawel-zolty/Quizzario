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
            Title = "";
            Description = "";
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
            Question = "";
            NewAnswerRequested = false;
        }
        public string Question { get; set; }
        public List<CreateAnswerViewModel> Answers { get; set; }
        public Boolean NewAnswerRequested { get; set; }
    }
    public class CreateAnswerViewModel
    {
        public string Answer { get; set; }
        public bool isCorrect { get; set; }

        public CreateAnswerViewModel()
        {
            isCorrect = false;
            Answer = "";
        }

    }
}

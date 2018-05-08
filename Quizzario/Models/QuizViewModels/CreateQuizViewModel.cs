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
        public string Question { get; set; }
        public List<String> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuestionNumber { get; set; }
    }
}

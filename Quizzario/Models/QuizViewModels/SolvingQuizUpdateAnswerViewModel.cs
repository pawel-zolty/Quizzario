using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    /// <summary>
    /// This model is send from Solving view to controller.
    /// It contains information about selected answers for a specific question.
    /// </summary>
    public class SolvingQuizUpdateAnswerViewModel
    {
        public SolvingQuizUpdateAnswerViewModel(string quizId)
        {
            QuestionNumber = 0;
            SelectedAnswersNumbers = new List<int>();
        }
        /// <summary>
        /// Question number
        /// </summary>
        public int QuestionNumber { get; set; }
        public string QuizId { get; }
        /// <summary>
        /// Selected answers list
        /// </summary>
        public List<int> SelectedAnswersNumbers { get; set; }
    }
}

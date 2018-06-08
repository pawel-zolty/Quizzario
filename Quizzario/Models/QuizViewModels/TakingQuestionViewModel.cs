using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    /// <summary>
    /// View model for question
    /// </summary>
    public class TakingQuestionViewModel
    {
        public TakingQuestionViewModel()
        {
            Answers = new List<TakingAnswerViewModel>();
            Title = "";
            Question = "";
            Number = 0;
            Multiple = true;
        }

        /// <summary>
        /// Title of a quiz
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Text of a question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Number of a question
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// List of answers
        /// </summary>
        public List<TakingAnswerViewModel> Answers { get; set; }

        /// <summary>
        /// Is question multiple answer or not
        /// </summary>
        public bool Multiple { get; set; }
    }

    /// <summary>
    /// View model for answer
    /// </summary>
    public class TakingAnswerViewModel
    {
        public TakingAnswerViewModel()
        {
            Number = 0;
            Answer = "";
        }

        /// <summary>
        /// Number of an answer
        /// </summary>
        public int Number;

        /// <summary>
        /// Text of an answer
        /// </summary>
        public string Answer;
    }
}

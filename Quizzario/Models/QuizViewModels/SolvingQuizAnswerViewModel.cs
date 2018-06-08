using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    /// <summary>
    /// View model for answer
    /// </summary>
    public class SolvingQuizAnswerViewModel
    {
        public SolvingQuizAnswerViewModel()
        {
            Number = 0;
            Answer = "";
            Selected = false;
        }

        /// <summary>
        /// Number of an answer
        /// </summary>
        public int Number;

        /// <summary>
        /// Text of an answer
        /// </summary>
        public string Answer;

        /// <summary>
        /// Is selected
        /// </summary>
        public bool Selected;
    }
}

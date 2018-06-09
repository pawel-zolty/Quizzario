using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    /// <summary>
    /// View model for question
    /// </summary>
    public class SolvingQuizQuestionViewModel
    {
        public SolvingQuizQuestionViewModel(BusinessLogic.DTOs.QuestionDTO questionDTO, int questionNumber)
        {
            Answers = new List<SolvingQuizAnswerViewModel>();

            this.Question = questionDTO.Question;
            this.Number = questionNumber;
            this.Multiple = questionDTO.Multiple;
            for(int i= 0; i < questionDTO.Answers.Count; i++)
            {
                this.Answers.Add(new SolvingQuizAnswerViewModel(questionDTO.Answers[i], i));
            }
        }

        /// <summary>
        /// Title of a quiz
        /// </summary>
        //public string Title { get; set; }

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
        public List<SolvingQuizAnswerViewModel> Answers { get; set; }

        /// <summary>
        /// Is question multiple answer or not
        /// </summary>
        public bool Multiple { get; set; }
    }
}

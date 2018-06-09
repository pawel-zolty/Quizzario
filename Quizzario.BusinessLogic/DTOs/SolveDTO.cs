using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.DTOs
{
    public class SolveDTO
    {
        public SolveDTO(QuizDTO quizDTO)
        {
            this.quizID = quizDTO.Id;
            this.Time = 0;
            this.Date = DateTime.Now.Date;
            this.Answers = new List<UserAnswerDTO>(quizDTO.Questions.Count);
            foreach(var question in quizDTO.Questions)
            {
                this.Answers.Add(new UserAnswerDTO(question));
            }

        }
        public SolveDTO(QuizDTO quizDTO, int timeSpent) : this(quizDTO)
        {
            this.Time = timeSpent;
        }

        public SolveDTO(string quizId, List<UserAnswerDTO> Answers, int Time, DateTime Date, string UserId)
        {
            this.quizID = quizID;
            this.Answers = Answers;
            this.Time = Time;
            this.Date = Date;
            this.UserId = UserId;
        }

        public SolveDTO()
        {

        }

        public string quizID { get; set; }
        public List<UserAnswerDTO> Answers { get; set; }
        // In seconds
        private int _time;
        public int Time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
                this.Date = DateTime.Now.Date;
            }
        }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }

    public class UserAnswerDTO
    {
        public UserAnswerDTO(QuestionDTO questionDTO)
        {
            this.Question = questionDTO;
            this.isChecked = false;
            this.isCorrect = true;
        }
        public UserAnswerDTO(QuestionDTO Question, Boolean isChecked, Boolean isCorrect)
        {
            this.Question = Question;
            this.isChecked = isChecked;
            this.isCorrect = isCorrect;
        }
        public UserAnswerDTO()
        {

        }
        public QuestionDTO Question {get; set;}
        public Boolean isChecked { get; set; }
        public Boolean isCorrect { get; set; }
    }
}

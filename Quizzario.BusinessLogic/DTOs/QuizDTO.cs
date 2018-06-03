using System;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.DTOs
{
    public enum QuizType
    {
        Quiz, Exam, Test
    }

    public class QuizDTO
    {
        public QuizDTO(SaveQuizDelegate saveQuiz)
        {
            this.SaveQuiz = saveQuiz;
        }
        
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public string QuizSettingsId { get; set; }
        public QuizType? QuizType { get; set; }
        public string FilePath { get; set; }
        public string CreationDate { get; set; }

        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        //public virtual ICollection<AssignedUserDTO> AssignedUsers { get; set; }
        public virtual List<ApplicationUserDTO> FavouritesUsers { get; set; } = new List<ApplicationUserDTO>();
        //public virtual ICollection<ScoreDTO> Scores { get; set; }
        //public virtual QuizSettingsDTO QuizSettings { get; set; }

        public delegate void SaveQuizDelegate(QuizDTO quizDTo);
        private readonly SaveQuizDelegate SaveQuiz;

        public void AddToFavouritesUsers(ApplicationUserDTO user)
        {
            FavouritesUsers.Add(user);
            SaveQuiz(this);
        }
    }
}

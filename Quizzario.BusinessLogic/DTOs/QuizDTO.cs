using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Quizzario.BusinessLogic.DTOs
{
    public enum QuizType
    {
        Quiz, Exam, Test
    }

    public enum QuizAccessLevel
    {
        Public, Private
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
        public QuizAccessLevel? QuizAccessLevel { get; set; }
        public string FilePath { get; set; }
        public string CreationDate { get; set; }
        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();

        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        //public virtual ICollection<AssignedUserDTO> AssignedUsers { get; set; }       //RACZEJ NIE POTRZEBNE - 1 do 1 z encji EF. Nizej sa odpowiendnki biznesowe
        public virtual List<ApplicationUserDTO> FavouritesUsers { get; set; } = new List<ApplicationUserDTO>();
        public virtual List<ApplicationUserDTO> PrivateAssignedUsers { get; set; } = new List<ApplicationUserDTO>();
        //public virtual ICollection<ScoreDTO> Scores { get; set; }
        //public virtual QuizSettingsDTO QuizSettings { get; set; }        

        public delegate void SaveQuizDelegate(QuizDTO quizDTo);
        private readonly SaveQuizDelegate SaveQuiz;
        public void Update() => SaveQuiz(this);//hermetyzacja Delegata

        public string JSON => JsonConvert.SerializeObject(new {
                Questions = this.Questions,
                Title = this.Title,
                Description = this.Description
            }
        );

        public void AddToFavouritesUsers(ApplicationUserDTO user)
        {
            FavouritesUsers.Add(user);
            SaveQuiz(this);
        }

        public void AddToPrivateAssignedUsers(ApplicationUserDTO user)
        {
            PrivateAssignedUsers.Add(user);
            SaveQuiz(this);
        }

        public void RemoveFromFavouritesUsers(ApplicationUserDTO user)
        {
            FavouritesUsers.Remove(user);
            SaveQuiz(this);
        }

        public void RemoveFromPrivateAssignedUsers(ApplicationUserDTO user)
        {
            PrivateAssignedUsers.Remove(user);
            SaveQuiz(this);
        }

        public bool IsFavouritesUsers(ApplicationUserDTO userDTO)
        {
            var isFavourite = FavouritesUsers.
                Where(user => user.Id.Equals(userDTO.Id)).
                Select(u => u.Id).
                FirstOrDefault();
            return isFavourite == null ? false : true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var q = (QuizDTO)obj;
            return (Id.Equals(q.Id));
        }
    }
}

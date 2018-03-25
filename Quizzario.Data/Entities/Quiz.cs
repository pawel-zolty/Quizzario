using Quizzario.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzario.Data.Entities
{

    public enum QuizType
    {
        Quiz, Exam, Test
    }

    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        [ForeignKey("QuizSettings")]
        public int QuizSettingsId { get; set; }
        public QuizType? QuizType { get; set; }
        public string FilePath { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual QuizSettings QuizSettings { get; set; }
    }
}
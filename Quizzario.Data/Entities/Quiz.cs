using System;
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
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [ForeignKey("QuizSettings")]
        public string QuizSettingsId { get; set; }
        public QuizType? QuizType { get; set; }
        public string FilePath { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual QuizSettings QuizSettings { get; set; }
    }
}
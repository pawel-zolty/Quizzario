using Microsoft.EntityFrameworkCore.Infrastructure;
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
        [Key, Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("ApplicationUser"), Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("QuizSettings")]
        public string QuizSettingsId { get; set; }
        [Required]
        public QuizType? QuizType { get; set; }
        public string FilePath { get; set; }
        [Column(TypeName = "Date"), Required]
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
        public virtual QuizSettings QuizSettings { get; set; }
    }
}
using Quizzario.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzario.Data.Entities
{
    public enum AssignType
    {
        Favourite, AssignedToPrivate
    }

    public class AssignedUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        public AssignType? AssignType { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
using Quizzario.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzario.Data.Entities
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public float? Result { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzario.Data.Entities
{
    public class Score
    {
        [Key, Required]
        public string Id { get; set; }
        [ForeignKey("ApplicationUser"), Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("Quiz"), Required]
        public string QuizId { get; set; }
        [Required]
        public float? Result { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
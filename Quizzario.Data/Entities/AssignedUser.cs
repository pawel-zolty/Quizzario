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
        [Key, Required]
        public string Id { get; set; }
        [ForeignKey("Quiz"), Required]
        public string QuizId { get; set; }
        [ForeignKey("ApplicationUser"), Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public AssignType? AssignType { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
namespace Quizzario.Data.DTOs
{
    public enum AssignType
    {
        Favourite, AssignedToPrivate
    }

    public class AssignedUserDTO
    {
        public string Id { get; set; }    
        public string QuizId { get; set; }
        public string ApplicationUserId { get; set; }
        public AssignType? AssignType { get; set; }

        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        public virtual QuizDTO Quiz { get; set; }
    }
}
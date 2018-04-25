namespace Quizzario.BusinessLogic.DTOs
{
    public class ScoreDTO
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string QuizId { get; set; }
        public float? Result { get; set; }

        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        public virtual QuizDTO Quiz { get; set; }
    }
}
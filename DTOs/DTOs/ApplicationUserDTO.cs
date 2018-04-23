using System.Collections.Generic;

namespace Quizzario.Data.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Origin { get; set; }

        public virtual ICollection<AssignedUserDTO> AssignedUsers { get; set; }
        public virtual ICollection<QuizDTO> Quizes { get; set; }
        public virtual ICollection<ScoreDTO> Scores { get; set; }
    }
}
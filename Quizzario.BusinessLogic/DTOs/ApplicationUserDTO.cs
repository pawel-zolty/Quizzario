using System.Collections.Generic;

namespace Quizzario.BusinessLogic.DTOs
{
    public class ApplicationUserDTO
    {
        public ApplicationUserDTO(string userName)
        {
            this.UserName = userName;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Origin { get; set; }
        public readonly string UserName;

        //public virtual ICollection<AssignedUserDTO> AssignedUsers { get; set; }
        public virtual ICollection<QuizDTO> Quizes { get; set; }
        public virtual ICollection<ScoreDTO> Scores { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var appuser = (ApplicationUserDTO)obj;
            return (Id.Equals(appuser.Id));
        }
    }
}
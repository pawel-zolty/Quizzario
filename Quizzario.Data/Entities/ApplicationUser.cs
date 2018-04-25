using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Quizzario.Data.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }     
        public string Origin { get; set; }

        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}

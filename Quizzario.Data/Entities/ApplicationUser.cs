using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Quizzario.Data.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }     
        public string Origin { get; set; }

        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}

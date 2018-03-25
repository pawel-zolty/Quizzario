using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Quizzario.Data.Entities;

namespace Quizzario.Infrastructure
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }     
        public string Origin { get; set; }

        public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}

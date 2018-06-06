using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data.Repositories
{
    public class EFApplicationUserRepository : IApplicationUserRepository
    {
        private ApplicationDbContext context;

        public EFApplicationUserRepository(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }

        public List<ApplicationUser> Users
        {
            get { return context.Users.ToList(); }
        }

        public ApplicationUser GetById(string id)
        {
            return context.Users.ToList<ApplicationUser>().
                Where(u => u.Id.Equals(id)).
                FirstOrDefault();
        }

        public ApplicationUser GetByName(string userName)
        {
            return context.Users.ToList<ApplicationUser>().
                Where(u => u.UserName.Equals(userName)).
                FirstOrDefault();
        }
    }
}

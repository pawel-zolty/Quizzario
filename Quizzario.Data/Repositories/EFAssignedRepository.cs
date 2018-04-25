using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data.Repositories
{
    public class EFAssignedRepository : IAssignedRepository
    {
        private ApplicationDbContext context;

        public EFAssignedRepository(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }
        
        public IEnumerable<AssignedUser> GetUserAssigns(string userId)
        {
            return context.AssignedUsers.ToList<AssignedUser>().
                Where(q => q.ApplicationUserId.Equals(userId));
        }
    }
}

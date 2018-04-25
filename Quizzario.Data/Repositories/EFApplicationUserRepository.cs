﻿using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data.Repositories
{
    public class EFApplicationUserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext context;

        public EFApplicationUserRepository(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }

        public IEnumerable<ApplicationUser> All
        {
            get { return context.Users; }
        }

        public ApplicationUser GetById(string id)
        {
            return context.Users.ToList<ApplicationUser>().
                Where(u => u.Id.Equals(id)).
                FirstOrDefault();
        }
    }
}

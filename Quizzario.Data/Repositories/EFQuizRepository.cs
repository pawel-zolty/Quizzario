using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data.Repositories
{
    public class EFQuizRepository : IRepository<Quiz>
    {
        private ApplicationDbContext context;

        public EFQuizRepository(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }

        public IEnumerable<Quiz> All
        {
            get { return context.Quizes; }
        }

        public Quiz GetById(string id)
        {
            return context.Quizes.ToList<Quiz>().
                Where(q => q.Id.Equals(id)).
                FirstOrDefault();
        }
    }
}

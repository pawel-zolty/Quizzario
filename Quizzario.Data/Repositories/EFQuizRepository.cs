﻿using Microsoft.EntityFrameworkCore;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data.Repositories
{
    public class EFQuizRepository : IQuizRepository
    {
        private ApplicationDbContext context;

        public EFQuizRepository(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }

        public IEnumerable<Quiz> Quizes
        {
            get { return context.Quizes; }
        }

        public Quiz GetById(string id)
        {
            return context.Quizes.ToList<Quiz>().
                Where(q => q.Id.Equals(id)).
                FirstOrDefault();
        }
        public Quiz GetByType(QuizType quizType)
        {
            return context.Quizes.ToList<Quiz>().
                Where(q => q.QuizType.Equals(quizType)).
                FirstOrDefault();
        }
        public Quiz GetByTitle(string title)
        {
            return context.Quizes.ToList<Quiz>().
               Where(q => q.Title.Equals(title)).
               FirstOrDefault();
        }

    }
}

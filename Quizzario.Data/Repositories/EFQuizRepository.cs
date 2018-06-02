using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Quiz> GetQuiz()
        {
            throw new System.NotImplementedException();
        }

        public void SaveQuiz(Quiz quiz)
        {
            if (quiz.ApplicationUserId != null)
            {
                context.Quizes.Add(quiz);
            }else
            {

                Quiz dbEntry;
                    dbEntry = new Quiz();
                    dbEntry.Title = quiz.Title;
                    dbEntry.QuizSettings = quiz.QuizSettings;
                    dbEntry.QuizSettingsId = quiz.QuizSettingsId;
                    dbEntry.QuizType = quiz.QuizType;
                    dbEntry.Scores = quiz.Scores;
                    dbEntry.ApplicationUser = quiz.ApplicationUser;
                    dbEntry.ApplicationUserId = quiz.ApplicationUserId;
                    dbEntry.AssignedUsers = quiz.AssignedUsers;
                    dbEntry.CreationDate = quiz.CreationDate;
                    dbEntry.Description = quiz.Description;
                    context.Quizes.Add(dbEntry);

                
            }
            context.SaveChanges();
        }
    }
}

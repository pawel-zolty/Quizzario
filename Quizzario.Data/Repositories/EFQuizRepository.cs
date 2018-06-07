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
        private string directoryPath = "C://quizzario//";
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

        //public Quiz GetByType(QuizType quizType)
        //{
        //    return context.Quizes.ToList<Quiz>().
        //        Where(q => q.QuizType.Equals(quizType)).
        //        FirstOrDefault();
        //}

        //public Quiz GetByTitle(string title)
        //{
        //    return context.Quizes.ToList<Quiz>().
        //       Where(q => q.Title.Equals(title)).
        //       FirstOrDefault();
        //}

        //public List<Quiz> GetQuiz()
        //{
        //    throw new System.NotImplementedException();
        //}

        public void Add(Quiz quiz)
        {
            if (quiz == null)
                return;
            context.Quizes.Add(quiz);
            context.SaveChanges();
        }

        public void Update(Quiz quiz)
        {
            if (quiz.ApplicationUserId != null)
            {
                context.Quizes.Update(quiz);
                context.SaveChanges();
            }
            else
            {
                Quiz dbEntry;
                dbEntry = new Quiz
                {
                    Title = quiz.Title,
                    QuizSettings = quiz.QuizSettings,
                    QuizSettingsId = quiz.QuizSettingsId,
                    QuizType = quiz.QuizType,
                    Scores = quiz.Scores,
                    ApplicationUser = quiz.ApplicationUser,
                    ApplicationUserId = quiz.ApplicationUserId,
                    AssignedUsers = quiz.AssignedUsers,
                    CreationDate = quiz.CreationDate,
                    Description = quiz.Description
                };
                context.Quizes.Add(dbEntry);
            }
        }
    }
}

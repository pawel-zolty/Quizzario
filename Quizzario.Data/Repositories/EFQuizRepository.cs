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
        //private IAssignedRepository assignedRepository;

        public EFQuizRepository(DbContextOptions<ApplicationDbContext> options)
           // IAssignedRepository assignedRepo)
        {
            context = new ApplicationDbContext(options);
            //this.assignedRepository = assignedRepo;
        }

        public IEnumerable<Quiz> Quizes
        {
            get
            {
                var qs = context.Quizes;
                foreach (var q in qs)
                {
                    //q.AssignedUsers = new List<AssignedUser>();
                    //var assings = assignedRepository.GetAssingsByQuizId(q.Id);
                    //foreach(var a in assings)
                    //{
                    //    q.AssignedUsers.Add(a);
                    //}
                }
                return qs;
            }
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

        public List<Quiz> GetQuiz()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Quiz quiz)
        {
            if (quiz.ApplicationUserId != null)
            {
                context.Quizes.Update(quiz);
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
            context.SaveChanges();
        }
    }
}

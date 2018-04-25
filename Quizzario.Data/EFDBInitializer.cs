using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.Data
{
    public class EFDBInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new List<ApplicationUser>
            {
            new ApplicationUser{Id = "1", FirstName = "Paweł", LastName = "Kowal", Email="zolty13@vp.pl", PasswordHash="12345"},
            new ApplicationUser{Id = "2", FirstName = "Michał", LastName = "Nowak", }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var quizes = new List<Quiz>
            {
            new Quiz{Id = "1", Title = "User's 1 Quiz", ApplicationUserId = "1", FilePath = "C://q1" },
            new Quiz{Id = "2", Title = "User's 2 Quiz", ApplicationUserId = "2", FilePath = "C://q2", QuizType = QuizType.Quiz}
            };
            quizes.ForEach(q => context.Quizes.Add(q));
            context.SaveChanges();

            var assignedUser = new List<AssignedUser>
            {
            new AssignedUser{QuizId = "1", ApplicationUserId = "2", AssignType = AssignType.Favourite},
            new AssignedUser{QuizId = "1", ApplicationUserId = "2", },
            };
            assignedUser.ForEach(au => context.AssignedUsers.Add(au));
            context.SaveChanges();

            var scores = new List<Score>
            {
            new Score{QuizId = "1", ApplicationUserId = "1", Result = 10},
            new Score{QuizId = "1", ApplicationUserId = "2", Result = 5}
            };
            scores.ForEach(au => context.Scores.Add(au));
            context.SaveChanges();

            var settings = new List<QuizSettings>
            {
            new QuizSettings{QuizId = "1", AttemptLimit = 3}
            };
            settings.ForEach(s => context.QuizSettingss.Add(s));
            context.SaveChanges();
        }
    }
}

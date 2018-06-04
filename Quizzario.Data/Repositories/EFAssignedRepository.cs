//using Microsoft.EntityFrameworkCore;
//using Quizzario.Data.Abstracts;
//using Quizzario.Data.Entities;
//using System.Collections.Generic;
//using System.Linq;

//namespace Quizzario.Data.Repositories
//{
//    public class EFAssignedRepository : IAssignedRepository
//    {
//        private ApplicationDbContext context;

//        public EFAssignedRepository(DbContextOptions<ApplicationDbContext> options)
//        {
//            context = new ApplicationDbContext(options);
//        }
        
//        public List<AssignedUser> GetUserAssigns(string userId)
//        {
//            return context.AssignedUsers.
//                Where(q => q.ApplicationUserId.Equals(userId)).
//                ToList<AssignedUser>();
//        }

//        public void AddFavouriteAssign(string userId, string quizId)
//        {
//            var assign = new AssignedUser
//            {
//                AssignType = AssignType.Favourite,
//                QuizId = quizId,
//                ApplicationUserId = userId
//            };
//            context.AssignedUsers.Add(assign);
//            context.SaveChanges();
//        }

//        public void RemoveFavouriteAssign(string userId, string quizId)
//        {
//            var assign = context.AssignedUsers.SingleOrDefault(
//                a => a.ApplicationUserId.Equals(userId) && a.QuizId.Equals(quizId)); 
//            if (assign != null)
//            {
//                context.AssignedUsers.Remove(assign);
//                context.SaveChanges();
//            }
//        }

//        public List<AssignedUser> GetAssingsByQuizId(string id)
//        {
//            return context.AssignedUsers.Where(a => a.QuizId.Equals(id)).ToList();
//        }
//    }
//}

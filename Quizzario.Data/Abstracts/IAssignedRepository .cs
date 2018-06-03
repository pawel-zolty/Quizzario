using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IAssignedRepository
    {
        List<AssignedUser> GetUserAssigns(string userId);
        void AddFavouriteAssign(string userId, string quizId);
        void RemoveFavouriteAssign(string userId, string quizId);
        List<AssignedUser> GetAssingsByQuizId(string id);
    }
}

using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IAssignedRepository
    {
        IEnumerable<AssignedUser> GetUserAssigns(string userId);
    }
}

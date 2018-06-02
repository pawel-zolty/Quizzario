using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> Users { get; }
        ApplicationUser GetById(string id);
    }
}

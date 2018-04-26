using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstracts
{
    public interface IApplicationUserDTOFactory
    {
        ApplicationUserDTO CreateUserWithId(string id);
    }
}

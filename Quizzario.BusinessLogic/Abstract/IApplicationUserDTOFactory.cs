using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IApplicationUserDTOFactory
    {
        ApplicationUserDTO CreateUserWithId(string id);
    }
}

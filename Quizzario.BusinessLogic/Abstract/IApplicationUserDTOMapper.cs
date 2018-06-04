using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IApplicationUserDTOMapper
    {
        ApplicationUserDTO CreateUserWithId(string id);
    }
}

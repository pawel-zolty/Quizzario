using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IApplicationUserEntityMapper
    {
        void CreateUserEntity(ApplicationUserDTO user);
        void SaveUser(ApplicationUserDTO user);
    }
}

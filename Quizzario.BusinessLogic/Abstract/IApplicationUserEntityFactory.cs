using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IApplicationUserEntityFactory
    {
        void CreateUserEntity(ApplicationUserDTO user);
        void SaveUser(ApplicationUserDTO user);
    }
}

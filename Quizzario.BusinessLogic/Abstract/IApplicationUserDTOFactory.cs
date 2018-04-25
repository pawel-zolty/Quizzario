using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.BusinessLogic.Abstracts
{
    public interface IApplicationUserDTOFactory
    {
        ApplicationUserDTO Create(string id);
    }
}

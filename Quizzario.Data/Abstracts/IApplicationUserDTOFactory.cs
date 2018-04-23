using Quizzario.Data.DTOs;

namespace Quizzario.Data.Abstracts
{
    public interface IApplicationUserDTOFactory
    {
        ApplicationUserDTO Create(string id);
    }
}

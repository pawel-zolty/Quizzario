using Quizzario.BusinessLogic.Abstract;
using Quizzario.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IApplicationUserDTOMapper applicationUserDTOMapper;

        public UserService(IApplicationUserDTOMapper applicationUserDTOMapper)
        {
            this.applicationUserDTOMapper = applicationUserDTOMapper;
        }

        public string GetUserIdByName(string userName)
        {
            var user = applicationUserDTOMapper.GetByName(userName);
            return user.Id;
        }
    }
}

using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.Factories
{
    public class ApplicationUserEntityFactory : IApplicationUserEntityFactory
    {
        public void CreateUserEntity(ApplicationUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser();
            var propInfo = userDTO.GetType().GetProperties();
            foreach (var item in propInfo)
            {
                user.GetType().GetProperty(item.Name).SetValue(user, item.GetValue(userDTO, null), null);
            }
        }

        public void SaveUser(ApplicationUserDTO user)
        {

        }
    }
}

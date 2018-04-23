using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizzario.Data.Abstracts;
using Quizzario.Data.DTOs;
using Quizzario.Data.Entities;

namespace Quizzario.Data.Factories
{
    public class ApplicationUserDTOFactory : IApplicationUserDTOFactory
    {
        private IRepository<ApplicationUser> repository;

        public ApplicationUserDTO Create(string id)
        {
            ApplicationUser user = repository.GetById(id);
            return CreateUser(user);
        }

        private ApplicationUserDTO CreateUser(ApplicationUser user)
        {            
            string id = user.Id;
            string firstName = user.FirstName;
            string lastName = user.LastName;
            string origin = user.Origin;

            if (firstName == null || lastName == null)
                return null;
            ApplicationUserDTO userDTO = new ApplicationUserDTO
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Origin = origin,
                //AssignedUsers,
                //Quizes,
                //Scores
                //TO DO 
            };
            return userDTO;
        }
    }
}

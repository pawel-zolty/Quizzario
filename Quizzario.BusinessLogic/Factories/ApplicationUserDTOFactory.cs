using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;

namespace Quizzario.BusinessLogic.Factories
{
    public class ApplicationUserDTOFactory : IApplicationUserDTOFactory
    {
        private IApplicationUserRepository repository;

        public ApplicationUserDTOFactory(IApplicationUserRepository repository)
        {
            this.repository = repository;
        }

        public ApplicationUserDTO getUser(int userId)
        {
            return this.CreateUser(this.repository.GetById(userId.ToString()));
        }

        public ApplicationUserDTO CreateUserWithId(string id)
        {
            ApplicationUser user = repository.GetById(id);
            return CreateUser(user);
        }

        private ApplicationUserDTO CreateUser(ApplicationUser user)
        {
            if (user == null)
                return null;
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
